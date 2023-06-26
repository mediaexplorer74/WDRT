using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies overrides of default event settings such as the log level, keywords and operation code when the <see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions,``0)" /> method is called.</summary>
	// Token: 0x02000446 RID: 1094
	[__DynamicallyInvokable]
	public struct EventSourceOptions
	{
		/// <summary>Gets or sets the event level applied to the event.</summary>
		/// <returns>The event level for the event. If not set, the default is Verbose (5).</returns>
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x0600364C RID: 13900 RVA: 0x000D4122 File Offset: 0x000D2322
		// (set) Token: 0x0600364D RID: 13901 RVA: 0x000D412A File Offset: 0x000D232A
		[__DynamicallyInvokable]
		public EventLevel Level
		{
			[__DynamicallyInvokable]
			get
			{
				return (EventLevel)this.level;
			}
			[__DynamicallyInvokable]
			set
			{
				this.level = checked((byte)value);
				this.valuesSet |= 4;
			}
		}

		/// <summary>Gets or sets the operation code to use for the specified event.</summary>
		/// <returns>The operation code to use for the specified event. If not set, the default is <see langword="Info" /> (0).</returns>
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600364E RID: 13902 RVA: 0x000D4143 File Offset: 0x000D2343
		// (set) Token: 0x0600364F RID: 13903 RVA: 0x000D414B File Offset: 0x000D234B
		[__DynamicallyInvokable]
		public EventOpcode Opcode
		{
			[__DynamicallyInvokable]
			get
			{
				return (EventOpcode)this.opcode;
			}
			[__DynamicallyInvokable]
			set
			{
				this.opcode = checked((byte)value);
				this.valuesSet |= 8;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x000D4164 File Offset: 0x000D2364
		internal bool IsOpcodeSet
		{
			get
			{
				return (this.valuesSet & 8) > 0;
			}
		}

		/// <summary>Gets or sets the keywords applied to the event. If this property is not set, the event's keywords will be <see langword="None" />.</summary>
		/// <returns>The keywords applied to the event, or <see langword="None" /> if no keywords are set.</returns>
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06003651 RID: 13905 RVA: 0x000D4171 File Offset: 0x000D2371
		// (set) Token: 0x06003652 RID: 13906 RVA: 0x000D4179 File Offset: 0x000D2379
		[__DynamicallyInvokable]
		public EventKeywords Keywords
		{
			[__DynamicallyInvokable]
			get
			{
				return this.keywords;
			}
			[__DynamicallyInvokable]
			set
			{
				this.keywords = value;
				this.valuesSet |= 1;
			}
		}

		/// <summary>The event tags defined for this event source.</summary>
		/// <returns>Returns <see cref="T:System.Diagnostics.Tracing.EventTags" />.</returns>
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06003653 RID: 13907 RVA: 0x000D4191 File Offset: 0x000D2391
		// (set) Token: 0x06003654 RID: 13908 RVA: 0x000D4199 File Offset: 0x000D2399
		[__DynamicallyInvokable]
		public EventTags Tags
		{
			[__DynamicallyInvokable]
			get
			{
				return this.tags;
			}
			[__DynamicallyInvokable]
			set
			{
				this.tags = value;
				this.valuesSet |= 2;
			}
		}

		/// <summary>The activity options defined for this event source.</summary>
		/// <returns>Returns <see cref="T:System.Diagnostics.Tracing.EventActivityOptions" />.</returns>
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06003655 RID: 13909 RVA: 0x000D41B1 File Offset: 0x000D23B1
		// (set) Token: 0x06003656 RID: 13910 RVA: 0x000D41B9 File Offset: 0x000D23B9
		[__DynamicallyInvokable]
		public EventActivityOptions ActivityOptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.activityOptions;
			}
			[__DynamicallyInvokable]
			set
			{
				this.activityOptions = value;
				this.valuesSet |= 16;
			}
		}

		// Token: 0x0400183F RID: 6207
		internal EventKeywords keywords;

		// Token: 0x04001840 RID: 6208
		internal EventTags tags;

		// Token: 0x04001841 RID: 6209
		internal EventActivityOptions activityOptions;

		// Token: 0x04001842 RID: 6210
		internal byte level;

		// Token: 0x04001843 RID: 6211
		internal byte opcode;

		// Token: 0x04001844 RID: 6212
		internal byte valuesSet;

		// Token: 0x04001845 RID: 6213
		internal const byte keywordsSet = 1;

		// Token: 0x04001846 RID: 6214
		internal const byte tagsSet = 2;

		// Token: 0x04001847 RID: 6215
		internal const byte levelSet = 4;

		// Token: 0x04001848 RID: 6216
		internal const byte opcodeSet = 8;

		// Token: 0x04001849 RID: 6217
		internal const byte activityOptionsSet = 16;
	}
}
