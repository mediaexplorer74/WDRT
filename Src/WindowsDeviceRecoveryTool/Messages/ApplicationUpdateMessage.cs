using System;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x02000098 RID: 152
	public class ApplicationUpdateMessage
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x0001B2A5 File Offset: 0x000194A5
		public ApplicationUpdateMessage(ApplicationUpdate update)
		{
			this.Update = update;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001B2B7 File Offset: 0x000194B7
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0001B2BF File Offset: 0x000194BF
		public ApplicationUpdate Update { get; set; }
	}
}
