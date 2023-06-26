using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F4 RID: 244
	[NullableContext(1)]
	internal interface IXmlDeclaration : IXmlNode
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000CC1 RID: 3265
		string Version { get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000CC2 RID: 3266
		// (set) Token: 0x06000CC3 RID: 3267
		string Encoding { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000CC4 RID: 3268
		// (set) Token: 0x06000CC5 RID: 3269
		string Standalone { get; set; }
	}
}
