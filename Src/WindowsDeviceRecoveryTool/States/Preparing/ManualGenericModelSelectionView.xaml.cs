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
	// Token: 0x02000047 RID: 71
	[Export]
	[Region(new string[] { "MainArea" })]
	public partial class ManualGenericModelSelectionView : Grid
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000FCB9 File Offset: 0x0000DEB9
		public ManualGenericModelSelectionView()
		{
			this.InitializeComponent();
			this.selectedItem = null;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000FCD4 File Offset: 0x0000DED4
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
					ManualGenericModelSelectionViewModel manualGenericModelSelectionViewModel = (ManualGenericModelSelectionViewModel)base.DataContext;
					bool flag3 = manualGenericModelSelectionViewModel.SelectTileCommand.CanExecute(null);
					if (flag3)
					{
						manualGenericModelSelectionViewModel.SelectTileCommand.Execute(this.selectedItem.DataContext);
					}
				}
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000FD64 File Offset: 0x0000DF64
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

		// Token: 0x060002CD RID: 717 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		private void ManufacturersListBoxOnFocusedItemKeyPressed(object sender, KeyEventArgs e)
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
						ManualGenericModelSelectionViewModel manualGenericModelSelectionViewModel = (ManualGenericModelSelectionViewModel)base.DataContext;
						bool flag4 = manualGenericModelSelectionViewModel.SelectTileCommand.CanExecute(null);
						if (flag4)
						{
							manualGenericModelSelectionViewModel.SelectTileCommand.Execute(frameworkElement.DataContext);
						}
					}
				}
			}
		}

		// Token: 0x04000143 RID: 323
		private FrameworkElement selectedItem;
	}
}
