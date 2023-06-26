using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query.Metadata
{
	// Token: 0x0200003F RID: 63
	internal static class UriEdmHelpers
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00006F58 File Offset: 0x00005158
		public static IEdmSchemaType FindTypeFromModel(IEdmModel model, string qualifiedName)
		{
			return model.FindType(qualifiedName);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006F64 File Offset: 0x00005164
		public static IEdmTypeReference FindCollectionTypeFromModel(IEdmModel model, string qualifiedName)
		{
			if (qualifiedName.StartsWith("Collection", StringComparison.Ordinal))
			{
				string[] array = qualifiedName.Split(new char[] { '(' });
				string text = array[1].Split(new char[] { ')' })[0];
				return EdmCoreModel.GetCollection(UriEdmHelpers.FindTypeFromModel(model, text).ToTypeReference());
			}
			return null;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006FBE File Offset: 0x000051BE
		public static IEdmTypeReference GetFunctionReturnType(IEdmFunctionImport serviceOperation)
		{
			return serviceOperation.ReturnType;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006FC6 File Offset: 0x000051C6
		public static IEdmEntityType GetEntitySetElementType(IEdmEntitySet entitySet)
		{
			return entitySet.ElementType;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006FCE File Offset: 0x000051CE
		public static IEdmTypeReference GetOperationParameterType(IEdmFunctionParameter serviceOperationParameter)
		{
			return serviceOperationParameter.Type;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006FD8 File Offset: 0x000051D8
		public static void CheckRelatedTo(IEdmType parentType, IEdmType childType)
		{
			IEdmEntityType edmEntityType = childType as IEdmEntityType;
			if (!edmEntityType.IsOrInheritsFrom(parentType) && !parentType.IsOrInheritsFrom(edmEntityType))
			{
				string text = ((parentType != null) ? parentType.ODataFullName() : "<null>");
				throw new ODataException(Strings.MetadataBinder_HierarchyNotFollowed(edmEntityType.FullName(), text));
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007024 File Offset: 0x00005224
		public static IEdmNavigationProperty GetNavigationPropertyFromExpandPath(ODataPath path)
		{
			NavigationPropertySegment navigationPropertySegment = null;
			foreach (ODataPathSegment odataPathSegment in path)
			{
				TypeSegment typeSegment = odataPathSegment as TypeSegment;
				navigationPropertySegment = odataPathSegment as NavigationPropertySegment;
				if (typeSegment == null && navigationPropertySegment == null)
				{
					throw new ODataException(Strings.ExpandItemBinder_TypeSegmentNotFollowedByPath);
				}
			}
			if (navigationPropertySegment == null)
			{
				throw new ODataException(Strings.ExpandItemBinder_TypeSegmentNotFollowedByPath);
			}
			return navigationPropertySegment.NavigationProperty;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000709C File Offset: 0x0000529C
		public static IEdmType GetMostDerivedTypeFromPath(ODataPath path, IEdmType startingType)
		{
			IEdmType edmType = startingType;
			foreach (ODataPathSegment odataPathSegment in path)
			{
				TypeSegment typeSegment = odataPathSegment as TypeSegment;
				if (typeSegment != null && typeSegment.EdmType.IsOrInheritsFrom(edmType))
				{
					edmType = typeSegment.EdmType;
				}
			}
			return edmType;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007100 File Offset: 0x00005300
		public static bool TryGetEntityContainer(string containerIdentifier, IEdmModel model, out IEdmEntityContainer entityContainer)
		{
			entityContainer = model.FindEntityContainer(containerIdentifier);
			return entityContainer != null;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007114 File Offset: 0x00005314
		public static bool IsEntityCollection(this IEdmTypeReference type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(type, "type");
			IEdmCollectionTypeReference edmCollectionTypeReference = type as IEdmCollectionTypeReference;
			return edmCollectionTypeReference != null && edmCollectionTypeReference.ElementType().IsEntity();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007144 File Offset: 0x00005344
		public static bool AllHaveEqualReturnTypeAndAttributes(this IList<IEdmFunctionImport> functionImports)
		{
			if (!functionImports.Any<IEdmFunctionImport>())
			{
				return true;
			}
			IEdmType edmType = ((functionImports[0].ReturnType == null) ? null : functionImports[0].ReturnType.Definition);
			bool isComposable = functionImports[0].IsComposable;
			bool isSideEffecting = functionImports[0].IsSideEffecting;
			foreach (IEdmFunctionImport edmFunctionImport in functionImports)
			{
				if (edmFunctionImport.IsComposable != isComposable)
				{
					return false;
				}
				if (edmFunctionImport.IsSideEffecting != isSideEffecting)
				{
					return false;
				}
				if (edmType != null)
				{
					if (edmFunctionImport.ReturnType.Definition.ODataFullName() != edmType.ODataFullName())
					{
						return false;
					}
				}
				else if (edmFunctionImport.ReturnType != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007224 File Offset: 0x00005424
		public static bool IsBindingTypeValid(IEdmType bindingType)
		{
			return bindingType == null || bindingType.IsEntityOrEntityCollectionType() || bindingType.IsODataComplexTypeKind();
		}
	}
}
