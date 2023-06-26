using System;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E8 RID: 1256
	internal sealed class WindowsPen : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x0600520E RID: 21006 RVA: 0x00154D9A File Offset: 0x00152F9A
		public WindowsPen(DeviceContext dc)
			: this(dc, WindowsPenStyle.Solid, 1, Color.Black)
		{
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x00154DAA File Offset: 0x00152FAA
		public WindowsPen(DeviceContext dc, Color color)
			: this(dc, WindowsPenStyle.Solid, 1, color)
		{
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x00154DB6 File Offset: 0x00152FB6
		public WindowsPen(DeviceContext dc, WindowsBrush windowsBrush)
			: this(dc, WindowsPenStyle.Solid, 1, windowsBrush)
		{
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x00154DC2 File Offset: 0x00152FC2
		public WindowsPen(DeviceContext dc, WindowsPenStyle style, int width, Color color)
		{
			this.style = style;
			this.width = width;
			this.color = color;
			this.dc = dc;
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x00154DE7 File Offset: 0x00152FE7
		public WindowsPen(DeviceContext dc, WindowsPenStyle style, int width, WindowsBrush windowsBrush)
		{
			this.style = style;
			this.wndBrush = (WindowsBrush)windowsBrush.Clone();
			this.width = width;
			this.color = windowsBrush.Color;
			this.dc = dc;
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x00154E24 File Offset: 0x00153024
		private void CreatePen()
		{
			if (this.width > 1)
			{
				this.style |= WindowsPenStyle.Geometric;
			}
			if (this.wndBrush == null)
			{
				this.nativeHandle = IntSafeNativeMethods.CreatePen((int)this.style, this.width, ColorTranslator.ToWin32(this.color));
				return;
			}
			IntNativeMethods.LOGBRUSH logbrush = new IntNativeMethods.LOGBRUSH();
			logbrush.lbColor = ColorTranslator.ToWin32(this.wndBrush.Color);
			logbrush.lbStyle = 0;
			logbrush.lbHatch = 0;
			this.nativeHandle = IntSafeNativeMethods.ExtCreatePen((int)this.style, this.width, logbrush, 0, null);
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x00154EBC File Offset: 0x001530BC
		public object Clone()
		{
			if (this.wndBrush == null)
			{
				return new WindowsPen(this.dc, this.style, this.width, this.color);
			}
			return new WindowsPen(this.dc, this.style, this.width, (WindowsBrush)this.wndBrush.Clone());
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x00154F18 File Offset: 0x00153118
		~WindowsPen()
		{
			this.Dispose(false);
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x00154F48 File Offset: 0x00153148
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x00154F54 File Offset: 0x00153154
		private void Dispose(bool disposing)
		{
			if (this.nativeHandle != IntPtr.Zero && this.dc != null)
			{
				this.dc.DeleteObject(this.nativeHandle, GdiObjectType.Pen);
				this.nativeHandle = IntPtr.Zero;
			}
			if (this.wndBrush != null)
			{
				this.wndBrush.Dispose();
				this.wndBrush = null;
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06005218 RID: 21016 RVA: 0x00154FBB File Offset: 0x001531BB
		public IntPtr HPen
		{
			get
			{
				if (this.nativeHandle == IntPtr.Zero)
				{
					this.CreatePen();
				}
				return this.nativeHandle;
			}
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x00154FDC File Offset: 0x001531DC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: Style={1}, Color={2}, Width={3}, Brush={4}", new object[]
			{
				base.GetType().Name,
				this.style,
				this.color,
				this.width,
				(this.wndBrush != null) ? this.wndBrush.ToString() : "null"
			});
		}

		// Token: 0x040035EE RID: 13806
		private IntPtr nativeHandle;

		// Token: 0x040035EF RID: 13807
		private const int dashStyleMask = 15;

		// Token: 0x040035F0 RID: 13808
		private const int endCapMask = 3840;

		// Token: 0x040035F1 RID: 13809
		private const int joinMask = 61440;

		// Token: 0x040035F2 RID: 13810
		private DeviceContext dc;

		// Token: 0x040035F3 RID: 13811
		private WindowsBrush wndBrush;

		// Token: 0x040035F4 RID: 13812
		private WindowsPenStyle style;

		// Token: 0x040035F5 RID: 13813
		private Color color;

		// Token: 0x040035F6 RID: 13814
		private int width;

		// Token: 0x040035F7 RID: 13815
		private const int cosmeticPenWidth = 1;
	}
}
