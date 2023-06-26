using System;
using System.Reflection;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200000D RID: 13
	public sealed class EventObserver : IDisposable
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002B48 File Offset: 0x00000D48
		public EventObserver(EventInfo eventInfo, object target, Delegate handler)
		{
			if (eventInfo == null)
			{
				throw new ArgumentNullException("eventInfo");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.eventInfo = eventInfo;
			this.target = target;
			this.handler = handler;
			this.eventInfo.AddEventHandler(this.target, handler);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public void Dispose()
		{
			this.eventInfo.RemoveEventHandler(this.target, this.handler);
		}

		// Token: 0x0400001E RID: 30
		private EventInfo eventInfo;

		// Token: 0x0400001F RID: 31
		private object target;

		// Token: 0x04000020 RID: 32
		private Delegate handler;
	}
}
