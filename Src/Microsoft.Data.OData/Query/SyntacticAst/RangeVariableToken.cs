using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000B4 RID: 180
	internal sealed class RangeVariableToken : QueryToken
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x0000E92D File Offset: 0x0000CB2D
		public RangeVariableToken(string name)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "visitor");
			this.name = name;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000E947 File Offset: 0x0000CB47
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.RangeVariable;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000E94B File Offset: 0x0000CB4B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000E953 File Offset: 0x0000CB53
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400017E RID: 382
		private readonly string name;
	}
}
