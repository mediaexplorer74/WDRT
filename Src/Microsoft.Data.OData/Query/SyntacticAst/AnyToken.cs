using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x0200009F RID: 159
	internal sealed class AnyToken : LambdaToken
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0000BE0A File Offset: 0x0000A00A
		public AnyToken(QueryToken expression, string parameter, QueryToken parent)
			: base(expression, parameter, parent)
		{
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000BE15 File Offset: 0x0000A015
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Any;
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000BE19 File Offset: 0x0000A019
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
