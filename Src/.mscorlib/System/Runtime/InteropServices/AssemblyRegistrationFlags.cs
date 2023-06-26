using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Defines a set of flags used when registering assemblies.</summary>
	// Token: 0x02000967 RID: 2407
	[Flags]
	[ComVisible(true)]
	public enum AssemblyRegistrationFlags
	{
		/// <summary>Indicates no special settings.</summary>
		// Token: 0x04002BA2 RID: 11170
		None = 0,
		/// <summary>Indicates that the code base key for the assembly should be set in the registry.</summary>
		// Token: 0x04002BA3 RID: 11171
		SetCodeBase = 1
	}
}
