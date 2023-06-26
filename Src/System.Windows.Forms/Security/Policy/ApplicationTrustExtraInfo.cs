using System;

namespace System.Security.Policy
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	internal class ApplicationTrustExtraInfo
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000DB35 File Offset: 0x0000BD35
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000DB3D File Offset: 0x0000BD3D
		public bool RequestsShellIntegration
		{
			get
			{
				return this.requestsShellIntegration;
			}
			set
			{
				this.requestsShellIntegration = value;
			}
		}

		// Token: 0x04000434 RID: 1076
		private bool requestsShellIntegration = true;
	}
}
