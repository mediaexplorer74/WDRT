using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies additional event schema information for an event.</summary>
	// Token: 0x02000425 RID: 1061
	[AttributeUsage(AttributeTargets.Method)]
	[__DynamicallyInvokable]
	public sealed class EventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> class with the specified event identifier.</summary>
		/// <param name="eventId">The event identifier for the event.</param>
		// Token: 0x06003547 RID: 13639 RVA: 0x000CFE6C File Offset: 0x000CE06C
		[__DynamicallyInvokable]
		public EventAttribute(int eventId)
		{
			this.EventId = eventId;
			this.Level = EventLevel.Informational;
			this.m_opcodeSet = false;
		}

		/// <summary>Gets or sets the identifier for the event.</summary>
		/// <returns>The event identifier. This value should be between 0 and 65535.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x000CFE89 File Offset: 0x000CE089
		// (set) Token: 0x06003549 RID: 13641 RVA: 0x000CFE91 File Offset: 0x000CE091
		[__DynamicallyInvokable]
		public int EventId
		{
			[__DynamicallyInvokable]
			get;
			private set; }

		/// <summary>Gets or sets the level for the event.</summary>
		/// <returns>One of the enumeration values that specifies the level for the event.</returns>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x000CFE9A File Offset: 0x000CE09A
		// (set) Token: 0x0600354B RID: 13643 RVA: 0x000CFEA2 File Offset: 0x000CE0A2
		[__DynamicallyInvokable]
		public EventLevel Level
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Gets or sets the keywords for the event.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x000CFEAB File Offset: 0x000CE0AB
		// (set) Token: 0x0600354D RID: 13645 RVA: 0x000CFEB3 File Offset: 0x000CE0B3
		[__DynamicallyInvokable]
		public EventKeywords Keywords
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Gets or sets the operation code for the event.</summary>
		/// <returns>One of the enumeration values that specifies the operation code.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x000CFEBC File Offset: 0x000CE0BC
		// (set) Token: 0x0600354F RID: 13647 RVA: 0x000CFEC4 File Offset: 0x000CE0C4
		[__DynamicallyInvokable]
		public EventOpcode Opcode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_opcode;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_opcode = value;
				this.m_opcodeSet = true;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06003550 RID: 13648 RVA: 0x000CFED4 File Offset: 0x000CE0D4
		internal bool IsOpcodeSet
		{
			get
			{
				return this.m_opcodeSet;
			}
		}

		/// <summary>Gets or sets the task for the event.</summary>
		/// <returns>The task for the event.</returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x000CFEDC File Offset: 0x000CE0DC
		// (set) Token: 0x06003552 RID: 13650 RVA: 0x000CFEE4 File Offset: 0x000CE0E4
		[__DynamicallyInvokable]
		public EventTask Task
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Gets or sets an additional event log where the event should be written.</summary>
		/// <returns>An additional event log where the event should be written.</returns>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x000CFEED File Offset: 0x000CE0ED
		// (set) Token: 0x06003554 RID: 13652 RVA: 0x000CFEF5 File Offset: 0x000CE0F5
		[__DynamicallyInvokable]
		public EventChannel Channel
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Gets or sets the version of the event.</summary>
		/// <returns>The version of the event.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x000CFEFE File Offset: 0x000CE0FE
		// (set) Token: 0x06003556 RID: 13654 RVA: 0x000CFF06 File Offset: 0x000CE106
		[__DynamicallyInvokable]
		public byte Version
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Gets or sets the message for the event.</summary>
		/// <returns>The message for the event.</returns>
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06003557 RID: 13655 RVA: 0x000CFF0F File Offset: 0x000CE10F
		// (set) Token: 0x06003558 RID: 13656 RVA: 0x000CFF17 File Offset: 0x000CE117
		[__DynamicallyInvokable]
		public string Message
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Gets or sets the <see cref="T:System.Diagnostics.Tracing.EventTags" /> value for this <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> object. An event tag is a user-defined value that is passed through when the event is logged.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.Tracing.EventTags" /> value for this <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> object. An event tag is a user-defined value that is passed through when the event is logged.</returns>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000CFF20 File Offset: 0x000CE120
		// (set) Token: 0x0600355A RID: 13658 RVA: 0x000CFF28 File Offset: 0x000CE128
		[__DynamicallyInvokable]
		public EventTags Tags
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Specifies the behavior of the start and stop events of an activity. An activity is the region of time in an app between the start and the stop.</summary>
		/// <returns>Returns <see cref="T:System.Diagnostics.Tracing.EventActivityOptions" />.</returns>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x0600355B RID: 13659 RVA: 0x000CFF31 File Offset: 0x000CE131
		// (set) Token: 0x0600355C RID: 13660 RVA: 0x000CFF39 File Offset: 0x000CE139
		[__DynamicallyInvokable]
		public EventActivityOptions ActivityOptions
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		// Token: 0x040017AA RID: 6058
		private EventOpcode m_opcode;

		// Token: 0x040017AB RID: 6059
		private bool m_opcodeSet;
	}
}
