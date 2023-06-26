using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000140 RID: 320
	internal sealed class JsonLightAnnotationGroupDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x0001BA57 File Offset: 0x00019C57
		internal JsonLightAnnotationGroupDeserializer(ODataJsonLightInputContext inputContext)
			: base(inputContext)
		{
			this.annotationGroups = new Dictionary<string, ODataJsonLightAnnotationGroup>(EqualityComparer<string>.Default);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001BA70 File Offset: 0x00019C70
		internal ODataJsonLightAnnotationGroup ReadAnnotationGroup(Func<string, object> readPropertyAnnotationValue, Func<string, DuplicatePropertyNamesChecker, object> readInstanceAnnotationValue)
		{
			string propertyName = base.JsonReader.GetPropertyName();
			if (string.CompareOrdinal(propertyName, "odata.annotationGroup") == 0)
			{
				base.JsonReader.ReadPropertyName();
				return this.ReadAnnotationGroupDeclaration(readPropertyAnnotationValue, readInstanceAnnotationValue);
			}
			if (string.CompareOrdinal(propertyName, "odata.annotationGroupReference") == 0)
			{
				base.JsonReader.ReadPropertyName();
				return this.ReadAnnotationGroupReference();
			}
			return null;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001BACC File Offset: 0x00019CCC
		internal void AddAnnotationGroup(ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (this.annotationGroups.ContainsKey(annotationGroup.Name))
			{
				throw new ODataException(Strings.JsonLightAnnotationGroupDeserializer_MultipleAnnotationGroupsWithSameName(annotationGroup.Name));
			}
			this.annotationGroups.Add(annotationGroup.Name, annotationGroup);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001BB04 File Offset: 0x00019D04
		private static void VerifyAnnotationGroupNameNotYetFound(ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (!string.IsNullOrEmpty(annotationGroup.Name))
			{
				throw new ODataException(Strings.JsonLightAnnotationGroupDeserializer_EncounteredMultipleNameProperties);
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001BB1E File Offset: 0x00019D1E
		private static bool IsAnnotationGroupName(string propertyName)
		{
			return string.CompareOrdinal(propertyName, "name") == 0;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001BB2E File Offset: 0x00019D2E
		private static void VerifyAnnotationGroupNameFound(ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (string.IsNullOrEmpty(annotationGroup.Name))
			{
				throw new ODataException(Strings.JsonLightAnnotationGroupDeserializer_AnnotationGroupDeclarationWithoutName);
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001BB48 File Offset: 0x00019D48
		private static void VerifyDataPropertyIsAnnotationName(string propertyName, ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (!JsonLightAnnotationGroupDeserializer.IsAnnotationGroupName(propertyName))
			{
				throw JsonLightAnnotationGroupDeserializer.CreateExceptionForInvalidAnnotationInsideAnnotationGroup(propertyName, annotationGroup);
			}
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001BB5A File Offset: 0x00019D5A
		private static ODataException CreateExceptionForInvalidAnnotationInsideAnnotationGroup(string propertyName, ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (string.IsNullOrEmpty(annotationGroup.Name))
			{
				return new ODataException(Strings.JsonLightAnnotationGroupDeserializer_InvalidAnnotationFoundInsideAnnotationGroup(propertyName));
			}
			return new ODataException(Strings.JsonLightAnnotationGroupDeserializer_InvalidAnnotationFoundInsideNamedAnnotationGroup(annotationGroup.Name, propertyName));
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001BB88 File Offset: 0x00019D88
		private ODataJsonLightAnnotationGroup ReadAnnotationGroupReference()
		{
			string text = base.JsonReader.ReadStringValue("odata.annotationGroupReference");
			ODataJsonLightAnnotationGroup odataJsonLightAnnotationGroup;
			if (this.annotationGroups.TryGetValue(text, out odataJsonLightAnnotationGroup))
			{
				return odataJsonLightAnnotationGroup;
			}
			throw new ODataException(Strings.JsonLightAnnotationGroupDeserializer_UndefinedAnnotationGroupReference(text));
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001BD00 File Offset: 0x00019F00
		private ODataJsonLightAnnotationGroup ReadAnnotationGroupDeclaration(Func<string, object> readPropertyAnnotationValue, Func<string, DuplicatePropertyNamesChecker, object> readInstanceAnnotationValue)
		{
			ODataJsonLightAnnotationGroup annotationGroup = new ODataJsonLightAnnotationGroup
			{
				Annotations = new Dictionary<string, object>(EqualityComparer<string>.Default)
			};
			base.JsonReader.ReadStartObject();
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(duplicatePropertyNamesChecker, readPropertyAnnotationValue, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						JsonLightAnnotationGroupDeserializer.VerifyDataPropertyIsAnnotationName(propertyName, annotationGroup);
						JsonLightAnnotationGroupDeserializer.VerifyAnnotationGroupNameNotYetFound(annotationGroup);
						annotationGroup.Name = this.JsonReader.ReadStringValue(propertyName);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
					{
						Dictionary<string, object> odataPropertyAnnotations = duplicatePropertyNamesChecker.GetODataPropertyAnnotations(propertyName);
						if (odataPropertyAnnotations == null)
						{
							return;
						}
						using (Dictionary<string, object>.Enumerator enumerator = odataPropertyAnnotations.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<string, object> keyValuePair = enumerator.Current;
								annotationGroup.Annotations.Add(propertyName + '@' + keyValuePair.Key, keyValuePair.Value);
							}
							return;
						}
						break;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						break;
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						this.JsonReader.SkipValue();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw JsonLightAnnotationGroupDeserializer.CreateExceptionForInvalidAnnotationInsideAnnotationGroup(propertyName, annotationGroup);
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightAnnotationGroupDeserializer_ReadAnnotationGroupDeclaration));
					}
					annotationGroup.Annotations.Add(propertyName, readInstanceAnnotationValue(propertyName, duplicatePropertyNamesChecker));
				});
			}
			JsonLightAnnotationGroupDeserializer.VerifyAnnotationGroupNameFound(annotationGroup);
			base.JsonReader.ReadEndObject();
			this.AddAnnotationGroup(annotationGroup);
			return annotationGroup;
		}

		// Token: 0x0400034C RID: 844
		private readonly Dictionary<string, ODataJsonLightAnnotationGroup> annotationGroups;
	}
}
