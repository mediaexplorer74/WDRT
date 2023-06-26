using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the location where an environment variable is stored or retrieved in a set or get operation.</summary>
	// Token: 0x020000DD RID: 221
	[ComVisible(true)]
	public enum EnvironmentVariableTarget
	{
		/// <summary>The environment variable is stored or retrieved from the environment block associated with the current process.</summary>
		// Token: 0x0400056C RID: 1388
		Process,
		/// <summary>The environment variable is stored or retrieved from the <see langword="HKEY_CURRENT_USER\Environment" /> key in the Windows operating system registry.</summary>
		// Token: 0x0400056D RID: 1389
		User,
		/// <summary>The environment variable is stored or retrieved from the <see langword="HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager\Environment" /> key in the Windows operating system registry.</summary>
		// Token: 0x0400056E RID: 1390
		Machine
	}
}
