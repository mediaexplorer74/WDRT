using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x020001C8 RID: 456
	internal class NegotiateClient : ISessionAuthenticationModule, IAuthenticationModule
	{
		// Token: 0x06001214 RID: 4628 RVA: 0x000608A0 File Offset: 0x0005EAA0
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(challenge, webRequest, credentials, false);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000608AC File Offset: 0x0005EAAC
		private Authorization DoAuthenticate(string challenge, WebRequest webRequest, ICredentials credentials, bool preAuthenticate)
		{
			if (credentials == null)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			NTAuthentication ntauthentication = null;
			string text = null;
			bool flag = false;
			if (!preAuthenticate)
			{
				int num = NegotiateClient.GetSignatureIndex(challenge, out flag);
				if (num < 0)
				{
					return null;
				}
				int num2 = num + (flag ? "nego2".Length : "negotiate".Length);
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
				NetworkCredential credential = credentials.GetCredential(httpWebRequest.ChallengedUri, "negotiate");
				string empty = string.Empty;
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
				ntauthentication = new NTAuthentication("Negotiate", credential, computeSpn, httpWebRequest, channelBinding);
				httpWebRequest.CurrentAuthenticationState.SetSecurityContext(ntauthentication, this);
			}
			string outgoingBlob = ntauthentication.GetOutgoingBlob(text);
			if (outgoingBlob == null)
			{
				return null;
			}
			bool unsafeOrProxyAuthenticatedConnectionSharing = httpWebRequest.UnsafeOrProxyAuthenticatedConnectionSharing;
			if (unsafeOrProxyAuthenticatedConnectionSharing)
			{
				httpWebRequest.LockConnection = true;
			}
			httpWebRequest.NtlmKeepAlive = text == null && ntauthentication.IsValidContext && !ntauthentication.IsKerberos;
			return AuthenticationManager.GetGroupAuthorization(this, (flag ? "Nego2" : "Negotiate") + " " + outgoingBlob, ntauthentication.IsCompleted, ntauthentication, unsafeOrProxyAuthenticatedConnectionSharing, ntauthentication.IsKerberos);
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00060A88 File Offset: 0x0005EC88
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00060A8B File Offset: 0x0005EC8B
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(null, webRequest, credentials, true);
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00060A97 File Offset: 0x0005EC97
		public string AuthenticationType
		{
			get
			{
				return "Negotiate";
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00060AA0 File Offset: 0x0005ECA0
		public bool Update(string challenge, WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			NTAuthentication securityContext = httpWebRequest.CurrentAuthenticationState.GetSecurityContext(this);
			if (securityContext == null)
			{
				return true;
			}
			if (!securityContext.IsCompleted && httpWebRequest.CurrentAuthenticationState.StatusCodeMatch == httpWebRequest.ResponseStatusCode)
			{
				return false;
			}
			if (!httpWebRequest.UnsafeOrProxyAuthenticatedConnectionSharing)
			{
				httpWebRequest.ServicePoint.ReleaseConnectionGroup(httpWebRequest.GetConnectionGroupLine());
			}
			bool flag = true;
			int num = ((challenge == null) ? (-1) : NegotiateClient.GetSignatureIndex(challenge, out flag));
			if (num >= 0)
			{
				int num2 = num + (flag ? "nego2".Length : "negotiate".Length);
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

		// Token: 0x0600121A RID: 4634 RVA: 0x00060BAC File Offset: 0x0005EDAC
		public void ClearSession(WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			httpWebRequest.CurrentAuthenticationState.ClearSession();
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00060BCB File Offset: 0x0005EDCB
		public bool CanUseDefaultCredentials
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00060BD0 File Offset: 0x0005EDD0
		private static int GetSignatureIndex(string challenge, out bool useNego2)
		{
			useNego2 = true;
			int num = -1;
			if (ComNetOS.IsWin7orLater)
			{
				num = AuthenticationManager.FindSubstringNotInQuotes(challenge, "nego2");
			}
			if (num < 0)
			{
				useNego2 = false;
				num = AuthenticationManager.FindSubstringNotInQuotes(challenge, "negotiate");
			}
			return num;
		}

		// Token: 0x0400146C RID: 5228
		internal const string AuthType = "Negotiate";

		// Token: 0x0400146D RID: 5229
		private const string negotiateHeader = "Negotiate";

		// Token: 0x0400146E RID: 5230
		private const string negotiateSignature = "negotiate";

		// Token: 0x0400146F RID: 5231
		private const string nego2Header = "Nego2";

		// Token: 0x04001470 RID: 5232
		private const string nego2Signature = "nego2";
	}
}
