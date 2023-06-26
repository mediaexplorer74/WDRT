using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010F RID: 271
	internal class BsonRegex : BsonToken
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00036943 File Offset: 0x00034B43
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x0003694B File Offset: 0x00034B4B
		public BsonString Pattern { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00036954 File Offset: 0x00034B54
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0003695C File Offset: 0x00034B5C
		public BsonString Options { get; set; }

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00036965 File Offset: 0x00034B65
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00036987 File Offset: 0x00034B87
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}
	}
}
