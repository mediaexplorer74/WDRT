using System;
using System.Configuration;
using System.Net.Cache;
using System.Threading;
using Microsoft.Win32;

namespace System.Net.Configuration
{
	// Token: 0x0200033E RID: 830
	internal sealed class RequestCachingSectionInternal
	{
		// Token: 0x06001D95 RID: 7573 RVA: 0x0008C1AC File Offset: 0x0008A3AC
		private RequestCachingSectionInternal()
		{
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0008C1B4 File Offset: 0x0008A3B4
		internal RequestCachingSectionInternal(RequestCachingSection section)
		{
			if (!section.DisableAllCaching)
			{
				this.defaultCachePolicy = new RequestCachePolicy(section.DefaultPolicyLevel);
				this.isPrivateCache = section.IsPrivateCache;
				this.unspecifiedMaximumAge = section.UnspecifiedMaximumAge;
			}
			else
			{
				this.disableAllCaching = true;
			}
			this.httpRequestCacheValidator = new HttpRequestCacheValidator(false, this.UnspecifiedMaximumAge);
			this.ftpRequestCacheValidator = new FtpRequestCacheValidator(false, this.UnspecifiedMaximumAge);
			this.defaultCache = new WinInetCache(this.IsPrivateCache, true, true);
			if (section.DisableAllCaching)
			{
				return;
			}
			HttpCachePolicyElement httpCachePolicyElement = section.DefaultHttpCachePolicy;
			if (httpCachePolicyElement.WasReadFromConfig)
			{
				if (httpCachePolicyElement.PolicyLevel == HttpRequestCacheLevel.Default)
				{
					HttpCacheAgeControl httpCacheAgeControl = ((httpCachePolicyElement.MinimumFresh != TimeSpan.MinValue) ? HttpCacheAgeControl.MaxAgeAndMinFresh : HttpCacheAgeControl.MaxAgeAndMaxStale);
					this.defaultHttpCachePolicy = new HttpRequestCachePolicy(httpCacheAgeControl, httpCachePolicyElement.MaximumAge, (httpCachePolicyElement.MinimumFresh != TimeSpan.MinValue) ? httpCachePolicyElement.MinimumFresh : httpCachePolicyElement.MaximumStale);
				}
				else
				{
					this.defaultHttpCachePolicy = new HttpRequestCachePolicy(httpCachePolicyElement.PolicyLevel);
				}
			}
			FtpCachePolicyElement ftpCachePolicyElement = section.DefaultFtpCachePolicy;
			if (ftpCachePolicyElement.WasReadFromConfig)
			{
				this.defaultFtpCachePolicy = new RequestCachePolicy(ftpCachePolicyElement.PolicyLevel);
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0008C2D4 File Offset: 0x0008A4D4
		internal static object ClassSyncObject
		{
			get
			{
				if (RequestCachingSectionInternal.classSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref RequestCachingSectionInternal.classSyncObject, obj, null);
				}
				return RequestCachingSectionInternal.classSyncObject;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x0008C300 File Offset: 0x0008A500
		internal bool DisableAllCaching
		{
			get
			{
				return this.disableAllCaching;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x0008C308 File Offset: 0x0008A508
		internal RequestCache DefaultCache
		{
			get
			{
				return this.defaultCache;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0008C310 File Offset: 0x0008A510
		internal RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				return this.defaultCachePolicy;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x0008C318 File Offset: 0x0008A518
		internal bool IsPrivateCache
		{
			get
			{
				return this.isPrivateCache;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0008C320 File Offset: 0x0008A520
		internal TimeSpan UnspecifiedMaximumAge
		{
			get
			{
				return this.unspecifiedMaximumAge;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0008C328 File Offset: 0x0008A528
		internal HttpRequestCachePolicy DefaultHttpCachePolicy
		{
			get
			{
				return this.defaultHttpCachePolicy;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0008C330 File Offset: 0x0008A530
		internal RequestCachePolicy DefaultFtpCachePolicy
		{
			get
			{
				return this.defaultFtpCachePolicy;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x0008C338 File Offset: 0x0008A538
		internal HttpRequestCacheValidator DefaultHttpValidator
		{
			get
			{
				return this.httpRequestCacheValidator;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x0008C340 File Offset: 0x0008A540
		internal FtpRequestCacheValidator DefaultFtpValidator
		{
			get
			{
				return this.ftpRequestCacheValidator;
			}
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0008C348 File Offset: 0x0008A548
		internal static RequestCachingSectionInternal GetSection()
		{
			object obj = RequestCachingSectionInternal.ClassSyncObject;
			RequestCachingSectionInternal requestCachingSectionInternal;
			lock (obj)
			{
				RequestCachingSection requestCachingSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.RequestCachingSectionPath) as RequestCachingSection;
				if (requestCachingSection == null)
				{
					requestCachingSectionInternal = null;
				}
				else
				{
					try
					{
						requestCachingSectionInternal = new RequestCachingSectionInternal(requestCachingSection);
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						throw new ConfigurationErrorsException(SR.GetString("net_config_requestcaching"), ex);
					}
				}
			}
			return requestCachingSectionInternal;
		}

		// Token: 0x04001C49 RID: 7241
		private static object classSyncObject;

		// Token: 0x04001C4A RID: 7242
		private RequestCache defaultCache;

		// Token: 0x04001C4B RID: 7243
		private HttpRequestCachePolicy defaultHttpCachePolicy;

		// Token: 0x04001C4C RID: 7244
		private RequestCachePolicy defaultFtpCachePolicy;

		// Token: 0x04001C4D RID: 7245
		private RequestCachePolicy defaultCachePolicy;

		// Token: 0x04001C4E RID: 7246
		private bool disableAllCaching;

		// Token: 0x04001C4F RID: 7247
		private HttpRequestCacheValidator httpRequestCacheValidator;

		// Token: 0x04001C50 RID: 7248
		private FtpRequestCacheValidator ftpRequestCacheValidator;

		// Token: 0x04001C51 RID: 7249
		private bool isPrivateCache;

		// Token: 0x04001C52 RID: 7250
		private TimeSpan unspecifiedMaximumAge;
	}
}
