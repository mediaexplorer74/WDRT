using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x0200025F RID: 607
	public interface IODataRequestMessage
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600140B RID: 5131
		IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600140C RID: 5132
		// (set) Token: 0x0600140D RID: 5133
		Uri Url { get; set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600140E RID: 5134
		// (set) Token: 0x0600140F RID: 5135
		string Method { get; set; }

		// Token: 0x06001410 RID: 5136
		string GetHeader(string headerName);

		// Token: 0x06001411 RID: 5137
		void SetHeader(string headerName, string headerValue);

		// Token: 0x06001412 RID: 5138
		Stream GetStream();
	}
}
