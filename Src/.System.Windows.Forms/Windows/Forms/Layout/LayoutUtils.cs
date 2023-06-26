using System;
using System.Collections;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004CC RID: 1228
	internal class LayoutUtils
	{
		// Token: 0x060050A0 RID: 20640 RVA: 0x0014F5B8 File Offset: 0x0014D7B8
		public static Size OldGetLargestStringSizeInCollection(Font font, ICollection objects)
		{
			Size empty = Size.Empty;
			if (objects != null)
			{
				foreach (object obj in objects)
				{
					Size size = TextRenderer.MeasureText(obj.ToString(), font, new Size(32767, 32767), TextFormatFlags.SingleLine);
					empty.Width = Math.Max(empty.Width, size.Width);
					empty.Height = Math.Max(empty.Height, size.Height);
				}
			}
			return empty;
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x0014F664 File Offset: 0x0014D864
		public static int ContentAlignmentToIndex(ContentAlignment alignment)
		{
			int num = (int)LayoutUtils.xContentAlignmentToIndex((int)(alignment & (ContentAlignment)15));
			int num2 = (int)LayoutUtils.xContentAlignmentToIndex((int)((alignment >> 4) & (ContentAlignment)15));
			int num3 = (int)LayoutUtils.xContentAlignmentToIndex((int)((alignment >> 8) & (ContentAlignment)15));
			int num4 = ((num2 != 0) ? 4 : 0) | ((num3 != 0) ? 8 : 0) | num | num2 | num3;
			return num4 - 1;
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x0014F6B0 File Offset: 0x0014D8B0
		private static byte xContentAlignmentToIndex(int threeBitFlag)
		{
			return (threeBitFlag == 4) ? 3 : ((byte)threeBitFlag);
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x0014F6C8 File Offset: 0x0014D8C8
		public static Size ConvertZeroToUnbounded(Size size)
		{
			if (size.Width == 0)
			{
				size.Width = int.MaxValue;
			}
			if (size.Height == 0)
			{
				size.Height = int.MaxValue;
			}
			return size;
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x0014F6F8 File Offset: 0x0014D8F8
		public static Padding ClampNegativePaddingToZero(Padding padding)
		{
			if (padding.All < 0)
			{
				padding.Left = Math.Max(0, padding.Left);
				padding.Top = Math.Max(0, padding.Top);
				padding.Right = Math.Max(0, padding.Right);
				padding.Bottom = Math.Max(0, padding.Bottom);
			}
			return padding;
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x0014F760 File Offset: 0x0014D960
		private static AnchorStyles GetOppositeAnchor(AnchorStyles anchor)
		{
			AnchorStyles anchorStyles = AnchorStyles.None;
			if (anchor == AnchorStyles.None)
			{
				return anchorStyles;
			}
			for (int i = 1; i <= 8; i <<= 1)
			{
				switch (anchor & (AnchorStyles)i)
				{
				case AnchorStyles.Top:
					anchorStyles |= AnchorStyles.Bottom;
					break;
				case AnchorStyles.Bottom:
					anchorStyles |= AnchorStyles.Top;
					break;
				case AnchorStyles.Left:
					anchorStyles |= AnchorStyles.Right;
					break;
				case AnchorStyles.Right:
					anchorStyles |= AnchorStyles.Left;
					break;
				}
			}
			return anchorStyles;
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x0014F7C7 File Offset: 0x0014D9C7
		public static TextImageRelation GetOppositeTextImageRelation(TextImageRelation relation)
		{
			return (TextImageRelation)LayoutUtils.GetOppositeAnchor((AnchorStyles)relation);
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x0014F7CF File Offset: 0x0014D9CF
		public static Size UnionSizes(Size a, Size b)
		{
			return new Size(Math.Max(a.Width, b.Width), Math.Max(a.Height, b.Height));
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x0014F7FC File Offset: 0x0014D9FC
		public static Size IntersectSizes(Size a, Size b)
		{
			return new Size(Math.Min(a.Width, b.Width), Math.Min(a.Height, b.Height));
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x0014F82C File Offset: 0x0014DA2C
		public static bool IsIntersectHorizontally(Rectangle rect1, Rectangle rect2)
		{
			return rect1.IntersectsWith(rect2) && ((rect1.X <= rect2.X && rect1.X + rect1.Width >= rect2.X + rect2.Width) || (rect2.X <= rect1.X && rect2.X + rect2.Width >= rect1.X + rect1.Width));
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x0014F8AC File Offset: 0x0014DAAC
		public static bool IsIntersectVertically(Rectangle rect1, Rectangle rect2)
		{
			return rect1.IntersectsWith(rect2) && ((rect1.Y <= rect2.Y && rect1.Y + rect1.Width >= rect2.Y + rect2.Width) || (rect2.Y <= rect1.Y && rect2.Y + rect2.Width >= rect1.Y + rect1.Width));
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x0014F92C File Offset: 0x0014DB2C
		internal static AnchorStyles GetUnifiedAnchor(IArrangedElement element)
		{
			DockStyle dock = DefaultLayout.GetDock(element);
			if (dock != DockStyle.None)
			{
				return LayoutUtils.dockingToAnchor[(int)dock];
			}
			return DefaultLayout.GetAnchor(element);
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x0014F951 File Offset: 0x0014DB51
		public static Rectangle AlignAndStretch(Size fitThis, Rectangle withinThis, AnchorStyles anchorStyles)
		{
			return LayoutUtils.Align(LayoutUtils.Stretch(fitThis, withinThis.Size, anchorStyles), withinThis, anchorStyles);
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x0014F968 File Offset: 0x0014DB68
		public static Rectangle Align(Size alignThis, Rectangle withinThis, AnchorStyles anchorStyles)
		{
			return LayoutUtils.VAlign(alignThis, LayoutUtils.HAlign(alignThis, withinThis, anchorStyles), anchorStyles);
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x0014F979 File Offset: 0x0014DB79
		public static Rectangle Align(Size alignThis, Rectangle withinThis, ContentAlignment align)
		{
			return LayoutUtils.VAlign(alignThis, LayoutUtils.HAlign(alignThis, withinThis, align), align);
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x0014F98C File Offset: 0x0014DB8C
		public static Rectangle HAlign(Size alignThis, Rectangle withinThis, AnchorStyles anchorStyles)
		{
			if ((anchorStyles & AnchorStyles.Right) != AnchorStyles.None)
			{
				withinThis.X += withinThis.Width - alignThis.Width;
			}
			else if (anchorStyles == AnchorStyles.None || (anchorStyles & (AnchorStyles.Left | AnchorStyles.Right)) == AnchorStyles.None)
			{
				withinThis.X += (withinThis.Width - alignThis.Width) / 2;
			}
			withinThis.Width = alignThis.Width;
			return withinThis;
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x0014F9F4 File Offset: 0x0014DBF4
		private static Rectangle HAlign(Size alignThis, Rectangle withinThis, ContentAlignment align)
		{
			if ((align & (ContentAlignment)1092) != (ContentAlignment)0)
			{
				withinThis.X += withinThis.Width - alignThis.Width;
			}
			else if ((align & (ContentAlignment)546) != (ContentAlignment)0)
			{
				withinThis.X += (withinThis.Width - alignThis.Width) / 2;
			}
			withinThis.Width = alignThis.Width;
			return withinThis;
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x0014FA60 File Offset: 0x0014DC60
		public static Rectangle VAlign(Size alignThis, Rectangle withinThis, AnchorStyles anchorStyles)
		{
			if ((anchorStyles & AnchorStyles.Bottom) != AnchorStyles.None)
			{
				withinThis.Y += withinThis.Height - alignThis.Height;
			}
			else if (anchorStyles == AnchorStyles.None || (anchorStyles & (AnchorStyles.Top | AnchorStyles.Bottom)) == AnchorStyles.None)
			{
				withinThis.Y += (withinThis.Height - alignThis.Height) / 2;
			}
			withinThis.Height = alignThis.Height;
			return withinThis;
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x0014FAC8 File Offset: 0x0014DCC8
		public static Rectangle VAlign(Size alignThis, Rectangle withinThis, ContentAlignment align)
		{
			if ((align & (ContentAlignment)1792) != (ContentAlignment)0)
			{
				withinThis.Y += withinThis.Height - alignThis.Height;
			}
			else if ((align & (ContentAlignment)112) != (ContentAlignment)0)
			{
				withinThis.Y += (withinThis.Height - alignThis.Height) / 2;
			}
			withinThis.Height = alignThis.Height;
			return withinThis;
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x0014FB34 File Offset: 0x0014DD34
		public static Size Stretch(Size stretchThis, Size withinThis, AnchorStyles anchorStyles)
		{
			Size size = new Size(((anchorStyles & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right)) ? withinThis.Width : stretchThis.Width, ((anchorStyles & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom)) ? withinThis.Height : stretchThis.Height);
			if (size.Width > withinThis.Width)
			{
				size.Width = withinThis.Width;
			}
			if (size.Height > withinThis.Height)
			{
				size.Height = withinThis.Height;
			}
			return size;
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x0014FBB4 File Offset: 0x0014DDB4
		public static Rectangle InflateRect(Rectangle rect, Padding padding)
		{
			rect.X -= padding.Left;
			rect.Y -= padding.Top;
			rect.Width += padding.Horizontal;
			rect.Height += padding.Vertical;
			return rect;
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x0014FC18 File Offset: 0x0014DE18
		public static Rectangle DeflateRect(Rectangle rect, Padding padding)
		{
			rect.X += padding.Left;
			rect.Y += padding.Top;
			rect.Width -= padding.Horizontal;
			rect.Height -= padding.Vertical;
			return rect;
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x0014FC7A File Offset: 0x0014DE7A
		public static Size AddAlignedRegion(Size textSize, Size imageSize, TextImageRelation relation)
		{
			return LayoutUtils.AddAlignedRegionCore(textSize, imageSize, LayoutUtils.IsVerticalRelation(relation));
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x0014FC8C File Offset: 0x0014DE8C
		public static Size AddAlignedRegionCore(Size currentSize, Size contentSize, bool vertical)
		{
			if (vertical)
			{
				currentSize.Width = Math.Max(currentSize.Width, contentSize.Width);
				currentSize.Height += contentSize.Height;
			}
			else
			{
				currentSize.Width += contentSize.Width;
				currentSize.Height = Math.Max(currentSize.Height, contentSize.Height);
			}
			return currentSize;
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x0014FD00 File Offset: 0x0014DF00
		public static Padding FlipPadding(Padding padding)
		{
			if (padding.All != -1)
			{
				return padding;
			}
			int num = padding.Top;
			padding.Top = padding.Left;
			padding.Left = num;
			num = padding.Bottom;
			padding.Bottom = padding.Right;
			padding.Right = num;
			return padding;
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x0014FD58 File Offset: 0x0014DF58
		public static Point FlipPoint(Point point)
		{
			int x = point.X;
			point.X = point.Y;
			point.Y = x;
			return point;
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x0014FD84 File Offset: 0x0014DF84
		public static Rectangle FlipRectangle(Rectangle rect)
		{
			rect.Location = LayoutUtils.FlipPoint(rect.Location);
			rect.Size = LayoutUtils.FlipSize(rect.Size);
			return rect;
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x0014FDAD File Offset: 0x0014DFAD
		public static Rectangle FlipRectangleIf(bool condition, Rectangle rect)
		{
			if (!condition)
			{
				return rect;
			}
			return LayoutUtils.FlipRectangle(rect);
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x0014FDBC File Offset: 0x0014DFBC
		public static Size FlipSize(Size size)
		{
			int width = size.Width;
			size.Width = size.Height;
			size.Height = width;
			return size;
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x0014FDE8 File Offset: 0x0014DFE8
		public static Size FlipSizeIf(bool condition, Size size)
		{
			if (!condition)
			{
				return size;
			}
			return LayoutUtils.FlipSize(size);
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x0014FDF5 File Offset: 0x0014DFF5
		public static bool IsHorizontalAlignment(ContentAlignment align)
		{
			return !LayoutUtils.IsVerticalAlignment(align);
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x0014FE00 File Offset: 0x0014E000
		public static bool IsHorizontalRelation(TextImageRelation relation)
		{
			return (relation & (TextImageRelation)12) > TextImageRelation.Overlay;
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x0014FE09 File Offset: 0x0014E009
		public static bool IsVerticalAlignment(ContentAlignment align)
		{
			return (align & (ContentAlignment)514) > (ContentAlignment)0;
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x0014FE15 File Offset: 0x0014E015
		public static bool IsVerticalRelation(TextImageRelation relation)
		{
			return (relation & (TextImageRelation)3) > TextImageRelation.Overlay;
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x0014FE1D File Offset: 0x0014E01D
		public static bool IsZeroWidthOrHeight(Rectangle rectangle)
		{
			return rectangle.Width == 0 || rectangle.Height == 0;
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x0014FE34 File Offset: 0x0014E034
		public static bool IsZeroWidthOrHeight(Size size)
		{
			return size.Width == 0 || size.Height == 0;
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x0014FE4B File Offset: 0x0014E04B
		public static bool AreWidthAndHeightLarger(Size size1, Size size2)
		{
			return size1.Width >= size2.Width && size1.Height >= size2.Height;
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x0014FE74 File Offset: 0x0014E074
		public static void SplitRegion(Rectangle bounds, Size specifiedContent, AnchorStyles region1Align, out Rectangle region1, out Rectangle region2)
		{
			region1 = (region2 = bounds);
			switch (region1Align)
			{
			case AnchorStyles.Top:
				region1.Height = specifiedContent.Height;
				region2.Y += specifiedContent.Height;
				region2.Height -= specifiedContent.Height;
				return;
			case AnchorStyles.Bottom:
				region1.Y += bounds.Height - specifiedContent.Height;
				region1.Height = specifiedContent.Height;
				region2.Height -= specifiedContent.Height;
				break;
			case AnchorStyles.Top | AnchorStyles.Bottom:
				break;
			case AnchorStyles.Left:
				region1.Width = specifiedContent.Width;
				region2.X += specifiedContent.Width;
				region2.Width -= specifiedContent.Width;
				return;
			default:
				if (region1Align != AnchorStyles.Right)
				{
					return;
				}
				region1.X += bounds.Width - specifiedContent.Width;
				region1.Width = specifiedContent.Width;
				region2.Width -= specifiedContent.Width;
				return;
			}
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x0014FF9C File Offset: 0x0014E19C
		public static void ExpandRegionsToFillBounds(Rectangle bounds, AnchorStyles region1Align, ref Rectangle region1, ref Rectangle region2)
		{
			switch (region1Align)
			{
			case AnchorStyles.Top:
				region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Bottom);
				region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Top);
				return;
			case AnchorStyles.Bottom:
				region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Top);
				region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Bottom);
				break;
			case AnchorStyles.Top | AnchorStyles.Bottom:
				break;
			case AnchorStyles.Left:
				region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Right);
				region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Left);
				return;
			default:
				if (region1Align != AnchorStyles.Right)
				{
					return;
				}
				region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Left);
				region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Right);
				return;
			}
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x00150061 File Offset: 0x0014E261
		public static Size SubAlignedRegion(Size currentSize, Size contentSize, TextImageRelation relation)
		{
			return LayoutUtils.SubAlignedRegionCore(currentSize, contentSize, LayoutUtils.IsVerticalRelation(relation));
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x00150070 File Offset: 0x0014E270
		public static Size SubAlignedRegionCore(Size currentSize, Size contentSize, bool vertical)
		{
			if (vertical)
			{
				currentSize.Height -= contentSize.Height;
			}
			else
			{
				currentSize.Width -= contentSize.Width;
			}
			return currentSize;
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x001500A4 File Offset: 0x0014E2A4
		private static Rectangle SubstituteSpecifiedBounds(Rectangle originalBounds, Rectangle substitutionBounds, AnchorStyles specified)
		{
			int num = (((specified & AnchorStyles.Left) != AnchorStyles.None) ? substitutionBounds.Left : originalBounds.Left);
			int num2 = (((specified & AnchorStyles.Top) != AnchorStyles.None) ? substitutionBounds.Top : originalBounds.Top);
			int num3 = (((specified & AnchorStyles.Right) != AnchorStyles.None) ? substitutionBounds.Right : originalBounds.Right);
			int num4 = (((specified & AnchorStyles.Bottom) != AnchorStyles.None) ? substitutionBounds.Bottom : originalBounds.Bottom);
			return Rectangle.FromLTRB(num, num2, num3, num4);
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x00150112 File Offset: 0x0014E312
		public static Rectangle RTLTranslate(Rectangle bounds, Rectangle withinBounds)
		{
			bounds.X = withinBounds.Width - bounds.Right;
			return bounds;
		}

		// Token: 0x0400348E RID: 13454
		public static readonly Size MaxSize = new Size(int.MaxValue, int.MaxValue);

		// Token: 0x0400348F RID: 13455
		public static readonly Size InvalidSize = new Size(int.MinValue, int.MinValue);

		// Token: 0x04003490 RID: 13456
		public static readonly Rectangle MaxRectangle = new Rectangle(0, 0, int.MaxValue, int.MaxValue);

		// Token: 0x04003491 RID: 13457
		public const ContentAlignment AnyTop = (ContentAlignment)7;

		// Token: 0x04003492 RID: 13458
		public const ContentAlignment AnyBottom = (ContentAlignment)1792;

		// Token: 0x04003493 RID: 13459
		public const ContentAlignment AnyLeft = (ContentAlignment)273;

		// Token: 0x04003494 RID: 13460
		public const ContentAlignment AnyRight = (ContentAlignment)1092;

		// Token: 0x04003495 RID: 13461
		public const ContentAlignment AnyCenter = (ContentAlignment)546;

		// Token: 0x04003496 RID: 13462
		public const ContentAlignment AnyMiddle = (ContentAlignment)112;

		// Token: 0x04003497 RID: 13463
		public const AnchorStyles HorizontalAnchorStyles = AnchorStyles.Left | AnchorStyles.Right;

		// Token: 0x04003498 RID: 13464
		public const AnchorStyles VerticalAnchorStyles = AnchorStyles.Top | AnchorStyles.Bottom;

		// Token: 0x04003499 RID: 13465
		private static readonly AnchorStyles[] dockingToAnchor = new AnchorStyles[]
		{
			AnchorStyles.Top | AnchorStyles.Left,
			AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
			AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
			AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
			AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right,
			AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
		};

		// Token: 0x0400349A RID: 13466
		public static readonly string TestString = "j^";

		// Token: 0x02000864 RID: 2148
		public sealed class MeasureTextCache
		{
			// Token: 0x060070D0 RID: 28880 RVA: 0x0019D408 File Offset: 0x0019B608
			public void InvalidateCache()
			{
				this.unconstrainedPreferredSize = LayoutUtils.InvalidSize;
				this.sizeCacheList = null;
			}

			// Token: 0x060070D1 RID: 28881 RVA: 0x0019D41C File Offset: 0x0019B61C
			public Size GetTextSize(string text, Font font, Size proposedConstraints, TextFormatFlags flags)
			{
				if (!this.TextRequiresWordBreak(text, font, proposedConstraints, flags))
				{
					return this.unconstrainedPreferredSize;
				}
				if (this.sizeCacheList == null)
				{
					this.sizeCacheList = new LayoutUtils.MeasureTextCache.PreferredSizeCache[6];
				}
				foreach (LayoutUtils.MeasureTextCache.PreferredSizeCache preferredSizeCache in this.sizeCacheList)
				{
					if (preferredSizeCache.ConstrainingSize == proposedConstraints)
					{
						return preferredSizeCache.PreferredSize;
					}
					Size size = preferredSizeCache.ConstrainingSize;
					if (size.Width == proposedConstraints.Width)
					{
						size = preferredSizeCache.PreferredSize;
						if (size.Height <= proposedConstraints.Height)
						{
							return preferredSizeCache.PreferredSize;
						}
					}
				}
				Size size2 = TextRenderer.MeasureText(text, font, proposedConstraints, flags);
				this.nextCacheEntry = (this.nextCacheEntry + 1) % 6;
				this.sizeCacheList[this.nextCacheEntry] = new LayoutUtils.MeasureTextCache.PreferredSizeCache(proposedConstraints, size2);
				return size2;
			}

			// Token: 0x060070D2 RID: 28882 RVA: 0x0019D4EE File Offset: 0x0019B6EE
			private Size GetUnconstrainedSize(string text, Font font, TextFormatFlags flags)
			{
				if (this.unconstrainedPreferredSize == LayoutUtils.InvalidSize)
				{
					flags &= ~TextFormatFlags.WordBreak;
					this.unconstrainedPreferredSize = TextRenderer.MeasureText(text, font, LayoutUtils.MaxSize, flags);
				}
				return this.unconstrainedPreferredSize;
			}

			// Token: 0x060070D3 RID: 28883 RVA: 0x0019D524 File Offset: 0x0019B724
			public bool TextRequiresWordBreak(string text, Font font, Size size, TextFormatFlags flags)
			{
				return this.GetUnconstrainedSize(text, font, flags).Width > size.Width;
			}

			// Token: 0x040043F3 RID: 17395
			private Size unconstrainedPreferredSize = LayoutUtils.InvalidSize;

			// Token: 0x040043F4 RID: 17396
			private const int MaxCacheSize = 6;

			// Token: 0x040043F5 RID: 17397
			private int nextCacheEntry = -1;

			// Token: 0x040043F6 RID: 17398
			private LayoutUtils.MeasureTextCache.PreferredSizeCache[] sizeCacheList;

			// Token: 0x0200097B RID: 2427
			private struct PreferredSizeCache
			{
				// Token: 0x06007545 RID: 30021 RVA: 0x001A7AE4 File Offset: 0x001A5CE4
				public PreferredSizeCache(Size constrainingSize, Size preferredSize)
				{
					this.ConstrainingSize = constrainingSize;
					this.PreferredSize = preferredSize;
				}

				// Token: 0x040047CB RID: 18379
				public Size ConstrainingSize;

				// Token: 0x040047CC RID: 18380
				public Size PreferredSize;
			}
		}
	}
}
