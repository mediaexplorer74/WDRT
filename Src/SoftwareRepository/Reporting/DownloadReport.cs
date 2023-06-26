using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SoftwareRepository.Reporting
{
	// Token: 0x0200001C RID: 28
	[DataContract]
	internal class DownloadReport
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000040B9 File Offset: 0x000022B9
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000040C1 File Offset: 0x000022C1
		[DataMember(Name = "api-version")]
		internal string ApiVersion { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000040CA File Offset: 0x000022CA
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000040D2 File Offset: 0x000022D2
		[DataMember(Name = "id")]
		internal string Id { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000040DB File Offset: 0x000022DB
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000040E3 File Offset: 0x000022E3
		[DataMember(Name = "fileName")]
		internal string FileName { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000040EC File Offset: 0x000022EC
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000040F4 File Offset: 0x000022F4
		[DataMember(Name = "url")]
		internal List<string> Url { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000040FD File Offset: 0x000022FD
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004105 File Offset: 0x00002305
		[DataMember(Name = "status")]
		internal int Status { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000410E File Offset: 0x0000230E
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004116 File Offset: 0x00002316
		[DataMember(Name = "time")]
		internal long Time { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000411F File Offset: 0x0000231F
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004127 File Offset: 0x00002327
		[DataMember(Name = "size")]
		internal long Size { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004130 File Offset: 0x00002330
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004138 File Offset: 0x00002338
		[DataMember(Name = "connections")]
		internal int Connections { get; set; }
	}
}
