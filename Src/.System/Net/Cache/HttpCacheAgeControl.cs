using System;

namespace System.Net.Cache
{
	/// <summary>Specifies the meaning of time values that control caching behavior for resources obtained using <see cref="T:System.Net.HttpWebRequest" /> objects.</summary>
	// Token: 0x02000314 RID: 788
	public enum HttpCacheAgeControl
	{
		/// <summary>For internal use only. The Framework will throw an <see cref="T:System.ArgumentException" /> if you try to use this member.</summary>
		// Token: 0x04001B51 RID: 6993
		None,
		/// <summary>Content can be taken from the cache if the time remaining before expiration is greater than or equal to the time specified with this value.</summary>
		// Token: 0x04001B52 RID: 6994
		MinFresh,
		/// <summary>Content can be taken from the cache until it is older than the age specified with this value.</summary>
		// Token: 0x04001B53 RID: 6995
		MaxAge,
		/// <summary>Content can be taken from the cache after it has expired, until the time specified with this value elapses.</summary>
		// Token: 0x04001B54 RID: 6996
		MaxStale = 4,
		/// <summary>
		///   <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxAge" /> and <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MinFresh" />.</summary>
		// Token: 0x04001B55 RID: 6997
		MaxAgeAndMinFresh = 3,
		/// <summary>
		///   <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxAge" /> and <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxStale" />.</summary>
		// Token: 0x04001B56 RID: 6998
		MaxAgeAndMaxStale = 6
	}
}
