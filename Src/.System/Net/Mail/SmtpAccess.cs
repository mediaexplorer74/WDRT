using System;

namespace System.Net.Mail
{
	/// <summary>Specifies the level of access allowed to a Simple Mail Transport Protocol (SMTP) server.</summary>
	// Token: 0x02000290 RID: 656
	public enum SmtpAccess
	{
		/// <summary>No access to an SMTP host.</summary>
		// Token: 0x0400184A RID: 6218
		None,
		/// <summary>Connection to an SMTP host on the default port (port 25).</summary>
		// Token: 0x0400184B RID: 6219
		Connect,
		/// <summary>Connection to an SMTP host on any port.</summary>
		// Token: 0x0400184C RID: 6220
		ConnectToUnrestrictedPort
	}
}
