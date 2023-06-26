using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000766 RID: 1894
	[Serializable]
	internal enum BinaryHeaderEnum
	{
		// Token: 0x040024ED RID: 9453
		SerializedStreamHeader,
		// Token: 0x040024EE RID: 9454
		Object,
		// Token: 0x040024EF RID: 9455
		ObjectWithMap,
		// Token: 0x040024F0 RID: 9456
		ObjectWithMapAssemId,
		// Token: 0x040024F1 RID: 9457
		ObjectWithMapTyped,
		// Token: 0x040024F2 RID: 9458
		ObjectWithMapTypedAssemId,
		// Token: 0x040024F3 RID: 9459
		ObjectString,
		// Token: 0x040024F4 RID: 9460
		Array,
		// Token: 0x040024F5 RID: 9461
		MemberPrimitiveTyped,
		// Token: 0x040024F6 RID: 9462
		MemberReference,
		// Token: 0x040024F7 RID: 9463
		ObjectNull,
		// Token: 0x040024F8 RID: 9464
		MessageEnd,
		// Token: 0x040024F9 RID: 9465
		Assembly,
		// Token: 0x040024FA RID: 9466
		ObjectNullMultiple256,
		// Token: 0x040024FB RID: 9467
		ObjectNullMultiple,
		// Token: 0x040024FC RID: 9468
		ArraySinglePrimitive,
		// Token: 0x040024FD RID: 9469
		ArraySingleObject,
		// Token: 0x040024FE RID: 9470
		ArraySingleString,
		// Token: 0x040024FF RID: 9471
		CrossAppDomainMap,
		// Token: 0x04002500 RID: 9472
		CrossAppDomainString,
		// Token: 0x04002501 RID: 9473
		CrossAppDomainAssembly,
		// Token: 0x04002502 RID: 9474
		MethodCall,
		// Token: 0x04002503 RID: 9475
		MethodReturn
	}
}
