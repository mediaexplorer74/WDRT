using System;

namespace System.Net
{
	// Token: 0x0200019F RID: 415
	internal enum WebParseErrorCode
	{
		// Token: 0x04001315 RID: 4885
		Generic,
		// Token: 0x04001316 RID: 4886
		InvalidHeaderName,
		// Token: 0x04001317 RID: 4887
		InvalidContentLength,
		// Token: 0x04001318 RID: 4888
		IncompleteHeaderLine,
		// Token: 0x04001319 RID: 4889
		CrLfError,
		// Token: 0x0400131A RID: 4890
		InvalidChunkFormat,
		// Token: 0x0400131B RID: 4891
		UnexpectedServerResponse
	}
}
