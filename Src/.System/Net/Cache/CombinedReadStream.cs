using System;
using System.IO;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x0200031B RID: 795
	internal class CombinedReadStream : BaseWrapperStream, ICloseEx
	{
		// Token: 0x06001C43 RID: 7235 RVA: 0x000866E0 File Offset: 0x000848E0
		internal CombinedReadStream(Stream headStream, Stream tailStream)
			: base(tailStream)
		{
			this.m_HeadStream = headStream;
			this.m_HeadEOF = headStream == Stream.Null;
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x000866FE File Offset: 0x000848FE
		public override bool CanRead
		{
			get
			{
				if (!this.m_HeadEOF)
				{
					return this.m_HeadStream.CanRead;
				}
				return base.WrappedStream.CanRead;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x0008671F File Offset: 0x0008491F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x00086722 File Offset: 0x00084922
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00086725 File Offset: 0x00084925
		public override long Length
		{
			get
			{
				return base.WrappedStream.Length + (this.m_HeadEOF ? this.m_HeadLength : this.m_HeadStream.Length);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x0008674E File Offset: 0x0008494E
		// (set) Token: 0x06001C49 RID: 7241 RVA: 0x00086777 File Offset: 0x00084977
		public override long Position
		{
			get
			{
				return base.WrappedStream.Position + (this.m_HeadEOF ? this.m_HeadLength : this.m_HeadStream.Position);
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00086788 File Offset: 0x00084988
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00086799 File Offset: 0x00084999
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x000867AA File Offset: 0x000849AA
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000867BB File Offset: 0x000849BB
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000867CC File Offset: 0x000849CC
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000867DD File Offset: 0x000849DD
		public override void Flush()
		{
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x000867E0 File Offset: 0x000849E0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num;
			try
			{
				if (Interlocked.Increment(ref this.m_ReadNesting) != 1)
				{
					throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[] { "Read", "read" }));
				}
				if (this.m_HeadEOF)
				{
					num = base.WrappedStream.Read(buffer, offset, count);
				}
				else
				{
					int num2 = this.m_HeadStream.Read(buffer, offset, count);
					this.m_HeadLength += (long)num2;
					if (num2 == 0 && count != 0)
					{
						this.m_HeadEOF = true;
						this.m_HeadStream.Close();
						num2 = base.WrappedStream.Read(buffer, offset, count);
					}
					num = num2;
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.m_ReadNesting);
			}
			return num;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000868A4 File Offset: 0x00084AA4
		private void ReadCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			CombinedReadStream.InnerAsyncResult innerAsyncResult = transportResult.AsyncState as CombinedReadStream.InnerAsyncResult;
			try
			{
				int num;
				if (!this.m_HeadEOF)
				{
					num = this.m_HeadStream.EndRead(transportResult);
					this.m_HeadLength += (long)num;
				}
				else
				{
					num = base.WrappedStream.EndRead(transportResult);
				}
				if (!this.m_HeadEOF && num == 0 && innerAsyncResult.Count != 0)
				{
					this.m_HeadEOF = true;
					this.m_HeadStream.Close();
					IAsyncResult asyncResult = base.WrappedStream.BeginRead(innerAsyncResult.Buffer, innerAsyncResult.Offset, innerAsyncResult.Count, this.m_ReadCallback, innerAsyncResult);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					num = base.WrappedStream.EndRead(asyncResult);
				}
				innerAsyncResult.Buffer = null;
				innerAsyncResult.InvokeCallback(num);
			}
			catch (Exception ex)
			{
				if (innerAsyncResult.InternalPeekCompleted)
				{
					throw;
				}
				innerAsyncResult.InvokeCallback(ex);
			}
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x00086994 File Offset: 0x00084B94
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			try
			{
				if (Interlocked.Increment(ref this.m_ReadNesting) != 1)
				{
					throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[] { "BeginRead", "read" }));
				}
				if (this.m_ReadCallback == null)
				{
					this.m_ReadCallback = new AsyncCallback(this.ReadCallback);
				}
				if (this.m_HeadEOF)
				{
					asyncResult = base.WrappedStream.BeginRead(buffer, offset, count, callback, state);
				}
				else
				{
					CombinedReadStream.InnerAsyncResult innerAsyncResult = new CombinedReadStream.InnerAsyncResult(state, callback, buffer, offset, count);
					IAsyncResult asyncResult2 = this.m_HeadStream.BeginRead(buffer, offset, count, this.m_ReadCallback, innerAsyncResult);
					if (!asyncResult2.CompletedSynchronously)
					{
						asyncResult = innerAsyncResult;
					}
					else
					{
						int num = this.m_HeadStream.EndRead(asyncResult2);
						this.m_HeadLength += (long)num;
						if (num == 0 && innerAsyncResult.Count != 0)
						{
							this.m_HeadEOF = true;
							this.m_HeadStream.Close();
							asyncResult = base.WrappedStream.BeginRead(buffer, offset, count, callback, state);
						}
						else
						{
							innerAsyncResult.Buffer = null;
							innerAsyncResult.InvokeCallback(count);
							asyncResult = innerAsyncResult;
						}
					}
				}
			}
			catch
			{
				Interlocked.Decrement(ref this.m_ReadNesting);
				throw;
			}
			return asyncResult;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00086AD0 File Offset: 0x00084CD0
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (Interlocked.Decrement(ref this.m_ReadNesting) != 0)
			{
				Interlocked.Increment(ref this.m_ReadNesting);
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndRead" }));
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			CombinedReadStream.InnerAsyncResult innerAsyncResult = asyncResult as CombinedReadStream.InnerAsyncResult;
			if (innerAsyncResult == null)
			{
				if (!this.m_HeadEOF)
				{
					return this.m_HeadStream.EndRead(asyncResult);
				}
				return base.WrappedStream.EndRead(asyncResult);
			}
			else
			{
				innerAsyncResult.InternalWaitForCompletion();
				if (innerAsyncResult.Result is Exception)
				{
					throw (Exception)innerAsyncResult.Result;
				}
				return (int)innerAsyncResult.Result;
			}
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00086B79 File Offset: 0x00084D79
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00086B83 File Offset: 0x00084D83
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			this.Dispose(true, closeState);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00086B90 File Offset: 0x00084D90
		protected virtual void Dispose(bool disposing, CloseExState closeState)
		{
			try
			{
				if (disposing)
				{
					try
					{
						if (!this.m_HeadEOF)
						{
							ICloseEx closeEx = this.m_HeadStream as ICloseEx;
							if (closeEx != null)
							{
								closeEx.CloseEx(closeState);
							}
							else
							{
								this.m_HeadStream.Close();
							}
						}
					}
					finally
					{
						ICloseEx closeEx2 = base.WrappedStream as ICloseEx;
						if (closeEx2 != null)
						{
							closeEx2.CloseEx(closeState);
						}
						else
						{
							base.WrappedStream.Close();
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00086C18 File Offset: 0x00084E18
		public override bool CanTimeout
		{
			get
			{
				return base.WrappedStream.CanTimeout && this.m_HeadStream.CanTimeout;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00086C34 File Offset: 0x00084E34
		// (set) Token: 0x06001C59 RID: 7257 RVA: 0x00086C58 File Offset: 0x00084E58
		public override int ReadTimeout
		{
			get
			{
				if (!this.m_HeadEOF)
				{
					return this.m_HeadStream.ReadTimeout;
				}
				return base.WrappedStream.ReadTimeout;
			}
			set
			{
				Stream wrappedStream = base.WrappedStream;
				this.m_HeadStream.ReadTimeout = value;
				wrappedStream.ReadTimeout = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00086C7F File Offset: 0x00084E7F
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x00086CA0 File Offset: 0x00084EA0
		public override int WriteTimeout
		{
			get
			{
				if (!this.m_HeadEOF)
				{
					return this.m_HeadStream.WriteTimeout;
				}
				return base.WrappedStream.WriteTimeout;
			}
			set
			{
				Stream wrappedStream = base.WrappedStream;
				this.m_HeadStream.WriteTimeout = value;
				wrappedStream.WriteTimeout = value;
			}
		}

		// Token: 0x04001B7E RID: 7038
		private Stream m_HeadStream;

		// Token: 0x04001B7F RID: 7039
		private bool m_HeadEOF;

		// Token: 0x04001B80 RID: 7040
		private long m_HeadLength;

		// Token: 0x04001B81 RID: 7041
		private int m_ReadNesting;

		// Token: 0x04001B82 RID: 7042
		private AsyncCallback m_ReadCallback;

		// Token: 0x020007B8 RID: 1976
		private class InnerAsyncResult : LazyAsyncResult
		{
			// Token: 0x06004317 RID: 17175 RVA: 0x001191A8 File Offset: 0x001173A8
			public InnerAsyncResult(object userState, AsyncCallback userCallback, byte[] buffer, int offset, int count)
				: base(null, userState, userCallback)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
			}

			// Token: 0x04003435 RID: 13365
			public byte[] Buffer;

			// Token: 0x04003436 RID: 13366
			public int Offset;

			// Token: 0x04003437 RID: 13367
			public int Count;
		}
	}
}
