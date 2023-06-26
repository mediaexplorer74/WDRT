using System;
using System.Security;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Allows security actions for <see cref="T:System.Web.AspNetHostingPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000069 RID: 105
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class AspNetHostingPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.AspNetHostingPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> enumeration values.</param>
		// Token: 0x0600045D RID: 1117 RVA: 0x0001EA59 File Offset: 0x0001CC59
		public AspNetHostingPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this._level = AspNetHostingPermissionLevel.None;
		}

		/// <summary>Gets or sets the current hosting permission level.</summary>
		/// <returns>One of the <see cref="T:System.Web.AspNetHostingPermissionLevel" /> enumeration values.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0001EA6A File Offset: 0x0001CC6A
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0001EA72 File Offset: 0x0001CC72
		public AspNetHostingPermissionLevel Level
		{
			get
			{
				return this._level;
			}
			set
			{
				AspNetHostingPermission.VerifyAspNetHostingPermissionLevel(value, "Level");
				this._level = value;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Web.AspNetHostingPermission" /> with the permission level previously set by the <see cref="P:System.Web.AspNetHostingPermissionAttribute.Level" /> property.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that is the new <see cref="T:System.Web.AspNetHostingPermission" />.</returns>
		// Token: 0x06000460 RID: 1120 RVA: 0x0001EA86 File Offset: 0x0001CC86
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new AspNetHostingPermission(PermissionState.Unrestricted);
			}
			return new AspNetHostingPermission(this._level);
		}

		// Token: 0x04000BC8 RID: 3016
		private AspNetHostingPermissionLevel _level;
	}
}
