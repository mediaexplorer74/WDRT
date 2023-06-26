using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.EnvironmentPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002EF RID: 751
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.SecurityAction" />.</exception>
		// Token: 0x06002677 RID: 9847 RVA: 0x0008DB8C File Offset: 0x0008BD8C
		public EnvironmentPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets read access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for read access.</returns>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x0008DB95 File Offset: 0x0008BD95
		// (set) Token: 0x06002679 RID: 9849 RVA: 0x0008DB9D File Offset: 0x0008BD9D
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

		/// <summary>Gets or sets write access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for write access.</returns>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x0008DBA6 File Offset: 0x0008BDA6
		// (set) Token: 0x0600267B RID: 9851 RVA: 0x0008DBAE File Offset: 0x0008BDAE
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

		/// <summary>Sets full access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for full access.</returns>
		/// <exception cref="T:System.NotSupportedException">The get method is not supported for this property.</exception>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x0008DBB7 File Offset: 0x0008BDB7
		// (set) Token: 0x0600267D RID: 9853 RVA: 0x0008DBC8 File Offset: 0x0008BDC8
		public string All
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
			}
			set
			{
				this.m_write = value;
				this.m_read = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.EnvironmentPermission" />.</summary>
		/// <returns>An <see cref="T:System.Security.Permissions.EnvironmentPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x0600267E RID: 9854 RVA: 0x0008DBD8 File Offset: 0x0008BDD8
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new EnvironmentPermission(PermissionState.Unrestricted);
			}
			EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.None);
			if (this.m_read != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Read, this.m_read);
			}
			if (this.m_write != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Write, this.m_write);
			}
			return environmentPermission;
		}

		// Token: 0x04000EF0 RID: 3824
		private string m_read;

		// Token: 0x04000EF1 RID: 3825
		private string m_write;
	}
}
