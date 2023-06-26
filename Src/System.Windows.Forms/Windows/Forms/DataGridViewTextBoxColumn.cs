using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" /> cells.</summary>
	// Token: 0x0200021C RID: 540
	[ToolboxBitmap(typeof(DataGridViewTextBoxColumn), "DataGridViewTextBoxColumn.bmp")]
	public class DataGridViewTextBoxColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxColumn" /> class to the default state.</summary>
		// Token: 0x0600233F RID: 9023 RVA: 0x000A81A6 File Offset: 0x000A63A6
		public DataGridViewTextBoxColumn()
			: base(new DataGridViewTextBoxCell())
		{
			this.SortMode = DataGridViewColumnSortMode.Automatic;
		}

		/// <summary>Gets or sets the template used to model cell appearance.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after.</returns>
		/// <exception cref="T:System.InvalidCastException">The set type is not compatible with type <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" />.</exception>
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x00089219 File Offset: 0x00087419
		// (set) Token: 0x06002341 RID: 9025 RVA: 0x000A81BA File Offset: 0x000A63BA
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
				if (value != null && !(value is DataGridViewTextBoxCell))
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[] { "System.Windows.Forms.DataGridViewTextBoxCell" }));
				}
				base.CellTemplate = value;
			}
		}

		/// <summary>Gets or sets the maximum number of characters that can be entered into the text box.</summary>
		/// <returns>The maximum number of characters that can be entered into the text box; the default value is 32767.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewTextBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x000A81EC File Offset: 0x000A63EC
		// (set) Token: 0x06002343 RID: 9027 RVA: 0x000A8214 File Offset: 0x000A6414
		[DefaultValue(32767)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_TextBoxColumnMaxInputLengthDescr")]
		public int MaxInputLength
		{
			get
			{
				if (this.TextBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.TextBoxCellTemplate.MaxInputLength;
			}
			set
			{
				if (this.MaxInputLength != value)
				{
					this.TextBoxCellTemplate.MaxInputLength = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewTextBoxCell dataGridViewTextBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewTextBoxCell;
							if (dataGridViewTextBoxCell != null)
							{
								dataGridViewTextBoxCell.MaxInputLength = value;
							}
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the sort mode for the column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewColumnSortMode" /> that specifies the criteria used to order the rows based on the cell values in a column.</returns>
		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x000A8289 File Offset: 0x000A6489
		// (set) Token: 0x06002345 RID: 9029 RVA: 0x000A8291 File Offset: 0x000A6491
		[DefaultValue(DataGridViewColumnSortMode.Automatic)]
		public new DataGridViewColumnSortMode SortMode
		{
			get
			{
				return base.SortMode;
			}
			set
			{
				base.SortMode = value;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x000A829A File Offset: 0x000A649A
		private DataGridViewTextBoxCell TextBoxCellTemplate
		{
			get
			{
				return (DataGridViewTextBoxCell)this.CellTemplate;
			}
		}

		/// <summary>Gets a string that describes the column.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		// Token: 0x06002347 RID: 9031 RVA: 0x000A82A8 File Offset: 0x000A64A8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewTextBoxColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000E7C RID: 3708
		private const int DATAGRIDVIEWTEXTBOXCOLUMN_maxInputLength = 32767;
	}
}
