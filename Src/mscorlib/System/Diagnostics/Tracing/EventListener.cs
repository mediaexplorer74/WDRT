using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides methods for enabling and disabling events from event sources.</summary>
	// Token: 0x02000420 RID: 1056
	[__DynamicallyInvokable]
	public class EventListener : IDisposable
	{
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06003508 RID: 13576 RVA: 0x000CF2E4 File Offset: 0x000CD4E4
		// (remove) Token: 0x06003509 RID: 13577 RVA: 0x000CF31C File Offset: 0x000CD51C
		private event EventHandler<EventSourceCreatedEventArgs> _EventSourceCreated;

		/// <summary>Occurs when an event source (<see cref="T:System.Diagnostics.Tracing.EventSource" /> object) is attached to the dispatcher.</summary>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600350A RID: 13578 RVA: 0x000CF354 File Offset: 0x000CD554
		// (remove) Token: 0x0600350B RID: 13579 RVA: 0x000CF3AC File Offset: 0x000CD5AC
		public event EventHandler<EventSourceCreatedEventArgs> EventSourceCreated
		{
			add
			{
				object obj = EventListener.s_EventSourceCreatedLock;
				lock (obj)
				{
					this.CallBackForExistingEventSources(false, value);
					this._EventSourceCreated = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Combine(this._EventSourceCreated, value);
				}
			}
			remove
			{
				object obj = EventListener.s_EventSourceCreatedLock;
				lock (obj)
				{
					this._EventSourceCreated = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Remove(this._EventSourceCreated, value);
				}
			}
		}

		/// <summary>Occurs when an event has been written by an event source (<see cref="T:System.Diagnostics.Tracing.EventSource" /> object) for which the event listener has enabled events.</summary>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600350C RID: 13580 RVA: 0x000CF3FC File Offset: 0x000CD5FC
		// (remove) Token: 0x0600350D RID: 13581 RVA: 0x000CF434 File Offset: 0x000CD634
		public event EventHandler<EventWrittenEventArgs> EventWritten;

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventListener" /> class.</summary>
		// Token: 0x0600350E RID: 13582 RVA: 0x000CF469 File Offset: 0x000CD669
		[__DynamicallyInvokable]
		public EventListener()
		{
			this.CallBackForExistingEventSources(true, delegate(object obj, EventSourceCreatedEventArgs args)
			{
				args.EventSource.AddListener(this);
			});
		}

		/// <summary>Releases the resources used by the current instance of the <see cref="T:System.Diagnostics.Tracing.EventListener" /> class.</summary>
		// Token: 0x0600350F RID: 13583 RVA: 0x000CF484 File Offset: 0x000CD684
		[__DynamicallyInvokable]
		public virtual void Dispose()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_Listeners != null)
				{
					if (this == EventListener.s_Listeners)
					{
						EventListener eventListener = EventListener.s_Listeners;
						EventListener.s_Listeners = this.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(eventListener);
					}
					else
					{
						EventListener eventListener2 = EventListener.s_Listeners;
						EventListener next;
						for (;;)
						{
							next = eventListener2.m_Next;
							if (next == null)
							{
								break;
							}
							if (next == this)
							{
								goto Block_6;
							}
							eventListener2 = next;
						}
						return;
						Block_6:
						eventListener2.m_Next = next.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(next);
					}
				}
			}
		}

		/// <summary>Enables events for the specified event source that has the specified verbosity level or lower.</summary>
		/// <param name="eventSource">The event source to enable events for.</param>
		/// <param name="level">The level of events to enable.</param>
		// Token: 0x06003510 RID: 13584 RVA: 0x000CF524 File Offset: 0x000CD724
		[__DynamicallyInvokable]
		public void EnableEvents(EventSource eventSource, EventLevel level)
		{
			this.EnableEvents(eventSource, level, EventKeywords.None);
		}

		/// <summary>Enables events for the specified event source that has the specified verbosity level or lower, and matching keyword flags.</summary>
		/// <param name="eventSource">The event source to enable events for.</param>
		/// <param name="level">The level of events to enable.</param>
		/// <param name="matchAnyKeyword">The keyword flags necessary to enable the events.</param>
		// Token: 0x06003511 RID: 13585 RVA: 0x000CF530 File Offset: 0x000CD730
		[__DynamicallyInvokable]
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.EnableEvents(eventSource, level, matchAnyKeyword, null);
		}

		/// <summary>Enables events for the specified event source that has the specified verbosity level or lower, matching event keyword flag, and matching arguments.</summary>
		/// <param name="eventSource">The event source to enable events for.</param>
		/// <param name="level">The level of events to enable.</param>
		/// <param name="matchAnyKeyword">The keyword flags necessary to enable the events.</param>
		/// <param name="arguments">The arguments to be matched to enable the events.</param>
		// Token: 0x06003512 RID: 13586 RVA: 0x000CF53C File Offset: 0x000CD73C
		[__DynamicallyInvokable]
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> arguments)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			eventSource.SendCommand(this, 0, 0, EventCommand.Update, true, level, matchAnyKeyword, arguments);
		}

		/// <summary>Disables all events for the specified event source.</summary>
		/// <param name="eventSource">The event source to disable events for.</param>
		// Token: 0x06003513 RID: 13587 RVA: 0x000CF568 File Offset: 0x000CD768
		[__DynamicallyInvokable]
		public void DisableEvents(EventSource eventSource)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			eventSource.SendCommand(this, 0, 0, EventCommand.Update, false, EventLevel.LogAlways, EventKeywords.None, null);
		}

		/// <summary>Gets a small non-negative number that represents the specified event source.</summary>
		/// <param name="eventSource">The event source to find the index for.</param>
		/// <returns>A small non-negative number that represents the specified event source.</returns>
		// Token: 0x06003514 RID: 13588 RVA: 0x000CF592 File Offset: 0x000CD792
		[__DynamicallyInvokable]
		public static int EventSourceIndex(EventSource eventSource)
		{
			return eventSource.m_id;
		}

		/// <summary>Called for all existing event sources when the event listener is created and when a new event source is attached to the listener.</summary>
		/// <param name="eventSource">The event source.</param>
		// Token: 0x06003515 RID: 13589 RVA: 0x000CF59C File Offset: 0x000CD79C
		[__DynamicallyInvokable]
		protected internal virtual void OnEventSourceCreated(EventSource eventSource)
		{
			EventHandler<EventSourceCreatedEventArgs> eventSourceCreated = this._EventSourceCreated;
			if (eventSourceCreated != null)
			{
				eventSourceCreated(this, new EventSourceCreatedEventArgs
				{
					EventSource = eventSource
				});
			}
		}

		/// <summary>Called whenever an event has been written by an event source for which the event listener has enabled events.</summary>
		/// <param name="eventData">The event arguments that describe the event.</param>
		// Token: 0x06003516 RID: 13590 RVA: 0x000CF5C8 File Offset: 0x000CD7C8
		[__DynamicallyInvokable]
		protected internal virtual void OnEventWritten(EventWrittenEventArgs eventData)
		{
			EventHandler<EventWrittenEventArgs> eventWritten = this.EventWritten;
			if (eventWritten != null)
			{
				eventWritten(this, eventData);
			}
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000CF5E8 File Offset: 0x000CD7E8
		internal static void AddEventSource(EventSource newEventSource)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_EventSources == null)
				{
					EventListener.s_EventSources = new List<WeakReference>(2);
				}
				if (!EventListener.s_EventSourceShutdownRegistered)
				{
					EventListener.s_EventSourceShutdownRegistered = true;
					AppDomain.CurrentDomain.ProcessExit += EventListener.DisposeOnShutdown;
					AppDomain.CurrentDomain.DomainUnload += EventListener.DisposeOnShutdown;
				}
				int num = -1;
				if (EventListener.s_EventSources.Count % 64 == 63)
				{
					int num2 = EventListener.s_EventSources.Count;
					while (0 < num2)
					{
						num2--;
						WeakReference weakReference = EventListener.s_EventSources[num2];
						if (!weakReference.IsAlive)
						{
							num = num2;
							weakReference.Target = newEventSource;
							break;
						}
					}
				}
				if (num < 0)
				{
					num = EventListener.s_EventSources.Count;
					EventListener.s_EventSources.Add(new WeakReference(newEventSource));
				}
				newEventSource.m_id = num;
				for (EventListener next = EventListener.s_Listeners; next != null; next = next.m_Next)
				{
					newEventSource.AddListener(next);
				}
			}
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000CF6FC File Offset: 0x000CD8FC
		private static void DisposeOnShutdown(object sender, EventArgs e)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						eventSource.Dispose();
					}
				}
			}
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x000CF788 File Offset: 0x000CD988
		private static void RemoveReferencesToListenerInEventSources(EventListener listenerToRemove)
		{
			using (List<WeakReference>.Enumerator enumerator = EventListener.s_EventSources.GetEnumerator())
			{
				IL_7E:
				while (enumerator.MoveNext())
				{
					WeakReference weakReference = enumerator.Current;
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						if (eventSource.m_Dispatchers.m_Listener == listenerToRemove)
						{
							eventSource.m_Dispatchers = eventSource.m_Dispatchers.m_Next;
						}
						else
						{
							EventDispatcher eventDispatcher = eventSource.m_Dispatchers;
							EventDispatcher next;
							for (;;)
							{
								next = eventDispatcher.m_Next;
								if (next == null)
								{
									goto IL_7E;
								}
								if (next.m_Listener == listenerToRemove)
								{
									break;
								}
								eventDispatcher = next;
							}
							eventDispatcher.m_Next = next.m_Next;
						}
					}
				}
			}
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x000CF83C File Offset: 0x000CDA3C
		[Conditional("DEBUG")]
		internal static void Validate()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				Dictionary<EventListener, bool> dictionary = new Dictionary<EventListener, bool>();
				for (EventListener next = EventListener.s_Listeners; next != null; next = next.m_Next)
				{
					dictionary.Add(next, true);
				}
				int num = -1;
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					num++;
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						for (EventDispatcher eventDispatcher = eventSource.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
						{
						}
						foreach (EventListener eventListener in dictionary.Keys)
						{
							EventDispatcher eventDispatcher = eventSource.m_Dispatchers;
							while (eventDispatcher.m_Listener != eventListener)
							{
								eventDispatcher = eventDispatcher.m_Next;
							}
						}
					}
				}
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000CF96C File Offset: 0x000CDB6C
		internal static object EventListenersLock
		{
			get
			{
				if (EventListener.s_EventSources == null)
				{
					Interlocked.CompareExchange<List<WeakReference>>(ref EventListener.s_EventSources, new List<WeakReference>(2), null);
				}
				return EventListener.s_EventSources;
			}
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000CF98C File Offset: 0x000CDB8C
		private void CallBackForExistingEventSources(bool addToListenersList, EventHandler<EventSourceCreatedEventArgs> callback)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_CreatingListener)
				{
					throw new InvalidOperationException(Environment.GetResourceString("EventSource_ListenerCreatedInsideCallback"));
				}
				try
				{
					EventListener.s_CreatingListener = true;
					if (addToListenersList)
					{
						this.m_Next = EventListener.s_Listeners;
						EventListener.s_Listeners = this;
					}
					foreach (WeakReference weakReference in EventListener.s_EventSources.ToArray())
					{
						EventSource eventSource = weakReference.Target as EventSource;
						if (eventSource != null)
						{
							callback(this, new EventSourceCreatedEventArgs
							{
								EventSource = eventSource
							});
						}
					}
				}
				finally
				{
					EventListener.s_CreatingListener = false;
				}
			}
		}

		// Token: 0x0400177D RID: 6013
		private static readonly object s_EventSourceCreatedLock = new object();

		// Token: 0x04001780 RID: 6016
		internal volatile EventListener m_Next;

		// Token: 0x04001781 RID: 6017
		internal ActivityFilter m_activityFilter;

		// Token: 0x04001782 RID: 6018
		internal static EventListener s_Listeners;

		// Token: 0x04001783 RID: 6019
		internal static List<WeakReference> s_EventSources;

		// Token: 0x04001784 RID: 6020
		private static bool s_CreatingListener = false;

		// Token: 0x04001785 RID: 6021
		private static bool s_EventSourceShutdownRegistered = false;
	}
}
