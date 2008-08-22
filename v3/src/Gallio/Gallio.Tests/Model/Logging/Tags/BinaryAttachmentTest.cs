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

using Gallio.Model.Logging;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace Gallio.Tests.Model.Logging.Tags
{
    [VerifyEqualityContract(typeof(BinaryAttachment), ImplementsOperatorOverloads = false)]
    public class BinaryAttachmentTest : IEquivalenceClassProvider<BinaryAttachment>
    {
        public EquivalenceClassCollection<BinaryAttachment> GetEquivalenceClasses()
        {
            return EquivalenceClassCollection<BinaryAttachment>.FromDistinctInstances(
                new BinaryAttachment("abc", MimeTypes.PlainText, new byte[] { 1, 2, 3 }),
                new BinaryAttachment("def", MimeTypes.PlainText, new byte[] { 1, 2, 3 }),
                new BinaryAttachment("abc", MimeTypes.Xml, new byte[] { 1, 2, 3 }),
                new BinaryAttachment("abc", MimeTypes.PlainText, new byte[] { 1, 2 }));
        }

        [Test]
        public void NullAttachmentNamePicksAUniqueOne()
        {
            Assert.IsNotNull(new BinaryAttachment(null, MimeTypes.PlainText, new byte[0]));
        }
    }
}
