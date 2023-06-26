using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000BB RID: 187
	internal sealed class StarToken : PathToken
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x0000EBF1 File Offset: 0x0000CDF1
		public StarToken(QueryToken nextToken)
		{
			this.nextToken = nextToken;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000EC00 File Offset: 0x0000CE00
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Star;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000EC04 File Offset: 0x0000CE04
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0000EC0C File Offset: 0x0000CE0C
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

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000EC15 File Offset: 0x0000CE15
		public override string Identifier
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000189 RID: 393
		private QueryToken nextToken;
	}
}
