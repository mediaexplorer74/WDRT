using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Provides the Web site from which a code assembly originates as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000368 RID: 872
	[ComVisible(true)]
	[Serializable]
	public sealed class Site : EvidenceBase, IIdentityPermissionFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Site" /> class with the website from which a code assembly originates.</summary>
		/// <param name="name">The website of origin for the associated code assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B4B RID: 11083 RVA: 0x000A2A87 File Offset: 0x000A0C87
		public Site(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_name = new SiteString(name);
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000A2AA9 File Offset: 0x000A0CA9
		private Site(SiteString name)
		{
			this.m_name = name;
		}

		/// <summary>Creates a new <see cref="T:System.Security.Policy.Site" /> object from the specified URL.</summary>
		/// <param name="url">The URL from which to create the new <see cref="T:System.Security.Policy.Site" /> object.</param>
		/// <returns>A new site object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter is not a valid URL.  
		///  -or-  
		///  The <paramref name="url" /> parameter is a file name.</exception>
		// Token: 0x06002B4D RID: 11085 RVA: 0x000A2AB8 File Offset: 0x000A0CB8
		public static Site CreateFromUrl(string url)
		{
			return new Site(Site.ParseSiteFromUrl(url));
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000A2AC8 File Offset: 0x000A0CC8
		private static SiteString ParseSiteFromUrl(string name)
		{
			URLString urlstring = new URLString(name);
			if (string.Compare(urlstring.Scheme, "file", StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
			}
			return new SiteString(new URLString(name).Host);
		}

		/// <summary>Gets the website from which the code assembly originates.</summary>
		/// <returns>The name of the website from which the code assembly originates.</returns>
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x000A2B0F File Offset: 0x000A0D0F
		public string Name
		{
			get
			{
				return this.m_name.ToString();
			}
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000A2B1C File Offset: 0x000A0D1C
		internal SiteString GetSiteString()
		{
			return this.m_name;
		}

		/// <summary>Creates an identity permission that corresponds to the current <see cref="T:System.Security.Policy.Site" /> object.</summary>
		/// <param name="evidence">The evidence from which to construct the identity permission.</param>
		/// <returns>A site identity permission for the current <see cref="T:System.Security.Policy.Site" /> object.</returns>
		// Token: 0x06002B51 RID: 11089 RVA: 0x000A2B24 File Offset: 0x000A0D24
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new SiteIdentityPermission(this.Name);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Site" /> to the specified object for equivalence.</summary>
		/// <param name="o">The object to test for equivalence with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the two instances of the <see cref="T:System.Security.Policy.Site" /> class are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B52 RID: 11090 RVA: 0x000A2B34 File Offset: 0x000A0D34
		public override bool Equals(object o)
		{
			Site site = o as Site;
			return site != null && string.Equals(this.Name, site.Name, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Returns the hash code of the current website name.</summary>
		/// <returns>The hash code of the current website name.</returns>
		// Token: 0x06002B53 RID: 11091 RVA: 0x000A2B5F File Offset: 0x000A0D5F
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06002B54 RID: 11092 RVA: 0x000A2B6C File Offset: 0x000A0D6C
		public override EvidenceBase Clone()
		{
			return new Site(this.m_name);
		}

		/// <summary>Creates an equivalent copy of the <see cref="T:System.Security.Policy.Site" /> object.</summary>
		/// <returns>A new object that is identical to the current <see cref="T:System.Security.Policy.Site" /> object.</returns>
		// Token: 0x06002B55 RID: 11093 RVA: 0x000A2B79 File Offset: 0x000A0D79
		public object Copy()
		{
			return this.Clone();
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000A2B84 File Offset: 0x000A0D84
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Site");
			securityElement.AddAttribute("version", "1");
			if (this.m_name != null)
			{
				securityElement.AddChild(new SecurityElement("Name", this.m_name.ToString()));
			}
			return securityElement;
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Site" /> object.</summary>
		/// <returns>A representation of the current site.</returns>
		// Token: 0x06002B57 RID: 11095 RVA: 0x000A2BD0 File Offset: 0x000A0DD0
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000A2BDD File Offset: 0x000A0DDD
		internal object Normalize()
		{
			return this.m_name.ToString().ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x040011A0 RID: 4512
		private SiteString m_name;
	}
}
