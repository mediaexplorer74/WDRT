using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies a type to be passed to the <see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions,``0)" /> method.</summary>
	// Token: 0x0200043F RID: 1087
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	[__DynamicallyInvokable]
	public class EventDataAttribute : Attribute
	{
		/// <summary>Gets or sets the name to apply to an event if the event type or property is not explicitly named.</summary>
		/// <returns>The name to apply to the event or property.</returns>
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x000D3B4F File Offset: 0x000D1D4F
		// (set) Token: 0x06003613 RID: 13843 RVA: 0x000D3B57 File Offset: 0x000D1D57
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x000D3B60 File Offset: 0x000D1D60
		// (set) Token: 0x06003615 RID: 13845 RVA: 0x000D3B68 File Offset: 0x000D1D68
		internal EventLevel Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x000D3B71 File Offset: 0x000D1D71
		// (set) Token: 0x06003617 RID: 13847 RVA: 0x000D3B79 File Offset: 0x000D1D79
		internal EventOpcode Opcode
		{
			get
			{
				return this.opcode;
			}
			set
			{
				this.opcode = value;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x000D3B82 File Offset: 0x000D1D82
		// (set) Token: 0x06003619 RID: 13849 RVA: 0x000D3B8A File Offset: 0x000D1D8A
		internal EventKeywords Keywords { get; set; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x000D3B93 File Offset: 0x000D1D93
		// (set) Token: 0x0600361B RID: 13851 RVA: 0x000D3B9B File Offset: 0x000D1D9B
		internal EventTags Tags { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> class.</summary>
		// Token: 0x0600361C RID: 13852 RVA: 0x000D3BA4 File Offset: 0x000D1DA4
		[__DynamicallyInvokable]
		public EventDataAttribute()
		{
		}

		// Token: 0x04001825 RID: 6181
		private EventLevel level = (EventLevel)(-1);

		// Token: 0x04001826 RID: 6182
		private EventOpcode opcode = (EventOpcode)(-1);
	}
}
