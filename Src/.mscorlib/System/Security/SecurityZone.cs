using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Defines the integer values corresponding to security zones used by security policy.</summary>
	// Token: 0x020001F5 RID: 501
	[ComVisible(true)]
	[Serializable]
	public enum SecurityZone
	{
		/// <summary>The local computer zone is an implicit zone used for content that exists on the user's computer.</summary>
		// Token: 0x04000A96 RID: 2710
		MyComputer,
		/// <summary>The local intranet zone is used for content located on a company's intranet. Because the servers and information would be within a company's firewall, a user or company could assign a higher trust level to the content on the intranet.</summary>
		// Token: 0x04000A97 RID: 2711
		Intranet,
		/// <summary>The trusted sites zone is used for content located on Web sites considered more reputable or trustworthy than other sites on the Internet. Users can use this zone to assign a higher trust level to these sites to minimize the number of authentication requests. The URLs of these trusted Web sites need to be mapped into this zone by the user.</summary>
		// Token: 0x04000A98 RID: 2712
		Trusted,
		/// <summary>The Internet zone is used for the Web sites on the Internet that do not belong to another zone.</summary>
		// Token: 0x04000A99 RID: 2713
		Internet,
		/// <summary>The restricted sites zone is used for Web sites with content that could cause, or could have caused, problems when downloaded. The URLs of these untrusted Web sites need to be mapped into this zone by the user.</summary>
		// Token: 0x04000A9A RID: 2714
		Untrusted,
		/// <summary>No zone is specified.</summary>
		// Token: 0x04000A9B RID: 2715
		NoZone = -1
	}
}
