using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000C9 RID: 201
	[TemplateVisualState(Name = "ShowingPage", GroupName = "FlipStates")]
	[TemplateVisualState(Name = "FlipNext", GroupName = "FlipStates")]
	[TemplateVisualState(Name = "FlipPrevious", GroupName = "FlipStates")]
	[TemplatePart(Name = "PART_SwitchNextButton", Type = typeof(Button))]
	[TemplatePart(Name = "PART_SwitchNextButton", Type = typeof(Button))]
	[TemplatePart(Name = "PART_ItemsPresenter", Type = typeof(ItemsPresenter))]
	[TemplatePart(Name = "PART_ItemsScrollViewer", Type = typeof(ScrollViewer))]
	public class FlipListView : ListView
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x0001CB90 File Offset: 0x0001AD90
		static FlipListView()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(FlipListView), new FrameworkPropertyMetadata(typeof(FlipListView)));
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001CC9E File Offset: 0x0001AE9E
		public FlipListView()
		{
			base.Loaded += this.OnLoaded;
			base.LayoutUpdated += this.OnLayoutUpdated;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001CCD0 File Offset: 0x0001AED0
		protected override AutomationPeer OnCreateAutomationPeer()
		{
			return new FlipListView.FlipListViewAutomationPeer(this);
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0001CCE8 File Offset: 0x0001AEE8
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0001CD0A File Offset: 0x0001AF0A
		public Orientation ItemsOrientantion
		{
			get
			{
				return (Orientation)base.GetValue(FlipListView.ItemsOrientantionProperty);
			}
			set
			{
				base.SetValue(FlipListView.ItemsOrientantionProperty, value);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001CD20 File Offset: 0x0001AF20
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0001CD42 File Offset: 0x0001AF42
		public Style SwitchNextButtonStyle
		{
			get
			{
				return (Style)base.GetValue(FlipListView.SwitchNextButtonStyleProperty);
			}
			set
			{
				base.SetValue(FlipListView.SwitchNextButtonStyleProperty, value);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001CD54 File Offset: 0x0001AF54
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001CD76 File Offset: 0x0001AF76
		public Style SwitchPreviousButtonStyle
		{
			get
			{
				return (Style)base.GetValue(FlipListView.SwitchPreviousButtonStyleProperty);
			}
			set
			{
				base.SetValue(FlipListView.SwitchPreviousButtonStyleProperty, value);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001CD88 File Offset: 0x0001AF88
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0001CDAA File Offset: 0x0001AFAA
		public bool CanFlipNext
		{
			get
			{
				return (bool)base.GetValue(FlipListView.CanFlipNextProperty);
			}
			private set
			{
				base.SetValue(FlipListView.CanFlipNextProperty, value);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001CDC0 File Offset: 0x0001AFC0
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001CDE2 File Offset: 0x0001AFE2
		public bool CanFlipPrevious
		{
			get
			{
				return (bool)base.GetValue(FlipListView.CanFlipPreviousProperty);
			}
			private set
			{
				base.SetValue(FlipListView.CanFlipPreviousProperty, value);
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001CDF7 File Offset: 0x0001AFF7
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.OnApplySwitchNextButtonTemplate();
			this.OnApplySwitchPrevButtonTemplate();
			this.OnApplyItemsPresenterTemplate();
			this.OnApplyItemsScrollViewerTemplate();
			this.border = base.GetTemplateChild("Border") as Border;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001CE34 File Offset: 0x0001B034
		protected override void OnPreviewKeyUp(KeyEventArgs e)
		{
			base.OnPreviewKeyUp(e);
			bool flag = e.Key == Key.Next;
			if (flag)
			{
				this.SwitchPrevButtonPartOnClick(this, e);
			}
			bool flag2 = e.Key == Key.Prior;
			if (flag2)
			{
				this.SwitchNextButtonPartOnClick(this, e);
			}
			bool flag3 = e.Key == Key.End;
			if (flag3)
			{
				bool flag4 = !this.ScrollToLastPage();
				if (flag4)
				{
					this.itemsScrollViewer.ScrollToRightEnd();
				}
				this.GoToState("FlipNext", true);
			}
			bool flag5 = e.Key == Key.Home;
			if (flag5)
			{
				bool flag6 = !this.ScrollToFirstPage();
				if (flag6)
				{
					this.itemsScrollViewer.ScrollToLeftEnd();
				}
				this.GoToState("FlipNext", true);
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001CEF0 File Offset: 0x0001B0F0
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			base.OnPreviewKeyDown(e);
			bool flag = e.Key == Key.Right && this.buttonFocused == this.switchNextButtonPart;
			if (flag)
			{
				this.SwitchNextButtonPartOnClick(this, e);
			}
			bool flag2 = e.Key == Key.Left && this.buttonFocused == this.switchPrevButtonPart;
			if (flag2)
			{
				this.SwitchPrevButtonPartOnClick(this, e);
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001CF5A File Offset: 0x0001B15A
		protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
		{
			base.OnItemsSourceChanged(oldValue, newValue);
			this.GoToState("FlipNext", true);
			this.SetItemsPresenterPagedSize();
			this.ComputePagesData();
			this.ScrollToFirstPage();
			this.ValidateCanFlipProperties();
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001CF90 File Offset: 0x0001B190
		protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
		{
			base.OnItemsChanged(e);
			bool flag = e.Action != NotifyCollectionChangedAction.Reset;
			if (flag)
			{
				this.ComputePagesData();
				this.ScrollToFirstPage();
			}
			else
			{
				bool flag2 = e.Action == NotifyCollectionChangedAction.Reset;
				if (flag2)
				{
					this.GoToState("FlipNext", true);
				}
			}
			this.ValidateCanFlipProperties();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001CFEC File Offset: 0x0001B1EC
		private static bool IsFullyVisible(FrameworkElement element, FrameworkElement container)
		{
			Rect rect = element.TransformToAncestor(container).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
			Rect rect2 = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
			return rect2.Contains(rect.TopLeft) && rect2.Contains(rect.BottomRight);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001D074 File Offset: 0x0001B274
		private void OnApplyItemsScrollViewerTemplate()
		{
			bool flag = this.itemsScrollViewer != null;
			if (flag)
			{
				this.itemsScrollViewer.ScrollChanged -= this.ItemsScrollViewerOnScrollChanged;
				this.itemsScrollViewer.Loaded -= this.ItemsScrollViewerOnLoaded;
			}
			this.itemsScrollViewer = base.GetTemplateChild("PART_ItemsScrollViewer") as ScrollViewer;
			bool flag2 = this.itemsScrollViewer != null;
			if (flag2)
			{
				this.itemsScrollViewer.ScrollChanged += this.ItemsScrollViewerOnScrollChanged;
				this.itemsScrollViewer.Loaded += this.ItemsScrollViewerOnLoaded;
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001D116 File Offset: 0x0001B316
		private void ItemsScrollViewerOnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.SetItemsPresenterPagedSize();
			this.ComputePagesData();
			this.ScrollToFirstPage();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001D130 File Offset: 0x0001B330
		private void ItemsScrollViewerOnScrollChanged(object sender, ScrollChangedEventArgs routedEventArgs)
		{
			this.SetItemsPresenterPagedSize();
			this.ComputePagesData();
			int num = ((this.currentPage != null) ? this.currentPage.FirstIndexInPage : 0);
			this.ScrollToPage(this.FindPageForitem(num));
			this.SetItemsVisibility(base.Items);
			this.ValidateCanFlipProperties();
			bool flag = this.IsInVisualState("FlipNext") || this.IsInVisualState("FlipPrevious");
			if (flag)
			{
				this.GoToState("ShowingPage", true);
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001D1B4 File Offset: 0x0001B3B4
		private void ValidateCanFlipProperties()
		{
			bool flag = this.pages == null || this.pages.Count == 0 || this.currentPage == null;
			if (flag)
			{
				this.CanFlipPrevious = (this.CanFlipNext = false);
			}
			else
			{
				this.CanFlipPrevious = this.currentPage.PageIndex > 0;
				this.CanFlipNext = this.pages.Any((FlipListView.FlipPage p) => p.PageIndex > this.currentPage.PageIndex);
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001D22F File Offset: 0x0001B42F
		private void OnApplyItemsPresenterTemplate()
		{
			this.itemsPresenterPart = base.GetTemplateChild("PART_ItemsPresenter") as ItemsPresenter;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001D248 File Offset: 0x0001B448
		private void SetItemsPresenterPagedSize()
		{
			bool flag = this.itemsScrollViewer == null || this.itemsPresenterPart == null;
			if (!flag)
			{
				Size size = new Size(this.itemsScrollViewer.ViewportWidth, this.itemsScrollViewer.ViewportHeight);
				bool flag2 = this.ItemsOrientantion == Orientation.Vertical;
				if (flag2)
				{
					double num = this.itemsPresenterPart.ActualWidth % size.Width;
					bool flag3 = num > 0.0;
					if (flag3)
					{
						this.itemsPresenterPart.Width = this.itemsPresenterPart.ActualWidth + (size.Width - num);
					}
				}
				else
				{
					double num2 = this.itemsPresenterPart.ActualHeight % size.Height;
					bool flag4 = num2 > 0.0;
					if (flag4)
					{
						this.itemsPresenterPart.Height = this.itemsPresenterPart.ActualHeight + (size.Height - num2);
					}
				}
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001D33C File Offset: 0x0001B53C
		private void ComputePagesData()
		{
			bool flag = base.Items == null || base.Items.IsEmpty;
			if (flag)
			{
				this.pages = new List<FlipListView.FlipPage>();
				this.currentPage = null;
			}
			else
			{
				List<FlipListView.FlipPage> list = new List<FlipListView.FlipPage>();
				ListViewItem listViewItem = base.ItemContainerGenerator.ContainerFromIndex(0) as ListViewItem;
				bool flag2 = listViewItem != null && this.itemsScrollViewer != null && this.itemsPresenterPart != null;
				if (flag2)
				{
					bool flag3 = this.itemsScrollViewer.ViewportHeight > 0.0 && this.itemsScrollViewer.ViewportWidth > 0.0;
					if (flag3)
					{
						Size renderSize = listViewItem.RenderSize;
						bool flag4 = renderSize.Width <= 0.0 || renderSize.Height <= 0.0;
						if (flag4)
						{
							renderSize = this.lastKnownItemSize;
						}
						else
						{
							this.lastKnownItemSize = renderSize;
						}
						int num = (int)(this.itemsScrollViewer.ViewportHeight / renderSize.Height);
						int num2 = (int)(this.itemsScrollViewer.ViewportWidth / renderSize.Width);
						int num3 = num2 * num;
						bool flag5 = num3 > 0;
						if (flag5)
						{
							int count = base.Items.Count;
							int num4 = 0;
							for (int i = 0; i < count; i += num3)
							{
								list.Add(new FlipListView.FlipPage
								{
									PageIndex = num4,
									FirstIndexInPage = i,
									LastIndexInPage = i + num3 - 1,
									PageSize = new Size(this.itemsScrollViewer.ViewportWidth, this.itemsScrollViewer.ViewportHeight)
								});
								num4++;
							}
						}
					}
				}
				this.pages = list;
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001D504 File Offset: 0x0001B704
		private FlipListView.FlipPage FindPageForitem(int itemIndex)
		{
			bool flag = this.pages == null || this.pages.Count == 0;
			if (flag)
			{
				this.ComputePagesData();
			}
			return this.pages.FirstOrDefault((FlipListView.FlipPage p) => p.FirstIndexInPage <= itemIndex && p.LastIndexInPage >= itemIndex);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001D564 File Offset: 0x0001B764
		private bool ScrollToPage(FlipListView.FlipPage page)
		{
			bool flag = page == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.currentPage != null;
				if (flag3)
				{
					this.currentPage.IsVisible = false;
				}
				page.IsVisible = true;
				this.ScrollToItem(page.FirstIndexInPage);
				this.currentPage = page;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001D5BC File Offset: 0x0001B7BC
		private void SetItemsVisibility(ItemCollection items)
		{
			int count = items.Count;
			for (int i = 0; i < count; i++)
			{
				object obj = items[i];
				DependencyObject dependencyObject = base.ItemContainerGenerator.ContainerFromItem(obj);
				ListViewItem listViewItem = dependencyObject as ListViewItem;
				bool flag = listViewItem != null;
				if (flag)
				{
					bool flag2 = !FlipListView.IsFullyVisible(listViewItem, this.itemsScrollViewer);
					if (flag2)
					{
						listViewItem.Visibility = Visibility.Hidden;
					}
					else
					{
						listViewItem.Visibility = Visibility.Visible;
					}
					TileAutomationPeer tileAutomationPeer = new TileAutomationPeer(listViewItem);
					object obj2 = typeof(UIElement).InvokeMember("AutomationPeerField", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField, null, null, null);
					obj2.GetType().InvokeMember("SetValue", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, obj2, new object[] { listViewItem, tileAutomationPeer });
				}
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001D698 File Offset: 0x0001B898
		private void OnApplySwitchPrevButtonTemplate()
		{
			bool flag = this.switchPrevButtonPart != null;
			if (flag)
			{
				this.switchPrevButtonPart.Click -= this.SwitchPrevButtonPartOnClick;
				this.switchPrevButtonPart.GotFocus -= this.SwitchButtonPartOnGotFocus;
				this.switchPrevButtonPart.LostFocus -= this.SwitchButtonPartOnLostFocus;
			}
			this.switchPrevButtonPart = base.GetTemplateChild("PART_SwitchPrevButton") as Button;
			bool flag2 = this.switchPrevButtonPart != null;
			if (flag2)
			{
				this.switchPrevButtonPart.Click += this.SwitchPrevButtonPartOnClick;
				this.switchPrevButtonPart.GotFocus += this.SwitchButtonPartOnGotFocus;
				this.switchPrevButtonPart.LostFocus += this.SwitchButtonPartOnLostFocus;
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001D76C File Offset: 0x0001B96C
		private void OnApplySwitchNextButtonTemplate()
		{
			bool flag = this.switchNextButtonPart != null;
			if (flag)
			{
				this.switchNextButtonPart.Click -= this.SwitchNextButtonPartOnClick;
				this.switchNextButtonPart.GotFocus -= this.SwitchButtonPartOnGotFocus;
				this.switchNextButtonPart.LostFocus -= this.SwitchButtonPartOnLostFocus;
			}
			this.switchNextButtonPart = base.GetTemplateChild("PART_SwitchNextButton") as Button;
			bool flag2 = this.switchNextButtonPart != null;
			if (flag2)
			{
				this.switchNextButtonPart.Click += this.SwitchNextButtonPartOnClick;
				this.switchNextButtonPart.GotFocus += this.SwitchButtonPartOnGotFocus;
				this.switchNextButtonPart.LostFocus += this.SwitchButtonPartOnLostFocus;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001D83E File Offset: 0x0001BA3E
		private void SwitchButtonPartOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
		{
			this.buttonFocused = null;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001D848 File Offset: 0x0001BA48
		private void SwitchButtonPartOnGotFocus(object sender, RoutedEventArgs routedEventArgs)
		{
			this.buttonFocused = sender as Button;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001D858 File Offset: 0x0001BA58
		private void SwitchNextButtonPartOnClick(object sender, RoutedEventArgs e)
		{
			bool flag = this.itemsScrollViewer == null || !this.CanFlipNext;
			if (!flag)
			{
				bool flag2 = !this.ScrollToNextPage();
				if (flag2)
				{
					bool flag3 = this.ItemsOrientantion == Orientation.Vertical;
					if (flag3)
					{
						this.itemsScrollViewer.PageRight();
					}
					else
					{
						this.itemsScrollViewer.PageDown();
					}
				}
				this.GoToState("FlipNext", true);
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001D8C8 File Offset: 0x0001BAC8
		private void SwitchPrevButtonPartOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			bool flag = this.itemsScrollViewer == null || !this.CanFlipPrevious;
			if (!flag)
			{
				bool flag2 = !this.ScrollToPreviousPage();
				if (flag2)
				{
					bool flag3 = this.ItemsOrientantion == Orientation.Vertical;
					if (flag3)
					{
						this.itemsScrollViewer.PageLeft();
					}
					else
					{
						this.itemsScrollViewer.PageUp();
					}
				}
				this.GoToState("FlipPrevious", true);
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001D938 File Offset: 0x0001BB38
		private bool ScrollToPreviousPage()
		{
			bool flag = this.currentPage != null && this.currentPage.PageIndex > 0;
			bool flag4;
			if (flag)
			{
				bool flag2 = this.ScrollToPage(this.pages.FirstOrDefault((FlipListView.FlipPage p) => p.PageIndex == this.currentPage.PageIndex - 1));
				bool flag3 = this.switchNextButtonPart != null;
				if (flag3)
				{
					this.switchNextButtonPart.Focus();
					Keyboard.Focus(this.switchNextButtonPart);
				}
				flag4 = flag2;
			}
			else
			{
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001D9B4 File Offset: 0x0001BBB4
		private bool ScrollToNextPage()
		{
			bool flag = this.currentPage != null;
			bool flag4;
			if (flag)
			{
				bool flag2 = this.ScrollToPage(this.pages.FirstOrDefault((FlipListView.FlipPage p) => p.PageIndex == this.currentPage.PageIndex + 1));
				bool flag3 = this.switchPrevButtonPart != null;
				if (flag3)
				{
					this.switchPrevButtonPart.Focus();
					Keyboard.Focus(this.switchPrevButtonPart);
				}
				flag4 = flag2;
			}
			else
			{
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001DA20 File Offset: 0x0001BC20
		private bool ScrollToFirstPage()
		{
			bool flag = this.pages == null || this.pages.Count == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.ScrollToPage(this.pages.FirstOrDefault<FlipListView.FlipPage>());
				bool flag4 = this.switchNextButtonPart != null;
				if (flag4)
				{
					this.switchNextButtonPart.Focus();
					Keyboard.Focus(this.switchNextButtonPart);
				}
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001DA90 File Offset: 0x0001BC90
		private bool ScrollToLastPage()
		{
			bool flag = this.pages == null || this.pages.Count == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.ScrollToPage(this.pages.LastOrDefault<FlipListView.FlipPage>());
				bool flag4 = this.switchNextButtonPart != null;
				if (flag4)
				{
					this.switchNextButtonPart.Focus();
					Keyboard.Focus(this.switchNextButtonPart);
				}
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001DB00 File Offset: 0x0001BD00
		private bool ScrollToItem(int index)
		{
			ListViewItem listViewItem = base.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
			bool flag = this.itemsScrollViewer == null || listViewItem == null || this.itemsPresenterPart == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Point point = listViewItem.TranslatePoint(default(Point), this.itemsPresenterPart);
				bool flag3 = this.ItemsOrientantion == Orientation.Vertical;
				if (flag3)
				{
					this.itemsScrollViewer.ScrollToHorizontalOffset(point.X);
				}
				else
				{
					this.itemsScrollViewer.ScrollToVerticalOffset(point.Y);
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001DB97 File Offset: 0x0001BD97
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.GoToState("ShowingPage", true);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001DBA7 File Offset: 0x0001BDA7
		private void OnLayoutUpdated(object sender, EventArgs eventArgs)
		{
			this.SetItemsVisibility(base.Items);
			this.GoToState("ShowingPage", true);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
		private bool GoToState(string stateName, bool useTransitions)
		{
			bool flag = this.border != null && VisualStateManager.GoToElementState(this.border, stateName, useTransitions);
			bool flag2;
			if (flag)
			{
				this.currentFlipState = stateName;
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001DC00 File Offset: 0x0001BE00
		private bool IsInVisualState(string stateName)
		{
			return string.Equals(stateName, this.currentFlipState, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x040002BD RID: 701
		private const string FlipVisualStateGroupName = "FlipStates";

		// Token: 0x040002BE RID: 702
		private const string FlipNextVisualStateName = "FlipNext";

		// Token: 0x040002BF RID: 703
		private const string FlipPreviousVisualStateName = "FlipPrevious";

		// Token: 0x040002C0 RID: 704
		private const string ShowingPageVisualStateName = "ShowingPage";

		// Token: 0x040002C1 RID: 705
		private const string SwitchNextButtonPartName = "PART_SwitchNextButton";

		// Token: 0x040002C2 RID: 706
		private const string SwitchPrevButtonPartName = "PART_SwitchPrevButton";

		// Token: 0x040002C3 RID: 707
		private const string ItemsPresenterPartName = "PART_ItemsPresenter";

		// Token: 0x040002C4 RID: 708
		private const string ItemsScrollViewerPartName = "PART_ItemsScrollViewer";

		// Token: 0x040002C5 RID: 709
		public static readonly DependencyProperty SwitchNextButtonStyleProperty = DependencyProperty.Register("SwitchNextButtonStyle", typeof(Style), typeof(FlipListView), new PropertyMetadata(null));

		// Token: 0x040002C6 RID: 710
		public static readonly DependencyProperty SwitchPreviousButtonStyleProperty = DependencyProperty.Register("SwitchPreviousButtonStyle", typeof(Style), typeof(FlipListView), new PropertyMetadata(null));

		// Token: 0x040002C7 RID: 711
		public static readonly DependencyProperty CanFlipNextProperty = DependencyProperty.Register("CanFlipNext", typeof(bool), typeof(FlipListView), new PropertyMetadata(false));

		// Token: 0x040002C8 RID: 712
		public static readonly DependencyProperty CanFlipPreviousProperty = DependencyProperty.Register("CanFlipPrevious", typeof(bool), typeof(FlipListView), new PropertyMetadata(false));

		// Token: 0x040002C9 RID: 713
		public static readonly DependencyProperty ItemsOrientantionProperty = DependencyProperty.Register("ItemsOrientantion", typeof(Orientation), typeof(FlipListView), new PropertyMetadata(Orientation.Horizontal));

		// Token: 0x040002CA RID: 714
		private Button switchPrevButtonPart;

		// Token: 0x040002CB RID: 715
		private Button switchNextButtonPart;

		// Token: 0x040002CC RID: 716
		private ItemsPresenter itemsPresenterPart;

		// Token: 0x040002CD RID: 717
		private ScrollViewer itemsScrollViewer;

		// Token: 0x040002CE RID: 718
		private List<FlipListView.FlipPage> pages;

		// Token: 0x040002CF RID: 719
		private FlipListView.FlipPage currentPage;

		// Token: 0x040002D0 RID: 720
		private string currentFlipState;

		// Token: 0x040002D1 RID: 721
		private Border border;

		// Token: 0x040002D2 RID: 722
		private Size lastKnownItemSize;

		// Token: 0x040002D3 RID: 723
		private Button buttonFocused;

		// Token: 0x0200015E RID: 350
		private class FlipPage
		{
			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x06000882 RID: 2178 RVA: 0x00025964 File Offset: 0x00023B64
			// (set) Token: 0x06000883 RID: 2179 RVA: 0x0002596C File Offset: 0x00023B6C
			public int PageIndex { get; set; }

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x06000884 RID: 2180 RVA: 0x00025975 File Offset: 0x00023B75
			// (set) Token: 0x06000885 RID: 2181 RVA: 0x0002597D File Offset: 0x00023B7D
			public int FirstIndexInPage { get; set; }

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x06000886 RID: 2182 RVA: 0x00025986 File Offset: 0x00023B86
			// (set) Token: 0x06000887 RID: 2183 RVA: 0x0002598E File Offset: 0x00023B8E
			public int LastIndexInPage { get; set; }

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06000888 RID: 2184 RVA: 0x00025997 File Offset: 0x00023B97
			// (set) Token: 0x06000889 RID: 2185 RVA: 0x0002599F File Offset: 0x00023B9F
			public Size PageSize { get; set; }

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x0600088A RID: 2186 RVA: 0x000259A8 File Offset: 0x00023BA8
			// (set) Token: 0x0600088B RID: 2187 RVA: 0x000259B0 File Offset: 0x00023BB0
			public bool IsVisible { get; set; }
		}

		// Token: 0x0200015F RID: 351
		public class FlipListViewAutomationPeer : FrameworkElementAutomationPeer
		{
			// Token: 0x0600088D RID: 2189 RVA: 0x000259B9 File Offset: 0x00023BB9
			public FlipListViewAutomationPeer(FlipListView owner)
				: base(owner)
			{
			}

			// Token: 0x0600088E RID: 2190 RVA: 0x000259C4 File Offset: 0x00023BC4
			protected override string GetAutomationIdCore()
			{
				return string.Empty;
			}

			// Token: 0x0600088F RID: 2191 RVA: 0x000259DC File Offset: 0x00023BDC
			protected override string GetNameCore()
			{
				return string.Empty;
			}
		}
	}
}
