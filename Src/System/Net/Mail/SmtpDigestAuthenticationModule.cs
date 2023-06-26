using System;
using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x02000289 RID: 649
	internal class SmtpDigestAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x0600183B RID: 6203 RVA: 0x0007B973 File Offset: 0x00079B73
		internal SmtpDigestAuthenticationModule()
		{
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0007B988 File Offset: 0x00079B88
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Authorization Authenticate(string challenge, NetworkCredential credential, object sessionCookie, string spn, ChannelBinding channelBindingToken)
		{
			Hashtable hashtable = this.sessions;
			Authorization authorization;
			lock (hashtable)
			{
				NTAuthentication ntauthentication = this.sessions[sessionCookie] as NTAuthentication;
				if (ntauthentication == null)
				{
					if (credential == null)
					{
						return null;
					}
					ntauthentication = (this.sessions[sessionCookie] = new NTAuthentication(false, "WDigest", credential, spn, ContextFlags.Connection, channelBindingToken));
				}
				string outgoingBlob = ntauthentication.GetOutgoingBlob(challenge);
				if (!ntauthentication.IsCompleted)
				{
					authorization = new Authorization(outgoingBlob, false);
				}
				else
				{
					this.sessions.Remove(sessionCookie);
					authorization = new Authorization(outgoingBlob, true);
				}
			}
			return authorization;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x0007BA34 File Offset: 0x00079C34
		public string AuthenticationType
		{
			get
			{
				return "WDigest";
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0007BA3B File Offset: 0x00079C3B
		public void CloseContext(object sessionCookie)
		{
		}

		// Token: 0x04001841 RID: 6209
		private Hashtable sessions = new Hashtable();
	}
}
