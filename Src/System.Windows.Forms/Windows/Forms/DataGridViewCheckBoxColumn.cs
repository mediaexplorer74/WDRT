using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> objects.</summary>
	// Token: 0x020001BB RID: 443
	[ToolboxBitmap(typeof(DataGridViewCheckBoxColumn), "DataGridViewCheckBoxColumn.bmp")]
	public class DataGridViewCheckBoxColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxColumn" /> class to the default state.</summary>
		// Token: 0x06001EFE RID: 7934 RVA: 0x00092C99 File Offset: 0x00090E99
		public DataGridViewCheckBoxColumn()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxColumn" /> and configures it to display check boxes with two or three states.</summary>
		/// <param name="threeState">
		///   <see langword="true" /> to display check boxes with three states; <see langword="false" /> to display check boxes with two states.</param>
		// Token: 0x06001EFF RID: 7935 RVA: 0x00092CA4 File Offset: 0x00090EA4
		public DataGridViewCheckBoxColumn(bool threeState)
			: base(new DataGridViewCheckBoxCell(threeState))
		{
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			dataGridViewCellStyle.AlignmentInternal = DataGridViewContentAlignment.MiddleCenter;
			if (threeState)
			{
				dataGridViewCellStyle.NullValue = CheckState.Indeterminate;
			}
			else
			{
				dataGridViewCellStyle.NullValue = false;
			}
			this.DefaultCellStyle = dataGridViewCellStyle;
		}

		/// <summary>Gets or sets the template used to create new cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default value is a new <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> instance.</returns>
		/// <exception cref="T:System.InvalidCastException">The property is set to a value that is not of type <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />.</exception>
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00089219 File Offset: 0x00087419
		// (set) Token: 0x06001F01 RID: 7937 RVA: 0x00092CEF File Offset: 0x00090EEF
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
				if (value != null && !(value is DataGridViewCheckBoxCell))
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[] { "System.Windows.Forms.DataGridViewCheckBoxCell" }));
				}
				base.CellTemplate = value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x00092D21 File Offset: 0x00090F21
		private DataGridViewCheckBoxCell CheckBoxCellTemplate
		{
			get
			{
				return (DataGridViewCheckBoxCell)this.CellTemplate;
			}
		}

		/// <summary>Gets or sets the column's default cell style.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied as the default style.</returns>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x00089253 File Offset: 0x00087453
		// (set) Token: 0x06001F04 RID: 7940 RVA: 0x0008925B File Offset: 0x0008745B
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

		/// <summary>Gets or sets the underlying value corresponding to a cell value of <see langword="false" />, which appears as an unchecked box.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing a value that the cells in this column will treat as a <see langword="false" /> value. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x00092D2E File Offset: 0x00090F2E
		// (set) Token: 0x06001F06 RID: 7942 RVA: 0x00092D54 File Offset: 0x00090F54
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_CheckBoxColumnFalseValueDescr")]
		[TypeConverter(typeof(StringConverter))]
		public object FalseValue
		{
			get
			{
				if (this.CheckBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.CheckBoxCellTemplate.FalseValue;
			}
			set
			{
				if (this.FalseValue != value)
				{
					this.CheckBoxCellTemplate.FalseValueInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewCheckBoxCell dataGridViewCheckBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewCheckBoxCell;
							if (dataGridViewCheckBoxCell != null)
							{
								dataGridViewCheckBoxCell.FalseValueInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		/// <summary>Gets or sets the flat style appearance of the check box cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FlatStyle" /> value indicating the appearance of cells in the column. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x00092DDA File Offset: 0x00090FDA
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x00092E00 File Offset: 0x00091000
		[DefaultValue(FlatStyle.Standard)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_CheckBoxColumnFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				if (this.CheckBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.CheckBoxCellTemplate.FlatStyle;
			}
			set
			{
				if (this.FlatStyle != value)
				{
					this.CheckBoxCellTemplate.FlatStyle = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewCheckBoxCell dataGridViewCheckBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewCheckBoxCell;
							if (dataGridViewCheckBoxCell != null)
							{
								dataGridViewCheckBoxCell.FlatStyleInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		/// <summary>Gets or sets the underlying value corresponding to an indeterminate or <see langword="null" /> cell value, which appears as a disabled checkbox.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing a value that the cells in this column will treat as an indeterminate value. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x00092E86 File Offset: 0x00091086
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x00092EAC File Offset: 0x000910AC
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_CheckBoxColumnIndeterminateValueDescr")]
		[TypeConverter(typeof(StringConverter))]
		public object IndeterminateValue
		{
			get
			{
				if (this.CheckBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.CheckBoxCellTemplate.IndeterminateValue;
			}
			set
			{
				if (this.IndeterminateValue != value)
				{
					this.CheckBoxCellTemplate.IndeterminateValueInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewCheckBoxCell dataGridViewCheckBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewCheckBoxCell;
							if (dataGridViewCheckBoxCell != null)
							{
								dataGridViewCheckBoxCell.IndeterminateValueInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the hosted check box cells will allow three check states rather than two.</summary>
		/// <returns>
		///   <see langword="true" /> if the hosted <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> objects are able to have a third, indeterminate, state; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x00092F32 File Offset: 0x00091132
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x00092F58 File Offset: 0x00091158
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_CheckBoxColumnThreeStateDescr")]
		public bool ThreeState
		{
			get
			{
				if (this.CheckBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.CheckBoxCellTemplate.ThreeState;
			}
			set
			{
				if (this.ThreeState != value)
				{
					this.CheckBoxCellTemplate.ThreeStateInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewCheckBoxCell dataGridViewCheckBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewCheckBoxCell;
							if (dataGridViewCheckBoxCell != null)
							{
								dataGridViewCheckBoxCell.ThreeStateInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
					if (value && this.DefaultCellStyle.NullValue is bool && !(bool)this.DefaultCellStyle.NullValue)
					{
						this.DefaultCellStyle.NullValue = CheckState.Indeterminate;
						return;
					}
					if (!value && this.DefaultCellStyle.NullValue is CheckState && (CheckState)this.DefaultCellStyle.NullValue == CheckState.Indeterminate)
					{
						this.DefaultCellStyle.NullValue = false;
					}
				}
			}
		}

		/// <summary>Gets or sets the underlying value corresponding to a cell value of <see langword="true" />, which appears as a checked box.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing a value that the cell will treat as a <see langword="true" /> value. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00093053 File Offset: 0x00091253
		// (set) Token: 0x06001F0E RID: 7950 RVA: 0x00093078 File Offset: 0x00091278
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_CheckBoxColumnTrueValueDescr")]
		[TypeConverter(typeof(StringConverter))]
		public object TrueValue
		{
			get
			{
				if (this.CheckBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.CheckBoxCellTemplate.TrueValue;
			}
			set
			{
				if (this.TrueValue != value)
				{
					this.CheckBoxCellTemplate.TrueValueInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewCheckBoxCell dataGridViewCheckBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewCheckBoxCell;
							if (dataGridViewCheckBoxCell != null)
							{
								dataGridViewCheckBoxCell.TrueValueInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x00093100 File Offset: 0x00091300
		private bool ShouldSerializeDefaultCellStyle()
		{
			DataGridViewCheckBoxCell dataGridViewCheckBoxCell = this.CellTemplate as DataGridViewCheckBoxCell;
			if (dataGridViewCheckBoxCell == null)
			{
				return true;
			}
			object obj;
			if (dataGridViewCheckBoxCell.ThreeState)
			{
				obj = CheckState.Indeterminate;
			}
			else
			{
				obj = false;
			}
			if (!base.HasDefaultCellStyle)
			{
				return false;
			}
			DataGridViewCellStyle defaultCellStyle = this.DefaultCellStyle;
			return !defaultCellStyle.BackColor.IsEmpty || !defaultCellStyle.ForeColor.IsEmpty || !defaultCellStyle.SelectionBackColor.IsEmpty || !defaultCellStyle.SelectionForeColor.IsEmpty || defaultCellStyle.Font != null || !defaultCellStyle.NullValue.Equals(obj) || !defaultCellStyle.IsDataSourceNullValueDefault || !string.IsNullOrEmpty(defaultCellStyle.Format) || !defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) || defaultCellStyle.Alignment != DataGridViewContentAlignment.MiddleCenter || defaultCellStyle.WrapMode != DataGridViewTriState.NotSet || defaultCellStyle.Tag != null || !defaultCellStyle.Padding.Equals(Padding.Empty);
		}

		/// <summary>Gets a string that describes the column.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		// Token: 0x06001F10 RID: 7952 RVA: 0x0009320C File Offset: 0x0009140C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewCheckBoxColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}
	}
}
