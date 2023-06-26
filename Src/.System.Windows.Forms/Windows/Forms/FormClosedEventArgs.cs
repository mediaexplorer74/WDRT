using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.FormClosed" /> event.</summary>
	// Token: 0x0200025E RID: 606
	public class FormClosedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FormClosedEventArgs" /> class.</summary>
		/// <param name="closeReason">A <see cref="T:System.Windows.Forms.CloseReason" /> value that represents the reason why the form was closed.</param>
		// Token: 0x06002779 RID: 10105 RVA: 0x000B8ADE File Offset: 0x000B6CDE
		public FormClosedEventArgs(CloseReason closeReason)
		{
			this.closeReason = closeReason;
		}

		/// <summary>Gets a value that indicates why the form was closed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CloseReason" /> enumerated values.</returns>
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x000B8AED File Offset: 0x000B6CED
		public CloseReason CloseReason
		{
			get
			{
				return this.closeReason;
			}
		}

		// Token: 0x04001038 RID: 4152
		private CloseReason closeReason;
	}
}
