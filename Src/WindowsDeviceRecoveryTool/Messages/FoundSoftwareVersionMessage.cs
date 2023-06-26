using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000A6 RID: 166
	public class FoundSoftwareVersionMessage
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0001B4DA File Offset: 0x000196DA
		public FoundSoftwareVersionMessage(bool status, PackageFileInfo packageFileInfo)
		{
			this.Status = status;
			this.PackageFileInfo = packageFileInfo;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001B4F4 File Offset: 0x000196F4
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0001B4FC File Offset: 0x000196FC
		public PackageFileInfo PackageFileInfo { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001B505 File Offset: 0x00019705
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001B50D File Offset: 0x0001970D
		public bool Status { get; set; }
	}
}
