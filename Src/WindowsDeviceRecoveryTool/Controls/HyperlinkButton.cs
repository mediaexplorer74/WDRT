using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000C7 RID: 199
	public class HyperlinkButton : ButtonBase
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001C98C File Offset: 0x0001AB8C
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0001C9AE File Offset: 0x0001ABAE
		[Bindable(true)]
		[Localizability(LocalizationCategory.Hyperlink)]
		[TypeConverter(typeof(UriTypeConverter))]
		public Uri NavigateUri
		{
			get
			{
				return base.GetValue(HyperlinkButton.NavigateUriProperty) as Uri;
			}
			set
			{
				base.SetValue(HyperlinkButton.NavigateUriProperty, value);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x0001C9E2 File Offset: 0x0001ABE2
		[Bindable(true)]
		[Localizability(LocalizationCategory.None, Modifiability = Modifiability.Unmodifiable)]
		public string TargetName
		{
			get
			{
				return base.GetValue(HyperlinkButton.TargetNameProperty) as string;
			}
			set
			{
				base.SetValue(HyperlinkButton.TargetNameProperty, value);
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001C9F4 File Offset: 0x0001ABF4
		static HyperlinkButton()
		{
			HyperlinkButton.RequestNavigateEvent = Hyperlink.RequestNavigateEvent.AddOwner(typeof(HyperlinkButton));
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(HyperlinkButton), new FrameworkPropertyMetadata(typeof(HyperlinkButton)));
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060005FF RID: 1535 RVA: 0x0001CA71 File Offset: 0x0001AC71
		// (remove) Token: 0x06000600 RID: 1536 RVA: 0x0001CA81 File Offset: 0x0001AC81
		public event RequestNavigateEventHandler RequestNavigate
		{
			add
			{
				base.AddHandler(HyperlinkButton.RequestNavigateEvent, value);
			}
			remove
			{
				base.RemoveHandler(HyperlinkButton.RequestNavigateEvent, value);
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001C95C File Offset: 0x0001AB5C
		internal void DoAutomationPeerClick()
		{
			this.OnClick();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001CA94 File Offset: 0x0001AC94
		protected override void OnClick()
		{
			bool flag = this.NavigateUri != null;
			if (flag)
			{
				base.RaiseEvent(new RequestNavigateEventArgs(this.NavigateUri, this.TargetName));
			}
			base.OnClick();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
		protected override AutomationPeer OnCreateAutomationPeer()
		{
			return new HyperlinkButton.HyperlinkButtonAutomationPeer(this);
		}

		// Token: 0x040002B9 RID: 697
		public static readonly DependencyProperty NavigateUriProperty = Hyperlink.NavigateUriProperty.AddOwner(typeof(HyperlinkButton));

		// Token: 0x040002BA RID: 698
		public static readonly DependencyProperty TargetNameProperty = Hyperlink.TargetNameProperty.AddOwner(typeof(HyperlinkButton));

		// Token: 0x0200015D RID: 349
		private sealed class HyperlinkButtonAutomationPeer : ButtonBaseAutomationPeer, IInvokeProvider
		{
			// Token: 0x0600087A RID: 2170 RVA: 0x00025884 File Offset: 0x00023A84
			public HyperlinkButtonAutomationPeer(HyperlinkButton owner)
				: base(owner)
			{
				owner.Click += delegate(object s, RoutedEventArgs a)
				{
					base.RaiseAutomationEvent(AutomationEvents.InvokePatternOnInvoked);
				};
			}

			// Token: 0x0600087B RID: 2171 RVA: 0x000258A4 File Offset: 0x00023AA4
			protected override AutomationControlType GetAutomationControlTypeCore()
			{
				return AutomationControlType.Hyperlink;
			}

			// Token: 0x0600087C RID: 2172 RVA: 0x000258B8 File Offset: 0x00023AB8
			protected override string GetClassNameCore()
			{
				return "Hyperlink";
			}

			// Token: 0x0600087D RID: 2173 RVA: 0x000258D0 File Offset: 0x00023AD0
			protected override bool IsControlElementCore()
			{
				return true;
			}

			// Token: 0x0600087E RID: 2174 RVA: 0x000258E4 File Offset: 0x00023AE4
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

			// Token: 0x0600087F RID: 2175 RVA: 0x00025920 File Offset: 0x00023B20
			public override object GetPattern(PatternInterface patternInterface)
			{
				return (patternInterface == PatternInterface.Invoke) ? this : base.GetPattern(patternInterface);
			}
		}
	}
}
