using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F4 RID: 500
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeLoadLibrary : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x00064268 File Offset: 0x00062468
		static SafeLoadLibrary()
		{
			try
			{
				IntPtr moduleHandleW = UnsafeNclNativeMethods.SafeNetHandles.GetModuleHandleW("kernel32.dll");
				if (moduleHandleW != IntPtr.Zero && UnsafeNclNativeMethods.GetProcAddress(moduleHandleW, "AddDllDirectory") != IntPtr.Zero)
				{
					SafeLoadLibrary._flags = 2048U;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000642D4 File Offset: 0x000624D4
		private SafeLoadLibrary()
			: base(true)
		{
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x000642DD File Offset: 0x000624DD
		private SafeLoadLibrary(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000642E8 File Offset: 0x000624E8
		public static SafeLoadLibrary LoadLibraryEx(string library)
		{
			SafeLoadLibrary safeLoadLibrary = UnsafeNclNativeMethods.SafeNetHandles.LoadLibraryExW(library, null, SafeLoadLibrary._flags);
			if (safeLoadLibrary.IsInvalid)
			{
				safeLoadLibrary.SetHandleAsInvalid();
			}
			return safeLoadLibrary;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00064314 File Offset: 0x00062514
		public bool HasFunction(string functionName)
		{
			IntPtr procAddress = UnsafeNclNativeMethods.GetProcAddress(this, functionName);
			return procAddress != IntPtr.Zero;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00064334 File Offset: 0x00062534
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.FreeLibrary(this.handle);
		}

		// Token: 0x0400152C RID: 5420
		private const string KERNEL32 = "kernel32.dll";

		// Token: 0x0400152D RID: 5421
		private const string AddDllDirectory = "AddDllDirectory";

		// Token: 0x0400152E RID: 5422
		private const uint LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048U;

		// Token: 0x0400152F RID: 5423
		public static readonly SafeLoadLibrary Zero = new SafeLoadLibrary(false);

		// Token: 0x04001530 RID: 5424
		private static uint _flags = 0U;
	}
}
