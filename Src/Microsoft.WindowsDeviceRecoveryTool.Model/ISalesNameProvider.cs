using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000011 RID: 17
	public interface ISalesNameProvider
	{
		// Token: 0x060000E0 RID: 224
		string NameForVidPid(string vid, string pid);

		// Token: 0x060000E1 RID: 225
		string NameForString(string text);

		// Token: 0x060000E2 RID: 226
		string NameForTypeDesignator(string typeDesignator);
	}
}
