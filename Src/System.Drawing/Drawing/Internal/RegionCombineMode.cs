using System;

namespace System.Drawing.Internal
{
	// Token: 0x020000E0 RID: 224
	internal enum RegionCombineMode
	{
		// Token: 0x04000A5E RID: 2654
		AND = 1,
		// Token: 0x04000A5F RID: 2655
		OR,
		// Token: 0x04000A60 RID: 2656
		XOR,
		// Token: 0x04000A61 RID: 2657
		DIFF,
		// Token: 0x04000A62 RID: 2658
		COPY,
		// Token: 0x04000A63 RID: 2659
		MIN = 1,
		// Token: 0x04000A64 RID: 2660
		MAX = 5
	}
}
