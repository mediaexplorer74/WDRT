using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004C9 RID: 1225
	internal class FlowLayout : LayoutEngine
	{
		// Token: 0x06005080 RID: 20608 RVA: 0x0014F01C File Offset: 0x0014D21C
		internal static FlowLayoutSettings CreateSettings(IArrangedElement owner)
		{
			return new FlowLayoutSettings(owner);
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x0014F024 File Offset: 0x0014D224
		internal override bool LayoutCore(IArrangedElement container, LayoutEventArgs args)
		{
			CommonProperties.SetLayoutBounds(container, this.xLayout(container, container.DisplayRectangle, false));
			return CommonProperties.GetAutoSize(container);
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x0014F040 File Offset: 0x0014D240
		internal override Size GetPreferredSize(IArrangedElement container, Size proposedConstraints)
		{
			Rectangle rectangle = new Rectangle(new Point(0, 0), proposedConstraints);
			Size size = this.xLayout(container, rectangle, true);
			if (size.Width > proposedConstraints.Width || size.Height > proposedConstraints.Height)
			{
				rectangle.Size = size;
				size = this.xLayout(container, rectangle, true);
			}
			return size;
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x0014F099 File Offset: 0x0014D299
		private static FlowLayout.ContainerProxy CreateContainerProxy(IArrangedElement container, FlowDirection flowDirection)
		{
			switch (flowDirection)
			{
			case FlowDirection.TopDown:
				return new FlowLayout.TopDownProxy(container);
			case FlowDirection.RightToLeft:
				return new FlowLayout.RightToLeftProxy(container);
			case FlowDirection.BottomUp:
				return new FlowLayout.BottomUpProxy(container);
			}
			return new FlowLayout.ContainerProxy(container);
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x0014F0D0 File Offset: 0x0014D2D0
		private Size xLayout(IArrangedElement container, Rectangle displayRect, bool measureOnly)
		{
			FlowDirection flowDirection = FlowLayout.GetFlowDirection(container);
			bool wrapContents = FlowLayout.GetWrapContents(container);
			FlowLayout.ContainerProxy containerProxy = FlowLayout.CreateContainerProxy(container, flowDirection);
			containerProxy.DisplayRect = displayRect;
			displayRect = containerProxy.DisplayRect;
			FlowLayout.ElementProxy elementProxy = containerProxy.ElementProxy;
			Size empty = Size.Empty;
			if (!wrapContents)
			{
				displayRect.Width = int.MaxValue - displayRect.X;
			}
			int num;
			for (int i = 0; i < container.Children.Count; i = num)
			{
				Size size = Size.Empty;
				Rectangle rectangle = new Rectangle(displayRect.X, displayRect.Y, displayRect.Width, displayRect.Height - empty.Height);
				size = this.MeasureRow(containerProxy, elementProxy, i, rectangle, out num);
				if (!measureOnly)
				{
					Rectangle rectangle2 = new Rectangle(displayRect.X, empty.Height + displayRect.Y, size.Width, size.Height);
					this.LayoutRow(containerProxy, elementProxy, i, num, rectangle2);
				}
				empty.Width = Math.Max(empty.Width, size.Width);
				empty.Height += size.Height;
			}
			if (container.Children.Count != 0)
			{
			}
			return LayoutUtils.FlipSizeIf(flowDirection == FlowDirection.TopDown || FlowLayout.GetFlowDirection(container) == FlowDirection.BottomUp, empty);
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x0014F218 File Offset: 0x0014D418
		private void LayoutRow(FlowLayout.ContainerProxy containerProxy, FlowLayout.ElementProxy elementProxy, int startIndex, int endIndex, Rectangle rowBounds)
		{
			int num;
			Size size = this.xLayoutRow(containerProxy, elementProxy, startIndex, endIndex, rowBounds, out num, false);
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x0014F236 File Offset: 0x0014D436
		private Size MeasureRow(FlowLayout.ContainerProxy containerProxy, FlowLayout.ElementProxy elementProxy, int startIndex, Rectangle displayRectangle, out int breakIndex)
		{
			return this.xLayoutRow(containerProxy, elementProxy, startIndex, containerProxy.Container.Children.Count, displayRectangle, out breakIndex, true);
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x0014F258 File Offset: 0x0014D458
		private Size xLayoutRow(FlowLayout.ContainerProxy containerProxy, FlowLayout.ElementProxy elementProxy, int startIndex, int endIndex, Rectangle rowBounds, out int breakIndex, bool measureOnly)
		{
			Point location = rowBounds.Location;
			Size empty = Size.Empty;
			int num = 0;
			breakIndex = startIndex;
			bool wrapContents = FlowLayout.GetWrapContents(containerProxy.Container);
			bool flag = false;
			ArrangedElementCollection children = containerProxy.Container.Children;
			int i = startIndex;
			while (i < endIndex)
			{
				elementProxy.Element = children[i];
				if (elementProxy.ParticipatesInLayout)
				{
					Size size2;
					if (elementProxy.AutoSize)
					{
						Size size = new Size(int.MaxValue, rowBounds.Height - elementProxy.Margin.Size.Height);
						if (i == startIndex)
						{
							size.Width = rowBounds.Width - empty.Width - elementProxy.Margin.Size.Width;
						}
						size = LayoutUtils.UnionSizes(new Size(1, 1), size);
						size2 = elementProxy.GetPreferredSize(size);
					}
					else
					{
						size2 = elementProxy.SpecifiedSize;
						if (elementProxy.Stretches)
						{
							size2.Height = 0;
						}
						if (size2.Height < elementProxy.MinimumSize.Height)
						{
							size2.Height = elementProxy.MinimumSize.Height;
						}
					}
					Size size3 = size2 + elementProxy.Margin.Size;
					if (!measureOnly)
					{
						Rectangle rectangle = new Rectangle(location, new Size(size3.Width, rowBounds.Height));
						rectangle = LayoutUtils.DeflateRect(rectangle, elementProxy.Margin);
						AnchorStyles anchorStyles = elementProxy.AnchorStyles;
						containerProxy.Bounds = LayoutUtils.AlignAndStretch(size2, rectangle, anchorStyles);
					}
					location.X += size3.Width;
					if (num > 0 && location.X > rowBounds.Right)
					{
						break;
					}
					empty.Width = location.X - rowBounds.X;
					empty.Height = Math.Max(empty.Height, size3.Height);
					if (wrapContents)
					{
						if (flag)
						{
							break;
						}
						if (i + 1 < endIndex && CommonProperties.GetFlowBreak(elementProxy.Element))
						{
							if (num != 0)
							{
								breakIndex++;
								break;
							}
							flag = true;
						}
					}
					num++;
				}
				i++;
				breakIndex++;
			}
			return empty;
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x0014F484 File Offset: 0x0014D684
		public static bool GetWrapContents(IArrangedElement container)
		{
			int integer = container.Properties.GetInteger(FlowLayout._wrapContentsProperty);
			return integer == 0;
		}

		// Token: 0x06005089 RID: 20617 RVA: 0x0014F4A6 File Offset: 0x0014D6A6
		public static void SetWrapContents(IArrangedElement container, bool value)
		{
			container.Properties.SetInteger(FlowLayout._wrapContentsProperty, value ? 0 : 1);
			LayoutTransaction.DoLayout(container, container, PropertyNames.WrapContents);
		}

		// Token: 0x0600508A RID: 20618 RVA: 0x0014F4CB File Offset: 0x0014D6CB
		public static FlowDirection GetFlowDirection(IArrangedElement container)
		{
			return (FlowDirection)container.Properties.GetInteger(FlowLayout._flowDirectionProperty);
		}

		// Token: 0x0600508B RID: 20619 RVA: 0x0014F4E0 File Offset: 0x0014D6E0
		public static void SetFlowDirection(IArrangedElement container, FlowDirection value)
		{
			if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
			{
				throw new InvalidEnumArgumentException("value", (int)value, typeof(FlowDirection));
			}
			container.Properties.SetInteger(FlowLayout._flowDirectionProperty, (int)value);
			LayoutTransaction.DoLayout(container, container, PropertyNames.FlowDirection);
		}

		// Token: 0x0600508C RID: 20620 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG_VERIFY_ALIGNMENT")]
		private void Debug_VerifyAlignment(IArrangedElement container, FlowDirection flowDirection)
		{
		}

		// Token: 0x0400348B RID: 13451
		internal static readonly FlowLayout Instance = new FlowLayout();

		// Token: 0x0400348C RID: 13452
		private static readonly int _wrapContentsProperty = PropertyStore.CreateKey();

		// Token: 0x0400348D RID: 13453
		private static readonly int _flowDirectionProperty = PropertyStore.CreateKey();

		// Token: 0x0200085E RID: 2142
		private class ContainerProxy
		{
			// Token: 0x060070AD RID: 28845 RVA: 0x0019D03E File Offset: 0x0019B23E
			public ContainerProxy(IArrangedElement container)
			{
				this._container = container;
				this._isContainerRTL = false;
				if (this._container is Control)
				{
					this._isContainerRTL = ((Control)this._container).RightToLeft == RightToLeft.Yes;
				}
			}

			// Token: 0x17001897 RID: 6295
			// (set) Token: 0x060070AE RID: 28846 RVA: 0x0019D07C File Offset: 0x0019B27C
			public virtual Rectangle Bounds
			{
				set
				{
					if (this.IsContainerRTL)
					{
						if (this.IsVertical)
						{
							value.Y = this.DisplayRect.Bottom - value.Bottom;
						}
						else
						{
							value.X = this.DisplayRect.Right - value.Right;
						}
						FlowLayoutPanel flowLayoutPanel = this.Container as FlowLayoutPanel;
						if (flowLayoutPanel != null)
						{
							Point autoScrollPosition = flowLayoutPanel.AutoScrollPosition;
							if (autoScrollPosition != Point.Empty)
							{
								Point point = new Point(value.X, value.Y);
								if (this.IsVertical)
								{
									point.Offset(0, autoScrollPosition.X);
								}
								else
								{
									point.Offset(autoScrollPosition.X, 0);
								}
								value.Location = point;
							}
						}
					}
					this.ElementProxy.Bounds = value;
				}
			}

			// Token: 0x17001898 RID: 6296
			// (get) Token: 0x060070AF RID: 28847 RVA: 0x0019D14C File Offset: 0x0019B34C
			public IArrangedElement Container
			{
				get
				{
					return this._container;
				}
			}

			// Token: 0x17001899 RID: 6297
			// (get) Token: 0x060070B0 RID: 28848 RVA: 0x0001180C File Offset: 0x0000FA0C
			protected virtual bool IsVertical
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700189A RID: 6298
			// (get) Token: 0x060070B1 RID: 28849 RVA: 0x0019D154 File Offset: 0x0019B354
			protected bool IsContainerRTL
			{
				get
				{
					return this._isContainerRTL;
				}
			}

			// Token: 0x1700189B RID: 6299
			// (get) Token: 0x060070B2 RID: 28850 RVA: 0x0019D15C File Offset: 0x0019B35C
			// (set) Token: 0x060070B3 RID: 28851 RVA: 0x0019D164 File Offset: 0x0019B364
			public Rectangle DisplayRect
			{
				get
				{
					return this._displayRect;
				}
				set
				{
					if (this._displayRect != value)
					{
						this._displayRect = LayoutUtils.FlipRectangleIf(this.IsVertical, value);
					}
				}
			}

			// Token: 0x1700189C RID: 6300
			// (get) Token: 0x060070B4 RID: 28852 RVA: 0x0019D186 File Offset: 0x0019B386
			public FlowLayout.ElementProxy ElementProxy
			{
				get
				{
					if (this._elementProxy == null)
					{
						this._elementProxy = (this.IsVertical ? new FlowLayout.VerticalElementProxy() : new FlowLayout.ElementProxy());
					}
					return this._elementProxy;
				}
			}

			// Token: 0x060070B5 RID: 28853 RVA: 0x0019D1B0 File Offset: 0x0019B3B0
			protected Rectangle RTLTranslateNoMarginSwap(Rectangle bounds)
			{
				Rectangle rectangle = bounds;
				rectangle.X = this.DisplayRect.Right - bounds.X - bounds.Width + this.ElementProxy.Margin.Left - this.ElementProxy.Margin.Right;
				FlowLayoutPanel flowLayoutPanel = this.Container as FlowLayoutPanel;
				if (flowLayoutPanel != null)
				{
					Point autoScrollPosition = flowLayoutPanel.AutoScrollPosition;
					if (autoScrollPosition != Point.Empty)
					{
						Point point = new Point(rectangle.X, rectangle.Y);
						if (this.IsVertical)
						{
							point.Offset(autoScrollPosition.Y, 0);
						}
						else
						{
							point.Offset(autoScrollPosition.X, 0);
						}
						rectangle.Location = point;
					}
				}
				return rectangle;
			}

			// Token: 0x040043EE RID: 17390
			private IArrangedElement _container;

			// Token: 0x040043EF RID: 17391
			private FlowLayout.ElementProxy _elementProxy;

			// Token: 0x040043F0 RID: 17392
			private Rectangle _displayRect;

			// Token: 0x040043F1 RID: 17393
			private bool _isContainerRTL;
		}

		// Token: 0x0200085F RID: 2143
		private class RightToLeftProxy : FlowLayout.ContainerProxy
		{
			// Token: 0x060070B6 RID: 28854 RVA: 0x0019D278 File Offset: 0x0019B478
			public RightToLeftProxy(IArrangedElement container)
				: base(container)
			{
			}

			// Token: 0x1700189D RID: 6301
			// (set) Token: 0x060070B7 RID: 28855 RVA: 0x0019D281 File Offset: 0x0019B481
			public override Rectangle Bounds
			{
				set
				{
					base.Bounds = base.RTLTranslateNoMarginSwap(value);
				}
			}
		}

		// Token: 0x02000860 RID: 2144
		private class TopDownProxy : FlowLayout.ContainerProxy
		{
			// Token: 0x060070B8 RID: 28856 RVA: 0x0019D278 File Offset: 0x0019B478
			public TopDownProxy(IArrangedElement container)
				: base(container)
			{
			}

			// Token: 0x1700189E RID: 6302
			// (get) Token: 0x060070B9 RID: 28857 RVA: 0x00012E4E File Offset: 0x0001104E
			protected override bool IsVertical
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x02000861 RID: 2145
		private class BottomUpProxy : FlowLayout.ContainerProxy
		{
			// Token: 0x060070BA RID: 28858 RVA: 0x0019D278 File Offset: 0x0019B478
			public BottomUpProxy(IArrangedElement container)
				: base(container)
			{
			}

			// Token: 0x1700189F RID: 6303
			// (get) Token: 0x060070BB RID: 28859 RVA: 0x00012E4E File Offset: 0x0001104E
			protected override bool IsVertical
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170018A0 RID: 6304
			// (set) Token: 0x060070BC RID: 28860 RVA: 0x0019D281 File Offset: 0x0019B481
			public override Rectangle Bounds
			{
				set
				{
					base.Bounds = base.RTLTranslateNoMarginSwap(value);
				}
			}
		}

		// Token: 0x02000862 RID: 2146
		private class ElementProxy
		{
			// Token: 0x170018A1 RID: 6305
			// (get) Token: 0x060070BD RID: 28861 RVA: 0x0019D290 File Offset: 0x0019B490
			public virtual AnchorStyles AnchorStyles
			{
				get
				{
					AnchorStyles unifiedAnchor = LayoutUtils.GetUnifiedAnchor(this.Element);
					bool flag = (unifiedAnchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom);
					bool flag2 = (unifiedAnchor & AnchorStyles.Top) > AnchorStyles.None;
					bool flag3 = (unifiedAnchor & AnchorStyles.Bottom) > AnchorStyles.None;
					if (flag)
					{
						return AnchorStyles.Top | AnchorStyles.Bottom;
					}
					if (flag2)
					{
						return AnchorStyles.Top;
					}
					if (flag3)
					{
						return AnchorStyles.Bottom;
					}
					return AnchorStyles.None;
				}
			}

			// Token: 0x170018A2 RID: 6306
			// (get) Token: 0x060070BE RID: 28862 RVA: 0x0019D2CE File Offset: 0x0019B4CE
			public bool AutoSize
			{
				get
				{
					return CommonProperties.GetAutoSize(this._element);
				}
			}

			// Token: 0x170018A3 RID: 6307
			// (set) Token: 0x060070BF RID: 28863 RVA: 0x0019D2DB File Offset: 0x0019B4DB
			public virtual Rectangle Bounds
			{
				set
				{
					this._element.SetBounds(value, BoundsSpecified.None);
				}
			}

			// Token: 0x170018A4 RID: 6308
			// (get) Token: 0x060070C0 RID: 28864 RVA: 0x0019D2EA File Offset: 0x0019B4EA
			// (set) Token: 0x060070C1 RID: 28865 RVA: 0x0019D2F2 File Offset: 0x0019B4F2
			public IArrangedElement Element
			{
				get
				{
					return this._element;
				}
				set
				{
					this._element = value;
				}
			}

			// Token: 0x170018A5 RID: 6309
			// (get) Token: 0x060070C2 RID: 28866 RVA: 0x0019D2FC File Offset: 0x0019B4FC
			public bool Stretches
			{
				get
				{
					AnchorStyles anchorStyles = this.AnchorStyles;
					return ((AnchorStyles.Top | AnchorStyles.Bottom) & anchorStyles) == (AnchorStyles.Top | AnchorStyles.Bottom);
				}
			}

			// Token: 0x170018A6 RID: 6310
			// (get) Token: 0x060070C3 RID: 28867 RVA: 0x0019D319 File Offset: 0x0019B519
			public virtual Padding Margin
			{
				get
				{
					return CommonProperties.GetMargin(this.Element);
				}
			}

			// Token: 0x170018A7 RID: 6311
			// (get) Token: 0x060070C4 RID: 28868 RVA: 0x0019D326 File Offset: 0x0019B526
			public virtual Size MinimumSize
			{
				get
				{
					return CommonProperties.GetMinimumSize(this.Element, Size.Empty);
				}
			}

			// Token: 0x170018A8 RID: 6312
			// (get) Token: 0x060070C5 RID: 28869 RVA: 0x0019D338 File Offset: 0x0019B538
			public bool ParticipatesInLayout
			{
				get
				{
					return this._element.ParticipatesInLayout;
				}
			}

			// Token: 0x170018A9 RID: 6313
			// (get) Token: 0x060070C6 RID: 28870 RVA: 0x0019D348 File Offset: 0x0019B548
			public virtual Size SpecifiedSize
			{
				get
				{
					return CommonProperties.GetSpecifiedBounds(this._element).Size;
				}
			}

			// Token: 0x060070C7 RID: 28871 RVA: 0x0019D368 File Offset: 0x0019B568
			public virtual Size GetPreferredSize(Size proposedSize)
			{
				return this._element.GetPreferredSize(proposedSize);
			}

			// Token: 0x040043F2 RID: 17394
			private IArrangedElement _element;
		}

		// Token: 0x02000863 RID: 2147
		private class VerticalElementProxy : FlowLayout.ElementProxy
		{
			// Token: 0x170018AA RID: 6314
			// (get) Token: 0x060070C9 RID: 28873 RVA: 0x0019D378 File Offset: 0x0019B578
			public override AnchorStyles AnchorStyles
			{
				get
				{
					AnchorStyles unifiedAnchor = LayoutUtils.GetUnifiedAnchor(base.Element);
					bool flag = (unifiedAnchor & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right);
					bool flag2 = (unifiedAnchor & AnchorStyles.Left) > AnchorStyles.None;
					bool flag3 = (unifiedAnchor & AnchorStyles.Right) > AnchorStyles.None;
					if (flag)
					{
						return AnchorStyles.Top | AnchorStyles.Bottom;
					}
					if (flag2)
					{
						return AnchorStyles.Top;
					}
					if (flag3)
					{
						return AnchorStyles.Bottom;
					}
					return AnchorStyles.None;
				}
			}

			// Token: 0x170018AB RID: 6315
			// (set) Token: 0x060070CA RID: 28874 RVA: 0x0019D3B8 File Offset: 0x0019B5B8
			public override Rectangle Bounds
			{
				set
				{
					base.Bounds = LayoutUtils.FlipRectangle(value);
				}
			}

			// Token: 0x170018AC RID: 6316
			// (get) Token: 0x060070CB RID: 28875 RVA: 0x0019D3C6 File Offset: 0x0019B5C6
			public override Padding Margin
			{
				get
				{
					return LayoutUtils.FlipPadding(base.Margin);
				}
			}

			// Token: 0x170018AD RID: 6317
			// (get) Token: 0x060070CC RID: 28876 RVA: 0x0019D3D3 File Offset: 0x0019B5D3
			public override Size MinimumSize
			{
				get
				{
					return LayoutUtils.FlipSize(base.MinimumSize);
				}
			}

			// Token: 0x170018AE RID: 6318
			// (get) Token: 0x060070CD RID: 28877 RVA: 0x0019D3E0 File Offset: 0x0019B5E0
			public override Size SpecifiedSize
			{
				get
				{
					return LayoutUtils.FlipSize(base.SpecifiedSize);
				}
			}

			// Token: 0x060070CE RID: 28878 RVA: 0x0019D3ED File Offset: 0x0019B5ED
			public override Size GetPreferredSize(Size proposedSize)
			{
				return LayoutUtils.FlipSize(base.GetPreferredSize(LayoutUtils.FlipSize(proposedSize)));
			}
		}
	}
}
