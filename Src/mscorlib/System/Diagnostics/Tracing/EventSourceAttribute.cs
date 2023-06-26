using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Allows the event tracing for Windows (ETW) name to be defined independently of the name of the event source class.</summary>
	// Token: 0x02000424 RID: 1060
	[AttributeUsage(AttributeTargets.Class)]
	[__DynamicallyInvokable]
	public sealed class EventSourceAttribute : Attribute
	{
		/// <summary>Gets or sets the name of the event source.</summary>
		/// <returns>The name of the event source.</returns>
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000CFE31 File Offset: 0x000CE031
		// (set) Token: 0x06003541 RID: 13633 RVA: 0x000CFE39 File Offset: 0x000CE039
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Gets or sets the event source identifier.</summary>
		/// <returns>The event source identifier.</returns>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x000CFE42 File Offset: 0x000CE042
		// (set) Token: 0x06003543 RID: 13635 RVA: 0x000CFE4A File Offset: 0x000CE04A
		[__DynamicallyInvokable]
		public string Guid
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Gets or sets the name of the localization resource file.</summary>
		/// <returns>The name of the localization resource file, or <see langword="null" /> if the localization resource file does not exist.</returns>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000CFE53 File Offset: 0x000CE053
		// (set) Token: 0x06003545 RID: 13637 RVA: 0x000CFE5B File Offset: 0x000CE05B
		[__DynamicallyInvokable]
		public string LocalizationResources
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> class.</summary>
		// Token: 0x06003546 RID: 13638 RVA: 0x000CFE64 File Offset: 0x000CE064
		[__DynamicallyInvokable]
		public EventSourceAttribute()
		{
		}
	}
}
