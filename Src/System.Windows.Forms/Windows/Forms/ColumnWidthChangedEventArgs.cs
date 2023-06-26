using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnWidthChanged" /> event.</summary>
	// Token: 0x0200015A RID: 346
	public class ColumnWidthChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnWidthChangedEventArgs" /> class.</summary>
		/// <param name="columnIndex">The index of the column whose width is being changed.</param>
		// Token: 0x06000DCC RID: 3532 RVA: 0x00027AC1 File Offset: 0x00025CC1
		public ColumnWidthChangedEventArgs(int columnIndex)
		{
			this.columnIndex = columnIndex;
		}

		/// <summary>Gets the column index for the column whose width is being changed.</summary>
		/// <returns>The index of the column whose width is being changed.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00027AD0 File Offset: 0x00025CD0
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x040007A0 RID: 1952
		private readonly int columnIndex;
	}
}
