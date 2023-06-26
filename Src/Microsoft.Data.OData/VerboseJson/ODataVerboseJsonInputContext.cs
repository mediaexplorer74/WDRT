using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000238 RID: 568
	internal sealed class ODataVerboseJsonInputContext : ODataInputContext
	{
		// Token: 0x06001220 RID: 4640 RVA: 0x00044954 File Offset: 0x00042B54
		internal ODataVerboseJsonInputContext(ODataFormat format, TextReader reader, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
		{
			try
			{
				ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
				ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
				this.textReader = reader;
				if (base.UseServerFormatBehavior)
				{
					this.jsonReader = new PropertyDeduplicatingJsonReader(this.textReader, messageReaderSettings.MessageQuotas.MaxNestingDepth);
				}
				else
				{
					this.jsonReader = new BufferingJsonReader(this.textReader, "error", messageReaderSettings.MessageQuotas.MaxNestingDepth, ODataFormat.VerboseJson);
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
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00044A04 File Offset: 0x00042C04
		internal ODataVerboseJsonInputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: this(format, ODataVerboseJsonInputContext.CreateTextReaderForMessageStreamConstructor(messageStream, encoding), messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
		{
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00044A2B File Offset: 0x00042C2B
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonReader;
			}
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00044A33 File Offset: 0x00042C33
		internal override ODataReader CreateFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return this.CreateFeedReaderImplementation(entitySet, expectedBaseEntityType);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00044A60 File Offset: 0x00042C60
		internal override Task<ODataReader> CreateFeedReaderAsync(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateFeedReaderImplementation(entitySet, expectedBaseEntityType));
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00044A99 File Offset: 0x00042C99
		internal override ODataReader CreateEntryReader(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return this.CreateEntryReaderImplementation(entitySet, expectedEntityType);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00044AC4 File Offset: 0x00042CC4
		internal override Task<ODataReader> CreateEntryReaderAsync(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateEntryReaderImplementation(entitySet, expectedEntityType));
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00044AFD File Offset: 0x00042CFD
		internal override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return this.CreateCollectionReaderImplementation(expectedItemTypeReference);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00044B24 File Offset: 0x00042D24
		internal override Task<ODataCollectionReader> CreateCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionReader>(() => this.CreateCollectionReaderImplementation(expectedItemTypeReference));
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00044B56 File Offset: 0x00042D56
		internal override ODataParameterReader CreateParameterReader(IEdmFunctionImport functionImport)
		{
			ODataVerboseJsonInputContext.VerifyCanCreateParameterReader(functionImport);
			return this.CreateParameterReaderImplementation(functionImport);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00044B80 File Offset: 0x00042D80
		internal override Task<ODataParameterReader> CreateParameterReaderAsync(IEdmFunctionImport functionImport)
		{
			ODataVerboseJsonInputContext.VerifyCanCreateParameterReader(functionImport);
			return TaskUtils.GetTaskForSynchronousOperation<ODataParameterReader>(() => this.CreateParameterReaderImplementation(functionImport));
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00044BBD File Offset: 0x00042DBD
		internal override ODataWorkspace ReadServiceDocument()
		{
			return this.ReadServiceDocumentImplementation();
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00044BC5 File Offset: 0x00042DC5
		internal override Task<ODataWorkspace> ReadServiceDocumentAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWorkspace>(new Func<ODataWorkspace>(this.ReadServiceDocumentImplementation));
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00044BD8 File Offset: 0x00042DD8
		internal override ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			return this.ReadPropertyImplementation(property, expectedPropertyTypeReference);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00044C04 File Offset: 0x00042E04
		internal override Task<ODataProperty> ReadPropertyAsync(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataProperty>(() => this.ReadPropertyImplementation(property, expectedPropertyTypeReference));
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00044C3D File Offset: 0x00042E3D
		internal override ODataError ReadError()
		{
			return this.ReadErrorImplementation();
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00044C4D File Offset: 0x00042E4D
		internal override Task<ODataError> ReadErrorAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataError>(() => this.ReadErrorImplementation());
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00044C60 File Offset: 0x00042E60
		internal override ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			return this.ReadEntityReferenceLinksImplementation();
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00044C70 File Offset: 0x00042E70
		internal override Task<ODataEntityReferenceLinks> ReadEntityReferenceLinksAsync(IEdmNavigationProperty navigationProperty)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataEntityReferenceLinks>(() => this.ReadEntityReferenceLinksImplementation());
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00044C83 File Offset: 0x00042E83
		internal override ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			return this.ReadEntityReferenceLinkImplementation();
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00044C8B File Offset: 0x00042E8B
		internal override Task<ODataEntityReferenceLink> ReadEntityReferenceLinkAsync(IEdmNavigationProperty navigationProperty)
		{
			return TaskUtils.GetCompletedTask<ODataEntityReferenceLink>(this.ReadEntityReferenceLinkImplementation());
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00044C98 File Offset: 0x00042E98
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind()
		{
			ODataVerboseJsonPayloadKindDetectionDeserializer odataVerboseJsonPayloadKindDetectionDeserializer = new ODataVerboseJsonPayloadKindDetectionDeserializer(this);
			return odataVerboseJsonPayloadKindDetectionDeserializer.DetectPayloadKind();
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00044CB4 File Offset: 0x00042EB4
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

		// Token: 0x06001237 RID: 4663 RVA: 0x00044CF8 File Offset: 0x00042EF8
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

		// Token: 0x06001238 RID: 4664 RVA: 0x00044D34 File Offset: 0x00042F34
		private static void VerifyCanCreateParameterReader(IEdmFunctionImport functionImport)
		{
			if (functionImport == null)
			{
				throw new ArgumentNullException("functionImport", Strings.ODataJsonInputContext_FunctionImportCannotBeNullForCreateParameterReader("functionImport"));
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00044D4E File Offset: 0x00042F4E
		private ODataReader CreateFeedReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return new ODataVerboseJsonReader(this, entitySet, expectedBaseEntityType, true, null);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00044D5A File Offset: 0x00042F5A
		private ODataReader CreateEntryReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return new ODataVerboseJsonReader(this, entitySet, expectedEntityType, false, null);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00044D66 File Offset: 0x00042F66
		private ODataCollectionReader CreateCollectionReaderImplementation(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataVerboseJsonCollectionReader(this, expectedItemTypeReference, null);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00044D70 File Offset: 0x00042F70
		private ODataParameterReader CreateParameterReaderImplementation(IEdmFunctionImport functionImport)
		{
			return new ODataVerboseJsonParameterReader(this, functionImport);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00044D7C File Offset: 0x00042F7C
		private ODataProperty ReadPropertyImplementation(IEdmStructuralProperty Property, IEdmTypeReference expectedPropertyTypeReference)
		{
			ODataVerboseJsonPropertyAndValueDeserializer odataVerboseJsonPropertyAndValueDeserializer = new ODataVerboseJsonPropertyAndValueDeserializer(this);
			return odataVerboseJsonPropertyAndValueDeserializer.ReadTopLevelProperty(Property, expectedPropertyTypeReference);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00044D98 File Offset: 0x00042F98
		private ODataWorkspace ReadServiceDocumentImplementation()
		{
			ODataVerboseJsonServiceDocumentDeserializer odataVerboseJsonServiceDocumentDeserializer = new ODataVerboseJsonServiceDocumentDeserializer(this);
			return odataVerboseJsonServiceDocumentDeserializer.ReadServiceDocument();
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00044DB4 File Offset: 0x00042FB4
		private ODataError ReadErrorImplementation()
		{
			ODataVerboseJsonErrorDeserializer odataVerboseJsonErrorDeserializer = new ODataVerboseJsonErrorDeserializer(this);
			return odataVerboseJsonErrorDeserializer.ReadTopLevelError();
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00044DD0 File Offset: 0x00042FD0
		private ODataEntityReferenceLinks ReadEntityReferenceLinksImplementation()
		{
			ODataVerboseJsonEntityReferenceLinkDeserializer odataVerboseJsonEntityReferenceLinkDeserializer = new ODataVerboseJsonEntityReferenceLinkDeserializer(this);
			return odataVerboseJsonEntityReferenceLinkDeserializer.ReadEntityReferenceLinks();
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00044DEC File Offset: 0x00042FEC
		private ODataEntityReferenceLink ReadEntityReferenceLinkImplementation()
		{
			ODataVerboseJsonEntityReferenceLinkDeserializer odataVerboseJsonEntityReferenceLinkDeserializer = new ODataVerboseJsonEntityReferenceLinkDeserializer(this);
			return odataVerboseJsonEntityReferenceLinkDeserializer.ReadEntityReferenceLink();
		}

		// Token: 0x04000693 RID: 1683
		private TextReader textReader;

		// Token: 0x04000694 RID: 1684
		private BufferingJsonReader jsonReader;
	}
}
