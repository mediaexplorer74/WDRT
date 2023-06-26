using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnWidthChanging" /> event.</summary>
	// Token: 0x0200015C RID: 348
	public class ColumnWidthChangingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnWidthChangingEventArgs" /> class, specifying the column index and width and whether to cancel the event.</summary>
		/// <param name="columnIndex">The index of the column whose width is changing.</param>
		/// <param name="newWidth">The new width of the column.</param>
		/// <param name="cancel">
		///   <see langword="true" /> to cancel the width change; otherwise, <see langword="false" />.</param>
		// Token: 0x06000DD2 RID: 3538 RVA: 0x00027AD8 File Offset: 0x00025CD8
		public ColumnWidthChangingEventArgs(int columnIndex, int newWidth, bool cancel)
			: base(cancel)
		{
			this.columnIndex = columnIndex;
			this.newWidth = newWidth;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnWidthChangingEventArgs" /> class with the specified column index and width.</summary>
		/// <param name="columnIndex">The index of the column whose width is changing.</param>
		/// <param name="newWidth">The new width for the column.</param>
		// Token: 0x06000DD3 RID: 3539 RVA: 0x00027AEF File Offset: 0x00025CEF
		public ColumnWidthChangingEventArgs(int columnIndex, int newWidth)
		{
			this.columnIndex = columnIndex;
			this.newWidth = newWidth;
		}

		/// <summary>Gets the index of the column whose width is changing.</summary>
		/// <returns>The index of the column whose width is changing.</returns>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00027B05 File Offset: 0x00025D05
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets or sets the new width for the column.</summary>
		/// <returns>The new width for the column.</returns>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00027B0D File Offset: 0x00025D0D
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x00027B15 File Offset: 0x00025D15
		public int NewWidth
		{
			get
			{
				return this.newWidth;
			}
			set
			{
				this.newWidth = value;
			}
		}

		// Token: 0x040007A1 RID: 1953
		private int columnIndex;

		// Token: 0x040007A2 RID: 1954
		private int newWidth;
	}
}
