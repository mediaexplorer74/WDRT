using System;

namespace Microsoft.Win32
{
	/// <summary>Specifies optional behavior when retrieving name/value pairs from a registry key.</summary>
	// Token: 0x02000013 RID: 19
	[Flags]
	public enum RegistryValueOptions
	{
		/// <summary>No optional behavior is specified.</summary>
		// Token: 0x04000198 RID: 408
		None = 0,
		/// <summary>A value of type <see cref="F:Microsoft.Win32.RegistryValueKind.ExpandString" /> is retrieved without expanding its embedded environment variables.</summary>
		// Token: 0x04000199 RID: 409
		DoNotExpandEnvironmentNames = 1
	}
}
