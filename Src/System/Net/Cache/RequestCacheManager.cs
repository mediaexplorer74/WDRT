using System;
using System.Net.Configuration;

namespace System.Net.Cache
{
	// Token: 0x0200030F RID: 783
	internal sealed class RequestCacheManager
	{
		// Token: 0x06001BFB RID: 7163 RVA: 0x000857D4 File Offset: 0x000839D4
		private RequestCacheManager()
		{
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x000857DC File Offset: 0x000839DC
		internal static RequestCacheBinding GetBinding(string internedScheme)
		{
			if (internedScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return RequestCacheManager.s_BypassCacheBinding;
			}
			if (internedScheme.Length == 0)
			{
				return RequestCacheManager.s_DefaultGlobalBinding;
			}
			if (internedScheme == Uri.UriSchemeHttp || internedScheme == Uri.UriSchemeHttps)
			{
				return RequestCacheManager.s_DefaultHttpBinding;
			}
			if (internedScheme == Uri.UriSchemeFtp)
			{
				return RequestCacheManager.s_DefaultFtpBinding;
			}
			return RequestCacheManager.s_BypassCacheBinding;
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00085856 File Offset: 0x00083A56
		internal static bool IsCachingEnabled
		{
			get
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCacheManager.LoadConfigSettings();
				}
				return !RequestCacheManager.s_CacheConfigSettings.DisableAllCaching;
			}
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00085878 File Offset: 0x00083A78
		internal static void SetBinding(string uriScheme, RequestCacheBinding binding)
		{
			if (uriScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return;
			}
			if (uriScheme.Length == 0)
			{
				RequestCacheManager.s_DefaultGlobalBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeHttp || uriScheme == Uri.UriSchemeHttps)
			{
				RequestCacheManager.s_DefaultHttpBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeFtp)
			{
				RequestCacheManager.s_DefaultFtpBinding = binding;
			}
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000858FC File Offset: 0x00083AFC
		private static void LoadConfigSettings()
		{
			RequestCacheBinding requestCacheBinding = RequestCacheManager.s_BypassCacheBinding;
			lock (requestCacheBinding)
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCachingSectionInternal section = RequestCachingSectionInternal.GetSection();
					RequestCacheManager.s_DefaultGlobalBinding = new RequestCacheBinding(section.DefaultCache, section.DefaultHttpValidator, section.DefaultCachePolicy);
					RequestCacheManager.s_DefaultHttpBinding = new RequestCacheBinding(section.DefaultCache, section.DefaultHttpValidator, section.DefaultHttpCachePolicy);
					RequestCacheManager.s_DefaultFtpBinding = new RequestCacheBinding(section.DefaultCache, section.DefaultFtpValidator, section.DefaultFtpCachePolicy);
					RequestCacheManager.s_CacheConfigSettings = section;
				}
			}
		}

		// Token: 0x04001B35 RID: 6965
		private static volatile RequestCachingSectionInternal s_CacheConfigSettings;

		// Token: 0x04001B36 RID: 6966
		private static readonly RequestCacheBinding s_BypassCacheBinding = new RequestCacheBinding(null, null, new RequestCachePolicy(RequestCacheLevel.BypassCache));

		// Token: 0x04001B37 RID: 6967
		private static volatile RequestCacheBinding s_DefaultGlobalBinding;

		// Token: 0x04001B38 RID: 6968
		private static volatile RequestCacheBinding s_DefaultHttpBinding;

		// Token: 0x04001B39 RID: 6969
		private static volatile RequestCacheBinding s_DefaultFtpBinding;
	}
}
