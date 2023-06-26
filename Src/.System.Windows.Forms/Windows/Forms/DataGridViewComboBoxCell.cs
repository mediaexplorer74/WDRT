using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Displays a combo box in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001C6 RID: 454
	public class DataGridViewComboBoxCell : DataGridViewCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> class.</summary>
		// Token: 0x06001FC3 RID: 8131 RVA: 0x00097560 File Offset: 0x00095760
		public DataGridViewComboBoxCell()
		{
			this.flags = 8;
			if (!DataGridViewComboBoxCell.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					DataGridViewComboBoxCell.offset2X = DpiHelper.LogicalToDeviceUnitsX(DataGridViewComboBoxCell.OFFSET_2PIXELS);
					DataGridViewComboBoxCell.offset2Y = DpiHelper.LogicalToDeviceUnitsY(DataGridViewComboBoxCell.OFFSET_2PIXELS);
					DataGridViewComboBoxCell.nonXPTriangleWidth = (byte)DpiHelper.LogicalToDeviceUnitsX(7);
					DataGridViewComboBoxCell.nonXPTriangleHeight = (byte)DpiHelper.LogicalToDeviceUnitsY(4);
				}
				DataGridViewComboBoxCell.isScalingInitialized = true;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Windows.Forms.AccessibleObject" /> for this <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> instance.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.AccessibleObject" /> instance that supports the <see cref="T:System.Windows.Automation.ControlType" /> UI Automation property.</returns>
		// Token: 0x06001FC4 RID: 8132 RVA: 0x000975C4 File Offset: 0x000957C4
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level2)
			{
				return new DataGridViewComboBoxCell.DataGridViewComboBoxCellAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		/// <summary>Gets or sets a value indicating whether the cell will match the characters being entered in the cell with a selection from the drop-down list.</summary>
		/// <returns>
		///   <see langword="true" /> if automatic completion is activated; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x000975DA File Offset: 0x000957DA
		// (set) Token: 0x06001FC6 RID: 8134 RVA: 0x000975E8 File Offset: 0x000957E8
		[DefaultValue(true)]
		public virtual bool AutoComplete
		{
			get
			{
				return (this.flags & 8) > 0;
			}
			set
			{
				if (value != this.AutoComplete)
				{
					if (value)
					{
						this.flags |= 8;
					}
					else
					{
						this.flags = (byte)((int)this.flags & -9);
					}
					if (this.OwnsEditingComboBox(base.RowIndex))
					{
						if (value)
						{
							this.EditingComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
							this.EditingComboBox.AutoCompleteMode = AutoCompleteMode.Append;
							return;
						}
						this.EditingComboBox.AutoCompleteMode = AutoCompleteMode.None;
						this.EditingComboBox.AutoCompleteSource = AutoCompleteSource.None;
					}
				}
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x0009766C File Offset: 0x0009586C
		// (set) Token: 0x06001FC8 RID: 8136 RVA: 0x0009767A File Offset: 0x0009587A
		private CurrencyManager DataManager
		{
			get
			{
				return this.GetDataManager(base.DataGridView);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellDataManager))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellDataManager, value);
				}
			}
		}

		/// <summary>Gets or sets the data source whose data contains the possible selections shown in the drop-down list.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> or <see cref="T:System.ComponentModel.IListSource" /> that contains a collection of values used to supply data to the drop-down list. The default value is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not <see langword="null" /> and is not of type <see cref="T:System.Collections.IList" /> nor <see cref="T:System.ComponentModel.IListSource" />.</exception>
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x000976A2 File Offset: 0x000958A2
		// (set) Token: 0x06001FCA RID: 8138 RVA: 0x000976B4 File Offset: 0x000958B4
		public virtual object DataSource
		{
			get
			{
				return base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellDataSource);
			}
			set
			{
				if (value != null && !(value is IList) && !(value is IListSource))
				{
					throw new ArgumentException(SR.GetString("BadDataSourceForComplexBinding"));
				}
				if (this.DataSource != value)
				{
					this.DataManager = null;
					this.UnwireDataSource();
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellDataSource, value);
					this.WireDataSource(value);
					this.CreateItemsFromDataSource = true;
					DataGridViewComboBoxCell.cachedDropDownWidth = -1;
					try
					{
						this.InitializeDisplayMemberPropertyDescriptor(this.DisplayMember);
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
						this.DisplayMemberInternal = null;
					}
					try
					{
						this.InitializeValueMemberPropertyDescriptor(this.ValueMember);
					}
					catch (Exception ex2)
					{
						if (ClientUtils.IsCriticalException(ex2))
						{
							throw;
						}
						this.ValueMemberInternal = null;
					}
					if (value == null)
					{
						this.DisplayMemberInternal = null;
						this.ValueMemberInternal = null;
					}
					if (this.OwnsEditingComboBox(base.RowIndex))
					{
						this.EditingComboBox.DataSource = value;
						this.InitializeComboBoxText();
						return;
					}
					base.OnCommonChange();
				}
			}
		}

		/// <summary>Gets or sets a string that specifies where to gather selections to display in the drop-down list.</summary>
		/// <returns>A string specifying the name of a property or column in the data source specified in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property. The default value is <see cref="F:System.String.Empty" />, which indicates that the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DisplayMember" /> property will not be used.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property is not <see langword="null" /> and the specified value when setting this property is not <see langword="null" /> or <see cref="F:System.String.Empty" /> and does not name a valid property or column in the data source.</exception>
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x000977BC File Offset: 0x000959BC
		// (set) Token: 0x06001FCC RID: 8140 RVA: 0x000977E9 File Offset: 0x000959E9
		[DefaultValue("")]
		public virtual string DisplayMember
		{
			get
			{
				object @object = base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMember);
				if (@object == null)
				{
					return string.Empty;
				}
				return (string)@object;
			}
			set
			{
				this.DisplayMemberInternal = value;
				if (this.OwnsEditingComboBox(base.RowIndex))
				{
					this.EditingComboBox.DisplayMember = value;
					this.InitializeComboBoxText();
					return;
				}
				base.OnCommonChange();
			}
		}

		// Token: 0x17000720 RID: 1824
		// (set) Token: 0x06001FCD RID: 8141 RVA: 0x00097819 File Offset: 0x00095A19
		private string DisplayMemberInternal
		{
			set
			{
				this.InitializeDisplayMemberPropertyDescriptor(value);
				if ((value != null && value.Length > 0) || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMember))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMember, value);
				}
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x00097851 File Offset: 0x00095A51
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x00097868 File Offset: 0x00095A68
		private PropertyDescriptor DisplayMemberProperty
		{
			get
			{
				return (PropertyDescriptor)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMemberProp);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMemberProp))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMemberProp, value);
				}
			}
		}

		/// <summary>Gets or sets a value that determines how the combo box is displayed when it is not in edit mode.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxDisplayStyle" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewComboBoxDisplayStyle.DropDownButton" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewComboBoxDisplayStyle" /> value.</exception>
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x00097890 File Offset: 0x00095A90
		// (set) Token: 0x06001FD1 RID: 8145 RVA: 0x000978B8 File Offset: 0x00095AB8
		[DefaultValue(DataGridViewComboBoxDisplayStyle.DropDownButton)]
		public DataGridViewComboBoxDisplayStyle DisplayStyle
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyle, out flag);
				if (flag)
				{
					return (DataGridViewComboBoxDisplayStyle)integer;
				}
				return DataGridViewComboBoxDisplayStyle.DropDownButton;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewComboBoxDisplayStyle));
				}
				if (value != this.DisplayStyle)
				{
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyle, (int)value);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x17000723 RID: 1827
		// (set) Token: 0x06001FD2 RID: 8146 RVA: 0x00097934 File Offset: 0x00095B34
		internal DataGridViewComboBoxDisplayStyle DisplayStyleInternal
		{
			set
			{
				if (value != this.DisplayStyle)
				{
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyle, (int)value);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DisplayStyle" /> property value applies to the cell only when it is the current cell in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <returns>
		///   <see langword="true" /> if the display style applies to the cell only when it is the current cell; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x00097950 File Offset: 0x00095B50
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x0009797C File Offset: 0x00095B7C
		[DefaultValue(false)]
		public bool DisplayStyleForCurrentCellOnly
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyleForCurrentCellOnly, out flag);
				return flag && integer != 0;
			}
			set
			{
				if (value != this.DisplayStyleForCurrentCellOnly)
				{
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyleForCurrentCellOnly, value ? 1 : 0);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x17000725 RID: 1829
		// (set) Token: 0x06001FD5 RID: 8149 RVA: 0x000979D8 File Offset: 0x00095BD8
		internal bool DisplayStyleForCurrentCellOnlyInternal
		{
			set
			{
				if (value != this.DisplayStyleForCurrentCellOnly)
				{
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyleForCurrentCellOnly, value ? 1 : 0);
				}
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x000979FA File Offset: 0x00095BFA
		private Type DisplayType
		{
			get
			{
				if (this.DisplayMemberProperty != null)
				{
					return this.DisplayMemberProperty.PropertyType;
				}
				if (this.ValueMemberProperty != null)
				{
					return this.ValueMemberProperty.PropertyType;
				}
				return DataGridViewComboBoxCell.defaultFormattedValueType;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x00097A29 File Offset: 0x00095C29
		private TypeConverter DisplayTypeConverter
		{
			get
			{
				if (base.DataGridView != null)
				{
					return base.DataGridView.GetCachedTypeConverter(this.DisplayType);
				}
				return TypeDescriptor.GetConverter(this.DisplayType);
			}
		}

		/// <summary>Gets or sets the width of the of the drop-down list portion of a combo box.</summary>
		/// <returns>The width, in pixels, of the drop-down list. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than one.</exception>
		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x00097A50 File Offset: 0x00095C50
		// (set) Token: 0x06001FD9 RID: 8153 RVA: 0x00097A78 File Offset: 0x00095C78
		[DefaultValue(1)]
		public virtual int DropDownWidth
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewComboBoxCell.PropComboBoxCellDropDownWidth, out flag);
				if (!flag)
				{
					return 1;
				}
				return integer;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("DropDownWidth", value, SR.GetString("DataGridViewComboBoxCell_DropDownWidthOutOfRange", new object[] { 1.ToString(CultureInfo.CurrentCulture) }));
				}
				base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellDropDownWidth, value);
				if (this.OwnsEditingComboBox(base.RowIndex))
				{
					this.EditingComboBox.DropDownWidth = value;
				}
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x00097AE6 File Offset: 0x00095CE6
		// (set) Token: 0x06001FDB RID: 8155 RVA: 0x00097AFD File Offset: 0x00095CFD
		private DataGridViewComboBoxEditingControl EditingComboBox
		{
			get
			{
				return (DataGridViewComboBoxEditingControl)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellEditingComboBox);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellEditingComboBox))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellEditingComboBox, value);
				}
			}
		}

		/// <summary>Gets the type of the cell's hosted editing control.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the underlying editing control. This property always returns <see cref="T:System.Windows.Forms.DataGridViewComboBoxEditingControl" />.</returns>
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x00097B25 File Offset: 0x00095D25
		public override Type EditType
		{
			get
			{
				return DataGridViewComboBoxCell.defaultEditType;
			}
		}

		/// <summary>Gets or sets the flat style appearance of the cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a valid <see cref="T:System.Windows.Forms.FlatStyle" /> value.</exception>
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x00097B2C File Offset: 0x00095D2C
		// (set) Token: 0x06001FDE RID: 8158 RVA: 0x00097B54 File Offset: 0x00095D54
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewComboBoxCell.PropComboBoxCellFlatStyle, out flag);
				if (flag)
				{
					return (FlatStyle)integer;
				}
				return FlatStyle.Standard;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FlatStyle));
				}
				if (value != this.FlatStyle)
				{
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellFlatStyle, (int)value);
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x1700072C RID: 1836
		// (set) Token: 0x06001FDF RID: 8159 RVA: 0x00097BA7 File Offset: 0x00095DA7
		internal FlatStyle FlatStyleInternal
		{
			set
			{
				if (value != this.FlatStyle)
				{
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellFlatStyle, (int)value);
				}
			}
		}

		/// <summary>Gets the class type of the formatted value associated with the cell.</summary>
		/// <returns>The type of the cell's formatted value. This property always returns <see cref="T:System.String" />.</returns>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x00097BC3 File Offset: 0x00095DC3
		public override Type FormattedValueType
		{
			get
			{
				return DataGridViewComboBoxCell.defaultFormattedValueType;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00097BCA File Offset: 0x00095DCA
		internal bool HasItems
		{
			get
			{
				return base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellItems) && base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellItems) != null;
			}
		}

		/// <summary>Gets the objects that represent the selection displayed in the drop-down list.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> containing the selection.</returns>
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x00097BF3 File Offset: 0x00095DF3
		[Browsable(false)]
		public virtual DataGridViewComboBoxCell.ObjectCollection Items
		{
			get
			{
				return this.GetItems(base.DataGridView);
			}
		}

		/// <summary>Gets or sets the maximum number of items shown in the drop-down list.</summary>
		/// <returns>The number of drop-down list items to allow. The minimum is 1 and the maximum is 100; the default is 8.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 1 or greater than 100 when setting this property.</exception>
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x00097C04 File Offset: 0x00095E04
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x00097C2C File Offset: 0x00095E2C
		[DefaultValue(8)]
		public virtual int MaxDropDownItems
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewComboBoxCell.PropComboBoxCellMaxDropDownItems, out flag);
				if (flag)
				{
					return integer;
				}
				return 8;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("MaxDropDownItems", value, SR.GetString("DataGridViewComboBoxCell_MaxDropDownItemsOutOfRange", new object[]
					{
						1.ToString(CultureInfo.CurrentCulture),
						100.ToString(CultureInfo.CurrentCulture)
					}));
				}
				base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellMaxDropDownItems, value);
				if (this.OwnsEditingComboBox(base.RowIndex))
				{
					this.EditingComboBox.MaxDropDownItems = value;
				}
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00097CB4 File Offset: 0x00095EB4
		private bool PaintXPThemes
		{
			get
			{
				return this.FlatStyle != FlatStyle.Flat && this.FlatStyle != FlatStyle.Popup && base.DataGridView.ApplyVisualStylesToInnerCells;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x00097CE6 File Offset: 0x00095EE6
		private static bool PostXPThemesExist
		{
			get
			{
				return VisualStyleRenderer.IsElementDefined(VisualStyleElement.ComboBox.ReadOnlyButton.Normal);
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the combo box are automatically sorted.</summary>
		/// <returns>
		///   <see langword="true" /> if the combo box is sorted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to sort a cell that is attached to a data source.</exception>
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x00097CF2 File Offset: 0x00095EF2
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x00097D00 File Offset: 0x00095F00
		[DefaultValue(false)]
		public virtual bool Sorted
		{
			get
			{
				return (this.flags & 2) > 0;
			}
			set
			{
				if (value != this.Sorted)
				{
					if (value)
					{
						if (this.DataSource != null)
						{
							throw new ArgumentException(SR.GetString("ComboBoxSortWithDataSource"));
						}
						this.Items.SortInternal();
						this.flags |= 2;
					}
					else
					{
						this.flags = (byte)((int)this.flags & -3);
					}
					if (this.OwnsEditingComboBox(base.RowIndex))
					{
						this.EditingComboBox.Sorted = value;
					}
				}
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x00097D79 File Offset: 0x00095F79
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x00097D90 File Offset: 0x00095F90
		internal DataGridViewComboBoxColumn TemplateComboBoxColumn
		{
			get
			{
				return (DataGridViewComboBoxColumn)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellColumnTemplate);
			}
			set
			{
				base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellColumnTemplate, value);
			}
		}

		/// <summary>Gets or sets a string that specifies where to gather the underlying values used in the drop-down list.</summary>
		/// <returns>A string specifying the name of a property or column. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is ignored.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property is not <see langword="null" /> and the specified value when setting this property is not <see langword="null" /> or <see cref="F:System.String.Empty" /> and does not name a valid property or column in the data source.</exception>
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x00097DA4 File Offset: 0x00095FA4
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x00097DD1 File Offset: 0x00095FD1
		[DefaultValue("")]
		public virtual string ValueMember
		{
			get
			{
				object @object = base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellValueMember);
				if (@object == null)
				{
					return string.Empty;
				}
				return (string)@object;
			}
			set
			{
				this.ValueMemberInternal = value;
				if (this.OwnsEditingComboBox(base.RowIndex))
				{
					this.EditingComboBox.ValueMember = value;
					this.InitializeComboBoxText();
					return;
				}
				base.OnCommonChange();
			}
		}

		// Token: 0x17000736 RID: 1846
		// (set) Token: 0x06001FED RID: 8173 RVA: 0x00097E01 File Offset: 0x00096001
		private string ValueMemberInternal
		{
			set
			{
				this.InitializeValueMemberPropertyDescriptor(value);
				if ((value != null && value.Length > 0) || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellValueMember))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellValueMember, value);
				}
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x00097E39 File Offset: 0x00096039
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x00097E50 File Offset: 0x00096050
		private PropertyDescriptor ValueMemberProperty
		{
			get
			{
				return (PropertyDescriptor)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellValueMemberProp);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellValueMemberProp))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellValueMemberProp, value);
				}
			}
		}

		/// <summary>Gets or sets the data type of the values in the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x00097E78 File Offset: 0x00096078
		public override Type ValueType
		{
			get
			{
				if (this.ValueMemberProperty != null)
				{
					return this.ValueMemberProperty.PropertyType;
				}
				if (this.DisplayMemberProperty != null)
				{
					return this.DisplayMemberProperty.PropertyType;
				}
				Type valueType = base.ValueType;
				if (valueType != null)
				{
					return valueType;
				}
				return DataGridViewComboBoxCell.defaultValueType;
			}
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00097EC4 File Offset: 0x000960C4
		internal override void CacheEditingControl()
		{
			this.EditingComboBox = base.DataGridView.EditingControl as DataGridViewComboBoxEditingControl;
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x00097EDC File Offset: 0x000960DC
		private void CheckDropDownList(int x, int y, int rowIndex)
		{
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = new DataGridViewAdvancedBorderStyle();
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle2 = this.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStyle, false, false, false, false);
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			Rectangle rectangle = this.BorderWidths(dataGridViewAdvancedBorderStyle2);
			rectangle.X += inheritedStyle.Padding.Left;
			rectangle.Y += inheritedStyle.Padding.Top;
			rectangle.Width += inheritedStyle.Padding.Right;
			rectangle.Height += inheritedStyle.Padding.Bottom;
			Size size = this.GetSize(rowIndex);
			Size size2 = new Size(size.Width - rectangle.X - rectangle.Width, size.Height - rectangle.Y - rectangle.Height);
			int num;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				num = Math.Min(this.GetDropDownButtonHeight(graphics, inheritedStyle), size2.Height - 2);
			}
			int num2 = Math.Min(SystemInformation.HorizontalScrollBarThumbWidth, size2.Width - 6 - 1);
			if (num > 0 && num2 > 0 && y >= rectangle.Y + 1 && y <= rectangle.Y + 1 + num)
			{
				if (base.DataGridView.RightToLeftInternal)
				{
					if (x >= rectangle.X + 1 && x <= rectangle.X + num2 + 1)
					{
						this.EditingComboBox.DroppedDown = true;
						return;
					}
				}
				else if (x >= size.Width - rectangle.Width - num2 - 1 && x <= size.Width - rectangle.Width - 1)
				{
					this.EditingComboBox.DroppedDown = true;
				}
			}
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000980B8 File Offset: 0x000962B8
		private void CheckNoDataSource()
		{
			if (this.DataSource != null)
			{
				throw new ArgumentException(SR.GetString("DataSourceLocksItems"));
			}
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000980D4 File Offset: 0x000962D4
		private void ComboBox_DropDown(object sender, EventArgs e)
		{
			ComboBox editingComboBox = this.EditingComboBox;
			DataGridViewComboBoxColumn dataGridViewComboBoxColumn = base.OwningColumn as DataGridViewComboBoxColumn;
			if (dataGridViewComboBoxColumn != null)
			{
				DataGridViewAutoSizeColumnMode inheritedAutoSizeMode = dataGridViewComboBoxColumn.GetInheritedAutoSizeMode(base.DataGridView);
				if (inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.ColumnHeader && inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.Fill && inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.None)
				{
					if (this.DropDownWidth == 1)
					{
						if (DataGridViewComboBoxCell.cachedDropDownWidth == -1)
						{
							int num = -1;
							if ((this.HasItems || this.CreateItemsFromDataSource) && this.Items.Count > 0)
							{
								foreach (object obj in this.Items)
								{
									Size size = TextRenderer.MeasureText(editingComboBox.GetItemText(obj), editingComboBox.Font);
									if (size.Width > num)
									{
										num = size.Width;
									}
								}
							}
							DataGridViewComboBoxCell.cachedDropDownWidth = num + 2 + SystemInformation.VerticalScrollBarWidth;
						}
						UnsafeNativeMethods.SendMessage(new HandleRef(editingComboBox, editingComboBox.Handle), 352, DataGridViewComboBoxCell.cachedDropDownWidth, 0);
						return;
					}
				}
				else
				{
					int num2 = (int)(long)UnsafeNativeMethods.SendMessage(new HandleRef(editingComboBox, editingComboBox.Handle), 351, 0, 0);
					if (num2 != this.DropDownWidth)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(editingComboBox, editingComboBox.Handle), 352, this.DropDownWidth, 0);
					}
				}
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</returns>
		// Token: 0x06001FF5 RID: 8181 RVA: 0x00098238 File Offset: 0x00096438
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewComboBoxCell dataGridViewComboBoxCell;
			if (type == DataGridViewComboBoxCell.cellType)
			{
				dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
			}
			else
			{
				dataGridViewComboBoxCell = (DataGridViewComboBoxCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewComboBoxCell);
			dataGridViewComboBoxCell.DropDownWidth = this.DropDownWidth;
			dataGridViewComboBoxCell.MaxDropDownItems = this.MaxDropDownItems;
			dataGridViewComboBoxCell.CreateItemsFromDataSource = false;
			dataGridViewComboBoxCell.DataSource = this.DataSource;
			dataGridViewComboBoxCell.DisplayMember = this.DisplayMember;
			dataGridViewComboBoxCell.ValueMember = this.ValueMember;
			if (this.HasItems && this.DataSource == null && this.Items.Count > 0)
			{
				dataGridViewComboBoxCell.Items.AddRangeInternal(this.Items.InnerArray.ToArray());
			}
			dataGridViewComboBoxCell.AutoComplete = this.AutoComplete;
			dataGridViewComboBoxCell.Sorted = this.Sorted;
			dataGridViewComboBoxCell.FlatStyleInternal = this.FlatStyle;
			dataGridViewComboBoxCell.DisplayStyleInternal = this.DisplayStyle;
			dataGridViewComboBoxCell.DisplayStyleForCurrentCellOnlyInternal = this.DisplayStyleForCurrentCellOnly;
			return dataGridViewComboBoxCell;
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x0009832D File Offset: 0x0009652D
		// (set) Token: 0x06001FF7 RID: 8183 RVA: 0x0009833A File Offset: 0x0009653A
		private bool CreateItemsFromDataSource
		{
			get
			{
				return (this.flags & 4) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 4;
					return;
				}
				this.flags = (byte)((int)this.flags & -5);
			}
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0009835F File Offset: 0x0009655F
		private void DataSource_Disposed(object sender, EventArgs e)
		{
			this.DataSource = null;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00098368 File Offset: 0x00096568
		private void DataSource_Initialized(object sender, EventArgs e)
		{
			ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null)
			{
				supportInitializeNotification.Initialized -= this.DataSource_Initialized;
			}
			this.flags = (byte)((int)this.flags & -17);
			this.InitializeDisplayMemberPropertyDescriptor(this.DisplayMember);
			this.InitializeValueMemberPropertyDescriptor(this.ValueMember);
		}

		/// <summary>Removes the cell's editing control from the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x06001FFA RID: 8186 RVA: 0x000983C0 File Offset: 0x000965C0
		public override void DetachEditingControl()
		{
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView == null || dataGridView.EditingControl == null)
			{
				throw new InvalidOperationException();
			}
			if (this.EditingComboBox != null && (this.flags & 32) != 0)
			{
				this.EditingComboBox.DropDown -= this.ComboBox_DropDown;
				this.flags = (byte)((int)this.flags & -33);
			}
			this.EditingComboBox = null;
			base.DetachEditingControl();
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> and cell style.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		// Token: 0x06001FFB RID: 8187 RVA: 0x0009842C File Offset: 0x0009662C
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null)
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			object editedFormattedValue = base.GetEditedFormattedValue(value, rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			Rectangle rectangle2;
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, editedFormattedValue, null, cellStyle, dataGridViewAdvancedBorderStyle, out rectangle2, DataGridViewPaintParts.ContentForeground, true, false, false, false);
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x000984A0 File Offset: 0x000966A0
		private CurrencyManager GetDataManager(DataGridView dataGridView)
		{
			CurrencyManager currencyManager = (CurrencyManager)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellDataManager);
			if (currencyManager == null && this.DataSource != null && dataGridView != null && dataGridView.BindingContext != null && this.DataSource != Convert.DBNull)
			{
				ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
				if (supportInitializeNotification != null && !supportInitializeNotification.IsInitialized)
				{
					if ((this.flags & 16) == 0)
					{
						supportInitializeNotification.Initialized += this.DataSource_Initialized;
						this.flags |= 16;
					}
				}
				else
				{
					currencyManager = (CurrencyManager)dataGridView.BindingContext[this.DataSource];
					this.DataManager = currencyManager;
				}
			}
			return currencyManager;
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x00098550 File Offset: 0x00096750
		private int GetDropDownButtonHeight(Graphics graphics, DataGridViewCellStyle cellStyle)
		{
			int num = 4;
			if (this.PaintXPThemes)
			{
				if (DataGridViewComboBoxCell.PostXPThemesExist)
				{
					num = 8;
				}
				else
				{
					num = 6;
				}
			}
			return DataGridViewCell.MeasureTextHeight(graphics, " ", cellStyle.Font, int.MaxValue, TextFormatFlags.Default) + num;
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		// Token: 0x06001FFE RID: 8190 RVA: 0x00098590 File Offset: 0x00096790
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null || !base.DataGridView.ShowCellErrors || string.IsNullOrEmpty(this.GetErrorText(rowIndex)))
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			object editedFormattedValue = base.GetEditedFormattedValue(value, rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			Rectangle rectangle2;
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, editedFormattedValue, this.GetErrorText(rowIndex), cellStyle, dataGridViewAdvancedBorderStyle, out rectangle2, DataGridViewPaintParts.ContentForeground, false, true, false, false);
		}

		/// <summary>Gets the formatted value of the cell's data.</summary>
		/// <param name="value">The value to be formatted.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or <see langword="null" /> if no such custom conversion is needed.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or <see langword="null" /> if no such custom conversion is needed.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed.</param>
		/// <returns>The value of the cell's data after formatting has been applied or <see langword="null" /> if the cell is not part of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to <see langword="true" />. The exception object can typically be cast to type <see cref="T:System.FormatException" /> for type conversion errors or to type <see cref="T:System.ArgumentException" /> if <paramref name="value" /> cannot be found in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> or the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.Items" /> collection.</exception>
		// Token: 0x06001FFF RID: 8191 RVA: 0x00098624 File Offset: 0x00096824
		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if (valueTypeConverter == null)
			{
				if (this.ValueMemberProperty != null)
				{
					valueTypeConverter = this.ValueMemberProperty.Converter;
				}
				else if (this.DisplayMemberProperty != null)
				{
					valueTypeConverter = this.DisplayMemberProperty.Converter;
				}
			}
			if (value == null || (this.ValueType != null && !this.ValueType.IsAssignableFrom(value.GetType()) && value != DBNull.Value))
			{
				if (value == null)
				{
					return base.GetFormattedValue(null, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
				}
				if (base.DataGridView != null)
				{
					DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs = new DataGridViewDataErrorEventArgs(new FormatException(SR.GetString("DataGridViewComboBoxCell_InvalidValue")), base.ColumnIndex, rowIndex, context);
					base.RaiseDataError(dataGridViewDataErrorEventArgs);
					if (dataGridViewDataErrorEventArgs.ThrowException)
					{
						throw dataGridViewDataErrorEventArgs.Exception;
					}
				}
				return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
			}
			else
			{
				string text = value as string;
				if ((this.DataManager != null && (this.ValueMemberProperty != null || this.DisplayMemberProperty != null)) || !string.IsNullOrEmpty(this.ValueMember) || !string.IsNullOrEmpty(this.DisplayMember))
				{
					object obj;
					if (!this.LookupDisplayValue(rowIndex, value, out obj))
					{
						if (value == DBNull.Value)
						{
							obj = DBNull.Value;
						}
						else if (text != null && string.IsNullOrEmpty(text) && this.DisplayType == typeof(string))
						{
							obj = string.Empty;
						}
						else if (base.DataGridView != null)
						{
							DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs2 = new DataGridViewDataErrorEventArgs(new ArgumentException(SR.GetString("DataGridViewComboBoxCell_InvalidValue")), base.ColumnIndex, rowIndex, context);
							base.RaiseDataError(dataGridViewDataErrorEventArgs2);
							if (dataGridViewDataErrorEventArgs2.ThrowException)
							{
								throw dataGridViewDataErrorEventArgs2.Exception;
							}
							if (this.OwnsEditingComboBox(rowIndex))
							{
								((IDataGridViewEditingControl)this.EditingComboBox).EditingControlValueChanged = true;
								base.DataGridView.NotifyCurrentCellDirty(true);
							}
						}
					}
					return base.GetFormattedValue(obj, rowIndex, ref cellStyle, this.DisplayTypeConverter, formattedValueTypeConverter, context);
				}
				if (!this.Items.Contains(value) && value != DBNull.Value && (!(value is string) || !string.IsNullOrEmpty(text)))
				{
					if (base.DataGridView != null)
					{
						DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs3 = new DataGridViewDataErrorEventArgs(new ArgumentException(SR.GetString("DataGridViewComboBoxCell_InvalidValue")), base.ColumnIndex, rowIndex, context);
						base.RaiseDataError(dataGridViewDataErrorEventArgs3);
						if (dataGridViewDataErrorEventArgs3.ThrowException)
						{
							throw dataGridViewDataErrorEventArgs3.Exception;
						}
					}
					if (this.Items.Count > 0)
					{
						value = this.Items[0];
					}
					else
					{
						value = string.Empty;
					}
				}
				return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
			}
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x00098880 File Offset: 0x00096A80
		internal string GetItemDisplayText(object item)
		{
			object itemDisplayValue = this.GetItemDisplayValue(item);
			if (itemDisplayValue == null)
			{
				return string.Empty;
			}
			return Convert.ToString(itemDisplayValue, CultureInfo.CurrentCulture);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000988AC File Offset: 0x00096AAC
		internal object GetItemDisplayValue(object item)
		{
			bool flag = false;
			object obj = null;
			if (this.DisplayMemberProperty != null)
			{
				obj = this.DisplayMemberProperty.GetValue(item);
				flag = true;
			}
			else if (this.ValueMemberProperty != null)
			{
				obj = this.ValueMemberProperty.GetValue(item);
				flag = true;
			}
			else if (!string.IsNullOrEmpty(this.DisplayMember))
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(item).Find(this.DisplayMember, true);
				if (propertyDescriptor != null)
				{
					obj = propertyDescriptor.GetValue(item);
					flag = true;
				}
			}
			else if (!string.IsNullOrEmpty(this.ValueMember))
			{
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(item).Find(this.ValueMember, true);
				if (propertyDescriptor2 != null)
				{
					obj = propertyDescriptor2.GetValue(item);
					flag = true;
				}
			}
			if (!flag)
			{
				obj = item;
			}
			return obj;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00098954 File Offset: 0x00096B54
		internal DataGridViewComboBoxCell.ObjectCollection GetItems(DataGridView dataGridView)
		{
			DataGridViewComboBoxCell.ObjectCollection objectCollection = (DataGridViewComboBoxCell.ObjectCollection)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellItems);
			if (objectCollection == null)
			{
				objectCollection = new DataGridViewComboBoxCell.ObjectCollection(this);
				base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellItems, objectCollection);
			}
			if (this.CreateItemsFromDataSource)
			{
				objectCollection.ClearInternal();
				CurrencyManager dataManager = this.GetDataManager(dataGridView);
				if (dataManager != null && dataManager.Count != -1)
				{
					object[] array = new object[dataManager.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = dataManager[i];
					}
					objectCollection.AddRangeInternal(array);
				}
				if (dataManager != null || (this.flags & 16) == 0)
				{
					this.CreateItemsFromDataSource = false;
				}
			}
			return objectCollection;
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x000989F8 File Offset: 0x00096BF8
		internal object GetItemValue(object item)
		{
			bool flag = false;
			object obj = null;
			if (this.ValueMemberProperty != null)
			{
				obj = this.ValueMemberProperty.GetValue(item);
				flag = true;
			}
			else if (this.DisplayMemberProperty != null)
			{
				obj = this.DisplayMemberProperty.GetValue(item);
				flag = true;
			}
			else if (!string.IsNullOrEmpty(this.ValueMember))
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(item).Find(this.ValueMember, true);
				if (propertyDescriptor != null)
				{
					obj = propertyDescriptor.GetValue(item);
					flag = true;
				}
			}
			if (!flag && !string.IsNullOrEmpty(this.DisplayMember))
			{
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(item).Find(this.DisplayMember, true);
				if (propertyDescriptor2 != null)
				{
					obj = propertyDescriptor2.GetValue(item);
					flag = true;
				}
			}
			if (!flag)
			{
				obj = item;
			}
			return obj;
		}

		/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		// Token: 0x06002004 RID: 8196 RVA: 0x00098AA0 File Offset: 0x00096CA0
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			Size size = Size.Empty;
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			Rectangle stdBorderWidths = base.StdBorderWidths;
			int num = stdBorderWidths.Left + stdBorderWidths.Width + cellStyle.Padding.Horizontal;
			int num2 = stdBorderWidths.Top + stdBorderWidths.Height + cellStyle.Padding.Vertical;
			TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			string text = base.GetFormattedValue(rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize) as string;
			if (!string.IsNullOrEmpty(text))
			{
				size = DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags);
			}
			else
			{
				size = DataGridViewCell.MeasureTextSize(graphics, " ", cellStyle.Font, textFormatFlags);
			}
			if (freeDimensionFromConstraint == DataGridViewFreeDimension.Height)
			{
				size.Width = 0;
			}
			else if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
			{
				size.Height = 0;
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				size.Width += SystemInformation.HorizontalScrollBarThumbWidth + 1 + 6 + num;
				if (base.DataGridView.ShowCellErrors)
				{
					size.Width = Math.Max(size.Width, num + SystemInformation.HorizontalScrollBarThumbWidth + 1 + 8 + (int)DataGridViewCell.iconsWidth);
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				if (this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup)
				{
					size.Height += 6;
				}
				else
				{
					size.Height += 8;
				}
				size.Height += num2;
				if (base.DataGridView.ShowCellErrors)
				{
					size.Height = Math.Max(size.Height, num2 + 8 + (int)DataGridViewCell.iconsHeight);
				}
			}
			return size;
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00098C58 File Offset: 0x00096E58
		private void InitializeComboBoxText()
		{
			((IDataGridViewEditingControl)this.EditingComboBox).EditingControlValueChanged = false;
			int editingControlRowIndex = ((IDataGridViewEditingControl)this.EditingComboBox).EditingControlRowIndex;
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, editingControlRowIndex, false);
			this.EditingComboBox.Text = (string)this.GetFormattedValue(this.GetValue(editingControlRowIndex), editingControlRowIndex, ref inheritedStyle, null, null, DataGridViewDataErrorContexts.Formatting);
		}

		/// <summary>Attaches and initializes the hosted editing control.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="initialFormattedValue">The initial value to be displayed in the control.</param>
		/// <param name="dataGridViewCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that determines the appearance of the hosted control.</param>
		// Token: 0x06002006 RID: 8198 RVA: 0x00098CAC File Offset: 0x00096EAC
		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			ComboBox comboBox = base.DataGridView.EditingControl as ComboBox;
			if (comboBox != null)
			{
				if ((this.GetInheritedState(rowIndex) & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
				{
					base.DataGridView.EditingPanel.BackColor = dataGridViewCellStyle.SelectionBackColor;
				}
				IntPtr intPtr;
				if (comboBox.ParentInternal != null)
				{
					intPtr = comboBox.ParentInternal.Handle;
				}
				intPtr = comboBox.Handle;
				comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				comboBox.FormattingEnabled = true;
				comboBox.MaxDropDownItems = this.MaxDropDownItems;
				comboBox.DropDownWidth = this.DropDownWidth;
				comboBox.DataSource = null;
				comboBox.ValueMember = null;
				comboBox.Items.Clear();
				comboBox.DataSource = this.DataSource;
				comboBox.DisplayMember = this.DisplayMember;
				comboBox.ValueMember = this.ValueMember;
				if (this.HasItems && this.DataSource == null && this.Items.Count > 0)
				{
					comboBox.Items.AddRange(this.Items.InnerArray.ToArray());
				}
				comboBox.Sorted = this.Sorted;
				comboBox.FlatStyle = this.FlatStyle;
				if (this.AutoComplete)
				{
					comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
					comboBox.AutoCompleteMode = AutoCompleteMode.Append;
				}
				else
				{
					comboBox.AutoCompleteMode = AutoCompleteMode.None;
					comboBox.AutoCompleteSource = AutoCompleteSource.None;
				}
				string text = initialFormattedValue as string;
				if (text == null)
				{
					text = string.Empty;
				}
				comboBox.Text = text;
				if ((this.flags & 32) == 0)
				{
					comboBox.DropDown += this.ComboBox_DropDown;
					this.flags |= 32;
				}
				DataGridViewComboBoxCell.cachedDropDownWidth = -1;
				this.EditingComboBox = base.DataGridView.EditingControl as DataGridViewComboBoxEditingControl;
				if (base.GetHeight(rowIndex) > 21)
				{
					Rectangle cellDisplayRectangle = base.DataGridView.GetCellDisplayRectangle(base.ColumnIndex, rowIndex, true);
					cellDisplayRectangle.Y += 21;
					cellDisplayRectangle.Height -= 21;
					base.DataGridView.Invalidate(cellDisplayRectangle);
				}
			}
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00098EA8 File Offset: 0x000970A8
		private void InitializeDisplayMemberPropertyDescriptor(string displayMember)
		{
			if (this.DataManager != null)
			{
				if (string.IsNullOrEmpty(displayMember))
				{
					this.DisplayMemberProperty = null;
					return;
				}
				BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(displayMember);
				this.DataManager = base.DataGridView.BindingContext[this.DataSource, bindingMemberInfo.BindingPath] as CurrencyManager;
				PropertyDescriptorCollection itemProperties = this.DataManager.GetItemProperties();
				PropertyDescriptor propertyDescriptor = itemProperties.Find(bindingMemberInfo.BindingField, true);
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(SR.GetString("DataGridViewComboBoxCell_FieldNotFound", new object[] { displayMember }));
				}
				this.DisplayMemberProperty = propertyDescriptor;
			}
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00098F3C File Offset: 0x0009713C
		private void InitializeValueMemberPropertyDescriptor(string valueMember)
		{
			if (this.DataManager != null)
			{
				if (string.IsNullOrEmpty(valueMember))
				{
					this.ValueMemberProperty = null;
					return;
				}
				BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(valueMember);
				this.DataManager = base.DataGridView.BindingContext[this.DataSource, bindingMemberInfo.BindingPath] as CurrencyManager;
				PropertyDescriptorCollection itemProperties = this.DataManager.GetItemProperties();
				PropertyDescriptor propertyDescriptor = itemProperties.Find(bindingMemberInfo.BindingField, true);
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(SR.GetString("DataGridViewComboBoxCell_FieldNotFound", new object[] { valueMember }));
				}
				this.ValueMemberProperty = propertyDescriptor;
			}
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00098FD0 File Offset: 0x000971D0
		private object ItemFromComboBoxDataSource(PropertyDescriptor property, object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			object obj = null;
			if (this.DataManager.List is IBindingList && ((IBindingList)this.DataManager.List).SupportsSearching)
			{
				int num = ((IBindingList)this.DataManager.List).Find(property, key);
				if (num != -1)
				{
					obj = this.DataManager.List[num];
				}
			}
			else
			{
				for (int i = 0; i < this.DataManager.List.Count; i++)
				{
					object obj2 = this.DataManager.List[i];
					object value = property.GetValue(obj2);
					if (key.Equals(value))
					{
						obj = obj2;
						break;
					}
				}
			}
			return obj;
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0009908C File Offset: 0x0009728C
		private object ItemFromComboBoxItems(int rowIndex, string field, object key)
		{
			object obj = null;
			if (this.OwnsEditingComboBox(rowIndex))
			{
				obj = this.EditingComboBox.SelectedItem;
				object obj2 = null;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj).Find(field, true);
				if (propertyDescriptor != null)
				{
					obj2 = propertyDescriptor.GetValue(obj);
				}
				if (obj2 == null || !obj2.Equals(key))
				{
					obj = null;
				}
			}
			if (obj == null)
			{
				foreach (object obj3 in this.Items)
				{
					object obj4 = null;
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(obj3).Find(field, true);
					if (propertyDescriptor2 != null)
					{
						obj4 = propertyDescriptor2.GetValue(obj3);
					}
					if (obj4 != null && obj4.Equals(key))
					{
						obj = obj3;
						break;
					}
				}
			}
			if (obj == null)
			{
				if (this.OwnsEditingComboBox(rowIndex))
				{
					obj = this.EditingComboBox.SelectedItem;
					if (obj == null || !obj.Equals(key))
					{
						obj = null;
					}
				}
				if (obj == null && this.Items.Contains(key))
				{
					obj = key;
				}
			}
			return obj;
		}

		/// <summary>Determines if edit mode should be started based on the given key.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that represents the key that was pressed.</param>
		/// <returns>
		///   <see langword="true" /> if edit mode should be started; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600200B RID: 8203 RVA: 0x00099190 File Offset: 0x00097390
		public override bool KeyEntersEditMode(KeyEventArgs e)
		{
			return (((char.IsLetterOrDigit((char)e.KeyCode) && (e.KeyCode < Keys.F1 || e.KeyCode > Keys.F24)) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.Divide) || (e.KeyCode >= Keys.OemSemicolon && e.KeyCode <= Keys.OemBackslash) || (e.KeyCode == Keys.Space && !e.Shift) || e.KeyCode == Keys.F4 || ((e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && e.Alt)) && (!e.Alt || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && !e.Control) || base.KeyEntersEditMode(e);
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x00099258 File Offset: 0x00097458
		private bool LookupDisplayValue(int rowIndex, object value, out object displayValue)
		{
			object obj;
			if (this.DisplayMemberProperty != null || this.ValueMemberProperty != null)
			{
				obj = this.ItemFromComboBoxDataSource((this.ValueMemberProperty != null) ? this.ValueMemberProperty : this.DisplayMemberProperty, value);
			}
			else
			{
				obj = this.ItemFromComboBoxItems(rowIndex, string.IsNullOrEmpty(this.ValueMember) ? this.DisplayMember : this.ValueMember, value);
			}
			if (obj == null)
			{
				displayValue = null;
				return false;
			}
			displayValue = this.GetItemDisplayValue(obj);
			return true;
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000992D0 File Offset: 0x000974D0
		private bool LookupValue(object formattedValue, out object value)
		{
			if (formattedValue == null)
			{
				value = null;
				return true;
			}
			object obj;
			if (this.DisplayMemberProperty != null || this.ValueMemberProperty != null)
			{
				obj = this.ItemFromComboBoxDataSource((this.DisplayMemberProperty != null) ? this.DisplayMemberProperty : this.ValueMemberProperty, formattedValue);
			}
			else
			{
				obj = this.ItemFromComboBoxItems(base.RowIndex, string.IsNullOrEmpty(this.DisplayMember) ? this.ValueMember : this.DisplayMember, formattedValue);
			}
			if (obj == null)
			{
				value = null;
				return false;
			}
			value = this.GetItemValue(obj);
			return true;
		}

		/// <summary>Called when the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell changes.</summary>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property is not <see langword="null" /> and the value of either the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DisplayMember" /> property or the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.ValueMember" /> property is not <see langword="null" /> or <see cref="F:System.String.Empty" /> and does not name a valid property or column in the data source.</exception>
		// Token: 0x0600200E RID: 8206 RVA: 0x00099352 File Offset: 0x00097552
		protected override void OnDataGridViewChanged()
		{
			if (base.DataGridView != null)
			{
				this.InitializeDisplayMemberPropertyDescriptor(this.DisplayMember);
				this.InitializeValueMemberPropertyDescriptor(this.ValueMember);
			}
			base.OnDataGridViewChanged();
		}

		/// <summary>Called when the focus moves to a cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="throughMouseClick">
		///   <see langword="true" /> if a user action moved focus to the cell; <see langword="false" /> if a programmatic operation moved focus to the cell.</param>
		// Token: 0x0600200F RID: 8207 RVA: 0x0009937A File Offset: 0x0009757A
		protected override void OnEnter(int rowIndex, bool throughMouseClick)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (throughMouseClick && base.DataGridView.EditMode != DataGridViewEditMode.EditOnEnter)
			{
				this.flags |= 1;
			}
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x000993A4 File Offset: 0x000975A4
		private void OnItemsCollectionChanged()
		{
			if (this.TemplateComboBoxColumn != null)
			{
				this.TemplateComboBoxColumn.OnItemsCollectionChanged();
			}
			DataGridViewComboBoxCell.cachedDropDownWidth = -1;
			if (this.OwnsEditingComboBox(base.RowIndex))
			{
				this.InitializeComboBoxText();
				return;
			}
			base.OnCommonChange();
		}

		/// <summary>Called when the focus moves from a cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="throughMouseClick">
		///   <see langword="true" /> if a user action moved focus from the cell; <see langword="false" /> if a programmatic operation moved focus from the cell.</param>
		// Token: 0x06002011 RID: 8209 RVA: 0x000993DA File Offset: 0x000975DA
		protected override void OnLeave(int rowIndex, bool throughMouseClick)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			this.flags = (byte)((int)this.flags & -2);
		}

		/// <summary>Called when the user clicks a mouse button while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06002012 RID: 8210 RVA: 0x000993F8 File Offset: 0x000975F8
		protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			if (currentCellAddress.X == e.ColumnIndex && currentCellAddress.Y == e.RowIndex)
			{
				if ((this.flags & 1) != 0)
				{
					this.flags = (byte)((int)this.flags & -2);
					return;
				}
				if ((this.EditingComboBox == null || !this.EditingComboBox.DroppedDown) && base.DataGridView.EditMode != DataGridViewEditMode.EditProgrammatically && base.DataGridView.BeginEdit(true) && this.EditingComboBox != null && this.DisplayStyle != DataGridViewComboBoxDisplayStyle.Nothing)
				{
					this.CheckDropDownList(e.X, e.Y, e.RowIndex);
				}
			}
		}

		/// <summary>Called when the mouse pointer moves over a cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x06002013 RID: 8211 RVA: 0x000994B0 File Offset: 0x000976B0
		protected override void OnMouseEnter(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (this.DisplayStyle == DataGridViewComboBoxDisplayStyle.ComboBox && this.FlatStyle == FlatStyle.Popup)
			{
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
			base.OnMouseEnter(rowIndex);
		}

		/// <summary>Called when the mouse pointer leaves the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x06002014 RID: 8212 RVA: 0x000994E8 File Offset: 0x000976E8
		protected override void OnMouseLeave(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (DataGridViewComboBoxCell.mouseInDropDownButtonBounds)
			{
				DataGridViewComboBoxCell.mouseInDropDownButtonBounds = false;
				if (base.ColumnIndex >= 0 && rowIndex >= 0 && (this.FlatStyle == FlatStyle.Standard || this.FlatStyle == FlatStyle.System) && base.DataGridView.ApplyVisualStylesToInnerCells)
				{
					base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
				}
			}
			if (this.DisplayStyle == DataGridViewComboBoxDisplayStyle.ComboBox && this.FlatStyle == FlatStyle.Popup)
			{
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
			base.OnMouseEnter(rowIndex);
		}

		/// <summary>Called when the mouse pointer moves within a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06002015 RID: 8213 RVA: 0x00099574 File Offset: 0x00097774
		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if ((this.FlatStyle == FlatStyle.Standard || this.FlatStyle == FlatStyle.System) && base.DataGridView.ApplyVisualStylesToInnerCells)
			{
				int rowIndex = e.RowIndex;
				DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
				bool flag = !base.DataGridView.RowHeadersVisible && base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single;
				bool flag2 = !base.DataGridView.ColumnHeadersVisible && base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single;
				bool flag3 = rowIndex == base.DataGridView.FirstDisplayedScrollingRowIndex;
				bool flag4 = base.OwningColumn.Index == base.DataGridView.FirstDisplayedColumnIndex;
				bool flag5 = base.OwningColumn.Index == base.DataGridView.FirstDisplayedScrollingColumnIndex;
				DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = new DataGridViewAdvancedBorderStyle();
				DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle2 = this.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStyle, flag, flag2, flag3, flag4);
				Rectangle cellDisplayRectangle = base.DataGridView.GetCellDisplayRectangle(base.OwningColumn.Index, rowIndex, false);
				if (flag5)
				{
					cellDisplayRectangle.X -= base.DataGridView.FirstDisplayedScrollingColumnHiddenWidth;
					cellDisplayRectangle.Width += base.DataGridView.FirstDisplayedScrollingColumnHiddenWidth;
				}
				DataGridViewElementStates rowState = base.DataGridView.Rows.GetRowState(rowIndex);
				DataGridViewElementStates dataGridViewElementStates = base.CellStateFromColumnRowStates(rowState);
				dataGridViewElementStates |= this.State;
				Rectangle rectangle;
				using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
				{
					this.PaintPrivate(graphics, cellDisplayRectangle, cellDisplayRectangle, rowIndex, dataGridViewElementStates, null, null, inheritedStyle, dataGridViewAdvancedBorderStyle2, out rectangle, DataGridViewPaintParts.ContentForeground, false, false, true, false);
				}
				bool flag6 = rectangle.Contains(base.DataGridView.PointToClient(Control.MousePosition));
				if (flag6 != DataGridViewComboBoxCell.mouseInDropDownButtonBounds)
				{
					DataGridViewComboBoxCell.mouseInDropDownButtonBounds = flag6;
					base.DataGridView.InvalidateCell(e.ColumnIndex, rowIndex);
				}
			}
			base.OnMouseMove(e);
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00099770 File Offset: 0x00097970
		private bool OwnsEditingComboBox(int rowIndex)
		{
			return rowIndex != -1 && this.EditingComboBox != null && rowIndex == ((IDataGridViewEditingControl)this.EditingComboBox).EditingControlRowIndex;
		}

		/// <summary>Paints the current <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the cell.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the cell that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="elementState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
		/// <param name="value">The data of the cell that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the cell that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
		// Token: 0x06002017 RID: 8215 RVA: 0x00099790 File Offset: 0x00097990
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			Rectangle rectangle;
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, elementState, formattedValue, errorText, cellStyle, advancedBorderStyle, out rectangle, paintParts, false, false, false, true);
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x000997CC File Offset: 0x000979CC
		private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, out Rectangle dropDownButtonRect, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool computeDropDownButtonRect, bool paint)
		{
			Rectangle rectangle = Rectangle.Empty;
			dropDownButtonRect = Rectangle.Empty;
			bool flag = this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup;
			bool flag2 = this.FlatStyle == FlatStyle.Popup && base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex;
			bool flag3 = !flag && base.DataGridView.ApplyVisualStylesToInnerCells;
			bool flag4 = flag3 && DataGridViewComboBoxCell.PostXPThemesExist;
			ComboBoxState comboBoxState = ComboBoxState.Normal;
			if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex && DataGridViewComboBoxCell.mouseInDropDownButtonBounds)
			{
				comboBoxState = ComboBoxState.Hot;
			}
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
			Rectangle rectangle3 = cellBounds;
			rectangle3.Offset(rectangle2.X, rectangle2.Y);
			rectangle3.Width -= rectangle2.Right;
			rectangle3.Height -= rectangle2.Bottom;
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			bool flag5 = currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex;
			bool flag6 = flag5 && base.DataGridView.EditingControl != null;
			bool flag7 = (elementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			bool flag8 = this.DisplayStyle == DataGridViewComboBoxDisplayStyle.ComboBox && ((this.DisplayStyleForCurrentCellOnly && flag5) || !this.DisplayStyleForCurrentCellOnly);
			bool flag9 = this.DisplayStyle != DataGridViewComboBoxDisplayStyle.Nothing && ((this.DisplayStyleForCurrentCellOnly && flag5) || !this.DisplayStyleForCurrentCellOnly);
			SolidBrush solidBrush;
			if (DataGridViewCell.PaintSelectionBackground(paintParts) && flag7 && !flag6)
			{
				solidBrush = base.DataGridView.GetCachedBrush(cellStyle.SelectionBackColor);
			}
			else
			{
				solidBrush = base.DataGridView.GetCachedBrush(cellStyle.BackColor);
			}
			if (paint && DataGridViewCell.PaintBackground(paintParts) && solidBrush.Color.A == 255 && rectangle3.Width > 0 && rectangle3.Height > 0)
			{
				DataGridViewCell.PaintPadding(g, rectangle3, cellStyle, solidBrush, base.DataGridView.RightToLeftInternal);
			}
			if (cellStyle.Padding != Padding.Empty)
			{
				if (base.DataGridView.RightToLeftInternal)
				{
					rectangle3.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
				}
				else
				{
					rectangle3.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
				}
				rectangle3.Width -= cellStyle.Padding.Horizontal;
				rectangle3.Height -= cellStyle.Padding.Vertical;
			}
			if (paint && rectangle3.Width > 0 && rectangle3.Height > 0)
			{
				if (flag3 && flag8)
				{
					if (flag4 && DataGridViewCell.PaintBackground(paintParts) && solidBrush.Color.A == 255)
					{
						g.FillRectangle(solidBrush, rectangle3.Left, rectangle3.Top, rectangle3.Width, rectangle3.Height);
					}
					if (DataGridViewCell.PaintContentBackground(paintParts))
					{
						if (flag4)
						{
							DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawBorder(g, rectangle3);
						}
						else
						{
							DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawTextBox(g, rectangle3, comboBoxState);
						}
					}
					if (!flag4 && DataGridViewCell.PaintBackground(paintParts) && solidBrush.Color.A == 255 && rectangle3.Width > 2 && rectangle3.Height > 2)
					{
						g.FillRectangle(solidBrush, rectangle3.Left + 1, rectangle3.Top + 1, rectangle3.Width - 2, rectangle3.Height - 2);
					}
				}
				else if (DataGridViewCell.PaintBackground(paintParts) && solidBrush.Color.A == 255)
				{
					if (flag4 && flag9 && !flag8)
					{
						g.DrawRectangle(SystemPens.ControlLightLight, new Rectangle(rectangle3.X, rectangle3.Y, rectangle3.Width - 1, rectangle3.Height - 1));
					}
					else
					{
						g.FillRectangle(solidBrush, rectangle3.Left, rectangle3.Top, rectangle3.Width, rectangle3.Height);
					}
				}
			}
			int num = Math.Min(SystemInformation.HorizontalScrollBarThumbWidth, rectangle3.Width - 6 - 1);
			if (!flag6)
			{
				int num2;
				if (flag3 || flag)
				{
					num2 = Math.Min(this.GetDropDownButtonHeight(g, cellStyle), flag4 ? rectangle3.Height : (rectangle3.Height - 2));
				}
				else
				{
					num2 = Math.Min(this.GetDropDownButtonHeight(g, cellStyle), rectangle3.Height - 4);
				}
				if (num > 0 && num2 > 0)
				{
					Rectangle rectangle4;
					if (flag3 || flag)
					{
						if (flag4)
						{
							rectangle4 = new Rectangle(base.DataGridView.RightToLeftInternal ? rectangle3.Left : (rectangle3.Right - num), rectangle3.Top, num, num2);
						}
						else
						{
							rectangle4 = new Rectangle(base.DataGridView.RightToLeftInternal ? (rectangle3.Left + 1) : (rectangle3.Right - num - 1), rectangle3.Top + 1, num, num2);
						}
					}
					else
					{
						rectangle4 = new Rectangle(base.DataGridView.RightToLeftInternal ? (rectangle3.Left + 2) : (rectangle3.Right - num - 2), rectangle3.Top + 2, num, num2);
					}
					if (flag4 && flag9 && !flag8)
					{
						dropDownButtonRect = rectangle3;
					}
					else
					{
						dropDownButtonRect = rectangle4;
					}
					if (paint && DataGridViewCell.PaintContentBackground(paintParts))
					{
						if (flag9)
						{
							if (flag)
							{
								g.FillRectangle(SystemBrushes.Control, rectangle4);
							}
							else if (flag3)
							{
								if (flag4)
								{
									if (flag8)
									{
										DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawDropDownButton(g, rectangle4, comboBoxState, base.DataGridView.RightToLeftInternal);
									}
									else
									{
										DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawReadOnlyButton(g, rectangle3, comboBoxState);
										DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawDropDownButton(g, rectangle4, ComboBoxState.Normal);
									}
									if (SystemInformation.HighContrast && AccessibilityImprovements.Level1)
									{
										solidBrush = base.DataGridView.GetCachedBrush(cellStyle.BackColor);
									}
								}
								else
								{
									DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawDropDownButton(g, rectangle4, comboBoxState);
								}
							}
							else
							{
								g.FillRectangle(SystemBrushes.Control, rectangle4);
							}
						}
						if (!flag && !flag3 && (flag8 || flag9))
						{
							Color control = SystemColors.Control;
							Color color = control;
							bool flag10 = control.ToKnownColor() == SystemColors.Control.ToKnownColor();
							bool highContrast = SystemInformation.HighContrast;
							Color color2;
							Color color3;
							Color color4;
							if (control == SystemColors.Control)
							{
								color2 = SystemColors.ControlDark;
								color3 = SystemColors.ControlDarkDark;
								color4 = SystemColors.ControlLightLight;
							}
							else
							{
								color2 = ControlPaint.Dark(control);
								color4 = ControlPaint.LightLight(control);
								if (highContrast)
								{
									color3 = ControlPaint.LightLight(control);
								}
								else
								{
									color3 = ControlPaint.DarkDark(control);
								}
							}
							color2 = g.GetNearestColor(color2);
							color3 = g.GetNearestColor(color3);
							color = g.GetNearestColor(color);
							color4 = g.GetNearestColor(color4);
							Pen pen;
							if (flag10)
							{
								if (SystemInformation.HighContrast)
								{
									pen = SystemPens.ControlLight;
								}
								else
								{
									pen = SystemPens.Control;
								}
							}
							else
							{
								pen = new Pen(color4);
							}
							if (flag9)
							{
								g.DrawLine(pen, rectangle4.X, rectangle4.Y, rectangle4.X + rectangle4.Width - 1, rectangle4.Y);
								g.DrawLine(pen, rectangle4.X, rectangle4.Y, rectangle4.X, rectangle4.Y + rectangle4.Height - 1);
							}
							if (flag8)
							{
								g.DrawLine(pen, rectangle3.X, rectangle3.Y + rectangle3.Height - 1, rectangle3.X + rectangle3.Width - 1, rectangle3.Y + rectangle3.Height - 1);
								g.DrawLine(pen, rectangle3.X + rectangle3.Width - 1, rectangle3.Y, rectangle3.X + rectangle3.Width - 1, rectangle3.Y + rectangle3.Height - 1);
							}
							if (flag10)
							{
								pen = SystemPens.ControlDarkDark;
							}
							else
							{
								pen.Color = color3;
							}
							if (flag9)
							{
								g.DrawLine(pen, rectangle4.X, rectangle4.Y + rectangle4.Height - 1, rectangle4.X + rectangle4.Width - 1, rectangle4.Y + rectangle4.Height - 1);
								g.DrawLine(pen, rectangle4.X + rectangle4.Width - 1, rectangle4.Y, rectangle4.X + rectangle4.Width - 1, rectangle4.Y + rectangle4.Height - 1);
							}
							if (flag8)
							{
								g.DrawLine(pen, rectangle3.X, rectangle3.Y, rectangle3.X + rectangle3.Width - 2, rectangle3.Y);
								g.DrawLine(pen, rectangle3.X, rectangle3.Y, rectangle3.X, rectangle3.Y + rectangle3.Height - 1);
							}
							if (flag10)
							{
								pen = SystemPens.ControlLightLight;
							}
							else
							{
								pen.Color = color;
							}
							if (flag9)
							{
								g.DrawLine(pen, rectangle4.X + 1, rectangle4.Y + 1, rectangle4.X + rectangle4.Width - 2, rectangle4.Y + 1);
								g.DrawLine(pen, rectangle4.X + 1, rectangle4.Y + 1, rectangle4.X + 1, rectangle4.Y + rectangle4.Height - 2);
							}
							if (flag10)
							{
								pen = SystemPens.ControlDark;
							}
							else
							{
								pen.Color = color2;
							}
							if (flag9)
							{
								g.DrawLine(pen, rectangle4.X + 1, rectangle4.Y + rectangle4.Height - 2, rectangle4.X + rectangle4.Width - 2, rectangle4.Y + rectangle4.Height - 2);
								g.DrawLine(pen, rectangle4.X + rectangle4.Width - 2, rectangle4.Y + 1, rectangle4.X + rectangle4.Width - 2, rectangle4.Y + rectangle4.Height - 2);
							}
							if (!flag10)
							{
								pen.Dispose();
							}
						}
						if (num >= 5 && num2 >= 3 && flag9)
						{
							if (flag)
							{
								Point point = new Point(rectangle4.Left + rectangle4.Width / 2, rectangle4.Top + rectangle4.Height / 2);
								point.X += rectangle4.Width % 2;
								point.Y += rectangle4.Height % 2;
								g.FillPolygon(SystemBrushes.ControlText, new Point[]
								{
									new Point(point.X - DataGridViewComboBoxCell.offset2X, point.Y - 1),
									new Point(point.X + DataGridViewComboBoxCell.offset2X + 1, point.Y - 1),
									new Point(point.X, point.Y + DataGridViewComboBoxCell.offset2Y)
								});
							}
							else if (!flag3)
							{
								int num3 = rectangle4.X;
								rectangle4.X = num3 - 1;
								num3 = rectangle4.Width;
								rectangle4.Width = num3 + 1;
								Point point2 = new Point(rectangle4.Left + (rectangle4.Width - 1) / 2, rectangle4.Top + (rectangle4.Height + (int)DataGridViewComboBoxCell.nonXPTriangleHeight) / 2);
								point2.X += (rectangle4.Width + 1) % 2;
								point2.Y += rectangle4.Height % 2;
								Point point3 = new Point(point2.X - (int)((DataGridViewComboBoxCell.nonXPTriangleWidth - 1) / 2), point2.Y - (int)DataGridViewComboBoxCell.nonXPTriangleHeight);
								Point point4 = new Point(point2.X + (int)((DataGridViewComboBoxCell.nonXPTriangleWidth - 1) / 2), point2.Y - (int)DataGridViewComboBoxCell.nonXPTriangleHeight);
								g.FillPolygon(SystemBrushes.ControlText, new Point[] { point3, point4, point2 });
								g.DrawLine(SystemPens.ControlText, point3.X, point3.Y, point4.X, point4.Y);
								num3 = rectangle4.X;
								rectangle4.X = num3 + 1;
								num3 = rectangle4.Width;
								rectangle4.Width = num3 - 1;
							}
						}
						if (flag2 && flag8)
						{
							int num3 = rectangle4.Y;
							rectangle4.Y = num3 - 1;
							num3 = rectangle4.Height;
							rectangle4.Height = num3 + 1;
							g.DrawRectangle(SystemPens.ControlDark, rectangle4);
						}
					}
				}
			}
			Rectangle rectangle5 = rectangle3;
			Rectangle rectangle6 = Rectangle.Inflate(rectangle3, -2, -2);
			if (flag4)
			{
				int num3;
				if (!base.DataGridView.RightToLeftInternal)
				{
					num3 = rectangle6.X;
					rectangle6.X = num3 - 1;
				}
				num3 = rectangle6.Width;
				rectangle6.Width = num3 + 1;
			}
			if (flag9)
			{
				if (flag3 || flag)
				{
					rectangle5.Width -= num;
					rectangle6.Width -= num;
					if (base.DataGridView.RightToLeftInternal)
					{
						rectangle5.X += num;
						rectangle6.X += num;
					}
				}
				else
				{
					rectangle5.Width -= num + 1;
					rectangle6.Width -= num + 1;
					if (base.DataGridView.RightToLeftInternal)
					{
						rectangle5.X += num + 1;
						rectangle6.X += num + 1;
					}
				}
			}
			if (rectangle6.Width > 1 && rectangle6.Height > 1)
			{
				if (flag5 && !flag6 && DataGridViewCell.PaintFocus(paintParts) && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && paint)
				{
					if (flag)
					{
						Rectangle rectangle7 = rectangle6;
						int num3;
						if (!base.DataGridView.RightToLeftInternal)
						{
							num3 = rectangle7.X;
							rectangle7.X = num3 - 1;
						}
						num3 = rectangle7.Width;
						rectangle7.Width = num3 + 1;
						num3 = rectangle7.Y;
						rectangle7.Y = num3 - 1;
						rectangle7.Height += 2;
						ControlPaint.DrawFocusRectangle(g, rectangle7, Color.Empty, solidBrush.Color);
					}
					else if (flag4)
					{
						Rectangle rectangle8 = rectangle6;
						int num3 = rectangle8.X;
						rectangle8.X = num3 + 1;
						rectangle8.Width -= 2;
						num3 = rectangle8.Y;
						rectangle8.Y = num3 + 1;
						rectangle8.Height -= 2;
						if (rectangle8.Width > 0 && rectangle8.Height > 0)
						{
							ControlPaint.DrawFocusRectangle(g, rectangle8, Color.Empty, solidBrush.Color);
						}
					}
					else
					{
						ControlPaint.DrawFocusRectangle(g, rectangle6, Color.Empty, solidBrush.Color);
					}
				}
				if (flag2)
				{
					int num3 = rectangle3.Width;
					rectangle3.Width = num3 - 1;
					num3 = rectangle3.Height;
					rectangle3.Height = num3 - 1;
					if (!flag6 && paint && DataGridViewCell.PaintContentBackground(paintParts) && flag8)
					{
						g.DrawRectangle(SystemPens.ControlDark, rectangle3);
					}
				}
				string text = formattedValue as string;
				if (text != null)
				{
					int num4 = ((cellStyle.WrapMode == DataGridViewTriState.True) ? 0 : 1);
					if (base.DataGridView.RightToLeftInternal)
					{
						rectangle6.Offset(0, num4);
						rectangle6.Width += 2;
					}
					else
					{
						rectangle6.Offset(-1, num4);
						rectangle6.Width++;
					}
					rectangle6.Height -= num4;
					if (rectangle6.Width > 0 && rectangle6.Height > 0)
					{
						TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
						if (!flag6 && paint)
						{
							if (DataGridViewCell.PaintContentForeground(paintParts))
							{
								if ((textFormatFlags & TextFormatFlags.SingleLine) != TextFormatFlags.Default)
								{
									textFormatFlags |= TextFormatFlags.EndEllipsis;
								}
								Color color5;
								if (flag4 && (flag9 || flag8))
								{
									color5 = DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
								}
								else
								{
									color5 = (flag7 ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
								}
								TextRenderer.DrawText(g, text, cellStyle.Font, rectangle6, color5, textFormatFlags);
							}
						}
						else if (computeContentBounds)
						{
							rectangle = DataGridViewUtilities.GetTextBounds(rectangle6, text, textFormatFlags, cellStyle);
						}
					}
				}
				if (base.DataGridView.ShowCellErrors && paint && DataGridViewCell.PaintErrorIcon(paintParts))
				{
					base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, rectangle5, errorText);
					if (flag6)
					{
						return Rectangle.Empty;
					}
				}
			}
			if (computeErrorIconBounds)
			{
				if (!string.IsNullOrEmpty(errorText))
				{
					rectangle = base.ComputeErrorIconBounds(rectangle5);
				}
				else
				{
					rectangle = Rectangle.Empty;
				}
			}
			return rectangle;
		}

		/// <summary>Converts a value formatted for display to an actual cell value.</summary>
		/// <param name="formattedValue">The display value of the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the display value type, or null to use the default converter.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the cell value type, or null to use the default converter.</param>
		/// <returns>The cell value.</returns>
		// Token: 0x06002019 RID: 8217 RVA: 0x0009A8CC File Offset: 0x00098ACC
		public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			if (valueTypeConverter == null)
			{
				if (this.ValueMemberProperty != null)
				{
					valueTypeConverter = this.ValueMemberProperty.Converter;
				}
				else if (this.DisplayMemberProperty != null)
				{
					valueTypeConverter = this.DisplayMemberProperty.Converter;
				}
			}
			if ((this.DataManager != null && (this.DisplayMemberProperty != null || this.ValueMemberProperty != null)) || !string.IsNullOrEmpty(this.DisplayMember) || !string.IsNullOrEmpty(this.ValueMember))
			{
				object obj = base.ParseFormattedValueInternal(this.DisplayType, formattedValue, cellStyle, formattedValueTypeConverter, this.DisplayTypeConverter);
				object obj2 = obj;
				if (!this.LookupValue(obj2, out obj))
				{
					if (obj2 != DBNull.Value)
					{
						throw new FormatException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Formatter_CantConvert"), new object[] { obj, this.DisplayType }));
					}
					obj = DBNull.Value;
				}
				return obj;
			}
			return base.ParseFormattedValueInternal(this.ValueType, formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
		}

		/// <summary>Returns a string that describes the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x0600201A RID: 8218 RVA: 0x0009A9B0 File Offset: 0x00098BB0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewComboBoxCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0009AA0C File Offset: 0x00098C0C
		private void UnwireDataSource()
		{
			IComponent component = this.DataSource as IComponent;
			if (component != null)
			{
				component.Disposed -= this.DataSource_Disposed;
			}
			ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null && (this.flags & 16) != 0)
			{
				supportInitializeNotification.Initialized -= this.DataSource_Initialized;
				this.flags = (byte)((int)this.flags & -17);
			}
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0009AA78 File Offset: 0x00098C78
		private void WireDataSource(object dataSource)
		{
			IComponent component = dataSource as IComponent;
			if (component != null)
			{
				component.Disposed += this.DataSource_Disposed;
			}
		}

		// Token: 0x04000D5A RID: 3418
		private static readonly int PropComboBoxCellDataSource = PropertyStore.CreateKey();

		// Token: 0x04000D5B RID: 3419
		private static readonly int PropComboBoxCellDisplayMember = PropertyStore.CreateKey();

		// Token: 0x04000D5C RID: 3420
		private static readonly int PropComboBoxCellValueMember = PropertyStore.CreateKey();

		// Token: 0x04000D5D RID: 3421
		private static readonly int PropComboBoxCellItems = PropertyStore.CreateKey();

		// Token: 0x04000D5E RID: 3422
		private static readonly int PropComboBoxCellDropDownWidth = PropertyStore.CreateKey();

		// Token: 0x04000D5F RID: 3423
		private static readonly int PropComboBoxCellMaxDropDownItems = PropertyStore.CreateKey();

		// Token: 0x04000D60 RID: 3424
		private static readonly int PropComboBoxCellEditingComboBox = PropertyStore.CreateKey();

		// Token: 0x04000D61 RID: 3425
		private static readonly int PropComboBoxCellValueMemberProp = PropertyStore.CreateKey();

		// Token: 0x04000D62 RID: 3426
		private static readonly int PropComboBoxCellDisplayMemberProp = PropertyStore.CreateKey();

		// Token: 0x04000D63 RID: 3427
		private static readonly int PropComboBoxCellDataManager = PropertyStore.CreateKey();

		// Token: 0x04000D64 RID: 3428
		private static readonly int PropComboBoxCellColumnTemplate = PropertyStore.CreateKey();

		// Token: 0x04000D65 RID: 3429
		private static readonly int PropComboBoxCellFlatStyle = PropertyStore.CreateKey();

		// Token: 0x04000D66 RID: 3430
		private static readonly int PropComboBoxCellDisplayStyle = PropertyStore.CreateKey();

		// Token: 0x04000D67 RID: 3431
		private static readonly int PropComboBoxCellDisplayStyleForCurrentCellOnly = PropertyStore.CreateKey();

		// Token: 0x04000D68 RID: 3432
		private const byte DATAGRIDVIEWCOMBOBOXCELL_margin = 3;

		// Token: 0x04000D69 RID: 3433
		private const byte DATAGRIDVIEWCOMBOBOXCELL_nonXPTriangleHeight = 4;

		// Token: 0x04000D6A RID: 3434
		private const byte DATAGRIDVIEWCOMBOBOXCELL_nonXPTriangleWidth = 7;

		// Token: 0x04000D6B RID: 3435
		private const byte DATAGRIDVIEWCOMBOBOXCELL_horizontalTextMarginLeft = 0;

		// Token: 0x04000D6C RID: 3436
		private const byte DATAGRIDVIEWCOMBOBOXCELL_verticalTextMarginTopWithWrapping = 0;

		// Token: 0x04000D6D RID: 3437
		private const byte DATAGRIDVIEWCOMBOBOXCELL_verticalTextMarginTopWithoutWrapping = 1;

		// Token: 0x04000D6E RID: 3438
		private const byte DATAGRIDVIEWCOMBOBOXCELL_ignoreNextMouseClick = 1;

		// Token: 0x04000D6F RID: 3439
		private const byte DATAGRIDVIEWCOMBOBOXCELL_sorted = 2;

		// Token: 0x04000D70 RID: 3440
		private const byte DATAGRIDVIEWCOMBOBOXCELL_createItemsFromDataSource = 4;

		// Token: 0x04000D71 RID: 3441
		private const byte DATAGRIDVIEWCOMBOBOXCELL_autoComplete = 8;

		// Token: 0x04000D72 RID: 3442
		private const byte DATAGRIDVIEWCOMBOBOXCELL_dataSourceInitializedHookedUp = 16;

		// Token: 0x04000D73 RID: 3443
		private const byte DATAGRIDVIEWCOMBOBOXCELL_dropDownHookedUp = 32;

		// Token: 0x04000D74 RID: 3444
		internal const int DATAGRIDVIEWCOMBOBOXCELL_defaultMaxDropDownItems = 8;

		// Token: 0x04000D75 RID: 3445
		private static Type defaultFormattedValueType = typeof(string);

		// Token: 0x04000D76 RID: 3446
		private static Type defaultEditType = typeof(DataGridViewComboBoxEditingControl);

		// Token: 0x04000D77 RID: 3447
		private static Type defaultValueType = typeof(object);

		// Token: 0x04000D78 RID: 3448
		private static Type cellType = typeof(DataGridViewComboBoxCell);

		// Token: 0x04000D79 RID: 3449
		private byte flags;

		// Token: 0x04000D7A RID: 3450
		private static bool mouseInDropDownButtonBounds = false;

		// Token: 0x04000D7B RID: 3451
		private static int cachedDropDownWidth = -1;

		// Token: 0x04000D7C RID: 3452
		private static bool isScalingInitialized = false;

		// Token: 0x04000D7D RID: 3453
		private static int OFFSET_2PIXELS = 2;

		// Token: 0x04000D7E RID: 3454
		private static int offset2X = DataGridViewComboBoxCell.OFFSET_2PIXELS;

		// Token: 0x04000D7F RID: 3455
		private static int offset2Y = DataGridViewComboBoxCell.OFFSET_2PIXELS;

		// Token: 0x04000D80 RID: 3456
		private static byte nonXPTriangleHeight = 4;

		// Token: 0x04000D81 RID: 3457
		private static byte nonXPTriangleWidth = 7;

		/// <summary>Represents the collection of selection choices in a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
		// Token: 0x0200066C RID: 1644
		[ListBindable(false)]
		public class ObjectCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> that owns the collection.</param>
			// Token: 0x0600662F RID: 26159 RVA: 0x0017D9FF File Offset: 0x0017BBFF
			public ObjectCollection(DataGridViewComboBoxCell owner)
			{
				this.owner = owner;
			}

			// Token: 0x1700163A RID: 5690
			// (get) Token: 0x06006630 RID: 26160 RVA: 0x0017DA0E File Offset: 0x0017BC0E
			private IComparer Comparer
			{
				get
				{
					if (this.comparer == null)
					{
						this.comparer = new DataGridViewComboBoxCell.ItemComparer(this.owner);
					}
					return this.comparer;
				}
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x1700163B RID: 5691
			// (get) Token: 0x06006631 RID: 26161 RVA: 0x0017DA2F File Offset: 0x0017BC2F
			public int Count
			{
				get
				{
					return this.InnerArray.Count;
				}
			}

			// Token: 0x1700163C RID: 5692
			// (get) Token: 0x06006632 RID: 26162 RVA: 0x0017DA3C File Offset: 0x0017BC3C
			internal ArrayList InnerArray
			{
				get
				{
					if (this.items == null)
					{
						this.items = new ArrayList();
					}
					return this.items;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" />.</returns>
			// Token: 0x1700163D RID: 5693
			// (get) Token: 0x06006633 RID: 26163 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x1700163E RID: 5694
			// (get) Token: 0x06006634 RID: 26164 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x1700163F RID: 5695
			// (get) Token: 0x06006635 RID: 26165 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x17001640 RID: 5696
			// (get) Token: 0x06006636 RID: 26166 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
			/// <param name="item">An object representing the item to add to the collection.</param>
			/// <returns>The position into which the new element was inserted.</returns>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not <see langword="null" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x06006637 RID: 26167 RVA: 0x0017DA58 File Offset: 0x0017BC58
			public int Add(object item)
			{
				this.owner.CheckNoDataSource();
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				int num = this.InnerArray.Add(item);
				bool flag = false;
				if (this.owner.Sorted)
				{
					try
					{
						this.InnerArray.Sort(this.Comparer);
						num = this.InnerArray.IndexOf(item);
						flag = true;
					}
					finally
					{
						if (!flag)
						{
							this.InnerArray.Remove(item);
						}
					}
				}
				this.owner.OnItemsCollectionChanged();
				return num;
			}

			/// <summary>Adds an object to the collection.</summary>
			/// <param name="item">The object to add to the collection.</param>
			/// <returns>The position in which to insert the new element.</returns>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not <see langword="null" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x06006638 RID: 26168 RVA: 0x0017DAE8 File Offset: 0x0017BCE8
			int IList.Add(object item)
			{
				return this.Add(item);
			}

			/// <summary>Adds one or more items to the list of items for a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
			/// <param name="items">One or more objects that represent items for the drop-down list.  
			///  -or-  
			///  An <see cref="T:System.Array" /> of <see cref="T:System.Object" /> values.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="items" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">One or more of the items in the <paramref name="items" /> array is <see langword="null" />.
			/// -or-
			/// The cell is in a shared row.</exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not <see langword="null" />.</exception>
			// Token: 0x06006639 RID: 26169 RVA: 0x0017DAF1 File Offset: 0x0017BCF1
			public void AddRange(params object[] items)
			{
				this.owner.CheckNoDataSource();
				this.AddRangeInternal(items);
				this.owner.OnItemsCollectionChanged();
			}

			/// <summary>Adds the items of an existing <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> to the list of items in a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> to load into this collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">One or more of the items in the <paramref name="value" /> collection is <see langword="null" />.
			/// -or-
			/// The cell is in a shared row.</exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not <see langword="null" />.</exception>
			// Token: 0x0600663A RID: 26170 RVA: 0x0017DAF1 File Offset: 0x0017BCF1
			public void AddRange(DataGridViewComboBoxCell.ObjectCollection value)
			{
				this.owner.CheckNoDataSource();
				this.AddRangeInternal(value);
				this.owner.OnItemsCollectionChanged();
			}

			// Token: 0x0600663B RID: 26171 RVA: 0x0017DB10 File Offset: 0x0017BD10
			internal void AddRangeInternal(ICollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				using (IEnumerator enumerator = items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							throw new InvalidOperationException(SR.GetString("InvalidNullItemInCollection"));
						}
					}
				}
				this.InnerArray.AddRange(items);
				if (this.owner.Sorted)
				{
					this.InnerArray.Sort(this.Comparer);
				}
			}

			// Token: 0x0600663C RID: 26172 RVA: 0x0017DBA4 File Offset: 0x0017BDA4
			internal void SortInternal()
			{
				this.InnerArray.Sort(this.Comparer);
			}

			/// <summary>Gets or sets the item at the current index location. In C#, this property is the indexer for the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> class.</summary>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <returns>The <see cref="T:System.Object" /> stored at the given index.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than the number of items in the collection minus one.</exception>
			/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentException">When setting this property, the cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not <see langword="null" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">When setting this property, the cell is in a shared row.</exception>
			// Token: 0x17001641 RID: 5697
			public virtual object this[int index]
			{
				get
				{
					if (index < 0 || index >= this.InnerArray.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.InnerArray[index];
				}
				set
				{
					this.owner.CheckNoDataSource();
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					if (index < 0 || index >= this.InnerArray.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.InnerArray[index] = value;
					this.owner.OnItemsCollectionChanged();
				}
			}

			/// <summary>Clears all items from the collection.</summary>
			/// <exception cref="T:System.ArgumentException">The collection contains at least one item and the cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not <see langword="null" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The collection contains at least one item and the cell is in a shared row.</exception>
			// Token: 0x0600663F RID: 26175 RVA: 0x0017DC9A File Offset: 0x0017BE9A
			public void Clear()
			{
				if (this.InnerArray.Count > 0)
				{
					this.owner.CheckNoDataSource();
					this.InnerArray.Clear();
					this.owner.OnItemsCollectionChanged();
				}
			}

			// Token: 0x06006640 RID: 26176 RVA: 0x0017DCCB File Offset: 0x0017BECB
			internal void ClearInternal()
			{
				this.InnerArray.Clear();
			}

			/// <summary>Determines whether the specified item is contained in the collection.</summary>
			/// <param name="value">An object representing the item to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the <paramref name="item" /> is in the collection; otherwise, <see langword="false" />.</returns>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is <see langword="null" />.</exception>
			// Token: 0x06006641 RID: 26177 RVA: 0x0017DCD8 File Offset: 0x0017BED8
			public bool Contains(object value)
			{
				return this.IndexOf(value) != -1;
			}

			/// <summary>Copies the entire collection into an existing array of objects at a specified location within the array.</summary>
			/// <param name="destination">The destination array to which the contents will be copied.</param>
			/// <param name="arrayIndex">The index of the element in <paramref name="destination" /> at which to start copying.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="destination" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="arrayIndex" /> is less than 0 or equal to or greater than the length of <paramref name="destination" />.  
			/// -or-  
			/// The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of <paramref name="destination" />.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="destination" /> is multidimensional.</exception>
			// Token: 0x06006642 RID: 26178 RVA: 0x0017DCE8 File Offset: 0x0017BEE8
			public void CopyTo(object[] destination, int arrayIndex)
			{
				int count = this.InnerArray.Count;
				for (int i = 0; i < count; i++)
				{
					destination[i + arrayIndex] = this.InnerArray[i];
				}
			}

			/// <summary>Copies the elements of the collection to the specified array, starting at the specified index.</summary>
			/// <param name="destination">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="destination" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or equal to or greater than the length of <paramref name="destination" />.  
			/// -or-  
			/// The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="destination" />.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="destination" /> is multidimensional.</exception>
			// Token: 0x06006643 RID: 26179 RVA: 0x0017DD20 File Offset: 0x0017BF20
			void ICollection.CopyTo(Array destination, int index)
			{
				int count = this.InnerArray.Count;
				for (int i = 0; i < count; i++)
				{
					destination.SetValue(this.InnerArray[i], i + index);
				}
			}

			/// <summary>Returns an enumerator that can iterate through a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" />.</summary>
			/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
			// Token: 0x06006644 RID: 26180 RVA: 0x0017DD5A File Offset: 0x0017BF5A
			public IEnumerator GetEnumerator()
			{
				return this.InnerArray.GetEnumerator();
			}

			/// <summary>Returns the index of the specified item in the collection.</summary>
			/// <param name="value">An object representing the item to locate in the collection.</param>
			/// <returns>The zero-based index of the <paramref name="value" /> parameter if it is found in the collection; otherwise, -1.</returns>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is <see langword="null" />.</exception>
			// Token: 0x06006645 RID: 26181 RVA: 0x0017DD67 File Offset: 0x0017BF67
			public int IndexOf(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.InnerArray.IndexOf(value);
			}

			/// <summary>Inserts an item into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index at which to place <paramref name="item" /> within an unsorted <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</param>
			/// <param name="item">An object representing the item to insert.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than the number of items in the collection.</exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not <see langword="null" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x06006646 RID: 26182 RVA: 0x0017DD84 File Offset: 0x0017BF84
			public void Insert(int index, object item)
			{
				this.owner.CheckNoDataSource();
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				if (index < 0 || index > this.InnerArray.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.Sorted)
				{
					this.Add(item);
					return;
				}
				this.InnerArray.Insert(index, item);
				this.owner.OnItemsCollectionChanged();
			}

			/// <summary>Removes the specified object from the collection.</summary>
			/// <param name="value">An object representing the item to remove from the collection.</param>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not <see langword="null" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x06006647 RID: 26183 RVA: 0x0017DE1C File Offset: 0x0017C01C
			public void Remove(object value)
			{
				int num = this.InnerArray.IndexOf(value);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the object at the specified index.</summary>
			/// <param name="index">The zero-based index of the object to be removed.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than the number of items in the collection minus one.</exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not <see langword="null" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x06006648 RID: 26184 RVA: 0x0017DE44 File Offset: 0x0017C044
			public void RemoveAt(int index)
			{
				this.owner.CheckNoDataSource();
				if (index < 0 || index >= this.InnerArray.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.InnerArray.RemoveAt(index);
				this.owner.OnItemsCollectionChanged();
			}

			// Token: 0x04003A5E RID: 14942
			private DataGridViewComboBoxCell owner;

			// Token: 0x04003A5F RID: 14943
			private ArrayList items;

			// Token: 0x04003A60 RID: 14944
			private IComparer comparer;
		}

		// Token: 0x0200066D RID: 1645
		private sealed class ItemComparer : IComparer
		{
			// Token: 0x06006649 RID: 26185 RVA: 0x0017DEB7 File Offset: 0x0017C0B7
			public ItemComparer(DataGridViewComboBoxCell dataGridViewComboBoxCell)
			{
				this.dataGridViewComboBoxCell = dataGridViewComboBoxCell;
			}

			// Token: 0x0600664A RID: 26186 RVA: 0x0017DEC8 File Offset: 0x0017C0C8
			public int Compare(object item1, object item2)
			{
				if (item1 == null)
				{
					if (item2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (item2 == null)
					{
						return 1;
					}
					string itemDisplayText = this.dataGridViewComboBoxCell.GetItemDisplayText(item1);
					string itemDisplayText2 = this.dataGridViewComboBoxCell.GetItemDisplayText(item2);
					CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
					return compareInfo.Compare(itemDisplayText, itemDisplayText2, CompareOptions.StringSort);
				}
			}

			// Token: 0x04003A61 RID: 14945
			private DataGridViewComboBoxCell dataGridViewComboBoxCell;
		}

		// Token: 0x0200066E RID: 1646
		private class DataGridViewComboBoxCellRenderer
		{
			// Token: 0x0600664B RID: 26187 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewComboBoxCellRenderer()
			{
			}

			// Token: 0x17001642 RID: 5698
			// (get) Token: 0x0600664C RID: 26188 RVA: 0x0017DF16 File Offset: 0x0017C116
			public static VisualStyleRenderer VisualStyleRenderer
			{
				get
				{
					if (DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxReadOnlyButton);
					}
					return DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x0600664D RID: 26189 RVA: 0x0017DF33 File Offset: 0x0017C133
			public static void DrawTextBox(Graphics g, Rectangle bounds, ComboBoxState state)
			{
				ComboBoxRenderer.DrawTextBox(g, bounds, state);
			}

			// Token: 0x0600664E RID: 26190 RVA: 0x0017DF3D File Offset: 0x0017C13D
			public static void DrawDropDownButton(Graphics g, Rectangle bounds, ComboBoxState state)
			{
				ComboBoxRenderer.DrawDropDownButton(g, bounds, state);
			}

			// Token: 0x0600664F RID: 26191 RVA: 0x0017DF48 File Offset: 0x0017C148
			public static void DrawBorder(Graphics g, Rectangle bounds)
			{
				if (DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer == null)
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxBorder);
				}
				else
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.SetParameters(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxBorder.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxBorder.Part, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxBorder.State);
				}
				DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			}

			// Token: 0x06006650 RID: 26192 RVA: 0x0017DFA4 File Offset: 0x0017C1A4
			public static void DrawDropDownButton(Graphics g, Rectangle bounds, ComboBoxState state, bool rightToLeft)
			{
				if (rightToLeft)
				{
					if (DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonLeft.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonLeft.Part, (int)state);
					}
					else
					{
						DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.SetParameters(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonLeft.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonLeft.Part, (int)state);
					}
				}
				else if (DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer == null)
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonRight.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonRight.Part, (int)state);
				}
				else
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.SetParameters(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonRight.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonRight.Part, (int)state);
				}
				DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			}

			// Token: 0x06006651 RID: 26193 RVA: 0x0017E050 File Offset: 0x0017C250
			public static void DrawReadOnlyButton(Graphics g, Rectangle bounds, ComboBoxState state)
			{
				if (DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer == null)
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxReadOnlyButton.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxReadOnlyButton.Part, (int)state);
				}
				else
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.SetParameters(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxReadOnlyButton.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxReadOnlyButton.Part, (int)state);
				}
				DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			}

			// Token: 0x04003A62 RID: 14946
			[ThreadStatic]
			private static VisualStyleRenderer visualStyleRenderer;

			// Token: 0x04003A63 RID: 14947
			private static readonly VisualStyleElement ComboBoxBorder = VisualStyleElement.ComboBox.Border.Normal;

			// Token: 0x04003A64 RID: 14948
			private static readonly VisualStyleElement ComboBoxDropDownButtonRight = VisualStyleElement.ComboBox.DropDownButtonRight.Normal;

			// Token: 0x04003A65 RID: 14949
			private static readonly VisualStyleElement ComboBoxDropDownButtonLeft = VisualStyleElement.ComboBox.DropDownButtonLeft.Normal;

			// Token: 0x04003A66 RID: 14950
			private static readonly VisualStyleElement ComboBoxReadOnlyButton = VisualStyleElement.ComboBox.ReadOnlyButton.Normal;
		}

		/// <summary>Represents the accessibility object for the current <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> object.</summary>
		// Token: 0x0200066F RID: 1647
		[ComVisible(true)]
		protected class DataGridViewComboBoxCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Instantiates a new instance of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.DataGridViewComboBoxCellAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> control to which this object belongs.</param>
			// Token: 0x06006653 RID: 26195 RVA: 0x0017BC46 File Offset: 0x00179E46
			public DataGridViewComboBoxCellAccessibleObject(DataGridViewCell owner)
				: base(owner)
			{
			}

			// Token: 0x06006654 RID: 26196 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override bool IsIAccessibleExSupported()
			{
				return true;
			}

			// Token: 0x06006655 RID: 26197 RVA: 0x0017E0DA File Offset: 0x0017C2DA
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50003;
				}
				if (AccessibilityImprovements.Level4 && propertyID == 30028)
				{
					return this.IsPatternSupported(10005);
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006656 RID: 26198 RVA: 0x0017E116 File Offset: 0x0017C316
			internal override bool IsPatternSupported(int patternId)
			{
				return (AccessibilityImprovements.Level4 && patternId == 10005) || base.IsPatternSupported(patternId);
			}

			// Token: 0x17001643 RID: 5699
			// (get) Token: 0x06006657 RID: 26199 RVA: 0x0017E130 File Offset: 0x0017C330
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					if (!AccessibilityImprovements.Level4)
					{
						return base.ExpandCollapseState;
					}
					DataGridViewCell owner = base.Owner;
					object obj;
					if (owner == null)
					{
						obj = null;
					}
					else
					{
						PropertyStore properties = owner.Properties;
						obj = ((properties != null) ? properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellEditingComboBox) : null);
					}
					DataGridViewComboBoxEditingControl dataGridViewComboBoxEditingControl = obj as DataGridViewComboBoxEditingControl;
					if (dataGridViewComboBoxEditingControl == null)
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					if (!dataGridViewComboBoxEditingControl.DroppedDown)
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					return UnsafeNativeMethods.ExpandCollapseState.Expanded;
				}
			}
		}
	}
}
