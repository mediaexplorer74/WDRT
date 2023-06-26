using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010B RID: 267
	internal class BsonValue : BsonToken
	{
		// Token: 0x06000D97 RID: 3479 RVA: 0x000368AA File Offset: 0x00034AAA
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x000368C0 File Offset: 0x00034AC0
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x000368C8 File Offset: 0x00034AC8
		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000423 RID: 1059
		private readonly object _value;

		// Token: 0x04000424 RID: 1060
		private readonly BsonType _type;
	}
}
