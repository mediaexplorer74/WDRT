using System;

namespace System.Security.Principal
{
	// Token: 0x0200032B RID: 811
	[Serializable]
	internal enum KerbLogonSubmitType
	{
		// Token: 0x0400104D RID: 4173
		KerbInteractiveLogon = 2,
		// Token: 0x0400104E RID: 4174
		KerbSmartCardLogon = 6,
		// Token: 0x0400104F RID: 4175
		KerbWorkstationUnlockLogon,
		// Token: 0x04001050 RID: 4176
		KerbSmartCardUnlockLogon,
		// Token: 0x04001051 RID: 4177
		KerbProxyLogon,
		// Token: 0x04001052 RID: 4178
		KerbTicketLogon,
		// Token: 0x04001053 RID: 4179
		KerbTicketUnlockLogon,
		// Token: 0x04001054 RID: 4180
		KerbS4ULogon
	}
}
