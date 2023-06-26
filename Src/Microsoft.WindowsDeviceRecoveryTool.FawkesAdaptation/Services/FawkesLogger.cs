using System;
using System.Collections.Generic;
using ClickerUtilityLibrary.Misc;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.FawkesAdaptation.Services
{
	// Token: 0x02000008 RID: 8
	internal class FawkesLogger : ILogger
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000031CA File Offset: 0x000013CA
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000031D2 File Offset: 0x000013D2
		internal List<string> LoggedErrorMessages { get; private set; }

		// Token: 0x0600003C RID: 60 RVA: 0x000031DB File Offset: 0x000013DB
		public FawkesLogger()
		{
			this.LoggedErrorMessages = new List<string>();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000031EE File Offset: 0x000013EE
		public void LogDebug(string message)
		{
			if (message.Contains("Error"))
			{
				this.LoggedErrorMessages.Add(message);
				Tracer<FawkesLogger>.WriteError(message, new object[0]);
				return;
			}
			Tracer<FawkesLogger>.WriteInformation(message);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000321C File Offset: 0x0000141C
		public void LogInfo(string message)
		{
			Tracer<FawkesLogger>.WriteInformation(message);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003224 File Offset: 0x00001424
		public void LogError(string message)
		{
			this.LoggedErrorMessages.Add(message);
			Tracer<FawkesLogger>.WriteError(message, new object[0]);
		}
	}
}
