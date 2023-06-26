using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x020000FA RID: 250
	internal abstract class ODataOutputContext : IDisposable
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x00017574 File Offset: 0x00015774
		protected ODataOutputContext(ODataFormat format, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			this.format = format;
			this.messageWriterSettings = messageWriterSettings;
			this.writingResponse = writingResponse;
			this.synchronous = synchronous;
			this.model = model ?? EdmCoreModel.Instance;
			this.urlResolver = urlResolver;
			this.edmTypeResolver = EdmTypeWriterResolver.Instance;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x000175DE File Offset: 0x000157DE
		internal ODataMessageWriterSettings MessageWriterSettings
		{
			get
			{
				return this.messageWriterSettings;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x000175E8 File Offset: 0x000157E8
		internal ODataVersion Version
		{
			get
			{
				return this.messageWriterSettings.Version.Value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00017608 File Offset: 0x00015808
		internal bool WritingResponse
		{
			get
			{
				return this.writingResponse;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00017610 File Offset: 0x00015810
		internal bool Synchronous
		{
			get
			{
				return this.synchronous;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00017618 File Offset: 0x00015818
		internal IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00017620 File Offset: 0x00015820
		internal IODataUrlResolver UrlResolver
		{
			get
			{
				return this.urlResolver;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00017628 File Offset: 0x00015828
		internal EdmTypeResolver EdmTypeResolver
		{
			get
			{
				return this.edmTypeResolver;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00017630 File Offset: 0x00015830
		protected internal bool UseClientFormatBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00017645 File Offset: 0x00015845
		protected internal bool UseServerFormatBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesServer;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001765A File Offset: 0x0001585A
		protected internal bool UseDefaultFormatBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.FormatBehaviorKind == ODataBehaviorKind.Default;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001766F File Offset: 0x0001586F
		protected internal bool UseServerApiBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesServer;
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00017684 File Offset: 0x00015884
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00017693 File Offset: 0x00015893
		internal virtual void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001769D File Offset: 0x0001589D
		internal virtual Task WriteInStreamErrorAsync(ODataError error, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000176A7 File Offset: 0x000158A7
		internal virtual ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Feed);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000176B0 File Offset: 0x000158B0
		internal virtual Task<ODataWriter> CreateODataFeedWriterAsync(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Feed);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000176B9 File Offset: 0x000158B9
		internal virtual ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Entry);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000176C2 File Offset: 0x000158C2
		internal virtual Task<ODataWriter> CreateODataEntryWriterAsync(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Entry);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000176CB File Offset: 0x000158CB
		internal virtual ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000176D4 File Offset: 0x000158D4
		internal virtual Task<ODataCollectionWriter> CreateODataCollectionWriterAsync(IEdmTypeReference itemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000176DD File Offset: 0x000158DD
		internal virtual ODataBatchWriter CreateODataBatchWriter(string batchBoundary)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000176E7 File Offset: 0x000158E7
		internal virtual Task<ODataBatchWriter> CreateODataBatchWriterAsync(string batchBoundary)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000176F1 File Offset: 0x000158F1
		internal virtual ODataParameterWriter CreateODataParameterWriter(IEdmFunctionImport functionImport)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000176FB File Offset: 0x000158FB
		internal virtual Task<ODataParameterWriter> CreateODataParameterWriterAsync(IEdmFunctionImport functionImport)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00017705 File Offset: 0x00015905
		internal virtual void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001770E File Offset: 0x0001590E
		internal virtual Task WriteServiceDocumentAsync(ODataWorkspace defaultWorkspace)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00017717 File Offset: 0x00015917
		internal virtual void WriteProperty(ODataProperty property)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00017720 File Offset: 0x00015920
		internal virtual Task WritePropertyAsync(ODataProperty property)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00017729 File Offset: 0x00015929
		internal virtual void WriteError(ODataError error, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00017733 File Offset: 0x00015933
		internal virtual Task WriteErrorAsync(ODataError error, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001773D File Offset: 0x0001593D
		internal virtual void WriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00017746 File Offset: 0x00015946
		internal virtual Task WriteEntityReferenceLinksAsync(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001774F File Offset: 0x0001594F
		internal virtual void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00017758 File Offset: 0x00015958
		internal virtual Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00017761 File Offset: 0x00015961
		internal virtual void WriteValue(object value)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001776A File Offset: 0x0001596A
		internal virtual Task WriteValueAsync(object value)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00017773 File Offset: 0x00015973
		internal virtual void WriteMetadataDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.MetadataDocument);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001777D File Offset: 0x0001597D
		[Conditional("DEBUG")]
		internal void AssertSynchronous()
		{
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001777F File Offset: 0x0001597F
		[Conditional("DEBUG")]
		internal void AssertAsynchronous()
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00017781 File Offset: 0x00015981
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00017783 File Offset: 0x00015983
		private ODataException CreatePayloadKindNotSupportedException(ODataPayloadKind payloadKind)
		{
			return new ODataException(Strings.ODataOutputContext_UnsupportedPayloadKindForFormat(this.format.ToString(), payloadKind.ToString()));
		}

		// Token: 0x0400028A RID: 650
		private readonly ODataFormat format;

		// Token: 0x0400028B RID: 651
		private readonly ODataMessageWriterSettings messageWriterSettings;

		// Token: 0x0400028C RID: 652
		private readonly bool writingResponse;

		// Token: 0x0400028D RID: 653
		private readonly bool synchronous;

		// Token: 0x0400028E RID: 654
		private readonly IEdmModel model;

		// Token: 0x0400028F RID: 655
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x04000290 RID: 656
		private readonly EdmTypeResolver edmTypeResolver;
	}
}
