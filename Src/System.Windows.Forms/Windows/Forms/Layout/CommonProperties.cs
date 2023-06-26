using System;
using System.Collections.Specialized;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004C7 RID: 1223
	internal class CommonProperties
	{
		// Token: 0x06005037 RID: 20535 RVA: 0x0014D45B File Offset: 0x0014B65B
		internal static void ClearMaximumSize(IArrangedElement element)
		{
			if (element.Properties.ContainsObject(CommonProperties._maximumSizeProperty))
			{
				element.Properties.RemoveObject(CommonProperties._maximumSizeProperty);
			}
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x0014D480 File Offset: 0x0014B680
		internal static bool GetAutoSize(IArrangedElement element)
		{
			int num = CommonProperties.GetLayoutState(element)[CommonProperties._autoSizeSection];
			return num != 0;
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x0014D4A8 File Offset: 0x0014B6A8
		internal static Padding GetMargin(IArrangedElement element)
		{
			bool flag;
			Padding padding = element.Properties.GetPadding(CommonProperties._marginProperty, out flag);
			if (flag)
			{
				return padding;
			}
			return CommonProperties.DefaultMargin;
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x0014D4D4 File Offset: 0x0014B6D4
		internal static Size GetMaximumSize(IArrangedElement element, Size defaultMaximumSize)
		{
			bool flag;
			Size size = element.Properties.GetSize(CommonProperties._maximumSizeProperty, out flag);
			if (flag)
			{
				return size;
			}
			return defaultMaximumSize;
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0014D4FC File Offset: 0x0014B6FC
		internal static Size GetMinimumSize(IArrangedElement element, Size defaultMinimumSize)
		{
			bool flag;
			Size size = element.Properties.GetSize(CommonProperties._minimumSizeProperty, out flag);
			if (flag)
			{
				return size;
			}
			return defaultMinimumSize;
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x0014D524 File Offset: 0x0014B724
		internal static Padding GetPadding(IArrangedElement element, Padding defaultPadding)
		{
			bool flag;
			Padding padding = element.Properties.GetPadding(CommonProperties._paddingProperty, out flag);
			if (flag)
			{
				return padding;
			}
			return defaultPadding;
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x0014D54C File Offset: 0x0014B74C
		internal static Rectangle GetSpecifiedBounds(IArrangedElement element)
		{
			bool flag;
			Rectangle rectangle = element.Properties.GetRectangle(CommonProperties._specifiedBoundsProperty, out flag);
			if (flag && rectangle != LayoutUtils.MaxRectangle)
			{
				return rectangle;
			}
			return element.Bounds;
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x0014D584 File Offset: 0x0014B784
		internal static void ResetPadding(IArrangedElement element)
		{
			object @object = element.Properties.GetObject(CommonProperties._paddingProperty);
			if (@object != null)
			{
				element.Properties.RemoveObject(CommonProperties._paddingProperty);
			}
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x0014D5B8 File Offset: 0x0014B7B8
		internal static void SetAutoSize(IArrangedElement element, bool value)
		{
			BitVector32 layoutState = CommonProperties.GetLayoutState(element);
			layoutState[CommonProperties._autoSizeSection] = (value ? 1 : 0);
			CommonProperties.SetLayoutState(element, layoutState);
			if (!value)
			{
				element.SetBounds(CommonProperties.GetSpecifiedBounds(element), BoundsSpecified.None);
			}
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x0014D5F6 File Offset: 0x0014B7F6
		internal static void SetMargin(IArrangedElement element, Padding value)
		{
			element.Properties.SetPadding(CommonProperties._marginProperty, value);
			LayoutTransaction.DoLayout(element.Container, element, PropertyNames.Margin);
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x0014D61C File Offset: 0x0014B81C
		internal static void SetMaximumSize(IArrangedElement element, Size value)
		{
			element.Properties.SetSize(CommonProperties._maximumSizeProperty, value);
			Rectangle bounds = element.Bounds;
			bounds.Width = Math.Min(bounds.Width, value.Width);
			bounds.Height = Math.Min(bounds.Height, value.Height);
			element.SetBounds(bounds, BoundsSpecified.Size);
			LayoutTransaction.DoLayout(element.Container, element, PropertyNames.MaximumSize);
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x0014D690 File Offset: 0x0014B890
		internal static void SetMinimumSize(IArrangedElement element, Size value)
		{
			element.Properties.SetSize(CommonProperties._minimumSizeProperty, value);
			using (new LayoutTransaction(element.Container as Control, element, PropertyNames.MinimumSize))
			{
				Rectangle bounds = element.Bounds;
				bounds.Width = Math.Max(bounds.Width, value.Width);
				bounds.Height = Math.Max(bounds.Height, value.Height);
				element.SetBounds(bounds, BoundsSpecified.Size);
			}
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x0014D728 File Offset: 0x0014B928
		internal static void SetPadding(IArrangedElement element, Padding value)
		{
			value = LayoutUtils.ClampNegativePaddingToZero(value);
			element.Properties.SetPadding(CommonProperties._paddingProperty, value);
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x0014D744 File Offset: 0x0014B944
		internal static void UpdateSpecifiedBounds(IArrangedElement element, int x, int y, int width, int height, BoundsSpecified specified)
		{
			Rectangle specifiedBounds = CommonProperties.GetSpecifiedBounds(element);
			bool flag = ((specified & BoundsSpecified.X) == BoundsSpecified.None) & (x != specifiedBounds.X);
			bool flag2 = ((specified & BoundsSpecified.Y) == BoundsSpecified.None) & (y != specifiedBounds.Y);
			bool flag3 = ((specified & BoundsSpecified.Width) == BoundsSpecified.None) & (width != specifiedBounds.Width);
			bool flag4 = ((specified & BoundsSpecified.Height) == BoundsSpecified.None) & (height != specifiedBounds.Height);
			if (flag || flag2 || flag3 || flag4)
			{
				if (!flag)
				{
					specifiedBounds.X = x;
				}
				if (!flag2)
				{
					specifiedBounds.Y = y;
				}
				if (!flag3)
				{
					specifiedBounds.Width = width;
				}
				if (!flag4)
				{
					specifiedBounds.Height = height;
				}
				element.Properties.SetRectangle(CommonProperties._specifiedBoundsProperty, specifiedBounds);
				return;
			}
			if (element.Properties.ContainsObject(CommonProperties._specifiedBoundsProperty))
			{
				element.Properties.SetRectangle(CommonProperties._specifiedBoundsProperty, LayoutUtils.MaxRectangle);
			}
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x0014D824 File Offset: 0x0014BA24
		internal static void UpdateSpecifiedBounds(IArrangedElement element, int x, int y, int width, int height)
		{
			Rectangle rectangle = new Rectangle(x, y, width, height);
			element.Properties.SetRectangle(CommonProperties._specifiedBoundsProperty, rectangle);
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x0014D84E File Offset: 0x0014BA4E
		internal static void xClearPreferredSizeCache(IArrangedElement element)
		{
			element.Properties.SetSize(CommonProperties._preferredSizeCacheProperty, LayoutUtils.InvalidSize);
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x0014D868 File Offset: 0x0014BA68
		internal static void xClearAllPreferredSizeCaches(IArrangedElement start)
		{
			CommonProperties.xClearPreferredSizeCache(start);
			ArrangedElementCollection children = start.Children;
			for (int i = 0; i < children.Count; i++)
			{
				CommonProperties.xClearAllPreferredSizeCaches(children[i]);
			}
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x0014D8A0 File Offset: 0x0014BAA0
		internal static Size xGetPreferredSizeCache(IArrangedElement element)
		{
			bool flag;
			Size size = element.Properties.GetSize(CommonProperties._preferredSizeCacheProperty, out flag);
			if (flag && size != LayoutUtils.InvalidSize)
			{
				return size;
			}
			return Size.Empty;
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x0014D8D7 File Offset: 0x0014BAD7
		internal static void xSetPreferredSizeCache(IArrangedElement element, Size value)
		{
			element.Properties.SetSize(CommonProperties._preferredSizeCacheProperty, value);
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x0014D8EC File Offset: 0x0014BAEC
		internal static AutoSizeMode GetAutoSizeMode(IArrangedElement element)
		{
			if (CommonProperties.GetLayoutState(element)[CommonProperties._autoSizeModeSection] != 0)
			{
				return AutoSizeMode.GrowAndShrink;
			}
			return AutoSizeMode.GrowOnly;
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x0014D914 File Offset: 0x0014BB14
		internal static bool GetNeedsDockAndAnchorLayout(IArrangedElement element)
		{
			return CommonProperties.GetLayoutState(element)[CommonProperties._dockAndAnchorNeedsLayoutSection] != 0;
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x0014D93C File Offset: 0x0014BB3C
		internal static bool GetNeedsAnchorLayout(IArrangedElement element)
		{
			BitVector32 layoutState = CommonProperties.GetLayoutState(element);
			return layoutState[CommonProperties._dockAndAnchorNeedsLayoutSection] != 0 && layoutState[CommonProperties._dockModeSection] == 0;
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x0014D974 File Offset: 0x0014BB74
		internal static bool GetNeedsDockLayout(IArrangedElement element)
		{
			return CommonProperties.GetLayoutState(element)[CommonProperties._dockModeSection] == 1 && element.ParticipatesInLayout;
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x0014D9A4 File Offset: 0x0014BBA4
		internal static bool GetSelfAutoSizeInDefaultLayout(IArrangedElement element)
		{
			int num = CommonProperties.GetLayoutState(element)[CommonProperties._selfAutoSizingSection];
			return num == 1;
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x0014D9CC File Offset: 0x0014BBCC
		internal static void SetAutoSizeMode(IArrangedElement element, AutoSizeMode mode)
		{
			BitVector32 layoutState = CommonProperties.GetLayoutState(element);
			layoutState[CommonProperties._autoSizeModeSection] = ((mode == AutoSizeMode.GrowAndShrink) ? 1 : 0);
			CommonProperties.SetLayoutState(element, layoutState);
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x0014D9FA File Offset: 0x0014BBFA
		internal static bool ShouldSelfSize(IArrangedElement element)
		{
			return !CommonProperties.GetAutoSize(element) || (element.Container is Control && ((Control)element.Container).LayoutEngine is DefaultLayout && CommonProperties.GetSelfAutoSizeInDefaultLayout(element));
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x0014DA34 File Offset: 0x0014BC34
		internal static void SetSelfAutoSizeInDefaultLayout(IArrangedElement element, bool value)
		{
			BitVector32 layoutState = CommonProperties.GetLayoutState(element);
			layoutState[CommonProperties._selfAutoSizingSection] = (value ? 1 : 0);
			CommonProperties.SetLayoutState(element, layoutState);
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x0014DA64 File Offset: 0x0014BC64
		internal static AnchorStyles xGetAnchor(IArrangedElement element)
		{
			BitVector32 layoutState = CommonProperties.GetLayoutState(element);
			AnchorStyles anchorStyles = (AnchorStyles)layoutState[CommonProperties._dockAndAnchorSection];
			CommonProperties.DockAnchorMode dockAnchorMode = (CommonProperties.DockAnchorMode)layoutState[CommonProperties._dockModeSection];
			return (dockAnchorMode == CommonProperties.DockAnchorMode.Anchor) ? CommonProperties.xTranslateAnchorValue(anchorStyles) : (AnchorStyles.Top | AnchorStyles.Left);
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x0014DAA0 File Offset: 0x0014BCA0
		internal static bool xGetAutoSizedAndAnchored(IArrangedElement element)
		{
			BitVector32 layoutState = CommonProperties.GetLayoutState(element);
			return layoutState[CommonProperties._selfAutoSizingSection] == 0 && layoutState[CommonProperties._autoSizeSection] != 0 && layoutState[CommonProperties._dockModeSection] == 0;
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x0014DAE8 File Offset: 0x0014BCE8
		internal static DockStyle xGetDock(IArrangedElement element)
		{
			BitVector32 layoutState = CommonProperties.GetLayoutState(element);
			DockStyle dockStyle = (DockStyle)layoutState[CommonProperties._dockAndAnchorSection];
			CommonProperties.DockAnchorMode dockAnchorMode = (CommonProperties.DockAnchorMode)layoutState[CommonProperties._dockModeSection];
			return (dockAnchorMode == CommonProperties.DockAnchorMode.Dock) ? dockStyle : DockStyle.None;
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x0014DB20 File Offset: 0x0014BD20
		internal static void xSetAnchor(IArrangedElement element, AnchorStyles value)
		{
			BitVector32 layoutState = CommonProperties.GetLayoutState(element);
			layoutState[CommonProperties._dockAndAnchorSection] = (int)CommonProperties.xTranslateAnchorValue(value);
			layoutState[CommonProperties._dockModeSection] = 0;
			CommonProperties.SetLayoutState(element, layoutState);
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x0014DB5C File Offset: 0x0014BD5C
		internal static void xSetDock(IArrangedElement element, DockStyle value)
		{
			BitVector32 layoutState = CommonProperties.GetLayoutState(element);
			layoutState[CommonProperties._dockAndAnchorSection] = (int)value;
			layoutState[CommonProperties._dockModeSection] = ((value == DockStyle.None) ? 0 : 1);
			CommonProperties.SetLayoutState(element, layoutState);
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x0014DB97 File Offset: 0x0014BD97
		private static AnchorStyles xTranslateAnchorValue(AnchorStyles anchor)
		{
			if (anchor == AnchorStyles.None)
			{
				return AnchorStyles.Top | AnchorStyles.Left;
			}
			if (anchor != (AnchorStyles.Top | AnchorStyles.Left))
			{
				return anchor;
			}
			return AnchorStyles.None;
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x0014DBA8 File Offset: 0x0014BDA8
		internal static bool GetFlowBreak(IArrangedElement element)
		{
			int num = CommonProperties.GetLayoutState(element)[CommonProperties._flowBreakSection];
			return num == 1;
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x0014DBD0 File Offset: 0x0014BDD0
		internal static void SetFlowBreak(IArrangedElement element, bool value)
		{
			BitVector32 layoutState = CommonProperties.GetLayoutState(element);
			layoutState[CommonProperties._flowBreakSection] = (value ? 1 : 0);
			CommonProperties.SetLayoutState(element, layoutState);
			LayoutTransaction.DoLayout(element.Container, element, PropertyNames.FlowBreak);
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x0014DC10 File Offset: 0x0014BE10
		internal static Size GetLayoutBounds(IArrangedElement element)
		{
			bool flag;
			Size size = element.Properties.GetSize(CommonProperties._layoutBoundsProperty, out flag);
			if (flag)
			{
				return size;
			}
			return Size.Empty;
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x0014DC3A File Offset: 0x0014BE3A
		internal static void SetLayoutBounds(IArrangedElement element, Size value)
		{
			element.Properties.SetSize(CommonProperties._layoutBoundsProperty, value);
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x0014DC50 File Offset: 0x0014BE50
		internal static bool HasLayoutBounds(IArrangedElement element)
		{
			bool flag;
			element.Properties.GetSize(CommonProperties._layoutBoundsProperty, out flag);
			return flag;
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x0014DC71 File Offset: 0x0014BE71
		internal static BitVector32 GetLayoutState(IArrangedElement element)
		{
			return new BitVector32(element.Properties.GetInteger(CommonProperties._layoutStateProperty));
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x0014DC88 File Offset: 0x0014BE88
		internal static void SetLayoutState(IArrangedElement element, BitVector32 state)
		{
			element.Properties.SetInteger(CommonProperties._layoutStateProperty, state.Data);
		}

		// Token: 0x04003470 RID: 13424
		private static readonly int _layoutStateProperty = PropertyStore.CreateKey();

		// Token: 0x04003471 RID: 13425
		private static readonly int _specifiedBoundsProperty = PropertyStore.CreateKey();

		// Token: 0x04003472 RID: 13426
		private static readonly int _preferredSizeCacheProperty = PropertyStore.CreateKey();

		// Token: 0x04003473 RID: 13427
		private static readonly int _paddingProperty = PropertyStore.CreateKey();

		// Token: 0x04003474 RID: 13428
		private static readonly int _marginProperty = PropertyStore.CreateKey();

		// Token: 0x04003475 RID: 13429
		private static readonly int _minimumSizeProperty = PropertyStore.CreateKey();

		// Token: 0x04003476 RID: 13430
		private static readonly int _maximumSizeProperty = PropertyStore.CreateKey();

		// Token: 0x04003477 RID: 13431
		private static readonly int _layoutBoundsProperty = PropertyStore.CreateKey();

		// Token: 0x04003478 RID: 13432
		internal const ContentAlignment DefaultAlignment = ContentAlignment.TopLeft;

		// Token: 0x04003479 RID: 13433
		internal const AnchorStyles DefaultAnchor = AnchorStyles.Top | AnchorStyles.Left;

		// Token: 0x0400347A RID: 13434
		internal const bool DefaultAutoSize = false;

		// Token: 0x0400347B RID: 13435
		internal const DockStyle DefaultDock = DockStyle.None;

		// Token: 0x0400347C RID: 13436
		internal static readonly Padding DefaultMargin = new Padding(3);

		// Token: 0x0400347D RID: 13437
		internal static readonly Size DefaultMinimumSize = new Size(0, 0);

		// Token: 0x0400347E RID: 13438
		internal static readonly Size DefaultMaximumSize = new Size(0, 0);

		// Token: 0x0400347F RID: 13439
		private static readonly BitVector32.Section _dockAndAnchorNeedsLayoutSection = BitVector32.CreateSection(127);

		// Token: 0x04003480 RID: 13440
		private static readonly BitVector32.Section _dockAndAnchorSection = BitVector32.CreateSection(15);

		// Token: 0x04003481 RID: 13441
		private static readonly BitVector32.Section _dockModeSection = BitVector32.CreateSection(1, CommonProperties._dockAndAnchorSection);

		// Token: 0x04003482 RID: 13442
		private static readonly BitVector32.Section _autoSizeSection = BitVector32.CreateSection(1, CommonProperties._dockModeSection);

		// Token: 0x04003483 RID: 13443
		private static readonly BitVector32.Section _BoxStretchInternalSection = BitVector32.CreateSection(3, CommonProperties._autoSizeSection);

		// Token: 0x04003484 RID: 13444
		private static readonly BitVector32.Section _anchorNeverShrinksSection = BitVector32.CreateSection(1, CommonProperties._BoxStretchInternalSection);

		// Token: 0x04003485 RID: 13445
		private static readonly BitVector32.Section _flowBreakSection = BitVector32.CreateSection(1, CommonProperties._anchorNeverShrinksSection);

		// Token: 0x04003486 RID: 13446
		private static readonly BitVector32.Section _selfAutoSizingSection = BitVector32.CreateSection(1, CommonProperties._flowBreakSection);

		// Token: 0x04003487 RID: 13447
		private static readonly BitVector32.Section _autoSizeModeSection = BitVector32.CreateSection(1, CommonProperties._selfAutoSizingSection);

		// Token: 0x0200085B RID: 2139
		private enum DockAnchorMode
		{
			// Token: 0x040043E2 RID: 17378
			Anchor,
			// Token: 0x040043E3 RID: 17379
			Dock
		}
	}
}
