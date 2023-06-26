using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closed" /> event.</summary>
	// Token: 0x020003B9 RID: 953
	public class ToolStripDropDownClosedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs" /> class.</summary>
		/// <param name="reason">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</param>
		// Token: 0x060040C0 RID: 16576 RVA: 0x0011475C File Offset: 0x0011295C
		public ToolStripDropDownClosedEventArgs(ToolStripDropDownCloseReason reason)
		{
			this.closeReason = reason;
		}

		/// <summary>Gets the reason that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> closed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</returns>
		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x060040C1 RID: 16577 RVA: 0x0011476B File Offset: 0x0011296B
		public ToolStripDropDownCloseReason CloseReason
		{
			get
			{
				return this.closeReason;
			}
		}

		// Token: 0x040024D3 RID: 9427
		private ToolStripDropDownCloseReason closeReason;
	}
}
