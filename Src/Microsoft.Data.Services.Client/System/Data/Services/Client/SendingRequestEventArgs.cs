using System;
using System.Net;

namespace System.Data.Services.Client
{
	// Token: 0x02000121 RID: 289
	public class SendingRequestEventArgs : EventArgs
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x0002715D File Offset: 0x0002535D
		internal SendingRequestEventArgs(WebRequest request, WebHeaderCollection requestHeaders)
		{
			this.request = request;
			this.requestHeaders = requestHeaders;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00027173 File Offset: 0x00025373
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x0002717B File Offset: 0x0002537B
		public WebRequest Request
		{
			get
			{
				return this.request;
			}
			set
			{
				Util.CheckArgumentNull<WebRequest>(value, "value");
				if (!(value is HttpWebRequest))
				{
					throw Error.Argument(Strings.Context_SendingRequestEventArgsNotHttp, "value");
				}
				this.request = value;
				this.requestHeaders = value.Headers;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x000271B4 File Offset: 0x000253B4
		public WebHeaderCollection RequestHeaders
		{
			get
			{
				return this.requestHeaders;
			}
		}

		// Token: 0x04000595 RID: 1429
		private WebRequest request;

		// Token: 0x04000596 RID: 1430
		private WebHeaderCollection requestHeaders;
	}
}
