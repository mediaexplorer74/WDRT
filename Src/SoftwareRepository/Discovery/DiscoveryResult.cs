using System;
using System.Net;

namespace SoftwareRepository.Discovery
{
	// Token: 0x02000022 RID: 34
	public class DiscoveryResult
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004743 File Offset: 0x00002943
		// (set) Token: 0x06000115 RID: 277 RVA: 0x0000474B File Offset: 0x0000294B
		public SoftwarePackages Result { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004754 File Offset: 0x00002954
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000475C File Offset: 0x0000295C
		public HttpStatusCode StatusCode { get; set; }
	}
}
