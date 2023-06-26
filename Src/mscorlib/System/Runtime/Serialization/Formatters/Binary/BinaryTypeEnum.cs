using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000767 RID: 1895
	[Serializable]
	internal enum BinaryTypeEnum
	{
		// Token: 0x04002505 RID: 9477
		Primitive,
		// Token: 0x04002506 RID: 9478
		String,
		// Token: 0x04002507 RID: 9479
		Object,
		// Token: 0x04002508 RID: 9480
		ObjectUrt,
		// Token: 0x04002509 RID: 9481
		ObjectUser,
		// Token: 0x0400250A RID: 9482
		ObjectArray,
		// Token: 0x0400250B RID: 9483
		StringArray,
		// Token: 0x0400250C RID: 9484
		PrimitiveArray
	}
}
