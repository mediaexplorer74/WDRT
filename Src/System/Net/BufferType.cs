using System;

namespace System.Net
{
	// Token: 0x02000122 RID: 290
	internal enum BufferType
	{
		// Token: 0x04000FC8 RID: 4040
		Empty,
		// Token: 0x04000FC9 RID: 4041
		Data,
		// Token: 0x04000FCA RID: 4042
		Token,
		// Token: 0x04000FCB RID: 4043
		Parameters,
		// Token: 0x04000FCC RID: 4044
		Missing,
		// Token: 0x04000FCD RID: 4045
		Extra,
		// Token: 0x04000FCE RID: 4046
		Trailer,
		// Token: 0x04000FCF RID: 4047
		Header,
		// Token: 0x04000FD0 RID: 4048
		Padding = 9,
		// Token: 0x04000FD1 RID: 4049
		Stream,
		// Token: 0x04000FD2 RID: 4050
		ChannelBindings = 14,
		// Token: 0x04000FD3 RID: 4051
		TargetHost = 16,
		// Token: 0x04000FD4 RID: 4052
		ReadOnlyFlag = -2147483648,
		// Token: 0x04000FD5 RID: 4053
		ReadOnlyWithChecksum = 268435456
	}
}
