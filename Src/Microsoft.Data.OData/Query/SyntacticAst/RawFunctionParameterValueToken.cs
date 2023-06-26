using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000014 RID: 20
	internal sealed class RawFunctionParameterValueToken : QueryToken
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000368A File Offset: 0x0000188A
		public RawFunctionParameterValueToken(string rawText)
		{
			this.RawText = rawText;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003699 File Offset: 0x00001899
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000036A1 File Offset: 0x000018A1
		public string RawText { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000036AA File Offset: 0x000018AA
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.RawFunctionParameterValue;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000036AE File Offset: 0x000018AE
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
