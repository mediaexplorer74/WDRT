using System;

namespace System.Net.Cache
{
	// Token: 0x02000310 RID: 784
	internal class RequestCacheBinding
	{
		// Token: 0x06001C01 RID: 7169 RVA: 0x000859BC File Offset: 0x00083BBC
		internal RequestCacheBinding(RequestCache requestCache, RequestCacheValidator cacheValidator, RequestCachePolicy policy)
		{
			this.m_RequestCache = requestCache;
			this.m_CacheValidator = cacheValidator;
			this.m_Policy = policy;
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x000859D9 File Offset: 0x00083BD9
		internal RequestCache Cache
		{
			get
			{
				return this.m_RequestCache;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x000859E1 File Offset: 0x00083BE1
		internal RequestCacheValidator Validator
		{
			get
			{
				return this.m_CacheValidator;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x000859E9 File Offset: 0x00083BE9
		internal RequestCachePolicy Policy
		{
			get
			{
				return this.m_Policy;
			}
		}

		// Token: 0x04001B3A RID: 6970
		private RequestCache m_RequestCache;

		// Token: 0x04001B3B RID: 6971
		private RequestCacheValidator m_CacheValidator;

		// Token: 0x04001B3C RID: 6972
		private RequestCachePolicy m_Policy;
	}
}
