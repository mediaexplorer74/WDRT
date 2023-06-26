using System;

namespace System.Net
{
	// Token: 0x0200019C RID: 412
	internal enum DataParseStatus
	{
		// Token: 0x04001305 RID: 4869
		NeedMoreData,
		// Token: 0x04001306 RID: 4870
		ContinueParsing,
		// Token: 0x04001307 RID: 4871
		Done,
		// Token: 0x04001308 RID: 4872
		Invalid,
		// Token: 0x04001309 RID: 4873
		DataTooBig
	}
}
