using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net.Mail
{
	// Token: 0x02000267 RID: 615
	internal interface ISmtpAuthenticationModule
	{
		// Token: 0x06001712 RID: 5906
		Authorization Authenticate(string challenge, NetworkCredential credentials, object sessionCookie, string spn, ChannelBinding channelBindingToken);

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001713 RID: 5907
		string AuthenticationType { get; }

		// Token: 0x06001714 RID: 5908
		void CloseContext(object sessionCookie);
	}
}
