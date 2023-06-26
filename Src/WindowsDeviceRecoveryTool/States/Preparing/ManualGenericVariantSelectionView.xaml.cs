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
	// Token: 0x0200004B RID: 75
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class ManualGenericVariantSelectionView : Grid
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x00010A54 File Offset: 0x0000EC54
		public ManualGenericVariantSelectionView()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00010A68 File Offset: 0x0000EC68
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
					ManualGenericVariantSelectionViewModel manualGenericVariantSelectionViewModel = (ManualGenericVariantSelectionViewModel)base.DataContext;
					bool flag3 = manualGenericVariantSelectionViewModel.SelectTileCommand.CanExecute(null);
					if (flag3)
					{
						manualGenericVariantSelectionViewModel.SelectTileCommand.Execute(this.selectedItem.DataContext);
					}
				}
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00010AF8 File Offset: 0x0000ECF8
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

		// Token: 0x060002F7 RID: 759 RVA: 0x00010B58 File Offset: 0x0000ED58
		private void DevicesListBoxOnFocusedItemKeyPressed(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Space || e.Key == Key.Return;
			if (flag)
			{
				ListBox listBox = sender as ListBox;
				bool flag2 = listBox != null;
				if (flag2)
				{
					FrameworkElement frameworkElement = listBox.ItemContainerGenerator.ContainerFromItem(listBox.SelectedItem) as FrameworkElement;
					DependencyObject dependencyObject = e.OriginalSource as DependencyObject;
					bool flag3 = frameworkElement != null && dependencyObject != null && object.Equals(frameworkElement, dependencyObject);
					if (flag3)
					{
						ManualGenericVariantSelectionViewModel manualGenericVariantSelectionViewModel = (ManualGenericVariantSelectionViewModel)base.DataContext;
						bool flag4 = manualGenericVariantSelectionViewModel.SelectTileCommand.CanExecute(null);
						if (flag4)
						{
							manualGenericVariantSelectionViewModel.SelectTileCommand.Execute(frameworkElement.DataContext);
						}
					}
				}
			}
		}

		// Token: 0x04000154 RID: 340
		private FrameworkElement selectedItem;
	}
}
