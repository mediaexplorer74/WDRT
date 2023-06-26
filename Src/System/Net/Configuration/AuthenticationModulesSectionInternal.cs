using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000327 RID: 807
	internal sealed class AuthenticationModulesSectionInternal
	{
		// Token: 0x06001CE6 RID: 7398 RVA: 0x0008A5F8 File Offset: 0x000887F8
		internal AuthenticationModulesSectionInternal(AuthenticationModulesSection section)
		{
			if (section.AuthenticationModules.Count > 0)
			{
				this.authenticationModules = new List<Type>(section.AuthenticationModules.Count);
				foreach (object obj in section.AuthenticationModules)
				{
					AuthenticationModuleElement authenticationModuleElement = (AuthenticationModuleElement)obj;
					Type type = null;
					try
					{
						type = Type.GetType(authenticationModuleElement.Type, true, true);
						if (!typeof(IAuthenticationModule).IsAssignableFrom(type))
						{
							throw new InvalidCastException(SR.GetString("net_invalid_cast", new object[] { type.FullName, "IAuthenticationModule" }));
						}
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						throw new ConfigurationErrorsException(SR.GetString("net_config_authenticationmodules"), ex);
					}
					this.authenticationModules.Add(type);
				}
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x0008A6FC File Offset: 0x000888FC
		internal List<Type> AuthenticationModules
		{
			get
			{
				List<Type> list = this.authenticationModules;
				if (list == null)
				{
					list = new List<Type>(0);
				}
				return list;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x0008A71C File Offset: 0x0008891C
		internal static object ClassSyncObject
		{
			get
			{
				if (AuthenticationModulesSectionInternal.classSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref AuthenticationModulesSectionInternal.classSyncObject, obj, null);
				}
				return AuthenticationModulesSectionInternal.classSyncObject;
			}
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0008A748 File Offset: 0x00088948
		internal static AuthenticationModulesSectionInternal GetSection()
		{
			object obj = AuthenticationModulesSectionInternal.ClassSyncObject;
			AuthenticationModulesSectionInternal authenticationModulesSectionInternal;
			lock (obj)
			{
				AuthenticationModulesSection authenticationModulesSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.AuthenticationModulesSectionPath) as AuthenticationModulesSection;
				if (authenticationModulesSection == null)
				{
					authenticationModulesSectionInternal = null;
				}
				else
				{
					authenticationModulesSectionInternal = new AuthenticationModulesSectionInternal(authenticationModulesSection);
				}
			}
			return authenticationModulesSectionInternal;
		}

		// Token: 0x04001BB0 RID: 7088
		private List<Type> authenticationModules;

		// Token: 0x04001BB1 RID: 7089
		private static object classSyncObject;
	}
}
