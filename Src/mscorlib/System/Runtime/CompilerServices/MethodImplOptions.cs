using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines the details of how a method is implemented.</summary>
	// Token: 0x020008BB RID: 2235
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum MethodImplOptions
	{
		/// <summary>The method is implemented in unmanaged code.</summary>
		// Token: 0x04002A23 RID: 10787
		Unmanaged = 4,
		/// <summary>The method is declared, but its implementation is provided elsewhere.</summary>
		// Token: 0x04002A24 RID: 10788
		ForwardRef = 16,
		/// <summary>The method signature is exported exactly as declared.</summary>
		// Token: 0x04002A25 RID: 10789
		[__DynamicallyInvokable]
		PreserveSig = 128,
		/// <summary>The call is internal, that is, it calls a method that is implemented within the common language runtime.</summary>
		// Token: 0x04002A26 RID: 10790
		InternalCall = 4096,
		/// <summary>The method can be executed by only one thread at a time. Static methods lock on the type, whereas instance methods lock on the instance. Only one thread can execute in any of the instance functions, and only one thread can execute in any of a class's static functions.</summary>
		// Token: 0x04002A27 RID: 10791
		Synchronized = 32,
		/// <summary>The method cannot be inlined. Inlining is an optimization by which a method call is replaced with the method body.</summary>
		// Token: 0x04002A28 RID: 10792
		[__DynamicallyInvokable]
		NoInlining = 8,
		/// <summary>The method should be inlined if possible.</summary>
		// Token: 0x04002A29 RID: 10793
		[ComVisible(false)]
		[__DynamicallyInvokable]
		AggressiveInlining = 256,
		/// <summary>The method is not optimized by the just-in-time (JIT) compiler or by native code generation (see Ngen.exe) when debugging possible code generation problems.</summary>
		// Token: 0x04002A2A RID: 10794
		[__DynamicallyInvokable]
		NoOptimization = 64,
		/// <summary>The JIT compiler should look for security mitigation attributes, such as the user-defined <see langword="System.Runtime.CompilerServices.SecurityMitigationsAttribute" />. If found, the JIT compiler applies any related security mitigations. Available starting with .NET Framework 4.8.</summary>
		// Token: 0x04002A2B RID: 10795
		SecurityMitigations = 1024
	}
}
