using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.DataBindingComplete" /> event.</summary>
	// Token: 0x0200019E RID: 414
	public class DataGridViewBindingCompleteEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs" /> class.</summary>
		/// <param name="listChangedType">One of the <see cref="T:System.ComponentModel.ListChangedType" /> values.</param>
		// Token: 0x06001CF7 RID: 7415 RVA: 0x00087B9B File Offset: 0x00085D9B
		public DataGridViewBindingCompleteEventArgs(ListChangedType listChangedType)
		{
			this.listChangedType = listChangedType;
		}

		/// <summary>Gets a value specifying how the list changed.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListChangedType" /> values.</returns>
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00087BAA File Offset: 0x00085DAA
		public ListChangedType ListChangedType
		{
			get
			{
				return this.listChangedType;
			}
		}

		// Token: 0x04000C74 RID: 3188
		private ListChangedType listChangedType;
	}
}
