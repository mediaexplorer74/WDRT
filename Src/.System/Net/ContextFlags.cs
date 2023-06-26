using System;

namespace System.Net
{
	// Token: 0x020001CE RID: 462
	[Flags]
	internal enum ContextFlags
	{
		// Token: 0x04001490 RID: 5264
		Zero = 0,
		// Token: 0x04001491 RID: 5265
		Delegate = 1,
		// Token: 0x04001492 RID: 5266
		MutualAuth = 2,
		// Token: 0x04001493 RID: 5267
		ReplayDetect = 4,
		// Token: 0x04001494 RID: 5268
		SequenceDetect = 8,
		// Token: 0x04001495 RID: 5269
		Confidentiality = 16,
		// Token: 0x04001496 RID: 5270
		UseSessionKey = 32,
		// Token: 0x04001497 RID: 5271
		AllocateMemory = 256,
		// Token: 0x04001498 RID: 5272
		Connection = 2048,
		// Token: 0x04001499 RID: 5273
		InitExtendedError = 16384,
		// Token: 0x0400149A RID: 5274
		AcceptExtendedError = 32768,
		// Token: 0x0400149B RID: 5275
		InitStream = 32768,
		// Token: 0x0400149C RID: 5276
		AcceptStream = 65536,
		// Token: 0x0400149D RID: 5277
		InitIntegrity = 65536,
		// Token: 0x0400149E RID: 5278
		AcceptIntegrity = 131072,
		// Token: 0x0400149F RID: 5279
		InitManualCredValidation = 524288,
		// Token: 0x040014A0 RID: 5280
		InitUseSuppliedCreds = 128,
		// Token: 0x040014A1 RID: 5281
		InitIdentify = 131072,
		// Token: 0x040014A2 RID: 5282
		AcceptIdentify = 524288,
		// Token: 0x040014A3 RID: 5283
		ProxyBindings = 67108864,
		// Token: 0x040014A4 RID: 5284
		AllowMissingBindings = 268435456,
		// Token: 0x040014A5 RID: 5285
		UnverifiedTargetName = 536870912
	}
}
