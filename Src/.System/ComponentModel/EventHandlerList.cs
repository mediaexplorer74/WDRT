using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a simple list of delegates. This class cannot be inherited.</summary>
	// Token: 0x02000550 RID: 1360
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public sealed class EventHandlerList : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventHandlerList" /> class.</summary>
		// Token: 0x06003324 RID: 13092 RVA: 0x000E3269 File Offset: 0x000E1469
		public EventHandlerList()
		{
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000E3271 File Offset: 0x000E1471
		internal EventHandlerList(Component parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets or sets the delegate for the specified object.</summary>
		/// <param name="key">An object to find in the list.</param>
		/// <returns>The delegate for the specified key, or <see langword="null" /> if a delegate does not exist.</returns>
		// Token: 0x17000C7F RID: 3199
		public Delegate this[object key]
		{
			get
			{
				EventHandlerList.ListEntry listEntry = null;
				if (this.parent == null || this.parent.CanRaiseEventsInternal)
				{
					listEntry = this.Find(key);
				}
				if (listEntry != null)
				{
					return listEntry.handler;
				}
				return null;
			}
			set
			{
				EventHandlerList.ListEntry listEntry = this.Find(key);
				if (listEntry != null)
				{
					listEntry.handler = value;
					return;
				}
				this.head = new EventHandlerList.ListEntry(key, value, this.head);
			}
		}

		/// <summary>Adds a delegate to the list.</summary>
		/// <param name="key">The object that owns the event.</param>
		/// <param name="value">The delegate to add to the list.</param>
		// Token: 0x06003328 RID: 13096 RVA: 0x000E32EC File Offset: 0x000E14EC
		public void AddHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry.handler = Delegate.Combine(listEntry.handler, value);
				return;
			}
			this.head = new EventHandlerList.ListEntry(key, value, this.head);
		}

		/// <summary>Adds a list of delegates to the current list.</summary>
		/// <param name="listToAddFrom">The list to add.</param>
		// Token: 0x06003329 RID: 13097 RVA: 0x000E332C File Offset: 0x000E152C
		public void AddHandlers(EventHandlerList listToAddFrom)
		{
			for (EventHandlerList.ListEntry next = listToAddFrom.head; next != null; next = next.next)
			{
				this.AddHandler(next.key, next.handler);
			}
		}

		/// <summary>Disposes the delegate list.</summary>
		// Token: 0x0600332A RID: 13098 RVA: 0x000E335E File Offset: 0x000E155E
		public void Dispose()
		{
			this.head = null;
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000E3368 File Offset: 0x000E1568
		private EventHandlerList.ListEntry Find(object key)
		{
			EventHandlerList.ListEntry next = this.head;
			while (next != null && next.key != key)
			{
				next = next.next;
			}
			return next;
		}

		/// <summary>Removes a delegate from the list.</summary>
		/// <param name="key">The object that owns the event.</param>
		/// <param name="value">The delegate to remove from the list.</param>
		// Token: 0x0600332C RID: 13100 RVA: 0x000E3394 File Offset: 0x000E1594
		public void RemoveHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry.handler = Delegate.Remove(listEntry.handler, value);
			}
		}

		// Token: 0x0400299E RID: 10654
		private EventHandlerList.ListEntry head;

		// Token: 0x0400299F RID: 10655
		private Component parent;

		// Token: 0x02000891 RID: 2193
		private sealed class ListEntry
		{
			// Token: 0x0600456F RID: 17775 RVA: 0x00122349 File Offset: 0x00120549
			public ListEntry(object key, Delegate handler, EventHandlerList.ListEntry next)
			{
				this.next = next;
				this.key = key;
				this.handler = handler;
			}

			// Token: 0x040037A9 RID: 14249
			internal EventHandlerList.ListEntry next;

			// Token: 0x040037AA RID: 14250
			internal object key;

			// Token: 0x040037AB RID: 14251
			internal Delegate handler;
		}
	}
}
