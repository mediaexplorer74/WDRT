using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Contains functionality common to row header cells and column header cells.</summary>
	// Token: 0x020001FA RID: 506
	public class DataGridViewHeaderCell : DataGridViewCell
	{
		/// <summary>Gets the buttonlike visual state of the header cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ButtonState" /> values; the default is <see cref="F:System.Windows.Forms.ButtonState.Normal" />.</returns>
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x0009B948 File Offset: 0x00099B48
		protected ButtonState ButtonState
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewHeaderCell.PropButtonState, out flag);
				if (flag)
				{
					return (ButtonState)integer;
				}
				return ButtonState.Normal;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (set) Token: 0x06002115 RID: 8469 RVA: 0x0009B96E File Offset: 0x00099B6E
		private ButtonState ButtonStatePrivate
		{
			set
			{
				if (this.ButtonState != value)
				{
					base.Properties.SetInteger(DataGridViewHeaderCell.PropButtonState, (int)value);
				}
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002116 RID: 8470 RVA: 0x0009B98A File Offset: 0x00099B8A
		protected override void Dispose(bool disposing)
		{
			if (this.FlipXPThemesBitmap != null && disposing)
			{
				this.FlipXPThemesBitmap.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets a value that indicates whether the cell is currently displayed on-screen.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell is on-screen or partially on-screen; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x0009B9AC File Offset: 0x00099BAC
		[Browsable(false)]
		public override bool Displayed
		{
			get
			{
				if (base.DataGridView == null || !base.DataGridView.Visible)
				{
					return false;
				}
				if (base.OwningRow != null)
				{
					return base.DataGridView.RowHeadersVisible && base.OwningRow.Displayed;
				}
				if (base.OwningColumn != null)
				{
					return base.DataGridView.ColumnHeadersVisible && base.OwningColumn.Displayed;
				}
				return base.DataGridView.LayoutInfo.TopLeftHeader != Rectangle.Empty;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x0009BA30 File Offset: 0x00099C30
		// (set) Token: 0x06002119 RID: 8473 RVA: 0x0009BA47 File Offset: 0x00099C47
		internal Bitmap FlipXPThemesBitmap
		{
			get
			{
				return (Bitmap)base.Properties.GetObject(DataGridViewHeaderCell.PropFlipXPThemesBitmap);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewHeaderCell.PropFlipXPThemesBitmap))
				{
					base.Properties.SetObject(DataGridViewHeaderCell.PropFlipXPThemesBitmap, value);
				}
			}
		}

		/// <summary>Gets the type of the formatted value of the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.String" /> type.</returns>
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x0009BA6F File Offset: 0x00099C6F
		public override Type FormattedValueType
		{
			get
			{
				return DataGridViewHeaderCell.defaultFormattedValueType;
			}
		}

		/// <summary>Gets a value indicating whether the cell is frozen.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell is frozen; otherwise, <see langword="false" />. The default is <see langword="false" /> if the cell is detached from a <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x0009BA76 File Offset: 0x00099C76
		[Browsable(false)]
		public override bool Frozen
		{
			get
			{
				if (base.OwningRow != null)
				{
					return base.OwningRow.Frozen;
				}
				if (base.OwningColumn != null)
				{
					return base.OwningColumn.Frozen;
				}
				return base.DataGridView != null;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x0009BAAB File Offset: 0x00099CAB
		internal override bool HasValueType
		{
			get
			{
				return base.Properties.ContainsObject(DataGridViewHeaderCell.PropValueType) && base.Properties.GetObject(DataGridViewHeaderCell.PropValueType) != null;
			}
		}

		/// <summary>Gets a value indicating whether the header cell is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		/// <exception cref="T:System.InvalidOperationException">An operation tries to set this property.</exception>
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x00012E4E File Offset: 0x0001104E
		// (set) Token: 0x0600211E RID: 8478 RVA: 0x0009BAD4 File Offset: 0x00099CD4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool ReadOnly
		{
			get
			{
				return true;
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_HeaderCellReadOnlyProperty", new object[] { "ReadOnly" }));
			}
		}

		/// <summary>Gets a value indicating whether the cell is resizable.</summary>
		/// <returns>
		///   <see langword="true" /> if this cell can be resized; otherwise, <see langword="false" />. The default is <see langword="false" /> if the cell is not attached to a <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x0009BAF4 File Offset: 0x00099CF4
		[Browsable(false)]
		public override bool Resizable
		{
			get
			{
				if (base.OwningRow != null)
				{
					return base.OwningRow.Resizable == DataGridViewTriState.True || (base.DataGridView != null && base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing);
				}
				if (base.OwningColumn != null)
				{
					return base.OwningColumn.Resizable == DataGridViewTriState.True || (base.DataGridView != null && base.DataGridView.ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.EnableResizing);
				}
				return base.DataGridView != null && (base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing || base.DataGridView.ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.EnableResizing);
			}
		}

		/// <summary>Gets or sets a value indicating whether the cell is selected.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property is being set.</exception>
		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x06002121 RID: 8481 RVA: 0x0009BB8A File Offset: 0x00099D8A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Selected
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_HeaderCellReadOnlyProperty", new object[] { "Selected" }));
			}
		}

		/// <summary>Gets the type of the value stored in the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.String" /> type.</returns>
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x0009BBAC File Offset: 0x00099DAC
		// (set) Token: 0x06002123 RID: 8483 RVA: 0x0009BBDF File Offset: 0x00099DDF
		public override Type ValueType
		{
			get
			{
				Type type = (Type)base.Properties.GetObject(DataGridViewHeaderCell.PropValueType);
				if (type != null)
				{
					return type;
				}
				return DataGridViewHeaderCell.defaultValueType;
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewHeaderCell.PropValueType))
				{
					base.Properties.SetObject(DataGridViewHeaderCell.PropValueType, value);
				}
			}
		}

		/// <summary>Gets a value indicating whether or not the cell is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell is visible; otherwise, <see langword="false" />. The default is <see langword="false" /> if the cell is detached from a <see cref="T:System.Windows.Forms.DataGridView" /></returns>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x0009BC10 File Offset: 0x00099E10
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				if (base.OwningRow != null)
				{
					return base.OwningRow.Visible && (base.DataGridView == null || base.DataGridView.RowHeadersVisible);
				}
				if (base.OwningColumn != null)
				{
					return base.OwningColumn.Visible && (base.DataGridView == null || base.DataGridView.ColumnHeadersVisible);
				}
				return base.DataGridView != null && base.DataGridView.RowHeadersVisible && base.DataGridView.ColumnHeadersVisible;
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" />.</returns>
		// Token: 0x06002125 RID: 8485 RVA: 0x0009BC9C File Offset: 0x00099E9C
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewHeaderCell dataGridViewHeaderCell;
			if (type == DataGridViewHeaderCell.cellType)
			{
				dataGridViewHeaderCell = new DataGridViewHeaderCell();
			}
			else
			{
				dataGridViewHeaderCell = (DataGridViewHeaderCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewHeaderCell);
			dataGridViewHeaderCell.Value = base.Value;
			return dataGridViewHeaderCell;
		}

		/// <summary>Gets the shortcut menu of the header cell.</summary>
		/// <param name="rowIndex">Ignored by this implementation.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" /> if the <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> or <see cref="T:System.Windows.Forms.DataGridView" /> has a shortcut menu assigned; otherwise, <see langword="null" />.</returns>
		// Token: 0x06002126 RID: 8486 RVA: 0x0009BCE8 File Offset: 0x00099EE8
		public override ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
		{
			ContextMenuStrip contextMenuStrip = base.GetContextMenuStrip(rowIndex);
			if (contextMenuStrip != null)
			{
				return contextMenuStrip;
			}
			if (base.DataGridView != null)
			{
				return base.DataGridView.ContextMenuStrip;
			}
			return null;
		}

		/// <summary>Returns a value indicating the current state of the cell as inherited from the state of its row or column.</summary>
		/// <param name="rowIndex">The index of the row containing the cell or -1 if the cell is not a row header cell or is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control.</param>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values representing the current state of the cell.</returns>
		/// <exception cref="T:System.ArgumentException">The cell is a row header cell, the cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control, and <paramref name="rowIndex" /> is not -1.  
		/// -or-
		///  The cell is a row header cell, the cell is contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control, and <paramref name="rowIndex" /> is outside the valid range of 0 to the number of rows in the control minus 1.  
		/// -or-
		///  The cell is a row header cell and <paramref name="rowIndex" /> is not the index of the row containing this cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The cell is a column header cell or the control's <see cref="P:System.Windows.Forms.DataGridView.TopLeftHeaderCell" /> and <paramref name="rowIndex" /> is not -1.</exception>
		// Token: 0x06002127 RID: 8487 RVA: 0x0009BD18 File Offset: 0x00099F18
		public override DataGridViewElementStates GetInheritedState(int rowIndex)
		{
			DataGridViewElementStates dataGridViewElementStates = DataGridViewElementStates.ReadOnly | DataGridViewElementStates.ResizableSet;
			if (base.OwningRow != null)
			{
				if ((base.DataGridView == null && rowIndex != -1) || (base.DataGridView != null && (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)))
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"rowIndex",
						rowIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (base.DataGridView != null && base.DataGridView.Rows.SharedRow(rowIndex) != base.OwningRow)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"rowIndex",
						rowIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				dataGridViewElementStates |= base.OwningRow.GetState(rowIndex) & DataGridViewElementStates.Frozen;
				if (base.OwningRow.GetResizable(rowIndex) == DataGridViewTriState.True || (base.DataGridView != null && base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Resizable;
				}
				if (base.OwningRow.GetVisible(rowIndex) && (base.DataGridView == null || base.DataGridView.RowHeadersVisible))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Visible;
					if (base.OwningRow.GetDisplayed(rowIndex))
					{
						dataGridViewElementStates |= DataGridViewElementStates.Displayed;
					}
				}
			}
			else if (base.OwningColumn != null)
			{
				if (rowIndex != -1)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				dataGridViewElementStates |= base.OwningColumn.State & DataGridViewElementStates.Frozen;
				if (base.OwningColumn.Resizable == DataGridViewTriState.True || (base.DataGridView != null && base.DataGridView.ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.EnableResizing))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Resizable;
				}
				if (base.OwningColumn.Visible && (base.DataGridView == null || base.DataGridView.ColumnHeadersVisible))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Visible;
					if (base.OwningColumn.Displayed)
					{
						dataGridViewElementStates |= DataGridViewElementStates.Displayed;
					}
				}
			}
			else if (base.DataGridView != null)
			{
				if (rowIndex != -1)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				dataGridViewElementStates |= DataGridViewElementStates.Frozen;
				if (base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing || base.DataGridView.ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.EnableResizing)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Resizable;
				}
				if (base.DataGridView.RowHeadersVisible && base.DataGridView.ColumnHeadersVisible)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Visible;
					if (base.DataGridView.LayoutInfo.TopLeftHeader != Rectangle.Empty)
					{
						dataGridViewElementStates |= DataGridViewElementStates.Displayed;
					}
				}
			}
			return dataGridViewElementStates;
		}

		/// <summary>Gets the size of the cell.</summary>
		/// <param name="rowIndex">The row index of the header cell.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the header cell.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property for this cell is <see langword="null" /> and <paramref name="rowIndex" /> does not equal -1.  
		///  -or-  
		///  The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningColumn" /> property for this cell is not <see langword="null" /> and <paramref name="rowIndex" /> does not equal -1.  
		///  -or-  
		///  The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningRow" /> property for this cell is not <see langword="null" /> and <paramref name="rowIndex" /> is less than zero or greater than or equal to the number of rows in the control.  
		///  -or-  
		///  The values of the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningColumn" /> and <see cref="P:System.Windows.Forms.DataGridViewCell.OwningRow" /> properties of this cell are both <see langword="null" /> and <paramref name="rowIndex" /> does not equal -1.</exception>
		/// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningRow" /> property for this cell is not <see langword="null" /> and <paramref name="rowIndex" /> indicates a row other than the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningRow" />.</exception>
		// Token: 0x06002128 RID: 8488 RVA: 0x0009BF64 File Offset: 0x0009A164
		protected override Size GetSize(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				if (rowIndex != -1)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				return new Size(-1, -1);
			}
			else if (base.OwningColumn != null)
			{
				if (rowIndex != -1)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				return new Size(base.OwningColumn.Thickness, base.DataGridView.ColumnHeadersHeight);
			}
			else if (base.OwningRow != null)
			{
				if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (base.DataGridView.Rows.SharedRow(rowIndex) != base.OwningRow)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"rowIndex",
						rowIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				return new Size(base.DataGridView.RowHeadersWidth, base.OwningRow.GetHeight(rowIndex));
			}
			else
			{
				if (rowIndex != -1)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				return new Size(base.DataGridView.RowHeadersWidth, base.DataGridView.ColumnHeadersHeight);
			}
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0009C080 File Offset: 0x0009A280
		internal static Rectangle GetThemeMargins(Graphics g)
		{
			if (DataGridViewHeaderCell.rectThemeMargins.X == -1)
			{
				Rectangle rectangle = new Rectangle(0, 0, 100, 100);
				Rectangle backgroundContentRectangle = DataGridViewHeaderCell.DataGridViewHeaderCellRenderer.VisualStyleRenderer.GetBackgroundContentRectangle(g, rectangle);
				DataGridViewHeaderCell.rectThemeMargins.X = backgroundContentRectangle.X;
				DataGridViewHeaderCell.rectThemeMargins.Y = backgroundContentRectangle.Y;
				DataGridViewHeaderCell.rectThemeMargins.Width = 100 - backgroundContentRectangle.Right;
				DataGridViewHeaderCell.rectThemeMargins.Height = 100 - backgroundContentRectangle.Bottom;
				if (DataGridViewHeaderCell.rectThemeMargins.X == 3 && DataGridViewHeaderCell.rectThemeMargins.Y + DataGridViewHeaderCell.rectThemeMargins.Width + DataGridViewHeaderCell.rectThemeMargins.Height == 0)
				{
					DataGridViewHeaderCell.rectThemeMargins = new Rectangle(0, 0, 2, 3);
				}
				else
				{
					try
					{
						string fileName = Path.GetFileName(VisualStyleInformation.ThemeFilename);
						if (string.Equals(fileName, "Aero.msstyles", StringComparison.OrdinalIgnoreCase))
						{
							DataGridViewHeaderCell.rectThemeMargins = new Rectangle(2, 1, 0, 2);
						}
					}
					catch
					{
					}
				}
			}
			return DataGridViewHeaderCell.rectThemeMargins;
		}

		/// <summary>Gets the value of the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not -1.</exception>
		// Token: 0x0600212A RID: 8490 RVA: 0x0009C184 File Offset: 0x0009A384
		protected override object GetValue(int rowIndex)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			return base.Properties.GetObject(DataGridViewCell.PropCellValue);
		}

		/// <summary>Indicates whether a row will be unshared when the mouse button is held down while the pointer is on a cell in the row.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains information about the mouse position.</param>
		/// <returns>
		///   <see langword="true" /> if the user clicks with the left mouse button, visual styles are enabled, and the <see cref="P:System.Windows.Forms.DataGridView.EnableHeadersVisualStyles" /> property is <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600212B RID: 8491 RVA: 0x0009C1A5 File Offset: 0x0009A3A5
		protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left && base.DataGridView.ApplyVisualStylesToHeaderCells;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
		/// <param name="rowIndex">The index of the row that the mouse pointer entered.</param>
		/// <returns>
		///   <see langword="true" /> if visual styles are enabled, and the <see cref="P:System.Windows.Forms.DataGridView.EnableHeadersVisualStyles" /> property is <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600212C RID: 8492 RVA: 0x0009C1C4 File Offset: 0x0009A3C4
		protected override bool MouseEnterUnsharesRow(int rowIndex)
		{
			return base.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && rowIndex == base.DataGridView.MouseDownCellAddress.Y && base.DataGridView.ApplyVisualStylesToHeaderCells;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer leaves the row.</summary>
		/// <param name="rowIndex">The index of the row that the mouse pointer left.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.DataGridViewHeaderCell.ButtonState" /> property value is not <see cref="F:System.Windows.Forms.ButtonState.Normal" />, visual styles are enabled, and the <see cref="P:System.Windows.Forms.DataGridView.EnableHeadersVisualStyles" /> property is <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600212D RID: 8493 RVA: 0x0009C20F File Offset: 0x0009A40F
		protected override bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return this.ButtonState != ButtonState.Normal && base.DataGridView.ApplyVisualStylesToHeaderCells;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse button is released while the pointer is on a cell in the row.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains information about the mouse position.</param>
		/// <returns>
		///   <see langword="true" /> if the left mouse button was released, visual styles are enabled, and the <see cref="P:System.Windows.Forms.DataGridView.EnableHeadersVisualStyles" /> property is <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600212E RID: 8494 RVA: 0x0009C1A5 File Offset: 0x0009A3A5
		protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left && base.DataGridView.ApplyVisualStylesToHeaderCells;
		}

		/// <summary>Called when the mouse button is held down while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains information about the mouse position.</param>
		// Token: 0x0600212F RID: 8495 RVA: 0x0009C228 File Offset: 0x0009A428
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Left && base.DataGridView.ApplyVisualStylesToHeaderCells && !base.DataGridView.ResizingOperationAboutToStart)
			{
				this.UpdateButtonState(ButtonState.Pushed, e.RowIndex);
			}
		}

		/// <summary>Called when the mouse pointer enters the cell.</summary>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		// Token: 0x06002130 RID: 8496 RVA: 0x0009C278 File Offset: 0x0009A478
		protected override void OnMouseEnter(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				if (base.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && rowIndex == base.DataGridView.MouseDownCellAddress.Y && this.ButtonState == ButtonState.Normal && Control.MouseButtons == MouseButtons.Left && !base.DataGridView.ResizingOperationAboutToStart)
				{
					this.UpdateButtonState(ButtonState.Pushed, rowIndex);
				}
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
		}

		/// <summary>Called when the mouse pointer leaves the cell.</summary>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		// Token: 0x06002131 RID: 8497 RVA: 0x0009C30B File Offset: 0x0009A50B
		protected override void OnMouseLeave(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				if (this.ButtonState != ButtonState.Normal)
				{
					this.UpdateButtonState(ButtonState.Normal, rowIndex);
				}
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
		}

		/// <summary>Called when the mouse button is released while the pointer is over the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains information about the mouse position.</param>
		// Token: 0x06002132 RID: 8498 RVA: 0x0009C345 File Offset: 0x0009A545
		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Left && base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				this.UpdateButtonState(ButtonState.Normal, e.RowIndex);
			}
		}

		/// <summary>Paints the current <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the cell.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the cell that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="dataGridViewElementState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
		/// <param name="value">The data of the cell that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the cell that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
		// Token: 0x06002133 RID: 8499 RVA: 0x0009C378 File Offset: 0x0009A578
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			if (DataGridViewCell.PaintBackground(paintParts))
			{
				Rectangle rectangle = cellBounds;
				Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
				rectangle.Offset(rectangle2.X, rectangle2.Y);
				rectangle.Width -= rectangle2.Right;
				rectangle.Height -= rectangle2.Bottom;
				bool flag = (dataGridViewElementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
				SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
				if (cachedBrush.Color.A == 255)
				{
					graphics.FillRectangle(cachedBrush, rectangle);
				}
			}
		}

		/// <summary>Returns a string that describes the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x06002134 RID: 8500 RVA: 0x0009C450 File Offset: 0x0009A650
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewHeaderCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x0009C4AC File Offset: 0x0009A6AC
		private void UpdateButtonState(ButtonState newButtonState, int rowIndex)
		{
			this.ButtonStatePrivate = newButtonState;
			base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
		}

		// Token: 0x04000DC3 RID: 3523
		private const byte DATAGRIDVIEWHEADERCELL_themeMargin = 100;

		// Token: 0x04000DC4 RID: 3524
		private static Type defaultFormattedValueType = typeof(string);

		// Token: 0x04000DC5 RID: 3525
		private static Type defaultValueType = typeof(object);

		// Token: 0x04000DC6 RID: 3526
		private static Type cellType = typeof(DataGridViewHeaderCell);

		// Token: 0x04000DC7 RID: 3527
		private static Rectangle rectThemeMargins = new Rectangle(-1, -1, 0, 0);

		// Token: 0x04000DC8 RID: 3528
		private static readonly int PropValueType = PropertyStore.CreateKey();

		// Token: 0x04000DC9 RID: 3529
		private static readonly int PropButtonState = PropertyStore.CreateKey();

		// Token: 0x04000DCA RID: 3530
		private static readonly int PropFlipXPThemesBitmap = PropertyStore.CreateKey();

		// Token: 0x04000DCB RID: 3531
		private const string AEROTHEMEFILENAME = "Aero.msstyles";

		// Token: 0x02000670 RID: 1648
		private class DataGridViewHeaderCellRenderer
		{
			// Token: 0x06006658 RID: 26200 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewHeaderCellRenderer()
			{
			}

			// Token: 0x17001644 RID: 5700
			// (get) Token: 0x06006659 RID: 26201 RVA: 0x0017E184 File Offset: 0x0017C384
			public static VisualStyleRenderer VisualStyleRenderer
			{
				get
				{
					if (DataGridViewHeaderCell.DataGridViewHeaderCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewHeaderCell.DataGridViewHeaderCellRenderer.visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Header.Item.Normal);
					}
					return DataGridViewHeaderCell.DataGridViewHeaderCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x04003A67 RID: 14951
			private static VisualStyleRenderer visualStyleRenderer;
		}
	}
}
