using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000FB RID: 251
	internal sealed class ODataAtomOutputContext : ODataOutputContext
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x000177A8 File Offset: 0x000159A8
		internal ODataAtomOutputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
			try
			{
				this.messageOutputStream = messageStream;
				Stream stream;
				if (synchronous)
				{
					stream = messageStream;
				}
				else
				{
					this.asynchronousOutputStream = new AsyncBufferedStream(messageStream);
					stream = this.asynchronousOutputStream;
				}
				this.xmlRootWriter = ODataAtomWriterUtils.CreateXmlWriter(stream, messageWriterSettings, encoding);
				this.xmlWriter = this.xmlRootWriter;
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

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00017834 File Offset: 0x00015A34
		internal XmlWriter XmlWriter
		{
			get
			{
				return this.xmlWriter;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001783C File Offset: 0x00015A3C
		internal AtomAndVerboseJsonTypeNameOracle TypeNameOracle
		{
			get
			{
				return this.typeNameOracle;
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00017844 File Offset: 0x00015A44
		internal void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001785F File Offset: 0x00015A5F
		internal void Flush()
		{
			this.xmlWriter.Flush();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00017891 File Offset: 0x00015A91
		internal Task FlushAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.xmlWriter.Flush();
				return this.asynchronousOutputStream.FlushAsync();
			}).FollowOnSuccessWithTask((Task asyncBufferedStreamFlushTask) => this.messageOutputStream.FlushAsync());
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000178B5 File Offset: 0x00015AB5
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			this.WriteInStreamErrorImplementation(error, includeDebugInformation);
			this.Flush();
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000178F4 File Offset: 0x00015AF4
		internal override Task WriteInStreamErrorAsync(ODataError error, bool includeDebugInformation)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteInStreamErrorImplementation(error, includeDebugInformation);
				return this.FlushAsync();
			});
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001792D File Offset: 0x00015B2D
		internal override ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataFeedWriterImplementation(entitySet, entityType);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00017958 File Offset: 0x00015B58
		internal override Task<ODataWriter> CreateODataFeedWriterAsync(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(() => this.CreateODataFeedWriterImplementation(entitySet, entityType));
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00017991 File Offset: 0x00015B91
		internal override ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataEntryWriterImplementation(entitySet, entityType);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000179BC File Offset: 0x00015BBC
		internal override Task<ODataWriter> CreateODataEntryWriterAsync(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(() => this.CreateODataEntryWriterImplementation(entitySet, entityType));
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000179F5 File Offset: 0x00015BF5
		internal override ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			return this.CreateODataCollectionWriterImplementation(itemTypeReference);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00017A1C File Offset: 0x00015C1C
		internal override Task<ODataCollectionWriter> CreateODataCollectionWriterAsync(IEdmTypeReference itemTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionWriter>(() => this.CreateODataCollectionWriterImplementation(itemTypeReference));
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00017A4E File Offset: 0x00015C4E
		internal override void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			this.WriteServiceDocumentImplementation(defaultWorkspace);
			this.Flush();
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00017A84 File Offset: 0x00015C84
		internal override Task WriteServiceDocumentAsync(ODataWorkspace defaultWorkspace)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteServiceDocumentImplementation(defaultWorkspace);
				return this.FlushAsync();
			});
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00017AB6 File Offset: 0x00015CB6
		internal override void WriteProperty(ODataProperty property)
		{
			this.WritePropertyImplementation(property);
			this.Flush();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00017AEC File Offset: 0x00015CEC
		internal override Task WritePropertyAsync(ODataProperty property)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WritePropertyImplementation(property);
				return this.FlushAsync();
			});
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00017B1E File Offset: 0x00015D1E
		internal override void WriteError(ODataError error, bool includeDebugInformation)
		{
			this.WriteErrorImplementation(error, includeDebugInformation);
			this.Flush();
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00017B5C File Offset: 0x00015D5C
		internal override Task WriteErrorAsync(ODataError error, bool includeDebugInformation)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteErrorImplementation(error, includeDebugInformation);
				return this.FlushAsync();
			});
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00017B95 File Offset: 0x00015D95
		internal override void WriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinksImplementation(links);
			this.Flush();
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00017BCC File Offset: 0x00015DCC
		internal override Task WriteEntityReferenceLinksAsync(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteEntityReferenceLinksImplementation(links);
				return this.FlushAsync();
			});
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00017BFE File Offset: 0x00015DFE
		internal override void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinkImplementation(link);
			this.Flush();
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00017C34 File Offset: 0x00015E34
		internal override Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteEntityReferenceLinkImplementation(link);
				return this.FlushAsync();
			});
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00017C66 File Offset: 0x00015E66
		internal void InitializeWriterCustomization()
		{
			this.xmlCustomizationWriters = new Stack<XmlWriter>();
			this.xmlCustomizationWriters.Push(this.xmlRootWriter);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00017C84 File Offset: 0x00015E84
		internal void PushCustomWriter(XmlWriter customXmlWriter)
		{
			this.xmlCustomizationWriters.Push(customXmlWriter);
			this.xmlWriter = customXmlWriter;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00017C9C File Offset: 0x00015E9C
		internal XmlWriter PopCustomWriter()
		{
			XmlWriter xmlWriter = this.xmlCustomizationWriters.Pop();
			this.xmlWriter = this.xmlCustomizationWriters.Peek();
			return xmlWriter;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00017CC8 File Offset: 0x00015EC8
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			try
			{
				if (this.messageOutputStream != null)
				{
					this.xmlRootWriter.Flush();
					if (this.asynchronousOutputStream != null)
					{
						this.asynchronousOutputStream.FlushSync();
						this.asynchronousOutputStream.Dispose();
					}
					this.messageOutputStream.Dispose();
				}
			}
			finally
			{
				this.messageOutputStream = null;
				this.asynchronousOutputStream = null;
				this.xmlWriter = null;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00017D40 File Offset: 0x00015F40
		private void WriteInStreamErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			ODataAtomWriterUtils.WriteError(this.xmlWriter, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00017D74 File Offset: 0x00015F74
		private ODataWriter CreateODataFeedWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataAtomWriter odataAtomWriter = new ODataAtomWriter(this, entitySet, entityType, true);
			this.outputInStreamErrorListener = odataAtomWriter;
			return odataAtomWriter;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00017D94 File Offset: 0x00015F94
		private ODataWriter CreateODataEntryWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataAtomWriter odataAtomWriter = new ODataAtomWriter(this, entitySet, entityType, false);
			this.outputInStreamErrorListener = odataAtomWriter;
			return odataAtomWriter;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00017DB4 File Offset: 0x00015FB4
		private ODataCollectionWriter CreateODataCollectionWriterImplementation(IEdmTypeReference itemTypeReference)
		{
			ODataAtomCollectionWriter odataAtomCollectionWriter = new ODataAtomCollectionWriter(this, itemTypeReference);
			this.outputInStreamErrorListener = odataAtomCollectionWriter;
			return odataAtomCollectionWriter;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00017DD4 File Offset: 0x00015FD4
		private void WritePropertyImplementation(ODataProperty property)
		{
			ODataAtomPropertyAndValueSerializer odataAtomPropertyAndValueSerializer = new ODataAtomPropertyAndValueSerializer(this);
			odataAtomPropertyAndValueSerializer.WriteTopLevelProperty(property);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00017DF0 File Offset: 0x00015FF0
		private void WriteServiceDocumentImplementation(ODataWorkspace defaultWorkspace)
		{
			ODataAtomServiceDocumentSerializer odataAtomServiceDocumentSerializer = new ODataAtomServiceDocumentSerializer(this);
			odataAtomServiceDocumentSerializer.WriteServiceDocument(defaultWorkspace);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00017E0C File Offset: 0x0001600C
		private void WriteErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			ODataAtomSerializer odataAtomSerializer = new ODataAtomSerializer(this);
			odataAtomSerializer.WriteTopLevelError(error, includeDebugInformation);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00017E28 File Offset: 0x00016028
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks links)
		{
			ODataAtomEntityReferenceLinkSerializer odataAtomEntityReferenceLinkSerializer = new ODataAtomEntityReferenceLinkSerializer(this);
			odataAtomEntityReferenceLinkSerializer.WriteEntityReferenceLinks(links);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00017E44 File Offset: 0x00016044
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink link)
		{
			ODataAtomEntityReferenceLinkSerializer odataAtomEntityReferenceLinkSerializer = new ODataAtomEntityReferenceLinkSerializer(this);
			odataAtomEntityReferenceLinkSerializer.WriteEntityReferenceLink(link);
		}

		// Token: 0x04000291 RID: 657
		private readonly AtomAndVerboseJsonTypeNameOracle typeNameOracle = new AtomAndVerboseJsonTypeNameOracle();

		// Token: 0x04000292 RID: 658
		private Stream messageOutputStream;

		// Token: 0x04000293 RID: 659
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x04000294 RID: 660
		private XmlWriter xmlRootWriter;

		// Token: 0x04000295 RID: 661
		private XmlWriter xmlWriter;

		// Token: 0x04000296 RID: 662
		private Stack<XmlWriter> xmlCustomizationWriters;

		// Token: 0x04000297 RID: 663
		private IODataOutputInStreamErrorListener outputInStreamErrorListener;
	}
}
