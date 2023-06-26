using System;
using System.Collections.Generic;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x0200009D RID: 157
	public class CompatibleFfuFilesMessage
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x0001B39F File Offset: 0x0001959F
		public CompatibleFfuFilesMessage(List<PackageFileInfo> packages)
		{
			this.Packages = packages;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001B3B1 File Offset: 0x000195B1
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001B3B9 File Offset: 0x000195B9
		public List<PackageFileInfo> Packages { get; set; }
	}
}
