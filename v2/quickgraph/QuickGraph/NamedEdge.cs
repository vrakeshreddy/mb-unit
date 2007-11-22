// QuickGraph Library 
// 
// Copyright (c) 2004 Jonathan de Halleux
//
// This software is provided 'as-is', without any express or implied warranty. 
// 
// In no event will the authors be held liable for any damages arising from 
// the use of this software.
// Permission is granted to anyone to use this software for any purpose, 
// including commercial applications, and to alter it and redistribute it 
// freely, subject to the following restrictions:
//
//		1. The origin of this software must not be misrepresented; 
//		you must not claim that you wrote the original software. 
//		If you use this software in a product, an acknowledgment in the product 
//		documentation would be appreciated but is not required.
//
//		2. Altered source versions must be plainly marked as such, and must 
//		not be misrepresented as being the original software.
//
//		3. This notice may not be removed or altered from any source 
//		distribution.
//		
//		QuickGraph Library HomePage: http://mbunit.tigris.org
//		Author: Jonathan de Halleux



using System;
using System.Xml.Serialization;

namespace QuickGraph
{
	using QuickGraph.Concepts;
	using QuickGraph.Concepts.Serialization;
	using QuickGraph.Exceptions;
	
	/// <summary>
	/// A vertex with a name
	/// </summary>
	[XmlRoot("named-edge")]
	public class NamedEdge : Edge, IGraphBiSerializable
	{
		private string name;

		/// <summary>
		/// Empty constructor. Used internally
		/// </summary>
		public NamedEdge()
			:base()
		{}

		/// <summary>
		/// Constructs a new named edge
		/// </summary>
		/// <param name="id"></param>
		/// <param name="source"></param>
		/// <param name="target"></param>
		public NamedEdge(int id, IVertex source, IVertex target)
			:base(id,source,target)
		{}

		/// <summary>
		/// Vertex name
		/// </summary>
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				if (this.name==null)
					throw new Exception("name not set");
				return this.name;
			}
			set
			{
				if (value==null)
					throw new ArgumentNullException("name");
				this.name=value;
			}
		}

		/// <summary>
		/// Adds nothing to serialization info
		/// </summary>
		/// <param name="info">data holder</param>
		/// <exception cref="ArgumentNullException">info is null</exception>
		/// <exception cref="ArgumentException">info is not serializing</exception>
		public override void WriteGraphData(IGraphSerializationInfo info)
		{
			base.WriteGraphData(info);
			info.Add("name",Name);
		}

		/// <summary>
		/// Reads no data from serialization info
		/// </summary>
		/// <param name="info">data holder</param>
		/// <exception cref="ArgumentNullException">info is null</exception>
		/// <exception cref="ArgumentException">info is serializing</exception>
		public override void ReadGraphData(IGraphSerializationInfo info)
		{
			base.ReadGraphData(info);
			this.name = info["name"].ToString();
		}
	}
}
