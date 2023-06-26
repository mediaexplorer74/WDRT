using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000136 RID: 310
	[StructLayout(LayoutKind.Sequential)]
	internal class SecChannelBindings
	{
		// Token: 0x04001062 RID: 4194
		internal int dwInitiatorAddrType;

		// Token: 0x04001063 RID: 4195
		internal int cbInitiatorLength;

		// Token: 0x04001064 RID: 4196
		internal int dwInitiatorOffset;

		// Token: 0x04001065 RID: 4197
		internal int dwAcceptorAddrType;

		// Token: 0x04001066 RID: 4198
		internal int cbAcceptorLength;

		// Token: 0x04001067 RID: 4199
		internal int dwAcceptorOffset;

		// Token: 0x04001068 RID: 4200
		internal int cbApplicationDataLength;

		// Token: 0x04001069 RID: 4201
		internal int dwApplicationDataOffset;
	}
}
