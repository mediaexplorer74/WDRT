using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000092 RID: 146
	internal sealed class ExpandToken : QueryToken
	{
		// Token: 0x06000375 RID: 885 RVA: 0x0000BA98 File Offset: 0x00009C98
		public ExpandToken(IEnumerable<ExpandTermToken> expandTerms)
		{
			this.expandTerms = new ReadOnlyEnumerableForUriParser<ExpandTermToken>(expandTerms ?? ((IEnumerable<ExpandTermToken>)new ExpandTermToken[0]));
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000BABB File Offset: 0x00009CBB
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Expand;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000BABF File Offset: 0x00009CBF
		public IEnumerable<ExpandTermToken> ExpandTerms
		{
			get
			{
				return this.expandTerms;
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000BAC7 File Offset: 0x00009CC7
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000107 RID: 263
		private readonly IEnumerable<ExpandTermToken> expandTerms;
	}
}
