using System;

namespace System.Net
{
	// Token: 0x02000212 RID: 530
	[Flags]
	internal enum Alg
	{
		// Token: 0x04001584 RID: 5508
		Any = 0,
		// Token: 0x04001585 RID: 5509
		ClassSignture = 8192,
		// Token: 0x04001586 RID: 5510
		ClassEncrypt = 24576,
		// Token: 0x04001587 RID: 5511
		ClassHash = 32768,
		// Token: 0x04001588 RID: 5512
		ClassKeyXch = 40960,
		// Token: 0x04001589 RID: 5513
		TypeRSA = 1024,
		// Token: 0x0400158A RID: 5514
		TypeBlock = 1536,
		// Token: 0x0400158B RID: 5515
		TypeStream = 2048,
		// Token: 0x0400158C RID: 5516
		TypeDH = 2560,
		// Token: 0x0400158D RID: 5517
		NameDES = 1,
		// Token: 0x0400158E RID: 5518
		NameRC2 = 2,
		// Token: 0x0400158F RID: 5519
		Name3DES = 3,
		// Token: 0x04001590 RID: 5520
		NameAES_128 = 14,
		// Token: 0x04001591 RID: 5521
		NameAES_192 = 15,
		// Token: 0x04001592 RID: 5522
		NameAES_256 = 16,
		// Token: 0x04001593 RID: 5523
		NameAES = 17,
		// Token: 0x04001594 RID: 5524
		NameRC4 = 1,
		// Token: 0x04001595 RID: 5525
		NameMD5 = 3,
		// Token: 0x04001596 RID: 5526
		NameSHA = 4,
		// Token: 0x04001597 RID: 5527
		NameSHA256 = 12,
		// Token: 0x04001598 RID: 5528
		NameSHA384 = 13,
		// Token: 0x04001599 RID: 5529
		NameSHA512 = 14,
		// Token: 0x0400159A RID: 5530
		NameDH_Ephem = 2
	}
}
