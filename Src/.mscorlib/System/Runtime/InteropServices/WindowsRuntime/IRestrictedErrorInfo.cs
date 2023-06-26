﻿using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E1 RID: 2529
	[Guid("82BA7092-4C88-427D-A7BC-16DD93FEB67E")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IRestrictedErrorInfo
	{
		// Token: 0x06006499 RID: 25753
		void GetErrorDetails([MarshalAs(UnmanagedType.BStr)] out string description, out int error, [MarshalAs(UnmanagedType.BStr)] out string restrictedDescription, [MarshalAs(UnmanagedType.BStr)] out string capabilitySid);

		// Token: 0x0600649A RID: 25754
		void GetReference([MarshalAs(UnmanagedType.BStr)] out string reference);
	}
}
