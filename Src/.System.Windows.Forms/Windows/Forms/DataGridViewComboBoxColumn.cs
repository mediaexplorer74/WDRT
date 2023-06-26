using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents a column of <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> objects.</summary>
	// Token: 0x020001C7 RID: 455
	[Designer("System.Windows.Forms.Design.DataGridViewComboBoxColumnDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxBitmap(typeof(DataGridViewComboBoxColumn), "DataGridViewComboBoxColumn.bmp")]
	public class DataGridViewComboBoxColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxColumn" /> class to the default state.</summary>
		// Token: 0x0600201E RID: 8222 RVA: 0x0009ABB1 File Offset: 0x00098DB1
		public DataGridViewComboBoxColumn()
			: base(new DataGridViewComboBoxCell())
		{
			((DataGridViewComboBoxCell)base.CellTemplate).TemplateComboBoxColumn = this;
		}

		/// <summary>Gets or sets a value indicating whether cells in the column will match the characters being entered in the cell with one from the possible selections.</summary>
		/// <returns>
		///   <see langword="true" /> if auto completion is activated; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x0009ABCF File Offset: 0x00098DCF
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x0009ABF4 File Offset: 0x00098DF4
		[Browsable(true)]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ComboBoxColumnAutoCompleteDescr")]
		public bool AutoComplete
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.AutoComplete;
			}
			set
			{
				if (this.AutoComplete != value)
				{
					this.ComboBoxCellTemplate.AutoComplete = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
							if (dataGridViewComboBoxCell != null)
							{
								dataGridViewComboBoxCell.AutoComplete = value;
							}
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the template used to create cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default value is a new <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</returns>
		/// <exception cref="T:System.InvalidCastException">When setting this property to a value that is not of type <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</exception>
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x00089219 File Offset: 0x00087419
		// (set) Token: 0x06002022 RID: 8226 RVA: 0x0009AC6C File Offset: 0x00098E6C
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
				DataGridViewComboBoxCell dataGridViewComboBoxCell = value as DataGridViewComboBoxCell;
				if (value != null && dataGridViewComboBoxCell == null)
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[] { "System.Windows.Forms.DataGridViewComboBoxCell" }));
				}
				base.CellTemplate = value;
				if (value != null)
				{
					dataGridViewComboBoxCell.TemplateComboBoxColumn = this;
				}
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0009ACB5 File Offset: 0x00098EB5
		private DataGridViewComboBoxCell ComboBoxCellTemplate
		{
			get
			{
				return (DataGridViewComboBoxCell)this.CellTemplate;
			}
		}

		/// <summary>Gets or sets the data source that populates the selections for the combo boxes.</summary>
		/// <returns>An object that represents a data source. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x0009ACC2 File Offset: 0x00098EC2
		// (set) Token: 0x06002025 RID: 8229 RVA: 0x0009ACE8 File Offset: 0x00098EE8
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_ComboBoxColumnDataSourceDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[AttributeProvider(typeof(IListSource))]
		public object DataSource
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.DataSource;
			}
			set
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ComboBoxCellTemplate.DataSource = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
						if (dataGridViewComboBoxCell != null)
						{
							dataGridViewComboBoxCell.DataSource = value;
						}
					}
					base.DataGridView.OnColumnCommonChange(base.Index);
				}
			}
		}

		/// <summary>Gets or sets a string that specifies the property or column from which to retrieve strings for display in the combo boxes.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of a property or column in the data source specified in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.DataSource" /> property. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x0009AD7D File Offset: 0x00098F7D
		// (set) Token: 0x06002027 RID: 8231 RVA: 0x0009ADA4 File Offset: 0x00098FA4
		[DefaultValue("")]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_ComboBoxColumnDisplayMemberDescr")]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string DisplayMember
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.DisplayMember;
			}
			set
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ComboBoxCellTemplate.DisplayMember = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
						if (dataGridViewComboBoxCell != null)
						{
							dataGridViewComboBoxCell.DisplayMember = value;
						}
					}
					base.DataGridView.OnColumnCommonChange(base.Index);
				}
			}
		}

		/// <summary>Gets or sets a value that determines how the combo box is displayed when not editing.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewComboBoxDisplayStyle" /> value indicating the combo box appearance. The default is <see cref="F:System.Windows.Forms.DataGridViewComboBoxDisplayStyle.DropDownButton" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x0009AE39 File Offset: 0x00099039
		// (set) Token: 0x06002029 RID: 8233 RVA: 0x0009AE60 File Offset: 0x00099060
		[DefaultValue(DataGridViewComboBoxDisplayStyle.DropDownButton)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ComboBoxColumnDisplayStyleDescr")]
		public DataGridViewComboBoxDisplayStyle DisplayStyle
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.DisplayStyle;
			}
			set
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ComboBoxCellTemplate.DisplayStyle = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
						if (dataGridViewComboBoxCell != null)
						{
							dataGridViewComboBoxCell.DisplayStyleInternal = value;
						}
					}
					base.DataGridView.InvalidateColumn(base.Index);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.DisplayStyle" /> property value applies only to the current cell in the <see cref="T:System.Windows.Forms.DataGridView" /> control when the current cell is in this column.</summary>
		/// <returns>
		///   <see langword="true" /> if the display style applies only to the current cell; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x0009AEF5 File Offset: 0x000990F5
		// (set) Token: 0x0600202B RID: 8235 RVA: 0x0009AF1C File Offset: 0x0009911C
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ComboBoxColumnDisplayStyleForCurrentCellOnlyDescr")]
		public bool DisplayStyleForCurrentCellOnly
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.DisplayStyleForCurrentCellOnly;
			}
			set
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ComboBoxCellTemplate.DisplayStyleForCurrentCellOnly = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
						if (dataGridViewComboBoxCell != null)
						{
							dataGridViewComboBoxCell.DisplayStyleForCurrentCellOnlyInternal = value;
						}
					}
					base.DataGridView.InvalidateColumn(base.Index);
				}
			}
		}

		/// <summary>Gets or sets the width of the drop-down lists of the combo boxes.</summary>
		/// <returns>The width, in pixels, of the drop-down lists. The default is 1.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x0009AFB1 File Offset: 0x000991B1
		// (set) Token: 0x0600202D RID: 8237 RVA: 0x0009AFD8 File Offset: 0x000991D8
		[DefaultValue(1)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ComboBoxColumnDropDownWidthDescr")]
		public int DropDownWidth
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.DropDownWidth;
			}
			set
			{
				if (this.DropDownWidth != value)
				{
					this.ComboBoxCellTemplate.DropDownWidth = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
							if (dataGridViewComboBoxCell != null)
							{
								dataGridViewComboBoxCell.DropDownWidth = value;
							}
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the flat style appearance of the column's cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FlatStyle" /> value indicating the cell appearance. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x0009B04D File Offset: 0x0009924D
		// (set) Token: 0x0600202F RID: 8239 RVA: 0x0009B078 File Offset: 0x00099278
		[DefaultValue(FlatStyle.Standard)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ComboBoxColumnFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewComboBoxCell)this.CellTemplate).FlatStyle;
			}
			set
			{
				if (this.FlatStyle != value)
				{
					((DataGridViewComboBoxCell)this.CellTemplate).FlatStyle = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
							if (dataGridViewComboBoxCell != null)
							{
								dataGridViewComboBoxCell.FlatStyleInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		/// <summary>Gets the collection of objects used as selections in the combo boxes.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> that represents the selections in the combo boxes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x0009B103 File Offset: 0x00099303
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_ComboBoxColumnItemsDescr")]
		public DataGridViewComboBoxCell.ObjectCollection Items
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.GetItems(base.DataGridView);
			}
		}

		/// <summary>Gets or sets a string that specifies the property or column from which to get values that correspond to the selections in the drop-down list.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of a property or column used in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.DataSource" /> property. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x0009B12E File Offset: 0x0009932E
		// (set) Token: 0x06002032 RID: 8242 RVA: 0x0009B154 File Offset: 0x00099354
		[DefaultValue("")]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_ComboBoxColumnValueMemberDescr")]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string ValueMember
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.ValueMember;
			}
			set
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ComboBoxCellTemplate.ValueMember = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
						if (dataGridViewComboBoxCell != null)
						{
							dataGridViewComboBoxCell.ValueMember = value;
						}
					}
					base.DataGridView.OnColumnCommonChange(base.Index);
				}
			}
		}

		/// <summary>Gets or sets the maximum number of items in the drop-down list of the cells in the column.</summary>
		/// <returns>The maximum number of drop-down list items, from 1 to 100. The default is 8.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x0009B1E9 File Offset: 0x000993E9
		// (set) Token: 0x06002034 RID: 8244 RVA: 0x0009B210 File Offset: 0x00099410
		[DefaultValue(8)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ComboBoxColumnMaxDropDownItemsDescr")]
		public int MaxDropDownItems
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.MaxDropDownItems;
			}
			set
			{
				if (this.MaxDropDownItems != value)
				{
					this.ComboBoxCellTemplate.MaxDropDownItems = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
							if (dataGridViewComboBoxCell != null)
							{
								dataGridViewComboBoxCell.MaxDropDownItems = value;
							}
						}
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the combo box are sorted.</summary>
		/// <returns>
		///   <see langword="true" /> if the combo box is sorted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x0009B285 File Offset: 0x00099485
		// (set) Token: 0x06002036 RID: 8246 RVA: 0x0009B2AC File Offset: 0x000994AC
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ComboBoxColumnSortedDescr")]
		public bool Sorted
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.Sorted;
			}
			set
			{
				if (this.Sorted != value)
				{
					this.ComboBoxCellTemplate.Sorted = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
							if (dataGridViewComboBoxCell != null)
							{
								dataGridViewComboBoxCell.Sorted = value;
							}
						}
					}
				}
			}
		}

		/// <summary>Creates an exact copy of this column.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewComboBoxColumn" />.</returns>
		// Token: 0x06002037 RID: 8247 RVA: 0x0009B324 File Offset: 0x00099524
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewComboBoxColumn dataGridViewComboBoxColumn;
			if (type == DataGridViewComboBoxColumn.columnType)
			{
				dataGridViewComboBoxColumn = new DataGridViewComboBoxColumn();
			}
			else
			{
				dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)Activator.CreateInstance(type);
			}
			if (dataGridViewComboBoxColumn != null)
			{
				base.CloneInternal(dataGridViewComboBoxColumn);
				((DataGridViewComboBoxCell)dataGridViewComboBoxColumn.CellTemplate).TemplateComboBoxColumn = dataGridViewComboBoxColumn;
			}
			return dataGridViewComboBoxColumn;
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x0009B378 File Offset: 0x00099578
		internal void OnItemsCollectionChanged()
		{
			if (base.DataGridView != null)
			{
				DataGridViewRowCollection rows = base.DataGridView.Rows;
				int count = rows.Count;
				object[] array = ((DataGridViewComboBoxCell)this.CellTemplate).Items.InnerArray.ToArray();
				for (int i = 0; i < count; i++)
				{
					DataGridViewRow dataGridViewRow = rows.SharedRow(i);
					DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
					if (dataGridViewComboBoxCell != null)
					{
						dataGridViewComboBoxCell.Items.ClearInternal();
						dataGridViewComboBoxCell.Items.AddRangeInternal(array);
					}
				}
				base.DataGridView.OnColumnCommonChange(base.Index);
			}
		}

		/// <summary>Gets a string that describes the column.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		// Token: 0x06002039 RID: 8249 RVA: 0x0009B41C File Offset: 0x0009961C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewComboBoxColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000D82 RID: 3458
		private static Type columnType = typeof(DataGridViewComboBoxColumn);
	}
}
