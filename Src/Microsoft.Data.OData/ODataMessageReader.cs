using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000250 RID: 592
	public sealed class ODataMessageReader : IDisposable
	{
		// Token: 0x06001319 RID: 4889 RVA: 0x00047A43 File Offset: 0x00045C43
		public ODataMessageReader(IODataRequestMessage requestMessage)
			: this(requestMessage, new ODataMessageReaderSettings())
		{
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00047A51 File Offset: 0x00045C51
		public ODataMessageReader(IODataRequestMessage requestMessage, ODataMessageReaderSettings settings)
			: this(requestMessage, settings, null)
		{
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00047A5C File Offset: 0x00045C5C
		public ODataMessageReader(IODataRequestMessage requestMessage, ODataMessageReaderSettings settings, IEdmModel model)
		{
			this.readerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			this.settings = ((settings == null) ? new ODataMessageReaderSettings() : new ODataMessageReaderSettings(settings));
			ReaderValidationUtils.ValidateMessageReaderSettings(this.settings, false);
			this.readingResponse = false;
			this.message = new ODataRequestMessage(requestMessage, false, this.settings.DisableMessageStreamDisposal, this.settings.MessageQuotas.MaxReceivedMessageSize);
			this.urlResolver = requestMessage as IODataUrlResolver;
			this.version = ODataUtilsInternal.GetDataServiceVersion(this.message, this.settings.MaxProtocolVersion);
			this.model = model ?? EdmCoreModel.Instance;
			this.edmTypeResolver = new EdmTypeReaderResolver(this.model, this.settings.ReaderBehavior, this.version);
			ODataVersionChecker.CheckVersionSupported(this.version, this.settings);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00047B41 File Offset: 0x00045D41
		public ODataMessageReader(IODataResponseMessage responseMessage)
			: this(responseMessage, new ODataMessageReaderSettings())
		{
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00047B4F File Offset: 0x00045D4F
		public ODataMessageReader(IODataResponseMessage responseMessage, ODataMessageReaderSettings settings)
			: this(responseMessage, settings, null)
		{
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00047B5C File Offset: 0x00045D5C
		public ODataMessageReader(IODataResponseMessage responseMessage, ODataMessageReaderSettings settings, IEdmModel model)
		{
			this.readerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			this.settings = ((settings == null) ? new ODataMessageReaderSettings() : new ODataMessageReaderSettings(settings));
			ReaderValidationUtils.ValidateMessageReaderSettings(this.settings, true);
			this.readingResponse = true;
			this.message = new ODataResponseMessage(responseMessage, false, this.settings.DisableMessageStreamDisposal, this.settings.MessageQuotas.MaxReceivedMessageSize);
			this.urlResolver = responseMessage as IODataUrlResolver;
			this.version = ODataUtilsInternal.GetDataServiceVersion(this.message, this.settings.MaxProtocolVersion);
			this.model = model ?? EdmCoreModel.Instance;
			this.edmTypeResolver = new EdmTypeReaderResolver(this.model, this.settings.ReaderBehavior, this.version);
			string annotationFilter = responseMessage.PreferenceAppliedHeader().AnnotationFilter;
			if (this.settings.ShouldIncludeAnnotation == null && !string.IsNullOrEmpty(annotationFilter))
			{
				this.settings.ShouldIncludeAnnotation = ODataUtils.CreateAnnotationFilter(annotationFilter);
			}
			ODataVersionChecker.CheckVersionSupported(this.version, this.settings);
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00047C73 File Offset: 0x00045E73
		internal ODataMessageReaderSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00047C7B File Offset: 0x00045E7B
		private MediaTypeResolver MediaTypeResolver
		{
			get
			{
				if (this.mediaTypeResolver == null)
				{
					this.mediaTypeResolver = MediaTypeResolver.CreateReaderMediaTypeResolver(this.version, this.settings.ReaderBehavior.FormatBehaviorKind);
				}
				return this.mediaTypeResolver;
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00047CD4 File Offset: 0x00045ED4
		public IEnumerable<ODataPayloadKindDetectionResult> DetectPayloadKind()
		{
			if (this.settings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesServer)
			{
				throw new ODataException(Strings.ODataMessageReader_PayloadKindDetectionInServerMode);
			}
			IEnumerable<ODataPayloadKindDetectionResult> enumerable;
			if (this.TryGetSinglePayloadKindResultFromContentType(out enumerable))
			{
				return enumerable;
			}
			this.payloadKindDetectionFormatStates = new Dictionary<ODataFormat, object>(ReferenceEqualityComparer<ODataFormat>.Instance);
			List<ODataPayloadKindDetectionResult> list = new List<ODataPayloadKindDetectionResult>();
			try
			{
				IEnumerable<IGrouping<ODataFormat, ODataPayloadKindDetectionResult>> enumerable2 = from kvp in enumerable
					group kvp by kvp.Format;
				foreach (IGrouping<ODataFormat, ODataPayloadKindDetectionResult> grouping in enumerable2)
				{
					ODataPayloadKindDetectionInfo odataPayloadKindDetectionInfo = new ODataPayloadKindDetectionInfo(this.contentType, this.encoding, this.settings, this.model, grouping.Select((ODataPayloadKindDetectionResult pkg) => pkg.PayloadKind));
					IEnumerable<ODataPayloadKind> enumerable3 = (this.readingResponse ? grouping.Key.DetectPayloadKind((IODataResponseMessage)this.message, odataPayloadKindDetectionInfo) : grouping.Key.DetectPayloadKind((IODataRequestMessage)this.message, odataPayloadKindDetectionInfo));
					if (enumerable3 != null)
					{
						using (IEnumerator<ODataPayloadKind> enumerator2 = enumerable3.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ODataPayloadKind kind = enumerator2.Current;
								if (enumerable.Any((ODataPayloadKindDetectionResult pk) => pk.PayloadKind == kind))
								{
									list.Add(new ODataPayloadKindDetectionResult(kind, grouping.Key));
								}
							}
						}
					}
					this.payloadKindDetectionFormatStates.Add(grouping.Key, odataPayloadKindDetectionInfo.PayloadKindDetectionFormatState);
				}
			}
			finally
			{
				this.message.UseBufferingReadStream = new bool?(false);
				this.message.BufferingReadStream.StopBuffering();
			}
			list.Sort(new Comparison<ODataPayloadKindDetectionResult>(this.ComparePayloadKindDetectionResult));
			return list;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00047F50 File Offset: 0x00046150
		public Task<IEnumerable<ODataPayloadKindDetectionResult>> DetectPayloadKindAsync()
		{
			if (this.settings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesServer)
			{
				throw new ODataException(Strings.ODataMessageReader_PayloadKindDetectionInServerMode);
			}
			IEnumerable<ODataPayloadKindDetectionResult> enumerable;
			if (this.TryGetSinglePayloadKindResultFromContentType(out enumerable))
			{
				return TaskUtils.GetCompletedTask<IEnumerable<ODataPayloadKindDetectionResult>>(enumerable);
			}
			this.payloadKindDetectionFormatStates = new Dictionary<ODataFormat, object>(ReferenceEqualityComparer<ODataFormat>.Instance);
			List<ODataPayloadKindDetectionResult> detectedPayloadKinds = new List<ODataPayloadKindDetectionResult>();
			return Task.Factory.Iterate(this.GetPayloadKindDetectionTasks(enumerable, detectedPayloadKinds)).FollowAlwaysWith(delegate(Task t)
			{
				this.message.UseBufferingReadStream = new bool?(false);
				this.message.BufferingReadStream.StopBuffering();
			}).FollowOnSuccessWith(delegate(Task t)
			{
				detectedPayloadKinds.Sort(new Comparison<ODataPayloadKindDetectionResult>(this.ComparePayloadKindDetectionResult));
				return detectedPayloadKinds;
			});
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00047FED File Offset: 0x000461ED
		public ODataReader CreateODataFeedReader()
		{
			return this.CreateODataFeedReader(null, null);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00047FF7 File Offset: 0x000461F7
		public ODataReader CreateODataFeedReader(IEdmEntityType expectedBaseEntityType)
		{
			return this.CreateODataFeedReader(null, expectedBaseEntityType);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00048020 File Offset: 0x00046220
		public ODataReader CreateODataFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataFeedReader(entitySet, expectedBaseEntityType);
			expectedBaseEntityType = expectedBaseEntityType ?? this.edmTypeResolver.GetElementType(entitySet);
			Func<ODataInputContext, ODataReader> func = (ODataInputContext context) => context.CreateFeedReader(entitySet, expectedBaseEntityType);
			ODataPayloadKind[] array = new ODataPayloadKind[1];
			return this.ReadFromInput<ODataReader>(func, array);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0004808E File Offset: 0x0004628E
		public Task<ODataReader> CreateODataFeedReaderAsync()
		{
			return this.CreateODataFeedReaderAsync(null, null);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00048098 File Offset: 0x00046298
		public Task<ODataReader> CreateODataFeedReaderAsync(IEdmEntityType expectedBaseEntityType)
		{
			return this.CreateODataFeedReaderAsync(null, expectedBaseEntityType);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x000480C0 File Offset: 0x000462C0
		public Task<ODataReader> CreateODataFeedReaderAsync(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataFeedReader(entitySet, expectedBaseEntityType);
			expectedBaseEntityType = expectedBaseEntityType ?? this.edmTypeResolver.GetElementType(entitySet);
			Func<ODataInputContext, Task<ODataReader>> func = (ODataInputContext context) => context.CreateFeedReaderAsync(entitySet, expectedBaseEntityType);
			ODataPayloadKind[] array = new ODataPayloadKind[1];
			return this.ReadFromInputAsync<ODataReader>(func, array);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004812E File Offset: 0x0004632E
		public ODataReader CreateODataEntryReader()
		{
			return this.CreateODataEntryReader(null, null);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00048138 File Offset: 0x00046338
		public ODataReader CreateODataEntryReader(IEdmEntityType entityType)
		{
			return this.CreateODataEntryReader(null, entityType);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00048160 File Offset: 0x00046360
		public ODataReader CreateODataEntryReader(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataEntryReader(entitySet, entityType);
			entityType = entityType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateEntryReader(entitySet, entityType), new ODataPayloadKind[] { ODataPayloadKind.Entry });
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000481D2 File Offset: 0x000463D2
		public Task<ODataReader> CreateODataEntryReaderAsync()
		{
			return this.CreateODataEntryReaderAsync(null, null);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000481DC File Offset: 0x000463DC
		public Task<ODataReader> CreateODataEntryReaderAsync(IEdmEntityType entityType)
		{
			return this.CreateODataEntryReaderAsync(null, entityType);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00048204 File Offset: 0x00046404
		public Task<ODataReader> CreateODataEntryReaderAsync(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataEntryReader(entitySet, entityType);
			entityType = entityType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInputAsync<ODataReader>((ODataInputContext context) => context.CreateEntryReaderAsync(entitySet, entityType), new ODataPayloadKind[] { ODataPayloadKind.Entry });
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00048276 File Offset: 0x00046476
		public ODataCollectionReader CreateODataCollectionReader()
		{
			return this.CreateODataCollectionReader(null);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00048298 File Offset: 0x00046498
		public ODataCollectionReader CreateODataCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateODataCollectionReader(expectedItemTypeReference);
			return this.ReadFromInput<ODataCollectionReader>((ODataInputContext context) => context.CreateCollectionReader(expectedItemTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Collection });
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x000482DC File Offset: 0x000464DC
		public Task<ODataCollectionReader> CreateODataCollectionReaderAsync()
		{
			return this.CreateODataCollectionReaderAsync(null);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000482FC File Offset: 0x000464FC
		public Task<ODataCollectionReader> CreateODataCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateODataCollectionReader(expectedItemTypeReference);
			return this.ReadFromInputAsync<ODataCollectionReader>((ODataInputContext context) => context.CreateCollectionReaderAsync(expectedItemTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Collection });
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00048350 File Offset: 0x00046550
		public ODataBatchReader CreateODataBatchReader()
		{
			this.VerifyCanCreateODataBatchReader();
			return this.ReadFromInput<ODataBatchReader>((ODataInputContext context) => context.CreateBatchReader(this.batchBoundary), new ODataPayloadKind[] { ODataPayloadKind.Batch });
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00048390 File Offset: 0x00046590
		public Task<ODataBatchReader> CreateODataBatchReaderAsync()
		{
			this.VerifyCanCreateODataBatchReader();
			return this.ReadFromInputAsync<ODataBatchReader>((ODataInputContext context) => context.CreateBatchReaderAsync(this.batchBoundary), new ODataPayloadKind[] { ODataPayloadKind.Batch });
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000483D8 File Offset: 0x000465D8
		public ODataParameterReader CreateODataParameterReader(IEdmFunctionImport functionImport)
		{
			this.VerifyCanCreateODataParameterReader(functionImport);
			return this.ReadFromInput<ODataParameterReader>((ODataInputContext context) => context.CreateParameterReader(functionImport), new ODataPayloadKind[] { ODataPayloadKind.Parameter });
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00048434 File Offset: 0x00046634
		public Task<ODataParameterReader> CreateODataParameterReaderAsync(IEdmFunctionImport functionImport)
		{
			this.VerifyCanCreateODataParameterReader(functionImport);
			return this.ReadFromInputAsync<ODataParameterReader>((ODataInputContext context) => context.CreateParameterReaderAsync(functionImport), new ODataPayloadKind[] { ODataPayloadKind.Parameter });
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00048484 File Offset: 0x00046684
		public ODataWorkspace ReadServiceDocument()
		{
			this.VerifyCanReadServiceDocument();
			return this.ReadFromInput<ODataWorkspace>((ODataInputContext context) => context.ReadServiceDocument(), new ODataPayloadKind[] { ODataPayloadKind.ServiceDocument });
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000484D0 File Offset: 0x000466D0
		public Task<ODataWorkspace> ReadServiceDocumentAsync()
		{
			this.VerifyCanReadServiceDocument();
			return this.ReadFromInputAsync<ODataWorkspace>((ODataInputContext context) => context.ReadServiceDocumentAsync(), new ODataPayloadKind[] { ODataPayloadKind.ServiceDocument });
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00048512 File Offset: 0x00046712
		public ODataProperty ReadProperty()
		{
			return this.ReadProperty(null);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00048534 File Offset: 0x00046734
		public ODataProperty ReadProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty(expectedPropertyTypeReference);
			return this.ReadFromInput<ODataProperty>((ODataInputContext context) => context.ReadProperty(null, expectedPropertyTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0004859C File Offset: 0x0004679C
		public ODataProperty ReadProperty(IEdmStructuralProperty property)
		{
			this.VerifyCanReadProperty(property);
			return this.ReadFromInput<ODataProperty>((ODataInputContext context) => context.ReadProperty(property, property.Type), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000485E0 File Offset: 0x000467E0
		public Task<ODataProperty> ReadPropertyAsync()
		{
			return this.ReadPropertyAsync(null);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00048600 File Offset: 0x00046800
		public Task<ODataProperty> ReadPropertyAsync(IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty(expectedPropertyTypeReference);
			return this.ReadFromInputAsync<ODataProperty>((ODataInputContext context) => context.ReadPropertyAsync(null, expectedPropertyTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00048668 File Offset: 0x00046868
		public Task<ODataProperty> ReadPropertyAsync(IEdmStructuralProperty property)
		{
			this.VerifyCanReadProperty(property);
			return this.ReadFromInputAsync<ODataProperty>((ODataInputContext context) => context.ReadPropertyAsync(property, property.Type), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000486B4 File Offset: 0x000468B4
		public ODataError ReadError()
		{
			this.VerifyCanReadError();
			return this.ReadFromInput<ODataError>((ODataInputContext context) => context.ReadError(), new ODataPayloadKind[] { ODataPayloadKind.Error });
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00048700 File Offset: 0x00046900
		public Task<ODataError> ReadErrorAsync()
		{
			this.VerifyCanReadError();
			return this.ReadFromInputAsync<ODataError>((ODataInputContext context) => context.ReadErrorAsync(), new ODataPayloadKind[] { ODataPayloadKind.Error });
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00048743 File Offset: 0x00046943
		public ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			return this.ReadEntityReferenceLinks(null);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00048764 File Offset: 0x00046964
		public ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanReadEntityReferenceLinks(navigationProperty);
			return this.ReadFromInput<ODataEntityReferenceLinks>((ODataInputContext context) => context.ReadEntityReferenceLinks(navigationProperty), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLinks });
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000487A8 File Offset: 0x000469A8
		public Task<ODataEntityReferenceLinks> ReadEntityReferenceLinksAsync()
		{
			return this.ReadEntityReferenceLinksAsync(null);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000487C8 File Offset: 0x000469C8
		public Task<ODataEntityReferenceLinks> ReadEntityReferenceLinksAsync(IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanReadEntityReferenceLinks(navigationProperty);
			return this.ReadFromInputAsync<ODataEntityReferenceLinks>((ODataInputContext context) => context.ReadEntityReferenceLinksAsync(navigationProperty), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLinks });
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0004880C File Offset: 0x00046A0C
		public ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			return this.ReadEntityReferenceLink(null);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0004882C File Offset: 0x00046A2C
		public ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanReadEntityReferenceLink();
			return this.ReadFromInput<ODataEntityReferenceLink>((ODataInputContext context) => context.ReadEntityReferenceLink(navigationProperty), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLink });
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0004886A File Offset: 0x00046A6A
		public Task<ODataEntityReferenceLink> ReadEntityReferenceLinkAsync()
		{
			return this.ReadEntityReferenceLinkAsync(null);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0004888C File Offset: 0x00046A8C
		public Task<ODataEntityReferenceLink> ReadEntityReferenceLinkAsync(IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanReadEntityReferenceLink();
			return this.ReadFromInputAsync<ODataEntityReferenceLink>((ODataInputContext context) => context.ReadEntityReferenceLinkAsync(navigationProperty), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLink });
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000488E8 File Offset: 0x00046AE8
		public object ReadValue(IEdmTypeReference expectedTypeReference)
		{
			ODataPayloadKind[] array = this.VerifyCanReadValue(expectedTypeReference);
			return this.ReadFromInput<object>((ODataInputContext context) => context.ReadValue((IEdmPrimitiveTypeReference)expectedTypeReference), array);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00048940 File Offset: 0x00046B40
		public Task<object> ReadValueAsync(IEdmTypeReference expectedTypeReference)
		{
			ODataPayloadKind[] array = this.VerifyCanReadValue(expectedTypeReference);
			return this.ReadFromInputAsync<object>((ODataInputContext context) => context.ReadValueAsync((IEdmPrimitiveTypeReference)expectedTypeReference), array);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00048984 File Offset: 0x00046B84
		public IEdmModel ReadMetadataDocument()
		{
			this.VerifyCanReadMetadataDocument();
			return this.ReadFromInput<IEdmModel>((ODataInputContext context) => context.ReadMetadataDocument(), new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument });
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x000489C7 File Offset: 0x00046BC7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000489D6 File Offset: 0x00046BD6
		internal ODataFormat GetFormat()
		{
			if (this.format == null)
			{
				throw new ODataException(Strings.ODataMessageReader_GetFormatCalledBeforeReadingStarted);
			}
			return this.format;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000489F4 File Offset: 0x00046BF4
		private void ProcessContentType(params ODataPayloadKind[] payloadKinds)
		{
			string contentTypeHeader = this.GetContentTypeHeader();
			this.format = MediaTypeUtils.GetFormatFromContentType(contentTypeHeader, payloadKinds, this.MediaTypeResolver, out this.contentType, out this.encoding, out this.readerPayloadKind, out this.batchBoundary);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00048A34 File Offset: 0x00046C34
		private string GetContentTypeHeader()
		{
			string text = this.message.GetHeader("Content-Type");
			text = ((text == null) ? null : text.Trim());
			if (string.IsNullOrEmpty(text))
			{
				throw new ODataContentTypeException(Strings.ODataMessageReader_NoneOrEmptyContentTypeHeader);
			}
			return text;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00048A74 File Offset: 0x00046C74
		private void VerifyCanCreateODataFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.model.IsUserModel())
			{
				if (entitySet != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_EntitySetSpecifiedWithoutMetadata("entitySet"), "entitySet");
				}
				if (expectedBaseEntityType != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedBaseEntityType"), "expectedBaseEntityType");
				}
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00048AC4 File Offset: 0x00046CC4
		private void VerifyCanCreateODataEntryReader(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.model.IsUserModel())
			{
				if (entitySet != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_EntitySetSpecifiedWithoutMetadata("entitySet"), "entitySet");
				}
				if (entityType != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("entityType"), "entityType");
				}
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00048B14 File Offset: 0x00046D14
		private void VerifyCanCreateODataCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (expectedItemTypeReference != null)
			{
				if (!this.model.IsUserModel())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedItemTypeReference"), "expectedItemTypeReference");
				}
				if (!expectedItemTypeReference.IsODataPrimitiveTypeKind() && expectedItemTypeReference.TypeKind() != EdmTypeKind.Complex)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedCollectionTypeWrongKind(expectedItemTypeReference.TypeKind().ToString()), "expectedItemTypeReference");
				}
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00048B7D File Offset: 0x00046D7D
		private void VerifyCanCreateODataBatchReader()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00048B88 File Offset: 0x00046D88
		private void VerifyCanCreateODataParameterReader(IEdmFunctionImport functionImport)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ParameterPayloadInResponse);
			}
			ODataVersionChecker.CheckParameterPayload(this.version);
			if (functionImport != null && !this.model.IsUserModel())
			{
				throw new ArgumentException(Strings.ODataMessageReader_FunctionImportSpecifiedWithoutMetadata("functionImport"), "functionImport");
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00048BDE File Offset: 0x00046DDE
		private void VerifyCanReadServiceDocument()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ServiceDocumentInRequest);
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00048BF9 File Offset: 0x00046DF9
		private void VerifyCanReadMetadataDocument()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_MetadataDocumentInRequest);
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00048C14 File Offset: 0x00046E14
		private void VerifyCanReadProperty(IEdmStructuralProperty property)
		{
			if (property == null)
			{
				return;
			}
			this.VerifyCanReadProperty(property.Type);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00048C28 File Offset: 0x00046E28
		private void VerifyCanReadProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (expectedPropertyTypeReference != null)
			{
				if (!this.model.IsUserModel())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedPropertyTypeReference"), "expectedPropertyTypeReference");
				}
				IEdmCollectionType edmCollectionType = expectedPropertyTypeReference.Definition as IEdmCollectionType;
				if (edmCollectionType != null && edmCollectionType.ElementType.IsODataEntityTypeKind())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind, "expectedPropertyTypeReference");
				}
				if (expectedPropertyTypeReference.IsODataEntityTypeKind())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedPropertyTypeEntityKind, "expectedPropertyTypeReference");
				}
				if (expectedPropertyTypeReference.IsStream())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedPropertyTypeStream, "expectedPropertyTypeReference");
				}
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00048CBC File Offset: 0x00046EBC
		private void VerifyCanReadError()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ErrorPayloadInRequest);
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00048CD8 File Offset: 0x00046ED8
		private void VerifyCanReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_EntityReferenceLinksInRequestNotAllowed);
			}
			if (navigationProperty != null && !navigationProperty.Type.IsCollection())
			{
				throw new ODataException(Strings.ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks(navigationProperty.Name, navigationProperty.DeclaringEntityType().FullName()));
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00048D2A File Offset: 0x00046F2A
		private void VerifyCanReadEntityReferenceLink()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00048D34 File Offset: 0x00046F34
		private ODataPayloadKind[] VerifyCanReadValue(IEdmTypeReference expectedTypeReference)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (expectedTypeReference == null)
			{
				return new ODataPayloadKind[]
				{
					ODataPayloadKind.Value,
					ODataPayloadKind.BinaryValue
				};
			}
			if (!expectedTypeReference.IsODataPrimitiveTypeKind())
			{
				throw new ArgumentException(Strings.ODataMessageReader_ExpectedValueTypeWrongKind(expectedTypeReference.TypeKind().ToString()), "expectedTypeReference");
			}
			if (expectedTypeReference.IsBinary())
			{
				return new ODataPayloadKind[] { ODataPayloadKind.BinaryValue };
			}
			return new ODataPayloadKind[] { ODataPayloadKind.Value };
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00048DA4 File Offset: 0x00046FA4
		private void VerifyReaderNotDisposedAndNotUsed()
		{
			this.VerifyNotDisposed();
			if (this.readMethodCalled)
			{
				throw new ODataException(Strings.ODataMessageReader_ReaderAlreadyUsed);
			}
			if (this.message.BufferingReadStream != null && this.message.BufferingReadStream.IsBuffering)
			{
				throw new ODataException(Strings.ODataMessageReader_PayloadKindDetectionRunning);
			}
			this.readMethodCalled = true;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00048DFB File Offset: 0x00046FFB
		private void VerifyNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00048E18 File Offset: 0x00047018
		private void Dispose(bool disposing)
		{
			this.isDisposed = true;
			if (disposing)
			{
				try
				{
					if (this.inputContext != null)
					{
						this.inputContext.Dispose();
					}
				}
				finally
				{
					this.inputContext = null;
				}
				if (!this.settings.DisableMessageStreamDisposal && this.message.BufferingReadStream != null)
				{
					this.message.BufferingReadStream.Dispose();
				}
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00048E88 File Offset: 0x00047088
		private T ReadFromInput<T>(Func<ODataInputContext, T> readFunc, params ODataPayloadKind[] payloadKinds) where T : class
		{
			this.ProcessContentType(payloadKinds);
			object obj = null;
			if (this.payloadKindDetectionFormatStates != null)
			{
				this.payloadKindDetectionFormatStates.TryGetValue(this.format, out obj);
			}
			this.inputContext = this.format.CreateInputContext(this.readerPayloadKind, this.message, this.contentType, this.encoding, this.settings, this.version, this.readingResponse, this.model, this.urlResolver, obj);
			return readFunc(this.inputContext);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00048F24 File Offset: 0x00047124
		private bool TryGetSinglePayloadKindResultFromContentType(out IEnumerable<ODataPayloadKindDetectionResult> payloadKindResults)
		{
			if (this.message.UseBufferingReadStream == true)
			{
				throw new ODataException(Strings.ODataMessageReader_DetectPayloadKindMultipleTimes);
			}
			string contentTypeHeader = this.GetContentTypeHeader();
			IList<ODataPayloadKindDetectionResult> payloadKindsForContentType = MediaTypeUtils.GetPayloadKindsForContentType(contentTypeHeader, this.MediaTypeResolver, out this.contentType, out this.encoding);
			payloadKindResults = payloadKindsForContentType.Where((ODataPayloadKindDetectionResult r) => ODataUtilsInternal.IsPayloadKindSupported(r.PayloadKind, !this.readingResponse));
			if (payloadKindResults.Count<ODataPayloadKindDetectionResult>() > 1)
			{
				this.message.UseBufferingReadStream = new bool?(true);
				return false;
			}
			return true;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00048FB0 File Offset: 0x000471B0
		private int ComparePayloadKindDetectionResult(ODataPayloadKindDetectionResult first, ODataPayloadKindDetectionResult second)
		{
			ODataPayloadKind payloadKind = first.PayloadKind;
			ODataPayloadKind payloadKind2 = second.PayloadKind;
			if (payloadKind == payloadKind2)
			{
				return 0;
			}
			if (first.PayloadKind >= second.PayloadKind)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00049480 File Offset: 0x00047680
		private IEnumerable<Task> GetPayloadKindDetectionTasks(IEnumerable<ODataPayloadKindDetectionResult> payloadKindsFromContentType, List<ODataPayloadKindDetectionResult> detectionResults)
		{
			IEnumerable<IGrouping<ODataFormat, ODataPayloadKindDetectionResult>> payloadKindFromContentTypeGroups = from kvp in payloadKindsFromContentType
				group kvp by kvp.Format;
			using (IEnumerator<IGrouping<ODataFormat, ODataPayloadKindDetectionResult>> enumerator = payloadKindFromContentTypeGroups.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ODataMessageReader.<>c__DisplayClass57 CS$<>8__locals2 = new ODataMessageReader.<>c__DisplayClass57();
					CS$<>8__locals2.payloadKindGroup = enumerator.Current;
					ODataPayloadKindDetectionInfo detectionInfo = new ODataPayloadKindDetectionInfo(this.contentType, this.encoding, this.settings, this.model, CS$<>8__locals2.payloadKindGroup.Select((ODataPayloadKindDetectionResult pkg) => pkg.PayloadKind));
					Task<IEnumerable<ODataPayloadKind>> detectionResult = (this.readingResponse ? CS$<>8__locals2.payloadKindGroup.Key.DetectPayloadKindAsync((IODataResponseMessageAsync)this.message, detectionInfo) : CS$<>8__locals2.payloadKindGroup.Key.DetectPayloadKindAsync((IODataRequestMessageAsync)this.message, detectionInfo));
					yield return detectionResult.FollowOnSuccessWith(delegate(Task<IEnumerable<ODataPayloadKind>> t)
					{
						IEnumerable<ODataPayloadKind> result = t.Result;
						if (result != null)
						{
							using (IEnumerator<ODataPayloadKind> enumerator2 = result.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									ODataPayloadKind kind = enumerator2.Current;
									if (payloadKindsFromContentType.Any((ODataPayloadKindDetectionResult pk) => pk.PayloadKind == kind))
									{
										detectionResults.Add(new ODataPayloadKindDetectionResult(kind, CS$<>8__locals2.payloadKindGroup.Key));
									}
								}
							}
						}
						this.payloadKindDetectionFormatStates.Add(CS$<>8__locals2.payloadKindGroup.Key, detectionInfo.PayloadKindDetectionFormatState);
					});
				}
			}
			yield break;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000494DC File Offset: 0x000476DC
		private Task<T> ReadFromInputAsync<T>(Func<ODataInputContext, Task<T>> readFunc, params ODataPayloadKind[] payloadKinds) where T : class
		{
			this.ProcessContentType(payloadKinds);
			object obj = null;
			if (this.payloadKindDetectionFormatStates != null)
			{
				this.payloadKindDetectionFormatStates.TryGetValue(this.format, out obj);
			}
			return this.format.CreateInputContextAsync(this.readerPayloadKind, this.message, this.contentType, this.encoding, this.settings, this.version, this.readingResponse, this.model, this.urlResolver, obj).FollowOnSuccessWithTask(delegate(Task<ODataInputContext> createInputContextTask)
			{
				this.inputContext = createInputContextTask.Result;
				return readFunc(this.inputContext);
			});
		}

		// Token: 0x040006D8 RID: 1752
		private readonly ODataMessage message;

		// Token: 0x040006D9 RID: 1753
		private readonly bool readingResponse;

		// Token: 0x040006DA RID: 1754
		private readonly ODataMessageReaderSettings settings;

		// Token: 0x040006DB RID: 1755
		private readonly IEdmModel model;

		// Token: 0x040006DC RID: 1756
		private readonly ODataVersion version;

		// Token: 0x040006DD RID: 1757
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x040006DE RID: 1758
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x040006DF RID: 1759
		private bool readMethodCalled;

		// Token: 0x040006E0 RID: 1760
		private bool isDisposed;

		// Token: 0x040006E1 RID: 1761
		private ODataInputContext inputContext;

		// Token: 0x040006E2 RID: 1762
		private ODataPayloadKind readerPayloadKind;

		// Token: 0x040006E3 RID: 1763
		private ODataFormat format;

		// Token: 0x040006E4 RID: 1764
		private MediaType contentType;

		// Token: 0x040006E5 RID: 1765
		private Encoding encoding;

		// Token: 0x040006E6 RID: 1766
		private string batchBoundary;

		// Token: 0x040006E7 RID: 1767
		private MediaTypeResolver mediaTypeResolver;

		// Token: 0x040006E8 RID: 1768
		private Dictionary<ODataFormat, object> payloadKindDetectionFormatStates;
	}
}
