using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000951 RID: 2385
	[Serializable]
	internal enum PInvokeMap
	{
		// Token: 0x04002B6F RID: 11119
		NoMangle = 1,
		// Token: 0x04002B70 RID: 11120
		CharSetMask = 6,
		// Token: 0x04002B71 RID: 11121
		CharSetNotSpec = 0,
		// Token: 0x04002B72 RID: 11122
		CharSetAnsi = 2,
		// Token: 0x04002B73 RID: 11123
		CharSetUnicode = 4,
		// Token: 0x04002B74 RID: 11124
		CharSetAuto = 6,
		// Token: 0x04002B75 RID: 11125
		PinvokeOLE = 32,
		// Token: 0x04002B76 RID: 11126
		SupportsLastError = 64,
		// Token: 0x04002B77 RID: 11127
		BestFitMask = 48,
		// Token: 0x04002B78 RID: 11128
		BestFitEnabled = 16,
		// Token: 0x04002B79 RID: 11129
		BestFitDisabled = 32,
		// Token: 0x04002B7A RID: 11130
		BestFitUseAsm = 48,
		// Token: 0x04002B7B RID: 11131
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x04002B7C RID: 11132
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x04002B7D RID: 11133
		ThrowOnUnmappableCharDisabled = 8192,
		// Token: 0x04002B7E RID: 11134
		ThrowOnUnmappableCharUseAsm = 12288,
		// Token: 0x04002B7F RID: 11135
		CallConvMask = 1792,
		// Token: 0x04002B80 RID: 11136
		CallConvWinapi = 256,
		// Token: 0x04002B81 RID: 11137
		CallConvCdecl = 512,
		// Token: 0x04002B82 RID: 11138
		CallConvStdcall = 768,
		// Token: 0x04002B83 RID: 11139
		CallConvThiscall = 1024,
		// Token: 0x04002B84 RID: 11140
		CallConvFastcall = 1280
	}
}
