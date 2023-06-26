using System;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000D6 RID: 214
	public class MainAreaControl : ContentControl
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x0001F0BC File Offset: 0x0001D2BC
		static MainAreaControl()
		{
			MainAreaControl.ContentChangedEvent = EventManager.RegisterRoutedEvent("ContentChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MainAreaControl));
			ContentControl.ContentProperty.OverrideMetadata(typeof(MainAreaControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(MainAreaControl.OnContentPropertyChanged)));
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060006B8 RID: 1720 RVA: 0x0001F114 File Offset: 0x0001D314
		// (remove) Token: 0x060006B9 RID: 1721 RVA: 0x0001F124 File Offset: 0x0001D324
		public event RoutedEventHandler ContentChanged
		{
			add
			{
				base.AddHandler(MainAreaControl.ContentChangedEvent, value);
			}
			remove
			{
				base.RemoveHandler(MainAreaControl.ContentChangedEvent, value);
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001F134 File Offset: 0x0001D334
		private static void OnContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MainAreaControl mainAreaControl = d as MainAreaControl;
			bool flag = mainAreaControl != null && e.NewValue != e.OldValue;
			if (flag)
			{
				mainAreaControl.RaiseContentChangedEvent();
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001F170 File Offset: 0x0001D370
		private void RaiseContentChangedEvent()
		{
			RoutedEventArgs routedEventArgs = new RoutedEventArgs(MainAreaControl.ContentChangedEvent);
			base.RaiseEvent(routedEventArgs);
		}
	}
}
