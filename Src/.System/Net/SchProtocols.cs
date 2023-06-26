using System;

namespace System.Net
{
	// Token: 0x02000211 RID: 529
	[Flags]
	internal enum SchProtocols
	{
		// Token: 0x04001567 RID: 5479
		Zero = 0,
		// Token: 0x04001568 RID: 5480
		PctClient = 2,
		// Token: 0x04001569 RID: 5481
		PctServer = 1,
		// Token: 0x0400156A RID: 5482
		Pct = 3,
		// Token: 0x0400156B RID: 5483
		Ssl2Client = 8,
		// Token: 0x0400156C RID: 5484
		Ssl2Server = 4,
		// Token: 0x0400156D RID: 5485
		Ssl2 = 12,
		// Token: 0x0400156E RID: 5486
		Ssl3Client = 32,
		// Token: 0x0400156F RID: 5487
		Ssl3Server = 16,
		// Token: 0x04001570 RID: 5488
		Ssl3 = 48,
		// Token: 0x04001571 RID: 5489
		Tls10Client = 128,
		// Token: 0x04001572 RID: 5490
		Tls10Server = 64,
		// Token: 0x04001573 RID: 5491
		Tls10 = 192,
		// Token: 0x04001574 RID: 5492
		Tls11Client = 512,
		// Token: 0x04001575 RID: 5493
		Tls11Server = 256,
		// Token: 0x04001576 RID: 5494
		Tls11 = 768,
		// Token: 0x04001577 RID: 5495
		Tls12Client = 2048,
		// Token: 0x04001578 RID: 5496
		Tls12Server = 1024,
		// Token: 0x04001579 RID: 5497
		Tls12 = 3072,
		// Token: 0x0400157A RID: 5498
		Tls13Client = 8192,
		// Token: 0x0400157B RID: 5499
		Tls13Server = 4096,
		// Token: 0x0400157C RID: 5500
		Tls13 = 12288,
		// Token: 0x0400157D RID: 5501
		Ssl3Tls = 240,
		// Token: 0x0400157E RID: 5502
		UniClient = -2147483648,
		// Token: 0x0400157F RID: 5503
		UniServer = 1073741824,
		// Token: 0x04001580 RID: 5504
		Unified = -1073741824,
		// Token: 0x04001581 RID: 5505
		ClientMask = -2147472726,
		// Token: 0x04001582 RID: 5506
		ServerMask = 1073747285
	}
}
