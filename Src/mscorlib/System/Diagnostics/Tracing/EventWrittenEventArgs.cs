using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides data for the <see cref="M:System.Diagnostics.Tracing.EventListener.OnEventWritten(System.Diagnostics.Tracing.EventWrittenEventArgs)" /> callback.</summary>
	// Token: 0x02000423 RID: 1059
	[__DynamicallyInvokable]
	public class EventWrittenEventArgs : EventArgs
	{
		/// <summary>Gets the name of the event.</summary>
		/// <returns>The name of the event.</returns>
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000CFB71 File Offset: 0x000CDD71
		// (set) Token: 0x0600352A RID: 13610 RVA: 0x000CFBA8 File Offset: 0x000CDDA8
		[__DynamicallyInvokable]
		public string EventName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_eventName != null || this.EventId < 0)
				{
					return this.m_eventName;
				}
				return this.m_eventSource.m_eventData[this.EventId].Name;
			}
			internal set
			{
				this.m_eventName = value;
			}
		}

		/// <summary>Gets the event identifier.</summary>
		/// <returns>The event identifier.</returns>
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x000CFBB1 File Offset: 0x000CDDB1
		// (set) Token: 0x0600352C RID: 13612 RVA: 0x000CFBB9 File Offset: 0x000CDDB9
		[__DynamicallyInvokable]
		public int EventId
		{
			[__DynamicallyInvokable]
			get;
			internal set; }

		/// <summary>Gets the activity ID on the thread that the event was written to.</summary>
		/// <returns>The activity ID on the thread that the event was written to.</returns>
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x000CFBC4 File Offset: 0x000CDDC4
		// (set) Token: 0x0600352E RID: 13614 RVA: 0x000CFBEC File Offset: 0x000CDDEC
		[__DynamicallyInvokable]
		public Guid ActivityId
		{
			[SecurityCritical]
			[__DynamicallyInvokable]
			get
			{
				Guid guid = this.m_activityId;
				if (guid == Guid.Empty)
				{
					guid = EventSource.CurrentThreadActivityId;
				}
				return guid;
			}
			internal set
			{
				this.m_activityId = value;
			}
		}

		/// <summary>Gets the identifier of an activity that is related to the activity represented by the current instance.</summary>
		/// <returns>The identifier of the related activity, or <see cref="F:System.Guid.Empty" /> if there is no related activity.</returns>
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000CFBF5 File Offset: 0x000CDDF5
		// (set) Token: 0x06003530 RID: 13616 RVA: 0x000CFBFD File Offset: 0x000CDDFD
		[__DynamicallyInvokable]
		public Guid RelatedActivityId
		{
			[SecurityCritical]
			[__DynamicallyInvokable]
			get;
			internal set; }

		/// <summary>Gets the payload for the event.</summary>
		/// <returns>The payload for the event.</returns>
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x000CFC06 File Offset: 0x000CDE06
		// (set) Token: 0x06003532 RID: 13618 RVA: 0x000CFC0E File Offset: 0x000CDE0E
		[__DynamicallyInvokable]
		public ReadOnlyCollection<object> Payload
		{
			[__DynamicallyInvokable]
			get;
			internal set; }

		/// <summary>Returns a list of strings that represent the property names of the event.</summary>
		/// <returns>Returns <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</returns>
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x000CFC18 File Offset: 0x000CDE18
		// (set) Token: 0x06003534 RID: 13620 RVA: 0x000CFC81 File Offset: 0x000CDE81
		[__DynamicallyInvokable]
		public ReadOnlyCollection<string> PayloadNames
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_payloadNames == null)
				{
					List<string> list = new List<string>();
					foreach (ParameterInfo parameterInfo in this.m_eventSource.m_eventData[this.EventId].Parameters)
					{
						list.Add(parameterInfo.Name);
					}
					this.m_payloadNames = new ReadOnlyCollection<string>(list);
				}
				return this.m_payloadNames;
			}
			internal set
			{
				this.m_payloadNames = value;
			}
		}

		/// <summary>Gets the event source object.</summary>
		/// <returns>The event source object.</returns>
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x000CFC8A File Offset: 0x000CDE8A
		[__DynamicallyInvokable]
		public EventSource EventSource
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_eventSource;
			}
		}

		/// <summary>Gets the keywords for the event.</summary>
		/// <returns>The keywords for the event.</returns>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x000CFC92 File Offset: 0x000CDE92
		[__DynamicallyInvokable]
		public EventKeywords Keywords
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_keywords;
				}
				return (EventKeywords)this.m_eventSource.m_eventData[this.EventId].Descriptor.Keywords;
			}
		}

		/// <summary>Gets the operation code for the event.</summary>
		/// <returns>The operation code for the event.</returns>
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x000CFCC6 File Offset: 0x000CDEC6
		[__DynamicallyInvokable]
		public EventOpcode Opcode
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_opcode;
				}
				return (EventOpcode)this.m_eventSource.m_eventData[this.EventId].Descriptor.Opcode;
			}
		}

		/// <summary>Gets the task for the event.</summary>
		/// <returns>The task for the event.</returns>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x000CFCFA File Offset: 0x000CDEFA
		[__DynamicallyInvokable]
		public EventTask Task
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return EventTask.None;
				}
				return (EventTask)this.m_eventSource.m_eventData[this.EventId].Descriptor.Task;
			}
		}

		/// <summary>Returns the tags specified in the call to the <see cref="M:System.Diagnostics.Tracing.EventSource.Write(System.String,System.Diagnostics.Tracing.EventSourceOptions)" /> method.</summary>
		/// <returns>Returns <see cref="T:System.Diagnostics.Tracing.EventTags" />.</returns>
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x000CFD29 File Offset: 0x000CDF29
		[__DynamicallyInvokable]
		public EventTags Tags
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_tags;
				}
				return this.m_eventSource.m_eventData[this.EventId].Tags;
			}
		}

		/// <summary>Gets the message for the event.</summary>
		/// <returns>The message for the event.</returns>
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x000CFD58 File Offset: 0x000CDF58
		// (set) Token: 0x0600353B RID: 13627 RVA: 0x000CFD87 File Offset: 0x000CDF87
		[__DynamicallyInvokable]
		public string Message
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_message;
				}
				return this.m_eventSource.m_eventData[this.EventId].Message;
			}
			internal set
			{
				this.m_message = value;
			}
		}

		/// <summary>Gets the channel for the event.</summary>
		/// <returns>The channel for the event.</returns>
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000CFD90 File Offset: 0x000CDF90
		[__DynamicallyInvokable]
		public EventChannel Channel
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return EventChannel.None;
				}
				return (EventChannel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Channel;
			}
		}

		/// <summary>Gets the version of the event.</summary>
		/// <returns>The version of the event.</returns>
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x000CFDBF File Offset: 0x000CDFBF
		[__DynamicallyInvokable]
		public byte Version
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return 0;
				}
				return this.m_eventSource.m_eventData[this.EventId].Descriptor.Version;
			}
		}

		/// <summary>Gets the level of the event.</summary>
		/// <returns>The level of the event.</returns>
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x000CFDEE File Offset: 0x000CDFEE
		[__DynamicallyInvokable]
		public EventLevel Level
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_level;
				}
				return (EventLevel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Level;
			}
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000CFE22 File Offset: 0x000CE022
		internal EventWrittenEventArgs(EventSource eventSource)
		{
			this.m_eventSource = eventSource;
		}

		// Token: 0x04001795 RID: 6037
		private string m_message;

		// Token: 0x04001796 RID: 6038
		private string m_eventName;

		// Token: 0x04001797 RID: 6039
		private EventSource m_eventSource;

		// Token: 0x04001798 RID: 6040
		private ReadOnlyCollection<string> m_payloadNames;

		// Token: 0x04001799 RID: 6041
		private Guid m_activityId;

		// Token: 0x0400179A RID: 6042
		internal EventTags m_tags;

		// Token: 0x0400179B RID: 6043
		internal EventOpcode m_opcode;

		// Token: 0x0400179C RID: 6044
		internal EventKeywords m_keywords;

		// Token: 0x0400179D RID: 6045
		internal EventLevel m_level;
	}
}
