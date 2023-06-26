using System;
using System.Collections.Generic;

namespace System.Data.Services.Client
{
	// Token: 0x02000066 RID: 102
	public class InvokeResponse : OperationResponse
	{
		// Token: 0x0600036B RID: 875 RVA: 0x0000ED47 File Offset: 0x0000CF47
		public InvokeResponse(Dictionary<string, string> headers)
			: base(new HeaderCollection(headers))
		{
		}
	}
}
