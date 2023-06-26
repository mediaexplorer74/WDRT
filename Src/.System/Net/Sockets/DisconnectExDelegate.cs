using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net.Sockets
{
	// Token: 0x02000394 RID: 916
	// (Invoke) Token: 0x06002243 RID: 8771
	[SuppressUnmanagedCodeSecurity]
	internal delegate bool DisconnectExDelegate(SafeCloseSocket socketHandle, SafeHandle overlapped, int flags, int reserved);
}
