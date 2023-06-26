using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnClick" /> event.</summary>
	// Token: 0x02000152 RID: 338
	public class ColumnClickEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnClickEventArgs" /> class.</summary>
		/// <param name="column">The zero-based index of the column that is clicked.</param>
		// Token: 0x06000D96 RID: 3478 RVA: 0x000271CF File Offset: 0x000253CF
		public ColumnClickEventArgs(int column)
		{
			this.column = column;
		}

		/// <summary>Gets the zero-based index of the column that is clicked.</summary>
		/// <returns>The zero-based index within the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of the column that is clicked.</returns>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x000271DE File Offset: 0x000253DE
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x0400078A RID: 1930
		private readonly int column;
	}
}
