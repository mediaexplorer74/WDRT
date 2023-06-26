using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000110 RID: 272
	internal class BsonProperty
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0003698B File Offset: 0x00034B8B
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x00036993 File Offset: 0x00034B93
		public BsonString Name { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0003699C File Offset: 0x00034B9C
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x000369A4 File Offset: 0x00034BA4
		public BsonToken Value { get; set; }
	}
}
