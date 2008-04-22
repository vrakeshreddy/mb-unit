// Copyright 2005-2008 Gallio Project - http://www.gallio.org/
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

using MbUnit.Framework;

namespace MbUnit.TestResources
{
    /// <summary>
    /// This class is used by the MSBuild task tests. Please don't modify it.
    /// </summary>
    [TestFixture]
    public class PassingTests
    {
        [Test]
        public void Pass()
        {
            Assert.AreEqual(1, 0 + 1);
            Assert.AreEqual(2, 1 + 1);
            Assert.AreEqual(3, 2 + 1);
        }

        [Test]
        public void PassAgain()
        {
        }
    }

    [Context]
    public class Context
    {
        [TestFixture]
        public class Spec
        {
        }
    }
}
