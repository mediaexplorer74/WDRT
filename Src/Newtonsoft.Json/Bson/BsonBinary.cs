using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010E RID: 270
	internal class BsonBinary : BsonValue
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00036921 File Offset: 0x00034B21
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x00036929 File Offset: 0x00034B29
		public BsonBinaryType BinaryType { get; set; }

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00036932 File Offset: 0x00034B32
		public BsonBinary(byte[] value, BsonBinaryType binaryType)
			: base(value, BsonType.Binary)
		{
			this.BinaryType = binaryType;
		}
	}
}
