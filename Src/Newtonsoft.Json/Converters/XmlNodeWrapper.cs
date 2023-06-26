using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F2 RID: 242
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlNodeWrapper : IXmlNode
	{
		// Token: 0x06000CA6 RID: 3238 RVA: 0x00032DB8 File Offset: 0x00030FB8
		[NullableContext(1)]
		public XmlNodeWrapper(XmlNode node)
		{
			this._node = node;
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00032DC7 File Offset: 0x00030FC7
		public object WrappedNode
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00032DCF File Offset: 0x00030FCF
		public XmlNodeType NodeType
		{
			get
			{
				return this._node.NodeType;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00032DDC File Offset: 0x00030FDC
		public virtual string LocalName
		{
			get
			{
				return this._node.LocalName;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00032DEC File Offset: 0x00030FEC
		[Nullable(1)]
		public List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				if (this._childNodes == null)
				{
					if (!this._node.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>(this._node.ChildNodes.Count);
						foreach (object obj in this._node.ChildNodes)
						{
							XmlNode xmlNode = (XmlNode)obj;
							this._childNodes.Add(XmlNodeWrapper.WrapNode(xmlNode));
						}
					}
				}
				return this._childNodes;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00032E9C File Offset: 0x0003109C
		protected virtual bool HasChildNodes
		{
			get
			{
				return this._node.HasChildNodes;
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00032EAC File Offset: 0x000310AC
		[NullableContext(1)]
		internal static IXmlNode WrapNode(XmlNode node)
		{
			XmlNodeType nodeType = node.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return new XmlElementWrapper((XmlElement)node);
			}
			if (nodeType == XmlNodeType.DocumentType)
			{
				return new XmlDocumentTypeWrapper((XmlDocumentType)node);
			}
			if (nodeType != XmlNodeType.XmlDeclaration)
			{
				return new XmlNodeWrapper(node);
			}
			return new XmlDeclarationWrapper((XmlDeclaration)node);
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00032EFC File Offset: 0x000310FC
		[Nullable(1)]
		public List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				if (this._attributes == null)
				{
					if (!this.HasAttributes)
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>(this._node.Attributes.Count);
						foreach (object obj in this._node.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj;
							this._attributes.Add(XmlNodeWrapper.WrapNode(xmlAttribute));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00032FA4 File Offset: 0x000311A4
		private bool HasAttributes
		{
			get
			{
				XmlElement xmlElement = this._node as XmlElement;
				if (xmlElement != null)
				{
					return xmlElement.HasAttributes;
				}
				XmlAttributeCollection attributes = this._node.Attributes;
				return attributes != null && attributes.Count > 0;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00032FE0 File Offset: 0x000311E0
		public IXmlNode ParentNode
		{
			get
			{
				XmlAttribute xmlAttribute = this._node as XmlAttribute;
				XmlNode xmlNode = ((xmlAttribute != null) ? xmlAttribute.OwnerElement : this._node.ParentNode);
				if (xmlNode == null)
				{
					return null;
				}
				return XmlNodeWrapper.WrapNode(xmlNode);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0003301B File Offset: 0x0003121B
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x00033028 File Offset: 0x00031228
		public string Value
		{
			get
			{
				return this._node.Value;
			}
			set
			{
				this._node.Value = value;
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00033038 File Offset: 0x00031238
		[NullableContext(1)]
		public IXmlNode AppendChild(IXmlNode newChild)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)newChild;
			this._node.AppendChild(xmlNodeWrapper._node);
			this._childNodes = null;
			this._attributes = null;
			return newChild;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0003306D File Offset: 0x0003126D
		public string NamespaceUri
		{
			get
			{
				return this._node.NamespaceURI;
			}
		}

		// Token: 0x040003EC RID: 1004
		[Nullable(1)]
		private readonly XmlNode _node;

		// Token: 0x040003ED RID: 1005
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _childNodes;

		// Token: 0x040003EE RID: 1006
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _attributes;
	}
}
