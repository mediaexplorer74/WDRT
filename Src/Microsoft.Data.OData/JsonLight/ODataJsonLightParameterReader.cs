using System;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000196 RID: 406
	internal sealed class ODataJsonLightParameterReader : ODataParameterReaderCoreAsync
	{
		// Token: 0x06000C1A RID: 3098 RVA: 0x0002A331 File Offset: 0x00028531
		internal ODataJsonLightParameterReader(ODataJsonLightInputContext jsonLightInputContext, IEdmFunctionImport functionImport)
			: base(jsonLightInputContext, functionImport)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightParameterDeserializer = new ODataJsonLightParameterDeserializer(this, jsonLightInputContext);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002A34F File Offset: 0x0002854F
		protected override bool ReadAtStartImplementation()
		{
			this.duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			this.jsonLightParameterDeserializer.ReadPayloadStart(ODataPayloadKind.Parameter, this.duplicatePropertyNamesChecker, false, true);
			return this.ReadAtStartImplementationSynchronously();
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002A385 File Offset: 0x00028585
		protected override Task<bool> ReadAtStartImplementationAsync()
		{
			this.duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			return this.jsonLightParameterDeserializer.ReadPayloadStartAsync(ODataPayloadKind.Parameter, this.duplicatePropertyNamesChecker, false, true).FollowOnSuccessWith((Task t) => this.ReadAtStartImplementationSynchronously());
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002A3BE File Offset: 0x000285BE
		protected override bool ReadNextParameterImplementation()
		{
			return this.ReadNextParameterImplementationSynchronously();
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002A3C6 File Offset: 0x000285C6
		protected override Task<bool> ReadNextParameterImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadNextParameterImplementationSynchronously));
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002A3D9 File Offset: 0x000285D9
		protected override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return this.CreateCollectionReaderSynchronously(expectedItemTypeReference);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002A400 File Offset: 0x00028600
		protected override Task<ODataCollectionReader> CreateCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionReader>(() => this.CreateCollectionReaderSynchronously(expectedItemTypeReference));
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002A432 File Offset: 0x00028632
		private bool ReadAtStartImplementationSynchronously()
		{
			if (this.jsonLightInputContext.JsonReader.NodeType == JsonNodeType.EndOfInput)
			{
				base.PopScope(ODataParameterReaderState.Start);
				base.EnterScope(ODataParameterReaderState.Completed, null, null);
				return false;
			}
			return this.jsonLightParameterDeserializer.ReadNextParameter(this.duplicatePropertyNamesChecker);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002A46A File Offset: 0x0002866A
		private bool ReadNextParameterImplementationSynchronously()
		{
			base.PopScope(this.State);
			return this.jsonLightParameterDeserializer.ReadNextParameter(this.duplicatePropertyNamesChecker);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002A489 File Offset: 0x00028689
		private ODataCollectionReader CreateCollectionReaderSynchronously(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataJsonLightCollectionReader(this.jsonLightInputContext, expectedItemTypeReference, this);
		}

		// Token: 0x0400042F RID: 1071
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000430 RID: 1072
		private readonly ODataJsonLightParameterDeserializer jsonLightParameterDeserializer;

		// Token: 0x04000431 RID: 1073
		private DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
	}
}
