using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>The <see cref="T:System.Diagnostics.Tracing.EventFieldAttribute" /> is placed on fields of user-defined types that are passed as <see cref="T:System.Diagnostics.Tracing.EventSource" /> payloads.</summary>
	// Token: 0x02000441 RID: 1089
	[AttributeUsage(AttributeTargets.Property)]
	[__DynamicallyInvokable]
	public class EventFieldAttribute : Attribute
	{
		/// <summary>Gets or sets the user-defined <see cref="T:System.Diagnostics.Tracing.EventFieldTags" /> value that is required for fields that contain data that isn't one of the supported types.</summary>
		/// <returns>Returns <see cref="T:System.Diagnostics.Tracing.EventFieldTags" />.</returns>
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600361D RID: 13853 RVA: 0x000D3BBA File Offset: 0x000D1DBA
		// (set) Token: 0x0600361E RID: 13854 RVA: 0x000D3BC2 File Offset: 0x000D1DC2
		[__DynamicallyInvokable]
		public EventFieldTags Tags
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600361F RID: 13855 RVA: 0x000D3BCB File Offset: 0x000D1DCB
		// (set) Token: 0x06003620 RID: 13856 RVA: 0x000D3BD3 File Offset: 0x000D1DD3
		internal string Name { get; set; }

		/// <summary>Gets or sets the value that specifies how to format the value of a user-defined type.</summary>
		/// <returns>The value that specifies how to format the value of a user-defined type.</returns>
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x000D3BDC File Offset: 0x000D1DDC
		// (set) Token: 0x06003622 RID: 13858 RVA: 0x000D3BE4 File Offset: 0x000D1DE4
		[__DynamicallyInvokable]
		public EventFieldFormat Format
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventFieldAttribute" /> class.</summary>
		// Token: 0x06003623 RID: 13859 RVA: 0x000D3BED File Offset: 0x000D1DED
		[__DynamicallyInvokable]
		public EventFieldAttribute()
		{
		}
	}
}
