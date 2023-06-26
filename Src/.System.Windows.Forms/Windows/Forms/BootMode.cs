using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the boot mode in which the system was started.</summary>
	// Token: 0x0200013C RID: 316
	public enum BootMode
	{
		/// <summary>The computer was started in the standard boot mode. This mode uses the normal drivers and settings for the system.</summary>
		// Token: 0x040006FD RID: 1789
		Normal,
		/// <summary>The computer was started in safe mode without network support. This mode uses a limited drivers and settings profile.</summary>
		// Token: 0x040006FE RID: 1790
		FailSafe,
		/// <summary>The computer was started in safe mode with network support. This mode uses a limited drivers and settings profile, and loads the services needed to start networking.</summary>
		// Token: 0x040006FF RID: 1791
		FailSafeWithNetwork
	}
}
