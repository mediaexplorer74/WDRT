using System;

namespace System.Net
{
	// Token: 0x020001DB RID: 475
	[Flags]
	internal enum SocketConstructorFlags
	{
		// Token: 0x040014EA RID: 5354
		WSA_FLAG_OVERLAPPED = 1,
		// Token: 0x040014EB RID: 5355
		WSA_FLAG_MULTIPOINT_C_ROOT = 2,
		// Token: 0x040014EC RID: 5356
		WSA_FLAG_MULTIPOINT_C_LEAF = 4,
		// Token: 0x040014ED RID: 5357
		WSA_FLAG_MULTIPOINT_D_ROOT = 8,
		// Token: 0x040014EE RID: 5358
		WSA_FLAG_MULTIPOINT_D_LEAF = 16
	}
}
