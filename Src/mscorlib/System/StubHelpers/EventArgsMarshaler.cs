using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200059A RID: 1434
	[FriendAccessAllowed]
	internal static class EventArgsMarshaler
	{
		// Token: 0x06004305 RID: 17157 RVA: 0x000FAFE8 File Offset: 0x000F91E8
		[SecurityCritical]
		[FriendAccessAllowed]
		internal static IntPtr CreateNativeNCCEventArgsInstance(int action, object newItems, object oldItems, int newIndex, int oldIndex)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			IntPtr intPtr3;
			try
			{
				if (newItems != null)
				{
					intPtr = Marshal.GetComInterfaceForObject(newItems, typeof(IBindableVector));
				}
				if (oldItems != null)
				{
					intPtr2 = Marshal.GetComInterfaceForObject(oldItems, typeof(IBindableVector));
				}
				intPtr3 = EventArgsMarshaler.CreateNativeNCCEventArgsInstanceHelper(action, intPtr, intPtr2, newIndex, oldIndex);
			}
			finally
			{
				if (!intPtr2.IsNull())
				{
					Marshal.Release(intPtr2);
				}
				if (!intPtr.IsNull())
				{
					Marshal.Release(intPtr);
				}
			}
			return intPtr3;
		}

		// Token: 0x06004306 RID: 17158
		[SecurityCritical]
		[FriendAccessAllowed]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall")]
		internal static extern IntPtr CreateNativePCEventArgsInstance([MarshalAs(UnmanagedType.HString)] string name);

		// Token: 0x06004307 RID: 17159
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall")]
		internal static extern IntPtr CreateNativeNCCEventArgsInstanceHelper(int action, IntPtr newItem, IntPtr oldItem, int newIndex, int oldIndex);
	}
}
