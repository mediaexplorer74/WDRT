using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A9 RID: 1705
	internal enum StoreTransactionOperationType
	{
		// Token: 0x04002262 RID: 8802
		Invalid,
		// Token: 0x04002263 RID: 8803
		SetCanonicalizationContext = 14,
		// Token: 0x04002264 RID: 8804
		StageComponent = 20,
		// Token: 0x04002265 RID: 8805
		PinDeployment,
		// Token: 0x04002266 RID: 8806
		UnpinDeployment,
		// Token: 0x04002267 RID: 8807
		StageComponentFile,
		// Token: 0x04002268 RID: 8808
		InstallDeployment,
		// Token: 0x04002269 RID: 8809
		UninstallDeployment,
		// Token: 0x0400226A RID: 8810
		SetDeploymentMetadata,
		// Token: 0x0400226B RID: 8811
		Scavenge
	}
}
