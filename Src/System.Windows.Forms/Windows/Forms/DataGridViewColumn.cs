using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents a column in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001BD RID: 445
	[Designer("System.Windows.Forms.Design.DataGridViewColumnDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[TypeConverter(typeof(DataGridViewColumnConverter))]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class DataGridViewColumn : DataGridViewBand, IComponent, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> class to the default state.</summary>
		// Token: 0x06001F11 RID: 7953 RVA: 0x00093272 File Offset: 0x00091472
		public DataGridViewColumn()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> class using an existing <see cref="T:System.Windows.Forms.DataGridViewCell" /> as a template.</summary>
		/// <param name="cellTemplate">An existing <see cref="T:System.Windows.Forms.DataGridViewCell" /> to use as a template.</param>
		// Token: 0x06001F12 RID: 7954 RVA: 0x0009327C File Offset: 0x0009147C
		public DataGridViewColumn(DataGridViewCell cellTemplate)
		{
			this.fillWeight = 100f;
			this.usedFillWeight = 100f;
			base.Thickness = this.ScaleToCurrentDpi(100);
			base.MinimumThickness = this.ScaleToCurrentDpi(5);
			this.name = string.Empty;
			this.bandIsRow = false;
			this.displayIndex = -1;
			this.cellTemplate = cellTemplate;
			this.autoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000932F9 File Offset: 0x000914F9
		private int ScaleToCurrentDpi(int value)
		{
			if (!DpiHelper.EnableDataGridViewControlHighDpiImprovements)
			{
				return value;
			}
			return DpiHelper.LogicalToDeviceUnits(value, 0);
		}

		/// <summary>Gets or sets the mode by which the column automatically adjusts its width.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value that determines whether the column will automatically adjust its width and how it will determine its preferred width. The default is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is a <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> that is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property results in an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> for a visible column when column headers are hidden.  
		///  -or-  
		///  The specified value when setting this property results in an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> for a visible column that is frozen.</exception>
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0009330B File Offset: 0x0009150B
		// (set) Token: 0x06001F15 RID: 7957 RVA: 0x00093314 File Offset: 0x00091514
		[SRCategory("CatLayout")]
		[DefaultValue(DataGridViewAutoSizeColumnMode.NotSet)]
		[SRDescription("DataGridViewColumn_AutoSizeModeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public DataGridViewAutoSizeColumnMode AutoSizeMode
		{
			get
			{
				return this.autoSizeMode;
			}
			set
			{
				switch (value)
				{
				case DataGridViewAutoSizeColumnMode.NotSet:
				case DataGridViewAutoSizeColumnMode.None:
				case DataGridViewAutoSizeColumnMode.ColumnHeader:
				case DataGridViewAutoSizeColumnMode.AllCellsExceptHeader:
				case DataGridViewAutoSizeColumnMode.AllCells:
				case DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader:
				case DataGridViewAutoSizeColumnMode.DisplayedCells:
					goto IL_4D;
				case (DataGridViewAutoSizeColumnMode)3:
				case (DataGridViewAutoSizeColumnMode)5:
				case (DataGridViewAutoSizeColumnMode)7:
				case (DataGridViewAutoSizeColumnMode)9:
					break;
				default:
					if (value == DataGridViewAutoSizeColumnMode.Fill)
					{
						goto IL_4D;
					}
					break;
				}
				throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAutoSizeColumnMode));
				IL_4D:
				if (this.autoSizeMode != value)
				{
					if (this.Visible && base.DataGridView != null)
					{
						if (!base.DataGridView.ColumnHeadersVisible && (value == DataGridViewAutoSizeColumnMode.ColumnHeader || (value == DataGridViewAutoSizeColumnMode.NotSet && base.DataGridView.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.ColumnHeader)))
						{
							throw new InvalidOperationException(SR.GetString("DataGridViewColumn_AutoSizeCriteriaCannotUseInvisibleHeaders"));
						}
						if (this.Frozen && (value == DataGridViewAutoSizeColumnMode.Fill || (value == DataGridViewAutoSizeColumnMode.NotSet && base.DataGridView.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)))
						{
							throw new InvalidOperationException(SR.GetString("DataGridViewColumn_FrozenColumnCannotAutoFill"));
						}
					}
					DataGridViewAutoSizeColumnMode inheritedAutoSizeMode = this.InheritedAutoSizeMode;
					bool flag = inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.Fill && inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.None && inheritedAutoSizeMode > DataGridViewAutoSizeColumnMode.NotSet;
					this.autoSizeMode = value;
					if (base.DataGridView == null)
					{
						if (this.InheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.Fill && this.InheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.None && this.InheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.NotSet)
						{
							if (!flag)
							{
								base.CachedThickness = base.Thickness;
								return;
							}
						}
						else if (base.Thickness != base.CachedThickness && flag)
						{
							base.ThicknessInternal = base.CachedThickness;
							return;
						}
					}
					else
					{
						base.DataGridView.OnAutoSizeColumnModeChanged(this, inheritedAutoSizeMode);
					}
				}
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0009346C File Offset: 0x0009166C
		// (set) Token: 0x06001F17 RID: 7959 RVA: 0x00093474 File Offset: 0x00091674
		internal TypeConverter BoundColumnConverter
		{
			get
			{
				return this.boundColumnConverter;
			}
			set
			{
				this.boundColumnConverter = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x0009347D File Offset: 0x0009167D
		// (set) Token: 0x06001F19 RID: 7961 RVA: 0x00093485 File Offset: 0x00091685
		internal int BoundColumnIndex
		{
			get
			{
				return this.boundColumnIndex;
			}
			set
			{
				this.boundColumnIndex = value;
			}
		}

		/// <summary>Gets or sets the template used to create new cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default is <see langword="null" />.</returns>
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0009348E File Offset: 0x0009168E
		// (set) Token: 0x06001F1B RID: 7963 RVA: 0x00093496 File Offset: 0x00091696
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DataGridViewCell CellTemplate
		{
			get
			{
				return this.cellTemplate;
			}
			set
			{
				this.cellTemplate = value;
			}
		}

		/// <summary>Gets the run-time type of the cell template.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> used as a template for this column. The default is <see langword="null" />.</returns>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x0009349F File Offset: 0x0009169F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Type CellType
		{
			get
			{
				if (this.cellTemplate != null)
				{
					return this.cellTemplate.GetType();
				}
				return null;
			}
		}

		/// <summary>Gets or sets the shortcut menu for the column.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the current <see cref="T:System.Windows.Forms.DataGridViewColumn" />. The default is <see langword="null" />.</returns>
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x000934B6 File Offset: 0x000916B6
		// (set) Token: 0x06001F1E RID: 7966 RVA: 0x000934BE File Offset: 0x000916BE
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ColumnContextMenuStripDescr")]
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
			}
		}

		/// <summary>Gets or sets the name of the data source property or database column to which the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> is bound.</summary>
		/// <returns>The case-insensitive name of the property or database column associated with the <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</returns>
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x000934C7 File Offset: 0x000916C7
		// (set) Token: 0x06001F20 RID: 7968 RVA: 0x000934CF File Offset: 0x000916CF
		[Browsable(true)]
		[DefaultValue("")]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.DataGridViewColumnDataPropertyNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("DataGridView_ColumnDataPropertyNameDescr")]
		[SRCategory("CatData")]
		public string DataPropertyName
		{
			get
			{
				return this.dataPropertyName;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value != this.dataPropertyName)
				{
					this.dataPropertyName = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnDataPropertyNameChanged(this);
					}
				}
			}
		}

		/// <summary>Gets or sets the column's default cell style.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the default style of the cells in the column.</returns>
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x00093504 File Offset: 0x00091704
		// (set) Token: 0x06001F22 RID: 7970 RVA: 0x0009350C File Offset: 0x0009170C
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

		// Token: 0x06001F23 RID: 7971 RVA: 0x00093518 File Offset: 0x00091718
		private bool ShouldSerializeDefaultCellStyle()
		{
			if (!base.HasDefaultCellStyle)
			{
				return false;
			}
			DataGridViewCellStyle defaultCellStyle = this.DefaultCellStyle;
			return !defaultCellStyle.BackColor.IsEmpty || !defaultCellStyle.ForeColor.IsEmpty || !defaultCellStyle.SelectionBackColor.IsEmpty || !defaultCellStyle.SelectionForeColor.IsEmpty || defaultCellStyle.Font != null || !defaultCellStyle.IsNullValueDefault || !defaultCellStyle.IsDataSourceNullValueDefault || !string.IsNullOrEmpty(defaultCellStyle.Format) || !defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) || defaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet || defaultCellStyle.WrapMode != DataGridViewTriState.NotSet || defaultCellStyle.Tag != null || !defaultCellStyle.Padding.Equals(Padding.Empty);
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x000935F1 File Offset: 0x000917F1
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x000935F9 File Offset: 0x000917F9
		internal int DesiredFillWidth
		{
			get
			{
				return this.desiredFillWidth;
			}
			set
			{
				this.desiredFillWidth = value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x00093602 File Offset: 0x00091802
		// (set) Token: 0x06001F27 RID: 7975 RVA: 0x0009360A File Offset: 0x0009180A
		internal int DesiredMinimumWidth
		{
			get
			{
				return this.desiredMinimumWidth;
			}
			set
			{
				this.desiredMinimumWidth = value;
			}
		}

		/// <summary>Gets or sets the display order of the column relative to the currently displayed columns.</summary>
		/// <returns>The zero-based position of the column as it is displayed in the associated <see cref="T:System.Windows.Forms.DataGridView" />, or -1 if the band is not contained within a control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> is not <see langword="null" /> and the specified value when setting this property is less than 0 or greater than or equal to the number of columns in the control.  
		/// -or-  
		/// <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> is <see langword="null" /> and the specified value when setting this property is less than -1.  
		/// -or-  
		/// The specified value when setting this property is equal to <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x00093613 File Offset: 0x00091813
		// (set) Token: 0x06001F29 RID: 7977 RVA: 0x0009361C File Offset: 0x0009181C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int DisplayIndex
		{
			get
			{
				return this.displayIndex;
			}
			set
			{
				if (this.displayIndex != value)
				{
					if (value == 2147483647)
					{
						throw new ArgumentOutOfRangeException("DisplayIndex", value, SR.GetString("DataGridViewColumn_DisplayIndexTooLarge", new object[] { int.MaxValue.ToString(CultureInfo.CurrentCulture) }));
					}
					if (base.DataGridView != null)
					{
						if (value < 0)
						{
							throw new ArgumentOutOfRangeException("DisplayIndex", value, SR.GetString("DataGridViewColumn_DisplayIndexNegative"));
						}
						if (value >= base.DataGridView.Columns.Count)
						{
							throw new ArgumentOutOfRangeException("DisplayIndex", value, SR.GetString("DataGridViewColumn_DisplayIndexExceedsColumnCount"));
						}
						base.DataGridView.OnColumnDisplayIndexChanging(this, value);
						this.displayIndex = value;
						try
						{
							base.DataGridView.InDisplayIndexAdjustments = true;
							base.DataGridView.OnColumnDisplayIndexChanged_PreNotification();
							base.DataGridView.OnColumnDisplayIndexChanged(this);
							base.DataGridView.OnColumnDisplayIndexChanged_PostNotification();
							return;
						}
						finally
						{
							base.DataGridView.InDisplayIndexAdjustments = false;
						}
					}
					if (value < -1)
					{
						throw new ArgumentOutOfRangeException("DisplayIndex", value, SR.GetString("DataGridViewColumn_DisplayIndexTooNegative"));
					}
					this.displayIndex = value;
				}
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x00093754 File Offset: 0x00091954
		// (set) Token: 0x06001F2B RID: 7979 RVA: 0x00093762 File Offset: 0x00091962
		internal bool DisplayIndexHasChanged
		{
			get
			{
				return (this.flags & 16) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 16;
					return;
				}
				this.flags = (byte)((int)this.flags & -17);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x00093788 File Offset: 0x00091988
		internal int DisplayIndexInternal
		{
			set
			{
				this.displayIndex = value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> is disposed.</summary>
		// Token: 0x14000184 RID: 388
		// (add) Token: 0x06001F2D RID: 7981 RVA: 0x00093791 File Offset: 0x00091991
		// (remove) Token: 0x06001F2E RID: 7982 RVA: 0x000937AA File Offset: 0x000919AA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler Disposed
		{
			add
			{
				this.disposed = (EventHandler)Delegate.Combine(this.disposed, value);
			}
			remove
			{
				this.disposed = (EventHandler)Delegate.Remove(this.disposed, value);
			}
		}

		/// <summary>Gets or sets the width, in pixels, of the column divider.</summary>
		/// <returns>The thickness, in pixels, of the divider (the column's right margin).</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x000937C3 File Offset: 0x000919C3
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x000937CB File Offset: 0x000919CB
		[DefaultValue(0)]
		[SRCategory("CatLayout")]
		[SRDescription("DataGridView_ColumnDividerWidthDescr")]
		public int DividerWidth
		{
			get
			{
				return base.DividerThickness;
			}
			set
			{
				base.DividerThickness = value;
			}
		}

		/// <summary>Gets or sets a value that represents the width of the column when it is in fill mode relative to the widths of other fill-mode columns in the control.</summary>
		/// <returns>A <see cref="T:System.Single" /> representing the width of the column when it is in fill mode relative to the widths of other fill-mode columns. The default is 100.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than or equal to 0.</exception>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x000937D4 File Offset: 0x000919D4
		// (set) Token: 0x06001F32 RID: 7986 RVA: 0x000937DC File Offset: 0x000919DC
		[SRCategory("CatLayout")]
		[DefaultValue(100f)]
		[SRDescription("DataGridViewColumn_FillWeightDescr")]
		public float FillWeight
		{
			get
			{
				return this.fillWeight;
			}
			set
			{
				if (value <= 0f)
				{
					throw new ArgumentOutOfRangeException("FillWeight", SR.GetString("InvalidLowBoundArgument", new object[]
					{
						"FillWeight",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (value > 65535f)
				{
					throw new ArgumentOutOfRangeException("FillWeight", SR.GetString("InvalidHighBoundArgumentEx", new object[]
					{
						"FillWeight",
						value.ToString(CultureInfo.CurrentCulture),
						ushort.MaxValue.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnFillWeightChanging(this, value);
					this.fillWeight = value;
					base.DataGridView.OnColumnFillWeightChanged(this);
					return;
				}
				this.fillWeight = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (set) Token: 0x06001F33 RID: 7987 RVA: 0x000938B3 File Offset: 0x00091AB3
		internal float FillWeightInternal
		{
			set
			{
				this.fillWeight = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a column will move when a user scrolls the <see cref="T:System.Windows.Forms.DataGridView" /> control horizontally.</summary>
		/// <returns>
		///   <see langword="true" /> to freeze the column; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x000938BC File Offset: 0x00091ABC
		// (set) Token: 0x06001F35 RID: 7989 RVA: 0x000938C4 File Offset: 0x00091AC4
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		[SRCategory("CatLayout")]
		[SRDescription("DataGridView_ColumnFrozenDescr")]
		public override bool Frozen
		{
			get
			{
				return base.Frozen;
			}
			set
			{
				base.Frozen = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> that represents the column header.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> that represents the header cell for the column.</returns>
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x000938CD File Offset: 0x00091ACD
		// (set) Token: 0x06001F37 RID: 7991 RVA: 0x000938DA File Offset: 0x00091ADA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataGridViewColumnHeaderCell HeaderCell
		{
			get
			{
				return (DataGridViewColumnHeaderCell)base.HeaderCellCore;
			}
			set
			{
				base.HeaderCellCore = value;
			}
		}

		/// <summary>Gets or sets the caption text on the column's header cell.</summary>
		/// <returns>A <see cref="T:System.String" /> with the desired text. The default is an empty string ("").</returns>
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x000938E4 File Offset: 0x00091AE4
		// (set) Token: 0x06001F39 RID: 7993 RVA: 0x0009391C File Offset: 0x00091B1C
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ColumnHeaderTextDescr")]
		[Localizable(true)]
		public string HeaderText
		{
			get
			{
				if (!base.HasHeaderCell)
				{
					return string.Empty;
				}
				string text = this.HeaderCell.Value as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if ((value != null || base.HasHeaderCell) && this.HeaderCell.ValueType != null && this.HeaderCell.ValueType.IsAssignableFrom(typeof(string)))
				{
					this.HeaderCell.Value = value;
				}
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0009396F File Offset: 0x00091B6F
		private bool ShouldSerializeHeaderText()
		{
			return base.HasHeaderCell && this.HeaderCell.ContainsLocalValue;
		}

		/// <summary>Gets the sizing mode in effect for the column.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value in effect for the column.</returns>
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x00093986 File Offset: 0x00091B86
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataGridViewAutoSizeColumnMode InheritedAutoSizeMode
		{
			get
			{
				return this.GetInheritedAutoSizeMode(base.DataGridView);
			}
		}

		/// <summary>Gets the cell style currently applied to the column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the cell style used to display the column.</returns>
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x00093994 File Offset: 0x00091B94
		[Browsable(false)]
		public override DataGridViewCellStyle InheritedStyle
		{
			get
			{
				DataGridViewCellStyle dataGridViewCellStyle = null;
				if (base.HasDefaultCellStyle)
				{
					dataGridViewCellStyle = this.DefaultCellStyle;
				}
				if (base.DataGridView == null)
				{
					return dataGridViewCellStyle;
				}
				DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
				DataGridViewCellStyle defaultCellStyle = base.DataGridView.DefaultCellStyle;
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.BackColor.IsEmpty)
				{
					dataGridViewCellStyle2.BackColor = dataGridViewCellStyle.BackColor;
				}
				else
				{
					dataGridViewCellStyle2.BackColor = defaultCellStyle.BackColor;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle2.ForeColor = dataGridViewCellStyle.ForeColor;
				}
				else
				{
					dataGridViewCellStyle2.ForeColor = defaultCellStyle.ForeColor;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle2.SelectionBackColor = dataGridViewCellStyle.SelectionBackColor;
				}
				else
				{
					dataGridViewCellStyle2.SelectionBackColor = defaultCellStyle.SelectionBackColor;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle2.SelectionForeColor = dataGridViewCellStyle.SelectionForeColor;
				}
				else
				{
					dataGridViewCellStyle2.SelectionForeColor = defaultCellStyle.SelectionForeColor;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.Font != null)
				{
					dataGridViewCellStyle2.Font = dataGridViewCellStyle.Font;
				}
				else
				{
					dataGridViewCellStyle2.Font = defaultCellStyle.Font;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsNullValueDefault)
				{
					dataGridViewCellStyle2.NullValue = dataGridViewCellStyle.NullValue;
				}
				else
				{
					dataGridViewCellStyle2.NullValue = defaultCellStyle.NullValue;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsDataSourceNullValueDefault)
				{
					dataGridViewCellStyle2.DataSourceNullValue = dataGridViewCellStyle.DataSourceNullValue;
				}
				else
				{
					dataGridViewCellStyle2.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.Format.Length != 0)
				{
					dataGridViewCellStyle2.Format = dataGridViewCellStyle.Format;
				}
				else
				{
					dataGridViewCellStyle2.Format = defaultCellStyle.Format;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsFormatProviderDefault)
				{
					dataGridViewCellStyle2.FormatProvider = dataGridViewCellStyle.FormatProvider;
				}
				else
				{
					dataGridViewCellStyle2.FormatProvider = defaultCellStyle.FormatProvider;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
				{
					dataGridViewCellStyle2.AlignmentInternal = dataGridViewCellStyle.Alignment;
				}
				else
				{
					dataGridViewCellStyle2.AlignmentInternal = defaultCellStyle.Alignment;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.WrapMode != DataGridViewTriState.NotSet)
				{
					dataGridViewCellStyle2.WrapModeInternal = dataGridViewCellStyle.WrapMode;
				}
				else
				{
					dataGridViewCellStyle2.WrapModeInternal = defaultCellStyle.WrapMode;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.Tag != null)
				{
					dataGridViewCellStyle2.Tag = dataGridViewCellStyle.Tag;
				}
				else
				{
					dataGridViewCellStyle2.Tag = defaultCellStyle.Tag;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.Padding != Padding.Empty)
				{
					dataGridViewCellStyle2.PaddingInternal = dataGridViewCellStyle.Padding;
				}
				else
				{
					dataGridViewCellStyle2.PaddingInternal = defaultCellStyle.Padding;
				}
				return dataGridViewCellStyle2;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x00093BDF File Offset: 0x00091DDF
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x00093BEC File Offset: 0x00091DEC
		internal bool IsBrowsableInternal
		{
			get
			{
				return (this.flags & 8) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 8;
					return;
				}
				this.flags = (byte)((int)this.flags & -9);
			}
		}

		/// <summary>Gets a value indicating whether the column is bound to a data source.</summary>
		/// <returns>
		///   <see langword="true" /> if the column is connected to a data source; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x00093C11 File Offset: 0x00091E11
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsDataBound
		{
			get
			{
				return this.IsDataBoundInternal;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x00093C19 File Offset: 0x00091E19
		// (set) Token: 0x06001F41 RID: 8001 RVA: 0x00093C26 File Offset: 0x00091E26
		internal bool IsDataBoundInternal
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

		/// <summary>Gets or sets the minimum width, in pixels, of the column.</summary>
		/// <returns>The number of pixels, from 2 to <see cref="F:System.Int32.MaxValue" />, that specifies the minimum width of the column. The default is 5.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 2 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x00093C4B File Offset: 0x00091E4B
		// (set) Token: 0x06001F43 RID: 8003 RVA: 0x00093C53 File Offset: 0x00091E53
		[DefaultValue(5)]
		[Localizable(true)]
		[SRCategory("CatLayout")]
		[SRDescription("DataGridView_ColumnMinimumWidthDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int MinimumWidth
		{
			get
			{
				return base.MinimumThickness;
			}
			set
			{
				base.MinimumThickness = value;
			}
		}

		/// <summary>Gets or sets the name of the column.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the column. The default is an empty string ("").</returns>
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x00093C5C File Offset: 0x00091E5C
		// (set) Token: 0x06001F45 RID: 8005 RVA: 0x00093C90 File Offset: 0x00091E90
		[Browsable(false)]
		public string Name
		{
			get
			{
				if (this.Site != null && !string.IsNullOrEmpty(this.Site.Name))
				{
					this.name = this.Site.Name;
				}
				return this.name;
			}
			set
			{
				string text = this.name;
				if (string.IsNullOrEmpty(value))
				{
					this.name = string.Empty;
				}
				else
				{
					this.name = value;
				}
				if (base.DataGridView != null && !string.Equals(this.name, text, StringComparison.Ordinal))
				{
					base.DataGridView.OnColumnNameChanged(this);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can edit the column's cells.</summary>
		/// <returns>
		///   <see langword="true" /> if the user cannot edit the column's cells; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property is set to <see langword="false" /> for a column that is bound to a read-only data source.</exception>
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x00093CE3 File Offset: 0x00091EE3
		// (set) Token: 0x06001F47 RID: 8007 RVA: 0x00093CEC File Offset: 0x00091EEC
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ColumnReadOnlyDescr")]
		public override bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				if (this.IsDataBound && base.DataGridView != null && base.DataGridView.DataConnection != null && this.boundColumnIndex != -1 && base.DataGridView.DataConnection.DataFieldIsReadOnly(this.boundColumnIndex) && !value)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_ColumnBoundToAReadOnlyFieldMustRemainReadOnly"));
				}
				base.ReadOnly = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the column is resizable.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewTriState" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.True" />.</returns>
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x00093D51 File Offset: 0x00091F51
		// (set) Token: 0x06001F49 RID: 8009 RVA: 0x00093D59 File Offset: 0x00091F59
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ColumnResizableDescr")]
		public override DataGridViewTriState Resizable
		{
			get
			{
				return base.Resizable;
			}
			set
			{
				base.Resizable = value;
			}
		}

		/// <summary>Gets or sets the site of the column.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the column, if any.</returns>
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x00093D62 File Offset: 0x00091F62
		// (set) Token: 0x06001F4B RID: 8011 RVA: 0x00093D6A File Offset: 0x00091F6A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ISite Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		/// <summary>Gets or sets the sort mode for the column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewColumnSortMode" /> that specifies the criteria used to order the rows based on the cell values in a column.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value assigned to the property conflicts with <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" />.</exception>
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00093D73 File Offset: 0x00091F73
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x00093D90 File Offset: 0x00091F90
		[DefaultValue(DataGridViewColumnSortMode.NotSortable)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ColumnSortModeDescr")]
		public DataGridViewColumnSortMode SortMode
		{
			get
			{
				if ((this.flags & 1) != 0)
				{
					return DataGridViewColumnSortMode.Automatic;
				}
				if ((this.flags & 2) != 0)
				{
					return DataGridViewColumnSortMode.Programmatic;
				}
				return DataGridViewColumnSortMode.NotSortable;
			}
			set
			{
				if (value != this.SortMode)
				{
					if (value != DataGridViewColumnSortMode.NotSortable)
					{
						if (base.DataGridView != null && !base.DataGridView.InInitialization && value == DataGridViewColumnSortMode.Automatic && (base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect))
						{
							throw new InvalidOperationException(SR.GetString("DataGridViewColumn_SortModeAndSelectionModeClash", new object[]
							{
								value.ToString(),
								base.DataGridView.SelectionMode.ToString()
							}));
						}
						if (value == DataGridViewColumnSortMode.Automatic)
						{
							this.flags = (byte)((int)this.flags & -3);
							this.flags |= 1;
						}
						else
						{
							this.flags = (byte)((int)this.flags & -2);
							this.flags |= 2;
						}
					}
					else
					{
						this.flags = (byte)((int)this.flags & -2);
						this.flags = (byte)((int)this.flags & -3);
					}
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnSortModeChanged(this);
					}
				}
			}
		}

		/// <summary>Gets or sets the text used for ToolTips.</summary>
		/// <returns>The text to display as a ToolTip for the column.</returns>
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x00093EA0 File Offset: 0x000920A0
		// (set) Token: 0x06001F4F RID: 8015 RVA: 0x00093EAD File Offset: 0x000920AD
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ColumnToolTipTextDescr")]
		public string ToolTipText
		{
			get
			{
				return this.HeaderCell.ToolTipText;
			}
			set
			{
				if (string.Compare(this.ToolTipText, value, false, CultureInfo.InvariantCulture) != 0)
				{
					this.HeaderCell.ToolTipText = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnToolTipTextChanged(this);
					}
				}
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x00093EE3 File Offset: 0x000920E3
		// (set) Token: 0x06001F51 RID: 8017 RVA: 0x00093EEB File Offset: 0x000920EB
		internal float UsedFillWeight
		{
			get
			{
				return this.usedFillWeight;
			}
			set
			{
				this.usedFillWeight = value;
			}
		}

		/// <summary>Gets or sets the data type of the values in the column's cells.</summary>
		/// <returns>A <see cref="T:System.Type" /> that describes the run-time class of the values stored in the column's cells.</returns>
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x00093EF4 File Offset: 0x000920F4
		// (set) Token: 0x06001F53 RID: 8019 RVA: 0x00093F0B File Offset: 0x0009210B
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Type ValueType
		{
			get
			{
				return (Type)base.Properties.GetObject(DataGridViewColumn.PropDataGridViewColumnValueType);
			}
			set
			{
				base.Properties.SetObject(DataGridViewColumn.PropDataGridViewColumnValueType, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the column is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the column is visible; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x00093F1E File Offset: 0x0009211E
		// (set) Token: 0x06001F55 RID: 8021 RVA: 0x00093F26 File Offset: 0x00092126
		[DefaultValue(true)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ColumnVisibleDescr")]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		/// <summary>Gets or sets the current width of the column.</summary>
		/// <returns>The width, in pixels, of the column. The default is 100.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is greater than 65536.</exception>
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x00093F2F File Offset: 0x0009212F
		// (set) Token: 0x06001F57 RID: 8023 RVA: 0x00093F37 File Offset: 0x00092137
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("DataGridView_ColumnWidthDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int Width
		{
			get
			{
				return base.Thickness;
			}
			set
			{
				base.Thickness = value;
			}
		}

		/// <summary>Creates an exact copy of this band.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
		// Token: 0x06001F58 RID: 8024 RVA: 0x00093F40 File Offset: 0x00092140
		public override object Clone()
		{
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)Activator.CreateInstance(base.GetType());
			if (dataGridViewColumn != null)
			{
				this.CloneInternal(dataGridViewColumn);
			}
			return dataGridViewColumn;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00093F6C File Offset: 0x0009216C
		internal void CloneInternal(DataGridViewColumn dataGridViewColumn)
		{
			base.CloneInternal(dataGridViewColumn);
			dataGridViewColumn.name = this.Name;
			dataGridViewColumn.displayIndex = -1;
			dataGridViewColumn.HeaderText = this.HeaderText;
			dataGridViewColumn.DataPropertyName = this.DataPropertyName;
			if (dataGridViewColumn.CellTemplate != null)
			{
				dataGridViewColumn.cellTemplate = (DataGridViewCell)this.CellTemplate.Clone();
			}
			else
			{
				dataGridViewColumn.cellTemplate = null;
			}
			if (base.HasHeaderCell)
			{
				dataGridViewColumn.HeaderCell = (DataGridViewColumnHeaderCell)this.HeaderCell.Clone();
			}
			dataGridViewColumn.AutoSizeMode = this.AutoSizeMode;
			dataGridViewColumn.SortMode = this.SortMode;
			dataGridViewColumn.FillWeightInternal = this.FillWeight;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.DataGridViewBand" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001F5A RID: 8026 RVA: 0x00094014 File Offset: 0x00092214
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					lock (this)
					{
						if (this.site != null && this.site.Container != null)
						{
							this.site.Container.Remove(this);
						}
						if (this.disposed != null)
						{
							this.disposed(this, EventArgs.Empty);
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x000940A4 File Offset: 0x000922A4
		internal DataGridViewAutoSizeColumnMode GetInheritedAutoSizeMode(DataGridView dataGridView)
		{
			if (dataGridView != null && this.autoSizeMode == DataGridViewAutoSizeColumnMode.NotSet)
			{
				DataGridViewAutoSizeColumnsMode autoSizeColumnsMode = dataGridView.AutoSizeColumnsMode;
				switch (autoSizeColumnsMode)
				{
				case DataGridViewAutoSizeColumnsMode.ColumnHeader:
					return DataGridViewAutoSizeColumnMode.ColumnHeader;
				case (DataGridViewAutoSizeColumnsMode)3:
				case (DataGridViewAutoSizeColumnsMode)5:
				case (DataGridViewAutoSizeColumnsMode)7:
				case (DataGridViewAutoSizeColumnsMode)9:
					break;
				case DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader:
					return DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
				case DataGridViewAutoSizeColumnsMode.AllCells:
					return DataGridViewAutoSizeColumnMode.AllCells;
				case DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader:
					return DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
				case DataGridViewAutoSizeColumnsMode.DisplayedCells:
					return DataGridViewAutoSizeColumnMode.DisplayedCells;
				default:
					if (autoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)
					{
						return DataGridViewAutoSizeColumnMode.Fill;
					}
					break;
				}
				return DataGridViewAutoSizeColumnMode.None;
			}
			return this.autoSizeMode;
		}

		/// <summary>Calculates the ideal width of the column based on the specified criteria.</summary>
		/// <param name="autoSizeColumnMode">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value that specifies an automatic sizing mode.</param>
		/// <param name="fixedHeight">
		///   <see langword="true" /> to calculate the width of the column based on the current row heights; <see langword="false" /> to calculate the width with the expectation that the row heights will be adjusted.</param>
		/// <returns>The ideal width, in pixels, of the column.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="autoSizeColumnMode" /> is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet" />, <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.None" />, or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" />.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeColumnMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value.</exception>
		// Token: 0x06001F5C RID: 8028 RVA: 0x0009410C File Offset: 0x0009230C
		public virtual int GetPreferredWidth(DataGridViewAutoSizeColumnMode autoSizeColumnMode, bool fixedHeight)
		{
			if (autoSizeColumnMode == DataGridViewAutoSizeColumnMode.NotSet || autoSizeColumnMode == DataGridViewAutoSizeColumnMode.None || autoSizeColumnMode == DataGridViewAutoSizeColumnMode.Fill)
			{
				throw new ArgumentException(SR.GetString("DataGridView_NeedColumnAutoSizingCriteria", new object[] { "autoSizeColumnMode" }));
			}
			switch (autoSizeColumnMode)
			{
			case DataGridViewAutoSizeColumnMode.NotSet:
			case DataGridViewAutoSizeColumnMode.None:
			case DataGridViewAutoSizeColumnMode.ColumnHeader:
			case DataGridViewAutoSizeColumnMode.AllCellsExceptHeader:
			case DataGridViewAutoSizeColumnMode.AllCells:
			case DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader:
			case DataGridViewAutoSizeColumnMode.DisplayedCells:
				goto IL_77;
			case (DataGridViewAutoSizeColumnMode)3:
			case (DataGridViewAutoSizeColumnMode)5:
			case (DataGridViewAutoSizeColumnMode)7:
			case (DataGridViewAutoSizeColumnMode)9:
				break;
			default:
				if (autoSizeColumnMode == DataGridViewAutoSizeColumnMode.Fill)
				{
					goto IL_77;
				}
				break;
			}
			throw new InvalidEnumArgumentException("value", (int)autoSizeColumnMode, typeof(DataGridViewAutoSizeColumnMode));
			IL_77:
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView == null)
			{
				return -1;
			}
			int num = 0;
			if (dataGridView.ColumnHeadersVisible && (autoSizeColumnMode & DataGridViewAutoSizeColumnMode.ColumnHeader) != DataGridViewAutoSizeColumnMode.NotSet)
			{
				int num2;
				if (fixedHeight)
				{
					num2 = this.HeaderCell.GetPreferredWidth(-1, dataGridView.ColumnHeadersHeight);
				}
				else
				{
					num2 = this.HeaderCell.GetPreferredSize(-1).Width;
				}
				if (num < num2)
				{
					num = num2;
				}
			}
			if ((autoSizeColumnMode & DataGridViewAutoSizeColumnMode.AllCellsExceptHeader) != DataGridViewAutoSizeColumnMode.NotSet)
			{
				for (int num3 = dataGridView.Rows.GetFirstRow(DataGridViewElementStates.Visible); num3 != -1; num3 = dataGridView.Rows.GetNextRow(num3, DataGridViewElementStates.Visible))
				{
					DataGridViewRow dataGridViewRow = dataGridView.Rows.SharedRow(num3);
					int num2;
					if (fixedHeight)
					{
						num2 = dataGridViewRow.Cells[base.Index].GetPreferredWidth(num3, dataGridViewRow.Thickness);
					}
					else
					{
						num2 = dataGridViewRow.Cells[base.Index].GetPreferredSize(num3).Width;
					}
					if (num < num2)
					{
						num = num2;
					}
				}
			}
			else if ((autoSizeColumnMode & DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader) != DataGridViewAutoSizeColumnMode.NotSet)
			{
				int height = dataGridView.LayoutInfo.Data.Height;
				int num4 = 0;
				int num3 = dataGridView.Rows.GetFirstRow(DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible);
				while (num3 != -1 && num4 < height)
				{
					DataGridViewRow dataGridViewRow = dataGridView.Rows.SharedRow(num3);
					int num2;
					if (fixedHeight)
					{
						num2 = dataGridViewRow.Cells[base.Index].GetPreferredWidth(num3, dataGridViewRow.Thickness);
					}
					else
					{
						num2 = dataGridViewRow.Cells[base.Index].GetPreferredSize(num3).Width;
					}
					if (num < num2)
					{
						num = num2;
					}
					num4 += dataGridViewRow.Thickness;
					num3 = dataGridView.Rows.GetNextRow(num3, DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible);
				}
				if (num4 < height)
				{
					num3 = dataGridView.DisplayedBandsInfo.FirstDisplayedScrollingRow;
					while (num3 != -1 && num4 < height)
					{
						DataGridViewRow dataGridViewRow = dataGridView.Rows.SharedRow(num3);
						int num2;
						if (fixedHeight)
						{
							num2 = dataGridViewRow.Cells[base.Index].GetPreferredWidth(num3, dataGridViewRow.Thickness);
						}
						else
						{
							num2 = dataGridViewRow.Cells[base.Index].GetPreferredSize(num3).Width;
						}
						if (num < num2)
						{
							num = num2;
						}
						num4 += dataGridViewRow.Thickness;
						num3 = dataGridView.Rows.GetNextRow(num3, DataGridViewElementStates.Visible);
					}
				}
			}
			return num;
		}

		/// <summary>Gets a string that describes the column.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		// Token: 0x06001F5D RID: 8029 RVA: 0x000943CC File Offset: 0x000925CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewColumn { Name=");
			stringBuilder.Append(this.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000D1E RID: 3358
		private const float DATAGRIDVIEWCOLUMN_defaultFillWeight = 100f;

		// Token: 0x04000D1F RID: 3359
		private const int DATAGRIDVIEWCOLUMN_defaultWidth = 100;

		// Token: 0x04000D20 RID: 3360
		private const int DATAGRIDVIEWCOLUMN_defaultMinColumnThickness = 5;

		// Token: 0x04000D21 RID: 3361
		private const byte DATAGRIDVIEWCOLUMN_automaticSort = 1;

		// Token: 0x04000D22 RID: 3362
		private const byte DATAGRIDVIEWCOLUMN_programmaticSort = 2;

		// Token: 0x04000D23 RID: 3363
		private const byte DATAGRIDVIEWCOLUMN_isDataBound = 4;

		// Token: 0x04000D24 RID: 3364
		private const byte DATAGRIDVIEWCOLUMN_isBrowsableInternal = 8;

		// Token: 0x04000D25 RID: 3365
		private const byte DATAGRIDVIEWCOLUMN_displayIndexHasChangedInternal = 16;

		// Token: 0x04000D26 RID: 3366
		private byte flags;

		// Token: 0x04000D27 RID: 3367
		private DataGridViewCell cellTemplate;

		// Token: 0x04000D28 RID: 3368
		private string name;

		// Token: 0x04000D29 RID: 3369
		private int displayIndex;

		// Token: 0x04000D2A RID: 3370
		private int desiredFillWidth;

		// Token: 0x04000D2B RID: 3371
		private int desiredMinimumWidth;

		// Token: 0x04000D2C RID: 3372
		private float fillWeight;

		// Token: 0x04000D2D RID: 3373
		private float usedFillWeight;

		// Token: 0x04000D2E RID: 3374
		private DataGridViewAutoSizeColumnMode autoSizeMode;

		// Token: 0x04000D2F RID: 3375
		private int boundColumnIndex = -1;

		// Token: 0x04000D30 RID: 3376
		private string dataPropertyName = string.Empty;

		// Token: 0x04000D31 RID: 3377
		private TypeConverter boundColumnConverter;

		// Token: 0x04000D32 RID: 3378
		private ISite site;

		// Token: 0x04000D33 RID: 3379
		private EventHandler disposed;

		// Token: 0x04000D34 RID: 3380
		private static readonly int PropDataGridViewColumnValueType = PropertyStore.CreateKey();
	}
}
