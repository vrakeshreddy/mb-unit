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
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Gallio.Utilities;

namespace Gallio.Model.Logging.Tags
{
    /// <summary>
    /// A text tag, containing text.
    /// </summary>
    [Serializable]
    [XmlRoot("text", Namespace = XmlSerializationUtils.GallioNamespace)]
    public sealed class TextTag : Tag, IXmlSerializable, ICloneable<TextTag>, IEquatable<TextTag>
    {
        private string text;

        /// <summary>
        /// Creates an uninitialized instance for Xml deserialization.
        /// </summary>
        private TextTag()
        {
        }

        /// <summary>
        /// Creates an initialized text tag.
        /// </summary>
        /// <param name="text">The text within the tag</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="text"/> is null</exception>
        public TextTag(string text)
        {
            if (text == null)
                throw new ArgumentNullException(@"text");
            this.text = text;
        }

        /// <summary>
        /// Gets or sets the text within the tag, not null.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null</exception>
        public string Text
        {
            get { return text; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(@"value");
                text = value;
            }
        }

        /// <inheritdoc />
        new public TextTag Clone()
        {
            TextTag copy = new TextTag();
            copy.text = text;
            return copy;
        }

        /// <inheritdoc />
        public bool Equals(TextTag other)
        {
            return other != null
                && text == other.text;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as TextTag);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return 2 ^ text.GetHashCode();
        }

        internal override Tag CloneImpl()
        {
            return Clone();
        }

        internal override void AcceptImpl(ITagVisitor visitor)
        {
            visitor.VisitTextTag(this);
        }

        internal override void WriteToImpl(TestLogStreamWriter writer)
        {
            writer.Write(text);
        }

        #region Xml Serialization
        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            bool isEmpty = reader.IsEmptyElement;
            reader.ReadStartElement(@"text");

            if (isEmpty)
            {
                text = string.Empty;
            }
            else
            {
                text = reader.ReadContentAsString();
                reader.ReadEndElement();
            }
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            // encode text with linefeeds so that they do not get lost during Xml whitespace stripping
            if (text.Contains("\n"))
                writer.WriteCData(text);
            else
                writer.WriteValue(text);
        }
        #endregion
    }
}
