using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000191 RID: 401
	internal sealed class ODataJsonLightErrorDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x000289D8 File Offset: 0x00026BD8
		internal ODataJsonLightErrorDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000289E4 File Offset: 0x00026BE4
		internal ODataError ReadTopLevelError()
		{
			base.JsonReader.DisableInStreamErrorDetection = true;
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			ODataError odataError2;
			try
			{
				base.ReadPayloadStart(ODataPayloadKind.Error, duplicatePropertyNamesChecker, false, false);
				ODataError odataError = this.ReadTopLevelErrorImplementation();
				odataError2 = odataError;
			}
			finally
			{
				base.JsonReader.DisableInStreamErrorDetection = false;
			}
			return odataError2;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00028A5C File Offset: 0x00026C5C
		internal Task<ODataError> ReadTopLevelErrorAsync()
		{
			base.JsonReader.DisableInStreamErrorDetection = true;
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			return base.ReadPayloadStartAsync(ODataPayloadKind.Error, duplicatePropertyNamesChecker, false, false).FollowOnSuccessWith((Task t) => this.ReadTopLevelErrorImplementation()).FollowAlwaysWith(delegate(Task<ODataError> t)
			{
				base.JsonReader.DisableInStreamErrorDetection = false;
			});
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00028AAC File Offset: 0x00026CAC
		private ODataError ReadTopLevelErrorImplementation()
		{
			ODataError odataError = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("odata.error", text) != 0)
				{
					throw new ODataException(Strings.ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty(text));
				}
				if (odataError != null)
				{
					throw new ODataException(Strings.ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName("odata.error"));
				}
				odataError = new ODataError();
				this.ReadODataErrorObject(odataError);
			}
			base.JsonReader.ReadEndObject();
			base.ReadPayloadEnd(false);
			return odataError;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00028BA4 File Offset: 0x00026DA4
		private void ReadJsonObjectInErrorPayload(Action<string, DuplicatePropertyNamesChecker> readPropertyWithValue)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.JsonReader.ReadStartObject();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(duplicatePropertyNamesChecker, new Func<string, object>(this.ReadErrorPropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						readPropertyWithValue(propertyName, duplicatePropertyNamesChecker);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						readPropertyWithValue(propertyName, duplicatePropertyNamesChecker);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						return;
					}
				});
			}
			base.JsonReader.ReadEndObject();
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00028C1C File Offset: 0x00026E1C
		private object ReadErrorPropertyAnnotationValue(string propertyAnnotationName)
		{
			if (string.CompareOrdinal(propertyAnnotationName, "odata.type") != 0)
			{
				throw new ODataException(Strings.ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload(propertyAnnotationName));
			}
			string text = base.JsonReader.ReadStringValue();
			if (text == null)
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName(propertyAnnotationName));
			}
			return text;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00028C7C File Offset: 0x00026E7C
		private void ReadODataErrorObject(ODataError error)
		{
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, DuplicatePropertyNamesChecker duplicationPropertyNameChecker)
			{
				this.ReadPropertyValueInODataErrorObject(error, propertyName, duplicationPropertyNameChecker);
			});
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00028CCC File Offset: 0x00026ECC
		private void ReadErrorMessageObject(ODataError error)
		{
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
			{
				this.ReadPropertyValueInMessageObject(error, propertyName);
			});
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00028D24 File Offset: 0x00026F24
		private ODataInnerError ReadInnerError(int recursionDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
			ODataInnerError innerError = new ODataInnerError();
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
			{
				this.ReadPropertyValueInInnerError(recursionDepth, innerError, propertyName);
			});
			return innerError;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00028D84 File Offset: 0x00026F84
		private void ReadPropertyValueInInnerError(int recursionDepth, ODataInnerError innerError, string propertyName)
		{
			if (propertyName != null)
			{
				if (propertyName == "message")
				{
					innerError.Message = base.JsonReader.ReadStringValue("message");
					return;
				}
				if (propertyName == "type")
				{
					innerError.TypeName = base.JsonReader.ReadStringValue("type");
					return;
				}
				if (propertyName == "stacktrace")
				{
					innerError.StackTrace = base.JsonReader.ReadStringValue("stacktrace");
					return;
				}
				if (propertyName == "internalexception")
				{
					innerError.InnerError = this.ReadInnerError(recursionDepth);
					return;
				}
			}
			base.JsonReader.SkipValue();
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00028E30 File Offset: 0x00027030
		private void ReadPropertyValueInODataErrorObject(ODataError error, string propertyName, DuplicatePropertyNamesChecker duplicationPropertyNameChecker)
		{
			if (propertyName != null)
			{
				if (propertyName == "code")
				{
					error.ErrorCode = base.JsonReader.ReadStringValue("code");
					return;
				}
				if (propertyName == "message")
				{
					this.ReadErrorMessageObject(error);
					return;
				}
				if (propertyName == "innererror")
				{
					error.InnerError = this.ReadInnerError(0);
					return;
				}
			}
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName))
			{
				ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(base.JsonLightInputContext);
				object obj = null;
				Dictionary<string, object> odataPropertyAnnotations = duplicationPropertyNameChecker.GetODataPropertyAnnotations(propertyName);
				if (odataPropertyAnnotations != null)
				{
					odataPropertyAnnotations.TryGetValue("odata.type", out obj);
				}
				object obj2 = odataJsonLightPropertyAndValueDeserializer.ReadNonEntityValue(obj as string, null, null, null, false, false, false, propertyName, true);
				error.AddInstanceAnnotationForReading(propertyName, obj2);
				return;
			}
			throw new ODataException(Strings.ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty(propertyName));
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00028EF4 File Offset: 0x000270F4
		private void ReadPropertyValueInMessageObject(ODataError error, string propertyName)
		{
			if (propertyName != null)
			{
				if (propertyName == "lang")
				{
					error.MessageLanguage = base.JsonReader.ReadStringValue("lang");
					return;
				}
				if (propertyName == "value")
				{
					error.Message = base.JsonReader.ReadStringValue("value");
					return;
				}
			}
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName))
			{
				base.JsonReader.SkipValue();
				return;
			}
			throw new ODataException(Strings.ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty(propertyName));
		}
	}
}
