using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using Microsoft.Win32;

namespace System.Net.Cache
{
	// Token: 0x02000321 RID: 801
	internal class SingleItemRequestCache : WinInetCache
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x0008A007 File Offset: 0x00088207
		internal SingleItemRequestCache(bool useWinInet)
			: base(true, true, false)
		{
			this._UseWinInet = useWinInet;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0008A01C File Offset: 0x0008821C
		internal override Stream Retrieve(string key, out RequestCacheEntry cacheEntry)
		{
			Stream stream;
			if (!this.TryRetrieve(key, out cacheEntry, out stream))
			{
				FileNotFoundException ex = new FileNotFoundException(null, key);
				throw new IOException(SR.GetString("net_cache_retrieve_failure", new object[] { ex.Message }), ex);
			}
			return stream;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0008A060 File Offset: 0x00088260
		internal override Stream Store(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata)
		{
			Stream stream;
			if (!this.TryStore(key, contentLength, expiresUtc, lastModifiedUtc, maxStale, entryMetadata, systemMetadata, out stream))
			{
				FileNotFoundException ex = new FileNotFoundException(null, key);
				throw new IOException(SR.GetString("net_cache_retrieve_failure", new object[] { ex.Message }), ex);
			}
			return stream;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0008A0AC File Offset: 0x000882AC
		internal override void Remove(string key)
		{
			if (!this.TryRemove(key))
			{
				FileNotFoundException ex = new FileNotFoundException(null, key);
				throw new IOException(SR.GetString("net_cache_retrieve_failure", new object[] { ex.Message }), ex);
			}
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0008A0EC File Offset: 0x000882EC
		internal override void Update(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata)
		{
			if (!this.TryUpdate(key, expiresUtc, lastModifiedUtc, lastSynchronizedUtc, maxStale, entryMetadata, systemMetadata))
			{
				FileNotFoundException ex = new FileNotFoundException(null, key);
				throw new IOException(SR.GetString("net_cache_retrieve_failure", new object[] { ex.Message }), ex);
			}
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0008A134 File Offset: 0x00088334
		internal override bool TryRetrieve(string key, out RequestCacheEntry cacheEntry, out Stream readStream)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			SingleItemRequestCache.FrozenCacheEntry frozenCacheEntry = this._Entry;
			cacheEntry = null;
			readStream = null;
			if (frozenCacheEntry == null || frozenCacheEntry.Key != key)
			{
				RequestCacheEntry requestCacheEntry;
				Stream stream;
				if (!this._UseWinInet || !base.TryRetrieve(key, out requestCacheEntry, out stream))
				{
					return false;
				}
				frozenCacheEntry = new SingleItemRequestCache.FrozenCacheEntry(key, requestCacheEntry, stream);
				stream.Close();
				this._Entry = frozenCacheEntry;
			}
			cacheEntry = SingleItemRequestCache.FrozenCacheEntry.Create(frozenCacheEntry);
			readStream = new SingleItemRequestCache.ReadOnlyStream(frozenCacheEntry.StreamBytes);
			return true;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0008A1B0 File Offset: 0x000883B0
		internal override bool TryStore(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, out Stream writeStream)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			RequestCacheEntry requestCacheEntry = new RequestCacheEntry();
			requestCacheEntry.IsPrivateEntry = base.IsPrivateCache;
			requestCacheEntry.StreamSize = contentLength;
			requestCacheEntry.ExpiresUtc = expiresUtc;
			requestCacheEntry.LastModifiedUtc = lastModifiedUtc;
			requestCacheEntry.LastAccessedUtc = DateTime.UtcNow;
			requestCacheEntry.LastSynchronizedUtc = DateTime.UtcNow;
			requestCacheEntry.MaxStale = maxStale;
			requestCacheEntry.HitCount = 0;
			requestCacheEntry.UsageCount = 0;
			requestCacheEntry.IsPartialEntry = false;
			requestCacheEntry.EntryMetadata = entryMetadata;
			requestCacheEntry.SystemMetadata = systemMetadata;
			writeStream = null;
			Stream stream = null;
			if (this._UseWinInet)
			{
				base.TryStore(key, contentLength, expiresUtc, lastModifiedUtc, maxStale, entryMetadata, systemMetadata, out stream);
			}
			writeStream = new SingleItemRequestCache.WriteOnlyStream(key, this, requestCacheEntry, stream);
			return true;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0008A268 File Offset: 0x00088468
		private void Commit(string key, RequestCacheEntry tempEntry, byte[] allBytes)
		{
			SingleItemRequestCache.FrozenCacheEntry frozenCacheEntry = new SingleItemRequestCache.FrozenCacheEntry(key, tempEntry, allBytes);
			this._Entry = frozenCacheEntry;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0008A288 File Offset: 0x00088488
		internal override bool TryRemove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this._UseWinInet)
			{
				base.TryRemove(key);
			}
			SingleItemRequestCache.FrozenCacheEntry entry = this._Entry;
			if (entry != null && entry.Key == key)
			{
				this._Entry = null;
			}
			return true;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0008A2D4 File Offset: 0x000884D4
		internal override bool TryUpdate(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			SingleItemRequestCache.FrozenCacheEntry frozenCacheEntry = SingleItemRequestCache.FrozenCacheEntry.Create(this._Entry);
			if (frozenCacheEntry == null || frozenCacheEntry.Key != key)
			{
				return true;
			}
			frozenCacheEntry.ExpiresUtc = expiresUtc;
			frozenCacheEntry.LastModifiedUtc = lastModifiedUtc;
			frozenCacheEntry.LastSynchronizedUtc = lastSynchronizedUtc;
			frozenCacheEntry.MaxStale = maxStale;
			frozenCacheEntry.EntryMetadata = entryMetadata;
			frozenCacheEntry.SystemMetadata = systemMetadata;
			this._Entry = frozenCacheEntry;
			return true;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0008A344 File Offset: 0x00088544
		internal override void UnlockEntry(Stream stream)
		{
		}

		// Token: 0x04001BA1 RID: 7073
		private bool _UseWinInet;

		// Token: 0x04001BA2 RID: 7074
		private SingleItemRequestCache.FrozenCacheEntry _Entry;

		// Token: 0x020007BC RID: 1980
		private sealed class FrozenCacheEntry : RequestCacheEntry
		{
			// Token: 0x0600432D RID: 17197 RVA: 0x0011B955 File Offset: 0x00119B55
			public FrozenCacheEntry(string key, RequestCacheEntry entry, Stream stream)
				: this(key, entry, SingleItemRequestCache.FrozenCacheEntry.GetBytes(stream))
			{
			}

			// Token: 0x0600432E RID: 17198 RVA: 0x0011B968 File Offset: 0x00119B68
			public FrozenCacheEntry(string key, RequestCacheEntry entry, byte[] streamBytes)
			{
				this._Key = key;
				this._StreamBytes = streamBytes;
				base.IsPrivateEntry = entry.IsPrivateEntry;
				base.StreamSize = entry.StreamSize;
				base.ExpiresUtc = entry.ExpiresUtc;
				base.HitCount = entry.HitCount;
				base.LastAccessedUtc = entry.LastAccessedUtc;
				entry.LastModifiedUtc = entry.LastModifiedUtc;
				base.LastSynchronizedUtc = entry.LastSynchronizedUtc;
				base.MaxStale = entry.MaxStale;
				base.UsageCount = entry.UsageCount;
				base.IsPartialEntry = entry.IsPartialEntry;
				base.EntryMetadata = entry.EntryMetadata;
				base.SystemMetadata = entry.SystemMetadata;
			}

			// Token: 0x0600432F RID: 17199 RVA: 0x0011BA1C File Offset: 0x00119C1C
			private static byte[] GetBytes(Stream stream)
			{
				bool flag = false;
				byte[] array;
				if (stream.CanSeek)
				{
					array = new byte[stream.Length];
				}
				else
				{
					flag = true;
					array = new byte[8192];
				}
				int num = 0;
				for (;;)
				{
					int num2 = stream.Read(array, num, array.Length - num);
					if (num2 == 0)
					{
						break;
					}
					if ((num += num2) == array.Length && flag)
					{
						byte[] array2 = new byte[array.Length + 8192];
						Buffer.BlockCopy(array, 0, array2, 0, num);
						array = array2;
					}
				}
				if (flag)
				{
					byte[] array3 = new byte[num];
					Buffer.BlockCopy(array, 0, array3, 0, num);
					array = array3;
				}
				return array;
			}

			// Token: 0x06004330 RID: 17200 RVA: 0x0011BAAA File Offset: 0x00119CAA
			public static SingleItemRequestCache.FrozenCacheEntry Create(SingleItemRequestCache.FrozenCacheEntry clonedObject)
			{
				if (clonedObject != null)
				{
					return (SingleItemRequestCache.FrozenCacheEntry)clonedObject.MemberwiseClone();
				}
				return null;
			}

			// Token: 0x17000F3C RID: 3900
			// (get) Token: 0x06004331 RID: 17201 RVA: 0x0011BABC File Offset: 0x00119CBC
			public byte[] StreamBytes
			{
				get
				{
					return this._StreamBytes;
				}
			}

			// Token: 0x17000F3D RID: 3901
			// (get) Token: 0x06004332 RID: 17202 RVA: 0x0011BAC4 File Offset: 0x00119CC4
			public string Key
			{
				get
				{
					return this._Key;
				}
			}

			// Token: 0x04003442 RID: 13378
			private byte[] _StreamBytes;

			// Token: 0x04003443 RID: 13379
			private string _Key;
		}

		// Token: 0x020007BD RID: 1981
		internal class ReadOnlyStream : Stream, IRequestLifetimeTracker
		{
			// Token: 0x06004333 RID: 17203 RVA: 0x0011BACC File Offset: 0x00119CCC
			internal ReadOnlyStream(byte[] bytes)
			{
				this._Bytes = bytes;
				this._Offset = 0;
				this._Disposed = false;
				this._ReadTimeout = (this._WriteTimeout = -1);
			}

			// Token: 0x17000F3E RID: 3902
			// (get) Token: 0x06004334 RID: 17204 RVA: 0x0011BB04 File Offset: 0x00119D04
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F3F RID: 3903
			// (get) Token: 0x06004335 RID: 17205 RVA: 0x0011BB07 File Offset: 0x00119D07
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F40 RID: 3904
			// (get) Token: 0x06004336 RID: 17206 RVA: 0x0011BB0A File Offset: 0x00119D0A
			public override bool CanTimeout
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F41 RID: 3905
			// (get) Token: 0x06004337 RID: 17207 RVA: 0x0011BB0D File Offset: 0x00119D0D
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F42 RID: 3906
			// (get) Token: 0x06004338 RID: 17208 RVA: 0x0011BB10 File Offset: 0x00119D10
			public override long Length
			{
				get
				{
					return (long)this._Bytes.Length;
				}
			}

			// Token: 0x17000F43 RID: 3907
			// (get) Token: 0x06004339 RID: 17209 RVA: 0x0011BB1B File Offset: 0x00119D1B
			// (set) Token: 0x0600433A RID: 17210 RVA: 0x0011BB24 File Offset: 0x00119D24
			public override long Position
			{
				get
				{
					return (long)this._Offset;
				}
				set
				{
					if (value < 0L || value > (long)this._Bytes.Length)
					{
						throw new ArgumentOutOfRangeException("value");
					}
					this._Offset = (int)value;
				}
			}

			// Token: 0x17000F44 RID: 3908
			// (get) Token: 0x0600433B RID: 17211 RVA: 0x0011BB4A File Offset: 0x00119D4A
			// (set) Token: 0x0600433C RID: 17212 RVA: 0x0011BB52 File Offset: 0x00119D52
			public override int ReadTimeout
			{
				get
				{
					return this._ReadTimeout;
				}
				set
				{
					if (value <= 0 && value != -1)
					{
						throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._ReadTimeout = value;
				}
			}

			// Token: 0x17000F45 RID: 3909
			// (get) Token: 0x0600433D RID: 17213 RVA: 0x0011BB78 File Offset: 0x00119D78
			// (set) Token: 0x0600433E RID: 17214 RVA: 0x0011BB80 File Offset: 0x00119D80
			public override int WriteTimeout
			{
				get
				{
					return this._WriteTimeout;
				}
				set
				{
					if (value <= 0 && value != -1)
					{
						throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._WriteTimeout = value;
				}
			}

			// Token: 0x0600433F RID: 17215 RVA: 0x0011BBA6 File Offset: 0x00119DA6
			public override void Flush()
			{
			}

			// Token: 0x06004340 RID: 17216 RVA: 0x0011BBA8 File Offset: 0x00119DA8
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				int num = this.Read(buffer, offset, count);
				LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, callback);
				lazyAsyncResult.InvokeCallback(num);
				return lazyAsyncResult;
			}

			// Token: 0x06004341 RID: 17217 RVA: 0x0011BBD8 File Offset: 0x00119DD8
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
				if (lazyAsyncResult.EndCalled)
				{
					throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndRead" }));
				}
				lazyAsyncResult.EndCalled = true;
				return (int)lazyAsyncResult.InternalWaitForCompletion();
			}

			// Token: 0x06004342 RID: 17218 RVA: 0x0011BC34 File Offset: 0x00119E34
			public override int Read(byte[] buffer, int offset, int count)
			{
				if (this._Disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (offset < 0 || offset > buffer.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (count < 0 || count > buffer.Length - offset)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (this._Offset == this._Bytes.Length)
				{
					return 0;
				}
				int num = this._Offset;
				count = Math.Min(count, this._Bytes.Length - num);
				System.Buffer.BlockCopy(this._Bytes, num, buffer, offset, count);
				num += count;
				this._Offset = num;
				return count;
			}

			// Token: 0x06004343 RID: 17219 RVA: 0x0011BCDA File Offset: 0x00119EDA
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x06004344 RID: 17220 RVA: 0x0011BCEB File Offset: 0x00119EEB
			public override void EndWrite(IAsyncResult asyncResult)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x06004345 RID: 17221 RVA: 0x0011BCFC File Offset: 0x00119EFC
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x06004346 RID: 17222 RVA: 0x0011BD10 File Offset: 0x00119F10
			public override long Seek(long offset, SeekOrigin origin)
			{
				switch (origin)
				{
				case SeekOrigin.Begin:
					this.Position = offset;
					return offset;
				case SeekOrigin.Current:
					return this.Position += offset;
				case SeekOrigin.End:
					return this.Position = (long)this._Bytes.Length - offset;
				default:
					throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SeekOrigin" }), "origin");
				}
			}

			// Token: 0x06004347 RID: 17223 RVA: 0x0011BD85 File Offset: 0x00119F85
			public override void SetLength(long length)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x06004348 RID: 17224 RVA: 0x0011BD98 File Offset: 0x00119F98
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (!this._Disposed)
					{
						this._Disposed = true;
						if (disposing)
						{
							RequestLifetimeSetter.Report(this.m_RequestLifetimeSetter);
						}
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x17000F46 RID: 3910
			// (get) Token: 0x06004349 RID: 17225 RVA: 0x0011BDDC File Offset: 0x00119FDC
			internal byte[] Buffer
			{
				get
				{
					return this._Bytes;
				}
			}

			// Token: 0x0600434A RID: 17226 RVA: 0x0011BDE4 File Offset: 0x00119FE4
			void IRequestLifetimeTracker.TrackRequestLifetime(long requestStartTimestamp)
			{
				this.m_RequestLifetimeSetter = new RequestLifetimeSetter(requestStartTimestamp);
			}

			// Token: 0x04003444 RID: 13380
			private byte[] _Bytes;

			// Token: 0x04003445 RID: 13381
			private int _Offset;

			// Token: 0x04003446 RID: 13382
			private bool _Disposed;

			// Token: 0x04003447 RID: 13383
			private int _ReadTimeout;

			// Token: 0x04003448 RID: 13384
			private int _WriteTimeout;

			// Token: 0x04003449 RID: 13385
			private RequestLifetimeSetter m_RequestLifetimeSetter;
		}

		// Token: 0x020007BE RID: 1982
		private class WriteOnlyStream : Stream
		{
			// Token: 0x0600434B RID: 17227 RVA: 0x0011BDF2 File Offset: 0x00119FF2
			public WriteOnlyStream(string key, SingleItemRequestCache cache, RequestCacheEntry cacheEntry, Stream realWriteStream)
			{
				this._Key = key;
				this._Cache = cache;
				this._TempEntry = cacheEntry;
				this._RealStream = realWriteStream;
				this._Buffers = new ArrayList();
			}

			// Token: 0x17000F47 RID: 3911
			// (get) Token: 0x0600434C RID: 17228 RVA: 0x0011BE22 File Offset: 0x0011A022
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F48 RID: 3912
			// (get) Token: 0x0600434D RID: 17229 RVA: 0x0011BE25 File Offset: 0x0011A025
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F49 RID: 3913
			// (get) Token: 0x0600434E RID: 17230 RVA: 0x0011BE28 File Offset: 0x0011A028
			public override bool CanTimeout
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F4A RID: 3914
			// (get) Token: 0x0600434F RID: 17231 RVA: 0x0011BE2B File Offset: 0x0011A02B
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F4B RID: 3915
			// (get) Token: 0x06004350 RID: 17232 RVA: 0x0011BE2E File Offset: 0x0011A02E
			public override long Length
			{
				get
				{
					throw new NotSupportedException(SR.GetString("net_writeonlystream"));
				}
			}

			// Token: 0x17000F4C RID: 3916
			// (get) Token: 0x06004351 RID: 17233 RVA: 0x0011BE3F File Offset: 0x0011A03F
			// (set) Token: 0x06004352 RID: 17234 RVA: 0x0011BE50 File Offset: 0x0011A050
			public override long Position
			{
				get
				{
					throw new NotSupportedException(SR.GetString("net_writeonlystream"));
				}
				set
				{
					throw new NotSupportedException(SR.GetString("net_writeonlystream"));
				}
			}

			// Token: 0x17000F4D RID: 3917
			// (get) Token: 0x06004353 RID: 17235 RVA: 0x0011BE61 File Offset: 0x0011A061
			// (set) Token: 0x06004354 RID: 17236 RVA: 0x0011BE69 File Offset: 0x0011A069
			public override int ReadTimeout
			{
				get
				{
					return this._ReadTimeout;
				}
				set
				{
					if (value <= 0 && value != -1)
					{
						throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._ReadTimeout = value;
				}
			}

			// Token: 0x17000F4E RID: 3918
			// (get) Token: 0x06004355 RID: 17237 RVA: 0x0011BE8F File Offset: 0x0011A08F
			// (set) Token: 0x06004356 RID: 17238 RVA: 0x0011BE97 File Offset: 0x0011A097
			public override int WriteTimeout
			{
				get
				{
					return this._WriteTimeout;
				}
				set
				{
					if (value <= 0 && value != -1)
					{
						throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._WriteTimeout = value;
				}
			}

			// Token: 0x06004357 RID: 17239 RVA: 0x0011BEBD File Offset: 0x0011A0BD
			public override void Flush()
			{
			}

			// Token: 0x06004358 RID: 17240 RVA: 0x0011BEBF File Offset: 0x0011A0BF
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x06004359 RID: 17241 RVA: 0x0011BED0 File Offset: 0x0011A0D0
			public override int EndRead(IAsyncResult asyncResult)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x0600435A RID: 17242 RVA: 0x0011BEE1 File Offset: 0x0011A0E1
			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x0600435B RID: 17243 RVA: 0x0011BEF2 File Offset: 0x0011A0F2
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x0600435C RID: 17244 RVA: 0x0011BF03 File Offset: 0x0011A103
			public override void SetLength(long length)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x0600435D RID: 17245 RVA: 0x0011BF14 File Offset: 0x0011A114
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				this.Write(buffer, offset, count);
				LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, callback);
				lazyAsyncResult.InvokeCallback(null);
				return lazyAsyncResult;
			}

			// Token: 0x0600435E RID: 17246 RVA: 0x0011BF40 File Offset: 0x0011A140
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
				if (lazyAsyncResult.EndCalled)
				{
					throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndWrite" }));
				}
				lazyAsyncResult.EndCalled = true;
				lazyAsyncResult.InternalWaitForCompletion();
			}

			// Token: 0x0600435F RID: 17247 RVA: 0x0011BF98 File Offset: 0x0011A198
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (this._Disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (offset < 0 || offset > buffer.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (count < 0 || count > buffer.Length - offset)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (this._RealStream != null)
				{
					try
					{
						this._RealStream.Write(buffer, offset, count);
					}
					catch
					{
						this._RealStream.Close();
						this._RealStream = null;
					}
				}
				byte[] array = new byte[count];
				Buffer.BlockCopy(buffer, offset, array, 0, count);
				this._Buffers.Add(array);
				this._TotalSize += (long)count;
			}

			// Token: 0x06004360 RID: 17248 RVA: 0x0011C064 File Offset: 0x0011A264
			protected override void Dispose(bool disposing)
			{
				this._Disposed = true;
				base.Dispose(disposing);
				if (disposing)
				{
					if (this._RealStream != null)
					{
						try
						{
							this._RealStream.Close();
						}
						catch
						{
						}
					}
					byte[] array = new byte[this._TotalSize];
					int num = 0;
					for (int i = 0; i < this._Buffers.Count; i++)
					{
						byte[] array2 = (byte[])this._Buffers[i];
						Buffer.BlockCopy(array2, 0, array, num, array2.Length);
						num += array2.Length;
					}
					this._Cache.Commit(this._Key, this._TempEntry, array);
				}
			}

			// Token: 0x0400344A RID: 13386
			private string _Key;

			// Token: 0x0400344B RID: 13387
			private SingleItemRequestCache _Cache;

			// Token: 0x0400344C RID: 13388
			private RequestCacheEntry _TempEntry;

			// Token: 0x0400344D RID: 13389
			private Stream _RealStream;

			// Token: 0x0400344E RID: 13390
			private long _TotalSize;

			// Token: 0x0400344F RID: 13391
			private ArrayList _Buffers;

			// Token: 0x04003450 RID: 13392
			private bool _Disposed;

			// Token: 0x04003451 RID: 13393
			private int _ReadTimeout;

			// Token: 0x04003452 RID: 13394
			private int _WriteTimeout;
		}
	}
}
