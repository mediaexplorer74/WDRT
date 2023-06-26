using System;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes
{
	// Token: 0x02000012 RID: 18
	public class BaseStateMachineErrorEventArgs : EventArgs
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00003831 File Offset: 0x00001A31
		public BaseStateMachineErrorEventArgs(Error error)
		{
			this.Error = error;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003843 File Offset: 0x00001A43
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000384B File Offset: 0x00001A4B
		public Error Error { get; private set; }
	}
}
