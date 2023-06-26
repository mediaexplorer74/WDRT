using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripContentPanel.RendererChanged" /> event.</summary>
	// Token: 0x020003F0 RID: 1008
	public class ToolStripContentPanelRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripContentPanelRenderEventArgs" /> class.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> representing the GDI+ drawing surface.</param>
		/// <param name="contentPanel">The <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> to render.</param>
		// Token: 0x0600454A RID: 17738 RVA: 0x00122FEF File Offset: 0x001211EF
		public ToolStripContentPanelRenderEventArgs(Graphics g, ToolStripContentPanel contentPanel)
		{
			this.contentPanel = contentPanel;
			this.graphics = g;
		}

		/// <summary>Gets the object to use for drawing.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> to use for drawing.</returns>
		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x00123005 File Offset: 0x00121205
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets or sets a value indicating whether the event was handled.</summary>
		/// <returns>
		///   <see langword="true" /> if the event was handled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x0600454C RID: 17740 RVA: 0x0012300D File Offset: 0x0012120D
		// (set) Token: 0x0600454D RID: 17741 RVA: 0x00123015 File Offset: 0x00121215
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

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> affected by the click.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> object affected by the click.</returns>
		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x0600454E RID: 17742 RVA: 0x0012301E File Offset: 0x0012121E
		public ToolStripContentPanel ToolStripContentPanel
		{
			get
			{
				return this.contentPanel;
			}
		}

		// Token: 0x04002644 RID: 9796
		private ToolStripContentPanel contentPanel;

		// Token: 0x04002645 RID: 9797
		private Graphics graphics;

		// Token: 0x04002646 RID: 9798
		private bool handled;
	}
}
