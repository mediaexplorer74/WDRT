using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Enums
{
	// Token: 0x02000041 RID: 65
	public enum ConnectedDeviceMode
	{
		// Token: 0x040000F8 RID: 248
		Normal,
		// Token: 0x040000F9 RID: 249
		Label,
		// Token: 0x040000FA RID: 250
		Uefi,
		// Token: 0x040000FB RID: 251
		QcomSerialComposite,
		// Token: 0x040000FC RID: 252
		QcomRmnetComposite,
		// Token: 0x040000FD RID: 253
		MassStorage,
		// Token: 0x040000FE RID: 254
		QcomDload,
		// Token: 0x040000FF RID: 255
		KernelModeDebugging,
		// Token: 0x04000100 RID: 256
		MsFlashing,
		// Token: 0x04000101 RID: 257
		Test,
		// Token: 0x04000102 RID: 258
		Unknown
	}
}
