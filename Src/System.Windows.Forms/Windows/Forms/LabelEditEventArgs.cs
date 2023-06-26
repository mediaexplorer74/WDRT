using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.BeforeLabelEdit" /> and <see cref="E:System.Windows.Forms.ListView.AfterLabelEdit" /> events.</summary>
	// Token: 0x020002BA RID: 698
	public class LabelEditEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> class with the specified index to the <see cref="T:System.Windows.Forms.ListViewItem" /> to edit.</summary>
		/// <param name="item">The zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" />, containing the label to edit.</param>
		// Token: 0x06002B18 RID: 11032 RVA: 0x000C2087 File Offset: 0x000C0287
		public LabelEditEventArgs(int item)
		{
			this.item = item;
			this.label = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> class with the specified index to the <see cref="T:System.Windows.Forms.ListViewItem" /> being edited and the new text for the label of the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <param name="item">The zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" />, containing the label to edit.</param>
		/// <param name="label">The new text assigned to the label of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		// Token: 0x06002B19 RID: 11033 RVA: 0x000C209D File Offset: 0x000C029D
		public LabelEditEventArgs(int item, string label)
		{
			this.item = item;
			this.label = label;
		}

		/// <summary>Gets the new text assigned to the label of the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The new text to associate with the <see cref="T:System.Windows.Forms.ListViewItem" /> or <see langword="null" /> if the text is unchanged.</returns>
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000C20B3 File Offset: 0x000C02B3
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		/// <summary>Gets the zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" /> containing the label to edit.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x000C20BB File Offset: 0x000C02BB
		public int Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets or sets a value indicating whether changes made to the label of the <see cref="T:System.Windows.Forms.ListViewItem" /> should be canceled.</summary>
		/// <returns>
		///   <see langword="true" /> if the edit operation of the label for the <see cref="T:System.Windows.Forms.ListViewItem" /> should be canceled; otherwise <see langword="false" />.</returns>
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x000C20C3 File Offset: 0x000C02C3
		// (set) Token: 0x06002B1D RID: 11037 RVA: 0x000C20CB File Offset: 0x000C02CB
		public bool CancelEdit
		{
			get
			{
				return this.cancelEdit;
			}
			set
			{
				this.cancelEdit = value;
			}
		}

		// Token: 0x04001219 RID: 4633
		private readonly string label;

		// Token: 0x0400121A RID: 4634
		private readonly int item;

		// Token: 0x0400121B RID: 4635
		private bool cancelEdit;
	}
}
