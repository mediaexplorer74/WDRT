using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000096 RID: 150
	internal sealed class SystemToken : PathSegmentToken
	{
		// Token: 0x0600038A RID: 906 RVA: 0x0000BB64 File Offset: 0x00009D64
		public SystemToken(string identifier, PathSegmentToken nextToken)
			: base(nextToken)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000BB7F File Offset: 0x00009D7F
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000BB87 File Offset: 0x00009D87
		public override bool IsNamespaceOrContainerQualified()
		{
			return false;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000BB8A File Offset: 0x00009D8A
		public override T Accept<T>(IPathSegmentTokenVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000BB9E File Offset: 0x00009D9E
		public override void Accept(IPathSegmentTokenVisitor visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0400010B RID: 267
		private readonly string identifier;
	}
}
