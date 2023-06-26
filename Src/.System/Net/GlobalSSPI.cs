using System;

namespace System.Net
{
	// Token: 0x020001C4 RID: 452
	internal static class GlobalSSPI
	{
		// Token: 0x04001466 RID: 5222
		internal static SSPIInterface SSPIAuth = new SSPIAuthType();

		// Token: 0x04001467 RID: 5223
		internal static SSPIInterface SSPISecureChannel = new SSPISecureChannelType();
	}
}
