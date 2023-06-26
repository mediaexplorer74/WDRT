using System;
using System.Drawing.Printing;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing
{
	// Token: 0x02000023 RID: 35
	internal static class IntSecurity
	{
		// Token: 0x06000369 RID: 873 RVA: 0x00010498 File Offset: 0x0000E698
		internal static void DemandReadFileIO(string fileName)
		{
			string text = IntSecurity.UnsafeGetFullPath(fileName);
			new FileIOPermission(FileIOPermissionAccess.Read, text).Demand();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000104BC File Offset: 0x0000E6BC
		internal static void DemandWriteFileIO(string fileName)
		{
			string text = IntSecurity.UnsafeGetFullPath(fileName);
			new FileIOPermission(FileIOPermissionAccess.Write, text).Demand();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000104E0 File Offset: 0x0000E6E0
		internal static string UnsafeGetFullPath(string fileName)
		{
			string text = fileName;
			new FileIOPermission(PermissionState.None)
			{
				AllFiles = FileIOPermissionAccess.PathDiscovery
			}.Assert();
			try
			{
				text = Path.GetFullPath(fileName);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return text;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00010524 File Offset: 0x0000E724
		public static PermissionSet AllPrintingAndUnmanagedCode
		{
			get
			{
				if (IntSecurity.allPrintingAndUnmanagedCode == null)
				{
					PermissionSet permissionSet = new PermissionSet(PermissionState.None);
					permissionSet.SetPermission(IntSecurity.UnmanagedCode);
					permissionSet.SetPermission(IntSecurity.AllPrinting);
					IntSecurity.allPrintingAndUnmanagedCode = permissionSet;
				}
				return IntSecurity.allPrintingAndUnmanagedCode;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00010564 File Offset: 0x0000E764
		internal static bool HasPermission(PrintingPermission permission)
		{
			bool flag;
			try
			{
				permission.Demand();
				flag = true;
			}
			catch (SecurityException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000197 RID: 407
		private static readonly UIPermission AllWindows = new UIPermission(UIPermissionWindow.AllWindows);

		// Token: 0x04000198 RID: 408
		private static readonly UIPermission SafeSubWindows = new UIPermission(UIPermissionWindow.SafeSubWindows);

		// Token: 0x04000199 RID: 409
		public static readonly CodeAccessPermission UnmanagedCode = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);

		// Token: 0x0400019A RID: 410
		public static readonly CodeAccessPermission ObjectFromWin32Handle = IntSecurity.UnmanagedCode;

		// Token: 0x0400019B RID: 411
		public static readonly CodeAccessPermission Win32HandleManipulation = IntSecurity.UnmanagedCode;

		// Token: 0x0400019C RID: 412
		public static readonly PrintingPermission NoPrinting = new PrintingPermission(PrintingPermissionLevel.NoPrinting);

		// Token: 0x0400019D RID: 413
		public static readonly PrintingPermission SafePrinting = new PrintingPermission(PrintingPermissionLevel.SafePrinting);

		// Token: 0x0400019E RID: 414
		public static readonly PrintingPermission DefaultPrinting = new PrintingPermission(PrintingPermissionLevel.DefaultPrinting);

		// Token: 0x0400019F RID: 415
		public static readonly PrintingPermission AllPrinting = new PrintingPermission(PrintingPermissionLevel.AllPrinting);

		// Token: 0x040001A0 RID: 416
		private static PermissionSet allPrintingAndUnmanagedCode;
	}
}
