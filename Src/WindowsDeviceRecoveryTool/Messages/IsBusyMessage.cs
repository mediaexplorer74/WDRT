using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000B4 RID: 180
	public class IsBusyMessage
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x0001B86E File Offset: 0x00019A6E
		public IsBusyMessage(bool isBusy, string message = "")
		{
			this.IsBusy = isBusy;
			this.Message = message;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001B888 File Offset: 0x00019A88
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0001B890 File Offset: 0x00019A90
		public bool IsBusy { get; private set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001B899 File Offset: 0x00019A99
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0001B8A1 File Offset: 0x00019AA1
		public string Message { get; private set; }
	}
}
