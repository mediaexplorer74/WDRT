using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000257 RID: 599
	internal static class MetadataUtils
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x0004A170 File Offset: 0x00048370
		internal static bool TryGetODataAnnotation(this IEdmModel model, IEdmElement annotatable, string localName, out string value)
		{
			object annotationValue = model.GetAnnotationValue(annotatable, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", localName);
			if (annotationValue == null)
			{
				value = null;
				return false;
			}
			IEdmStringValue edmStringValue = annotationValue as IEdmStringValue;
			if (edmStringValue == null)
			{
				throw new ODataException(Strings.ODataAtomWriterMetadataUtils_InvalidAnnotationValue(localName, annotationValue.GetType().FullName));
			}
			value = edmStringValue.Value;
			return true;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0004A1C0 File Offset: 0x000483C0
		internal static void SetODataAnnotation(this IEdmModel model, IEdmElement annotatable, string localName, string value)
		{
			IEdmStringValue edmStringValue = null;
			if (value != null)
			{
				IEdmStringTypeReference @string = EdmCoreModel.Instance.GetString(true);
				edmStringValue = new EdmStringConstant(@string, value);
			}
			model.SetAnnotationValue(annotatable, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", localName, edmStringValue);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0004A208 File Offset: 0x00048408
		internal static IEnumerable<IEdmDirectValueAnnotation> GetODataAnnotations(this IEdmModel model, IEdmElement annotatable)
		{
			IEnumerable<IEdmDirectValueAnnotation> enumerable = model.DirectValueAnnotations(annotatable);
			if (enumerable == null)
			{
				return null;
			}
			return enumerable.Where((IEdmDirectValueAnnotation a) => a.NamespaceUri == "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0004A248 File Offset: 0x00048448
		internal static IEdmTypeReference GetEdmType(this ODataAnnotatable annotatable)
		{
			if (annotatable == null)
			{
				return null;
			}
			ODataTypeAnnotation annotation = annotatable.GetAnnotation<ODataTypeAnnotation>();
			if (annotation != null)
			{
				return annotation.Type;
			}
			return null;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0004A26C File Offset: 0x0004846C
		internal static IEdmType ResolveTypeNameForWrite(IEdmModel model, string typeName)
		{
			EdmTypeKind edmTypeKind;
			return MetadataUtils.ResolveTypeName(model, null, typeName, null, ODataVersion.V3, out edmTypeKind);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0004A288 File Offset: 0x00048488
		internal static IEdmType ResolveTypeNameForRead(IEdmModel model, IEdmType expectedType, string typeName, ODataReaderBehavior readerBehavior, ODataVersion version, out EdmTypeKind typeKind)
		{
			Func<IEdmType, string, IEdmType> func = ((readerBehavior == null) ? null : readerBehavior.TypeResolver);
			return MetadataUtils.ResolveTypeName(model, expectedType, typeName, func, version, out typeKind);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0004A2B0 File Offset: 0x000484B0
		internal static IEdmType ResolveTypeName(IEdmModel model, IEdmType expectedType, string typeName, Func<IEdmType, string, IEdmType> customTypeResolver, ODataVersion version, out EdmTypeKind typeKind)
		{
			IEdmType edmType = null;
			string text = ((version >= ODataVersion.V3) ? EdmLibraryExtensions.GetCollectionItemTypeName(typeName) : null);
			if (text == null)
			{
				if (customTypeResolver != null && model.IsUserModel())
				{
					edmType = customTypeResolver(expectedType, typeName);
					if (edmType == null)
					{
						throw new ODataException(Strings.MetadataUtils_ResolveTypeName(typeName));
					}
				}
				else
				{
					edmType = model.FindType(typeName);
				}
				if (version < ODataVersion.V3 && edmType != null && edmType.IsSpatial())
				{
					edmType = null;
				}
				typeKind = ((edmType == null) ? EdmTypeKind.None : edmType.TypeKind);
			}
			else
			{
				typeKind = EdmTypeKind.Collection;
				IEdmType edmType2 = null;
				if (customTypeResolver != null && expectedType != null && expectedType.TypeKind == EdmTypeKind.Collection)
				{
					edmType2 = ((IEdmCollectionType)expectedType).ElementType.Definition;
				}
				EdmTypeKind edmTypeKind;
				IEdmType edmType3 = MetadataUtils.ResolveTypeName(model, edmType2, text, customTypeResolver, version, out edmTypeKind);
				if (edmType3 != null)
				{
					edmType = EdmLibraryExtensions.GetCollectionType(edmType3);
				}
			}
			return edmType;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0004A364 File Offset: 0x00048564
		internal static IEdmFunctionImport[] CalculateAlwaysBindableOperationsForType(IEdmType bindingType, IEdmModel model, EdmTypeResolver edmTypeResolver)
		{
			List<IEdmFunctionImport> list = new List<IEdmFunctionImport>();
			foreach (IEdmEntityContainer edmEntityContainer in model.EntityContainers())
			{
				foreach (IEdmFunctionImport edmFunctionImport in edmEntityContainer.FunctionImports())
				{
					if (edmFunctionImport.IsBindable && model.IsAlwaysBindable(edmFunctionImport))
					{
						IEdmFunctionParameter edmFunctionParameter = edmFunctionImport.Parameters.FirstOrDefault<IEdmFunctionParameter>();
						if (edmFunctionParameter != null)
						{
							IEdmType definition = edmTypeResolver.GetParameterType(edmFunctionParameter).Definition;
							if (definition.IsAssignableFrom(bindingType))
							{
								list.Add(edmFunctionImport);
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0004A434 File Offset: 0x00048634
		internal static IEdmTypeReference LookupTypeOfValueTerm(string qualifiedTermName, IEdmModel model)
		{
			IEdmTypeReference edmTypeReference = null;
			IEdmValueTerm edmValueTerm = model.FindValueTerm(qualifiedTermName);
			if (edmValueTerm != null)
			{
				edmTypeReference = edmValueTerm.Type;
			}
			return edmTypeReference;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0004A456 File Offset: 0x00048656
		internal static IEdmTypeReference GetNullablePayloadTypeReference(IEdmType payloadType)
		{
			if (payloadType != null)
			{
				return payloadType.ToTypeReference(true);
			}
			return null;
		}
	}
}
