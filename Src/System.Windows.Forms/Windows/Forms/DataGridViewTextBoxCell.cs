using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Displays editable text information in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x0200021B RID: 539
	public class DataGridViewTextBoxCell : DataGridViewCell
	{
		/// <summary>Creates a new <see cref="T:System.Windows.Forms.AccessibleObject" /> for this <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" /> instance.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> instance that supports the ControlType UI Automation property.</returns>
		// Token: 0x06002326 RID: 8998 RVA: 0x000A7284 File Offset: 0x000A5484
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level1)
			{
				return new DataGridViewTextBoxCell.DataGridViewTextBoxCellAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x000A729A File Offset: 0x000A549A
		// (set) Token: 0x06002328 RID: 9000 RVA: 0x000A72B1 File Offset: 0x000A54B1
		private DataGridViewTextBoxEditingControl EditingTextBox
		{
			get
			{
				return (DataGridViewTextBoxEditingControl)base.Properties.GetObject(DataGridViewTextBoxCell.PropTextBoxCellEditingTextBox);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewTextBoxCell.PropTextBoxCellEditingTextBox))
				{
					base.Properties.SetObject(DataGridViewTextBoxCell.PropTextBoxCellEditingTextBox, value);
				}
			}
		}

		/// <summary>Gets the type of the formatted value associated with the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the <see cref="T:System.String" /> type in all cases.</returns>
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x000A72D9 File Offset: 0x000A54D9
		public override Type FormattedValueType
		{
			get
			{
				return DataGridViewTextBoxCell.defaultFormattedValueType;
			}
		}

		/// <summary>Gets or sets the maximum number of characters that can be entered into the text box.</summary>
		/// <returns>The maximum number of characters that can be entered into the text box; the default value is 32767.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0.</exception>
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x000A72E0 File Offset: 0x000A54E0
		// (set) Token: 0x0600232B RID: 9003 RVA: 0x000A730C File Offset: 0x000A550C
		[DefaultValue(32767)]
		public virtual int MaxInputLength
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewTextBoxCell.PropTextBoxCellMaxInputLength, out flag);
				if (flag)
				{
					return integer;
				}
				return 32767;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MaxInputLength", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"MaxInputLength",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				base.Properties.SetInteger(DataGridViewTextBoxCell.PropTextBoxCellMaxInputLength, value);
				if (this.OwnsEditingTextBox(base.RowIndex))
				{
					this.EditingTextBox.MaxLength = value;
				}
			}
		}

		/// <summary>Gets or sets the data type of the values in the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x000A738C File Offset: 0x000A558C
		public override Type ValueType
		{
			get
			{
				Type valueType = base.ValueType;
				if (valueType != null)
				{
					return valueType;
				}
				return DataGridViewTextBoxCell.defaultValueType;
			}
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000A73B0 File Offset: 0x000A55B0
		internal override void CacheEditingControl()
		{
			this.EditingTextBox = base.DataGridView.EditingControl as DataGridViewTextBoxEditingControl;
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" />.</returns>
		// Token: 0x0600232E RID: 9006 RVA: 0x000A73C8 File Offset: 0x000A55C8
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewTextBoxCell dataGridViewTextBoxCell;
			if (type == DataGridViewTextBoxCell.cellType)
			{
				dataGridViewTextBoxCell = new DataGridViewTextBoxCell();
			}
			else
			{
				dataGridViewTextBoxCell = (DataGridViewTextBoxCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewTextBoxCell);
			dataGridViewTextBoxCell.MaxInputLength = this.MaxInputLength;
			return dataGridViewTextBoxCell;
		}

		/// <summary>Removes the cell's editing control from the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x0600232F RID: 9007 RVA: 0x000A7414 File Offset: 0x000A5614
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override void DetachEditingControl()
		{
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView == null || dataGridView.EditingControl == null)
			{
				throw new InvalidOperationException();
			}
			TextBox textBox = dataGridView.EditingControl as TextBox;
			if (textBox != null)
			{
				textBox.ClearUndo();
			}
			this.EditingTextBox = null;
			base.DetachEditingControl();
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000A745C File Offset: 0x000A565C
		private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds, DataGridViewCellStyle cellStyle)
		{
			TextBox textBox = base.DataGridView.EditingControl as TextBox;
			int width = editingControlBounds.Width;
			if (textBox != null)
			{
				DataGridViewContentAlignment alignment = cellStyle.Alignment;
				if (alignment <= DataGridViewContentAlignment.MiddleCenter)
				{
					switch (alignment)
					{
					case DataGridViewContentAlignment.TopLeft:
						break;
					case DataGridViewContentAlignment.TopCenter:
						goto IL_EF;
					case (DataGridViewContentAlignment)3:
						goto IL_171;
					case DataGridViewContentAlignment.TopRight:
						goto IL_116;
					default:
						if (alignment != DataGridViewContentAlignment.MiddleLeft)
						{
							if (alignment != DataGridViewContentAlignment.MiddleCenter)
							{
								goto IL_171;
							}
							goto IL_EF;
						}
						break;
					}
				}
				else if (alignment <= DataGridViewContentAlignment.BottomLeft)
				{
					if (alignment == DataGridViewContentAlignment.MiddleRight)
					{
						goto IL_116;
					}
					if (alignment != DataGridViewContentAlignment.BottomLeft)
					{
						goto IL_171;
					}
				}
				else
				{
					if (alignment == DataGridViewContentAlignment.BottomCenter)
					{
						goto IL_EF;
					}
					if (alignment != DataGridViewContentAlignment.BottomRight)
					{
						goto IL_171;
					}
					goto IL_116;
				}
				if (base.DataGridView.RightToLeftInternal)
				{
					editingControlBounds.X++;
					editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 3 - 2);
					goto IL_171;
				}
				editingControlBounds.X += 3;
				editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 3 - 1);
				goto IL_171;
				IL_EF:
				editingControlBounds.X++;
				editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 3);
				goto IL_171;
				IL_116:
				if (base.DataGridView.RightToLeftInternal)
				{
					editingControlBounds.X += 3;
					editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 4);
				}
				else
				{
					editingControlBounds.X++;
					editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 4 - 1);
				}
				IL_171:
				DataGridViewContentAlignment alignment2 = cellStyle.Alignment;
				if (alignment2 > DataGridViewContentAlignment.MiddleCenter)
				{
					if (alignment2 <= DataGridViewContentAlignment.BottomLeft)
					{
						if (alignment2 == DataGridViewContentAlignment.MiddleRight)
						{
							goto IL_1FB;
						}
						if (alignment2 != DataGridViewContentAlignment.BottomLeft)
						{
							goto IL_226;
						}
					}
					else if (alignment2 != DataGridViewContentAlignment.BottomCenter && alignment2 != DataGridViewContentAlignment.BottomRight)
					{
						goto IL_226;
					}
					editingControlBounds.Height = Math.Max(0, editingControlBounds.Height - 1);
					goto IL_226;
				}
				if (alignment2 <= DataGridViewContentAlignment.TopRight)
				{
					if (alignment2 - DataGridViewContentAlignment.TopLeft > 1 && alignment2 != DataGridViewContentAlignment.TopRight)
					{
						goto IL_226;
					}
					editingControlBounds.Y += 2;
					editingControlBounds.Height = Math.Max(0, editingControlBounds.Height - 2);
					goto IL_226;
				}
				else if (alignment2 != DataGridViewContentAlignment.MiddleLeft && alignment2 != DataGridViewContentAlignment.MiddleCenter)
				{
					goto IL_226;
				}
				IL_1FB:
				int height = editingControlBounds.Height;
				editingControlBounds.Height = height + 1;
				IL_226:
				int num;
				if (cellStyle.WrapMode == DataGridViewTriState.False)
				{
					num = textBox.PreferredSize.Height;
				}
				else
				{
					string text = (string)((IDataGridViewEditingControl)textBox).GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
					if (string.IsNullOrEmpty(text))
					{
						text = " ";
					}
					TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
					using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
					{
						num = DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, width, textFormatFlags);
					}
				}
				if (num < editingControlBounds.Height)
				{
					DataGridViewContentAlignment alignment3 = cellStyle.Alignment;
					if (alignment3 > DataGridViewContentAlignment.MiddleCenter)
					{
						if (alignment3 <= DataGridViewContentAlignment.BottomLeft)
						{
							if (alignment3 == DataGridViewContentAlignment.MiddleRight)
							{
								goto IL_314;
							}
							if (alignment3 != DataGridViewContentAlignment.BottomLeft)
							{
								return editingControlBounds;
							}
						}
						else if (alignment3 != DataGridViewContentAlignment.BottomCenter && alignment3 != DataGridViewContentAlignment.BottomRight)
						{
							return editingControlBounds;
						}
						editingControlBounds.Y += editingControlBounds.Height - num;
						return editingControlBounds;
					}
					if (alignment3 <= DataGridViewContentAlignment.TopRight)
					{
						if (alignment3 - DataGridViewContentAlignment.TopLeft > 1 && alignment3 != DataGridViewContentAlignment.TopRight)
						{
							return editingControlBounds;
						}
						return editingControlBounds;
					}
					else if (alignment3 != DataGridViewContentAlignment.MiddleLeft && alignment3 != DataGridViewContentAlignment.MiddleCenter)
					{
						return editingControlBounds;
					}
					IL_314:
					editingControlBounds.Y += (editingControlBounds.Height - num) / 2;
				}
			}
			return editingControlBounds;
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> and cell style.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		// Token: 0x06002331 RID: 9009 RVA: 0x000A77C0 File Offset: 0x000A59C0
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
			object formattedValue = this.GetFormattedValue(value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, formattedValue, null, cellStyle, dataGridViewAdvancedBorderStyle, DataGridViewPaintParts.ContentForeground, true, false, false);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		// Token: 0x06002332 RID: 9010 RVA: 0x000A7834 File Offset: 0x000A5A34
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
		// Token: 0x06002333 RID: 9011 RVA: 0x000A78AC File Offset: 0x000A5AAC
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
			object formattedValue = base.GetFormattedValue(rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize);
			string text = formattedValue as string;
			if (string.IsNullOrEmpty(text))
			{
				text = " ";
			}
			TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			Size size;
			if (cellStyle.WrapMode == DataGridViewTriState.True && text.Length > 1)
			{
				if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
				{
					if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
					{
						size = new Size(DataGridViewCell.MeasureTextWidth(graphics, text, cellStyle.Font, Math.Max(1, constraintSize.Height - num2 - 1 - 1), textFormatFlags), 0);
					}
					else
					{
						size = DataGridViewCell.MeasureTextPreferredSize(graphics, text, cellStyle.Font, 5f, textFormatFlags);
					}
				}
				else
				{
					size = new Size(0, DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, Math.Max(1, constraintSize.Width - num), textFormatFlags));
				}
			}
			else if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
				{
					size = new Size(DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags).Width, 0);
				}
				else
				{
					size = DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags);
				}
			}
			else
			{
				size = new Size(0, DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags).Height);
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				size.Width += num;
				if (base.DataGridView.ShowCellErrors)
				{
					size.Width = Math.Max(size.Width, num + 8 + (int)DataGridViewCell.iconsWidth);
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				int num3 = ((cellStyle.WrapMode == DataGridViewTriState.True) ? 1 : 2);
				size.Height += num3 + 1 + num2;
				if (base.DataGridView.ShowCellErrors)
				{
					size.Height = Math.Max(size.Height, num2 + 8 + (int)DataGridViewCell.iconsHeight);
				}
			}
			return size;
		}

		/// <summary>Attaches and initializes the hosted editing control.</summary>
		/// <param name="rowIndex">The index of the row being edited.</param>
		/// <param name="initialFormattedValue">The initial value to be displayed in the control.</param>
		/// <param name="dataGridViewCellStyle">A cell style that is used to determine the appearance of the hosted control.</param>
		// Token: 0x06002334 RID: 9012 RVA: 0x000A7AEC File Offset: 0x000A5CEC
		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			TextBox textBox = base.DataGridView.EditingControl as TextBox;
			if (textBox != null)
			{
				textBox.BorderStyle = BorderStyle.None;
				textBox.AcceptsReturn = (textBox.Multiline = dataGridViewCellStyle.WrapMode == DataGridViewTriState.True);
				textBox.MaxLength = this.MaxInputLength;
				string text = initialFormattedValue as string;
				if (text == null)
				{
					textBox.Text = string.Empty;
				}
				else
				{
					textBox.Text = text;
				}
				this.EditingTextBox = base.DataGridView.EditingControl as DataGridViewTextBoxEditingControl;
			}
		}

		/// <summary>Determines if edit mode should be started based on the given key.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that represents the key that was pressed.</param>
		/// <returns>
		///   <see langword="true" /> if edit mode should be started; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002335 RID: 9013 RVA: 0x000A7B78 File Offset: 0x000A5D78
		public override bool KeyEntersEditMode(KeyEventArgs e)
		{
			return (((char.IsLetterOrDigit((char)e.KeyCode) && (e.KeyCode < Keys.F1 || e.KeyCode > Keys.F24)) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.Divide) || (e.KeyCode >= Keys.OemSemicolon && e.KeyCode <= Keys.OemBackslash) || (e.KeyCode == Keys.Space && !e.Shift)) && !e.Alt && !e.Control) || base.KeyEntersEditMode(e);
		}

		/// <summary>Called by <see cref="T:System.Windows.Forms.DataGridView" /> when the selection cursor moves onto a cell.</summary>
		/// <param name="rowIndex">The index of the row entered by the mouse.</param>
		/// <param name="throughMouseClick">
		///   <see langword="true" /> if the cell was entered as a result of a mouse click; otherwise, <see langword="false" />.</param>
		// Token: 0x06002336 RID: 9014 RVA: 0x000A7C03 File Offset: 0x000A5E03
		protected override void OnEnter(int rowIndex, bool throughMouseClick)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (throughMouseClick)
			{
				this.flagsState |= 1;
			}
		}

		/// <summary>Called by the <see cref="T:System.Windows.Forms.DataGridView" /> when the mouse leaves a cell.</summary>
		/// <param name="rowIndex">The index of the row the mouse has left.</param>
		/// <param name="throughMouseClick">
		///   <see langword="true" /> if the cell was left as a result of a mouse click; otherwise, <see langword="false" />.</param>
		// Token: 0x06002337 RID: 9015 RVA: 0x000A7C20 File Offset: 0x000A5E20
		protected override void OnLeave(int rowIndex, bool throughMouseClick)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			this.flagsState = (byte)((int)this.flagsState & -2);
		}

		/// <summary>Called by <see cref="T:System.Windows.Forms.DataGridView" /> when the mouse leaves a cell.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06002338 RID: 9016 RVA: 0x000A7C3C File Offset: 0x000A5E3C
		protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			if (currentCellAddress.X == e.ColumnIndex && currentCellAddress.Y == e.RowIndex && e.Button == MouseButtons.Left)
			{
				if ((this.flagsState & 1) != 0)
				{
					this.flagsState = (byte)((int)this.flagsState & -2);
					return;
				}
				if (base.DataGridView.EditMode != DataGridViewEditMode.EditProgrammatically)
				{
					base.DataGridView.BeginEdit(true);
				}
			}
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000A7CBF File Offset: 0x000A5EBF
		private bool OwnsEditingTextBox(int rowIndex)
		{
			return rowIndex != -1 && this.EditingTextBox != null && rowIndex == ((IDataGridViewEditingControl)this.EditingTextBox).EditingControlRowIndex;
		}

		/// <summary>Paints the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="cellState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
		/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
		// Token: 0x0600233A RID: 9018 RVA: 0x000A7CE0 File Offset: 0x000A5EE0
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000A7D18 File Offset: 0x000A5F18
		private Rectangle PaintPrivate(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			Rectangle rectangle = Rectangle.Empty;
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
			Rectangle rectangle3 = cellBounds;
			rectangle3.Offset(rectangle2.X, rectangle2.Y);
			rectangle3.Width -= rectangle2.Right;
			rectangle3.Height -= rectangle2.Bottom;
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			bool flag = currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex;
			bool flag2 = flag && base.DataGridView.EditingControl != null;
			bool flag3 = (cellState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			SolidBrush solidBrush;
			if (DataGridViewCell.PaintSelectionBackground(paintParts) && flag3 && !flag2)
			{
				solidBrush = base.DataGridView.GetCachedBrush(cellStyle.SelectionBackColor);
			}
			else
			{
				solidBrush = base.DataGridView.GetCachedBrush(cellStyle.BackColor);
			}
			if (paint && DataGridViewCell.PaintBackground(paintParts) && solidBrush.Color.A == 255 && rectangle3.Width > 0 && rectangle3.Height > 0)
			{
				graphics.FillRectangle(solidBrush, rectangle3);
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
			if (paint && flag && !flag2 && DataGridViewCell.PaintFocus(paintParts) && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && rectangle3.Width > 0 && rectangle3.Height > 0)
			{
				ControlPaint.DrawFocusRectangle(graphics, rectangle3, Color.Empty, solidBrush.Color);
			}
			Rectangle rectangle4 = rectangle3;
			string text = formattedValue as string;
			if (text != null && ((paint && !flag2) || computeContentBounds))
			{
				int num = ((cellStyle.WrapMode == DataGridViewTriState.True) ? 1 : 2);
				rectangle3.Offset(0, num);
				rectangle3.Width = rectangle3.Width;
				rectangle3.Height -= num + 1;
				if (rectangle3.Width > 0 && rectangle3.Height > 0)
				{
					TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
					if (paint)
					{
						if (DataGridViewCell.PaintContentForeground(paintParts))
						{
							if ((textFormatFlags & TextFormatFlags.SingleLine) != TextFormatFlags.Default)
							{
								textFormatFlags |= TextFormatFlags.EndEllipsis;
							}
							TextRenderer.DrawText(graphics, text, cellStyle.Font, rectangle3, flag3 ? cellStyle.SelectionForeColor : cellStyle.ForeColor, textFormatFlags);
						}
					}
					else
					{
						rectangle = DataGridViewUtilities.GetTextBounds(rectangle3, text, textFormatFlags, cellStyle);
					}
				}
			}
			else if (computeErrorIconBounds && !string.IsNullOrEmpty(errorText))
			{
				rectangle = base.ComputeErrorIconBounds(rectangle4);
			}
			if (base.DataGridView.ShowCellErrors && paint && DataGridViewCell.PaintErrorIcon(paintParts))
			{
				base.PaintErrorIcon(graphics, cellStyle, rowIndex, cellBounds, rectangle4, errorText);
			}
			return rectangle;
		}

		/// <summary>Sets the location and size of the editing control hosted by a cell in the DataGridView control.</summary>
		/// <param name="setLocation">
		///   <see langword="true" /> to have the control placed as specified by the other arguments; <see langword="false" /> to allow the control to place itself.</param>
		/// <param name="setSize">
		///   <see langword="true" /> to specify the size; <see langword="false" /> to allow the control to size itself.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that defines the cell bounds.</param>
		/// <param name="cellClip">The area that will be used to paint the editing control.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell being edited.</param>
		/// <param name="singleVerticalBorderAdded">
		///   <see langword="true" /> to add a vertical border to the cell; otherwise, <see langword="false" />.</param>
		/// <param name="singleHorizontalBorderAdded">
		///   <see langword="true" /> to add a horizontal border to the cell; otherwise, <see langword="false" />.</param>
		/// <param name="isFirstDisplayedColumn">
		///   <see langword="true" /> if the hosting cell is in the first visible column; otherwise, <see langword="false" />.</param>
		/// <param name="isFirstDisplayedRow">
		///   <see langword="true" /> if the hosting cell is in the first visible row; otherwise, <see langword="false" />.</param>
		// Token: 0x0600233C RID: 9020 RVA: 0x000A8088 File Offset: 0x000A6288
		public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			Rectangle rectangle = this.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
			rectangle = this.GetAdjustedEditingControlBounds(rectangle, cellStyle);
			base.DataGridView.EditingControl.Location = new Point(rectangle.X, rectangle.Y);
			base.DataGridView.EditingControl.Size = new Size(rectangle.Width, rectangle.Height);
		}

		/// <summary>Returns a string that describes the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x0600233D RID: 9021 RVA: 0x000A80FC File Offset: 0x000A62FC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewTextBoxCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x04000E6B RID: 3691
		private static readonly int PropTextBoxCellMaxInputLength = PropertyStore.CreateKey();

		// Token: 0x04000E6C RID: 3692
		private static readonly int PropTextBoxCellEditingTextBox = PropertyStore.CreateKey();

		// Token: 0x04000E6D RID: 3693
		private const byte DATAGRIDVIEWTEXTBOXCELL_ignoreNextMouseClick = 1;

		// Token: 0x04000E6E RID: 3694
		private const byte DATAGRIDVIEWTEXTBOXCELL_horizontalTextOffsetLeft = 3;

		// Token: 0x04000E6F RID: 3695
		private const byte DATAGRIDVIEWTEXTBOXCELL_horizontalTextOffsetRight = 4;

		// Token: 0x04000E70 RID: 3696
		private const byte DATAGRIDVIEWTEXTBOXCELL_horizontalTextMarginLeft = 0;

		// Token: 0x04000E71 RID: 3697
		private const byte DATAGRIDVIEWTEXTBOXCELL_horizontalTextMarginRight = 0;

		// Token: 0x04000E72 RID: 3698
		private const byte DATAGRIDVIEWTEXTBOXCELL_verticalTextOffsetTop = 2;

		// Token: 0x04000E73 RID: 3699
		private const byte DATAGRIDVIEWTEXTBOXCELL_verticalTextOffsetBottom = 1;

		// Token: 0x04000E74 RID: 3700
		private const byte DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginTopWithWrapping = 1;

		// Token: 0x04000E75 RID: 3701
		private const byte DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginTopWithoutWrapping = 2;

		// Token: 0x04000E76 RID: 3702
		private const byte DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginBottom = 1;

		// Token: 0x04000E77 RID: 3703
		private const int DATAGRIDVIEWTEXTBOXCELL_maxInputLength = 32767;

		// Token: 0x04000E78 RID: 3704
		private byte flagsState;

		// Token: 0x04000E79 RID: 3705
		private static Type defaultFormattedValueType = typeof(string);

		// Token: 0x04000E7A RID: 3706
		private static Type defaultValueType = typeof(object);

		// Token: 0x04000E7B RID: 3707
		private static Type cellType = typeof(DataGridViewTextBoxCell);

		/// <summary>Represents the accessibility object for the current <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" /> object.</summary>
		// Token: 0x0200067A RID: 1658
		protected class DataGridViewTextBoxCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Instantiates a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell.DataGridViewTextBoxCellAccessibleObject" /> class.</summary>
			/// <param name="owner">The control to which this object belongs.</param>
			// Token: 0x060066AA RID: 26282 RVA: 0x0017BC46 File Offset: 0x00179E46
			public DataGridViewTextBoxCellAccessibleObject(DataGridViewCell owner)
				: base(owner)
			{
			}

			// Token: 0x060066AB RID: 26283 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override bool IsIAccessibleExSupported()
			{
				return true;
			}

			// Token: 0x060066AC RID: 26284 RVA: 0x0017FA0F File Offset: 0x0017DC0F
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50004;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}
