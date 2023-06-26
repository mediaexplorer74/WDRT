using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls the marshaling behavior of a delegate signature passed as an unmanaged function pointer to or from unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x0200090D RID: 2317
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class UnmanagedFunctionPointerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute" /> class with the specified calling convention.</summary>
		/// <param name="callingConvention">The specified calling convention.</param>
		// Token: 0x06006005 RID: 24581 RVA: 0x0014CD66 File Offset: 0x0014AF66
		[__DynamicallyInvokable]
		public UnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
		{
			this.m_callingConvention = callingConvention;
		}

		/// <summary>Gets the value of the calling convention.</summary>
		/// <returns>The value of the calling convention specified by the <see cref="M:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute.#ctor(System.Runtime.InteropServices.CallingConvention)" /> constructor.</returns>
		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06006006 RID: 24582 RVA: 0x0014CD75 File Offset: 0x0014AF75
		[__DynamicallyInvokable]
		public CallingConvention CallingConvention
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x04002A65 RID: 10853
		private CallingConvention m_callingConvention;

		/// <summary>Indicates how to marshal string parameters to the method, and controls name mangling.</summary>
		// Token: 0x04002A66 RID: 10854
		[__DynamicallyInvokable]
		public CharSet CharSet;

		/// <summary>Enables or disables best-fit mapping behavior when converting Unicode characters to ANSI characters.</summary>
		// Token: 0x04002A67 RID: 10855
		[__DynamicallyInvokable]
		public bool BestFitMapping;

		/// <summary>Enables or disables the throwing of an exception on an unmappable Unicode character that is converted to an ANSI "?" character.</summary>
		// Token: 0x04002A68 RID: 10856
		[__DynamicallyInvokable]
		public bool ThrowOnUnmappableChar;

		/// <summary>Indicates whether the callee calls the <see langword="SetLastError" /> Win32 API function before returning from the attributed method.</summary>
		// Token: 0x04002A69 RID: 10857
		[__DynamicallyInvokable]
		public bool SetLastError;
	}
}
