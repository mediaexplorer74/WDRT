using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000098 RID: 152
	internal static class SelectExpandPathBinder
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x0000BC38 File Offset: 0x00009E38
		public static IEnumerable<ODataPathSegment> FollowTypeSegments(PathSegmentToken firstTypeToken, IEdmModel model, int maxDepth, ref IEdmEntityType currentLevelEntityType, out PathSegmentToken firstNonTypeToken)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentToken>(firstTypeToken, "firstTypeToken");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (!firstTypeToken.IsNamespaceOrContainerQualified())
			{
				throw new ODataException(Strings.SelectExpandPathBinder_FollowNonTypeSegment(firstTypeToken.Identifier));
			}
			int num = 0;
			List<ODataPathSegment> list = new List<ODataPathSegment>();
			PathSegmentToken pathSegmentToken = firstTypeToken;
			while (pathSegmentToken.IsNamespaceOrContainerQualified() && pathSegmentToken.NextToken != null)
			{
				IEdmEntityType edmEntityType = currentLevelEntityType;
				currentLevelEntityType = UriEdmHelpers.FindTypeFromModel(model, pathSegmentToken.Identifier) as IEdmEntityType;
				if (currentLevelEntityType == null)
				{
					throw new ODataException(Strings.ExpandItemBinder_CannotFindType(pathSegmentToken.Identifier));
				}
				UriEdmHelpers.CheckRelatedTo(edmEntityType, currentLevelEntityType);
				list.Add(new TypeSegment(currentLevelEntityType, null));
				num++;
				pathSegmentToken = pathSegmentToken.NextToken;
				if (num >= maxDepth)
				{
					throw new ODataException(Strings.ExpandItemBinder_PathTooDeep);
				}
			}
			firstNonTypeToken = pathSegmentToken;
			return list;
		}
	}
}
