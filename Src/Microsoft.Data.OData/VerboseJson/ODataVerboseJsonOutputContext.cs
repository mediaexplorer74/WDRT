using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C8 RID: 456
	internal sealed class ODataVerboseJsonOutputContext : ODataJsonOutputContextBase
	{
		// Token: 0x06000E16 RID: 3606 RVA: 0x00031ED9 File Offset: 0x000300D9
		internal ODataVerboseJsonOutputContext(ODataFormat format, TextWriter textWriter, ODataMessageWriterSettings messageWriterSettings, IEdmModel model)
			: base(format, textWriter, messageWriterSettings, model)
		{
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00031EF4 File Offset: 0x000300F4
		internal ODataVerboseJsonOutputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageStream, encoding, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00031F1F File Offset: 0x0003011F
		internal AtomAndVerboseJsonTypeNameOracle TypeNameOracle
		{
			get
			{
				return this.typeNameOracle;
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00031F27 File Offset: 0x00030127
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			this.WriteInStreamErrorImplementation(error, includeDebugInformation);
			base.Flush();
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00031F64 File Offset: 0x00030164
		internal override Task WriteInStreamErrorAsync(ODataError error, bool includeDebugInformation)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteInStreamErrorImplementation(error, includeDebugInformation);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00031F9D File Offset: 0x0003019D
		internal override ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataFeedWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00031FC8 File Offset: 0x000301C8
		internal override Task<ODataWriter> CreateODataFeedWriterAsync(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(() => this.CreateODataFeedWriterImplementation(entitySet, entityType));
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00032001 File Offset: 0x00030201
		internal override ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataEntryWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003202C File Offset: 0x0003022C
		internal override Task<ODataWriter> CreateODataEntryWriterAsync(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(() => this.CreateODataEntryWriterImplementation(entitySet, entityType));
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00032065 File Offset: 0x00030265
		internal override ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			return this.CreateODataCollectionWriterImplementation(itemTypeReference);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003208C File Offset: 0x0003028C
		internal override Task<ODataCollectionWriter> CreateODataCollectionWriterAsync(IEdmTypeReference itemTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionWriter>(() => this.CreateODataCollectionWriterImplementation(itemTypeReference));
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000320BE File Offset: 0x000302BE
		internal override ODataParameterWriter CreateODataParameterWriter(IEdmFunctionImport functionImport)
		{
			return this.CreateODataParameterWriterImplementation(functionImport);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000320E4 File Offset: 0x000302E4
		internal override Task<ODataParameterWriter> CreateODataParameterWriterAsync(IEdmFunctionImport functionImport)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataParameterWriter>(() => this.CreateODataParameterWriterImplementation(functionImport));
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00032116 File Offset: 0x00030316
		internal override void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			this.WriteServiceDocumentImplementation(defaultWorkspace);
			base.Flush();
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003214C File Offset: 0x0003034C
		internal override Task WriteServiceDocumentAsync(ODataWorkspace defaultWorkspace)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteServiceDocumentImplementation(defaultWorkspace);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003217E File Offset: 0x0003037E
		internal override void WriteProperty(ODataProperty property)
		{
			this.WritePropertyImplementation(property);
			base.Flush();
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000321B4 File Offset: 0x000303B4
		internal override Task WritePropertyAsync(ODataProperty property)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WritePropertyImplementation(property);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x000321E6 File Offset: 0x000303E6
		internal override void WriteError(ODataError error, bool includeDebugInformation)
		{
			this.WriteErrorImplementation(error, includeDebugInformation);
			base.Flush();
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00032224 File Offset: 0x00030424
		internal override Task WriteErrorAsync(ODataError error, bool includeDebugInformation)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteErrorImplementation(error, includeDebugInformation);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003225D File Offset: 0x0003045D
		internal override void WriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinksImplementation(links);
			base.Flush();
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00032294 File Offset: 0x00030494
		internal override Task WriteEntityReferenceLinksAsync(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteEntityReferenceLinksImplementation(links);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x000322C6 File Offset: 0x000304C6
		internal override void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinkImplementation(link);
			base.Flush();
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000322FC File Offset: 0x000304FC
		internal override Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteEntityReferenceLinkImplementation(link);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00032330 File Offset: 0x00030530
		private ODataWriter CreateODataFeedWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataVerboseJsonWriter odataVerboseJsonWriter = new ODataVerboseJsonWriter(this, entitySet, entityType, true);
			this.outputInStreamErrorListener = odataVerboseJsonWriter;
			return odataVerboseJsonWriter;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00032350 File Offset: 0x00030550
		private ODataWriter CreateODataEntryWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataVerboseJsonWriter odataVerboseJsonWriter = new ODataVerboseJsonWriter(this, entitySet, entityType, false);
			this.outputInStreamErrorListener = odataVerboseJsonWriter;
			return odataVerboseJsonWriter;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00032370 File Offset: 0x00030570
		private ODataCollectionWriter CreateODataCollectionWriterImplementation(IEdmTypeReference itemTypeReference)
		{
			ODataVerboseJsonCollectionWriter odataVerboseJsonCollectionWriter = new ODataVerboseJsonCollectionWriter(this, itemTypeReference);
			this.outputInStreamErrorListener = odataVerboseJsonCollectionWriter;
			return odataVerboseJsonCollectionWriter;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00032390 File Offset: 0x00030590
		private ODataParameterWriter CreateODataParameterWriterImplementation(IEdmFunctionImport functionImport)
		{
			ODataVerboseJsonParameterWriter odataVerboseJsonParameterWriter = new ODataVerboseJsonParameterWriter(this, functionImport);
			this.outputInStreamErrorListener = odataVerboseJsonParameterWriter;
			return odataVerboseJsonParameterWriter;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x000323AD File Offset: 0x000305AD
		private void WriteInStreamErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			ODataJsonWriterUtils.WriteError(base.JsonWriter, null, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth, false);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000323E4 File Offset: 0x000305E4
		private void WritePropertyImplementation(ODataProperty property)
		{
			ODataVerboseJsonPropertyAndValueSerializer odataVerboseJsonPropertyAndValueSerializer = new ODataVerboseJsonPropertyAndValueSerializer(this);
			odataVerboseJsonPropertyAndValueSerializer.WriteTopLevelProperty(property);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00032400 File Offset: 0x00030600
		private void WriteServiceDocumentImplementation(ODataWorkspace defaultWorkspace)
		{
			ODataVerboseJsonServiceDocumentSerializer odataVerboseJsonServiceDocumentSerializer = new ODataVerboseJsonServiceDocumentSerializer(this);
			odataVerboseJsonServiceDocumentSerializer.WriteServiceDocument(defaultWorkspace);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003241C File Offset: 0x0003061C
		private void WriteErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			ODataVerboseJsonSerializer odataVerboseJsonSerializer = new ODataVerboseJsonSerializer(this);
			odataVerboseJsonSerializer.WriteTopLevelError(error, includeDebugInformation);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00032438 File Offset: 0x00030638
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks links)
		{
			ODataVerboseJsonEntityReferenceLinkSerializer odataVerboseJsonEntityReferenceLinkSerializer = new ODataVerboseJsonEntityReferenceLinkSerializer(this);
			odataVerboseJsonEntityReferenceLinkSerializer.WriteEntityReferenceLinks(links);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00032454 File Offset: 0x00030654
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink link)
		{
			ODataVerboseJsonEntityReferenceLinkSerializer odataVerboseJsonEntityReferenceLinkSerializer = new ODataVerboseJsonEntityReferenceLinkSerializer(this);
			odataVerboseJsonEntityReferenceLinkSerializer.WriteEntityReferenceLink(link);
		}

		// Token: 0x040004B1 RID: 1201
		private readonly AtomAndVerboseJsonTypeNameOracle typeNameOracle = new AtomAndVerboseJsonTypeNameOracle();
	}
}
