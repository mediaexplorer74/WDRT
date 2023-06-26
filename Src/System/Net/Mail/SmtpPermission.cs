using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Controls access to Simple Mail Transport Protocol (SMTP) servers.</summary>
	// Token: 0x02000292 RID: 658
	[Serializable]
	public sealed class SmtpPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class using the specified permission state value.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		// Token: 0x06001871 RID: 6257 RVA: 0x0007C50A File Offset: 0x0007A70A
		public SmtpPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.access = SmtpAccess.ConnectToUnrestrictedPort;
				this.unrestricted = true;
				return;
			}
			this.access = SmtpAccess.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class with the specified state.</summary>
		/// <param name="unrestricted">
		///   <see langword="true" /> if the new permission is unrestricted; otherwise, <see langword="false" />.</param>
		// Token: 0x06001872 RID: 6258 RVA: 0x0007C52C File Offset: 0x0007A72C
		public SmtpPermission(bool unrestricted)
		{
			if (unrestricted)
			{
				this.access = SmtpAccess.ConnectToUnrestrictedPort;
				this.unrestricted = true;
				return;
			}
			this.access = SmtpAccess.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class using the specified access level.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values.</param>
		// Token: 0x06001873 RID: 6259 RVA: 0x0007C54D File Offset: 0x0007A74D
		public SmtpPermission(SmtpAccess access)
		{
			this.access = access;
		}

		/// <summary>Gets the level of access to SMTP servers controlled by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values.</returns>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0007C55C File Offset: 0x0007A75C
		public SmtpAccess Access
		{
			get
			{
				return this.access;
			}
		}

		/// <summary>Adds the specified access level value to the permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values.</param>
		// Token: 0x06001875 RID: 6261 RVA: 0x0007C564 File Offset: 0x0007A764
		public void AddPermission(SmtpAccess access)
		{
			if (access > this.access)
			{
				this.access = access;
			}
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001876 RID: 6262 RVA: 0x0007C576 File Offset: 0x0007A776
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> that is identical to the current permission.</returns>
		// Token: 0x06001877 RID: 6263 RVA: 0x0007C57E File Offset: 0x0007A77E
		public override IPermission Copy()
		{
			if (this.unrestricted)
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission(this.access);
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> to combine with the current permission.</param>
		/// <returns>A new <see cref="T:System.Net.Mail.SmtpPermission" /> permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x06001878 RID: 6264 RVA: 0x0007C59C File Offset: 0x0007A79C
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			SmtpPermission smtpPermission = target as SmtpPermission;
			if (smtpPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (this.unrestricted || smtpPermission.IsUnrestricted())
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission((this.access > smtpPermission.access) ? this.access : smtpPermission.access);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> that represents the intersection of the current permission and the specified permission. Returns <see langword="null" /> if the intersection is empty or <paramref name="target" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x06001879 RID: 6265 RVA: 0x0007C60C File Offset: 0x0007A80C
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SmtpPermission smtpPermission = target as SmtpPermission;
			if (smtpPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (this.IsUnrestricted() && smtpPermission.IsUnrestricted())
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission((this.access < smtpPermission.access) ? this.access : smtpPermission.access);
		}

		/// <summary>Returns a value indicating whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x0600187A RID: 6266 RVA: 0x0007C678 File Offset: 0x0007A878
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.access == SmtpAccess.None;
			}
			SmtpPermission smtpPermission = target as SmtpPermission;
			if (smtpPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			return (!this.unrestricted || smtpPermission.IsUnrestricted()) && smtpPermission.access >= this.access;
		}

		/// <summary>Sets the state of the permission using the specified XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to set the state of the current permission.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> does not describe an <see cref="T:System.Net.Mail.SmtpPermission" /> object.  
		/// -or-  
		/// <paramref name="securityElement" /> does not contain the required state information to reconstruct the permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		// Token: 0x0600187B RID: 6267 RVA: 0x0007C6D4 File Offset: 0x0007A8D4
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("net_not_ipermission"), "securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(SR.GetString("net_no_classname"), "securityElement");
			}
			if (text.IndexOf(base.GetType().FullName) < 0)
			{
				throw new ArgumentException(SR.GetString("net_no_typename"), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.access = SmtpAccess.ConnectToUnrestrictedPort;
				this.unrestricted = true;
				return;
			}
			text2 = securityElement.Attribute("Access");
			if (text2 == null)
			{
				return;
			}
			if (string.Compare(text2, "Connect", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.access = SmtpAccess.Connect;
				return;
			}
			if (string.Compare(text2, "ConnectToUnrestrictedPort", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.access = SmtpAccess.ConnectToUnrestrictedPort;
				return;
			}
			if (string.Compare(text2, "None", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.access = SmtpAccess.None;
				return;
			}
			throw new ArgumentException(SR.GetString("net_perm_invalid_val_in_element"), "Access");
		}

		/// <summary>Creates an XML encoding of the state of the permission.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML encoding of the current permission.</returns>
		// Token: 0x0600187C RID: 6268 RVA: 0x0007C7F8 File Offset: 0x0007A9F8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
				return securityElement;
			}
			if (this.access == SmtpAccess.Connect)
			{
				securityElement.AddAttribute("Access", "Connect");
			}
			else if (this.access == SmtpAccess.ConnectToUnrestrictedPort)
			{
				securityElement.AddAttribute("Access", "ConnectToUnrestrictedPort");
			}
			return securityElement;
		}

		// Token: 0x0400184F RID: 6223
		private SmtpAccess access;

		// Token: 0x04001850 RID: 6224
		private bool unrestricted;
	}
}
