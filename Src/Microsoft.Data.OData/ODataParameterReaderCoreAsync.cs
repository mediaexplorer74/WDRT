using System;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x02000157 RID: 343
	internal abstract class ODataParameterReaderCoreAsync : ODataParameterReaderCore
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x0001D2E2 File Offset: 0x0001B4E2
		protected ODataParameterReaderCoreAsync(ODataInputContext inputContext, IEdmFunctionImport functionImport)
			: base(inputContext, functionImport)
		{
		}

		// Token: 0x06000946 RID: 2374
		protected abstract Task<bool> ReadAtStartImplementationAsync();

		// Token: 0x06000947 RID: 2375
		protected abstract Task<bool> ReadNextParameterImplementationAsync();

		// Token: 0x06000948 RID: 2376
		protected abstract Task<ODataCollectionReader> CreateCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference);

		// Token: 0x06000949 RID: 2377 RVA: 0x0001D2EC File Offset: 0x0001B4EC
		protected override Task<bool> ReadAsynchronously()
		{
			switch (this.State)
			{
			case ODataParameterReaderState.Start:
				return this.ReadAtStartImplementationAsync();
			case ODataParameterReaderState.Value:
			case ODataParameterReaderState.Collection:
				base.OnParameterCompleted();
				return this.ReadNextParameterImplementationAsync();
			case ODataParameterReaderState.Exception:
			case ODataParameterReaderState.Completed:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCoreAsync_ReadAsynchronously));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCoreAsync_ReadAsynchronously));
			}
		}
	}
}
