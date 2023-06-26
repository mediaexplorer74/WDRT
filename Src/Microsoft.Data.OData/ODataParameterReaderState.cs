using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001E5 RID: 485
	public enum ODataParameterReaderState
	{
		// Token: 0x04000531 RID: 1329
		Start,
		// Token: 0x04000532 RID: 1330
		Value,
		// Token: 0x04000533 RID: 1331
		Collection,
		// Token: 0x04000534 RID: 1332
		Exception,
		// Token: 0x04000535 RID: 1333
		Completed
	}
}
