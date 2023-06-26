using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Identifies the processor and bits-per-word of the platform targeted by an executable.</summary>
	// Token: 0x020005C9 RID: 1481
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum ProcessorArchitecture
	{
		/// <summary>An unknown or unspecified combination of processor and bits-per-word.</summary>
		// Token: 0x04001C31 RID: 7217
		[__DynamicallyInvokable]
		None,
		/// <summary>Neutral with respect to processor and bits-per-word.</summary>
		// Token: 0x04001C32 RID: 7218
		[__DynamicallyInvokable]
		MSIL,
		/// <summary>A 32-bit Intel processor, either native or in the Windows on Windows environment on a 64-bit platform (WOW64).</summary>
		// Token: 0x04001C33 RID: 7219
		[__DynamicallyInvokable]
		X86,
		/// <summary>A 64-bit Intel Itanium processor only.</summary>
		// Token: 0x04001C34 RID: 7220
		[__DynamicallyInvokable]
		IA64,
		/// <summary>A 64-bit processor based on the x64 architecture.</summary>
		// Token: 0x04001C35 RID: 7221
		[__DynamicallyInvokable]
		Amd64,
		/// <summary>An ARM processor.</summary>
		// Token: 0x04001C36 RID: 7222
		[__DynamicallyInvokable]
		Arm
	}
}
