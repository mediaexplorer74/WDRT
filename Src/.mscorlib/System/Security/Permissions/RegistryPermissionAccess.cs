using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted access to registry keys and values.</summary>
	// Token: 0x02000318 RID: 792
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum RegistryPermissionAccess
	{
		/// <summary>No access to registry variables. <see cref="F:System.Security.Permissions.RegistryPermissionAccess.NoAccess" /> represents no valid <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values and causes an <see cref="T:System.ArgumentException" /> when used as the parameter for <see cref="M:System.Security.Permissions.RegistryPermission.GetPathList(System.Security.Permissions.RegistryPermissionAccess)" />, which expects a single value.</summary>
		// Token: 0x04000F80 RID: 3968
		NoAccess = 0,
		/// <summary>Read access to registry variables.</summary>
		// Token: 0x04000F81 RID: 3969
		Read = 1,
		/// <summary>Write access to registry variables.</summary>
		// Token: 0x04000F82 RID: 3970
		Write = 2,
		/// <summary>Create access to registry variables.</summary>
		// Token: 0x04000F83 RID: 3971
		Create = 4,
		/// <summary>
		///   <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Create" />, <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Read" />, and <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Write" /> access to registry variables. <see cref="F:System.Security.Permissions.RegistryPermissionAccess.AllAccess" /> represents multiple <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values and causes an <see cref="T:System.ArgumentException" /> when used as the <paramref name="access" /> parameter for the <see cref="M:System.Security.Permissions.RegistryPermission.GetPathList(System.Security.Permissions.RegistryPermissionAccess)" /> method, which expects a single value.</summary>
		// Token: 0x04000F84 RID: 3972
		AllAccess = 7
	}
}
