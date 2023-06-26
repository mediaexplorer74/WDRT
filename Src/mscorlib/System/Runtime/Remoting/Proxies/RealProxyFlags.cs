using System;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x020007FD RID: 2045
	[Flags]
	internal enum RealProxyFlags
	{
		// Token: 0x0400283D RID: 10301
		None = 0,
		// Token: 0x0400283E RID: 10302
		RemotingProxy = 1,
		// Token: 0x0400283F RID: 10303
		Initialized = 2
	}
}
