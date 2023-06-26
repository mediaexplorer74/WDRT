using System;
using System.Net.Security;

namespace System.Net
{
	// Token: 0x02000190 RID: 400
	internal class AuthenticationState
	{
		// Token: 0x06000F50 RID: 3920 RVA: 0x0004F5BE File Offset: 0x0004D7BE
		internal NTAuthentication GetSecurityContext(IAuthenticationModule module)
		{
			if (module != this.Module)
			{
				return null;
			}
			return this.SecurityContext;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0004F5D1 File Offset: 0x0004D7D1
		internal void SetSecurityContext(NTAuthentication securityContext, IAuthenticationModule module)
		{
			this.SecurityContext = securityContext;
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0004F5DA File Offset: 0x0004D7DA
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x0004F5E2 File Offset: 0x0004D7E2
		internal TransportContext TransportContext
		{
			get
			{
				return this._TransportContext;
			}
			set
			{
				this._TransportContext = value;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0004F5EB File Offset: 0x0004D7EB
		internal HttpResponseHeader AuthenticateHeader
		{
			get
			{
				if (!this.IsProxyAuth)
				{
					return HttpResponseHeader.WwwAuthenticate;
				}
				return HttpResponseHeader.ProxyAuthenticate;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0004F5FA File Offset: 0x0004D7FA
		internal string AuthorizationHeader
		{
			get
			{
				if (!this.IsProxyAuth)
				{
					return "Authorization";
				}
				return "Proxy-Authorization";
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0004F60F File Offset: 0x0004D80F
		internal HttpStatusCode StatusCodeMatch
		{
			get
			{
				if (!this.IsProxyAuth)
				{
					return HttpStatusCode.Unauthorized;
				}
				return HttpStatusCode.ProxyAuthenticationRequired;
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0004F624 File Offset: 0x0004D824
		internal AuthenticationState(bool isProxyAuth)
		{
			this.IsProxyAuth = isProxyAuth;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0004F634 File Offset: 0x0004D834
		private void PrepareState(HttpWebRequest httpWebRequest)
		{
			Uri uri = (this.IsProxyAuth ? httpWebRequest.ServicePoint.InternalAddress : httpWebRequest.GetRemoteResourceUri());
			if (this.ChallengedUri != uri)
			{
				if (this.ChallengedUri == null || this.ChallengedUri.Scheme != uri.Scheme || this.ChallengedUri.Host != uri.Host || this.ChallengedUri.Port != uri.Port)
				{
					this.ChallengedSpn = null;
				}
				this.ChallengedUri = uri;
			}
			httpWebRequest.CurrentAuthenticationState = this;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0004F6C4 File Offset: 0x0004D8C4
		internal SpnToken GetComputeSpn(HttpWebRequest httpWebRequest)
		{
			if (this.ChallengedSpn != null)
			{
				return this.ChallengedSpn;
			}
			bool flag = true;
			string text = httpWebRequest.ChallengedUri.GetParts(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.SafeUnescaped);
			SpnToken spnToken = AuthenticationManager.SpnDictionary.InternalGet(text);
			if (spnToken == null || spnToken.Spn == null)
			{
				string text2;
				if (!this.IsProxyAuth && (httpWebRequest.ServicePoint.InternalProxyServicePoint || httpWebRequest.UseCustomHost))
				{
					text2 = httpWebRequest.ChallengedUri.Host;
					if (httpWebRequest.ChallengedUri.HostNameType == UriHostNameType.IPv6 || httpWebRequest.ChallengedUri.HostNameType == UriHostNameType.IPv4 || text2.IndexOf('.') != -1)
					{
						goto IL_D1;
					}
					try
					{
						IPHostEntry iphostEntry;
						if (Dns.TryInternalResolve(text2, out iphostEntry))
						{
							text2 = iphostEntry.HostName;
							flag &= iphostEntry.isTrustedHost;
						}
						goto IL_D1;
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						goto IL_D1;
					}
				}
				text2 = httpWebRequest.ServicePoint.Hostname;
				flag &= httpWebRequest.ServicePoint.IsTrustedHost;
				IL_D1:
				string text3 = "HTTP/" + text2;
				text = httpWebRequest.ChallengedUri.GetParts(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + "/";
				spnToken = new SpnToken(text3, flag);
				AuthenticationManager.SpnDictionary.InternalSet(text, spnToken);
			}
			this.ChallengedSpn = spnToken;
			return this.ChallengedSpn;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0004F7FC File Offset: 0x0004D9FC
		internal void PreAuthIfNeeded(HttpWebRequest httpWebRequest, ICredentials authInfo)
		{
			if (!this.TriedPreAuth)
			{
				this.TriedPreAuth = true;
				if (authInfo != null)
				{
					this.PrepareState(httpWebRequest);
					try
					{
						Authorization authorization = AuthenticationManager.PreAuthenticate(httpWebRequest, authInfo);
						if (authorization != null && authorization.Message != null)
						{
							this.UniqueGroupId = authorization.ConnectionGroupId;
							httpWebRequest.Headers.Set(this.AuthorizationHeader, authorization.Message);
						}
					}
					catch (Exception ex)
					{
						this.ClearSession(httpWebRequest);
					}
				}
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0004F878 File Offset: 0x0004DA78
		internal bool AttemptAuthenticate(HttpWebRequest httpWebRequest, ICredentials authInfo)
		{
			if (this.Authorization != null && this.Authorization.Complete)
			{
				if (this.IsProxyAuth)
				{
					this.ClearAuthReq(httpWebRequest);
				}
				return false;
			}
			if (authInfo == null)
			{
				return false;
			}
			string text = httpWebRequest.AuthHeader(this.AuthenticateHeader);
			if (text == null)
			{
				if (!this.IsProxyAuth && this.Authorization != null && httpWebRequest.ProxyAuthenticationState.Authorization != null)
				{
					httpWebRequest.Headers.Set(this.AuthorizationHeader, this.Authorization.Message);
				}
				return false;
			}
			this.PrepareState(httpWebRequest);
			try
			{
				this.Authorization = AuthenticationManager.Authenticate(text, httpWebRequest, authInfo);
			}
			catch (Exception ex)
			{
				this.Authorization = null;
				this.ClearSession(httpWebRequest);
				throw;
			}
			if (this.Authorization == null)
			{
				return false;
			}
			if (this.Authorization.Message == null)
			{
				this.Authorization = null;
				return false;
			}
			this.UniqueGroupId = this.Authorization.ConnectionGroupId;
			try
			{
				httpWebRequest.Headers.Set(this.AuthorizationHeader, this.Authorization.Message);
			}
			catch
			{
				this.Authorization = null;
				this.ClearSession(httpWebRequest);
				throw;
			}
			return true;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0004F9A0 File Offset: 0x0004DBA0
		internal void ClearAuthReq(HttpWebRequest httpWebRequest)
		{
			this.TriedPreAuth = false;
			this.Authorization = null;
			this.UniqueGroupId = null;
			httpWebRequest.Headers.Remove(this.AuthorizationHeader);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0004F9C8 File Offset: 0x0004DBC8
		internal void Update(HttpWebRequest httpWebRequest)
		{
			if (this.Authorization != null)
			{
				this.PrepareState(httpWebRequest);
				ISessionAuthenticationModule sessionAuthenticationModule = this.Module as ISessionAuthenticationModule;
				if (sessionAuthenticationModule != null)
				{
					string text = httpWebRequest.AuthHeader(this.AuthenticateHeader);
					if (this.IsProxyAuth || httpWebRequest.ResponseStatusCode != HttpStatusCode.ProxyAuthenticationRequired)
					{
						bool flag = true;
						try
						{
							flag = sessionAuthenticationModule.Update(text, httpWebRequest);
						}
						catch (Exception ex)
						{
							this.ClearSession(httpWebRequest);
							if (httpWebRequest.AuthenticationLevel == AuthenticationLevel.MutualAuthRequired && (httpWebRequest.CurrentAuthenticationState == null || httpWebRequest.CurrentAuthenticationState.Authorization == null || !httpWebRequest.CurrentAuthenticationState.Authorization.MutuallyAuthenticated))
							{
								throw;
							}
						}
						this.Authorization.SetComplete(flag);
					}
				}
				if (httpWebRequest.PreAuthenticate && this.Module != null && this.Authorization.Complete && this.Module.CanPreAuthenticate && httpWebRequest.ResponseStatusCode != this.StatusCodeMatch)
				{
					AuthenticationManager.BindModule(this.ChallengedUri, this.Authorization, this.Module);
				}
			}
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0004FACC File Offset: 0x0004DCCC
		internal void ClearSession()
		{
			if (this.SecurityContext != null)
			{
				this.SecurityContext.CloseContext();
				this.SecurityContext = null;
			}
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0004FAE8 File Offset: 0x0004DCE8
		internal void ClearSession(HttpWebRequest httpWebRequest)
		{
			this.PrepareState(httpWebRequest);
			ISessionAuthenticationModule sessionAuthenticationModule = this.Module as ISessionAuthenticationModule;
			this.Module = null;
			if (sessionAuthenticationModule != null)
			{
				try
				{
					sessionAuthenticationModule.ClearSession(httpWebRequest);
				}
				catch (Exception ex)
				{
					if (NclUtilities.IsFatal(ex))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0400129A RID: 4762
		private bool TriedPreAuth;

		// Token: 0x0400129B RID: 4763
		internal Authorization Authorization;

		// Token: 0x0400129C RID: 4764
		internal IAuthenticationModule Module;

		// Token: 0x0400129D RID: 4765
		internal string UniqueGroupId;

		// Token: 0x0400129E RID: 4766
		private bool IsProxyAuth;

		// Token: 0x0400129F RID: 4767
		internal Uri ChallengedUri;

		// Token: 0x040012A0 RID: 4768
		private SpnToken ChallengedSpn;

		// Token: 0x040012A1 RID: 4769
		private NTAuthentication SecurityContext;

		// Token: 0x040012A2 RID: 4770
		private TransportContext _TransportContext;
	}
}
