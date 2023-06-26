using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008E4 RID: 2276
	[ComVisible(false)]
	internal struct DependentHandle
	{
		// Token: 0x06005E0F RID: 24079 RVA: 0x0014BC70 File Offset: 0x00149E70
		[SecurityCritical]
		public DependentHandle(object primary, object secondary)
		{
			IntPtr intPtr = (IntPtr)0;
			DependentHandle.nInitialize(primary, secondary, out intPtr);
			this._handle = intPtr;
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06005E10 RID: 24080 RVA: 0x0014BC94 File Offset: 0x00149E94
		public bool IsAllocated
		{
			get
			{
				return this._handle != (IntPtr)0;
			}
		}

		// Token: 0x06005E11 RID: 24081 RVA: 0x0014BCA8 File Offset: 0x00149EA8
		[SecurityCritical]
		public object GetPrimary()
		{
			object obj;
			DependentHandle.nGetPrimary(this._handle, out obj);
			return obj;
		}

		// Token: 0x06005E12 RID: 24082 RVA: 0x0014BCC3 File Offset: 0x00149EC3
		[SecurityCritical]
		public void GetPrimaryAndSecondary(out object primary, out object secondary)
		{
			DependentHandle.nGetPrimaryAndSecondary(this._handle, out primary, out secondary);
		}

		// Token: 0x06005E13 RID: 24083 RVA: 0x0014BCD4 File Offset: 0x00149ED4
		[SecurityCritical]
		public void Free()
		{
			if (this._handle != (IntPtr)0)
			{
				IntPtr handle = this._handle;
				this._handle = (IntPtr)0;
				DependentHandle.nFree(handle);
			}
		}

		// Token: 0x06005E14 RID: 24084
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nInitialize(object primary, object secondary, out IntPtr dependentHandle);

		// Token: 0x06005E15 RID: 24085
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nGetPrimary(IntPtr dependentHandle, out object primary);

		// Token: 0x06005E16 RID: 24086
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nGetPrimaryAndSecondary(IntPtr dependentHandle, out object primary, out object secondary);

		// Token: 0x06005E17 RID: 24087
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nFree(IntPtr dependentHandle);

		// Token: 0x04002A4C RID: 10828
		private IntPtr _handle;
	}
}
