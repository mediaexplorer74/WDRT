using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies the target operating system platform.</summary>
	// Token: 0x02000A4D RID: 2637
	[__DynamicallyInvokable]
	[Serializable]
	public enum SYSKIND
	{
		/// <summary>The target operating system for the type library is 16-bit Windows systems. By default, data fields are packed.</summary>
		// Token: 0x04002E0A RID: 11786
		[__DynamicallyInvokable]
		SYS_WIN16,
		/// <summary>The target operating system for the type library is 32-bit Windows systems. By default, data fields are naturally aligned (for example, 2-byte integers are aligned on even-byte boundaries; 4-byte integers are aligned on quad-word boundaries, and so on).</summary>
		// Token: 0x04002E0B RID: 11787
		[__DynamicallyInvokable]
		SYS_WIN32,
		/// <summary>The target operating system for the type library is Apple Macintosh. By default, all data fields are aligned on even-byte boundaries.</summary>
		// Token: 0x04002E0C RID: 11788
		[__DynamicallyInvokable]
		SYS_MAC,
		/// <summary>The target operating system for the type library is 64-bit Windows systems.</summary>
		// Token: 0x04002E0D RID: 11789
		[__DynamicallyInvokable]
		SYS_WIN64
	}
}
