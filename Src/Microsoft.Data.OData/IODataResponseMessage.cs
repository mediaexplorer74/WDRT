using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x02000260 RID: 608
	public interface IODataResponseMessage
	{
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001413 RID: 5139
		IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001414 RID: 5140
		// (set) Token: 0x06001415 RID: 5141
		int StatusCode { get; set; }

		// Token: 0x06001416 RID: 5142
		string GetHeader(string headerName);

		// Token: 0x06001417 RID: 5143
		void SetHeader(string headerName, string headerValue);

		// Token: 0x06001418 RID: 5144
		Stream GetStream();
	}
}
