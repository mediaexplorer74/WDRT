using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides information about the type of code contained in an assembly.</summary>
	// Token: 0x020005C8 RID: 1480
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum AssemblyContentType
	{
		/// <summary>The assembly contains .NET Framework code.</summary>
		// Token: 0x04001C2E RID: 7214
		[__DynamicallyInvokable]
		Default,
		/// <summary>The assembly contains Windows Runtime code.</summary>
		// Token: 0x04001C2F RID: 7215
		[__DynamicallyInvokable]
		WindowsRuntime
	}
}
