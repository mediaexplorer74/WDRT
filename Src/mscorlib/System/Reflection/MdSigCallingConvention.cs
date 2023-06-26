using System;

namespace System.Reflection
{
	// Token: 0x020005F5 RID: 1525
	[Flags]
	[Serializable]
	internal enum MdSigCallingConvention : byte
	{
		// Token: 0x04001D0D RID: 7437
		CallConvMask = 15,
		// Token: 0x04001D0E RID: 7438
		Default = 0,
		// Token: 0x04001D0F RID: 7439
		C = 1,
		// Token: 0x04001D10 RID: 7440
		StdCall = 2,
		// Token: 0x04001D11 RID: 7441
		ThisCall = 3,
		// Token: 0x04001D12 RID: 7442
		FastCall = 4,
		// Token: 0x04001D13 RID: 7443
		Vararg = 5,
		// Token: 0x04001D14 RID: 7444
		Field = 6,
		// Token: 0x04001D15 RID: 7445
		LocalSig = 7,
		// Token: 0x04001D16 RID: 7446
		Property = 8,
		// Token: 0x04001D17 RID: 7447
		Unmgd = 9,
		// Token: 0x04001D18 RID: 7448
		GenericInst = 10,
		// Token: 0x04001D19 RID: 7449
		Generic = 16,
		// Token: 0x04001D1A RID: 7450
		HasThis = 32,
		// Token: 0x04001D1B RID: 7451
		ExplicitThis = 64
	}
}
