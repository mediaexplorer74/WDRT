using System;
using System.Net.Sockets;

namespace System.Net
{
	// Token: 0x020001D7 RID: 471
	internal struct AddressInfo
	{
		// Token: 0x040014D3 RID: 5331
		internal AddressInfoHints ai_flags;

		// Token: 0x040014D4 RID: 5332
		internal AddressFamily ai_family;

		// Token: 0x040014D5 RID: 5333
		internal SocketType ai_socktype;

		// Token: 0x040014D6 RID: 5334
		internal ProtocolFamily ai_protocol;

		// Token: 0x040014D7 RID: 5335
		internal int ai_addrlen;

		// Token: 0x040014D8 RID: 5336
		internal unsafe sbyte* ai_canonname;

		// Token: 0x040014D9 RID: 5337
		internal unsafe byte* ai_addr;

		// Token: 0x040014DA RID: 5338
		internal unsafe AddressInfo* ai_next;
	}
}
