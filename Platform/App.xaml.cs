using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Elysium.Platform
{
    public sealed partial class App
    {
        public App()
        {
#if (DEBUG == false)
            if (Environment.Is64BitOperatingSystem)
                if (!Environment.Is64BitProcess)
                {
                    var location = Assembly.GetExecutingAssembly().Location;
                    var console = Process.Start(new ProcessStartInfo(@"C:\Windows\System32\cmd.exe")
                                                    {
                                                        CreateNoWindow = true,
                                                        WindowStyle = ProcessWindowStyle.Hidden,
                                                        UseShellExecute = false,
                                                        RedirectStandardInput = true,
                                                        RedirectStandardOutput = true,
                                                        RedirectStandardError = true
                                                    });
                    console.StandardInput.WriteLine("cd " + Path.GetDirectoryName(location));
                    console.StandardInput.WriteLine("start" + Path.GetFileName(location));
                    console.CloseMainWindow();
                    console.Close();
                }
#endif
        }
    }
} ;