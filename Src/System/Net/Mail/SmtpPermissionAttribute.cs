using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Controls access to Simple Mail Transport Protocol (SMTP) servers.</summary>
	// Token: 0x02000291 RID: 657
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SmtpPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values that specifies the permission behavior.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />.</exception>
		// Token: 0x0600186D RID: 6253 RVA: 0x0007C441 File Offset: 0x0007A641
		public SmtpPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the level of access to SMTP servers controlled by the attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> value. Valid values are "Connect" and "None".</returns>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0007C44A File Offset: 0x0007A64A
		// (set) Token: 0x0600186F RID: 6255 RVA: 0x0007C452 File Offset: 0x0007A652
		public string Access
		{
			get
			{
				return this.access;
			}
			set
			{
				this.access = value;
			}
		}

		/// <summary>Creates a permission object that can be stored with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> instance.</returns>
		// Token: 0x06001870 RID: 6256 RVA: 0x0007C45C File Offset: 0x0007A65C
		public override IPermission CreatePermission()
		{
			SmtpPermission smtpPermission;
			if (base.Unrestricted)
			{
				smtpPermission = new SmtpPermission(PermissionState.Unrestricted);
			}
			else
			{
				smtpPermission = new SmtpPermission(PermissionState.None);
				if (this.access != null)
				{
					if (string.Compare(this.access, "Connect", StringComparison.OrdinalIgnoreCase) == 0)
					{
						smtpPermission.AddPermission(SmtpAccess.Connect);
					}
					else if (string.Compare(this.access, "ConnectToUnrestrictedPort", StringComparison.OrdinalIgnoreCase) == 0)
					{
						smtpPermission.AddPermission(SmtpAccess.ConnectToUnrestrictedPort);
					}
					else
					{
						if (string.Compare(this.access, "None", StringComparison.OrdinalIgnoreCase) != 0)
						{
							throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[] { "Access", this.access }));
						}
						smtpPermission.AddPermission(SmtpAccess.None);
					}
				}
			}
			return smtpPermission;
		}

		// Token: 0x0400184D RID: 6221
		private const string strAccess = "Access";

		// Token: 0x0400184E RID: 6222
		private string access;
	}
}
