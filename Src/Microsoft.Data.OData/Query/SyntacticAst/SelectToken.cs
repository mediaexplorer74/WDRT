using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000B7 RID: 183
	internal sealed class SelectToken : QueryToken
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x0000EAE6 File Offset: 0x0000CCE6
		public SelectToken(IEnumerable<PathSegmentToken> properties)
		{
			this.properties = ((properties != null) ? new ReadOnlyEnumerableForUriParser<PathSegmentToken>(properties) : new ReadOnlyEnumerableForUriParser<PathSegmentToken>(new List<PathSegmentToken>()));
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000EB09 File Offset: 0x0000CD09
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Select;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000EB0D File Offset: 0x0000CD0D
		public IEnumerable<PathSegmentToken> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000EB15 File Offset: 0x0000CD15
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000181 RID: 385
		private readonly IEnumerable<PathSegmentToken> properties;
	}
}
