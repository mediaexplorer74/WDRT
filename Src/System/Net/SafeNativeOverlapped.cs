using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001FF RID: 511
	internal class SafeNativeOverlapped : SafeHandle
	{
		// Token: 0x06001334 RID: 4916 RVA: 0x000659A0 File Offset: 0x00063BA0
		internal SafeNativeOverlapped()
			: this(IntPtr.Zero)
		{
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000659AD File Offset: 0x00063BAD
		internal unsafe SafeNativeOverlapped(NativeOverlapped* handle)
			: this((IntPtr)((void*)handle))
		{
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000659BB File Offset: 0x00063BBB
		internal SafeNativeOverlapped(IntPtr handle)
			: base(IntPtr.Zero, true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x000659D0 File Offset: 0x00063BD0
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000659E4 File Offset: 0x00063BE4
		public unsafe void ReinitializeNativeOverlapped()
		{
			IntPtr handle = this.handle;
			if (handle != IntPtr.Zero)
			{
				((NativeOverlapped*)(void*)handle)->InternalHigh = IntPtr.Zero;
				((NativeOverlapped*)(void*)handle)->InternalLow = IntPtr.Zero;
				((NativeOverlapped*)(void*)handle)->EventHandle = IntPtr.Zero;
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00065A38 File Offset: 0x00063C38
		protected unsafe override bool ReleaseHandle()
		{
			IntPtr intPtr = Interlocked.Exchange(ref this.handle, IntPtr.Zero);
			if (intPtr != IntPtr.Zero && !NclUtilities.HasShutdownStarted)
			{
				Overlapped.Free((NativeOverlapped*)(void*)intPtr);
			}
			return true;
		}

		// Token: 0x04001542 RID: 5442
		internal static readonly SafeNativeOverlapped Zero = new SafeNativeOverlapped();
	}
}
