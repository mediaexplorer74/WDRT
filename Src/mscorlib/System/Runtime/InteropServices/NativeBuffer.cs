using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000959 RID: 2393
	internal class NativeBuffer : IDisposable
	{
		// Token: 0x0600621F RID: 25119 RVA: 0x00150C93 File Offset: 0x0014EE93
		public NativeBuffer(ulong initialMinCapacity = 0UL)
		{
			this.EnsureByteCapacity(initialMinCapacity);
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06006220 RID: 25120 RVA: 0x00150CA4 File Offset: 0x0014EEA4
		protected unsafe void* VoidPointer
		{
			[SecurityCritical]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this._handle != null)
				{
					return this._handle.DangerousGetHandle().ToPointer();
				}
				return null;
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06006221 RID: 25121 RVA: 0x00150CCF File Offset: 0x0014EECF
		protected unsafe byte* BytePointer
		{
			[SecurityCritical]
			get
			{
				return (byte*)this.VoidPointer;
			}
		}

		// Token: 0x06006222 RID: 25122 RVA: 0x00150CD7 File Offset: 0x0014EED7
		[SecuritySafeCritical]
		public SafeHandle GetHandle()
		{
			return this._handle ?? NativeBuffer.s_emptyHandle;
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06006223 RID: 25123 RVA: 0x00150CE8 File Offset: 0x0014EEE8
		public ulong ByteCapacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x00150CF0 File Offset: 0x0014EEF0
		[SecuritySafeCritical]
		public void EnsureByteCapacity(ulong minCapacity)
		{
			if (this._capacity < minCapacity)
			{
				this.Resize(minCapacity);
				this._capacity = minCapacity;
			}
		}

		// Token: 0x1700110F RID: 4367
		public unsafe byte this[ulong index]
		{
			[SecuritySafeCritical]
			get
			{
				if (index >= this._capacity)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.BytePointer[index];
			}
			[SecuritySafeCritical]
			set
			{
				if (index >= this._capacity)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.BytePointer[index] = value;
			}
		}

		// Token: 0x06006227 RID: 25127 RVA: 0x00150D40 File Offset: 0x0014EF40
		[SecuritySafeCritical]
		private void Resize(ulong byteLength)
		{
			if (byteLength == 0UL)
			{
				this.ReleaseHandle();
				return;
			}
			if (this._handle == null)
			{
				this._handle = NativeBuffer.s_handleCache.Acquire(byteLength);
				return;
			}
			this._handle.Resize(byteLength);
		}

		// Token: 0x06006228 RID: 25128 RVA: 0x00150D72 File Offset: 0x0014EF72
		[SecuritySafeCritical]
		private void ReleaseHandle()
		{
			if (this._handle != null)
			{
				NativeBuffer.s_handleCache.Release(this._handle);
				this._capacity = 0UL;
				this._handle = null;
			}
		}

		// Token: 0x06006229 RID: 25129 RVA: 0x00150D9B File Offset: 0x0014EF9B
		[SecuritySafeCritical]
		public virtual void Free()
		{
			this.ReleaseHandle();
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x00150DA3 File Offset: 0x0014EFA3
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Free();
		}

		// Token: 0x04002B8F RID: 11151
		private static readonly SafeHeapHandleCache s_handleCache = new SafeHeapHandleCache(64UL, 2048UL, 0);

		// Token: 0x04002B90 RID: 11152
		[SecurityCritical]
		private static readonly SafeHandle s_emptyHandle = new NativeBuffer.EmptySafeHandle();

		// Token: 0x04002B91 RID: 11153
		[SecurityCritical]
		private SafeHeapHandle _handle;

		// Token: 0x04002B92 RID: 11154
		private ulong _capacity;

		// Token: 0x02000C92 RID: 3218
		[SecurityCritical]
		private sealed class EmptySafeHandle : SafeHandle
		{
			// Token: 0x0600712C RID: 28972 RVA: 0x00186B1A File Offset: 0x00184D1A
			public EmptySafeHandle()
				: base(IntPtr.Zero, true)
			{
			}

			// Token: 0x17001365 RID: 4965
			// (get) Token: 0x0600712D RID: 28973 RVA: 0x00186B28 File Offset: 0x00184D28
			public override bool IsInvalid
			{
				[SecurityCritical]
				get
				{
					return true;
				}
			}

			// Token: 0x0600712E RID: 28974 RVA: 0x00186B2B File Offset: 0x00184D2B
			[SecurityCritical]
			protected override bool ReleaseHandle()
			{
				return true;
			}
		}
	}
}
