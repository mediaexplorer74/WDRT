using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
	// Token: 0x02000317 RID: 791
	public class PaintEventArgs : EventArgs, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PaintEventArgs" /> class with the specified graphics and clipping rectangle.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the item.</param>
		/// <param name="clipRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle in which to paint.</param>
		// Token: 0x06003259 RID: 12889 RVA: 0x000E2312 File Offset: 0x000E0512
		public PaintEventArgs(Graphics graphics, Rectangle clipRect)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			this.graphics = graphics;
			this.clipRect = clipRect;
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000E234C File Offset: 0x000E054C
		internal PaintEventArgs(IntPtr dc, Rectangle clipRect)
		{
			this.dc = dc;
			this.clipRect = clipRect;
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x0600325B RID: 12891 RVA: 0x000E2378 File Offset: 0x000E0578
		~PaintEventArgs()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the rectangle in which to paint.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> in which to paint.</returns>
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x000E23A8 File Offset: 0x000E05A8
		public Rectangle ClipRectangle
		{
			get
			{
				return this.clipRect;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000E23B0 File Offset: 0x000E05B0
		internal IntPtr HDC
		{
			get
			{
				if (this.graphics == null)
				{
					return this.dc;
				}
				return IntPtr.Zero;
			}
		}

		/// <summary>Gets the graphics used to paint.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> object used to paint. The <see cref="T:System.Drawing.Graphics" /> object provides methods for drawing objects on the display device.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000E23C8 File Offset: 0x000E05C8
		public Graphics Graphics
		{
			get
			{
				if (this.graphics == null && this.dc != IntPtr.Zero)
				{
					this.oldPal = Control.SetUpPalette(this.dc, false, false);
					this.graphics = Graphics.FromHdcInternal(this.dc);
					this.graphics.PageUnit = GraphicsUnit.Pixel;
					this.savedGraphicsState = this.graphics.Save();
				}
				return this.graphics;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.PaintEventArgs" />.</summary>
		// Token: 0x0600325F RID: 12895 RVA: 0x000E2436 File Offset: 0x000E0636
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.PaintEventArgs" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003260 RID: 12896 RVA: 0x000E2448 File Offset: 0x000E0648
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.graphics != null && this.dc != IntPtr.Zero)
			{
				this.graphics.Dispose();
			}
			if (this.oldPal != IntPtr.Zero && this.dc != IntPtr.Zero)
			{
				SafeNativeMethods.SelectPalette(new HandleRef(this, this.dc), new HandleRef(this, this.oldPal), 0);
				this.oldPal = IntPtr.Zero;
			}
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000E24CB File Offset: 0x000E06CB
		internal void ResetGraphics()
		{
			if (this.graphics != null && this.savedGraphicsState != null)
			{
				this.graphics.Restore(this.savedGraphicsState);
				this.savedGraphicsState = null;
			}
		}

		// Token: 0x04001E69 RID: 7785
		private Graphics graphics;

		// Token: 0x04001E6A RID: 7786
		private GraphicsState savedGraphicsState;

		// Token: 0x04001E6B RID: 7787
		private readonly IntPtr dc = IntPtr.Zero;

		// Token: 0x04001E6C RID: 7788
		private IntPtr oldPal = IntPtr.Zero;

		// Token: 0x04001E6D RID: 7789
		private readonly Rectangle clipRect;
	}
}
