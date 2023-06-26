using System;

namespace System.Net
{
	// Token: 0x0200019B RID: 411
	internal enum ReadState
	{
		// Token: 0x04001300 RID: 4864
		Start,
		// Token: 0x04001301 RID: 4865
		StatusLine,
		// Token: 0x04001302 RID: 4866
		Headers,
		// Token: 0x04001303 RID: 4867
		Data
	}
}
