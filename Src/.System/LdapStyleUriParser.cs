using System;

namespace System
{
	/// <summary>A customizable parser based on the Lightweight Directory Access Protocol (LDAP) scheme.</summary>
	// Token: 0x02000057 RID: 87
	public class LdapStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the Lightweight Directory Access Protocol (LDAP) scheme.</summary>
		// Token: 0x06000401 RID: 1025 RVA: 0x0001D490 File Offset: 0x0001B690
		public LdapStyleUriParser()
			: base(UriParser.LdapUri.Flags)
		{
		}
	}
}
