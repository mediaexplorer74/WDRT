using System;
using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x0200028F RID: 655
	internal class SmtpNtlmAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x06001869 RID: 6249 RVA: 0x0007C338 File Offset: 0x0007A538
		internal SmtpNtlmAuthenticationModule()
		{
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0007C34C File Offset: 0x0007A54C
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Authorization Authenticate(string challenge, NetworkCredential credential, object sessionCookie, string spn, ChannelBinding channelBindingToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "Authenticate", null);
			}
			Authorization authorization;
			try
			{
				Hashtable hashtable = this.sessions;
				lock (hashtable)
				{
					NTAuthentication ntauthentication = this.sessions[sessionCookie] as NTAuthentication;
					if (ntauthentication == null)
					{
						if (credential == null)
						{
							return null;
						}
						ntauthentication = (this.sessions[sessionCookie] = new NTAuthentication(false, "Ntlm", credential, spn, ContextFlags.Connection, channelBindingToken));
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
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "Authenticate", null);
				}
			}
			return authorization;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x0007C438 File Offset: 0x0007A638
		public string AuthenticationType
		{
			get
			{
				return "ntlm";
			}
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0007C43F File Offset: 0x0007A63F
		public void CloseContext(object sessionCookie)
		{
		}

		// Token: 0x04001848 RID: 6216
		private Hashtable sessions = new Hashtable();
	}
}
