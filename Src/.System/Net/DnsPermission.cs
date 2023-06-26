using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Controls rights to access Domain Name System (DNS) servers on the network.</summary>
	// Token: 0x020000E2 RID: 226
	[Serializable]
	public sealed class DnsPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.DnsPermission" /> class that either allows unrestricted DNS access or disallows DNS access.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value.</exception>
		// Token: 0x060007BB RID: 1979 RVA: 0x0002B1C7 File Offset: 0x000293C7
		public DnsPermission(PermissionState state)
		{
			this.m_noRestriction = state == PermissionState.Unrestricted;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0002B1D9 File Offset: 0x000293D9
		internal DnsPermission(bool free)
		{
			this.m_noRestriction = free;
		}

		/// <summary>Checks the overall permission state of the object.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.DnsPermission" /> instance was created with <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007BD RID: 1981 RVA: 0x0002B1E8 File Offset: 0x000293E8
		public bool IsUnrestricted()
		{
			return this.m_noRestriction;
		}

		/// <summary>Creates an identical copy of the current permission instance.</summary>
		/// <returns>A new instance of the <see cref="T:System.Net.DnsPermission" /> class that is an identical copy of the current instance.</returns>
		// Token: 0x060007BE RID: 1982 RVA: 0x0002B1F0 File Offset: 0x000293F0
		public override IPermission Copy()
		{
			return new DnsPermission(this.m_noRestriction);
		}

		/// <summary>Creates a permission instance that is the union of the current permission instance and the specified permission instance.</summary>
		/// <param name="target">The <see cref="T:System.Net.DnsPermission" /> instance to combine with the current instance.</param>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> instance that represents the union of the current <see cref="T:System.Net.DnsPermission" /> instance with the specified <see cref="T:System.Net.DnsPermission" /> instance. If <paramref name="target" /> is <see langword="null" />, this method returns a copy of the current instance. If the current instance or <paramref name="target" /> is unrestricted, this method returns a <see cref="T:System.Net.DnsPermission" /> instance that is unrestricted; otherwise, it returns a <see cref="T:System.Net.DnsPermission" /> instance that is restricted.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor <see langword="null" />.</exception>
		// Token: 0x060007BF RID: 1983 RVA: 0x0002B200 File Offset: 0x00029400
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			DnsPermission dnsPermission = target as DnsPermission;
			if (dnsPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			return new DnsPermission(this.m_noRestriction || dnsPermission.m_noRestriction);
		}

		/// <summary>Creates a permission instance that is the intersection of the current permission instance and the specified permission instance.</summary>
		/// <param name="target">The <see cref="T:System.Net.DnsPermission" /> instance to intersect with the current instance.</param>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> instance that represents the intersection of the current <see cref="T:System.Net.DnsPermission" /> instance with the specified <see cref="T:System.Net.DnsPermission" /> instance, or <see langword="null" /> if the intersection is empty. If both the current instance and <paramref name="target" /> are unrestricted, this method returns a new <see cref="T:System.Net.DnsPermission" /> instance that is unrestricted; otherwise, it returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor <see langword="null" />.</exception>
		// Token: 0x060007C0 RID: 1984 RVA: 0x0002B24C File Offset: 0x0002944C
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			DnsPermission dnsPermission = target as DnsPermission;
			if (dnsPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (this.m_noRestriction && dnsPermission.m_noRestriction)
			{
				return new DnsPermission(true);
			}
			return null;
		}

		/// <summary>Determines whether the current permission instance is a subset of the specified permission instance.</summary>
		/// <param name="target">The second <see cref="T:System.Net.DnsPermission" /> instance to be tested for the subset relationship.</param>
		/// <returns>
		///   <see langword="false" /> if the current instance is unrestricted and <paramref name="target" /> is either <see langword="null" /> or unrestricted; otherwise, <see langword="true" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor <see langword="null" />.</exception>
		// Token: 0x060007C1 RID: 1985 RVA: 0x0002B298 File Offset: 0x00029498
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_noRestriction;
			}
			DnsPermission dnsPermission = target as DnsPermission;
			if (dnsPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			return !this.m_noRestriction || dnsPermission.m_noRestriction;
		}

		/// <summary>Reconstructs a <see cref="T:System.Net.DnsPermission" /> instance from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the <see cref="T:System.Net.DnsPermission" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a <see cref="T:System.Net.DnsPermission" /> element.</exception>
		// Token: 0x060007C2 RID: 1986 RVA: 0x0002B2E4 File Offset: 0x000294E4
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("net_no_classname"), "securityElement");
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
			this.m_noRestriction = text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>Creates an XML encoding of a <see cref="T:System.Net.DnsPermission" /> instance and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> instance that contains an XML-encoded representation of the security object, including state information.</returns>
		// Token: 0x060007C3 RID: 1987 RVA: 0x0002B39C File Offset: 0x0002959C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.m_noRestriction)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x04000D2A RID: 3370
		private bool m_noRestriction;
	}
}
