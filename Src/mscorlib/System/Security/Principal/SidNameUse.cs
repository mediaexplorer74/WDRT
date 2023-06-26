using System;

namespace System.Security.Principal
{
	// Token: 0x02000337 RID: 823
	internal enum SidNameUse
	{
		// Token: 0x040010AA RID: 4266
		User = 1,
		// Token: 0x040010AB RID: 4267
		Group,
		// Token: 0x040010AC RID: 4268
		Domain,
		// Token: 0x040010AD RID: 4269
		Alias,
		// Token: 0x040010AE RID: 4270
		WellKnownGroup,
		// Token: 0x040010AF RID: 4271
		DeletedAccount,
		// Token: 0x040010B0 RID: 4272
		Invalid,
		// Token: 0x040010B1 RID: 4273
		Unknown,
		// Token: 0x040010B2 RID: 4274
		Computer
	}
}
