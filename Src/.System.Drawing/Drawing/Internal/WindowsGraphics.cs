using System;
using System.Drawing.Drawing2D;

namespace System.Drawing.Internal
{
	// Token: 0x020000E1 RID: 225
	internal sealed class WindowsGraphics : MarshalByRefObject, IDisposable, IDeviceContext
	{
		// Token: 0x06000BAE RID: 2990 RVA: 0x0002AA30 File Offset: 0x00028C30
		public WindowsGraphics(DeviceContext dc)
		{
			this.dc = dc;
			this.dc.SaveHdc();
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002AA4C File Offset: 0x00028C4C
		public static WindowsGraphics CreateMeasurementWindowsGraphics()
		{
			DeviceContext deviceContext = DeviceContext.FromCompatibleDC(IntPtr.Zero);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002AA74 File Offset: 0x00028C74
		public static WindowsGraphics CreateMeasurementWindowsGraphics(IntPtr screenDC)
		{
			DeviceContext deviceContext = DeviceContext.FromCompatibleDC(screenDC);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002AA98 File Offset: 0x00028C98
		public static WindowsGraphics FromHwnd(IntPtr hWnd)
		{
			DeviceContext deviceContext = DeviceContext.FromHwnd(hWnd);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002AABC File Offset: 0x00028CBC
		public static WindowsGraphics FromHdc(IntPtr hDc)
		{
			DeviceContext deviceContext = DeviceContext.FromHdc(hDc);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		public static WindowsGraphics FromGraphics(Graphics g)
		{
			ApplyGraphicsProperties applyGraphicsProperties = ApplyGraphicsProperties.All;
			return WindowsGraphics.FromGraphics(g, applyGraphicsProperties);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002AAF8 File Offset: 0x00028CF8
		public static WindowsGraphics FromGraphics(Graphics g, ApplyGraphicsProperties properties)
		{
			WindowsRegion windowsRegion = null;
			float[] array = null;
			Region region = null;
			Matrix matrix = null;
			if ((properties & ApplyGraphicsProperties.TranslateTransform) != ApplyGraphicsProperties.None || (properties & ApplyGraphicsProperties.Clipping) != ApplyGraphicsProperties.None)
			{
				object[] array2 = g.GetContextInfo() as object[];
				if (array2 != null && array2.Length == 2)
				{
					region = array2[0] as Region;
					matrix = array2[1] as Matrix;
				}
				if (matrix != null)
				{
					if ((properties & ApplyGraphicsProperties.TranslateTransform) != ApplyGraphicsProperties.None)
					{
						array = matrix.Elements;
					}
					matrix.Dispose();
				}
				if (region != null)
				{
					if ((properties & ApplyGraphicsProperties.Clipping) != ApplyGraphicsProperties.None && !region.IsInfinite(g))
					{
						windowsRegion = WindowsRegion.FromRegion(region, g);
					}
					region.Dispose();
				}
			}
			WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(g.GetHdc());
			windowsGraphics.graphics = g;
			if (windowsRegion != null)
			{
				using (windowsRegion)
				{
					windowsGraphics.DeviceContext.IntersectClip(windowsRegion);
				}
			}
			if (array != null)
			{
				windowsGraphics.DeviceContext.TranslateTransform((int)array[4], (int)array[5]);
			}
			return windowsGraphics;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002ABD8 File Offset: 0x00028DD8
		~WindowsGraphics()
		{
			this.Dispose(false);
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0002AC08 File Offset: 0x00028E08
		public DeviceContext DeviceContext
		{
			get
			{
				return this.dc;
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002AC10 File Offset: 0x00028E10
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002AC20 File Offset: 0x00028E20
		internal void Dispose(bool disposing)
		{
			if (this.dc != null)
			{
				try
				{
					this.dc.RestoreHdc();
					if (this.disposeDc)
					{
						this.dc.Dispose(disposing);
					}
					if (this.graphics != null)
					{
						this.graphics.ReleaseHdcInternal(this.dc.Hdc);
						this.graphics = null;
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.dc = null;
				}
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002ACAC File Offset: 0x00028EAC
		public IntPtr GetHdc()
		{
			return this.dc.Hdc;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002ACB9 File Offset: 0x00028EB9
		public void ReleaseHdc()
		{
			this.dc.Dispose();
		}

		// Token: 0x04000A65 RID: 2661
		private DeviceContext dc;

		// Token: 0x04000A66 RID: 2662
		private bool disposeDc;

		// Token: 0x04000A67 RID: 2663
		private Graphics graphics;
	}
}
