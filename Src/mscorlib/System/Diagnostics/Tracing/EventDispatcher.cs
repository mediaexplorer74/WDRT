using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042D RID: 1069
	internal class EventDispatcher
	{
		// Token: 0x06003586 RID: 13702 RVA: 0x000D0912 File Offset: 0x000CEB12
		internal EventDispatcher(EventDispatcher next, bool[] eventEnabled, EventListener listener)
		{
			this.m_Next = next;
			this.m_EventEnabled = eventEnabled;
			this.m_Listener = listener;
		}

		// Token: 0x040017CA RID: 6090
		internal readonly EventListener m_Listener;

		// Token: 0x040017CB RID: 6091
		internal bool[] m_EventEnabled;

		// Token: 0x040017CC RID: 6092
		internal bool m_activityFilteringEnabled;

		// Token: 0x040017CD RID: 6093
		internal EventDispatcher m_Next;
	}
}
