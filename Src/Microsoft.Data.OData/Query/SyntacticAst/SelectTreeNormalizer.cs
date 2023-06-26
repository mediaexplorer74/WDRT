using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000061 RID: 97
	internal static class SelectTreeNormalizer
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00009F9C File Offset: 0x0000819C
		public static SelectToken NormalizeSelectTree(SelectToken treeToNormalize)
		{
			PathReverser pathReverser = new PathReverser();
			List<PathSegmentToken> list = treeToNormalize.Properties.Select((PathSegmentToken property) => property.Accept<PathSegmentToken>(pathReverser)).ToList<PathSegmentToken>();
			return new SelectToken(list);
		}
	}
}
