using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> objects.</summary>
	// Token: 0x020001FF RID: 511
	[ToolboxBitmap(typeof(DataGridViewImageColumn), "DataGridViewImageColumn.bmp")]
	public class DataGridViewImageColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageColumn" /> class, configuring it for use with cell values of type <see cref="T:System.Drawing.Image" />.</summary>
		// Token: 0x06002154 RID: 8532 RVA: 0x0009D655 File Offset: 0x0009B855
		public DataGridViewImageColumn()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageColumn" /> class, optionally configuring it for use with <see cref="T:System.Drawing.Icon" /> cell values.</summary>
		/// <param name="valuesAreIcons">
		///   <see langword="true" /> to indicate that the <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property of cells in this column will be set to values of type <see cref="T:System.Drawing.Icon" />; <see langword="false" /> to indicate that they will be set to values of type <see cref="T:System.Drawing.Image" />.</param>
		// Token: 0x06002155 RID: 8533 RVA: 0x0009D660 File Offset: 0x0009B860
		public DataGridViewImageColumn(bool valuesAreIcons)
			: base(new DataGridViewImageCell(valuesAreIcons))
		{
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			dataGridViewCellStyle.AlignmentInternal = DataGridViewContentAlignment.MiddleCenter;
			if (valuesAreIcons)
			{
				dataGridViewCellStyle.NullValue = DataGridViewImageCell.ErrorIcon;
			}
			else
			{
				dataGridViewCellStyle.NullValue = DataGridViewImageCell.ErrorBitmap;
			}
			this.DefaultCellStyle = dataGridViewCellStyle;
		}

		/// <summary>Gets or sets the template used to create new cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after.</returns>
		/// <exception cref="T:System.InvalidCastException">The set type is not compatible with type <see cref="T:System.Windows.Forms.DataGridViewImageCell" />.</exception>
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x00089219 File Offset: 0x00087419
		// (set) Token: 0x06002157 RID: 8535 RVA: 0x0009D6A9 File Offset: 0x0009B8A9
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
				if (value != null && !(value is DataGridViewImageCell))
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[] { "System.Windows.Forms.DataGridViewImageCell" }));
				}
				base.CellTemplate = value;
			}
		}

		/// <summary>Gets or sets the column's default cell style.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied as the default style.</returns>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x00089253 File Offset: 0x00087453
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x0008925B File Offset: 0x0008745B
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

		/// <summary>Gets or sets a string that describes the column's image.</summary>
		/// <returns>The textual description of the column image. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewImageColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x0009D6DB File Offset: 0x0009B8DB
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x0009D700 File Offset: 0x0009B900
		[Browsable(true)]
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridViewImageColumn_DescriptionDescr")]
		public string Description
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ImageCellTemplate.Description;
			}
			set
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ImageCellTemplate.Description = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewImageCell dataGridViewImageCell = dataGridViewRow.Cells[base.Index] as DataGridViewImageCell;
						if (dataGridViewImageCell != null)
						{
							dataGridViewImageCell.Description = value;
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the icon displayed in the cells of this column when the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property is not set and the cell's <see cref="P:System.Windows.Forms.DataGridViewImageCell.ValueIsIcon" /> property is set to <see langword="true" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Icon" /> to display. The default is <see langword="null" />.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x0009D784 File Offset: 0x0009B984
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x0009D78C File Offset: 0x0009B98C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnCommonChange(base.Index);
				}
			}
		}

		/// <summary>Gets or sets the image displayed in the cells of this column when the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property is not set and the cell's <see cref="P:System.Windows.Forms.DataGridViewImageCell.ValueIsIcon" /> property is set to <see langword="false" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> to display. The default is <see langword="null" />.</returns>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x0009D7AE File Offset: 0x0009B9AE
		// (set) Token: 0x0600215F RID: 8543 RVA: 0x0009D7B6 File Offset: 0x0009B9B6
		[DefaultValue(null)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridViewImageColumn_ImageDescr")]
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnCommonChange(base.Index);
				}
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x0009D7D8 File Offset: 0x0009B9D8
		private DataGridViewImageCell ImageCellTemplate
		{
			get
			{
				return (DataGridViewImageCell)this.CellTemplate;
			}
		}

		/// <summary>Gets or sets the image layout in the cells for this column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewImageCellLayout" /> that specifies the cell layout. The default is <see cref="F:System.Windows.Forms.DataGridViewImageCellLayout.Normal" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewImageColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x0009D7E8 File Offset: 0x0009B9E8
		// (set) Token: 0x06002162 RID: 8546 RVA: 0x0009D820 File Offset: 0x0009BA20
		[DefaultValue(DataGridViewImageCellLayout.Normal)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridViewImageColumn_ImageLayoutDescr")]
		public DataGridViewImageCellLayout ImageLayout
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				DataGridViewImageCellLayout dataGridViewImageCellLayout = this.ImageCellTemplate.ImageLayout;
				if (dataGridViewImageCellLayout == DataGridViewImageCellLayout.NotSet)
				{
					dataGridViewImageCellLayout = DataGridViewImageCellLayout.Normal;
				}
				return dataGridViewImageCellLayout;
			}
			set
			{
				if (this.ImageLayout != value)
				{
					this.ImageCellTemplate.ImageLayout = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewImageCell dataGridViewImageCell = dataGridViewRow.Cells[base.Index] as DataGridViewImageCell;
							if (dataGridViewImageCell != null)
							{
								dataGridViewImageCell.ImageLayoutInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether cells in this column display <see cref="T:System.Drawing.Icon" /> values.</summary>
		/// <returns>
		///   <see langword="true" /> if cells display values of type <see cref="T:System.Drawing.Icon" />; <see langword="false" /> if cells display values of type <see cref="T:System.Drawing.Image" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewImageColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x0009D8A6 File Offset: 0x0009BAA6
		// (set) Token: 0x06002164 RID: 8548 RVA: 0x0009D8CC File Offset: 0x0009BACC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ValuesAreIcons
		{
			get
			{
				if (this.ImageCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ImageCellTemplate.ValueIsIcon;
			}
			set
			{
				if (this.ValuesAreIcons != value)
				{
					this.ImageCellTemplate.ValueIsIconInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewImageCell dataGridViewImageCell = dataGridViewRow.Cells[base.Index] as DataGridViewImageCell;
							if (dataGridViewImageCell != null)
							{
								dataGridViewImageCell.ValueIsIconInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
					if (value && this.DefaultCellStyle.NullValue is Bitmap && (Bitmap)this.DefaultCellStyle.NullValue == DataGridViewImageCell.ErrorBitmap)
					{
						this.DefaultCellStyle.NullValue = DataGridViewImageCell.ErrorIcon;
						return;
					}
					if (!value && this.DefaultCellStyle.NullValue is Icon && (Icon)this.DefaultCellStyle.NullValue == DataGridViewImageCell.ErrorIcon)
					{
						this.DefaultCellStyle.NullValue = DataGridViewImageCell.ErrorBitmap;
					}
				}
			}
		}

		/// <summary>Creates an exact copy of this column.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewImageColumn" />.</returns>
		// Token: 0x06002165 RID: 8549 RVA: 0x0009D9D0 File Offset: 0x0009BBD0
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewImageColumn dataGridViewImageColumn;
			if (type == DataGridViewImageColumn.columnType)
			{
				dataGridViewImageColumn = new DataGridViewImageColumn();
			}
			else
			{
				dataGridViewImageColumn = (DataGridViewImageColumn)Activator.CreateInstance(type);
			}
			if (dataGridViewImageColumn != null)
			{
				base.CloneInternal(dataGridViewImageColumn);
				dataGridViewImageColumn.Icon = this.icon;
				dataGridViewImageColumn.Image = this.image;
			}
			return dataGridViewImageColumn;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0009DA28 File Offset: 0x0009BC28
		private bool ShouldSerializeDefaultCellStyle()
		{
			DataGridViewImageCell dataGridViewImageCell = this.CellTemplate as DataGridViewImageCell;
			if (dataGridViewImageCell == null)
			{
				return true;
			}
			if (!base.HasDefaultCellStyle)
			{
				return false;
			}
			object obj;
			if (dataGridViewImageCell.ValueIsIcon)
			{
				obj = DataGridViewImageCell.ErrorIcon;
			}
			else
			{
				obj = DataGridViewImageCell.ErrorBitmap;
			}
			DataGridViewCellStyle defaultCellStyle = this.DefaultCellStyle;
			return !defaultCellStyle.BackColor.IsEmpty || !defaultCellStyle.ForeColor.IsEmpty || !defaultCellStyle.SelectionBackColor.IsEmpty || !defaultCellStyle.SelectionForeColor.IsEmpty || defaultCellStyle.Font != null || !obj.Equals(defaultCellStyle.NullValue) || !defaultCellStyle.IsDataSourceNullValueDefault || !string.IsNullOrEmpty(defaultCellStyle.Format) || !defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) || defaultCellStyle.Alignment != DataGridViewContentAlignment.MiddleCenter || defaultCellStyle.WrapMode != DataGridViewTriState.NotSet || defaultCellStyle.Tag != null || !defaultCellStyle.Padding.Equals(Padding.Empty);
		}

		/// <summary>Gets a string that describes the column.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		// Token: 0x06002167 RID: 8551 RVA: 0x0009DB34 File Offset: 0x0009BD34
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewImageColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000DE9 RID: 3561
		private static Type columnType = typeof(DataGridViewImageColumn);

		// Token: 0x04000DEA RID: 3562
		private Image image;

		// Token: 0x04000DEB RID: 3563
		private Icon icon;
	}
}
