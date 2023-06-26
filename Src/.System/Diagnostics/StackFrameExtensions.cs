using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Provides extension methods for the <see cref="T:System.Diagnostics.StackFrame" /> class, which represents a function call on the call stack for the current thread.</summary>
	// Token: 0x020004BB RID: 1211
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class StackFrameExtensions
	{
		/// <summary>Indicates whether the native image is available for the specified stack frame.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>
		///   <see langword="true" /> if a native image is available for this stack frame; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002D40 RID: 11584 RVA: 0x000CBE38 File Offset: 0x000CA038
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool HasNativeImage(this StackFrame stackFrame)
		{
			return stackFrame.GetNativeImageBase() != IntPtr.Zero;
		}

		/// <summary>Indicates whether information about the method in which the specified frame is executing is available.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>
		///   <see langword="true" /> if information about the method in which the current frame is executing is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002D41 RID: 11585 RVA: 0x000CBE4A File Offset: 0x000CA04A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool HasMethod(this StackFrame stackFrame)
		{
			return stackFrame.GetMethod() != null;
		}

		/// <summary>Indicates whether an offset from the start of the IL code for the method that is executing is available.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>
		///   <see langword="true" /> if the offset is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002D42 RID: 11586 RVA: 0x000CBE58 File Offset: 0x000CA058
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool HasILOffset(this StackFrame stackFrame)
		{
			return stackFrame.GetILOffset() != -1;
		}

		/// <summary>Indicates whether the file that contains the code that the specified stack frame is executing is available.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>
		///   <see langword="true" /> if the code that the specified stack frame is executing is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002D43 RID: 11587 RVA: 0x000CBE66 File Offset: 0x000CA066
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool HasSource(this StackFrame stackFrame)
		{
			return stackFrame.GetFileName() != null;
		}

		/// <summary>Gets an interface pointer to the start of the native code for the method that is being executed.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>An interface pointer to the start of the native code for the method that is being executed or <see cref="F:System.IntPtr.Zero" /> if you're targeting the .NET Framework.</returns>
		// Token: 0x06002D44 RID: 11588 RVA: 0x000CBE71 File Offset: 0x000CA071
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IntPtr GetNativeIP(this StackFrame stackFrame)
		{
			return IntPtr.Zero;
		}

		/// <summary>Returns a pointer to the base address of the native image that this stack frame is executing.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>A pointer to the base address of the native image or <see cref="F:System.IntPtr.Zero" /> if you're targeting the .NET Framework.</returns>
		// Token: 0x06002D45 RID: 11589 RVA: 0x000CBE78 File Offset: 0x000CA078
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IntPtr GetNativeImageBase(this StackFrame stackFrame)
		{
			return IntPtr.Zero;
		}
	}
}
