using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000100 RID: 256
	[NullableContext(2)]
	[Nullable(0)]
	internal class XAttributeWrapper : XObjectWrapper
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00033590 File Offset: 0x00031790
		[Nullable(1)]
		private XAttribute Attribute
		{
			[NullableContext(1)]
			get
			{
				return (XAttribute)base.WrappedNode;
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003359D File Offset: 0x0003179D
		[NullableContext(1)]
		public XAttributeWrapper(XAttribute attribute)
			: base(attribute)
		{
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x000335A6 File Offset: 0x000317A6
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x000335B3 File Offset: 0x000317B3
		public override string Value
		{
			get
			{
				return this.Attribute.Value;
			}
			set
			{
				this.Attribute.Value = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x000335C1 File Offset: 0x000317C1
		public override string LocalName
		{
			get
			{
				return this.Attribute.Name.LocalName;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x000335D3 File Offset: 0x000317D3
		public override string NamespaceUri
		{
			get
			{
				return this.Attribute.Name.NamespaceName;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x000335E5 File Offset: 0x000317E5
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Attribute.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Attribute.Parent);
			}
		}
	}
}
