using System;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004D8 RID: 1240
	[Flags]
	internal enum DeviceContextLayout
	{
		// Token: 0x04003527 RID: 13607
		Normal = 0,
		// Token: 0x04003528 RID: 13608
		RightToLeft = 1,
		// Token: 0x04003529 RID: 13609
		BottomToTop = 2,
		// Token: 0x0400352A RID: 13610
		VerticalBeforeHorizontal = 4,
		// Token: 0x0400352B RID: 13611
		BitmapOrientationPreserved = 8
	}
}
