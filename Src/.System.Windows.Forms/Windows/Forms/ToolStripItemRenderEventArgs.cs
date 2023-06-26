using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the events that render the background of objects derived from <see cref="T:System.Windows.Forms.ToolStripItem" /> in the <see cref="T:System.Windows.Forms.ToolStripRenderer" /> class.</summary>
	// Token: 0x020003D9 RID: 985
	public class ToolStripItemRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object used to draw the item.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to be drawn.</param>
		// Token: 0x06004348 RID: 17224 RVA: 0x0011CE72 File Offset: 0x0011B072
		public ToolStripItemRenderEventArgs(Graphics g, ToolStripItem item)
		{
			this.item = item;
			this.graphics = g;
		}

		/// <summary>Gets the graphics used to paint the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x0011CE88 File Offset: 0x0011B088
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</returns>
		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x0600434A RID: 17226 RVA: 0x0011CE90 File Offset: 0x0011B090
		public ToolStripItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Owner" /> property for the <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> that is the owner of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x0600434B RID: 17227 RVA: 0x0011CE98 File Offset: 0x0011B098
		public ToolStrip ToolStrip
		{
			get
			{
				return this.item.ParentInternal;
			}
		}

		// Token: 0x040025B7 RID: 9655
		private ToolStripItem item;

		// Token: 0x040025B8 RID: 9656
		private Graphics graphics;
	}
}
