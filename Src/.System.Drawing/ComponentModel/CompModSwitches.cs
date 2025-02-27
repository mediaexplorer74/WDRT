﻿using System;
using System.Diagnostics;

namespace System.ComponentModel
{
	// Token: 0x020000EC RID: 236
	internal static class CompModSwitches
	{
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0002BC10 File Offset: 0x00029E10
		public static TraceSwitch HandleLeak
		{
			get
			{
				if (CompModSwitches.handleLeak == null)
				{
					CompModSwitches.handleLeak = new TraceSwitch("HANDLELEAK", "HandleCollector: Track Win32 Handle Leaks");
				}
				return CompModSwitches.handleLeak;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0002BC32 File Offset: 0x00029E32
		public static BooleanSwitch TraceCollect
		{
			get
			{
				if (CompModSwitches.traceCollect == null)
				{
					CompModSwitches.traceCollect = new BooleanSwitch("TRACECOLLECT", "HandleCollector: Trace HandleCollector operations");
				}
				return CompModSwitches.traceCollect;
			}
		}

		// Token: 0x04000AD1 RID: 2769
		private static TraceSwitch handleLeak;

		// Token: 0x04000AD2 RID: 2770
		private static BooleanSwitch traceCollect;
	}
}
