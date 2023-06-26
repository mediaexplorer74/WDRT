using System;

namespace System.Globalization
{
	// Token: 0x020003B3 RID: 947
	internal struct DaylightTimeStruct
	{
		// Token: 0x06002F5D RID: 12125 RVA: 0x000B7129 File Offset: 0x000B5329
		public DaylightTimeStruct(DateTime start, DateTime end, TimeSpan delta)
		{
			this.Start = start;
			this.End = end;
			this.Delta = delta;
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x000B7140 File Offset: 0x000B5340
		public DateTime Start { get; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002F5F RID: 12127 RVA: 0x000B7148 File Offset: 0x000B5348
		public DateTime End { get; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x000B7150 File Offset: 0x000B5350
		public TimeSpan Delta { get; }
	}
}
