using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000052 RID: 82
	internal sealed class OrderByBinder
	{
		// Token: 0x06000226 RID: 550 RVA: 0x0000828B File Offset: 0x0000648B
		internal OrderByBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000082A8 File Offset: 0x000064A8
		internal OrderByClause BindOrderBy(BindingState state, IEnumerable<OrderByToken> orderByTokens)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<OrderByToken>>(orderByTokens, "orderByTokens");
			OrderByClause orderByClause = null;
			foreach (OrderByToken orderByToken in orderByTokens.Reverse<OrderByToken>())
			{
				orderByClause = this.ProcessSingleOrderBy(state, orderByClause, orderByToken);
			}
			return orderByClause;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008314 File Offset: 0x00006514
		private OrderByClause ProcessSingleOrderBy(BindingState state, OrderByClause thenBy, OrderByToken orderByToken)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			ExceptionUtils.CheckArgumentNotNull<OrderByToken>(orderByToken, "orderByToken");
			QueryNode queryNode = this.bindMethod(orderByToken.Expression);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			if (singleValueNode == null || (singleValueNode.TypeReference != null && !singleValueNode.TypeReference.IsODataPrimitiveTypeKind()))
			{
				throw new ODataException(Strings.MetadataBinder_OrderByExpressionNotSingleValue);
			}
			return new OrderByClause(thenBy, singleValueNode, orderByToken.Direction, state.ImplicitRangeVariable);
		}

		// Token: 0x04000088 RID: 136
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
