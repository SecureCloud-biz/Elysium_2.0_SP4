using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Windows.Threading;

namespace Elysium.Platform.Communication
{
    internal static class Helper
    {
        private const string MutexName = "elysium";
        private static Mutex _mutex;

        private const string PipeStreamName = "Elysium Platform Communication Stream";

        public static bool IsSingleInstance()
        {
            bool isSingleInstance;
            _mutex = new Mutex(false, MutexName, out isSingleInstance);
            return isSingleInstance;
        }

        public static void ExecuteServer()
        {
            var thread = new Thread(ExecuteServerAction);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private static void ExecuteServerAction()
        {
            var abort = new Action(() => System.Windows.Application.Current.Dispatcher.Invoke(() => System.Windows.Application.Current.Shutdown(0xE6)));
            try
            {
                using (var pipeServer = new NamedPipeServerStream(PipeStreamName, PipeDirection.InOut, 1))
                {
                    // Dot not use using statement because after disposing child stream parent stream disposing too
                    var reader = new StreamReader(pipeServer);
                    var writer = new StreamWriter(pipeServer);
                    while (true)
                    {
                        pipeServer.WaitForConnection();

                        var isRegistered = false;
                        try
                        {
                            var isApplication = string.Equals(reader.ReadLine(), true.ToString());
                            var path = reader.ReadLine();
                            var type = reader.ReadLine();
                            System.Windows.Application.Current.Dispatcher.Invoke(
                                new Action(() =>
                                           isRegistered =
                                           !isApplication ? GadgetHelper.Register(path, type) : ApplicationHelper.Register(path, type)));
                        }
                        finally
                        {
                            writer.WriteLine(isRegistered);
                            writer.Flush();
                        }

                        pipeServer.Disconnect();
                    }
                }
            }
            catch
            {
                abort();
            }
        }

        public static void ExecuteClient(string isApplication, string path, string type)
        {
            ExecuteClientAction(isApplication, path, type);
        }

        private static void ExecuteClientAction(string isApplication, string path, string type)
        {
            var abort = new Action(() => System.Windows.Application.Current.Dispatcher.Invoke(() => System.Windows.Application.Current.Shutdown(0xE6)));
            try
            {
                using (var pipeClient = new NamedPipeClientStream(".", PipeStreamName, PipeDirection.InOut))
                {
                    pipeClient.Connect();

                    // Dot not use using statement because after disposing child stream parent stream disposing too
                    var writer = new StreamWriter(pipeClient);
                    try
                    {
                        writer.WriteLine(bool.Parse(isApplication));
                        writer.WriteLine(path);
                        writer.WriteLine(type);
                        writer.Flush();
                    }
                    catch
                    {
                        abort();
                    }

                    pipeClient.WaitForPipeDrain();

                    // Dot not use using statement because after disposing child stream parent stream disposing too
                    var reader = new StreamReader(pipeClient);
                    try
                    {
                        var result = reader.ReadLine();
                        if (!string.Equals(result, true.ToString()))
                            abort();
                    }
                    catch
                    {
                        abort();
                    }
                }
            }
            catch
            {
                abort();
            }
        }
    }
} ;