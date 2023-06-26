using System;
using System.Collections.Generic;

namespace System.Data.Services.Client
{
	// Token: 0x0200003C RID: 60
	public class DataServiceClientRequestMessageArgs
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x0000AC38 File Offset: 0x00008E38
		public DataServiceClientRequestMessageArgs(string method, Uri requestUri, bool useDefaultCredentials, bool usePostTunneling, IDictionary<string, string> headers)
		{
			this.Headers = headers;
			this.Method = method;
			this.RequestUri = requestUri;
			this.UsePostTunneling = usePostTunneling;
			this.UseDefaultCredentials = useDefaultCredentials;
			this.actualMethod = this.Method;
			if (this.UsePostTunneling && this.Headers.ContainsKey("X-HTTP-Method"))
			{
				this.actualMethod = "POST";
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000ACA1 File Offset: 0x00008EA1
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000ACA9 File Offset: 0x00008EA9
		public string Method { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000ACB2 File Offset: 0x00008EB2
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000ACBA File Offset: 0x00008EBA
		public Uri RequestUri { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000ACC3 File Offset: 0x00008EC3
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000ACCB File Offset: 0x00008ECB
		public bool UsePostTunneling { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000ACDC File Offset: 0x00008EDC
		public IDictionary<string, string> Headers { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000ACE5 File Offset: 0x00008EE5
		public string ActualMethod
		{
			get
			{
				return this.actualMethod;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000ACED File Offset: 0x00008EED
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000ACF5 File Offset: 0x00008EF5
		public bool UseDefaultCredentials { get; private set; }

		// Token: 0x04000215 RID: 533
		private readonly string actualMethod;
	}
}
