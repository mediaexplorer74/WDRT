using System;
using System.Net;
using System.Net.NetworkInformation;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers
{
	// Token: 0x02000031 RID: 49
	public interface IEnvironmentInfo
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600031C RID: 796
		string UserSiteLanguage { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600031D RID: 797
		PhysicalAddress[] PhysicalAddressList { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600031E RID: 798
		IPAddress[] IPAddressList { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600031F RID: 799
		string ApplicationName { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000320 RID: 800
		string ApplicationVersion { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000321 RID: 801
		string ApplicationVendor { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000322 RID: 802
		string NasVersion { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000323 RID: 803
		string PapiVersion { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000324 RID: 804
		string CapiVersion { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000325 RID: 805
		string FuseVersion { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000326 RID: 806
		string OSVersion { get; }
	}
}
