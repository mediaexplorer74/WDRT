using System;
using System.Security;

namespace System.Net.Sockets
{
	// Token: 0x02000392 RID: 914
	// (Invoke) Token: 0x0600223B RID: 8763
	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetAcceptExSockaddrsDelegate(IntPtr buffer, int receiveDataLength, int localAddressLength, int remoteAddressLength, out IntPtr localSocketAddress, out int localSocketAddressLength, out IntPtr remoteSocketAddress, out int remoteSocketAddressLength);
}
