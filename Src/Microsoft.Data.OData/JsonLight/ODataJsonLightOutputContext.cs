using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000195 RID: 405
	internal sealed class ODataJsonLightOutputContext : ODataJsonOutputContextBase
	{
		// Token: 0x06000BF7 RID: 3063 RVA: 0x00029CD0 File Offset: 0x00027ED0
		internal ODataJsonLightOutputContext(ODataFormat format, TextWriter textWriter, ODataMessageWriterSettings messageWriterSettings, IEdmModel model)
			: base(format, textWriter, messageWriterSettings, model)
		{
			this.metadataLevel = new JsonMinimalMetadataLevel();
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00029CE8 File Offset: 0x00027EE8
		internal ODataJsonLightOutputContext(ODataFormat format, Stream messageStream, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageStream, encoding, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
			Uri uri = ((messageWriterSettings.MetadataDocumentUri == null) ? null : messageWriterSettings.MetadataDocumentUri.BaseUri);
			this.metadataLevel = JsonLightMetadataLevel.Create(mediaType, uri, model, writingResponse);
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00029D33 File Offset: 0x00027F33
		internal JsonLightTypeNameOracle TypeNameOracle
		{
			get
			{
				if (this.typeNameOracle == null)
				{
					this.typeNameOracle = this.MetadataLevel.GetTypeNameOracle(base.MessageWriterSettings.AutoComputePayloadMetadataInJson);
				}
				return this.typeNameOracle;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00029D5F File Offset: 0x00027F5F
		internal JsonLightMetadataLevel MetadataLevel
		{
			get
			{
				return this.metadataLevel;
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00029D67 File Offset: 0x00027F67
		internal ODataJsonLightMetadataUriBuilder CreateMetadataUriBuilder()
		{
			return ODataJsonLightMetadataUriBuilder.CreateFromSettings(this.MetadataLevel, base.WritingResponse, base.MessageWriterSettings, base.Model);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00029D86 File Offset: 0x00027F86
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			this.WriteInStreamErrorImplementation(error, includeDebugInformation);
			base.Flush();
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00029DC4 File Offset: 0x00027FC4
		internal override Task WriteInStreamErrorAsync(ODataError error, bool includeDebugInformation)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteInStreamErrorImplementation(error, includeDebugInformation);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00029DFD File Offset: 0x00027FFD
		internal override ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataFeedWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00029E28 File Offset: 0x00028028
		internal override Task<ODataWriter> CreateODataFeedWriterAsync(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(() => this.CreateODataFeedWriterImplementation(entitySet, entityType));
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00029E61 File Offset: 0x00028061
		internal override ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataEntryWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00029E8C File Offset: 0x0002808C
		internal override Task<ODataWriter> CreateODataEntryWriterAsync(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(() => this.CreateODataEntryWriterImplementation(entitySet, entityType));
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00029EC5 File Offset: 0x000280C5
		internal override ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			return this.CreateODataCollectionWriterImplementation(itemTypeReference);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00029EEC File Offset: 0x000280EC
		internal override Task<ODataCollectionWriter> CreateODataCollectionWriterAsync(IEdmTypeReference itemTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionWriter>(() => this.CreateODataCollectionWriterImplementation(itemTypeReference));
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00029F1E File Offset: 0x0002811E
		internal override ODataParameterWriter CreateODataParameterWriter(IEdmFunctionImport functionImport)
		{
			return this.CreateODataParameterWriterImplementation(functionImport);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00029F44 File Offset: 0x00028144
		internal override Task<ODataParameterWriter> CreateODataParameterWriterAsync(IEdmFunctionImport functionImport)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataParameterWriter>(() => this.CreateODataParameterWriterImplementation(functionImport));
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00029F76 File Offset: 0x00028176
		internal override void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			this.WriteServiceDocumentImplementation(defaultWorkspace);
			base.Flush();
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00029FAC File Offset: 0x000281AC
		internal override Task WriteServiceDocumentAsync(ODataWorkspace defaultWorkspace)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteServiceDocumentImplementation(defaultWorkspace);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00029FDE File Offset: 0x000281DE
		internal override void WriteProperty(ODataProperty property)
		{
			this.WritePropertyImplementation(property);
			base.Flush();
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002A014 File Offset: 0x00028214
		internal override Task WritePropertyAsync(ODataProperty property)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WritePropertyImplementation(property);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002A046 File Offset: 0x00028246
		internal override void WriteError(ODataError error, bool includeDebugInformation)
		{
			this.WriteErrorImplementation(error, includeDebugInformation);
			base.Flush();
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002A084 File Offset: 0x00028284
		internal override Task WriteErrorAsync(ODataError error, bool includeDebugInformation)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteErrorImplementation(error, includeDebugInformation);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002A0BD File Offset: 0x000282BD
		internal override void WriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinksImplementation(links, entitySet, navigationProperty);
			base.Flush();
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002A100 File Offset: 0x00028300
		internal override Task WriteEntityReferenceLinksAsync(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteEntityReferenceLinksImplementation(links, entitySet, navigationProperty);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002A140 File Offset: 0x00028340
		internal override void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinkImplementation(link, entitySet, navigationProperty);
			base.Flush();
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002A184 File Offset: 0x00028384
		internal override Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteEntityReferenceLinkImplementation(link, entitySet, navigationProperty);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002A1C4 File Offset: 0x000283C4
		private ODataWriter CreateODataFeedWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataJsonLightWriter odataJsonLightWriter = new ODataJsonLightWriter(this, entitySet, entityType, true);
			this.outputInStreamErrorListener = odataJsonLightWriter;
			return odataJsonLightWriter;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002A1E4 File Offset: 0x000283E4
		private ODataWriter CreateODataEntryWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataJsonLightWriter odataJsonLightWriter = new ODataJsonLightWriter(this, entitySet, entityType, false);
			this.outputInStreamErrorListener = odataJsonLightWriter;
			return odataJsonLightWriter;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002A204 File Offset: 0x00028404
		private ODataCollectionWriter CreateODataCollectionWriterImplementation(IEdmTypeReference itemTypeReference)
		{
			ODataJsonLightCollectionWriter odataJsonLightCollectionWriter = new ODataJsonLightCollectionWriter(this, itemTypeReference);
			this.outputInStreamErrorListener = odataJsonLightCollectionWriter;
			return odataJsonLightCollectionWriter;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002A224 File Offset: 0x00028424
		private ODataParameterWriter CreateODataParameterWriterImplementation(IEdmFunctionImport functionImport)
		{
			ODataJsonLightParameterWriter odataJsonLightParameterWriter = new ODataJsonLightParameterWriter(this, functionImport);
			this.outputInStreamErrorListener = odataJsonLightParameterWriter;
			return odataJsonLightParameterWriter;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002A244 File Offset: 0x00028444
		private void WriteInStreamErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			JsonLightInstanceAnnotationWriter jsonLightInstanceAnnotationWriter = new JsonLightInstanceAnnotationWriter(new ODataJsonLightValueSerializer(this), this.TypeNameOracle);
			ODataJsonWriterUtils.WriteError(base.JsonWriter, new Action<IEnumerable<ODataInstanceAnnotation>>(jsonLightInstanceAnnotationWriter.WriteInstanceAnnotations), error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth, true);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002A2A0 File Offset: 0x000284A0
		private void WritePropertyImplementation(ODataProperty property)
		{
			ODataJsonLightPropertySerializer odataJsonLightPropertySerializer = new ODataJsonLightPropertySerializer(this);
			odataJsonLightPropertySerializer.WriteTopLevelProperty(property);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002A2BC File Offset: 0x000284BC
		private void WriteServiceDocumentImplementation(ODataWorkspace defaultWorkspace)
		{
			ODataJsonLightServiceDocumentSerializer odataJsonLightServiceDocumentSerializer = new ODataJsonLightServiceDocumentSerializer(this);
			odataJsonLightServiceDocumentSerializer.WriteServiceDocument(defaultWorkspace);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002A2D8 File Offset: 0x000284D8
		private void WriteErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			ODataJsonLightSerializer odataJsonLightSerializer = new ODataJsonLightSerializer(this);
			odataJsonLightSerializer.WriteTopLevelError(error, includeDebugInformation);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002A2F4 File Offset: 0x000284F4
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			ODataJsonLightEntityReferenceLinkSerializer odataJsonLightEntityReferenceLinkSerializer = new ODataJsonLightEntityReferenceLinkSerializer(this);
			odataJsonLightEntityReferenceLinkSerializer.WriteEntityReferenceLinks(links, entitySet, navigationProperty);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002A314 File Offset: 0x00028514
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			ODataJsonLightEntityReferenceLinkSerializer odataJsonLightEntityReferenceLinkSerializer = new ODataJsonLightEntityReferenceLinkSerializer(this);
			odataJsonLightEntityReferenceLinkSerializer.WriteEntityReferenceLink(link, entitySet, navigationProperty);
		}

		// Token: 0x0400042D RID: 1069
		private readonly JsonLightMetadataLevel metadataLevel;

		// Token: 0x0400042E RID: 1070
		private JsonLightTypeNameOracle typeNameOracle;
	}
}
