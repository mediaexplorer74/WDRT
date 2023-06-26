using System;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes
{
	// Token: 0x02000009 RID: 9
	public class EndState : BaseState
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000022F0 File Offset: 0x000004F0
		public EndState()
		{
			this.Status = string.Empty;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002306 File Offset: 0x00000506
		public EndState(string status)
		{
			this.Status = status;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002318 File Offset: 0x00000518
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002320 File Offset: 0x00000520
		public string Status { get; private set; }

		// Token: 0x0600001B RID: 27 RVA: 0x00002329 File Offset: 0x00000529
		public override void Start()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002329 File Offset: 0x00000529
		public override void Stop()
		{
		}
	}
}
