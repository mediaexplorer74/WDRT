using System;

namespace System.Reflection
{
	// Token: 0x020005D6 RID: 1494
	[Serializable]
	internal enum CustomAttributeEncoding
	{
		// Token: 0x04001C6F RID: 7279
		Undefined,
		// Token: 0x04001C70 RID: 7280
		Boolean = 2,
		// Token: 0x04001C71 RID: 7281
		Char,
		// Token: 0x04001C72 RID: 7282
		SByte,
		// Token: 0x04001C73 RID: 7283
		Byte,
		// Token: 0x04001C74 RID: 7284
		Int16,
		// Token: 0x04001C75 RID: 7285
		UInt16,
		// Token: 0x04001C76 RID: 7286
		Int32,
		// Token: 0x04001C77 RID: 7287
		UInt32,
		// Token: 0x04001C78 RID: 7288
		Int64,
		// Token: 0x04001C79 RID: 7289
		UInt64,
		// Token: 0x04001C7A RID: 7290
		Float,
		// Token: 0x04001C7B RID: 7291
		Double,
		// Token: 0x04001C7C RID: 7292
		String,
		// Token: 0x04001C7D RID: 7293
		Array = 29,
		// Token: 0x04001C7E RID: 7294
		Type = 80,
		// Token: 0x04001C7F RID: 7295
		Object,
		// Token: 0x04001C80 RID: 7296
		Field = 83,
		// Token: 0x04001C81 RID: 7297
		Property,
		// Token: 0x04001C82 RID: 7298
		Enum
	}
}
