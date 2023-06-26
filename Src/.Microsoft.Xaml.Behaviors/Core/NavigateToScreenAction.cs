using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x0200004B RID: 75
	public sealed class NavigateToScreenAction : PrototypingActionBase
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000BF23 File Offset: 0x0000A123
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000BF35 File Offset: 0x0000A135
		public string TargetScreen
		{
			get
			{
				return base.GetValue(NavigateToScreenAction.TargetScreenProperty) as string;
			}
			set
			{
				base.SetValue(NavigateToScreenAction.TargetScreenProperty, value);
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000BF44 File Offset: 0x0000A144
		protected override void Invoke(object parameter)
		{
			Assembly assembly = null;
			UserControl userControl = base.AssociatedObject.GetSelfAndAncestors().OfType<UserControl>().FirstOrDefault<UserControl>();
			if (userControl != null)
			{
				assembly = userControl.GetType().Assembly;
			}
			InteractionContext.GoToScreen(this.TargetScreen, assembly);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000BF84 File Offset: 0x0000A184
		protected override Freezable CreateInstanceCore()
		{
			return new NavigateToScreenAction();
		}

		// Token: 0x040000DF RID: 223
		public static readonly DependencyProperty TargetScreenProperty = DependencyProperty.Register("TargetScreen", typeof(string), typeof(NavigateToScreenAction), new PropertyMetadata(null));
	}
}
