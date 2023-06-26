using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C4 RID: 196
	public class JsonSelectSettings
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002AC8C File Offset: 0x00028E8C
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x0002AC94 File Offset: 0x00028E94
		public TimeSpan? RegexMatchTimeout { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0002AC9D File Offset: 0x00028E9D
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x0002ACA5 File Offset: 0x00028EA5
		public bool ErrorWhenNoMatch { get; set; }
	}
}
