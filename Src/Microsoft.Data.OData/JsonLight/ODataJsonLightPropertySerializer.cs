using System;
using System.Collections.Generic;
using System.Globalization;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200018F RID: 399
	internal class ODataJsonLightPropertySerializer : ODataJsonLightSerializer
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x00027A52 File Offset: 0x00025C52
		internal ODataJsonLightPropertySerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext)
		{
			this.jsonLightValueSerializer = new ODataJsonLightValueSerializer(this);
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00027A67 File Offset: 0x00025C67
		internal ODataJsonLightValueSerializer JsonLightValueSerializer
		{
			get
			{
				return this.jsonLightValueSerializer;
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00027AF4 File Offset: 0x00025CF4
		internal void WriteTopLevelProperty(ODataProperty property)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.JsonWriter.StartObjectScope();
				ODataJsonLightMetadataUriBuilder odataJsonLightMetadataUriBuilder = this.JsonLightOutputContext.CreateMetadataUriBuilder();
				Uri uri;
				if (odataJsonLightMetadataUriBuilder.TryBuildMetadataUriForValue(property, out uri))
				{
					this.WriteMetadataUriProperty(uri);
				}
				this.WriteProperty(property, null, true, false, this.CreateDuplicatePropertyNamesChecker(), null);
				this.JsonWriter.EndObjectScope();
			});
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00027B28 File Offset: 0x00025D28
		internal void WriteProperties(IEdmStructuredType owningType, IEnumerable<ODataProperty> properties, bool isComplexValue, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			if (properties == null)
			{
				return;
			}
			foreach (ODataProperty odataProperty in properties)
			{
				this.WriteProperty(odataProperty, owningType, false, !isComplexValue, duplicatePropertyNamesChecker, projectedProperties);
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00027B80 File Offset: 0x00025D80
		private static bool IsOpenPropertyType(ODataProperty property, IEdmStructuredType owningType, IEdmProperty edmProperty)
		{
			if (property.SerializationInfo != null)
			{
				return property.SerializationInfo.PropertyKind == ODataPropertyKind.Open;
			}
			return owningType != null && owningType.IsOpen && edmProperty == null;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00027BAC File Offset: 0x00025DAC
		private bool ShouldWriteProperty(IEdmStructuredType owningType, ODataProperty property, IEdmProperty edmProperty, out bool shouldWriteRawAnnotations)
		{
			shouldWriteRawAnnotations = false;
			if (owningType == null)
			{
				return true;
			}
			if (edmProperty != null)
			{
				return true;
			}
			string name = property.Name;
			if (owningType.IsOpen)
			{
				ODataComplexValue odataComplexValue = property.Value as ODataComplexValue;
				if (odataComplexValue != null && !string.IsNullOrEmpty(odataComplexValue.TypeName))
				{
					return true;
				}
				ODataCollectionValue odataCollectionValue = property.Value as ODataCollectionValue;
				if (odataCollectionValue != null && !string.IsNullOrEmpty(odataCollectionValue.TypeName))
				{
					return true;
				}
				if (!(property.Value is ODataUntypedValue))
				{
					return true;
				}
			}
			if (base.MessageWriterSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
			{
				shouldWriteRawAnnotations = true;
				return true;
			}
			if (base.MessageWriterSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty))
			{
				return false;
			}
			throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(name, owningType.ODataFullName()));
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00027C58 File Offset: 0x00025E58
		private void WriteProperty(ODataProperty property, IEdmStructuredType owningType, bool isTopLevel, bool allowStreamProperty, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			WriterValidationUtils.ValidatePropertyNotNull(property);
			string name = property.Name;
			if (projectedProperties.ShouldSkipProperty(name))
			{
				return;
			}
			WriterValidationUtils.ValidatePropertyName(name);
			duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(property);
			IEdmProperty edmProperty = WriterValidationUtils.ValidatePropertyDefined(name, owningType, base.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
			bool flag = false;
			if (!this.ShouldWriteProperty(owningType, property, edmProperty, out flag))
			{
				return;
			}
			IEdmTypeReference edmTypeReference = ((edmProperty == null) ? null : edmProperty.Type);
			bool flag2 = false;
			if (flag)
			{
				this.TryWriteRawAnnotations(property, out flag2);
			}
			ODataUntypedValue odataUntypedValue = property.Value as ODataUntypedValue;
			if (odataUntypedValue != null)
			{
				if (base.MessageWriterSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
				{
					base.JsonWriter.WriteName(name);
					base.JsonWriter.WriteRawString(odataUntypedValue.RawJson);
				}
				return;
			}
			ODataValue odataValue = property.ODataValue;
			ODataPrimitiveValue odataPrimitiveValue = odataValue as ODataPrimitiveValue;
			bool flag3 = (edmTypeReference != null && edmTypeReference.IsSpatial()) || (edmTypeReference == null && odataPrimitiveValue != null && odataPrimitiveValue.Value is ISpatial);
			if (flag3)
			{
				ODataVersionChecker.CheckSpatialValue(base.Version);
			}
			ODataStreamReferenceValue odataStreamReferenceValue = odataValue as ODataStreamReferenceValue;
			if (odataStreamReferenceValue != null)
			{
				if (!allowStreamProperty)
				{
					throw new ODataException(Strings.ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry(name));
				}
				WriterValidationUtils.ValidateStreamReferenceProperty(property, edmProperty, base.Version, base.WritingResponse);
				this.WriteStreamReferenceProperty(name, odataStreamReferenceValue);
				return;
			}
			else
			{
				string text = (isTopLevel ? "value" : name);
				if (odataValue is ODataNullValue || odataValue == null)
				{
					WriterValidationUtils.ValidateNullPropertyValue(edmTypeReference, name, base.MessageWriterSettings.WriterBehavior, base.Model);
					if (isTopLevel)
					{
						base.JsonWriter.WriteName("odata.null");
						base.JsonWriter.WriteValue(true);
						return;
					}
					base.JsonWriter.WriteName(text);
					this.JsonLightValueSerializer.WriteNullValue();
					return;
				}
				else
				{
					bool flag4 = ODataJsonLightPropertySerializer.IsOpenPropertyType(property, owningType, edmProperty);
					if (flag4)
					{
						ValidationUtils.ValidateOpenPropertyValue(name, odataValue, base.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
					}
					ODataComplexValue odataComplexValue = odataValue as ODataComplexValue;
					if (odataComplexValue != null)
					{
						if (!isTopLevel)
						{
							base.JsonWriter.WriteName(text);
						}
						this.JsonLightValueSerializer.WriteComplexValue(odataComplexValue, edmTypeReference, isTopLevel, flag4, base.CreateDuplicatePropertyNamesChecker());
						return;
					}
					IEdmTypeReference edmTypeReference2 = TypeNameOracle.ResolveAndValidateTypeNameForValue(base.Model, edmTypeReference, odataValue, flag4);
					ODataCollectionValue odataCollectionValue = odataValue as ODataCollectionValue;
					if (odataCollectionValue != null)
					{
						if (!flag2)
						{
							string valueTypeNameForWriting = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(odataCollectionValue, edmTypeReference, edmTypeReference2, flag4);
							this.WritePropertyTypeName(text, valueTypeNameForWriting, isTopLevel);
						}
						base.JsonWriter.WriteName(text);
						ODataVersionChecker.CheckCollectionValueProperties(base.Version, name);
						this.JsonLightValueSerializer.WriteCollectionValue(odataCollectionValue, edmTypeReference, isTopLevel, false, flag4);
						return;
					}
					if (!flag2)
					{
						string valueTypeNameForWriting2 = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(odataPrimitiveValue, edmTypeReference, edmTypeReference2, flag4);
						this.WritePropertyTypeName(text, valueTypeNameForWriting2, isTopLevel);
					}
					base.JsonWriter.WriteName(text);
					this.JsonLightValueSerializer.WritePrimitiveValue(odataPrimitiveValue.Value, edmTypeReference);
					return;
				}
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00027F08 File Offset: 0x00026108
		private bool TryWriteRawAnnotations(ODataProperty property, out bool isODataTypeWritten)
		{
			ODataUntypedValue odataUntypedValue = property.Value as ODataUntypedValue;
			ODataAnnotatable odataAnnotatable = odataUntypedValue ?? property.ODataValue;
			isODataTypeWritten = false;
			if (odataAnnotatable != null)
			{
				ODataJsonLightRawAnnotationSet annotation = odataAnnotatable.GetAnnotation<ODataJsonLightRawAnnotationSet>();
				if (annotation != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in annotation.Annotations)
					{
						bool flag = string.Equals(keyValuePair.Key, "odata.type", StringComparison.OrdinalIgnoreCase);
						if (!flag || !(odataAnnotatable is ODataComplexValue))
						{
							base.JsonWriter.WriteName(string.Format(CultureInfo.InvariantCulture, "{0}@{1}", new object[] { property.Name, keyValuePair.Key }));
							base.JsonWriter.WriteRawString(keyValuePair.Value);
							if (flag)
							{
								isODataTypeWritten = true;
							}
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00027FFC File Offset: 0x000261FC
		private void WriteStreamReferenceProperty(string propertyName, ODataStreamReferenceValue streamReferenceValue)
		{
			Uri editLink = streamReferenceValue.EditLink;
			if (editLink != null)
			{
				base.JsonWriter.WritePropertyAnnotationName(propertyName, "odata.mediaEditLink");
				base.JsonWriter.WriteValue(base.UriToString(editLink));
			}
			Uri readLink = streamReferenceValue.ReadLink;
			if (readLink != null)
			{
				base.JsonWriter.WritePropertyAnnotationName(propertyName, "odata.mediaReadLink");
				base.JsonWriter.WriteValue(base.UriToString(readLink));
			}
			string contentType = streamReferenceValue.ContentType;
			if (contentType != null)
			{
				base.JsonWriter.WritePropertyAnnotationName(propertyName, "odata.mediaContentType");
				base.JsonWriter.WriteValue(contentType);
			}
			string etag = streamReferenceValue.ETag;
			if (etag != null)
			{
				base.JsonWriter.WritePropertyAnnotationName(propertyName, "odata.mediaETag");
				base.JsonWriter.WriteValue(etag);
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000280BD File Offset: 0x000262BD
		private void WritePropertyTypeName(string propertyName, string typeNameToWrite, bool isTopLevel)
		{
			if (typeNameToWrite != null)
			{
				if (isTopLevel)
				{
					base.JsonWriter.WriteName("odata.type");
					base.JsonWriter.WriteValue(typeNameToWrite);
					return;
				}
				ODataJsonLightWriterUtils.WriteODataTypePropertyAnnotation(base.JsonWriter, propertyName, typeNameToWrite);
			}
		}

		// Token: 0x0400041D RID: 1053
		private readonly ODataJsonLightValueSerializer jsonLightValueSerializer;
	}
}
