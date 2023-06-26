using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200002A RID: 42
	internal sealed class FilterBinder
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00005420 File Offset: 0x00003620
		internal FilterBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
		{
			this.bindMethod = bindMethod;
			this.state = state;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005438 File Offset: 0x00003638
		internal FilterClause BindFilter(QueryToken filter)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(filter, "filter");
			QueryNode queryNode = this.bindMethod(filter);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			if (singleValueNode == null || (singleValueNode.TypeReference != null && !singleValueNode.TypeReference.IsODataPrimitiveTypeKind()))
			{
				throw new ODataException(Strings.MetadataBinder_FilterExpressionNotSingleValue);
			}
			IEdmTypeReference typeReference = singleValueNode.TypeReference;
			if (typeReference != null)
			{
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
				if (edmPrimitiveTypeReference == null || edmPrimitiveTypeReference.PrimitiveKind() != EdmPrimitiveTypeKind.Boolean)
				{
					throw new ODataException(Strings.MetadataBinder_FilterExpressionNotSingleValue);
				}
			}
			return new FilterClause(singleValueNode, this.state.ImplicitRangeVariable);
		}

		// Token: 0x04000058 RID: 88
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x04000059 RID: 89
		private readonly BindingState state;
	}
}
