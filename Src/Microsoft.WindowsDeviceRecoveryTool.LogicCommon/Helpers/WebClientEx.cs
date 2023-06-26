using System;
using System.Net;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers
{
	// Token: 0x02000039 RID: 57
	public class WebClientEx : WebClient
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000D0B7 File Offset: 0x0000B2B7
		public WebClientEx(int timeoutInMiliseconds = 30000)
		{
			this.timeout = timeoutInMiliseconds;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000D0C8 File Offset: 0x0000B2C8
		protected override WebRequest GetWebRequest(Uri address)
		{
			WebRequest webRequest = base.GetWebRequest(address);
			webRequest.Timeout = this.timeout;
			return webRequest;
		}

		// Token: 0x04000184 RID: 388
		private readonly int timeout;
	}
}
