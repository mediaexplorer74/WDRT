using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000C6 RID: 198
	public class InvokableToggleButton : ToggleButton
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x0001C935 File Offset: 0x0001AB35
		static InvokableToggleButton()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(InvokableToggleButton), new FrameworkPropertyMetadata(typeof(InvokableToggleButton)));
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001C95C File Offset: 0x0001AB5C
		internal void DoAutomationPeerClick()
		{
			this.OnClick();
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001C968 File Offset: 0x0001AB68
		protected override AutomationPeer OnCreateAutomationPeer()
		{
			return new InvokableToggleButton.InvokableToggleButtonAutomationPeer(this);
		}

		// Token: 0x0200015C RID: 348
		private sealed class InvokableToggleButtonAutomationPeer : ToggleButtonAutomationPeer, IInvokeProvider
		{
			// Token: 0x06000875 RID: 2165 RVA: 0x000257D8 File Offset: 0x000239D8
			public InvokableToggleButtonAutomationPeer(InvokableToggleButton owner)
				: base(owner)
			{
				owner.Click += delegate(object s, RoutedEventArgs a)
				{
					base.RaiseAutomationEvent(AutomationEvents.InvokePatternOnInvoked);
				};
			}

			// Token: 0x06000876 RID: 2166 RVA: 0x000257F8 File Offset: 0x000239F8
			void IInvokeProvider.Invoke()
			{
				bool flag = !base.IsEnabled();
				if (flag)
				{
					throw new ElementNotEnabledException();
				}
				base.Dispatcher.BeginInvoke(DispatcherPriority.Input, new DispatcherOperationCallback(delegate
				{
					((HyperlinkButton)base.Owner).DoAutomationPeerClick();
					return null;
				}), null);
			}

			// Token: 0x06000877 RID: 2167 RVA: 0x00025834 File Offset: 0x00023A34
			public override object GetPattern(PatternInterface patternInterface)
			{
				return (patternInterface == PatternInterface.Invoke) ? this : base.GetPattern(patternInterface);
			}
		}
	}
}
