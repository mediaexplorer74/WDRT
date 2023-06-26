using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000193 RID: 403
	internal abstract class ODataInputContext : IDisposable
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002941C File Offset: 0x0002761C
		protected ODataInputContext(ODataFormat format, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			this.format = format;
			this.messageReaderSettings = messageReaderSettings;
			this.version = version;
			this.readingResponse = readingResponse;
			this.synchronous = synchronous;
			this.model = model;
			this.urlResolver = urlResolver;
			this.edmTypeResolver = new EdmTypeReaderResolver(this.Model, this.MessageReaderSettings.ReaderBehavior, this.Version);
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0002949C File Offset: 0x0002769C
		internal ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.messageReaderSettings;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x000294A4 File Offset: 0x000276A4
		internal ODataVersion Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x000294AC File Offset: 0x000276AC
		internal bool ReadingResponse
		{
			get
			{
				return this.readingResponse;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x000294B4 File Offset: 0x000276B4
		internal bool Synchronous
		{
			get
			{
				return this.synchronous;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x000294BC File Offset: 0x000276BC
		internal IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x000294C4 File Offset: 0x000276C4
		internal EdmTypeResolver EdmTypeResolver
		{
			get
			{
				return this.edmTypeResolver;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x000294CC File Offset: 0x000276CC
		internal IODataUrlResolver UrlResolver
		{
			get
			{
				return this.urlResolver;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x000294D4 File Offset: 0x000276D4
		protected internal bool UseClientFormatBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x000294E9 File Offset: 0x000276E9
		protected internal bool UseServerFormatBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesServer;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x000294FE File Offset: 0x000276FE
		protected internal bool UseDefaultFormatBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.FormatBehaviorKind == ODataBehaviorKind.Default;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x00029513 File Offset: 0x00027713
		protected internal bool UseClientApiBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesClient;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00029528 File Offset: 0x00027728
		protected internal bool UseServerApiBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesServer;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0002953D File Offset: 0x0002773D
		protected internal bool UseDefaultApiBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.Default;
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00029552 File Offset: 0x00027752
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00029561 File Offset: 0x00027761
		internal virtual ODataReader CreateFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Feed);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002956A File Offset: 0x0002776A
		internal virtual Task<ODataReader> CreateFeedReaderAsync(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Feed);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00029573 File Offset: 0x00027773
		internal virtual ODataReader CreateEntryReader(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Entry);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002957C File Offset: 0x0002777C
		internal virtual Task<ODataReader> CreateEntryReaderAsync(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Entry);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00029585 File Offset: 0x00027785
		internal virtual ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002958E File Offset: 0x0002778E
		internal virtual Task<ODataCollectionReader> CreateCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00029597 File Offset: 0x00027797
		internal virtual ODataBatchReader CreateBatchReader(string batchBoundary)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x000295A1 File Offset: 0x000277A1
		internal virtual Task<ODataBatchReader> CreateBatchReaderAsync(string batchBoundary)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000295AB File Offset: 0x000277AB
		internal virtual ODataParameterReader CreateParameterReader(IEdmFunctionImport functionImport)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Parameter);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000295B5 File Offset: 0x000277B5
		internal virtual Task<ODataParameterReader> CreateParameterReaderAsync(IEdmFunctionImport functionImport)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Parameter);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000295BF File Offset: 0x000277BF
		internal virtual ODataWorkspace ReadServiceDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000295C8 File Offset: 0x000277C8
		internal virtual Task<ODataWorkspace> ReadServiceDocumentAsync()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000295D1 File Offset: 0x000277D1
		internal virtual IEdmModel ReadMetadataDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.MetadataDocument);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000295DB File Offset: 0x000277DB
		internal virtual ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000295E4 File Offset: 0x000277E4
		internal virtual Task<ODataProperty> ReadPropertyAsync(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000295ED File Offset: 0x000277ED
		internal virtual ODataError ReadError()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000295F7 File Offset: 0x000277F7
		internal virtual Task<ODataError> ReadErrorAsync()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00029601 File Offset: 0x00027801
		internal virtual ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002960A File Offset: 0x0002780A
		internal virtual Task<ODataEntityReferenceLinks> ReadEntityReferenceLinksAsync(IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00029613 File Offset: 0x00027813
		internal virtual ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002961C File Offset: 0x0002781C
		internal virtual Task<ODataEntityReferenceLink> ReadEntityReferenceLinkAsync(IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00029625 File Offset: 0x00027825
		internal virtual object ReadValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002962E File Offset: 0x0002782E
		internal virtual Task<object> ReadValueAsync(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00029637 File Offset: 0x00027837
		internal void VerifyNotDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00029652 File Offset: 0x00027852
		[Conditional("DEBUG")]
		internal void AssertSynchronous()
		{
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00029654 File Offset: 0x00027854
		[Conditional("DEBUG")]
		internal void AssertAsynchronous()
		{
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00029656 File Offset: 0x00027856
		internal DuplicatePropertyNamesChecker CreateDuplicatePropertyNamesChecker()
		{
			return new DuplicatePropertyNamesChecker(this.MessageReaderSettings.ReaderBehavior.AllowDuplicatePropertyNames, this.ReadingResponse);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00029673 File Offset: 0x00027873
		internal Uri ResolveUri(Uri baseUri, Uri payloadUri)
		{
			if (this.UrlResolver != null)
			{
				return this.UrlResolver.ResolveUrl(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x06000BCF RID: 3023
		protected abstract void DisposeImplementation();

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002968C File Offset: 0x0002788C
		private void Dispose(bool disposing)
		{
			this.disposed = true;
			if (disposing)
			{
				this.DisposeImplementation();
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002969E File Offset: 0x0002789E
		private ODataException CreatePayloadKindNotSupportedException(ODataPayloadKind payloadKind)
		{
			return new ODataException(Strings.ODataInputContext_UnsupportedPayloadKindForFormat(this.format.ToString(), payloadKind.ToString()));
		}

		// Token: 0x04000421 RID: 1057
		private readonly ODataFormat format;

		// Token: 0x04000422 RID: 1058
		private readonly ODataMessageReaderSettings messageReaderSettings;

		// Token: 0x04000423 RID: 1059
		private readonly ODataVersion version;

		// Token: 0x04000424 RID: 1060
		private readonly bool readingResponse;

		// Token: 0x04000425 RID: 1061
		private readonly bool synchronous;

		// Token: 0x04000426 RID: 1062
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x04000427 RID: 1063
		private readonly IEdmModel model;

		// Token: 0x04000428 RID: 1064
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x04000429 RID: 1065
		private bool disposed;
	}
}
