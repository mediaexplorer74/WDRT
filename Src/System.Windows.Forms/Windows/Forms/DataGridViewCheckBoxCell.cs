using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Displays a check box user interface (UI) to use in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001BA RID: 442
	public class DataGridViewCheckBoxCell : DataGridViewCell, IDataGridViewEditingCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> class to its default state.</summary>
		// Token: 0x06001EBF RID: 7871 RVA: 0x000909B7 File Offset: 0x0008EBB7
		public DataGridViewCheckBoxCell()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> class, enabling binary or ternary state.</summary>
		/// <param name="threeState">
		///   <see langword="true" /> to enable ternary state; <see langword="false" /> to enable binary state.</param>
		// Token: 0x06001EC0 RID: 7872 RVA: 0x000909C0 File Offset: 0x0008EBC0
		public DataGridViewCheckBoxCell(bool threeState)
		{
			if (threeState)
			{
				this.flags = 1;
			}
		}

		/// <summary>Gets or sets the formatted value of the control hosted by the cell when it is in edit mode.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the cell's value.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property value is <see langword="null" />.  
		///  -or-  
		///  The assigned value is <see langword="null" /> or is not of the type indicated by the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property.  
		///  -or-  
		///  The assigned value is not of type <see cref="T:System.Boolean" /> nor of type <see cref="T:System.Windows.Forms.CheckState" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property value is <see langword="null" />.</exception>
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x000909D2 File Offset: 0x0008EBD2
		// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x000909DC File Offset: 0x0008EBDC
		public virtual object EditingCellFormattedValue
		{
			get
			{
				return this.GetEditingCellFormattedValue(DataGridViewDataErrorContexts.Formatting);
			}
			set
			{
				if (this.FormattedValueType == null)
				{
					throw new ArgumentException(SR.GetString("DataGridViewCell_FormattedValueTypeNull"));
				}
				if (value == null || !this.FormattedValueType.IsAssignableFrom(value.GetType()))
				{
					throw new ArgumentException(SR.GetString("DataGridViewCheckBoxCell_InvalidValueType"));
				}
				if (value is CheckState)
				{
					if ((CheckState)value == CheckState.Checked)
					{
						this.flags |= 16;
						this.flags = (byte)((int)this.flags & -33);
						return;
					}
					if ((CheckState)value == CheckState.Indeterminate)
					{
						this.flags |= 32;
						this.flags = (byte)((int)this.flags & -17);
						return;
					}
					this.flags = (byte)((int)this.flags & -17);
					this.flags = (byte)((int)this.flags & -33);
					return;
				}
				else
				{
					if (value is bool)
					{
						if ((bool)value)
						{
							this.flags |= 16;
						}
						else
						{
							this.flags = (byte)((int)this.flags & -17);
						}
						this.flags = (byte)((int)this.flags & -33);
						return;
					}
					throw new ArgumentException(SR.GetString("DataGridViewCheckBoxCell_InvalidValueType"));
				}
			}
		}

		/// <summary>Gets or sets a flag indicating that the value has been changed for this cell.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell's value has changed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x00090AFC File Offset: 0x0008ECFC
		// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x00090B09 File Offset: 0x0008ED09
		public virtual bool EditingCellValueChanged
		{
			get
			{
				return (this.flags & 2) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 2;
					return;
				}
				this.flags = (byte)((int)this.flags & -3);
			}
		}

		/// <summary>Gets the formatted value of the cell while it is in edit mode.</summary>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that describes the context in which any formatting error occurs.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the formatted value of the editing cell.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property value is <see langword="null" />.</exception>
		// Token: 0x06001EC5 RID: 7877 RVA: 0x00090B30 File Offset: 0x0008ED30
		public virtual object GetEditingCellFormattedValue(DataGridViewDataErrorContexts context)
		{
			if (this.FormattedValueType == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCell_FormattedValueTypeNull"));
			}
			if (this.FormattedValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultCheckStateType))
			{
				if ((this.flags & 16) != 0)
				{
					if ((context & DataGridViewDataErrorContexts.ClipboardContent) != (DataGridViewDataErrorContexts)0)
					{
						return SR.GetString("DataGridViewCheckBoxCell_ClipboardChecked");
					}
					return CheckState.Checked;
				}
				else if ((this.flags & 32) != 0)
				{
					if ((context & DataGridViewDataErrorContexts.ClipboardContent) != (DataGridViewDataErrorContexts)0)
					{
						return SR.GetString("DataGridViewCheckBoxCell_ClipboardIndeterminate");
					}
					return CheckState.Indeterminate;
				}
				else
				{
					if ((context & DataGridViewDataErrorContexts.ClipboardContent) != (DataGridViewDataErrorContexts)0)
					{
						return SR.GetString("DataGridViewCheckBoxCell_ClipboardUnchecked");
					}
					return CheckState.Unchecked;
				}
			}
			else
			{
				if (!this.FormattedValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultBooleanType))
				{
					return null;
				}
				bool flag = (this.flags & 16) > 0;
				if ((context & DataGridViewDataErrorContexts.ClipboardContent) != (DataGridViewDataErrorContexts)0)
				{
					return SR.GetString(flag ? "DataGridViewCheckBoxCell_ClipboardTrue" : "DataGridViewCheckBoxCell_ClipboardFalse");
				}
				return flag;
			}
		}

		/// <summary>This method is not meaningful for this type.</summary>
		/// <param name="selectAll">This parameter is ignored.</param>
		// Token: 0x06001EC6 RID: 7878 RVA: 0x000070A6 File Offset: 0x000052A6
		public virtual void PrepareEditingCellForEdit(bool selectAll)
		{
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x00090C1C File Offset: 0x0008EE1C
		// (set) Token: 0x06001EC8 RID: 7880 RVA: 0x00090C42 File Offset: 0x0008EE42
		private ButtonState ButtonState
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewCheckBoxCell.PropButtonCellState, out flag);
				if (flag)
				{
					return (ButtonState)integer;
				}
				return ButtonState.Normal;
			}
			set
			{
				if (this.ButtonState != value)
				{
					base.Properties.SetInteger(DataGridViewCheckBoxCell.PropButtonCellState, (int)value);
				}
			}
		}

		/// <summary>Gets the type of the cell's hosted editing control.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the underlying editing control.</returns>
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x00015C90 File Offset: 0x00013E90
		public override Type EditType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets the underlying value corresponding to a cell value of <see langword="false" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> corresponding to a cell value of <see langword="false" />. The default is <see langword="null" />.</returns>
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x00090C5E File Offset: 0x0008EE5E
		// (set) Token: 0x06001ECB RID: 7883 RVA: 0x00090C70 File Offset: 0x0008EE70
		[DefaultValue(null)]
		public object FalseValue
		{
			get
			{
				return base.Properties.GetObject(DataGridViewCheckBoxCell.PropFalseValue);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewCheckBoxCell.PropFalseValue))
				{
					base.Properties.SetObject(DataGridViewCheckBoxCell.PropFalseValue, value);
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

		// Token: 0x170006D3 RID: 1747
		// (set) Token: 0x06001ECC RID: 7884 RVA: 0x00090CD2 File Offset: 0x0008EED2
		internal object FalseValueInternal
		{
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewCheckBoxCell.PropFalseValue))
				{
					base.Properties.SetObject(DataGridViewCheckBoxCell.PropFalseValue, value);
				}
			}
		}

		/// <summary>Gets or sets the flat style appearance of the check box user interface (UI).</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.FlatStyle" /> value.</exception>
		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x00090CFC File Offset: 0x0008EEFC
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x00090D24 File Offset: 0x0008EF24
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewCheckBoxCell.PropFlatStyle, out flag);
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
					base.Properties.SetInteger(DataGridViewCheckBoxCell.PropFlatStyle, (int)value);
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (set) Token: 0x06001ECF RID: 7887 RVA: 0x00090D77 File Offset: 0x0008EF77
		internal FlatStyle FlatStyleInternal
		{
			set
			{
				if (value != this.FlatStyle)
				{
					base.Properties.SetInteger(DataGridViewCheckBoxCell.PropFlatStyle, (int)value);
				}
			}
		}

		/// <summary>Gets the type of the cell display value.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the display type of the cell.</returns>
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x00090D93 File Offset: 0x0008EF93
		public override Type FormattedValueType
		{
			get
			{
				if (this.ThreeState)
				{
					return DataGridViewCheckBoxCell.defaultCheckStateType;
				}
				return DataGridViewCheckBoxCell.defaultBooleanType;
			}
		}

		/// <summary>Gets or sets the underlying value corresponding to an indeterminate or <see langword="null" /> cell value.</summary>
		/// <returns>An <see cref="T:System.Object" /> corresponding to an indeterminate or <see langword="null" /> cell value. The default is <see langword="null" />.</returns>
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x00090DA8 File Offset: 0x0008EFA8
		// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x00090DBC File Offset: 0x0008EFBC
		[DefaultValue(null)]
		public object IndeterminateValue
		{
			get
			{
				return base.Properties.GetObject(DataGridViewCheckBoxCell.PropIndeterminateValue);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewCheckBoxCell.PropIndeterminateValue))
				{
					base.Properties.SetObject(DataGridViewCheckBoxCell.PropIndeterminateValue, value);
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

		// Token: 0x170006D8 RID: 1752
		// (set) Token: 0x06001ED3 RID: 7891 RVA: 0x00090E1E File Offset: 0x0008F01E
		internal object IndeterminateValueInternal
		{
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewCheckBoxCell.PropIndeterminateValue))
				{
					base.Properties.SetObject(DataGridViewCheckBoxCell.PropIndeterminateValue, value);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether ternary mode has been enabled for the hosted check box control.</summary>
		/// <returns>
		///   <see langword="true" /> if ternary mode is enabled; <see langword="false" /> if binary mode is enabled. The default is <see langword="false" />.</returns>
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x00090E46 File Offset: 0x0008F046
		// (set) Token: 0x06001ED5 RID: 7893 RVA: 0x00090E54 File Offset: 0x0008F054
		[DefaultValue(false)]
		public bool ThreeState
		{
			get
			{
				return (this.flags & 1) > 0;
			}
			set
			{
				if (this.ThreeState != value)
				{
					this.ThreeStateInternal = value;
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

		// Token: 0x170006DA RID: 1754
		// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x00090EA0 File Offset: 0x0008F0A0
		internal bool ThreeStateInternal
		{
			set
			{
				if (this.ThreeState != value)
				{
					if (value)
					{
						this.flags |= 1;
						return;
					}
					this.flags = (byte)((int)this.flags & -2);
				}
			}
		}

		/// <summary>Gets or sets the underlying value corresponding to a cell value of <see langword="true" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> corresponding to a cell value of <see langword="true" />. The default is <see langword="null" />.</returns>
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x00090ECE File Offset: 0x0008F0CE
		// (set) Token: 0x06001ED8 RID: 7896 RVA: 0x00090EE0 File Offset: 0x0008F0E0
		[DefaultValue(null)]
		public object TrueValue
		{
			get
			{
				return base.Properties.GetObject(DataGridViewCheckBoxCell.PropTrueValue);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewCheckBoxCell.PropTrueValue))
				{
					base.Properties.SetObject(DataGridViewCheckBoxCell.PropTrueValue, value);
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

		// Token: 0x170006DC RID: 1756
		// (set) Token: 0x06001ED9 RID: 7897 RVA: 0x00090F42 File Offset: 0x0008F142
		internal object TrueValueInternal
		{
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewCheckBoxCell.PropTrueValue))
				{
					base.Properties.SetObject(DataGridViewCheckBoxCell.PropTrueValue, value);
				}
			}
		}

		/// <summary>Gets the data type of the values in the cell.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the underlying value of the cell.</returns>
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x00090F6C File Offset: 0x0008F16C
		// (set) Token: 0x06001EDB RID: 7899 RVA: 0x00090F9E File Offset: 0x0008F19E
		public override Type ValueType
		{
			get
			{
				Type valueType = base.ValueType;
				if (valueType != null)
				{
					return valueType;
				}
				if (this.ThreeState)
				{
					return DataGridViewCheckBoxCell.defaultCheckStateType;
				}
				return DataGridViewCheckBoxCell.defaultBooleanType;
			}
			set
			{
				base.ValueType = value;
				this.ThreeState = value != null && DataGridViewCheckBoxCell.defaultCheckStateType.IsAssignableFrom(value);
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />.</returns>
		// Token: 0x06001EDC RID: 7900 RVA: 0x00090FC4 File Offset: 0x0008F1C4
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewCheckBoxCell dataGridViewCheckBoxCell;
			if (type == DataGridViewCheckBoxCell.cellType)
			{
				dataGridViewCheckBoxCell = new DataGridViewCheckBoxCell();
			}
			else
			{
				dataGridViewCheckBoxCell = (DataGridViewCheckBoxCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewCheckBoxCell);
			dataGridViewCheckBoxCell.ThreeStateInternal = this.ThreeState;
			dataGridViewCheckBoxCell.TrueValueInternal = this.TrueValue;
			dataGridViewCheckBoxCell.FalseValueInternal = this.FalseValue;
			dataGridViewCheckBoxCell.IndeterminateValueInternal = this.IndeterminateValue;
			dataGridViewCheckBoxCell.FlatStyleInternal = this.FlatStyle;
			return dataGridViewCheckBoxCell;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x00091040 File Offset: 0x0008F240
		private bool CommonContentClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			return currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == e.RowIndex && base.DataGridView.IsCurrentCellInEditMode;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the cell content is clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains data about the mouse click.</param>
		/// <returns>
		///   <see langword="true" /> if the cell is in edit mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001EDE RID: 7902 RVA: 0x00091084 File Offset: 0x0008F284
		protected override bool ContentClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return this.CommonContentClickUnsharesRow(e);
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the cell content is double-clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains data about the double-click.</param>
		/// <returns>
		///   <see langword="true" /> if the cell is in edit mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001EDF RID: 7903 RVA: 0x00091084 File Offset: 0x0008F284
		protected override bool ContentDoubleClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return this.CommonContentClickUnsharesRow(e);
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />.</returns>
		// Token: 0x06001EE0 RID: 7904 RVA: 0x0009108D File Offset: 0x0008F28D
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject(this);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> and cell style.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		// Token: 0x06001EE1 RID: 7905 RVA: 0x00091098 File Offset: 0x0008F298
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
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, null, null, cellStyle, dataGridViewAdvancedBorderStyle, DataGridViewPaintParts.ContentForeground, true, false, false);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		// Token: 0x06001EE2 RID: 7906 RVA: 0x000910F0 File Offset: 0x0008F2F0
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
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			if (currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex && base.DataGridView.IsCurrentCellInEditMode)
			{
				return Rectangle.Empty;
			}
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, null, this.GetErrorText(rowIndex), cellStyle, dataGridViewAdvancedBorderStyle, DataGridViewPaintParts.ContentForeground, false, true, false);
		}

		/// <summary>Gets the formatted value of the cell's data.</summary>
		/// <param name="value">The value to be formatted.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or <see langword="null" /> if no such custom conversion is needed.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or <see langword="null" /> if no such custom conversion is needed.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed.</param>
		/// <returns>The value of the cell's data after formatting has been applied or <see langword="null" /> if the cell is not part of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
		// Token: 0x06001EE3 RID: 7907 RVA: 0x000911A4 File Offset: 0x0008F3A4
		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if (value != null)
			{
				if (this.ThreeState)
				{
					if (value.Equals(this.TrueValue) || (value is int && (int)value == 1))
					{
						value = CheckState.Checked;
					}
					else if (value.Equals(this.FalseValue) || (value is int && (int)value == 0))
					{
						value = CheckState.Unchecked;
					}
					else if (value.Equals(this.IndeterminateValue) || (value is int && (int)value == 2))
					{
						value = CheckState.Indeterminate;
					}
				}
				else if (value.Equals(this.TrueValue) || (value is int && (int)value != 0))
				{
					value = true;
				}
				else if (value.Equals(this.FalseValue) || (value is int && (int)value == 0))
				{
					value = false;
				}
			}
			object formattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
			if (formattedValue != null && (context & DataGridViewDataErrorContexts.ClipboardContent) != (DataGridViewDataErrorContexts)0)
			{
				if (formattedValue is bool)
				{
					bool flag = (bool)formattedValue;
					if (flag)
					{
						return SR.GetString(this.ThreeState ? "DataGridViewCheckBoxCell_ClipboardChecked" : "DataGridViewCheckBoxCell_ClipboardTrue");
					}
					return SR.GetString(this.ThreeState ? "DataGridViewCheckBoxCell_ClipboardUnchecked" : "DataGridViewCheckBoxCell_ClipboardFalse");
				}
				else if (formattedValue is CheckState)
				{
					CheckState checkState = (CheckState)formattedValue;
					if (checkState == CheckState.Checked)
					{
						return SR.GetString(this.ThreeState ? "DataGridViewCheckBoxCell_ClipboardChecked" : "DataGridViewCheckBoxCell_ClipboardTrue");
					}
					if (checkState == CheckState.Unchecked)
					{
						return SR.GetString(this.ThreeState ? "DataGridViewCheckBoxCell_ClipboardUnchecked" : "DataGridViewCheckBoxCell_ClipboardFalse");
					}
					return SR.GetString("DataGridViewCheckBoxCell_ClipboardIndeterminate");
				}
			}
			return formattedValue;
		}

		/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		// Token: 0x06001EE4 RID: 7908 RVA: 0x0009134C File Offset: 0x0008F54C
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
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			Rectangle stdBorderWidths = base.StdBorderWidths;
			int num = stdBorderWidths.Left + stdBorderWidths.Width + cellStyle.Padding.Horizontal;
			int num2 = stdBorderWidths.Top + stdBorderWidths.Height + cellStyle.Padding.Vertical;
			Size size;
			if (base.DataGridView.ApplyVisualStylesToInnerCells)
			{
				Size glyphSize = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal);
				switch (this.FlatStyle)
				{
				case FlatStyle.Flat:
					glyphSize.Width -= 3;
					glyphSize.Height -= 3;
					break;
				case FlatStyle.Popup:
					glyphSize.Width -= 2;
					glyphSize.Height -= 2;
					break;
				}
				if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
				{
					if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
					{
						size = new Size(glyphSize.Width + num + 4, 0);
					}
					else
					{
						size = new Size(glyphSize.Width + num + 4, glyphSize.Height + num2 + 4);
					}
				}
				else
				{
					size = new Size(0, glyphSize.Height + num2 + 4);
				}
			}
			else
			{
				FlatStyle flatStyle = this.FlatStyle;
				int num3;
				if (flatStyle != FlatStyle.Flat)
				{
					if (flatStyle != FlatStyle.Popup)
					{
						num3 = SystemInformation.Border3DSize.Width * 2 + 9 + 4;
					}
					else
					{
						num3 = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal).Width - 2;
					}
				}
				else
				{
					num3 = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal).Width - 3;
				}
				if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
				{
					if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
					{
						size = new Size(num3 + num, 0);
					}
					else
					{
						size = new Size(num3 + num, num3 + num2);
					}
				}
				else
				{
					size = new Size(0, num3 + num2);
				}
			}
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			Rectangle rectangle2 = this.BorderWidths(dataGridViewAdvancedBorderStyle);
			size.Width += rectangle2.X;
			size.Height += rectangle2.Y;
			if (base.DataGridView.ShowCellErrors)
			{
				if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
				{
					size.Width = Math.Max(size.Width, num + 8 + (int)DataGridViewCell.iconsWidth);
				}
				if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
				{
					size.Height = Math.Max(size.Height, num2 + 8 + (int)DataGridViewCell.iconsHeight);
				}
			}
			return size;
		}

		/// <summary>Indicates whether the row containing the cell is unshared when a key is pressed while the cell has focus.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains data about the key press.</param>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		/// <returns>
		///   <see langword="true" /> if the SPACE key is pressed and the CTRL, ALT, and SHIFT keys are all not pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001EE5 RID: 7909 RVA: 0x000881F4 File Offset: 0x000863F4
		protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyCode == Keys.Space && !e.Alt && !e.Control && !e.Shift;
		}

		/// <summary>Indicates whether the row containing the cell is unshared when a key is released while the cell has focus.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains data about the key press.</param>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		/// <returns>
		///   <see langword="true" /> if the SPACE key is released; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001EE6 RID: 7910 RVA: 0x0008821B File Offset: 0x0008641B
		protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyCode == Keys.Space;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is pressed while the pointer is over the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
		/// <returns>Always <see langword="true" />.</returns>
		// Token: 0x06001EE7 RID: 7911 RVA: 0x00088227 File Offset: 0x00086427
		protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer moves over the cell.</summary>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		/// <returns>
		///   <see langword="true" /> if the cell was the last cell receiving a mouse click; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001EE8 RID: 7912 RVA: 0x000915B4 File Offset: 0x0008F7B4
		protected override bool MouseEnterUnsharesRow(int rowIndex)
		{
			return base.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && rowIndex == base.DataGridView.MouseDownCellAddress.Y;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer leaves the cell.</summary>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		/// <returns>
		///   <see langword="true" /> if the button is not in the normal state; <see langword="false" /> if the button is in the pressed state.</returns>
		// Token: 0x06001EE9 RID: 7913 RVA: 0x000915F4 File Offset: 0x0008F7F4
		protected override bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return (this.ButtonState & ButtonState.Pushed) > ButtonState.Normal;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is released while the pointer is over the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
		/// <returns>Always <see langword="true" />.</returns>
		// Token: 0x06001EEA RID: 7914 RVA: 0x00088227 File Offset: 0x00086427
		protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left;
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x00091605 File Offset: 0x0008F805
		private void NotifyDataGridViewOfValueChange()
		{
			this.flags |= 2;
			base.DataGridView.NotifyCurrentCellDirty(true);
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00091624 File Offset: 0x0008F824
		private void OnCommonContentClick(DataGridViewCellEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			if (currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == e.RowIndex && base.DataGridView.IsCurrentCellInEditMode && this.SwitchFormattedValue())
			{
				this.NotifyDataGridViewOfValueChange();
			}
		}

		/// <summary>Called when the cell's contents are clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data.</param>
		// Token: 0x06001EED RID: 7917 RVA: 0x0009167F File Offset: 0x0008F87F
		protected override void OnContentClick(DataGridViewCellEventArgs e)
		{
			this.OnCommonContentClick(e);
		}

		/// <summary>Called when the cell's contents are double-clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data.</param>
		// Token: 0x06001EEE RID: 7918 RVA: 0x0009167F File Offset: 0x0008F87F
		protected override void OnContentDoubleClick(DataGridViewCellEventArgs e)
		{
			this.OnCommonContentClick(e);
		}

		/// <summary>Called when a character key is pressed while the focus is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data</param>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x06001EEF RID: 7919 RVA: 0x00091688 File Offset: 0x0008F888
		protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.KeyCode == Keys.Space && !e.Alt && !e.Control && !e.Shift)
			{
				this.UpdateButtonState(this.ButtonState | ButtonState.Checked, rowIndex);
				e.Handled = true;
			}
		}

		/// <summary>Called when a character key is released while the focus is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data</param>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x06001EF0 RID: 7920 RVA: 0x000916DC File Offset: 0x0008F8DC
		protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.KeyCode == Keys.Space)
			{
				this.UpdateButtonState(this.ButtonState & ~ButtonState.Checked, rowIndex);
				if (!e.Alt && !e.Control && !e.Shift)
				{
					base.RaiseCellClick(new DataGridViewCellEventArgs(base.ColumnIndex, rowIndex));
					if (base.DataGridView != null && base.ColumnIndex < base.DataGridView.Columns.Count && rowIndex < base.DataGridView.Rows.Count)
					{
						base.RaiseCellContentClick(new DataGridViewCellEventArgs(base.ColumnIndex, rowIndex));
					}
					e.Handled = true;
				}
				this.NotifyMASSClient(new Point(base.ColumnIndex, rowIndex));
			}
		}

		/// <summary>Called when the focus moves from a cell.</summary>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		/// <param name="throughMouseClick">
		///   <see langword="true" /> if the cell was left as a result of user mouse click rather than a programmatic cell change; otherwise, <see langword="false" />.</param>
		// Token: 0x06001EF1 RID: 7921 RVA: 0x0009179A File Offset: 0x0008F99A
		protected override void OnLeave(int rowIndex, bool throughMouseClick)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (this.ButtonState != ButtonState.Normal)
			{
				this.UpdateButtonState(ButtonState.Normal, rowIndex);
			}
		}

		/// <summary>Called when the mouse button is held down while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001EF2 RID: 7922 RVA: 0x000917B5 File Offset: 0x0008F9B5
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Left && DataGridViewCheckBoxCell.mouseInContentBounds)
			{
				this.UpdateButtonState(this.ButtonState | ButtonState.Pushed, e.RowIndex);
			}
		}

		/// <summary>Called when the mouse pointer moves from a cell.</summary>
		/// <param name="rowIndex">The row index of the current cell or -1 if the cell is not owned by a row.</param>
		// Token: 0x06001EF3 RID: 7923 RVA: 0x000917EC File Offset: 0x0008F9EC
		protected override void OnMouseLeave(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (DataGridViewCheckBoxCell.mouseInContentBounds)
			{
				DataGridViewCheckBoxCell.mouseInContentBounds = false;
				if (base.ColumnIndex >= 0 && rowIndex >= 0 && (base.DataGridView.ApplyVisualStylesToInnerCells || this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup))
				{
					base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
				}
			}
			if ((this.ButtonState & ButtonState.Pushed) != ButtonState.Normal && base.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && rowIndex == base.DataGridView.MouseDownCellAddress.Y)
			{
				this.UpdateButtonState(this.ButtonState & ~ButtonState.Pushed, rowIndex);
			}
		}

		/// <summary>Called when the mouse pointer moves within a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001EF4 RID: 7924 RVA: 0x000918A0 File Offset: 0x0008FAA0
		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			bool flag = DataGridViewCheckBoxCell.mouseInContentBounds;
			DataGridViewCheckBoxCell.mouseInContentBounds = base.GetContentBounds(e.RowIndex).Contains(e.X, e.Y);
			if (flag != DataGridViewCheckBoxCell.mouseInContentBounds)
			{
				if (base.DataGridView.ApplyVisualStylesToInnerCells || this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup)
				{
					base.DataGridView.InvalidateCell(base.ColumnIndex, e.RowIndex);
				}
				if (e.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && e.RowIndex == base.DataGridView.MouseDownCellAddress.Y && Control.MouseButtons == MouseButtons.Left)
				{
					if ((this.ButtonState & ButtonState.Pushed) == ButtonState.Normal && DataGridViewCheckBoxCell.mouseInContentBounds && base.DataGridView.CellMouseDownInContentBounds)
					{
						this.UpdateButtonState(this.ButtonState | ButtonState.Pushed, e.RowIndex);
					}
					else if ((this.ButtonState & ButtonState.Pushed) != ButtonState.Normal && !DataGridViewCheckBoxCell.mouseInContentBounds)
					{
						this.UpdateButtonState(this.ButtonState & ~ButtonState.Pushed, e.RowIndex);
					}
				}
			}
			base.OnMouseMove(e);
		}

		/// <summary>Called when the mouse button is released while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001EF5 RID: 7925 RVA: 0x000919D8 File Offset: 0x0008FBD8
		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Left)
			{
				this.UpdateButtonState(this.ButtonState & ~ButtonState.Pushed, e.RowIndex);
				this.NotifyMASSClient(new Point(e.ColumnIndex, e.RowIndex));
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00091A2C File Offset: 0x0008FC2C
		private void NotifyMASSClient(Point position)
		{
			int rowCount = base.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible, 0, position.Y);
			int num = base.DataGridView.Columns.ColumnIndexToActualDisplayIndex(position.X, DataGridViewElementStates.Visible);
			int num2 = (base.DataGridView.ColumnHeadersVisible ? 1 : 0);
			int num3 = (base.DataGridView.RowHeadersVisible ? 1 : 0);
			int num4 = rowCount + num2 + 1;
			int num5 = num + num3;
			(base.DataGridView.AccessibilityObject as Control.ControlAccessibleObject).NotifyClients(AccessibleEvents.StateChange, num4, num5);
		}

		/// <summary>Paints the current <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />.</summary>
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
		// Token: 0x06001EF7 RID: 7927 RVA: 0x00091ABC File Offset: 0x0008FCBC
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, elementState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00091AF4 File Offset: 0x0008FCF4
		private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle = cellBounds;
			Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
			rectangle.Offset(rectangle2.X, rectangle2.Y);
			rectangle.Width -= rectangle2.Right;
			rectangle.Height -= rectangle2.Bottom;
			bool flag = (elementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			bool flag2 = false;
			bool flag3 = true;
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			if (currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex && base.DataGridView.IsCurrentCellInEditMode)
			{
				flag3 = false;
			}
			CheckState checkState;
			ButtonState buttonState;
			if (formattedValue != null && formattedValue is CheckState)
			{
				checkState = (CheckState)formattedValue;
				buttonState = ((checkState == CheckState.Unchecked) ? ButtonState.Normal : ButtonState.Checked);
				flag2 = checkState == CheckState.Indeterminate;
			}
			else if (formattedValue != null && formattedValue is bool)
			{
				if ((bool)formattedValue)
				{
					checkState = CheckState.Checked;
					buttonState = ButtonState.Checked;
				}
				else
				{
					checkState = CheckState.Unchecked;
					buttonState = ButtonState.Normal;
				}
			}
			else
			{
				buttonState = ButtonState.Normal;
				checkState = CheckState.Unchecked;
			}
			if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal)
			{
				buttonState |= ButtonState.Pushed;
			}
			SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
			if (paint && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
			{
				g.FillRectangle(cachedBrush, rectangle);
			}
			if (cellStyle.Padding != Padding.Empty)
			{
				if (base.DataGridView.RightToLeftInternal)
				{
					rectangle.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
				}
				else
				{
					rectangle.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
				}
				rectangle.Width -= cellStyle.Padding.Horizontal;
				rectangle.Height -= cellStyle.Padding.Vertical;
			}
			if (paint && DataGridViewCell.PaintFocus(paintParts) && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle, Color.Empty, cachedBrush.Color);
			}
			Rectangle rectangle3 = rectangle;
			rectangle.Inflate(-2, -2);
			CheckBoxState checkBoxState = CheckBoxState.UncheckedNormal;
			Size size;
			if (base.DataGridView.ApplyVisualStylesToInnerCells)
			{
				checkBoxState = CheckBoxRenderer.ConvertFromButtonState(buttonState, flag2, base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex && DataGridViewCheckBoxCell.mouseInContentBounds);
				size = CheckBoxRenderer.GetGlyphSize(g, checkBoxState);
				switch (this.FlatStyle)
				{
				case FlatStyle.Flat:
					size.Width -= 3;
					size.Height -= 3;
					break;
				case FlatStyle.Popup:
					size.Width -= 2;
					size.Height -= 2;
					break;
				}
			}
			else
			{
				FlatStyle flatStyle = this.FlatStyle;
				if (flatStyle != FlatStyle.Flat)
				{
					if (flatStyle != FlatStyle.Popup)
					{
						size = new Size(SystemInformation.Border3DSize.Width * 2 + 9, SystemInformation.Border3DSize.Width * 2 + 9);
					}
					else
					{
						size = CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal);
						size.Width -= 2;
						size.Height -= 2;
					}
				}
				else
				{
					size = CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal);
					size.Width -= 3;
					size.Height -= 3;
				}
			}
			Rectangle rectangle4;
			if (rectangle.Width >= size.Width && rectangle.Height >= size.Height && (paint || computeContentBounds))
			{
				int num = 0;
				int num2 = 0;
				if ((!base.DataGridView.RightToLeftInternal && (cellStyle.Alignment & DataGridViewCheckBoxCell.anyRight) != DataGridViewContentAlignment.NotSet) || (base.DataGridView.RightToLeftInternal && (cellStyle.Alignment & DataGridViewCheckBoxCell.anyLeft) != DataGridViewContentAlignment.NotSet))
				{
					num = rectangle.Right - size.Width;
				}
				else if ((cellStyle.Alignment & DataGridViewCheckBoxCell.anyCenter) != DataGridViewContentAlignment.NotSet)
				{
					num = rectangle.Left + (rectangle.Width - size.Width) / 2;
				}
				else
				{
					num = rectangle.Left;
				}
				if ((cellStyle.Alignment & DataGridViewCheckBoxCell.anyBottom) != DataGridViewContentAlignment.NotSet)
				{
					num2 = rectangle.Bottom - size.Height;
				}
				else if ((cellStyle.Alignment & DataGridViewCheckBoxCell.anyMiddle) != DataGridViewContentAlignment.NotSet)
				{
					num2 = rectangle.Top + (rectangle.Height - size.Height) / 2;
				}
				else
				{
					num2 = rectangle.Top;
				}
				if (base.DataGridView.ApplyVisualStylesToInnerCells && this.FlatStyle != FlatStyle.Flat && this.FlatStyle != FlatStyle.Popup)
				{
					if (paint && DataGridViewCell.PaintContentForeground(paintParts))
					{
						DataGridViewCheckBoxCell.DataGridViewCheckBoxCellRenderer.DrawCheckBox(g, new Rectangle(num, num2, size.Width, size.Height), (int)checkBoxState);
					}
					rectangle4 = new Rectangle(num, num2, size.Width, size.Height);
				}
				else if (this.FlatStyle == FlatStyle.System || this.FlatStyle == FlatStyle.Standard)
				{
					if (paint && DataGridViewCell.PaintContentForeground(paintParts))
					{
						if (flag2)
						{
							ControlPaint.DrawMixedCheckBox(g, num, num2, size.Width, size.Height, buttonState);
						}
						else
						{
							ControlPaint.DrawCheckBox(g, num, num2, size.Width, size.Height, buttonState);
						}
					}
					rectangle4 = new Rectangle(num, num2, size.Width, size.Height);
				}
				else if (this.FlatStyle == FlatStyle.Flat)
				{
					Rectangle rectangle5 = new Rectangle(num, num2, size.Width, size.Height);
					SolidBrush solidBrush = null;
					SolidBrush solidBrush2 = null;
					Color color = Color.Empty;
					if (paint && DataGridViewCell.PaintContentForeground(paintParts))
					{
						solidBrush = base.DataGridView.GetCachedBrush(flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
						solidBrush2 = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
						color = ControlPaint.LightLight(solidBrush2.Color);
						if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex && DataGridViewCheckBoxCell.mouseInContentBounds)
						{
							float num3 = 0.9f;
							if ((double)color.GetBrightness() < 0.5)
							{
								num3 = 1.2f;
							}
							color = Color.FromArgb(ButtonBaseAdapter.ColorOptions.Adjust255(num3, (int)color.R), ButtonBaseAdapter.ColorOptions.Adjust255(num3, (int)color.G), ButtonBaseAdapter.ColorOptions.Adjust255(num3, (int)color.B));
						}
						color = g.GetNearestColor(color);
						using (Pen pen = new Pen(solidBrush.Color))
						{
							g.DrawLine(pen, rectangle5.Left, rectangle5.Top, rectangle5.Right - 1, rectangle5.Top);
							g.DrawLine(pen, rectangle5.Left, rectangle5.Top, rectangle5.Left, rectangle5.Bottom - 1);
						}
					}
					rectangle5.Inflate(-1, -1);
					int num4 = rectangle5.Width;
					rectangle5.Width = num4 + 1;
					num4 = rectangle5.Height;
					rectangle5.Height = num4 + 1;
					if (paint && DataGridViewCell.PaintContentForeground(paintParts))
					{
						if (checkState == CheckState.Indeterminate)
						{
							ButtonBaseAdapter.DrawDitheredFill(g, solidBrush2.Color, color, rectangle5);
						}
						else
						{
							using (SolidBrush solidBrush3 = new SolidBrush(color))
							{
								g.FillRectangle(solidBrush3, rectangle5);
							}
						}
						if (checkState != CheckState.Unchecked)
						{
							Rectangle rectangle6 = new Rectangle(num - 1, num2 - 1, size.Width + 3, size.Height + 3);
							num4 = rectangle6.Width;
							rectangle6.Width = num4 + 1;
							num4 = rectangle6.Height;
							rectangle6.Height = num4 + 1;
							if (DataGridViewCheckBoxCell.checkImage == null || DataGridViewCheckBoxCell.checkImage.Width != rectangle6.Width || DataGridViewCheckBoxCell.checkImage.Height != rectangle6.Height)
							{
								if (DataGridViewCheckBoxCell.checkImage != null)
								{
									DataGridViewCheckBoxCell.checkImage.Dispose();
									DataGridViewCheckBoxCell.checkImage = null;
								}
								NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(0, 0, rectangle6.Width, rectangle6.Height);
								Bitmap bitmap = new Bitmap(rectangle6.Width, rectangle6.Height);
								using (Graphics graphics = Graphics.FromImage(bitmap))
								{
									graphics.Clear(Color.Transparent);
									IntPtr hdc = graphics.GetHdc();
									try
									{
										SafeNativeMethods.DrawFrameControl(new HandleRef(graphics, hdc), ref rect, 2, 1);
									}
									finally
									{
										graphics.ReleaseHdcInternal(hdc);
									}
								}
								bitmap.MakeTransparent();
								DataGridViewCheckBoxCell.checkImage = bitmap;
							}
							num4 = rectangle6.Y;
							rectangle6.Y = num4 - 1;
							ControlPaint.DrawImageColorized(g, DataGridViewCheckBoxCell.checkImage, rectangle6, (checkState == CheckState.Indeterminate) ? ControlPaint.LightLight(solidBrush.Color) : solidBrush.Color);
						}
					}
					rectangle4 = rectangle5;
				}
				else
				{
					Rectangle rectangle7 = new Rectangle(num, num2, size.Width - 1, size.Height - 1);
					rectangle7.Y -= 3;
					if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal)
					{
						ButtonBaseAdapter.LayoutOptions layoutOptions = CheckBoxPopupAdapter.PaintPopupLayout(g, true, size.Width, rectangle7, Padding.Empty, false, cellStyle.Font, string.Empty, base.DataGridView.Enabled, DataGridViewUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.DataGridView.RightToLeft, null);
						layoutOptions.everettButtonCompat = false;
						ButtonBaseAdapter.LayoutData layoutData = layoutOptions.Layout();
						if (paint && DataGridViewCell.PaintContentForeground(paintParts))
						{
							ButtonBaseAdapter.ColorData colorData = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
							CheckBoxBaseAdapter.DrawCheckBackground(base.DataGridView.Enabled, checkState, g, layoutData.checkBounds, colorData.windowText, colorData.buttonFace, true, colorData);
							CheckBoxBaseAdapter.DrawPopupBorder(g, layoutData.checkBounds, colorData);
							CheckBoxBaseAdapter.DrawCheckOnly(size.Width, checkState == CheckState.Checked || checkState == CheckState.Indeterminate, base.DataGridView.Enabled, checkState, g, layoutData, colorData, colorData.windowText, colorData.buttonFace);
						}
						rectangle4 = layoutData.checkBounds;
					}
					else if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex && DataGridViewCheckBoxCell.mouseInContentBounds)
					{
						ButtonBaseAdapter.LayoutOptions layoutOptions2 = CheckBoxPopupAdapter.PaintPopupLayout(g, true, size.Width, rectangle7, Padding.Empty, false, cellStyle.Font, string.Empty, base.DataGridView.Enabled, DataGridViewUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.DataGridView.RightToLeft, null);
						layoutOptions2.everettButtonCompat = false;
						ButtonBaseAdapter.LayoutData layoutData2 = layoutOptions2.Layout();
						if (paint && DataGridViewCell.PaintContentForeground(paintParts))
						{
							ButtonBaseAdapter.ColorData colorData2 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
							CheckBoxBaseAdapter.DrawCheckBackground(base.DataGridView.Enabled, checkState, g, layoutData2.checkBounds, colorData2.windowText, colorData2.options.highContrast ? colorData2.buttonFace : colorData2.highlight, true, colorData2);
							CheckBoxBaseAdapter.DrawPopupBorder(g, layoutData2.checkBounds, colorData2);
							CheckBoxBaseAdapter.DrawCheckOnly(size.Width, checkState == CheckState.Checked || checkState == CheckState.Indeterminate, base.DataGridView.Enabled, checkState, g, layoutData2, colorData2, colorData2.windowText, colorData2.highlight);
						}
						rectangle4 = layoutData2.checkBounds;
					}
					else
					{
						ButtonBaseAdapter.LayoutOptions layoutOptions3 = CheckBoxPopupAdapter.PaintPopupLayout(g, false, size.Width, rectangle7, Padding.Empty, false, cellStyle.Font, string.Empty, base.DataGridView.Enabled, DataGridViewUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.DataGridView.RightToLeft, null);
						layoutOptions3.everettButtonCompat = false;
						ButtonBaseAdapter.LayoutData layoutData3 = layoutOptions3.Layout();
						if (paint && DataGridViewCell.PaintContentForeground(paintParts))
						{
							ButtonBaseAdapter.ColorData colorData3 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
							CheckBoxBaseAdapter.DrawCheckBackground(base.DataGridView.Enabled, checkState, g, layoutData3.checkBounds, colorData3.windowText, colorData3.options.highContrast ? colorData3.buttonFace : colorData3.highlight, true, colorData3);
							ButtonBaseAdapter.DrawFlatBorder(g, layoutData3.checkBounds, colorData3.buttonShadow);
							CheckBoxBaseAdapter.DrawCheckOnly(size.Width, checkState == CheckState.Checked || checkState == CheckState.Indeterminate, base.DataGridView.Enabled, checkState, g, layoutData3, colorData3, colorData3.windowText, colorData3.highlight);
						}
						rectangle4 = layoutData3.checkBounds;
					}
				}
			}
			else if (computeErrorIconBounds)
			{
				if (!string.IsNullOrEmpty(errorText))
				{
					rectangle4 = base.ComputeErrorIconBounds(rectangle3);
				}
				else
				{
					rectangle4 = Rectangle.Empty;
				}
			}
			else
			{
				rectangle4 = Rectangle.Empty;
			}
			if (paint && DataGridViewCell.PaintErrorIcon(paintParts) && flag3 && base.DataGridView.ShowCellErrors)
			{
				base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, rectangle3, errorText);
			}
			return rectangle4;
		}

		/// <summary>Converts a value formatted for display to an actual cell value.</summary>
		/// <param name="formattedValue">The display value of the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the display value type, or <see langword="null" /> to use the default converter.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the cell value type, or <see langword="null" /> to use the default converter.</param>
		/// <returns>The cell value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cellStyle" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="formattedValue" /> is <see langword="null" />.  
		/// -or-
		///  The type of <paramref name="formattedValue" /> does not match the type indicated by the <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property.</exception>
		// Token: 0x06001EF9 RID: 7929 RVA: 0x000928B4 File Offset: 0x00090AB4
		public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			if (formattedValue != null)
			{
				if (formattedValue is bool)
				{
					if ((bool)formattedValue)
					{
						if (this.TrueValue != null)
						{
							return this.TrueValue;
						}
						if (this.ValueType != null && this.ValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultBooleanType))
						{
							return true;
						}
						if (this.ValueType != null && this.ValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultCheckStateType))
						{
							return CheckState.Checked;
						}
					}
					else
					{
						if (this.FalseValue != null)
						{
							return this.FalseValue;
						}
						if (this.ValueType != null && this.ValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultBooleanType))
						{
							return false;
						}
						if (this.ValueType != null && this.ValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultCheckStateType))
						{
							return CheckState.Unchecked;
						}
					}
				}
				else if (formattedValue is CheckState)
				{
					switch ((CheckState)formattedValue)
					{
					case CheckState.Unchecked:
						if (this.FalseValue != null)
						{
							return this.FalseValue;
						}
						if (this.ValueType != null && this.ValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultBooleanType))
						{
							return false;
						}
						if (this.ValueType != null && this.ValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultCheckStateType))
						{
							return CheckState.Unchecked;
						}
						break;
					case CheckState.Checked:
						if (this.TrueValue != null)
						{
							return this.TrueValue;
						}
						if (this.ValueType != null && this.ValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultBooleanType))
						{
							return true;
						}
						if (this.ValueType != null && this.ValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultCheckStateType))
						{
							return CheckState.Checked;
						}
						break;
					case CheckState.Indeterminate:
						if (this.IndeterminateValue != null)
						{
							return this.IndeterminateValue;
						}
						if (this.ValueType != null && this.ValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultCheckStateType))
						{
							return CheckState.Indeterminate;
						}
						break;
					}
				}
			}
			return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00092ACC File Offset: 0x00090CCC
		private bool SwitchFormattedValue()
		{
			if (this.FormattedValueType == null)
			{
				return false;
			}
			if (this.FormattedValueType.IsAssignableFrom(typeof(CheckState)))
			{
				if ((this.flags & 16) != 0)
				{
					((IDataGridViewEditingCell)this).EditingCellFormattedValue = CheckState.Indeterminate;
				}
				else if ((this.flags & 32) != 0)
				{
					((IDataGridViewEditingCell)this).EditingCellFormattedValue = CheckState.Unchecked;
				}
				else
				{
					((IDataGridViewEditingCell)this).EditingCellFormattedValue = CheckState.Checked;
				}
			}
			else if (this.FormattedValueType.IsAssignableFrom(DataGridViewCheckBoxCell.defaultBooleanType))
			{
				((IDataGridViewEditingCell)this).EditingCellFormattedValue = !(bool)((IDataGridViewEditingCell)this).GetEditingCellFormattedValue(DataGridViewDataErrorContexts.Formatting);
			}
			return true;
		}

		/// <summary>Returns the string representation of the cell.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current cell.</returns>
		// Token: 0x06001EFB RID: 7931 RVA: 0x00092B70 File Offset: 0x00090D70
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewCheckBoxCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00092BCC File Offset: 0x00090DCC
		private void UpdateButtonState(ButtonState newButtonState, int rowIndex)
		{
			this.ButtonState = newButtonState;
			base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
		}

		// Token: 0x04000D03 RID: 3331
		private static readonly DataGridViewContentAlignment anyLeft = (DataGridViewContentAlignment)273;

		// Token: 0x04000D04 RID: 3332
		private static readonly DataGridViewContentAlignment anyRight = (DataGridViewContentAlignment)1092;

		// Token: 0x04000D05 RID: 3333
		private static readonly DataGridViewContentAlignment anyCenter = (DataGridViewContentAlignment)546;

		// Token: 0x04000D06 RID: 3334
		private static readonly DataGridViewContentAlignment anyBottom = (DataGridViewContentAlignment)1792;

		// Token: 0x04000D07 RID: 3335
		private static readonly DataGridViewContentAlignment anyMiddle = (DataGridViewContentAlignment)112;

		// Token: 0x04000D08 RID: 3336
		private static readonly VisualStyleElement CheckBoxElement = VisualStyleElement.Button.CheckBox.UncheckedNormal;

		// Token: 0x04000D09 RID: 3337
		private static readonly int PropButtonCellState = PropertyStore.CreateKey();

		// Token: 0x04000D0A RID: 3338
		private static readonly int PropTrueValue = PropertyStore.CreateKey();

		// Token: 0x04000D0B RID: 3339
		private static readonly int PropFalseValue = PropertyStore.CreateKey();

		// Token: 0x04000D0C RID: 3340
		private static readonly int PropFlatStyle = PropertyStore.CreateKey();

		// Token: 0x04000D0D RID: 3341
		private static readonly int PropIndeterminateValue = PropertyStore.CreateKey();

		// Token: 0x04000D0E RID: 3342
		private static Bitmap checkImage = null;

		// Token: 0x04000D0F RID: 3343
		private const byte DATAGRIDVIEWCHECKBOXCELL_threeState = 1;

		// Token: 0x04000D10 RID: 3344
		private const byte DATAGRIDVIEWCHECKBOXCELL_valueChanged = 2;

		// Token: 0x04000D11 RID: 3345
		private const byte DATAGRIDVIEWCHECKBOXCELL_checked = 16;

		// Token: 0x04000D12 RID: 3346
		private const byte DATAGRIDVIEWCHECKBOXCELL_indeterminate = 32;

		// Token: 0x04000D13 RID: 3347
		private const byte DATAGRIDVIEWCHECKBOXCELL_margin = 2;

		// Token: 0x04000D14 RID: 3348
		private byte flags;

		// Token: 0x04000D15 RID: 3349
		private static bool mouseInContentBounds = false;

		// Token: 0x04000D16 RID: 3350
		private static Type defaultCheckStateType = typeof(CheckState);

		// Token: 0x04000D17 RID: 3351
		private static Type defaultBooleanType = typeof(bool);

		// Token: 0x04000D18 RID: 3352
		private static Type cellType = typeof(DataGridViewCheckBoxCell);

		// Token: 0x02000667 RID: 1639
		private class DataGridViewCheckBoxCellRenderer
		{
			// Token: 0x0600660A RID: 26122 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewCheckBoxCellRenderer()
			{
			}

			// Token: 0x1700162B RID: 5675
			// (get) Token: 0x0600660B RID: 26123 RVA: 0x0017CF5A File Offset: 0x0017B15A
			public static VisualStyleRenderer CheckBoxRenderer
			{
				get
				{
					if (DataGridViewCheckBoxCell.DataGridViewCheckBoxCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewCheckBoxCell.DataGridViewCheckBoxCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewCheckBoxCell.CheckBoxElement);
					}
					return DataGridViewCheckBoxCell.DataGridViewCheckBoxCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x0600660C RID: 26124 RVA: 0x0017CF77 File Offset: 0x0017B177
			public static void DrawCheckBox(Graphics g, Rectangle bounds, int state)
			{
				DataGridViewCheckBoxCell.DataGridViewCheckBoxCellRenderer.CheckBoxRenderer.SetParameters(DataGridViewCheckBoxCell.CheckBoxElement.ClassName, DataGridViewCheckBoxCell.CheckBoxElement.Part, state);
				DataGridViewCheckBoxCell.DataGridViewCheckBoxCellRenderer.CheckBoxRenderer.DrawBackground(g, bounds, Rectangle.Truncate(g.ClipBounds));
			}

			// Token: 0x04003A5B RID: 14939
			private static VisualStyleRenderer visualStyleRenderer;
		}

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> to accessibility client applications.</summary>
		// Token: 0x02000668 RID: 1640
		protected class DataGridViewCheckBoxCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</param>
			// Token: 0x0600660D RID: 26125 RVA: 0x0017BC46 File Offset: 0x00179E46
			public DataGridViewCheckBoxCellAccessibleObject(DataGridViewCell owner)
				: base(owner)
			{
			}

			/// <summary>Gets the state of this <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values.</returns>
			// Token: 0x1700162C RID: 5676
			// (get) Token: 0x0600660E RID: 26126 RVA: 0x0017CFB0 File Offset: 0x0017B1B0
			public override AccessibleStates State
			{
				get
				{
					if (((DataGridViewCheckBoxCell)base.Owner).EditedFormattedValue is CheckState)
					{
						CheckState checkState = (CheckState)((DataGridViewCheckBoxCell)base.Owner).EditedFormattedValue;
						if (checkState == CheckState.Checked)
						{
							return AccessibleStates.Checked | base.State;
						}
						if (checkState == CheckState.Indeterminate)
						{
							return AccessibleStates.Mixed | base.State;
						}
					}
					else if (((DataGridViewCheckBoxCell)base.Owner).EditedFormattedValue is bool)
					{
						bool flag = (bool)((DataGridViewCheckBoxCell)base.Owner).EditedFormattedValue;
						if (flag)
						{
							return AccessibleStates.Checked | base.State;
						}
					}
					return base.State;
				}
			}

			/// <summary>Gets a string that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</summary>
			/// <returns>A description of the default action.</returns>
			// Token: 0x1700162D RID: 5677
			// (get) Token: 0x0600660F RID: 26127 RVA: 0x0017D048 File Offset: 0x0017B248
			public override string DefaultAction
			{
				get
				{
					if (base.Owner.ReadOnly)
					{
						return string.Empty;
					}
					bool flag = true;
					object formattedValue = base.Owner.FormattedValue;
					if (formattedValue is CheckState)
					{
						flag = (CheckState)formattedValue == CheckState.Unchecked;
					}
					else if (formattedValue is bool)
					{
						flag = !(bool)formattedValue;
					}
					if (flag)
					{
						return SR.GetString("DataGridView_AccCheckBoxCellDefaultActionCheck");
					}
					return SR.GetString("DataGridView_AccCheckBoxCellDefaultActionUncheck");
				}
			}

			/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property does not belong to a <see langword="DataGridView" /> control.  
			///  -or-  
			///  The <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property belongs to a shared row.</exception>
			// Token: 0x06006610 RID: 26128 RVA: 0x0017D0B4 File Offset: 0x0017B2B4
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				DataGridViewCheckBoxCell dataGridViewCheckBoxCell = (DataGridViewCheckBoxCell)base.Owner;
				DataGridView dataGridView = dataGridViewCheckBoxCell.DataGridView;
				if (dataGridView != null && dataGridViewCheckBoxCell.RowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
				}
				if (!dataGridViewCheckBoxCell.ReadOnly && dataGridViewCheckBoxCell.OwningColumn != null && dataGridViewCheckBoxCell.OwningRow != null)
				{
					dataGridView.CurrentCell = dataGridViewCheckBoxCell;
					bool flag = false;
					if (!dataGridView.IsCurrentCellInEditMode)
					{
						flag = true;
						dataGridView.BeginEdit(false);
					}
					if (dataGridView.IsCurrentCellInEditMode)
					{
						if (dataGridViewCheckBoxCell.SwitchFormattedValue())
						{
							dataGridViewCheckBoxCell.NotifyDataGridViewOfValueChange();
							dataGridView.InvalidateCell(dataGridViewCheckBoxCell.ColumnIndex, dataGridViewCheckBoxCell.RowIndex);
							DataGridViewCheckBoxCell dataGridViewCheckBoxCell2 = base.Owner as DataGridViewCheckBoxCell;
							if (dataGridViewCheckBoxCell2 != null)
							{
								dataGridViewCheckBoxCell2.NotifyMASSClient(new Point(dataGridViewCheckBoxCell.ColumnIndex, dataGridViewCheckBoxCell.RowIndex));
							}
						}
						if (flag)
						{
							dataGridView.EndEdit();
						}
					}
				}
			}

			/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</summary>
			/// <returns>The value -1.</returns>
			// Token: 0x06006611 RID: 26129 RVA: 0x0001180C File Offset: 0x0000FA0C
			public override int GetChildCount()
			{
				return 0;
			}

			// Token: 0x06006612 RID: 26130 RVA: 0x0017D17E File Offset: 0x0017B37E
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level1;
			}

			// Token: 0x1700162E RID: 5678
			// (get) Token: 0x06006613 RID: 26131 RVA: 0x0017D18A File Offset: 0x0017B38A
			internal override int[] RuntimeId
			{
				get
				{
					if (this.runtimeId == null)
					{
						this.runtimeId = new int[2];
						this.runtimeId[0] = 42;
						this.runtimeId[1] = this.GetHashCode();
					}
					return this.runtimeId;
				}
			}

			// Token: 0x06006614 RID: 26132 RVA: 0x0017D1BE File Offset: 0x0017B3BE
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30041)
				{
					return this.IsPatternSupported(10015);
				}
				if (propertyID == 30003 && AccessibilityImprovements.Level2)
				{
					return 50002;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006615 RID: 26133 RVA: 0x0017D1FA File Offset: 0x0017B3FA
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10015 || base.IsPatternSupported(patternId);
			}

			// Token: 0x06006616 RID: 26134 RVA: 0x00016044 File Offset: 0x00014244
			internal override void Toggle()
			{
				this.DoDefaultAction();
			}

			// Token: 0x1700162F RID: 5679
			// (get) Token: 0x06006617 RID: 26135 RVA: 0x0017D210 File Offset: 0x0017B410
			internal override UnsafeNativeMethods.ToggleState ToggleState
			{
				get
				{
					object formattedValue = base.Owner.FormattedValue;
					bool flag;
					if (formattedValue is CheckState)
					{
						flag = (CheckState)formattedValue == CheckState.Checked;
					}
					else
					{
						if (!(formattedValue is bool))
						{
							return UnsafeNativeMethods.ToggleState.ToggleState_Indeterminate;
						}
						flag = (bool)formattedValue;
					}
					if (!flag)
					{
						return UnsafeNativeMethods.ToggleState.ToggleState_Off;
					}
					return UnsafeNativeMethods.ToggleState.ToggleState_On;
				}
			}

			// Token: 0x04003A5C RID: 14940
			private int[] runtimeId;
		}
	}
}
