using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000221 RID: 545
	internal class DataGridViewUtilities
	{
		// Token: 0x06002373 RID: 9075 RVA: 0x000A8F14 File Offset: 0x000A7114
		internal static ContentAlignment ComputeDrawingContentAlignmentForCellStyleAlignment(DataGridViewContentAlignment alignment)
		{
			if (alignment <= DataGridViewContentAlignment.MiddleCenter)
			{
				switch (alignment)
				{
				case DataGridViewContentAlignment.TopLeft:
					return ContentAlignment.TopLeft;
				case DataGridViewContentAlignment.TopCenter:
					return ContentAlignment.TopCenter;
				case (DataGridViewContentAlignment)3:
					break;
				case DataGridViewContentAlignment.TopRight:
					return ContentAlignment.TopRight;
				default:
					if (alignment == DataGridViewContentAlignment.MiddleLeft)
					{
						return ContentAlignment.MiddleLeft;
					}
					if (alignment == DataGridViewContentAlignment.MiddleCenter)
					{
						return ContentAlignment.MiddleCenter;
					}
					break;
				}
			}
			else if (alignment <= DataGridViewContentAlignment.BottomLeft)
			{
				if (alignment == DataGridViewContentAlignment.MiddleRight)
				{
					return ContentAlignment.MiddleRight;
				}
				if (alignment == DataGridViewContentAlignment.BottomLeft)
				{
					return ContentAlignment.BottomLeft;
				}
			}
			else
			{
				if (alignment == DataGridViewContentAlignment.BottomCenter)
				{
					return ContentAlignment.BottomCenter;
				}
				if (alignment == DataGridViewContentAlignment.BottomRight)
				{
					return ContentAlignment.BottomRight;
				}
			}
			return ContentAlignment.MiddleCenter;
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000A8F98 File Offset: 0x000A7198
		internal static TextFormatFlags ComputeTextFormatFlagsForCellStyleAlignment(bool rightToLeft, DataGridViewContentAlignment alignment, DataGridViewTriState wrapMode)
		{
			TextFormatFlags textFormatFlags;
			if (alignment <= DataGridViewContentAlignment.MiddleCenter)
			{
				switch (alignment)
				{
				case DataGridViewContentAlignment.TopLeft:
					textFormatFlags = TextFormatFlags.Default;
					if (rightToLeft)
					{
						textFormatFlags |= TextFormatFlags.Right;
						goto IL_CD;
					}
					textFormatFlags |= TextFormatFlags.Default;
					goto IL_CD;
				case DataGridViewContentAlignment.TopCenter:
					textFormatFlags = TextFormatFlags.HorizontalCenter;
					goto IL_CD;
				case (DataGridViewContentAlignment)3:
					break;
				case DataGridViewContentAlignment.TopRight:
					textFormatFlags = TextFormatFlags.Default;
					if (rightToLeft)
					{
						textFormatFlags |= TextFormatFlags.Default;
						goto IL_CD;
					}
					textFormatFlags |= TextFormatFlags.Right;
					goto IL_CD;
				default:
					if (alignment != DataGridViewContentAlignment.MiddleLeft)
					{
						if (alignment == DataGridViewContentAlignment.MiddleCenter)
						{
							textFormatFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
							goto IL_CD;
						}
					}
					else
					{
						textFormatFlags = TextFormatFlags.VerticalCenter;
						if (rightToLeft)
						{
							textFormatFlags |= TextFormatFlags.Right;
							goto IL_CD;
						}
						textFormatFlags |= TextFormatFlags.Default;
						goto IL_CD;
					}
					break;
				}
			}
			else if (alignment <= DataGridViewContentAlignment.BottomLeft)
			{
				if (alignment != DataGridViewContentAlignment.MiddleRight)
				{
					if (alignment == DataGridViewContentAlignment.BottomLeft)
					{
						textFormatFlags = TextFormatFlags.Bottom;
						if (rightToLeft)
						{
							textFormatFlags |= TextFormatFlags.Right;
							goto IL_CD;
						}
						textFormatFlags |= TextFormatFlags.Default;
						goto IL_CD;
					}
				}
				else
				{
					textFormatFlags = TextFormatFlags.VerticalCenter;
					if (rightToLeft)
					{
						textFormatFlags |= TextFormatFlags.Default;
						goto IL_CD;
					}
					textFormatFlags |= TextFormatFlags.Right;
					goto IL_CD;
				}
			}
			else
			{
				if (alignment == DataGridViewContentAlignment.BottomCenter)
				{
					textFormatFlags = TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
					goto IL_CD;
				}
				if (alignment == DataGridViewContentAlignment.BottomRight)
				{
					textFormatFlags = TextFormatFlags.Bottom;
					if (rightToLeft)
					{
						textFormatFlags |= TextFormatFlags.Default;
						goto IL_CD;
					}
					textFormatFlags |= TextFormatFlags.Right;
					goto IL_CD;
				}
			}
			textFormatFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
			IL_CD:
			if (wrapMode == DataGridViewTriState.False)
			{
				textFormatFlags |= TextFormatFlags.SingleLine;
			}
			else
			{
				textFormatFlags |= TextFormatFlags.WordBreak;
			}
			textFormatFlags |= TextFormatFlags.NoPrefix;
			textFormatFlags |= TextFormatFlags.PreserveGraphicsClipping;
			if (rightToLeft)
			{
				textFormatFlags |= TextFormatFlags.RightToLeft;
			}
			return textFormatFlags;
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000A90A0 File Offset: 0x000A72A0
		internal static Size GetPreferredRowHeaderSize(Graphics graphics, string val, DataGridViewCellStyle cellStyle, int borderAndPaddingWidths, int borderAndPaddingHeights, bool showRowErrors, bool showGlyph, Size constraintSize, TextFormatFlags flags)
		{
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			if (freeDimensionFromConstraint == DataGridViewFreeDimension.Height)
			{
				int num = 1;
				int num2 = 1;
				int num3 = constraintSize.Width - borderAndPaddingWidths;
				if (!string.IsNullOrEmpty(val))
				{
					if (showGlyph && num3 >= 18)
					{
						num = 15;
						num3 -= 18;
					}
					if (showRowErrors && num3 >= 18)
					{
						num = 15;
						num3 -= 18;
					}
					if (num3 > 9)
					{
						num3 -= 9;
						if (cellStyle.WrapMode == DataGridViewTriState.True)
						{
							num2 = DataGridViewCell.MeasureTextHeight(graphics, val, cellStyle.Font, num3, flags);
						}
						else
						{
							num2 = DataGridViewCell.MeasureTextSize(graphics, val, cellStyle.Font, flags).Height;
						}
						num2 += 2;
					}
				}
				else if ((showGlyph || showRowErrors) && num3 >= 18)
				{
					num = 15;
				}
				return new Size(0, Math.Max(num, num2) + borderAndPaddingHeights);
			}
			if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
			{
				int num4 = 0;
				int num5 = constraintSize.Height - borderAndPaddingHeights;
				if (!string.IsNullOrEmpty(val))
				{
					int num6 = num5 - 2;
					if (num6 > 0)
					{
						if (cellStyle.WrapMode == DataGridViewTriState.True)
						{
							num4 = DataGridViewCell.MeasureTextWidth(graphics, val, cellStyle.Font, num6, flags);
						}
						else
						{
							num4 = DataGridViewCell.MeasureTextSize(graphics, val, cellStyle.Font, flags).Width;
						}
						num4 += 9;
					}
				}
				if (num5 >= 15)
				{
					if (showGlyph)
					{
						num4 += 18;
					}
					if (showRowErrors)
					{
						num4 += 18;
					}
				}
				num4 = Math.Max(num4, 1);
				num4 += borderAndPaddingWidths;
				return new Size(num4, 0);
			}
			Size size;
			if (!string.IsNullOrEmpty(val))
			{
				if (cellStyle.WrapMode == DataGridViewTriState.True)
				{
					size = DataGridViewCell.MeasureTextPreferredSize(graphics, val, cellStyle.Font, 5f, flags);
				}
				else
				{
					size = DataGridViewCell.MeasureTextSize(graphics, val, cellStyle.Font, flags);
				}
				size.Width += 9;
				size.Height += 2;
			}
			else
			{
				size = new Size(0, 1);
			}
			if (showGlyph)
			{
				size.Width += 18;
			}
			if (showRowErrors)
			{
				size.Width += 18;
			}
			if (showGlyph || showRowErrors)
			{
				size.Height = Math.Max(size.Height, 15);
			}
			size.Width += borderAndPaddingWidths;
			size.Height += borderAndPaddingHeights;
			return size;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000A92C2 File Offset: 0x000A74C2
		internal static Rectangle GetTextBounds(Rectangle cellBounds, string text, TextFormatFlags flags, DataGridViewCellStyle cellStyle)
		{
			return DataGridViewUtilities.GetTextBounds(cellBounds, text, flags, cellStyle, cellStyle.Font);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000A92D4 File Offset: 0x000A74D4
		internal static Rectangle GetTextBounds(Rectangle cellBounds, string text, TextFormatFlags flags, DataGridViewCellStyle cellStyle, Font font)
		{
			if ((flags & TextFormatFlags.SingleLine) != TextFormatFlags.Default && TextRenderer.MeasureText(text, font, new Size(2147483647, 2147483647), flags).Width > cellBounds.Width)
			{
				flags |= TextFormatFlags.EndEllipsis;
			}
			Size size = new Size(cellBounds.Width, cellBounds.Height);
			Size size2 = TextRenderer.MeasureText(text, font, size, flags);
			if (size2.Width > size.Width)
			{
				size2.Width = size.Width;
			}
			if (size2.Height > size.Height)
			{
				size2.Height = size.Height;
			}
			if (size2 == size)
			{
				return cellBounds;
			}
			return new Rectangle(DataGridViewUtilities.GetTextLocation(cellBounds, size2, flags, cellStyle), size2);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000A9390 File Offset: 0x000A7590
		internal static Point GetTextLocation(Rectangle cellBounds, Size sizeText, TextFormatFlags flags, DataGridViewCellStyle cellStyle)
		{
			Point point = new Point(0, 0);
			DataGridViewContentAlignment dataGridViewContentAlignment = cellStyle.Alignment;
			if ((flags & TextFormatFlags.RightToLeft) != TextFormatFlags.Default)
			{
				if (dataGridViewContentAlignment <= DataGridViewContentAlignment.MiddleLeft)
				{
					if (dataGridViewContentAlignment != DataGridViewContentAlignment.TopLeft)
					{
						if (dataGridViewContentAlignment != DataGridViewContentAlignment.TopRight)
						{
							if (dataGridViewContentAlignment == DataGridViewContentAlignment.MiddleLeft)
							{
								dataGridViewContentAlignment = DataGridViewContentAlignment.MiddleRight;
							}
						}
						else
						{
							dataGridViewContentAlignment = DataGridViewContentAlignment.TopLeft;
						}
					}
					else
					{
						dataGridViewContentAlignment = DataGridViewContentAlignment.TopRight;
					}
				}
				else if (dataGridViewContentAlignment != DataGridViewContentAlignment.MiddleRight)
				{
					if (dataGridViewContentAlignment != DataGridViewContentAlignment.BottomLeft)
					{
						if (dataGridViewContentAlignment == DataGridViewContentAlignment.BottomRight)
						{
							dataGridViewContentAlignment = DataGridViewContentAlignment.BottomLeft;
						}
					}
					else
					{
						dataGridViewContentAlignment = DataGridViewContentAlignment.BottomRight;
					}
				}
				else
				{
					dataGridViewContentAlignment = DataGridViewContentAlignment.MiddleLeft;
				}
			}
			if (dataGridViewContentAlignment <= DataGridViewContentAlignment.MiddleCenter)
			{
				switch (dataGridViewContentAlignment)
				{
				case DataGridViewContentAlignment.TopLeft:
					point.X = cellBounds.X;
					point.Y = cellBounds.Y;
					break;
				case DataGridViewContentAlignment.TopCenter:
					point.X = cellBounds.X + (cellBounds.Width - sizeText.Width) / 2;
					point.Y = cellBounds.Y;
					break;
				case (DataGridViewContentAlignment)3:
					break;
				case DataGridViewContentAlignment.TopRight:
					point.X = cellBounds.Right - sizeText.Width;
					point.Y = cellBounds.Y;
					break;
				default:
					if (dataGridViewContentAlignment != DataGridViewContentAlignment.MiddleLeft)
					{
						if (dataGridViewContentAlignment == DataGridViewContentAlignment.MiddleCenter)
						{
							point.X = cellBounds.X + (cellBounds.Width - sizeText.Width) / 2;
							point.Y = cellBounds.Y + (cellBounds.Height - sizeText.Height) / 2;
						}
					}
					else
					{
						point.X = cellBounds.X;
						point.Y = cellBounds.Y + (cellBounds.Height - sizeText.Height) / 2;
					}
					break;
				}
			}
			else if (dataGridViewContentAlignment <= DataGridViewContentAlignment.BottomLeft)
			{
				if (dataGridViewContentAlignment != DataGridViewContentAlignment.MiddleRight)
				{
					if (dataGridViewContentAlignment == DataGridViewContentAlignment.BottomLeft)
					{
						point.X = cellBounds.X;
						point.Y = cellBounds.Bottom - sizeText.Height;
					}
				}
				else
				{
					point.X = cellBounds.Right - sizeText.Width;
					point.Y = cellBounds.Y + (cellBounds.Height - sizeText.Height) / 2;
				}
			}
			else if (dataGridViewContentAlignment != DataGridViewContentAlignment.BottomCenter)
			{
				if (dataGridViewContentAlignment == DataGridViewContentAlignment.BottomRight)
				{
					point.X = cellBounds.Right - sizeText.Width;
					point.Y = cellBounds.Bottom - sizeText.Height;
				}
			}
			else
			{
				point.X = cellBounds.X + (cellBounds.Width - sizeText.Width) / 2;
				point.Y = cellBounds.Bottom - sizeText.Height;
			}
			return point;
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000A9629 File Offset: 0x000A7829
		internal static bool ValidTextFormatFlags(TextFormatFlags flags)
		{
			return (flags & ~(TextFormatFlags.Bottom | TextFormatFlags.EndEllipsis | TextFormatFlags.ExpandTabs | TextFormatFlags.ExternalLeading | TextFormatFlags.HidePrefix | TextFormatFlags.HorizontalCenter | TextFormatFlags.Internal | TextFormatFlags.ModifyString | TextFormatFlags.NoClipping | TextFormatFlags.NoPrefix | TextFormatFlags.NoFullWidthCharacterBreak | TextFormatFlags.PathEllipsis | TextFormatFlags.PrefixOnly | TextFormatFlags.Right | TextFormatFlags.RightToLeft | TextFormatFlags.SingleLine | TextFormatFlags.TextBoxControl | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak | TextFormatFlags.WordEllipsis | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform)) == TextFormatFlags.Default;
		}

		// Token: 0x04000E8E RID: 3726
		private const byte DATAGRIDVIEWROWHEADERCELL_iconMarginWidth = 3;

		// Token: 0x04000E8F RID: 3727
		private const byte DATAGRIDVIEWROWHEADERCELL_iconMarginHeight = 2;

		// Token: 0x04000E90 RID: 3728
		private const byte DATAGRIDVIEWROWHEADERCELL_contentMarginWidth = 3;

		// Token: 0x04000E91 RID: 3729
		private const byte DATAGRIDVIEWROWHEADERCELL_contentMarginHeight = 3;

		// Token: 0x04000E92 RID: 3730
		private const byte DATAGRIDVIEWROWHEADERCELL_iconsWidth = 12;

		// Token: 0x04000E93 RID: 3731
		private const byte DATAGRIDVIEWROWHEADERCELL_iconsHeight = 11;

		// Token: 0x04000E94 RID: 3732
		private const byte DATAGRIDVIEWROWHEADERCELL_horizontalTextMarginLeft = 1;

		// Token: 0x04000E95 RID: 3733
		private const byte DATAGRIDVIEWROWHEADERCELL_horizontalTextMarginRight = 2;

		// Token: 0x04000E96 RID: 3734
		private const byte DATAGRIDVIEWROWHEADERCELL_verticalTextMargin = 1;
	}
}
