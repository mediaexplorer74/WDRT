using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Annotations;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x0200020F RID: 527
	internal static class EpmExtensionMethods
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x0003B0D8 File Offset: 0x000392D8
		internal static ODataEntityPropertyMappingCache EnsureEpmCache(this IEdmModel model, IEdmEntityType entityType, int maxMappingCount)
		{
			bool flag;
			return EpmExtensionMethods.EnsureEpmCacheInternal(model, entityType, maxMappingCount, out flag);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0003B0F0 File Offset: 0x000392F0
		internal static bool HasEntityPropertyMappings(this IEdmModel model, IEdmEntityType entityType)
		{
			for (IEdmEntityType edmEntityType = entityType; edmEntityType != null; edmEntityType = edmEntityType.BaseEntityType())
			{
				if (model.GetEntityPropertyMappings(edmEntityType) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0003B117 File Offset: 0x00039317
		internal static ODataEntityPropertyMappingCollection GetEntityPropertyMappings(this IEdmModel model, IEdmEntityType entityType)
		{
			return model.GetAnnotationValue(entityType);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0003B120 File Offset: 0x00039320
		internal static ODataEntityPropertyMappingCache GetEpmCache(this IEdmModel model, IEdmEntityType entityType)
		{
			return model.GetAnnotationValue(entityType);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0003B12C File Offset: 0x0003932C
		internal static Dictionary<string, IEdmDirectValueAnnotationBinding> GetAnnotationBindingsToRemoveSerializableEpmAnnotations(this IEdmModel model, IEdmElement annotatable)
		{
			Dictionary<string, IEdmDirectValueAnnotationBinding> dictionary = new Dictionary<string, IEdmDirectValueAnnotationBinding>(StringComparer.Ordinal);
			IEnumerable<IEdmDirectValueAnnotation> odataAnnotations = model.GetODataAnnotations(annotatable);
			if (odataAnnotations != null)
			{
				foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in odataAnnotations)
				{
					if (edmDirectValueAnnotation.IsEpmAnnotation())
					{
						dictionary.Add(edmDirectValueAnnotation.Name, new EdmDirectValueAnnotationBinding(annotatable, edmDirectValueAnnotation.NamespaceUri, edmDirectValueAnnotation.Name, null));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0003B1AC File Offset: 0x000393AC
		internal static void ClearInMemoryEpmAnnotations(this IEdmModel model, IEdmElement annotatable)
		{
			model.SetAnnotationValues(new IEdmDirectValueAnnotationBinding[]
			{
				new EdmTypedDirectValueAnnotationBinding<ODataEntityPropertyMappingCollection>(annotatable, null),
				new EdmTypedDirectValueAnnotationBinding<ODataEntityPropertyMappingCache>(annotatable, null)
			});
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0003B250 File Offset: 0x00039450
		internal static void SaveEpmAnnotationsForProperty(this IEdmModel model, IEdmProperty property, ODataEntityPropertyMappingCache epmCache)
		{
			string propertyName = property.Name;
			IEnumerable<EntityPropertyMappingAttribute> enumerable = epmCache.MappingsForDeclaredProperties.Where((EntityPropertyMappingAttribute m) => m.SourcePath.StartsWith(propertyName, StringComparison.Ordinal) && (m.SourcePath.Length == propertyName.Length || m.SourcePath[propertyName.Length] == '/'));
			bool flag;
			bool flag2;
			if (property.Type.IsODataPrimitiveTypeKind())
			{
				flag = true;
				flag2 = false;
			}
			else
			{
				flag2 = true;
				flag = enumerable.Any((EntityPropertyMappingAttribute m) => m.SourcePath == propertyName);
			}
			model.SaveEpmAnnotations(property, enumerable, flag, flag2);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0003B2C4 File Offset: 0x000394C4
		internal static void SaveEpmAnnotations(this IEdmModel model, IEdmElement annotatable, IEnumerable<EntityPropertyMappingAttribute> mappings, bool skipSourcePath, bool removePrefix)
		{
			EpmAttributeNameBuilder epmAttributeNameBuilder = new EpmAttributeNameBuilder();
			Dictionary<string, IEdmDirectValueAnnotationBinding> annotationBindingsToRemoveSerializableEpmAnnotations = model.GetAnnotationBindingsToRemoveSerializableEpmAnnotations(annotatable);
			foreach (EntityPropertyMappingAttribute entityPropertyMappingAttribute in mappings)
			{
				string text;
				if (entityPropertyMappingAttribute.TargetSyndicationItem == SyndicationItemProperty.CustomProperty)
				{
					text = epmAttributeNameBuilder.EpmTargetPath;
					annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, entityPropertyMappingAttribute.TargetPath);
					text = epmAttributeNameBuilder.EpmNsUri;
					annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, entityPropertyMappingAttribute.TargetNamespaceUri);
					string targetNamespacePrefix = entityPropertyMappingAttribute.TargetNamespacePrefix;
					if (!string.IsNullOrEmpty(targetNamespacePrefix))
					{
						text = epmAttributeNameBuilder.EpmNsPrefix;
						annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, targetNamespacePrefix);
					}
				}
				else
				{
					text = epmAttributeNameBuilder.EpmTargetPath;
					annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, entityPropertyMappingAttribute.TargetSyndicationItem.ToAttributeValue());
					text = epmAttributeNameBuilder.EpmContentKind;
					annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, entityPropertyMappingAttribute.TargetTextContentKind.ToAttributeValue());
				}
				if (!skipSourcePath)
				{
					string text2 = entityPropertyMappingAttribute.SourcePath;
					if (removePrefix)
					{
						text2 = text2.Substring(text2.IndexOf('/') + 1);
					}
					text = epmAttributeNameBuilder.EpmSourcePath;
					annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, text2);
				}
				string text3 = (entityPropertyMappingAttribute.KeepInContent ? "true" : "false");
				text = epmAttributeNameBuilder.EpmKeepInContent;
				annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, text3);
				epmAttributeNameBuilder.MoveNext();
			}
			model.SetAnnotationValues(annotationBindingsToRemoveSerializableEpmAnnotations.Values);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0003B44C File Offset: 0x0003964C
		internal static CachedPrimitiveKeepInContentAnnotation EpmCachedKeepPrimitiveInContent(this IEdmModel model, IEdmComplexType complexType)
		{
			return model.GetAnnotationValue(complexType);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0003B458 File Offset: 0x00039658
		internal static string ToTargetPath(this SyndicationItemProperty targetSyndicationItem)
		{
			switch (targetSyndicationItem)
			{
			case SyndicationItemProperty.AuthorEmail:
				return "author/email";
			case SyndicationItemProperty.AuthorName:
				return "author/name";
			case SyndicationItemProperty.AuthorUri:
				return "author/uri";
			case SyndicationItemProperty.ContributorEmail:
				return "contributor/email";
			case SyndicationItemProperty.ContributorName:
				return "contributor/name";
			case SyndicationItemProperty.ContributorUri:
				return "contributor/uri";
			case SyndicationItemProperty.Updated:
				return "updated";
			case SyndicationItemProperty.Published:
				return "published";
			case SyndicationItemProperty.Rights:
				return "rights";
			case SyndicationItemProperty.Summary:
				return "summary";
			case SyndicationItemProperty.Title:
				return "title";
			default:
				throw new ArgumentException(Strings.EntityPropertyMapping_EpmAttribute("targetSyndicationItem"));
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0003B4F0 File Offset: 0x000396F0
		private static void LoadEpmAnnotations(IEdmModel model, IEdmEntityType entityType)
		{
			string text = entityType.ODataFullName();
			ODataEntityPropertyMappingCollection odataEntityPropertyMappingCollection = new ODataEntityPropertyMappingCollection();
			model.LoadEpmAnnotations(entityType, odataEntityPropertyMappingCollection, text, null);
			IEnumerable<IEdmProperty> declaredProperties = entityType.DeclaredProperties;
			if (declaredProperties != null)
			{
				foreach (IEdmProperty edmProperty in declaredProperties)
				{
					model.LoadEpmAnnotations(edmProperty, odataEntityPropertyMappingCollection, text, edmProperty);
				}
			}
			model.SetAnnotationValue(entityType, odataEntityPropertyMappingCollection);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0003B56C File Offset: 0x0003976C
		private static void LoadEpmAnnotations(this IEdmModel model, IEdmElement annotatable, ODataEntityPropertyMappingCollection mappings, string typeName, IEdmProperty property)
		{
			IEnumerable<EpmExtensionMethods.EpmAnnotationValues> enumerable = model.ParseSerializableEpmAnnotations(annotatable, typeName, property);
			if (enumerable != null)
			{
				foreach (EpmExtensionMethods.EpmAnnotationValues epmAnnotationValues in enumerable)
				{
					EntityPropertyMappingAttribute entityPropertyMappingAttribute = EpmExtensionMethods.ValidateAnnotationValues(epmAnnotationValues, typeName, property);
					mappings.Add(entityPropertyMappingAttribute);
				}
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0003B5CC File Offset: 0x000397CC
		private static SyndicationItemProperty MapTargetPathToSyndicationProperty(string targetPath)
		{
			SyndicationItemProperty syndicationItemProperty;
			if (!EpmExtensionMethods.TargetPathToSyndicationItemMap.TryGetValue(targetPath, out syndicationItemProperty))
			{
				return SyndicationItemProperty.CustomProperty;
			}
			return syndicationItemProperty;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0003B5EC File Offset: 0x000397EC
		private static string ToAttributeValue(this SyndicationTextContentKind contentKind)
		{
			switch (contentKind)
			{
			case SyndicationTextContentKind.Html:
				return "html";
			case SyndicationTextContentKind.Xhtml:
				return "xhtml";
			default:
				return "text";
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0003B620 File Offset: 0x00039820
		private static string ToAttributeValue(this SyndicationItemProperty syndicationItemProperty)
		{
			switch (syndicationItemProperty)
			{
			case SyndicationItemProperty.AuthorEmail:
				return "SyndicationAuthorEmail";
			case SyndicationItemProperty.AuthorName:
				return "SyndicationAuthorName";
			case SyndicationItemProperty.AuthorUri:
				return "SyndicationAuthorUri";
			case SyndicationItemProperty.ContributorEmail:
				return "SyndicationContributorEmail";
			case SyndicationItemProperty.ContributorName:
				return "SyndicationContributorName";
			case SyndicationItemProperty.ContributorUri:
				return "SyndicationContributorUri";
			case SyndicationItemProperty.Updated:
				return "SyndicationUpdated";
			case SyndicationItemProperty.Published:
				return "SyndicationPublished";
			case SyndicationItemProperty.Rights:
				return "SyndicationRights";
			case SyndicationItemProperty.Summary:
				return "SyndicationSummary";
			case SyndicationItemProperty.Title:
				return "SyndicationTitle";
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmExtensionMethods_ToAttributeValue_SyndicationItemProperty));
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0003B6BC File Offset: 0x000398BC
		private static SyndicationTextContentKind MapContentKindToSyndicationTextContentKind(string contentKind, string attributeSuffix, string typeName, string propertyName)
		{
			if (contentKind != null)
			{
				if (contentKind == "text")
				{
					return SyndicationTextContentKind.Plaintext;
				}
				if (contentKind == "html")
				{
					return SyndicationTextContentKind.Html;
				}
				if (contentKind == "xhtml")
				{
					return SyndicationTextContentKind.Xhtml;
				}
			}
			string text = ((propertyName == null) ? Strings.EpmExtensionMethods_InvalidTargetTextContentKindOnType("FC_ContentKind" + attributeSuffix, typeName) : Strings.EpmExtensionMethods_InvalidTargetTextContentKindOnProperty("FC_ContentKind" + attributeSuffix, propertyName, typeName));
			throw new ODataException(text);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0003B72C File Offset: 0x0003992C
		private static IEnumerable<EpmExtensionMethods.EpmAnnotationValues> ParseSerializableEpmAnnotations(this IEdmModel model, IEdmElement annotatable, string typeName, IEdmProperty property)
		{
			Dictionary<string, EpmExtensionMethods.EpmAnnotationValues> dictionary = null;
			IEnumerable<IEdmDirectValueAnnotation> odataAnnotations = model.GetODataAnnotations(annotatable);
			if (odataAnnotations != null)
			{
				foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in odataAnnotations)
				{
					string text;
					string text2;
					if (edmDirectValueAnnotation.IsEpmAnnotation(out text, out text2))
					{
						string text3 = EpmExtensionMethods.ConvertEdmAnnotationValue(edmDirectValueAnnotation);
						if (dictionary == null)
						{
							dictionary = new Dictionary<string, EpmExtensionMethods.EpmAnnotationValues>(StringComparer.Ordinal);
						}
						EpmExtensionMethods.EpmAnnotationValues epmAnnotationValues;
						if (!dictionary.TryGetValue(text2, out epmAnnotationValues))
						{
							epmAnnotationValues = new EpmExtensionMethods.EpmAnnotationValues
							{
								AttributeSuffix = text2
							};
							dictionary[text2] = epmAnnotationValues;
						}
						if (EpmExtensionMethods.NamesMatchByReference("FC_TargetPath", text))
						{
							epmAnnotationValues.TargetPath = text3;
						}
						else if (EpmExtensionMethods.NamesMatchByReference("FC_SourcePath", text))
						{
							epmAnnotationValues.SourcePath = text3;
						}
						else if (EpmExtensionMethods.NamesMatchByReference("FC_KeepInContent", text))
						{
							epmAnnotationValues.KeepInContent = text3;
						}
						else if (EpmExtensionMethods.NamesMatchByReference("FC_ContentKind", text))
						{
							epmAnnotationValues.ContentKind = text3;
						}
						else if (EpmExtensionMethods.NamesMatchByReference("FC_NsUri", text))
						{
							epmAnnotationValues.NamespaceUri = text3;
						}
						else
						{
							if (!EpmExtensionMethods.NamesMatchByReference("FC_NsPrefix", text))
							{
								throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataUtils_ParseSerializableEpmAnnotations_UnreachableCodePath));
							}
							epmAnnotationValues.NamespacePrefix = text3;
						}
					}
				}
				if (dictionary != null)
				{
					foreach (EpmExtensionMethods.EpmAnnotationValues epmAnnotationValues2 in dictionary.Values)
					{
						string sourcePath = epmAnnotationValues2.SourcePath;
						if (sourcePath == null)
						{
							if (property == null)
							{
								string text4 = "FC_SourcePath" + epmAnnotationValues2.AttributeSuffix;
								throw new ODataException(Strings.EpmExtensionMethods_MissingAttributeOnType(text4, typeName));
							}
							epmAnnotationValues2.SourcePath = property.Name;
						}
						else if (property != null && !property.Type.IsODataPrimitiveTypeKind())
						{
							epmAnnotationValues2.SourcePath = property.Name + "/" + sourcePath;
						}
					}
				}
			}
			if (dictionary != null)
			{
				return dictionary.Values;
			}
			return null;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0003B950 File Offset: 0x00039B50
		private static EntityPropertyMappingAttribute ValidateAnnotationValues(EpmExtensionMethods.EpmAnnotationValues annotationValues, string typeName, IEdmProperty property)
		{
			if (annotationValues.TargetPath == null)
			{
				string text = "FC_TargetPath" + annotationValues.AttributeSuffix;
				string text2 = ((property == null) ? Strings.EpmExtensionMethods_MissingAttributeOnType(text, typeName) : Strings.EpmExtensionMethods_MissingAttributeOnProperty(text, property.Name, typeName));
				throw new ODataException(text2);
			}
			bool flag = true;
			if (annotationValues.KeepInContent != null && !bool.TryParse(annotationValues.KeepInContent, out flag))
			{
				string text3 = "FC_KeepInContent" + annotationValues.AttributeSuffix;
				throw new InvalidOperationException((property == null) ? Strings.EpmExtensionMethods_InvalidKeepInContentOnType(text3, typeName) : Strings.EpmExtensionMethods_InvalidKeepInContentOnProperty(text3, property.Name, typeName));
			}
			SyndicationItemProperty syndicationItemProperty = EpmExtensionMethods.MapTargetPathToSyndicationProperty(annotationValues.TargetPath);
			EntityPropertyMappingAttribute entityPropertyMappingAttribute;
			if (syndicationItemProperty == SyndicationItemProperty.CustomProperty)
			{
				if (annotationValues.ContentKind != null)
				{
					string text4 = "FC_ContentKind" + annotationValues.AttributeSuffix;
					string text5 = ((property == null) ? Strings.EpmExtensionMethods_AttributeNotAllowedForCustomMappingOnType(text4, typeName) : Strings.EpmExtensionMethods_AttributeNotAllowedForCustomMappingOnProperty(text4, property.Name, typeName));
					throw new ODataException(text5);
				}
				entityPropertyMappingAttribute = new EntityPropertyMappingAttribute(annotationValues.SourcePath, annotationValues.TargetPath, annotationValues.NamespacePrefix, annotationValues.NamespaceUri, flag);
			}
			else
			{
				if (annotationValues.NamespaceUri != null)
				{
					string text6 = "FC_NsUri" + annotationValues.AttributeSuffix;
					string text7 = ((property == null) ? Strings.EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnType(text6, typeName) : Strings.EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnProperty(text6, property.Name, typeName));
					throw new ODataException(text7);
				}
				if (annotationValues.NamespacePrefix != null)
				{
					string text8 = "FC_NsPrefix" + annotationValues.AttributeSuffix;
					string text9 = ((property == null) ? Strings.EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnType(text8, typeName) : Strings.EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnProperty(text8, property.Name, typeName));
					throw new ODataException(text9);
				}
				SyndicationTextContentKind syndicationTextContentKind = SyndicationTextContentKind.Plaintext;
				if (annotationValues.ContentKind != null)
				{
					syndicationTextContentKind = EpmExtensionMethods.MapContentKindToSyndicationTextContentKind(annotationValues.ContentKind, annotationValues.AttributeSuffix, typeName, (property == null) ? null : property.Name);
				}
				entityPropertyMappingAttribute = new EntityPropertyMappingAttribute(annotationValues.SourcePath, syndicationItemProperty, syndicationTextContentKind, flag);
			}
			return entityPropertyMappingAttribute;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0003BB12 File Offset: 0x00039D12
		private static void RemoveEpmCache(this IEdmModel model, IEdmEntityType entityType)
		{
			model.SetAnnotationValue(entityType, null);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0003BB1C File Offset: 0x00039D1C
		private static bool IsEpmAnnotation(this IEdmDirectValueAnnotation annotation)
		{
			string text;
			string text2;
			return annotation.IsEpmAnnotation(out text, out text2);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0003BB34 File Offset: 0x00039D34
		private static bool IsEpmAnnotation(this IEdmDirectValueAnnotation annotation, out string baseName, out string suffix)
		{
			string name = annotation.Name;
			for (int i = 0; i < EpmExtensionMethods.EpmAnnotationBaseNames.Length; i++)
			{
				string text = EpmExtensionMethods.EpmAnnotationBaseNames[i];
				if (name.StartsWith(text, StringComparison.Ordinal))
				{
					baseName = text;
					suffix = name.Substring(text.Length);
					return true;
				}
			}
			baseName = null;
			suffix = null;
			return false;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0003BB88 File Offset: 0x00039D88
		private static string ConvertEdmAnnotationValue(IEdmDirectValueAnnotation annotation)
		{
			object value = annotation.Value;
			if (value == null)
			{
				return null;
			}
			IEdmStringValue edmStringValue = value as IEdmStringValue;
			if (edmStringValue != null)
			{
				return edmStringValue.Value;
			}
			throw new ODataException(Strings.EpmExtensionMethods_CannotConvertEdmAnnotationValue(annotation.NamespaceUri, annotation.Name, annotation.GetType().FullName));
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0003BBD3 File Offset: 0x00039DD3
		private static bool NamesMatchByReference(string first, string second)
		{
			return object.ReferenceEquals(first, second);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0003BBDC File Offset: 0x00039DDC
		private static bool HasOwnOrInheritedEpm(this IEdmModel model, IEdmEntityType entityType)
		{
			if (entityType == null)
			{
				return false;
			}
			if (model.GetAnnotationValue(entityType) != null)
			{
				return true;
			}
			EpmExtensionMethods.LoadEpmAnnotations(model, entityType);
			return model.GetAnnotationValue(entityType) != null || model.HasOwnOrInheritedEpm(entityType.BaseEntityType());
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0003BC0C File Offset: 0x00039E0C
		private static IEdmDirectValueAnnotationBinding GetODataAnnotationBinding(IEdmElement annotatable, string localName, string value)
		{
			IEdmStringValue edmStringValue = null;
			if (value != null)
			{
				IEdmStringTypeReference @string = EdmCoreModel.Instance.GetString(true);
				edmStringValue = new EdmStringConstant(@string, value);
			}
			return new EdmDirectValueAnnotationBinding(annotatable, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", localName, edmStringValue);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0003BC40 File Offset: 0x00039E40
		private static ODataEntityPropertyMappingCache EnsureEpmCacheInternal(IEdmModel model, IEdmEntityType entityType, int maxMappingCount, out bool cacheModified)
		{
			cacheModified = false;
			if (entityType == null)
			{
				return null;
			}
			IEdmEntityType edmEntityType = entityType.BaseEntityType();
			ODataEntityPropertyMappingCache odataEntityPropertyMappingCache = null;
			if (edmEntityType != null)
			{
				odataEntityPropertyMappingCache = EpmExtensionMethods.EnsureEpmCacheInternal(model, edmEntityType, maxMappingCount, out cacheModified);
			}
			ODataEntityPropertyMappingCache odataEntityPropertyMappingCache2 = model.GetEpmCache(entityType);
			if (model.HasOwnOrInheritedEpm(entityType))
			{
				ODataEntityPropertyMappingCollection entityPropertyMappings = model.GetEntityPropertyMappings(entityType);
				bool flag = odataEntityPropertyMappingCache2 == null || cacheModified || odataEntityPropertyMappingCache2.IsDirty(entityPropertyMappings);
				if (!flag)
				{
					return odataEntityPropertyMappingCache2;
				}
				cacheModified = true;
				int num = ValidationUtils.ValidateTotalEntityPropertyMappingCount(odataEntityPropertyMappingCache, entityPropertyMappings, maxMappingCount);
				odataEntityPropertyMappingCache2 = new ODataEntityPropertyMappingCache(entityPropertyMappings, model, num);
				try
				{
					odataEntityPropertyMappingCache2.BuildEpmForType(entityType, entityType);
					odataEntityPropertyMappingCache2.EpmSourceTree.Validate(entityType);
					model.SetAnnotationValue(entityType, odataEntityPropertyMappingCache2);
					return odataEntityPropertyMappingCache2;
				}
				catch
				{
					model.RemoveEpmCache(entityType);
					throw;
				}
			}
			if (odataEntityPropertyMappingCache2 != null)
			{
				cacheModified = true;
				model.RemoveEpmCache(entityType);
			}
			return odataEntityPropertyMappingCache2;
		}

		// Token: 0x040005EF RID: 1519
		private static readonly string[] EpmAnnotationBaseNames = new string[] { "FC_TargetPath", "FC_SourcePath", "FC_KeepInContent", "FC_ContentKind", "FC_NsUri", "FC_NsPrefix" };

		// Token: 0x040005F0 RID: 1520
		private static readonly Dictionary<string, SyndicationItemProperty> TargetPathToSyndicationItemMap = new Dictionary<string, SyndicationItemProperty>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"SyndicationAuthorEmail",
				SyndicationItemProperty.AuthorEmail
			},
			{
				"SyndicationAuthorName",
				SyndicationItemProperty.AuthorName
			},
			{
				"SyndicationAuthorUri",
				SyndicationItemProperty.AuthorUri
			},
			{
				"SyndicationContributorEmail",
				SyndicationItemProperty.ContributorEmail
			},
			{
				"SyndicationContributorName",
				SyndicationItemProperty.ContributorName
			},
			{
				"SyndicationContributorUri",
				SyndicationItemProperty.ContributorUri
			},
			{
				"SyndicationUpdated",
				SyndicationItemProperty.Updated
			},
			{
				"SyndicationPublished",
				SyndicationItemProperty.Published
			},
			{
				"SyndicationRights",
				SyndicationItemProperty.Rights
			},
			{
				"SyndicationSummary",
				SyndicationItemProperty.Summary
			},
			{
				"SyndicationTitle",
				SyndicationItemProperty.Title
			}
		};

		// Token: 0x02000210 RID: 528
		private sealed class EpmAnnotationValues
		{
			// Token: 0x17000365 RID: 869
			// (get) Token: 0x06001043 RID: 4163 RVA: 0x0003BDDA File Offset: 0x00039FDA
			// (set) Token: 0x06001044 RID: 4164 RVA: 0x0003BDE2 File Offset: 0x00039FE2
			internal string SourcePath { get; set; }

			// Token: 0x17000366 RID: 870
			// (get) Token: 0x06001045 RID: 4165 RVA: 0x0003BDEB File Offset: 0x00039FEB
			// (set) Token: 0x06001046 RID: 4166 RVA: 0x0003BDF3 File Offset: 0x00039FF3
			internal string TargetPath { get; set; }

			// Token: 0x17000367 RID: 871
			// (get) Token: 0x06001047 RID: 4167 RVA: 0x0003BDFC File Offset: 0x00039FFC
			// (set) Token: 0x06001048 RID: 4168 RVA: 0x0003BE04 File Offset: 0x0003A004
			internal string KeepInContent { get; set; }

			// Token: 0x17000368 RID: 872
			// (get) Token: 0x06001049 RID: 4169 RVA: 0x0003BE0D File Offset: 0x0003A00D
			// (set) Token: 0x0600104A RID: 4170 RVA: 0x0003BE15 File Offset: 0x0003A015
			internal string ContentKind { get; set; }

			// Token: 0x17000369 RID: 873
			// (get) Token: 0x0600104B RID: 4171 RVA: 0x0003BE1E File Offset: 0x0003A01E
			// (set) Token: 0x0600104C RID: 4172 RVA: 0x0003BE26 File Offset: 0x0003A026
			internal string NamespaceUri { get; set; }

			// Token: 0x1700036A RID: 874
			// (get) Token: 0x0600104D RID: 4173 RVA: 0x0003BE2F File Offset: 0x0003A02F
			// (set) Token: 0x0600104E RID: 4174 RVA: 0x0003BE37 File Offset: 0x0003A037
			internal string NamespacePrefix { get; set; }

			// Token: 0x1700036B RID: 875
			// (get) Token: 0x0600104F RID: 4175 RVA: 0x0003BE40 File Offset: 0x0003A040
			// (set) Token: 0x06001050 RID: 4176 RVA: 0x0003BE48 File Offset: 0x0003A048
			internal string AttributeSuffix { get; set; }
		}
	}
}
