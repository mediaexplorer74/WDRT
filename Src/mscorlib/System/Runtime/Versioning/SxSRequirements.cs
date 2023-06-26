using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000720 RID: 1824
	[Flags]
	internal enum SxSRequirements
	{
		// Token: 0x04002423 RID: 9251
		None = 0,
		// Token: 0x04002424 RID: 9252
		AppDomainID = 1,
		// Token: 0x04002425 RID: 9253
		ProcessID = 2,
		// Token: 0x04002426 RID: 9254
		CLRInstanceID = 4,
		// Token: 0x04002427 RID: 9255
		AssemblyName = 8,
		// Token: 0x04002428 RID: 9256
		TypeName = 16
	}
}
