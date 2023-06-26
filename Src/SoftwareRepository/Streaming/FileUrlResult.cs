using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using SoftwareRepository.Discovery;

namespace SoftwareRepository.Streaming
{
	// Token: 0x02000017 RID: 23
	[DataContract]
	internal class FileUrlResult
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003C8B File Offset: 0x00001E8B
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003C93 File Offset: 0x00001E93
		[DataMember(Name = "url")]
		internal string Url { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003C9C File Offset: 0x00001E9C
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003CA4 File Offset: 0x00001EA4
		[DataMember(Name = "alternateUrl")]
		internal List<string> AlternateUrl { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003CAD File Offset: 0x00001EAD
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003CB5 File Offset: 0x00001EB5
		[DataMember(Name = "fileSize")]
		internal long FileSize { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003CBE File Offset: 0x00001EBE
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003CC6 File Offset: 0x00001EC6
		[DataMember(Name = "checksum")]
		internal List<SoftwareFileChecksum> Checksum { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003CCF File Offset: 0x00001ECF
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003CD7 File Offset: 0x00001ED7
		internal HttpStatusCode StatusCode { get; set; }

		// Token: 0x060000A0 RID: 160 RVA: 0x00003CE0 File Offset: 0x00001EE0
		internal List<string> GetFileUrls()
		{
			List<string> list = new List<string>();
			bool flag = !string.IsNullOrEmpty(this.Url);
			if (flag)
			{
				list.Add(this.Url);
			}
			bool flag2 = this.AlternateUrl != null;
			if (flag2)
			{
				foreach (string text in this.AlternateUrl)
				{
					bool flag3 = !string.IsNullOrEmpty(text);
					if (flag3)
					{
						list.Add(text);
					}
				}
			}
			return list;
		}
	}
}
