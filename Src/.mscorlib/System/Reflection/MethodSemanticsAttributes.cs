using System;

namespace System.Reflection
{
	// Token: 0x020005F7 RID: 1527
	[Flags]
	[Serializable]
	internal enum MethodSemanticsAttributes
	{
		// Token: 0x04001D34 RID: 7476
		Setter = 1,
		// Token: 0x04001D35 RID: 7477
		Getter = 2,
		// Token: 0x04001D36 RID: 7478
		Other = 4,
		// Token: 0x04001D37 RID: 7479
		AddOn = 8,
		// Token: 0x04001D38 RID: 7480
		RemoveOn = 16,
		// Token: 0x04001D39 RID: 7481
		Fire = 32
	}
}
