using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000252 RID: 594
	public static class ODataUtils
	{
		// Token: 0x06001372 RID: 4978 RVA: 0x00049575 File Offset: 0x00047775
		public static ODataFormat SetHeadersForPayload(ODataMessageWriter messageWriter, ODataPayloadKind payloadKind)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriter>(messageWriter, "messageWriter");
			if (payloadKind == ODataPayloadKind.Unsupported)
			{
				throw new ArgumentException(Strings.ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind(payloadKind), "payloadKind");
			}
			return messageWriter.SetHeaders(payloadKind);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x000495A7 File Offset: 0x000477A7
		public static ODataFormat GetReadFormat(ODataMessageReader messageReader)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReader>(messageReader, "messageReader");
			return messageReader.GetFormat();
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x000495BA File Offset: 0x000477BA
		public static void LoadODataAnnotations(this IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			model.LoadODataAnnotations(100);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000495D0 File Offset: 0x000477D0
		public static void LoadODataAnnotations(this IEdmModel model, int maxEntityPropertyMappingsPerType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			foreach (IEdmEntityType edmEntityType in model.EntityTypes())
			{
				model.LoadODataAnnotations(edmEntityType, maxEntityPropertyMappingsPerType);
			}
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0004962C File Offset: 0x0004782C
		public static void LoadODataAnnotations(this IEdmModel model, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			model.LoadODataAnnotations(entityType, 100);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0004964D File Offset: 0x0004784D
		public static void LoadODataAnnotations(this IEdmModel model, IEdmEntityType entityType, int maxEntityPropertyMappingsPerType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			model.ClearInMemoryEpmAnnotations(entityType);
			model.EnsureEpmCache(entityType, maxEntityPropertyMappingsPerType);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00049678 File Offset: 0x00047878
		public static void SaveODataAnnotations(this IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (!model.IsUserModel())
			{
				throw new ODataException(Strings.ODataUtils_CannotSaveAnnotationsToBuiltInModel);
			}
			foreach (IEdmEntityType edmEntityType in model.EntityTypes())
			{
				ODataUtils.SaveODataAnnotationsImplementation(model, edmEntityType);
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x000496E4 File Offset: 0x000478E4
		public static void SaveODataAnnotations(this IEdmModel model, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			ODataUtils.SaveODataAnnotationsImplementation(model, entityType);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00049704 File Offset: 0x00047904
		public static bool HasDefaultStream(this IEdmModel model, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			bool flag;
			return ODataUtils.TryGetBooleanAnnotation(model, entityType, "HasStream", true, out flag) && flag;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0004973B File Offset: 0x0004793B
		public static void SetHasDefaultStream(this IEdmModel model, IEdmEntityType entityType, bool hasStream)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			ODataUtils.SetBooleanAnnotation(model, entityType, "HasStream", hasStream);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00049760 File Offset: 0x00047960
		public static bool IsDefaultEntityContainer(this IEdmModel model, IEdmEntityContainer entityContainer)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityContainer>(entityContainer, "entityContainer");
			bool flag;
			return ODataUtils.TryGetBooleanAnnotation(model, entityContainer, "IsDefaultEntityContainer", out flag) && flag;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00049796 File Offset: 0x00047996
		public static void SetIsDefaultEntityContainer(this IEdmModel model, IEdmEntityContainer entityContainer, bool isDefaultContainer)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityContainer>(entityContainer, "entityContainer");
			ODataUtils.SetBooleanAnnotation(model, entityContainer, "IsDefaultEntityContainer", isDefaultContainer);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000497BC File Offset: 0x000479BC
		public static string GetMimeType(this IEdmModel model, IEdmElement annotatable)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmElement>(annotatable, "annotatable");
			string text;
			if (!model.TryGetODataAnnotation(annotatable, "MimeType", out text))
			{
				return null;
			}
			if (text == null)
			{
				throw new ODataException(Strings.ODataUtils_NullValueForMimeTypeAnnotation);
			}
			return text;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00049800 File Offset: 0x00047A00
		public static void SetMimeType(this IEdmModel model, IEdmElement annotatable, string mimeType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmElement>(annotatable, "annotatable");
			model.SetODataAnnotation(annotatable, "MimeType", mimeType);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00049828 File Offset: 0x00047A28
		public static string GetHttpMethod(this IEdmModel model, IEdmElement annotatable)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmElement>(annotatable, "annotatable");
			string text;
			if (!model.TryGetODataAnnotation(annotatable, "HttpMethod", out text))
			{
				return null;
			}
			if (text == null)
			{
				throw new ODataException(Strings.ODataUtils_NullValueForHttpMethodAnnotation);
			}
			return text;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0004986C File Offset: 0x00047A6C
		public static void SetHttpMethod(this IEdmModel model, IEdmElement annotatable, string httpMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmElement>(annotatable, "annotatable");
			model.SetODataAnnotation(annotatable, "HttpMethod", httpMethod);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00049894 File Offset: 0x00047A94
		public static bool IsAlwaysBindable(this IEdmModel model, IEdmFunctionImport functionImport)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmFunctionImport>(functionImport, "functionImport");
			bool flag;
			if (!ODataUtils.TryGetBooleanAnnotation(model, functionImport, "IsAlwaysBindable", out flag))
			{
				return false;
			}
			if (!functionImport.IsBindable && flag)
			{
				throw new ODataException(Strings.ODataUtils_UnexpectedIsAlwaysBindableAnnotationInANonBindableFunctionImport);
			}
			return flag;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000498E0 File Offset: 0x00047AE0
		public static void SetIsAlwaysBindable(this IEdmModel model, IEdmFunctionImport functionImport, bool isAlwaysBindable)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmFunctionImport>(functionImport, "functionImport");
			if (!functionImport.IsBindable && isAlwaysBindable)
			{
				throw new ODataException(Strings.ODataUtils_IsAlwaysBindableAnnotationSetForANonBindableFunctionImport);
			}
			ODataUtils.SetBooleanAnnotation(model, functionImport, "IsAlwaysBindable", isAlwaysBindable);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0004991C File Offset: 0x00047B1C
		public static ODataNullValueBehaviorKind NullValueReadBehaviorKind(this IEdmModel model, IEdmProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			ODataEdmPropertyAnnotation annotationValue = model.GetAnnotationValue(property);
			if (annotationValue != null)
			{
				return annotationValue.NullValueReadBehaviorKind;
			}
			return ODataNullValueBehaviorKind.Default;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00049954 File Offset: 0x00047B54
		public static void SetNullValueReaderBehavior(this IEdmModel model, IEdmProperty property, ODataNullValueBehaviorKind nullValueReadBehaviorKind)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			ODataEdmPropertyAnnotation odataEdmPropertyAnnotation = model.GetAnnotationValue(property);
			if (odataEdmPropertyAnnotation == null)
			{
				if (nullValueReadBehaviorKind != ODataNullValueBehaviorKind.Default)
				{
					odataEdmPropertyAnnotation = new ODataEdmPropertyAnnotation
					{
						NullValueReadBehaviorKind = nullValueReadBehaviorKind
					};
					model.SetAnnotationValue(property, odataEdmPropertyAnnotation);
					return;
				}
			}
			else
			{
				odataEdmPropertyAnnotation.NullValueReadBehaviorKind = nullValueReadBehaviorKind;
			}
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x000499A4 File Offset: 0x00047BA4
		public static string ODataVersionToString(ODataVersion version)
		{
			switch (version)
			{
			case ODataVersion.V1:
				return "1.0";
			case ODataVersion.V2:
				return "2.0";
			case ODataVersion.V3:
				return "3.0";
			default:
				throw new ODataException(Strings.ODataUtils_UnsupportedVersionNumber);
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000499E4 File Offset: 0x00047BE4
		public static ODataVersion StringToODataVersion(string version)
		{
			string text = version;
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(version, "version");
			int num = text.IndexOf(';');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			string text2;
			if ((text2 = text.Trim()) != null)
			{
				if (text2 == "1.0")
				{
					return ODataVersion.V1;
				}
				if (text2 == "2.0")
				{
					return ODataVersion.V2;
				}
				if (text2 == "3.0")
				{
					return ODataVersion.V3;
				}
			}
			throw new ODataException(Strings.ODataUtils_UnsupportedVersionHeader(version));
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00049A58 File Offset: 0x00047C58
		public static Func<string, bool> CreateAnnotationFilter(string annotationFilter)
		{
			AnnotationFilter annotationFilter2 = AnnotationFilter.Create(annotationFilter);
			return new Func<string, bool>(annotationFilter2.Matches);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00049A7C File Offset: 0x00047C7C
		private static void SaveODataAnnotationsImplementation(IEdmModel model, IEdmEntityType entityType)
		{
			ODataEntityPropertyMappingCache odataEntityPropertyMappingCache = model.EnsureEpmCache(entityType, int.MaxValue);
			if (odataEntityPropertyMappingCache != null)
			{
				model.SaveEpmAnnotations(entityType, odataEntityPropertyMappingCache.MappingsForInheritedProperties, false, false);
				IEnumerable<IEdmProperty> declaredProperties = entityType.DeclaredProperties;
				if (declaredProperties != null)
				{
					foreach (IEdmProperty edmProperty in declaredProperties)
					{
						if (edmProperty.Type.IsODataPrimitiveTypeKind() || edmProperty.Type.IsODataComplexTypeKind())
						{
							model.SaveEpmAnnotationsForProperty(edmProperty, odataEntityPropertyMappingCache);
						}
						else if (edmProperty.Type.IsNonEntityCollectionType())
						{
							model.SaveEpmAnnotationsForProperty(edmProperty, odataEntityPropertyMappingCache);
						}
					}
				}
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00049B20 File Offset: 0x00047D20
		private static bool TryGetBooleanAnnotation(IEdmModel model, IEdmStructuredType structuredType, string annotationLocalName, bool recursive, out bool boolValue)
		{
			string text = null;
			bool flag;
			do
			{
				flag = model.TryGetODataAnnotation(structuredType, annotationLocalName, out text);
				if (flag)
				{
					break;
				}
				structuredType = structuredType.BaseType;
			}
			while (recursive && structuredType != null);
			if (!flag)
			{
				boolValue = false;
				return false;
			}
			boolValue = XmlConvert.ToBoolean(text);
			return true;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00049B60 File Offset: 0x00047D60
		private static bool TryGetBooleanAnnotation(IEdmModel model, IEdmElement annotatable, string annotationLocalName, out bool boolValue)
		{
			string text;
			if (model.TryGetODataAnnotation(annotatable, annotationLocalName, out text))
			{
				boolValue = XmlConvert.ToBoolean(text);
				return true;
			}
			boolValue = false;
			return false;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00049B87 File Offset: 0x00047D87
		private static void SetBooleanAnnotation(IEdmModel model, IEdmElement annotatable, string annotationLocalName, bool boolValue)
		{
			model.SetODataAnnotation(annotatable, annotationLocalName, boolValue ? "true" : null);
		}

		// Token: 0x040006FB RID: 1787
		private const string Version1NumberString = "1.0";

		// Token: 0x040006FC RID: 1788
		private const string Version2NumberString = "2.0";

		// Token: 0x040006FD RID: 1789
		private const string Version3NumberString = "3.0";
	}
}
