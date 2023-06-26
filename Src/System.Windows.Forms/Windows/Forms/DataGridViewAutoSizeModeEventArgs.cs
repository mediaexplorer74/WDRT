using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="T:System.Windows.Forms.DataGridView" /><see cref="E:System.Windows.Forms.DataGridView.AutoSizeRowsModeChanged" /> and <see cref="E:System.Windows.Forms.DataGridView.RowHeadersWidthSizeModeChanged" /> events.</summary>
	// Token: 0x0200019C RID: 412
	public class DataGridViewAutoSizeModeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeModeEventArgs" /> class.</summary>
		/// <param name="previousModeAutoSized">
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeRowsMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None" /> or the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersWidthSizeMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing" /> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing" />; otherwise, <see langword="false" />.</param>
		// Token: 0x06001CBA RID: 7354 RVA: 0x00086951 File Offset: 0x00084B51
		public DataGridViewAutoSizeModeEventArgs(bool previousModeAutoSized)
		{
			this.previousModeAutoSized = previousModeAutoSized;
		}

		/// <summary>Gets a value specifying whether the <see cref="T:System.Windows.Forms.DataGridView" /> was previously set to automatically resize.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeRowsMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None" /> or the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersWidthSizeMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing" /> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x00086960 File Offset: 0x00084B60
		public bool PreviousModeAutoSized
		{
			get
			{
				return this.previousModeAutoSized;
			}
		}

		// Token: 0x04000C65 RID: 3173
		private bool previousModeAutoSized;
	}
}
