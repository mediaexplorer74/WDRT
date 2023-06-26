using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010D RID: 269
	internal class BsonString : BsonValue
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x000368F7 File Offset: 0x00034AF7
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x000368FF File Offset: 0x00034AFF
		public int ByteCount { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00036908 File Offset: 0x00034B08
		public bool IncludeLength { get; }

		// Token: 0x06000D9F RID: 3487 RVA: 0x00036910 File Offset: 0x00034B10
		public BsonString(object value, bool includeLength)
			: base(value, BsonType.String)
		{
			this.IncludeLength = includeLength;
		}
	}
}
