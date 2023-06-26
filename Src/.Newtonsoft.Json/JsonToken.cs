using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000030 RID: 48
	public enum JsonToken
	{
		// Token: 0x040000EE RID: 238
		None,
		// Token: 0x040000EF RID: 239
		StartObject,
		// Token: 0x040000F0 RID: 240
		StartArray,
		// Token: 0x040000F1 RID: 241
		StartConstructor,
		// Token: 0x040000F2 RID: 242
		PropertyName,
		// Token: 0x040000F3 RID: 243
		Comment,
		// Token: 0x040000F4 RID: 244
		Raw,
		// Token: 0x040000F5 RID: 245
		Integer,
		// Token: 0x040000F6 RID: 246
		Float,
		// Token: 0x040000F7 RID: 247
		String,
		// Token: 0x040000F8 RID: 248
		Boolean,
		// Token: 0x040000F9 RID: 249
		Null,
		// Token: 0x040000FA RID: 250
		Undefined,
		// Token: 0x040000FB RID: 251
		EndObject,
		// Token: 0x040000FC RID: 252
		EndArray,
		// Token: 0x040000FD RID: 253
		EndConstructor,
		// Token: 0x040000FE RID: 254
		Date,
		// Token: 0x040000FF RID: 255
		Bytes
	}
}
