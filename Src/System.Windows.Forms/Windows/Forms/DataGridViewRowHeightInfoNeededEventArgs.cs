using System;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowHeightInfoNeeded" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	// Token: 0x0200020F RID: 527
	public class DataGridViewRowHeightInfoNeededEventArgs : EventArgs
	{
		// Token: 0x06002298 RID: 8856 RVA: 0x000A6547 File Offset: 0x000A4747
		internal DataGridViewRowHeightInfoNeededEventArgs()
		{
			this.rowIndex = -1;
			this.height = -1;
			this.minimumHeight = -1;
		}

		/// <summary>Gets or sets the height of the row the event occurred for.</summary>
		/// <returns>The row height.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is greater than 65,536.</exception>
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x000A6564 File Offset: 0x000A4764
		// (set) Token: 0x0600229A RID: 8858 RVA: 0x000A656C File Offset: 0x000A476C
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				if (value < this.minimumHeight)
				{
					value = this.minimumHeight;
				}
				if (value > 65536)
				{
					throw new ArgumentOutOfRangeException("Height", SR.GetString("InvalidHighBoundArgumentEx", new object[]
					{
						"Height",
						value.ToString(CultureInfo.CurrentCulture),
						65536.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.height = value;
			}
		}

		/// <summary>Gets or sets the minimum height of the row the event occurred for.</summary>
		/// <returns>The minimum row height.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 2.</exception>
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x000A65E0 File Offset: 0x000A47E0
		// (set) Token: 0x0600229C RID: 8860 RVA: 0x000A65E8 File Offset: 0x000A47E8
		public int MinimumHeight
		{
			get
			{
				return this.minimumHeight;
			}
			set
			{
				if (value < 2)
				{
					throw new ArgumentOutOfRangeException("MinimumHeight", value, SR.GetString("DataGridViewBand_MinimumHeightSmallerThanOne", new object[] { 2.ToString(CultureInfo.CurrentCulture) }));
				}
				if (this.height < value)
				{
					this.height = value;
				}
				this.minimumHeight = value;
			}
		}

		/// <summary>Gets the index of the row associated with this <see cref="T:System.Windows.Forms.DataGridViewRowHeightInfoNeededEventArgs" />.</summary>
		/// <returns>The zero-based index of the row the event occurred for.</returns>
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x000A6642 File Offset: 0x000A4842
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000A664A File Offset: 0x000A484A
		internal void SetProperties(int rowIndex, int height, int minimumHeight)
		{
			this.rowIndex = rowIndex;
			this.height = height;
			this.minimumHeight = minimumHeight;
		}

		// Token: 0x04000E3B RID: 3643
		private int rowIndex;

		// Token: 0x04000E3C RID: 3644
		private int height;

		// Token: 0x04000E3D RID: 3645
		private int minimumHeight;
	}
}
