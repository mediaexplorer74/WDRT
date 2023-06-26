using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000773 RID: 1907
	[Flags]
	[Serializable]
	internal enum MessageEnum
	{
		// Token: 0x0400255D RID: 9565
		NoArgs = 1,
		// Token: 0x0400255E RID: 9566
		ArgsInline = 2,
		// Token: 0x0400255F RID: 9567
		ArgsIsArray = 4,
		// Token: 0x04002560 RID: 9568
		ArgsInArray = 8,
		// Token: 0x04002561 RID: 9569
		NoContext = 16,
		// Token: 0x04002562 RID: 9570
		ContextInline = 32,
		// Token: 0x04002563 RID: 9571
		ContextInArray = 64,
		// Token: 0x04002564 RID: 9572
		MethodSignatureInArray = 128,
		// Token: 0x04002565 RID: 9573
		PropertyInArray = 256,
		// Token: 0x04002566 RID: 9574
		NoReturnValue = 512,
		// Token: 0x04002567 RID: 9575
		ReturnValueVoid = 1024,
		// Token: 0x04002568 RID: 9576
		ReturnValueInline = 2048,
		// Token: 0x04002569 RID: 9577
		ReturnValueInArray = 4096,
		// Token: 0x0400256A RID: 9578
		ExceptionInArray = 8192,
		// Token: 0x0400256B RID: 9579
		GenericMethod = 32768
	}
}
