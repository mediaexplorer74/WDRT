using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000771 RID: 1905
	[Serializable]
	internal enum InternalParseStateE
	{
		// Token: 0x04002544 RID: 9540
		Initial,
		// Token: 0x04002545 RID: 9541
		Object,
		// Token: 0x04002546 RID: 9542
		Member,
		// Token: 0x04002547 RID: 9543
		MemberChild
	}
}
