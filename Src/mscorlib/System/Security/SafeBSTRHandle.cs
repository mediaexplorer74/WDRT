using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Security
{
	// Token: 0x020001E9 RID: 489
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeBSTRHandle : SafeBuffer
	{
		// Token: 0x06001DA5 RID: 7589 RVA: 0x00067844 File Offset: 0x00065A44
		internal SafeBSTRHandle()
			: base(true)
		{
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x00067850 File Offset: 0x00065A50
		internal static SafeBSTRHandle Allocate(string src, uint len)
		{
			SafeBSTRHandle safeBSTRHandle = SafeBSTRHandle.SysAllocStringLen(src, len);
			safeBSTRHandle.Initialize((ulong)(len * 2U));
			return safeBSTRHandle;
		}

		// Token: 0x06001DA7 RID: 7591
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		private static extern SafeBSTRHandle SysAllocStringLen(string src, uint len);

		// Token: 0x06001DA8 RID: 7592 RVA: 0x00067870 File Offset: 0x00065A70
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			Win32Native.ZeroMemory(this.handle, (UIntPtr)(Win32Native.SysStringLen(this.handle) * 2U));
			Win32Native.SysFreeString(this.handle);
			return true;
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0006789C File Offset: 0x00065A9C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe void ClearBuffer()
		{
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.AcquirePointer(ref ptr);
				Win32Native.ZeroMemory((IntPtr)((void*)ptr), (UIntPtr)(Win32Native.SysStringLen((IntPtr)((void*)ptr)) * 2U));
			}
			finally
			{
				if (ptr != null)
				{
					base.ReleasePointer();
				}
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x000678F4 File Offset: 0x00065AF4
		internal int Length
		{
			get
			{
				return (int)Win32Native.SysStringLen(this);
			}
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x000678FC File Offset: 0x00065AFC
		internal unsafe static void Copy(SafeBSTRHandle source, SafeBSTRHandle target)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				source.AcquirePointer(ref ptr);
				target.AcquirePointer(ref ptr2);
				Buffer.Memcpy(ptr2, ptr, (int)(Win32Native.SysStringLen((IntPtr)((void*)ptr)) * 2U));
			}
			finally
			{
				if (ptr != null)
				{
					source.ReleasePointer();
				}
				if (ptr2 != null)
				{
					target.ReleasePointer();
				}
			}
		}
	}
}
