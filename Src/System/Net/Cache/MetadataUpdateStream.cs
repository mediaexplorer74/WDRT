using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x0200031D RID: 797
	internal class MetadataUpdateStream : BaseWrapperStream, ICloseEx
	{
		// Token: 0x06001C76 RID: 7286 RVA: 0x000875AC File Offset: 0x000857AC
		internal MetadataUpdateStream(Stream parentStream, RequestCache cache, string key, DateTime expiresGMT, DateTime lastModifiedGMT, DateTime lastSynchronizedGMT, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, bool isStrictCacheErrors)
			: base(parentStream)
		{
			this.m_Cache = cache;
			this.m_Key = key;
			this.m_Expires = expiresGMT;
			this.m_LastModified = lastModifiedGMT;
			this.m_LastSynchronized = lastSynchronizedGMT;
			this.m_MaxStale = maxStale;
			this.m_EntryMetadata = entryMetadata;
			this.m_SystemMetadata = systemMetadata;
			this.m_IsStrictCacheErrors = isStrictCacheErrors;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00087606 File Offset: 0x00085806
		private MetadataUpdateStream(Stream parentStream, RequestCache cache, string key, bool isStrictCacheErrors)
			: base(parentStream)
		{
			this.m_Cache = cache;
			this.m_Key = key;
			this.m_CacheDestroy = true;
			this.m_IsStrictCacheErrors = isStrictCacheErrors;
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x0008762C File Offset: 0x0008582C
		public override bool CanRead
		{
			get
			{
				return base.WrappedStream.CanRead;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x00087639 File Offset: 0x00085839
		public override bool CanSeek
		{
			get
			{
				return base.WrappedStream.CanSeek;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00087646 File Offset: 0x00085846
		public override bool CanWrite
		{
			get
			{
				return base.WrappedStream.CanWrite;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00087653 File Offset: 0x00085853
		public override long Length
		{
			get
			{
				return base.WrappedStream.Length;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x00087660 File Offset: 0x00085860
		// (set) Token: 0x06001C7D RID: 7293 RVA: 0x0008766D File Offset: 0x0008586D
		public override long Position
		{
			get
			{
				return base.WrappedStream.Position;
			}
			set
			{
				base.WrappedStream.Position = value;
			}
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0008767B File Offset: 0x0008587B
		public override long Seek(long offset, SeekOrigin origin)
		{
			return base.WrappedStream.Seek(offset, origin);
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0008768A File Offset: 0x0008588A
		public override void SetLength(long value)
		{
			base.WrappedStream.SetLength(value);
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x00087698 File Offset: 0x00085898
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.WrappedStream.Write(buffer, offset, count);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x000876A8 File Offset: 0x000858A8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return base.WrappedStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000876BC File Offset: 0x000858BC
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.WrappedStream.EndWrite(asyncResult);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x000876CA File Offset: 0x000858CA
		public override void Flush()
		{
			base.WrappedStream.Flush();
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x000876D7 File Offset: 0x000858D7
		public override int Read(byte[] buffer, int offset, int count)
		{
			return base.WrappedStream.Read(buffer, offset, count);
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x000876E7 File Offset: 0x000858E7
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return base.WrappedStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000876FB File Offset: 0x000858FB
		public override int EndRead(IAsyncResult asyncResult)
		{
			return base.WrappedStream.EndRead(asyncResult);
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x00087709 File Offset: 0x00085909
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x00087713 File Offset: 0x00085913
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			this.Dispose(true, closeState);
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0008771D File Offset: 0x0008591D
		public override bool CanTimeout
		{
			get
			{
				return base.WrappedStream.CanTimeout;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0008772A File Offset: 0x0008592A
		// (set) Token: 0x06001C8B RID: 7307 RVA: 0x00087737 File Offset: 0x00085937
		public override int ReadTimeout
		{
			get
			{
				return base.WrappedStream.ReadTimeout;
			}
			set
			{
				base.WrappedStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x00087745 File Offset: 0x00085945
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x00087752 File Offset: 0x00085952
		public override int WriteTimeout
		{
			get
			{
				return base.WrappedStream.WriteTimeout;
			}
			set
			{
				base.WrappedStream.WriteTimeout = value;
			}
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00087760 File Offset: 0x00085960
		protected virtual void Dispose(bool disposing, CloseExState closeState)
		{
			try
			{
				if (Interlocked.Increment(ref this._Disposed) == 1 && disposing)
				{
					ICloseEx closeEx = base.WrappedStream as ICloseEx;
					if (closeEx != null)
					{
						closeEx.CloseEx(closeState);
					}
					else
					{
						base.WrappedStream.Close();
					}
					if (this.m_CacheDestroy)
					{
						if (this.m_IsStrictCacheErrors)
						{
							this.m_Cache.Remove(this.m_Key);
						}
						else
						{
							this.m_Cache.TryRemove(this.m_Key);
						}
					}
					else if (this.m_IsStrictCacheErrors)
					{
						this.m_Cache.Update(this.m_Key, this.m_Expires, this.m_LastModified, this.m_LastSynchronized, this.m_MaxStale, this.m_EntryMetadata, this.m_SystemMetadata);
					}
					else
					{
						this.m_Cache.TryUpdate(this.m_Key, this.m_Expires, this.m_LastModified, this.m_LastSynchronized, this.m_MaxStale, this.m_EntryMetadata, this.m_SystemMetadata);
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x04001B8B RID: 7051
		private RequestCache m_Cache;

		// Token: 0x04001B8C RID: 7052
		private string m_Key;

		// Token: 0x04001B8D RID: 7053
		private DateTime m_Expires;

		// Token: 0x04001B8E RID: 7054
		private DateTime m_LastModified;

		// Token: 0x04001B8F RID: 7055
		private DateTime m_LastSynchronized;

		// Token: 0x04001B90 RID: 7056
		private TimeSpan m_MaxStale;

		// Token: 0x04001B91 RID: 7057
		private StringCollection m_EntryMetadata;

		// Token: 0x04001B92 RID: 7058
		private StringCollection m_SystemMetadata;

		// Token: 0x04001B93 RID: 7059
		private bool m_CacheDestroy;

		// Token: 0x04001B94 RID: 7060
		private bool m_IsStrictCacheErrors;

		// Token: 0x04001B95 RID: 7061
		private int _Disposed;
	}
}
