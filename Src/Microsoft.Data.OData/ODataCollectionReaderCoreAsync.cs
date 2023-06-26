using System;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x0200014E RID: 334
	internal abstract class ODataCollectionReaderCoreAsync : ODataCollectionReaderCore
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x0001CBF1 File Offset: 0x0001ADF1
		protected ODataCollectionReaderCoreAsync(ODataInputContext inputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(inputContext, expectedItemTypeReference, listener)
		{
		}

		// Token: 0x0600090D RID: 2317
		protected abstract Task<bool> ReadAtStartImplementationAsync();

		// Token: 0x0600090E RID: 2318
		protected abstract Task<bool> ReadAtCollectionStartImplementationAsync();

		// Token: 0x0600090F RID: 2319
		protected abstract Task<bool> ReadAtValueImplementationAsync();

		// Token: 0x06000910 RID: 2320
		protected abstract Task<bool> ReadAtCollectionEndImplementationAsync();

		// Token: 0x06000911 RID: 2321 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		protected override Task<bool> ReadAsynchronously()
		{
			switch (this.State)
			{
			case ODataCollectionReaderState.Start:
				return this.ReadAtStartImplementationAsync();
			case ODataCollectionReaderState.CollectionStart:
				return this.ReadAtCollectionStartImplementationAsync();
			case ODataCollectionReaderState.Value:
				return this.ReadAtValueImplementationAsync();
			case ODataCollectionReaderState.CollectionEnd:
				return this.ReadAtCollectionEndImplementationAsync();
			case ODataCollectionReaderState.Exception:
			case ODataCollectionReaderState.Completed:
				return TaskUtils.GetFaultedTask<bool>(new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionReaderCoreAsync_ReadAsynchronously)));
			default:
				return TaskUtils.GetFaultedTask<bool>(new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionReaderCoreAsync_ReadAsynchronously)));
			}
		}
	}
}
