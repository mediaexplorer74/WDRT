using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemImage" /> event.</summary>
	// Token: 0x020003D4 RID: 980
	public class ToolStripItemImageRenderEventArgs : ToolStripItemRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> within the specified space and that has the specified properties.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to paint the image.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="imageRectangle">The bounding area of the image.</param>
		// Token: 0x0600433C RID: 17212 RVA: 0x0011CDD0 File Offset: 0x0011AFD0
		public ToolStripItemImageRenderEventArgs(Graphics g, ToolStripItem item, Rectangle imageRectangle)
			: base(g, item)
		{
			this.image = ((item.RightToLeftAutoMirrorImage && item.RightToLeft == RightToLeft.Yes) ? item.MirroredImage : item.Image);
			this.imageRectangle = imageRectangle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays an image within the specified space and that has the specified properties.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to paint the image.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to draw the image.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to paint.</param>
		/// <param name="imageRectangle">The bounding area of the image.</param>
		// Token: 0x0600433D RID: 17213 RVA: 0x0011CE1C File Offset: 0x0011B01C
		public ToolStripItemImageRenderEventArgs(Graphics g, ToolStripItem item, Image image, Rectangle imageRectangle)
			: base(g, item)
		{
			this.image = image;
			this.imageRectangle = imageRectangle;
		}

		/// <summary>Gets the image painted on the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> painted on the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x0600433E RID: 17214 RVA: 0x0011CE40 File Offset: 0x0011B040
		public Image Image
		{
			get
			{
				return this.image;
			}
		}

		/// <summary>Gets the rectangle that represents the bounding area of the image.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding area of the image.</returns>
		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x0600433F RID: 17215 RVA: 0x0011CE48 File Offset: 0x0011B048
		public Rectangle ImageRectangle
		{
			get
			{
				return this.imageRectangle;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06004340 RID: 17216 RVA: 0x0011CE50 File Offset: 0x0011B050
		// (set) Token: 0x06004341 RID: 17217 RVA: 0x0011CE58 File Offset: 0x0011B058
		internal bool ShiftOnPress
		{
			get
			{
				return this.shiftOnPress;
			}
			set
			{
				this.shiftOnPress = value;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06004342 RID: 17218 RVA: 0x0011CE61 File Offset: 0x0011B061
		// (set) Token: 0x06004343 RID: 17219 RVA: 0x0011CE69 File Offset: 0x0011B069
		internal ImageAttributes ImageAttributes
		{
			get
			{
				return this.imageAttr;
			}
			set
			{
				this.imageAttr = value;
			}
		}

		// Token: 0x040025A8 RID: 9640
		private Image image;

		// Token: 0x040025A9 RID: 9641
		private Rectangle imageRectangle = Rectangle.Empty;

		// Token: 0x040025AA RID: 9642
		private bool shiftOnPress;

		// Token: 0x040025AB RID: 9643
		private ImageAttributes imageAttr;
	}
}
