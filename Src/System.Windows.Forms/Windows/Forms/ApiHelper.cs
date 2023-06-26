using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000112 RID: 274
	internal static class ApiHelper
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x000155EC File Offset: 0x000137EC
		public static bool IsApiAvailable(string libName, string procName)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(libName) && !string.IsNullOrEmpty(procName))
			{
				Tuple<string, string> tuple = new Tuple<string, string>(libName, procName);
				if (ApiHelper.availableApis.TryGetValue(tuple, out flag))
				{
					return flag;
				}
				IntPtr intPtr = CommonUnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable(libName);
				if (intPtr != IntPtr.Zero)
				{
					IntPtr procAddress = CommonUnsafeNativeMethods.GetProcAddress(new HandleRef(flag, intPtr), procName);
					if (procAddress != IntPtr.Zero)
					{
						flag = true;
					}
				}
				CommonUnsafeNativeMethods.FreeLibrary(new HandleRef(flag, intPtr));
				ApiHelper.availableApis[tuple] = flag;
			}
			return flag;
		}

		// Token: 0x040004F8 RID: 1272
		private static ConcurrentDictionary<Tuple<string, string>, bool> availableApis = new ConcurrentDictionary<Tuple<string, string>, bool>();
	}
}
