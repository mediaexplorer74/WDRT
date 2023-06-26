using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000027 RID: 39
	internal static class ExpandTreeNormalizer
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00004CD0 File Offset: 0x00002ED0
		public static ExpandToken NormalizeExpandTree(ExpandToken treeToNormalize)
		{
			ExpandToken expandToken = ExpandTreeNormalizer.InvertPaths(treeToNormalize);
			return ExpandTreeNormalizer.CombineTerms(expandToken);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004CEC File Offset: 0x00002EEC
		public static ExpandToken InvertPaths(ExpandToken treeToInvert)
		{
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			foreach (ExpandTermToken expandTermToken in treeToInvert.ExpandTerms)
			{
				PathReverser pathReverser = new PathReverser();
				PathSegmentToken pathSegmentToken = expandTermToken.PathToNavProp.Accept<PathSegmentToken>(pathReverser);
				ExpandTermToken expandTermToken2 = new ExpandTermToken(pathSegmentToken, expandTermToken.FilterOption, expandTermToken.OrderByOption, expandTermToken.TopOption, expandTermToken.SkipOption, expandTermToken.InlineCountOption, expandTermToken.SelectOption, expandTermToken.ExpandOption);
				list.Add(expandTermToken2);
			}
			return new ExpandToken(list);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004D90 File Offset: 0x00002F90
		public static ExpandToken CombineTerms(ExpandToken treeToCollapse)
		{
			Dictionary<PathSegmentToken, ExpandTermToken> dictionary = new Dictionary<PathSegmentToken, ExpandTermToken>(new PathSegmentTokenEqualityComparer());
			foreach (ExpandTermToken expandTermToken in treeToCollapse.ExpandTerms)
			{
				ExpandTermToken expandTermToken2 = ExpandTreeNormalizer.BuildSubExpandTree(expandTermToken);
				ExpandTreeNormalizer.AddOrCombine(dictionary, expandTermToken2);
			}
			return new ExpandToken(dictionary.Values);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004DFC File Offset: 0x00002FFC
		public static ExpandTermToken BuildSubExpandTree(ExpandTermToken termToExpand)
		{
			if (termToExpand.PathToNavProp.NextToken == null)
			{
				return termToExpand;
			}
			PathSegmentToken pathToNavProp = termToExpand.PathToNavProp;
			PathSegmentToken pathSegmentToken = pathToNavProp;
			while (pathSegmentToken.IsNamespaceOrContainerQualified())
			{
				pathSegmentToken = pathSegmentToken.NextToken;
				if (pathSegmentToken == null)
				{
					throw new ODataException(Strings.ExpandTreeNormalizer_NonPathInPropertyChain);
				}
			}
			PathSegmentToken nextToken = pathSegmentToken.NextToken;
			pathSegmentToken.SetNextToken(null);
			ExpandToken expandToken;
			if (nextToken != null)
			{
				ExpandTermToken expandTermToken = new ExpandTermToken(nextToken, termToExpand.FilterOption, termToExpand.OrderByOption, termToExpand.TopOption, termToExpand.SkipOption, termToExpand.InlineCountOption, termToExpand.SelectOption, null);
				ExpandTermToken expandTermToken2 = ExpandTreeNormalizer.BuildSubExpandTree(expandTermToken);
				expandToken = new ExpandToken(new ExpandTermToken[] { expandTermToken2 });
			}
			else
			{
				expandToken = new ExpandToken(new ExpandTermToken[0]);
			}
			return new ExpandTermToken(pathToNavProp, termToExpand.FilterOption, termToExpand.OrderByOption, termToExpand.TopOption, termToExpand.SkipOption, termToExpand.InlineCountOption, termToExpand.SelectOption, expandToken);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004ED8 File Offset: 0x000030D8
		public static ExpandTermToken CombineTerms(ExpandTermToken existingToken, ExpandTermToken newToken)
		{
			List<ExpandTermToken> list = ExpandTreeNormalizer.CombineChildNodes(existingToken, newToken).ToList<ExpandTermToken>();
			return new ExpandTermToken(existingToken.PathToNavProp, existingToken.FilterOption, existingToken.OrderByOption, existingToken.TopOption, existingToken.SkipOption, existingToken.InlineCountOption, existingToken.SelectOption, new ExpandToken(list));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004F28 File Offset: 0x00003128
		public static IEnumerable<ExpandTermToken> CombineChildNodes(ExpandTermToken existingToken, ExpandTermToken newToken)
		{
			if (existingToken.ExpandOption == null && newToken.ExpandOption == null)
			{
				return new List<ExpandTermToken>();
			}
			Dictionary<PathSegmentToken, ExpandTermToken> dictionary = new Dictionary<PathSegmentToken, ExpandTermToken>(new PathSegmentTokenEqualityComparer());
			if (existingToken.ExpandOption != null)
			{
				ExpandTreeNormalizer.AddChildOptionsToDictionary(existingToken, dictionary);
			}
			if (newToken.ExpandOption != null)
			{
				ExpandTreeNormalizer.AddChildOptionsToDictionary(newToken, dictionary);
			}
			return dictionary.Values;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004F7C File Offset: 0x0000317C
		private static void AddChildOptionsToDictionary(ExpandTermToken newToken, Dictionary<PathSegmentToken, ExpandTermToken> combinedTerms)
		{
			foreach (ExpandTermToken expandTermToken in newToken.ExpandOption.ExpandTerms)
			{
				ExpandTreeNormalizer.AddOrCombine(combinedTerms, expandTermToken);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004FD0 File Offset: 0x000031D0
		private static void AddOrCombine(IDictionary<PathSegmentToken, ExpandTermToken> combinedTerms, ExpandTermToken expandedTerm)
		{
			ExpandTermToken expandTermToken;
			if (combinedTerms.TryGetValue(expandedTerm.PathToNavProp, out expandTermToken))
			{
				combinedTerms[expandedTerm.PathToNavProp] = ExpandTreeNormalizer.CombineTerms(expandedTerm, expandTermToken);
				return;
			}
			combinedTerms.Add(expandedTerm.PathToNavProp, expandedTerm);
		}
	}
}
