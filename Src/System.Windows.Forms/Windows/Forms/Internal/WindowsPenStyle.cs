using System;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E9 RID: 1257
	[Flags]
	internal enum WindowsPenStyle
	{
		// Token: 0x040035F9 RID: 13817
		Solid = 0,
		// Token: 0x040035FA RID: 13818
		Dash = 1,
		// Token: 0x040035FB RID: 13819
		Dot = 2,
		// Token: 0x040035FC RID: 13820
		DashDot = 3,
		// Token: 0x040035FD RID: 13821
		DashDotDot = 4,
		// Token: 0x040035FE RID: 13822
		Null = 5,
		// Token: 0x040035FF RID: 13823
		InsideFrame = 6,
		// Token: 0x04003600 RID: 13824
		UserStyle = 7,
		// Token: 0x04003601 RID: 13825
		Alternate = 8,
		// Token: 0x04003602 RID: 13826
		EndcapRound = 0,
		// Token: 0x04003603 RID: 13827
		EndcapSquare = 256,
		// Token: 0x04003604 RID: 13828
		EndcapFlat = 512,
		// Token: 0x04003605 RID: 13829
		JoinRound = 0,
		// Token: 0x04003606 RID: 13830
		JoinBevel = 4096,
		// Token: 0x04003607 RID: 13831
		JoinMiter = 8192,
		// Token: 0x04003608 RID: 13832
		Cosmetic = 0,
		// Token: 0x04003609 RID: 13833
		Geometric = 65536,
		// Token: 0x0400360A RID: 13834
		Default = 0
	}
}
