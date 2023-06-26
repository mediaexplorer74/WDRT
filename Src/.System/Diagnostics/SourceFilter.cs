using System;

namespace System.Diagnostics
{
	/// <summary>Indicates whether a listener should trace a message based on the source of a trace.</summary>
	// Token: 0x020004A2 RID: 1186
	public class SourceFilter : TraceFilter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceFilter" /> class, specifying the name of the trace source.</summary>
		/// <param name="source">The name of the trace source.</param>
		// Token: 0x06002BF2 RID: 11250 RVA: 0x000C6A15 File Offset: 0x000C4C15
		public SourceFilter(string source)
		{
			this.Source = source;
		}

		/// <summary>Determines whether the trace listener should trace the event.</summary>
		/// <param name="cache">An object that represents the information cache for the trace event.</param>
		/// <param name="source">The name of the source.</param>
		/// <param name="eventType">One of the enumeration values that identifies the event type.</param>
		/// <param name="id">A trace identifier number.</param>
		/// <param name="formatOrMessage">The format to use for writing an array of arguments or a message to write.</param>
		/// <param name="args">An array of argument objects.</param>
		/// <param name="data1">A trace data object.</param>
		/// <param name="data">An array of trace data objects.</param>
		/// <returns>
		///   <see langword="true" /> if the trace should be produced; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06002BF3 RID: 11251 RVA: 0x000C6A24 File Offset: 0x000C4C24
		public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return string.Equals(this.src, source);
		}

		/// <summary>Gets or sets the name of the trace source.</summary>
		/// <returns>The name of the trace source.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is <see langword="null" />.</exception>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x000C6A40 File Offset: 0x000C4C40
		// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x000C6A48 File Offset: 0x000C4C48
		public string Source
		{
			get
			{
				return this.src;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("source");
				}
				this.src = value;
			}
		}

		// Token: 0x0400268F RID: 9871
		private string src;
	}
}
