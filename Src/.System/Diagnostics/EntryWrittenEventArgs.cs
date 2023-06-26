using System;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event.</summary>
	// Token: 0x020004C7 RID: 1223
	public class EntryWrittenEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class.</summary>
		// Token: 0x06002D98 RID: 11672 RVA: 0x000CD412 File Offset: 0x000CB612
		public EntryWrittenEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class with the specified event log entry.</summary>
		/// <param name="entry">An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written.</param>
		// Token: 0x06002D99 RID: 11673 RVA: 0x000CD41A File Offset: 0x000CB61A
		public EntryWrittenEventArgs(EventLogEntry entry)
		{
			this.entry = entry;
		}

		/// <summary>Gets the event log entry that was written to the log.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written to the event log.</returns>
		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002D9A RID: 11674 RVA: 0x000CD429 File Offset: 0x000CB629
		public EventLogEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x0400271C RID: 10012
		private EventLogEntry entry;
	}
}
