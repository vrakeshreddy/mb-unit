﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using MbUnit.Core.Utilities;

namespace MbUnit.Core.Runtime
{
    /// <summary>
    /// Resolves assemblies using hint paths and custom resolvers.
    /// </summary>
    /// <remarks>
    /// Note that this implementation does not depend on any particular test framework
    /// or on components that cannot be found in the main MbUnit assembly.  If it
    /// did, we could have interesting bootstrapping problems with the assembly resolver
    /// if the other required components used assemblies not located in the application
    /// base directory.
    /// </remarks>
    public class DefaultAssemblyResolverManager : LongLivingMarshalByRefObject, IAssemblyResolverManager
    {
        private List<string> hintDirectories = null;

        /// <summary>
        /// Initializes the assembly resolver manager.
        /// </summary>
        public DefaultAssemblyResolverManager()
        {
            hintDirectories = new List<string>();

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            hintDirectories = null;
        }

        /// <inheritdoc />
        public event ResolveEventHandler AssemblyResolve;

        /// <inheritdoc />
        public void AddHintDirectory(string hintDirectory)
        {
            if (hintDirectory == null)
                throw new ArgumentNullException("hintDirectory");

            hintDirectory = Path.GetFullPath(hintDirectory);

            if (!hintDirectories.Contains(hintDirectory))
                hintDirectories.Add(hintDirectory);
        }

        /// <inheritdoc />
        public void AddHintDirectoryContainingFile(string file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            string directory = Path.GetDirectoryName(file);
            if (directory == null || directory.Length == 0)
                directory = ".";

            AddHintDirectory(directory);
        }

        /// <inheritdoc />
        public void AddMbUnitDirectories()
        {
            AddHintDirectory(typeof(DefaultAssemblyResolverManager).Assembly.Location);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.EndsWith("XmlSerializers"))
                return null;

            // Try the custom handler chain
            ResolveEventHandler multiHandler = AssemblyResolve;
            if (multiHandler != null)
            {
                foreach (ResolveEventHandler handler in multiHandler.GetInvocationList())
                {
                    Assembly assembly = handler(sender, args);
                    if (assembly != null)
                        return assembly;
                }
            }

            // Try hint directories
            return RecursiveAssemblyResolve(args);
        }

        private Assembly RecursiveAssemblyResolve(ResolveEventArgs args)
        {
            String[] splitName = args.Name.Split(',');
            String displayName = splitName[0];
            Assembly assembly;

            // Try with current directory
            assembly = ResolveAssembly(Directory.GetCurrentDirectory(), displayName);
            if (assembly != null)
                return assembly;

            // Try with hint directories
            foreach (String directory in hintDirectories)
            {
                assembly = ResolveAssembly(directory, displayName);
                if (assembly != null)
                    return assembly;
            }

            return null;
        }

        private static Assembly ResolveAssembly(string directory, string file)
        {
            string assemblyName = Path.GetFullPath(Path.Combine(directory, file));

            if (File.Exists(assemblyName))
                return Assembly.LoadFrom(assemblyName);

            if (File.Exists(assemblyName + ".dll"))
                return Assembly.LoadFrom(assemblyName + ".dll");

            if (File.Exists(assemblyName + ".exe"))
                return Assembly.LoadFrom(assemblyName + ".exe");

            return null;
        }
    }
}