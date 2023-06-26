using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.FileIOPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002F1 RID: 753
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class FileIOPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />.</exception>
		// Token: 0x06002685 RID: 9861 RVA: 0x0008DCA1 File Offset: 0x0008BEA1
		public FileIOPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets read access for the file or directory specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for read access.</returns>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x0008DCAA File Offset: 0x0008BEAA
		// (set) Token: 0x06002687 RID: 9863 RVA: 0x0008DCB2 File Offset: 0x0008BEB2
		public string Read
		{
			get
			{
				return this.m_read;
			}
			set
			{
				this.m_read = value;
			}
		}

		/// <summary>Gets or sets write access for the file or directory specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for write access.</returns>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x0008DCBB File Offset: 0x0008BEBB
		// (set) Token: 0x06002689 RID: 9865 RVA: 0x0008DCC3 File Offset: 0x0008BEC3
		public string Write
		{
			get
			{
				return this.m_write;
			}
			set
			{
				this.m_write = value;
			}
		}

		/// <summary>Gets or sets append access for the file or directory that is specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for append access.</returns>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x0008DCCC File Offset: 0x0008BECC
		// (set) Token: 0x0600268B RID: 9867 RVA: 0x0008DCD4 File Offset: 0x0008BED4
		public string Append
		{
			get
			{
				return this.m_append;
			}
			set
			{
				this.m_append = value;
			}
		}

		/// <summary>Gets or sets the file or directory to which to grant path discovery.</summary>
		/// <returns>The absolute path of the file or directory.</returns>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x0008DCDD File Offset: 0x0008BEDD
		// (set) Token: 0x0600268D RID: 9869 RVA: 0x0008DCE5 File Offset: 0x0008BEE5
		public string PathDiscovery
		{
			get
			{
				return this.m_pathDiscovery;
			}
			set
			{
				this.m_pathDiscovery = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which access control information can be viewed.</summary>
		/// <returns>The absolute path of the file or directory in which access control information can be viewed.</returns>
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x0008DCEE File Offset: 0x0008BEEE
		// (set) Token: 0x0600268F RID: 9871 RVA: 0x0008DCF6 File Offset: 0x0008BEF6
		public string ViewAccessControl
		{
			get
			{
				return this.m_viewAccess;
			}
			set
			{
				this.m_viewAccess = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which access control information can be changed.</summary>
		/// <returns>The absolute path of the file or directory in which access control information can be changed.</returns>
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x0008DCFF File Offset: 0x0008BEFF
		// (set) Token: 0x06002691 RID: 9873 RVA: 0x0008DD07 File Offset: 0x0008BF07
		public string ChangeAccessControl
		{
			get
			{
				return this.m_changeAccess;
			}
			set
			{
				this.m_changeAccess = value;
			}
		}

		/// <summary>Gets or sets full access for the file or directory that is specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for full access.</returns>
		/// <exception cref="T:System.NotSupportedException">The get method is not supported for this property.</exception>
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x0008DD2E File Offset: 0x0008BF2E
		// (set) Token: 0x06002692 RID: 9874 RVA: 0x0008DD10 File Offset: 0x0008BF10
		[Obsolete("Please use the ViewAndModify property instead.")]
		public string All
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
			}
			set
			{
				this.m_read = value;
				this.m_write = value;
				this.m_append = value;
				this.m_pathDiscovery = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which file data can be viewed and modified.</summary>
		/// <returns>The absolute path of the file or directory in which file data can be viewed and modified.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see langword="get" /> accessor is called. The accessor is provided only for C# compiler compatibility.</exception>
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x0008DD3F File Offset: 0x0008BF3F
		// (set) Token: 0x06002695 RID: 9877 RVA: 0x0008DD50 File Offset: 0x0008BF50
		public string ViewAndModify
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
			}
			set
			{
				this.m_read = value;
				this.m_write = value;
				this.m_append = value;
				this.m_pathDiscovery = value;
			}
		}

		/// <summary>Gets or sets the permitted access to all files.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents the permissions for all files. The default is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />.</returns>
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x0008DD6E File Offset: 0x0008BF6E
		// (set) Token: 0x06002697 RID: 9879 RVA: 0x0008DD76 File Offset: 0x0008BF76
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.m_allFiles;
			}
			set
			{
				this.m_allFiles = value;
			}
		}

		/// <summary>Gets or sets the permitted access to all local files.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents the permissions for all local files. The default is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />.</returns>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x0008DD7F File Offset: 0x0008BF7F
		// (set) Token: 0x06002699 RID: 9881 RVA: 0x0008DD87 File Offset: 0x0008BF87
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.m_allLocalFiles;
			}
			set
			{
				this.m_allLocalFiles = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.FileIOPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.FileIOPermission" /> that corresponds to this attribute.</returns>
		/// <exception cref="T:System.ArgumentException">The path information for a file or directory for which access is to be secured contains invalid characters or wildcard specifiers.</exception>
		// Token: 0x0600269A RID: 9882 RVA: 0x0008DD90 File Offset: 0x0008BF90
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.None);
			if (this.m_read != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.Read, this.m_read);
			}
			if (this.m_write != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.Write, this.m_write);
			}
			if (this.m_append != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.Append, this.m_append);
			}
			if (this.m_pathDiscovery != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.PathDiscovery, this.m_pathDiscovery);
			}
			if (this.m_viewAccess != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.NoAccess, AccessControlActions.View, new string[] { this.m_viewAccess }, false);
			}
			if (this.m_changeAccess != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, new string[] { this.m_changeAccess }, false);
			}
			fileIOPermission.AllFiles = this.m_allFiles;
			fileIOPermission.AllLocalFiles = this.m_allLocalFiles;
			return fileIOPermission;
		}

		// Token: 0x04000EF3 RID: 3827
		private string m_read;

		// Token: 0x04000EF4 RID: 3828
		private string m_write;

		// Token: 0x04000EF5 RID: 3829
		private string m_append;

		// Token: 0x04000EF6 RID: 3830
		private string m_pathDiscovery;

		// Token: 0x04000EF7 RID: 3831
		private string m_viewAccess;

		// Token: 0x04000EF8 RID: 3832
		private string m_changeAccess;

		// Token: 0x04000EF9 RID: 3833
		[OptionalField(VersionAdded = 2)]
		private FileIOPermissionAccess m_allLocalFiles;

		// Token: 0x04000EFA RID: 3834
		[OptionalField(VersionAdded = 2)]
		private FileIOPermissionAccess m_allFiles;
	}
}
