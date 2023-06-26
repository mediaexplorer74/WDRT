using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Enums
{
	// Token: 0x02000045 RID: 69
	public enum SwVersionComparisonResult
	{
		// Token: 0x04000132 RID: 306
		UnableToCompare,
		// Token: 0x04000133 RID: 307
		FirstIsGreater,
		// Token: 0x04000134 RID: 308
		SecondIsGreater,
		// Token: 0x04000135 RID: 309
		NumbersAreEqual,
		// Token: 0x04000136 RID: 310
		PackageNotFound
	}
}
