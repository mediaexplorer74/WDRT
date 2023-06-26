using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.WindowsDeviceRecoveryTool.Framework;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x02000054 RID: 84
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class DeviceSelectionView : Grid
	{
		// Token: 0x06000353 RID: 851 RVA: 0x00012B94 File Offset: 0x00010D94
		public DeviceSelectionView()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00012BA8 File Offset: 0x00010DA8
		private void DevicesListBoxOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ListBox listBox = sender as ListBox;
			bool flag = listBox != null;
			if (flag)
			{
				FrameworkElement frameworkElement = listBox.ItemContainerGenerator.ContainerFromItem(listBox.SelectedItem) as FrameworkElement;
				bool flag2 = frameworkElement != null && this.selectedItem != null && object.Equals(frameworkElement, this.selectedItem);
				if (flag2)
				{
					DeviceSelectionViewModel deviceSelectionViewModel = (DeviceSelectionViewModel)base.DataContext;
					bool flag3 = deviceSelectionViewModel.SelectTileCommand.CanExecute(this.selectedItem.DataContext);
					if (flag3)
					{
						deviceSelectionViewModel.SelectTileCommand.Execute(this.selectedItem.DataContext);
					}
				}
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00012C44 File Offset: 0x00010E44
		private void DevicesListBoxOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
		{
			ListBox listBox = sender as ListBox;
			bool flag = args != null && listBox != null;
			if (flag)
			{
				DependencyObject dependencyObject = args.OriginalSource as DependencyObject;
				bool flag2 = dependencyObject != null;
				if (flag2)
				{
					ListBoxItem listBoxItem = ItemsControl.ContainerFromElement(listBox, dependencyObject) as ListBoxItem;
					bool flag3 = listBoxItem != null;
					if (flag3)
					{
						this.selectedItem = listBoxItem;
					}
				}
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00012CA4 File Offset: 0x00010EA4
		private void DevicesListBoxOnFocusedItemKeyPressed(object sender, KeyEventArgs e)
		{
			bool flag = e.Key != Key.Space && e.Key != Key.Return;
			if (!flag)
			{
				ListBoxItem listBoxItem = e.OriginalSource as ListBoxItem;
				bool flag2 = listBoxItem == null;
				if (!flag2)
				{
					DeviceSelectionViewModel deviceSelectionViewModel = (DeviceSelectionViewModel)base.DataContext;
					ICommand selectTileCommand = deviceSelectionViewModel.SelectTileCommand;
					bool flag3 = selectTileCommand.CanExecute(listBoxItem.DataContext);
					if (flag3)
					{
						selectTileCommand.Execute(listBoxItem.DataContext);
					}
				}
			}
		}

		// Token: 0x04000179 RID: 377
		private FrameworkElement selectedItem;
	}
}
