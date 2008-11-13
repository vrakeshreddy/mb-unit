// Copyright 2005-2008 Gallio Project - http://www.gallio.org/
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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Gallio.Navigator
{
    /// <summary>
    /// Provides access to Gallio services.
    /// </summary>
    [ComVisible(true)]
    [Guid("B7F075D8-56EC-49f7-8692-89BEECBD7A0F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IGallioNavigator
    {
        /// <summary>
        /// Opens up an editor for the specified path and line number.
        /// </summary>
        /// <remarks>
        /// Lines and columns are numbered starting from 1.
        /// Zero indicates an unspecified value.
        /// </remarks>
        /// <param name="path">The path</param>
        /// <param name="lineNumber">The line number, or 0 if unspecified</param>
        /// <param name="columnNumber">The column number, or 0 if unspecified</param>
        /// <returns>True if the navigation succeeded</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="path"/> is invalid, or if
        /// <paramref name="lineNumber"/> or <paramref name="columnNumber"/> is less than 0</exception>
        bool NavigateTo(string path, int lineNumber, int columnNumber);
    }
}
