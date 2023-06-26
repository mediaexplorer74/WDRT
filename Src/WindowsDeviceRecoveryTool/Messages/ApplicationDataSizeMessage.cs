using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x02000099 RID: 153
	public class ApplicationDataSizeMessage
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0001B2C8 File Offset: 0x000194C8
		public ApplicationDataSizeMessage(ApplicationDataSizeMessage.DataType type, long filesSize)
		{
			this.Type = type;
			this.FilesSize = filesSize;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001B2E2 File Offset: 0x000194E2
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0001B2EA File Offset: 0x000194EA
		public long FilesSize { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001B2F3 File Offset: 0x000194F3
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0001B2FB File Offset: 0x000194FB
		public ApplicationDataSizeMessage.DataType Type { get; private set; }

		// Token: 0x02000141 RID: 321
		public enum DataType
		{
			// Token: 0x04000407 RID: 1031
			Logs,
			// Token: 0x04000408 RID: 1032
			Reports,
			// Token: 0x04000409 RID: 1033
			Packages
		}
	}
}
