using System;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes
{
	// Token: 0x02000017 RID: 23
	public class BaseErrorState : BaseState
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000393D File Offset: 0x00001B3D
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003945 File Offset: 0x00001B45
		public new Error Error { get; private set; }

		// Token: 0x06000093 RID: 147 RVA: 0x0000394E File Offset: 0x00001B4E
		public sealed override void Start()
		{
			base.Start();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003958 File Offset: 0x00001B58
		public virtual void Start(Error error)
		{
			this.Error = error;
			Tracer<BaseErrorState>.WriteInformation("Started Error state for error: " + error.Message);
			base.Start();
		}
	}
}
