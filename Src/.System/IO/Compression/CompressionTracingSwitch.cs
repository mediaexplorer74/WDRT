using System;
using System.Diagnostics;

namespace System.IO.Compression
{
	// Token: 0x0200041A RID: 1050
	internal class CompressionTracingSwitch : Switch
	{
		// Token: 0x06002755 RID: 10069 RVA: 0x000B563B File Offset: 0x000B383B
		internal CompressionTracingSwitch(string displayName, string description)
			: base(displayName, description)
		{
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x000B5645 File Offset: 0x000B3845
		public static bool Verbose
		{
			get
			{
				return CompressionTracingSwitch.tracingSwitch.SwitchSetting >= 2;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x000B5657 File Offset: 0x000B3857
		public static bool Informational
		{
			get
			{
				return CompressionTracingSwitch.tracingSwitch.SwitchSetting >= 1;
			}
		}

		// Token: 0x04002151 RID: 8529
		internal static readonly CompressionTracingSwitch tracingSwitch = new CompressionTracingSwitch("CompressionSwitch", "Compression Library Tracing Switch");
	}
}
