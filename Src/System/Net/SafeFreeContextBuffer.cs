using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001EF RID: 495
	[SuppressUnmanagedCodeSecurity]
	internal abstract class SafeFreeContextBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012EA RID: 4842 RVA: 0x00063F06 File Offset: 0x00062106
		protected SafeFreeContextBuffer()
			: base(true)
		{
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00063F0F File Offset: 0x0006210F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00063F18 File Offset: 0x00062118
		internal static int EnumeratePackages(SecurDll Dll, out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			if (Dll == SecurDll.SECURITY)
			{
				SafeFreeContextBuffer_SECURITY safeFreeContextBuffer_SECURITY = null;
				int num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.EnumerateSecurityPackagesW(out pkgnum, out safeFreeContextBuffer_SECURITY);
				pkgArray = safeFreeContextBuffer_SECURITY;
				if (num != 0 && pkgArray != null)
				{
					pkgArray.SetHandleAsInvalid();
				}
				return num;
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SecurDll" }), "Dll");
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00063F6C File Offset: 0x0006216C
		internal static SafeFreeContextBuffer CreateEmptyHandle(SecurDll dll)
		{
			if (dll == SecurDll.SECURITY)
			{
				return new SafeFreeContextBuffer_SECURITY();
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SecurDll" }), "dll");
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00063F99 File Offset: 0x00062199
		public unsafe static int QueryContextAttributes(SecurDll dll, SafeDeleteContext phContext, ContextAttribute contextAttribute, byte* buffer, SafeHandle refHandle)
		{
			if (dll == SecurDll.SECURITY)
			{
				return SafeFreeContextBuffer.QueryContextAttributes_SECURITY(phContext, contextAttribute, buffer, refHandle);
			}
			return -1;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00063FAC File Offset: 0x000621AC
		private unsafe static int QueryContextAttributes_SECURITY(SafeDeleteContext phContext, ContextAttribute contextAttribute, byte* buffer, SafeHandle refHandle)
		{
			int num = -2146893055;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				phContext.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					phContext.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.QueryContextAttributesW(ref phContext._handle, contextAttribute, (void*)buffer);
					phContext.DangerousRelease();
				}
				if (num == 0 && refHandle != null)
				{
					if (refHandle is SafeFreeContextBuffer)
					{
						((SafeFreeContextBuffer)refHandle).Set(*(IntPtr*)buffer);
					}
					else
					{
						((SafeFreeCertContext)refHandle).Set(*(IntPtr*)buffer);
					}
				}
				if (num != 0 && refHandle != null)
				{
					refHandle.SetHandleAsInvalid();
				}
			}
			return num;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00064054 File Offset: 0x00062254
		public static int SetContextAttributes(SecurDll dll, SafeDeleteContext phContext, ContextAttribute contextAttribute, byte[] buffer)
		{
			if (dll == SecurDll.SECURITY)
			{
				return SafeFreeContextBuffer.SetContextAttributes_SECURITY(phContext, contextAttribute, buffer);
			}
			return -1;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00064064 File Offset: 0x00062264
		private static int SetContextAttributes_SECURITY(SafeDeleteContext phContext, ContextAttribute contextAttribute, byte[] buffer)
		{
			int num = -2146893055;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				phContext.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					phContext.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.SetContextAttributesW(ref phContext._handle, contextAttribute, buffer, buffer.Length);
					phContext.DangerousRelease();
				}
			}
			return num;
		}
	}
}
