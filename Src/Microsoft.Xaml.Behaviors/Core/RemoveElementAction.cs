using System;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000051 RID: 81
	public class RemoveElementAction : TargetedTriggerAction<FrameworkElement>
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x0000C444 File Offset: 0x0000A644
		protected override void Invoke(object parameter)
		{
			if (base.AssociatedObject != null && base.Target != null)
			{
				DependencyObject parent = base.Target.Parent;
				Panel panel = parent as Panel;
				if (panel != null)
				{
					panel.Children.Remove(base.Target);
					return;
				}
				ContentControl contentControl = parent as ContentControl;
				if (contentControl != null)
				{
					if (contentControl.Content == base.Target)
					{
						contentControl.Content = null;
					}
					return;
				}
				ItemsControl itemsControl = parent as ItemsControl;
				if (itemsControl != null)
				{
					itemsControl.Items.Remove(base.Target);
					return;
				}
				Page page = parent as Page;
				if (page != null)
				{
					if (page.Content == base.Target)
					{
						page.Content = null;
					}
					return;
				}
				Decorator decorator = parent as Decorator;
				if (decorator != null)
				{
					if (decorator.Child == base.Target)
					{
						decorator.Child = null;
					}
					return;
				}
				if (parent != null)
				{
					throw new InvalidOperationException(ExceptionStringTable.UnsupportedRemoveTargetExceptionMessage);
				}
			}
		}
	}
}
