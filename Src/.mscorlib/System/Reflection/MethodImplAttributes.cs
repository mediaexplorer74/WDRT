using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies flags for the attributes of a method implementation.</summary>
	// Token: 0x02000605 RID: 1541
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum MethodImplAttributes
	{
		/// <summary>Specifies flags about code type.</summary>
		// Token: 0x04001D97 RID: 7575
		[__DynamicallyInvokable]
		CodeTypeMask = 3,
		/// <summary>Specifies that the method implementation is in Microsoft intermediate language (MSIL).</summary>
		// Token: 0x04001D98 RID: 7576
		[__DynamicallyInvokable]
		IL = 0,
		/// <summary>Specifies that the method implementation is native.</summary>
		// Token: 0x04001D99 RID: 7577
		[__DynamicallyInvokable]
		Native,
		/// <summary>Specifies that the method implementation is in Optimized Intermediate Language (OPTIL).</summary>
		// Token: 0x04001D9A RID: 7578
		[__DynamicallyInvokable]
		OPTIL,
		/// <summary>Specifies that the method implementation is provided by the runtime.</summary>
		// Token: 0x04001D9B RID: 7579
		[__DynamicallyInvokable]
		Runtime,
		/// <summary>Specifies whether the method is implemented in managed or unmanaged code.</summary>
		// Token: 0x04001D9C RID: 7580
		[__DynamicallyInvokable]
		ManagedMask,
		/// <summary>Specifies that the method is implemented in unmanaged code.</summary>
		// Token: 0x04001D9D RID: 7581
		[__DynamicallyInvokable]
		Unmanaged = 4,
		/// <summary>Specifies that the method is implemented in managed code.</summary>
		// Token: 0x04001D9E RID: 7582
		[__DynamicallyInvokable]
		Managed = 0,
		/// <summary>Specifies that the method is not defined.</summary>
		// Token: 0x04001D9F RID: 7583
		[__DynamicallyInvokable]
		ForwardRef = 16,
		/// <summary>Specifies that the method signature is exported exactly as declared.</summary>
		// Token: 0x04001DA0 RID: 7584
		[__DynamicallyInvokable]
		PreserveSig = 128,
		/// <summary>Specifies an internal call.</summary>
		// Token: 0x04001DA1 RID: 7585
		[__DynamicallyInvokable]
		InternalCall = 4096,
		/// <summary>Specifies that the method is single-threaded through the body. Static methods (<see langword="Shared" /> in Visual Basic) lock on the type, whereas instance methods lock on the instance. You can also use the C# lock statement or the Visual Basic SyncLock statement for this purpose.</summary>
		// Token: 0x04001DA2 RID: 7586
		[__DynamicallyInvokable]
		Synchronized = 32,
		/// <summary>Specifies that the method cannot be inlined.</summary>
		// Token: 0x04001DA3 RID: 7587
		[__DynamicallyInvokable]
		NoInlining = 8,
		/// <summary>Specifies that the method should be inlined wherever possible.</summary>
		// Token: 0x04001DA4 RID: 7588
		[ComVisible(false)]
		[__DynamicallyInvokable]
		AggressiveInlining = 256,
		/// <summary>Specifies that the method is not optimized by the just-in-time (JIT) compiler or by native code generation (see Ngen.exe) when debugging possible code generation problems.</summary>
		// Token: 0x04001DA5 RID: 7589
		[__DynamicallyInvokable]
		NoOptimization = 64,
		/// <summary>Specifies that the JIT compiler should look for security mitigation attributes, such as the user-defined <see langword="System.Runtime.CompilerServices.SecurityMitigationsAttribute" />. If found, the JIT compiler applies any related security mitigations. Available starting with .NET Framework 4.8.</summary>
		// Token: 0x04001DA6 RID: 7590
		SecurityMitigations = 1024,
		/// <summary>Specifies a range check value.</summary>
		// Token: 0x04001DA7 RID: 7591
		MaxMethodImplVal = 65535
	}
}
