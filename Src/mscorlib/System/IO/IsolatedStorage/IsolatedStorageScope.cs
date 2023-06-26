using System;
using System.Runtime.InteropServices;

namespace System.IO.IsolatedStorage
{
	/// <summary>Enumerates the levels of isolated storage scope that are supported by <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" />.</summary>
	// Token: 0x020001AE RID: 430
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum IsolatedStorageScope
	{
		/// <summary>No isolated storage usage.</summary>
		// Token: 0x04000944 RID: 2372
		None = 0,
		/// <summary>Isolated storage scoped by user identity.</summary>
		// Token: 0x04000945 RID: 2373
		User = 1,
		/// <summary>Isolated storage scoped to the application domain identity.</summary>
		// Token: 0x04000946 RID: 2374
		Domain = 2,
		/// <summary>Isolated storage scoped to the identity of the assembly.</summary>
		// Token: 0x04000947 RID: 2375
		Assembly = 4,
		/// <summary>The isolated store can be placed in a location on the file system that might roam (if roaming user data is enabled on the underlying operating system).</summary>
		// Token: 0x04000948 RID: 2376
		Roaming = 8,
		/// <summary>Isolated storage scoped to the machine.</summary>
		// Token: 0x04000949 RID: 2377
		Machine = 16,
		/// <summary>Isolated storage scoped to the application.</summary>
		// Token: 0x0400094A RID: 2378
		Application = 32
	}
}
