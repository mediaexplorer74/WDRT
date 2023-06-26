using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000194 RID: 404
	internal sealed class ODataJsonLightInputContext : ODataInputContext
	{
		// Token: 0x06000BD2 RID: 3026 RVA: 0x000296C0 File Offset: 0x000278C0
		internal ODataJsonLightInputContext(ODataFormat format, Stream messageStream, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver, ODataJsonLightPayloadKindDetectionState payloadKindDetectionState)
			: this(format, ODataJsonLightInputContext.CreateTextReaderForMessageStreamConstructor(messageStream, encoding), contentType, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver, payloadKindDetectionState)
		{
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000296EC File Offset: 0x000278EC
		internal ODataJsonLightInputContext(ODataFormat format, TextReader reader, MediaType contentType, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver, ODataJsonLightPayloadKindDetectionState payloadKindDetectionState)
			: base(format, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
		{
			try
			{
				ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
				ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			}
			catch (ArgumentNullException)
			{
				reader.Dispose();
				throw;
			}
			try
			{
				this.textReader = reader;
				if (contentType.HasStreamingSetToTrue())
				{
					this.jsonReader = new BufferingJsonReader(this.textReader, "odata.error", messageReaderSettings.MessageQuotas.MaxNestingDepth, ODataFormat.Json);
				}
				else
				{
					this.jsonReader = new ReorderingJsonReader(this.textReader, messageReaderSettings.MessageQuotas.MaxNestingDepth);
				}
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && reader != null)
				{
					reader.Dispose();
				}
				throw;
			}
			this.payloadKindDetectionState = payloadKindDetectionState;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x000297C0 File Offset: 0x000279C0
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonReader;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x000297C8 File Offset: 0x000279C8
		internal ODataJsonLightPayloadKindDetectionState PayloadKindDetectionState
		{
			get
			{
				return this.payloadKindDetectionState;
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000297D0 File Offset: 0x000279D0
		internal override ODataReader CreateFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedBaseEntityType);
			return this.CreateFeedReaderImplementation(entitySet, expectedBaseEntityType);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00029804 File Offset: 0x00027A04
		internal override Task<ODataReader> CreateFeedReaderAsync(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedBaseEntityType);
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateFeedReaderImplementation(entitySet, expectedBaseEntityType));
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002984F File Offset: 0x00027A4F
		internal override ODataReader CreateEntryReader(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedEntityType);
			return this.CreateEntryReaderImplementation(entitySet, expectedEntityType);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00029884 File Offset: 0x00027A84
		internal override Task<ODataReader> CreateEntryReaderAsync(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedEntityType);
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateEntryReaderImplementation(entitySet, expectedEntityType));
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000298CF File Offset: 0x00027ACF
		internal override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateCollectionReader(expectedItemTypeReference);
			return this.CreateCollectionReaderImplementation(expectedItemTypeReference);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000298FC File Offset: 0x00027AFC
		internal override Task<ODataCollectionReader> CreateCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateCollectionReader(expectedItemTypeReference);
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionReader>(() => this.CreateCollectionReaderImplementation(expectedItemTypeReference));
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002993A File Offset: 0x00027B3A
		internal override ODataParameterReader CreateParameterReader(IEdmFunctionImport functionImport)
		{
			this.VerifyCanCreateParameterReader(functionImport);
			return this.CreateParameterReaderImplementation(functionImport);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00029968 File Offset: 0x00027B68
		internal override Task<ODataParameterReader> CreateParameterReaderAsync(IEdmFunctionImport functionImport)
		{
			this.VerifyCanCreateParameterReader(functionImport);
			return TaskUtils.GetTaskForSynchronousOperation<ODataParameterReader>(() => this.CreateParameterReaderImplementation(functionImport));
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000299A8 File Offset: 0x00027BA8
		internal override ODataWorkspace ReadServiceDocument()
		{
			ODataJsonLightServiceDocumentDeserializer odataJsonLightServiceDocumentDeserializer = new ODataJsonLightServiceDocumentDeserializer(this);
			return odataJsonLightServiceDocumentDeserializer.ReadServiceDocument();
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000299C4 File Offset: 0x00027BC4
		internal override Task<ODataWorkspace> ReadServiceDocumentAsync()
		{
			ODataJsonLightServiceDocumentDeserializer odataJsonLightServiceDocumentDeserializer = new ODataJsonLightServiceDocumentDeserializer(this);
			return odataJsonLightServiceDocumentDeserializer.ReadServiceDocumentAsync();
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x000299E0 File Offset: 0x00027BE0
		internal override ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty();
			ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(this);
			return odataJsonLightPropertyAndValueDeserializer.ReadTopLevelProperty(expectedPropertyTypeReference);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00029A04 File Offset: 0x00027C04
		internal override Task<ODataProperty> ReadPropertyAsync(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty();
			ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(this);
			return odataJsonLightPropertyAndValueDeserializer.ReadTopLevelPropertyAsync(expectedPropertyTypeReference);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00029A28 File Offset: 0x00027C28
		internal override ODataError ReadError()
		{
			ODataJsonLightErrorDeserializer odataJsonLightErrorDeserializer = new ODataJsonLightErrorDeserializer(this);
			return odataJsonLightErrorDeserializer.ReadTopLevelError();
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00029A44 File Offset: 0x00027C44
		internal override Task<ODataError> ReadErrorAsync()
		{
			ODataJsonLightErrorDeserializer odataJsonLightErrorDeserializer = new ODataJsonLightErrorDeserializer(this);
			return odataJsonLightErrorDeserializer.ReadTopLevelErrorAsync();
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00029A60 File Offset: 0x00027C60
		internal override ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLinks(navigationProperty);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00029A7C File Offset: 0x00027C7C
		internal override Task<ODataEntityReferenceLinks> ReadEntityReferenceLinksAsync(IEdmNavigationProperty navigationProperty)
		{
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLinksAsync(navigationProperty);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00029A98 File Offset: 0x00027C98
		internal override ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanReadEntityReferenceLink(navigationProperty);
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLink(navigationProperty);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00029ABC File Offset: 0x00027CBC
		internal override Task<ODataEntityReferenceLink> ReadEntityReferenceLinkAsync(IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanReadEntityReferenceLink(navigationProperty);
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLinkAsync(navigationProperty);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00029AE0 File Offset: 0x00027CE0
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataPayloadKindDetectionInfo detectionInfo)
		{
			this.VerifyCanDetectPayloadKind();
			ODataJsonLightPayloadKindDetectionDeserializer odataJsonLightPayloadKindDetectionDeserializer = new ODataJsonLightPayloadKindDetectionDeserializer(this);
			return odataJsonLightPayloadKindDetectionDeserializer.DetectPayloadKind(detectionInfo);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00029B04 File Offset: 0x00027D04
		internal Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(ODataPayloadKindDetectionInfo detectionInfo)
		{
			this.VerifyCanDetectPayloadKind();
			ODataJsonLightPayloadKindDetectionDeserializer odataJsonLightPayloadKindDetectionDeserializer = new ODataJsonLightPayloadKindDetectionDeserializer(this);
			return odataJsonLightPayloadKindDetectionDeserializer.DetectPayloadKindAsync(detectionInfo);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00029B28 File Offset: 0x00027D28
		protected override void DisposeImplementation()
		{
			try
			{
				if (this.textReader != null)
				{
					this.textReader.Dispose();
				}
			}
			finally
			{
				this.textReader = null;
				this.jsonReader = null;
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00029B6C File Offset: 0x00027D6C
		private static TextReader CreateTextReaderForMessageStreamConstructor(Stream messageStream, Encoding encoding)
		{
			TextReader textReader;
			try
			{
				textReader = new StreamReader(messageStream, encoding);
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && messageStream != null)
				{
					messageStream.Dispose();
				}
				throw;
			}
			return textReader;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00029BA8 File Offset: 0x00027DA8
		private void VerifyCanCreateParameterReader(IEdmFunctionImport functionImport)
		{
			this.VerifyUserModel();
			if (functionImport == null)
			{
				throw new ArgumentNullException("functionImport", Strings.ODataJsonLightInputContext_FunctionImportCannotBeNullForCreateParameterReader("functionImport"));
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00029BC8 File Offset: 0x00027DC8
		private void VerifyCanCreateODataReader(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
				if (entitySet == null)
				{
					throw new ODataException(Strings.ODataJsonLightInputContext_NoEntitySetForRequest);
				}
			}
			IEdmEntityType elementType = base.EdmTypeResolver.GetElementType(entitySet);
			if (entitySet != null && entityType != null && !entityType.IsOrInheritsFrom(elementType))
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType(entityType.FullName(), elementType.FullName(), entitySet.FullName()));
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00029C2A File Offset: 0x00027E2A
		private void VerifyCanCreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
				if (expectedItemTypeReference == null)
				{
					throw new ODataException(Strings.ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests);
				}
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00029C48 File Offset: 0x00027E48
		private void VerifyCanReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
				if (navigationProperty == null)
				{
					throw new ODataException(Strings.ODataJsonLightInputContext_NavigationPropertyRequiredForReadEntityReferenceLinkInRequests);
				}
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00029C66 File Offset: 0x00027E66
		private void VerifyCanReadProperty()
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00029C76 File Offset: 0x00027E76
		private void VerifyCanDetectPayloadKind()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_PayloadKindDetectionForRequest);
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00029C8B File Offset: 0x00027E8B
		private void VerifyUserModel()
		{
			if (!base.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_ModelRequiredForReading);
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00029CA5 File Offset: 0x00027EA5
		private ODataReader CreateFeedReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return new ODataJsonLightReader(this, entitySet, expectedBaseEntityType, true, null);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00029CB1 File Offset: 0x00027EB1
		private ODataReader CreateEntryReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return new ODataJsonLightReader(this, entitySet, expectedEntityType, false, null);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00029CBD File Offset: 0x00027EBD
		private ODataCollectionReader CreateCollectionReaderImplementation(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataJsonLightCollectionReader(this, expectedItemTypeReference, null);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00029CC7 File Offset: 0x00027EC7
		private ODataParameterReader CreateParameterReaderImplementation(IEdmFunctionImport functionImport)
		{
			return new ODataJsonLightParameterReader(this, functionImport);
		}

		// Token: 0x0400042A RID: 1066
		private readonly ODataJsonLightPayloadKindDetectionState payloadKindDetectionState;

		// Token: 0x0400042B RID: 1067
		private TextReader textReader;

		// Token: 0x0400042C RID: 1068
		private BufferingJsonReader jsonReader;
	}
}
