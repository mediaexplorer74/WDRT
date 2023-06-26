using System;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x0200031E RID: 798
	internal class RangeStream : BaseWrapperStream, ICloseEx
	{
		// Token: 0x06001C8F RID: 7311 RVA: 0x00087870 File Offset: 0x00085A70
		internal RangeStream(Stream parentStream, long offset, long size)
			: base(parentStream)
		{
			this.m_Offset = offset;
			this.m_Size = size;
			if (base.WrappedStream.CanSeek)
			{
				base.WrappedStream.Position = offset;
				this.m_Position = offset;
				return;
			}
			throw new NotSupportedException(SR.GetString("net_cache_non_seekable_stream_not_supported"));
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x000878C2 File Offset: 0x00085AC2
		public override bool CanRead
		{
			get
			{
				return base.WrappedStream.CanRead;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x000878CF File Offset: 0x00085ACF
		public override bool CanSeek
		{
			get
			{
				return base.WrappedStream.CanSeek;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x000878DC File Offset: 0x00085ADC
		public override bool CanWrite
		{
			get
			{
				return base.WrappedStream.CanWrite;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x000878EC File Offset: 0x00085AEC
		public override long Length
		{
			get
			{
				long length = base.WrappedStream.Length;
				return this.m_Size;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0008790B File Offset: 0x00085B0B
		// (set) Token: 0x06001C95 RID: 7317 RVA: 0x0008791F File Offset: 0x00085B1F
		public override long Position
		{
			get
			{
				return base.WrappedStream.Position - this.m_Offset;
			}
			set
			{
				value += this.m_Offset;
				if (value > this.m_Offset + this.m_Size)
				{
					value = this.m_Offset + this.m_Size;
				}
				base.WrappedStream.Position = value;
			}
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00087958 File Offset: 0x00085B58
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin != SeekOrigin.Begin)
			{
				if (origin != SeekOrigin.End)
				{
					if (this.m_Position + offset > this.m_Offset + this.m_Size)
					{
						offset = this.m_Offset + this.m_Size - this.m_Position;
					}
					if (this.m_Position + offset < this.m_Offset)
					{
						offset = this.m_Offset - this.m_Position;
					}
				}
				else
				{
					offset -= this.m_Offset + this.m_Size;
					if (offset > 0L)
					{
						offset = 0L;
					}
					if (offset < -this.m_Size)
					{
						offset = -this.m_Size;
					}
				}
			}
			else
			{
				offset += this.m_Offset;
				if (offset > this.m_Offset + this.m_Size)
				{
					offset = this.m_Offset + this.m_Size;
				}
				if (offset < this.m_Offset)
				{
					offset = this.m_Offset;
				}
			}
			this.m_Position = base.WrappedStream.Seek(offset, origin);
			return this.m_Position - this.m_Offset;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00087A48 File Offset: 0x00085C48
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_cache_unsupported_partial_stream"));
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00087A5C File Offset: 0x00085C5C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.m_Position + (long)count > this.m_Offset + this.m_Size)
			{
				throw new NotSupportedException(SR.GetString("net_cache_unsupported_partial_stream"));
			}
			base.WrappedStream.Write(buffer, offset, count);
			this.m_Position += (long)count;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00087AAE File Offset: 0x00085CAE
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.m_Position + (long)offset > this.m_Offset + this.m_Size)
			{
				throw new NotSupportedException(SR.GetString("net_cache_unsupported_partial_stream"));
			}
			return base.WrappedStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00087AEA File Offset: 0x00085CEA
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.WrappedStream.EndWrite(asyncResult);
			this.m_Position = base.WrappedStream.Position;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00087B09 File Offset: 0x00085D09
		public override void Flush()
		{
			base.WrappedStream.Flush();
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00087B18 File Offset: 0x00085D18
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.m_Position >= this.m_Offset + this.m_Size)
			{
				return 0;
			}
			if (this.m_Position + (long)count > this.m_Offset + this.m_Size)
			{
				count = (int)(this.m_Offset + this.m_Size - this.m_Position);
			}
			int num = base.WrappedStream.Read(buffer, offset, count);
			this.m_Position += (long)num;
			return num;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00087B8C File Offset: 0x00085D8C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.m_Position >= this.m_Offset + this.m_Size)
			{
				count = 0;
			}
			else if (this.m_Position + (long)count > this.m_Offset + this.m_Size)
			{
				count = (int)(this.m_Offset + this.m_Size - this.m_Position);
			}
			return base.WrappedStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00087BF4 File Offset: 0x00085DF4
		public override int EndRead(IAsyncResult asyncResult)
		{
			int num = base.WrappedStream.EndRead(asyncResult);
			this.m_Position += (long)num;
			return num;
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x00087C1E File Offset: 0x00085E1E
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00087C28 File Offset: 0x00085E28
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			this.Dispose(true, closeState);
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x00087C32 File Offset: 0x00085E32
		public override bool CanTimeout
		{
			get
			{
				return base.WrappedStream.CanTimeout;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x00087C3F File Offset: 0x00085E3F
		// (set) Token: 0x06001CA3 RID: 7331 RVA: 0x00087C4C File Offset: 0x00085E4C
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

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x00087C5A File Offset: 0x00085E5A
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x00087C67 File Offset: 0x00085E67
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

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00087C78 File Offset: 0x00085E78
		protected virtual void Dispose(bool disposing, CloseExState closeState)
		{
			try
			{
				if (disposing)
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
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x04001B96 RID: 7062
		private long m_Offset;

		// Token: 0x04001B97 RID: 7063
		private long m_Size;

		// Token: 0x04001B98 RID: 7064
		private long m_Position;
	}
}
