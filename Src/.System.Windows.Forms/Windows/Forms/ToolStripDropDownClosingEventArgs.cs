using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closing" /> event.</summary>
	// Token: 0x020003BB RID: 955
	public class ToolStripDropDownClosingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownClosingEventArgs" /> class with the specified reason for closing.</summary>
		/// <param name="reason">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</param>
		// Token: 0x060040C6 RID: 16582 RVA: 0x00114773 File Offset: 0x00112973
		public ToolStripDropDownClosingEventArgs(ToolStripDropDownCloseReason reason)
		{
			this.closeReason = reason;
		}

		/// <summary>Gets the reason that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is closing.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</returns>
		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x060040C7 RID: 16583 RVA: 0x00114782 File Offset: 0x00112982
		public ToolStripDropDownCloseReason CloseReason
		{
			get
			{
				return this.closeReason;
			}
		}

		// Token: 0x040024D4 RID: 9428
		private ToolStripDropDownCloseReason closeReason;
	}
}
