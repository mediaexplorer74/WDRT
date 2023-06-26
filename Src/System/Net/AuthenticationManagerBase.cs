using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x020000C1 RID: 193
	internal abstract class AuthenticationManagerBase : IAuthenticationManager
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x000246F8 File Offset: 0x000228F8
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x00024701 File Offset: 0x00022901
		public ICredentialPolicy CredentialPolicy
		{
			get
			{
				return AuthenticationManagerBase.s_ICredentialPolicy;
			}
			set
			{
				AuthenticationManagerBase.s_ICredentialPolicy = value;
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0002470B File Offset: 0x0002290B
		public virtual void EnsureConfigLoaded()
		{
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0002470D File Offset: 0x0002290D
		public StringDictionary CustomTargetNameDictionary
		{
			get
			{
				return AuthenticationManagerBase.m_SpnDictionary;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00024714 File Offset: 0x00022914
		public SpnDictionary SpnDictionary
		{
			get
			{
				return AuthenticationManagerBase.m_SpnDictionary;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0002471C File Offset: 0x0002291C
		public bool OSSupportsExtendedProtection
		{
			get
			{
				if (AuthenticationManagerBase.s_OSSupportsExtendedProtection == TriState.Unspecified)
				{
					if (ComNetOS.IsWin7orLater)
					{
						AuthenticationManagerBase.s_OSSupportsExtendedProtection = TriState.True;
					}
					else if (this.SspSupportsExtendedProtection)
					{
						if (UnsafeNclNativeMethods.HttpApi.ExtendedProtectionSupported)
						{
							AuthenticationManagerBase.s_OSSupportsExtendedProtection = TriState.True;
						}
						else
						{
							AuthenticationManagerBase.s_OSSupportsExtendedProtection = TriState.False;
						}
					}
					else
					{
						AuthenticationManagerBase.s_OSSupportsExtendedProtection = TriState.False;
					}
				}
				return AuthenticationManagerBase.s_OSSupportsExtendedProtection == TriState.True;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00024770 File Offset: 0x00022970
		public bool SspSupportsExtendedProtection
		{
			get
			{
				if (AuthenticationManagerBase.s_SspSupportsExtendedProtection == TriState.Unspecified)
				{
					if (ComNetOS.IsWin7orLater)
					{
						AuthenticationManagerBase.s_SspSupportsExtendedProtection = TriState.True;
					}
					else
					{
						ContextFlags contextFlags = ContextFlags.Connection | ContextFlags.AcceptIntegrity;
						NTAuthentication ntauthentication = new NTAuthentication(false, "NTLM", SystemNetworkCredential.defaultCredential, "http/localhost", contextFlags, null);
						try
						{
							NTAuthentication ntauthentication2 = new NTAuthentication(true, "NTLM", SystemNetworkCredential.defaultCredential, null, ContextFlags.Connection, null);
							try
							{
								byte[] array = null;
								while (!ntauthentication2.IsCompleted)
								{
									SecurityStatus securityStatus;
									array = ntauthentication.GetOutgoingBlob(array, true, out securityStatus);
									array = ntauthentication2.GetOutgoingBlob(array, true, out securityStatus);
								}
								if (ntauthentication2.OSSupportsExtendedProtection)
								{
									AuthenticationManagerBase.s_SspSupportsExtendedProtection = TriState.True;
								}
								else
								{
									if (Logging.On)
									{
										Logging.PrintWarning(Logging.Web, SR.GetString("net_ssp_dont_support_cbt"));
									}
									AuthenticationManagerBase.s_SspSupportsExtendedProtection = TriState.False;
								}
							}
							finally
							{
								ntauthentication2.CloseContext();
							}
						}
						finally
						{
							ntauthentication.CloseContext();
						}
					}
				}
				return AuthenticationManagerBase.s_SspSupportsExtendedProtection == TriState.True;
			}
		}

		// Token: 0x06000671 RID: 1649
		public abstract Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials);

		// Token: 0x06000672 RID: 1650
		public abstract Authorization PreAuthenticate(WebRequest request, ICredentials credentials);

		// Token: 0x06000673 RID: 1651
		public abstract void Register(IAuthenticationModule authenticationModule);

		// Token: 0x06000674 RID: 1652
		public abstract void Unregister(IAuthenticationModule authenticationModule);

		// Token: 0x06000675 RID: 1653
		public abstract void Unregister(string authenticationScheme);

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000676 RID: 1654
		public abstract IEnumerator RegisteredModules { get; }

		// Token: 0x06000677 RID: 1655
		public abstract void BindModule(Uri uri, Authorization response, IAuthenticationModule module);

		// Token: 0x06000678 RID: 1656 RVA: 0x0002485C File Offset: 0x00022A5C
		protected static string generalize(Uri location)
		{
			string components = location.GetComponents(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);
			int num = components.LastIndexOf('/');
			if (num < 0)
			{
				return components;
			}
			return components.Substring(0, num + 1);
		}

		// Token: 0x04000C71 RID: 3185
		private static volatile ICredentialPolicy s_ICredentialPolicy;

		// Token: 0x04000C72 RID: 3186
		private static SpnDictionary m_SpnDictionary = new SpnDictionary();

		// Token: 0x04000C73 RID: 3187
		private static TriState s_OSSupportsExtendedProtection = TriState.Unspecified;

		// Token: 0x04000C74 RID: 3188
		private static TriState s_SspSupportsExtendedProtection = TriState.Unspecified;
	}
}
