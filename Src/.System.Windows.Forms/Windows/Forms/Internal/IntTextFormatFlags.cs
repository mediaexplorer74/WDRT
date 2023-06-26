using System;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E0 RID: 1248
	[Flags]
	internal enum IntTextFormatFlags
	{
		// Token: 0x040035AF RID: 13743
		Bottom = 8,
		// Token: 0x040035B0 RID: 13744
		CalculateRectangle = 1024,
		// Token: 0x040035B1 RID: 13745
		EndEllipsis = 32768,
		// Token: 0x040035B2 RID: 13746
		ExpandTabs = 64,
		// Token: 0x040035B3 RID: 13747
		ExternalLeading = 512,
		// Token: 0x040035B4 RID: 13748
		Default = 0,
		// Token: 0x040035B5 RID: 13749
		HidePrefix = 1048576,
		// Token: 0x040035B6 RID: 13750
		HorizontalCenter = 1,
		// Token: 0x040035B7 RID: 13751
		Internal = 4096,
		// Token: 0x040035B8 RID: 13752
		Left = 0,
		// Token: 0x040035B9 RID: 13753
		ModifyString = 65536,
		// Token: 0x040035BA RID: 13754
		NoClipping = 256,
		// Token: 0x040035BB RID: 13755
		NoPrefix = 2048,
		// Token: 0x040035BC RID: 13756
		NoFullWidthCharacterBreak = 524288,
		// Token: 0x040035BD RID: 13757
		PathEllipsis = 16384,
		// Token: 0x040035BE RID: 13758
		PrefixOnly = 2097152,
		// Token: 0x040035BF RID: 13759
		Right = 2,
		// Token: 0x040035C0 RID: 13760
		RightToLeft = 131072,
		// Token: 0x040035C1 RID: 13761
		SingleLine = 32,
		// Token: 0x040035C2 RID: 13762
		TabStop = 128,
		// Token: 0x040035C3 RID: 13763
		TextBoxControl = 8192,
		// Token: 0x040035C4 RID: 13764
		Top = 0,
		// Token: 0x040035C5 RID: 13765
		VerticalCenter = 4,
		// Token: 0x040035C6 RID: 13766
		WordBreak = 16,
		// Token: 0x040035C7 RID: 13767
		WordEllipsis = 262144
	}
}
