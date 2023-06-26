using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000427 RID: 1063
	[AttributeUsage(AttributeTargets.Field)]
	internal class EventChannelAttribute : Attribute
	{
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x000CFF4A File Offset: 0x000CE14A
		// (set) Token: 0x0600355F RID: 13663 RVA: 0x000CFF52 File Offset: 0x000CE152
		public bool Enabled { get; set; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000CFF5B File Offset: 0x000CE15B
		// (set) Token: 0x06003561 RID: 13665 RVA: 0x000CFF63 File Offset: 0x000CE163
		public EventChannelType EventChannelType { get; set; }
	}
}
