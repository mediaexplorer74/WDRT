using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000110 RID: 272
	internal static class DpiHelper
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x00014D00 File Offset: 0x00012F00
		private static void Initialize()
		{
			if (DpiHelper.isInitialized)
			{
				return;
			}
			if (DpiHelper.IsDpiAwarenessValueSet())
			{
				DpiHelper.enableHighDpi = true;
			}
			else
			{
				try
				{
					string text = ConfigurationManager.AppSettings.Get("EnableWindowsFormsHighDpiAutoResizing");
					if (!string.IsNullOrEmpty(text) && string.Equals(text, "true", StringComparison.InvariantCultureIgnoreCase))
					{
						DpiHelper.enableHighDpi = true;
					}
				}
				catch
				{
				}
			}
			if (DpiHelper.enableHighDpi)
			{
				try
				{
					DpiHelper.SetWinformsApplicationDpiAwareness();
				}
				catch (Exception ex)
				{
				}
				IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
				if (dc != IntPtr.Zero)
				{
					DpiHelper.deviceDpi = (double)UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 88);
					UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
				}
			}
			DpiHelper.isInitialized = true;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00014DC8 File Offset: 0x00012FC8
		internal static bool IsDpiAwarenessValueSet()
		{
			bool flag = false;
			try
			{
				if (string.IsNullOrEmpty(DpiHelper.dpiAwarenessValue))
				{
					DpiHelper.dpiAwarenessValue = ConfigurationOptions.GetConfigSettingValue("DpiAwareness");
				}
			}
			catch
			{
			}
			if (!string.IsNullOrEmpty(DpiHelper.dpiAwarenessValue))
			{
				string text = DpiHelper.dpiAwarenessValue.ToLowerInvariant();
				if (!(text == "true") && !(text == "system") && !(text == "true/pm") && !(text == "permonitor") && !(text == "permonitorv2"))
				{
					if (!(text == "false"))
					{
					}
				}
				else
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00014E74 File Offset: 0x00013074
		internal static void InitializeDpiHelperForWinforms()
		{
			DpiHelper.Initialize();
			DpiHelper.InitializeDpiHelperQuirks();
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00014E80 File Offset: 0x00013080
		internal static void InitializeDpiHelperQuirks()
		{
			if (DpiHelper.isDpiHelperQuirksInitialized)
			{
				return;
			}
			try
			{
				if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.CompareTo(ConfigurationOptions.RS2Version) >= 0 && DpiHelper.IsExpectedConfigValue("DisableDpiChangedMessageHandling", false) && DpiHelper.IsDpiAwarenessValueSet() && Application.RenderWithVisualStyles)
				{
					DpiHelper.enableDpiChangedMessageHandling = true;
				}
				if ((DpiHelper.IsScalingRequired || DpiHelper.enableDpiChangedMessageHandling) && DpiHelper.IsDpiAwarenessValueSet())
				{
					if (DpiHelper.IsExpectedConfigValue("CheckedListBox.DisableHighDpiImprovements", false))
					{
						DpiHelper.enableCheckedListBoxHighDpiImprovements = true;
					}
					if (DpiHelper.IsExpectedConfigValue("ToolStrip.DisableHighDpiImprovements", false))
					{
						DpiHelper.enableToolStripHighDpiImprovements = true;
					}
					if (DpiHelper.IsExpectedConfigValue("Form.DisableSinglePassScalingOfDpiForms", false))
					{
						DpiHelper.enableSinglePassScalingOfDpiForms = true;
					}
					if (DpiHelper.IsExpectedConfigValue("DataGridView.DisableHighDpiImprovements", false))
					{
						DpiHelper.enableDataGridViewControlHighDpiImprovements = true;
					}
					if (DpiHelper.IsExpectedConfigValue("AnchorLayout.DisableHighDpiImprovements", false))
					{
						DpiHelper.enableAnchorLayoutHighDpiImprovements = true;
					}
					if (DpiHelper.IsExpectedConfigValue("MonthCalendar.DisableHighDpiImprovements", false))
					{
						DpiHelper.enableMonthCalendarHighDpiImprovements = true;
					}
					if (ConfigurationOptions.GetConfigSettingValue("DisableDpiChangedHighDpiImprovements") == null)
					{
						if (ConfigurationOptions.NetFrameworkVersion.CompareTo(DpiHelper.dpiChangedMessageHighDpiImprovementsMinimumFrameworkVersion) >= 0)
						{
							DpiHelper.enableDpiChangedHighDpiImprovements = true;
						}
					}
					else if (DpiHelper.IsExpectedConfigValue("DisableDpiChangedHighDpiImprovements", false))
					{
						DpiHelper.enableDpiChangedHighDpiImprovements = true;
					}
					DpiHelper.enableThreadExceptionDialogHighDpiImprovements = true;
				}
			}
			catch
			{
			}
			DpiHelper.isDpiHelperQuirksInitialized = true;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00014FD0 File Offset: 0x000131D0
		internal static bool IsExpectedConfigValue(string configurationSettingName, bool expectedValue)
		{
			string configSettingValue = ConfigurationOptions.GetConfigSettingValue(configurationSettingName);
			bool flag;
			if (!bool.TryParse(configSettingValue, out flag))
			{
				flag = false;
			}
			return flag == expectedValue;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00014FF4 File Offset: 0x000131F4
		internal static bool EnableDpiChangedHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableDpiChangedHighDpiImprovements;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00015000 File Offset: 0x00013200
		internal static bool EnableToolStripHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableToolStripHighDpiImprovements;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001500C File Offset: 0x0001320C
		internal static bool EnableToolStripPerMonitorV2HighDpiImprovements
		{
			get
			{
				return DpiHelper.EnableDpiChangedMessageHandling && DpiHelper.enableToolStripHighDpiImprovements && DpiHelper.enableDpiChangedHighDpiImprovements;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00015024 File Offset: 0x00013224
		internal static bool EnableDpiChangedMessageHandling
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				if (DpiHelper.enableDpiChangedMessageHandling)
				{
					DpiAwarenessContext threadDpiAwarenessContext = CommonUnsafeNativeMethods.GetThreadDpiAwarenessContext();
					return CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(threadDpiAwarenessContext, DpiAwarenessContext.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
				}
				return false;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001504D File Offset: 0x0001324D
		internal static bool EnableCheckedListBoxHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableCheckedListBoxHighDpiImprovements;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00015059 File Offset: 0x00013259
		internal static bool EnableSinglePassScalingOfDpiForms
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableSinglePassScalingOfDpiForms;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00015065 File Offset: 0x00013265
		internal static bool EnableThreadExceptionDialogHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableThreadExceptionDialogHighDpiImprovements;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00015071 File Offset: 0x00013271
		internal static bool EnableDataGridViewControlHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableDataGridViewControlHighDpiImprovements;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001507D File Offset: 0x0001327D
		internal static bool EnableAnchorLayoutHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableAnchorLayoutHighDpiImprovements;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00015089 File Offset: 0x00013289
		internal static bool EnableMonthCalendarHighDpiImprovements
		{
			get
			{
				DpiHelper.InitializeDpiHelperForWinforms();
				return DpiHelper.enableMonthCalendarHighDpiImprovements;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00015095 File Offset: 0x00013295
		internal static int DeviceDpi
		{
			get
			{
				DpiHelper.Initialize();
				return (int)DpiHelper.deviceDpi;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x000150A2 File Offset: 0x000132A2
		private static double LogicalToDeviceUnitsScalingFactor
		{
			get
			{
				if (DpiHelper.logicalToDeviceUnitsScalingFactor == 0.0)
				{
					DpiHelper.Initialize();
					DpiHelper.logicalToDeviceUnitsScalingFactor = DpiHelper.deviceDpi / 96.0;
				}
				return DpiHelper.logicalToDeviceUnitsScalingFactor;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x000150D4 File Offset: 0x000132D4
		private static InterpolationMode InterpolationMode
		{
			get
			{
				if (DpiHelper.interpolationMode == InterpolationMode.Invalid)
				{
					int num = (int)Math.Round(DpiHelper.LogicalToDeviceUnitsScalingFactor * 100.0);
					if (num % 100 == 0)
					{
						DpiHelper.interpolationMode = InterpolationMode.NearestNeighbor;
					}
					else if (num < 100)
					{
						DpiHelper.interpolationMode = InterpolationMode.HighQualityBilinear;
					}
					else
					{
						DpiHelper.interpolationMode = InterpolationMode.HighQualityBicubic;
					}
				}
				return DpiHelper.interpolationMode;
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00015128 File Offset: 0x00013328
		private static Bitmap ScaleBitmapToSize(Bitmap logicalImage, Size deviceImageSize)
		{
			Bitmap bitmap = new Bitmap(deviceImageSize.Width, deviceImageSize.Height, logicalImage.PixelFormat);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.InterpolationMode = DpiHelper.InterpolationMode;
				RectangleF rectangleF = new RectangleF(0f, 0f, (float)logicalImage.Size.Width, (float)logicalImage.Size.Height);
				RectangleF rectangleF2 = new RectangleF(0f, 0f, (float)deviceImageSize.Width, (float)deviceImageSize.Height);
				rectangleF.Offset(-0.5f, -0.5f);
				graphics.DrawImage(logicalImage, rectangleF2, rectangleF, GraphicsUnit.Pixel);
			}
			return bitmap;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000151EC File Offset: 0x000133EC
		private static Bitmap CreateScaledBitmap(Bitmap logicalImage, int deviceDpi = 0)
		{
			Size size = DpiHelper.LogicalToDeviceUnits(logicalImage.Size, deviceDpi);
			return DpiHelper.ScaleBitmapToSize(logicalImage, size);
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001520D File Offset: 0x0001340D
		public static bool IsScalingRequired
		{
			get
			{
				DpiHelper.Initialize();
				return DpiHelper.deviceDpi != 96.0;
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00015228 File Offset: 0x00013428
		public static int LogicalToDeviceUnits(int value, int devicePixels = 0)
		{
			if (devicePixels == 0)
			{
				return (int)Math.Round(DpiHelper.LogicalToDeviceUnitsScalingFactor * (double)value);
			}
			double num = (double)devicePixels / 96.0;
			return (int)Math.Round(num * (double)value);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00015260 File Offset: 0x00013460
		public static double LogicalToDeviceUnits(double value, int devicePixels = 0)
		{
			if (devicePixels == 0)
			{
				return DpiHelper.LogicalToDeviceUnitsScalingFactor * value;
			}
			double num = (double)devicePixels / 96.0;
			return num * value;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00015288 File Offset: 0x00013488
		public static int LogicalToDeviceUnitsX(int value)
		{
			return DpiHelper.LogicalToDeviceUnits(value, 0);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00015288 File Offset: 0x00013488
		public static int LogicalToDeviceUnitsY(int value)
		{
			return DpiHelper.LogicalToDeviceUnits(value, 0);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00015291 File Offset: 0x00013491
		public static Size LogicalToDeviceUnits(Size logicalSize, int deviceDpi = 0)
		{
			return new Size(DpiHelper.LogicalToDeviceUnits(logicalSize.Width, deviceDpi), DpiHelper.LogicalToDeviceUnits(logicalSize.Height, deviceDpi));
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000152B2 File Offset: 0x000134B2
		public static Bitmap CreateResizedBitmap(Bitmap logicalImage, Size targetImageSize)
		{
			if (logicalImage == null)
			{
				return null;
			}
			return DpiHelper.ScaleBitmapToSize(logicalImage, targetImageSize);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000152C0 File Offset: 0x000134C0
		public static void ScaleBitmapLogicalToDevice(ref Bitmap logicalBitmap, int deviceDpi = 0)
		{
			if (logicalBitmap == null)
			{
				return;
			}
			Bitmap bitmap = DpiHelper.CreateScaledBitmap(logicalBitmap, deviceDpi);
			if (bitmap != null)
			{
				logicalBitmap.Dispose();
				logicalBitmap = bitmap;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000152E8 File Offset: 0x000134E8
		public static int ConvertToGivenDpiPixel(int value, double pixelFactor)
		{
			int num = (int)Math.Round((double)value * pixelFactor);
			if (num != 0)
			{
				return num;
			}
			return 1;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00015306 File Offset: 0x00013506
		public static IDisposable EnterDpiAwarenessScope(DpiAwarenessContext awareness)
		{
			return new DpiHelper.DpiAwarenessScope(awareness);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00015310 File Offset: 0x00013510
		public static T CreateInstanceInSystemAwareContext<T>(Func<T> createInstance)
		{
			T t;
			using (DpiHelper.EnterDpiAwarenessScope(DpiAwarenessContext.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE))
			{
				t = createInstance();
			}
			return t;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001534C File Offset: 0x0001354C
		public static bool SetWinformsApplicationDpiAwareness()
		{
			Version version = Environment.OSVersion.Version;
			if (!DpiHelper.IsDpiAwarenessValueSet() || Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				return false;
			}
			string text = (DpiHelper.dpiAwarenessValue ?? string.Empty).ToLowerInvariant();
			if (version.CompareTo(ConfigurationOptions.RS2Version) >= 0)
			{
				int num;
				if (!(text == "true") && !(text == "system"))
				{
					if (!(text == "true/pm") && !(text == "permonitor"))
					{
						if (!(text == "permonitorv2"))
						{
							if (!(text == "false"))
							{
							}
							num = -1;
						}
						else
						{
							num = -4;
						}
					}
					else
					{
						num = -3;
					}
				}
				else
				{
					num = -2;
				}
				if (!SafeNativeMethods.SetProcessDpiAwarenessContext(num))
				{
					return false;
				}
			}
			else if (version.CompareTo(new Version(6, 3, 0, 0)) >= 0 && version.CompareTo(ConfigurationOptions.RS2Version) < 0)
			{
				NativeMethods.PROCESS_DPI_AWARENESS process_DPI_AWARENESS;
				if (!(text == "false"))
				{
					if (!(text == "true") && !(text == "system"))
					{
						if (!(text == "true/pm") && !(text == "permonitor") && !(text == "permonitorv2"))
						{
							process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_DPI_UNINITIALIZED;
						}
						else
						{
							process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE;
						}
					}
					else
					{
						process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE;
					}
				}
				else
				{
					process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE;
				}
				if (SafeNativeMethods.SetProcessDpiAwareness(process_DPI_AWARENESS) != 0)
				{
					return false;
				}
			}
			else
			{
				if (version.CompareTo(new Version(6, 1, 0, 0)) < 0 || version.CompareTo(new Version(6, 3, 0, 0)) >= 0)
				{
					return false;
				}
				NativeMethods.PROCESS_DPI_AWARENESS process_DPI_AWARENESS;
				if (!(text == "false"))
				{
					if (!(text == "true") && !(text == "system") && !(text == "true/pm") && !(text == "permonitor") && !(text == "permonitorv2"))
					{
						process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_DPI_UNINITIALIZED;
					}
					else
					{
						process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE;
					}
				}
				else
				{
					process_DPI_AWARENESS = NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE;
				}
				if (process_DPI_AWARENESS == NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE && !SafeNativeMethods.SetProcessDPIAware())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00015525 File Offset: 0x00013725
		public static Padding LogicalToDeviceUnits(Padding logicalPadding, int deviceDpi = 0)
		{
			return new Padding(DpiHelper.LogicalToDeviceUnits(logicalPadding.Left, deviceDpi), DpiHelper.LogicalToDeviceUnits(logicalPadding.Top, deviceDpi), DpiHelper.LogicalToDeviceUnits(logicalPadding.Right, deviceDpi), DpiHelper.LogicalToDeviceUnits(logicalPadding.Bottom, deviceDpi));
		}

		// Token: 0x040004E0 RID: 1248
		internal const double LogicalDpi = 96.0;

		// Token: 0x040004E1 RID: 1249
		private static bool isInitialized = false;

		// Token: 0x040004E2 RID: 1250
		private static double deviceDpi = 96.0;

		// Token: 0x040004E3 RID: 1251
		private static double logicalToDeviceUnitsScalingFactor = 0.0;

		// Token: 0x040004E4 RID: 1252
		private static bool enableHighDpi = false;

		// Token: 0x040004E5 RID: 1253
		private static string dpiAwarenessValue = null;

		// Token: 0x040004E6 RID: 1254
		private static InterpolationMode interpolationMode = InterpolationMode.Invalid;

		// Token: 0x040004E7 RID: 1255
		private static bool isDpiHelperQuirksInitialized = false;

		// Token: 0x040004E8 RID: 1256
		private static bool enableToolStripHighDpiImprovements = false;

		// Token: 0x040004E9 RID: 1257
		private static bool enableDpiChangedMessageHandling = false;

		// Token: 0x040004EA RID: 1258
		private static bool enableCheckedListBoxHighDpiImprovements = false;

		// Token: 0x040004EB RID: 1259
		private static bool enableThreadExceptionDialogHighDpiImprovements = false;

		// Token: 0x040004EC RID: 1260
		private static bool enableDataGridViewControlHighDpiImprovements = false;

		// Token: 0x040004ED RID: 1261
		private static bool enableSinglePassScalingOfDpiForms = false;

		// Token: 0x040004EE RID: 1262
		private static bool enableAnchorLayoutHighDpiImprovements = false;

		// Token: 0x040004EF RID: 1263
		private static bool enableMonthCalendarHighDpiImprovements = false;

		// Token: 0x040004F0 RID: 1264
		private static bool enableDpiChangedHighDpiImprovements = false;

		// Token: 0x040004F1 RID: 1265
		private static readonly Version dpiChangedMessageHighDpiImprovementsMinimumFrameworkVersion = new Version(4, 8);

		// Token: 0x020005FB RID: 1531
		private class DpiAwarenessScope : IDisposable
		{
			// Token: 0x060061AD RID: 25005 RVA: 0x00168DC4 File Offset: 0x00166FC4
			public DpiAwarenessScope(DpiAwarenessContext awareness)
			{
				if (DpiHelper.EnableDpiChangedHighDpiImprovements)
				{
					try
					{
						if (!CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(awareness, DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED))
						{
							this.originalAwareness = CommonUnsafeNativeMethods.GetThreadDpiAwarenessContext();
							if (!CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(this.originalAwareness, awareness) && !CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(this.originalAwareness, DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNAWARE))
							{
								this.originalAwareness = CommonUnsafeNativeMethods.SetThreadDpiAwarenessContext(awareness);
								this.dpiAwarenessScopeIsSet = true;
							}
						}
					}
					catch (EntryPointNotFoundException)
					{
						this.dpiAwarenessScopeIsSet = false;
					}
				}
			}

			// Token: 0x060061AE RID: 25006 RVA: 0x00168E40 File Offset: 0x00167040
			public void Dispose()
			{
				this.ResetDpiAwarenessContextChanges();
			}

			// Token: 0x060061AF RID: 25007 RVA: 0x00168E48 File Offset: 0x00167048
			private void ResetDpiAwarenessContextChanges()
			{
				if (this.dpiAwarenessScopeIsSet)
				{
					CommonUnsafeNativeMethods.TrySetThreadDpiAwarenessContext(this.originalAwareness);
					this.dpiAwarenessScopeIsSet = false;
				}
			}

			// Token: 0x04003892 RID: 14482
			private bool dpiAwarenessScopeIsSet;

			// Token: 0x04003893 RID: 14483
			private DpiAwarenessContext originalAwareness;
		}
	}
}
