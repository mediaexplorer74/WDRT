using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderGrip" /> event.</summary>
	// Token: 0x020003C6 RID: 966
	public class ToolStripGripRenderEventArgs : ToolStripRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> class.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object used to paint the move handle.</param>
		/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> the move handle is to be drawn on.</param>
		// Token: 0x0600418A RID: 16778 RVA: 0x001188FF File Offset: 0x00116AFF
		public ToolStripGripRenderEventArgs(Graphics g, ToolStrip toolStrip)
			: base(g, toolStrip)
		{
		}

		/// <summary>Gets the rectangle representing the area in which to paint the move handle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the area in which to paint the move handle.</returns>
		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x0600418B RID: 16779 RVA: 0x00118909 File Offset: 0x00116B09
		public Rectangle GripBounds
		{
			get
			{
				return base.ToolStrip.GripRectangle;
			}
		}

		/// <summary>Gets the style that indicates whether the move handle is displayed vertically or horizontally.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> values.</returns>
		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x0600418C RID: 16780 RVA: 0x00118916 File Offset: 0x00116B16
		public ToolStripGripDisplayStyle GripDisplayStyle
		{
			get
			{
				return base.ToolStrip.GripDisplayStyle;
			}
		}

		/// <summary>Gets the style that indicates whether or not the move handle is visible.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> values.</returns>
		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x00118923 File Offset: 0x00116B23
		public ToolStripGripStyle GripStyle
		{
			get
			{
				return base.ToolStrip.GripStyle;
			}
		}
	}
}
