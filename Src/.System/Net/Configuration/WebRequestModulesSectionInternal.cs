using System;
using System.Collections;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x0200034D RID: 845
	internal sealed class WebRequestModulesSectionInternal
	{
		// Token: 0x06001E41 RID: 7745 RVA: 0x0008DACC File Offset: 0x0008BCCC
		internal WebRequestModulesSectionInternal(WebRequestModulesSection section)
		{
			if (section.WebRequestModules.Count > 0)
			{
				this.webRequestModules = new ArrayList(section.WebRequestModules.Count);
				foreach (object obj in section.WebRequestModules)
				{
					WebRequestModuleElement webRequestModuleElement = (WebRequestModuleElement)obj;
					try
					{
						this.webRequestModules.Add(new WebRequestPrefixElement(webRequestModuleElement.Prefix, webRequestModuleElement.Type));
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						throw new ConfigurationErrorsException(SR.GetString("net_config_webrequestmodules"), ex);
					}
				}
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0008DB94 File Offset: 0x0008BD94
		internal static object ClassSyncObject
		{
			get
			{
				if (WebRequestModulesSectionInternal.classSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref WebRequestModulesSectionInternal.classSyncObject, obj, null);
				}
				return WebRequestModulesSectionInternal.classSyncObject;
			}
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0008DBC0 File Offset: 0x0008BDC0
		internal static WebRequestModulesSectionInternal GetSection()
		{
			object obj = WebRequestModulesSectionInternal.ClassSyncObject;
			WebRequestModulesSectionInternal webRequestModulesSectionInternal;
			lock (obj)
			{
				WebRequestModulesSection webRequestModulesSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.WebRequestModulesSectionPath) as WebRequestModulesSection;
				if (webRequestModulesSection == null)
				{
					webRequestModulesSectionInternal = null;
				}
				else
				{
					webRequestModulesSectionInternal = new WebRequestModulesSectionInternal(webRequestModulesSection);
				}
			}
			return webRequestModulesSectionInternal;
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0008DC18 File Offset: 0x0008BE18
		internal ArrayList WebRequestModules
		{
			get
			{
				ArrayList arrayList = this.webRequestModules;
				if (arrayList == null)
				{
					arrayList = new ArrayList(0);
				}
				return arrayList;
			}
		}

		// Token: 0x04001CA8 RID: 7336
		private static object classSyncObject;

		// Token: 0x04001CA9 RID: 7337
		private ArrayList webRequestModules;
	}
}
