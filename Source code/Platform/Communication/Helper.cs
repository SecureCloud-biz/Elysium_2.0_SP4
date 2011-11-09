using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Windows.Threading;

namespace Elysium.Platform.Communication
{
    internal static class Helper
    {
        private const string MutexName = "Elysium Platform Mutex";
        private const string PipeStreamName = "Elysium Platform Communication Stream";

        internal static bool IsSingleInstance()
        {
            bool result;
            var mutex = new Mutex(true, MutexName, out result);
            mutex.Dispose();
            return result;
        }

        internal static void ExecuteServer()
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
                    pipeServer.WaitForConnection();
                    var isRegistered = false;
                    try
                    {
                        using (var reader = new StreamReader(pipeServer))
                        {
                            var isApplication = string.Equals(reader.ReadLine(), true.ToString());
                            var path = reader.ReadLine();
                            var type = reader.ReadLine();
                            isRegistered = !isApplication ? GadgetHelper.Register(path, type) : ApplicationHelper.Register(path, type);
                        }
                    }
                    finally
                    {
                        using (var writer = new StreamWriter(pipeServer))
                        {
                            writer.WriteLine(isRegistered);
                            writer.Flush();
                        }
                    }
                }
            }
            catch
            {
                abort();
            }
        }

        internal static void ExecuteClient(string isApplication, string path, string type)
        {
            var thread = new Thread(() => ExecuteClientAction(isApplication, path, type));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private static void ExecuteClientAction(string isApplication, string path, string type)
        {
            var abort = new Action(() => System.Windows.Application.Current.Dispatcher.Invoke(() => System.Windows.Application.Current.Shutdown(0xE6)));
            try
            {
                using (var pipeClient = new NamedPipeClientStream(".", PipeStreamName, PipeDirection.InOut))
                {
                    pipeClient.Connect();
                    try
                    {
                        using (var writer = new StreamWriter(pipeClient))
                        {
                            writer.WriteLine(bool.Parse(isApplication));
                            writer.WriteLine(path);
                            writer.WriteLine(type);
                        }
                    }
                    catch
                    {
                        abort();
                    }
                    pipeClient.WaitForPipeDrain();
                    try
                    {
                        using (var reader = new StreamReader(pipeClient))
                        {
                            if (!string.Equals(reader.ReadLine(), true.ToString()))
                                abort();
                        }
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