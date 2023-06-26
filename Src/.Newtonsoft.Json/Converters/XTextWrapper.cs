using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FB RID: 251
	[NullableContext(2)]
	[Nullable(0)]
	internal class XTextWrapper : XObjectWrapper
	{
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x000332C1 File Offset: 0x000314C1
		[Nullable(1)]
		private XText Text
		{
			[NullableContext(1)]
			get
			{
				return (XText)base.WrappedNode;
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000332CE File Offset: 0x000314CE
		[NullableContext(1)]
		public XTextWrapper(XText text)
			: base(text)
		{
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x000332D7 File Offset: 0x000314D7
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x000332E4 File Offset: 0x000314E4
		public override string Value
		{
			get
			{
				return this.Text.Value;
			}
			set
			{
				this.Text.Value = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x000332F2 File Offset: 0x000314F2
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Text.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Text.Parent);
			}
		}
	}
}
