using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderGrip" /> event.</summary>
	// Token: 0x020003FE RID: 1022
	public class ToolStripSeparatorRenderEventArgs : ToolStripItemRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> class.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to paint with.</param>
		/// <param name="separator">The <see cref="T:System.Windows.Forms.ToolStripSeparator" /> to be painted.</param>
		/// <param name="vertical">A value indicating whether or not the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> is to be drawn vertically.</param>
		// Token: 0x060046BC RID: 18108 RVA: 0x00128BB9 File Offset: 0x00126DB9
		public ToolStripSeparatorRenderEventArgs(Graphics g, ToolStripSeparator separator, bool vertical)
			: base(g, separator)
		{
			this.vertical = vertical;
		}

		/// <summary>Gets a value indicating whether the display style for the grip is vertical.</summary>
		/// <returns>
		///   <see langword="true" /> if the display style for the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> is vertical; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x060046BD RID: 18109 RVA: 0x00128BCA File Offset: 0x00126DCA
		public bool Vertical
		{
			get
			{
				return this.vertical;
			}
		}

		// Token: 0x040026AE RID: 9902
		private bool vertical;
	}
}
