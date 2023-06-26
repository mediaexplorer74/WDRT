using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000AD RID: 173
	public class NotificationMessage
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0001B715 File Offset: 0x00019915
		public NotificationMessage(bool showNotification, string header, string text)
		{
			this.ShowNotification = showNotification;
			this.Header = header;
			this.Text = text;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001B737 File Offset: 0x00019937
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001B73F File Offset: 0x0001993F
		public bool ShowNotification { get; private set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001B748 File Offset: 0x00019948
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0001B750 File Offset: 0x00019950
		public string Header { get; private set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001B759 File Offset: 0x00019959
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0001B761 File Offset: 0x00019961
		public string Text { get; private set; }
	}
}
