using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000015 RID: 21
	internal sealed class FunctionParameterAliasToken : QueryToken
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000036B5 File Offset: 0x000018B5
		public FunctionParameterAliasToken(string alias)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.Alias = alias;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000036CF File Offset: 0x000018CF
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000036D7 File Offset: 0x000018D7
		public string Alias { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000036E0 File Offset: 0x000018E0
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionParameterAlias;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000036E4 File Offset: 0x000018E4
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
