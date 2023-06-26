using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000263 RID: 611
	public interface IODataResponseMessageAsync : IODataResponseMessage
	{
		// Token: 0x06001436 RID: 5174
		Task<Stream> GetStreamAsync();
	}
}
