using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000050 RID: 80
	public sealed class RemoveItemInListBoxAction : TriggerAction<FrameworkElement>
	{
		// Token: 0x060002DD RID: 733 RVA: 0x0000C348 File Offset: 0x0000A548
		protected override void Invoke(object parameter)
		{
			ItemsControl itemsControl = this.ItemsControl;
			if (itemsControl != null)
			{
				if (itemsControl.ItemsSource != null)
				{
					IList list = itemsControl.ItemsSource as IList;
					if (list != null && !list.IsReadOnly && list.Contains(base.AssociatedObject.DataContext))
					{
						list.Remove(base.AssociatedObject.DataContext);
						return;
					}
				}
				else
				{
					ListBox listBox = this.ItemsControl as ListBox;
					if (listBox != null)
					{
						ListBoxItem itemContainer = this.ItemContainer;
						if (itemContainer != null)
						{
							listBox.Items.Remove(itemContainer.Content);
						}
					}
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000C3CD File Offset: 0x0000A5CD
		private ListBoxItem ItemContainer
		{
			get
			{
				return (ListBoxItem)base.AssociatedObject.GetSelfAndAncestors().FirstOrDefault((DependencyObject element) => element is ListBoxItem);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000C403 File Offset: 0x0000A603
		private ItemsControl ItemsControl
		{
			get
			{
				return (ItemsControl)base.AssociatedObject.GetSelfAndAncestors().FirstOrDefault((DependencyObject element) => element is ItemsControl);
			}
		}
	}
}
