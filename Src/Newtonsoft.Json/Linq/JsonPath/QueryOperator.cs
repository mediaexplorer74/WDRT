using System;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000D5 RID: 213
	internal enum QueryOperator
	{
		// Token: 0x040003BA RID: 954
		None,
		// Token: 0x040003BB RID: 955
		Equals,
		// Token: 0x040003BC RID: 956
		NotEquals,
		// Token: 0x040003BD RID: 957
		Exists,
		// Token: 0x040003BE RID: 958
		LessThan,
		// Token: 0x040003BF RID: 959
		LessThanOrEquals,
		// Token: 0x040003C0 RID: 960
		GreaterThan,
		// Token: 0x040003C1 RID: 961
		GreaterThanOrEquals,
		// Token: 0x040003C2 RID: 962
		And,
		// Token: 0x040003C3 RID: 963
		Or,
		// Token: 0x040003C4 RID: 964
		RegexEquals,
		// Token: 0x040003C5 RID: 965
		StrictEquals,
		// Token: 0x040003C6 RID: 966
		StrictNotEquals
	}
}
