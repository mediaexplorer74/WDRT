using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.RegistryPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002F5 RID: 757
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class RegistryPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />.</exception>
		// Token: 0x060026BD RID: 9917 RVA: 0x0008E0B9 File Offset: 0x0008C2B9
		public RegistryPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets read access for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for read access.</returns>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x0008E0C2 File Offset: 0x0008C2C2
		// (set) Token: 0x060026BF RID: 9919 RVA: 0x0008E0CA File Offset: 0x0008C2CA
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

		/// <summary>Gets or sets write access for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for write access.</returns>
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x0008E0D3 File Offset: 0x0008C2D3
		// (set) Token: 0x060026C1 RID: 9921 RVA: 0x0008E0DB File Offset: 0x0008C2DB
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

		/// <summary>Gets or sets create-level access for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for create-level access.</returns>
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0008E0E4 File Offset: 0x0008C2E4
		// (set) Token: 0x060026C3 RID: 9923 RVA: 0x0008E0EC File Offset: 0x0008C2EC
		public string Create
		{
			get
			{
				return this.m_create;
			}
			set
			{
				this.m_create = value;
			}
		}

		/// <summary>Gets or sets view access control for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for view access control.</returns>
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x0008E0F5 File Offset: 0x0008C2F5
		// (set) Token: 0x060026C5 RID: 9925 RVA: 0x0008E0FD File Offset: 0x0008C2FD
		public string ViewAccessControl
		{
			get
			{
				return this.m_viewAcl;
			}
			set
			{
				this.m_viewAcl = value;
			}
		}

		/// <summary>Gets or sets change access control for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for change access control. .</returns>
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x0008E106 File Offset: 0x0008C306
		// (set) Token: 0x060026C7 RID: 9927 RVA: 0x0008E10E File Offset: 0x0008C30E
		public string ChangeAccessControl
		{
			get
			{
				return this.m_changeAcl;
			}
			set
			{
				this.m_changeAcl = value;
			}
		}

		/// <summary>Gets or sets a specified set of registry keys that can be viewed and modified.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for create, read, and write access.</returns>
		/// <exception cref="T:System.NotSupportedException">The get accessor is called; it is only provided for C# compiler compatibility.</exception>
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x0008E117 File Offset: 0x0008C317
		// (set) Token: 0x060026C9 RID: 9929 RVA: 0x0008E128 File Offset: 0x0008C328
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
				this.m_create = value;
			}
		}

		/// <summary>Gets or sets full access for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for full access.</returns>
		/// <exception cref="T:System.NotSupportedException">The get accessor is called; it is only provided for C# compiler compatibility.</exception>
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x0008E13F File Offset: 0x0008C33F
		// (set) Token: 0x060026CB RID: 9931 RVA: 0x0008E150 File Offset: 0x0008C350
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
				this.m_create = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.RegistryPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.RegistryPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x060026CC RID: 9932 RVA: 0x0008E168 File Offset: 0x0008C368
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new RegistryPermission(PermissionState.Unrestricted);
			}
			RegistryPermission registryPermission = new RegistryPermission(PermissionState.None);
			if (this.m_read != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Read, this.m_read);
			}
			if (this.m_write != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Write, this.m_write);
			}
			if (this.m_create != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Create, this.m_create);
			}
			if (this.m_viewAcl != null)
			{
				registryPermission.SetPathList(AccessControlActions.View, this.m_viewAcl);
			}
			if (this.m_changeAcl != null)
			{
				registryPermission.SetPathList(AccessControlActions.Change, this.m_changeAcl);
			}
			return registryPermission;
		}

		// Token: 0x04000F05 RID: 3845
		private string m_read;

		// Token: 0x04000F06 RID: 3846
		private string m_write;

		// Token: 0x04000F07 RID: 3847
		private string m_create;

		// Token: 0x04000F08 RID: 3848
		private string m_viewAcl;

		// Token: 0x04000F09 RID: 3849
		private string m_changeAcl;
	}
}
