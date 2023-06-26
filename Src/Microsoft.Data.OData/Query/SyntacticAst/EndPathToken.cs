using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000D2 RID: 210
	internal sealed class EndPathToken : PathToken
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x00011A83 File Offset: 0x0000FC83
		public EndPathToken(string identifier, QueryToken nextToken)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.EndPath;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00011AA7 File Offset: 0x0000FCA7
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00011AAF File Offset: 0x0000FCAF
		public override QueryToken NextToken
		{
			get
			{
				return this.nextToken;
			}
			set
			{
				this.nextToken = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00011AB8 File Offset: 0x0000FCB8
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00011AC0 File Offset: 0x0000FCC0
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040001E3 RID: 483
		private readonly string identifier;

		// Token: 0x040001E4 RID: 484
		private QueryToken nextToken;
	}
}
