using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	/// <summary>Replaces the standard common language runtime (CLR) free-threaded marshaler with the standard OLE STA marshaler.</summary>
	// Token: 0x020003DC RID: 988
	[ComVisible(true)]
	public class StandardOleMarshalObject : MarshalByRefObject, Microsoft.Win32.UnsafeNativeMethods.IMarshal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.StandardOleMarshalObject" /> class.</summary>
		// Token: 0x060025ED RID: 9709 RVA: 0x000B0277 File Offset: 0x000AE477
		protected StandardOleMarshalObject()
		{
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000B0280 File Offset: 0x000AE480
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private IntPtr GetStdMarshaler(ref Guid riid, int dwDestContext, int mshlflags)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this);
			if (iunknownForObject != IntPtr.Zero)
			{
				try
				{
					if (Microsoft.Win32.UnsafeNativeMethods.CoGetStandardMarshal(ref riid, iunknownForObject, dwDestContext, IntPtr.Zero, mshlflags, out zero) == 0)
					{
						return zero;
					}
				}
				finally
				{
					Marshal.Release(iunknownForObject);
				}
			}
			throw new InvalidOperationException(SR.GetString("StandardOleMarshalObjectGetMarshalerFailed", new object[] { riid.ToString() }));
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000B0300 File Offset: 0x000AE500
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int Microsoft.Win32.UnsafeNativeMethods.IMarshal.GetUnmarshalClass(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out Guid pCid)
		{
			pCid = StandardOleMarshalObject.CLSID_StdMarshal;
			return 0;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x000B0310 File Offset: 0x000AE510
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		unsafe int Microsoft.Win32.UnsafeNativeMethods.IMarshal.GetMarshalSizeMax(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out int pSize)
		{
			IntPtr stdMarshaler = this.GetStdMarshaler(ref riid, dwDestContext, mshlflags);
			int num;
			try
			{
				IntPtr intPtr = *(IntPtr*)stdMarshaler.ToPointer();
				IntPtr intPtr2 = *(IntPtr*)((byte*)intPtr.ToPointer() + (IntPtr)4 * (IntPtr)sizeof(IntPtr));
				StandardOleMarshalObject.GetMarshalSizeMax_Delegate getMarshalSizeMax_Delegate = (StandardOleMarshalObject.GetMarshalSizeMax_Delegate)Marshal.GetDelegateForFunctionPointer(intPtr2, typeof(StandardOleMarshalObject.GetMarshalSizeMax_Delegate));
				num = getMarshalSizeMax_Delegate(stdMarshaler, ref riid, pv, dwDestContext, pvDestContext, mshlflags, out pSize);
			}
			finally
			{
				Marshal.Release(stdMarshaler);
			}
			return num;
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x000B0388 File Offset: 0x000AE588
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		unsafe int Microsoft.Win32.UnsafeNativeMethods.IMarshal.MarshalInterface(IntPtr pStm, ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags)
		{
			IntPtr stdMarshaler = this.GetStdMarshaler(ref riid, dwDestContext, mshlflags);
			int num;
			try
			{
				IntPtr intPtr = *(IntPtr*)stdMarshaler.ToPointer();
				IntPtr intPtr2 = *(IntPtr*)((byte*)intPtr.ToPointer() + (IntPtr)5 * (IntPtr)sizeof(IntPtr));
				StandardOleMarshalObject.MarshalInterface_Delegate marshalInterface_Delegate = (StandardOleMarshalObject.MarshalInterface_Delegate)Marshal.GetDelegateForFunctionPointer(intPtr2, typeof(StandardOleMarshalObject.MarshalInterface_Delegate));
				num = marshalInterface_Delegate(stdMarshaler, pStm, ref riid, pv, dwDestContext, pvDestContext, mshlflags);
			}
			finally
			{
				Marshal.Release(stdMarshaler);
			}
			return num;
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000B0404 File Offset: 0x000AE604
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int Microsoft.Win32.UnsafeNativeMethods.IMarshal.UnmarshalInterface(IntPtr pStm, ref Guid riid, out IntPtr ppv)
		{
			ppv = IntPtr.Zero;
			return -2147467263;
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000B0412 File Offset: 0x000AE612
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int Microsoft.Win32.UnsafeNativeMethods.IMarshal.ReleaseMarshalData(IntPtr pStm)
		{
			return -2147467263;
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x000B0419 File Offset: 0x000AE619
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int Microsoft.Win32.UnsafeNativeMethods.IMarshal.DisconnectObject(int dwReserved)
		{
			return -2147467263;
		}

		// Token: 0x0400206D RID: 8301
		private static readonly Guid CLSID_StdMarshal = new Guid("00000017-0000-0000-c000-000000000046");

		// Token: 0x0200080E RID: 2062
		// (Invoke) Token: 0x060044C9 RID: 17609
		[SuppressUnmanagedCodeSecurity]
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int GetMarshalSizeMax_Delegate(IntPtr _this, ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out int pSize);

		// Token: 0x0200080F RID: 2063
		// (Invoke) Token: 0x060044CD RID: 17613
		[SuppressUnmanagedCodeSecurity]
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int MarshalInterface_Delegate(IntPtr _this, IntPtr pStm, ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags);
	}
}
