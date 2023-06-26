using System;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;

namespace System.Net
{
	/// <summary>Manages the authentication modules called during the client authentication process.</summary>
	// Token: 0x020000C0 RID: 192
	public class AuthenticationManager
	{
		// Token: 0x06000653 RID: 1619 RVA: 0x00024262 File Offset: 0x00022462
		private AuthenticationManager()
		{
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0002426C File Offset: 0x0002246C
		private static IAuthenticationManager Instance
		{
			get
			{
				if (AuthenticationManager.internalInstance == null)
				{
					object obj = AuthenticationManager.instanceLock;
					lock (obj)
					{
						if (AuthenticationManager.internalInstance == null)
						{
							AuthenticationManager.internalInstance = AuthenticationManager.SelectAuthenticationManagerInstance();
						}
					}
				}
				return AuthenticationManager.internalInstance;
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000242C4 File Offset: 0x000224C4
		private static IAuthenticationManager SelectAuthenticationManagerInstance()
		{
			bool flag = false;
			try
			{
				if (RegistryConfiguration.GlobalConfigReadInt("System.Net.AuthenticationManager.HighPerformance", 0) == 1)
				{
					flag = true;
				}
				else if (RegistryConfiguration.AppConfigReadInt("System.Net.AuthenticationManager.HighPerformance", 0) == 1)
				{
					flag = true;
				}
				if (flag)
				{
					int? num = AuthenticationManager.ReadPrefixLookupMaxEntriesConfig();
					if (num != null)
					{
						int? num2 = num;
						int num3 = 0;
						if ((num2.GetValueOrDefault() > num3) & (num2 != null))
						{
							return new AuthenticationManager2(num.Value);
						}
					}
					return new AuthenticationManager2();
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
			}
			return new AuthenticationManagerDefault();
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00024374 File Offset: 0x00022574
		private static int? ReadPrefixLookupMaxEntriesConfig()
		{
			int? num = null;
			int num2 = RegistryConfiguration.GlobalConfigReadInt("System.Net.AuthenticationManager.PrefixLookupMaxCount", -1);
			if (num2 > 0)
			{
				num = new int?(num2);
			}
			num2 = RegistryConfiguration.AppConfigReadInt("System.Net.AuthenticationManager.PrefixLookupMaxCount", -1);
			if (num2 > 0)
			{
				num = new int?(num2);
			}
			return num;
		}

		/// <summary>Gets or sets the credential policy to be used for resource requests made using the <see cref="T:System.Net.HttpWebRequest" /> class.</summary>
		/// <returns>An object that implements the <see cref="T:System.Net.ICredentialPolicy" /> interface that determines whether credentials are sent with requests. The default value is <see langword="null" />.</returns>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000243BA File Offset: 0x000225BA
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x000243C6 File Offset: 0x000225C6
		public static ICredentialPolicy CredentialPolicy
		{
			get
			{
				return AuthenticationManager.Instance.CredentialPolicy;
			}
			set
			{
				ExceptionHelper.ControlPolicyPermission.Demand();
				AuthenticationManager.Instance.CredentialPolicy = value;
			}
		}

		/// <summary>Gets the dictionary that contains Service Principal Names (SPNs) that are used to identify hosts during Kerberos authentication for requests made using <see cref="T:System.Net.WebRequest" /> and its derived classes.</summary>
		/// <returns>A writable <see cref="T:System.Collections.Specialized.StringDictionary" /> that contains the SPN values for keys composed of host information.</returns>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x000243DD File Offset: 0x000225DD
		public static StringDictionary CustomTargetNameDictionary
		{
			get
			{
				return AuthenticationManager.Instance.CustomTargetNameDictionary;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x000243E9 File Offset: 0x000225E9
		internal static SpnDictionary SpnDictionary
		{
			get
			{
				return AuthenticationManager.Instance.SpnDictionary;
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000243F5 File Offset: 0x000225F5
		internal static void EnsureConfigLoaded()
		{
			AuthenticationManager.Instance.EnsureConfigLoaded();
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00024401 File Offset: 0x00022601
		internal static bool OSSupportsExtendedProtection
		{
			get
			{
				return AuthenticationManager.Instance.OSSupportsExtendedProtection;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0002440D File Offset: 0x0002260D
		internal static bool SspSupportsExtendedProtection
		{
			get
			{
				return AuthenticationManager.Instance.SspSupportsExtendedProtection;
			}
		}

		/// <summary>Calls each registered authentication module to find the first module that can respond to the authentication request.</summary>
		/// <param name="challenge">The challenge returned by the Internet resource.</param>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> that initiated the authentication challenge.</param>
		/// <param name="credentials">The <see cref="T:System.Net.ICredentials" /> associated with this request.</param>
		/// <returns>An instance of the <see cref="T:System.Net.Authorization" /> class containing the result of the authorization attempt. If there is no authentication module to respond to the challenge, this method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="challenge" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="request" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="credentials" /> is <see langword="null" />.</exception>
		// Token: 0x0600065E RID: 1630 RVA: 0x00024419 File Offset: 0x00022619
		public static Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials)
		{
			return AuthenticationManager.Instance.Authenticate(challenge, request, credentials);
		}

		/// <summary>Preauthenticates a request.</summary>
		/// <param name="request">A <see cref="T:System.Net.WebRequest" /> to an Internet resource.</param>
		/// <param name="credentials">The <see cref="T:System.Net.ICredentials" /> associated with the request.</param>
		/// <returns>An instance of the <see cref="T:System.Net.Authorization" /> class if the request can be preauthenticated; otherwise, <see langword="null" />. If <paramref name="credentials" /> is <see langword="null" />, this method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="request" /> is <see langword="null" />.</exception>
		// Token: 0x0600065F RID: 1631 RVA: 0x00024428 File Offset: 0x00022628
		public static Authorization PreAuthenticate(WebRequest request, ICredentials credentials)
		{
			return AuthenticationManager.Instance.PreAuthenticate(request, credentials);
		}

		/// <summary>Registers an authentication module with the authentication manager.</summary>
		/// <param name="authenticationModule">The <see cref="T:System.Net.IAuthenticationModule" /> to register with the authentication manager.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="authenticationModule" /> is <see langword="null" />.</exception>
		// Token: 0x06000660 RID: 1632 RVA: 0x00024436 File Offset: 0x00022636
		public static void Register(IAuthenticationModule authenticationModule)
		{
			ExceptionHelper.UnmanagedPermission.Demand();
			AuthenticationManager.Instance.Register(authenticationModule);
		}

		/// <summary>Removes the specified authentication module from the list of registered modules.</summary>
		/// <param name="authenticationModule">The <see cref="T:System.Net.IAuthenticationModule" /> to remove from the list of registered modules.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="authenticationModule" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.Net.IAuthenticationModule" /> is not registered.</exception>
		// Token: 0x06000661 RID: 1633 RVA: 0x0002444D File Offset: 0x0002264D
		public static void Unregister(IAuthenticationModule authenticationModule)
		{
			ExceptionHelper.UnmanagedPermission.Demand();
			AuthenticationManager.Instance.Unregister(authenticationModule);
		}

		/// <summary>Removes authentication modules with the specified authentication scheme from the list of registered modules.</summary>
		/// <param name="authenticationScheme">The authentication scheme of the module to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="authenticationScheme" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A module for this authentication scheme is not registered.</exception>
		// Token: 0x06000662 RID: 1634 RVA: 0x00024464 File Offset: 0x00022664
		public static void Unregister(string authenticationScheme)
		{
			ExceptionHelper.UnmanagedPermission.Demand();
			AuthenticationManager.Instance.Unregister(authenticationScheme);
		}

		/// <summary>Gets a list of authentication modules that are registered with the authentication manager.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enables the registered authentication modules to be read.</returns>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0002447B File Offset: 0x0002267B
		public static IEnumerator RegisteredModules
		{
			get
			{
				return AuthenticationManager.Instance.RegisteredModules;
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00024487 File Offset: 0x00022687
		internal static void BindModule(Uri uri, Authorization response, IAuthenticationModule module)
		{
			AuthenticationManager.Instance.BindModule(uri, response, module);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00024498 File Offset: 0x00022698
		internal static int FindSubstringNotInQuotes(string challenge, string signature)
		{
			int num = -1;
			if (challenge != null && signature != null && challenge.Length >= signature.Length)
			{
				int num2 = -1;
				int num3 = -1;
				int num4 = 0;
				while (num4 < challenge.Length && num < 0)
				{
					if (challenge[num4] == '"')
					{
						if (num2 <= num3)
						{
							num2 = num4;
						}
						else
						{
							num3 = num4;
						}
					}
					if (num4 == challenge.Length - 1 || (challenge[num4] == '"' && num2 > num3))
					{
						if (num4 == challenge.Length - 1)
						{
							num2 = challenge.Length;
						}
						if (num2 >= num3 + 3)
						{
							int num5 = num3 + 1;
							int num6 = num2 - num3 - 1;
							do
							{
								num = AuthenticationManager.IndexOf(challenge, signature, num5, num6);
								if (num >= 0)
								{
									if ((num == 0 || challenge[num - 1] == ' ' || challenge[num - 1] == ',') && (num + signature.Length == challenge.Length || challenge[num + signature.Length] == ' ' || challenge[num + signature.Length] == ','))
									{
										break;
									}
									num6 -= num - num5 + 1;
									num5 = num + 1;
								}
							}
							while (num >= 0);
						}
					}
					num4++;
				}
			}
			return num;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000245B8 File Offset: 0x000227B8
		private static int IndexOf(string challenge, string lwrCaseSignature, int start, int count)
		{
			count += start + 1 - lwrCaseSignature.Length;
			while (start < count)
			{
				int num = 0;
				while (num < lwrCaseSignature.Length && (challenge[start + num] | ' ') == lwrCaseSignature[num])
				{
					num++;
				}
				if (num == lwrCaseSignature.Length)
				{
					return start;
				}
				start++;
			}
			return -1;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00024610 File Offset: 0x00022810
		internal static int SplitNoQuotes(string challenge, ref int offset)
		{
			int num = offset;
			offset = -1;
			if (challenge != null && num < challenge.Length)
			{
				int num2 = -1;
				int num3 = -1;
				for (int i = num; i < challenge.Length; i++)
				{
					if (num2 > num3 && challenge[i] == '\\' && i + 1 < challenge.Length && challenge[i + 1] == '"')
					{
						i++;
					}
					else if (challenge[i] == '"')
					{
						if (num2 <= num3)
						{
							num2 = i;
						}
						else
						{
							num3 = i;
						}
					}
					else if (challenge[i] == '=' && num2 <= num3 && offset < 0)
					{
						offset = i;
					}
					else if (challenge[i] == ',' && num2 <= num3)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000246B9 File Offset: 0x000228B9
		internal static Authorization GetGroupAuthorization(IAuthenticationModule thisModule, string token, bool finished, NTAuthentication authSession, bool shareAuthenticatedConnections, bool mutualAuth)
		{
			return new Authorization(token, finished, shareAuthenticatedConnections ? null : (thisModule.GetType().FullName + "/" + authSession.UniqueUserId), mutualAuth);
		}

		// Token: 0x04000C6C RID: 3180
		private static object instanceLock = new object();

		// Token: 0x04000C6D RID: 3181
		private static IAuthenticationManager internalInstance = null;

		// Token: 0x04000C6E RID: 3182
		internal const string authenticationManagerRoot = "System.Net.AuthenticationManager";

		// Token: 0x04000C6F RID: 3183
		internal const string configHighPerformance = "System.Net.AuthenticationManager.HighPerformance";

		// Token: 0x04000C70 RID: 3184
		internal const string configPrefixLookupMaxCount = "System.Net.AuthenticationManager.PrefixLookupMaxCount";
	}
}
