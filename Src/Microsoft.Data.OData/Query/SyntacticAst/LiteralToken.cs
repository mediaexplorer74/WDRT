using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000CD RID: 205
	internal sealed class LiteralToken : QueryToken
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x000115D0 File Offset: 0x0000F7D0
		public LiteralToken(object value)
		{
			this.value = value;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000115DF File Offset: 0x0000F7DF
		internal LiteralToken(object value, string originalText)
			: this(value)
		{
			this.originalText = originalText;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000115EF File Offset: 0x0000F7EF
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Literal;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000115F2 File Offset: 0x0000F7F2
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x000115FA File Offset: 0x0000F7FA
		internal string OriginalText
		{
			get
			{
				return this.originalText;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00011602 File Offset: 0x0000F802
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040001DB RID: 475
		private readonly string originalText;

		// Token: 0x040001DC RID: 476
		private readonly object value;
	}
}
