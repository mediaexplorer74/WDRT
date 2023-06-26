using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020003AF RID: 943
	internal class CachedItemHdcInfo : IDisposable
	{
		// Token: 0x06003EC5 RID: 16069 RVA: 0x001103C2 File Offset: 0x0010E5C2
		internal CachedItemHdcInfo()
		{
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x001103EC File Offset: 0x0010E5EC
		~CachedItemHdcInfo()
		{
			this.Dispose();
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x00110418 File Offset: 0x0010E618
		public HandleRef GetCachedItemDC(HandleRef toolStripHDC, Size bitmapSize)
		{
			if (this.cachedHDCSize.Width < bitmapSize.Width || this.cachedHDCSize.Height < bitmapSize.Height)
			{
				if (this.cachedItemHDC.Handle == IntPtr.Zero)
				{
					IntPtr intPtr = UnsafeNativeMethods.CreateCompatibleDC(toolStripHDC);
					this.cachedItemHDC = new HandleRef(this, intPtr);
				}
				this.cachedItemBitmap = new HandleRef(this, SafeNativeMethods.CreateCompatibleBitmap(toolStripHDC, bitmapSize.Width, bitmapSize.Height));
				IntPtr intPtr2 = SafeNativeMethods.SelectObject(this.cachedItemHDC, this.cachedItemBitmap);
				if (intPtr2 != IntPtr.Zero)
				{
					SafeNativeMethods.ExternalDeleteObject(new HandleRef(null, intPtr2));
					intPtr2 = IntPtr.Zero;
				}
				this.cachedHDCSize = bitmapSize;
			}
			return this.cachedItemHDC;
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x001104DC File Offset: 0x0010E6DC
		private void DeleteCachedItemHDC()
		{
			if (this.cachedItemHDC.Handle != IntPtr.Zero)
			{
				if (this.cachedItemBitmap.Handle != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(this.cachedItemBitmap);
					this.cachedItemBitmap = NativeMethods.NullHandleRef;
				}
				UnsafeNativeMethods.DeleteCompatibleDC(this.cachedItemHDC);
			}
			this.cachedItemHDC = NativeMethods.NullHandleRef;
			this.cachedItemBitmap = NativeMethods.NullHandleRef;
			this.cachedHDCSize = Size.Empty;
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x0011055B File Offset: 0x0010E75B
		public void Dispose()
		{
			this.DeleteCachedItemHDC();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04002483 RID: 9347
		private HandleRef cachedItemHDC = NativeMethods.NullHandleRef;

		// Token: 0x04002484 RID: 9348
		private Size cachedHDCSize = Size.Empty;

		// Token: 0x04002485 RID: 9349
		private HandleRef cachedItemBitmap = NativeMethods.NullHandleRef;
	}
}
