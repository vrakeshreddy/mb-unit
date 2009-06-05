﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=1.1.4322.573.
// 
namespace QuickGraph.Serialization.GraphML {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="locator.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlRootAttribute("locator", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc", IsNullable=false)]
    public class locatortype {
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="data.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlRootAttribute("data", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc", IsNullable=false)]
    public class datatype : dataextensiontype {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string key;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string id;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="data-extension.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(defaulttype))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(datatype))]
    public class dataextensiontype {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="default.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlRootAttribute("default", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc", IsNullable=false)]
    public class defaulttype : dataextensiontype {
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="key.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlRootAttribute("key", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc", IsNullable=false)]
    public class keytype {
        
        /// <remarks/>
        public string desc;
        
        /// <remarks/>
        public defaulttype @default;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string id;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(keyfortype.all)]
        public keyfortype @for = keyfortype.all;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="key.for.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    public enum keyfortype {
        
        /// <remarks/>
        all,
        
        /// <remarks/>
        graph,
        
        /// <remarks/>
        node,
        
        /// <remarks/>
        edge,
        
        /// <remarks/>
        hyperedge,
        
        /// <remarks/>
        port,
        
        /// <remarks/>
        endpoint,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="graphml.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlRootAttribute("graphml", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc", IsNullable=false)]
    public class graphmltype {
        
        /// <remarks/>
        public string desc;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("key")]
        public keytype[] key;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("data", typeof(datatype))]
        [System.Xml.Serialization.XmlElementAttribute("graph", typeof(graphtype))]
        public object[] Items;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="graph.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlRootAttribute("graph", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc", IsNullable=false)]
    public class graphtype {
        
        /// <remarks/>
        public string desc;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("locator", typeof(locatortype))]
        [System.Xml.Serialization.XmlElementAttribute("data", typeof(datatype))]
        [System.Xml.Serialization.XmlElementAttribute("hyperedge", typeof(hyperedgetype))]
        [System.Xml.Serialization.XmlElementAttribute("edge", typeof(edgetype))]
        [System.Xml.Serialization.XmlElementAttribute("node", typeof(nodetype))]
        public object[] Items;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string id;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public graphedgedefaulttype edgedefault;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="hyperedge.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlRootAttribute("hyperedge", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc", IsNullable=false)]
    public class hyperedgetype {
        
        /// <remarks/>
        public string desc;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("data", typeof(datatype))]
        [System.Xml.Serialization.XmlElementAttribute("endpoint", typeof(endpointtype))]
        public object[] Items;
        
        /// <remarks/>
        public graphtype graph;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string id;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="endpoint.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlRootAttribute("endpoint", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc", IsNullable=false)]
    public class endpointtype {
        
        /// <remarks/>
        public string desc;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string id;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string port;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string node;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(endpointtypetype.undir)]
        public endpointtypetype type = endpointtypetype.undir;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="endpoint.type.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    public enum endpointtypetype {
        
        /// <remarks/>
        @in,
        
        /// <remarks/>
        @out,
        
        /// <remarks/>
        undir,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="edge.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlRootAttribute("edge", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc", IsNullable=false)]
    public class edgetype {
        
        /// <remarks/>
        public string desc;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("data")]
        public datatype[] data;
        
        /// <remarks/>
        public graphtype graph;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string id;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool directed;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool directedSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string source;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string target;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string sourceport;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string targetport;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="node.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlRootAttribute("node", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc", IsNullable=false)]
    public class nodetype {
        
        /// <remarks/>
        public string desc;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("locator", typeof(locatortype))]
        [System.Xml.Serialization.XmlElementAttribute("data", typeof(datatype))]
        [System.Xml.Serialization.XmlElementAttribute("graph", typeof(graphtype))]
        [System.Xml.Serialization.XmlElementAttribute("port", typeof(porttype))]
        public object[] Items;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string id;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="port.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    [System.Xml.Serialization.XmlRootAttribute("port", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc", IsNullable=false)]
    public class porttype {
        
        /// <remarks/>
        public string desc;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("data", typeof(datatype))]
        [System.Xml.Serialization.XmlElementAttribute("port", typeof(porttype))]
        public object[] Items;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKEN")]
        public string name;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="graph.edgedefault.type", Namespace="http://graphml.graphdrawing.org/xmlns/1.0rc")]
    public enum graphedgedefaulttype {
        
        /// <remarks/>
        directed,
        
        /// <remarks/>
        undirected,
    }
}