using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the action that a custom application domain manager takes when initializing a new domain.</summary>
	// Token: 0x0200009A RID: 154
	[Flags]
	[ComVisible(true)]
	public enum AppDomainManagerInitializationOptions
	{
		/// <summary>No initialization action.</summary>
		// Token: 0x0400039E RID: 926
		None = 0,
		/// <summary>Register the COM callable wrapper for the current <see cref="T:System.AppDomainManager" /> with the unmanaged host.</summary>
		// Token: 0x0400039F RID: 927
		RegisterWithHost = 1
	}
}
