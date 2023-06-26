using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.HtcAdaptation.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.HtcAdaptation
{
	// Token: 0x02000003 RID: 3
	[Export]
	internal sealed class HtcModelsCatalog
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		[ImportingConstructor]
		public HtcModelsCatalog()
		{
			this.Initialize();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002123 File Offset: 0x00000323
		internal ModelInfo[] Models { get; private set; }

		// Token: 0x0600000B RID: 11 RVA: 0x0000212C File Offset: 0x0000032C
		private void Initialize()
		{
			this.Models = new ModelInfo[]
			{
				new ModelInfo(Resources.FriendlyName_HTC_One, Resources.HTCOne, HtcModelsCatalog.HtcOneNormalModeVidPids),
				new ModelInfo(Resources.FriendlyName_HTC_8X, Resources.HTC8X, HtcModelsCatalog.HtcXNomralModeVidPids)
			};
		}

		// Token: 0x04000004 RID: 4
		private static readonly VidPidPair[] HtcOneNormalModeVidPids = new VidPidPair[]
		{
			new VidPidPair("0BB4", "0BAC"),
			new VidPidPair("0BB4", "0BAD"),
			new VidPidPair("0BB4", "0BAE")
		};

		// Token: 0x04000005 RID: 5
		private static readonly VidPidPair[] HtcXNomralModeVidPids = new VidPidPair[]
		{
			new VidPidPair("0BB4", "0BA1"),
			new VidPidPair("0BB4", "0BAB"),
			new VidPidPair("0BB4", "0BA3"),
			new VidPidPair("0BB4", "0BA4"),
			new VidPidPair("0BB4", "F0CA")
		};
	}
}
