using System;

namespace System.Net.Mime
{
	// Token: 0x0200023D RID: 573
	internal class Base64WriteStateInfo : WriteStateInfoBase
	{
		// Token: 0x060015A9 RID: 5545 RVA: 0x00070672 File Offset: 0x0006E872
		internal Base64WriteStateInfo()
		{
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0007067A File Offset: 0x0006E87A
		internal Base64WriteStateInfo(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength)
			: base(bufferSize, header, footer, maxLineLength, mimeHeaderLength)
		{
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x00070689 File Offset: 0x0006E889
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x00070691 File Offset: 0x0006E891
		internal int Padding { get; set; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0007069A File Offset: 0x0006E89A
		// (set) Token: 0x060015AE RID: 5550 RVA: 0x000706A2 File Offset: 0x0006E8A2
		internal byte LastBits { get; set; }
	}
}
