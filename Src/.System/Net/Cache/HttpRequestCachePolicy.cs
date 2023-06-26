using System;
using System.Globalization;

namespace System.Net.Cache
{
	/// <summary>Defines an application's caching requirements for resources obtained by using <see cref="T:System.Net.HttpWebRequest" /> objects.</summary>
	// Token: 0x02000315 RID: 789
	public class HttpRequestCachePolicy : RequestCachePolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class.</summary>
		// Token: 0x06001C09 RID: 7177 RVA: 0x00085A41 File Offset: 0x00083C41
		public HttpRequestCachePolicy()
			: this(HttpRequestCacheLevel.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified cache policy.</summary>
		/// <param name="level">An <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value.</param>
		// Token: 0x06001C0A RID: 7178 RVA: 0x00085A4C File Offset: 0x00083C4C
		public HttpRequestCachePolicy(HttpRequestCacheLevel level)
			: base(HttpRequestCachePolicy.MapLevel(level))
		{
			this.m_Level = level;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified age control and time values.</summary>
		/// <param name="cacheAgeControl">One of the following <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> enumeration values: <see cref="F:System.Net.Cache.HttpCacheAgeControl.MaxAge" />, <see cref="F:System.Net.Cache.HttpCacheAgeControl.MaxStale" />, or <see cref="F:System.Net.Cache.HttpCacheAgeControl.MinFresh" />.</param>
		/// <param name="ageOrFreshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time.</param>
		/// <exception cref="T:System.ArgumentException">The value specified for the <paramref name="cacheAgeControl" /> parameter cannot be used with this constructor.</exception>
		// Token: 0x06001C0B RID: 7179 RVA: 0x00085A98 File Offset: 0x00083C98
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan ageOrFreshOrStale)
			: this(HttpRequestCacheLevel.Default)
		{
			switch (cacheAgeControl)
			{
			case HttpCacheAgeControl.MinFresh:
				this.m_MinFresh = ageOrFreshOrStale;
				return;
			case HttpCacheAgeControl.MaxAge:
				this.m_MaxAge = ageOrFreshOrStale;
				return;
			case HttpCacheAgeControl.MaxStale:
				this.m_MaxStale = ageOrFreshOrStale;
				return;
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "HttpCacheAgeControl" }), "cacheAgeControl");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified maximum age, age control value, and time value.</summary>
		/// <param name="cacheAgeControl">An <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> value.</param>
		/// <param name="maxAge">A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for resources.</param>
		/// <param name="freshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time.</param>
		/// <exception cref="T:System.ArgumentException">The value specified for the <paramref name="cacheAgeControl" /> parameter is not valid.</exception>
		// Token: 0x06001C0C RID: 7180 RVA: 0x00085B00 File Offset: 0x00083D00
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan maxAge, TimeSpan freshOrStale)
			: this(HttpRequestCacheLevel.Default)
		{
			switch (cacheAgeControl)
			{
			case HttpCacheAgeControl.MinFresh:
				this.m_MinFresh = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxAge:
				this.m_MaxAge = maxAge;
				return;
			case HttpCacheAgeControl.MaxAgeAndMinFresh:
				this.m_MaxAge = maxAge;
				this.m_MinFresh = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxStale:
				this.m_MaxStale = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxAgeAndMaxStale:
				this.m_MaxAge = maxAge;
				this.m_MaxStale = freshOrStale;
				return;
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "HttpCacheAgeControl" }), "cacheAgeControl");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified cache synchronization date.</summary>
		/// <param name="cacheSyncDate">A <see cref="T:System.DateTime" /> object that specifies the time when resources stored in the cache must be revalidated.</param>
		// Token: 0x06001C0D RID: 7181 RVA: 0x00085B8E File Offset: 0x00083D8E
		public HttpRequestCachePolicy(DateTime cacheSyncDate)
			: this(HttpRequestCacheLevel.Default)
		{
			this.m_LastSyncDateUtc = cacheSyncDate.ToUniversalTime();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified maximum age, age control value, time value, and cache synchronization date.</summary>
		/// <param name="cacheAgeControl">An <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> value.</param>
		/// <param name="maxAge">A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for resources.</param>
		/// <param name="freshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time.</param>
		/// <param name="cacheSyncDate">A <see cref="T:System.DateTime" /> object that specifies the time when resources stored in the cache must be revalidated.</param>
		// Token: 0x06001C0E RID: 7182 RVA: 0x00085BA4 File Offset: 0x00083DA4
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan maxAge, TimeSpan freshOrStale, DateTime cacheSyncDate)
			: this(cacheAgeControl, maxAge, freshOrStale)
		{
			this.m_LastSyncDateUtc = cacheSyncDate.ToUniversalTime();
		}

		/// <summary>Gets the <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that was specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that specifies the cache behavior for resources that were obtained using <see cref="T:System.Net.HttpWebRequest" /> objects.</returns>
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00085BBC File Offset: 0x00083DBC
		public new HttpRequestCacheLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		/// <summary>Gets the cache synchronization date for this instance.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value set to the date specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x00085BC4 File Offset: 0x00083DC4
		public DateTime CacheSyncDate
		{
			get
			{
				if (this.m_LastSyncDateUtc == DateTime.MinValue || this.m_LastSyncDateUtc == DateTime.MaxValue)
				{
					return this.m_LastSyncDateUtc;
				}
				return this.m_LastSyncDateUtc.ToLocalTime();
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00085BFC File Offset: 0x00083DFC
		internal DateTime InternalCacheSyncDateUtc
		{
			get
			{
				return this.m_LastSyncDateUtc;
			}
		}

		/// <summary>Gets the maximum age permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum age value specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x00085C04 File Offset: 0x00083E04
		public TimeSpan MaxAge
		{
			get
			{
				return this.m_MaxAge;
			}
		}

		/// <summary>Gets the minimum freshness that is permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the minimum freshness specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x00085C0C File Offset: 0x00083E0C
		public TimeSpan MinFresh
		{
			get
			{
				return this.m_MinFresh;
			}
		}

		/// <summary>Gets the maximum staleness value that is permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum staleness value specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x00085C14 File Offset: 0x00083E14
		public TimeSpan MaxStale
		{
			get
			{
				return this.m_MaxStale;
			}
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the property values for this instance.</returns>
		// Token: 0x06001C15 RID: 7189 RVA: 0x00085C1C File Offset: 0x00083E1C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Level:",
				this.m_Level.ToString(),
				(this.m_MaxAge == TimeSpan.MaxValue) ? string.Empty : (" MaxAge:" + this.m_MaxAge.ToString()),
				(this.m_MinFresh == TimeSpan.MinValue) ? string.Empty : (" MinFresh:" + this.m_MinFresh.ToString()),
				(this.m_MaxStale == TimeSpan.MinValue) ? string.Empty : (" MaxStale:" + this.m_MaxStale.ToString()),
				(this.CacheSyncDate == DateTime.MinValue) ? string.Empty : (" CacheSyncDate:" + this.CacheSyncDate.ToString(CultureInfo.CurrentCulture))
			});
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00085D2E File Offset: 0x00083F2E
		private static RequestCacheLevel MapLevel(HttpRequestCacheLevel level)
		{
			if (level <= HttpRequestCacheLevel.NoCacheNoStore)
			{
				return (RequestCacheLevel)level;
			}
			if (level == HttpRequestCacheLevel.CacheOrNextCacheOnly)
			{
				return RequestCacheLevel.CacheOnly;
			}
			if (level == HttpRequestCacheLevel.Refresh)
			{
				return RequestCacheLevel.Reload;
			}
			throw new ArgumentOutOfRangeException("level");
		}

		// Token: 0x04001B57 RID: 6999
		internal static readonly HttpRequestCachePolicy BypassCache = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);

		// Token: 0x04001B58 RID: 7000
		private HttpRequestCacheLevel m_Level;

		// Token: 0x04001B59 RID: 7001
		private DateTime m_LastSyncDateUtc = DateTime.MinValue;

		// Token: 0x04001B5A RID: 7002
		private TimeSpan m_MaxAge = TimeSpan.MaxValue;

		// Token: 0x04001B5B RID: 7003
		private TimeSpan m_MinFresh = TimeSpan.MinValue;

		// Token: 0x04001B5C RID: 7004
		private TimeSpan m_MaxStale = TimeSpan.MinValue;
	}
}
