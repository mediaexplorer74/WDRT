using System;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Behaviors
{
	// Token: 0x02000012 RID: 18
	public sealed class ListBoxBehavior
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002B9C File Offset: 0x00000D9C
		public static bool GetAllowUnselect(ListBox listBox)
		{
			return (bool)listBox.GetValue(ListBoxBehavior.AllowUnselectProperty);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BBE File Offset: 0x00000DBE
		public static void SetAllowUnselect(ListBox listBox, bool value)
		{
			listBox.SetValue(ListBoxBehavior.AllowUnselectProperty, value);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BD4 File Offset: 0x00000DD4
		private static void AllowUnselect_OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			ListBox listBox = obj as ListBox;
			bool flag = listBox == null;
			if (!flag)
			{
				bool flag2 = (bool)args.NewValue;
				bool flag3 = !flag2;
				if (flag3)
				{
					listBox.SelectionChanged += ListBoxBehavior.ListBox_OnSelectionChanged;
				}
				else
				{
					listBox.SelectionChanged -= ListBoxBehavior.ListBox_OnSelectionChanged;
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C38 File Offset: 0x00000E38
		private static void ListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox = (ListBox)sender;
			bool flag = listBox.SelectionMode != SelectionMode.Single || e.RemovedItems.Count < 1 || e.AddedItems.Count > 0;
			if (!flag)
			{
				listBox.SelectedItem = e.RemovedItems[0];
			}
		}

		// Token: 0x0400000C RID: 12
		public static readonly DependencyProperty AllowUnselectProperty = DependencyProperty.RegisterAttached("AllowUnselect", typeof(bool), typeof(ListBoxBehavior), new FrameworkPropertyMetadata(true, new PropertyChangedCallback(ListBoxBehavior.AllowUnselect_OnPropertyChanged)));
	}
}
