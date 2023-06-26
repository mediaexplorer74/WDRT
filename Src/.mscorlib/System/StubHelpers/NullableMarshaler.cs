using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x020005A0 RID: 1440
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class NullableMarshaler
	{
		// Token: 0x06004338 RID: 17208 RVA: 0x000FB828 File Offset: 0x000F9A28
		[SecurityCritical]
		internal static IntPtr ConvertToNative<T>(ref T? pManaged) where T : struct
		{
			if (pManaged != null)
			{
				object obj = IReferenceFactory.CreateIReference(pManaged);
				return Marshal.GetComInterfaceForObject(obj, typeof(IReference<T>));
			}
			return IntPtr.Zero;
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x000FB864 File Offset: 0x000F9A64
		[SecurityCritical]
		internal static void ConvertToManagedRetVoid<T>(IntPtr pNative, ref T? retObj) where T : struct
		{
			retObj = NullableMarshaler.ConvertToManaged<T>(pNative);
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x000FB874 File Offset: 0x000F9A74
		[SecurityCritical]
		internal static T? ConvertToManaged<T>(IntPtr pNative) where T : struct
		{
			if (pNative != IntPtr.Zero)
			{
				object obj = InterfaceMarshaler.ConvertToManagedWithoutUnboxing(pNative);
				return (T?)CLRIReferenceImpl<T>.UnboxHelper(obj);
			}
			return null;
		}
	}
}
