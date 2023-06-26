using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Internal;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Displays a button-like user interface (UI) for use in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x0200019F RID: 415
	public class DataGridViewButtonCell : DataGridViewCell
	{
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00087BBC File Offset: 0x00085DBC
		// (set) Token: 0x06001CFB RID: 7419 RVA: 0x00087BE2 File Offset: 0x00085DE2
		private ButtonState ButtonState
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewButtonCell.PropButtonCellState, out flag);
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
					base.Properties.SetInteger(DataGridViewButtonCell.PropButtonCellState, (int)value);
				}
			}
		}

		/// <summary>Gets the type of the cell's hosted editing control.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the underlying editing control.</returns>
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x00015C90 File Offset: 0x00013E90
		public override Type EditType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets the style determining the button's appearance.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.FlatStyle" /> value.</exception>
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x00087C00 File Offset: 0x00085E00
		// (set) Token: 0x06001CFE RID: 7422 RVA: 0x00087C28 File Offset: 0x00085E28
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewButtonCell.PropButtonCellFlatStyle, out flag);
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
					base.Properties.SetInteger(DataGridViewButtonCell.PropButtonCellFlatStyle, (int)value);
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x17000646 RID: 1606
		// (set) Token: 0x06001CFF RID: 7423 RVA: 0x00087C7B File Offset: 0x00085E7B
		internal FlatStyle FlatStyleInternal
		{
			set
			{
				if (value != this.FlatStyle)
				{
					base.Properties.SetInteger(DataGridViewButtonCell.PropButtonCellFlatStyle, (int)value);
				}
			}
		}

		/// <summary>Gets the type of the formatted value associated with the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of the cell's formatted value.</returns>
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x00087C97 File Offset: 0x00085E97
		public override Type FormattedValueType
		{
			get
			{
				return DataGridViewButtonCell.defaultFormattedValueType;
			}
		}

		/// <summary>Gets or sets a value indicating whether the owning column's text will appear on the button displayed by the cell.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property will automatically match the value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> property of the owning column; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x00087CA0 File Offset: 0x00085EA0
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x00087CCB File Offset: 0x00085ECB
		[DefaultValue(false)]
		public bool UseColumnTextForButtonValue
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewButtonCell.PropButtonCellUseColumnTextForButtonValue, out flag);
				return flag && integer != 0;
			}
			set
			{
				if (value != this.UseColumnTextForButtonValue)
				{
					base.Properties.SetInteger(DataGridViewButtonCell.PropButtonCellUseColumnTextForButtonValue, value ? 1 : 0);
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x17000649 RID: 1609
		// (set) Token: 0x06001D03 RID: 7427 RVA: 0x00087CF3 File Offset: 0x00085EF3
		internal bool UseColumnTextForButtonValueInternal
		{
			set
			{
				if (value != this.UseColumnTextForButtonValue)
				{
					base.Properties.SetInteger(DataGridViewButtonCell.PropButtonCellUseColumnTextForButtonValue, value ? 1 : 0);
				}
			}
		}

		/// <summary>Gets or sets the data type of the values in the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x00087D18 File Offset: 0x00085F18
		public override Type ValueType
		{
			get
			{
				Type valueType = base.ValueType;
				if (valueType != null)
				{
					return valueType;
				}
				return DataGridViewButtonCell.defaultValueType;
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />.</returns>
		// Token: 0x06001D05 RID: 7429 RVA: 0x00087D3C File Offset: 0x00085F3C
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewButtonCell dataGridViewButtonCell;
			if (type == DataGridViewButtonCell.cellType)
			{
				dataGridViewButtonCell = new DataGridViewButtonCell();
			}
			else
			{
				dataGridViewButtonCell = (DataGridViewButtonCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewButtonCell);
			dataGridViewButtonCell.FlatStyleInternal = this.FlatStyle;
			dataGridViewButtonCell.UseColumnTextForButtonValueInternal = this.UseColumnTextForButtonValue;
			return dataGridViewButtonCell;
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />.</returns>
		// Token: 0x06001D06 RID: 7430 RVA: 0x00087D91 File Offset: 0x00085F91
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject(this);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> and cell style.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		// Token: 0x06001D07 RID: 7431 RVA: 0x00087D9C File Offset: 0x00085F9C
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
		// Token: 0x06001D08 RID: 7432 RVA: 0x00087DF4 File Offset: 0x00085FF4
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
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, null, this.GetErrorText(rowIndex), cellStyle, dataGridViewAdvancedBorderStyle, DataGridViewPaintParts.ContentForeground, false, true, false);
		}

		/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		// Token: 0x06001D09 RID: 7433 RVA: 0x00087E6C File Offset: 0x0008606C
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
			Rectangle stdBorderWidths = base.StdBorderWidths;
			int num = stdBorderWidths.Left + stdBorderWidths.Width + cellStyle.Padding.Horizontal;
			int num2 = stdBorderWidths.Top + stdBorderWidths.Height + cellStyle.Padding.Vertical;
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			string text = base.GetFormattedValue(rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize) as string;
			if (string.IsNullOrEmpty(text))
			{
				text = " ";
			}
			TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			int num3;
			int num4;
			if (base.DataGridView.ApplyVisualStylesToInnerCells)
			{
				Rectangle themeMargins = DataGridViewButtonCell.GetThemeMargins(graphics);
				num3 = themeMargins.X + themeMargins.Width;
				num4 = themeMargins.Y + themeMargins.Height;
			}
			else
			{
				num4 = (num3 = 5);
			}
			Size size;
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
				{
					if (cellStyle.WrapMode == DataGridViewTriState.True && text.Length > 1 && constraintSize.Height - num2 - num4 - 2 > 0)
					{
						size = new Size(DataGridViewCell.MeasureTextWidth(graphics, text, cellStyle.Font, constraintSize.Height - num2 - num4 - 2, textFormatFlags), 0);
					}
					else
					{
						size = new Size(DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags).Width, 0);
					}
				}
				else if (cellStyle.WrapMode == DataGridViewTriState.True && text.Length > 1)
				{
					size = DataGridViewCell.MeasureTextPreferredSize(graphics, text, cellStyle.Font, 5f, textFormatFlags);
				}
				else
				{
					size = DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags);
				}
			}
			else if (cellStyle.WrapMode == DataGridViewTriState.True && text.Length > 1 && constraintSize.Width - num - num3 - 4 > 0)
			{
				size = new Size(0, DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, constraintSize.Width - num - num3 - 4, textFormatFlags));
			}
			else
			{
				size = new Size(0, DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags).Height);
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				size.Width += num + num3 + 4;
				if (base.DataGridView.ShowCellErrors)
				{
					size.Width = Math.Max(size.Width, num + 8 + (int)DataGridViewCell.iconsWidth);
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				size.Height += num2 + num4 + 2;
				if (base.DataGridView.ShowCellErrors)
				{
					size.Height = Math.Max(size.Height, num2 + 8 + (int)DataGridViewCell.iconsHeight);
				}
			}
			return size;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00088118 File Offset: 0x00086318
		private static Rectangle GetThemeMargins(Graphics g)
		{
			if (DataGridViewButtonCell.rectThemeMargins.X == -1)
			{
				Rectangle rectangle = new Rectangle(0, 0, 100, 100);
				Rectangle backgroundContentRectangle = DataGridViewButtonCell.DataGridViewButtonCellRenderer.DataGridViewButtonRenderer.GetBackgroundContentRectangle(g, rectangle);
				DataGridViewButtonCell.rectThemeMargins.X = backgroundContentRectangle.X;
				DataGridViewButtonCell.rectThemeMargins.Y = backgroundContentRectangle.Y;
				DataGridViewButtonCell.rectThemeMargins.Width = 100 - backgroundContentRectangle.Right;
				DataGridViewButtonCell.rectThemeMargins.Height = 100 - backgroundContentRectangle.Bottom;
			}
			return DataGridViewButtonCell.rectThemeMargins;
		}

		/// <summary>Retrieves the text associated with the button.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The value of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> or the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> value of the owning column if <see cref="P:System.Windows.Forms.DataGridViewButtonCell.UseColumnTextForButtonValue" /> is <see langword="true" />.</returns>
		// Token: 0x06001D0B RID: 7435 RVA: 0x0008819C File Offset: 0x0008639C
		protected override object GetValue(int rowIndex)
		{
			if (this.UseColumnTextForButtonValue && base.DataGridView != null && base.DataGridView.NewRowIndex != rowIndex && base.OwningColumn != null && base.OwningColumn is DataGridViewButtonColumn)
			{
				return ((DataGridViewButtonColumn)base.OwningColumn).Text;
			}
			return base.GetValue(rowIndex);
		}

		/// <summary>Indicates whether a row is unshared if a key is pressed while the focus is on a cell in the row.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>
		///   <see langword="true" /> if the user pressed the SPACE key without modifier keys; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D0C RID: 7436 RVA: 0x000881F4 File Offset: 0x000863F4
		protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyCode == Keys.Space && !e.Alt && !e.Control && !e.Shift;
		}

		/// <summary>Indicates whether a row is unshared when a key is released while the focus is on a cell in the row.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>
		///   <see langword="true" /> if the user released the SPACE key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D0D RID: 7437 RVA: 0x0008821B File Offset: 0x0008641B
		protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyCode == Keys.Space;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse button is held down while the pointer is on a cell in the row.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		/// <returns>
		///   <see langword="true" /> if the user pressed the left mouse button; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D0E RID: 7438 RVA: 0x00088227 File Offset: 0x00086427
		protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		/// <returns>
		///   <see langword="true" /> if the cell was the last cell receiving a mouse click; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D0F RID: 7439 RVA: 0x00088238 File Offset: 0x00086438
		protected override bool MouseEnterUnsharesRow(int rowIndex)
		{
			return base.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && rowIndex == base.DataGridView.MouseDownCellAddress.Y;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer leaves the row.</summary>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		/// <returns>
		///   <see langword="true" /> if the button displayed by the cell is in the pressed state; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D10 RID: 7440 RVA: 0x00088278 File Offset: 0x00086478
		protected override bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return (this.ButtonState & ButtonState.Pushed) > ButtonState.Normal;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse button is released while the pointer is on a cell in the row.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		/// <returns>
		///   <see langword="true" /> if the left mouse button was released; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D11 RID: 7441 RVA: 0x00088227 File Offset: 0x00086427
		protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left;
		}

		/// <summary>Called when a character key is pressed while the focus is on the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x06001D12 RID: 7442 RVA: 0x0008828C File Offset: 0x0008648C
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

		/// <summary>Called when a character key is released while the focus is on the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data</param>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x06001D13 RID: 7443 RVA: 0x000882E0 File Offset: 0x000864E0
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
			}
		}

		/// <summary>Called when the focus moves from the cell.</summary>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		/// <param name="throughMouseClick">
		///   <see langword="true" /> if focus left the cell as a result of user mouse click; <see langword="false" /> if focus left due to a programmatic cell change.</param>
		// Token: 0x06001D14 RID: 7444 RVA: 0x0008838C File Offset: 0x0008658C
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

		/// <summary>Called when the mouse button is held down while the pointer is on the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001D15 RID: 7445 RVA: 0x000883A7 File Offset: 0x000865A7
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Left && DataGridViewButtonCell.mouseInContentBounds)
			{
				this.UpdateButtonState(this.ButtonState | ButtonState.Pushed, e.RowIndex);
			}
		}

		/// <summary>Called when the mouse pointer moves out of the cell.</summary>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x06001D16 RID: 7446 RVA: 0x000883E0 File Offset: 0x000865E0
		protected override void OnMouseLeave(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (DataGridViewButtonCell.mouseInContentBounds)
			{
				DataGridViewButtonCell.mouseInContentBounds = false;
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

		/// <summary>Called when the mouse pointer moves while it is over the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001D17 RID: 7447 RVA: 0x00088494 File Offset: 0x00086694
		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			bool flag = DataGridViewButtonCell.mouseInContentBounds;
			DataGridViewButtonCell.mouseInContentBounds = base.GetContentBounds(e.RowIndex).Contains(e.X, e.Y);
			if (flag != DataGridViewButtonCell.mouseInContentBounds)
			{
				if (base.DataGridView.ApplyVisualStylesToInnerCells || this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup)
				{
					base.DataGridView.InvalidateCell(base.ColumnIndex, e.RowIndex);
				}
				if (e.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && e.RowIndex == base.DataGridView.MouseDownCellAddress.Y && Control.MouseButtons == MouseButtons.Left)
				{
					if ((this.ButtonState & ButtonState.Pushed) == ButtonState.Normal && DataGridViewButtonCell.mouseInContentBounds && base.DataGridView.CellMouseDownInContentBounds)
					{
						this.UpdateButtonState(this.ButtonState | ButtonState.Pushed, e.RowIndex);
					}
					else if ((this.ButtonState & ButtonState.Pushed) != ButtonState.Normal && !DataGridViewButtonCell.mouseInContentBounds)
					{
						this.UpdateButtonState(this.ButtonState & ~ButtonState.Pushed, e.RowIndex);
					}
				}
			}
			base.OnMouseMove(e);
		}

		/// <summary>Called when the mouse button is released while the pointer is on the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001D18 RID: 7448 RVA: 0x000885CA File Offset: 0x000867CA
		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Left)
			{
				this.UpdateButtonState(this.ButtonState & ~ButtonState.Pushed, e.RowIndex);
			}
		}

		/// <summary>Paints the current <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />.</summary>
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
		// Token: 0x06001D19 RID: 7449 RVA: 0x000885FC File Offset: 0x000867FC
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, elementState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x00088634 File Offset: 0x00086834
		private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			bool flag = (elementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			bool flag2 = currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex;
			string text = formattedValue as string;
			SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
			SolidBrush cachedBrush2 = base.DataGridView.GetCachedBrush(flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle = cellBounds;
			Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
			rectangle.Offset(rectangle2.X, rectangle2.Y);
			rectangle.Width -= rectangle2.Right;
			rectangle.Height -= rectangle2.Bottom;
			Rectangle rectangle4;
			if (rectangle.Height > 0 && rectangle.Width > 0)
			{
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
				Rectangle rectangle3 = rectangle;
				if (rectangle.Height > 0 && rectangle.Width > 0 && (paint || computeContentBounds))
				{
					if (this.FlatStyle == FlatStyle.Standard || this.FlatStyle == FlatStyle.System)
					{
						if (base.DataGridView.ApplyVisualStylesToInnerCells)
						{
							if (paint && DataGridViewCell.PaintContentBackground(paintParts))
							{
								PushButtonState pushButtonState = PushButtonState.Normal;
								if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal)
								{
									pushButtonState = PushButtonState.Pressed;
								}
								else if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex && DataGridViewButtonCell.mouseInContentBounds)
								{
									pushButtonState = PushButtonState.Hot;
								}
								if (DataGridViewCell.PaintFocus(paintParts) && flag2 && base.DataGridView.ShowFocusCues && base.DataGridView.Focused)
								{
									pushButtonState |= PushButtonState.Default;
								}
								DataGridViewButtonCell.DataGridViewButtonCellRenderer.DrawButton(g, rectangle, (int)pushButtonState);
							}
							rectangle4 = rectangle;
							rectangle = DataGridViewButtonCell.DataGridViewButtonCellRenderer.DataGridViewButtonRenderer.GetBackgroundContentRectangle(g, rectangle);
						}
						else
						{
							if (paint && DataGridViewCell.PaintContentBackground(paintParts))
							{
								ControlPaint.DrawBorder(g, rectangle, SystemColors.Control, (this.ButtonState == ButtonState.Normal) ? ButtonBorderStyle.Outset : ButtonBorderStyle.Inset);
							}
							rectangle4 = rectangle;
							rectangle.Inflate(-SystemInformation.Border3DSize.Width, -SystemInformation.Border3DSize.Height);
						}
					}
					else if (this.FlatStyle == FlatStyle.Flat)
					{
						rectangle.Inflate(-1, -1);
						if (paint && DataGridViewCell.PaintContentBackground(paintParts))
						{
							ButtonBaseAdapter.DrawDefaultBorder(g, rectangle, cachedBrush2.Color, true);
							if (cachedBrush.Color.A == 255)
							{
								if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal)
								{
									ButtonBaseAdapter.ColorData colorData = ButtonBaseAdapter.PaintFlatRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
									IntPtr hdc = g.GetHdc();
									try
									{
										using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
										{
											WindowsBrush windowsBrush;
											if (colorData.options.highContrast)
											{
												windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, colorData.buttonShadow);
											}
											else
											{
												windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, colorData.lowHighlight);
											}
											try
											{
												ButtonBaseAdapter.PaintButtonBackground(windowsGraphics, rectangle, windowsBrush);
												goto IL_4C2;
											}
											finally
											{
												windowsBrush.Dispose();
											}
										}
									}
									finally
									{
										g.ReleaseHdc();
									}
								}
								if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex && DataGridViewButtonCell.mouseInContentBounds)
								{
									IntPtr hdc2 = g.GetHdc();
									try
									{
										using (WindowsGraphics windowsGraphics2 = WindowsGraphics.FromHdc(hdc2))
										{
											Color controlDark = SystemColors.ControlDark;
											using (WindowsBrush windowsBrush2 = new WindowsSolidBrush(windowsGraphics2.DeviceContext, controlDark))
											{
												ButtonBaseAdapter.PaintButtonBackground(windowsGraphics2, rectangle, windowsBrush2);
											}
										}
									}
									finally
									{
										g.ReleaseHdc();
									}
								}
							}
						}
						IL_4C2:
						rectangle4 = rectangle;
					}
					else
					{
						rectangle.Inflate(-1, -1);
						if (paint && DataGridViewCell.PaintContentBackground(paintParts))
						{
							if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal)
							{
								ButtonBaseAdapter.ColorData colorData2 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
								ButtonBaseAdapter.DrawDefaultBorder(g, rectangle, colorData2.options.highContrast ? colorData2.windowText : colorData2.windowFrame, true);
								ControlPaint.DrawBorder(g, rectangle, colorData2.options.highContrast ? colorData2.windowText : colorData2.buttonShadow, ButtonBorderStyle.Solid);
							}
							else if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex && DataGridViewButtonCell.mouseInContentBounds)
							{
								ButtonBaseAdapter.ColorData colorData3 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
								ButtonBaseAdapter.DrawDefaultBorder(g, rectangle, colorData3.options.highContrast ? colorData3.windowText : colorData3.buttonShadow, false);
								ButtonBaseAdapter.Draw3DLiteBorder(g, rectangle, colorData3, true);
							}
							else
							{
								ButtonBaseAdapter.ColorData colorData4 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
								ButtonBaseAdapter.DrawDefaultBorder(g, rectangle, colorData4.options.highContrast ? colorData4.windowText : colorData4.buttonShadow, false);
								ButtonBaseAdapter.DrawFlatBorder(g, rectangle, colorData4.options.highContrast ? colorData4.windowText : colorData4.buttonShadow);
							}
						}
						rectangle4 = rectangle;
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
				if (paint && DataGridViewCell.PaintFocus(paintParts) && flag2 && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && rectangle.Width > 2 * SystemInformation.Border3DSize.Width + 1 && rectangle.Height > 2 * SystemInformation.Border3DSize.Height + 1)
				{
					if (this.FlatStyle == FlatStyle.System || this.FlatStyle == FlatStyle.Standard)
					{
						ControlPaint.DrawFocusRectangle(g, Rectangle.Inflate(rectangle, -1, -1), Color.Empty, SystemColors.Control);
					}
					else if (this.FlatStyle == FlatStyle.Flat)
					{
						if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal || (base.DataGridView.CurrentCellAddress.Y == rowIndex && base.DataGridView.CurrentCellAddress.X == base.ColumnIndex))
						{
							ButtonBaseAdapter.ColorData colorData5 = ButtonBaseAdapter.PaintFlatRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
							string text2 = ((text != null) ? text : string.Empty);
							ButtonBaseAdapter.LayoutOptions layoutOptions = ButtonFlatAdapter.PaintFlatLayout(g, true, SystemInformation.HighContrast, 1, rectangle, Padding.Empty, false, cellStyle.Font, text2, base.DataGridView.Enabled, DataGridViewUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.DataGridView.RightToLeft);
							layoutOptions.everettButtonCompat = false;
							ButtonBaseAdapter.LayoutData layoutData = layoutOptions.Layout();
							ButtonBaseAdapter.DrawFlatFocus(g, layoutData.focus, colorData5.options.highContrast ? colorData5.windowText : colorData5.constrastButtonShadow);
						}
					}
					else if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal || (base.DataGridView.CurrentCellAddress.Y == rowIndex && base.DataGridView.CurrentCellAddress.X == base.ColumnIndex))
					{
						bool flag3 = this.ButtonState == ButtonState.Normal;
						string text3 = ((text != null) ? text : string.Empty);
						ButtonBaseAdapter.LayoutOptions layoutOptions2 = ButtonPopupAdapter.PaintPopupLayout(g, flag3, SystemInformation.HighContrast ? 2 : 1, rectangle, Padding.Empty, false, cellStyle.Font, text3, base.DataGridView.Enabled, DataGridViewUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.DataGridView.RightToLeft);
						layoutOptions2.everettButtonCompat = false;
						ButtonBaseAdapter.LayoutData layoutData2 = layoutOptions2.Layout();
						ControlPaint.DrawFocusRectangle(g, layoutData2.focus, cellStyle.ForeColor, cellStyle.BackColor);
					}
				}
				if (text != null && paint && DataGridViewCell.PaintContentForeground(paintParts))
				{
					rectangle.Offset(2, 1);
					rectangle.Width -= 4;
					rectangle.Height -= 2;
					if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal && this.FlatStyle != FlatStyle.Flat && this.FlatStyle != FlatStyle.Popup)
					{
						rectangle.Offset(1, 1);
						int num = rectangle.Width;
						rectangle.Width = num - 1;
						num = rectangle.Height;
						rectangle.Height = num - 1;
					}
					if (rectangle.Width > 0 && rectangle.Height > 0)
					{
						Color color;
						if (base.DataGridView.ApplyVisualStylesToInnerCells && (this.FlatStyle == FlatStyle.System || this.FlatStyle == FlatStyle.Standard))
						{
							color = DataGridViewButtonCell.DataGridViewButtonCellRenderer.DataGridViewButtonRenderer.GetColor(ColorProperty.TextColor);
						}
						else
						{
							color = cachedBrush2.Color;
						}
						TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
						TextRenderer.DrawText(g, text, cellStyle.Font, rectangle, color, textFormatFlags);
					}
				}
				if (base.DataGridView.ShowCellErrors && paint && DataGridViewCell.PaintErrorIcon(paintParts))
				{
					base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, rectangle3, errorText);
				}
			}
			else
			{
				rectangle4 = Rectangle.Empty;
			}
			return rectangle4;
		}

		/// <summary>Returns the string representation of the cell.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current cell.</returns>
		// Token: 0x06001D1B RID: 7451 RVA: 0x000890F4 File Offset: 0x000872F4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewButtonCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00089150 File Offset: 0x00087350
		private void UpdateButtonState(ButtonState newButtonState, int rowIndex)
		{
			if (this.ButtonState != newButtonState)
			{
				this.ButtonState = newButtonState;
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
		}

		// Token: 0x04000C75 RID: 3189
		private static readonly int PropButtonCellFlatStyle = PropertyStore.CreateKey();

		// Token: 0x04000C76 RID: 3190
		private static readonly int PropButtonCellState = PropertyStore.CreateKey();

		// Token: 0x04000C77 RID: 3191
		private static readonly int PropButtonCellUseColumnTextForButtonValue = PropertyStore.CreateKey();

		// Token: 0x04000C78 RID: 3192
		private static readonly VisualStyleElement ButtonElement = VisualStyleElement.Button.PushButton.Normal;

		// Token: 0x04000C79 RID: 3193
		private const byte DATAGRIDVIEWBUTTONCELL_themeMargin = 100;

		// Token: 0x04000C7A RID: 3194
		private const byte DATAGRIDVIEWBUTTONCELL_horizontalTextMargin = 2;

		// Token: 0x04000C7B RID: 3195
		private const byte DATAGRIDVIEWBUTTONCELL_verticalTextMargin = 1;

		// Token: 0x04000C7C RID: 3196
		private const byte DATAGRIDVIEWBUTTONCELL_textPadding = 5;

		// Token: 0x04000C7D RID: 3197
		private static Rectangle rectThemeMargins = new Rectangle(-1, -1, 0, 0);

		// Token: 0x04000C7E RID: 3198
		private static bool mouseInContentBounds = false;

		// Token: 0x04000C7F RID: 3199
		private static Type defaultFormattedValueType = typeof(string);

		// Token: 0x04000C80 RID: 3200
		private static Type defaultValueType = typeof(object);

		// Token: 0x04000C81 RID: 3201
		private static Type cellType = typeof(DataGridViewButtonCell);

		// Token: 0x02000663 RID: 1635
		private class DataGridViewButtonCellRenderer
		{
			// Token: 0x060065D8 RID: 26072 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewButtonCellRenderer()
			{
			}

			// Token: 0x17001617 RID: 5655
			// (get) Token: 0x060065D9 RID: 26073 RVA: 0x0017BBF1 File Offset: 0x00179DF1
			public static VisualStyleRenderer DataGridViewButtonRenderer
			{
				get
				{
					if (DataGridViewButtonCell.DataGridViewButtonCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewButtonCell.DataGridViewButtonCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewButtonCell.ButtonElement);
					}
					return DataGridViewButtonCell.DataGridViewButtonCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x060065DA RID: 26074 RVA: 0x0017BC0E File Offset: 0x00179E0E
			public static void DrawButton(Graphics g, Rectangle bounds, int buttonState)
			{
				DataGridViewButtonCell.DataGridViewButtonCellRenderer.DataGridViewButtonRenderer.SetParameters(DataGridViewButtonCell.ButtonElement.ClassName, DataGridViewButtonCell.ButtonElement.Part, buttonState);
				DataGridViewButtonCell.DataGridViewButtonCellRenderer.DataGridViewButtonRenderer.DrawBackground(g, bounds, Rectangle.Truncate(g.ClipBounds));
			}

			// Token: 0x04003A52 RID: 14930
			private static VisualStyleRenderer visualStyleRenderer;
		}

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> to accessibility client applications.</summary>
		// Token: 0x02000664 RID: 1636
		protected class DataGridViewButtonCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" />.</param>
			// Token: 0x060065DB RID: 26075 RVA: 0x0017BC46 File Offset: 0x00179E46
			public DataGridViewButtonCellAccessibleObject(DataGridViewCell owner)
				: base(owner)
			{
			}

			/// <summary>Gets a <see cref="T:System.String" /> that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" />.</summary>
			/// <returns>The <see cref="T:System.String" /> "Press" if the <see cref="P:System.Windows.Forms.DataGridViewCell.ReadOnly" /> property is set to <see langword="false" />; otherwise, an empty <see cref="T:System.String" /> ("").</returns>
			// Token: 0x17001618 RID: 5656
			// (get) Token: 0x060065DC RID: 26076 RVA: 0x0017BC4F File Offset: 0x00179E4F
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("DataGridView_AccButtonCellDefaultAction");
				}
			}

			/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" /></summary>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
			///  -or-  
			///  The <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property belongs to a shared row.</exception>
			// Token: 0x060065DD RID: 26077 RVA: 0x0017BC5C File Offset: 0x00179E5C
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				DataGridViewButtonCell dataGridViewButtonCell = (DataGridViewButtonCell)base.Owner;
				DataGridView dataGridView = dataGridViewButtonCell.DataGridView;
				if (dataGridView != null && dataGridViewButtonCell.RowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
				}
				if (dataGridViewButtonCell.OwningColumn != null && dataGridViewButtonCell.OwningRow != null)
				{
					dataGridView.OnCellClickInternal(new DataGridViewCellEventArgs(dataGridViewButtonCell.ColumnIndex, dataGridViewButtonCell.RowIndex));
					dataGridView.OnCellContentClickInternal(new DataGridViewCellEventArgs(dataGridViewButtonCell.ColumnIndex, dataGridViewButtonCell.RowIndex));
				}
			}

			/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" />.</summary>
			/// <returns>The value -1.</returns>
			// Token: 0x060065DE RID: 26078 RVA: 0x0001180C File Offset: 0x0000FA0C
			public override int GetChildCount()
			{
				return 0;
			}

			// Token: 0x060065DF RID: 26079 RVA: 0x0017BCD6 File Offset: 0x00179ED6
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level2 || base.IsIAccessibleExSupported();
			}

			// Token: 0x060065E0 RID: 26080 RVA: 0x0017BCE7 File Offset: 0x00179EE7
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50000;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}
