using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x0200028E RID: 654
	internal class SmtpNegotiateAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x06001864 RID: 6244 RVA: 0x0007C131 File Offset: 0x0007A331
		internal SmtpNegotiateAuthenticationModule()
		{
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0007C144 File Offset: 0x0007A344
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
						ntauthentication = (this.sessions[sessionCookie] = new NTAuthentication(false, "Negotiate", credential, spn, ContextFlags.Connection | ContextFlags.AcceptStream, channelBindingToken));
					}
					string text = null;
					if (!ntauthentication.IsCompleted)
					{
						byte[] array = null;
						if (challenge != null)
						{
							array = Convert.FromBase64String(challenge);
						}
						SecurityStatus securityStatus;
						byte[] outgoingBlob = ntauthentication.GetOutgoingBlob(array, false, out securityStatus);
						if (outgoingBlob != null)
						{
							text = Convert.ToBase64String(outgoingBlob);
						}
					}
					else
					{
						text = this.GetSecurityLayerOutgoingBlob(challenge, ntauthentication);
					}
					authorization = new Authorization(text, ntauthentication.IsCompleted);
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

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0007C24C File Offset: 0x0007A44C
		public string AuthenticationType
		{
			get
			{
				return "gssapi";
			}
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0007C254 File Offset: 0x0007A454
		public void CloseContext(object sessionCookie)
		{
			NTAuthentication ntauthentication = null;
			Hashtable hashtable = this.sessions;
			lock (hashtable)
			{
				ntauthentication = this.sessions[sessionCookie] as NTAuthentication;
				if (ntauthentication != null)
				{
					this.sessions.Remove(sessionCookie);
				}
			}
			if (ntauthentication != null)
			{
				ntauthentication.CloseContext();
			}
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0007C2BC File Offset: 0x0007A4BC
		private string GetSecurityLayerOutgoingBlob(string challenge, NTAuthentication clientContext)
		{
			if (challenge == null)
			{
				return null;
			}
			byte[] array = Convert.FromBase64String(challenge);
			int num;
			try
			{
				num = clientContext.VerifySignature(array, 0, array.Length);
			}
			catch (Win32Exception)
			{
				return null;
			}
			if (num < 4 || (array[0] & 1) != 1)
			{
				return null;
			}
			array[0] = 1;
			byte[] array2 = null;
			try
			{
				num = clientContext.MakeSignature(array, 0, 4, ref array2);
			}
			catch (Win32Exception)
			{
				return null;
			}
			return Convert.ToBase64String(array2, 0, num);
		}

		// Token: 0x04001847 RID: 6215
		private Hashtable sessions = new Hashtable();
	}
}
