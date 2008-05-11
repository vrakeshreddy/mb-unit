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
using Gallio.Collections;
using Gallio.Framework.Data;
using MbUnit.Framework;
using Rhino.Mocks;

namespace Gallio.Tests.Framework.Data
{
    [TestFixture]
    [TestsOn(typeof(ConcatenationMergeStrategy))]
    public class ConcatenationMergeStrategyTest : BaseUnitTest
    {
        [Test]
        public void CombinesRowsIntoASingleSequence()
        {
            DataBinding[] bindings = new DataBinding[] {
                new SimpleDataBinding(0, null)
            };
            IDataProvider[] providers = new IDataProvider[] {
                Mocks.CreateMock<IDataProvider>(),
                Mocks.CreateMock<IDataProvider>(),
                Mocks.CreateMock<IDataProvider>()
            };

            using (Mocks.Record())
            {
                Expect.Call(providers[0].GetRows(bindings, true)).Return(new IDataRow[] {
                    new ScalarDataRow<int>(1, null, false),
                    new ScalarDataRow<int>(2, null, false),
                });

                Expect.Call(providers[1].GetRows(bindings, true)).Return(EmptyArray<IDataRow>.Instance);

                Expect.Call(providers[2].GetRows(bindings, true)).Return(new IDataRow[] {
                    new ScalarDataRow<int>(3, null, true),
                });
            }

            using (Mocks.Playback())
            {
                List<IDataRow> rows = new List<IDataRow>(ConcatenationMergeStrategy.Instance.Merge(providers, bindings, true));
                Assert.AreEqual(3, rows.Count);

                Assert.AreEqual(1, rows[0].GetValue(bindings[0]));
                Assert.IsFalse(rows[0].IsDynamic);

                Assert.AreEqual(2, rows[1].GetValue(bindings[0]));
                Assert.IsFalse(rows[1].IsDynamic);

                Assert.AreEqual(3, rows[2].GetValue(bindings[0]));
                Assert.IsTrue(rows[2].IsDynamic);
            }
        }
    }
}
