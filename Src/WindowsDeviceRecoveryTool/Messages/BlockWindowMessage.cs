using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x0200009C RID: 156
	public class BlockWindowMessage
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x0001B34A File Offset: 0x0001954A
		public BlockWindowMessage(bool block, string message = null, string title = null)
		{
			this.Block = block;
			this.Message = message;
			this.Title = title;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001B36C File Offset: 0x0001956C
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0001B374 File Offset: 0x00019574
		public bool Block { get; private set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001B37D File Offset: 0x0001957D
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0001B385 File Offset: 0x00019585
		public string Message { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001B38E File Offset: 0x0001958E
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0001B396 File Offset: 0x00019596
		public string Title { get; private set; }
	}
}
