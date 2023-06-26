using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000034 RID: 52
	internal sealed class LambdaBinder
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00006584 File Offset: 0x00004784
		internal LambdaBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000065A0 File Offset: 0x000047A0
		internal LambdaNode BindLambdaToken(LambdaToken lambdaToken, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<LambdaToken>(lambdaToken, "LambdaToken");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			CollectionNode collectionNode = this.BindParentToken(lambdaToken.Parent);
			RangeVariable rangeVariable = null;
			if (lambdaToken.Parameter != null)
			{
				rangeVariable = NodeFactory.CreateParameterNode(lambdaToken.Parameter, collectionNode);
				state.RangeVariables.Push(rangeVariable);
			}
			SingleValueNode singleValueNode = this.BindExpressionToken(lambdaToken.Expression);
			LambdaNode lambdaNode = NodeFactory.CreateLambdaNode(state, collectionNode, singleValueNode, rangeVariable, lambdaToken.Kind);
			if (rangeVariable != null)
			{
				state.RangeVariables.Pop();
			}
			return lambdaNode;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006620 File Offset: 0x00004820
		private CollectionNode BindParentToken(QueryToken queryToken)
		{
			QueryNode queryNode = this.bindMethod(queryToken);
			CollectionNode collectionNode = queryNode as CollectionNode;
			if (collectionNode != null)
			{
				return collectionNode;
			}
			if (!(queryNode is SingleValueOpenPropertyAccessNode))
			{
				throw new ODataException(Strings.MetadataBinder_LambdaParentMustBeCollection);
			}
			throw new ODataException(Strings.MetadataBinder_CollectionOpenPropertiesNotSupportedInThisRelease);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006668 File Offset: 0x00004868
		private SingleValueNode BindExpressionToken(QueryToken queryToken)
		{
			SingleValueNode singleValueNode = this.bindMethod(queryToken) as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_AnyAllExpressionNotSingleValue);
			}
			IEdmTypeReference edmTypeReference = singleValueNode.GetEdmTypeReference();
			if (edmTypeReference != null && !edmTypeReference.AsPrimitive().IsBoolean())
			{
				throw new ODataException(Strings.MetadataBinder_AnyAllExpressionNotSingleValue);
			}
			return singleValueNode;
		}

		// Token: 0x04000069 RID: 105
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
