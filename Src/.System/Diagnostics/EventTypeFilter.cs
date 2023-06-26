using System;

namespace System.Diagnostics
{
	/// <summary>Indicates whether a listener should trace based on the event type.</summary>
	// Token: 0x0200049F RID: 1183
	public class EventTypeFilter : TraceFilter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventTypeFilter" /> class.</summary>
		/// <param name="level">A bitwise combination of the <see cref="T:System.Diagnostics.SourceLevels" /> values that specifies the event type of the messages to trace.</param>
		// Token: 0x06002BD9 RID: 11225 RVA: 0x000C6688 File Offset: 0x000C4888
		public EventTypeFilter(SourceLevels level)
		{
			this.level = level;
		}

		/// <summary>Determines whether the trace listener should trace the event.</summary>
		/// <param name="cache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that represents the information cache for the trace event.</param>
		/// <param name="source">The name of the source.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A trace identifier number.</param>
		/// <param name="formatOrMessage">The format to use for writing an array of arguments, or a message to write.</param>
		/// <param name="args">An array of argument objects.</param>
		/// <param name="data1">A trace data object.</param>
		/// <param name="data">An array of trace data objects.</param>
		/// <returns>
		///   <see langword="true" /> if the trace should be produced; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002BDA RID: 11226 RVA: 0x000C6697 File Offset: 0x000C4897
		public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
		{
			return (eventType & (TraceEventType)this.level) > (TraceEventType)0;
		}

		/// <summary>Gets or sets the event type of the messages to trace.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.SourceLevels" /> values.</returns>
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x000C66A4 File Offset: 0x000C48A4
		// (set) Token: 0x06002BDC RID: 11228 RVA: 0x000C66AC File Offset: 0x000C48AC
		public SourceLevels EventType
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		// Token: 0x04002687 RID: 9863
		private SourceLevels level;
	}
}
