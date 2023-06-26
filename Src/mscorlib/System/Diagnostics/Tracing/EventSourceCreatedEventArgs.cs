using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.Tracing.EventListener.EventSourceCreated" /> event.</summary>
	// Token: 0x02000422 RID: 1058
	public class EventSourceCreatedEventArgs : EventArgs
	{
		/// <summary>Get the event source that is attaching to the listener.</summary>
		/// <returns>The event source that is attaching to the listener.</returns>
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000CFB58 File Offset: 0x000CDD58
		// (set) Token: 0x06003527 RID: 13607 RVA: 0x000CFB60 File Offset: 0x000CDD60
		public EventSource EventSource { get; internal set; }
	}
}
