using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000C2 RID: 194
	internal sealed class DottedIdentifierToken : PathToken
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000FEF7 File Offset: 0x0000E0F7
		public DottedIdentifierToken(string identifier, QueryToken nextToken)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000FF18 File Offset: 0x0000E118
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.DottedIdentifier;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000FF1C File Offset: 0x0000E11C
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000FF24 File Offset: 0x0000E124
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0000FF2C File Offset: 0x0000E12C
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

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000FF35 File Offset: 0x0000E135
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400019A RID: 410
		private readonly string identifier;

		// Token: 0x0400019B RID: 411
		private QueryToken nextToken;
	}
}
