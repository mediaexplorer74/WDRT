using System;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes
{
	// Token: 0x02000013 RID: 19
	public abstract class BaseTransition
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003854 File Offset: 0x00001A54
		protected BaseTransition(BaseState next)
		{
			this.Next = next;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003866 File Offset: 0x00001A66
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000386E File Offset: 0x00001A6E
		public virtual BaseState Next { get; protected set; }

		// Token: 0x06000084 RID: 132 RVA: 0x00003878 File Offset: 0x00001A78
		public override string ToString()
		{
			string text = base.ToString();
			return text.Substring(text.LastIndexOf('.') + 1);
		}

		// Token: 0x06000085 RID: 133
		public abstract bool ConditionsAreMet(object sender, TransitionEventArgs eventArgs);
	}
}
