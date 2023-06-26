using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010C RID: 268
	internal class BsonBoolean : BsonValue
	{
		// Token: 0x06000D9A RID: 3482 RVA: 0x000368D0 File Offset: 0x00034AD0
		private BsonBoolean(bool value)
			: base(value, BsonType.Boolean)
		{
		}

		// Token: 0x04000425 RID: 1061
		public static readonly BsonBoolean False = new BsonBoolean(false);

		// Token: 0x04000426 RID: 1062
		public static readonly BsonBoolean True = new BsonBoolean(true);
	}
}
