using System;
using System.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
	// Token: 0x020000DF RID: 223
	internal sealed class WindowsRegion : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x06000BA0 RID: 2976 RVA: 0x000037F8 File Offset: 0x000019F8
		private WindowsRegion()
		{
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002A818 File Offset: 0x00028A18
		public WindowsRegion(Rectangle rect)
		{
			this.CreateRegion(rect);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002A827 File Offset: 0x00028A27
		public WindowsRegion(int x, int y, int width, int height)
		{
			this.CreateRegion(new Rectangle(x, y, width, height));
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002A840 File Offset: 0x00028A40
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

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002A87E File Offset: 0x00028A7E
		public static WindowsRegion FromRegion(Region region, Graphics g)
		{
			if (region.IsInfinite(g))
			{
				return new WindowsRegion();
			}
			return WindowsRegion.FromHregion(region.GetHrgn(g), true);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002A89C File Offset: 0x00028A9C
		public object Clone()
		{
			if (!this.IsInfinite)
			{
				return new WindowsRegion(this.ToRectangle());
			}
			return new WindowsRegion();
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002A8B7 File Offset: 0x00028AB7
		public IntNativeMethods.RegionFlags CombineRegion(WindowsRegion region1, WindowsRegion region2, RegionCombineMode mode)
		{
			return IntUnsafeNativeMethods.CombineRgn(new HandleRef(this, this.HRegion), new HandleRef(region1, region1.HRegion), new HandleRef(region2, region2.HRegion), mode);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002A8E3 File Offset: 0x00028AE3
		private void CreateRegion(Rectangle rect)
		{
			this.nativeHandle = IntSafeNativeMethods.CreateRectRgn(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
			this.ownHandle = true;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002A923 File Offset: 0x00028B23
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002A92C File Offset: 0x00028B2C
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

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002A97C File Offset: 0x00028B7C
		~WindowsRegion()
		{
			this.Dispose(false);
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0002A9AC File Offset: 0x00028BAC
		public IntPtr HRegion
		{
			get
			{
				return this.nativeHandle;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0002A9B4 File Offset: 0x00028BB4
		public bool IsInfinite
		{
			get
			{
				return this.nativeHandle == IntPtr.Zero;
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002A9C8 File Offset: 0x00028BC8
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

		// Token: 0x04000A5B RID: 2651
		private IntPtr nativeHandle;

		// Token: 0x04000A5C RID: 2652
		private bool ownHandle;
	}
}
