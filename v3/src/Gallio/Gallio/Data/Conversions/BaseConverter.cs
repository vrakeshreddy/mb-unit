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

namespace Gallio.Data.Conversions
{
    /// <summary>
    /// An abstract base class implementation of <see cref="IConverter" />
    /// that mostly validates arguments and provides a default implement for
    /// <see cref="CanConvert" /> based on <see cref="GetConversionCost"/>.
    /// </summary>
    public abstract class BaseConverter : IConverter
    {
        /// <inheritdoc />
        public bool CanConvert(Type sourceType, Type targetType)
        {
            if (sourceType == null)
                throw new ArgumentNullException("sourceType");
            if (targetType == null)
                throw new ArgumentNullException("targetType");

            return CanConvertInternal(sourceType, targetType);
        }

        /// <inheritdoc />
        public ConversionCost GetConversionCost(Type sourceType, Type targetType)
        {
            if (sourceType == null)
                throw new ArgumentNullException("sourceType");
            if (targetType == null)
                throw new ArgumentNullException("targetType");

            return GetConversionCostInternal(sourceType, targetType);
        }

        /// <inheritdoc />
        public object Convert(object sourceValue, Type targetType)
        {
            if (targetType == null)
                throw new ArgumentNullException("targetType");

            return ConvertInternal(sourceValue, targetType);
        }

        /// <summary>
        /// Implements <see cref="CanConvert" />.
        /// </summary>
        /// <remarks>
        /// The default implementation returns <c>true</c> if <see cref="GetConversionCostInternal"/>
        /// returns a valid conversion cost.
        /// </remarks>
        /// <param name="sourceType">The source type, never null</param>
        /// <param name="targetType">The target type, never null</param>
        /// <returns>True if the conversion is supported</returns>
        protected virtual bool CanConvertInternal(Type sourceType, Type targetType)
        {
            return ! GetConversionCostInternal(sourceType, targetType).IsInvalid;
        }

        /// <summary>
        /// Implements <see cref="GetConversionCost" />.
        /// </summary>
        /// <param name="sourceType">The source type, never null</param>
        /// <param name="targetType">The target type, never null</param>
        /// <returns>The conversion cost</returns>
        protected abstract ConversionCost GetConversionCostInternal(Type sourceType, Type targetType);

        /// <summary>
        /// Implements <see cref="Convert" />
        /// </summary>
        /// <param name="sourceValue">The source value</param>
        /// <param name="targetType">The target type, never null</param>
        /// <returns>The converted value</returns>
        protected abstract object ConvertInternal(object sourceValue, Type targetType);
    }
}
