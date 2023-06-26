using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.PrincipalPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002F3 RID: 755
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class PrincipalPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PrincipalPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x060026A9 RID: 9897 RVA: 0x0008DF6C File Offset: 0x0008C16C
		public PrincipalPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the name of the identity associated with the current principal.</summary>
		/// <returns>A name to match against that provided by the underlying role-based security provider.</returns>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x0008DF7C File Offset: 0x0008C17C
		// (set) Token: 0x060026AB RID: 9899 RVA: 0x0008DF84 File Offset: 0x0008C184
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		/// <summary>Gets or sets membership in a specified security role.</summary>
		/// <returns>The name of a role from the underlying role-based security provider.</returns>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x0008DF8D File Offset: 0x0008C18D
		// (set) Token: 0x060026AD RID: 9901 RVA: 0x0008DF95 File Offset: 0x0008C195
		public string Role
		{
			get
			{
				return this.m_role;
			}
			set
			{
				this.m_role = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the current principal has been authenticated by the underlying role-based security provider.</summary>
		/// <returns>
		///   <see langword="true" /> if the current principal has been authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060026AE RID: 9902 RVA: 0x0008DF9E File Offset: 0x0008C19E
		// (set) Token: 0x060026AF RID: 9903 RVA: 0x0008DFA6 File Offset: 0x0008C1A6
		public bool Authenticated
		{
			get
			{
				return this.m_authenticated;
			}
			set
			{
				this.m_authenticated = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.PrincipalPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.PrincipalPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x060026B0 RID: 9904 RVA: 0x0008DFAF File Offset: 0x0008C1AF
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new PrincipalPermission(PermissionState.Unrestricted);
			}
			return new PrincipalPermission(this.m_name, this.m_role, this.m_authenticated);
		}

		// Token: 0x04000F01 RID: 3841
		private string m_name;

		// Token: 0x04000F02 RID: 3842
		private string m_role;

		// Token: 0x04000F03 RID: 3843
		private bool m_authenticated = true;
	}
}
