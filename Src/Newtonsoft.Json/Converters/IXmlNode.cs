using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F7 RID: 247
	[NullableContext(2)]
	internal interface IXmlNode
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000CCD RID: 3277
		XmlNodeType NodeType { get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000CCE RID: 3278
		string LocalName { get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000CCF RID: 3279
		[Nullable(1)]
		List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CD0 RID: 3280
		[Nullable(1)]
		List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CD1 RID: 3281
		IXmlNode ParentNode { get; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CD2 RID: 3282
		// (set) Token: 0x06000CD3 RID: 3283
		string Value { get; set; }

		// Token: 0x06000CD4 RID: 3284
		[NullableContext(1)]
		IXmlNode AppendChild(IXmlNode newChild);

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CD5 RID: 3285
		string NamespaceUri { get; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CD6 RID: 3286
		object WrappedNode { get; }
	}
}
