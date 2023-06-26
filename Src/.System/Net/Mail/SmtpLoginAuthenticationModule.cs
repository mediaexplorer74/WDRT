using System;
using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x0200028D RID: 653
	internal class SmtpLoginAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x06001860 RID: 6240 RVA: 0x0007BFF3 File Offset: 0x0007A1F3
		internal SmtpLoginAuthenticationModule()
		{
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0007C008 File Offset: 0x0007A208
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
					NetworkCredential networkCredential = this.sessions[sessionCookie] as NetworkCredential;
					if (networkCredential == null)
					{
						if (credential == null || credential is SystemNetworkCredential)
						{
							authorization = null;
						}
						else
						{
							this.sessions[sessionCookie] = credential;
							string text = credential.UserName;
							string domain = credential.Domain;
							if (domain != null && domain.Length > 0)
							{
								text = domain + "\\" + text;
							}
							authorization = new Authorization(Convert.ToBase64String(Encoding.UTF8.GetBytes(text)), false);
						}
					}
					else
					{
						this.sessions.Remove(sessionCookie);
						authorization = new Authorization(Convert.ToBase64String(Encoding.UTF8.GetBytes(networkCredential.Password)), true);
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

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x0007C128 File Offset: 0x0007A328
		public string AuthenticationType
		{
			get
			{
				return "login";
			}
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0007C12F File Offset: 0x0007A32F
		public void CloseContext(object sessionCookie)
		{
		}

		// Token: 0x04001846 RID: 6214
		private Hashtable sessions = new Hashtable();
	}
}
