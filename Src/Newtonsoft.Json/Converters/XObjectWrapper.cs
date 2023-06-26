using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FF RID: 255
	[NullableContext(2)]
	[Nullable(0)]
	internal class XObjectWrapper : IXmlNode
	{
		// Token: 0x06000D0D RID: 3341 RVA: 0x0003353E File Offset: 0x0003173E
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0003354D File Offset: 0x0003174D
		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00033555 File Offset: 0x00031755
		public virtual XmlNodeType NodeType
		{
			get
			{
				XObject xmlObject = this._xmlObject;
				if (xmlObject == null)
				{
					return XmlNodeType.None;
				}
				return xmlObject.NodeType;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00033568 File Offset: 0x00031768
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0003356B File Offset: 0x0003176B
		[Nullable(1)]
		public virtual List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00033572 File Offset: 0x00031772
		[Nullable(1)]
		public virtual List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00033579 File Offset: 0x00031779
		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0003357C File Offset: 0x0003177C
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x0003357F File Offset: 0x0003177F
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00033586 File Offset: 0x00031786
		[NullableContext(1)]
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0003358D File Offset: 0x0003178D
		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040003F2 RID: 1010
		private readonly XObject _xmlObject;
	}
}
