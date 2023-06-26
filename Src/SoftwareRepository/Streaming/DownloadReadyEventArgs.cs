using System;

namespace SoftwareRepository.Streaming
{
	// Token: 0x02000010 RID: 16
	public class DownloadReadyEventArgs : EventArgs
	{
		// Token: 0x0600005E RID: 94 RVA: 0x0000359C File Offset: 0x0000179C
		public DownloadReadyEventArgs(string packageId, string fileName)
		{
			this.PackageId = packageId;
			this.FileName = fileName;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000035B6 File Offset: 0x000017B6
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000035BE File Offset: 0x000017BE
		public string PackageId { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000035C7 File Offset: 0x000017C7
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000035CF File Offset: 0x000017CF
		public string FileName { get; set; }
	}
}
