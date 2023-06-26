using System;

namespace System.Reflection
{
	// Token: 0x020005F6 RID: 1526
	[Flags]
	[Serializable]
	internal enum PInvokeAttributes
	{
		// Token: 0x04001D1D RID: 7453
		NoMangle = 1,
		// Token: 0x04001D1E RID: 7454
		CharSetMask = 6,
		// Token: 0x04001D1F RID: 7455
		CharSetNotSpec = 0,
		// Token: 0x04001D20 RID: 7456
		CharSetAnsi = 2,
		// Token: 0x04001D21 RID: 7457
		CharSetUnicode = 4,
		// Token: 0x04001D22 RID: 7458
		CharSetAuto = 6,
		// Token: 0x04001D23 RID: 7459
		BestFitUseAssem = 0,
		// Token: 0x04001D24 RID: 7460
		BestFitEnabled = 16,
		// Token: 0x04001D25 RID: 7461
		BestFitDisabled = 32,
		// Token: 0x04001D26 RID: 7462
		BestFitMask = 48,
		// Token: 0x04001D27 RID: 7463
		ThrowOnUnmappableCharUseAssem = 0,
		// Token: 0x04001D28 RID: 7464
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x04001D29 RID: 7465
		ThrowOnUnmappableCharDisabled = 8192,
		// Token: 0x04001D2A RID: 7466
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x04001D2B RID: 7467
		SupportsLastError = 64,
		// Token: 0x04001D2C RID: 7468
		CallConvMask = 1792,
		// Token: 0x04001D2D RID: 7469
		CallConvWinapi = 256,
		// Token: 0x04001D2E RID: 7470
		CallConvCdecl = 512,
		// Token: 0x04001D2F RID: 7471
		CallConvStdcall = 768,
		// Token: 0x04001D30 RID: 7472
		CallConvThiscall = 1024,
		// Token: 0x04001D31 RID: 7473
		CallConvFastcall = 1280,
		// Token: 0x04001D32 RID: 7474
		MaxValue = 65535
	}
}
