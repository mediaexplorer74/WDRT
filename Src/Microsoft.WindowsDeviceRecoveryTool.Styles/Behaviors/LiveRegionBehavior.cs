using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.Common;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Behaviors
{
	// Token: 0x02000015 RID: 21
	public sealed class LiveRegionBehavior
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000031A0 File Offset: 0x000013A0
		private LiveRegionBehavior(FrameworkElement element)
		{
			this.element = element;
			LiveRegionBehavior.AddLiveRegionChangedHandler(element, new RoutedEventHandler(this.FrameworkElement_OnLiveRegionChanged));
			element.Loaded += this.FrameworkElement_OnLoaded;
			element.DataContextChanged += this.FrameworkElement_OnDataContextChanged;
			INotifyLiveRegionChanged notifyLiveRegionChanged = element.DataContext as INotifyLiveRegionChanged;
			bool flag = notifyLiveRegionChanged != null;
			if (flag)
			{
				notifyLiveRegionChanged.LiveRegionChanged += this.DataContext_OnLiveRegionChanged;
			}
			this.LiveSetting = (LiveSetting)LiveRegionBehavior.LiveSettingProperty.DefaultMetadata.DefaultValue;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003239 File Offset: 0x00001439
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003241 File Offset: 0x00001441
		public LiveSetting LiveSetting { get; set; }

		// Token: 0x06000060 RID: 96 RVA: 0x0000324C File Offset: 0x0000144C
		public static LiveSetting GetLiveSetting(FrameworkElement element)
		{
			return (LiveSetting)element.GetValue(LiveRegionBehavior.LiveSettingProperty);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000326E File Offset: 0x0000146E
		public static void SetLiveSetting(FrameworkElement element, LiveSetting value)
		{
			element.SetValue(LiveRegionBehavior.LiveSettingProperty, value);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003284 File Offset: 0x00001484
		public static void RaiseLiveRegionChanged(FrameworkElement element)
		{
			RoutedEventArgs routedEventArgs = new RoutedEventArgs
			{
				RoutedEvent = LiveRegionBehavior.LiveRegionChangedEvent
			};
			element.RaiseEvent(routedEventArgs);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032AC File Offset: 0x000014AC
		public static void AddLiveRegionChangedHandler(FrameworkElement element, RoutedEventHandler handler)
		{
			element.AddHandler(LiveRegionBehavior.LiveRegionChangedEvent, handler);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000032BC File Offset: 0x000014BC
		public static void RemoveLiveRegionChangedHandler(FrameworkElement element, RoutedEventHandler handler)
		{
			element.RemoveHandler(LiveRegionBehavior.LiveRegionChangedEvent, handler);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000032CC File Offset: 0x000014CC
		private static void SetBehavior(FrameworkElement element, LiveRegionBehavior value)
		{
			element.SetValue(LiveRegionBehavior.BehaviorProperty, value);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000032DC File Offset: 0x000014DC
		private static LiveRegionBehavior GetBehavior(FrameworkElement element)
		{
			return (LiveRegionBehavior)element.GetValue(LiveRegionBehavior.BehaviorProperty);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003300 File Offset: 0x00001500
		private static void LiveSetting_OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			FrameworkElement frameworkElement = obj as FrameworkElement;
			bool flag = frameworkElement == null;
			if (!flag)
			{
				LiveRegionBehavior liveRegionBehavior = LiveRegionBehavior.GetBehavior(frameworkElement);
				bool flag2 = liveRegionBehavior == null;
				if (flag2)
				{
					liveRegionBehavior = new LiveRegionBehavior(frameworkElement);
					LiveRegionBehavior.SetBehavior(frameworkElement, liveRegionBehavior);
				}
				liveRegionBehavior.LiveSetting = (LiveSetting)args.NewValue;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003354 File Offset: 0x00001554
		private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
		{
			bool flag = this.isNotificationPending;
			if (flag)
			{
				this.NotifyLiveRegionChanged();
				this.isNotificationPending = false;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000337C File Offset: 0x0000157C
		private void FrameworkElement_OnLiveRegionChanged(object sender, RoutedEventArgs e)
		{
			bool isLoaded = this.element.IsLoaded;
			if (isLoaded)
			{
				this.NotifyLiveRegionChanged();
			}
			else
			{
				this.isNotificationPending = true;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000033B0 File Offset: 0x000015B0
		private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			INotifyLiveRegionChanged notifyLiveRegionChanged = e.OldValue as INotifyLiveRegionChanged;
			INotifyLiveRegionChanged notifyLiveRegionChanged2 = e.NewValue as INotifyLiveRegionChanged;
			bool flag = notifyLiveRegionChanged != null;
			if (flag)
			{
				notifyLiveRegionChanged.LiveRegionChanged -= this.DataContext_OnLiveRegionChanged;
			}
			bool flag2 = notifyLiveRegionChanged2 != null;
			if (flag2)
			{
				notifyLiveRegionChanged2.LiveRegionChanged += this.DataContext_OnLiveRegionChanged;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003412 File Offset: 0x00001612
		private void DataContext_OnLiveRegionChanged(object sender, EventArgs e)
		{
			LiveRegionBehavior.RaiseLiveRegionChanged(this.element);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003424 File Offset: 0x00001624
		private void NotifyLiveRegionChanged()
		{
			LiveSetting liveSetting = this.LiveSetting;
			LiveSetting liveSetting2 = liveSetting;
			if (liveSetting2 != LiveSetting.Off)
			{
				if (liveSetting2 == LiveSetting.Assertive)
				{
					this.ResetKeyboardFocus();
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003454 File Offset: 0x00001654
		private void ResetKeyboardFocus()
		{
			bool isKeyboardFocused = this.element.IsKeyboardFocused;
			if (isKeyboardFocused)
			{
				Keyboard.ClearFocus();
			}
			Keyboard.Focus(this.element);
		}

		// Token: 0x04000014 RID: 20
		public static readonly DependencyProperty LiveSettingProperty = DependencyProperty.RegisterAttached("LiveSetting", typeof(LiveSetting), typeof(LiveRegionBehavior), new FrameworkPropertyMetadata(LiveSetting.Off, new PropertyChangedCallback(LiveRegionBehavior.LiveSetting_OnPropertyChanged)));

		// Token: 0x04000015 RID: 21
		public static readonly RoutedEvent LiveRegionChangedEvent = EventManager.RegisterRoutedEvent("LiveRegionChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(LiveRegionBehavior));

		// Token: 0x04000016 RID: 22
		private static readonly DependencyProperty BehaviorProperty = DependencyProperty.RegisterAttached("Behavior", typeof(LiveRegionBehavior), typeof(LiveRegionBehavior), new FrameworkPropertyMetadata(null));

		// Token: 0x04000017 RID: 23
		private readonly FrameworkElement element;

		// Token: 0x04000018 RID: 24
		private bool isNotificationPending;
	}
}
