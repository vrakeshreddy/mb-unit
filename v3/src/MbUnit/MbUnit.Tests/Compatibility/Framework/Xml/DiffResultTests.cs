#pragma warning disable 618

namespace MbUnit.Tests.Compatibility.Framework.Xml {
    using MbUnit.Framework;
    using MbUnit.Framework.Xml;
	using System;
    using System.Xml;
    
    [TestFixture]
    public class DiffResultTests {
        private DiffResult _result;
    	private XmlDiff _diff;
        private Difference _majorDifference, _minorDifference;
        
        [SetUp] public void CreateDiffResult() {
            _result = new DiffResult();
        	_diff = new XmlDiff("<a/>", "<b/>");
            _majorDifference = new Difference(DifferenceType.ElementTagName, XmlNodeType.Element, XmlNodeType.Element);
            _minorDifference = new Difference(DifferenceType.AttributeSequence, XmlNodeType.Comment, XmlNodeType.Comment);
        }
        
        [Test] public void NewDiffResultIsEqualAndIdentical() {
            OldAssert.AreEqual(true, _result.Identical);
            OldAssert.AreEqual(true, _result.Equal);
        	OldAssert.AreEqual("Identical", _result.StringValue);
        }
        
        [Test] public void NotEqualOrIdenticalAfterMajorDifferenceFound() {
            _result.DifferenceFound(_diff, _majorDifference);
            OldAssert.AreEqual(false, _result.Identical);
            OldAssert.AreEqual(false, _result.Equal);
        	OldAssert.AreEqual(_diff.OptionalDescription
        	                       + Environment.NewLine
        	                       + _majorDifference.ToString(), _result.StringValue);
        }
        
        [Test] public void NotIdenticalButEqualAfterMinorDifferenceFound() {
            _result.DifferenceFound(_diff, _minorDifference);
            OldAssert.AreEqual(false, _result.Identical);
            OldAssert.AreEqual(true, _result.Equal);
        	OldAssert.AreEqual(_diff.OptionalDescription
        	                       + Environment.NewLine
        	                       + _minorDifference.ToString(), _result.StringValue);
        }
    }
}
