using System;

namespace System.Net
{
	// Token: 0x0200011F RID: 287
	internal enum ContextAttribute
	{
		// Token: 0x04000FAF RID: 4015
		Sizes,
		// Token: 0x04000FB0 RID: 4016
		Names,
		// Token: 0x04000FB1 RID: 4017
		Lifespan,
		// Token: 0x04000FB2 RID: 4018
		DceInfo,
		// Token: 0x04000FB3 RID: 4019
		StreamSizes,
		// Token: 0x04000FB4 RID: 4020
		Authority = 6,
		// Token: 0x04000FB5 RID: 4021
		PackageInfo = 10,
		// Token: 0x04000FB6 RID: 4022
		NegotiationInfo = 12,
		// Token: 0x04000FB7 RID: 4023
		UniqueBindings = 25,
		// Token: 0x04000FB8 RID: 4024
		EndpointBindings,
		// Token: 0x04000FB9 RID: 4025
		ClientSpecifiedSpn,
		// Token: 0x04000FBA RID: 4026
		RemoteCertificate = 83,
		// Token: 0x04000FBB RID: 4027
		LocalCertificate,
		// Token: 0x04000FBC RID: 4028
		RootStore,
		// Token: 0x04000FBD RID: 4029
		IssuerListInfoEx = 89,
		// Token: 0x04000FBE RID: 4030
		ConnectionInfo,
		// Token: 0x04000FBF RID: 4031
		UiInfo = 104
	}
}
