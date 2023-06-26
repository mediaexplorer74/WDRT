using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.</summary>
	// Token: 0x02000260 RID: 608
	public class FormClosingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> class.</summary>
		/// <param name="closeReason">A <see cref="T:System.Windows.Forms.CloseReason" /> value that represents the reason why the form is being closed.</param>
		/// <param name="cancel">
		///   <see langword="true" /> to cancel the event; otherwise, <see langword="false" />.</param>
		// Token: 0x0600277F RID: 10111 RVA: 0x000B8AF5 File Offset: 0x000B6CF5
		public FormClosingEventArgs(CloseReason closeReason, bool cancel)
			: base(cancel)
		{
			this.closeReason = closeReason;
		}

		/// <summary>Gets a value that indicates why the form is being closed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CloseReason" /> enumerated values.</returns>
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x000B8B05 File Offset: 0x000B6D05
		public CloseReason CloseReason
		{
			get
			{
				return this.closeReason;
			}
		}

		// Token: 0x04001039 RID: 4153
		private CloseReason closeReason;
	}
}
