using System;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004D6 RID: 1238
	[Flags]
	internal enum DeviceContextBinaryRasterOperationFlags
	{
		// Token: 0x04003512 RID: 13586
		Black = 1,
		// Token: 0x04003513 RID: 13587
		NotMergePen = 2,
		// Token: 0x04003514 RID: 13588
		MaskNotPen = 3,
		// Token: 0x04003515 RID: 13589
		NotCopyPen = 4,
		// Token: 0x04003516 RID: 13590
		MaskPenNot = 5,
		// Token: 0x04003517 RID: 13591
		Not = 6,
		// Token: 0x04003518 RID: 13592
		XorPen = 7,
		// Token: 0x04003519 RID: 13593
		NotMaskPen = 8,
		// Token: 0x0400351A RID: 13594
		MaskPen = 9,
		// Token: 0x0400351B RID: 13595
		NotXorPen = 10,
		// Token: 0x0400351C RID: 13596
		Nop = 11,
		// Token: 0x0400351D RID: 13597
		MergeNotPen = 12,
		// Token: 0x0400351E RID: 13598
		CopyPen = 13,
		// Token: 0x0400351F RID: 13599
		MergePenNot = 14,
		// Token: 0x04003520 RID: 13600
		MergePen = 15,
		// Token: 0x04003521 RID: 13601
		White = 16
	}
}
