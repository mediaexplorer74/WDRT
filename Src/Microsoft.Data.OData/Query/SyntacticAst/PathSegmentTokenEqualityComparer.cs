using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000056 RID: 86
	internal sealed class PathSegmentTokenEqualityComparer : EqualityComparer<PathSegmentToken>
	{
		// Token: 0x06000236 RID: 566 RVA: 0x00008607 File Offset: 0x00006807
		public override bool Equals(PathSegmentToken first, PathSegmentToken second)
		{
			return (first == null && second == null) || (first != null && second != null && this.ToHashableString(first) == this.ToHashableString(second));
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000862C File Offset: 0x0000682C
		public override int GetHashCode(PathSegmentToken path)
		{
			if (path == null)
			{
				return 0;
			}
			return this.ToHashableString(path).GetHashCode();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000863F File Offset: 0x0000683F
		private string ToHashableString(PathSegmentToken token)
		{
			if (token.NextToken == null)
			{
				return token.Identifier;
			}
			return token.Identifier + "/" + this.ToHashableString(token.NextToken);
		}
	}
}
