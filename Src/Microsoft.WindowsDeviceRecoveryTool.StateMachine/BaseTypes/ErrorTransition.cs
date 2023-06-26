using System;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes
{
	// Token: 0x02000015 RID: 21
	public class ErrorTransition
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000038DA File Offset: 0x00001ADA
		public ErrorTransition(BaseErrorState next)
		{
			this.Next = next;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000038EC File Offset: 0x00001AEC
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000038F4 File Offset: 0x00001AF4
		public virtual BaseErrorState Next { get; private set; }
	}
}
