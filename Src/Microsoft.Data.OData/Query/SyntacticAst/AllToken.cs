using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x0200009D RID: 157
	internal sealed class AllToken : LambdaToken
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x0000BDB9 File Offset: 0x00009FB9
		public AllToken(QueryToken expression, string parameter, QueryToken parent)
			: base(expression, parameter, parent)
		{
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.All;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000BDC8 File Offset: 0x00009FC8
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
