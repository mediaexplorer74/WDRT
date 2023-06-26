using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Represents a column header in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001C3 RID: 451
	public class DataGridViewColumnHeaderCell : DataGridViewHeaderCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> class.</summary>
		// Token: 0x06001FAD RID: 8109 RVA: 0x00095A08 File Offset: 0x00093C08
		public DataGridViewColumnHeaderCell()
		{
			if (!DataGridViewColumnHeaderCell.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth = (byte)DpiHelper.LogicalToDeviceUnitsX(2);
					DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin = (byte)DpiHelper.LogicalToDeviceUnitsX(4);
					DataGridViewColumnHeaderCell.sortGlyphWidth = (byte)DpiHelper.LogicalToDeviceUnitsX(9);
					if (DataGridViewColumnHeaderCell.sortGlyphWidth % 2 == 0)
					{
						DataGridViewColumnHeaderCell.sortGlyphWidth += 1;
					}
					DataGridViewColumnHeaderCell.sortGlyphHeight = (byte)DpiHelper.LogicalToDeviceUnitsY(7);
				}
				DataGridViewColumnHeaderCell.isScalingInitialized = true;
			}
			this.sortGlyphDirection = SortOrder.None;
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x00095A7D File Offset: 0x00093C7D
		internal bool ContainsLocalValue
		{
			get
			{
				return base.Properties.ContainsObject(DataGridViewCell.PropCellValue);
			}
		}

		/// <summary>Gets or sets a value indicating which sort glyph is displayed.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.SortOrder" /> value representing the current glyph. The default is <see cref="F:System.Windows.Forms.SortOrder.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.SortOrder" /> value.</exception>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the value of either the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningColumn" /> property or the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is <see langword="null" />.  
		///  -or-  
		///  When changing the value of this property, the specified value is not <see cref="F:System.Windows.Forms.SortOrder.None" /> and the value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property of the owning column is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.NotSortable" />.</exception>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x00095A8F File Offset: 0x00093C8F
		// (set) Token: 0x06001FB0 RID: 8112 RVA: 0x00095A98 File Offset: 0x00093C98
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SortOrder SortGlyphDirection
		{
			get
			{
				return this.sortGlyphDirection;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(SortOrder));
				}
				if (base.OwningColumn == null || base.DataGridView == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_CellDoesNotYetBelongToDataGridView"));
				}
				if (value != this.sortGlyphDirection)
				{
					if (base.OwningColumn.SortMode == DataGridViewColumnSortMode.NotSortable && value != SortOrder.None)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewColumnHeaderCell_SortModeAndSortGlyphDirectionClash", new object[] { value.ToString() }));
					}
					this.sortGlyphDirection = value;
					base.DataGridView.OnSortGlyphDirectionChanged(this);
				}
			}
		}

		// Token: 0x17000719 RID: 1817
		// (set) Token: 0x06001FB1 RID: 8113 RVA: 0x00095B3D File Offset: 0x00093D3D
		internal SortOrder SortGlyphDirectionInternal
		{
			set
			{
				this.sortGlyphDirection = value;
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" />.</returns>
		// Token: 0x06001FB2 RID: 8114 RVA: 0x00095B48 File Offset: 0x00093D48
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewColumnHeaderCell dataGridViewColumnHeaderCell;
			if (type == DataGridViewColumnHeaderCell.cellType)
			{
				dataGridViewColumnHeaderCell = new DataGridViewColumnHeaderCell();
			}
			else
			{
				dataGridViewColumnHeaderCell = (DataGridViewColumnHeaderCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewColumnHeaderCell);
			dataGridViewColumnHeaderCell.Value = base.Value;
			return dataGridViewColumnHeaderCell;
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" />.</returns>
		// Token: 0x06001FB3 RID: 8115 RVA: 0x00095B91 File Offset: 0x00093D91
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject(this);
		}

		/// <summary>Retrieves the formatted value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</summary>
		/// <param name="rowIndex">The zero-based index of the row containing the cell.</param>
		/// <param name="firstCell">
		///   <see langword="true" /> to indicate that the cell is in the first column of the region defined by the selected cells; otherwise, <see langword="false" />.</param>
		/// <param name="lastCell">
		///   <see langword="true" /> to indicate that the cell is the last column of the region defined by the selected cells; otherwise, <see langword="false" />.</param>
		/// <param name="inFirstRow">
		///   <see langword="true" /> to indicate that the cell is in the first row of the region defined by the selected cells; otherwise, <see langword="false" />.</param>
		/// <param name="inLastRow">
		///   <see langword="true" /> to indicate that the cell is in the last row of the region defined by the selected cells; otherwise, <see langword="false" />.</param>
		/// <param name="format">The current format string of the cell.</param>
		/// <returns>A <see cref="T:System.Object" /> that represents the value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not -1.</exception>
		// Token: 0x06001FB4 RID: 8116 RVA: 0x00095B9C File Offset: 0x00093D9C
		protected override object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView == null)
			{
				return null;
			}
			object value = this.GetValue(rowIndex);
			StringBuilder stringBuilder = new StringBuilder(64);
			if (string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
			{
				if (firstCell)
				{
					stringBuilder.Append("<TABLE>");
					stringBuilder.Append("<THEAD>");
				}
				stringBuilder.Append("<TH>");
				if (value != null)
				{
					DataGridViewCell.FormatPlainTextAsHtml(value.ToString(), new StringWriter(stringBuilder, CultureInfo.CurrentCulture));
				}
				else
				{
					stringBuilder.Append("&nbsp;");
				}
				stringBuilder.Append("</TH>");
				if (lastCell)
				{
					stringBuilder.Append("</THEAD>");
					if (inLastRow)
					{
						stringBuilder.Append("</TABLE>");
					}
				}
				return stringBuilder.ToString();
			}
			bool flag = string.Equals(format, DataFormats.CommaSeparatedValue, StringComparison.OrdinalIgnoreCase);
			if (flag || string.Equals(format, DataFormats.Text, StringComparison.OrdinalIgnoreCase) || string.Equals(format, DataFormats.UnicodeText, StringComparison.OrdinalIgnoreCase))
			{
				if (value != null)
				{
					bool flag2 = false;
					int length = stringBuilder.Length;
					DataGridViewCell.FormatPlainText(value.ToString(), flag, new StringWriter(stringBuilder, CultureInfo.CurrentCulture), ref flag2);
					if (flag2)
					{
						stringBuilder.Insert(length, '"');
					}
				}
				if (lastCell)
				{
					if (!inLastRow)
					{
						stringBuilder.Append('\r');
						stringBuilder.Append('\n');
					}
				}
				else
				{
					stringBuilder.Append(flag ? ',' : '\t');
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> object and cell style.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not -1.</exception>
		// Token: 0x06001FB5 RID: 8117 RVA: 0x00095CFC File Offset: 0x00093EFC
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
			if (base.DataGridView == null || base.OwningColumn == null)
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, value, cellStyle, dataGridViewAdvancedBorderStyle, DataGridViewPaintParts.ContentForeground, false);
		}

		/// <summary>Retrieves the inherited shortcut menu for the specified row.</summary>
		/// <param name="rowIndex">The index of the row to get the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> of. The index must be -1 to indicate the row of column headers.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> of the column headers if one exists; otherwise, the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> inherited from <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not -1.</exception>
		// Token: 0x06001FB6 RID: 8118 RVA: 0x00095D68 File Offset: 0x00093F68
		public override ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			ContextMenuStrip contextMenuStrip = base.GetContextMenuStrip(-1);
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

		/// <summary>Gets the style applied to the cell.</summary>
		/// <param name="inheritedCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be populated with the inherited cell style.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="includeColors">
		///   <see langword="true" /> to include inherited colors in the returned cell style; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that includes the style settings of the cell inherited from the cell's parent row, column, and <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not -1.</exception>
		// Token: 0x06001FB7 RID: 8119 RVA: 0x00095DA8 File Offset: 0x00093FA8
		public override DataGridViewCellStyle GetInheritedStyle(DataGridViewCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			DataGridViewCellStyle dataGridViewCellStyle = ((inheritedCellStyle == null) ? new DataGridViewCellStyle() : inheritedCellStyle);
			DataGridViewCellStyle dataGridViewCellStyle2 = null;
			if (base.HasStyle)
			{
				dataGridViewCellStyle2 = base.Style;
			}
			DataGridViewCellStyle columnHeadersDefaultCellStyle = base.DataGridView.ColumnHeadersDefaultCellStyle;
			DataGridViewCellStyle defaultCellStyle = base.DataGridView.DefaultCellStyle;
			if (includeColors)
			{
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = dataGridViewCellStyle2.BackColor;
				}
				else if (!columnHeadersDefaultCellStyle.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = columnHeadersDefaultCellStyle.BackColor;
				}
				else
				{
					dataGridViewCellStyle.BackColor = defaultCellStyle.BackColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = dataGridViewCellStyle2.ForeColor;
				}
				else if (!columnHeadersDefaultCellStyle.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = columnHeadersDefaultCellStyle.ForeColor;
				}
				else
				{
					dataGridViewCellStyle.ForeColor = defaultCellStyle.ForeColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = dataGridViewCellStyle2.SelectionBackColor;
				}
				else if (!columnHeadersDefaultCellStyle.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = columnHeadersDefaultCellStyle.SelectionBackColor;
				}
				else
				{
					dataGridViewCellStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = dataGridViewCellStyle2.SelectionForeColor;
				}
				else if (!columnHeadersDefaultCellStyle.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = columnHeadersDefaultCellStyle.SelectionForeColor;
				}
				else
				{
					dataGridViewCellStyle.SelectionForeColor = defaultCellStyle.SelectionForeColor;
				}
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Font != null)
			{
				dataGridViewCellStyle.Font = dataGridViewCellStyle2.Font;
			}
			else if (columnHeadersDefaultCellStyle.Font != null)
			{
				dataGridViewCellStyle.Font = columnHeadersDefaultCellStyle.Font;
			}
			else
			{
				dataGridViewCellStyle.Font = defaultCellStyle.Font;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = dataGridViewCellStyle2.NullValue;
			}
			else if (!columnHeadersDefaultCellStyle.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = columnHeadersDefaultCellStyle.NullValue;
			}
			else
			{
				dataGridViewCellStyle.NullValue = defaultCellStyle.NullValue;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = dataGridViewCellStyle2.DataSourceNullValue;
			}
			else if (!columnHeadersDefaultCellStyle.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = columnHeadersDefaultCellStyle.DataSourceNullValue;
			}
			else
			{
				dataGridViewCellStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = dataGridViewCellStyle2.Format;
			}
			else if (columnHeadersDefaultCellStyle.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = columnHeadersDefaultCellStyle.Format;
			}
			else
			{
				dataGridViewCellStyle.Format = defaultCellStyle.Format;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = dataGridViewCellStyle2.FormatProvider;
			}
			else if (!columnHeadersDefaultCellStyle.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = columnHeadersDefaultCellStyle.FormatProvider;
			}
			else
			{
				dataGridViewCellStyle.FormatProvider = defaultCellStyle.FormatProvider;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = dataGridViewCellStyle2.Alignment;
			}
			else if (columnHeadersDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = columnHeadersDefaultCellStyle.Alignment;
			}
			else
			{
				dataGridViewCellStyle.AlignmentInternal = defaultCellStyle.Alignment;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = dataGridViewCellStyle2.WrapMode;
			}
			else if (columnHeadersDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = columnHeadersDefaultCellStyle.WrapMode;
			}
			else
			{
				dataGridViewCellStyle.WrapModeInternal = defaultCellStyle.WrapMode;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Tag != null)
			{
				dataGridViewCellStyle.Tag = dataGridViewCellStyle2.Tag;
			}
			else if (columnHeadersDefaultCellStyle.Tag != null)
			{
				dataGridViewCellStyle.Tag = columnHeadersDefaultCellStyle.Tag;
			}
			else
			{
				dataGridViewCellStyle.Tag = defaultCellStyle.Tag;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = dataGridViewCellStyle2.Padding;
			}
			else if (columnHeadersDefaultCellStyle.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = columnHeadersDefaultCellStyle.Padding;
			}
			else
			{
				dataGridViewCellStyle.PaddingInternal = defaultCellStyle.Padding;
			}
			return dataGridViewCellStyle;
		}

		/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not -1.</exception>
		// Token: 0x06001FB8 RID: 8120 RVA: 0x00096168 File Offset: 0x00094368
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
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = new DataGridViewAdvancedBorderStyle();
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle2 = base.DataGridView.AdjustColumnHeaderBorderStyle(base.DataGridView.AdvancedColumnHeadersBorderStyle, dataGridViewAdvancedBorderStyle, false, false);
			Rectangle rectangle = this.BorderWidths(dataGridViewAdvancedBorderStyle2);
			int num = rectangle.Left + rectangle.Width + cellStyle.Padding.Horizontal;
			int num2 = rectangle.Top + rectangle.Height + cellStyle.Padding.Vertical;
			TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			string text = this.GetValue(rowIndex) as string;
			Size size;
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
				{
					size = new Size(0, 0);
					if (!string.IsNullOrEmpty(text))
					{
						if (cellStyle.WrapMode == DataGridViewTriState.True)
						{
							size = new Size(DataGridViewCell.MeasureTextWidth(graphics, text, cellStyle.Font, Math.Max(1, constraintSize.Height - num2 - 2), textFormatFlags), 0);
						}
						else
						{
							size = new Size(DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags).Width, 0);
						}
					}
					if (constraintSize.Height - num2 - 2 > (int)DataGridViewColumnHeaderCell.sortGlyphHeight && base.OwningColumn != null && base.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
					{
						size.Width += (int)(DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin);
						if (!string.IsNullOrEmpty(text))
						{
							size.Width += (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth;
						}
					}
					size.Width = Math.Max(size.Width, 1);
				}
				else
				{
					if (!string.IsNullOrEmpty(text))
					{
						if (cellStyle.WrapMode == DataGridViewTriState.True)
						{
							size = DataGridViewCell.MeasureTextPreferredSize(graphics, text, cellStyle.Font, 5f, textFormatFlags);
						}
						else
						{
							size = DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags);
						}
					}
					else
					{
						size = new Size(0, 0);
					}
					if (base.OwningColumn != null && base.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
					{
						size.Width += (int)(DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin);
						if (!string.IsNullOrEmpty(text))
						{
							size.Width += (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth;
						}
						size.Height = Math.Max(size.Height, (int)DataGridViewColumnHeaderCell.sortGlyphHeight);
					}
					size.Width = Math.Max(size.Width, 1);
					size.Height = Math.Max(size.Height, 1);
				}
			}
			else
			{
				int num3 = constraintSize.Width - num;
				size = new Size(0, 0);
				Size empty;
				if (num3 >= (int)(DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin) && base.OwningColumn != null && base.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
				{
					empty = new Size((int)(DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin), (int)DataGridViewColumnHeaderCell.sortGlyphHeight);
				}
				else
				{
					empty = Size.Empty;
				}
				if (num3 - 2 - 2 > 0 && !string.IsNullOrEmpty(text))
				{
					if (cellStyle.WrapMode == DataGridViewTriState.True)
					{
						if (empty.Width > 0 && num3 - 2 - 2 - (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth - empty.Width > 0)
						{
							size = new Size(0, DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, num3 - 2 - 2 - (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth - empty.Width, textFormatFlags));
						}
						else
						{
							size = new Size(0, DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, num3 - 2 - 2, textFormatFlags));
						}
					}
					else
					{
						size = new Size(0, DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags).Height);
					}
				}
				size.Height = Math.Max(size.Height, empty.Height);
				size.Height = Math.Max(size.Height, 1);
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				if (!string.IsNullOrEmpty(text))
				{
					size.Width += 4;
				}
				size.Width += num;
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				size.Height += 2 + num2;
			}
			if (base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				Rectangle themeMargins = DataGridViewHeaderCell.GetThemeMargins(graphics);
				if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
				{
					size.Width += themeMargins.X + themeMargins.Width;
				}
				if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
				{
					size.Height += themeMargins.Y + themeMargins.Height;
				}
			}
			return size;
		}

		/// <summary>Gets the value of the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not -1.</exception>
		// Token: 0x06001FB9 RID: 8121 RVA: 0x000965DE File Offset: 0x000947DE
		protected override object GetValue(int rowIndex)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (this.ContainsLocalValue)
			{
				return base.Properties.GetObject(DataGridViewCell.PropCellValue);
			}
			if (base.OwningColumn != null)
			{
				return base.OwningColumn.Name;
			}
			return null;
		}

		/// <summary>Paints the current <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" />.</summary>
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
		// Token: 0x06001FBA RID: 8122 RVA: 0x00096620 File Offset: 0x00094820
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, formattedValue, cellStyle, advancedBorderStyle, paintParts, true);
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00096654 File Offset: 0x00094854
		private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object formattedValue, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool paint)
		{
			Rectangle rectangle = Rectangle.Empty;
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle2 = cellBounds;
			Rectangle rectangle3 = this.BorderWidths(advancedBorderStyle);
			rectangle2.Offset(rectangle3.X, rectangle3.Y);
			rectangle2.Width -= rectangle3.Right;
			rectangle2.Height -= rectangle3.Bottom;
			Rectangle rectangle4 = rectangle2;
			bool flag = (dataGridViewElementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			if (base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				if (cellStyle.Padding != Padding.Empty && cellStyle.Padding != Padding.Empty)
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
				if (paint && DataGridViewCell.PaintBackground(paintParts) && rectangle4.Width > 0 && rectangle4.Height > 0)
				{
					int num = 1;
					if ((base.OwningColumn != null && base.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable) || base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
					{
						if (base.ButtonState != ButtonState.Normal)
						{
							num = 3;
						}
						else if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex)
						{
							num = 2;
						}
						else if (flag)
						{
							num = 3;
						}
					}
					if (this.IsHighlighted())
					{
						num = 3;
					}
					if (base.DataGridView.RightToLeftInternal)
					{
						Bitmap bitmap = base.FlipXPThemesBitmap;
						if (bitmap == null || bitmap.Width < rectangle4.Width || bitmap.Width > 2 * rectangle4.Width || bitmap.Height < rectangle4.Height || bitmap.Height > 2 * rectangle4.Height)
						{
							bitmap = (base.FlipXPThemesBitmap = new Bitmap(rectangle4.Width, rectangle4.Height));
						}
						Graphics graphics = Graphics.FromImage(bitmap);
						DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.DrawHeader(graphics, new Rectangle(0, 0, rectangle4.Width, rectangle4.Height), num);
						bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
						g.DrawImage(bitmap, rectangle4, new Rectangle(bitmap.Width - rectangle4.Width, 0, rectangle4.Width, rectangle4.Height), GraphicsUnit.Pixel);
					}
					else
					{
						DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.DrawHeader(g, rectangle4, num);
					}
				}
				Rectangle themeMargins = DataGridViewHeaderCell.GetThemeMargins(g);
				rectangle2.Y += themeMargins.Y;
				rectangle2.Height -= themeMargins.Y + themeMargins.Height;
				if (base.DataGridView.RightToLeftInternal)
				{
					rectangle2.X += themeMargins.Width;
					rectangle2.Width -= themeMargins.X + themeMargins.Width;
				}
				else
				{
					rectangle2.X += themeMargins.X;
					rectangle2.Width -= themeMargins.X + themeMargins.Width;
				}
			}
			else
			{
				if (paint && DataGridViewCell.PaintBackground(paintParts) && rectangle4.Width > 0 && rectangle4.Height > 0)
				{
					SolidBrush cachedBrush = base.DataGridView.GetCachedBrush(((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) || this.IsHighlighted()) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
					if (cachedBrush.Color.A == 255)
					{
						g.FillRectangle(cachedBrush, rectangle4);
					}
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
			}
			bool flag2 = false;
			Point point = new Point(0, 0);
			string text = formattedValue as string;
			rectangle2.Y++;
			rectangle2.Height -= 2;
			if (rectangle2.Width - 2 - 2 > 0 && rectangle2.Height > 0 && !string.IsNullOrEmpty(text))
			{
				rectangle2.Offset(2, 0);
				rectangle2.Width -= 4;
				Color color;
				if (base.DataGridView.ApplyVisualStylesToHeaderCells)
				{
					color = DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
				}
				else
				{
					color = (flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
				}
				if (base.OwningColumn != null && base.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
				{
					int num2 = rectangle2.Width - (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth - (int)DataGridViewColumnHeaderCell.sortGlyphWidth - (int)(2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin);
					if (num2 > 0)
					{
						bool flag3;
						int preferredTextHeight = DataGridViewCell.GetPreferredTextHeight(g, base.DataGridView.RightToLeftInternal, text, cellStyle, num2, out flag3);
						if (preferredTextHeight <= rectangle2.Height && !flag3)
						{
							flag2 = this.SortGlyphDirection > SortOrder.None;
							rectangle2.Width -= (int)(DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth + DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin);
							if (base.DataGridView.RightToLeftInternal)
							{
								rectangle2.X += (int)(DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth + DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin);
								point = new Point(rectangle2.Left - 2 - (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth - (int)DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin - (int)DataGridViewColumnHeaderCell.sortGlyphWidth, rectangle2.Top + (rectangle2.Height - (int)DataGridViewColumnHeaderCell.sortGlyphHeight) / 2);
							}
							else
							{
								point = new Point(rectangle2.Right + 2 + (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth + (int)DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin, rectangle2.Top + (rectangle2.Height - (int)DataGridViewColumnHeaderCell.sortGlyphHeight) / 2);
							}
						}
					}
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
						TextRenderer.DrawText(g, text, cellStyle.Font, rectangle2, color, textFormatFlags);
					}
				}
				else
				{
					rectangle = DataGridViewUtilities.GetTextBounds(rectangle2, text, textFormatFlags, cellStyle);
				}
			}
			else if (paint && this.SortGlyphDirection != SortOrder.None && rectangle2.Width >= (int)(DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin) && rectangle2.Height >= (int)DataGridViewColumnHeaderCell.sortGlyphHeight)
			{
				flag2 = true;
				point = new Point(rectangle2.Left + (rectangle2.Width - (int)DataGridViewColumnHeaderCell.sortGlyphWidth) / 2, rectangle2.Top + (rectangle2.Height - (int)DataGridViewColumnHeaderCell.sortGlyphHeight) / 2);
			}
			if (paint && flag2 && DataGridViewCell.PaintContentBackground(paintParts))
			{
				Pen pen = null;
				Pen pen2 = null;
				base.GetContrastedPens(cellStyle.BackColor, ref pen, ref pen2);
				if (this.SortGlyphDirection == SortOrder.Ascending)
				{
					DataGridViewAdvancedCellBorderStyle right = advancedBorderStyle.Right;
					if (right != DataGridViewAdvancedCellBorderStyle.Inset)
					{
						if (right - DataGridViewAdvancedCellBorderStyle.Outset <= 2)
						{
							g.DrawLine(pen, point.X, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y);
							g.DrawLine(pen, point.X + 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y);
							g.DrawLine(pen2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2);
							g.DrawLine(pen2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 3, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2);
							g.DrawLine(pen2, point.X, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
						}
						else
						{
							for (int i = 0; i < (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2); i++)
							{
								g.DrawLine(pen, point.X + i, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - i - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - i - 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - i - 1);
							}
							g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2));
						}
					}
					else
					{
						g.DrawLine(pen2, point.X, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y);
						g.DrawLine(pen2, point.X + 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y);
						g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2);
						g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 3, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2);
						g.DrawLine(pen, point.X, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
					}
				}
				else
				{
					DataGridViewAdvancedCellBorderStyle right2 = advancedBorderStyle.Right;
					if (right2 != DataGridViewAdvancedCellBorderStyle.Inset)
					{
						if (right2 - DataGridViewAdvancedCellBorderStyle.Outset <= 2)
						{
							g.DrawLine(pen, point.X, point.Y + 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
							g.DrawLine(pen, point.X + 1, point.Y + 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
							g.DrawLine(pen2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + 1);
							g.DrawLine(pen2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 3, point.Y + 1);
							g.DrawLine(pen2, point.X, point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y);
						}
						else
						{
							for (int j = 0; j < (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2); j++)
							{
								g.DrawLine(pen, point.X + j, point.Y + j + 2, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - j - 1, point.Y + j + 2);
							}
							g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) + 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) + 2);
						}
					}
					else
					{
						g.DrawLine(pen2, point.X, point.Y + 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
						g.DrawLine(pen2, point.X + 1, point.Y + 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
						g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + 1);
						g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 3, point.Y + 1);
						g.DrawLine(pen, point.X, point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y);
					}
				}
			}
			return rectangle;
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00097420 File Offset: 0x00095620
		private bool IsHighlighted()
		{
			return base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect && base.DataGridView.CurrentCell != null && base.DataGridView.CurrentCell.Selected && base.DataGridView.CurrentCell.OwningColumn == base.OwningColumn && AccessibilityImprovements.Level2;
		}

		/// <summary>Sets the value of the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="value">The cell value to set.</param>
		/// <returns>
		///   <see langword="true" /> if the value has been set; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not -1.</exception>
		// Token: 0x06001FBD RID: 8125 RVA: 0x0009747C File Offset: 0x0009567C
		protected override bool SetValue(int rowIndex, object value)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			object value2 = this.GetValue(rowIndex);
			base.Properties.SetObject(DataGridViewCell.PropCellValue, value);
			if (base.DataGridView != null && value2 != value)
			{
				base.RaiseCellValueChanged(new DataGridViewCellEventArgs(base.ColumnIndex, -1));
			}
			return true;
		}

		/// <summary>Returns the string representation of the cell.</summary>
		/// <returns>A string that represents the current cell.</returns>
		// Token: 0x06001FBE RID: 8126 RVA: 0x000974D0 File Offset: 0x000956D0
		public override string ToString()
		{
			return "DataGridViewColumnHeaderCell { ColumnIndex=" + base.ColumnIndex.ToString(CultureInfo.CurrentCulture) + " }";
		}

		// Token: 0x04000D45 RID: 3397
		private static readonly VisualStyleElement HeaderElement = VisualStyleElement.Header.Item.Normal;

		// Token: 0x04000D46 RID: 3398
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_sortGlyphSeparatorWidth = 2;

		// Token: 0x04000D47 RID: 3399
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_sortGlyphHorizontalMargin = 4;

		// Token: 0x04000D48 RID: 3400
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_sortGlyphWidth = 9;

		// Token: 0x04000D49 RID: 3401
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_sortGlyphHeight = 7;

		// Token: 0x04000D4A RID: 3402
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_horizontalTextMarginLeft = 2;

		// Token: 0x04000D4B RID: 3403
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_horizontalTextMarginRight = 2;

		// Token: 0x04000D4C RID: 3404
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_verticalMargin = 1;

		// Token: 0x04000D4D RID: 3405
		private static bool isScalingInitialized = false;

		// Token: 0x04000D4E RID: 3406
		private static byte sortGlyphSeparatorWidth = 2;

		// Token: 0x04000D4F RID: 3407
		private static byte sortGlyphHorizontalMargin = 4;

		// Token: 0x04000D50 RID: 3408
		private static byte sortGlyphWidth = 9;

		// Token: 0x04000D51 RID: 3409
		private static byte sortGlyphHeight = 7;

		// Token: 0x04000D52 RID: 3410
		private static Type cellType = typeof(DataGridViewColumnHeaderCell);

		// Token: 0x04000D53 RID: 3411
		private SortOrder sortGlyphDirection;

		// Token: 0x0200066A RID: 1642
		private class DataGridViewColumnHeaderCellRenderer
		{
			// Token: 0x0600661A RID: 26138 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewColumnHeaderCellRenderer()
			{
			}

			// Token: 0x17001630 RID: 5680
			// (get) Token: 0x0600661B RID: 26139 RVA: 0x0017D280 File Offset: 0x0017B480
			public static VisualStyleRenderer VisualStyleRenderer
			{
				get
				{
					if (DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewColumnHeaderCell.HeaderElement);
					}
					return DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x0600661C RID: 26140 RVA: 0x0017D2A0 File Offset: 0x0017B4A0
			public static void DrawHeader(Graphics g, Rectangle bounds, int headerState)
			{
				Rectangle rectangle = Rectangle.Truncate(g.ClipBounds);
				if (2 == headerState)
				{
					DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.SetParameters(DataGridViewColumnHeaderCell.HeaderElement);
					Rectangle rectangle2 = new Rectangle(bounds.Left, bounds.Bottom - 2, 2, 2);
					rectangle2.Intersect(rectangle);
					DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.DrawBackground(g, bounds, rectangle2);
					rectangle2 = new Rectangle(bounds.Right - 2, bounds.Bottom - 2, 2, 2);
					rectangle2.Intersect(rectangle);
					DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.DrawBackground(g, bounds, rectangle2);
				}
				DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.SetParameters(DataGridViewColumnHeaderCell.HeaderElement.ClassName, DataGridViewColumnHeaderCell.HeaderElement.Part, headerState);
				DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.DrawBackground(g, bounds, rectangle);
			}

			// Token: 0x04003A5D RID: 14941
			private static VisualStyleRenderer visualStyleRenderer;
		}

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> to accessibility client applications.</summary>
		// Token: 0x0200066B RID: 1643
		protected class DataGridViewColumnHeaderCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</param>
			// Token: 0x0600661D RID: 26141 RVA: 0x0017BC46 File Offset: 0x00179E46
			public DataGridViewColumnHeaderCellAccessibleObject(DataGridViewColumnHeaderCell owner)
				: base(owner)
			{
			}

			/// <summary>Gets the location and size of the accessible object.</summary>
			/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
			// Token: 0x17001631 RID: 5681
			// (get) Token: 0x0600661E RID: 26142 RVA: 0x0017D356 File Offset: 0x0017B556
			public override Rectangle Bounds
			{
				get
				{
					return base.GetAccessibleObjectBounds(this.ParentPrivate);
				}
			}

			/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
			/// <returns>A string that describes the default action of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" /></returns>
			// Token: 0x17001632 RID: 5682
			// (get) Token: 0x0600661F RID: 26143 RVA: 0x0017D364 File Offset: 0x0017B564
			public override string DefaultAction
			{
				get
				{
					if (base.Owner.OwningColumn == null)
					{
						return string.Empty;
					}
					if (base.Owner.OwningColumn.SortMode == DataGridViewColumnSortMode.Automatic)
					{
						return SR.GetString("DataGridView_AccColumnHeaderCellDefaultAction");
					}
					if (base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
					{
						return SR.GetString("DataGridView_AccColumnHeaderCellSelectDefaultAction");
					}
					return string.Empty;
				}
			}

			/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
			/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</returns>
			// Token: 0x17001633 RID: 5683
			// (get) Token: 0x06006620 RID: 26144 RVA: 0x0017D3D8 File Offset: 0x0017B5D8
			public override string Name
			{
				get
				{
					if (base.Owner.OwningColumn != null)
					{
						return base.Owner.OwningColumn.HeaderText;
					}
					return string.Empty;
				}
			}

			/// <summary>Gets the parent of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>The parent of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</returns>
			// Token: 0x17001634 RID: 5684
			// (get) Token: 0x06006621 RID: 26145 RVA: 0x0017D3FD File Offset: 0x0017B5FD
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.ParentPrivate;
				}
			}

			// Token: 0x17001635 RID: 5685
			// (get) Token: 0x06006622 RID: 26146 RVA: 0x0017D405 File Offset: 0x0017B605
			private AccessibleObject ParentPrivate
			{
				get
				{
					return base.Owner.DataGridView.AccessibilityObject.GetChild(0);
				}
			}

			/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.RowHeader" /> value.</returns>
			// Token: 0x17001636 RID: 5686
			// (get) Token: 0x06006623 RID: 26147 RVA: 0x00177384 File Offset: 0x00175584
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ColumnHeader;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
			/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values. The default is <see cref="F:System.Windows.Forms.AccessibleStates.Selectable" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x17001637 RID: 5687
			// (get) Token: 0x06006624 RID: 26148 RVA: 0x0017D420 File Offset: 0x0017B620
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
					if ((base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect) && base.Owner.OwningColumn != null && base.Owner.OwningColumn.Selected)
					{
						accessibleStates |= AccessibleStates.Selected;
					}
					return accessibleStates;
				}
			}

			/// <summary>Gets the value of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
			/// <returns>The value of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</returns>
			// Token: 0x17001638 RID: 5688
			// (get) Token: 0x06006625 RID: 26149 RVA: 0x00016178 File Offset: 0x00014378
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.Name;
				}
			}

			// Token: 0x17001639 RID: 5689
			// (get) Token: 0x06006626 RID: 26150 RVA: 0x0017D49C File Offset: 0x0017B69C
			private int VisibleIndex
			{
				get
				{
					DataGridViewCell owner = base.Owner;
					if (((owner != null) ? owner.DataGridView : null) == null || base.Owner.OwningColumn == null)
					{
						return -1;
					}
					if (!base.Owner.DataGridView.RowHeadersVisible)
					{
						return base.Owner.DataGridView.Columns.GetVisibleIndex(base.Owner.OwningColumn);
					}
					return base.Owner.DataGridView.Columns.GetVisibleIndex(base.Owner.OwningColumn) + 1;
				}
			}

			/// <summary>Performs the default action associated with the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
			// Token: 0x06006627 RID: 26151 RVA: 0x0017D524 File Offset: 0x0017B724
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				DataGridViewColumnHeaderCell dataGridViewColumnHeaderCell = (DataGridViewColumnHeaderCell)base.Owner;
				DataGridView dataGridView = dataGridViewColumnHeaderCell.DataGridView;
				if (dataGridViewColumnHeaderCell.OwningColumn != null)
				{
					if (dataGridViewColumnHeaderCell.OwningColumn.SortMode == DataGridViewColumnSortMode.Automatic)
					{
						ListSortDirection listSortDirection = ListSortDirection.Ascending;
						if (dataGridView.SortedColumn == dataGridViewColumnHeaderCell.OwningColumn && dataGridView.SortOrder == SortOrder.Ascending)
						{
							listSortDirection = ListSortDirection.Descending;
						}
						dataGridView.Sort(dataGridViewColumnHeaderCell.OwningColumn, listSortDirection);
						return;
					}
					if (dataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || dataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
					{
						dataGridViewColumnHeaderCell.OwningColumn.Selected = true;
					}
				}
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents an object in the specified direction.</returns>
			// Token: 0x06006628 RID: 26152 RVA: 0x0017D5A4 File Offset: 0x0017B7A4
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				if (base.Owner.OwningColumn == null)
				{
					return null;
				}
				switch (navigationDirection)
				{
				case AccessibleNavigation.Left:
					if (base.Owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return this.NavigateBackward();
					}
					return this.NavigateForward();
				case AccessibleNavigation.Right:
					if (base.Owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return this.NavigateForward();
					}
					return this.NavigateBackward();
				case AccessibleNavigation.Next:
					return this.NavigateForward();
				case AccessibleNavigation.Previous:
					return this.NavigateBackward();
				case AccessibleNavigation.FirstChild:
					if (AccessibilityImprovements.Level5)
					{
						return null;
					}
					return base.Owner.DataGridView.AccessibilityObject.GetChild(0).GetChild(0);
				case AccessibleNavigation.LastChild:
				{
					if (AccessibilityImprovements.Level5)
					{
						return null;
					}
					AccessibleObject child = base.Owner.DataGridView.AccessibilityObject.GetChild(0);
					return child.GetChild(child.GetChildCount() - 1);
				}
				default:
					return null;
				}
			}

			// Token: 0x06006629 RID: 26153 RVA: 0x0017D68C File Offset: 0x0017B88C
			private AccessibleObject NavigateBackward()
			{
				if (base.Owner.OwningColumn == base.Owner.DataGridView.Columns.GetFirstColumn(DataGridViewElementStates.Visible))
				{
					if (base.Owner.DataGridView.RowHeadersVisible)
					{
						return this.Parent.GetChild(0);
					}
					return null;
				}
				else if (AccessibilityImprovements.Level5)
				{
					AccessibleObject parent = this.Parent;
					if (parent == null)
					{
						return null;
					}
					return parent.GetChild(this.VisibleIndex - 1);
				}
				else
				{
					int index = base.Owner.DataGridView.Columns.GetPreviousColumn(base.Owner.OwningColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
					int num = base.Owner.DataGridView.Columns.ColumnIndexToActualDisplayIndex(index, DataGridViewElementStates.Visible);
					if (base.Owner.DataGridView.RowHeadersVisible)
					{
						return this.Parent.GetChild(num + 1);
					}
					return this.Parent.GetChild(num);
				}
			}

			// Token: 0x0600662A RID: 26154 RVA: 0x0017D770 File Offset: 0x0017B970
			private AccessibleObject NavigateForward()
			{
				if (base.Owner.OwningColumn == base.Owner.DataGridView.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None))
				{
					return null;
				}
				if (AccessibilityImprovements.Level5)
				{
					int visibleIndex = this.VisibleIndex;
					if (visibleIndex < 0)
					{
						return null;
					}
					AccessibleObject parent = this.Parent;
					if (parent == null)
					{
						return null;
					}
					return parent.GetChild(visibleIndex + 1);
				}
				else
				{
					int index = base.Owner.DataGridView.Columns.GetNextColumn(base.Owner.OwningColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
					int num = base.Owner.DataGridView.Columns.ColumnIndexToActualDisplayIndex(index, DataGridViewElementStates.Visible);
					if (base.Owner.DataGridView.RowHeadersVisible)
					{
						return this.Parent.GetChild(num + 1);
					}
					return this.Parent.GetChild(num);
				}
			}

			/// <summary>Modifies the column selection depending on the selection mode.</summary>
			/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
			// Token: 0x0600662B RID: 26155 RVA: 0x0017D83C File Offset: 0x0017BA3C
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if (base.Owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				DataGridViewColumnHeaderCell dataGridViewColumnHeaderCell = (DataGridViewColumnHeaderCell)base.Owner;
				DataGridView dataGridView = dataGridViewColumnHeaderCell.DataGridView;
				if (dataGridView == null)
				{
					return;
				}
				if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
				{
					dataGridView.FocusInternal();
				}
				if (dataGridViewColumnHeaderCell.OwningColumn != null && (dataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || dataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect))
				{
					if ((flags & (AccessibleSelection.TakeSelection | AccessibleSelection.AddSelection)) != AccessibleSelection.None)
					{
						dataGridViewColumnHeaderCell.OwningColumn.Selected = true;
						return;
					}
					if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection)
					{
						dataGridViewColumnHeaderCell.OwningColumn.Selected = false;
					}
				}
			}

			// Token: 0x0600662C RID: 26156 RVA: 0x0017D8C6 File Offset: 0x0017BAC6
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.Owner.OwningColumn == null)
				{
					return null;
				}
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return this.Parent;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					return this.NavigateForward();
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					return this.NavigateBackward();
				default:
					return null;
				}
			}

			// Token: 0x0600662D RID: 26157 RVA: 0x0017D901 File Offset: 0x0017BB01
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId.Equals(10018) || patternId.Equals(10000);
			}

			// Token: 0x0600662E RID: 26158 RVA: 0x0017D920 File Offset: 0x0017BB20
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
					case 30011:
					case 30012:
						goto IL_CB;
					case 30005:
						return this.Name;
					case 30007:
						return string.Empty;
					case 30008:
						break;
					case 30009:
						return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
					case 30010:
						return base.Owner.DataGridView.Enabled;
					case 30013:
						return this.Help ?? string.Empty;
					default:
						if (propertyId != 30019)
						{
							if (propertyId != 30022)
							{
								goto IL_CB;
							}
							return (this.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen;
						}
						break;
					}
					return false;
				}
				IL_CB:
				return base.GetPropertyValue(propertyId);
			}
		}
	}
}
