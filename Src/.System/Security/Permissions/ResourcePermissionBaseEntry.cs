using System;

namespace System.Security.Permissions
{
	/// <summary>Defines the smallest unit of a code access security permission set.</summary>
	// Token: 0x0200048A RID: 1162
	[Serializable]
	public class ResourcePermissionBaseEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> class.</summary>
		// Token: 0x06002B1D RID: 11037 RVA: 0x000C467D File Offset: 0x000C287D
		public ResourcePermissionBaseEntry()
		{
			this.permissionAccess = 0;
			this.accessPath = new string[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> class with the specified permission access and permission access path.</summary>
		/// <param name="permissionAccess">The integer representation of the permission access level enumeration value. The <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccess" /> property is set to this value.</param>
		/// <param name="permissionAccessPath">The array of strings that identify the resource you are protecting. The <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is set to this value.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="permissionAccessPath" /> is <see langword="null" />.</exception>
		// Token: 0x06002B1E RID: 11038 RVA: 0x000C4698 File Offset: 0x000C2898
		public ResourcePermissionBaseEntry(int permissionAccess, string[] permissionAccessPath)
		{
			if (permissionAccessPath == null)
			{
				throw new ArgumentNullException("permissionAccessPath");
			}
			this.permissionAccess = permissionAccess;
			this.accessPath = permissionAccessPath;
		}

		/// <summary>Gets an integer representation of the access level enumeration value.</summary>
		/// <returns>The access level enumeration value.</returns>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x000C46BC File Offset: 0x000C28BC
		public int PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		/// <summary>Gets an array of strings that identify the resource you are protecting.</summary>
		/// <returns>An array of strings that identify the resource you are protecting.</returns>
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x000C46C4 File Offset: 0x000C28C4
		public string[] PermissionAccessPath
		{
			get
			{
				return this.accessPath;
			}
		}

		// Token: 0x0400265D RID: 9821
		private string[] accessPath;

		// Token: 0x0400265E RID: 9822
		private int permissionAccess;
	}
}
