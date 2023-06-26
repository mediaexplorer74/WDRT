using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000093 RID: 147
	internal abstract class PathSegmentToken : ODataAnnotatable
	{
		// Token: 0x06000379 RID: 889 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		protected PathSegmentToken(PathSegmentToken nextToken)
		{
			this.nextToken = nextToken;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000BADF File Offset: 0x00009CDF
		public PathSegmentToken NextToken
		{
			get
			{
				return this.nextToken;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600037B RID: 891
		public abstract string Identifier { get; }

		// Token: 0x0600037C RID: 892
		public abstract bool IsNamespaceOrContainerQualified();

		// Token: 0x0600037D RID: 893
		public abstract T Accept<T>(IPathSegmentTokenVisitor<T> visitor);

		// Token: 0x0600037E RID: 894
		public abstract void Accept(IPathSegmentTokenVisitor visitor);

		// Token: 0x0600037F RID: 895 RVA: 0x0000BAE7 File Offset: 0x00009CE7
		internal void SetNextToken(PathSegmentToken nextTokenIn)
		{
			this.nextToken = nextTokenIn;
		}

		// Token: 0x04000108 RID: 264
		private PathSegmentToken nextToken;
	}
}
