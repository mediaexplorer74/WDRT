using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001FA RID: 506
	internal abstract class SafeFreeCredentials : SafeHandle
	{
		// Token: 0x0600131C RID: 4892 RVA: 0x000646AF File Offset: 0x000628AF
		protected SafeFreeCredentials()
			: base(IntPtr.Zero, true)
		{
			this._handle = default(SSPIHandle);
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x000646C9 File Offset: 0x000628C9
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this._handle.IsZero;
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000646E0 File Offset: 0x000628E0
		public static int AcquireCredentialsHandle(SecurDll dll, string package, CredentialUse intent, ref AuthIdentity authdata, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			if (dll == SecurDll.SECURITY)
			{
				outCredential = new SafeFreeCredential_SECURITY();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					goto IL_52;
				}
				finally
				{
					long num2;
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
				}
				goto IL_2F;
				IL_52:
				if (num != 0)
				{
					outCredential.SetHandleAsInvalid();
				}
				return num;
			}
			IL_2F:
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SecurDll" }), "Dll");
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0006475C File Offset: 0x0006295C
		public static int AcquireDefaultCredential(SecurDll dll, string package, CredentialUse intent, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			if (dll == SecurDll.SECURITY)
			{
				outCredential = new SafeFreeCredential_SECURITY();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					goto IL_54;
				}
				finally
				{
					long num2;
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, IntPtr.Zero, null, null, ref outCredential._handle, out num2);
				}
				goto IL_31;
				IL_54:
				if (num != 0)
				{
					outCredential.SetHandleAsInvalid();
				}
				return num;
			}
			IL_31:
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SecurDll" }), "Dll");
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000647D8 File Offset: 0x000629D8
		public static int AcquireCredentialsHandle(string package, CredentialUse intent, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			outCredential = new SafeFreeCredential_SECURITY();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				long num2;
				num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, authdata, null, null, ref outCredential._handle, out num2);
			}
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0006482C File Offset: 0x00062A2C
		public unsafe static int AcquireCredentialsHandle(SecurDll dll, string package, CredentialUse intent, ref SecureCredential authdata, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			IntPtr certContextArray = authdata.certContextArray;
			try
			{
				IntPtr intPtr = new IntPtr((void*)(&certContextArray));
				if (certContextArray != IntPtr.Zero)
				{
					authdata.certContextArray = intPtr;
				}
				if (dll == SecurDll.SECURITY)
				{
					outCredential = new SafeFreeCredential_SECURITY();
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						goto IL_7F;
					}
					finally
					{
						long num2;
						num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
					}
				}
				throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SecurDll" }), "Dll");
			}
			finally
			{
				authdata.certContextArray = certContextArray;
			}
			IL_7F:
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x000648E0 File Offset: 0x00062AE0
		public static int AcquireCredentialsHandle(SecurDll dll, string package, CredentialUse intent, ref SecureCredential2 authdata, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			if (dll == SecurDll.SECURITY)
			{
				outCredential = new SafeFreeCredential_SECURITY();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					goto IL_52;
				}
				finally
				{
					long num2;
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
				}
				goto IL_2F;
				IL_52:
				if (num != 0)
				{
					outCredential.SetHandleAsInvalid();
				}
				return num;
			}
			IL_2F:
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SecurDll" }), "Dll");
		}

		// Token: 0x0400153C RID: 5436
		internal SSPIHandle _handle;
	}
}
