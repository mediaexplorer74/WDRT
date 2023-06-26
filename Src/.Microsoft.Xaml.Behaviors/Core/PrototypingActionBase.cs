using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000049 RID: 73
	[DefaultTrigger(typeof(ButtonBase), typeof(EventTrigger), "Click")]
	[DefaultTrigger(typeof(TextBox), typeof(EventTrigger), "TextChanged")]
	[DefaultTrigger(typeof(RichTextBox), typeof(EventTrigger), "TextChanged")]
	[DefaultTrigger(typeof(ListBoxItem), typeof(EventTrigger), "Selected")]
	[DefaultTrigger(typeof(TreeViewItem), typeof(EventTrigger), "Selected")]
	[DefaultTrigger(typeof(Selector), typeof(EventTrigger), "SelectionChanged")]
	[DefaultTrigger(typeof(TreeView), typeof(EventTrigger), "SelectedItemChanged")]
	[DefaultTrigger(typeof(RangeBase), typeof(EventTrigger), "ValueChanged")]
	public abstract class PrototypingActionBase : TriggerAction<DependencyObject>
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000BDD9 File Offset: 0x00009FD9
		internal void TestInvoke(object parameter)
		{
			this.Invoke(parameter);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		protected UserControl GetContainingScreen()
		{
			IEnumerable<UserControl> enumerable = base.AssociatedObject.GetSelfAndAncestors().OfType<UserControl>();
			return enumerable.FirstOrDefault((UserControl userControl) => InteractionContext.IsScreen(userControl.GetType().FullName)) ?? enumerable.First<UserControl>();
		}
	}
}
