using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002FA RID: 762
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SiteIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x060026FF RID: 9983 RVA: 0x0008E63E File Offset: 0x0008C83E
		public SiteIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the site name of the calling code.</summary>
		/// <returns>The site name to compare against the site name specified by the security provider.</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x0008E647 File Offset: 0x0008C847
		// (set) Token: 0x06002701 RID: 9985 RVA: 0x0008E64F File Offset: 0x0008C84F
		public string Site
		{
			get
			{
				return this.m_site;
			}
			set
			{
				this.m_site = value;
			}
		}

		/// <summary>Creates and returns a new instance of <see cref="T:System.Security.Permissions.SiteIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002702 RID: 9986 RVA: 0x0008E658 File Offset: 0x0008C858
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new SiteIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_site == null)
			{
				return new SiteIdentityPermission(PermissionState.None);
			}
			return new SiteIdentityPermission(this.m_site);
		}

		// Token: 0x04000F11 RID: 3857
		private string m_site;
	}
}
