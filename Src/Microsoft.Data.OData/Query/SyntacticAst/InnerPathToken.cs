using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000A4 RID: 164
	internal sealed class InnerPathToken : PathToken
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000BE7E File Offset: 0x0000A07E
		public InnerPathToken(string identifier, QueryToken nextToken, IEnumerable<NamedValue> namedValues)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
			this.namedValues = ((namedValues == null) ? null : new ReadOnlyEnumerableForUriParser<NamedValue>(namedValues));
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000BEB1 File Offset: 0x0000A0B1
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.InnerPath;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000BEB5 File Offset: 0x0000A0B5
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000BEBD File Offset: 0x0000A0BD
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000BEC5 File Offset: 0x0000A0C5
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

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000BECE File Offset: 0x0000A0CE
		public IEnumerable<NamedValue> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000BED6 File Offset: 0x0000A0D6
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400012A RID: 298
		private readonly string identifier;

		// Token: 0x0400012B RID: 299
		private readonly IEnumerable<NamedValue> namedValues;

		// Token: 0x0400012C RID: 300
		private QueryToken nextToken;
	}
}
