using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200005A RID: 90
	internal enum StoreTransactionOperationType
	{
		// Token: 0x04000182 RID: 386
		Invalid,
		// Token: 0x04000183 RID: 387
		SetCanonicalizationContext = 14,
		// Token: 0x04000184 RID: 388
		StageComponent = 20,
		// Token: 0x04000185 RID: 389
		PinDeployment,
		// Token: 0x04000186 RID: 390
		UnpinDeployment,
		// Token: 0x04000187 RID: 391
		StageComponentFile,
		// Token: 0x04000188 RID: 392
		InstallDeployment,
		// Token: 0x04000189 RID: 393
		UninstallDeployment,
		// Token: 0x0400018A RID: 394
		SetDeploymentMetadata,
		// Token: 0x0400018B RID: 395
		Scavenge
	}
}
