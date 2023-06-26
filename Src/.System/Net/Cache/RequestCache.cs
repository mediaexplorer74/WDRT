using System;
using System.Collections.Specialized;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x0200030D RID: 781
	internal abstract class RequestCache
	{
		// Token: 0x06001BD3 RID: 7123 RVA: 0x00085221 File Offset: 0x00083421
		protected RequestCache(bool isPrivateCache, bool canWrite)
		{
			this._IsPrivateCache = isPrivateCache;
			this._CanWrite = canWrite;
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00085237 File Offset: 0x00083437
		internal bool IsPrivateCache
		{
			get
			{
				return this._IsPrivateCache;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x0008523F File Offset: 0x0008343F
		internal bool CanWrite
		{
			get
			{
				return this._CanWrite;
			}
		}

		// Token: 0x06001BD6 RID: 7126
		internal abstract Stream Retrieve(string key, out RequestCacheEntry cacheEntry);

		// Token: 0x06001BD7 RID: 7127
		internal abstract Stream Store(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06001BD8 RID: 7128
		internal abstract void Remove(string key);

		// Token: 0x06001BD9 RID: 7129
		internal abstract void Update(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06001BDA RID: 7130
		internal abstract bool TryRetrieve(string key, out RequestCacheEntry cacheEntry, out Stream readStream);

		// Token: 0x06001BDB RID: 7131
		internal abstract bool TryStore(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, out Stream writeStream);

		// Token: 0x06001BDC RID: 7132
		internal abstract bool TryRemove(string key);

		// Token: 0x06001BDD RID: 7133
		internal abstract bool TryUpdate(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06001BDE RID: 7134
		internal abstract void UnlockEntry(Stream retrieveStream);

		// Token: 0x04001B26 RID: 6950
		internal static readonly char[] LineSplits = new char[] { '\r', '\n' };

		// Token: 0x04001B27 RID: 6951
		private bool _IsPrivateCache;

		// Token: 0x04001B28 RID: 6952
		private bool _CanWrite;
	}
}
