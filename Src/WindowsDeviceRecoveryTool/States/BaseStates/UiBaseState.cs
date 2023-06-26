using System;
using System.Collections.Generic;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.States.BaseStates
{
	// Token: 0x02000079 RID: 121
	public abstract class UiBaseState : BaseState
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00015B45 File Offset: 0x00013D45
		public void ShowRegions(params string[] regions)
		{
			this.VisibleRegions.AddRange(regions);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00015B55 File Offset: 0x00013D55
		public void HideRegions(params string[] regions)
		{
			this.InvisibleRegions.AddRange(regions);
		}

		// Token: 0x040001CD RID: 461
		protected readonly List<string> VisibleRegions = new List<string>();

		// Token: 0x040001CE RID: 462
		protected readonly List<string> InvisibleRegions = new List<string>();
	}
}
