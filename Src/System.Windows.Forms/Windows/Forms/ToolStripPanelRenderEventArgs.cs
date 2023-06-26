using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for <see cref="T:System.Windows.Forms.ToolStripPanel" /> drawing.</summary>
	// Token: 0x020003EE RID: 1006
	public class ToolStripPanelRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanelRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripPanel" /> that uses the specified graphics for drawing.</summary>
		/// <param name="g">The graphics used to paint the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		/// <param name="toolStripPanel">The <see cref="T:System.Windows.Forms.ToolStripPanel" /> to draw.</param>
		// Token: 0x06004541 RID: 17729 RVA: 0x00122FB8 File Offset: 0x001211B8
		public ToolStripPanelRenderEventArgs(Graphics g, ToolStripPanel toolStripPanel)
		{
			this.toolStripPanel = toolStripPanel;
			this.graphics = g;
		}

		/// <summary>Gets or sets the graphics used to paint the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint.</returns>
		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x00122FCE File Offset: 0x001211CE
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripPanel" /> to paint.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanel" /> to paint.</returns>
		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x00122FD6 File Offset: 0x001211D6
		public ToolStripPanel ToolStripPanel
		{
			get
			{
				return this.toolStripPanel;
			}
		}

		/// <summary>Gets or sets a value indicating whether the event was handled.</summary>
		/// <returns>
		///   <see langword="true" /> if the event was handled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06004544 RID: 17732 RVA: 0x00122FDE File Offset: 0x001211DE
		// (set) Token: 0x06004545 RID: 17733 RVA: 0x00122FE6 File Offset: 0x001211E6
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x04002641 RID: 9793
		private ToolStripPanel toolStripPanel;

		// Token: 0x04002642 RID: 9794
		private Graphics graphics;

		// Token: 0x04002643 RID: 9795
		private bool handled;
	}
}
