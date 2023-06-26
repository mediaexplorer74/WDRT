using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200000E RID: 14
	public class EventTrigger : EventTriggerBase<object>
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002BBD File Offset: 0x00000DBD
		public EventTrigger()
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002BC5 File Offset: 0x00000DC5
		public EventTrigger(string eventName)
		{
			this.EventName = eventName;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002BD4 File Offset: 0x00000DD4
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002BE6 File Offset: 0x00000DE6
		public string EventName
		{
			get
			{
				return (string)base.GetValue(EventTrigger.EventNameProperty);
			}
			set
			{
				base.SetValue(EventTrigger.EventNameProperty, value);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002BF4 File Offset: 0x00000DF4
		protected override string GetEventName()
		{
			return this.EventName;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002BFC File Offset: 0x00000DFC
		private static void OnEventNameChanged(object sender, DependencyPropertyChangedEventArgs args)
		{
			((EventTrigger)sender).OnEventNameChanged((string)args.OldValue, (string)args.NewValue);
		}

		// Token: 0x04000021 RID: 33
		public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register("EventName", typeof(string), typeof(EventTrigger), new FrameworkPropertyMetadata("Loaded", new PropertyChangedCallback(EventTrigger.OnEventNameChanged)));
	}
}
