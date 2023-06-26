using System;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowsRemoved" /> event.</summary>
	// Token: 0x02000214 RID: 532
	public class DataGridViewRowsRemovedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowsRemovedEventArgs" /> class.</summary>
		/// <param name="rowIndex">The zero-based index of the row that was deleted, or the first deleted row if multiple rows were deleted.</param>
		/// <param name="rowCount">The number of rows that were deleted.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than 0.  
		/// -or-  
		/// <paramref name="rowCount" /> is less than 1.</exception>
		// Token: 0x060022CE RID: 8910 RVA: 0x000A6EE0 File Offset: 0x000A50E0
		public DataGridViewRowsRemovedEventArgs(int rowIndex, int rowCount)
		{
			if (rowIndex < 0)
			{
				throw new ArgumentOutOfRangeException("rowIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"rowIndex",
					rowIndex.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (rowCount < 1)
			{
				throw new ArgumentOutOfRangeException("rowCount", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"rowCount",
					rowCount.ToString(CultureInfo.CurrentCulture),
					1.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.rowIndex = rowIndex;
			this.rowCount = rowCount;
		}

		/// <summary>Gets the zero-based index of the row deleted, or the first deleted row if multiple rows were deleted.</summary>
		/// <returns>The zero-based index of the row that was deleted, or the first deleted row if multiple rows were deleted.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x000A6F8F File Offset: 0x000A518F
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the number of rows that were deleted.</summary>
		/// <returns>The number of deleted rows.</returns>
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x000A6F97 File Offset: 0x000A5197
		public int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x04000E58 RID: 3672
		private int rowIndex;

		// Token: 0x04000E59 RID: 3673
		private int rowCount;
	}
}
