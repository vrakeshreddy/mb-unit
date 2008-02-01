// Copyright 2008 MbUnit Project - http://www.mbunit.com/
// Portions Copyright 2000-2004 Jonathan De Halleux, Jamie Cansdale
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

namespace Gallio.Utilities
{
    /// <summary>
    /// Provides helper functions for manipulating <see cref="Exception" />s.
    /// </summary>
    public static class ExceptionUtils
    {
        /// <summary>
        /// <para>
        /// Safely converts an exception to a string.
        /// </para>
        /// <para>
        /// This method protects the caller from unexpected failures that may occur while
        /// reporting an exception of untrusted origin.  If an error occurs while converting the
        /// exception to a string, this method returns a generic description of the exception
        /// that can be used instead of the real thing.
        /// </para>
        /// <para>
        /// It can happen that converting an exception to a string produces a secondary exception
        /// because the <see cref="Exception.ToString" /> method has been overridden or because the
        /// stack frames associated with the exception are malformed.  For example, we observed
        /// one case of a <see cref="TypeLoadException" /> being thrown while printing an exception
        /// because one of the stack frames referred to a dynamic method with incorrect metadata.
        /// </para>
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns>The string contents</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="ex"/> is null</exception>
        /// <seealso cref="Exception.ToString"/>
        public static string SafeToString(Exception ex)
        {
            if (ex == null)
                throw new ArgumentNullException("ex");

            try
            {
                return ex.ToString();
            }
            catch (Exception ex2)
            {
                return String.Format("An exception of type '{0}' occurred.  Details are not available because a second exception of type '{1}' occurred in Exception.ToString().",
                    ex.GetType().FullName, ex2.GetType().FullName);
            }
        }
    }
}
