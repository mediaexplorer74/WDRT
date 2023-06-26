using System;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000570 RID: 1392
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class IntSecurity
	{
		// Token: 0x060033C2 RID: 13250 RVA: 0x000E39D8 File Offset: 0x000E1BD8
		public static string UnsafeGetFullPath(string fileName)
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

		// Token: 0x040029AC RID: 10668
		public static readonly CodeAccessPermission UnmanagedCode = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);

		// Token: 0x040029AD RID: 10669
		public static readonly CodeAccessPermission FullReflection = new ReflectionPermission(PermissionState.Unrestricted);
	}
}
