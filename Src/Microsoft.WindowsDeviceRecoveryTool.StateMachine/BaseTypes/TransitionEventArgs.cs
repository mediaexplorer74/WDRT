using System;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes
{
	// Token: 0x02000016 RID: 22
	public class TransitionEventArgs : EventArgs
	{
		// Token: 0x0600008D RID: 141 RVA: 0x000038FD File Offset: 0x00001AFD
		public TransitionEventArgs(string status)
		{
			this.Status = status;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003910 File Offset: 0x00001B10
		public new static TransitionEventArgs Empty
		{
			get
			{
				return new TransitionEventArgs(string.Empty);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000392C File Offset: 0x00001B2C
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003934 File Offset: 0x00001B34
		public string Status { get; private set; }
	}
}
