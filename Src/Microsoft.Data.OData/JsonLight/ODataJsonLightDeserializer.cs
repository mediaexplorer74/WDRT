using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000114 RID: 276
	internal abstract class ODataJsonLightDeserializer : ODataDeserializer
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x00019108 File Offset: 0x00017308
		protected ODataJsonLightDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.jsonLightInputContext = jsonLightInputContext;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00019118 File Offset: 0x00017318
		internal IODataMetadataContext MetadataContext
		{
			get
			{
				Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified = base.MessageReaderSettings.ReaderBehavior.OperationsBoundToEntityTypeMustBeContainerQualified;
				IODataMetadataContext iodataMetadataContext;
				if ((iodataMetadataContext = this.metadataContext) == null)
				{
					iodataMetadataContext = (this.metadataContext = new ODataMetadataContext(base.ReadingResponse, operationsBoundToEntityTypeMustBeContainerQualified, this.JsonLightInputContext.EdmTypeResolver, base.Model, this.MetadataDocumentUri));
				}
				return iodataMetadataContext;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001916C File Offset: 0x0001736C
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonLightInputContext.JsonReader;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x00019179 File Offset: 0x00017379
		internal ODataJsonLightMetadataUriParseResult MetadataUriParseResult
		{
			get
			{
				return this.metadataUriParseResult;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00019181 File Offset: 0x00017381
		protected ODataJsonLightInputContext JsonLightInputContext
		{
			get
			{
				return this.jsonLightInputContext;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001918C File Offset: 0x0001738C
		private Uri MetadataDocumentUri
		{
			get
			{
				return (this.MetadataUriParseResult != null && this.MetadataUriParseResult.MetadataDocumentUri != null) ? this.MetadataUriParseResult.MetadataDocumentUri : null;
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000191C4 File Offset: 0x000173C4
		internal static bool TryParsePropertyAnnotation(string propertyAnnotationName, out string propertyName, out string annotationName)
		{
			int num = propertyAnnotationName.IndexOf('@');
			if (num <= 0 || num == propertyAnnotationName.Length - 1)
			{
				propertyName = null;
				annotationName = null;
				return false;
			}
			propertyName = propertyAnnotationName.Substring(0, num);
			annotationName = propertyAnnotationName.Substring(num + 1);
			return true;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00019208 File Offset: 0x00017408
		internal void ReadPayloadStart(ODataPayloadKind payloadKind, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool isReadingNestedPayload, bool allowEmptyPayload)
		{
			string text = this.ReadPayloadStartImplementation(payloadKind, duplicatePropertyNamesChecker, isReadingNestedPayload, allowEmptyPayload);
			ODataJsonLightMetadataUriParseResult odataJsonLightMetadataUriParseResult = null;
			if (!isReadingNestedPayload && payloadKind != ODataPayloadKind.Error)
			{
				odataJsonLightMetadataUriParseResult = ((this.jsonLightInputContext.PayloadKindDetectionState == null) ? null : this.jsonLightInputContext.PayloadKindDetectionState.MetadataUriParseResult);
				if (odataJsonLightMetadataUriParseResult == null && text != null)
				{
					odataJsonLightMetadataUriParseResult = ODataJsonLightMetadataUriParser.Parse(base.Model, text, payloadKind, base.Version, base.MessageReaderSettings.ReaderBehavior);
				}
			}
			this.metadataUriParseResult = odataJsonLightMetadataUriParseResult;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001934C File Offset: 0x0001754C
		internal Task ReadPayloadStartAsync(ODataPayloadKind payloadKind, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool isReadingNestedPayload, bool allowEmptyPayload)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				string text = this.ReadPayloadStartImplementation(payloadKind, duplicatePropertyNamesChecker, isReadingNestedPayload, allowEmptyPayload);
				if (!isReadingNestedPayload && payloadKind != ODataPayloadKind.Error)
				{
					this.metadataUriParseResult = ((this.jsonLightInputContext.PayloadKindDetectionState == null) ? null : this.jsonLightInputContext.PayloadKindDetectionState.MetadataUriParseResult);
					if (this.metadataUriParseResult == null && text != null)
					{
						this.metadataUriParseResult = ODataJsonLightMetadataUriParser.Parse(this.Model, text, payloadKind, this.Version, this.MessageReaderSettings.ReaderBehavior);
					}
				}
			});
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00019394 File Offset: 0x00017594
		internal void ReadPayloadEnd(bool isReadingNestedPayload)
		{
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00019398 File Offset: 0x00017598
		internal string ReadAndValidateAnnotationStringValue(string annotationName)
		{
			string text = this.JsonReader.ReadStringValue(annotationName);
			ODataJsonLightReaderUtils.ValidateAnnotationStringValue(text, annotationName);
			return text;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000193BC File Offset: 0x000175BC
		internal Uri ReadAndValidateAnnotationStringValueAsUri(string annotationName)
		{
			string text = this.ReadAndValidateAnnotationStringValue(annotationName);
			return this.ProcessUriFromPayload(text);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000193D8 File Offset: 0x000175D8
		internal long ReadAndValidateAnnotationStringValueAsLong(string annotationName)
		{
			string text = this.ReadAndValidateAnnotationStringValue(annotationName);
			return (long)ODataJsonLightReaderUtils.ConvertValue(text, EdmCoreModel.Instance.GetInt64(false), base.MessageReaderSettings, base.Version, true, annotationName);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00019414 File Offset: 0x00017614
		internal Uri ProcessUriFromPayload(string uriFromPayload)
		{
			Uri uri = new Uri(uriFromPayload, UriKind.RelativeOrAbsolute);
			Uri metadataDocumentUri = this.MetadataDocumentUri;
			Uri uri2 = this.JsonLightInputContext.ResolveUri(metadataDocumentUri, uri);
			if (uri2 != null)
			{
				return uri2;
			}
			if (!uri.IsAbsoluteUri)
			{
				if (metadataDocumentUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation(uriFromPayload, "odata.metadata"));
				}
				uri = UriUtils.UriToAbsoluteUri(metadataDocumentUri, uri);
			}
			return uri;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00019474 File Offset: 0x00017674
		internal void ProcessProperty(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, Func<string, object> readPropertyAnnotationValue, Action<ODataJsonLightDeserializer.PropertyParsingResult, string> handleProperty)
		{
			string text;
			ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult = this.ParseProperty(duplicatePropertyNamesChecker, readPropertyAnnotationValue, out text);
			while (propertyParsingResult == ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation && this.ShouldSkipCustomInstanceAnnotation(text))
			{
				duplicatePropertyNamesChecker.MarkPropertyAsProcessed(text);
				this.JsonReader.SkipValue();
				propertyParsingResult = this.ParseProperty(duplicatePropertyNamesChecker, readPropertyAnnotationValue, out text);
			}
			handleProperty(propertyParsingResult, text);
			if (propertyParsingResult != ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject)
			{
				duplicatePropertyNamesChecker.MarkPropertyAsProcessed(text);
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000194CA File Offset: 0x000176CA
		[Conditional("DEBUG")]
		internal void AssertJsonCondition(params JsonNodeType[] allowedNodeTypes)
		{
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000194CC File Offset: 0x000176CC
		private bool ShouldSkipCustomInstanceAnnotation(string annotationName)
		{
			return (!(this is ODataJsonLightErrorDeserializer) || base.MessageReaderSettings.ShouldIncludeAnnotation != null) && base.MessageReaderSettings.ShouldSkipAnnotation(annotationName);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000194F4 File Offset: 0x000176F4
		private bool SkippedOverUnknownODataAnnotation(string annotationName, out string skippedRawJson)
		{
			if (ODataAnnotationNames.IsUnknownODataAnnotationName(annotationName))
			{
				this.JsonReader.Read();
				StringBuilder stringBuilder = new StringBuilder();
				this.JsonReader.SkipValue(stringBuilder);
				skippedRawJson = stringBuilder.ToString();
				return true;
			}
			skippedRawJson = null;
			return false;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00019538 File Offset: 0x00017738
		private ODataJsonLightDeserializer.PropertyParsingResult ParseProperty(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, Func<string, object> readPropertyAnnotationValue, out string parsedPropertyName)
		{
			string text = null;
			parsedPropertyName = null;
			while (this.JsonReader.NodeType == JsonNodeType.Property)
			{
				string propertyName = this.JsonReader.GetPropertyName();
				string text2;
				string text3;
				bool flag = ODataJsonLightDeserializer.TryParsePropertyAnnotation(propertyName, out text2, out text3);
				text2 = text2 ?? propertyName;
				if (parsedPropertyName != null && string.CompareOrdinal(parsedPropertyName, text2) != 0)
				{
					if (ODataJsonLightReaderUtils.IsAnnotationProperty(parsedPropertyName))
					{
						throw new ODataException(Strings.ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue(text, parsedPropertyName));
					}
					return ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue;
				}
				else
				{
					duplicatePropertyNamesChecker.AnnotationCollector.ShouldCollectAnnotation = base.MessageReaderSettings.UndeclaredPropertyBehaviorKinds == ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty;
					string text4 = null;
					if (flag)
					{
						duplicatePropertyNamesChecker.AnnotationCollector.TryPeekAndCollectAnnotationRawJson(this.JsonReader, text2, text3);
						if (ODataJsonLightReaderUtils.IsAnnotationProperty(text2) || !this.SkippedOverUnknownODataAnnotation(text3, out text4))
						{
							parsedPropertyName = text2;
							text = text3;
							this.ProcessPropertyAnnotation(text2, text3, duplicatePropertyNamesChecker, readPropertyAnnotationValue);
						}
					}
					else if (this.SkippedOverUnknownODataAnnotation(text2, out text4))
					{
						duplicatePropertyNamesChecker.AnnotationCollector.TryAddPropertyAnnotationRawJson("", text2, text4);
					}
					else
					{
						this.JsonReader.Read();
						parsedPropertyName = text2;
						if (ODataJsonLightUtils.IsMetadataReferenceProperty(text2))
						{
							return ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty;
						}
						if (!ODataJsonLightReaderUtils.IsAnnotationProperty(text2))
						{
							return ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue;
						}
						duplicatePropertyNamesChecker.AnnotationCollector.TryPeekAndCollectAnnotationRawJson(this.JsonReader, "", text2);
						if (ODataJsonLightReaderUtils.IsODataAnnotationName(text2))
						{
							return ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation;
						}
						return ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation;
					}
				}
			}
			if (parsedPropertyName == null)
			{
				return ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject;
			}
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(parsedPropertyName))
			{
				throw new ODataException(Strings.ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue(text, parsedPropertyName));
			}
			return ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00019684 File Offset: 0x00017884
		private void ProcessPropertyAnnotation(string annotatedPropertyName, string annotationName, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, Func<string, object> readPropertyAnnotationValue)
		{
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(annotatedPropertyName) && string.CompareOrdinal(annotationName, "odata.type") != 0)
			{
				throw new ODataException(Strings.ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation(annotationName, annotatedPropertyName, "odata.type"));
			}
			this.JsonReader.Read();
			if (ODataJsonLightReaderUtils.IsODataAnnotationName(annotationName))
			{
				duplicatePropertyNamesChecker.AddODataPropertyAnnotation(annotatedPropertyName, annotationName, readPropertyAnnotationValue(annotationName));
				return;
			}
			duplicatePropertyNamesChecker.AddCustomPropertyAnnotation(annotatedPropertyName, annotationName);
			this.JsonReader.SkipValue();
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000196F0 File Offset: 0x000178F0
		private string ReadPayloadStartImplementation(ODataPayloadKind payloadKind, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool isReadingNestedPayload, bool allowEmptyPayload)
		{
			if (!isReadingNestedPayload)
			{
				this.JsonReader.Read();
				if (allowEmptyPayload && this.JsonReader.NodeType == JsonNodeType.EndOfInput)
				{
					return null;
				}
				this.JsonReader.ReadStartObject();
				if (payloadKind != ODataPayloadKind.Error)
				{
					bool flag = this.jsonLightInputContext.ReadingResponse && (this.jsonLightInputContext.PayloadKindDetectionState == null || this.jsonLightInputContext.PayloadKindDetectionState.MetadataUriParseResult == null);
					return this.ReadMetadataUriAnnotation(payloadKind, duplicatePropertyNamesChecker, flag);
				}
			}
			return null;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00019770 File Offset: 0x00017970
		private string ReadMetadataUriAnnotation(ODataPayloadKind payloadKind, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool failOnMissingMetadataUriAnnotation)
		{
			if (this.JsonReader.NodeType != JsonNodeType.Property)
			{
				if (!failOnMissingMetadataUriAnnotation || payloadKind == ODataPayloadKind.Unsupported)
				{
					return null;
				}
				throw new ODataException(Strings.ODataJsonLightDeserializer_MetadataLinkNotFoundAsFirstProperty);
			}
			else
			{
				string propertyName = this.JsonReader.GetPropertyName();
				if (string.CompareOrdinal("odata.metadata", propertyName) == 0)
				{
					if (duplicatePropertyNamesChecker != null)
					{
						duplicatePropertyNamesChecker.MarkPropertyAsProcessed(propertyName);
					}
					this.JsonReader.ReadNext();
					return this.JsonReader.ReadStringValue();
				}
				if (!failOnMissingMetadataUriAnnotation || payloadKind == ODataPayloadKind.Unsupported)
				{
					return null;
				}
				throw new ODataException(Strings.ODataJsonLightDeserializer_MetadataLinkNotFoundAsFirstProperty);
			}
		}

		// Token: 0x040002C5 RID: 709
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x040002C6 RID: 710
		private IODataMetadataContext metadataContext;

		// Token: 0x040002C7 RID: 711
		private ODataJsonLightMetadataUriParseResult metadataUriParseResult;

		// Token: 0x02000115 RID: 277
		internal enum PropertyParsingResult
		{
			// Token: 0x040002C9 RID: 713
			EndOfObject,
			// Token: 0x040002CA RID: 714
			PropertyWithValue,
			// Token: 0x040002CB RID: 715
			PropertyWithoutValue,
			// Token: 0x040002CC RID: 716
			ODataInstanceAnnotation,
			// Token: 0x040002CD RID: 717
			CustomInstanceAnnotation,
			// Token: 0x040002CE RID: 718
			MetadataReferenceProperty
		}
	}
}
