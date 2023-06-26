using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Modifies code generation for runtime just-in-time (JIT) debugging. This class cannot be inherited.</summary>
	// Token: 0x020003E9 RID: 1001
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DebuggableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class, using the specified tracking and optimization options for the just-in-time (JIT) compiler.</summary>
		/// <param name="isJITTrackingEnabled">
		///   <see langword="true" /> to enable debugging; otherwise, <see langword="false" />.</param>
		/// <param name="isJITOptimizerDisabled">
		///   <see langword="true" /> to disable the optimizer for execution; otherwise, <see langword="false" />.</param>
		// Token: 0x06003323 RID: 13091 RVA: 0x000C63B8 File Offset: 0x000C45B8
		public DebuggableAttribute(bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
		{
			this.m_debuggingModes = DebuggableAttribute.DebuggingModes.None;
			if (isJITTrackingEnabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.Default;
			}
			if (isJITOptimizerDisabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.DisableOptimizations;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class, using the specified debugging modes for the just-in-time (JIT) compiler.</summary>
		/// <param name="modes">A bitwise combination of the <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" /> values specifying the debugging mode for the JIT compiler.</param>
		// Token: 0x06003324 RID: 13092 RVA: 0x000C63ED File Offset: 0x000C45ED
		[__DynamicallyInvokable]
		public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
		{
			this.m_debuggingModes = modes;
		}

		/// <summary>Gets a value that indicates whether the runtime will track information during code generation for the debugger.</summary>
		/// <returns>
		///   <see langword="true" /> if the runtime will track information during code generation for the debugger; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000C63FC File Offset: 0x000C45FC
		public bool IsJITTrackingEnabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.Default) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		/// <summary>Gets a value that indicates whether the runtime optimizer is disabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the runtime optimizer is disabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x000C6409 File Offset: 0x000C4609
		public bool IsJITOptimizerDisabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.DisableOptimizations) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		/// <summary>Gets the debugging modes for the attribute.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" /> values describing the debugging mode for the just-in-time (JIT) compiler. The default is <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.Default" />.</returns>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06003327 RID: 13095 RVA: 0x000C641A File Offset: 0x000C461A
		public DebuggableAttribute.DebuggingModes DebuggingFlags
		{
			get
			{
				return this.m_debuggingModes;
			}
		}

		// Token: 0x040016AA RID: 5802
		private DebuggableAttribute.DebuggingModes m_debuggingModes;

		/// <summary>Specifies the debugging mode for the just-in-time (JIT) compiler.</summary>
		// Token: 0x02000B7C RID: 2940
		[Flags]
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public enum DebuggingModes
		{
			/// <summary>Starting with the .NET Framework version 2.0, JIT tracking information is always generated, and this flag has the same effect as <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.Default" />, except that it sets the <see cref="P:System.Diagnostics.DebuggableAttribute.IsJITTrackingEnabled" /> property to <see langword="false" />. However, because JIT tracking is always enabled, the property value is ignored in version 2.0 or later.  
			///  Note that, unlike the <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.DisableOptimizations" /> flag, the <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.None" /> flag cannot be used to disable JIT optimizations.</summary>
			// Token: 0x040034D6 RID: 13526
			[__DynamicallyInvokable]
			None = 0,
			/// <summary>Instructs the just-in-time (JIT) compiler to use its default behavior, which includes enabling optimizations, disabling Edit and Continue support, and using symbol store sequence points if present. Starting with the .NET Framework version 2.0, JIT tracking information, the Microsoft intermediate language (MSIL) offset to the native-code offset within a method, is always generated.</summary>
			// Token: 0x040034D7 RID: 13527
			[__DynamicallyInvokable]
			Default = 1,
			/// <summary>Disable optimizations performed by the compiler to make your output file smaller, faster, and more efficient. Optimizations result in code rearrangement in the output file, which can make debugging difficult. Typically optimization should be disabled while debugging. In versions 2.0 or later, combine this value with Default (Default | DisableOptimizations) to enable JIT tracking and disable optimizations.</summary>
			// Token: 0x040034D8 RID: 13528
			[__DynamicallyInvokable]
			DisableOptimizations = 256,
			/// <summary>Use the implicit MSIL sequence points, not the program database (PDB) sequence points. The symbolic information normally includes at least one Microsoft intermediate language (MSIL) offset for each source line. When the just-in-time (JIT) compiler is about to compile a method, it asks the profiling services for a list of MSIL offsets that should be preserved. These MSIL offsets are called sequence points.</summary>
			// Token: 0x040034D9 RID: 13529
			[__DynamicallyInvokable]
			IgnoreSymbolStoreSequencePoints = 2,
			/// <summary>Enable edit and continue. Edit and continue enables you to make changes to your source code while your program is in break mode. The ability to edit and continue is compiler dependent.</summary>
			// Token: 0x040034DA RID: 13530
			[__DynamicallyInvokable]
			EnableEditAndContinue = 4
		}
	}
}
