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
	// Token: 0x02000058 RID: 88
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class ManualManufacturerSelectionView : Grid
	{
		// Token: 0x0600036E RID: 878 RVA: 0x00013272 File Offset: 0x00011472
		public ManualManufacturerSelectionView()
		{
			this.InitializeComponent();
			this.selectedItem = null;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001328C File Offset: 0x0001148C
		private void ManufacturersListBoxOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ListBox listBox = sender as ListBox;
			bool flag = listBox != null;
			if (flag)
			{
				FrameworkElement frameworkElement = listBox.ItemContainerGenerator.ContainerFromItem(listBox.SelectedItem) as FrameworkElement;
				bool flag2 = frameworkElement != null && this.selectedItem != null && object.Equals(frameworkElement, this.selectedItem);
				if (flag2)
				{
					ManualManufacturerSelectionViewModel manualManufacturerSelectionViewModel = (ManualManufacturerSelectionViewModel)base.DataContext;
					bool flag3 = manualManufacturerSelectionViewModel.SelectTileCommand.CanExecute(null);
					if (flag3)
					{
						manualManufacturerSelectionViewModel.SelectTileCommand.Execute(this.selectedItem.DataContext);
					}
				}
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001331C File Offset: 0x0001151C
		private void ManufacturersListBoxOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
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

		// Token: 0x06000371 RID: 881 RVA: 0x0001337C File Offset: 0x0001157C
		private void ManufacturersListBoxOnFocusedItemKeyPressed(object sender, KeyEventArgs e)
		{
			bool flag = e.Key != Key.Space && e.Key != Key.Return;
			if (!flag)
			{
				ListBoxItem listBoxItem = e.OriginalSource as ListBoxItem;
				bool flag2 = listBoxItem == null;
				if (!flag2)
				{
					ManualManufacturerSelectionViewModel manualManufacturerSelectionViewModel = (ManualManufacturerSelectionViewModel)base.DataContext;
					bool flag3 = manualManufacturerSelectionViewModel.SelectTileCommand.CanExecute(null);
					if (flag3)
					{
						manualManufacturerSelectionViewModel.SelectTileCommand.Execute(listBoxItem.DataContext);
					}
				}
			}
		}

		// Token: 0x04000181 RID: 385
		private FrameworkElement selectedItem;
	}
}
