using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000DB RID: 219
	public partial class SettingsControl : Grid
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x0001F810 File Offset: 0x0001DA10
		public SettingsControl()
		{
			this.InitializeComponent();
			this.timer = new Timer(5000.0);
			this.timer.Elapsed += this.TimerOnElapsed;
			base.GotFocus += this.SettingsControlGotFocus;
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060006E9 RID: 1769 RVA: 0x0001F86B File Offset: 0x0001DA6B
		// (remove) Token: 0x060006EA RID: 1770 RVA: 0x0001F87B File Offset: 0x0001DA7B
		public event RoutedEventHandler Open
		{
			add
			{
				base.AddHandler(SettingsControl.OpenEvent, value);
			}
			remove
			{
				base.RemoveHandler(SettingsControl.OpenEvent, value);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060006EB RID: 1771 RVA: 0x0001F88B File Offset: 0x0001DA8B
		// (remove) Token: 0x060006EC RID: 1772 RVA: 0x0001F89B File Offset: 0x0001DA9B
		public event RoutedEventHandler Close
		{
			add
			{
				base.AddHandler(SettingsControl.CloseEvent, value);
			}
			remove
			{
				base.RemoveHandler(SettingsControl.CloseEvent, value);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001F8AC File Offset: 0x0001DAAC
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0001F8CE File Offset: 0x0001DACE
		public bool IsOpened
		{
			get
			{
				return (bool)base.GetValue(SettingsControl.IsOpenedProperty);
			}
			set
			{
				base.SetValue(SettingsControl.IsOpenedProperty, value);
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001F8E3 File Offset: 0x0001DAE3
		protected override void OnMouseEnter(MouseEventArgs e)
		{
			this.timer.Stop();
			base.OnMouseEnter(e);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001F8FA File Offset: 0x0001DAFA
		protected override void OnMouseLeave(MouseEventArgs e)
		{
			this.timer.Start();
			base.OnMouseLeave(e);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001F914 File Offset: 0x0001DB14
		protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			bool flag = !this.IsOpened;
			if (flag)
			{
				base.SetCurrentValue(SettingsControl.IsOpenedProperty, true);
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001F944 File Offset: 0x0001DB44
		protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
		{
			bool flag = !this.IsOpened;
			if (flag)
			{
				base.SetCurrentValue(SettingsControl.IsOpenedProperty, true);
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001F973 File Offset: 0x0001DB73
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			e.Handled = true;
			base.OnMouseLeftButtonDown(e);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001F986 File Offset: 0x0001DB86
		protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
		{
			e.Handled = true;
			base.OnMouseRightButtonDown(e);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001F99C File Offset: 0x0001DB9C
		private static void OnIsOpenedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SettingsControl settingsControl = d as SettingsControl;
			bool flag = settingsControl != null && (bool)e.NewValue != (bool)e.OldValue;
			if (flag)
			{
				bool flag2 = (bool)e.NewValue;
				if (flag2)
				{
					settingsControl.RaiseOpenEvent();
					settingsControl.timer.Start();
				}
				else
				{
					settingsControl.timer.Stop();
					settingsControl.RaiseCloseEvent();
				}
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001FA16 File Offset: 0x0001DC16
		private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate
			{
				bool flag = !base.IsMouseOver && !base.IsFocused;
				if (flag)
				{
					this.timer.Stop();
					base.SetCurrentValue(SettingsControl.IsOpenedProperty, false);
				}
			}), new object[0]);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001FA38 File Offset: 0x0001DC38
		private void RaiseCloseEvent()
		{
			RoutedEventArgs routedEventArgs = new RoutedEventArgs(SettingsControl.CloseEvent);
			base.RaiseEvent(routedEventArgs);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001FA5C File Offset: 0x0001DC5C
		private void RaiseOpenEvent()
		{
			RoutedEventArgs routedEventArgs = new RoutedEventArgs(SettingsControl.OpenEvent);
			base.RaiseEvent(routedEventArgs);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001FA7D File Offset: 0x0001DC7D
		private void SettingsButtonOnClick(object sender, RoutedEventArgs e)
		{
			base.SetCurrentValue(SettingsControl.IsOpenedProperty, false);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001FA94 File Offset: 0x0001DC94
		private void SettingsControlGotFocus(object sender, RoutedEventArgs e)
		{
			this.timer.Stop();
			bool flag = !this.IsOpened;
			if (flag)
			{
				base.Focus();
				base.SetCurrentValue(SettingsControl.IsOpenedProperty, true);
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001FAD6 File Offset: 0x0001DCD6
		private void HyperlinkButtonOnRequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
			e.Handled = true;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001FA7D File Offset: 0x0001DC7D
		private void HelpButtonOnClick(object sender, RoutedEventArgs e)
		{
			base.SetCurrentValue(SettingsControl.IsOpenedProperty, false);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001FBD8 File Offset: 0x0001DDD8
		// Note: this type is marked as 'beforefieldinit'.
		static SettingsControl()
		{
			SettingsControl.OpenEvent = EventManager.RegisterRoutedEvent("Open", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SettingsControl));
			SettingsControl.CloseEvent = EventManager.RegisterRoutedEvent("Close", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SettingsControl));
		}

		// Token: 0x0400031C RID: 796
		public static readonly DependencyProperty IsOpenedProperty = DependencyProperty.Register("IsOpened", typeof(bool), typeof(SettingsControl), new PropertyMetadata(false, new PropertyChangedCallback(SettingsControl.OnIsOpenedChanged)));

		// Token: 0x0400031F RID: 799
		private readonly Timer timer;
	}
}
