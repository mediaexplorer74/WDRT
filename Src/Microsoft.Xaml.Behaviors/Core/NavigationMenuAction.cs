using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x0200004F RID: 79
	[DefaultTrigger(typeof(FrameworkElement), typeof(EventTrigger), "Loaded")]
	[DefaultTrigger(typeof(ButtonBase), typeof(EventTrigger), "Loaded")]
	public sealed class NavigationMenuAction : TargetedTriggerAction<FrameworkElement>
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000C0D3 File Offset: 0x0000A2D3
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000C0E5 File Offset: 0x0000A2E5
		public string TargetScreen
		{
			get
			{
				return (string)base.GetValue(NavigationMenuAction.TargetScreenProperty);
			}
			set
			{
				base.SetValue(NavigationMenuAction.TargetScreenProperty, value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000C0F3 File Offset: 0x0000A2F3
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000C105 File Offset: 0x0000A305
		public string ActiveState
		{
			get
			{
				return (string)base.GetValue(NavigationMenuAction.ActiveStateProperty);
			}
			set
			{
				base.SetValue(NavigationMenuAction.ActiveStateProperty, value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000C113 File Offset: 0x0000A313
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000C125 File Offset: 0x0000A325
		public string InactiveState
		{
			get
			{
				return (string)base.GetValue(NavigationMenuAction.InactiveStateProperty);
			}
			set
			{
				base.SetValue(NavigationMenuAction.InactiveStateProperty, value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000C133 File Offset: 0x0000A333
		private bool IsTargetObjectSet
		{
			get
			{
				return base.ReadLocalValue(TargetedTriggerAction.TargetObjectProperty) != DependencyProperty.UnsetValue;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000C14A File Offset: 0x0000A34A
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000C152 File Offset: 0x0000A352
		private FrameworkElement StateTarget { get; set; }

		// Token: 0x060002D6 RID: 726 RVA: 0x0000C15C File Offset: 0x0000A35C
		protected override void OnTargetChanged(FrameworkElement oldTarget, FrameworkElement newTarget)
		{
			base.OnTargetChanged(oldTarget, newTarget);
			FrameworkElement frameworkElement = null;
			if (string.IsNullOrEmpty(base.TargetName) && !this.IsTargetObjectSet)
			{
				VisualStateUtilities.TryFindNearestStatefulControl(base.AssociatedObject as FrameworkElement, out frameworkElement);
			}
			else
			{
				frameworkElement = base.Target;
			}
			this.StateTarget = frameworkElement;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000C1AB File Offset: 0x0000A3AB
		protected override void Invoke(object parameter)
		{
			if (base.AssociatedObject != null)
			{
				this.InvokeImpl(this.StateTarget);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
		internal void InvokeImpl(FrameworkElement stateTarget)
		{
			if (stateTarget != null && !string.IsNullOrEmpty(this.ActiveState) && !string.IsNullOrEmpty(this.InactiveState) && !string.IsNullOrEmpty(this.TargetScreen))
			{
				bool flag = stateTarget.GetSelfAndAncestors().OfType<UserControl>().FirstOrDefault((UserControl control) => control.GetType().ToString() == this.TargetScreen) != null;
				string text = this.InactiveState;
				if (flag)
				{
					text = this.ActiveState;
				}
				if (!string.IsNullOrEmpty(text))
				{
					ToggleButton toggleButton = stateTarget as ToggleButton;
					if (toggleButton != null)
					{
						if (text == "Checked")
						{
							toggleButton.IsChecked = new bool?(true);
							return;
						}
						if (text == "Unchecked")
						{
							toggleButton.IsChecked = new bool?(false);
							return;
						}
					}
					if (text == "Disabled")
					{
						stateTarget.IsEnabled = false;
						return;
					}
					VisualStateUtilities.GoToState(stateTarget, text, true);
				}
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000C299 File Offset: 0x0000A499
		protected override Freezable CreateInstanceCore()
		{
			return new NavigationMenuAction();
		}

		// Token: 0x040000E2 RID: 226
		public static readonly DependencyProperty InactiveStateProperty = DependencyProperty.Register("InactiveState", typeof(string), typeof(NavigationMenuAction), new PropertyMetadata(null));

		// Token: 0x040000E3 RID: 227
		public static readonly DependencyProperty TargetScreenProperty = DependencyProperty.Register("TargetScreen", typeof(string), typeof(NavigationMenuAction), new PropertyMetadata(null));

		// Token: 0x040000E4 RID: 228
		public static readonly DependencyProperty ActiveStateProperty = DependencyProperty.Register("ActiveState", typeof(string), typeof(NavigationMenuAction), new PropertyMetadata(null));
	}
}
