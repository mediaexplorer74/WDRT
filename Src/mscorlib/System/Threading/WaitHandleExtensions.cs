using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	/// <summary>Provides convenience methods to for working with a safe handle for a wait handle.</summary>
	// Token: 0x02000532 RID: 1330
	[__DynamicallyInvokable]
	public static class WaitHandleExtensions
	{
		/// <summary>Gets the safe handle for a native operating system wait handle.</summary>
		/// <param name="waitHandle">A native operating system handle.</param>
		/// <returns>The safe wait handle that wraps the native operating system wait handle.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="waitHandle" /> is <see langword="null" />.</exception>
		// Token: 0x06003EBD RID: 16061 RVA: 0x000EAA08 File Offset: 0x000E8C08
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static SafeWaitHandle GetSafeWaitHandle(this WaitHandle waitHandle)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			return waitHandle.SafeWaitHandle;
		}

		/// <summary>Sets a safe handle for a native operating system wait handle.</summary>
		/// <param name="waitHandle">A wait handle that encapsulates an operating system-specific object that waits for exclusive access to a shared resource.</param>
		/// <param name="value">The safe handle to wrap the operating system handle.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="waitHandle" /> is <see langword="null" />.</exception>
		// Token: 0x06003EBE RID: 16062 RVA: 0x000EAA1E File Offset: 0x000E8C1E
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void SetSafeWaitHandle(this WaitHandle waitHandle, SafeWaitHandle value)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			waitHandle.SafeWaitHandle = value;
		}
	}
}
