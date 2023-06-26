using System;

namespace System.Reflection
{
	// Token: 0x020005F4 RID: 1524
	[Serializable]
	internal enum CorElementType : byte
	{
		// Token: 0x04001CE8 RID: 7400
		End,
		// Token: 0x04001CE9 RID: 7401
		Void,
		// Token: 0x04001CEA RID: 7402
		Boolean,
		// Token: 0x04001CEB RID: 7403
		Char,
		// Token: 0x04001CEC RID: 7404
		I1,
		// Token: 0x04001CED RID: 7405
		U1,
		// Token: 0x04001CEE RID: 7406
		I2,
		// Token: 0x04001CEF RID: 7407
		U2,
		// Token: 0x04001CF0 RID: 7408
		I4,
		// Token: 0x04001CF1 RID: 7409
		U4,
		// Token: 0x04001CF2 RID: 7410
		I8,
		// Token: 0x04001CF3 RID: 7411
		U8,
		// Token: 0x04001CF4 RID: 7412
		R4,
		// Token: 0x04001CF5 RID: 7413
		R8,
		// Token: 0x04001CF6 RID: 7414
		String,
		// Token: 0x04001CF7 RID: 7415
		Ptr,
		// Token: 0x04001CF8 RID: 7416
		ByRef,
		// Token: 0x04001CF9 RID: 7417
		ValueType,
		// Token: 0x04001CFA RID: 7418
		Class,
		// Token: 0x04001CFB RID: 7419
		Var,
		// Token: 0x04001CFC RID: 7420
		Array,
		// Token: 0x04001CFD RID: 7421
		GenericInst,
		// Token: 0x04001CFE RID: 7422
		TypedByRef,
		// Token: 0x04001CFF RID: 7423
		I = 24,
		// Token: 0x04001D00 RID: 7424
		U,
		// Token: 0x04001D01 RID: 7425
		FnPtr = 27,
		// Token: 0x04001D02 RID: 7426
		Object,
		// Token: 0x04001D03 RID: 7427
		SzArray,
		// Token: 0x04001D04 RID: 7428
		MVar,
		// Token: 0x04001D05 RID: 7429
		CModReqd,
		// Token: 0x04001D06 RID: 7430
		CModOpt,
		// Token: 0x04001D07 RID: 7431
		Internal,
		// Token: 0x04001D08 RID: 7432
		Max,
		// Token: 0x04001D09 RID: 7433
		Modifier = 64,
		// Token: 0x04001D0A RID: 7434
		Sentinel,
		// Token: 0x04001D0B RID: 7435
		Pinned = 69
	}
}
