using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see langword="MeasureItem" /> event of the <see cref="T:System.Windows.Forms.ListBox" />, <see cref="T:System.Windows.Forms.ComboBox" />, <see cref="T:System.Windows.Forms.CheckedListBox" />, and <see cref="T:System.Windows.Forms.MenuItem" /> controls.</summary>
	// Token: 0x020002EF RID: 751
	public class MeasureItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> class providing a parameter for the item height.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object being written to.</param>
		/// <param name="index">The index of the item for which you need the height or width.</param>
		/// <param name="itemHeight">The height of the item to measure relative to the <paramref name="graphics" /> object.</param>
		// Token: 0x06002F93 RID: 12179 RVA: 0x000D6C31 File Offset: 0x000D4E31
		public MeasureItemEventArgs(Graphics graphics, int index, int itemHeight)
		{
			this.graphics = graphics;
			this.index = index;
			this.itemHeight = itemHeight;
			this.itemWidth = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object being written to.</param>
		/// <param name="index">The index of the item for which you need the height or width.</param>
		// Token: 0x06002F94 RID: 12180 RVA: 0x000D6C55 File Offset: 0x000D4E55
		public MeasureItemEventArgs(Graphics graphics, int index)
		{
			this.graphics = graphics;
			this.index = index;
			this.itemHeight = 0;
			this.itemWidth = 0;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> object to measure against.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> object to use to determine the scale of the item you are drawing.</returns>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002F95 RID: 12181 RVA: 0x000D6C79 File Offset: 0x000D4E79
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the index of the item for which the height and width is needed.</summary>
		/// <returns>The index of the item to be measured.</returns>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x000D6C81 File Offset: 0x000D4E81
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets or sets the height of the item specified by the <see cref="P:System.Windows.Forms.MeasureItemEventArgs.Index" />.</summary>
		/// <returns>The height of the item measured.</returns>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002F97 RID: 12183 RVA: 0x000D6C89 File Offset: 0x000D4E89
		// (set) Token: 0x06002F98 RID: 12184 RVA: 0x000D6C91 File Offset: 0x000D4E91
		public int ItemHeight
		{
			get
			{
				return this.itemHeight;
			}
			set
			{
				this.itemHeight = value;
			}
		}

		/// <summary>Gets or sets the width of the item specified by the <see cref="P:System.Windows.Forms.MeasureItemEventArgs.Index" />.</summary>
		/// <returns>The width of the item measured.</returns>
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000D6C9A File Offset: 0x000D4E9A
		// (set) Token: 0x06002F9A RID: 12186 RVA: 0x000D6CA2 File Offset: 0x000D4EA2
		public int ItemWidth
		{
			get
			{
				return this.itemWidth;
			}
			set
			{
				this.itemWidth = value;
			}
		}

		// Token: 0x0400139E RID: 5022
		private int itemHeight;

		// Token: 0x0400139F RID: 5023
		private int itemWidth;

		// Token: 0x040013A0 RID: 5024
		private int index;

		// Token: 0x040013A1 RID: 5025
		private readonly Graphics graphics;
	}
}
