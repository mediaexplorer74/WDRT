using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F5 RID: 245
	[NullableContext(1)]
	internal interface IXmlDocumentType : IXmlNode
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000CC6 RID: 3270
		string Name { get; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000CC7 RID: 3271
		string System { get; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000CC8 RID: 3272
		string Public { get; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000CC9 RID: 3273
		string InternalSubset { get; }
	}
}
