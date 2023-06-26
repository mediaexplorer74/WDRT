using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000265 RID: 613
	public interface IODataRequestMessageAsync : IODataRequestMessage
	{
		// Token: 0x06001443 RID: 5187
		Task<Stream> GetStreamAsync();
	}
}
