using System;

namespace System.Net.Cache
{
	/// <summary>Defines an application's caching requirements for resources obtained by using <see cref="T:System.Net.WebRequest" /> objects.</summary>
	// Token: 0x02000312 RID: 786
	public class RequestCachePolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.RequestCachePolicy" /> class.</summary>
		// Token: 0x06001C05 RID: 7173 RVA: 0x000859F1 File Offset: 0x00083BF1
		public RequestCachePolicy()
			: this(RequestCacheLevel.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.RequestCachePolicy" /> class. using the specified cache policy.</summary>
		/// <param name="level">A <see cref="T:System.Net.Cache.RequestCacheLevel" /> that specifies the cache behavior for resources obtained using <see cref="T:System.Net.WebRequest" /> objects.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">level is not a valid <see cref="T:System.Net.Cache.RequestCacheLevel" />.value.</exception>
		// Token: 0x06001C06 RID: 7174 RVA: 0x000859FA File Offset: 0x00083BFA
		public RequestCachePolicy(RequestCacheLevel level)
		{
			if (level < RequestCacheLevel.Default || level > RequestCacheLevel.NoCacheNoStore)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.m_Level = level;
		}

		/// <summary>Gets the <see cref="T:System.Net.Cache.RequestCacheLevel" /> value specified when this instance was constructed.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> value that specifies the cache behavior for resources obtained using <see cref="T:System.Net.WebRequest" /> objects.</returns>
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x00085A1C File Offset: 0x00083C1C
		public RequestCacheLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the <see cref="P:System.Net.Cache.RequestCachePolicy.Level" /> for this instance.</returns>
		// Token: 0x06001C08 RID: 7176 RVA: 0x00085A24 File Offset: 0x00083C24
		public override string ToString()
		{
			return "Level:" + this.m_Level.ToString();
		}

		// Token: 0x04001B45 RID: 6981
		private RequestCacheLevel m_Level;
	}
}
