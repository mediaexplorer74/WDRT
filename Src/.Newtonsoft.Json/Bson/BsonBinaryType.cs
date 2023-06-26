using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000103 RID: 259
	internal enum BsonBinaryType : byte
	{
		// Token: 0x04000401 RID: 1025
		Binary,
		// Token: 0x04000402 RID: 1026
		Function,
		// Token: 0x04000403 RID: 1027
		[Obsolete("This type has been deprecated in the BSON specification. Use Binary instead.")]
		BinaryOld,
		// Token: 0x04000404 RID: 1028
		[Obsolete("This type has been deprecated in the BSON specification. Use Uuid instead.")]
		UuidOld,
		// Token: 0x04000405 RID: 1029
		Uuid,
		// Token: 0x04000406 RID: 1030
		Md5,
		// Token: 0x04000407 RID: 1031
		UserDefined = 128
	}
}
