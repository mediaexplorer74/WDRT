using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000768 RID: 1896
	[Serializable]
	internal enum BinaryArrayTypeEnum
	{
		// Token: 0x0400250E RID: 9486
		Single,
		// Token: 0x0400250F RID: 9487
		Jagged,
		// Token: 0x04002510 RID: 9488
		Rectangular,
		// Token: 0x04002511 RID: 9489
		SingleOffset,
		// Token: 0x04002512 RID: 9490
		JaggedOffset,
		// Token: 0x04002513 RID: 9491
		RectangularOffset
	}
}
