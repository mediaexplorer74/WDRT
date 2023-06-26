using System;
using System.Drawing;
using System.Internal;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004EA RID: 1258
	internal sealed class WindowsRegion : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x0600521A RID: 21018 RVA: 0x0001E46B File Offset: 0x0001C66B
		private WindowsRegion()
		{
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x00155053 File Offset: 0x00153253
		public WindowsRegion(Rectangle rect)
		{
			this.CreateRegion(rect);
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x00155062 File Offset: 0x00153262
		public WindowsRegion(int x, int y, int width, int height)
		{
			this.CreateRegion(new Rectangle(x, y, width, height));
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x0015507C File Offset: 0x0015327C
		public static WindowsRegion FromHregion(IntPtr hRegion, bool takeOwnership)
		{
			WindowsRegion windowsRegion = new WindowsRegion();
			if (hRegion != IntPtr.Zero)
			{
				windowsRegion.nativeHandle = hRegion;
				if (takeOwnership)
				{
					windowsRegion.ownHandle = true;
					System.Internal.HandleCollector.Add(hRegion, IntSafeNativeMethods.CommonHandles.GDI);
				}
			}
			return windowsRegion;
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x001550BA File Offset: 0x001532BA
		public static WindowsRegion FromRegion(Region region, Graphics g)
		{
			if (region.IsInfinite(g))
			{
				return new WindowsRegion();
			}
			return WindowsRegion.FromHregion(region.GetHrgn(g), true);
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x001550D8 File Offset: 0x001532D8
		public object Clone()
		{
			if (!this.IsInfinite)
			{
				return new WindowsRegion(this.ToRectangle());
			}
			return new WindowsRegion();
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x001550F3 File Offset: 0x001532F3
		public IntNativeMethods.RegionFlags CombineRegion(WindowsRegion region1, WindowsRegion region2, RegionCombineMode mode)
		{
			return IntUnsafeNativeMethods.CombineRgn(new HandleRef(this, this.HRegion), new HandleRef(region1, region1.HRegion), new HandleRef(region2, region2.HRegion), mode);
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x0015511F File Offset: 0x0015331F
		private void CreateRegion(Rectangle rect)
		{
			this.nativeHandle = IntSafeNativeMethods.CreateRectRgn(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
			this.ownHandle = true;
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0015515F File Offset: 0x0015335F
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x00155168 File Offset: 0x00153368
		public void Dispose(bool disposing)
		{
			if (this.nativeHandle != IntPtr.Zero)
			{
				if (this.ownHandle)
				{
					IntUnsafeNativeMethods.DeleteObject(new HandleRef(this, this.nativeHandle));
				}
				this.nativeHandle = IntPtr.Zero;
				if (disposing)
				{
					GC.SuppressFinalize(this);
				}
			}
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x001551B8 File Offset: 0x001533B8
		~WindowsRegion()
		{
			this.Dispose(false);
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06005225 RID: 21029 RVA: 0x001551E8 File Offset: 0x001533E8
		public IntPtr HRegion
		{
			get
			{
				return this.nativeHandle;
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06005226 RID: 21030 RVA: 0x001551F0 File Offset: 0x001533F0
		public bool IsInfinite
		{
			get
			{
				return this.nativeHandle == IntPtr.Zero;
			}
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x00155204 File Offset: 0x00153404
		public Rectangle ToRectangle()
		{
			if (this.IsInfinite)
			{
				return new Rectangle(-2147483647, -2147483647, int.MaxValue, int.MaxValue);
			}
			IntNativeMethods.RECT rect = default(IntNativeMethods.RECT);
			IntUnsafeNativeMethods.GetRgnBox(new HandleRef(this, this.nativeHandle), ref rect);
			return new Rectangle(new Point(rect.left, rect.top), rect.Size);
		}

		// Token: 0x0400360B RID: 13835
		private IntPtr nativeHandle;

		// Token: 0x0400360C RID: 13836
		private bool ownHandle;
	}
}
