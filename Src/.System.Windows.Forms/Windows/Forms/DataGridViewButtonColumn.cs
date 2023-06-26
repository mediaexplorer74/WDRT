using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> objects.</summary>
	// Token: 0x020001A0 RID: 416
	[ToolboxBitmap(typeof(DataGridViewButtonColumn), "DataGridViewButtonColumn.bmp")]
	public class DataGridViewButtonColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewButtonColumn" /> class to the default state.</summary>
		// Token: 0x06001D1E RID: 7454 RVA: 0x000891EC File Offset: 0x000873EC
		public DataGridViewButtonColumn()
			: base(new DataGridViewButtonCell())
		{
			this.DefaultCellStyle = new DataGridViewCellStyle
			{
				AlignmentInternal = DataGridViewContentAlignment.MiddleCenter
			};
		}

		/// <summary>Gets or sets the template used to create new cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified value when setting this property could not be cast to a <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />.</exception>
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x00089219 File Offset: 0x00087419
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x00089221 File Offset: 0x00087421
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				if (value != null && !(value is DataGridViewButtonCell))
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[] { "System.Windows.Forms.DataGridViewButtonCell" }));
				}
				base.CellTemplate = value;
			}
		}

		/// <summary>Gets or sets the column's default cell style.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied as the default style.</returns>
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x00089253 File Offset: 0x00087453
		// (set) Token: 0x06001D22 RID: 7458 RVA: 0x0008925B File Offset: 0x0008745B
		[Browsable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ColumnDefaultCellStyleDescr")]
		public override DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				return base.DefaultCellStyle;
			}
			set
			{
				base.DefaultCellStyle = value;
			}
		}

		/// <summary>Gets or sets the flat-style appearance of the button cells in the column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FlatStyle" /> value indicating the appearance of the buttons in the column. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x00089264 File Offset: 0x00087464
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x00089290 File Offset: 0x00087490
		[DefaultValue(FlatStyle.Standard)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ButtonColumnFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewButtonCell)this.CellTemplate).FlatStyle;
			}
			set
			{
				if (this.FlatStyle != value)
				{
					((DataGridViewButtonCell)this.CellTemplate).FlatStyle = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewButtonCell dataGridViewButtonCell = dataGridViewRow.Cells[base.Index] as DataGridViewButtonCell;
							if (dataGridViewButtonCell != null)
							{
								dataGridViewButtonCell.FlatStyleInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		/// <summary>Gets or sets the default text displayed on the button cell.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the text. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x0008931B File Offset: 0x0008751B
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x00089324 File Offset: 0x00087524
		[DefaultValue(null)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ButtonColumnTextDescr")]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (!string.Equals(value, this.text, StringComparison.Ordinal))
				{
					this.text = value;
					if (base.DataGridView != null)
					{
						if (this.UseColumnTextForButtonValue)
						{
							base.DataGridView.OnColumnCommonChange(base.Index);
							return;
						}
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewButtonCell dataGridViewButtonCell = dataGridViewRow.Cells[base.Index] as DataGridViewButtonCell;
							if (dataGridViewButtonCell != null && dataGridViewButtonCell.UseColumnTextForButtonValue)
							{
								base.DataGridView.OnColumnCommonChange(base.Index);
								return;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> property value is displayed as the button text for cells in this column.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> property value is displayed on buttons in the column; <see langword="false" /> if the <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValue" /> property value of each cell is displayed on its button. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x000893DE File Offset: 0x000875DE
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x00089408 File Offset: 0x00087608
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ButtonColumnUseColumnTextForButtonValueDescr")]
		public bool UseColumnTextForButtonValue
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewButtonCell)this.CellTemplate).UseColumnTextForButtonValue;
			}
			set
			{
				if (this.UseColumnTextForButtonValue != value)
				{
					((DataGridViewButtonCell)this.CellTemplate).UseColumnTextForButtonValueInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewButtonCell dataGridViewButtonCell = dataGridViewRow.Cells[base.Index] as DataGridViewButtonCell;
							if (dataGridViewButtonCell != null)
							{
								dataGridViewButtonCell.UseColumnTextForButtonValueInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		/// <summary>Creates an exact copy of this column.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewButtonColumn" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x06001D29 RID: 7465 RVA: 0x00089494 File Offset: 0x00087694
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewButtonColumn dataGridViewButtonColumn;
			if (type == DataGridViewButtonColumn.columnType)
			{
				dataGridViewButtonColumn = new DataGridViewButtonColumn();
			}
			else
			{
				dataGridViewButtonColumn = (DataGridViewButtonColumn)Activator.CreateInstance(type);
			}
			if (dataGridViewButtonColumn != null)
			{
				base.CloneInternal(dataGridViewButtonColumn);
				dataGridViewButtonColumn.Text = this.text;
			}
			return dataGridViewButtonColumn;
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x000894E0 File Offset: 0x000876E0
		private bool ShouldSerializeDefaultCellStyle()
		{
			if (!base.HasDefaultCellStyle)
			{
				return false;
			}
			DataGridViewCellStyle defaultCellStyle = this.DefaultCellStyle;
			return !defaultCellStyle.BackColor.IsEmpty || !defaultCellStyle.ForeColor.IsEmpty || !defaultCellStyle.SelectionBackColor.IsEmpty || !defaultCellStyle.SelectionForeColor.IsEmpty || defaultCellStyle.Font != null || !defaultCellStyle.IsNullValueDefault || !defaultCellStyle.IsDataSourceNullValueDefault || !string.IsNullOrEmpty(defaultCellStyle.Format) || !defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) || defaultCellStyle.Alignment != DataGridViewContentAlignment.MiddleCenter || defaultCellStyle.WrapMode != DataGridViewTriState.NotSet || defaultCellStyle.Tag != null || !defaultCellStyle.Padding.Equals(Padding.Empty);
		}

		/// <summary>Gets a string that describes the column.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		// Token: 0x06001D2B RID: 7467 RVA: 0x000895BC File Offset: 0x000877BC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewButtonColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000C82 RID: 3202
		private static Type columnType = typeof(DataGridViewButtonColumn);

		// Token: 0x04000C83 RID: 3203
		private string text;
	}
}
