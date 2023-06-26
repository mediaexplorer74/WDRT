using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Stores mappings between delegates and event tokens, to support the implementation of a Windows Runtime event in managed code.</summary>
	/// <typeparam name="T">The type of the event handler delegate for a particular event.</typeparam>
	// Token: 0x020009DF RID: 2527
	[__DynamicallyInvokable]
	public sealed class EventRegistrationTokenTable<T> where T : class
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationTokenTable`1" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="T" /> is not a delegate type.</exception>
		// Token: 0x0600648D RID: 25741 RVA: 0x00157E44 File Offset: 0x00156044
		[__DynamicallyInvokable]
		public EventRegistrationTokenTable()
		{
			if (!typeof(Delegate).IsAssignableFrom(typeof(T)))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EventTokenTableRequiresDelegate", new object[] { typeof(T) }));
			}
		}

		/// <summary>Gets or sets a delegate of type <paramref name="T" /> whose invocation list includes all the event handler delegates that have been added, and that have not yet been removed. Invoking this delegate invokes all the event handlers.</summary>
		/// <returns>A delegate of type <paramref name="T" /> that represents all the event handler delegates that are currently registered for an event.</returns>
		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x0600648E RID: 25742 RVA: 0x00157EA0 File Offset: 0x001560A0
		// (set) Token: 0x0600648F RID: 25743 RVA: 0x00157EAC File Offset: 0x001560AC
		[__DynamicallyInvokable]
		public T InvocationList
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_invokeList;
			}
			[__DynamicallyInvokable]
			set
			{
				Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
				lock (tokens)
				{
					this.m_tokens.Clear();
					this.m_invokeList = default(T);
					if (value != null)
					{
						this.AddEventHandlerNoLock(value);
					}
				}
			}
		}

		/// <summary>Adds the specified event handler to the table and to the invocation list, and returns a token that can be used to remove the event handler.</summary>
		/// <param name="handler">The event handler to add.</param>
		/// <returns>A token that can be used to remove the event handler from the table and the invocation list.</returns>
		// Token: 0x06006490 RID: 25744 RVA: 0x00157F14 File Offset: 0x00156114
		[__DynamicallyInvokable]
		public EventRegistrationToken AddEventHandler(T handler)
		{
			if (handler == null)
			{
				return new EventRegistrationToken(0UL);
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			EventRegistrationToken eventRegistrationToken;
			lock (tokens)
			{
				eventRegistrationToken = this.AddEventHandlerNoLock(handler);
			}
			return eventRegistrationToken;
		}

		// Token: 0x06006491 RID: 25745 RVA: 0x00157F68 File Offset: 0x00156168
		private EventRegistrationToken AddEventHandlerNoLock(T handler)
		{
			EventRegistrationToken preferredToken = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
			while (this.m_tokens.ContainsKey(preferredToken))
			{
				preferredToken = new EventRegistrationToken(preferredToken.Value + 1UL);
			}
			this.m_tokens[preferredToken] = handler;
			Delegate @delegate = (Delegate)((object)this.m_invokeList);
			@delegate = Delegate.Combine(@delegate, (Delegate)((object)handler));
			this.m_invokeList = (T)((object)@delegate);
			return preferredToken;
		}

		// Token: 0x06006492 RID: 25746 RVA: 0x00157FE0 File Offset: 0x001561E0
		[FriendAccessAllowed]
		internal T ExtractHandler(EventRegistrationToken token)
		{
			T t = default(T);
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				if (this.m_tokens.TryGetValue(token, out t))
				{
					this.RemoveEventHandlerNoLock(token);
				}
			}
			return t;
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x0015803C File Offset: 0x0015623C
		private static EventRegistrationToken GetPreferredToken(T handler)
		{
			Delegate[] invocationList = ((Delegate)((object)handler)).GetInvocationList();
			uint num;
			if (invocationList.Length == 1)
			{
				num = (uint)invocationList[0].Method.GetHashCode();
			}
			else
			{
				num = (uint)handler.GetHashCode();
			}
			ulong num2 = ((ulong)typeof(T).MetadataToken << 32) | (ulong)num;
			return new EventRegistrationToken(num2);
		}

		/// <summary>Removes the event handler that is associated with the specified token from the table and the invocation list.</summary>
		/// <param name="token">The token that was returned when the event handler was added.</param>
		// Token: 0x06006494 RID: 25748 RVA: 0x0015809C File Offset: 0x0015629C
		[__DynamicallyInvokable]
		public void RemoveEventHandler(EventRegistrationToken token)
		{
			if (token.Value == 0UL)
			{
				return;
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				this.RemoveEventHandlerNoLock(token);
			}
		}

		/// <summary>Removes the specified event handler delegate from the table and the invocation list.</summary>
		/// <param name="handler">The event handler to remove.</param>
		// Token: 0x06006495 RID: 25749 RVA: 0x001580E8 File Offset: 0x001562E8
		[__DynamicallyInvokable]
		public void RemoveEventHandler(T handler)
		{
			if (handler == null)
			{
				return;
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				EventRegistrationToken preferredToken = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
				T t;
				if (this.m_tokens.TryGetValue(preferredToken, out t) && t == handler)
				{
					this.RemoveEventHandlerNoLock(preferredToken);
				}
				else
				{
					foreach (KeyValuePair<EventRegistrationToken, T> keyValuePair in this.m_tokens)
					{
						if (keyValuePair.Value == (T)((object)handler))
						{
							this.RemoveEventHandlerNoLock(keyValuePair.Key);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x001581C4 File Offset: 0x001563C4
		private void RemoveEventHandlerNoLock(EventRegistrationToken token)
		{
			T t;
			if (this.m_tokens.TryGetValue(token, out t))
			{
				this.m_tokens.Remove(token);
				Delegate @delegate = (Delegate)((object)this.m_invokeList);
				@delegate = Delegate.Remove(@delegate, (Delegate)((object)t));
				this.m_invokeList = (T)((object)@delegate);
			}
		}

		/// <summary>Returns the specified event registration token table, if it is not <see langword="null" />; otherwise, returns a new event registration token table.</summary>
		/// <param name="refEventTable">An event registration token table, passed by reference.</param>
		/// <returns>The event registration token table that is specified by <paramref name="refEventTable" />, if it is not <see langword="null" />; otherwise, a new event registration token table.</returns>
		// Token: 0x06006497 RID: 25751 RVA: 0x00158221 File Offset: 0x00156421
		[__DynamicallyInvokable]
		public static EventRegistrationTokenTable<T> GetOrCreateEventRegistrationTokenTable(ref EventRegistrationTokenTable<T> refEventTable)
		{
			if (refEventTable == null)
			{
				Interlocked.CompareExchange<EventRegistrationTokenTable<T>>(ref refEventTable, new EventRegistrationTokenTable<T>(), null);
			}
			return refEventTable;
		}

		// Token: 0x04002CF8 RID: 11512
		private Dictionary<EventRegistrationToken, T> m_tokens = new Dictionary<EventRegistrationToken, T>();

		// Token: 0x04002CF9 RID: 11513
		private volatile T m_invokeList;
	}
}
