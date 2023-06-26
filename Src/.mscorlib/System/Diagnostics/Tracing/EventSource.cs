using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Reflection;
using Microsoft.Win32;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides the ability to create events for event tracing for Windows (ETW).</summary>
	// Token: 0x0200041E RID: 1054
	[__DynamicallyInvokable]
	public class EventSource : IDisposable
	{
		/// <summary>The friendly name of the class that is derived from the event source.</summary>
		/// <returns>The friendly name of the derived class.  The default is the simple name of the class.</returns>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000C9D7F File Offset: 0x000C7F7F
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_name;
			}
		}

		/// <summary>The unique identifier for the event source.</summary>
		/// <returns>A unique identifier for the event source.</returns>
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x000C9D87 File Offset: 0x000C7F87
		[__DynamicallyInvokable]
		public Guid Guid
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_guid;
			}
		}

		/// <summary>Determines whether the current event source is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the current event source is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600348F RID: 13455 RVA: 0x000C9D8F File Offset: 0x000C7F8F
		[__DynamicallyInvokable]
		public bool IsEnabled()
		{
			return this.m_eventSourceEnabled;
		}

		/// <summary>Determines whether the current event source that has the specified level and keyword is enabled.</summary>
		/// <param name="level">The level of the event source.</param>
		/// <param name="keywords">The keyword of the event source.</param>
		/// <returns>
		///   <see langword="true" /> if the event source is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003490 RID: 13456 RVA: 0x000C9D97 File Offset: 0x000C7F97
		[__DynamicallyInvokable]
		public bool IsEnabled(EventLevel level, EventKeywords keywords)
		{
			return this.IsEnabled(level, keywords, EventChannel.None);
		}

		/// <summary>Determines whether the current event source is enabled for events with the specified level, keywords and channel.</summary>
		/// <param name="level">The event level to check. An event source will be considered enabled when its level is greater than or equal to <paramref name="level" />.</param>
		/// <param name="keywords">The event keywords to check.</param>
		/// <param name="channel">The event channel to check.</param>
		/// <returns>
		///   <see langword="true" /> if the event source is enabled for the specified event level, keywords and channel; otherwise, <see langword="false" />.  
		/// The result of this method is only an approximation of whether a particular event is active.  Use it to avoid expensive computation for logging when logging is disabled. Event sources may have additional filtering that determines their activity.</returns>
		// Token: 0x06003491 RID: 13457 RVA: 0x000C9DA2 File Offset: 0x000C7FA2
		[__DynamicallyInvokable]
		public bool IsEnabled(EventLevel level, EventKeywords keywords, EventChannel channel)
		{
			return this.m_eventSourceEnabled && this.IsEnabledCommon(this.m_eventSourceEnabled, this.m_level, this.m_matchAnyKeyword, level, keywords, channel);
		}

		/// <summary>Gets the settings applied to this event source.</summary>
		/// <returns>The settings applied to this event source.</returns>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x000C9DCE File Offset: 0x000C7FCE
		[__DynamicallyInvokable]
		public EventSourceSettings Settings
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_config;
			}
		}

		/// <summary>Gets the unique identifier for this implementation of the event source.</summary>
		/// <param name="eventSourceType">The type of the event source.</param>
		/// <returns>A unique identifier for this event source type.</returns>
		// Token: 0x06003493 RID: 13459 RVA: 0x000C9DD8 File Offset: 0x000C7FD8
		[__DynamicallyInvokable]
		public static Guid GetGuid(Type eventSourceType)
		{
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), EventManifestOptions.None);
			string text = eventSourceType.Name;
			if (eventSourceAttribute != null)
			{
				if (eventSourceAttribute.Guid != null)
				{
					Guid empty = Guid.Empty;
					if (Guid.TryParse(eventSourceAttribute.Guid, out empty))
					{
						return empty;
					}
				}
				if (eventSourceAttribute.Name != null)
				{
					text = eventSourceAttribute.Name;
				}
			}
			if (text == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeName"), "eventSourceType");
			}
			return EventSource.GenerateGuidFromName(text.ToUpperInvariant());
		}

		/// <summary>Gets the friendly name of the event source.</summary>
		/// <param name="eventSourceType">The type of the event source.</param>
		/// <returns>The friendly name of the event source. The default is the simple name of the class.</returns>
		// Token: 0x06003494 RID: 13460 RVA: 0x000C9E6B File Offset: 0x000C806B
		[__DynamicallyInvokable]
		public static string GetName(Type eventSourceType)
		{
			return EventSource.GetName(eventSourceType, EventManifestOptions.None);
		}

		/// <summary>Returns a string of the XML manifest that is associated with the current event source.</summary>
		/// <param name="eventSourceType">The type of the event source.</param>
		/// <param name="assemblyPathToIncludeInManifest">The path to the assembly file (.dll) to include in the provider element of the manifest.</param>
		/// <returns>The XML data string.</returns>
		// Token: 0x06003495 RID: 13461 RVA: 0x000C9E74 File Offset: 0x000C8074
		[__DynamicallyInvokable]
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest)
		{
			return EventSource.GenerateManifest(eventSourceType, assemblyPathToIncludeInManifest, EventManifestOptions.None);
		}

		/// <summary>Returns a string of the XML manifest that is associated with the current event source.</summary>
		/// <param name="eventSourceType">The type of the event source.</param>
		/// <param name="assemblyPathToIncludeInManifest">The path to the assembly file (.dll) file to include in the provider element of the manifest.</param>
		/// <param name="flags">A bitwise combination of the enumeration values that specify how the manifest is generated.</param>
		/// <returns>The XML data string or <see langword="null" />.</returns>
		// Token: 0x06003496 RID: 13462 RVA: 0x000C9E80 File Offset: 0x000C8080
		[__DynamicallyInvokable]
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest, EventManifestOptions flags)
		{
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			byte[] array = EventSource.CreateManifestAndDescriptors(eventSourceType, assemblyPathToIncludeInManifest, null, flags);
			if (array != null)
			{
				return Encoding.UTF8.GetString(array, 0, array.Length);
			}
			return null;
		}

		/// <summary>Gets a snapshot of all the event sources for the application domain.</summary>
		/// <returns>An enumeration of all the event sources in the application domain.</returns>
		// Token: 0x06003497 RID: 13463 RVA: 0x000C9EC0 File Offset: 0x000C80C0
		[__DynamicallyInvokable]
		public static IEnumerable<EventSource> GetSources()
		{
			List<EventSource> list = new List<EventSource>();
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null && !eventSource.IsDisposed)
					{
						list.Add(eventSource);
					}
				}
			}
			return list;
		}

		/// <summary>Sends a command to a specified event source.</summary>
		/// <param name="eventSource">The event source to send the command to.</param>
		/// <param name="command">The event command to send.</param>
		/// <param name="commandArguments">The arguments for the event command.</param>
		// Token: 0x06003498 RID: 13464 RVA: 0x000C9F5C File Offset: 0x000C815C
		[__DynamicallyInvokable]
		public static void SendCommand(EventSource eventSource, EventCommand command, IDictionary<string, string> commandArguments)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			if (command <= EventCommand.Update && command != EventCommand.SendManifest)
			{
				throw new ArgumentException(Environment.GetResourceString("EventSource_InvalidCommand"), "command");
			}
			eventSource.SendCommand(null, 0, 0, command, true, EventLevel.LogAlways, EventKeywords.None, commandArguments);
		}

		/// <summary>Sets the activity ID on the current thread.</summary>
		/// <param name="activityId">The current thread's new activity ID, or <see cref="F:System.Guid.Empty" /> to indicate that work on the current thread is not associated with any activity.</param>
		// Token: 0x06003499 RID: 13465 RVA: 0x000C9FA4 File Offset: 0x000C81A4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void SetCurrentThreadActivityId(Guid activityId)
		{
			Guid guid = activityId;
			if (UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_SET_ID, ref activityId) == 0)
			{
				Action<Guid> action = EventSource.s_activityDying;
				if (action != null && guid != activityId)
				{
					if (activityId == Guid.Empty)
					{
						activityId = EventSource.FallbackActivityId;
					}
					action(activityId);
				}
			}
			if (TplEtwProvider.Log != null)
			{
				TplEtwProvider.Log.SetActivityId(activityId);
			}
		}

		/// <summary>Sets the activity ID on the current thread, and returns the previous activity ID.</summary>
		/// <param name="activityId">The current thread's new activity ID, or <see cref="F:System.Guid.Empty" /> to indicate that work on the current thread is not associated with any activity.</param>
		/// <param name="oldActivityThatWillContinue">When this method returns, contains the previous activity ID on the current thread.</param>
		// Token: 0x0600349A RID: 13466 RVA: 0x000C9FFC File Offset: 0x000C81FC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void SetCurrentThreadActivityId(Guid activityId, out Guid oldActivityThatWillContinue)
		{
			oldActivityThatWillContinue = activityId;
			UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_SET_ID, ref oldActivityThatWillContinue);
			if (TplEtwProvider.Log != null)
			{
				TplEtwProvider.Log.SetActivityId(activityId);
			}
		}

		/// <summary>Gets the activity ID of the current thread.</summary>
		/// <returns>The activity ID of the current thread.</returns>
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000CA020 File Offset: 0x000C8220
		[__DynamicallyInvokable]
		public static Guid CurrentThreadActivityId
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				Guid guid = default(Guid);
				UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_ID, ref guid);
				return guid;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000CA040 File Offset: 0x000C8240
		internal static Guid InternalCurrentThreadActivityId
		{
			[SecurityCritical]
			get
			{
				Guid guid = EventSource.CurrentThreadActivityId;
				if (guid == Guid.Empty)
				{
					guid = EventSource.FallbackActivityId;
				}
				return guid;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x000CA068 File Offset: 0x000C8268
		internal static Guid FallbackActivityId
		{
			[SecurityCritical]
			get
			{
				return new Guid((uint)AppDomain.GetCurrentThreadId(), (ushort)EventSource.s_currentPid, (ushort)(EventSource.s_currentPid >> 16), 148, 27, 135, 213, 166, 92, 54, 100);
			}
		}

		/// <summary>Gets any exception that was thrown during the construction of the event source.</summary>
		/// <returns>The exception that was thrown during the construction of the event source, or <see langword="null" /> if no exception was thrown.</returns>
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x000CA0AA File Offset: 0x000C82AA
		[__DynamicallyInvokable]
		public Exception ConstructionException
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_constructionException;
			}
		}

		/// <summary>Gets the trait value associated with the specified key.</summary>
		/// <param name="key">The key of the trait to get.</param>
		/// <returns>The trait value associated with the specified key. If the key is not found, returns <see langword="null" />.</returns>
		// Token: 0x0600349F RID: 13471 RVA: 0x000CA0B4 File Offset: 0x000C82B4
		[__DynamicallyInvokable]
		public string GetTrait(string key)
		{
			if (this.m_traits != null)
			{
				for (int i = 0; i < this.m_traits.Length - 1; i += 2)
				{
					if (this.m_traits[i] == key)
					{
						return this.m_traits[i + 1];
					}
				}
			}
			return null;
		}

		/// <summary>Obtains a string representation of the current event source instance.</summary>
		/// <returns>The name and unique identifier that identify the current event source.</returns>
		// Token: 0x060034A0 RID: 13472 RVA: 0x000CA0FA File Offset: 0x000C82FA
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Environment.GetResourceString("EventSource_ToString", new object[] { this.Name, this.Guid });
		}

		/// <summary>Occurs when a command comes from an event listener.</summary>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060034A1 RID: 13473 RVA: 0x000CA124 File Offset: 0x000C8324
		// (remove) Token: 0x060034A2 RID: 13474 RVA: 0x000CA163 File Offset: 0x000C8363
		public event EventHandler<EventCommandEventArgs> EventCommandExecuted
		{
			add
			{
				this.m_eventCommandExecuted = (EventHandler<EventCommandEventArgs>)Delegate.Combine(this.m_eventCommandExecuted, value);
				for (EventCommandEventArgs eventCommandEventArgs = this.m_deferredCommands; eventCommandEventArgs != null; eventCommandEventArgs = eventCommandEventArgs.nextCommand)
				{
					value(this, eventCommandEventArgs);
				}
			}
			remove
			{
				this.m_eventCommandExecuted = (EventHandler<EventCommandEventArgs>)Delegate.Remove(this.m_eventCommandExecuted, value);
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class.</summary>
		// Token: 0x060034A3 RID: 13475 RVA: 0x000CA17C File Offset: 0x000C837C
		[__DynamicallyInvokable]
		protected EventSource()
			: this(EventSourceSettings.EtwManifestEventFormat)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class and specifies whether to throw an exception when an error occurs in the underlying Windows code.</summary>
		/// <param name="throwOnEventWriteErrors">
		///   <see langword="true" /> to throw an exception when an error occurs in the underlying Windows code; otherwise, <see langword="false" />.</param>
		// Token: 0x060034A4 RID: 13476 RVA: 0x000CA185 File Offset: 0x000C8385
		[__DynamicallyInvokable]
		protected EventSource(bool throwOnEventWriteErrors)
			: this(EventSourceSettings.EtwManifestEventFormat | (throwOnEventWriteErrors ? EventSourceSettings.ThrowOnEventWriteErrors : EventSourceSettings.Default))
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class with the specified configuration settings.</summary>
		/// <param name="settings">A bitwise combination of the enumeration values that specify the configuration settings to apply to the event source.</param>
		// Token: 0x060034A5 RID: 13477 RVA: 0x000CA196 File Offset: 0x000C8396
		[__DynamicallyInvokable]
		protected EventSource(EventSourceSettings settings)
			: this(settings, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> to be used with non-contract events that contains the specified settings and traits.</summary>
		/// <param name="settings">A bitwise combination of the enumeration values that specify the configuration settings to apply to the event source.</param>
		/// <param name="traits">The key-value pairs that specify traits for the event source.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="traits" /> is not specified in key-value pairs.</exception>
		// Token: 0x060034A6 RID: 13478 RVA: 0x000CA1A0 File Offset: 0x000C83A0
		[__DynamicallyInvokable]
		protected EventSource(EventSourceSettings settings, params string[] traits)
		{
			this.m_config = this.ValidateSettings(settings);
			Type type = base.GetType();
			this.Initialize(EventSource.GetGuid(type), EventSource.GetName(type), traits);
		}

		/// <summary>Called when the current event source is updated by the controller.</summary>
		/// <param name="command">The arguments for the event.</param>
		// Token: 0x060034A7 RID: 13479 RVA: 0x000CA1DA File Offset: 0x000C83DA
		[__DynamicallyInvokable]
		protected virtual void OnEventCommand(EventCommandEventArgs command)
		{
		}

		/// <summary>Writes an event by using the provided event identifier.</summary>
		/// <param name="eventId">The event identifier. This value should be between 0 and 65535.</param>
		// Token: 0x060034A8 RID: 13480 RVA: 0x000CA1DC File Offset: 0x000C83DC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected void WriteEvent(int eventId)
		{
			this.WriteEventCore(eventId, 0, null);
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer argument.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">An integer argument.</param>
		// Token: 0x060034A9 RID: 13481 RVA: 0x000CA1E8 File Offset: 0x000C83E8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, int arg1)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				this.WriteEventCore(eventId, 1, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">An integer argument.</param>
		/// <param name="arg2">An integer argument.</param>
		// Token: 0x060034AA RID: 13482 RVA: 0x000CA228 File Offset: 0x000C8428
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, int arg1, int arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				this.WriteEventCore(eventId, 2, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">An integer argument.</param>
		/// <param name="arg2">An integer argument.</param>
		/// <param name="arg3">An integer argument.</param>
		// Token: 0x060034AB RID: 13483 RVA: 0x000CA28C File Offset: 0x000C848C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, int arg1, int arg2, int arg3)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 4;
				this.WriteEventCore(eventId, 3, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit integer argument.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		// Token: 0x060034AC RID: 13484 RVA: 0x000CA318 File Offset: 0x000C8518
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, long arg1)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				this.WriteEventCore(eventId, 1, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		// Token: 0x060034AD RID: 13485 RVA: 0x000CA358 File Offset: 0x000C8558
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, long arg1, long arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 8;
				this.WriteEventCore(eventId, 2, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		/// <param name="arg3">A 64 bit integer argument.</param>
		// Token: 0x060034AE RID: 13486 RVA: 0x000CA3BC File Offset: 0x000C85BC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, long arg1, long arg2, long arg3)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 8;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 8;
				this.WriteEventCore(eventId, 3, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and string argument.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		// Token: 0x060034AF RID: 13487 RVA: 0x000CA448 File Offset: 0x000C8648
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, string arg1)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					this.WriteEventCore(eventId, 1, ptr2);
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and string arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A string argument.</param>
		// Token: 0x060034B0 RID: 13488 RVA: 0x000CA4AC File Offset: 0x000C86AC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, string arg1, string arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						EventSource.EventData* ptr3;
						checked
						{
							ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
							ptr3->DataPointer = (IntPtr)((void*)ptr);
						}
						ptr3->Size = (arg1.Length + 1) * 2;
						ptr3[1].DataPointer = (IntPtr)((void*)ptr2);
						ptr3[1].Size = (arg2.Length + 1) * 2;
						this.WriteEventCore(eventId, 2, ptr3);
					}
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and string arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A string argument.</param>
		/// <param name="arg3">A string argument.</param>
		// Token: 0x060034B1 RID: 13489 RVA: 0x000CA560 File Offset: 0x000C8760
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text3 = arg3)
						{
							char* ptr3 = text3;
							if (ptr3 != null)
							{
								ptr3 += RuntimeHelpers.OffsetToStringData / 2;
							}
							EventSource.EventData* ptr4;
							checked
							{
								ptr4 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
								ptr4->DataPointer = (IntPtr)((void*)ptr);
							}
							ptr4->Size = (arg1.Length + 1) * 2;
							ptr4[1].DataPointer = (IntPtr)((void*)ptr2);
							ptr4[1].Size = (arg2.Length + 1) * 2;
							ptr4[2].DataPointer = (IntPtr)((void*)ptr3);
							ptr4[2].Size = (arg3.Length + 1) * 2;
							this.WriteEventCore(eventId, 3, ptr4);
						}
					}
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 32 bit integer argument.</param>
		// Token: 0x060034B2 RID: 13490 RVA: 0x000CA66C File Offset: 0x000C886C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, string arg1, int arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 4;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 32 bit integer argument.</param>
		/// <param name="arg3">A 32 bit integer argument.</param>
		// Token: 0x060034B3 RID: 13491 RVA: 0x000CA6F4 File Offset: 0x000C88F4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, string arg1, int arg2, int arg3)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 4;
					ptr2[2].DataPointer = (IntPtr)((void*)(&arg3));
					ptr2[2].Size = 4;
					this.WriteEventCore(eventId, 3, ptr2);
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		// Token: 0x060034B4 RID: 13492 RVA: 0x000CA7A8 File Offset: 0x000C89A8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, string arg1, long arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 8;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit integer, and string arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 64-bit integer argument.</param>
		/// <param name="arg2">A string argument.</param>
		// Token: 0x060034B5 RID: 13493 RVA: 0x000CA830 File Offset: 0x000C8A30
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, long arg1, string arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg2)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 8;
					ptr2[1].DataPointer = (IntPtr)((void*)ptr);
					ptr2[1].Size = (arg2.Length + 1) * 2;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer and string arguments.</summary>
		/// <param name="eventId">The event identifier. This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 32-bit integer argument.</param>
		/// <param name="arg2">A string argument.</param>
		// Token: 0x060034B6 RID: 13494 RVA: 0x000CA8B8 File Offset: 0x000C8AB8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, int arg1, string arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg2)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 4;
					ptr2[1].DataPointer = (IntPtr)((void*)ptr);
					ptr2[1].Size = (arg2.Length + 1) * 2;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and byte array argument.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A byte array argument.</param>
		// Token: 0x060034B7 RID: 13495 RVA: 0x000CA940 File Offset: 0x000C8B40
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, byte[] arg1)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = new byte[0];
				}
				int num = arg1.Length;
				fixed (byte* ptr = &arg1[0])
				{
					byte* ptr2 = ptr;
					EventSource.EventData* ptr3;
					checked
					{
						ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr3->DataPointer = (IntPtr)((void*)(&num));
					ptr3->Size = 4;
					ptr3[1].DataPointer = (IntPtr)((void*)ptr2);
					ptr3[1].Size = num;
					this.WriteEventCore(eventId, 2, ptr3);
				}
			}
		}

		/// <summary>Writes the event data using the specified indentifier and 64-bit integer and byte array arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 64-bit integer argument.</param>
		/// <param name="arg2">A byte array argument.</param>
		// Token: 0x060034B8 RID: 13496 RVA: 0x000CA9C0 File Offset: 0x000C8BC0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEvent(int eventId, long arg1, byte[] arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg2 == null)
				{
					arg2 = new byte[0];
				}
				int num = arg2.Length;
				fixed (byte* ptr = &arg2[0])
				{
					byte* ptr2 = ptr;
					EventSource.EventData* ptr3;
					checked
					{
						ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr3->DataPointer = (IntPtr)((void*)(&arg1));
					ptr3->Size = 8;
					ptr3[1].DataPointer = (IntPtr)((void*)(&num));
					ptr3[1].Size = 4;
					ptr3[2].DataPointer = (IntPtr)((void*)ptr2);
					ptr3[2].Size = num;
					this.WriteEventCore(eventId, 3, ptr3);
				}
			}
		}

		/// <summary>Creates a new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload by using the provided event identifier and event data.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="eventDataCount">The number of event data items.</param>
		/// <param name="data">The structure that contains the event data.</param>
		// Token: 0x060034B9 RID: 13497 RVA: 0x000CAA69 File Offset: 0x000C8C69
		[SecurityCritical]
		[CLSCompliant(false)]
		protected unsafe void WriteEventCore(int eventId, int eventDataCount, EventSource.EventData* data)
		{
			this.WriteEventWithRelatedActivityIdCore(eventId, null, eventDataCount, data);
		}

		/// <summary>Writes an event that indicates that the current activity is related to another activity.</summary>
		/// <param name="eventId">An identifier that uniquely identifies this event within the <see cref="T:System.Diagnostics.Tracing.EventSource" />.</param>
		/// <param name="relatedActivityId">A pointer to the GUID of the related activity ID.</param>
		/// <param name="eventDataCount">The number of items in the <paramref name="data" /> field.</param>
		/// <param name="data">A pointer to the first item in the event data field.</param>
		// Token: 0x060034BA RID: 13498 RVA: 0x000CAA78 File Offset: 0x000C8C78
		[SecurityCritical]
		[CLSCompliant(false)]
		protected unsafe void WriteEventWithRelatedActivityIdCore(int eventId, Guid* relatedActivityId, int eventDataCount, EventSource.EventData* data)
		{
			if (this.m_eventSourceEnabled)
			{
				try
				{
					if (relatedActivityId != null)
					{
						this.ValidateEventOpcodeForTransfer(ref this.m_eventData[eventId], this.m_eventData[eventId].Name);
					}
					EventOpcode opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode;
					EventActivityOptions activityOptions = this.m_eventData[eventId].ActivityOptions;
					Guid* ptr = null;
					Guid empty = Guid.Empty;
					Guid empty2 = Guid.Empty;
					if (opcode != EventOpcode.Info && relatedActivityId == null && (activityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
					{
						if (opcode == EventOpcode.Start)
						{
							this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty, ref empty2, this.m_eventData[eventId].ActivityOptions);
						}
						else if (opcode == EventOpcode.Stop)
						{
							this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty);
						}
						if (empty != Guid.Empty)
						{
							ptr = &empty;
						}
						if (empty2 != Guid.Empty)
						{
							relatedActivityId = &empty2;
						}
					}
					if (this.m_eventData[eventId].EnabledForETW)
					{
						SessionMask sessionMask = SessionMask.All;
						if ((ulong)this.m_curLiveSessions != 0UL)
						{
							sessionMask = this.GetEtwSessionMask(eventId, relatedActivityId);
						}
						if ((ulong)sessionMask != 0UL || (this.m_legacySessions != null && this.m_legacySessions.Count > 0))
						{
							if (!this.SelfDescribingEvents)
							{
								if (sessionMask.IsEqualOrSupersetOf(this.m_curLiveSessions))
								{
									if (!this.m_provider.WriteEvent(ref this.m_eventData[eventId].Descriptor, ptr, relatedActivityId, eventDataCount, (IntPtr)((void*)data)))
									{
										this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
									}
								}
								else
								{
									long num = this.m_eventData[eventId].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords());
									EventDescriptor eventDescriptor = new EventDescriptor(this.m_eventData[eventId].Descriptor.EventId, this.m_eventData[eventId].Descriptor.Version, this.m_eventData[eventId].Descriptor.Channel, this.m_eventData[eventId].Descriptor.Level, this.m_eventData[eventId].Descriptor.Opcode, this.m_eventData[eventId].Descriptor.Task, (long)(sessionMask.ToEventKeywords() | (ulong)num));
									if (!this.m_provider.WriteEvent(ref eventDescriptor, ptr, relatedActivityId, eventDataCount, (IntPtr)((void*)data)))
									{
										this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
									}
								}
							}
							else
							{
								TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
								if (traceLoggingEventTypes == null)
								{
									traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
									Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
								}
								long num2 = this.m_eventData[eventId].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords());
								EventSourceOptions eventSourceOptions = new EventSourceOptions
								{
									Keywords = (EventKeywords)(sessionMask.ToEventKeywords() | (ulong)num2),
									Level = (EventLevel)this.m_eventData[eventId].Descriptor.Level,
									Opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode
								};
								this.WriteMultiMerge(this.m_eventData[eventId].Name, ref eventSourceOptions, traceLoggingEventTypes, ptr, relatedActivityId, data);
							}
						}
					}
					if (this.m_Dispatchers != null && this.m_eventData[eventId].EnabledForAnyListener)
					{
						this.WriteToAllListeners(eventId, ptr, relatedActivityId, eventDataCount, data);
					}
				}
				catch (Exception ex)
				{
					if (ex is EventSourceException)
					{
						throw;
					}
					this.ThrowEventSourceException(this.m_eventData[eventId].Name, ex);
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and array of arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="args">An array of objects.</param>
		// Token: 0x060034BB RID: 13499 RVA: 0x000CAF18 File Offset: 0x000C9118
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected void WriteEvent(int eventId, params object[] args)
		{
			this.WriteEventVarargs(eventId, null, args);
		}

		/// <summary>Writes an event that indicates that the current activity is related to another activity.</summary>
		/// <param name="eventId">An identifier that uniquely identifies this event within the <see cref="T:System.Diagnostics.Tracing.EventSource" />.</param>
		/// <param name="relatedActivityId">The related activity identifier.</param>
		/// <param name="args">An array of objects that contain data about the event.</param>
		// Token: 0x060034BC RID: 13500 RVA: 0x000CAF24 File Offset: 0x000C9124
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected unsafe void WriteEventWithRelatedActivityId(int eventId, Guid relatedActivityId, params object[] args)
		{
			this.WriteEventVarargs(eventId, &relatedActivityId, args);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class.</summary>
		// Token: 0x060034BD RID: 13501 RVA: 0x000CAF31 File Offset: 0x000C9131
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060034BE RID: 13502 RVA: 0x000CAF40 File Offset: 0x000C9140
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_eventSourceEnabled)
				{
					try
					{
						this.SendManifest(this.m_rawManifest);
					}
					catch (Exception)
					{
					}
					this.m_eventSourceEnabled = false;
				}
				if (this.m_provider != null)
				{
					this.m_provider.Dispose();
					this.m_provider = null;
				}
			}
			this.m_eventSourceEnabled = false;
		}

		/// <summary>Allows the <see cref="T:System.Diagnostics.Tracing.EventSource" /> object to attempt to free resources and perform other cleanup operations before the  object is reclaimed by garbage collection.</summary>
		// Token: 0x060034BF RID: 13503 RVA: 0x000CAFAC File Offset: 0x000C91AC
		[__DynamicallyInvokable]
		~EventSource()
		{
			this.Dispose(false);
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000CAFDC File Offset: 0x000C91DC
		internal void WriteStringToListener(EventListener listener, string msg, SessionMask m)
		{
			if (this.m_eventSourceEnabled)
			{
				if (listener == null)
				{
					this.WriteEventString(EventLevel.LogAlways, (long)m.ToEventKeywords(), msg);
					return;
				}
				List<object> list = new List<object>();
				list.Add(msg);
				listener.OnEventWritten(new EventWrittenEventArgs(this)
				{
					EventId = 0,
					Payload = new ReadOnlyCollection<object>(list)
				});
			}
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000CB032 File Offset: 0x000C9232
		[SecurityCritical]
		private unsafe void WriteEventRaw(string eventName, ref EventDescriptor eventDescriptor, Guid* activityID, Guid* relatedActivityID, int dataCount, IntPtr data)
		{
			if (this.m_provider == null)
			{
				this.ThrowEventSourceException(eventName, null);
				return;
			}
			if (!this.m_provider.WriteEventRaw(ref eventDescriptor, activityID, relatedActivityID, dataCount, data))
			{
				this.ThrowEventSourceException(eventName, null);
			}
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000CB066 File Offset: 0x000C9266
		internal EventSource(Guid eventSourceGuid, string eventSourceName)
			: this(eventSourceGuid, eventSourceName, EventSourceSettings.EtwManifestEventFormat, null)
		{
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000CB072 File Offset: 0x000C9272
		internal EventSource(Guid eventSourceGuid, string eventSourceName, EventSourceSettings settings, string[] traits = null)
		{
			this.m_config = this.ValidateSettings(settings);
			this.Initialize(eventSourceGuid, eventSourceName, traits);
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000CB094 File Offset: 0x000C9294
		[SecuritySafeCritical]
		private unsafe void Initialize(Guid eventSourceGuid, string eventSourceName, string[] traits)
		{
			try
			{
				this.m_traits = traits;
				if (this.m_traits != null && this.m_traits.Length % 2 != 0)
				{
					throw new ArgumentException(Environment.GetResourceString("TraitEven"), "traits");
				}
				if (eventSourceGuid == Guid.Empty)
				{
					throw new ArgumentException(Environment.GetResourceString("EventSource_NeedGuid"));
				}
				if (eventSourceName == null)
				{
					throw new ArgumentException(Environment.GetResourceString("EventSource_NeedName"));
				}
				this.m_name = eventSourceName;
				this.m_guid = eventSourceGuid;
				this.m_curLiveSessions = new SessionMask(0U);
				this.m_etwSessionIdMap = new EtwSession[4];
				this.m_activityTracker = ActivityTracker.Instance;
				this.InitializeProviderMetadata();
				EventSource.OverideEventProvider overideEventProvider = new EventSource.OverideEventProvider(this);
				overideEventProvider.Register(eventSourceGuid);
				EventListener.AddEventSource(this);
				this.m_provider = overideEventProvider;
				int num = Environment.OSVersion.Version.Major * 10 + Environment.OSVersion.Version.Minor;
				if (this.Name != "System.Diagnostics.Eventing.FrameworkEventSource" || num >= 62)
				{
					try
					{
						byte[] array;
						void* ptr;
						if ((array = this.providerMetadata) == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = (void*)(&array[0]);
						}
						int num2 = this.m_provider.SetInformation(UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS.SetTraits, ptr, this.providerMetadata.Length);
					}
					finally
					{
						byte[] array = null;
					}
				}
				this.m_completelyInited = true;
			}
			catch (Exception ex)
			{
				if (this.m_constructionException == null)
				{
					this.m_constructionException = ex;
				}
				this.ReportOutOfBandMessage("ERROR: Exception during construction of EventSource " + this.Name + ": " + ex.Message, true);
			}
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				for (EventCommandEventArgs eventCommandEventArgs = this.m_deferredCommands; eventCommandEventArgs != null; eventCommandEventArgs = eventCommandEventArgs.nextCommand)
				{
					this.DoCommand(eventCommandEventArgs);
				}
			}
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000CB29C File Offset: 0x000C949C
		private static string GetName(Type eventSourceType, EventManifestOptions flags)
		{
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), flags);
			if (eventSourceAttribute != null && eventSourceAttribute.Name != null)
			{
				return eventSourceAttribute.Name;
			}
			return eventSourceType.Name;
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000CB2EC File Offset: 0x000C94EC
		private static Guid GenerateGuidFromName(string name)
		{
			byte[] bytes = Encoding.BigEndianUnicode.GetBytes(name);
			EventSource.Sha1ForNonSecretPurposes sha1ForNonSecretPurposes = default(EventSource.Sha1ForNonSecretPurposes);
			sha1ForNonSecretPurposes.Start();
			sha1ForNonSecretPurposes.Append(EventSource.namespaceBytes);
			sha1ForNonSecretPurposes.Append(bytes);
			Array.Resize<byte>(ref bytes, 16);
			sha1ForNonSecretPurposes.Finish(bytes);
			bytes[7] = (bytes[7] & 15) | 80;
			return new Guid(bytes);
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000CB34C File Offset: 0x000C954C
		[SecurityCritical]
		private unsafe object DecodeObject(int eventId, int parameterId, ref EventSource.EventData* data)
		{
			IntPtr intPtr = data.DataPointer;
			data += (IntPtr)sizeof(EventSource.EventData);
			Type type = this.m_eventData[eventId].Parameters[parameterId].ParameterType;
			while (!(type == typeof(IntPtr)))
			{
				if (type == typeof(int))
				{
					return *(int*)(void*)intPtr;
				}
				if (type == typeof(uint))
				{
					return *(uint*)(void*)intPtr;
				}
				if (type == typeof(long))
				{
					return *(long*)(void*)intPtr;
				}
				if (type == typeof(ulong))
				{
					return (ulong)(*(long*)(void*)intPtr);
				}
				if (type == typeof(byte))
				{
					return *(byte*)(void*)intPtr;
				}
				if (type == typeof(sbyte))
				{
					return *(sbyte*)(void*)intPtr;
				}
				if (type == typeof(short))
				{
					return *(short*)(void*)intPtr;
				}
				if (type == typeof(ushort))
				{
					return *(ushort*)(void*)intPtr;
				}
				if (type == typeof(float))
				{
					return *(float*)(void*)intPtr;
				}
				if (type == typeof(double))
				{
					return *(double*)(void*)intPtr;
				}
				if (type == typeof(decimal))
				{
					return *(decimal*)(void*)intPtr;
				}
				if (type == typeof(bool))
				{
					if (*(int*)(void*)intPtr == 1)
					{
						return true;
					}
					return false;
				}
				else
				{
					if (type == typeof(Guid))
					{
						return *(Guid*)(void*)intPtr;
					}
					if (type == typeof(char))
					{
						return (char)(*(ushort*)(void*)intPtr);
					}
					if (type == typeof(DateTime))
					{
						long num = *(long*)(void*)intPtr;
						return DateTime.FromFileTimeUtc(num);
					}
					if (type == typeof(byte[]))
					{
						int num2 = *(int*)(void*)intPtr;
						byte[] array = new byte[num2];
						intPtr = data.DataPointer;
						data += (IntPtr)sizeof(EventSource.EventData);
						for (int i = 0; i < num2; i++)
						{
							array[i] = ((byte*)(void*)intPtr)[i];
						}
						return array;
					}
					if (type == typeof(byte*))
					{
						return null;
					}
					if (!type.IsEnum())
					{
						return Marshal.PtrToStringUni(intPtr);
					}
					type = Enum.GetUnderlyingType(type);
				}
			}
			return *(IntPtr*)(void*)intPtr;
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000CB618 File Offset: 0x000C9818
		private EventDispatcher GetDispatcher(EventListener listener)
		{
			EventDispatcher eventDispatcher;
			for (eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				if (eventDispatcher.m_Listener == listener)
				{
					return eventDispatcher;
				}
			}
			return eventDispatcher;
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000CB648 File Offset: 0x000C9848
		[SecurityCritical]
		private unsafe void WriteEventVarargs(int eventId, Guid* childActivityID, object[] args)
		{
			if (this.m_eventSourceEnabled)
			{
				try
				{
					if (childActivityID != null)
					{
						this.ValidateEventOpcodeForTransfer(ref this.m_eventData[eventId], this.m_eventData[eventId].Name);
						if (!this.m_eventData[eventId].HasRelatedActivityID)
						{
							throw new ArgumentException(Environment.GetResourceString("EventSource_NoRelatedActivityId"));
						}
					}
					this.LogEventArgsMismatches(this.m_eventData[eventId].Parameters, args);
					Guid* ptr = null;
					Guid empty = Guid.Empty;
					Guid empty2 = Guid.Empty;
					EventOpcode opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode;
					EventActivityOptions activityOptions = this.m_eventData[eventId].ActivityOptions;
					if (childActivityID == null && (activityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
					{
						if (opcode == EventOpcode.Start)
						{
							this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty, ref empty2, this.m_eventData[eventId].ActivityOptions);
						}
						else if (opcode == EventOpcode.Stop)
						{
							this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty);
						}
						if (empty != Guid.Empty)
						{
							ptr = &empty;
						}
						if (empty2 != Guid.Empty)
						{
							childActivityID = &empty2;
						}
					}
					if (this.m_eventData[eventId].EnabledForETW)
					{
						SessionMask sessionMask = SessionMask.All;
						if ((ulong)this.m_curLiveSessions != 0UL)
						{
							sessionMask = this.GetEtwSessionMask(eventId, childActivityID);
						}
						if ((ulong)sessionMask != 0UL || (this.m_legacySessions != null && this.m_legacySessions.Count > 0))
						{
							if (!this.SelfDescribingEvents)
							{
								if (sessionMask.IsEqualOrSupersetOf(this.m_curLiveSessions))
								{
									if (!this.m_provider.WriteEvent(ref this.m_eventData[eventId].Descriptor, ptr, childActivityID, args))
									{
										this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
									}
								}
								else
								{
									long num = this.m_eventData[eventId].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords());
									EventDescriptor eventDescriptor = new EventDescriptor(this.m_eventData[eventId].Descriptor.EventId, this.m_eventData[eventId].Descriptor.Version, this.m_eventData[eventId].Descriptor.Channel, this.m_eventData[eventId].Descriptor.Level, this.m_eventData[eventId].Descriptor.Opcode, this.m_eventData[eventId].Descriptor.Task, (long)(sessionMask.ToEventKeywords() | (ulong)num));
									if (!this.m_provider.WriteEvent(ref eventDescriptor, ptr, childActivityID, args))
									{
										this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
									}
								}
							}
							else
							{
								TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
								if (traceLoggingEventTypes == null)
								{
									traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
									Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
								}
								long num2 = this.m_eventData[eventId].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords());
								EventSourceOptions eventSourceOptions = new EventSourceOptions
								{
									Keywords = (EventKeywords)(sessionMask.ToEventKeywords() | (ulong)num2),
									Level = (EventLevel)this.m_eventData[eventId].Descriptor.Level,
									Opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode
								};
								this.WriteMultiMerge(this.m_eventData[eventId].Name, ref eventSourceOptions, traceLoggingEventTypes, ptr, childActivityID, args);
							}
						}
					}
					if (this.m_Dispatchers != null && this.m_eventData[eventId].EnabledForAnyListener)
					{
						if (AppContextSwitches.PreserveEventListnerObjectIdentity)
						{
							this.WriteToAllListeners(eventId, ptr, childActivityID, args);
						}
						else
						{
							object[] array = this.SerializeEventArgs(eventId, args);
							this.WriteToAllListeners(eventId, ptr, childActivityID, array);
						}
					}
				}
				catch (Exception ex)
				{
					if (ex is EventSourceException)
					{
						throw;
					}
					this.ThrowEventSourceException(this.m_eventData[eventId].Name, ex);
				}
			}
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000CBB2C File Offset: 0x000C9D2C
		[SecurityCritical]
		private object[] SerializeEventArgs(int eventId, object[] args)
		{
			TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
			if (traceLoggingEventTypes == null)
			{
				traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
				Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
			}
			object[] array = new object[traceLoggingEventTypes.typeInfos.Length];
			for (int i = 0; i < traceLoggingEventTypes.typeInfos.Length; i++)
			{
				array[i] = traceLoggingEventTypes.typeInfos[i].GetData(args[i]);
			}
			return array;
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000CBBD0 File Offset: 0x000C9DD0
		private void LogEventArgsMismatches(ParameterInfo[] infos, object[] args)
		{
			bool flag = args.Length == infos.Length;
			int num = 0;
			while (flag && num < args.Length)
			{
				Type parameterType = infos[num].ParameterType;
				if ((args[num] != null && args[num].GetType() != parameterType) || (args[num] == null && (!parameterType.IsGenericType || !(parameterType.GetGenericTypeDefinition() == typeof(Nullable<>)))))
				{
					flag = false;
					break;
				}
				num++;
			}
			if (!flag)
			{
				Debugger.Log(0, null, Environment.GetResourceString("EventSource_VarArgsParameterMismatch") + "\r\n");
			}
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000CBC5C File Offset: 0x000C9E5C
		private int GetParamLengthIncludingByteArray(ParameterInfo[] parameters)
		{
			int num = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				if (parameterInfo.ParameterType == typeof(byte[]))
				{
					num += 2;
				}
				else
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000CBCA4 File Offset: 0x000C9EA4
		[SecurityCritical]
		private unsafe void WriteToAllListeners(int eventId, Guid* activityID, Guid* childActivityID, int eventDataCount, EventSource.EventData* data)
		{
			int num = this.m_eventData[eventId].Parameters.Length;
			int paramLengthIncludingByteArray = this.GetParamLengthIncludingByteArray(this.m_eventData[eventId].Parameters);
			if (eventDataCount != paramLengthIncludingByteArray)
			{
				this.ReportOutOfBandMessage(Environment.GetResourceString("EventSource_EventParametersMismatch", new object[] { eventId, eventDataCount, num }), true);
				num = Math.Min(num, eventDataCount);
			}
			object[] array = new object[num];
			EventSource.EventData* ptr = data;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.DecodeObject(eventId, i, ref ptr);
			}
			this.WriteToAllListeners(eventId, activityID, childActivityID, array);
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000CBD58 File Offset: 0x000C9F58
		[SecurityCritical]
		private unsafe void WriteToAllListeners(int eventId, Guid* activityID, Guid* childActivityID, params object[] args)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventId = eventId;
			if (activityID != null)
			{
				eventWrittenEventArgs.ActivityId = *activityID;
			}
			if (childActivityID != null)
			{
				eventWrittenEventArgs.RelatedActivityId = *childActivityID;
			}
			eventWrittenEventArgs.EventName = this.m_eventData[eventId].Name;
			eventWrittenEventArgs.Message = this.m_eventData[eventId].Message;
			eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>(args);
			this.DispatchToAllListeners(eventId, childActivityID, eventWrittenEventArgs);
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000CBDE0 File Offset: 0x000C9FE0
		[SecurityCritical]
		private unsafe void DispatchToAllListeners(int eventId, Guid* childActivityID, EventWrittenEventArgs eventCallbackArgs)
		{
			Exception ex = null;
			for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				if (eventId == -1 || eventDispatcher.m_EventEnabled[eventId])
				{
					ActivityFilter activityFilter = eventDispatcher.m_Listener.m_activityFilter;
					if (activityFilter == null || ActivityFilter.PassesActivityFilter(activityFilter, childActivityID, this.m_eventData[eventId].TriggersActivityTracking > 0, this, eventId) || !eventDispatcher.m_activityFilteringEnabled)
					{
						try
						{
							eventDispatcher.m_Listener.OnEventWritten(eventCallbackArgs);
						}
						catch (Exception ex2)
						{
							this.ReportOutOfBandMessage("ERROR: Exception during EventSource.OnEventWritten: " + ex2.Message, false);
							ex = ex2;
						}
					}
				}
			}
			if (ex != null)
			{
				throw new EventSourceException(ex);
			}
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000CBE90 File Offset: 0x000CA090
		[SecuritySafeCritical]
		private unsafe void WriteEventString(EventLevel level, long keywords, string msgString)
		{
			if (this.m_provider != null)
			{
				string text = "EventSourceMessage";
				if (this.SelfDescribingEvents)
				{
					EventSourceOptions eventSourceOptions = new EventSourceOptions
					{
						Keywords = (EventKeywords)keywords,
						Level = level
					};
					var <>f__AnonymousType = new
					{
						message = msgString
					};
					TraceLoggingEventTypes traceLoggingEventTypes = new TraceLoggingEventTypes(text, EventTags.None, new Type[] { <>f__AnonymousType.GetType() });
					this.WriteMultiMergeInner(text, ref eventSourceOptions, traceLoggingEventTypes, null, null, new object[] { <>f__AnonymousType });
					return;
				}
				if (this.m_rawManifest == null && this.m_outOfBandMessageCount == 1)
				{
					ManifestBuilder manifestBuilder = new ManifestBuilder(this.Name, this.Guid, this.Name, null, EventManifestOptions.None);
					manifestBuilder.StartEvent(text, new EventAttribute(0)
					{
						Level = EventLevel.LogAlways,
						Task = (EventTask)65534
					});
					manifestBuilder.AddEventParameter(typeof(string), "message");
					manifestBuilder.EndEvent();
					this.SendManifest(manifestBuilder.CreateManifest());
				}
				fixed (string text2 = msgString)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventDescriptor eventDescriptor = new EventDescriptor(0, 0, 0, (byte)level, 0, 0, keywords);
					EventProvider.EventData eventData = default(EventProvider.EventData);
					eventData.Ptr = ptr;
					eventData.Size = (uint)(2 * (msgString.Length + 1));
					eventData.Reserved = 0U;
					this.m_provider.WriteEvent(ref eventDescriptor, null, null, 1, (IntPtr)((void*)(&eventData)));
				}
			}
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000CBFF4 File Offset: 0x000CA1F4
		private void WriteStringToAllListeners(string eventName, string msg)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventId = 0;
			eventWrittenEventArgs.Message = msg;
			eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>(new List<object> { msg });
			eventWrittenEventArgs.PayloadNames = new ReadOnlyCollection<string>(new List<string> { "message" });
			eventWrittenEventArgs.EventName = eventName;
			for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				bool flag = false;
				if (eventDispatcher.m_EventEnabled == null)
				{
					flag = true;
				}
				else
				{
					for (int i = 0; i < eventDispatcher.m_EventEnabled.Length; i++)
					{
						if (eventDispatcher.m_EventEnabled[i])
						{
							flag = true;
							break;
						}
					}
				}
				try
				{
					if (flag)
					{
						eventDispatcher.m_Listener.OnEventWritten(eventWrittenEventArgs);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000CC0B8 File Offset: 0x000CA2B8
		[SecurityCritical]
		private unsafe SessionMask GetEtwSessionMask(int eventId, Guid* childActivityID)
		{
			SessionMask sessionMask = default(SessionMask);
			int num = 0;
			while ((long)num < 4L)
			{
				EtwSession etwSession = this.m_etwSessionIdMap[num];
				if (etwSession != null)
				{
					ActivityFilter activityFilter = etwSession.m_activityFilter;
					if ((activityFilter == null && !this.m_activityFilteringForETWEnabled[num]) || (activityFilter != null && ActivityFilter.PassesActivityFilter(activityFilter, childActivityID, this.m_eventData[eventId].TriggersActivityTracking > 0, this, eventId)) || !this.m_activityFilteringForETWEnabled[num])
					{
						sessionMask[num] = true;
					}
				}
				num++;
			}
			if (this.m_legacySessions != null && this.m_legacySessions.Count > 0 && this.m_eventData[eventId].Descriptor.Opcode == 9)
			{
				Guid* ptr = null;
				foreach (EtwSession etwSession2 in this.m_legacySessions)
				{
					if (etwSession2 != null)
					{
						ActivityFilter activityFilter2 = etwSession2.m_activityFilter;
						if (activityFilter2 != null)
						{
							if (ptr == null)
							{
								Guid internalCurrentThreadActivityId = EventSource.InternalCurrentThreadActivityId;
								ptr = &internalCurrentThreadActivityId;
							}
							ActivityFilter.FlowActivityIfNeeded(activityFilter2, ptr, childActivityID);
						}
					}
				}
			}
			return sessionMask;
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000CC1E4 File Offset: 0x000CA3E4
		private bool IsEnabledByDefault(int eventNum, bool enable, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword)
		{
			if (!enable)
			{
				return false;
			}
			EventLevel level = (EventLevel)this.m_eventData[eventNum].Descriptor.Level;
			EventKeywords eventKeywords = (EventKeywords)(this.m_eventData[eventNum].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords()));
			EventChannel channel = (EventChannel)this.m_eventData[eventNum].Descriptor.Channel;
			return this.IsEnabledCommon(enable, currentLevel, currentMatchAnyKeyword, level, eventKeywords, channel);
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000CC260 File Offset: 0x000CA460
		private bool IsEnabledCommon(bool enabled, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword, EventLevel eventLevel, EventKeywords eventKeywords, EventChannel eventChannel)
		{
			if (!enabled)
			{
				return false;
			}
			if (currentLevel != EventLevel.LogAlways && currentLevel < eventLevel)
			{
				return false;
			}
			if (currentMatchAnyKeyword != EventKeywords.None && eventKeywords != EventKeywords.None)
			{
				if (eventChannel != EventChannel.None && this.m_channelData != null && this.m_channelData.Length > (int)eventChannel)
				{
					EventKeywords eventKeywords2 = (EventKeywords)(this.m_channelData[(int)eventChannel] | (ulong)eventKeywords);
					if (eventKeywords2 != EventKeywords.None && (eventKeywords2 & currentMatchAnyKeyword) == EventKeywords.None)
					{
						return false;
					}
				}
				else if ((eventKeywords & currentMatchAnyKeyword) == EventKeywords.None)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000CC2C4 File Offset: 0x000CA4C4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ThrowEventSourceException(string eventName, Exception innerEx = null)
		{
			if (EventSource.m_EventSourceExceptionRecurenceCount > 0)
			{
				return;
			}
			try
			{
				EventSource.m_EventSourceExceptionRecurenceCount += 1;
				string text = "EventSourceException";
				if (eventName != null)
				{
					text = text + " while processing event \"" + eventName + "\"";
				}
				switch (EventProvider.GetLastWriteEventError())
				{
				case EventProvider.WriteEventErrorCode.NoFreeBuffers:
					this.ReportOutOfBandMessage(text + ": " + Environment.GetResourceString("EventSource_NoFreeBuffers"), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Environment.GetResourceString("EventSource_NoFreeBuffers"), innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.EventTooBig:
					this.ReportOutOfBandMessage(text + ": " + Environment.GetResourceString("EventSource_EventTooBig"), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Environment.GetResourceString("EventSource_EventTooBig"), innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.NullInput:
					this.ReportOutOfBandMessage(text + ": " + Environment.GetResourceString("EventSource_NullInput"), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Environment.GetResourceString("EventSource_NullInput"), innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.TooManyArgs:
					this.ReportOutOfBandMessage(text + ": " + Environment.GetResourceString("EventSource_TooManyArgs"), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Environment.GetResourceString("EventSource_TooManyArgs"), innerEx);
					}
					break;
				default:
					if (innerEx != null)
					{
						string[] array = new string[5];
						array[0] = text;
						array[1] = ": ";
						int num = 2;
						Type type = innerEx.GetType();
						array[num] = ((type != null) ? type.ToString() : null);
						array[3] = ":";
						array[4] = innerEx.Message;
						this.ReportOutOfBandMessage(string.Concat(array), true);
					}
					else
					{
						this.ReportOutOfBandMessage(text, true);
					}
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(innerEx);
					}
					break;
				}
			}
			finally
			{
				EventSource.m_EventSourceExceptionRecurenceCount -= 1;
			}
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000CC490 File Offset: 0x000CA690
		private void ValidateEventOpcodeForTransfer(ref EventSource.EventMetadata eventData, string eventName)
		{
			if (eventData.Descriptor.Opcode != 9 && eventData.Descriptor.Opcode != 240 && eventData.Descriptor.Opcode != 1)
			{
				this.ThrowEventSourceException(eventName, null);
			}
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000CC4C9 File Offset: 0x000CA6C9
		internal static EventOpcode GetOpcodeWithDefault(EventOpcode opcode, string eventName)
		{
			if (opcode == EventOpcode.Info && eventName != null)
			{
				if (eventName.EndsWith("Start"))
				{
					return EventOpcode.Start;
				}
				if (eventName.EndsWith("Stop"))
				{
					return EventOpcode.Stop;
				}
			}
			return opcode;
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000CC4F0 File Offset: 0x000CA6F0
		internal void SendCommand(EventListener listener, int perEventSourceSessionId, int etwSessionId, EventCommand command, bool enable, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> commandArguments)
		{
			EventCommandEventArgs eventCommandEventArgs = new EventCommandEventArgs(command, commandArguments, this, listener, perEventSourceSessionId, etwSessionId, enable, level, matchAnyKeyword);
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (this.m_completelyInited)
				{
					this.m_deferredCommands = null;
					this.DoCommand(eventCommandEventArgs);
				}
				else
				{
					eventCommandEventArgs.nextCommand = this.m_deferredCommands;
					this.m_deferredCommands = eventCommandEventArgs;
				}
			}
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000CC568 File Offset: 0x000CA768
		internal void DoCommand(EventCommandEventArgs commandArgs)
		{
			if (this.m_provider == null)
			{
				return;
			}
			this.m_outOfBandMessageCount = 0;
			bool flag = commandArgs.perEventSourceSessionId > 0 && (long)commandArgs.perEventSourceSessionId <= 4L;
			try
			{
				this.EnsureDescriptorsInitialized();
				commandArgs.dispatcher = this.GetDispatcher(commandArgs.listener);
				if (commandArgs.dispatcher == null && commandArgs.listener != null)
				{
					throw new ArgumentException(Environment.GetResourceString("EventSource_ListenerNotFound"));
				}
				if (commandArgs.Arguments == null)
				{
					commandArgs.Arguments = new Dictionary<string, string>();
				}
				if (commandArgs.Command == EventCommand.Update)
				{
					for (int i = 0; i < this.m_eventData.Length; i++)
					{
						this.EnableEventForDispatcher(commandArgs.dispatcher, i, this.IsEnabledByDefault(i, commandArgs.enable, commandArgs.level, commandArgs.matchAnyKeyword));
					}
					if (commandArgs.enable)
					{
						if (!this.m_eventSourceEnabled)
						{
							this.m_level = commandArgs.level;
							this.m_matchAnyKeyword = commandArgs.matchAnyKeyword;
						}
						else
						{
							if (commandArgs.level > this.m_level)
							{
								this.m_level = commandArgs.level;
							}
							if (commandArgs.matchAnyKeyword == EventKeywords.None)
							{
								this.m_matchAnyKeyword = EventKeywords.None;
							}
							else if (this.m_matchAnyKeyword != EventKeywords.None)
							{
								this.m_matchAnyKeyword |= commandArgs.matchAnyKeyword;
							}
						}
					}
					bool flag2 = commandArgs.perEventSourceSessionId >= 0;
					if (commandArgs.perEventSourceSessionId == 0 && !commandArgs.enable)
					{
						flag2 = false;
					}
					if (commandArgs.listener == null)
					{
						if (!flag2)
						{
							commandArgs.perEventSourceSessionId = -commandArgs.perEventSourceSessionId;
						}
						commandArgs.perEventSourceSessionId--;
					}
					commandArgs.Command = (flag2 ? EventCommand.Enable : EventCommand.Disable);
					if (flag2 && commandArgs.dispatcher == null && !this.SelfDescribingEvents)
					{
						this.SendManifest(this.m_rawManifest);
					}
					if (flag2 && commandArgs.perEventSourceSessionId != -1)
					{
						bool flag3 = false;
						string text;
						int num;
						EventSource.ParseCommandArgs(commandArgs.Arguments, out flag3, out text, out num);
						if (commandArgs.listener == null && commandArgs.Arguments.Count > 0 && commandArgs.perEventSourceSessionId != num)
						{
							throw new ArgumentException(Environment.GetResourceString("EventSource_SessionIdError", new object[]
							{
								commandArgs.perEventSourceSessionId + 44,
								num + 44
							}));
						}
						if (commandArgs.listener == null)
						{
							this.UpdateEtwSession(commandArgs.perEventSourceSessionId, commandArgs.etwSessionId, true, text, flag3);
						}
						else
						{
							ActivityFilter.UpdateFilter(ref commandArgs.listener.m_activityFilter, this, 0, text);
							commandArgs.dispatcher.m_activityFilteringEnabled = flag3;
						}
					}
					else if (!flag2 && commandArgs.listener == null && commandArgs.perEventSourceSessionId >= 0 && (long)commandArgs.perEventSourceSessionId < 4L)
					{
						commandArgs.Arguments["EtwSessionKeyword"] = (commandArgs.perEventSourceSessionId + 44).ToString(CultureInfo.InvariantCulture);
					}
					if (commandArgs.enable)
					{
						this.m_eventSourceEnabled = true;
					}
					this.OnEventCommand(commandArgs);
					EventHandler<EventCommandEventArgs> eventCommandExecuted = this.m_eventCommandExecuted;
					if (eventCommandExecuted != null)
					{
						eventCommandExecuted(this, commandArgs);
					}
					if (commandArgs.listener == null && !flag2 && commandArgs.perEventSourceSessionId != -1)
					{
						this.UpdateEtwSession(commandArgs.perEventSourceSessionId, commandArgs.etwSessionId, false, null, false);
					}
					if (!commandArgs.enable)
					{
						if (commandArgs.listener == null)
						{
							int num2 = 0;
							while ((long)num2 < 4L)
							{
								EtwSession etwSession = this.m_etwSessionIdMap[num2];
								if (etwSession != null)
								{
									ActivityFilter.DisableFilter(ref etwSession.m_activityFilter, this);
								}
								num2++;
							}
							this.m_activityFilteringForETWEnabled = new SessionMask(0U);
							this.m_curLiveSessions = new SessionMask(0U);
							if (this.m_etwSessionIdMap != null)
							{
								int num3 = 0;
								while ((long)num3 < 4L)
								{
									this.m_etwSessionIdMap[num3] = null;
									num3++;
								}
							}
							if (this.m_legacySessions != null)
							{
								this.m_legacySessions.Clear();
							}
						}
						else
						{
							ActivityFilter.DisableFilter(ref commandArgs.listener.m_activityFilter, this);
							commandArgs.dispatcher.m_activityFilteringEnabled = false;
						}
						for (int j = 0; j < this.m_eventData.Length; j++)
						{
							bool flag4 = false;
							for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
							{
								if (eventDispatcher.m_EventEnabled[j])
								{
									flag4 = true;
									break;
								}
							}
							this.m_eventData[j].EnabledForAnyListener = flag4;
						}
						if (!this.AnyEventEnabled())
						{
							this.m_level = EventLevel.LogAlways;
							this.m_matchAnyKeyword = EventKeywords.None;
							this.m_eventSourceEnabled = false;
						}
					}
					this.UpdateKwdTriggers(commandArgs.enable);
				}
				else
				{
					if (commandArgs.Command == EventCommand.SendManifest && this.m_rawManifest != null)
					{
						this.SendManifest(this.m_rawManifest);
					}
					this.OnEventCommand(commandArgs);
					EventHandler<EventCommandEventArgs> eventCommandExecuted2 = this.m_eventCommandExecuted;
					if (eventCommandExecuted2 != null)
					{
						eventCommandExecuted2(this, commandArgs);
					}
				}
				if (this.m_completelyInited && (commandArgs.listener != null || flag))
				{
					SessionMask sessionMask = SessionMask.FromId(commandArgs.perEventSourceSessionId);
					this.ReportActivitySamplingInfo(commandArgs.listener, sessionMask);
				}
			}
			catch (Exception ex)
			{
				this.ReportOutOfBandMessage("ERROR: Exception in Command Processing for EventSource " + this.Name + ": " + ex.Message, true);
			}
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000CCA5C File Offset: 0x000CAC5C
		internal void UpdateEtwSession(int sessionIdBit, int etwSessionId, bool bEnable, string activityFilters, bool participateInSampling)
		{
			if ((long)sessionIdBit < 4L)
			{
				if (bEnable)
				{
					EtwSession etwSession = EtwSession.GetEtwSession(etwSessionId, true);
					ActivityFilter.UpdateFilter(ref etwSession.m_activityFilter, this, sessionIdBit, activityFilters);
					this.m_etwSessionIdMap[sessionIdBit] = etwSession;
					this.m_activityFilteringForETWEnabled[sessionIdBit] = participateInSampling;
				}
				else
				{
					EtwSession etwSession2 = EtwSession.GetEtwSession(etwSessionId, false);
					this.m_etwSessionIdMap[sessionIdBit] = null;
					this.m_activityFilteringForETWEnabled[sessionIdBit] = false;
					if (etwSession2 != null)
					{
						ActivityFilter.DisableFilter(ref etwSession2.m_activityFilter, this);
						EtwSession.RemoveEtwSession(etwSession2);
					}
				}
				this.m_curLiveSessions[sessionIdBit] = bEnable;
				return;
			}
			if (bEnable)
			{
				if (this.m_legacySessions == null)
				{
					this.m_legacySessions = new List<EtwSession>(8);
				}
				EtwSession etwSession3 = EtwSession.GetEtwSession(etwSessionId, true);
				if (!this.m_legacySessions.Contains(etwSession3))
				{
					this.m_legacySessions.Add(etwSession3);
					return;
				}
			}
			else
			{
				EtwSession etwSession4 = EtwSession.GetEtwSession(etwSessionId, false);
				if (etwSession4 != null)
				{
					if (this.m_legacySessions != null)
					{
						this.m_legacySessions.Remove(etwSession4);
					}
					EtwSession.RemoveEtwSession(etwSession4);
				}
			}
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000CCB44 File Offset: 0x000CAD44
		internal static bool ParseCommandArgs(IDictionary<string, string> commandArguments, out bool participateInSampling, out string activityFilters, out int sessionIdBit)
		{
			bool flag = true;
			participateInSampling = false;
			if (commandArguments.TryGetValue("ActivitySamplingStartEvent", out activityFilters))
			{
				participateInSampling = true;
			}
			string text;
			if (commandArguments.TryGetValue("ActivitySampling", out text))
			{
				if (string.Compare(text, "false", StringComparison.OrdinalIgnoreCase) == 0 || text == "0")
				{
					participateInSampling = false;
				}
				else
				{
					participateInSampling = true;
				}
			}
			int num = -1;
			string text2;
			if (!commandArguments.TryGetValue("EtwSessionKeyword", out text2) || !int.TryParse(text2, out num) || num < 44 || (long)num >= 48L)
			{
				sessionIdBit = -1;
				flag = false;
			}
			else
			{
				sessionIdBit = num - 44;
			}
			return flag;
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000CCBD0 File Offset: 0x000CADD0
		internal void UpdateKwdTriggers(bool enable)
		{
			if (enable)
			{
				ulong num = (ulong)this.m_matchAnyKeyword;
				if (num == 0UL)
				{
					num = ulong.MaxValue;
				}
				this.m_keywordTriggers = 0L;
				int num2 = 0;
				while ((long)num2 < 4L)
				{
					EtwSession etwSession = this.m_etwSessionIdMap[num2];
					if (etwSession != null)
					{
						ActivityFilter activityFilter = etwSession.m_activityFilter;
						ActivityFilter.UpdateKwdTriggers(activityFilter, this.m_guid, this, (EventKeywords)num);
					}
					num2++;
				}
				return;
			}
			this.m_keywordTriggers = 0L;
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000CCC30 File Offset: 0x000CAE30
		internal bool EnableEventForDispatcher(EventDispatcher dispatcher, int eventId, bool value)
		{
			if (dispatcher == null)
			{
				if (eventId >= this.m_eventData.Length)
				{
					return false;
				}
				if (this.m_provider != null)
				{
					this.m_eventData[eventId].EnabledForETW = value;
				}
			}
			else
			{
				if (eventId >= dispatcher.m_EventEnabled.Length)
				{
					return false;
				}
				dispatcher.m_EventEnabled[eventId] = value;
				if (value)
				{
					this.m_eventData[eventId].EnabledForAnyListener = true;
				}
			}
			return true;
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000CCCA0 File Offset: 0x000CAEA0
		private bool AnyEventEnabled()
		{
			for (int i = 0; i < this.m_eventData.Length; i++)
			{
				if (this.m_eventData[i].EnabledForETW || this.m_eventData[i].EnabledForAnyListener)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000CCCEF File Offset: 0x000CAEEF
		private bool IsDisposed
		{
			get
			{
				return this.m_provider == null || this.m_provider.m_disposed;
			}
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x000CCD0C File Offset: 0x000CAF0C
		[SecuritySafeCritical]
		private void EnsureDescriptorsInitialized()
		{
			if (this.m_eventData == null)
			{
				this.m_rawManifest = EventSource.CreateManifestAndDescriptors(base.GetType(), this.Name, this, EventManifestOptions.None);
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null && eventSource.Guid == this.m_guid && !eventSource.IsDisposed && eventSource != this)
					{
						throw new ArgumentException(Environment.GetResourceString("EventSource_EventSourceGuidInUse", new object[] { this.m_guid }));
					}
				}
				for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
				{
					if (eventDispatcher.m_EventEnabled == null)
					{
						eventDispatcher.m_EventEnabled = new bool[this.m_eventData.Length];
					}
				}
			}
			if (EventSource.s_currentPid == 0U)
			{
				EventSource.s_currentPid = Win32Native.GetCurrentProcessId();
			}
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000CCE14 File Offset: 0x000CB014
		[SecuritySafeCritical]
		private unsafe bool SendManifest(byte[] rawManifest)
		{
			bool flag = true;
			if (rawManifest == null)
			{
				return false;
			}
			fixed (byte[] array = rawManifest)
			{
				byte* ptr;
				if (rawManifest == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				EventDescriptor eventDescriptor = new EventDescriptor(65534, 1, 0, 0, 254, 65534, 72057594037927935L);
				ManifestEnvelope manifestEnvelope = default(ManifestEnvelope);
				manifestEnvelope.Format = ManifestEnvelope.ManifestFormats.SimpleXmlFormat;
				manifestEnvelope.MajorVersion = 1;
				manifestEnvelope.MinorVersion = 0;
				manifestEnvelope.Magic = 91;
				int i = rawManifest.Length;
				manifestEnvelope.ChunkNumber = 0;
				EventProvider.EventData* ptr2;
				checked
				{
					ptr2 = stackalloc EventProvider.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventProvider.EventData)];
					ptr2->Ptr = &manifestEnvelope;
					ptr2->Size = (uint)sizeof(ManifestEnvelope);
					ptr2->Reserved = 0U;
				}
				ptr2[1].Ptr = ptr;
				ptr2[1].Reserved = 0U;
				int num = 65280;
				for (;;)
				{
					IL_CA:
					manifestEnvelope.TotalChunks = (ushort)((i + (num - 1)) / num);
					while (i > 0)
					{
						ptr2[1].Size = (uint)Math.Min(i, num);
						if (this.m_provider != null && !this.m_provider.WriteEvent(ref eventDescriptor, null, null, 2, (IntPtr)((void*)ptr2)))
						{
							if (EventProvider.GetLastWriteEventError() == EventProvider.WriteEventErrorCode.EventTooBig && manifestEnvelope.ChunkNumber == 0 && num > 256)
							{
								num /= 2;
								goto IL_CA;
							}
							goto IL_141;
						}
						else
						{
							i -= num;
							ptr2[1].Ptr += (ulong)num;
							manifestEnvelope.ChunkNumber += 1;
							if (manifestEnvelope.ChunkNumber % 5 == 0)
							{
								Thread.Sleep(15);
							}
						}
					}
					goto IL_19C;
				}
				IL_141:
				flag = false;
				if (this.ThrowOnEventWriteErrors)
				{
					this.ThrowEventSourceException("SendManifest", null);
				}
				IL_19C:;
			}
			return flag;
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000CCFC0 File Offset: 0x000CB1C0
		internal static Attribute GetCustomAttributeHelper(MemberInfo member, Type attributeType, EventManifestOptions flags = EventManifestOptions.None)
		{
			if (!member.Module.Assembly.ReflectionOnly() && (flags & EventManifestOptions.AllowEventSourceOverride) == EventManifestOptions.None)
			{
				Attribute attribute = null;
				object[] customAttributes = member.GetCustomAttributes(attributeType, false);
				int num = 0;
				if (num < customAttributes.Length)
				{
					object obj = customAttributes[num];
					attribute = (Attribute)obj;
				}
				return attribute;
			}
			string fullName = attributeType.FullName;
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(member))
			{
				if (EventSource.AttributeTypeNamesMatch(attributeType, customAttributeData.Constructor.ReflectedType))
				{
					Attribute attribute2 = null;
					if (customAttributeData.ConstructorArguments.Count == 1)
					{
						attribute2 = (Attribute)Activator.CreateInstance(attributeType, new object[] { customAttributeData.ConstructorArguments[0].Value });
					}
					else if (customAttributeData.ConstructorArguments.Count == 0)
					{
						attribute2 = (Attribute)Activator.CreateInstance(attributeType);
					}
					if (attribute2 != null)
					{
						Type type = attribute2.GetType();
						foreach (CustomAttributeNamedArgument customAttributeNamedArgument in customAttributeData.NamedArguments)
						{
							PropertyInfo property = type.GetProperty(customAttributeNamedArgument.MemberInfo.Name, BindingFlags.Instance | BindingFlags.Public);
							object obj2 = customAttributeNamedArgument.TypedValue.Value;
							if (property.PropertyType.IsEnum)
							{
								obj2 = Enum.Parse(property.PropertyType, obj2.ToString());
							}
							property.SetValue(attribute2, obj2, null);
						}
						return attribute2;
					}
				}
			}
			return null;
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000CD190 File Offset: 0x000CB390
		private static bool AttributeTypeNamesMatch(Type attributeType, Type reflectedAttributeType)
		{
			return attributeType == reflectedAttributeType || string.Equals(attributeType.FullName, reflectedAttributeType.FullName, StringComparison.Ordinal) || (string.Equals(attributeType.Name, reflectedAttributeType.Name, StringComparison.Ordinal) && attributeType.Namespace.EndsWith("Diagnostics.Tracing") && reflectedAttributeType.Namespace.EndsWith("Diagnostics.Tracing"));
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000CD1F4 File Offset: 0x000CB3F4
		private static Type GetEventSourceBaseType(Type eventSourceType, bool allowEventSourceOverride, bool reflectionOnly)
		{
			if (eventSourceType.BaseType() == null)
			{
				return null;
			}
			do
			{
				eventSourceType = eventSourceType.BaseType();
			}
			while (eventSourceType != null && eventSourceType.IsAbstract());
			if (eventSourceType != null)
			{
				if (!allowEventSourceOverride)
				{
					if ((reflectionOnly && eventSourceType.FullName != typeof(EventSource).FullName) || (!reflectionOnly && eventSourceType != typeof(EventSource)))
					{
						return null;
					}
				}
				else if (eventSourceType.Name != "EventSource")
				{
					return null;
				}
			}
			return eventSourceType;
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000CD284 File Offset: 0x000CB484
		private static byte[] CreateManifestAndDescriptors(Type eventSourceType, string eventSourceDllName, EventSource source, EventManifestOptions flags = EventManifestOptions.None)
		{
			ManifestBuilder manifestBuilder = null;
			bool flag = source == null || !source.SelfDescribingEvents;
			Exception ex = null;
			byte[] array = null;
			if (eventSourceType.IsAbstract() && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
			{
				return null;
			}
			try
			{
				MethodInfo[] methods = eventSourceType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				int num = 1;
				EventSource.EventMetadata[] array2 = null;
				Dictionary<string, string> dictionary = null;
				if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
				{
					array2 = new EventSource.EventMetadata[methods.Length + 1];
					array2[0].Name = "";
				}
				ResourceManager resourceManager = null;
				EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), flags);
				if (eventSourceAttribute != null && eventSourceAttribute.LocalizationResources != null)
				{
					resourceManager = new ResourceManager(eventSourceAttribute.LocalizationResources, eventSourceType.Assembly());
				}
				manifestBuilder = new ManifestBuilder(EventSource.GetName(eventSourceType, flags), EventSource.GetGuid(eventSourceType), eventSourceDllName, resourceManager, flags);
				manifestBuilder.StartEvent("EventSourceMessage", new EventAttribute(0)
				{
					Level = EventLevel.LogAlways,
					Task = (EventTask)65534
				});
				manifestBuilder.AddEventParameter(typeof(string), "message");
				manifestBuilder.EndEvent();
				if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None)
				{
					if (!(EventSource.GetEventSourceBaseType(eventSourceType, (flags & EventManifestOptions.AllowEventSourceOverride) > EventManifestOptions.None, eventSourceType.Assembly().ReflectionOnly()) != null))
					{
						manifestBuilder.ManifestError(Environment.GetResourceString("EventSource_TypeMustDeriveFromEventSource"), false);
					}
					if (!eventSourceType.IsAbstract() && !eventSourceType.IsSealed())
					{
						manifestBuilder.ManifestError(Environment.GetResourceString("EventSource_TypeMustBeSealedOrAbstract"), false);
					}
				}
				foreach (string text in new string[] { "Keywords", "Tasks", "Opcodes" })
				{
					Type nestedType = eventSourceType.GetNestedType(text);
					if (nestedType != null)
					{
						if (eventSourceType.IsAbstract())
						{
							manifestBuilder.ManifestError(Environment.GetResourceString("EventSource_AbstractMustNotDeclareKTOC", new object[] { nestedType.Name }), false);
						}
						else
						{
							foreach (FieldInfo fieldInfo in nestedType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
							{
								EventSource.AddProviderEnumKind(manifestBuilder, fieldInfo, text);
							}
						}
					}
				}
				manifestBuilder.AddKeyword("Session3", 17592186044416UL);
				manifestBuilder.AddKeyword("Session2", 35184372088832UL);
				manifestBuilder.AddKeyword("Session1", 70368744177664UL);
				manifestBuilder.AddKeyword("Session0", 140737488355328UL);
				if (eventSourceType != typeof(EventSource))
				{
					foreach (MethodInfo methodInfo in methods)
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						EventAttribute eventAttribute = (EventAttribute)EventSource.GetCustomAttributeHelper(methodInfo, typeof(EventAttribute), flags);
						if (eventAttribute != null && source != null && eventAttribute.EventId <= 3 && source.Guid.Equals(EventSource.AspNetEventSourceGuid))
						{
							eventAttribute.ActivityOptions |= EventActivityOptions.Disable;
						}
						if (!methodInfo.IsStatic)
						{
							if (eventSourceType.IsAbstract())
							{
								if (eventAttribute != null)
								{
									manifestBuilder.ManifestError(Environment.GetResourceString("EventSource_AbstractMustNotDeclareEventMethods", new object[] { methodInfo.Name, eventAttribute.EventId }), false);
								}
							}
							else
							{
								if (eventAttribute == null)
								{
									if (methodInfo.ReturnType != typeof(void) || methodInfo.IsVirtual || EventSource.GetCustomAttributeHelper(methodInfo, typeof(NonEventAttribute), flags) != null)
									{
										goto IL_666;
									}
									EventAttribute eventAttribute2 = new EventAttribute(num);
									eventAttribute = eventAttribute2;
								}
								else if (eventAttribute.EventId <= 0)
								{
									manifestBuilder.ManifestError(Environment.GetResourceString("EventSource_NeedPositiveId", new object[] { methodInfo.Name }), true);
									goto IL_666;
								}
								if (methodInfo.Name.LastIndexOf('.') >= 0)
								{
									manifestBuilder.ManifestError(Environment.GetResourceString("EventSource_EventMustNotBeExplicitImplementation", new object[] { methodInfo.Name, eventAttribute.EventId }), false);
								}
								num++;
								string name = methodInfo.Name;
								if (eventAttribute.Opcode == EventOpcode.Info)
								{
									bool flag2 = eventAttribute.Task == EventTask.None;
									if (flag2)
									{
										eventAttribute.Task = (EventTask)65534 - eventAttribute.EventId;
									}
									if (!eventAttribute.IsOpcodeSet)
									{
										eventAttribute.Opcode = EventSource.GetOpcodeWithDefault(EventOpcode.Info, name);
									}
									if (flag2)
									{
										if (eventAttribute.Opcode == EventOpcode.Start)
										{
											string text2 = name.Substring(0, name.Length - "Start".Length);
											if (string.Compare(name, 0, text2, 0, text2.Length) == 0 && string.Compare(name, text2.Length, "Start", 0, Math.Max(name.Length - text2.Length, "Start".Length)) == 0)
											{
												manifestBuilder.AddTask(text2, (int)eventAttribute.Task);
											}
										}
										else if (eventAttribute.Opcode == EventOpcode.Stop)
										{
											int num2 = eventAttribute.EventId - 1;
											if (array2 != null && num2 < array2.Length)
											{
												EventSource.EventMetadata eventMetadata = array2[num2];
												string text3 = name.Substring(0, name.Length - "Stop".Length);
												if (eventMetadata.Descriptor.Opcode == 1 && string.Compare(eventMetadata.Name, 0, text3, 0, text3.Length) == 0 && string.Compare(eventMetadata.Name, text3.Length, "Start", 0, Math.Max(eventMetadata.Name.Length - text3.Length, "Start".Length)) == 0)
												{
													eventAttribute.Task = (EventTask)eventMetadata.Descriptor.Task;
													flag2 = false;
												}
											}
											if (flag2 && (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
											{
												throw new ArgumentException(Environment.GetResourceString("EventSource_StopsFollowStarts"));
											}
										}
									}
								}
								bool flag3 = EventSource.RemoveFirstArgIfRelatedActivityId(ref parameters);
								if (source == null || !source.SelfDescribingEvents)
								{
									manifestBuilder.StartEvent(name, eventAttribute);
									for (int l = 0; l < parameters.Length; l++)
									{
										manifestBuilder.AddEventParameter(parameters[l].ParameterType, parameters[l].Name);
									}
									manifestBuilder.EndEvent();
								}
								if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
								{
									EventSource.DebugCheckEvent(ref dictionary, array2, methodInfo, eventAttribute, manifestBuilder, flags);
									if (eventAttribute.Channel != EventChannel.None)
									{
										eventAttribute.Keywords |= (EventKeywords)manifestBuilder.GetChannelKeyword(eventAttribute.Channel);
									}
									string text4 = "event_" + name;
									string localizedMessage = manifestBuilder.GetLocalizedMessage(text4, CultureInfo.CurrentUICulture, false);
									if (localizedMessage != null)
									{
										eventAttribute.Message = localizedMessage;
									}
									EventSource.AddEventDescriptor(ref array2, name, eventAttribute, parameters, flag3);
								}
							}
						}
						IL_666:;
					}
				}
				NameInfo.ReserveEventIDsBelow(num);
				if (source != null)
				{
					EventSource.TrimEventDescriptors(ref array2);
					source.m_eventData = array2;
					source.m_channelData = manifestBuilder.GetChannelData();
				}
				if (!eventSourceType.IsAbstract() && (source == null || !source.SelfDescribingEvents))
				{
					flag = (flags & EventManifestOptions.OnlyIfNeededForRegistration) == EventManifestOptions.None || manifestBuilder.GetChannelData().Length != 0;
					if (!flag && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
					{
						return null;
					}
					array = manifestBuilder.CreateManifest();
				}
			}
			catch (Exception ex2)
			{
				if ((flags & EventManifestOptions.Strict) == EventManifestOptions.None)
				{
					throw;
				}
				ex = ex2;
			}
			if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None && (manifestBuilder.Errors.Count > 0 || ex != null))
			{
				string text5 = string.Empty;
				if (manifestBuilder.Errors.Count > 0)
				{
					bool flag4 = true;
					using (IEnumerator<string> enumerator = manifestBuilder.Errors.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text6 = enumerator.Current;
							if (!flag4)
							{
								text5 += Environment.NewLine;
							}
							flag4 = false;
							text5 += text6;
						}
						goto IL_782;
					}
				}
				text5 = "Unexpected error: " + ex.Message;
				IL_782:
				throw new ArgumentException(text5, ex);
			}
			if (!flag)
			{
				return null;
			}
			return array;
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x000CDA5C File Offset: 0x000CBC5C
		private static bool RemoveFirstArgIfRelatedActivityId(ref ParameterInfo[] args)
		{
			if (args.Length != 0 && args[0].ParameterType == typeof(Guid) && string.Compare(args[0].Name, "relatedActivityId", StringComparison.OrdinalIgnoreCase) == 0)
			{
				ParameterInfo[] array = new ParameterInfo[args.Length - 1];
				Array.Copy(args, 1, array, 0, args.Length - 1);
				args = array;
				return true;
			}
			return false;
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000CDAC0 File Offset: 0x000CBCC0
		private static void AddProviderEnumKind(ManifestBuilder manifest, FieldInfo staticField, string providerEnumKind)
		{
			bool flag = staticField.Module.Assembly.ReflectionOnly();
			Type fieldType = staticField.FieldType;
			if ((!flag && fieldType == typeof(EventOpcode)) || EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventOpcode)))
			{
				if (!(providerEnumKind != "Opcodes"))
				{
					int num = (int)staticField.GetRawConstantValue();
					manifest.AddOpcode(staticField.Name, num);
					return;
				}
			}
			else
			{
				if ((flag || !(fieldType == typeof(EventTask))) && !EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventTask)))
				{
					if ((!flag && fieldType == typeof(EventKeywords)) || EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventKeywords)))
					{
						if (providerEnumKind != "Keywords")
						{
							goto IL_107;
						}
						ulong num2 = (ulong)((long)staticField.GetRawConstantValue());
						manifest.AddKeyword(staticField.Name, num2);
					}
					return;
				}
				if (!(providerEnumKind != "Tasks"))
				{
					int num3 = (int)staticField.GetRawConstantValue();
					manifest.AddTask(staticField.Name, num3);
					return;
				}
			}
			IL_107:
			manifest.ManifestError(Environment.GetResourceString("EventSource_EnumKindMismatch", new object[]
			{
				staticField.Name,
				staticField.FieldType.Name,
				providerEnumKind
			}), false);
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000CDC08 File Offset: 0x000CBE08
		private static void AddEventDescriptor(ref EventSource.EventMetadata[] eventData, string eventName, EventAttribute eventAttribute, ParameterInfo[] eventParameters, bool hasRelatedActivityID)
		{
			if (eventData == null || eventData.Length <= eventAttribute.EventId)
			{
				EventSource.EventMetadata[] array = new EventSource.EventMetadata[Math.Max(eventData.Length + 16, eventAttribute.EventId + 1)];
				Array.Copy(eventData, array, eventData.Length);
				eventData = array;
			}
			eventData[eventAttribute.EventId].Descriptor = new EventDescriptor(eventAttribute.EventId, eventAttribute.Version, (byte)eventAttribute.Channel, (byte)eventAttribute.Level, (byte)eventAttribute.Opcode, (int)eventAttribute.Task, (long)(eventAttribute.Keywords | (EventKeywords)SessionMask.All.ToEventKeywords()));
			eventData[eventAttribute.EventId].Tags = eventAttribute.Tags;
			eventData[eventAttribute.EventId].Name = eventName;
			eventData[eventAttribute.EventId].Parameters = eventParameters;
			eventData[eventAttribute.EventId].Message = eventAttribute.Message;
			eventData[eventAttribute.EventId].ActivityOptions = eventAttribute.ActivityOptions;
			eventData[eventAttribute.EventId].HasRelatedActivityID = hasRelatedActivityID;
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000CDD24 File Offset: 0x000CBF24
		private static void TrimEventDescriptors(ref EventSource.EventMetadata[] eventData)
		{
			int num = eventData.Length;
			while (0 < num)
			{
				num--;
				if (eventData[num].Descriptor.EventId != 0)
				{
					break;
				}
			}
			if (eventData.Length - num > 2)
			{
				EventSource.EventMetadata[] array = new EventSource.EventMetadata[num + 1];
				Array.Copy(eventData, array, array.Length);
				eventData = array;
			}
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000CDD74 File Offset: 0x000CBF74
		internal void AddListener(EventListener listener)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				bool[] array = null;
				if (this.m_eventData != null)
				{
					array = new bool[this.m_eventData.Length];
				}
				this.m_Dispatchers = new EventDispatcher(this.m_Dispatchers, array, listener);
				listener.OnEventSourceCreated(this);
			}
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000CDDE8 File Offset: 0x000CBFE8
		private static void DebugCheckEvent(ref Dictionary<string, string> eventsByName, EventSource.EventMetadata[] eventData, MethodInfo method, EventAttribute eventAttribute, ManifestBuilder manifest, EventManifestOptions options)
		{
			int eventId = eventAttribute.EventId;
			string name = method.Name;
			int helperCallFirstArg = EventSource.GetHelperCallFirstArg(method);
			if (helperCallFirstArg >= 0 && eventId != helperCallFirstArg)
			{
				manifest.ManifestError(Environment.GetResourceString("EventSource_MismatchIdToWriteEvent", new object[] { name, eventId, helperCallFirstArg }), true);
			}
			if (eventId < eventData.Length && eventData[eventId].Descriptor.EventId != 0)
			{
				manifest.ManifestError(Environment.GetResourceString("EventSource_EventIdReused", new object[]
				{
					name,
					eventId,
					eventData[eventId].Name
				}), true);
			}
			for (int i = 0; i < eventData.Length; i++)
			{
				if (eventData[i].Name != null && eventData[i].Descriptor.Task == (int)eventAttribute.Task && (EventOpcode)eventData[i].Descriptor.Opcode == eventAttribute.Opcode)
				{
					manifest.ManifestError(Environment.GetResourceString("EventSource_TaskOpcodePairReused", new object[]
					{
						name,
						eventId,
						eventData[i].Name,
						i
					}), false);
					if ((options & EventManifestOptions.Strict) == EventManifestOptions.None)
					{
						break;
					}
				}
			}
			if (eventAttribute.Opcode != EventOpcode.Info)
			{
				bool flag = false;
				if (eventAttribute.Task == EventTask.None)
				{
					flag = true;
				}
				else
				{
					EventTask eventTask = (EventTask)65534 - eventId;
					if (eventAttribute.Opcode != EventOpcode.Start && eventAttribute.Opcode != EventOpcode.Stop && eventAttribute.Task == eventTask)
					{
						flag = true;
					}
				}
				if (flag)
				{
					manifest.ManifestError(Environment.GetResourceString("EventSource_EventMustHaveTaskIfNonDefaultOpcode", new object[] { name, eventId }), false);
				}
			}
			if (eventsByName == null)
			{
				eventsByName = new Dictionary<string, string>();
			}
			if (eventsByName.ContainsKey(name))
			{
				manifest.ManifestError(Environment.GetResourceString("EventSource_EventNameReused", new object[] { name }), true);
			}
			eventsByName[name] = name;
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000CDFC8 File Offset: 0x000CC1C8
		[SecuritySafeCritical]
		private static int GetHelperCallFirstArg(MethodInfo method)
		{
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			byte[] ilasByteArray = method.GetMethodBody().GetILAsByteArray();
			int num = -1;
			for (int i = 0; i < ilasByteArray.Length; i++)
			{
				byte b = ilasByteArray[i];
				if (b <= 110)
				{
					switch (b)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 13:
					case 20:
					case 37:
						break;
					case 14:
					case 16:
						i++;
						break;
					case 15:
					case 17:
					case 18:
					case 19:
					case 33:
					case 34:
					case 35:
					case 36:
					case 38:
					case 39:
					case 41:
					case 42:
					case 43:
					case 46:
					case 47:
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
						return -1;
					case 21:
					case 22:
					case 23:
					case 24:
					case 25:
					case 26:
					case 27:
					case 28:
					case 29:
					case 30:
						if (i > 0 && ilasByteArray[i - 1] == 2)
						{
							num = (int)(ilasByteArray[i] - 22);
						}
						break;
					case 31:
						if (i > 0 && ilasByteArray[i - 1] == 2)
						{
							num = (int)ilasByteArray[i + 1];
						}
						i++;
						break;
					case 32:
						i += 4;
						break;
					case 40:
						i += 4;
						if (num >= 0)
						{
							for (int j = i + 1; j < ilasByteArray.Length; j++)
							{
								if (ilasByteArray[j] == 42)
								{
									return num;
								}
								if (ilasByteArray[j] != 0)
								{
									break;
								}
							}
						}
						num = -1;
						break;
					case 44:
					case 45:
						num = -1;
						i++;
						break;
					case 57:
					case 58:
						num = -1;
						i += 4;
						break;
					default:
						if (b - 103 > 3 && b - 109 > 1)
						{
							return -1;
						}
						break;
					}
				}
				else if (b - 140 > 1)
				{
					if (b != 162)
					{
						if (b != 254)
						{
							return -1;
						}
						i++;
						if (i >= ilasByteArray.Length || ilasByteArray[i] >= 6)
						{
							return -1;
						}
					}
				}
				else
				{
					i += 4;
				}
			}
			return -1;
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000CE1DC File Offset: 0x000CC3DC
		internal void ReportOutOfBandMessage(string msg, bool flush)
		{
			try
			{
				Debugger.Log(0, null, msg + "\r\n");
				if (this.m_outOfBandMessageCount < 15)
				{
					this.m_outOfBandMessageCount += 1;
				}
				else
				{
					if (this.m_outOfBandMessageCount == 16)
					{
						return;
					}
					this.m_outOfBandMessageCount = 16;
					msg = "Reached message limit.   End of EventSource error messages.";
				}
				this.WriteEventString(EventLevel.LogAlways, -1L, msg);
				this.WriteStringToAllListeners("EventSourceMessage", msg);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000CE25C File Offset: 0x000CC45C
		private EventSourceSettings ValidateSettings(EventSourceSettings settings)
		{
			EventSourceSettings eventSourceSettings = EventSourceSettings.EtwManifestEventFormat | EventSourceSettings.EtwSelfDescribingEventFormat;
			if ((settings & eventSourceSettings) == eventSourceSettings)
			{
				throw new ArgumentException(Environment.GetResourceString("EventSource_InvalidEventFormat"), "settings");
			}
			if ((settings & eventSourceSettings) == EventSourceSettings.Default)
			{
				settings |= EventSourceSettings.EtwSelfDescribingEventFormat;
			}
			return settings;
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x000CE292 File Offset: 0x000CC492
		// (set) Token: 0x060034F0 RID: 13552 RVA: 0x000CE29F File Offset: 0x000CC49F
		private bool ThrowOnEventWriteErrors
		{
			get
			{
				return (this.m_config & EventSourceSettings.ThrowOnEventWriteErrors) > EventSourceSettings.Default;
			}
			set
			{
				if (value)
				{
					this.m_config |= EventSourceSettings.ThrowOnEventWriteErrors;
					return;
				}
				this.m_config &= ~EventSourceSettings.ThrowOnEventWriteErrors;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060034F1 RID: 13553 RVA: 0x000CE2C2 File Offset: 0x000CC4C2
		// (set) Token: 0x060034F2 RID: 13554 RVA: 0x000CE2CF File Offset: 0x000CC4CF
		private bool SelfDescribingEvents
		{
			get
			{
				return (this.m_config & EventSourceSettings.EtwSelfDescribingEventFormat) > EventSourceSettings.Default;
			}
			set
			{
				if (!value)
				{
					this.m_config |= EventSourceSettings.EtwManifestEventFormat;
					this.m_config &= ~EventSourceSettings.EtwSelfDescribingEventFormat;
					return;
				}
				this.m_config |= EventSourceSettings.EtwSelfDescribingEventFormat;
				this.m_config &= ~EventSourceSettings.EtwManifestEventFormat;
			}
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000CE310 File Offset: 0x000CC510
		private void ReportActivitySamplingInfo(EventListener listener, SessionMask sessions)
		{
			int num = 0;
			while ((long)num < 4L)
			{
				if (sessions[num])
				{
					ActivityFilter activityFilter;
					if (listener == null)
					{
						EtwSession etwSession = this.m_etwSessionIdMap[num];
						activityFilter = etwSession.m_activityFilter;
					}
					else
					{
						activityFilter = listener.m_activityFilter;
					}
					if (activityFilter != null)
					{
						SessionMask sessionMask = default(SessionMask);
						sessionMask[num] = true;
						foreach (Tuple<int, int> tuple in activityFilter.GetFilterAsTuple(this.m_guid))
						{
							this.WriteStringToListener(listener, string.Format(CultureInfo.InvariantCulture, "Session {0}: {1} = {2}", num, tuple.Item1, tuple.Item2), sessionMask);
						}
						bool flag = ((listener == null) ? this.m_activityFilteringForETWEnabled[num] : this.GetDispatcher(listener).m_activityFilteringEnabled);
						this.WriteStringToListener(listener, string.Format(CultureInfo.InvariantCulture, "Session {0}: Activity Sampling support: {1}", num, flag ? "enabled" : "disabled"), sessionMask);
					}
				}
				num++;
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class with the specified name.</summary>
		/// <param name="eventSourceName">The name to apply to the event source. Must not be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventSourceName" /> is <see langword="null" />.</exception>
		// Token: 0x060034F4 RID: 13556 RVA: 0x000CE438 File Offset: 0x000CC638
		[__DynamicallyInvokable]
		public EventSource(string eventSourceName)
			: this(eventSourceName, EventSourceSettings.EtwSelfDescribingEventFormat)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class with the specified name and settings.</summary>
		/// <param name="eventSourceName">The name to apply to the event source. Must not be <see langword="null" />.</param>
		/// <param name="config">A bitwise combination of the enumeration values that specify the configuration settings to apply to the event source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventSourceName" /> is <see langword="null" />.</exception>
		// Token: 0x060034F5 RID: 13557 RVA: 0x000CE442 File Offset: 0x000CC642
		[__DynamicallyInvokable]
		public EventSource(string eventSourceName, EventSourceSettings config)
			: this(eventSourceName, config, null)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class with the specified configuration settings.</summary>
		/// <param name="eventSourceName">The name to apply to the event source. Must not be <see langword="null" />.</param>
		/// <param name="config">A bitwise combination of the enumeration values that specify the configuration settings to apply to the event source.</param>
		/// <param name="traits">The key-value pairs that specify traits for the event source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventSourceName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="traits" /> is not specified in key-value pairs.</exception>
		// Token: 0x060034F6 RID: 13558 RVA: 0x000CE450 File Offset: 0x000CC650
		[__DynamicallyInvokable]
		public EventSource(string eventSourceName, EventSourceSettings config, params string[] traits)
			: this((eventSourceName == null) ? default(Guid) : EventSource.GenerateGuidFromName(eventSourceName.ToUpperInvariant()), eventSourceName, config, traits)
		{
			if (eventSourceName == null)
			{
				throw new ArgumentNullException("eventSourceName");
			}
		}

		/// <summary>Writes an event without fields, but with the specified name and default options.</summary>
		/// <param name="eventName">The name of the event to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventName" /> is <see langword="null" />.</exception>
		// Token: 0x060034F7 RID: 13559 RVA: 0x000CE490 File Offset: 0x000CC690
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void Write(string eventName)
		{
			if (eventName == null)
			{
				throw new ArgumentNullException("eventName");
			}
			if (!this.IsEnabled())
			{
				return;
			}
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.WriteImpl<EmptyStruct>(eventName, ref eventSourceOptions, ref emptyStruct, null, null);
		}

		/// <summary>Writes an event without fields, but with the specified name and options.</summary>
		/// <param name="eventName">The name of the event to write.</param>
		/// <param name="options">The options such as level, keywords and operation code for the event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventName" /> is <see langword="null" />.</exception>
		// Token: 0x060034F8 RID: 13560 RVA: 0x000CE4D4 File Offset: 0x000CC6D4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void Write(string eventName, EventSourceOptions options)
		{
			if (eventName == null)
			{
				throw new ArgumentNullException("eventName");
			}
			if (!this.IsEnabled())
			{
				return;
			}
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.WriteImpl<EmptyStruct>(eventName, ref options, ref emptyStruct, null, null);
		}

		/// <summary>Writes an event with the specified name and data.</summary>
		/// <param name="eventName">The name of the event.</param>
		/// <param name="data">The event data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> attribute.</param>
		/// <typeparam name="T">The type that defines the event and its associated data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> attribute.</typeparam>
		// Token: 0x060034F9 RID: 13561 RVA: 0x000CE510 File Offset: 0x000CC710
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void Write<T>(string eventName, T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			this.WriteImpl<T>(eventName, ref eventSourceOptions, ref data, null, null);
		}

		/// <summary>Writes an event with the specified name, event data and options.</summary>
		/// <param name="eventName">The name of the event.</param>
		/// <param name="options">The event options.</param>
		/// <param name="data">The event data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> attribute.</param>
		/// <typeparam name="T">The type that defines the event and its associated data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> attribute.</typeparam>
		// Token: 0x060034FA RID: 13562 RVA: 0x000CE53D File Offset: 0x000CC73D
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void Write<T>(string eventName, EventSourceOptions options, T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			this.WriteImpl<T>(eventName, ref options, ref data, null, null);
		}

		/// <summary>Writes an event with the specified name, options and event data.</summary>
		/// <param name="eventName">The name of the event.</param>
		/// <param name="options">The event options.</param>
		/// <param name="data">The event data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> attribute.</param>
		/// <typeparam name="T">The type that defines the event and its associated data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> attribute.</typeparam>
		// Token: 0x060034FB RID: 13563 RVA: 0x000CE557 File Offset: 0x000CC757
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void Write<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			this.WriteImpl<T>(eventName, ref options, ref data, null, null);
		}

		/// <summary>Writes an event with the specified name, options, related activity and event data.</summary>
		/// <param name="eventName">The name of the event.</param>
		/// <param name="options">The event options.</param>
		/// <param name="activityId">The ID of the activity associated with the event.</param>
		/// <param name="relatedActivityId">The ID of an associated activity, or <see cref="F:System.Guid.Empty" /> if there is no associated activity.</param>
		/// <param name="data">The event data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> attribute.</param>
		/// <typeparam name="T">The type that defines the event and its associated data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> attribute.</typeparam>
		// Token: 0x060034FC RID: 13564 RVA: 0x000CE570 File Offset: 0x000CC770
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe void Write<T>(string eventName, ref EventSourceOptions options, ref Guid activityId, ref Guid relatedActivityId, ref T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			fixed (Guid* ptr = &activityId)
			{
				Guid* ptr2 = ptr;
				fixed (Guid* ptr3 = &relatedActivityId)
				{
					Guid* ptr4 = ptr3;
					this.WriteImpl<T>(eventName, ref options, ref data, ptr2, (relatedActivityId == Guid.Empty) ? null : ptr4);
					ptr = null;
				}
				return;
			}
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000CE5BC File Offset: 0x000CC7BC
		[SecuritySafeCritical]
		private unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			byte b = (((options.valuesSet & 4) != 0) ? options.level : eventTypes.level);
			EventKeywords eventKeywords = (((options.valuesSet & 1) != 0) ? options.keywords : eventTypes.keywords);
			if (this.IsEnabled((EventLevel)b, eventKeywords))
			{
				this.WriteMultiMergeInner(eventName, ref options, eventTypes, activityID, childActivityID, values);
			}
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000CE620 File Offset: 0x000CC820
		[SecuritySafeCritical]
		private unsafe void WriteMultiMergeInner(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
		{
			byte b = (((options.valuesSet & 4) != 0) ? options.level : eventTypes.level);
			byte b2 = (((options.valuesSet & 8) != 0) ? options.opcode : eventTypes.opcode);
			EventTags eventTags = (((options.valuesSet & 2) != 0) ? options.tags : eventTypes.Tags);
			EventKeywords eventKeywords = (((options.valuesSet & 1) != 0) ? options.keywords : eventTypes.keywords);
			NameInfo nameInfo = eventTypes.GetNameInfo(eventName ?? eventTypes.Name, eventTags);
			if (nameInfo == null)
			{
				return;
			}
			int identity = nameInfo.identity;
			EventDescriptor eventDescriptor = new EventDescriptor(identity, b, b2, (long)eventKeywords);
			int pinCount = eventTypes.pinCount;
			byte* ptr = stackalloc byte[(UIntPtr)eventTypes.scratchSize];
			EventSource.EventData* ptr2;
			GCHandle* ptr3;
			byte[] array;
			byte[] array2;
			byte* ptr5;
			byte[] array3;
			byte* ptr6;
			checked
			{
				ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)(eventTypes.dataCount + 3)) * (UIntPtr)sizeof(EventSource.EventData)];
				ptr3 = stackalloc GCHandle[unchecked((UIntPtr)pinCount) * (UIntPtr)sizeof(GCHandle)];
				byte* ptr4;
				if ((array = this.providerMetadata) == null || array.Length == 0)
				{
					ptr4 = null;
				}
				else
				{
					ptr4 = &array[0];
				}
				if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
				{
					ptr5 = null;
				}
				else
				{
					ptr5 = &array2[0];
				}
				if ((array3 = eventTypes.typeMetadata) == null || array3.Length == 0)
				{
					ptr6 = null;
				}
				else
				{
					ptr6 = &array3[0];
				}
				ptr2->SetMetadata(ptr4, this.providerMetadata.Length, 2);
			}
			ptr2[1].SetMetadata(ptr5, nameInfo.nameMetadata.Length, 1);
			ptr2[2].SetMetadata(ptr6, eventTypes.typeMetadata.Length, 1);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				DataCollector.ThreadInstance.Enable(ptr, eventTypes.scratchSize, ptr2 + 3, eventTypes.dataCount, ptr3, pinCount);
				for (int i = 0; i < eventTypes.typeInfos.Length; i++)
				{
					eventTypes.typeInfos[i].WriteObjectData(TraceLoggingDataCollector.Instance, values[i]);
				}
				this.WriteEventRaw(eventName, ref eventDescriptor, activityID, childActivityID, (int)((long)(DataCollector.ThreadInstance.Finish() - ptr2)), (IntPtr)((void*)ptr2));
			}
			finally
			{
				this.WriteCleanup(ptr3, pinCount);
			}
			array = null;
			array2 = null;
			array3 = null;
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000CE854 File Offset: 0x000CCA54
		[SecuritySafeCritical]
		internal unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, EventSource.EventData* data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			fixed (EventSourceOptions* ptr = &options)
			{
				EventSourceOptions* ptr2 = ptr;
				EventDescriptor eventDescriptor;
				NameInfo nameInfo = this.UpdateDescriptor(eventName, eventTypes, ref options, out eventDescriptor);
				if (nameInfo == null)
				{
					return;
				}
				EventSource.EventData* ptr3;
				byte[] array;
				byte[] array2;
				byte* ptr5;
				byte[] array3;
				byte* ptr6;
				checked
				{
					ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)(eventTypes.dataCount + eventTypes.typeInfos.Length * 2 + 3)) * (UIntPtr)sizeof(EventSource.EventData)];
					byte* ptr4;
					if ((array = this.providerMetadata) == null || array.Length == 0)
					{
						ptr4 = null;
					}
					else
					{
						ptr4 = &array[0];
					}
					if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
					{
						ptr5 = null;
					}
					else
					{
						ptr5 = &array2[0];
					}
					if ((array3 = eventTypes.typeMetadata) == null || array3.Length == 0)
					{
						ptr6 = null;
					}
					else
					{
						ptr6 = &array3[0];
					}
					ptr3->SetMetadata(ptr4, this.providerMetadata.Length, 2);
				}
				ptr3[1].SetMetadata(ptr5, nameInfo.nameMetadata.Length, 1);
				ptr3[2].SetMetadata(ptr6, eventTypes.typeMetadata.Length, 1);
				int num = 3;
				for (int i = 0; i < eventTypes.typeInfos.Length; i++)
				{
					if (eventTypes.typeInfos[i].DataType == typeof(string))
					{
						ptr3[num].DataPointer = (IntPtr)((void*)(&ptr3[num + 1].m_Size));
						ptr3[num].m_Size = 2;
						num++;
						ptr3[num].m_Ptr = data[i].m_Ptr;
						ptr3[num].m_Size = data[i].m_Size - 2;
						num++;
					}
					else
					{
						ptr3[num].m_Ptr = data[i].m_Ptr;
						ptr3[num].m_Size = data[i].m_Size;
						if (data[i].m_Size == 4 && eventTypes.typeInfos[i].DataType == typeof(bool))
						{
							ptr3[num].m_Size = 1;
						}
						num++;
					}
				}
				this.WriteEventRaw(eventName, ref eventDescriptor, activityID, childActivityID, num, (IntPtr)((void*)ptr3));
				array = null;
				array2 = null;
				array3 = null;
			}
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000CEAE0 File Offset: 0x000CCCE0
		[SecuritySafeCritical]
		private unsafe void WriteImpl<T>(string eventName, ref EventSourceOptions options, ref T data, Guid* pActivityId, Guid* pRelatedActivityId)
		{
			try
			{
				SimpleEventTypes<T> instance = SimpleEventTypes<T>.Instance;
				try
				{
					fixed (EventSourceOptions* ptr = &options)
					{
						EventSourceOptions* ptr2 = ptr;
						options.Opcode = (options.IsOpcodeSet ? options.Opcode : EventSource.GetOpcodeWithDefault(options.Opcode, eventName));
						EventDescriptor eventDescriptor;
						NameInfo nameInfo = this.UpdateDescriptor(eventName, instance, ref options, out eventDescriptor);
						if (nameInfo != null)
						{
							int pinCount = instance.pinCount;
							byte* ptr3 = stackalloc byte[(UIntPtr)instance.scratchSize];
							EventSource.EventData* ptr4;
							GCHandle* ptr5;
							checked
							{
								ptr4 = stackalloc EventSource.EventData[unchecked((UIntPtr)(instance.dataCount + 3)) * (UIntPtr)sizeof(EventSource.EventData)];
								ptr5 = stackalloc GCHandle[unchecked((UIntPtr)pinCount) * (UIntPtr)sizeof(GCHandle)];
							}
							try
							{
								byte[] array;
								byte* ptr6;
								if ((array = this.providerMetadata) == null || array.Length == 0)
								{
									ptr6 = null;
								}
								else
								{
									ptr6 = &array[0];
								}
								byte[] array2;
								byte* ptr7;
								if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
								{
									ptr7 = null;
								}
								else
								{
									ptr7 = &array2[0];
								}
								byte[] array3;
								byte* ptr8;
								if ((array3 = instance.typeMetadata) == null || array3.Length == 0)
								{
									ptr8 = null;
								}
								else
								{
									ptr8 = &array3[0];
								}
								ptr4->SetMetadata(ptr6, this.providerMetadata.Length, 2);
								ptr4[1].SetMetadata(ptr7, nameInfo.nameMetadata.Length, 1);
								ptr4[2].SetMetadata(ptr8, instance.typeMetadata.Length, 1);
								RuntimeHelpers.PrepareConstrainedRegions();
								EventOpcode opcode = (EventOpcode)eventDescriptor.Opcode;
								Guid empty = Guid.Empty;
								Guid empty2 = Guid.Empty;
								if (pActivityId == null && pRelatedActivityId == null && (options.ActivityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
								{
									if (opcode == EventOpcode.Start)
									{
										this.m_activityTracker.OnStart(this.m_name, eventName, 0, ref empty, ref empty2, options.ActivityOptions);
									}
									else if (opcode == EventOpcode.Stop)
									{
										this.m_activityTracker.OnStop(this.m_name, eventName, 0, ref empty);
									}
									if (empty != Guid.Empty)
									{
										pActivityId = &empty;
									}
									if (empty2 != Guid.Empty)
									{
										pRelatedActivityId = &empty2;
									}
								}
								try
								{
									DataCollector.ThreadInstance.Enable(ptr3, instance.scratchSize, ptr4 + 3, instance.dataCount, ptr5, pinCount);
									instance.typeInfo.WriteData(TraceLoggingDataCollector.Instance, ref data);
									this.WriteEventRaw(eventName, ref eventDescriptor, pActivityId, pRelatedActivityId, (int)((long)(DataCollector.ThreadInstance.Finish() - ptr4)), (IntPtr)((void*)ptr4));
									if (this.m_Dispatchers != null)
									{
										EventPayload eventPayload = (EventPayload)instance.typeInfo.GetData(data);
										this.WriteToAllListeners(eventName, ref eventDescriptor, nameInfo.tags, pActivityId, pRelatedActivityId, eventPayload);
									}
								}
								catch (Exception ex)
								{
									if (ex is EventSourceException)
									{
										throw;
									}
									this.ThrowEventSourceException(eventName, ex);
								}
								finally
								{
									this.WriteCleanup(ptr5, pinCount);
								}
							}
							finally
							{
								byte[] array = null;
								byte[] array2 = null;
								byte[] array3 = null;
							}
						}
					}
				}
				finally
				{
					EventSourceOptions* ptr = null;
				}
			}
			catch (Exception ex2)
			{
				if (ex2 is EventSourceException)
				{
					throw;
				}
				this.ThrowEventSourceException(eventName, ex2);
			}
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000CEE1C File Offset: 0x000CD01C
		[SecurityCritical]
		private unsafe void WriteToAllListeners(string eventName, ref EventDescriptor eventDescriptor, EventTags tags, Guid* pActivityId, Guid* pChildActivityId, EventPayload payload)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventName = eventName;
			eventWrittenEventArgs.m_level = (EventLevel)eventDescriptor.Level;
			eventWrittenEventArgs.m_keywords = (EventKeywords)eventDescriptor.Keywords;
			eventWrittenEventArgs.m_opcode = (EventOpcode)eventDescriptor.Opcode;
			eventWrittenEventArgs.m_tags = tags;
			eventWrittenEventArgs.EventId = -1;
			if (pActivityId != null)
			{
				eventWrittenEventArgs.ActivityId = *pActivityId;
			}
			if (pChildActivityId != null)
			{
				eventWrittenEventArgs.RelatedActivityId = *pChildActivityId;
			}
			if (payload != null)
			{
				eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>((IList<object>)payload.Values);
				eventWrittenEventArgs.PayloadNames = new ReadOnlyCollection<string>((IList<string>)payload.Keys);
			}
			this.DispatchToAllListeners(-1, pActivityId, eventWrittenEventArgs);
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000CEECC File Offset: 0x000CD0CC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		[NonEvent]
		private unsafe void WriteCleanup(GCHandle* pPins, int cPins)
		{
			DataCollector.ThreadInstance.Disable();
			for (int num = 0; num != cPins; num++)
			{
				if (IntPtr.Zero != (IntPtr)pPins[num])
				{
					pPins[num].Free();
				}
			}
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000CEF20 File Offset: 0x000CD120
		private void InitializeProviderMetadata()
		{
			if (this.m_traits != null)
			{
				List<byte> list = new List<byte>(100);
				for (int i = 0; i < this.m_traits.Length - 1; i += 2)
				{
					if (this.m_traits[i].StartsWith("ETW_"))
					{
						string text = this.m_traits[i].Substring(4);
						byte b;
						if (!byte.TryParse(text, out b))
						{
							if (!(text == "GROUP"))
							{
								throw new ArgumentException(Environment.GetResourceString("UnknownEtwTrait", new object[] { text }), "traits");
							}
							b = 1;
						}
						string text2 = this.m_traits[i + 1];
						int count = list.Count;
						list.Add(0);
						list.Add(0);
						list.Add(b);
						int num = EventSource.AddValueToMetaData(list, text2) + 3;
						list[count] = (byte)num;
						list[count + 1] = (byte)(num >> 8);
					}
				}
				this.providerMetadata = Statics.MetadataForString(this.Name, 0, list.Count, 0);
				int num2 = this.providerMetadata.Length - list.Count;
				using (List<byte>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						byte b2 = enumerator.Current;
						this.providerMetadata[num2++] = b2;
					}
					return;
				}
			}
			this.providerMetadata = Statics.MetadataForString(this.Name, 0, 0, 0);
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000CF094 File Offset: 0x000CD294
		private static int AddValueToMetaData(List<byte> metaData, string value)
		{
			if (value.Length == 0)
			{
				return 0;
			}
			int count = metaData.Count;
			char c = value[0];
			if (c == '@')
			{
				metaData.AddRange(Encoding.UTF8.GetBytes(value.Substring(1)));
			}
			else if (c == '{')
			{
				metaData.AddRange(new Guid(value).ToByteArray());
			}
			else if (c == '#')
			{
				for (int i = 1; i < value.Length; i++)
				{
					if (value[i] != ' ')
					{
						if (i + 1 >= value.Length)
						{
							throw new ArgumentException(Environment.GetResourceString("EvenHexDigits"), "traits");
						}
						metaData.Add((byte)(EventSource.HexDigit(value[i]) * 16 + EventSource.HexDigit(value[i + 1])));
						i++;
					}
				}
			}
			else
			{
				if (' ' > c)
				{
					throw new ArgumentException(Environment.GetResourceString("IllegalValue", new object[] { value }), "traits");
				}
				metaData.AddRange(Encoding.UTF8.GetBytes(value));
			}
			return metaData.Count - count;
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000CF1A4 File Offset: 0x000CD3A4
		private static int HexDigit(char c)
		{
			if ('0' <= c && c <= '9')
			{
				return (int)(c - '0');
			}
			if ('a' <= c)
			{
				c -= ' ';
			}
			if ('A' <= c && c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			throw new ArgumentException(Environment.GetResourceString("BadHexDigit", new object[] { c }), "traits");
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000CF204 File Offset: 0x000CD404
		private NameInfo UpdateDescriptor(string name, TraceLoggingEventTypes eventInfo, ref EventSourceOptions options, out EventDescriptor descriptor)
		{
			NameInfo nameInfo = null;
			int num = 0;
			byte b = (((options.valuesSet & 4) != 0) ? options.level : eventInfo.level);
			byte b2 = (((options.valuesSet & 8) != 0) ? options.opcode : eventInfo.opcode);
			EventTags eventTags = (((options.valuesSet & 2) != 0) ? options.tags : eventInfo.Tags);
			EventKeywords eventKeywords = (((options.valuesSet & 1) != 0) ? options.keywords : eventInfo.keywords);
			if (this.IsEnabled((EventLevel)b, eventKeywords))
			{
				nameInfo = eventInfo.GetNameInfo(name ?? eventInfo.Name, eventTags);
				num = nameInfo.identity;
			}
			descriptor = new EventDescriptor(num, b, b2, (long)eventKeywords);
			return nameInfo;
		}

		// Token: 0x04001758 RID: 5976
		private string m_name;

		// Token: 0x04001759 RID: 5977
		internal int m_id;

		// Token: 0x0400175A RID: 5978
		private Guid m_guid;

		// Token: 0x0400175B RID: 5979
		internal volatile EventSource.EventMetadata[] m_eventData;

		// Token: 0x0400175C RID: 5980
		private volatile byte[] m_rawManifest;

		// Token: 0x0400175D RID: 5981
		private EventHandler<EventCommandEventArgs> m_eventCommandExecuted;

		// Token: 0x0400175E RID: 5982
		private EventSourceSettings m_config;

		// Token: 0x0400175F RID: 5983
		private bool m_eventSourceEnabled;

		// Token: 0x04001760 RID: 5984
		internal EventLevel m_level;

		// Token: 0x04001761 RID: 5985
		internal EventKeywords m_matchAnyKeyword;

		// Token: 0x04001762 RID: 5986
		internal volatile EventDispatcher m_Dispatchers;

		// Token: 0x04001763 RID: 5987
		private volatile EventSource.OverideEventProvider m_provider;

		// Token: 0x04001764 RID: 5988
		private bool m_completelyInited;

		// Token: 0x04001765 RID: 5989
		private Exception m_constructionException;

		// Token: 0x04001766 RID: 5990
		private byte m_outOfBandMessageCount;

		// Token: 0x04001767 RID: 5991
		private EventCommandEventArgs m_deferredCommands;

		// Token: 0x04001768 RID: 5992
		private string[] m_traits;

		// Token: 0x04001769 RID: 5993
		internal static uint s_currentPid;

		// Token: 0x0400176A RID: 5994
		[ThreadStatic]
		private static byte m_EventSourceExceptionRecurenceCount = 0;

		// Token: 0x0400176B RID: 5995
		internal volatile ulong[] m_channelData;

		// Token: 0x0400176C RID: 5996
		private SessionMask m_curLiveSessions;

		// Token: 0x0400176D RID: 5997
		private EtwSession[] m_etwSessionIdMap;

		// Token: 0x0400176E RID: 5998
		private List<EtwSession> m_legacySessions;

		// Token: 0x0400176F RID: 5999
		internal long m_keywordTriggers;

		// Token: 0x04001770 RID: 6000
		internal SessionMask m_activityFilteringForETWEnabled;

		// Token: 0x04001771 RID: 6001
		internal static Action<Guid> s_activityDying;

		// Token: 0x04001772 RID: 6002
		private ActivityTracker m_activityTracker;

		// Token: 0x04001773 RID: 6003
		internal const string s_ActivityStartSuffix = "Start";

		// Token: 0x04001774 RID: 6004
		internal const string s_ActivityStopSuffix = "Stop";

		// Token: 0x04001775 RID: 6005
		private static readonly byte[] namespaceBytes = new byte[]
		{
			72, 44, 45, 178, 195, 144, 71, 200, 135, 248,
			26, 21, 191, 193, 48, 251
		};

		// Token: 0x04001776 RID: 6006
		private static readonly Guid AspNetEventSourceGuid = new Guid("ee799f41-cfa5-550b-bf2c-344747c1c668");

		// Token: 0x04001777 RID: 6007
		private byte[] providerMetadata;

		/// <summary>Provides the event data for creating fast <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overloads by using the <see cref="M:System.Diagnostics.Tracing.EventSource.WriteEventCore(System.Int32,System.Int32,System.Diagnostics.Tracing.EventSource.EventData*)" /> method.</summary>
		// Token: 0x02000B84 RID: 2948
		[__DynamicallyInvokable]
		protected internal struct EventData
		{
			/// <summary>Gets or sets the pointer to the data for the new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload.</summary>
			/// <returns>The pointer to the data.</returns>
			// Token: 0x1700125C RID: 4700
			// (get) Token: 0x06006C84 RID: 27780 RVA: 0x00178F55 File Offset: 0x00177155
			// (set) Token: 0x06006C85 RID: 27781 RVA: 0x00178F63 File Offset: 0x00177163
			[__DynamicallyInvokable]
			public unsafe IntPtr DataPointer
			{
				[SecuritySafeCritical]
				get
				{
					return (IntPtr)this.m_Ptr;
				}
				set
				{
					this.m_Ptr = (void*)value;
				}
			}

			/// <summary>Gets or sets the number of payload items in the new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload.</summary>
			/// <returns>The number of payload items in the new overload.</returns>
			// Token: 0x1700125D RID: 4701
			// (get) Token: 0x06006C86 RID: 27782 RVA: 0x00178F72 File Offset: 0x00177172
			// (set) Token: 0x06006C87 RID: 27783 RVA: 0x00178F7A File Offset: 0x0017717A
			[__DynamicallyInvokable]
			public int Size
			{
				[__DynamicallyInvokable]
				get
				{
					return this.m_Size;
				}
				[__DynamicallyInvokable]
				set
				{
					this.m_Size = value;
				}
			}

			// Token: 0x06006C88 RID: 27784 RVA: 0x00178F83 File Offset: 0x00177183
			[SecurityCritical]
			internal unsafe void SetMetadata(byte* pointer, int size, int reserved)
			{
				this.m_Ptr = pointer;
				this.m_Size = size;
				this.m_Reserved = reserved;
			}

			// Token: 0x040034F6 RID: 13558
			internal ulong m_Ptr;

			// Token: 0x040034F7 RID: 13559
			internal int m_Size;

			// Token: 0x040034F8 RID: 13560
			internal int m_Reserved;
		}

		// Token: 0x02000B85 RID: 2949
		private struct Sha1ForNonSecretPurposes
		{
			// Token: 0x06006C89 RID: 27785 RVA: 0x00178F9C File Offset: 0x0017719C
			public void Start()
			{
				if (this.w == null)
				{
					this.w = new uint[85];
				}
				this.length = 0L;
				this.pos = 0;
				this.w[80] = 1732584193U;
				this.w[81] = 4023233417U;
				this.w[82] = 2562383102U;
				this.w[83] = 271733878U;
				this.w[84] = 3285377520U;
			}

			// Token: 0x06006C8A RID: 27786 RVA: 0x00179014 File Offset: 0x00177214
			public void Append(byte input)
			{
				this.w[this.pos / 4] = (this.w[this.pos / 4] << 8) | (uint)input;
				int num = 64;
				int num2 = this.pos + 1;
				this.pos = num2;
				if (num == num2)
				{
					this.Drain();
				}
			}

			// Token: 0x06006C8B RID: 27787 RVA: 0x00179060 File Offset: 0x00177260
			public void Append(byte[] input)
			{
				foreach (byte b in input)
				{
					this.Append(b);
				}
			}

			// Token: 0x06006C8C RID: 27788 RVA: 0x00179088 File Offset: 0x00177288
			public void Finish(byte[] output)
			{
				long num = this.length + (long)(8 * this.pos);
				this.Append(128);
				while (this.pos != 56)
				{
					this.Append(0);
				}
				this.Append((byte)(num >> 56));
				this.Append((byte)(num >> 48));
				this.Append((byte)(num >> 40));
				this.Append((byte)(num >> 32));
				this.Append((byte)(num >> 24));
				this.Append((byte)(num >> 16));
				this.Append((byte)(num >> 8));
				this.Append((byte)num);
				int num2 = ((output.Length < 20) ? output.Length : 20);
				for (int num3 = 0; num3 != num2; num3++)
				{
					uint num4 = this.w[80 + num3 / 4];
					output[num3] = (byte)(num4 >> 24);
					this.w[80 + num3 / 4] = num4 << 8;
				}
			}

			// Token: 0x06006C8D RID: 27789 RVA: 0x0017915C File Offset: 0x0017735C
			private void Drain()
			{
				for (int num = 16; num != 80; num++)
				{
					this.w[num] = EventSource.Sha1ForNonSecretPurposes.Rol1(this.w[num - 3] ^ this.w[num - 8] ^ this.w[num - 14] ^ this.w[num - 16]);
				}
				uint num2 = this.w[80];
				uint num3 = this.w[81];
				uint num4 = this.w[82];
				uint num5 = this.w[83];
				uint num6 = this.w[84];
				for (int num7 = 0; num7 != 20; num7++)
				{
					uint num8 = (num3 & num4) | (~num3 & num5);
					uint num9 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num8 + num6 + 1518500249U + this.w[num7];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num9;
				}
				for (int num10 = 20; num10 != 40; num10++)
				{
					uint num11 = num3 ^ num4 ^ num5;
					uint num12 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num11 + num6 + 1859775393U + this.w[num10];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num12;
				}
				for (int num13 = 40; num13 != 60; num13++)
				{
					uint num14 = (num3 & num4) | (num3 & num5) | (num4 & num5);
					uint num15 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num14 + num6 + 2400959708U + this.w[num13];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num15;
				}
				for (int num16 = 60; num16 != 80; num16++)
				{
					uint num17 = num3 ^ num4 ^ num5;
					uint num18 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num17 + num6 + 3395469782U + this.w[num16];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num18;
				}
				this.w[80] += num2;
				this.w[81] += num3;
				this.w[82] += num4;
				this.w[83] += num5;
				this.w[84] += num6;
				this.length += 512L;
				this.pos = 0;
			}

			// Token: 0x06006C8E RID: 27790 RVA: 0x00179390 File Offset: 0x00177590
			private static uint Rol1(uint input)
			{
				return (input << 1) | (input >> 31);
			}

			// Token: 0x06006C8F RID: 27791 RVA: 0x0017939A File Offset: 0x0017759A
			private static uint Rol5(uint input)
			{
				return (input << 5) | (input >> 27);
			}

			// Token: 0x06006C90 RID: 27792 RVA: 0x001793A4 File Offset: 0x001775A4
			private static uint Rol30(uint input)
			{
				return (input << 30) | (input >> 2);
			}

			// Token: 0x040034F9 RID: 13561
			private long length;

			// Token: 0x040034FA RID: 13562
			private uint[] w;

			// Token: 0x040034FB RID: 13563
			private int pos;
		}

		// Token: 0x02000B86 RID: 2950
		private class OverideEventProvider : EventProvider
		{
			// Token: 0x06006C91 RID: 27793 RVA: 0x001793AE File Offset: 0x001775AE
			public OverideEventProvider(EventSource eventSource)
			{
				this.m_eventSource = eventSource;
			}

			// Token: 0x06006C92 RID: 27794 RVA: 0x001793C0 File Offset: 0x001775C0
			protected override void OnControllerCommand(ControllerCommand command, IDictionary<string, string> arguments, int perEventSourceSessionId, int etwSessionId)
			{
				EventListener eventListener = null;
				this.m_eventSource.SendCommand(eventListener, perEventSourceSessionId, etwSessionId, (EventCommand)command, base.IsEnabled(), base.Level, base.MatchAnyKeyword, arguments);
			}

			// Token: 0x040034FC RID: 13564
			private EventSource m_eventSource;
		}

		// Token: 0x02000B87 RID: 2951
		internal struct EventMetadata
		{
			// Token: 0x040034FD RID: 13565
			public EventDescriptor Descriptor;

			// Token: 0x040034FE RID: 13566
			public EventTags Tags;

			// Token: 0x040034FF RID: 13567
			public bool EnabledForAnyListener;

			// Token: 0x04003500 RID: 13568
			public bool EnabledForETW;

			// Token: 0x04003501 RID: 13569
			public bool HasRelatedActivityID;

			// Token: 0x04003502 RID: 13570
			public byte TriggersActivityTracking;

			// Token: 0x04003503 RID: 13571
			public string Name;

			// Token: 0x04003504 RID: 13572
			public string Message;

			// Token: 0x04003505 RID: 13573
			public ParameterInfo[] Parameters;

			// Token: 0x04003506 RID: 13574
			public TraceLoggingEventTypes TraceLoggingEventTypes;

			// Token: 0x04003507 RID: 13575
			public EventActivityOptions ActivityOptions;
		}
	}
}
