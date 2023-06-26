using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents a wrapper class for handle resources.</summary>
	// Token: 0x02000943 RID: 2371
	[SecurityCritical]
	[__DynamicallyInvokable]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalHandle : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> class with the specified invalid handle value.</summary>
		/// <param name="invalidHandleValue">The value of an invalid handle (usually 0 or -1).</param>
		/// <exception cref="T:System.TypeLoadException">The derived class resides in an assembly without unmanaged code access permission.</exception>
		// Token: 0x06006084 RID: 24708 RVA: 0x0014D876 File Offset: 0x0014BA76
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandle(IntPtr invalidHandleValue)
		{
			this.handle = invalidHandleValue;
			this._isClosed = false;
		}

		/// <summary>Frees all resources associated with the handle.</summary>
		// Token: 0x06006085 RID: 24709 RVA: 0x0014D88C File Offset: 0x0014BA8C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		~CriticalHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x0014D8BC File Offset: 0x0014BABC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Cleanup()
		{
			if (this.IsClosed)
			{
				return;
			}
			this._isClosed = true;
			if (this.IsInvalid)
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!this.ReleaseHandle())
			{
				this.FireCustomerDebugProbe();
			}
			Marshal.SetLastWin32Error(lastWin32Error);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006087 RID: 24711
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void FireCustomerDebugProbe();

		/// <summary>Sets the handle to the specified pre-existing handle.</summary>
		/// <param name="handle">The pre-existing handle to use.</param>
		// Token: 0x06006088 RID: 24712 RVA: 0x0014D902 File Offset: 0x0014BB02
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		/// <summary>Gets a value indicating whether the handle is closed.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is closed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06006089 RID: 24713 RVA: 0x0014D90B File Offset: 0x0014BB0B
		[__DynamicallyInvokable]
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return this._isClosed;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the handle value is invalid.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is valid; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x0600608A RID: 24714
		[__DynamicallyInvokable]
		public abstract bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Marks the handle for releasing and freeing resources.</summary>
		// Token: 0x0600608B RID: 24715 RVA: 0x0014D913 File Offset: 0x0014BB13
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.InteropServices.CriticalHandle" />.</summary>
		// Token: 0x0600608C RID: 24716 RVA: 0x0014D91C File Offset: 0x0014BB1C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> class specifying whether to perform a normal dispose operation.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> for a normal dispose operation; <see langword="false" /> to finalize the handle.</param>
		// Token: 0x0600608D RID: 24717 RVA: 0x0014D925 File Offset: 0x0014BB25
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			this.Cleanup();
		}

		/// <summary>Marks a handle as invalid.</summary>
		// Token: 0x0600608E RID: 24718 RVA: 0x0014D92D File Offset: 0x0014BB2D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void SetHandleAsInvalid()
		{
			this._isClosed = true;
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, executes the code required to free the handle.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is released successfully; otherwise, in the event of a catastrophic failure, <see langword="false" />. In this case, it generates a releaseHandleFailed Managed Debugging Assistant.</returns>
		// Token: 0x0600608F RID: 24719
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		protected abstract bool ReleaseHandle();

		/// <summary>Specifies the handle to be wrapped.</summary>
		// Token: 0x04002B46 RID: 11078
		protected IntPtr handle;

		// Token: 0x04002B47 RID: 11079
		private bool _isClosed;
	}
}
