using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000529 RID: 1321
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class CompModSwitches
	{
		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x000E00E1 File Offset: 0x000DE2E1
		public static BooleanSwitch CommonDesignerServices
		{
			get
			{
				if (CompModSwitches.commonDesignerServices == null)
				{
					CompModSwitches.commonDesignerServices = new BooleanSwitch("CommonDesignerServices", "Assert if any common designer service is not found.");
				}
				return CompModSwitches.commonDesignerServices;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x000E0109 File Offset: 0x000DE309
		public static TraceSwitch EventLog
		{
			get
			{
				if (CompModSwitches.eventLog == null)
				{
					CompModSwitches.eventLog = new TraceSwitch("EventLog", "Enable tracing for the EventLog component.");
				}
				return CompModSwitches.eventLog;
			}
		}

		// Token: 0x04002944 RID: 10564
		private static volatile BooleanSwitch commonDesignerServices;

		// Token: 0x04002945 RID: 10565
		private static volatile TraceSwitch eventLog;
	}
}
