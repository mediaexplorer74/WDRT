using System;
using System.Collections.Generic;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000A9 RID: 169
	public class FlashResultMessage
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x0001B586 File Offset: 0x00019786
		public FlashResultMessage(bool result)
		{
			this.Result = result;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001B598 File Offset: 0x00019798
		public FlashResultMessage(bool result, List<string> extendedMessage)
		{
			this.Result = result;
			this.ExtendedMessage = extendedMessage;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001B5B2 File Offset: 0x000197B2
		public FlashResultMessage(bool result, List<string> extendedMessage, string argument)
		{
			this.Result = result;
			this.ExtendedMessage = extendedMessage;
			this.Argument = argument;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001B5D4 File Offset: 0x000197D4
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0001B5DC File Offset: 0x000197DC
		public bool Result { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001B5E5 File Offset: 0x000197E5
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0001B5ED File Offset: 0x000197ED
		public List<string> ExtendedMessage { get; private set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001B5F6 File Offset: 0x000197F6
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x0001B5FE File Offset: 0x000197FE
		public string Argument { get; private set; }
	}
}
