using System;
using System.Diagnostics;

namespace System.ComponentModel
{
	// Token: 0x020000F7 RID: 247
	internal static class CoreSwitches
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000C30F File Offset: 0x0000A50F
		public static BooleanSwitch PerfTrack
		{
			get
			{
				if (CoreSwitches.perfTrack == null)
				{
					CoreSwitches.perfTrack = new BooleanSwitch("PERFTRACK", "Debug performance critical sections.");
				}
				return CoreSwitches.perfTrack;
			}
		}

		// Token: 0x04000424 RID: 1060
		private static BooleanSwitch perfTrack;
	}
}
