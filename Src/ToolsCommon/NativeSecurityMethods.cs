﻿using System;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x02000027 RID: 39
	public class NativeSecurityMethods
	{
		// Token: 0x0600012F RID: 303
		[CLSCompliant(false)]
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ConvertSecurityDescriptorToStringSecurityDescriptor([In] byte[] pBinarySecurityDescriptor, int RequestedStringSDRevision, SecurityInformationFlags SecurityInformation, out IntPtr StringSecurityDescriptor, out int StringSecurityDescriptorLen);

		// Token: 0x06000130 RID: 304
		[CLSCompliant(false)]
		[DllImport("AdvAPI32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetFileSecurity(string lpFileName, SecurityInformationFlags RequestedInformation, IntPtr pSecurityDescriptor, int nLength, ref int lpnLengthNeeded);

		// Token: 0x06000131 RID: 305
		[DllImport("UpdateDLL.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int IU_AdjustProcessPrivilege(string strPrivilegeName, bool fEnabled);
	}
}
