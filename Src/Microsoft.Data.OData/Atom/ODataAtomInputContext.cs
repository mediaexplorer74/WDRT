using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000228 RID: 552
	internal sealed class ODataAtomInputContext : ODataInputContext
	{
		// Token: 0x0600114A RID: 4426 RVA: 0x00040E10 File Offset: 0x0003F010
		internal ODataAtomInputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
		{
			try
			{
				ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
				ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
				this.baseXmlReader = ODataAtomReaderUtils.CreateXmlReader(messageStream, encoding, messageReaderSettings);
				this.xmlReader = new BufferingXmlReader(this.baseXmlReader, null, messageReaderSettings.BaseUri, base.UseServerFormatBehavior && base.Version < ODataVersion.V3, messageReaderSettings.MessageQuotas.MaxNestingDepth, messageReaderSettings.ReaderBehavior.ODataNamespace);
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && messageStream != null)
				{
					messageStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00040EC4 File Offset: 0x0003F0C4
		internal BufferingXmlReader XmlReader
		{
			get
			{
				return this.xmlReader;
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00040ECC File Offset: 0x0003F0CC
		internal override ODataReader CreateFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return this.CreateFeedReaderImplementation(entitySet, expectedBaseEntityType);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00040EF8 File Offset: 0x0003F0F8
		internal override Task<ODataReader> CreateFeedReaderAsync(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateFeedReaderImplementation(entitySet, expectedBaseEntityType));
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00040F31 File Offset: 0x0003F131
		internal override ODataReader CreateEntryReader(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return this.CreateEntryReaderImplementation(entitySet, expectedEntityType);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00040F5C File Offset: 0x0003F15C
		internal override Task<ODataReader> CreateEntryReaderAsync(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateEntryReaderImplementation(entitySet, expectedEntityType));
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00040F95 File Offset: 0x0003F195
		internal override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return this.CreateCollectionReaderImplementation(expectedItemTypeReference);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00040FBC File Offset: 0x0003F1BC
		internal override Task<ODataCollectionReader> CreateCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionReader>(() => this.CreateCollectionReaderImplementation(expectedItemTypeReference));
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00040FEE File Offset: 0x0003F1EE
		internal override ODataWorkspace ReadServiceDocument()
		{
			return this.ReadServiceDocumentImplementation();
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00040FFE File Offset: 0x0003F1FE
		internal override Task<ODataWorkspace> ReadServiceDocumentAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWorkspace>(() => this.ReadServiceDocumentImplementation());
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00041011 File Offset: 0x0003F211
		internal override ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			return this.ReadPropertyImplementation(property, expectedPropertyTypeReference);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0004103C File Offset: 0x0003F23C
		internal override Task<ODataProperty> ReadPropertyAsync(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataProperty>(() => this.ReadPropertyImplementation(property, expectedPropertyTypeReference));
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00041075 File Offset: 0x0003F275
		internal override ODataError ReadError()
		{
			return this.ReadErrorImplementation();
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00041085 File Offset: 0x0003F285
		internal override Task<ODataError> ReadErrorAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataError>(() => this.ReadErrorImplementation());
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00041098 File Offset: 0x0003F298
		internal override ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			return this.ReadEntityReferenceLinksImplementation();
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x000410A0 File Offset: 0x0003F2A0
		internal override Task<ODataEntityReferenceLinks> ReadEntityReferenceLinksAsync(IEdmNavigationProperty navigationProperty)
		{
			return TaskUtils.GetCompletedTask<ODataEntityReferenceLinks>(this.ReadEntityReferenceLinksImplementation());
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000410AD File Offset: 0x0003F2AD
		internal override ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			return this.ReadEntityReferenceLinkImplementation();
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x000410B5 File Offset: 0x0003F2B5
		internal override Task<ODataEntityReferenceLink> ReadEntityReferenceLinkAsync(IEdmNavigationProperty navigationProperty)
		{
			return TaskUtils.GetCompletedTask<ODataEntityReferenceLink>(this.ReadEntityReferenceLinkImplementation());
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000410C4 File Offset: 0x0003F2C4
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataPayloadKindDetectionInfo detectionInfo)
		{
			ODataAtomPayloadKindDetectionDeserializer odataAtomPayloadKindDetectionDeserializer = new ODataAtomPayloadKindDetectionDeserializer(this);
			return odataAtomPayloadKindDetectionDeserializer.DetectPayloadKind(detectionInfo);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x000410DF File Offset: 0x0003F2DF
		internal void InitializeReaderCustomization()
		{
			this.xmlCustomizationReaders = new Stack<BufferingXmlReader>();
			this.xmlCustomizationReaders.Push(this.xmlReader);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00041100 File Offset: 0x0003F300
		internal void PushCustomReader(XmlReader customXmlReader, Uri xmlBaseUri)
		{
			if (!object.ReferenceEquals(this.xmlReader, customXmlReader))
			{
				BufferingXmlReader bufferingXmlReader = new BufferingXmlReader(customXmlReader, xmlBaseUri, base.MessageReaderSettings.BaseUri, false, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth, base.MessageReaderSettings.ReaderBehavior.ODataNamespace);
				this.xmlCustomizationReaders.Push(bufferingXmlReader);
				this.xmlReader = bufferingXmlReader;
				return;
			}
			this.xmlCustomizationReaders.Push(this.xmlReader);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00041174 File Offset: 0x0003F374
		internal BufferingXmlReader PopCustomReader()
		{
			BufferingXmlReader bufferingXmlReader = this.xmlCustomizationReaders.Pop();
			this.xmlReader = this.xmlCustomizationReaders.Peek();
			return bufferingXmlReader;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x000411A0 File Offset: 0x0003F3A0
		protected override void DisposeImplementation()
		{
			try
			{
				if (this.baseXmlReader != null)
				{
					((IDisposable)this.baseXmlReader).Dispose();
				}
			}
			finally
			{
				this.baseXmlReader = null;
				this.xmlReader = null;
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x000411E4 File Offset: 0x0003F3E4
		private ODataReader CreateFeedReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return new ODataAtomReader(this, entitySet, expectedBaseEntityType, true);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000411EF File Offset: 0x0003F3EF
		private ODataReader CreateEntryReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return new ODataAtomReader(this, entitySet, expectedEntityType, false);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000411FA File Offset: 0x0003F3FA
		private ODataCollectionReader CreateCollectionReaderImplementation(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataAtomCollectionReader(this, expectedItemTypeReference);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00041204 File Offset: 0x0003F404
		private ODataProperty ReadPropertyImplementation(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			ODataAtomPropertyAndValueDeserializer odataAtomPropertyAndValueDeserializer = new ODataAtomPropertyAndValueDeserializer(this);
			return odataAtomPropertyAndValueDeserializer.ReadTopLevelProperty(property, expectedPropertyTypeReference);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00041220 File Offset: 0x0003F420
		private ODataWorkspace ReadServiceDocumentImplementation()
		{
			ODataAtomServiceDocumentDeserializer odataAtomServiceDocumentDeserializer = new ODataAtomServiceDocumentDeserializer(this);
			return odataAtomServiceDocumentDeserializer.ReadServiceDocument();
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0004123C File Offset: 0x0003F43C
		private ODataError ReadErrorImplementation()
		{
			ODataAtomErrorDeserializer odataAtomErrorDeserializer = new ODataAtomErrorDeserializer(this);
			return odataAtomErrorDeserializer.ReadTopLevelError();
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00041258 File Offset: 0x0003F458
		private ODataEntityReferenceLinks ReadEntityReferenceLinksImplementation()
		{
			ODataAtomEntityReferenceLinkDeserializer odataAtomEntityReferenceLinkDeserializer = new ODataAtomEntityReferenceLinkDeserializer(this);
			return odataAtomEntityReferenceLinkDeserializer.ReadEntityReferenceLinks();
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00041274 File Offset: 0x0003F474
		private ODataEntityReferenceLink ReadEntityReferenceLinkImplementation()
		{
			ODataAtomEntityReferenceLinkDeserializer odataAtomEntityReferenceLinkDeserializer = new ODataAtomEntityReferenceLinkDeserializer(this);
			return odataAtomEntityReferenceLinkDeserializer.ReadEntityReferenceLink();
		}

		// Token: 0x0400065F RID: 1631
		private XmlReader baseXmlReader;

		// Token: 0x04000660 RID: 1632
		private BufferingXmlReader xmlReader;

		// Token: 0x04000661 RID: 1633
		private Stack<BufferingXmlReader> xmlCustomizationReaders;
	}
}
