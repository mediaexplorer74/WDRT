using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Defines size and enumerators for a collection of <see cref="T:System.Diagnostics.EventLogEntry" /> instances.</summary>
	// Token: 0x020004CD RID: 1229
	public class EventLogEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x06002E59 RID: 11865 RVA: 0x000D161F File Offset: 0x000CF81F
		internal EventLogEntryCollection(EventLogInternal log)
		{
			this.log = log;
		}

		/// <summary>Gets the number of entries in the event log (that is, the number of elements in the <see cref="T:System.Diagnostics.EventLogEntry" /> collection).</summary>
		/// <returns>The number of entries currently in the event log.</returns>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x000D162E File Offset: 0x000CF82E
		public int Count
		{
			get
			{
				return this.log.EntryCount;
			}
		}

		/// <summary>Gets an entry in the event log, based on an index that starts at 0 (zero).</summary>
		/// <param name="index">The zero-based index that is associated with the event log entry.</param>
		/// <returns>The event log entry at the location that is specified by the <paramref name="index" /> parameter.</returns>
		// Token: 0x17000B33 RID: 2867
		public virtual EventLogEntry this[int index]
		{
			get
			{
				return this.log.GetEntryAt(index);
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> to an array of <see cref="T:System.Diagnostics.EventLogEntry" /> instances, starting at a particular array index.</summary>
		/// <param name="entries">The one-dimensional array of <see cref="T:System.Diagnostics.EventLogEntry" /> instances that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		// Token: 0x06002E5C RID: 11868 RVA: 0x000D1649 File Offset: 0x000CF849
		public void CopyTo(EventLogEntry[] entries, int index)
		{
			((ICollection)this).CopyTo(entries, index);
		}

		/// <summary>Supports a simple iteration over the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> object.</summary>
		/// <returns>An object that can be used to iterate over the collection.</returns>
		// Token: 0x06002E5D RID: 11869 RVA: 0x000D1653 File Offset: 0x000CF853
		public IEnumerator GetEnumerator()
		{
			return new EventLogEntryCollection.EntriesEnumerator(this);
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000D165B File Offset: 0x000CF85B
		internal EventLogEntry GetEntryAtNoThrow(int index)
		{
			return this.log.GetEntryAtNoThrow(index);
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>
		///   <see langword="false" /> if access to the collection is not synchronized (thread-safe).</returns>
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000D1669 File Offset: 0x000CF869
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x000D166C File Offset: 0x000CF86C
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements that are copied from the collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06002E61 RID: 11873 RVA: 0x000D1670 File Offset: 0x000CF870
		void ICollection.CopyTo(Array array, int index)
		{
			EventLogEntry[] allEntries = this.log.GetAllEntries();
			Array.Copy(allEntries, 0, array, index, allEntries.Length);
		}

		// Token: 0x04002755 RID: 10069
		private EventLogInternal log;

		// Token: 0x0200087C RID: 2172
		private class EntriesEnumerator : IEnumerator
		{
			// Token: 0x06004551 RID: 17745 RVA: 0x00120D93 File Offset: 0x0011EF93
			internal EntriesEnumerator(EventLogEntryCollection entries)
			{
				this.entries = entries;
			}

			// Token: 0x17000FAF RID: 4015
			// (get) Token: 0x06004552 RID: 17746 RVA: 0x00120DA9 File Offset: 0x0011EFA9
			public object Current
			{
				get
				{
					if (this.cachedEntry == null)
					{
						throw new InvalidOperationException(SR.GetString("NoCurrentEntry"));
					}
					return this.cachedEntry;
				}
			}

			// Token: 0x06004553 RID: 17747 RVA: 0x00120DC9 File Offset: 0x0011EFC9
			public bool MoveNext()
			{
				this.num++;
				this.cachedEntry = this.entries.GetEntryAtNoThrow(this.num);
				return this.cachedEntry != null;
			}

			// Token: 0x06004554 RID: 17748 RVA: 0x00120DF9 File Offset: 0x0011EFF9
			public void Reset()
			{
				this.num = -1;
			}

			// Token: 0x04003721 RID: 14113
			private EventLogEntryCollection entries;

			// Token: 0x04003722 RID: 14114
			private int num = -1;

			// Token: 0x04003723 RID: 14115
			private EventLogEntry cachedEntry;
		}
	}
}
