// Copyright 2005-2010 Gallio Project - http://www.gallio.org/
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
using Gallio.Common.Collections;

namespace Gallio.Common.Xml
{
    /// <summary>
    /// Represents a comment tag in an XML fragment.
    /// </summary>
    public class MarkupComment : MarkupBase, IDiffable<MarkupComment>
    {
        private readonly string text;

        /// <summary>
        /// Gets the textual content of the comment tag.
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
        }

        /// <summary>
        /// Constructs an XML comment tag.
        /// </summary>
        /// <param name="index">The index of the markup.</param>
        /// <param name="text">The literal content of the comment tag.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="text"/> is null.</exception>
        public MarkupComment(int index, string text)
            : base(index, EmptyArray<IMarkup>.Instance)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index");
            if (text == null)
                throw new ArgumentNullException("text");

            this.text = text;
        }

        /// <inheritdoc />
        public override DiffSet Diff(IMarkup expected, IXmlPathStrict path, Options options)
        {
            var expectedComment = expected as MarkupComment;

            if (expectedComment != null)
                return Diff(expectedComment, path, options);

            return new DiffSetBuilder()
                .Add(new Diff("Unexpected comment markup.", path.Element(Index), DiffTargets.Actual))
                .ToDiffSet();
        }

        /// <inheritdoc />
        public DiffSet Diff(MarkupComment expected, IXmlPathStrict path, Options options)
        {
            if (expected == null)
                throw new ArgumentNullException("expected");
            if (path == null)
                throw new ArgumentNullException("path");

            if (((options & Options.IgnoreComments) != 0) || Text.Equals(expected.Text))
            {
                return DiffSet.Empty;
            }
            
            return new DiffSetBuilder()
                .Add(new Diff("Unexpected comment found.", path.Element(Index), DiffTargets.Both))
                .ToDiffSet();
        }

        /// <inheritdoc />
        public override void Aggregate(XmlPathFormatAggregator aggregator)
        {
            aggregator.Add(String.Format("<!--{0}-->", text));
        }
    }
}