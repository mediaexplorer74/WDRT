using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Behaviors
{
	// Token: 0x02000014 RID: 20
	public static class ScrollBehavior
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002CCC File Offset: 0x00000ECC
		public static bool GetResetScrollOnItemsChanged(DependencyObject obj)
		{
			return (bool)obj.GetValue(ScrollBehavior.ResetScrollOnItemsChangedProperty);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CEE File Offset: 0x00000EEE
		public static void SetResetScrollOnItemsChanged(DependencyObject obj, bool value)
		{
			obj.SetValue(ScrollBehavior.ResetScrollOnItemsChangedProperty, value);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D03 File Offset: 0x00000F03
		public static void SetMouseWheelScrollValue(DependencyObject element, int value)
		{
			element.SetValue(ScrollBehavior.MouseWheelScrollValueProperty, value);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D18 File Offset: 0x00000F18
		public static double GetMouseWheelScrollValue(DependencyObject element)
		{
			return (double)element.GetValue(ScrollBehavior.MouseWheelScrollValueProperty);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D3A File Offset: 0x00000F3A
		public static void SetScrollWithChildren(DependencyObject element, bool value)
		{
			element.SetValue(ScrollBehavior.ScrollWithChildrenProperty, value);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D50 File Offset: 0x00000F50
		public static bool GetScrollWithChildren(DependencyObject element)
		{
			return (bool)element.GetValue(ScrollBehavior.ScrollWithChildrenProperty);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D72 File Offset: 0x00000F72
		public static void SetHorizontalScroll(ScrollViewer scrollViewer, bool value)
		{
			scrollViewer.SetValue(ScrollBehavior.HorizontalScrollProperty, value);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D88 File Offset: 0x00000F88
		public static bool GetHorizontalScroll(ScrollViewer scrollViewer)
		{
			return (bool)scrollViewer.GetValue(ScrollBehavior.HorizontalScrollProperty);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DAC File Offset: 0x00000FAC
		private static void HorizontalScrollOnPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			ScrollViewer scrollViewer = dependencyObject as ScrollViewer;
			bool flag = scrollViewer == null;
			if (!flag)
			{
				bool flag2 = (bool)eventArgs.NewValue;
				if (flag2)
				{
					scrollViewer.PreviewMouseWheel += ScrollBehavior.HorizontalScrollScrollViewerOnPreviewMouseWheel;
				}
				else
				{
					scrollViewer.PreviewMouseWheel -= ScrollBehavior.HorizontalScrollScrollViewerOnPreviewMouseWheel;
				}
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E08 File Offset: 0x00001008
		private static void HorizontalScrollScrollViewerOnPreviewMouseWheel(object sender, MouseWheelEventArgs eventArgs)
		{
			ScrollViewer scrollViewer = (ScrollViewer)sender;
			bool flag = scrollViewer.VerticalScrollBarVisibility > ScrollBarVisibility.Disabled;
			if (!flag)
			{
				bool flag2 = scrollViewer.HorizontalScrollBarVisibility != ScrollBarVisibility.Auto && scrollViewer.HorizontalScrollBarVisibility != ScrollBarVisibility.Visible;
				if (!flag2)
				{
					int num = eventArgs.Delta;
					bool flag3 = scrollViewer.GetValue(ScrollBehavior.MouseWheelScrollValueProperty) != DependencyProperty.UnsetValue;
					if (flag3)
					{
						int num2 = Math.Sign(num);
						num = num2 * (int)scrollViewer.GetValue(ScrollBehavior.MouseWheelScrollValueProperty);
						bool flag4 = num == 0;
						if (flag4)
						{
							num = eventArgs.Delta;
						}
					}
					scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - (double)num);
					eventArgs.Handled = true;
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EBC File Offset: 0x000010BC
		private static void ScrollWithChildrenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			ScrollViewer scrollViewer = dependencyObject as ScrollViewer;
			bool flag = scrollViewer == null;
			if (!flag)
			{
				bool flag2 = (bool)eventArgs.NewValue;
				if (flag2)
				{
					scrollViewer.PreviewMouseWheel += ScrollBehavior.ScrollWithChildrenScrollViewerOnPreviewMouseWheel;
				}
				else
				{
					scrollViewer.PreviewMouseWheel -= ScrollBehavior.ScrollWithChildrenScrollViewerOnPreviewMouseWheel;
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F18 File Offset: 0x00001118
		private static void ScrollWithChildrenScrollViewerOnPreviewMouseWheel(object sender, MouseWheelEventArgs eventArgs)
		{
			ScrollViewer scrollViewer = (ScrollViewer)sender;
			bool flag = scrollViewer.HorizontalScrollBarVisibility > ScrollBarVisibility.Disabled;
			if (flag)
			{
				scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - (double)eventArgs.Delta);
			}
			else
			{
				scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - (double)eventArgs.Delta);
			}
			eventArgs.Handled = true;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F74 File Offset: 0x00001174
		private static void OnResetScrollOnItemsChangedPropertyChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
		{
			ItemsControl itemsControl = dpo as ItemsControl;
			bool flag = itemsControl != null;
			if (flag)
			{
				DependencyPropertyDescriptor dependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(ItemsControl));
				bool flag2 = dependencyPropertyDescriptor != null;
				if (flag2)
				{
					bool flag3 = (bool)e.NewValue;
					if (flag3)
					{
						dependencyPropertyDescriptor.AddValueChanged(itemsControl, new EventHandler(ScrollBehavior.ItemsSourceChanged));
					}
					else
					{
						dependencyPropertyDescriptor.RemoveValueChanged(itemsControl, new EventHandler(ScrollBehavior.ItemsSourceChanged));
					}
				}
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002FF4 File Offset: 0x000011F4
		private static void ItemsSourceChanged(object sender, EventArgs e)
		{
			ItemsControl itemsControl = sender as ItemsControl;
			bool flag = itemsControl == null;
			if (!flag)
			{
				bool flag2 = itemsControl.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated;
				if (flag2)
				{
					ScrollViewer visualChild = ScrollBehavior.GetVisualChild<ScrollViewer>(itemsControl);
					visualChild.ScrollToHome();
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003038 File Offset: 0x00001238
		private static T GetVisualChild<T>(DependencyObject parent) where T : Visual
		{
			T t = default(T);
			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				Visual visual = (Visual)VisualTreeHelper.GetChild(parent, i);
				t = visual as T;
				bool flag = t == null;
				if (flag)
				{
					t = ScrollBehavior.GetVisualChild<T>(visual);
				}
				bool flag2 = t != null;
				if (flag2)
				{
					break;
				}
			}
			return t;
		}

		// Token: 0x04000010 RID: 16
		public static readonly DependencyProperty HorizontalScrollProperty = DependencyProperty.RegisterAttached("HorizontalScroll", typeof(bool), typeof(ScrollBehavior), new PropertyMetadata(false, new PropertyChangedCallback(ScrollBehavior.HorizontalScrollOnPropertyChanged)));

		// Token: 0x04000011 RID: 17
		public static readonly DependencyProperty ScrollWithChildrenProperty = DependencyProperty.RegisterAttached("ScrollWithChildren", typeof(bool), typeof(ScrollBehavior), new PropertyMetadata(false, new PropertyChangedCallback(ScrollBehavior.ScrollWithChildrenPropertyChangedCallback)));

		// Token: 0x04000012 RID: 18
		public static readonly DependencyProperty MouseWheelScrollValueProperty = DependencyProperty.RegisterAttached("MouseWheelScrollValue", typeof(int), typeof(ScrollBehavior), new PropertyMetadata(0));

		// Token: 0x04000013 RID: 19
		public static readonly DependencyProperty ResetScrollOnItemsChangedProperty = DependencyProperty.RegisterAttached("ResetScrollOnItemsChanged", typeof(bool), typeof(ScrollBehavior), new UIPropertyMetadata(false, new PropertyChangedCallback(ScrollBehavior.OnResetScrollOnItemsChangedPropertyChanged)));
	}
}
