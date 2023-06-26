using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000016 RID: 22
	internal sealed class FunctionParameterToken : QueryToken
	{
		// Token: 0x06000082 RID: 130 RVA: 0x000036EB File Offset: 0x000018EB
		public FunctionParameterToken(string parameterName, QueryToken valueToken)
		{
			this.parameterName = parameterName;
			this.valueToken = valueToken;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003701 File Offset: 0x00001901
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003709 File Offset: 0x00001909
		public QueryToken ValueToken
		{
			get
			{
				return this.valueToken;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003711 File Offset: 0x00001911
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionParameter;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003715 File Offset: 0x00001915
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000034 RID: 52
		public static FunctionParameterToken[] EmptyParameterList = new FunctionParameterToken[0];

		// Token: 0x04000035 RID: 53
		private readonly string parameterName;

		// Token: 0x04000036 RID: 54
		private readonly QueryToken valueToken;
	}
}
