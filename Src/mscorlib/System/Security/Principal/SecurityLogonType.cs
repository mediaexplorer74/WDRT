using System;

namespace System.Security.Principal
{
	// Token: 0x0200032C RID: 812
	[Serializable]
	internal enum SecurityLogonType
	{
		// Token: 0x04001056 RID: 4182
		Interactive = 2,
		// Token: 0x04001057 RID: 4183
		Network,
		// Token: 0x04001058 RID: 4184
		Batch,
		// Token: 0x04001059 RID: 4185
		Service,
		// Token: 0x0400105A RID: 4186
		Proxy,
		// Token: 0x0400105B RID: 4187
		Unlock
	}
}
