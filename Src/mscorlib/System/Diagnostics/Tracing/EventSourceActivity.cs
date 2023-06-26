using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000445 RID: 1093
	internal sealed class EventSourceActivity : IDisposable
	{
		// Token: 0x06003637 RID: 13879 RVA: 0x000D3E0C File Offset: 0x000D200C
		public EventSourceActivity(EventSource eventSource)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			this.eventSource = eventSource;
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x000D3E29 File Offset: 0x000D2029
		public static implicit operator EventSourceActivity(EventSource eventSource)
		{
			return new EventSourceActivity(eventSource);
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000D3E31 File Offset: 0x000D2031
		public EventSource EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600363A RID: 13882 RVA: 0x000D3E39 File Offset: 0x000D2039
		public Guid Id
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x000D3E41 File Offset: 0x000D2041
		public EventSourceActivity Start<T>(string eventName, EventSourceOptions options, T data)
		{
			return this.Start<T>(eventName, ref options, ref data);
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x000D3E50 File Offset: 0x000D2050
		public EventSourceActivity Start(string eventName)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			return this.Start<EmptyStruct>(eventName, ref eventSourceOptions, ref emptyStruct);
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x000D3E78 File Offset: 0x000D2078
		public EventSourceActivity Start(string eventName, EventSourceOptions options)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			return this.Start<EmptyStruct>(eventName, ref options, ref emptyStruct);
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000D3E98 File Offset: 0x000D2098
		public EventSourceActivity Start<T>(string eventName, T data)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			return this.Start<T>(eventName, ref eventSourceOptions, ref data);
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000D3EB8 File Offset: 0x000D20B8
		public void Stop<T>(T data)
		{
			this.Stop<T>(null, ref data);
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000D3EC4 File Offset: 0x000D20C4
		public void Stop<T>(string eventName)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Stop<EmptyStruct>(eventName, ref emptyStruct);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000D3EE2 File Offset: 0x000D20E2
		public void Stop<T>(string eventName, T data)
		{
			this.Stop<T>(eventName, ref data);
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000D3EED File Offset: 0x000D20ED
		public void Write<T>(string eventName, EventSourceOptions options, T data)
		{
			this.Write<T>(this.eventSource, eventName, ref options, ref data);
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000D3F00 File Offset: 0x000D2100
		public void Write<T>(string eventName, T data)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			this.Write<T>(this.eventSource, eventName, ref eventSourceOptions, ref data);
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000D3F28 File Offset: 0x000D2128
		public void Write(string eventName, EventSourceOptions options)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Write<EmptyStruct>(this.eventSource, eventName, ref options, ref emptyStruct);
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000D3F50 File Offset: 0x000D2150
		public void Write(string eventName)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Write<EmptyStruct>(this.eventSource, eventName, ref eventSourceOptions, ref emptyStruct);
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000D3F7E File Offset: 0x000D217E
		public void Write<T>(EventSource source, string eventName, EventSourceOptions options, T data)
		{
			this.Write<T>(source, eventName, ref options, ref data);
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000D3F8C File Offset: 0x000D218C
		public void Dispose()
		{
			if (this.state == EventSourceActivity.State.Started)
			{
				EmptyStruct emptyStruct = default(EmptyStruct);
				this.Stop<EmptyStruct>(null, ref emptyStruct);
			}
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000D3FB4 File Offset: 0x000D21B4
		private EventSourceActivity Start<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (!this.eventSource.IsEnabled())
			{
				return this;
			}
			EventSourceActivity eventSourceActivity = new EventSourceActivity(this.eventSource);
			if (!this.eventSource.IsEnabled(options.Level, options.Keywords))
			{
				Guid id = this.Id;
				eventSourceActivity.activityId = Guid.NewGuid();
				eventSourceActivity.startStopOptions = options;
				eventSourceActivity.eventName = eventName;
				eventSourceActivity.startStopOptions.Opcode = EventOpcode.Start;
				this.eventSource.Write<T>(eventName, ref eventSourceActivity.startStopOptions, ref eventSourceActivity.activityId, ref id, ref data);
			}
			else
			{
				eventSourceActivity.activityId = this.Id;
			}
			return eventSourceActivity;
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000D405E File Offset: 0x000D225E
		private void Write<T>(EventSource eventSource, string eventName, ref EventSourceOptions options, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (eventName == null)
			{
				throw new ArgumentNullException();
			}
			eventSource.Write<T>(eventName, ref options, ref this.activityId, ref EventSourceActivity.s_empty, ref data);
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000D408C File Offset: 0x000D228C
		private void Stop<T>(string eventName, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (!this.StartEventWasFired)
			{
				return;
			}
			this.state = EventSourceActivity.State.Stopped;
			if (eventName == null)
			{
				eventName = this.eventName;
				if (eventName.EndsWith("Start"))
				{
					eventName = eventName.Substring(0, eventName.Length - 5);
				}
				eventName += "Stop";
			}
			this.startStopOptions.Opcode = EventOpcode.Stop;
			this.eventSource.Write<T>(eventName, ref this.startStopOptions, ref this.activityId, ref EventSourceActivity.s_empty, ref data);
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x000D4117 File Offset: 0x000D2317
		private bool StartEventWasFired
		{
			get
			{
				return this.eventName != null;
			}
		}

		// Token: 0x04001839 RID: 6201
		private readonly EventSource eventSource;

		// Token: 0x0400183A RID: 6202
		private EventSourceOptions startStopOptions;

		// Token: 0x0400183B RID: 6203
		internal Guid activityId;

		// Token: 0x0400183C RID: 6204
		private EventSourceActivity.State state;

		// Token: 0x0400183D RID: 6205
		private string eventName;

		// Token: 0x0400183E RID: 6206
		internal static Guid s_empty;

		// Token: 0x02000B97 RID: 2967
		private enum State
		{
			// Token: 0x0400352F RID: 13615
			Started,
			// Token: 0x04003530 RID: 13616
			Stopped
		}
	}
}
