using System;
using System.Xml.Serialization;
using MbUnit.Core.Model;

namespace MbUnit.Core.Serialization
{
    /// <summary>
    /// Describes a test in a portable manner for serialization.
    /// </summary>
    /// <seealso cref="ITest"/>
    [Serializable]
    [XmlType(Namespace=SerializationUtils.XmlNamespace)]
    public class TestInfo
    {
        private string name;
        private CodeReferenceInfo codeReference;
        private MetadataMapInfo metadata;

        /// <summary>
        /// Gets or sets the test name.  (non-null)
        /// </summary>
        /// <seealso cref="ITest.Name"/>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the code reference.  (non-null)
        /// </summary>
        /// <seealso cref="ITest.CodeReference"/>
        [XmlElement("codeReference", IsNullable=false)]
        public CodeReferenceInfo CodeReference
        {
            get { return codeReference; }
            set { codeReference = value; }
        }

        /// <summary>
        /// Gets or sets the metadata map.  (non-null)
        /// </summary>
        /// <seealso cref="ITest.Metadata"/>
        [XmlElement("metadata", IsNullable=false)]
        public MetadataMapInfo Metadata
        {
            get { return metadata; }
            set { metadata = value; }
        }
    }
}