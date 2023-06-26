using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000109 RID: 265
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x00036835 File Offset: 0x00034A35
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0003684A File Offset: 0x00034A4A
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003684D File Offset: 0x00034A4D
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0003685F File Offset: 0x00034A5F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400041F RID: 1055
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
