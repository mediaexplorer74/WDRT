using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200002B RID: 43
	internal sealed class FunctionCallBinder
	{
		// Token: 0x06000114 RID: 276 RVA: 0x000054C4 File Offset: 0x000036C4
		internal FunctionCallBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000054D4 File Offset: 0x000036D4
		internal static void TypePromoteArguments(FunctionSignature signature, List<QueryNode> argumentNodes)
		{
			for (int i = 0; i < argumentNodes.Count; i++)
			{
				SingleValueNode singleValueNode = (SingleValueNode)argumentNodes[i];
				IEdmTypeReference edmTypeReference = signature.ArgumentTypes[i];
				argumentNodes[i] = MetadataBindingUtils.ConvertToTypeIfNeeded(singleValueNode, edmTypeReference);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005518 File Offset: 0x00003718
		internal static IEdmTypeReference[] EnsureArgumentsAreSingleValue(string functionName, List<QueryNode> argumentNodes)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(functionName, "functionCallToken");
			ExceptionUtils.CheckArgumentNotNull<List<QueryNode>>(argumentNodes, "argumentNodes");
			IEdmTypeReference[] array = new IEdmTypeReference[argumentNodes.Count];
			for (int i = 0; i < argumentNodes.Count; i++)
			{
				SingleValueNode singleValueNode = argumentNodes[i] as SingleValueNode;
				if (singleValueNode == null)
				{
					throw new ODataException(Strings.MetadataBinder_FunctionArgumentNotSingleValue(functionName));
				}
				array[i] = singleValueNode.TypeReference;
			}
			return array;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000055A4 File Offset: 0x000037A4
		internal static FunctionSignatureWithReturnType MatchSignatureToBuiltInFunction(string functionName, IEdmTypeReference[] argumentTypes, FunctionSignatureWithReturnType[] signatures)
		{
			int argumentCount = argumentTypes.Length;
			FunctionSignatureWithReturnType functionSignatureWithReturnType;
			if (argumentTypes.All((IEdmTypeReference a) => a == null) && argumentCount > 0)
			{
				functionSignatureWithReturnType = signatures.FirstOrDefault((FunctionSignatureWithReturnType candidateFunction) => candidateFunction.ArgumentTypes.Count<IEdmTypeReference>() == argumentCount);
				if (functionSignatureWithReturnType == null)
				{
					throw new ODataException(Strings.FunctionCallBinder_CannotFindASuitableOverload(functionName, argumentTypes.Count<IEdmTypeReference>()));
				}
				functionSignatureWithReturnType = new FunctionSignatureWithReturnType(null, functionSignatureWithReturnType.ArgumentTypes);
			}
			else
			{
				functionSignatureWithReturnType = TypePromotionUtils.FindBestFunctionSignature(signatures, argumentTypes);
				if (functionSignatureWithReturnType == null)
				{
					throw new ODataException(Strings.MetadataBinder_NoApplicableFunctionFound(functionName, BuiltInFunctions.BuildFunctionSignatureListDescription(functionName, signatures)));
				}
			}
			return functionSignatureWithReturnType;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005650 File Offset: 0x00003850
		internal static FunctionSignatureWithReturnType[] GetBuiltInFunctionSignatures(string functionName)
		{
			FunctionSignatureWithReturnType[] array;
			if (!BuiltInFunctions.TryGetBuiltInFunction(functionName, out array))
			{
				throw new ODataException(Strings.MetadataBinder_UnknownFunction(functionName));
			}
			return array;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005684 File Offset: 0x00003884
		internal QueryNode BindFunctionCall(FunctionCallToken functionCallToken, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<FunctionCallToken>(functionCallToken, "functionCallToken");
			ExceptionUtils.CheckArgumentNotNull<string>(functionCallToken.Name, "functionCallToken.Name");
			QueryNode queryNode;
			if (functionCallToken.Source != null)
			{
				queryNode = this.bindMethod(functionCallToken.Source);
			}
			else
			{
				queryNode = NodeFactory.CreateRangeVariableReferenceNode(state.ImplicitRangeVariable);
			}
			QueryNode queryNode2;
			if (this.TryBindIdentifier(functionCallToken.Name, functionCallToken.Arguments, queryNode, state, out queryNode2))
			{
				return queryNode2;
			}
			if (this.TryBindIdentifier(functionCallToken.Name, functionCallToken.Arguments, null, state, out queryNode2))
			{
				return queryNode2;
			}
			List<QueryNode> list = new List<QueryNode>(functionCallToken.Arguments.Select((FunctionParameterToken ar) => this.bindMethod(ar)));
			return FunctionCallBinder.BindAsBuiltInFunction(functionCallToken, state, list);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000572F File Offset: 0x0000392F
		internal bool TryBindEndPathAsFunctionCall(EndPathToken endPathToken, QueryNode parent, BindingState state, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(endPathToken.Identifier, null, parent, state, out boundFunction);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005742 File Offset: 0x00003942
		internal bool TryBindInnerPathAsFunctionCall(InnerPathToken innerPathToken, QueryNode parent, BindingState state, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(innerPathToken.Identifier, null, parent, state, out boundFunction);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005755 File Offset: 0x00003955
		internal bool TryBindDottedIdentifierAsFunctionCall(DottedIdentifierToken dottedIdentifierToken, SingleValueNode parent, BindingState state, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(dottedIdentifierToken.Identifier, null, parent, state, out boundFunction);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005768 File Offset: 0x00003968
		private static QueryNode BindAsBuiltInFunction(FunctionCallToken functionCallToken, BindingState state, List<QueryNode> argumentNodes)
		{
			if (functionCallToken.Source != null)
			{
				throw new ODataException(Strings.FunctionCallBinder_BuiltInFunctionMustHaveHaveNullParent(functionCallToken.Name));
			}
			if (FunctionCallBinder.IsUnboundFunction(functionCallToken.Name))
			{
				return FunctionCallBinder.CreateUnboundFunctionNode(functionCallToken, argumentNodes, state);
			}
			FunctionSignatureWithReturnType[] builtInFunctionSignatures = FunctionCallBinder.GetBuiltInFunctionSignatures(functionCallToken.Name);
			IEdmTypeReference[] array = FunctionCallBinder.EnsureArgumentsAreSingleValue(functionCallToken.Name, argumentNodes);
			FunctionSignatureWithReturnType functionSignatureWithReturnType = FunctionCallBinder.MatchSignatureToBuiltInFunction(functionCallToken.Name, array, builtInFunctionSignatures);
			if (functionSignatureWithReturnType.ReturnType != null)
			{
				FunctionCallBinder.TypePromoteArguments(functionSignatureWithReturnType, argumentNodes);
			}
			IEdmTypeReference returnType = functionSignatureWithReturnType.ReturnType;
			return new SingleValueFunctionCallNode(functionCallToken.Name, new ReadOnlyCollection<QueryNode>(argumentNodes), returnType);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000580C File Offset: 0x00003A0C
		private bool TryBindIdentifier(string identifier, IEnumerable<FunctionParameterToken> arguments, QueryNode parent, BindingState state, out QueryNode boundFunction)
		{
			boundFunction = null;
			IEdmType edmType = null;
			SingleValueNode singleValueNode = parent as SingleValueNode;
			if (singleValueNode != null)
			{
				if (singleValueNode.TypeReference != null)
				{
					edmType = singleValueNode.TypeReference.Definition;
				}
			}
			else
			{
				CollectionNode collectionNode = parent as CollectionNode;
				if (collectionNode != null)
				{
					edmType = collectionNode.CollectionType.Definition;
				}
			}
			if (!UriEdmHelpers.IsBindingTypeValid(edmType))
			{
				return false;
			}
			List<FunctionParameterToken> list = ((arguments == null) ? new List<FunctionParameterToken>() : arguments.ToList<FunctionParameterToken>());
			IEdmFunctionImport edmFunctionImport;
			if (!FunctionOverloadResolver.ResolveFunctionsFromList(identifier, list.Select((FunctionParameterToken ar) => ar.ParameterName).ToList<string>(), edmType, state.Model, out edmFunctionImport))
			{
				return false;
			}
			if (singleValueNode != null && singleValueNode.TypeReference == null)
			{
				throw new ODataException(Strings.FunctionCallBinder_CallingFunctionOnOpenProperty(identifier));
			}
			if (edmFunctionImport.IsSideEffecting)
			{
				return false;
			}
			ICollection<FunctionParameterToken> collection;
			if (!FunctionParameterParser.TryParseFunctionParameters(list, state.Configuration, edmFunctionImport, out collection))
			{
				return false;
			}
			IEnumerable<QueryNode> enumerable = collection.Select((FunctionParameterToken p) => this.bindMethod(p));
			IEdmTypeReference returnType = edmFunctionImport.ReturnType;
			IEdmEntitySet edmEntitySet = null;
			SingleEntityNode singleEntityNode = parent as SingleEntityNode;
			if (singleEntityNode != null)
			{
				edmEntitySet = edmFunctionImport.GetTargetEntitySet(singleEntityNode.EntitySet, state.Model);
			}
			if (returnType.IsEntity())
			{
				boundFunction = new SingleEntityFunctionCallNode(identifier, new IEdmFunctionImport[] { edmFunctionImport }, enumerable, (IEdmEntityTypeReference)returnType.Definition.ToTypeReference(), edmEntitySet, parent);
			}
			else if (returnType.IsEntityCollection())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = (IEdmCollectionTypeReference)returnType;
				boundFunction = new EntityCollectionFunctionCallNode(identifier, new IEdmFunctionImport[] { edmFunctionImport }, enumerable, edmCollectionTypeReference, edmEntitySet, parent);
			}
			else if (returnType.IsCollection())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference2 = (IEdmCollectionTypeReference)returnType;
				boundFunction = new CollectionFunctionCallNode(identifier, new IEdmFunctionImport[] { edmFunctionImport }, enumerable, edmCollectionTypeReference2, parent);
			}
			else
			{
				boundFunction = new SingleValueFunctionCallNode(identifier, new IEdmFunctionImport[] { edmFunctionImport }, enumerable, returnType, parent);
			}
			return true;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000059E4 File Offset: 0x00003BE4
		private static bool IsUnboundFunction(string functionName)
		{
			return FunctionCallBinder.UnboundFunctionNames.Contains(functionName);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000059F4 File Offset: 0x00003BF4
		private static SingleValueNode CreateUnboundFunctionNode(FunctionCallToken functionCallToken, List<QueryNode> args, BindingState state)
		{
			IEdmTypeReference edmTypeReference = null;
			string name;
			if ((name = functionCallToken.Name) != null)
			{
				if (!(name == "isof"))
				{
					if (name == "cast")
					{
						edmTypeReference = FunctionCallBinder.ValidateAndBuildCastArgs(state, ref args);
						if (edmTypeReference.IsEntity())
						{
							IEdmEntityTypeReference edmEntityTypeReference = edmTypeReference.AsEntity();
							SingleEntityNode singleEntityNode = args.ElementAt(0) as SingleEntityNode;
							if (singleEntityNode != null)
							{
								return new SingleEntityFunctionCallNode(functionCallToken.Name, args, edmEntityTypeReference, singleEntityNode.EntitySet);
							}
						}
					}
				}
				else
				{
					edmTypeReference = FunctionCallBinder.ValidateAndBuildIsOfArgs(state, ref args);
				}
			}
			return new SingleValueFunctionCallNode(functionCallToken.Name, args, edmTypeReference);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005A7D File Offset: 0x00003C7D
		private static IEdmTypeReference ValidateAndBuildCastArgs(BindingState state, ref List<QueryNode> args)
		{
			return FunctionCallBinder.ValidateIsOfOrCast(state, true, ref args);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005A87 File Offset: 0x00003C87
		private static IEdmTypeReference ValidateAndBuildIsOfArgs(BindingState state, ref List<QueryNode> args)
		{
			return FunctionCallBinder.ValidateIsOfOrCast(state, false, ref args);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005A94 File Offset: 0x00003C94
		private static IEdmTypeReference ValidateIsOfOrCast(BindingState state, bool isCast, ref List<QueryNode> args)
		{
			if (args.Count != 1 && args.Count != 2)
			{
				throw new ODataErrorException(Strings.MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands(args.Count));
			}
			ConstantNode constantNode = args.Last<QueryNode>() as ConstantNode;
			IEdmTypeReference edmTypeReference = null;
			if (constantNode != null)
			{
				edmTypeReference = FunctionCallBinder.TryGetTypeReference(state.Model, constantNode.Value as string);
			}
			if (edmTypeReference == null)
			{
				throw new ODataException(Strings.MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument);
			}
			if (edmTypeReference.IsCollection())
			{
				throw new ODataException(Strings.MetadataBinder_CastOrIsOfCollectionsNotSupported);
			}
			if (args.Count == 1)
			{
				args = new List<QueryNode>
				{
					new EntityRangeVariableReferenceNode(state.ImplicitRangeVariable.Name, state.ImplicitRangeVariable as EntityRangeVariable),
					args[0]
				};
			}
			else if (!(args[0] is SingleValueNode))
			{
				throw new ODataException(Strings.MetadataBinder_CastOrIsOfCollectionsNotSupported);
			}
			if (isCast)
			{
				return edmTypeReference;
			}
			return EdmCoreModel.Instance.GetBoolean(true);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005B84 File Offset: 0x00003D84
		private static IEdmTypeReference TryGetTypeReference(IEdmModel model, string fullTypeName)
		{
			IEdmTypeReference edmTypeReference = UriEdmHelpers.FindTypeFromModel(model, fullTypeName).ToTypeReference();
			if (edmTypeReference == null)
			{
				return UriEdmHelpers.FindCollectionTypeFromModel(model, fullTypeName);
			}
			return edmTypeReference;
		}

		// Token: 0x0400005A RID: 90
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x0400005B RID: 91
		private static readonly string[] UnboundFunctionNames = new string[] { "cast", "isof" };
	}
}
