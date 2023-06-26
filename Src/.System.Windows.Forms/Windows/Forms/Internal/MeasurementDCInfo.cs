using System;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004DE RID: 1246
	internal static class MeasurementDCInfo
	{
		// Token: 0x0600513A RID: 20794 RVA: 0x00152EBC File Offset: 0x001510BC
		internal static bool IsMeasurementDC(DeviceContext dc)
		{
			WindowsGraphics currentMeasurementGraphics = WindowsGraphicsCacheManager.GetCurrentMeasurementGraphics();
			return currentMeasurementGraphics != null && currentMeasurementGraphics.DeviceContext != null && currentMeasurementGraphics.DeviceContext.Hdc == dc.Hdc;
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x0600513B RID: 20795 RVA: 0x00152EF2 File Offset: 0x001510F2
		// (set) Token: 0x0600513C RID: 20796 RVA: 0x00152F07 File Offset: 0x00151107
		internal static WindowsFont LastUsedFont
		{
			get
			{
				if (MeasurementDCInfo.cachedMeasurementDCInfo != null)
				{
					return MeasurementDCInfo.cachedMeasurementDCInfo.LastUsedFont;
				}
				return null;
			}
			set
			{
				if (MeasurementDCInfo.cachedMeasurementDCInfo == null)
				{
					MeasurementDCInfo.cachedMeasurementDCInfo = new MeasurementDCInfo.CachedInfo();
				}
				MeasurementDCInfo.cachedMeasurementDCInfo.UpdateFont(value);
			}
		}

		// Token: 0x0600513D RID: 20797 RVA: 0x00152F28 File Offset: 0x00151128
		internal static IntNativeMethods.DRAWTEXTPARAMS GetTextMargins(WindowsGraphics wg, WindowsFont font)
		{
			MeasurementDCInfo.CachedInfo cachedInfo = MeasurementDCInfo.cachedMeasurementDCInfo;
			if (cachedInfo != null && cachedInfo.LeftTextMargin > 0 && cachedInfo.RightTextMargin > 0 && font == cachedInfo.LastUsedFont)
			{
				return new IntNativeMethods.DRAWTEXTPARAMS(cachedInfo.LeftTextMargin, cachedInfo.RightTextMargin);
			}
			if (cachedInfo == null)
			{
				cachedInfo = new MeasurementDCInfo.CachedInfo();
				MeasurementDCInfo.cachedMeasurementDCInfo = cachedInfo;
			}
			IntNativeMethods.DRAWTEXTPARAMS textMargins = wg.GetTextMargins(font);
			cachedInfo.LeftTextMargin = textMargins.iLeftMargin;
			cachedInfo.RightTextMargin = textMargins.iRightMargin;
			return new IntNativeMethods.DRAWTEXTPARAMS(cachedInfo.LeftTextMargin, cachedInfo.RightTextMargin);
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x00152FAC File Offset: 0x001511AC
		internal static void ResetIfIsMeasurementDC(IntPtr hdc)
		{
			WindowsGraphics currentMeasurementGraphics = WindowsGraphicsCacheManager.GetCurrentMeasurementGraphics();
			if (currentMeasurementGraphics != null && currentMeasurementGraphics.DeviceContext != null && currentMeasurementGraphics.DeviceContext.Hdc == hdc)
			{
				MeasurementDCInfo.CachedInfo cachedInfo = MeasurementDCInfo.cachedMeasurementDCInfo;
				if (cachedInfo != null)
				{
					cachedInfo.UpdateFont(null);
				}
			}
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x00152FF0 File Offset: 0x001511F0
		internal static void Reset()
		{
			MeasurementDCInfo.CachedInfo cachedInfo = MeasurementDCInfo.cachedMeasurementDCInfo;
			if (cachedInfo != null)
			{
				cachedInfo.UpdateFont(null);
			}
		}

		// Token: 0x040035AD RID: 13741
		[ThreadStatic]
		private static MeasurementDCInfo.CachedInfo cachedMeasurementDCInfo;

		// Token: 0x0200087C RID: 2172
		private sealed class CachedInfo
		{
			// Token: 0x06007149 RID: 29001 RVA: 0x0019E2D8 File Offset: 0x0019C4D8
			internal void UpdateFont(WindowsFont font)
			{
				if (this.LastUsedFont != font)
				{
					this.LastUsedFont = font;
					this.LeftTextMargin = -1;
					this.RightTextMargin = -1;
				}
			}

			// Token: 0x04004470 RID: 17520
			public WindowsFont LastUsedFont;

			// Token: 0x04004471 RID: 17521
			public int LeftTextMargin;

			// Token: 0x04004472 RID: 17522
			public int RightTextMargin;
		}
	}
}
