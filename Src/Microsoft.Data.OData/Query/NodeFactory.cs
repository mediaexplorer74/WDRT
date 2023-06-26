using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000043 RID: 67
	internal static class NodeFactory
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00007528 File Offset: 0x00005728
		internal static RangeVariable CreateImplicitRangeVariable(ODataPath path)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPath>(path, "path");
			IEdmTypeReference edmTypeReference = path.EdmType();
			if (edmTypeReference == null)
			{
				return null;
			}
			if (edmTypeReference.IsCollection())
			{
				edmTypeReference = edmTypeReference.AsCollection().ElementType();
			}
			if (edmTypeReference.IsEntity())
			{
				IEdmEntityTypeReference edmEntityTypeReference = edmTypeReference as IEdmEntityTypeReference;
				return new EntityRangeVariable("$it", edmEntityTypeReference, path.EntitySet());
			}
			return new NonentityRangeVariable("$it", edmTypeReference, null);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000758D File Offset: 0x0000578D
		internal static RangeVariable CreateImplicitRangeVariable(IEdmTypeReference elementType, IEdmEntitySet entitySet)
		{
			if (elementType.IsEntity())
			{
				return new EntityRangeVariable("$it", elementType as IEdmEntityTypeReference, entitySet);
			}
			return new NonentityRangeVariable("$it", elementType, null);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000075B8 File Offset: 0x000057B8
		internal static SingleValueNode CreateRangeVariableReferenceNode(RangeVariable rangeVariable)
		{
			if (rangeVariable.Kind == 1)
			{
				return new NonentityRangeVariableReferenceNode(rangeVariable.Name, (NonentityRangeVariable)rangeVariable);
			}
			EntityRangeVariable entityRangeVariable = (EntityRangeVariable)rangeVariable;
			return new EntityRangeVariableReferenceNode(entityRangeVariable.Name, entityRangeVariable);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000075F4 File Offset: 0x000057F4
		internal static RangeVariable CreateParameterNode(string parameter, CollectionNode nodeToIterateOver)
		{
			IEdmTypeReference itemType = nodeToIterateOver.ItemType;
			if (itemType.IsEntity())
			{
				EntityCollectionNode entityCollectionNode = nodeToIterateOver as EntityCollectionNode;
				return new EntityRangeVariable(parameter, itemType as IEdmEntityTypeReference, entityCollectionNode);
			}
			return new NonentityRangeVariable(parameter, itemType, null);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007630 File Offset: 0x00005830
		internal static LambdaNode CreateLambdaNode(BindingState state, CollectionNode parent, SingleValueNode lambdaExpression, RangeVariable newRangeVariable, QueryTokenKind queryTokenKind)
		{
			LambdaNode lambdaNode;
			if (queryTokenKind == QueryTokenKind.Any)
			{
				lambdaNode = new AnyNode(new Collection<RangeVariable>(state.RangeVariables.ToList<RangeVariable>()), newRangeVariable)
				{
					Body = lambdaExpression,
					Source = parent
				};
			}
			else
			{
				lambdaNode = new AllNode(new Collection<RangeVariable>(state.RangeVariables.ToList<RangeVariable>()), newRangeVariable)
				{
					Body = lambdaExpression,
					Source = parent
				};
			}
			return lambdaNode;
		}
	}
}
