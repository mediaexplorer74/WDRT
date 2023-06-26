using System;
using System.Globalization;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x020001BB RID: 443
	internal class KerberosClient : ISessionAuthenticationModule, IAuthenticationModule
	{
		// Token: 0x0600113D RID: 4413 RVA: 0x0005E139 File Offset: 0x0005C339
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(challenge, webRequest, credentials, false);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0005E148 File Offset: 0x0005C348
		private Authorization DoAuthenticate(string challenge, WebRequest webRequest, ICredentials credentials, bool preAuthenticate)
		{
			if (credentials == null)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			NTAuthentication ntauthentication = null;
			string text = null;
			if (!preAuthenticate)
			{
				int num = AuthenticationManager.FindSubstringNotInQuotes(challenge, KerberosClient.Signature);
				if (num < 0)
				{
					return null;
				}
				int num2 = num + KerberosClient.SignatureSize;
				if (challenge.Length > num2 && challenge[num2] != ',')
				{
					num2++;
				}
				else
				{
					num = -1;
				}
				if (num >= 0 && challenge.Length > num2)
				{
					num = challenge.IndexOf(',', num2);
					if (num != -1)
					{
						text = challenge.Substring(num2, num - num2);
					}
					else
					{
						text = challenge.Substring(num2);
					}
				}
				ntauthentication = httpWebRequest.CurrentAuthenticationState.GetSecurityContext(this);
			}
			if (ntauthentication == null)
			{
				NetworkCredential credential = credentials.GetCredential(httpWebRequest.ChallengedUri, KerberosClient.Signature);
				if (credential == null || (!(credential is SystemNetworkCredential) && credential.InternalGetUserName().Length == 0))
				{
					return null;
				}
				ICredentialPolicy credentialPolicy = AuthenticationManager.CredentialPolicy;
				if (credentialPolicy != null && !credentialPolicy.ShouldSendCredential(httpWebRequest.ChallengedUri, httpWebRequest, credential, this))
				{
					return null;
				}
				SpnToken computeSpn = httpWebRequest.CurrentAuthenticationState.GetComputeSpn(httpWebRequest);
				ChannelBinding channelBinding = null;
				if (httpWebRequest.CurrentAuthenticationState.TransportContext != null)
				{
					channelBinding = httpWebRequest.CurrentAuthenticationState.TransportContext.GetChannelBinding(ChannelBindingKind.Endpoint);
				}
				ntauthentication = new NTAuthentication("Kerberos", credential, computeSpn, httpWebRequest, channelBinding);
				httpWebRequest.CurrentAuthenticationState.SetSecurityContext(ntauthentication, this);
			}
			string outgoingBlob = ntauthentication.GetOutgoingBlob(text);
			if (outgoingBlob == null)
			{
				return null;
			}
			return new Authorization("Kerberos " + outgoingBlob, ntauthentication.IsCompleted, string.Empty, ntauthentication.IsMutualAuthFlag);
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0005E2C6 File Offset: 0x0005C4C6
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0005E2C9 File Offset: 0x0005C4C9
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(null, webRequest, credentials, true);
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0005E2D5 File Offset: 0x0005C4D5
		public string AuthenticationType
		{
			get
			{
				return "Kerberos";
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0005E2DC File Offset: 0x0005C4DC
		public bool Update(string challenge, WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			NTAuthentication securityContext = httpWebRequest.CurrentAuthenticationState.GetSecurityContext(this);
			if (securityContext == null)
			{
				return true;
			}
			if (httpWebRequest.CurrentAuthenticationState.StatusCodeMatch == httpWebRequest.ResponseStatusCode)
			{
				return false;
			}
			int num = ((challenge == null) ? (-1) : AuthenticationManager.FindSubstringNotInQuotes(challenge, KerberosClient.Signature));
			if (num >= 0)
			{
				int num2 = num + KerberosClient.SignatureSize;
				string text = null;
				if (challenge.Length > num2 && challenge[num2] != ',')
				{
					num2++;
				}
				else
				{
					num = -1;
				}
				if (num >= 0 && challenge.Length > num2)
				{
					text = challenge.Substring(num2);
				}
				string outgoingBlob = securityContext.GetOutgoingBlob(text);
				httpWebRequest.CurrentAuthenticationState.Authorization.MutuallyAuthenticated = securityContext.IsMutualAuthFlag;
			}
			httpWebRequest.ServicePoint.SetCachedChannelBinding(httpWebRequest.ChallengedUri, securityContext.ChannelBinding);
			this.ClearSession(httpWebRequest);
			return true;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0005E3AC File Offset: 0x0005C5AC
		public void ClearSession(WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			httpWebRequest.CurrentAuthenticationState.ClearSession();
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0005E3CB File Offset: 0x0005C5CB
		public bool CanUseDefaultCredentials
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001435 RID: 5173
		internal const string AuthType = "Kerberos";

		// Token: 0x04001436 RID: 5174
		internal static string Signature = "Kerberos".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x04001437 RID: 5175
		internal static int SignatureSize = KerberosClient.Signature.Length;
	}
}
