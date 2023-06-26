using System;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Represents the cell in the top left corner of the <see cref="T:System.Windows.Forms.DataGridView" /> that sits above the row headers and to the left of the column headers.</summary>
	// Token: 0x0200021F RID: 543
	public class DataGridViewTopLeftHeaderCell : DataGridViewColumnHeaderCell
	{
		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />.</returns>
		// Token: 0x0600236A RID: 9066 RVA: 0x000A8831 File Offset: 0x000A6A31
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject(this);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> object and cell style.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> does not equal -1.</exception>
		// Token: 0x0600236B RID: 9067 RVA: 0x000A883C File Offset: 0x000A6A3C
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, value, null, cellStyle, dataGridViewAdvancedBorderStyle, DataGridViewPaintParts.ContentForeground, true, false, false);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> does not equal -1.</exception>
		// Token: 0x0600236C RID: 9068 RVA: 0x000A88A0 File Offset: 0x000A6AA0
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
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
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> does not equal -1.</exception>
		// Token: 0x0600236D RID: 9069 RVA: 0x000A8900 File Offset: 0x000A6B00
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			Rectangle rectangle = this.BorderWidths(base.DataGridView.AdjustedTopLeftHeaderBorderStyle);
			int num = rectangle.Left + rectangle.Width + cellStyle.Padding.Horizontal;
			int num2 = rectangle.Top + rectangle.Height + cellStyle.Padding.Vertical;
			TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			object obj = this.GetValue(rowIndex);
			if (!(obj is string))
			{
				obj = null;
			}
			return DataGridViewUtilities.GetPreferredRowHeaderSize(graphics, (string)obj, cellStyle, num, num2, base.DataGridView.ShowCellErrors, false, constraintSize, textFormatFlags);
		}

		/// <summary>Paints the current <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />.</summary>
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
		// Token: 0x0600236E RID: 9070 RVA: 0x000A89E0 File Offset: 0x000A6BE0
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000A8A18 File Offset: 0x000A6C18
		private Rectangle PaintPrivate(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			Rectangle rectangle = Rectangle.Empty;
			Rectangle rectangle2 = cellBounds;
			Rectangle rectangle3 = this.BorderWidths(advancedBorderStyle);
			rectangle2.Offset(rectangle3.X, rectangle3.Y);
			rectangle2.Width -= rectangle3.Right;
			rectangle2.Height -= rectangle3.Bottom;
			bool flag = (cellState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			if (paint && DataGridViewCell.PaintBackground(paintParts))
			{
				if (base.DataGridView.ApplyVisualStylesToHeaderCells)
				{
					int num = 1;
					if (base.ButtonState != ButtonState.Normal)
					{
						num = 3;
					}
					else if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex)
					{
						num = 2;
					}
					rectangle2.Inflate(16, 16);
					DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.DrawHeader(graphics, rectangle2, num);
					rectangle2.Inflate(-16, -16);
				}
				else
				{
					SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
					if (cachedBrush.Color.A == 255)
					{
						graphics.FillRectangle(cachedBrush, rectangle2);
					}
				}
			}
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			if (cellStyle.Padding != Padding.Empty)
			{
				if (base.DataGridView.RightToLeftInternal)
				{
					rectangle2.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
				}
				else
				{
					rectangle2.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
				}
				rectangle2.Width -= cellStyle.Padding.Horizontal;
				rectangle2.Height -= cellStyle.Padding.Vertical;
			}
			Rectangle rectangle4 = rectangle2;
			string text = formattedValue as string;
			rectangle2.Offset(1, 1);
			rectangle2.Width -= 3;
			rectangle2.Height -= 2;
			if (rectangle2.Width > 0 && rectangle2.Height > 0 && !string.IsNullOrEmpty(text) && (paint || computeContentBounds))
			{
				Color color;
				if (base.DataGridView.ApplyVisualStylesToHeaderCells)
				{
					color = DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
				}
				else
				{
					color = (flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
				}
				TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
				if (paint)
				{
					if (DataGridViewCell.PaintContentForeground(paintParts))
					{
						if ((textFormatFlags & TextFormatFlags.SingleLine) != TextFormatFlags.Default)
						{
							textFormatFlags |= TextFormatFlags.EndEllipsis;
						}
						TextRenderer.DrawText(graphics, text, cellStyle.Font, rectangle2, color, textFormatFlags);
					}
				}
				else
				{
					rectangle = DataGridViewUtilities.GetTextBounds(rectangle2, text, textFormatFlags, cellStyle);
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

		/// <summary>Paints the border of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the border.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the area of the border that is being painted.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles of the border that is being painted.</param>
		// Token: 0x06002370 RID: 9072 RVA: 0x000A8D54 File Offset: 0x000A6F54
		protected override void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			base.PaintBorder(graphics, clipBounds, bounds, cellStyle, advancedBorderStyle);
			if (!base.DataGridView.RightToLeftInternal && base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				if (base.DataGridView.AdvancedColumnHeadersBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Inset)
				{
					Pen pen = null;
					Pen pen2 = null;
					base.GetContrastedPens(cellStyle.BackColor, ref pen, ref pen2);
					graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
					graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
					return;
				}
				if (base.DataGridView.AdvancedColumnHeadersBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Outset)
				{
					Pen pen3 = null;
					Pen pen4 = null;
					base.GetContrastedPens(cellStyle.BackColor, ref pen3, ref pen4);
					graphics.DrawLine(pen4, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
					graphics.DrawLine(pen4, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
					return;
				}
				if (base.DataGridView.AdvancedColumnHeadersBorderStyle.All == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					Pen pen5 = null;
					Pen pen6 = null;
					base.GetContrastedPens(cellStyle.BackColor, ref pen5, ref pen6);
					graphics.DrawLine(pen5, bounds.X + 1, bounds.Y + 1, bounds.X + 1, bounds.Bottom - 1);
					graphics.DrawLine(pen5, bounds.X + 1, bounds.Y + 1, bounds.Right - 1, bounds.Y + 1);
				}
			}
		}

		/// <summary>Returns the string representation of the cell.</summary>
		/// <returns>A string that represents the current cell.</returns>
		// Token: 0x06002371 RID: 9073 RVA: 0x000A8EFF File Offset: 0x000A70FF
		public override string ToString()
		{
			return "DataGridViewTopLeftHeaderCell";
		}

		// Token: 0x04000E86 RID: 3718
		private static readonly VisualStyleElement HeaderElement = VisualStyleElement.Header.Item.Normal;

		// Token: 0x04000E87 RID: 3719
		private const byte DATAGRIDVIEWTOPLEFTHEADERCELL_horizontalTextMarginLeft = 1;

		// Token: 0x04000E88 RID: 3720
		private const byte DATAGRIDVIEWTOPLEFTHEADERCELL_horizontalTextMarginRight = 2;

		// Token: 0x04000E89 RID: 3721
		private const byte DATAGRIDVIEWTOPLEFTHEADERCELL_verticalTextMargin = 1;

		// Token: 0x0200067B RID: 1659
		private class DataGridViewTopLeftHeaderCellRenderer
		{
			// Token: 0x060066AD RID: 26285 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewTopLeftHeaderCellRenderer()
			{
			}

			// Token: 0x17001664 RID: 5732
			// (get) Token: 0x060066AE RID: 26286 RVA: 0x0017FA2B File Offset: 0x0017DC2B
			public static VisualStyleRenderer VisualStyleRenderer
			{
				get
				{
					if (DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewTopLeftHeaderCell.HeaderElement);
					}
					return DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x060066AF RID: 26287 RVA: 0x0017FA48 File Offset: 0x0017DC48
			public static void DrawHeader(Graphics g, Rectangle bounds, int headerState)
			{
				DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.VisualStyleRenderer.SetParameters(DataGridViewTopLeftHeaderCell.HeaderElement.ClassName, DataGridViewTopLeftHeaderCell.HeaderElement.Part, headerState);
				DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.VisualStyleRenderer.DrawBackground(g, bounds, Rectangle.Truncate(g.ClipBounds));
			}

			// Token: 0x04003A78 RID: 14968
			private static VisualStyleRenderer visualStyleRenderer;
		}

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" /> to accessibility client applications.</summary>
		// Token: 0x0200067C RID: 1660
		protected class DataGridViewTopLeftHeaderCellAccessibleObject : DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</param>
			// Token: 0x060066B0 RID: 26288 RVA: 0x0017FA80 File Offset: 0x0017DC80
			public DataGridViewTopLeftHeaderCellAccessibleObject(DataGridViewTopLeftHeaderCell owner)
				: base(owner)
			{
			}

			/// <summary>Gets the location and size of the accessible object.</summary>
			/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
			// Token: 0x17001665 RID: 5733
			// (get) Token: 0x060066B1 RID: 26289 RVA: 0x0017FA8C File Offset: 0x0017DC8C
			public override Rectangle Bounds
			{
				get
				{
					Rectangle cellDisplayRectangle = base.Owner.DataGridView.GetCellDisplayRectangle(-1, -1, false);
					return base.Owner.DataGridView.RectangleToScreen(cellDisplayRectangle);
				}
			}

			/// <summary>Gets a description of the default action of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</summary>
			/// <returns>The string "Press to Select All" if the <see cref="P:System.Windows.Forms.DataGridView.MultiSelect" /> property is <see langword="true" />; otherwise, an empty string ("").</returns>
			// Token: 0x17001666 RID: 5734
			// (get) Token: 0x060066B2 RID: 26290 RVA: 0x0017FABE File Offset: 0x0017DCBE
			public override string DefaultAction
			{
				get
				{
					if (base.Owner.DataGridView.MultiSelect)
					{
						return SR.GetString("DataGridView_AccTopLeftColumnHeaderCellDefaultAction");
					}
					return string.Empty;
				}
			}

			/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</summary>
			/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</returns>
			// Token: 0x17001667 RID: 5735
			// (get) Token: 0x060066B3 RID: 26291 RVA: 0x0017FAE4 File Offset: 0x0017DCE4
			public override string Name
			{
				get
				{
					object value = base.Owner.Value;
					if (value != null && !(value is string))
					{
						return string.Empty;
					}
					string text = value as string;
					if (!string.IsNullOrEmpty(text))
					{
						return string.Empty;
					}
					if (base.Owner.DataGridView == null)
					{
						return string.Empty;
					}
					if (base.Owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return SR.GetString("DataGridView_AccTopLeftColumnHeaderCellName");
					}
					return SR.GetString("DataGridView_AccTopLeftColumnHeaderCellNameRTL");
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</summary>
			/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values. The default is <see cref="F:System.Windows.Forms.AccessibleStates.Selectable" />.</returns>
			// Token: 0x17001668 RID: 5736
			// (get) Token: 0x060066B4 RID: 26292 RVA: 0x0017FB60 File Offset: 0x0017DD60
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Selectable;
					AccessibleStates state = base.State;
					if ((state & AccessibleStates.Offscreen) == AccessibleStates.Offscreen)
					{
						accessibleStates |= AccessibleStates.Offscreen;
					}
					if (base.Owner.DataGridView.AreAllCellsSelected(false))
					{
						accessibleStates |= AccessibleStates.Selected;
					}
					return accessibleStates;
				}
			}

			/// <summary>The value of the containing <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />.</summary>
			/// <returns>Always returns <see cref="F:System.String.Empty" />.</returns>
			// Token: 0x17001669 RID: 5737
			// (get) Token: 0x060066B5 RID: 26293 RVA: 0x0017E1A1 File Offset: 0x0017C3A1
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return string.Empty;
				}
			}

			/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />.</summary>
			// Token: 0x060066B6 RID: 26294 RVA: 0x0017FBA8 File Offset: 0x0017DDA8
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				base.Owner.DataGridView.SelectAll();
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents an object in the specified direction.</returns>
			// Token: 0x060066B7 RID: 26295 RVA: 0x0017FBBC File Offset: 0x0017DDBC
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				switch (navigationDirection)
				{
				case AccessibleNavigation.Left:
					if (base.Owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return null;
					}
					return this.NavigateForward();
				case AccessibleNavigation.Right:
					if (base.Owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return this.NavigateForward();
					}
					return null;
				case AccessibleNavigation.Next:
					return this.NavigateForward();
				case AccessibleNavigation.Previous:
					return null;
				default:
					return null;
				}
			}

			// Token: 0x060066B8 RID: 26296 RVA: 0x0017FC23 File Offset: 0x0017DE23
			private AccessibleObject NavigateForward()
			{
				if (base.Owner.DataGridView.Columns.GetColumnCount(DataGridViewElementStates.Visible) == 0)
				{
					return null;
				}
				return base.Owner.DataGridView.AccessibilityObject.GetChild(0).GetChild(1);
			}

			/// <summary>Modifies the selection in the <see cref="T:System.Windows.Forms.DataGridView" /> control or sets input focus to the control.</summary>
			/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property value is <see langword="null" />.</exception>
			// Token: 0x060066B9 RID: 26297 RVA: 0x0017FC5C File Offset: 0x0017DE5C
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if (base.Owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
				{
					base.Owner.DataGridView.FocusInternal();
					if (base.Owner.DataGridView.Columns.GetColumnCount(DataGridViewElementStates.Visible) > 0 && base.Owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible) > 0)
					{
						DataGridViewRow dataGridViewRow = base.Owner.DataGridView.Rows[base.Owner.DataGridView.Rows.GetFirstRow(DataGridViewElementStates.Visible)];
						DataGridViewColumn firstColumn = base.Owner.DataGridView.Columns.GetFirstColumn(DataGridViewElementStates.Visible);
						base.Owner.DataGridView.SetCurrentCellAddressCoreInternal(firstColumn.Index, dataGridViewRow.Index, false, true, false);
					}
				}
				if ((flags & AccessibleSelection.AddSelection) == AccessibleSelection.AddSelection && base.Owner.DataGridView.MultiSelect)
				{
					base.Owner.DataGridView.SelectAll();
				}
				if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection && (flags & AccessibleSelection.AddSelection) == AccessibleSelection.None)
				{
					base.Owner.DataGridView.ClearSelection();
				}
			}

			// Token: 0x060066BA RID: 26298 RVA: 0x0017FD78 File Offset: 0x0017DF78
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				DataGridView dataGridView = base.Owner.DataGridView;
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return dataGridView.AccessibilityObject.GetChild(0);
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					if (dataGridView.Columns.GetColumnCount(DataGridViewElementStates.Visible) == 0)
					{
						return null;
					}
					return this.NavigateForward();
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					return null;
				default:
					return null;
				}
			}

			// Token: 0x060066BB RID: 26299 RVA: 0x0017FDD0 File Offset: 0x0017DFD0
			internal override object GetPropertyValue(int propertyId)
			{
				if (AccessibilityImprovements.Level3)
				{
					switch (propertyId)
					{
					case 30003:
						return 50034;
					case 30004:
					case 30006:
					case 30008:
					case 30011:
					case 30012:
						goto IL_99;
					case 30005:
						return this.Name;
					case 30007:
						return string.Empty;
					case 30009:
						break;
					case 30010:
						return base.Owner.DataGridView.Enabled;
					case 30013:
						return this.Help ?? string.Empty;
					default:
						if (propertyId != 30019 && propertyId != 30022)
						{
							goto IL_99;
						}
						break;
					}
					return false;
				}
				IL_99:
				return base.GetPropertyValue(propertyId);
			}
		}
	}
}
