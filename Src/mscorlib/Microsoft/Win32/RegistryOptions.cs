using System;

namespace Microsoft.Win32
{
	/// <summary>Specifies options to use when creating a registry key.</summary>
	// Token: 0x02000015 RID: 21
	[Flags]
	public enum RegistryOptions
	{
		/// <summary>A non-volatile key. This is the default.</summary>
		// Token: 0x0400019F RID: 415
		None = 0,
		/// <summary>A volatile key. The information is stored in memory and is not preserved when the corresponding registry hive is unloaded.</summary>
		// Token: 0x040001A0 RID: 416
		Volatile = 1
	}
}
