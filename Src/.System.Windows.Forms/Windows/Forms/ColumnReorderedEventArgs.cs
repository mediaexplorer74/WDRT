using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnReordered" /> event.</summary>
	// Token: 0x02000157 RID: 343
	public class ColumnReorderedEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnReorderedEventArgs" /> class.</summary>
		/// <param name="oldDisplayIndex">The previous display position of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		/// <param name="newDisplayIndex">The new display position for the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> that is being reordered.</param>
		// Token: 0x06000DC4 RID: 3524 RVA: 0x00027A8C File Offset: 0x00025C8C
		public ColumnReorderedEventArgs(int oldDisplayIndex, int newDisplayIndex, ColumnHeader header)
		{
			this.oldDisplayIndex = oldDisplayIndex;
			this.newDisplayIndex = newDisplayIndex;
			this.header = header;
		}

		/// <summary>Gets the previous display position of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <returns>The previous display position of the <see cref="T:System.Windows.Forms.ColumnHeader" /></returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00027AA9 File Offset: 0x00025CA9
		public int OldDisplayIndex
		{
			get
			{
				return this.oldDisplayIndex;
			}
		}

		/// <summary>Gets the new display position of the <see cref="T:System.Windows.Forms.ColumnHeader" /></summary>
		/// <returns>The new display position of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00027AB1 File Offset: 0x00025CB1
		public int NewDisplayIndex
		{
			get
			{
				return this.newDisplayIndex;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ColumnHeader" /> that is being reordered.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> that is being reordered.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00027AB9 File Offset: 0x00025CB9
		public ColumnHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x04000799 RID: 1945
		private int oldDisplayIndex;

		// Token: 0x0400079A RID: 1946
		private int newDisplayIndex;

		// Token: 0x0400079B RID: 1947
		private ColumnHeader header;
	}
}
