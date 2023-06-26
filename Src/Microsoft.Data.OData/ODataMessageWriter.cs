using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Csdl;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x0200028D RID: 653
	public sealed class ODataMessageWriter : IDisposable
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x0004FF5C File Offset: 0x0004E15C
		public ODataMessageWriter(IODataRequestMessage requestMessage)
			: this(requestMessage, null)
		{
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0004FF66 File Offset: 0x0004E166
		public ODataMessageWriter(IODataRequestMessage requestMessage, ODataMessageWriterSettings settings)
			: this(requestMessage, settings, null)
		{
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0004FF74 File Offset: 0x0004E174
		public ODataMessageWriter(IODataRequestMessage requestMessage, ODataMessageWriterSettings settings, IEdmModel model)
		{
			this.writerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			this.settings = ((settings == null) ? new ODataMessageWriterSettings() : new ODataMessageWriterSettings(settings));
			this.writingResponse = false;
			this.urlResolver = requestMessage as IODataUrlResolver;
			this.model = model ?? EdmCoreModel.Instance;
			WriterValidationUtils.ValidateMessageWriterSettings(this.settings, this.writingResponse);
			this.message = new ODataRequestMessage(requestMessage, true, this.settings.DisableMessageStreamDisposal, -1L);
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00050001 File Offset: 0x0004E201
		public ODataMessageWriter(IODataResponseMessage responseMessage)
			: this(responseMessage, null)
		{
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0005000B File Offset: 0x0004E20B
		public ODataMessageWriter(IODataResponseMessage responseMessage, ODataMessageWriterSettings settings)
			: this(responseMessage, settings, null)
		{
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00050018 File Offset: 0x0004E218
		public ODataMessageWriter(IODataResponseMessage responseMessage, ODataMessageWriterSettings settings, IEdmModel model)
		{
			this.writerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			this.settings = ((settings == null) ? new ODataMessageWriterSettings() : new ODataMessageWriterSettings(settings));
			this.writingResponse = true;
			this.urlResolver = responseMessage as IODataUrlResolver;
			this.model = model ?? EdmCoreModel.Instance;
			WriterValidationUtils.ValidateMessageWriterSettings(this.settings, this.writingResponse);
			this.message = new ODataResponseMessage(responseMessage, true, this.settings.DisableMessageStreamDisposal, -1L);
			string annotationFilter = responseMessage.PreferenceAppliedHeader().AnnotationFilter;
			if (!string.IsNullOrEmpty(annotationFilter))
			{
				this.settings.ShouldIncludeAnnotation = ODataUtils.CreateAnnotationFilter(annotationFilter);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x000500CA File Offset: 0x0004E2CA
		internal ODataMessageWriterSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x000500D4 File Offset: 0x0004E2D4
		private MediaTypeResolver MediaTypeResolver
		{
			get
			{
				if (this.mediaTypeResolver == null)
				{
					this.mediaTypeResolver = MediaTypeResolver.GetWriterMediaTypeResolver(this.settings.Version.Value);
				}
				return this.mediaTypeResolver;
			}
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0005010D File Offset: 0x0004E30D
		public ODataWriter CreateODataFeedWriter()
		{
			return this.CreateODataFeedWriter(null, null);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00050117 File Offset: 0x0004E317
		public ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet)
		{
			return this.CreateODataFeedWriter(entitySet, null);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00050140 File Offset: 0x0004E340
		public ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataFeedWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.Feed, null, (ODataOutputContext context) => context.CreateODataFeedWriter(entitySet, entityType));
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0005017B File Offset: 0x0004E37B
		public Task<ODataWriter> CreateODataFeedWriterAsync()
		{
			return this.CreateODataFeedWriterAsync(null, null);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00050185 File Offset: 0x0004E385
		public Task<ODataWriter> CreateODataFeedWriterAsync(IEdmEntitySet entitySet)
		{
			return this.CreateODataFeedWriterAsync(entitySet, null);
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x000501AC File Offset: 0x0004E3AC
		public Task<ODataWriter> CreateODataFeedWriterAsync(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataFeedWriter();
			return this.WriteToOutputAsync<ODataWriter>(ODataPayloadKind.Feed, null, (ODataOutputContext context) => context.CreateODataFeedWriterAsync(entitySet, entityType));
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x000501E7 File Offset: 0x0004E3E7
		public ODataWriter CreateODataEntryWriter()
		{
			return this.CreateODataEntryWriter(null, null);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000501F1 File Offset: 0x0004E3F1
		public ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet)
		{
			return this.CreateODataEntryWriter(entitySet, null);
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00050218 File Offset: 0x0004E418
		public ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataEntryWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.Entry, null, (ODataOutputContext context) => context.CreateODataEntryWriter(entitySet, entityType));
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00050253 File Offset: 0x0004E453
		public Task<ODataWriter> CreateODataEntryWriterAsync()
		{
			return this.CreateODataEntryWriterAsync(null, null);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0005025D File Offset: 0x0004E45D
		public Task<ODataWriter> CreateODataEntryWriterAsync(IEdmEntitySet entitySet)
		{
			return this.CreateODataEntryWriterAsync(entitySet, null);
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00050284 File Offset: 0x0004E484
		public Task<ODataWriter> CreateODataEntryWriterAsync(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataEntryWriter();
			return this.WriteToOutputAsync<ODataWriter>(ODataPayloadKind.Entry, null, (ODataOutputContext context) => context.CreateODataEntryWriterAsync(entitySet, entityType));
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000502BF File Offset: 0x0004E4BF
		public ODataCollectionWriter CreateODataCollectionWriter()
		{
			return this.CreateODataCollectionWriter(null);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000502E0 File Offset: 0x0004E4E0
		public ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			this.VerifyCanCreateODataCollectionWriter(itemTypeReference);
			return this.WriteToOutput<ODataCollectionWriter>(ODataPayloadKind.Collection, null, (ODataOutputContext context) => context.CreateODataCollectionWriter(itemTypeReference));
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0005031A File Offset: 0x0004E51A
		public Task<ODataCollectionWriter> CreateODataCollectionWriterAsync()
		{
			return this.CreateODataCollectionWriterAsync(null);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0005033C File Offset: 0x0004E53C
		public Task<ODataCollectionWriter> CreateODataCollectionWriterAsync(IEdmTypeReference itemTypeReference)
		{
			this.VerifyCanCreateODataCollectionWriter(itemTypeReference);
			return this.WriteToOutputAsync<ODataCollectionWriter>(ODataPayloadKind.Collection, null, (ODataOutputContext context) => context.CreateODataCollectionWriterAsync(itemTypeReference));
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00050384 File Offset: 0x0004E584
		public ODataBatchWriter CreateODataBatchWriter()
		{
			this.VerifyCanCreateODataBatchWriter();
			return this.WriteToOutput<ODataBatchWriter>(ODataPayloadKind.Batch, null, (ODataOutputContext context) => context.CreateODataBatchWriter(this.batchBoundary));
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000503AF File Offset: 0x0004E5AF
		public Task<ODataBatchWriter> CreateODataBatchWriterAsync()
		{
			this.VerifyCanCreateODataBatchWriter();
			return this.WriteToOutputAsync<ODataBatchWriter>(ODataPayloadKind.Batch, null, (ODataOutputContext context) => context.CreateODataBatchWriterAsync(this.batchBoundary));
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000503E4 File Offset: 0x0004E5E4
		public ODataParameterWriter CreateODataParameterWriter(IEdmFunctionImport functionImport)
		{
			this.VerifyCanCreateODataParameterWriter(functionImport);
			return this.WriteToOutput<ODataParameterWriter>(ODataPayloadKind.Parameter, new Action(this.VerifyODataParameterWriterHeaders), (ODataOutputContext context) => context.CreateODataParameterWriter(functionImport));
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00050440 File Offset: 0x0004E640
		public Task<ODataParameterWriter> CreateODataParameterWriterAsync(IEdmFunctionImport functionImport)
		{
			this.VerifyCanCreateODataParameterWriter(functionImport);
			return this.WriteToOutputAsync<ODataParameterWriter>(ODataPayloadKind.Parameter, new Action(this.VerifyODataParameterWriterHeaders), (ODataOutputContext context) => context.CreateODataParameterWriterAsync(functionImport));
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0005049C File Offset: 0x0004E69C
		public void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			this.VerifyCanWriteServiceDocument(defaultWorkspace);
			this.WriteToOutput(ODataPayloadKind.ServiceDocument, null, delegate(ODataOutputContext context)
			{
				context.WriteServiceDocument(defaultWorkspace);
			});
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000504EC File Offset: 0x0004E6EC
		public Task WriteServiceDocumentAsync(ODataWorkspace defaultWorkspace)
		{
			this.VerifyCanWriteServiceDocument(defaultWorkspace);
			return this.WriteToOutputAsync(ODataPayloadKind.ServiceDocument, null, (ODataOutputContext context) => context.WriteServiceDocumentAsync(defaultWorkspace));
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0005053C File Offset: 0x0004E73C
		public void WriteProperty(ODataProperty property)
		{
			this.VerifyCanWriteProperty(property);
			this.WriteToOutput(ODataPayloadKind.Property, null, delegate(ODataOutputContext context)
			{
				context.WriteProperty(property);
			});
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0005058C File Offset: 0x0004E78C
		public Task WritePropertyAsync(ODataProperty property)
		{
			this.VerifyCanWriteProperty(property);
			return this.WriteToOutputAsync(ODataPayloadKind.Property, null, (ODataOutputContext context) => context.WritePropertyAsync(property));
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000505E4 File Offset: 0x0004E7E4
		public void WriteError(ODataError error, bool includeDebugInformation)
		{
			if (this.outputContext == null)
			{
				this.VerifyCanWriteTopLevelError(error);
				this.WriteToOutput(ODataPayloadKind.Error, null, delegate(ODataOutputContext context)
				{
					context.WriteError(error, includeDebugInformation);
				});
				return;
			}
			this.VerifyCanWriteInStreamError(error);
			this.outputContext.WriteInStreamError(error, includeDebugInformation);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00050678 File Offset: 0x0004E878
		public Task WriteErrorAsync(ODataError error, bool includeDebugInformation)
		{
			if (this.outputContext == null)
			{
				this.VerifyCanWriteTopLevelError(error);
				return this.WriteToOutputAsync(ODataPayloadKind.Error, null, (ODataOutputContext context) => context.WriteErrorAsync(error, includeDebugInformation));
			}
			this.VerifyCanWriteInStreamError(error);
			return this.outputContext.WriteInStreamErrorAsync(error, includeDebugInformation);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000506ED File Offset: 0x0004E8ED
		public void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			this.WriteEntityReferenceLinks(links, null, null);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00050730 File Offset: 0x0004E930
		public void WriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanWriteEntityReferenceLinks(links, navigationProperty);
			this.WriteToOutput(ODataPayloadKind.EntityReferenceLinks, delegate
			{
				this.VerifyEntityReferenceLinksHeaders(links);
			}, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLinks(links, entitySet, navigationProperty);
			});
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00050790 File Offset: 0x0004E990
		public Task WriteEntityReferenceLinksAsync(ODataEntityReferenceLinks links)
		{
			return this.WriteEntityReferenceLinksAsync(links, null, null);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000507D0 File Offset: 0x0004E9D0
		public Task WriteEntityReferenceLinksAsync(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanWriteEntityReferenceLinks(links, navigationProperty);
			return this.WriteToOutputAsync(ODataPayloadKind.EntityReferenceLinks, delegate
			{
				this.VerifyEntityReferenceLinksHeaders(links);
			}, (ODataOutputContext context) => context.WriteEntityReferenceLinksAsync(links, entitySet, navigationProperty));
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00050830 File Offset: 0x0004EA30
		public void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			this.WriteEntityReferenceLink(link, null, null);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00050860 File Offset: 0x0004EA60
		public void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanWriteEntityReferenceLink(link);
			this.WriteToOutput(ODataPayloadKind.EntityReferenceLink, null, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLink(link, entitySet, navigationProperty);
			});
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000508A8 File Offset: 0x0004EAA8
		public Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink link)
		{
			return this.WriteEntityReferenceLinkAsync(link, null, null);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000508D8 File Offset: 0x0004EAD8
		public Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanWriteEntityReferenceLink(link);
			return this.WriteToOutputAsync(ODataPayloadKind.EntityReferenceLink, null, (ODataOutputContext context) => context.WriteEntityReferenceLinkAsync(link, entitySet, navigationProperty));
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00050938 File Offset: 0x0004EB38
		public void WriteValue(object value)
		{
			ODataPayloadKind odataPayloadKind = this.VerifyCanWriteValue(value);
			this.WriteToOutput(odataPayloadKind, null, delegate(ODataOutputContext context)
			{
				context.WriteValue(value);
			});
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0005098C File Offset: 0x0004EB8C
		public Task WriteValueAsync(object value)
		{
			ODataPayloadKind odataPayloadKind = this.VerifyCanWriteValue(value);
			return this.WriteToOutputAsync(odataPayloadKind, null, (ODataOutputContext context) => context.WriteValueAsync(value));
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x000509CF File Offset: 0x0004EBCF
		public void WriteMetadataDocument()
		{
			this.VerifyCanWriteMetadataDocument();
			this.WriteToOutput(ODataPayloadKind.MetadataDocument, new Action(this.VerifyMetadataDocumentHeaders), delegate(ODataOutputContext context)
			{
				context.WriteMetadataDocument();
			});
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00050A08 File Offset: 0x0004EC08
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00050A17 File Offset: 0x0004EC17
		internal ODataFormat SetHeaders(ODataPayloadKind payloadKind)
		{
			this.writerPayloadKind = payloadKind;
			this.EnsureODataVersion();
			this.EnsureODataFormatAndContentType();
			return this.format;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00050A32 File Offset: 0x0004EC32
		private void SetOrVerifyHeaders(ODataPayloadKind payloadKind)
		{
			this.VerifyPayloadKind(payloadKind);
			if (this.writerPayloadKind == ODataPayloadKind.Unsupported)
			{
				this.SetHeaders(payloadKind);
			}
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00050A50 File Offset: 0x0004EC50
		private void EnsureODataVersion()
		{
			if (this.settings.Version == null)
			{
				this.settings.Version = new ODataVersion?(ODataUtilsInternal.GetDataServiceVersion(this.message, ODataVersion.V3));
			}
			else
			{
				ODataUtilsInternal.SetDataServiceVersion(this.message, this.settings);
			}
			if (this.settings.Version >= ODataVersion.V3 && this.settings.WriterBehavior.FormatBehaviorKind != ODataBehaviorKind.Default)
			{
				this.settings.WriterBehavior.UseDefaultFormatBehavior();
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00050AE8 File Offset: 0x0004ECE8
		private void EnsureODataFormatAndContentType()
		{
			string text = null;
			if (this.settings.UseFormat == null)
			{
				text = this.message.GetHeader("Content-Type");
				text = ((text == null) ? null : text.Trim());
			}
			if (!string.IsNullOrEmpty(text))
			{
				ODataPayloadKind odataPayloadKind;
				this.format = MediaTypeUtils.GetFormatFromContentType(text, new ODataPayloadKind[] { this.writerPayloadKind }, this.MediaTypeResolver, out this.mediaType, out this.encoding, out odataPayloadKind, out this.batchBoundary);
				if (this.settings.HasJsonPaddingFunction())
				{
					text = MediaTypeUtils.AlterContentTypeForJsonPadding(text);
					this.message.SetHeader("Content-Type", text);
					return;
				}
			}
			else
			{
				this.format = MediaTypeUtils.GetContentTypeFromSettings(this.settings, this.writerPayloadKind, this.MediaTypeResolver, out this.mediaType, out this.encoding);
				if (this.writerPayloadKind == ODataPayloadKind.Batch)
				{
					this.batchBoundary = ODataBatchWriterUtils.CreateBatchBoundary(this.writingResponse);
					text = ODataBatchWriterUtils.CreateMultipartMixedContentType(this.batchBoundary);
				}
				else
				{
					this.batchBoundary = null;
					text = HttpUtils.BuildContentType(this.mediaType, this.encoding);
				}
				if (this.settings.HasJsonPaddingFunction())
				{
					text = MediaTypeUtils.AlterContentTypeForJsonPadding(text);
				}
				this.message.SetHeader("Content-Type", text);
			}
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00050C22 File Offset: 0x0004EE22
		private void VerifyCanCreateODataFeedWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00050C2A File Offset: 0x0004EE2A
		private void VerifyCanCreateODataEntryWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00050C32 File Offset: 0x0004EE32
		private void VerifyCanCreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			if (itemTypeReference != null && !itemTypeReference.IsPrimitive() && !itemTypeReference.IsComplex())
			{
				throw new ODataException(Strings.ODataMessageWriter_NonCollectionType(itemTypeReference.ODataFullName()));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00050C5E File Offset: 0x0004EE5E
		private void VerifyCanCreateODataBatchWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00050C66 File Offset: 0x0004EE66
		private void VerifyCanCreateODataParameterWriter(IEdmFunctionImport functionImport)
		{
			if (this.writingResponse)
			{
				throw new ODataException(Strings.ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage);
			}
			if (functionImport != null && !this.model.IsUserModel())
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotSpecifyFunctionImportWithoutModel);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00050C9C File Offset: 0x0004EE9C
		private void VerifyODataParameterWriterHeaders()
		{
			ODataVersionChecker.CheckParameterPayload(this.settings.Version.Value);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00050CC1 File Offset: 0x0004EEC1
		private void VerifyCanWriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataWorkspace>(defaultWorkspace, "defaultWorkspace");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_ServiceDocumentInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00050CE7 File Offset: 0x0004EEE7
		private void VerifyCanWriteProperty(ODataProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataProperty>(property, "property");
			if (property.Value is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty(property.Name));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00050D18 File Offset: 0x0004EF18
		private void VerifyCanWriteTopLevelError(ODataError error)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataError>(error, "error");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_ErrorPayloadInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
			this.writeErrorCalled = true;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00050D48 File Offset: 0x0004EF48
		private void VerifyCanWriteInStreamError(ODataError error)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataError>(error, "error");
			this.VerifyNotDisposed();
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_ErrorPayloadInRequest);
			}
			if (this.writeErrorCalled)
			{
				throw new ODataException(Strings.ODataMessageWriter_WriteErrorAlreadyCalled);
			}
			this.writeErrorCalled = true;
			this.writeMethodCalled = true;
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00050D9C File Offset: 0x0004EF9C
		private void VerifyCanWriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmNavigationProperty navigationProperty)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLinks>(links, "links");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed);
			}
			if (navigationProperty != null && navigationProperty.Type != null && !navigationProperty.Type.IsCollection())
			{
				throw new ODataException(Strings.ODataMessageWriter_EntityReferenceLinksWithSingletonNavPropNotAllowed(navigationProperty.Name));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00050DF8 File Offset: 0x0004EFF8
		private void VerifyEntityReferenceLinksHeaders(ODataEntityReferenceLinks links)
		{
			if (links.Count != null)
			{
				ODataVersionChecker.CheckCount(this.settings.Version.Value);
			}
			if (links.NextPageLink != null)
			{
				ODataVersionChecker.CheckNextLink(this.settings.Version.Value);
			}
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00050E53 File Offset: 0x0004F053
		private void VerifyCanWriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(link, "link");
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00050E66 File Offset: 0x0004F066
		private ODataPayloadKind VerifyCanWriteValue(object value)
		{
			if (value == null)
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotWriteNullInRawFormat);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
			if (!(value is byte[]))
			{
				return ODataPayloadKind.Value;
			}
			return ODataPayloadKind.BinaryValue;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00050E87 File Offset: 0x0004F087
		private void VerifyCanWriteMetadataDocument()
		{
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_MetadataDocumentInRequest);
			}
			if (!this.model.IsUserModel())
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotWriteMetadataWithoutModel);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00050EBC File Offset: 0x0004F0BC
		private void VerifyMetadataDocumentHeaders()
		{
			Version version = this.model.GetDataServiceVersion();
			if (version == null)
			{
				version = this.settings.Version.Value.ToDataServiceVersion();
				this.model.SetDataServiceVersion(version);
			}
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00050F03 File Offset: 0x0004F103
		private void VerifyWriterNotDisposedAndNotUsed()
		{
			this.VerifyNotDisposed();
			if (this.writeMethodCalled)
			{
				throw new ODataException(Strings.ODataMessageWriter_WriterAlreadyUsed);
			}
			this.writeMethodCalled = true;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00050F25 File Offset: 0x0004F125
		private void VerifyNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00050F40 File Offset: 0x0004F140
		private void Dispose(bool disposing)
		{
			this.isDisposed = true;
			if (disposing)
			{
				try
				{
					if (this.outputContext != null)
					{
						this.outputContext.Dispose();
					}
				}
				finally
				{
					this.outputContext = null;
				}
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00050F84 File Offset: 0x0004F184
		private void VerifyPayloadKind(ODataPayloadKind payloadKindToWrite)
		{
			if (this.writerPayloadKind != ODataPayloadKind.Unsupported && this.writerPayloadKind != payloadKindToWrite)
			{
				throw new ODataException(Strings.ODataMessageWriter_IncompatiblePayloadKinds(this.writerPayloadKind, payloadKindToWrite));
			}
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00050FB8 File Offset: 0x0004F1B8
		private void WriteToOutput(ODataPayloadKind payloadKind, Action verifyHeaders, Action<ODataOutputContext> writeAction)
		{
			this.SetOrVerifyHeaders(payloadKind);
			if (verifyHeaders != null)
			{
				verifyHeaders();
			}
			this.outputContext = this.format.CreateOutputContext(this.message, this.mediaType, this.encoding, this.settings, this.writingResponse, this.model, this.urlResolver);
			writeAction(this.outputContext);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0005101C File Offset: 0x0004F21C
		private TResult WriteToOutput<TResult>(ODataPayloadKind payloadKind, Action verifyHeaders, Func<ODataOutputContext, TResult> writeFunc)
		{
			this.SetOrVerifyHeaders(payloadKind);
			if (verifyHeaders != null)
			{
				verifyHeaders();
			}
			this.outputContext = this.format.CreateOutputContext(this.message, this.mediaType, this.encoding, this.settings, this.writingResponse, this.model, this.urlResolver);
			return writeFunc(this.outputContext);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x000510B4 File Offset: 0x0004F2B4
		private Task WriteToOutputAsync(ODataPayloadKind payloadKind, Action verifyHeaders, Func<ODataOutputContext, Task> writeAsyncAction)
		{
			this.SetOrVerifyHeaders(payloadKind);
			if (verifyHeaders != null)
			{
				verifyHeaders();
			}
			return this.format.CreateOutputContextAsync(this.message, this.mediaType, this.encoding, this.settings, this.writingResponse, this.model, this.urlResolver).FollowOnSuccessWithTask(delegate(Task<ODataOutputContext> createOutputContextTask)
			{
				this.outputContext = createOutputContextTask.Result;
				return writeAsyncAction(this.outputContext);
			});
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0005115C File Offset: 0x0004F35C
		private Task<TResult> WriteToOutputAsync<TResult>(ODataPayloadKind payloadKind, Action verifyHeaders, Func<ODataOutputContext, Task<TResult>> writeFunc)
		{
			this.SetOrVerifyHeaders(payloadKind);
			if (verifyHeaders != null)
			{
				verifyHeaders();
			}
			return this.format.CreateOutputContextAsync(this.message, this.mediaType, this.encoding, this.settings, this.writingResponse, this.model, this.urlResolver).FollowOnSuccessWithTask(delegate(Task<ODataOutputContext> createOutputContextTask)
			{
				this.outputContext = createOutputContextTask.Result;
				return writeFunc(this.outputContext);
			});
		}

		// Token: 0x04000857 RID: 2135
		private readonly ODataMessage message;

		// Token: 0x04000858 RID: 2136
		private readonly bool writingResponse;

		// Token: 0x04000859 RID: 2137
		private readonly ODataMessageWriterSettings settings;

		// Token: 0x0400085A RID: 2138
		private readonly IEdmModel model;

		// Token: 0x0400085B RID: 2139
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x0400085C RID: 2140
		private bool writeMethodCalled;

		// Token: 0x0400085D RID: 2141
		private bool isDisposed;

		// Token: 0x0400085E RID: 2142
		private ODataOutputContext outputContext;

		// Token: 0x0400085F RID: 2143
		private ODataPayloadKind writerPayloadKind;

		// Token: 0x04000860 RID: 2144
		private ODataFormat format;

		// Token: 0x04000861 RID: 2145
		private Encoding encoding;

		// Token: 0x04000862 RID: 2146
		private string batchBoundary;

		// Token: 0x04000863 RID: 2147
		private bool writeErrorCalled;

		// Token: 0x04000864 RID: 2148
		private MediaTypeResolver mediaTypeResolver;

		// Token: 0x04000865 RID: 2149
		private MediaType mediaType;
	}
}
