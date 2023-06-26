using System;
using System.IO;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x0200031C RID: 796
	internal class ForwardingReadStream : BaseWrapperStream, ICloseEx
	{
		// Token: 0x06001C5C RID: 7260 RVA: 0x00086CC7 File Offset: 0x00084EC7
		internal ForwardingReadStream(Stream originalStream, Stream shadowStream, long bytesToSkip, bool throwOnWriteError)
			: base(originalStream)
		{
			if (!shadowStream.CanWrite)
			{
				throw new ArgumentException(SR.GetString("net_cache_shadowstream_not_writable"), "shadowStream");
			}
			this.m_ShadowStream = shadowStream;
			this.m_BytesToSkip = bytesToSkip;
			this.m_ThrowOnWriteError = throwOnWriteError;
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00086D03 File Offset: 0x00084F03
		public override bool CanRead
		{
			get
			{
				return base.WrappedStream.CanRead;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00086D10 File Offset: 0x00084F10
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00086D13 File Offset: 0x00084F13
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00086D16 File Offset: 0x00084F16
		public override long Length
		{
			get
			{
				return base.WrappedStream.Length - this.m_BytesToSkip;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00086D2A File Offset: 0x00084F2A
		// (set) Token: 0x06001C62 RID: 7266 RVA: 0x00086D3E File Offset: 0x00084F3E
		public override long Position
		{
			get
			{
				return base.WrappedStream.Position - this.m_BytesToSkip;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00086D4F File Offset: 0x00084F4F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00086D60 File Offset: 0x00084F60
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x00086D71 File Offset: 0x00084F71
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x00086D82 File Offset: 0x00084F82
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00086D93 File Offset: 0x00084F93
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00086DA4 File Offset: 0x00084FA4
		public override void Flush()
		{
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00086DA8 File Offset: 0x00084FA8
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool flag = false;
			int num = -1;
			if (Interlocked.Increment(ref this.m_ReadNesting) != 1)
			{
				throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[] { "Read", "read" }));
			}
			int num3;
			try
			{
				if (this.m_BytesToSkip != 0L)
				{
					byte[] array = new byte[4096];
					while (this.m_BytesToSkip != 0L)
					{
						int num2 = base.WrappedStream.Read(array, 0, (this.m_BytesToSkip < (long)array.Length) ? ((int)this.m_BytesToSkip) : array.Length);
						if (num2 == 0)
						{
							this.m_SeenReadEOF = true;
						}
						this.m_BytesToSkip -= (long)num2;
						if (!this.m_ShadowStreamIsDead)
						{
							this.m_ShadowStream.Write(array, 0, num2);
						}
					}
				}
				num = base.WrappedStream.Read(buffer, offset, count);
				if (num == 0)
				{
					this.m_SeenReadEOF = true;
				}
				if (this.m_ShadowStreamIsDead)
				{
					num3 = num;
				}
				else
				{
					flag = true;
					this.m_ShadowStream.Write(buffer, offset, num);
					num3 = num;
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!this.m_ShadowStreamIsDead)
				{
					this.m_ShadowStreamIsDead = true;
					try
					{
						if (this.m_ShadowStream is ICloseEx)
						{
							((ICloseEx)this.m_ShadowStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
						}
						else
						{
							this.m_ShadowStream.Close();
						}
					}
					catch (Exception ex2)
					{
						if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
						{
							throw;
						}
					}
				}
				if (!flag || this.m_ThrowOnWriteError)
				{
					throw;
				}
				num3 = num;
			}
			finally
			{
				Interlocked.Decrement(ref this.m_ReadNesting);
			}
			return num3;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00086F8C File Offset: 0x0008518C
		private void ReadCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			ForwardingReadStream.InnerAsyncResult innerAsyncResult = transportResult.AsyncState as ForwardingReadStream.InnerAsyncResult;
			this.ReadComplete(transportResult);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00086FB8 File Offset: 0x000851B8
		private void ReadComplete(IAsyncResult transportResult)
		{
			for (;;)
			{
				ForwardingReadStream.InnerAsyncResult innerAsyncResult = transportResult.AsyncState as ForwardingReadStream.InnerAsyncResult;
				try
				{
					if (!innerAsyncResult.IsWriteCompletion)
					{
						innerAsyncResult.Count = base.WrappedStream.EndRead(transportResult);
						if (innerAsyncResult.Count == 0)
						{
							this.m_SeenReadEOF = true;
						}
						if (!this.m_ShadowStreamIsDead)
						{
							innerAsyncResult.IsWriteCompletion = true;
							transportResult = this.m_ShadowStream.BeginWrite(innerAsyncResult.Buffer, innerAsyncResult.Offset, innerAsyncResult.Count, this.m_ReadCallback, innerAsyncResult);
							if (transportResult.CompletedSynchronously)
							{
								continue;
							}
							break;
						}
					}
					else
					{
						this.m_ShadowStream.EndWrite(transportResult);
						innerAsyncResult.IsWriteCompletion = false;
					}
				}
				catch (Exception ex)
				{
					if (innerAsyncResult.InternalPeekCompleted)
					{
						throw;
					}
					try
					{
						this.m_ShadowStreamIsDead = true;
						if (this.m_ShadowStream is ICloseEx)
						{
							((ICloseEx)this.m_ShadowStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
						}
						else
						{
							this.m_ShadowStream.Close();
						}
					}
					catch (Exception ex2)
					{
					}
					if (!innerAsyncResult.IsWriteCompletion || this.m_ThrowOnWriteError)
					{
						if (transportResult.CompletedSynchronously)
						{
							throw;
						}
						innerAsyncResult.InvokeCallback(ex);
						break;
					}
				}
				try
				{
					if (this.m_BytesToSkip != 0L)
					{
						this.m_BytesToSkip -= (long)innerAsyncResult.Count;
						innerAsyncResult.Count = ((this.m_BytesToSkip < (long)innerAsyncResult.Buffer.Length) ? ((int)this.m_BytesToSkip) : innerAsyncResult.Buffer.Length);
						if (this.m_BytesToSkip == 0L)
						{
							transportResult = innerAsyncResult;
							innerAsyncResult = innerAsyncResult.AsyncState as ForwardingReadStream.InnerAsyncResult;
						}
						transportResult = base.WrappedStream.BeginRead(innerAsyncResult.Buffer, innerAsyncResult.Offset, innerAsyncResult.Count, this.m_ReadCallback, innerAsyncResult);
						if (transportResult.CompletedSynchronously)
						{
							continue;
						}
					}
					else
					{
						innerAsyncResult.InvokeCallback(innerAsyncResult.Count);
					}
				}
				catch (Exception ex3)
				{
					if (innerAsyncResult.InternalPeekCompleted)
					{
						throw;
					}
					try
					{
						this.m_ShadowStreamIsDead = true;
						if (this.m_ShadowStream is ICloseEx)
						{
							((ICloseEx)this.m_ShadowStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
						}
						else
						{
							this.m_ShadowStream.Close();
						}
					}
					catch (Exception ex4)
					{
					}
					if (transportResult.CompletedSynchronously)
					{
						throw;
					}
					innerAsyncResult.InvokeCallback(ex3);
				}
				break;
			}
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x000871F0 File Offset: 0x000853F0
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (Interlocked.Increment(ref this.m_ReadNesting) != 1)
			{
				throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[] { "BeginRead", "read" }));
			}
			IAsyncResult asyncResult;
			try
			{
				if (this.m_ReadCallback == null)
				{
					this.m_ReadCallback = new AsyncCallback(this.ReadCallback);
				}
				if (this.m_ShadowStreamIsDead && this.m_BytesToSkip == 0L)
				{
					asyncResult = base.WrappedStream.BeginRead(buffer, offset, count, callback, state);
				}
				else
				{
					ForwardingReadStream.InnerAsyncResult innerAsyncResult = new ForwardingReadStream.InnerAsyncResult(state, callback, buffer, offset, count);
					if (this.m_BytesToSkip != 0L)
					{
						ForwardingReadStream.InnerAsyncResult innerAsyncResult2 = innerAsyncResult;
						innerAsyncResult = new ForwardingReadStream.InnerAsyncResult(innerAsyncResult2, null, new byte[4096], 0, (this.m_BytesToSkip < (long)buffer.Length) ? ((int)this.m_BytesToSkip) : buffer.Length);
					}
					IAsyncResult asyncResult2 = base.WrappedStream.BeginRead(innerAsyncResult.Buffer, innerAsyncResult.Offset, innerAsyncResult.Count, this.m_ReadCallback, innerAsyncResult);
					if (asyncResult2.CompletedSynchronously)
					{
						this.ReadComplete(asyncResult2);
					}
					asyncResult = innerAsyncResult;
				}
			}
			catch
			{
				Interlocked.Decrement(ref this.m_ReadNesting);
				throw;
			}
			return asyncResult;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00087310 File Offset: 0x00085510
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
			ForwardingReadStream.InnerAsyncResult innerAsyncResult = asyncResult as ForwardingReadStream.InnerAsyncResult;
			if (innerAsyncResult == null && base.WrappedStream.EndRead(asyncResult) == 0)
			{
				this.m_SeenReadEOF = true;
			}
			bool flag = false;
			try
			{
				innerAsyncResult.InternalWaitForCompletion();
				if (innerAsyncResult.Result is Exception)
				{
					throw (Exception)innerAsyncResult.Result;
				}
				flag = true;
			}
			finally
			{
				if (!flag && !this.m_ShadowStreamIsDead)
				{
					this.m_ShadowStreamIsDead = true;
					if (this.m_ShadowStream is ICloseEx)
					{
						((ICloseEx)this.m_ShadowStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
					}
					else
					{
						this.m_ShadowStream.Close();
					}
				}
			}
			return (int)innerAsyncResult.Result;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00087404 File Offset: 0x00085604
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00087410 File Offset: 0x00085610
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			if (Interlocked.Increment(ref this._Disposed) == 1)
			{
				if (closeState == CloseExState.Silent)
				{
					try
					{
						int num = 0;
						int num2;
						while (num < ConnectStream.s_DrainingBuffer.Length && (num2 = this.Read(ConnectStream.s_DrainingBuffer, 0, ConnectStream.s_DrainingBuffer.Length)) > 0)
						{
							num += num2;
						}
					}
					catch (Exception ex)
					{
						if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
						{
							throw;
						}
					}
				}
				this.Dispose(true, closeState);
			}
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x00087490 File Offset: 0x00085690
		protected virtual void Dispose(bool disposing, CloseExState closeState)
		{
			try
			{
				if (disposing)
				{
					try
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
					finally
					{
						if (!this.m_SeenReadEOF)
						{
							closeState |= CloseExState.Abort;
						}
						if (this.m_ShadowStream is ICloseEx)
						{
							((ICloseEx)this.m_ShadowStream).CloseEx(closeState);
						}
						else
						{
							this.m_ShadowStream.Close();
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x00087524 File Offset: 0x00085724
		public override bool CanTimeout
		{
			get
			{
				return base.WrappedStream.CanTimeout && this.m_ShadowStream.CanTimeout;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00087540 File Offset: 0x00085740
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x00087550 File Offset: 0x00085750
		public override int ReadTimeout
		{
			get
			{
				return base.WrappedStream.ReadTimeout;
			}
			set
			{
				Stream wrappedStream = base.WrappedStream;
				this.m_ShadowStream.ReadTimeout = value;
				wrappedStream.ReadTimeout = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00087577 File Offset: 0x00085777
		// (set) Token: 0x06001C75 RID: 7285 RVA: 0x00087584 File Offset: 0x00085784
		public override int WriteTimeout
		{
			get
			{
				return this.m_ShadowStream.WriteTimeout;
			}
			set
			{
				Stream wrappedStream = base.WrappedStream;
				this.m_ShadowStream.WriteTimeout = value;
				wrappedStream.WriteTimeout = value;
			}
		}

		// Token: 0x04001B83 RID: 7043
		private Stream m_ShadowStream;

		// Token: 0x04001B84 RID: 7044
		private int m_ReadNesting;

		// Token: 0x04001B85 RID: 7045
		private bool m_ShadowStreamIsDead;

		// Token: 0x04001B86 RID: 7046
		private AsyncCallback m_ReadCallback;

		// Token: 0x04001B87 RID: 7047
		private long m_BytesToSkip;

		// Token: 0x04001B88 RID: 7048
		private bool m_ThrowOnWriteError;

		// Token: 0x04001B89 RID: 7049
		private bool m_SeenReadEOF;

		// Token: 0x04001B8A RID: 7050
		private int _Disposed;

		// Token: 0x020007B9 RID: 1977
		private class InnerAsyncResult : LazyAsyncResult
		{
			// Token: 0x06004318 RID: 17176 RVA: 0x001191CA File Offset: 0x001173CA
			public InnerAsyncResult(object userState, AsyncCallback userCallback, byte[] buffer, int offset, int count)
				: base(null, userState, userCallback)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
			}

			// Token: 0x04003438 RID: 13368
			public byte[] Buffer;

			// Token: 0x04003439 RID: 13369
			public int Offset;

			// Token: 0x0400343A RID: 13370
			public int Count;

			// Token: 0x0400343B RID: 13371
			public bool IsWriteCompletion;
		}
	}
}
