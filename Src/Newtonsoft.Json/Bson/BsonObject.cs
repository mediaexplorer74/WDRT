using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000108 RID: 264
	internal class BsonObject : BsonToken, IEnumerable<BsonProperty>, IEnumerable
	{
		// Token: 0x06000D8A RID: 3466 RVA: 0x000367D8 File Offset: 0x000349D8
		public void Add(string name, BsonToken token)
		{
			this._children.Add(new BsonProperty
			{
				Name = new BsonString(name, false),
				Value = token
			});
			token.Parent = this;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00036805 File Offset: 0x00034A05
		public override BsonType Type
		{
			get
			{
				return BsonType.Object;
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00036808 File Offset: 0x00034A08
		public IEnumerator<BsonProperty> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0003681A File Offset: 0x00034A1A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400041E RID: 1054
		private readonly List<BsonProperty> _children = new List<BsonProperty>();
	}
}
