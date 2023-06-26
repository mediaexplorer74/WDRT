using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents a cell in a <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
	// Token: 0x0200038F RID: 911
	[TypeConverter(typeof(TableLayoutPanelCellPositionTypeConverter))]
	public struct TableLayoutPanelCellPosition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> class.</summary>
		/// <param name="column">The column position of the cell.</param>
		/// <param name="row">The row position of the cell.</param>
		// Token: 0x06003BDD RID: 15325 RVA: 0x00105DF4 File Offset: 0x00103FF4
		public TableLayoutPanelCellPosition(int column, int row)
		{
			if (row < -1)
			{
				throw new ArgumentOutOfRangeException("row", SR.GetString("InvalidArgument", new object[]
				{
					"row",
					row.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (column < -1)
			{
				throw new ArgumentOutOfRangeException("column", SR.GetString("InvalidArgument", new object[]
				{
					"column",
					column.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.row = row;
			this.column = column;
		}

		/// <summary>Gets or sets the row number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
		/// <returns>The row number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06003BDE RID: 15326 RVA: 0x00105E7B File Offset: 0x0010407B
		// (set) Token: 0x06003BDF RID: 15327 RVA: 0x00105E83 File Offset: 0x00104083
		public int Row
		{
			get
			{
				return this.row;
			}
			set
			{
				this.row = value;
			}
		}

		/// <summary>Gets or sets the column number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
		/// <returns>The column number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003BE0 RID: 15328 RVA: 0x00105E8C File Offset: 0x0010408C
		// (set) Token: 0x06003BE1 RID: 15329 RVA: 0x00105E94 File Offset: 0x00104094
		public int Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		/// <summary>Specifies whether this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> contains the same row and column as the specified <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
		/// <param name="other">The <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="other" /> is a <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> and has the same row and column as the specified <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003BE2 RID: 15330 RVA: 0x00105EA0 File Offset: 0x001040A0
		public override bool Equals(object other)
		{
			if (other is TableLayoutPanelCellPosition)
			{
				TableLayoutPanelCellPosition tableLayoutPanelCellPosition = (TableLayoutPanelCellPosition)other;
				return tableLayoutPanelCellPosition.row == this.row && tableLayoutPanelCellPosition.column == this.column;
			}
			return false;
		}

		/// <summary>Compares two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects. The result specifies whether the values of the <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Row" /> and <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Column" /> properties of the two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects are equal.</summary>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="p1" /> and <paramref name="p2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003BE3 RID: 15331 RVA: 0x00105EDC File Offset: 0x001040DC
		public static bool operator ==(TableLayoutPanelCellPosition p1, TableLayoutPanelCellPosition p2)
		{
			return p1.Row == p2.Row && p1.Column == p2.Column;
		}

		/// <summary>Compares two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects. The result specifies whether the values of the <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Row" /> and <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Column" /> properties of the two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects are unequal.</summary>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="p1" /> and <paramref name="p2" /> differ; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003BE4 RID: 15332 RVA: 0x00105F00 File Offset: 0x00104100
		public static bool operator !=(TableLayoutPanelCellPosition p1, TableLayoutPanelCellPosition p2)
		{
			return !(p1 == p2);
		}

		/// <summary>Converts this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to a human readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
		// Token: 0x06003BE5 RID: 15333 RVA: 0x00105F0C File Offset: 0x0010410C
		public override string ToString()
		{
			return this.Column.ToString(CultureInfo.CurrentCulture) + "," + this.Row.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
		// Token: 0x06003BE6 RID: 15334 RVA: 0x00105F49 File Offset: 0x00104149
		public override int GetHashCode()
		{
			return WindowsFormsUtils.GetCombinedHashCodes(new int[] { this.row, this.column });
		}

		// Token: 0x04002380 RID: 9088
		private int row;

		// Token: 0x04002381 RID: 9089
		private int column;
	}
}
