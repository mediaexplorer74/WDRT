using System;
using System.Security;

namespace System.Net.Sockets
{
	// Token: 0x02000397 RID: 919
	// (Invoke) Token: 0x0600224F RID: 8783
	[SuppressUnmanagedCodeSecurity]
	internal delegate SocketError WSARecvMsgDelegate_Blocking(IntPtr socketHandle, IntPtr msg, out int bytesTransferred, IntPtr overlapped, IntPtr completionRoutine);
}
