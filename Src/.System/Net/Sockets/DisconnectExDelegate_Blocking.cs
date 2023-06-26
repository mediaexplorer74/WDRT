using System;
using System.Security;

namespace System.Net.Sockets
{
	// Token: 0x02000395 RID: 917
	// (Invoke) Token: 0x06002247 RID: 8775
	[SuppressUnmanagedCodeSecurity]
	internal delegate bool DisconnectExDelegate_Blocking(IntPtr socketHandle, IntPtr overlapped, int flags, int reserved);
}
