using System;
using System.Drawing;
using System.Linq;
using Microsoft.WindowsDeviceRecoveryTool.Core;

namespace Microsoft.WindowsDeviceRecoveryTool.HtcAdaptation
{
	// Token: 0x02000002 RID: 2
	internal sealed class ModelInfo
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ModelInfo(string friendlyName, Bitmap bitmap, params VidPidPair[] vidPidPairs)
		{
			bool flag = friendlyName == null;
			if (flag)
			{
				throw new ArgumentNullException("friendlyName");
			}
			bool flag2 = bitmap == null;
			if (flag2)
			{
				throw new ArgumentNullException("bitmap");
			}
			bool flag3 = vidPidPairs == null;
			if (flag3)
			{
				throw new ArgumentNullException("vidPidPairs");
			}
			VidPidPair[] array = vidPidPairs.ToArray<VidPidPair>();
			bool flag4 = array.Length == 0;
			if (flag4)
			{
				throw new ArgumentException("vidPidPairs should have at least one element");
			}
			this.FriendlyName = friendlyName;
			this.Bitmap = bitmap;
			this.VidPidPairs = array;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D7 File Offset: 0x000002D7
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020DF File Offset: 0x000002DF
		public string FriendlyName { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020F0 File Offset: 0x000002F0
		public Bitmap Bitmap { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020F9 File Offset: 0x000002F9
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002101 File Offset: 0x00000301
		public VidPidPair[] VidPidPairs { get; private set; }
	}
}
