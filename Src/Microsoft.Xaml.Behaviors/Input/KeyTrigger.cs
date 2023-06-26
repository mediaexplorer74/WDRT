using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Microsoft.Xaml.Behaviors.Input
{
	// Token: 0x02000036 RID: 54
	public class KeyTrigger : EventTriggerBase<UIElement>
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007683 File Offset: 0x00005883
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00007695 File Offset: 0x00005895
		public Key Key
		{
			get
			{
				return (Key)base.GetValue(KeyTrigger.KeyProperty);
			}
			set
			{
				base.SetValue(KeyTrigger.KeyProperty, value);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000076A8 File Offset: 0x000058A8
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x000076BA File Offset: 0x000058BA
		public ModifierKeys Modifiers
		{
			get
			{
				return (ModifierKeys)base.GetValue(KeyTrigger.ModifiersProperty);
			}
			set
			{
				base.SetValue(KeyTrigger.ModifiersProperty, value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000076CD File Offset: 0x000058CD
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x000076DF File Offset: 0x000058DF
		public bool ActiveOnFocus
		{
			get
			{
				return (bool)base.GetValue(KeyTrigger.ActiveOnFocusProperty);
			}
			set
			{
				base.SetValue(KeyTrigger.ActiveOnFocusProperty, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000076F2 File Offset: 0x000058F2
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00007704 File Offset: 0x00005904
		public KeyTriggerFiredOn FiredOn
		{
			get
			{
				return (KeyTriggerFiredOn)base.GetValue(KeyTrigger.FiredOnProperty);
			}
			set
			{
				base.SetValue(KeyTrigger.FiredOnProperty, value);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007717 File Offset: 0x00005917
		protected override string GetEventName()
		{
			return "Loaded";
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000771E File Offset: 0x0000591E
		private void OnKeyPress(object sender, KeyEventArgs e)
		{
			if (e.Key == this.Key && Keyboard.Modifiers == KeyTrigger.GetActualModifiers(e.Key, this.Modifiers))
			{
				base.InvokeActions(e);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000774D File Offset: 0x0000594D
		private static ModifierKeys GetActualModifiers(Key key, ModifierKeys modifiers)
		{
			if (key == Key.LeftCtrl || key == Key.RightCtrl)
			{
				modifiers |= ModifierKeys.Control;
			}
			else if (key == Key.LeftAlt || key == Key.RightAlt || key == Key.System)
			{
				modifiers |= ModifierKeys.Alt;
			}
			else if (key == Key.LeftShift || key == Key.RightShift)
			{
				modifiers |= ModifierKeys.Shift;
			}
			return modifiers;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000778C File Offset: 0x0000598C
		protected override void OnEvent(EventArgs eventArgs)
		{
			if (this.ActiveOnFocus)
			{
				this.targetElement = base.Source;
			}
			else
			{
				this.targetElement = KeyTrigger.GetRoot(base.Source);
			}
			if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
			{
				this.targetElement.KeyDown += this.OnKeyPress;
				return;
			}
			this.targetElement.KeyUp += this.OnKeyPress;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000077F8 File Offset: 0x000059F8
		protected override void OnDetaching()
		{
			if (this.targetElement != null)
			{
				if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
				{
					this.targetElement.KeyDown -= this.OnKeyPress;
				}
				else
				{
					this.targetElement.KeyUp -= this.OnKeyPress;
				}
			}
			base.OnDetaching();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000784C File Offset: 0x00005A4C
		private static UIElement GetRoot(DependencyObject current)
		{
			UIElement uielement = null;
			while (current != null)
			{
				uielement = current as UIElement;
				current = VisualTreeHelper.GetParent(current);
			}
			return uielement;
		}

		// Token: 0x04000097 RID: 151
		public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(Key), typeof(KeyTrigger));

		// Token: 0x04000098 RID: 152
		public static readonly DependencyProperty ModifiersProperty = DependencyProperty.Register("Modifiers", typeof(ModifierKeys), typeof(KeyTrigger));

		// Token: 0x04000099 RID: 153
		public static readonly DependencyProperty ActiveOnFocusProperty = DependencyProperty.Register("ActiveOnFocus", typeof(bool), typeof(KeyTrigger));

		// Token: 0x0400009A RID: 154
		public static readonly DependencyProperty FiredOnProperty = DependencyProperty.Register("FiredOn", typeof(KeyTriggerFiredOn), typeof(KeyTrigger));

		// Token: 0x0400009B RID: 155
		private UIElement targetElement;
	}
}
