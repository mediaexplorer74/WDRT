using System;

namespace System.Net.Sockets
{
	/// <summary>A value that enables restriction of an IPv6 socket to a specified scope, such as addresses with the same link local or site local prefix.</summary>
	// Token: 0x0200036C RID: 876
	public enum IPProtectionLevel
	{
		/// <summary>The IP protection level is unspecified. On Windows 7 and Windows Server 2008 R2, the default value for the IP Protection level on a socket is unspecified.</summary>
		// Token: 0x04001DC7 RID: 7623
		Unspecified = -1,
		/// <summary>The IP protection level is unrestricted. This value would be used by applications designed to operate across the Internet, including applications taking advantage of IPv6 NAT traversal capabilities built into Windows (Teredo, for example). These applications may bypass IPv4 firewalls, so applications must be hardened against Internet attacks directed at the opened port. On Windows Server 2008 R2 and Windows Vista, the default value for the IP Protection level on a socket is unrestricted.</summary>
		// Token: 0x04001DC8 RID: 7624
		Unrestricted = 10,
		/// <summary>The IP protection level is edge restricted. This value would be used by applications designed to operate across the Internet. This setting does not allow Network Address Translation (NAT) traversal using the Windows Teredo implementation. These applications may bypass IPv4 firewalls, so applications must be hardened against Internet attacks directed at the opened port. On Windows Server 2003 and Windows XP, the default value for the IP Protection level on a socket is edge restricted.</summary>
		// Token: 0x04001DC9 RID: 7625
		EdgeRestricted = 20,
		/// <summary>The IP protection level is restricted. This value would be used by intranet applications that do not implement Internet scenarios. These applications are generally not tested or hardened against Internet-style attacks. This setting will limit the received traffic to link-local only.</summary>
		// Token: 0x04001DCA RID: 7626
		Restricted = 30
	}
}
