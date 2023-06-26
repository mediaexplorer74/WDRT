using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200000D RID: 13
	internal sealed class DottedIdentifierBinder
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002A73 File Offset: 0x00000C73
		internal DottedIdentifierBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A90 File Offset: 0x00000C90
		internal QueryNode BindDottedIdentifier(DottedIdentifierToken dottedIdentifierToken, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<DottedIdentifierToken>(dottedIdentifierToken, "castToken");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			QueryNode queryNode;
			IEdmType edmType;
			if (dottedIdentifierToken.NextToken == null)
			{
				queryNode = NodeFactory.CreateRangeVariableReferenceNode(state.ImplicitRangeVariable);
				edmType = state.ImplicitRangeVariable.TypeReference.Definition;
			}
			else
			{
				queryNode = this.bindMethod(dottedIdentifierToken.NextToken);
				edmType = queryNode.GetEdmType();
			}
			SingleEntityNode singleEntityNode = queryNode as SingleEntityNode;
			IEdmSchemaType edmSchemaType = UriEdmHelpers.FindTypeFromModel(state.Model, dottedIdentifierToken.Identifier);
			IEdmEntityType edmEntityType = edmSchemaType as IEdmEntityType;
			if (edmEntityType == null)
			{
				FunctionCallBinder functionCallBinder = new FunctionCallBinder(this.bindMethod);
				QueryNode queryNode2;
				if (functionCallBinder.TryBindDottedIdentifierAsFunctionCall(dottedIdentifierToken, singleEntityNode, state, out queryNode2))
				{
					return queryNode2;
				}
				throw new ODataException(Strings.CastBinder_ChildTypeIsNotEntity(dottedIdentifierToken.Identifier));
			}
			else
			{
				UriEdmHelpers.CheckRelatedTo(edmType, edmSchemaType);
				EntityCollectionNode entityCollectionNode = queryNode as EntityCollectionNode;
				if (entityCollectionNode != null)
				{
					return new EntityCollectionCastNode(entityCollectionNode, edmEntityType);
				}
				if (queryNode == null)
				{
					return new SingleEntityCastNode(null, edmEntityType);
				}
				return new SingleEntityCastNode(singleEntityNode, edmEntityType);
			}
		}

		// Token: 0x04000019 RID: 25
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
