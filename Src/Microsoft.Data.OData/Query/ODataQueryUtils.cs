using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.Metadata;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000A5 RID: 165
	internal static class ODataQueryUtils
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x0000BEE0 File Offset: 0x0000A0E0
		public static bool GetCanReflectOnInstanceTypeProperty(this IEdmProperty property, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ODataQueryEdmPropertyAnnotation annotationValue = model.GetAnnotationValue(property);
			return annotationValue != null && annotationValue.CanReflectOnProperty;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000BF18 File Offset: 0x0000A118
		public static void SetCanReflectOnInstanceTypeProperty(this IEdmProperty property, IEdmModel model, bool canReflect)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ODataQueryEdmPropertyAnnotation odataQueryEdmPropertyAnnotation = model.GetAnnotationValue(property);
			if (odataQueryEdmPropertyAnnotation == null)
			{
				if (canReflect)
				{
					odataQueryEdmPropertyAnnotation = new ODataQueryEdmPropertyAnnotation
					{
						CanReflectOnProperty = true
					};
					model.SetAnnotationValue(property, odataQueryEdmPropertyAnnotation);
					return;
				}
			}
			else
			{
				odataQueryEdmPropertyAnnotation.CanReflectOnProperty = canReflect;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000BF68 File Offset: 0x0000A168
		public static ODataServiceOperationResultKind? GetServiceOperationResultKind(this IEdmFunctionImport serviceOperation, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmFunctionImport>(serviceOperation, "functionImport");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ODataQueryEdmServiceOperationAnnotation annotationValue = model.GetAnnotationValue(serviceOperation);
			if (annotationValue != null)
			{
				return new ODataServiceOperationResultKind?(annotationValue.ResultKind);
			}
			return null;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		public static void SetServiceOperationResultKind(this IEdmFunctionImport serviceOperation, IEdmModel model, ODataServiceOperationResultKind resultKind)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmFunctionImport>(serviceOperation, "serviceOperation");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ODataQueryEdmServiceOperationAnnotation annotationValue = model.GetAnnotationValue(serviceOperation);
			if (annotationValue == null)
			{
				ODataQueryEdmServiceOperationAnnotation odataQueryEdmServiceOperationAnnotation = new ODataQueryEdmServiceOperationAnnotation
				{
					ResultKind = resultKind
				};
				model.SetAnnotationValue(serviceOperation, odataQueryEdmServiceOperationAnnotation);
				return;
			}
			annotationValue.ResultKind = resultKind;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		public static IEdmFunctionImport ResolveServiceOperation(this IEdmModel model, string operationName)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			IEdmFunctionImport edmFunctionImport = model.TryResolveServiceOperation(operationName);
			if (edmFunctionImport == null)
			{
				throw new ODataException(Strings.ODataQueryUtils_DidNotFindServiceOperation(operationName));
			}
			return edmFunctionImport;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000C038 File Offset: 0x0000A238
		public static IEdmFunctionImport TryResolveServiceOperation(this IEdmModel model, string operationName)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			IEdmFunctionImport edmFunctionImport = null;
			foreach (IEdmFunctionImport edmFunctionImport2 in model.ResolveFunctionImports(operationName))
			{
				if (model.IsServiceOperation(edmFunctionImport2))
				{
					if (edmFunctionImport != null)
					{
						throw new ODataException(Strings.ODataQueryUtils_FoundMultipleServiceOperations(operationName));
					}
					edmFunctionImport = edmFunctionImport2;
				}
			}
			return edmFunctionImport;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
		public static Type GetInstanceType(this IEdmTypeReference typeReference, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (typeReference.TypeKind() == EdmTypeKind.Primitive)
			{
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitive();
				return EdmLibraryExtensions.GetPrimitiveClrType(edmPrimitiveTypeReference);
			}
			ODataQueryEdmTypeAnnotation annotationValue = model.GetAnnotationValue(typeReference.Definition);
			if (annotationValue != null)
			{
				return annotationValue.InstanceType;
			}
			return null;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000C108 File Offset: 0x0000A308
		public static Type GetInstanceType(this IEdmType type, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (type.TypeKind == EdmTypeKind.Primitive)
			{
				return EdmLibraryExtensions.GetPrimitiveClrType((IEdmPrimitiveTypeReference)type.ToTypeReference(false));
			}
			ODataQueryEdmTypeAnnotation annotationValue = model.GetAnnotationValue(type);
			if (annotationValue != null)
			{
				return annotationValue.InstanceType;
			}
			return null;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000C15C File Offset: 0x0000A35C
		public static void SetInstanceType(this IEdmType type, IEdmModel model, Type instanceType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (type.TypeKind == EdmTypeKind.Primitive)
			{
				throw new ODataException(Strings.ODataQueryUtils_CannotSetMetadataAnnotationOnPrimitiveType);
			}
			ODataQueryEdmTypeAnnotation annotationValue = model.GetAnnotationValue(type);
			if (annotationValue == null)
			{
				if (instanceType != null)
				{
					ODataQueryEdmTypeAnnotation odataQueryEdmTypeAnnotation = new ODataQueryEdmTypeAnnotation
					{
						InstanceType = instanceType
					};
					model.SetAnnotationValue(type, odataQueryEdmTypeAnnotation);
					return;
				}
			}
			else
			{
				annotationValue.InstanceType = instanceType;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		public static bool GetCanReflectOnInstanceType(this IEdmTypeReference typeReference, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (typeReference.TypeKind() == EdmTypeKind.Primitive)
			{
				return true;
			}
			ODataQueryEdmTypeAnnotation annotationValue = model.GetAnnotationValue(typeReference.Definition);
			return annotationValue != null && annotationValue.CanReflectOnInstanceType;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000C20E File Offset: 0x0000A40E
		public static void SetCanReflectOnInstanceType(this IEdmTypeReference typeReference, IEdmModel model, bool canReflect)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			typeReference.Definition.SetCanReflectOnInstanceType(model, canReflect);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000C234 File Offset: 0x0000A434
		public static void SetCanReflectOnInstanceType(this IEdmType type, IEdmModel model, bool canReflect)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (type.TypeKind == EdmTypeKind.Primitive)
			{
				throw new ODataException(Strings.ODataQueryUtils_CannotSetMetadataAnnotationOnPrimitiveType);
			}
			ODataQueryEdmTypeAnnotation odataQueryEdmTypeAnnotation = model.GetAnnotationValue(type);
			if (odataQueryEdmTypeAnnotation == null)
			{
				if (canReflect)
				{
					odataQueryEdmTypeAnnotation = new ODataQueryEdmTypeAnnotation
					{
						CanReflectOnInstanceType = true
					};
					model.SetAnnotationValue(type, odataQueryEdmTypeAnnotation);
					return;
				}
			}
			else
			{
				odataQueryEdmTypeAnnotation.CanReflectOnInstanceType = canReflect;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000C298 File Offset: 0x0000A498
		public static IEdmEntitySet ResolveEntitySet(this IEdmModel model, string entitySetName)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			IEdmEntitySet edmEntitySet = model.TryResolveEntitySet(entitySetName);
			if (edmEntitySet == null)
			{
				throw new ODataException(Strings.ODataQueryUtils_DidNotFindEntitySet(entitySetName));
			}
			return edmEntitySet;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
		public static IEdmEntitySet TryResolveEntitySet(this IEdmModel model, string entitySetName)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			IEnumerable<IEdmEntityContainer> enumerable = model.EntityContainers();
			if (enumerable == null)
			{
				return null;
			}
			IEdmEntitySet edmEntitySet = null;
			foreach (IEdmEntityContainer edmEntityContainer in enumerable)
			{
				edmEntitySet = edmEntityContainer.FindEntitySet(entitySetName);
				if (edmEntitySet != null)
				{
					break;
				}
			}
			return edmEntitySet;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000C348 File Offset: 0x0000A548
		internal static bool IsServiceOperation(this IEdmModel model, IEdmFunctionImport functionImport)
		{
			return model.GetHttpMethod(functionImport) != null;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000C357 File Offset: 0x0000A557
		internal static bool IsAction(this IEdmModel model, IEdmFunctionImport functionImport)
		{
			return !functionImport.IsComposable && functionImport.IsSideEffecting && !model.IsServiceOperation(functionImport);
		}
	}
}
