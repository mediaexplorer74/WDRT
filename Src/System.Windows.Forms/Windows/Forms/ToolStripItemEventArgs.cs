using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for <see cref="T:System.Windows.Forms.ToolStripItem" /> events.</summary>
	// Token: 0x020003D1 RID: 977
	public class ToolStripItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemEventArgs" /> class, specifying a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to specify events.</param>
		// Token: 0x06004336 RID: 17206 RVA: 0x0011CDB9 File Offset: 0x0011AFB9
		public ToolStripItemEventArgs(ToolStripItem item)
		{
			this.item = item;
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to handle events.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to handle events.</returns>
		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06004337 RID: 17207 RVA: 0x0011CDC8 File Offset: 0x0011AFC8
		public ToolStripItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x0400259C RID: 9628
		private ToolStripItem item;
	}
}
