using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000166 RID: 358
	internal sealed class ODataJsonLightParameterDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x000202F9 File Offset: 0x0001E4F9
		internal ODataJsonLightParameterDeserializer(ODataJsonLightParameterReader parameterReader, ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.parameterReader = parameterReader;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00020504 File Offset: 0x0001E704
		internal bool ReadNextParameter(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			bool parameterRead = false;
			if (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				bool foundCustomInstanceAnnotation = false;
				base.ProcessProperty(duplicatePropertyNamesChecker, ODataJsonLightParameterDeserializer.propertyAnnotationValueReader, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string parameterName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
					{
						IEdmTypeReference parameterTypeReference = this.parameterReader.GetParameterTypeReference(parameterName);
						object obj;
						ODataParameterReaderState odataParameterReaderState;
						switch (parameterTypeReference.TypeKind())
						{
						case EdmTypeKind.Primitive:
						{
							IEdmPrimitiveTypeReference edmPrimitiveTypeReference = parameterTypeReference.AsPrimitive();
							if (edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Stream)
							{
								throw new ODataException(Strings.ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType(parameterName, edmPrimitiveTypeReference.PrimitiveKind()));
							}
							obj = this.ReadNonEntityValue(null, edmPrimitiveTypeReference, null, null, true, false, false, parameterName);
							odataParameterReaderState = ODataParameterReaderState.Value;
							goto IL_1A4;
						}
						case EdmTypeKind.Complex:
							obj = this.ReadNonEntityValue(null, parameterTypeReference, null, null, true, false, false, parameterName);
							odataParameterReaderState = ODataParameterReaderState.Value;
							goto IL_1A4;
						case EdmTypeKind.Collection:
							obj = null;
							if (this.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
							{
								obj = this.JsonReader.ReadPrimitiveValue();
								if (obj != null)
								{
									throw new ODataException(Strings.ODataJsonLightParameterDeserializer_NullCollectionExpected(JsonNodeType.PrimitiveValue, obj));
								}
								odataParameterReaderState = ODataParameterReaderState.Value;
								goto IL_1A4;
							}
							else
							{
								if (((IEdmCollectionType)parameterTypeReference.Definition).ElementType.TypeKind() == EdmTypeKind.Entity)
								{
									throw new ODataException(Strings.ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind(parameterName, "Entity Collection"));
								}
								odataParameterReaderState = ODataParameterReaderState.Collection;
								goto IL_1A4;
							}
							break;
						}
						throw new ODataException(Strings.ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind(parameterName, parameterTypeReference.TypeKind()));
						IL_1A4:
						parameterRead = true;
						this.parameterReader.EnterScope(odataParameterReaderState, parameterName, obj);
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters(parameterName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(parameterName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						this.JsonReader.SkipValue();
						foundCustomInstanceAnnotation = true;
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(parameterName));
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightParameterDeserializer_ReadNextParameter));
					}
				});
				if (foundCustomInstanceAnnotation)
				{
					return this.ReadNextParameter(duplicatePropertyNamesChecker);
				}
			}
			if (!parameterRead && base.JsonReader.NodeType == JsonNodeType.EndObject)
			{
				base.JsonReader.ReadEndObject();
				base.ReadPayloadEnd(false);
				if (this.parameterReader.State != ODataParameterReaderState.Start)
				{
					this.parameterReader.PopScope(this.parameterReader.State);
				}
				this.parameterReader.PopScope(ODataParameterReaderState.Start);
				this.parameterReader.EnterScope(ODataParameterReaderState.Completed, null, null);
			}
			return parameterRead;
		}

		// Token: 0x040003A3 RID: 931
		private static readonly Func<string, object> propertyAnnotationValueReader = delegate(string annotationName)
		{
			throw new ODataException(Strings.ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters);
		};

		// Token: 0x040003A4 RID: 932
		private readonly ODataJsonLightParameterReader parameterReader;
	}
}
