using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.BaseStates
{
	// Token: 0x02000078 RID: 120
	[Export]
	public class Conditions
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x00015AE0 File Offset: 0x00013CE0
		[ImportingConstructor]
		public Conditions(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.appContext = appContext;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00015AF4 File Offset: 0x00013CF4
		public bool CanChangeToFlashingState()
		{
			return this.appContext.CurrentPhone != null;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00015B14 File Offset: 0x00013D14
		public bool IsHtcConnected()
		{
			return this.appContext.SelectedManufacturer == PhoneTypes.Htc && this.appContext.CurrentPhone != null;
		}

		// Token: 0x040001CC RID: 460
		private readonly Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;
	}
}
