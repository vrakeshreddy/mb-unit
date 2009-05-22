// Copyright 2005-2009 Gallio Project - http://www.gallio.org/
// Portions Copyright 2000-2004 Jonathan de Halleux
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Gallio.Common.IO;
using Gallio.Common.Policies;
using Gallio.Common.Reflection;
using Gallio.Runtime.Hosting;

namespace Gallio.MSTestAdapter.Wrapper
{
    /// <summary>
    /// Runs MSTest in-process in separate AppDomain (without process isolation)
    /// to enable debugging.
    /// </summary>
    internal sealed class DebugMSTestCommand : MSTestCommand
    {
        private DebugMSTestCommand()
        {
        }

        public static readonly DebugMSTestCommand Instance = new DebugMSTestCommand();

        public override int Run(string executablePath, string workingDirectory,
            MSTestCommandArguments args, TextWriter writer)
        {
            string baseDir = Path.GetDirectoryName(executablePath);

            using (new CurrentDirectorySwitcher(workingDirectory))
            {
                AppDomain appDomain = null;
                try
                {
                    appDomain = AppDomainUtils.CreateAppDomain("MSTest", baseDir, executablePath + @".config", false);

                    var extendedArgs = args.Copy();
                    extendedArgs.NoIsolation = true;

                    Type launcherType = typeof(Launcher);
                    Launcher launcher = (Launcher) appDomain.CreateInstanceFromAndUnwrap(
                        AssemblyUtils.GetFriendlyAssemblyLocation(launcherType.Assembly),
                        launcherType.FullName);
                    return launcher.Run(writer, executablePath, extendedArgs.ToStringArray());
                }
                finally
                {
                    if (appDomain != null)
                        AppDomain.Unload(appDomain);
                }
            }
        }

        // MSTest uses Console.OpenStandardOutput() to get a reference to the
        // real standard output stream.  This makes it difficult to redirect output
        // elsewhere during debugging.  Annoyingly it also causes a console
        // window to appear and it may cause other output to become garbled.
        //
        // There isn't much we can do about this without messing around with Win32
        // file handles.
        [Serializable]
        private sealed class Launcher : MarshalByRefObject
        {
            private const int StdOut = -11;
            
            public int Run(TextWriter outputWriter, string executable, string[] args)
            {
                string outputTempFile = null;
                FileStream outputStream = null;
                try
                {
                    outputTempFile = SpecialPathPolicy.For<DebugMSTestCommand>().CreateTempFileWithUniqueName().FullName;
                    outputStream = new FileStream(outputTempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Delete);

                    bool wasRedirected = false;
                    IntPtr oldHandle = GetStdHandle(StdOut);
                    try
                    {
                        wasRedirected = SetStdHandle(StdOut, outputStream.SafeFileHandle.DangerousGetHandle());

                        if (!wasRedirected)
                            outputWriter.WriteLine("MSTest output not available because the output stream could not be redirected.");

                        // Unfortunately we cannot use AppDomain.ExecuteAssembly because it has
                        // the nasty side-effect of causing the redirected console streams to
                        // be reset to defaults.  So instead we simply call the entrypoint directly.
                        Assembly assembly = Assembly.LoadFrom(executable);
                        return (int)assembly.EntryPoint.Invoke(null, new object[] { args });
                    }
                    finally
                    {
                        if (wasRedirected)
                            SetStdHandle(StdOut, oldHandle);
                    }
                }
                finally
                {
                    if (outputStream != null)
                    {
                        outputStream.Position = 0;

                        char[] buffer = new char[4096];
                        using (StreamReader outputReader = new StreamReader(outputStream))
                        {
                            int count;
                            while ((count = outputReader.Read(buffer, 0, buffer.Length)) > 0)
                                outputWriter.Write(buffer, 0, count);
                        }

                        outputStream.Close();
                    }

                    if (outputTempFile != null)
                        File.Delete(outputTempFile);
                }
            }

            public override object InitializeLifetimeService()
            {
                return null;
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern bool SetStdHandle(int nStdHandle, IntPtr hHandle);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr GetStdHandle(int nStdHandle);
        }
    }
}