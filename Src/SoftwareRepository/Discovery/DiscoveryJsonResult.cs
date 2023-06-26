using System;
using System.Net;

namespace SoftwareRepository.Discovery
{
	// Token: 0x02000020 RID: 32
	public class DiscoveryJsonResult
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000045DE File Offset: 0x000027DE
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x000045E6 File Offset: 0x000027E6
		public string Result { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000045EF File Offset: 0x000027EF
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000045F7 File Offset: 0x000027F7
		public HttpStatusCode StatusCode { get; set; }
	}
}
