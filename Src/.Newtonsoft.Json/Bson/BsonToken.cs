using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000107 RID: 263
	internal abstract class BsonToken
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000D84 RID: 3460
		public abstract BsonType Type { get; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x000367AE File Offset: 0x000349AE
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x000367B6 File Offset: 0x000349B6
		public BsonToken Parent { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x000367BF File Offset: 0x000349BF
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x000367C7 File Offset: 0x000349C7
		public int CalculatedSize { get; set; }
	}
}
