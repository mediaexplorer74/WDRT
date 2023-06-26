using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.MaskedTextBox.MaskInputRejected" /> event.</summary>
	// Token: 0x020002E7 RID: 743
	public class MaskInputRejectedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskInputRejectedEventArgs" /> class.</summary>
		/// <param name="position">An <see cref="T:System.Int32" /> value that contains the zero-based position of the character that failed the mask. The position includes literal characters.</param>
		/// <param name="rejectionHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that generally describes why the character was rejected.</param>
		// Token: 0x06002F5C RID: 12124 RVA: 0x000D58FA File Offset: 0x000D3AFA
		public MaskInputRejectedEventArgs(int position, MaskedTextResultHint rejectionHint)
		{
			this.position = position;
			this.hint = rejectionHint;
		}

		/// <summary>Gets the position in the mask corresponding to the invalid input character.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the zero-based position of the character that failed the mask. The position includes literal characters.</returns>
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002F5D RID: 12125 RVA: 0x000D5910 File Offset: 0x000D3B10
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		/// <summary>Gets an enumerated value that describes why the input character was rejected.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that generally describes why the character was rejected.</returns>
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x000D5918 File Offset: 0x000D3B18
		public MaskedTextResultHint RejectionHint
		{
			get
			{
				return this.hint;
			}
		}

		// Token: 0x04001388 RID: 5000
		private int position;

		// Token: 0x04001389 RID: 5001
		private MaskedTextResultHint hint;
	}
}
