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
using System.Windows.Forms;
using Gallio.Common.Collections;
using Gallio.Copy.Properties;
using Gallio.Runtime;
using Gallio.Runtime.ConsoleSupport;
using Gallio.Runtime.Logging;

namespace Gallio.Copy
{
    internal class CopyProgram : ConsoleProgram<CopyArguments>
    {
        /// <summary>
        /// Creates an instance of the program.
        /// </summary>
        private CopyProgram()
        {
            ApplicationName = Resources.ApplicationName;
        }

        /// <inheritdoc />
        protected override int RunImpl(string[] args)
        {
            if (!ParseArguments(args))
            {
                ShowHelp();
                return 1;
            }

            if (Arguments.Help)
            {
                ShowHelp();
                return 0;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var runtimeSetup = new RuntimeSetup();
            GenericCollectionUtils.ForEach(Arguments.PluginDirectories, runtimeSetup.AddPluginDirectory);

            var logger = new FilteredLogger(new RichConsoleLogger(Console), Verbosity.Normal);
            using (RuntimeBootstrap.Initialize(runtimeSetup, logger))
            {
                Application.Run(new CopyForm());
            }

            return 0;
        }

        protected override void ShowHelp()
        {
            ArgumentParser.ShowUsageInMessageBox(ApplicationTitle);
        }

        [STAThread]
        internal static int Main(string[] args)
        {
            return new CopyProgram().Run(NativeConsole.Instance, args);
        }
    }
}