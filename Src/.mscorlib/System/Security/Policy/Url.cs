using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Provides the URL from which a code assembly originates as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200036D RID: 877
	[ComVisible(true)]
	[Serializable]
	public sealed class Url : EvidenceBase, IIdentityPermissionFactory
	{
		// Token: 0x06002B99 RID: 11161 RVA: 0x000A3C86 File Offset: 0x000A1E86
		internal Url(string name, bool parsed)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_url = new URLString(name, parsed);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Url" /> class with the URL from which a code assembly originates.</summary>
		/// <param name="name">The URL of origin for the associated code assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B9A RID: 11162 RVA: 0x000A3CA9 File Offset: 0x000A1EA9
		public Url(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_url = new URLString(name);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000A3CCB File Offset: 0x000A1ECB
		private Url(Url url)
		{
			this.m_url = url.m_url;
		}

		/// <summary>Gets the URL from which the code assembly originates.</summary>
		/// <returns>The URL from which the code assembly originates.</returns>
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x000A3CDF File Offset: 0x000A1EDF
		public string Value
		{
			get
			{
				return this.m_url.ToString();
			}
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000A3CEC File Offset: 0x000A1EEC
		internal URLString GetURLString()
		{
			return this.m_url;
		}

		/// <summary>Creates an identity permission corresponding to the current instance of the <see cref="T:System.Security.Policy.Url" /> evidence class.</summary>
		/// <param name="evidence">The evidence set from which to construct the identity permission.</param>
		/// <returns>A <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Url" /> evidence.</returns>
		// Token: 0x06002B9E RID: 11166 RVA: 0x000A3CF4 File Offset: 0x000A1EF4
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new UrlIdentityPermission(this.m_url);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Url" /> evidence object to the specified object for equivalence.</summary>
		/// <param name="o">The <see cref="T:System.Security.Policy.Url" /> evidence object to test for equivalence with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Security.Policy.Url" /> objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B9F RID: 11167 RVA: 0x000A3D04 File Offset: 0x000A1F04
		public override bool Equals(object o)
		{
			Url url = o as Url;
			return url != null && url.m_url.Equals(this.m_url);
		}

		/// <summary>Gets the hash code of the current URL.</summary>
		/// <returns>The hash code of the current URL.</returns>
		// Token: 0x06002BA0 RID: 11168 RVA: 0x000A3D2E File Offset: 0x000A1F2E
		public override int GetHashCode()
		{
			return this.m_url.GetHashCode();
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06002BA1 RID: 11169 RVA: 0x000A3D3B File Offset: 0x000A1F3B
		public override EvidenceBase Clone()
		{
			return new Url(this);
		}

		/// <summary>Creates a new copy of the evidence object.</summary>
		/// <returns>A new, identical copy of the evidence object.</returns>
		// Token: 0x06002BA2 RID: 11170 RVA: 0x000A3D43 File Offset: 0x000A1F43
		public object Copy()
		{
			return this.Clone();
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000A3D4C File Offset: 0x000A1F4C
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Url");
			securityElement.AddAttribute("version", "1");
			if (this.m_url != null)
			{
				securityElement.AddChild(new SecurityElement("Url", this.m_url.ToString()));
			}
			return securityElement;
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Url" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Url" />.</returns>
		// Token: 0x06002BA4 RID: 11172 RVA: 0x000A3D98 File Offset: 0x000A1F98
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000A3DA5 File Offset: 0x000A1FA5
		internal object Normalize()
		{
			return this.m_url.NormalizeUrl();
		}

		// Token: 0x040011AF RID: 4527
		private URLString m_url;
	}
}
