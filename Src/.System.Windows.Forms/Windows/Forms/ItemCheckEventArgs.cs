using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.CheckedListBox.ItemCheck" /> event of the <see cref="T:System.Windows.Forms.CheckedListBox" /> and <see cref="T:System.Windows.Forms.ListView" /> controls.</summary>
	// Token: 0x020002AA RID: 682
	[ComVisible(true)]
	public class ItemCheckEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemCheckEventArgs" /> class.</summary>
		/// <param name="index">The zero-based index of the item to change.</param>
		/// <param name="newCheckValue">One of the <see cref="T:System.Windows.Forms.CheckState" /> values that indicates whether to change the check box for the item to be checked, unchecked, or indeterminate.</param>
		/// <param name="currentValue">One of the <see cref="T:System.Windows.Forms.CheckState" /> values that indicates whether the check box for the item is currently checked, unchecked, or indeterminate.</param>
		// Token: 0x06002A44 RID: 10820 RVA: 0x000BF914 File Offset: 0x000BDB14
		public ItemCheckEventArgs(int index, CheckState newCheckValue, CheckState currentValue)
		{
			this.index = index;
			this.newValue = newCheckValue;
			this.currentValue = currentValue;
		}

		/// <summary>Gets the zero-based index of the item to change.</summary>
		/// <returns>The zero-based index of the item to change.</returns>
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x000BF931 File Offset: 0x000BDB31
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets or sets a value indicating whether to set the check box for the item to be checked, unchecked, or indeterminate.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values.</returns>
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x000BF939 File Offset: 0x000BDB39
		// (set) Token: 0x06002A47 RID: 10823 RVA: 0x000BF941 File Offset: 0x000BDB41
		public CheckState NewValue
		{
			get
			{
				return this.newValue;
			}
			set
			{
				this.newValue = value;
			}
		}

		/// <summary>Gets a value indicating the current state of the item's check box.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values.</returns>
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x000BF94A File Offset: 0x000BDB4A
		public CheckState CurrentValue
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x04001121 RID: 4385
		private readonly int index;

		// Token: 0x04001122 RID: 4386
		private CheckState newValue;

		// Token: 0x04001123 RID: 4387
		private readonly CheckState currentValue;
	}
}
