using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020001A6 RID: 422
	internal class ConnectStream : Stream, ICloseEx, IRequestLifetimeTracker
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x000574A4 File Offset: 0x000556A4
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000574A7 File Offset: 0x000556A7
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x000574AF File Offset: 0x000556AF
		public override int ReadTimeout
		{
			get
			{
				return this.m_ReadTimeout;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
				}
				this.m_ReadTimeout = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x000574D5 File Offset: 0x000556D5
		// (set) Token: 0x06001052 RID: 4178 RVA: 0x000574DD File Offset: 0x000556DD
		public override int WriteTimeout
		{
			get
			{
				return this.m_WriteTimeout;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
				}
				this.m_WriteTimeout = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00057503 File Offset: 0x00055703
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x0005750B File Offset: 0x0005570B
		internal bool FinishedAfterWrite { get; set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00057514 File Offset: 0x00055714
		internal bool IgnoreSocketErrors
		{
			get
			{
				return this.m_IgnoreSocketErrors;
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0005751C File Offset: 0x0005571C
		internal void ErrorResponseNotify(bool isKeepAlive)
		{
			this.m_ErrorResponseStatus = true;
			this.m_IgnoreSocketErrors |= !isKeepAlive;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00057536 File Offset: 0x00055736
		internal void FatalResponseNotify()
		{
			if (this.m_ErrorException == null)
			{
				Interlocked.CompareExchange<Exception>(ref this.m_ErrorException, new IOException(SR.GetString("net_io_readfailure", new object[] { SR.GetString("net_io_connectionclosed") })), null);
			}
			this.m_ErrorResponseStatus = false;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00057578 File Offset: 0x00055778
		public ConnectStream(Connection connection, HttpWebRequest request)
		{
			this.m_Connection = connection;
			this.m_ReadTimeout = (this.m_WriteTimeout = -1);
			this.m_Request = request;
			this.m_HttpWriteMode = request.HttpWriteMode;
			this.m_BytesLeftToWrite = ((this.m_HttpWriteMode == HttpWriteMode.ContentLength) ? request.ContentLength : (-1L));
			if (request.HttpWriteMode == HttpWriteMode.Buffer)
			{
				this.m_BufferOnly = true;
				this.EnableWriteBuffering();
			}
			this.m_ReadCallbackDelegate = new AsyncCallback(this.ReadCallback);
			this.m_WriteCallbackDelegate = new AsyncCallback(this.WriteCallback);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00057608 File Offset: 0x00055808
		public ConnectStream(Connection connection, byte[] buffer, int offset, int bufferCount, long readCount, bool chunked, HttpWebRequest request)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, "ConnectStream", SR.GetString("net_log_buffered_n_bytes", new object[] { readCount }));
			}
			this.m_ReadBytes = readCount;
			this.m_ReadTimeout = (this.m_WriteTimeout = -1);
			this.m_Chunked = chunked;
			this.m_Connection = connection;
			if (this.m_Chunked)
			{
				this.m_ChunkParser = new ChunkParser(this.m_Connection, buffer, offset, bufferCount, request.MaximumResponseHeadersLength * 1024);
			}
			else
			{
				this.m_ReadBuffer = buffer;
				this.m_ReadOffset = offset;
				this.m_ReadBufferSize = bufferCount;
			}
			this.m_Request = request;
			this.m_ReadCallbackDelegate = new AsyncCallback(this.ReadCallback);
			this.m_WriteCallbackDelegate = new AsyncCallback(this.WriteCallback);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x000576E1 File Offset: 0x000558E1
		internal void SwitchToContentLength()
		{
			this.m_HttpWriteMode = HttpWriteMode.ContentLength;
		}

		// Token: 0x1700038C RID: 908
		// (set) Token: 0x0600105B RID: 4187 RVA: 0x000576EA File Offset: 0x000558EA
		internal bool SuppressWrite
		{
			set
			{
				this.m_SuppressWrite = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x000576F3 File Offset: 0x000558F3
		internal Connection Connection
		{
			get
			{
				return this.m_Connection;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x000576FB File Offset: 0x000558FB
		internal bool BufferOnly
		{
			get
			{
				return this.m_BufferOnly;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00057703 File Offset: 0x00055903
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x0005770B File Offset: 0x0005590B
		internal ScatterGatherBuffers BufferedData
		{
			get
			{
				return this.m_BufferedData;
			}
			set
			{
				this.m_BufferedData = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00057714 File Offset: 0x00055914
		private bool WriteChunked
		{
			get
			{
				return this.m_HttpWriteMode == HttpWriteMode.Chunked;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0005771F File Offset: 0x0005591F
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x00057727 File Offset: 0x00055927
		internal long BytesLeftToWrite
		{
			get
			{
				return this.m_BytesLeftToWrite;
			}
			set
			{
				this.m_BytesLeftToWrite = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00057730 File Offset: 0x00055930
		private bool WriteStream
		{
			get
			{
				return this.m_HttpWriteMode > HttpWriteMode.Unknown;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0005773B File Offset: 0x0005593B
		internal bool IsPostStream
		{
			get
			{
				return this.m_HttpWriteMode != HttpWriteMode.None;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00057749 File Offset: 0x00055949
		internal bool ErrorInStream
		{
			get
			{
				return this.m_ErrorException != null;
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00057754 File Offset: 0x00055954
		internal void CallDone()
		{
			this.CallDone(null);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00057760 File Offset: 0x00055960
		private void CallDone(ConnectionReturnResult returnResult)
		{
			if (Interlocked.Increment(ref this.m_DoneCalled) == 1)
			{
				if (!this.WriteStream)
				{
					if (returnResult == null)
					{
						byte[] array;
						int num;
						int num2;
						if (this.m_Chunked && this.m_ChunkParser.TryGetLeftoverBytes(out array, out num, out num2))
						{
							this.m_Connection.SetLeftoverBytes(array, num, num2);
						}
						this.m_Connection.ReadStartNextRequest(this.m_Request, ref returnResult);
						return;
					}
					ConnectionReturnResult.SetResponses(returnResult);
					return;
				}
				else
				{
					this.m_Request.WriteCallDone(this, returnResult);
				}
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000577D8 File Offset: 0x000559D8
		internal void ProcessWriteCallDone(ConnectionReturnResult returnResult)
		{
			try
			{
				if (returnResult == null)
				{
					this.m_Connection.WriteStartNextRequest(this.m_Request, ref returnResult);
					if (!this.m_Request.Async && this.m_Request.ConnectionReaderAsyncResult.InternalWaitForCompletion() == null && this.m_Request.NeedsToReadForResponse)
					{
						this.m_Connection.SyncRead(this.m_Request, true, false);
					}
					this.m_Request.NeedsToReadForResponse = true;
				}
				ConnectionReturnResult.SetResponses(returnResult);
			}
			finally
			{
				if (this.IsPostStream || this.m_Request.Async)
				{
					this.m_Request.CheckWriteSideResponseProcessing();
				}
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00057884 File Offset: 0x00055A84
		internal bool IsClosed
		{
			get
			{
				return this.m_ShutDown != 0;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0005788F File Offset: 0x00055A8F
		public override bool CanRead
		{
			get
			{
				return !this.WriteStream && !this.IsClosed;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x000578A4 File Offset: 0x00055AA4
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x000578A7 File Offset: 0x00055AA7
		public override bool CanWrite
		{
			get
			{
				return this.WriteStream && !this.IsClosed;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x000578BC File Offset: 0x00055ABC
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x000578CD File Offset: 0x00055ACD
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x000578DE File Offset: 0x00055ADE
		public override long Position
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x000578F0 File Offset: 0x00055AF0
		internal bool Eof
		{
			get
			{
				if (this.ErrorInStream)
				{
					return true;
				}
				if (this.m_Chunked)
				{
					return this.m_ChunkEofRecvd;
				}
				return this.m_ReadBytes == 0L || (this.m_ReadBytes == -1L && this.m_DoneCalled > 0 && this.m_ReadBufferSize <= 0);
			}
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00057944 File Offset: 0x00055B44
		internal void ResubmitWrite(ConnectStream oldStream, bool suppressWrite)
		{
			try
			{
				Interlocked.CompareExchange(ref this.m_CallNesting, 4, 0);
				ScatterGatherBuffers bufferedData = oldStream.BufferedData;
				this.SafeSetSocketTimeout(SocketShutdown.Send);
				if (!this.WriteChunked)
				{
					if (!suppressWrite)
					{
						this.m_Connection.Write(bufferedData);
					}
				}
				else
				{
					this.m_HttpWriteMode = HttpWriteMode.ContentLength;
					if (bufferedData.Length == 0)
					{
						this.m_Connection.Write(NclConstants.ChunkTerminator, 0, NclConstants.ChunkTerminator.Length);
					}
					else
					{
						int num = 0;
						byte[] chunkHeader = ConnectStream.GetChunkHeader(bufferedData.Length, out num);
						BufferOffsetSize[] buffers = bufferedData.GetBuffers();
						BufferOffsetSize[] array = new BufferOffsetSize[buffers.Length + 3];
						array[0] = new BufferOffsetSize(chunkHeader, num, chunkHeader.Length - num, false);
						int num2 = 0;
						foreach (BufferOffsetSize bufferOffsetSize in buffers)
						{
							array[++num2] = bufferOffsetSize;
						}
						array[++num2] = new BufferOffsetSize(NclConstants.CRLF, 0, NclConstants.CRLF.Length, false);
						array[num2 + 1] = new BufferOffsetSize(NclConstants.ChunkTerminator, 0, NclConstants.ChunkTerminator.Length, false);
						SplitWritesState splitWritesState = new SplitWritesState(array);
						for (BufferOffsetSize[] array3 = splitWritesState.GetNextBuffers(); array3 != null; array3 = splitWritesState.GetNextBuffers())
						{
							this.m_Connection.MultipleWrite(array3);
						}
					}
				}
				if (Logging.On && bufferedData.GetBuffers() != null)
				{
					foreach (BufferOffsetSize bufferOffsetSize2 in bufferedData.GetBuffers())
					{
						if (bufferOffsetSize2 == null)
						{
							Logging.Dump(Logging.Web, this, "ResubmitWrite", null, 0, 0);
						}
						else
						{
							Logging.Dump(Logging.Web, this, "ResubmitWrite", bufferOffsetSize2.Buffer, bufferOffsetSize2.Offset, bufferOffsetSize2.Size);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
				WebException ex2 = new WebException(NetRes.GetWebStatusString("net_connclosed", WebExceptionStatus.SendFailure), WebExceptionStatus.SendFailure, WebExceptionInternalStatus.RequestFatal, ex);
				this.IOError(ex2, false);
			}
			finally
			{
				Interlocked.CompareExchange(ref this.m_CallNesting, 0, 4);
			}
			this.m_BytesLeftToWrite = 0L;
			this.CallDone();
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00057B70 File Offset: 0x00055D70
		internal void EnableWriteBuffering()
		{
			if (this.BufferedData == null)
			{
				if (this.WriteChunked)
				{
					this.BufferedData = new ScatterGatherBuffers();
					return;
				}
				this.BufferedData = new ScatterGatherBuffers(this.BytesLeftToWrite);
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00057BA0 File Offset: 0x00055DA0
		internal int FillFromBufferedData(byte[] buffer, ref int offset, ref int size)
		{
			if (this.m_ReadBufferSize == 0)
			{
				return 0;
			}
			int num = Math.Min(size, this.m_ReadBufferSize);
			Buffer.BlockCopy(this.m_ReadBuffer, this.m_ReadOffset, buffer, offset, num);
			this.m_ReadOffset += num;
			this.m_ReadBufferSize -= num;
			if (this.m_ReadBufferSize == 0)
			{
				this.m_ReadBuffer = null;
			}
			size -= num;
			offset += num;
			return num;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00057C14 File Offset: 0x00055E14
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "Write", "");
			}
			if (!this.WriteStream)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (Logging.On)
			{
				Logging.Dump(Logging.Web, this, "Write", buffer, offset, size);
			}
			this.InternalWrite(false, buffer, offset, size, null, null);
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "Write", "");
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00057CD4 File Offset: 0x00055ED4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "BeginWrite", "");
			}
			if (!this.WriteStream)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (Logging.On)
			{
				Logging.Dump(Logging.Web, this, "BeginWrite", buffer, offset, size);
			}
			IAsyncResult asyncResult = this.InternalWrite(true, buffer, offset, size, callback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "BeginWrite", asyncResult);
			}
			return asyncResult;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00057D94 File Offset: 0x00055F94
		private IAsyncResult InternalWrite(bool async, byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			if (this.ErrorInStream)
			{
				throw this.m_ErrorException;
			}
			if (this.IsClosed && !this.IgnoreSocketErrors)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.ConnectionClosed), WebExceptionStatus.ConnectionClosed);
			}
			if (this.m_Request.Aborted && !this.IgnoreSocketErrors)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
			int num = Interlocked.CompareExchange(ref this.m_CallNesting, 1, 0);
			if (num != 0 && num != 2)
			{
				throw new NotSupportedException(SR.GetString("net_no_concurrent_io_allowed"));
			}
			if (this.BufferedData != null && size != 0 && (this.m_Request.ContentLength != 0L || !this.IsPostStream || !this.m_Request.NtlmKeepAlive))
			{
				this.BufferedData.Write(buffer, offset, size);
			}
			LazyAsyncResult lazyAsyncResult = null;
			bool flag = false;
			IAsyncResult asyncResult;
			try
			{
				if (size == 0 || this.BufferOnly || this.m_SuppressWrite || this.IgnoreSocketErrors)
				{
					if (this.m_SuppressWrite && this.m_BytesLeftToWrite > 0L && size > 0)
					{
						this.m_BytesLeftToWrite -= (long)size;
					}
					if (async)
					{
						lazyAsyncResult = new LazyAsyncResult(this, state, callback);
						flag = true;
					}
					asyncResult = lazyAsyncResult;
				}
				else if (this.WriteChunked)
				{
					int num2 = 0;
					byte[] chunkHeader = ConnectStream.GetChunkHeader(size, out num2);
					BufferOffsetSize[] array;
					if (this.m_ErrorResponseStatus)
					{
						this.m_IgnoreSocketErrors = true;
						array = new BufferOffsetSize[]
						{
							new BufferOffsetSize(NclConstants.ChunkTerminator, 0, NclConstants.ChunkTerminator.Length, false)
						};
					}
					else
					{
						array = new BufferOffsetSize[]
						{
							new BufferOffsetSize(chunkHeader, num2, chunkHeader.Length - num2, false),
							new BufferOffsetSize(buffer, offset, size, false),
							new BufferOffsetSize(NclConstants.CRLF, 0, NclConstants.CRLF.Length, false)
						};
					}
					lazyAsyncResult = (async ? new NestedMultipleAsyncResult(this, state, callback, array) : null);
					try
					{
						if (async)
						{
							this.m_Connection.BeginMultipleWrite(array, this.m_WriteCallbackDelegate, lazyAsyncResult);
						}
						else
						{
							this.SafeSetSocketTimeout(SocketShutdown.Send);
							this.m_Connection.MultipleWrite(array);
						}
					}
					catch (Exception ex)
					{
						if (this.IgnoreSocketErrors && !NclUtilities.IsFatal(ex))
						{
							if (async)
							{
								flag = true;
							}
							return lazyAsyncResult;
						}
						if (this.m_Request.Aborted && (ex is IOException || ex is ObjectDisposedException))
						{
							throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
						}
						num = 3;
						if (NclUtilities.IsFatal(ex))
						{
							this.m_ErrorResponseStatus = false;
							this.IOError(ex);
							throw;
						}
						if (!this.m_ErrorResponseStatus)
						{
							this.IOError(ex);
							throw;
						}
						this.m_IgnoreSocketErrors = true;
						if (async)
						{
							flag = true;
						}
					}
					asyncResult = lazyAsyncResult;
				}
				else
				{
					lazyAsyncResult = (async ? new NestedSingleAsyncResult(this, state, callback, buffer, offset, size) : null);
					if (this.BytesLeftToWrite != -1L)
					{
						if (this.BytesLeftToWrite < (long)size)
						{
							throw new ProtocolViolationException(SR.GetString("net_entitytoobig"));
						}
						if (!async)
						{
							this.m_BytesLeftToWrite -= (long)size;
						}
					}
					try
					{
						if (async)
						{
							if (this.m_Request.ContentLength == 0L && this.IsPostStream)
							{
								this.m_BytesLeftToWrite -= (long)size;
								flag = true;
							}
							else
							{
								this.m_BytesAlreadyTransferred = size;
								this.m_Connection.BeginWrite(buffer, offset, size, this.m_WriteCallbackDelegate, lazyAsyncResult);
							}
						}
						else
						{
							this.SafeSetSocketTimeout(SocketShutdown.Send);
							if (this.m_Request.ContentLength != 0L || !this.IsPostStream || !this.m_Request.NtlmKeepAlive)
							{
								this.m_Connection.Write(buffer, offset, size);
							}
						}
					}
					catch (Exception ex2)
					{
						if (this.IgnoreSocketErrors && !NclUtilities.IsFatal(ex2))
						{
							if (async)
							{
								flag = true;
							}
							return lazyAsyncResult;
						}
						if (this.m_Request.Aborted && (ex2 is IOException || ex2 is ObjectDisposedException))
						{
							throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
						}
						num = 3;
						if (NclUtilities.IsFatal(ex2))
						{
							this.m_ErrorResponseStatus = false;
							this.IOError(ex2);
							throw;
						}
						if (!this.m_ErrorResponseStatus)
						{
							this.IOError(ex2);
							throw;
						}
						this.m_IgnoreSocketErrors = true;
						if (async)
						{
							flag = true;
						}
					}
					asyncResult = lazyAsyncResult;
				}
			}
			finally
			{
				if (!async || num == 3 || flag)
				{
					num = Interlocked.CompareExchange(ref this.m_CallNesting, (num == 3) ? 3 : 0, 1);
					if (num == 2)
					{
						this.ResumeInternalClose(lazyAsyncResult);
					}
					else if (flag && lazyAsyncResult != null)
					{
						lazyAsyncResult.InvokeCallback();
					}
				}
			}
			return asyncResult;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00058214 File Offset: 0x00056414
		private void WriteCallback(IAsyncResult asyncResult)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult.AsyncState;
			((ConnectStream)lazyAsyncResult.AsyncObject).ProcessWriteCallback(asyncResult, lazyAsyncResult);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00058240 File Offset: 0x00056440
		private void ProcessWriteCallback(IAsyncResult asyncResult, LazyAsyncResult userResult)
		{
			Exception ex = null;
			try
			{
				NestedSingleAsyncResult nestedSingleAsyncResult = userResult as NestedSingleAsyncResult;
				if (nestedSingleAsyncResult != null)
				{
					try
					{
						this.m_Connection.EndWrite(asyncResult);
						if (this.BytesLeftToWrite != -1L)
						{
							this.m_BytesLeftToWrite -= (long)this.m_BytesAlreadyTransferred;
							this.m_BytesAlreadyTransferred = 0;
						}
						return;
					}
					catch (Exception ex2)
					{
						ex = ex2;
						if (NclUtilities.IsFatal(ex2))
						{
							this.m_ErrorResponseStatus = false;
							this.IOError(ex2);
							throw;
						}
						if (this.m_ErrorResponseStatus)
						{
							this.m_IgnoreSocketErrors = true;
							ex = null;
						}
						return;
					}
				}
				NestedMultipleAsyncResult nestedMultipleAsyncResult = (NestedMultipleAsyncResult)userResult;
				try
				{
					this.m_Connection.EndMultipleWrite(asyncResult);
				}
				catch (Exception ex3)
				{
					ex = ex3;
					if (NclUtilities.IsFatal(ex3))
					{
						this.m_ErrorResponseStatus = false;
						this.IOError(ex3);
						throw;
					}
					if (this.m_ErrorResponseStatus)
					{
						this.m_IgnoreSocketErrors = true;
						ex = null;
					}
				}
			}
			finally
			{
				if (2 == this.ExchangeCallNesting((ex == null) ? 0 : 3, 1))
				{
					if (ex != null && this.m_ErrorException == null)
					{
						Interlocked.CompareExchange<Exception>(ref this.m_ErrorException, ex, null);
					}
					this.ResumeInternalClose(userResult);
				}
				else
				{
					userResult.InvokeCallback(ex);
				}
			}
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0005836C File Offset: 0x0005656C
		private int ExchangeCallNesting(int value, int comparand)
		{
			return Interlocked.CompareExchange(ref this.m_CallNesting, value, comparand);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00058388 File Offset: 0x00056588
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "EndWrite", "");
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null || lazyAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndWrite" }));
			}
			lazyAsyncResult.EndCalled = true;
			object obj = lazyAsyncResult.InternalWaitForCompletion();
			if (this.ErrorInStream)
			{
				throw this.m_ErrorException;
			}
			Exception ex = obj as Exception;
			if (ex == null)
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "EndWrite", "");
				}
				return;
			}
			if (ex is IOException && this.m_Request.Aborted)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
			this.IOError(ex);
			throw ex;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00058480 File Offset: 0x00056680
		public override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "Read", "");
			}
			if (this.WriteStream)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (this.ErrorInStream)
			{
				throw this.m_ErrorException;
			}
			if (this.IsClosed)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.ConnectionClosed), WebExceptionStatus.ConnectionClosed);
			}
			if (this.m_Request.Aborted)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
			int num = Interlocked.CompareExchange(ref this.m_CallNesting, 1, 0);
			if (num != 0)
			{
				throw new NotSupportedException(SR.GetString("net_no_concurrent_io_allowed"));
			}
			int num2 = -1;
			try
			{
				this.SafeSetSocketTimeout(SocketShutdown.Receive);
			}
			catch (Exception ex)
			{
				this.IOError(ex);
				throw;
			}
			try
			{
				num2 = this.ReadWithoutValidation(buffer, offset, size);
			}
			catch (Exception ex2)
			{
				Win32Exception ex3 = ex2.InnerException as Win32Exception;
				if (ex3 != null && ex3.NativeErrorCode == 10060)
				{
					ex2 = new WebException(SR.GetString("net_timeout"), WebExceptionStatus.Timeout);
				}
				throw ex2;
			}
			Interlocked.CompareExchange(ref this.m_CallNesting, 0, 1);
			if (Logging.On && num2 > 0)
			{
				Logging.Dump(Logging.Web, this, "Read", buffer, offset, num2);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "Read", num2);
			}
			return num2;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00058620 File Offset: 0x00056820
		private int ReadWithoutValidation(byte[] buffer, int offset, int size)
		{
			return this.ReadWithoutValidation(buffer, offset, size, true);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0005862C File Offset: 0x0005682C
		private int ReadWithoutValidation([In] [Out] byte[] buffer, int offset, int size, bool abortOnError)
		{
			int num = 0;
			if (this.m_Chunked)
			{
				if (!this.m_ChunkEofRecvd)
				{
					try
					{
						num = this.m_ChunkParser.Read(buffer, offset, size);
						if (num == 0)
						{
							this.m_ChunkEofRecvd = true;
							this.CallDone();
						}
					}
					catch (Exception ex)
					{
						if (abortOnError)
						{
							this.IOError(ex);
						}
						throw;
					}
					return num;
				}
			}
			else if (this.m_ReadBytes != -1L)
			{
				num = (int)Math.Min(this.m_ReadBytes, (long)size);
			}
			else
			{
				num = size;
			}
			if (num == 0 || this.Eof)
			{
				return 0;
			}
			try
			{
				num = this.InternalRead(buffer, offset, num);
			}
			catch (Exception ex2)
			{
				if (abortOnError)
				{
					this.IOError(ex2);
				}
				throw;
			}
			int num2 = num;
			bool flag = false;
			if (num2 <= 0)
			{
				num2 = 0;
				if (this.m_ReadBytes != -1L)
				{
					if (!abortOnError)
					{
						throw this.m_ErrorException;
					}
					this.IOError(null, false);
				}
				else
				{
					flag = true;
				}
			}
			if (this.m_ReadBytes != -1L)
			{
				this.m_ReadBytes -= (long)num2;
				if (this.m_ReadBytes < 0L)
				{
					throw new InternalException();
				}
			}
			if (this.m_ReadBytes == 0L || flag)
			{
				this.m_ReadBytes = 0L;
				this.CallDone();
			}
			return num2;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00058750 File Offset: 0x00056950
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "BeginRead", "");
			}
			if (this.WriteStream)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (this.ErrorInStream)
			{
				throw this.m_ErrorException;
			}
			if (this.IsClosed)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.ConnectionClosed), WebExceptionStatus.ConnectionClosed);
			}
			if (this.m_Request.Aborted)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
			int num = Interlocked.CompareExchange(ref this.m_CallNesting, 1, 0);
			if (num != 0)
			{
				throw new NotSupportedException(SR.GetString("net_no_concurrent_io_allowed"));
			}
			IAsyncResult asyncResult = this.BeginReadWithoutValidation(buffer, offset, size, callback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "BeginRead", asyncResult);
			}
			return asyncResult;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0005885C File Offset: 0x00056A5C
		private IAsyncResult BeginReadWithoutValidation(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			int num = 0;
			if (this.m_Chunked)
			{
				if (!this.m_ChunkEofRecvd)
				{
					return this.m_ChunkParser.ReadAsync(this, buffer, offset, size, callback, state);
				}
			}
			else if (this.m_ReadBytes != -1L)
			{
				num = (int)Math.Min(this.m_ReadBytes, (long)size);
			}
			else
			{
				num = size;
			}
			if (num == 0 || this.Eof)
			{
				return new NestedSingleAsyncResult(this, state, callback, ConnectStream.ZeroLengthRead);
			}
			IAsyncResult asyncResult2;
			try
			{
				int num2 = 0;
				if (this.m_ReadBufferSize > 0)
				{
					num2 = this.FillFromBufferedData(buffer, ref offset, ref num);
					if (num == 0)
					{
						return new NestedSingleAsyncResult(this, state, callback, num2);
					}
				}
				if (this.ErrorInStream)
				{
					throw this.m_ErrorException;
				}
				this.m_BytesAlreadyTransferred = num2;
				IAsyncResult asyncResult = this.m_Connection.BeginRead(buffer, offset, num, callback, state);
				if (asyncResult == null)
				{
					this.m_BytesAlreadyTransferred = 0;
					this.m_ErrorException = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
					throw this.m_ErrorException;
				}
				asyncResult2 = asyncResult;
			}
			catch (Exception ex)
			{
				this.IOError(ex);
				throw;
			}
			return asyncResult2;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00058970 File Offset: 0x00056B70
		private int InternalRead(byte[] buffer, int offset, int size)
		{
			int num = this.FillFromBufferedData(buffer, ref offset, ref size);
			if (num > 0)
			{
				return num;
			}
			if (this.ErrorInStream)
			{
				throw this.m_ErrorException;
			}
			return this.m_Connection.Read(buffer, offset, size);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000589B0 File Offset: 0x00056BB0
		private void ReadCallback(IAsyncResult asyncResult)
		{
			NestedSingleAsyncResult nestedSingleAsyncResult = (NestedSingleAsyncResult)asyncResult.AsyncState;
			ConnectStream connectStream = (ConnectStream)nestedSingleAsyncResult.AsyncObject;
			object obj = null;
			try
			{
				int num = connectStream.m_Connection.EndRead(asyncResult);
				if (Logging.On)
				{
					Logging.Dump(Logging.Web, connectStream, "ReadCallback", nestedSingleAsyncResult.Buffer, nestedSingleAsyncResult.Offset, Math.Min(nestedSingleAsyncResult.Size, num));
				}
				obj = num;
			}
			catch (Exception ex)
			{
				obj = ex;
			}
			nestedSingleAsyncResult.InvokeCallback(obj);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00058A3C File Offset: 0x00056C3C
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "EndRead", "");
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			bool flag = false;
			int num;
			if (asyncResult.GetType() == typeof(NestedSingleAsyncResult) || this.m_Chunked)
			{
				LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
				if (lazyAsyncResult.AsyncObject != this)
				{
					throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
				}
				if (lazyAsyncResult.EndCalled)
				{
					throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndRead" }));
				}
				lazyAsyncResult.EndCalled = true;
				if (this.ErrorInStream)
				{
					throw this.m_ErrorException;
				}
				object obj = lazyAsyncResult.InternalWaitForCompletion();
				Exception ex = obj as Exception;
				if (ex != null)
				{
					this.IOError(ex, false);
					num = -1;
					goto IL_135;
				}
				if (obj == null)
				{
					num = 0;
					goto IL_135;
				}
				if (obj == ConnectStream.ZeroLengthRead)
				{
					num = 0;
					flag = true;
					goto IL_135;
				}
				try
				{
					num = (int)obj;
					if (this.m_Chunked && num == 0)
					{
						this.m_ChunkEofRecvd = true;
						this.CallDone();
					}
					goto IL_135;
				}
				catch (InvalidCastException)
				{
					num = -1;
					goto IL_135;
				}
			}
			try
			{
				num = this.m_Connection.EndRead(asyncResult);
			}
			catch (Exception ex2)
			{
				if (NclUtilities.IsFatal(ex2))
				{
					throw;
				}
				this.IOError(ex2, false);
				num = -1;
			}
			IL_135:
			num = this.EndReadWithoutValidation(num, flag);
			Interlocked.CompareExchange(ref this.m_CallNesting, 0, 1);
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "EndRead", num);
			}
			if (this.m_ErrorException != null)
			{
				throw this.m_ErrorException;
			}
			return num;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00058BE0 File Offset: 0x00056DE0
		private int EndReadWithoutValidation(int bytesTransferred, bool zeroLengthRead)
		{
			int bytesAlreadyTransferred = this.m_BytesAlreadyTransferred;
			this.m_BytesAlreadyTransferred = 0;
			if (!this.m_Chunked)
			{
				bool flag = false;
				if (bytesTransferred <= 0)
				{
					if (this.m_ReadBytes != -1L && (bytesTransferred < 0 || !zeroLengthRead))
					{
						this.IOError(null, false);
					}
					else
					{
						flag = true;
						bytesTransferred = 0;
					}
				}
				bytesTransferred += bytesAlreadyTransferred;
				if (this.m_ReadBytes != -1L)
				{
					this.m_ReadBytes -= (long)bytesTransferred;
				}
				if (this.m_ReadBytes == 0L || flag)
				{
					this.m_ReadBytes = 0L;
					this.CallDone();
				}
			}
			return bytesTransferred;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00058C64 File Offset: 0x00056E64
		private static void WriteHeadersCallback(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			WriteHeadersCallbackState writeHeadersCallbackState = (WriteHeadersCallbackState)ar.AsyncState;
			ConnectStream stream = writeHeadersCallbackState.stream;
			HttpWebRequest request = writeHeadersCallbackState.request;
			WebExceptionStatus webExceptionStatus = WebExceptionStatus.SendFailure;
			try
			{
				try
				{
					stream.m_Connection.EndWrite(ar);
				}
				finally
				{
					request.FreeWriteBuffer();
				}
				if (stream.m_Connection.m_InnerException != null)
				{
					throw stream.m_Connection.m_InnerException;
				}
				webExceptionStatus = WebExceptionStatus.Success;
			}
			catch (Exception ex)
			{
				stream.HandleWriteHeadersException(ex, webExceptionStatus);
			}
			stream.ExchangeCallNesting(0, 4);
			if (webExceptionStatus == WebExceptionStatus.Success && !stream.ErrorInStream)
			{
				webExceptionStatus = WebExceptionStatus.ReceiveFailure;
				try
				{
					request.StartAsync100ContinueTimer();
					stream.m_Connection.CheckStartReceive(request);
					if (stream.m_Connection.m_InnerException != null)
					{
						throw stream.m_Connection.m_InnerException;
					}
					webExceptionStatus = WebExceptionStatus.Success;
				}
				catch (Exception ex2)
				{
					stream.HandleWriteHeadersException(ex2, webExceptionStatus);
				}
			}
			request.WriteHeadersCallback(webExceptionStatus, stream, true);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00058D58 File Offset: 0x00056F58
		internal void WriteHeaders(bool async)
		{
			WebExceptionStatus webExceptionStatus = WebExceptionStatus.SendFailure;
			if (!this.ErrorInStream)
			{
				byte[] writeBuffer = this.m_Request.WriteBuffer;
				int writeBufferLength = this.m_Request.WriteBufferLength;
				try
				{
					Interlocked.CompareExchange(ref this.m_CallNesting, 4, 0);
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_sending_headers", new object[] { this.m_Request.Headers.ToString(true) }));
					}
					if (async)
					{
						WriteHeadersCallbackState writeHeadersCallbackState = new WriteHeadersCallbackState(this.m_Request, this);
						IAsyncResult asyncResult = this.m_Connection.UnsafeBeginWrite(writeBuffer, 0, writeBufferLength, ConnectStream.m_WriteHeadersCallback, writeHeadersCallbackState);
						if (asyncResult.CompletedSynchronously)
						{
							try
							{
								this.m_Connection.EndWrite(asyncResult);
							}
							finally
							{
								this.m_Request.FreeWriteBuffer();
							}
							webExceptionStatus = WebExceptionStatus.Success;
						}
						else
						{
							webExceptionStatus = WebExceptionStatus.Pending;
						}
					}
					else
					{
						this.SafeSetSocketTimeout(SocketShutdown.Send);
						try
						{
							this.m_Connection.Write(writeBuffer, 0, writeBufferLength);
						}
						finally
						{
							this.m_Request.FreeWriteBuffer();
						}
						webExceptionStatus = WebExceptionStatus.Success;
					}
				}
				catch (Exception ex)
				{
					this.HandleWriteHeadersException(ex, webExceptionStatus);
				}
				finally
				{
					if (webExceptionStatus != WebExceptionStatus.Pending)
					{
						Interlocked.CompareExchange(ref this.m_CallNesting, 0, 4);
					}
				}
			}
			if (webExceptionStatus == WebExceptionStatus.Pending)
			{
				return;
			}
			if (webExceptionStatus == WebExceptionStatus.Success && !this.ErrorInStream)
			{
				webExceptionStatus = WebExceptionStatus.ReceiveFailure;
				try
				{
					if (async)
					{
						this.m_Request.StartAsync100ContinueTimer();
						this.m_Connection.CheckStartReceive(this.m_Request);
					}
					else
					{
						this.m_Request.StartContinueWait();
						this.m_Connection.CheckStartReceive(this.m_Request);
						if (this.m_Request.ShouldWaitFor100Continue())
						{
							this.PollAndRead(this.m_Request.UserRetrievedWriteStream);
						}
					}
					webExceptionStatus = WebExceptionStatus.Success;
				}
				catch (Exception ex2)
				{
					this.HandleWriteHeadersException(ex2, webExceptionStatus);
				}
			}
			this.m_Request.WriteHeadersCallback(webExceptionStatus, this, async);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00058F3C File Offset: 0x0005713C
		private void HandleWriteHeadersException(Exception e, WebExceptionStatus error)
		{
			if (e is IOException || e is ObjectDisposedException)
			{
				if (!this.m_Connection.AtLeastOneResponseReceived && !this.m_Request.BodyStarted)
				{
					e = new WebException(NetRes.GetWebStatusString("net_connclosed", error), error, WebExceptionInternalStatus.Recoverable, e);
				}
				else
				{
					e = new WebException(NetRes.GetWebStatusString("net_connclosed", error), error, this.m_Connection.AtLeastOneResponseReceived ? WebExceptionInternalStatus.Isolated : WebExceptionInternalStatus.RequestFatal, e);
				}
			}
			this.IOError(e, false);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00058FB8 File Offset: 0x000571B8
		internal ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			ChannelBinding channelBinding = null;
			TlsStream tlsStream = this.m_Connection.NetworkStream as TlsStream;
			if (tlsStream != null)
			{
				channelBinding = tlsStream.GetChannelBinding(kind);
			}
			return channelBinding;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00058FE4 File Offset: 0x000571E4
		internal void PollAndRead(bool userRetrievedStream)
		{
			this.m_Connection.PollAndRead(this.m_Request, userRetrievedStream);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00058FF8 File Offset: 0x000571F8
		private void SafeSetSocketTimeout(SocketShutdown mode)
		{
			if (this.Eof)
			{
				return;
			}
			int num;
			if (mode == SocketShutdown.Receive)
			{
				num = this.ReadTimeout;
			}
			else
			{
				num = this.WriteTimeout;
			}
			Connection connection = this.m_Connection;
			if (connection != null)
			{
				NetworkStream networkStream = connection.NetworkStream;
				if (networkStream != null)
				{
					networkStream.SetSocketTimeoutOption(mode, num, false);
				}
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00059040 File Offset: 0x00057240
		internal int SetRtcOption(byte[] rtcInputSocketConfig, byte[] rtcOutputSocketResult)
		{
			Socket internalSocket = this.InternalSocket;
			try
			{
				internalSocket.IOControl(-1744830445, rtcInputSocketConfig, null);
				internalSocket.IOControl(-1744830444, rtcInputSocketConfig, rtcOutputSocketResult);
			}
			catch (SocketException ex)
			{
				this.IOError(ex, false);
				return ex.ErrorCode;
			}
			return 0;
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00059098 File Offset: 0x00057298
		private Socket InternalSocket
		{
			get
			{
				Connection connection = this.m_Connection;
				if (connection != null)
				{
					NetworkStream networkStream = connection.NetworkStream;
					if (networkStream != null)
					{
						return networkStream.InternalSocket;
					}
				}
				return null;
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000590C4 File Offset: 0x000572C4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (Logging.On)
					{
						Logging.Enter(Logging.Web, this, "Close", "");
					}
					((ICloseEx)this).CloseEx(CloseExState.Normal);
					if (Logging.On)
					{
						Logging.Exit(Logging.Web, this, "Close", "");
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00059130 File Offset: 0x00057330
		internal void CloseInternal(bool internalCall)
		{
			((ICloseEx)this).CloseEx(internalCall ? CloseExState.Silent : CloseExState.Normal);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0005913F File Offset: 0x0005733F
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			this.CloseInternal((closeState & CloseExState.Silent) > CloseExState.Normal, (closeState & CloseExState.Abort) > CloseExState.Normal);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0005915C File Offset: 0x0005735C
		private void ResumeInternalClose(LazyAsyncResult userResult)
		{
			if (this.WriteChunked && !this.ErrorInStream && !this.m_IgnoreSocketErrors)
			{
				this.m_IgnoreSocketErrors = true;
				try
				{
					if (userResult != null)
					{
						this.m_Connection.BeginWrite(NclConstants.ChunkTerminator, 0, NclConstants.ChunkTerminator.Length, new AsyncCallback(this.ResumeClose_Part2_Wrapper), userResult);
						return;
					}
					this.SafeSetSocketTimeout(SocketShutdown.Send);
					this.m_Connection.Write(NclConstants.ChunkTerminator, 0, NclConstants.ChunkTerminator.Length);
				}
				catch (Exception ex)
				{
				}
			}
			this.ResumeClose_Part2(userResult);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x000591F0 File Offset: 0x000573F0
		private void ResumeClose_Part2_Wrapper(IAsyncResult ar)
		{
			try
			{
				this.m_Connection.EndWrite(ar);
			}
			catch (Exception ex)
			{
			}
			this.ResumeClose_Part2((LazyAsyncResult)ar.AsyncState);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00059230 File Offset: 0x00057430
		private void ResumeClose_Part2(LazyAsyncResult userResult)
		{
			try
			{
				try
				{
					if (this.ErrorInStream)
					{
						this.m_Connection.AbortSocket(true);
					}
				}
				finally
				{
					this.CallDone();
				}
			}
			catch
			{
			}
			finally
			{
				if (userResult != null)
				{
					userResult.InvokeCallback();
				}
			}
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00059294 File Offset: 0x00057494
		private void CloseInternal(bool internalCall, bool aborting)
		{
			bool flag = !aborting;
			Exception ex = null;
			if (aborting)
			{
				if (Interlocked.Exchange(ref this.m_ShutDown, 777777) >= 777777)
				{
					return;
				}
			}
			else
			{
				if (Interlocked.Increment(ref this.m_ShutDown) > 1)
				{
					return;
				}
				RequestLifetimeSetter.Report(this.m_RequestLifetimeSetter);
			}
			int num = ((this.IsPostStream && internalCall && !this.IgnoreSocketErrors && !this.BufferOnly && flag && !NclUtilities.HasShutdownStarted) ? 2 : 3);
			if (Interlocked.Exchange(ref this.m_CallNesting, num) == 1)
			{
				if (num == 2)
				{
					return;
				}
				flag &= !NclUtilities.HasShutdownStarted;
			}
			if (this.IgnoreSocketErrors && this.IsPostStream && !internalCall)
			{
				this.m_BytesLeftToWrite = 0L;
			}
			if (!this.IgnoreSocketErrors && flag)
			{
				if (!this.WriteStream)
				{
					Connection connection = this.m_Connection;
					if (connection != null)
					{
						NetworkStream networkStream = connection.NetworkStream;
						if (networkStream != null && networkStream.Connected)
						{
							flag = this.DrainSocket();
						}
					}
				}
				else
				{
					try
					{
						if (!this.ErrorInStream)
						{
							if (this.WriteChunked)
							{
								try
								{
									if (!this.m_IgnoreSocketErrors)
									{
										this.m_IgnoreSocketErrors = true;
										this.SafeSetSocketTimeout(SocketShutdown.Send);
										this.m_Connection.Write(NclConstants.ChunkTerminator, 0, NclConstants.ChunkTerminator.Length);
									}
								}
								catch
								{
								}
								this.m_BytesLeftToWrite = 0L;
							}
							else if (this.BytesLeftToWrite > 0L)
							{
								if (!internalCall)
								{
									throw new IOException(SR.GetString("net_io_notenoughbyteswritten"));
								}
								this.m_Connection.AbortSocket(true);
							}
							else if (this.BufferOnly)
							{
								this.m_BytesLeftToWrite = (long)this.BufferedData.Length;
								this.m_Request.SwitchToContentLength();
								this.SafeSetSocketTimeout(SocketShutdown.Send);
								this.m_Request.NeedEndSubmitRequest();
								return;
							}
						}
						else
						{
							flag = false;
						}
					}
					catch (Exception ex2)
					{
						flag = false;
						if (NclUtilities.IsFatal(ex2))
						{
							this.m_ErrorException = ex2;
							throw;
						}
						ex = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), ex2, WebExceptionStatus.RequestCanceled, null);
					}
				}
			}
			if (!flag && this.m_DoneCalled == 0)
			{
				if (!aborting && Interlocked.Exchange(ref this.m_ShutDown, 777777) >= 777777)
				{
					return;
				}
				this.m_ErrorException = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
				this.m_Connection.AbortSocket(true);
				if (this.WriteStream)
				{
					HttpWebRequest request = this.m_Request;
					if (request != null)
					{
						request.Abort();
					}
				}
				if (ex != null)
				{
					this.CallDone();
					if (!internalCall)
					{
						throw ex;
					}
				}
			}
			this.CallDone();
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00059514 File Offset: 0x00057714
		public override void Flush()
		{
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00059516 File Offset: 0x00057716
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0005951D File Offset: 0x0005771D
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0005952E File Offset: 0x0005772E
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00059540 File Offset: 0x00057740
		private bool DrainSocket()
		{
			if (this.IgnoreSocketErrors)
			{
				return true;
			}
			long readBytes = this.m_ReadBytes;
			if (!this.m_Chunked)
			{
				if (this.m_ReadBufferSize != 0)
				{
					this.m_ReadOffset += this.m_ReadBufferSize;
					if (this.m_ReadBytes != -1L)
					{
						this.m_ReadBytes -= (long)this.m_ReadBufferSize;
						if (this.m_ReadBytes < 0L)
						{
							this.m_ReadBytes = 0L;
							return false;
						}
					}
					this.m_ReadBufferSize = 0;
					this.m_ReadBuffer = null;
				}
				if (readBytes == -1L)
				{
					return true;
				}
			}
			if (this.Eof)
			{
				return true;
			}
			int responseDrainTimeout = this.GetResponseDrainTimeout();
			if (responseDrainTimeout == 0 || this.m_ReadBytes > 65536L)
			{
				this.m_Connection.AbortSocket(false);
				return true;
			}
			int num = 0;
			Stopwatch stopwatch = new Stopwatch();
			int num2;
			try
			{
				NetworkStream networkStream = this.m_Connection.NetworkStream;
				networkStream.SetSocketTimeoutOption(SocketShutdown.Receive, responseDrainTimeout, false);
				stopwatch.Start();
				while (stopwatch.ElapsedMilliseconds < (long)responseDrainTimeout)
				{
					num2 = this.ReadWithoutValidation(ConnectStream.s_DrainingBuffer, 0, ConnectStream.s_DrainingBuffer.Length, false);
					num += num2;
					if (num2 <= 0 || (long)num > 65536L)
					{
						goto IL_12D;
					}
				}
				num2 = -1;
			}
			catch (IOException)
			{
				num2 = -1;
			}
			catch (ObjectDisposedException)
			{
				num2 = -1;
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
				num2 = -1;
			}
			finally
			{
				stopwatch.Stop();
			}
			IL_12D:
			if (num2 != 0)
			{
				this.m_Connection.AbortSocket(false);
			}
			else
			{
				this.SafeSetSocketTimeout(SocketShutdown.Receive);
			}
			return true;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000596C8 File Offset: 0x000578C8
		private int GetResponseDrainTimeout()
		{
			if (ConnectStream.responseDrainTimeoutMilliseconds == -1)
			{
				string text = ConfigurationManager.AppSettings["responseDrainTimeout"];
				int num;
				if (int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out num))
				{
					ConnectStream.responseDrainTimeoutMilliseconds = num;
				}
				else
				{
					ConnectStream.responseDrainTimeoutMilliseconds = 500;
				}
			}
			return ConnectStream.responseDrainTimeoutMilliseconds;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0005971C File Offset: 0x0005791C
		private void IOError(Exception exception)
		{
			this.IOError(exception, true);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00059728 File Offset: 0x00057928
		private void IOError(Exception exception, bool willThrow)
		{
			if (this.m_ErrorException == null)
			{
				if (exception == null)
				{
					string text;
					if (!this.WriteStream)
					{
						text = SR.GetString("net_io_readfailure", new object[] { SR.GetString("net_io_connectionclosed") });
					}
					else
					{
						text = SR.GetString("net_io_writefailure", new object[] { SR.GetString("net_io_connectionclosed") });
					}
					Interlocked.CompareExchange<Exception>(ref this.m_ErrorException, new IOException(text), null);
				}
				else
				{
					willThrow &= Interlocked.CompareExchange<Exception>(ref this.m_ErrorException, exception, null) != null;
				}
			}
			ConnectionReturnResult connectionReturnResult = null;
			if (this.WriteStream)
			{
				this.m_Connection.HandleConnectStreamException(true, false, WebExceptionStatus.SendFailure, ref connectionReturnResult, this.m_ErrorException);
			}
			else
			{
				this.m_Connection.HandleConnectStreamException(false, true, WebExceptionStatus.ReceiveFailure, ref connectionReturnResult, this.m_ErrorException);
			}
			this.CallDone(connectionReturnResult);
			if (willThrow)
			{
				throw this.m_ErrorException;
			}
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x000597F8 File Offset: 0x000579F8
		internal static byte[] GetChunkHeader(int size, out int offset)
		{
			uint num = 4026531840U;
			byte[] array = new byte[10];
			offset = -1;
			int i = 0;
			while (i < 8)
			{
				if (offset != -1 || ((long)size & (long)((ulong)num)) != 0L)
				{
					uint num2 = (uint)size >> 28;
					if (num2 < 10U)
					{
						array[i] = (byte)(num2 + 48U);
					}
					else
					{
						array[i] = (byte)(num2 - 10U + 65U);
					}
					if (offset == -1)
					{
						offset = i;
					}
				}
				i++;
				size <<= 4;
			}
			array[8] = 13;
			array[9] = 10;
			return array;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00059866 File Offset: 0x00057A66
		void IRequestLifetimeTracker.TrackRequestLifetime(long requestStartTimestamp)
		{
			this.m_RequestLifetimeSetter = new RequestLifetimeSetter(requestStartTimestamp);
		}

		// Token: 0x0400136B RID: 4971
		private const int ApplyTransportSetting = -1744830445;

		// Token: 0x0400136C RID: 4972
		private const int QueryTransportSetting = -1744830444;

		// Token: 0x0400136D RID: 4973
		private int m_CallNesting;

		// Token: 0x0400136E RID: 4974
		private ScatterGatherBuffers m_BufferedData;

		// Token: 0x0400136F RID: 4975
		private bool m_SuppressWrite;

		// Token: 0x04001370 RID: 4976
		private bool m_BufferOnly;

		// Token: 0x04001371 RID: 4977
		private long m_BytesLeftToWrite;

		// Token: 0x04001372 RID: 4978
		private int m_BytesAlreadyTransferred;

		// Token: 0x04001373 RID: 4979
		private Connection m_Connection;

		// Token: 0x04001374 RID: 4980
		private byte[] m_ReadBuffer;

		// Token: 0x04001375 RID: 4981
		private int m_ReadOffset;

		// Token: 0x04001376 RID: 4982
		private int m_ReadBufferSize;

		// Token: 0x04001377 RID: 4983
		private long m_ReadBytes;

		// Token: 0x04001378 RID: 4984
		private bool m_Chunked;

		// Token: 0x04001379 RID: 4985
		private int m_DoneCalled;

		// Token: 0x0400137A RID: 4986
		private int m_ShutDown;

		// Token: 0x0400137B RID: 4987
		private Exception m_ErrorException;

		// Token: 0x0400137C RID: 4988
		private bool m_ChunkEofRecvd;

		// Token: 0x0400137D RID: 4989
		private ChunkParser m_ChunkParser;

		// Token: 0x0400137E RID: 4990
		private HttpWriteMode m_HttpWriteMode;

		// Token: 0x0400137F RID: 4991
		private int m_ReadTimeout;

		// Token: 0x04001380 RID: 4992
		private int m_WriteTimeout;

		// Token: 0x04001381 RID: 4993
		private RequestLifetimeSetter m_RequestLifetimeSetter;

		// Token: 0x04001382 RID: 4994
		private const long c_MaxDrainBytes = 65536L;

		// Token: 0x04001383 RID: 4995
		private readonly AsyncCallback m_ReadCallbackDelegate;

		// Token: 0x04001384 RID: 4996
		private readonly AsyncCallback m_WriteCallbackDelegate;

		// Token: 0x04001385 RID: 4997
		private static readonly AsyncCallback m_WriteHeadersCallback = new AsyncCallback(ConnectStream.WriteHeadersCallback);

		// Token: 0x04001386 RID: 4998
		private static readonly object ZeroLengthRead = new object();

		// Token: 0x04001387 RID: 4999
		private HttpWebRequest m_Request;

		// Token: 0x04001388 RID: 5000
		private static volatile int responseDrainTimeoutMilliseconds = -1;

		// Token: 0x04001389 RID: 5001
		private const int defaultResponseDrainTimeoutMilliseconds = 500;

		// Token: 0x0400138A RID: 5002
		private const string responseDrainTimeoutAppSetting = "responseDrainTimeout";

		// Token: 0x0400138C RID: 5004
		private bool m_IgnoreSocketErrors;

		// Token: 0x0400138D RID: 5005
		private bool m_ErrorResponseStatus;

		// Token: 0x0400138E RID: 5006
		private const int AlreadyAborted = 777777;

		// Token: 0x0400138F RID: 5007
		internal static byte[] s_DrainingBuffer = new byte[4096];

		// Token: 0x0200074B RID: 1867
		private static class Nesting
		{
			// Token: 0x040031D1 RID: 12753
			public const int Idle = 0;

			// Token: 0x040031D2 RID: 12754
			public const int IoInProgress = 1;

			// Token: 0x040031D3 RID: 12755
			public const int Closed = 2;

			// Token: 0x040031D4 RID: 12756
			public const int InError = 3;

			// Token: 0x040031D5 RID: 12757
			public const int InternalIO = 4;
		}
	}
}
