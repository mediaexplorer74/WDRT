using System;
using System.Text;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage
{
	// Token: 0x0200004A RID: 74
	public class DownloadParameters
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000669B File Offset: 0x0000489B
		// (set) Token: 0x06000243 RID: 579 RVA: 0x000066A3 File Offset: 0x000048A3
		public QueryParameters DiscoveryParameters { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000066AC File Offset: 0x000048AC
		// (set) Token: 0x06000245 RID: 581 RVA: 0x000066B4 File Offset: 0x000048B4
		public string FileExtension { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000066BD File Offset: 0x000048BD
		// (set) Token: 0x06000247 RID: 583 RVA: 0x000066C5 File Offset: 0x000048C5
		public string DestinationFolder { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000066CE File Offset: 0x000048CE
		// (set) Token: 0x06000249 RID: 585 RVA: 0x000066D6 File Offset: 0x000048D6
		public bool FilesVersioned { get; set; }

		// Token: 0x0600024A RID: 586 RVA: 0x000066E0 File Offset: 0x000048E0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("FileExtension: {0}, ", this.FileExtension);
			stringBuilder.AppendFormat("DestinationFolder: {0}, ", this.DestinationFolder);
			stringBuilder.AppendFormat("DiscoveryParameters: {0}, ", this.DiscoveryParameters);
			return stringBuilder.ToString();
		}
	}
}
