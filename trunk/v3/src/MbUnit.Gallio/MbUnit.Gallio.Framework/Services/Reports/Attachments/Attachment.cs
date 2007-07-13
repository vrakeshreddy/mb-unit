// Copyright 2007 MbUnit Project - http://www.mbunit.com/
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
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace MbUnit.Framework.Services.Reports
{
    /// <summary>
    /// An attachment is an embedded object in a report.  An attachment must specify a
    /// content type (a MIME type), and some contents.
    /// </summary>
    [Serializable]
    public abstract class Attachment
    {
        private string name;
        private string contentType;

        /// <summary>
        /// Creates an attachment.
        /// </summary>
        /// <param name="name">The name of attachment, or null to automatically assign one</param>
        /// <param name="contentType">The content type, not null</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="contentType"/> is null</exception>
        protected Attachment(string name, string contentType)
        {
            if (contentType == null)
                throw new ArgumentNullException("contentType");

            this.name = name == null ? Guid.NewGuid().ToString() : name;
            this.contentType = contentType;
        }

        /// <summary>
        /// Gets the name of the attachment, not null.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the content type of the attachment specified as a MIME type, not null.
        /// <seealso cref="MimeTypes"/> for definitions of common MIME types used by MbUnit.
        /// </summary>
        public string ContentType
        {
            get { return contentType; }
        }

        /// <summary>
        /// Invokes the appropriate visitor method for this attachment type.
        /// </summary>
        /// <param name="visitor">The visitor</param>
        public abstract void Accept(IAttachmentVisitor visitor);
    }
}
