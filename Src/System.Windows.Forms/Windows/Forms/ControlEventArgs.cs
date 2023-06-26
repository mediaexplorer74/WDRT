using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> and <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> events.</summary>
	// Token: 0x0200016C RID: 364
	public class ControlEventArgs : EventArgs
	{
		/// <summary>Gets the control object used by this event.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> used by this event.</returns>
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0003D19A File Offset: 0x0003B39A
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ControlEventArgs" /> class for the specified control.</summary>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to store in this event.</param>
		// Token: 0x0600131A RID: 4890 RVA: 0x0003D1A2 File Offset: 0x0003B3A2
		public ControlEventArgs(Control control)
		{
			this.control = control;
		}

		// Token: 0x04000900 RID: 2304
		private Control control;
	}
}
