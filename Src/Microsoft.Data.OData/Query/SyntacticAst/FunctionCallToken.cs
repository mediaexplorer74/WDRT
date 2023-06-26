using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000C9 RID: 201
	internal sealed class FunctionCallToken : QueryToken
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x00011448 File Offset: 0x0000F648
		public FunctionCallToken(string name, IEnumerable<QueryToken> argumentValues)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.name = name;
			IEnumerable<FunctionParameterToken> enumerable;
			if (argumentValues != null)
			{
				enumerable = new ReadOnlyEnumerableForUriParser<FunctionParameterToken>(argumentValues.Select((QueryToken v) => new FunctionParameterToken(null, v)));
			}
			else
			{
				enumerable = new ReadOnlyEnumerableForUriParser<FunctionParameterToken>(FunctionParameterToken.EmptyParameterList);
			}
			this.arguments = enumerable;
			this.source = null;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000114B1 File Offset: 0x0000F6B1
		public FunctionCallToken(string name, IEnumerable<FunctionParameterToken> arguments, QueryToken source)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.name = name;
			this.arguments = new ReadOnlyEnumerableForUriParser<FunctionParameterToken>(arguments ?? ((IEnumerable<FunctionParameterToken>)FunctionParameterToken.EmptyParameterList));
			this.source = source;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x000114EC File Offset: 0x0000F6EC
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionCall;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x000114EF File Offset: 0x0000F6EF
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x000114F7 File Offset: 0x0000F6F7
		public IEnumerable<FunctionParameterToken> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x000114FF File Offset: 0x0000F6FF
		public QueryToken Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00011507 File Offset: 0x0000F707
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040001CF RID: 463
		private readonly string name;

		// Token: 0x040001D0 RID: 464
		private readonly IEnumerable<FunctionParameterToken> arguments;

		// Token: 0x040001D1 RID: 465
		private readonly QueryToken source;
	}
}
