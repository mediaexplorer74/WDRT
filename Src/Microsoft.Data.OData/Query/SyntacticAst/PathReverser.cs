using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000055 RID: 85
	internal sealed class PathReverser : PathSegmentTokenVisitor<PathSegmentToken>
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00008520 File Offset: 0x00006720
		public PathReverser()
		{
			this.childToken = null;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000852F File Offset: 0x0000672F
		private PathReverser(PathSegmentToken childToken)
		{
			this.childToken = childToken;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008540 File Offset: 0x00006740
		public override PathSegmentToken Visit(NonSystemToken tokenIn)
		{
			ExceptionUtils.CheckArgumentNotNull<NonSystemToken>(tokenIn, "tokenIn");
			if (tokenIn.NextToken != null)
			{
				NonSystemToken nonSystemToken = new NonSystemToken(tokenIn.Identifier, tokenIn.NamedValues, this.childToken);
				return PathReverser.BuildNextStep(tokenIn.NextToken, nonSystemToken);
			}
			return new NonSystemToken(tokenIn.Identifier, tokenIn.NamedValues, this.childToken);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000859C File Offset: 0x0000679C
		public override PathSegmentToken Visit(SystemToken tokenIn)
		{
			ExceptionUtils.CheckArgumentNotNull<SystemToken>(tokenIn, "tokenIn");
			if (tokenIn.NextToken != null)
			{
				SystemToken systemToken = new SystemToken(tokenIn.Identifier, this.childToken);
				return PathReverser.BuildNextStep(tokenIn.NextToken, systemToken);
			}
			return new SystemToken(tokenIn.Identifier, this.childToken);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000085EC File Offset: 0x000067EC
		private static PathSegmentToken BuildNextStep(PathSegmentToken nextLevelToken, PathSegmentToken nextChildToken)
		{
			PathReverser pathReverser = new PathReverser(nextChildToken);
			return nextLevelToken.Accept<PathSegmentToken>(pathReverser);
		}

		// Token: 0x04000089 RID: 137
		private readonly PathSegmentToken childToken;
	}
}
