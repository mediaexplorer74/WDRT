using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Security;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001A3 RID: 419
	internal class Connection : PooledStream
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00053889 File Offset: 0x00051A89
		internal override ServicePoint ServicePoint
		{
			get
			{
				return this.ConnectionGroup.ServicePoint;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00053896 File Offset: 0x00051A96
		private ConnectionGroup ConnectionGroup
		{
			get
			{
				return this.m_ConnectionGroup;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0005389E File Offset: 0x00051A9E
		// (set) Token: 0x06000FF9 RID: 4089 RVA: 0x000538A8 File Offset: 0x00051AA8
		internal HttpWebRequest LockedRequest
		{
			get
			{
				return this.m_LockedRequest;
			}
			set
			{
				HttpWebRequest lockedRequest = this.m_LockedRequest;
				if (value == lockedRequest)
				{
					if (value != null && value.UnlockConnectionDelegate != this.m_ConnectionUnlock)
					{
						throw new InternalException();
					}
					return;
				}
				else
				{
					object obj = ((lockedRequest == null) ? null : lockedRequest.UnlockConnectionDelegate);
					if (obj != null && (value != null || this.m_ConnectionUnlock != obj))
					{
						throw new InternalException();
					}
					if (value == null)
					{
						this.m_LockedRequest = null;
						lockedRequest.UnlockConnectionDelegate = null;
						return;
					}
					UnlockConnectionDelegate unlockConnectionDelegate = value.UnlockConnectionDelegate;
					if (unlockConnectionDelegate != null)
					{
						if (unlockConnectionDelegate == this.m_ConnectionUnlock)
						{
							throw new InternalException();
						}
						unlockConnectionDelegate();
					}
					value.UnlockConnectionDelegate = this.m_ConnectionUnlock;
					this.m_LockedRequest = value;
					return;
				}
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0005393E File Offset: 0x00051B3E
		private void UnlockRequest()
		{
			this.LockedRequest = null;
			if (this.ConnectionGroup != null)
			{
				this.ConnectionGroup.ConnectionGoneIdle();
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0005395C File Offset: 0x00051B5C
		internal Connection(ConnectionGroup connectionGroup)
			: base(null)
		{
			this.m_MaximumUnauthorizedUploadLength = (long)SettingsSectionInternal.Section.MaximumUnauthorizedUploadLength;
			if (this.m_MaximumUnauthorizedUploadLength > 0L)
			{
				this.m_MaximumUnauthorizedUploadLength *= 1024L;
			}
			this.m_ResponseData = new CoreResponseData();
			this.m_ConnectionGroup = connectionGroup;
			if (ServicePointManager.UseHttpPipeliningAndBufferPooling)
			{
				this.m_ReadBuffer = Connection.s_PinnableBufferCache.AllocateBuffer();
				this.m_ReadBufferFromPinnableCache = true;
			}
			else
			{
				this.m_ReadBuffer = new byte[4096];
			}
			this.m_ReadState = ReadState.Start;
			this.m_WaitList = new List<Connection.WaitListItem>();
			this.m_WriteList = new ArrayList();
			this.m_AbortDelegate = new HttpAbortDelegate(this.AbortOrDisassociate);
			this.m_ConnectionUnlock = new UnlockConnectionDelegate(this.UnlockRequest);
			this.m_StatusLineValues = new Connection.StatusLineValues();
			this.m_RecycleTimer = this.ConnectionGroup.ServicePoint.ConnectionLeaseTimerQueue.CreateTimer();
			this.ConnectionGroup.Associate(this);
			this.m_ReadDone = true;
			this.m_WriteDone = true;
			this.m_Error = WebExceptionStatus.Success;
			if (System.PinnableBufferCacheEventSource.Log.IsEnabled())
			{
				System.PinnableBufferCacheEventSource.Log.DebugMessage1("CTOR: In System.Net.Connection.Connnection", (long)this.GetHashCode());
			}
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00053AA8 File Offset: 0x00051CA8
		~Connection()
		{
			if (this.m_ReadBufferFromPinnableCache && System.PinnableBufferCacheEventSource.Log.IsEnabled())
			{
				System.PinnableBufferCacheEventSource.Log.DebugMessage1("DTOR: ERROR Needing to Free m_ReadBuffer in Connection Destructor", (long)this.m_ReadBuffer.GetHashCode());
			}
			this.FreeReadBuffer();
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00053B04 File Offset: 0x00051D04
		private void FreeReadBuffer()
		{
			if (this.m_ReadBufferFromPinnableCache)
			{
				Connection.s_PinnableBufferCache.FreeBuffer(this.m_ReadBuffer);
				this.m_ReadBufferFromPinnableCache = false;
			}
			this.m_ReadBuffer = null;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00053B2C File Offset: 0x00051D2C
		protected override void Dispose(bool disposing)
		{
			if (System.PinnableBufferCacheEventSource.Log.IsEnabled())
			{
				System.PinnableBufferCacheEventSource.Log.DebugMessage1("In System.Net.Connection.Dispose()", (long)this.GetHashCode());
			}
			base.Dispose(disposing);
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00053B57 File Offset: 0x00051D57
		internal int BusyCount
		{
			get
			{
				return (this.m_ReadDone ? 0 : 1) + 2 * (this.m_WaitList.Count + this.m_WriteList.Count) + this.m_ReservedCount;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x00053B86 File Offset: 0x00051D86
		internal int IISVersion
		{
			get
			{
				return this.m_IISVersion;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00053B8E File Offset: 0x00051D8E
		internal bool AtLeastOneResponseReceived
		{
			get
			{
				return this.m_AtLeastOneResponseReceived;
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00053B98 File Offset: 0x00051D98
		internal bool SubmitRequest(HttpWebRequest request, bool forcedsubmit)
		{
			TriState triState = TriState.Unspecified;
			ConnectionReturnResult connectionReturnResult = null;
			bool flag = false;
			lock (this)
			{
				request.AbortDelegate = this.m_AbortDelegate;
				if (request.Aborted)
				{
					this.UnlockIfNeeded(request);
					return true;
				}
				if (!base.CanBePooled)
				{
					this.UnlockIfNeeded(request);
					return false;
				}
				if (!forcedsubmit && this.NonKeepAliveRequestPipelined)
				{
					this.UnlockIfNeeded(request);
					return false;
				}
				if (this.m_RecycleTimer.Duration != this.ServicePoint.ConnectionLeaseTimerQueue.Duration)
				{
					this.m_RecycleTimer.Cancel();
					this.m_RecycleTimer = this.ServicePoint.ConnectionLeaseTimerQueue.CreateTimer();
				}
				if (this.m_RecycleTimer.HasExpired)
				{
					request.KeepAlive = false;
				}
				if (this.LockedRequest != null && this.LockedRequest != request)
				{
					return false;
				}
				if (!forcedsubmit && !this.m_NonKeepAliveRequestPipelined)
				{
					this.m_NonKeepAliveRequestPipelined = !request.KeepAlive && !request.NtlmKeepAlive;
				}
				if (this.m_Free && this.m_WriteDone && (this.m_WriteList.Count == 0 || (request.Pipelined && !request.HasEntityBody && this.m_CanPipeline && this.m_Pipelining && !this.m_IsPipelinePaused && !forcedsubmit)))
				{
					this.m_Free = false;
					triState = this.StartRequest(request, true);
					if (triState == TriState.Unspecified)
					{
						flag = true;
						this.PrepareCloseConnectionSocket(ref connectionReturnResult, 0);
						base.Close(0);
					}
				}
				else
				{
					this.m_WaitList.Add(new Connection.WaitListItem(request, NetworkingPerfCounters.GetTimestamp()));
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.HttpWebRequestQueued);
					this.CheckNonIdle();
				}
			}
			if (flag)
			{
				ConnectionReturnResult.SetResponses(connectionReturnResult);
				return false;
			}
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, request);
			}
			if (triState != TriState.Unspecified)
			{
				this.CompleteStartRequest(true, request, triState);
			}
			if (!request.Async)
			{
				object obj = request.ConnectionAsyncResult.InternalWaitForCompletion();
				ConnectStream connectStream = obj as ConnectStream;
				Connection.AsyncTriState asyncTriState = null;
				if (connectStream == null)
				{
					asyncTriState = obj as Connection.AsyncTriState;
				}
				if (triState == TriState.Unspecified && asyncTriState != null)
				{
					this.CompleteStartRequest(true, request, asyncTriState.Value);
				}
				else if (connectStream != null)
				{
					request.SetRequestSubmitDone(connectStream);
				}
			}
			return true;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00053DE0 File Offset: 0x00051FE0
		private void UnlockIfNeeded(HttpWebRequest request)
		{
			if (this.LockedRequest == request)
			{
				this.UnlockRequest();
			}
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00053DF4 File Offset: 0x00051FF4
		private TriState StartRequest(HttpWebRequest request, bool canPollRead)
		{
			if (this.m_WriteList.Count == 0)
			{
				if (this.ServicePoint.MaxIdleTime != -1 && this.m_IdleSinceUtc != DateTime.MinValue && this.m_IdleSinceUtc + TimeSpan.FromMilliseconds((double)this.ServicePoint.MaxIdleTime) < DateTime.UtcNow)
				{
					return TriState.Unspecified;
				}
				if (canPollRead && !this.IsConnectionReusable())
				{
					return TriState.Unspecified;
				}
			}
			TriState triState = TriState.False;
			this.m_IdleSinceUtc = DateTime.MinValue;
			if (!this.m_IsPipelinePaused)
			{
				this.m_IsPipelinePaused = this.m_WriteList.Count >= Connection.s_MaxPipelinedCount;
			}
			this.m_Pipelining = this.m_CanPipeline && request.Pipelined && !request.HasEntityBody;
			this.m_WriteDone = false;
			this.m_WriteList.Add(request);
			this.CheckNonIdle();
			if (base.IsInitalizing)
			{
				triState = TriState.True;
			}
			return triState;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00053EDC File Offset: 0x000520DC
		private bool IsConnectionReusable()
		{
			try
			{
				if (base.PollRead())
				{
					return false;
				}
			}
			catch (SocketException ex)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, this, "IsConnectionReusable", ex.ToString());
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00053F2C File Offset: 0x0005212C
		private void CompleteStartRequest(bool onSubmitThread, HttpWebRequest request, TriState needReConnect)
		{
			if (needReConnect == TriState.True)
			{
				try
				{
					if (request.Async)
					{
						this.CompleteStartConnection(true, request);
					}
					else if (onSubmitThread)
					{
						this.CompleteStartConnection(false, request);
					}
				}
				catch (Exception ex)
				{
					if (NclUtilities.IsFatal(ex))
					{
						throw;
					}
				}
				if (!request.Async)
				{
					request.ConnectionAsyncResult.InvokeCallback(new Connection.AsyncTriState(needReConnect));
				}
				return;
			}
			if (request.Async)
			{
				request.OpenWriteSideResponseWindow();
			}
			ConnectStream connectStream = new ConnectStream(this, request);
			if (request.Async || onSubmitThread)
			{
				request.SetRequestSubmitDone(connectStream);
				return;
			}
			request.ConnectionAsyncResult.InvokeCallback(connectStream);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00053FC8 File Offset: 0x000521C8
		private HttpWebRequest CheckNextRequest()
		{
			if (this.m_WaitList.Count == 0)
			{
				this.m_Free = this.m_KeepAlive;
				return null;
			}
			if (!base.CanBePooled)
			{
				return null;
			}
			Connection.WaitListItem waitListItem = this.m_WaitList[0];
			HttpWebRequest httpWebRequest = waitListItem.Request;
			if (this.m_IsPipelinePaused)
			{
				this.m_IsPipelinePaused = this.m_WriteList.Count > Connection.s_MinPipelinedCount;
			}
			if ((!httpWebRequest.Pipelined || httpWebRequest.HasEntityBody || !this.m_CanPipeline || !this.m_Pipelining || this.m_IsPipelinePaused) && this.m_WriteList.Count != 0)
			{
				httpWebRequest = null;
			}
			if (httpWebRequest != null)
			{
				NetworkingPerfCounters.Instance.IncrementAverage(NetworkingPerfCounterName.HttpWebRequestAvgQueueTime, waitListItem.QueueStartTime);
				this.m_WaitList.RemoveAt(0);
				this.CheckIdle();
			}
			return httpWebRequest;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00054090 File Offset: 0x00052290
		private void CompleteStartConnection(bool async, HttpWebRequest httpWebRequest)
		{
			WebExceptionStatus webExceptionStatus = WebExceptionStatus.ConnectFailure;
			this.m_InnerException = null;
			bool flag = true;
			try
			{
				if ((httpWebRequest.IsWebSocketRequest || httpWebRequest.Address.Scheme == Uri.UriSchemeHttps) && this.ServicePoint.InternalProxyServicePoint)
				{
					if (!this.TunnelThroughProxy(this.ServicePoint.InternalAddress, httpWebRequest, async))
					{
						webExceptionStatus = WebExceptionStatus.ConnectFailure;
						flag = false;
					}
					if (async && flag)
					{
						return;
					}
				}
				else if (!base.Activate(httpWebRequest, async, new GeneralAsyncDelegate(this.CompleteConnectionWrapper)))
				{
					return;
				}
			}
			catch (Exception ex)
			{
				if (this.m_InnerException == null)
				{
					this.m_InnerException = ex;
				}
				if (ex is WebException)
				{
					webExceptionStatus = ((WebException)ex).Status;
				}
				flag = false;
			}
			if (!flag)
			{
				ConnectionReturnResult connectionReturnResult = null;
				this.HandleError(false, false, webExceptionStatus, ref connectionReturnResult);
				ConnectionReturnResult.SetResponses(connectionReturnResult);
				return;
			}
			this.CompleteConnection(async, httpWebRequest);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00054168 File Offset: 0x00052368
		private void CompleteConnectionWrapper(object request, object state)
		{
			Exception ex = state as Exception;
			if (ex != null)
			{
				ConnectionReturnResult connectionReturnResult = null;
				if (this.m_InnerException == null)
				{
					this.m_InnerException = ex;
				}
				this.HandleError(false, false, WebExceptionStatus.ConnectFailure, ref connectionReturnResult);
				ConnectionReturnResult.SetResponses(connectionReturnResult);
			}
			this.CompleteConnection(true, (HttpWebRequest)request);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x000541B0 File Offset: 0x000523B0
		private void CompleteConnection(bool async, HttpWebRequest request)
		{
			WebExceptionStatus webExceptionStatus = WebExceptionStatus.ConnectFailure;
			if (request.Async)
			{
				request.OpenWriteSideResponseWindow();
			}
			try
			{
				try
				{
					if (request.Address.Scheme == Uri.UriSchemeHttps)
					{
						TlsStream tlsStream = new TlsStream(request.GetRemoteResourceUri().IdnHost, base.NetworkStream, request.CheckCertificateRevocationList, request.SslProtocols, request.ClientCertificates, this.ServicePoint, request, request.Async ? request.GetConnectingContext().ContextCopy : null);
						base.NetworkStream = tlsStream;
					}
					webExceptionStatus = WebExceptionStatus.Success;
				}
				catch
				{
					base.NetworkStream.Close();
					throw;
				}
				finally
				{
					this.m_ReadState = ReadState.Start;
					this.ClearReaderState();
					request.SetRequestSubmitDone(new ConnectStream(this, request));
				}
			}
			catch (Exception ex)
			{
				if (this.m_InnerException == null)
				{
					this.m_InnerException = ex;
				}
				WebException ex2 = ex as WebException;
				if (ex2 != null)
				{
					webExceptionStatus = ex2.Status;
				}
			}
			if (webExceptionStatus != WebExceptionStatus.Success)
			{
				ConnectionReturnResult connectionReturnResult = null;
				this.HandleError(false, false, webExceptionStatus, ref connectionReturnResult);
				ConnectionReturnResult.SetResponses(connectionReturnResult);
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, this, "CompleteConnection", "on error");
				}
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x000542E4 File Offset: 0x000524E4
		private void InternalWriteStartNextRequest(HttpWebRequest request, ref bool calledCloseConnection, ref TriState startRequestResult, ref HttpWebRequest nextRequest, ref ConnectionReturnResult returnResult)
		{
			lock (this)
			{
				this.m_WriteDone = true;
				if (!this.m_KeepAlive || this.m_Error != WebExceptionStatus.Success || !base.CanBePooled)
				{
					if (this.m_ReadDone)
					{
						if (this.m_Error == WebExceptionStatus.Success)
						{
							this.m_Error = WebExceptionStatus.KeepAliveFailure;
						}
						this.PrepareCloseConnectionSocket(ref returnResult, 0);
						calledCloseConnection = true;
						this.Close();
					}
					else if (this.m_Error != WebExceptionStatus.Success)
					{
					}
				}
				else
				{
					if (this.m_Pipelining || this.m_ReadDone)
					{
						nextRequest = this.CheckNextRequest();
					}
					if (nextRequest != null)
					{
						startRequestResult = this.StartRequest(nextRequest, false);
					}
				}
			}
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00054398 File Offset: 0x00052598
		internal void WriteStartNextRequest(HttpWebRequest request, ref ConnectionReturnResult returnResult)
		{
			TriState triState = TriState.Unspecified;
			HttpWebRequest httpWebRequest = null;
			bool flag = false;
			this.InternalWriteStartNextRequest(request, ref flag, ref triState, ref httpWebRequest, ref returnResult);
			if (!flag && triState != TriState.Unspecified)
			{
				this.CompleteStartRequest(false, httpWebRequest, triState);
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x000543C9 File Offset: 0x000525C9
		internal void SetLeftoverBytes(byte[] buffer, int bufferOffset, int bufferCount)
		{
			if (bufferOffset > 0)
			{
				Buffer.BlockCopy(buffer, bufferOffset, buffer, 0, bufferCount);
			}
			if (this.m_ReadBuffer != buffer)
			{
				this.FreeReadBuffer();
				this.m_ReadBuffer = buffer;
			}
			this.m_BytesScanned = 0;
			this.m_BytesRead = bufferCount;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00054400 File Offset: 0x00052600
		internal void ReadStartNextRequest(WebRequest currentRequest, ref ConnectionReturnResult returnResult)
		{
			HttpWebRequest httpWebRequest = null;
			TriState triState = TriState.Unspecified;
			bool flag = false;
			bool flag2 = false;
			int num = Interlocked.Decrement(ref this.m_ReservedCount);
			try
			{
				lock (this)
				{
					if (this.m_WriteList.Count > 0 && currentRequest == this.m_WriteList[0])
					{
						this.m_ReadState = ReadState.Start;
						this.m_WriteList.RemoveAt(0);
						this.m_ResponseData.m_ConnectStream = null;
					}
					else
					{
						flag2 = true;
					}
					if (!flag2)
					{
						if (this.m_ReadDone)
						{
							throw new InternalException();
						}
						if (!this.m_KeepAlive || this.m_Error != WebExceptionStatus.Success || !base.CanBePooled)
						{
							this.m_ReadDone = true;
							if (this.m_WriteDone)
							{
								if (this.m_Error == WebExceptionStatus.Success)
								{
									this.m_Error = WebExceptionStatus.KeepAliveFailure;
								}
								this.PrepareCloseConnectionSocket(ref returnResult, 0);
								HttpWebRequest httpWebRequest2 = currentRequest as HttpWebRequest;
								if (httpWebRequest2 != null && httpWebRequest2.TunnelConnection != null)
								{
									httpWebRequest2.TunnelConnection.RemoveFromConnectionList();
								}
								flag = true;
								this.Close();
							}
						}
						else
						{
							this.m_AtLeastOneResponseReceived = true;
							if (this.m_WriteList.Count != 0)
							{
								httpWebRequest = this.m_WriteList[0] as HttpWebRequest;
								if (!httpWebRequest.HeadersCompleted)
								{
									httpWebRequest = null;
									this.m_ReadDone = true;
								}
							}
							else
							{
								this.m_ReadDone = true;
								if (this.m_WriteDone)
								{
									httpWebRequest = this.CheckNextRequest();
									if (httpWebRequest != null)
									{
										if (httpWebRequest.HeadersCompleted)
										{
											throw new InternalException();
										}
										triState = this.StartRequest(httpWebRequest, false);
									}
									else
									{
										this.m_Free = true;
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				this.CheckIdle();
				if (returnResult != null)
				{
					ConnectionReturnResult.SetResponses(returnResult);
				}
			}
			if (!flag2 && !flag)
			{
				if (triState != TriState.Unspecified)
				{
					this.CompleteStartRequest(false, httpWebRequest, triState);
					return;
				}
				if (httpWebRequest != null)
				{
					if (!httpWebRequest.Async)
					{
						httpWebRequest.ConnectionReaderAsyncResult.InvokeCallback();
						return;
					}
					if (this.m_BytesScanned < this.m_BytesRead)
					{
						this.ReadComplete(0, WebExceptionStatus.Success);
						return;
					}
					if (Thread.CurrentThread.IsThreadPoolThread)
					{
						this.PostReceive();
						return;
					}
					ThreadPool.UnsafeQueueUserWorkItem(Connection.m_PostReceiveDelegate, this);
				}
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00054620 File Offset: 0x00052820
		internal void MarkAsReserved()
		{
			int num = Interlocked.Increment(ref this.m_ReservedCount);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0005463C File Offset: 0x0005283C
		internal void CheckStartReceive(HttpWebRequest request)
		{
			lock (this)
			{
				request.HeadersCompleted = true;
				if (this.m_WriteList.Count == 0)
				{
					return;
				}
				if (!this.m_ReadDone || this.m_WriteList[0] != request)
				{
					return;
				}
				this.m_ReadDone = false;
				this.m_CurrentRequest = (HttpWebRequest)this.m_WriteList[0];
			}
			if (!request.Async)
			{
				request.ConnectionReaderAsyncResult.InvokeCallback();
				return;
			}
			if (this.m_BytesScanned < this.m_BytesRead)
			{
				this.ReadComplete(0, WebExceptionStatus.Success);
				return;
			}
			if (Thread.CurrentThread.IsThreadPoolThread)
			{
				this.PostReceive();
				return;
			}
			ThreadPool.UnsafeQueueUserWorkItem(Connection.m_PostReceiveDelegate, this);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0005470C File Offset: 0x0005290C
		private void InitializeParseStatusLine()
		{
			this.m_StatusState = 0;
			this.m_StatusLineValues.MajorVersion = 0;
			this.m_StatusLineValues.MinorVersion = 0;
			this.m_StatusLineValues.StatusCode = 0;
			this.m_StatusLineValues.StatusDescription = null;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00054748 File Offset: 0x00052948
		private DataParseStatus ParseStatusLine(byte[] statusLine, int statusLineLength, ref int bytesParsed, ref int[] statusLineInts, ref string statusDescription, ref int statusState, ref WebParseError parseError)
		{
			DataParseStatus dataParseStatus = DataParseStatus.Done;
			int num = -1;
			int num2 = 0;
			while (bytesParsed < statusLineLength && statusLine[bytesParsed] != 13 && statusLine[bytesParsed] != 10)
			{
				switch (statusState)
				{
				case 0:
					if (statusLine[bytesParsed] == 47)
					{
						statusState++;
					}
					else if (statusLine[bytesParsed] == 32)
					{
						statusState = 3;
					}
					break;
				case 1:
					if (statusLine[bytesParsed] != 46)
					{
						goto IL_6D;
					}
					statusState++;
					break;
				case 2:
					goto IL_6D;
				case 3:
					goto IL_7F;
				case 4:
					if (statusLine[bytesParsed] != 32)
					{
						num2 = bytesParsed;
						if (num == -1)
						{
							num = bytesParsed;
						}
					}
					break;
				}
				IL_E2:
				bytesParsed++;
				if (this.m_MaximumResponseHeadersLength < 0)
				{
					continue;
				}
				int num3 = this.m_TotalResponseHeadersLength + 1;
				this.m_TotalResponseHeadersLength = num3;
				if (num3 >= this.m_MaximumResponseHeadersLength)
				{
					dataParseStatus = DataParseStatus.DataTooBig;
					IL_1D9:
					if (dataParseStatus == DataParseStatus.Done && statusState != 4 && (statusState != 3 || statusLineInts[3] <= 0))
					{
						dataParseStatus = DataParseStatus.Invalid;
					}
					if (dataParseStatus == DataParseStatus.Invalid)
					{
						parseError.Section = WebParseErrorSection.ResponseStatusLine;
						parseError.Code = WebParseErrorCode.Generic;
					}
					return dataParseStatus;
				}
				continue;
				IL_6D:
				if (statusLine[bytesParsed] == 32)
				{
					statusState++;
					goto IL_E2;
				}
				IL_7F:
				if (char.IsDigit((char)statusLine[bytesParsed]))
				{
					int num4 = (int)(statusLine[bytesParsed] - 48);
					statusLineInts[statusState] = statusLineInts[statusState] * 10 + num4;
					goto IL_E2;
				}
				if (statusLineInts[3] > 0)
				{
					statusState++;
					goto IL_E2;
				}
				if (!char.IsWhiteSpace((char)statusLine[bytesParsed]))
				{
					statusLineInts[statusState] = -1;
					goto IL_E2;
				}
				goto IL_E2;
			}
			int num5 = bytesParsed;
			if (num != -1)
			{
				statusDescription += WebHeaderCollection.HeaderEncoding.GetString(statusLine, num, num2 - num + 1);
			}
			if (bytesParsed == statusLineLength)
			{
				return DataParseStatus.NeedMoreData;
			}
			while (bytesParsed < statusLineLength && (statusLine[bytesParsed] == 13 || statusLine[bytesParsed] == 32))
			{
				bytesParsed++;
				if (this.m_MaximumResponseHeadersLength >= 0)
				{
					int num3 = this.m_TotalResponseHeadersLength + 1;
					this.m_TotalResponseHeadersLength = num3;
					if (num3 >= this.m_MaximumResponseHeadersLength)
					{
						dataParseStatus = DataParseStatus.DataTooBig;
						goto IL_1D9;
					}
				}
			}
			if (bytesParsed == statusLineLength)
			{
				dataParseStatus = DataParseStatus.NeedMoreData;
				goto IL_1D9;
			}
			if (statusLine[bytesParsed] == 10)
			{
				bytesParsed++;
				if (this.m_MaximumResponseHeadersLength >= 0)
				{
					int num3 = this.m_TotalResponseHeadersLength + 1;
					this.m_TotalResponseHeadersLength = num3;
					if (num3 >= this.m_MaximumResponseHeadersLength)
					{
						dataParseStatus = DataParseStatus.DataTooBig;
						goto IL_1D9;
					}
				}
				dataParseStatus = DataParseStatus.Done;
				goto IL_1D9;
			}
			goto IL_1D9;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00054960 File Offset: 0x00052B60
		private unsafe static DataParseStatus ParseStatusLineStrict(byte[] statusLine, int statusLineLength, ref int bytesParsed, ref int statusState, Connection.StatusLineValues statusLineValues, int maximumHeaderLength, ref int totalBytesParsed, ref WebParseError parseError)
		{
			int num = bytesParsed;
			DataParseStatus dataParseStatus = DataParseStatus.DataTooBig;
			int num2 = ((maximumHeaderLength <= 0) ? int.MaxValue : (maximumHeaderLength - totalBytesParsed + bytesParsed));
			if (statusLineLength < num2)
			{
				dataParseStatus = DataParseStatus.NeedMoreData;
				num2 = statusLineLength;
			}
			if (bytesParsed < num2)
			{
				try
				{
					fixed (byte[] array = statusLine)
					{
						byte* ptr;
						if (statusLine == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						int num3;
						switch (statusState)
						{
						case 0:
							while (totalBytesParsed - num + bytesParsed < "HTTP/".Length)
							{
								if ((byte)"HTTP/"[totalBytesParsed - num + bytesParsed] != ptr[bytesParsed])
								{
									dataParseStatus = DataParseStatus.Invalid;
									goto IL_447;
								}
								num3 = bytesParsed + 1;
								bytesParsed = num3;
								if (num3 == num2)
								{
									goto IL_447;
								}
							}
							if (ptr[bytesParsed] == 46)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							statusState = 1;
							break;
						case 1:
							break;
						case 2:
							goto IL_18B;
						case 3:
							goto IL_1F6;
						case 4:
							goto IL_2AB;
						case 5:
							goto IL_42C;
						default:
							goto IL_447;
						}
						while (ptr[bytesParsed] != 46)
						{
							if (ptr[bytesParsed] < 48 || ptr[bytesParsed] > 57)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							statusLineValues.MajorVersion = statusLineValues.MajorVersion * 10 + (int)ptr[bytesParsed] - 48;
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								goto IL_447;
							}
						}
						if (bytesParsed + 1 == num2)
						{
							goto IL_447;
						}
						bytesParsed++;
						if (ptr[bytesParsed] == 32)
						{
							dataParseStatus = DataParseStatus.Invalid;
							goto IL_447;
						}
						statusState = 2;
						IL_18B:
						while (ptr[bytesParsed] != 32)
						{
							if (ptr[bytesParsed] < 48 || ptr[bytesParsed] > 57)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							statusLineValues.MinorVersion = statusLineValues.MinorVersion * 10 + (int)ptr[bytesParsed] - 48;
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								goto IL_447;
							}
						}
						statusState = 3;
						statusLineValues.StatusCode = 1;
						num3 = bytesParsed + 1;
						bytesParsed = num3;
						if (num3 == num2)
						{
							goto IL_447;
						}
						IL_1F6:
						while (ptr[bytesParsed] >= 48 && ptr[bytesParsed] <= 57)
						{
							if (statusLineValues.StatusCode >= 1000)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							statusLineValues.StatusCode = statusLineValues.StatusCode * 10 + (int)ptr[bytesParsed] - 48;
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								goto IL_447;
							}
						}
						if (ptr[bytesParsed] != 32 || statusLineValues.StatusCode < 1000)
						{
							if (ptr[bytesParsed] != 13 || statusLineValues.StatusCode < 1000)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							statusLineValues.StatusDescription = statusLineValues.StatusDescription ?? string.Empty;
							statusLineValues.StatusCode -= 1000;
							statusState = 5;
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								goto IL_447;
							}
							goto IL_42C;
						}
						else
						{
							statusLineValues.StatusCode -= 1000;
							statusState = 4;
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								goto IL_447;
							}
						}
						IL_2AB:
						if (statusLineValues.StatusDescription == null)
						{
							string[] array2 = Connection.s_ShortcutStatusDescriptions;
							int i = 0;
							while (i < array2.Length)
							{
								string text = array2[i];
								if (bytesParsed < num2 - text.Length && ptr[bytesParsed] == (byte)text[0])
								{
									byte* ptr2 = ptr + bytesParsed + 1;
									int num4 = 1;
									while (num4 < text.Length && *(ptr2++) == (byte)text[num4])
									{
										num4++;
									}
									if (num4 == text.Length)
									{
										statusLineValues.StatusDescription = text;
										bytesParsed += text.Length;
										break;
									}
									break;
								}
								else
								{
									i++;
								}
							}
						}
						int num5 = bytesParsed;
						while (ptr[bytesParsed] != 13)
						{
							if (ptr[bytesParsed] < 32 || ptr[bytesParsed] == 127)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								string @string = WebHeaderCollection.HeaderEncoding.GetString(ptr + num5, bytesParsed - num5);
								if (statusLineValues.StatusDescription == null)
								{
									statusLineValues.StatusDescription = @string;
									goto IL_447;
								}
								statusLineValues.StatusDescription += @string;
								goto IL_447;
							}
						}
						if (bytesParsed > num5)
						{
							string string2 = WebHeaderCollection.HeaderEncoding.GetString(ptr + num5, bytesParsed - num5);
							if (statusLineValues.StatusDescription == null)
							{
								statusLineValues.StatusDescription = string2;
							}
							else
							{
								statusLineValues.StatusDescription += string2;
							}
						}
						else if (statusLineValues.StatusDescription == null)
						{
							statusLineValues.StatusDescription = "";
						}
						statusState = 5;
						num3 = bytesParsed + 1;
						bytesParsed = num3;
						if (num3 == num2)
						{
							goto IL_447;
						}
						IL_42C:
						if (ptr[bytesParsed] != 10)
						{
							dataParseStatus = DataParseStatus.Invalid;
						}
						else
						{
							dataParseStatus = DataParseStatus.Done;
							bytesParsed++;
						}
					}
				}
				finally
				{
					byte[] array = null;
				}
			}
			IL_447:
			totalBytesParsed += bytesParsed - num;
			if (dataParseStatus == DataParseStatus.Invalid)
			{
				parseError.Section = WebParseErrorSection.ResponseStatusLine;
				parseError.Code = WebParseErrorCode.Generic;
			}
			return dataParseStatus;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00054DF0 File Offset: 0x00052FF0
		private void SetStatusLineParsed()
		{
			this.m_ResponseData.m_StatusCode = (HttpStatusCode)this.m_StatusLineValues.StatusCode;
			this.m_ResponseData.m_StatusDescription = this.m_StatusLineValues.StatusDescription;
			this.m_ResponseData.m_IsVersionHttp11 = this.m_StatusLineValues.MajorVersion >= 1 && this.m_StatusLineValues.MinorVersion >= 1;
			if (this.ServicePoint.HttpBehaviour == HttpBehaviour.Unknown || (this.ServicePoint.HttpBehaviour == HttpBehaviour.HTTP11 && !this.m_ResponseData.m_IsVersionHttp11))
			{
				this.ServicePoint.HttpBehaviour = (this.m_ResponseData.m_IsVersionHttp11 ? HttpBehaviour.HTTP11 : HttpBehaviour.HTTP10);
			}
			if (ServicePointManager.UseHttpPipeliningAndBufferPooling)
			{
				this.m_CanPipeline = this.ServicePoint.SupportsPipelining;
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00054EB4 File Offset: 0x000530B4
		private long ProcessHeaderData(ref bool fHaveChunked, HttpWebRequest request, out bool dummyResponseStream)
		{
			long num = -1L;
			fHaveChunked = false;
			string text = this.m_ResponseData.m_ResponseHeaders["Transfer-Encoding"];
			if (text != null)
			{
				text = text.ToLower(CultureInfo.InvariantCulture);
				fHaveChunked = text.IndexOf("chunked") != -1;
			}
			if (!fHaveChunked)
			{
				string text2 = this.m_ResponseData.m_ResponseHeaders.ContentLength;
				if (text2 != null)
				{
					int num2 = text2.IndexOf(':');
					if (num2 != -1)
					{
						text2 = text2.Substring(num2 + 1);
					}
					if (!long.TryParse(text2, NumberStyles.None, CultureInfo.InvariantCulture.NumberFormat, out num))
					{
						num = -1L;
						num2 = text2.LastIndexOf(',');
						if (num2 != -1)
						{
							text2 = text2.Substring(num2 + 1);
							if (!long.TryParse(text2, NumberStyles.None, CultureInfo.InvariantCulture.NumberFormat, out num))
							{
								num = -1L;
							}
						}
					}
					if (num < 0L)
					{
						num = -2L;
					}
				}
			}
			dummyResponseStream = !request.CanGetResponseStream || this.m_ResponseData.m_StatusCode < HttpStatusCode.OK || this.m_ResponseData.m_StatusCode == HttpStatusCode.NoContent || (this.m_ResponseData.m_StatusCode == HttpStatusCode.NotModified && num < 0L);
			if (this.m_KeepAlive)
			{
				bool flag = false;
				if (!dummyResponseStream && num < 0L && !fHaveChunked)
				{
					flag = true;
				}
				else if (this.m_ResponseData.m_StatusCode == HttpStatusCode.Forbidden && base.NetworkStream is TlsStream)
				{
					flag = true;
				}
				else if (this.m_ResponseData.m_StatusCode > (HttpStatusCode)299 && (request.CurrentMethod == KnownHttpVerb.Post || request.CurrentMethod == KnownHttpVerb.Put) && this.m_MaximumUnauthorizedUploadLength >= 0L && request.ContentLength > this.m_MaximumUnauthorizedUploadLength && (request.CurrentAuthenticationState == null || request.CurrentAuthenticationState.Module == null))
				{
					flag = true;
				}
				else
				{
					bool flag2 = false;
					bool flag3 = false;
					string text3 = this.m_ResponseData.m_ResponseHeaders["Connection"];
					if (text3 == null && (this.ServicePoint.InternalProxyServicePoint || request.IsTunnelRequest))
					{
						text3 = this.m_ResponseData.m_ResponseHeaders["Proxy-Connection"];
					}
					if (text3 != null)
					{
						text3 = text3.ToLower(CultureInfo.InvariantCulture);
						if (text3.IndexOf("keep-alive") != -1)
						{
							flag3 = true;
						}
						else if (text3.IndexOf("close") != -1)
						{
							flag2 = true;
						}
					}
					if ((flag2 && this.ServicePoint.HttpBehaviour == HttpBehaviour.HTTP11) || (!flag3 && this.ServicePoint.HttpBehaviour <= HttpBehaviour.HTTP10))
					{
						flag = true;
					}
				}
				if (flag)
				{
					lock (this)
					{
						this.m_KeepAlive = false;
						this.m_Free = false;
					}
				}
			}
			return num;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0005516C File Offset: 0x0005336C
		internal bool KeepAlive
		{
			get
			{
				return this.m_KeepAlive;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00055174 File Offset: 0x00053374
		internal bool NonKeepAliveRequestPipelined
		{
			get
			{
				return this.m_NonKeepAliveRequestPipelined;
			}
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0005517C File Offset: 0x0005337C
		private DataParseStatus ParseStreamData(ref ConnectionReturnResult returnResult)
		{
			if (this.m_CurrentRequest == null)
			{
				this.m_ParseError.Section = WebParseErrorSection.Generic;
				this.m_ParseError.Code = WebParseErrorCode.UnexpectedServerResponse;
				return DataParseStatus.Invalid;
			}
			bool flag = false;
			bool flag2;
			long num = this.ProcessHeaderData(ref flag, this.m_CurrentRequest, out flag2);
			if (num == -2L)
			{
				this.m_ParseError.Section = WebParseErrorSection.ResponseHeader;
				this.m_ParseError.Code = WebParseErrorCode.InvalidContentLength;
				return DataParseStatus.Invalid;
			}
			int num2 = this.m_BytesRead - this.m_BytesScanned;
			if (this.m_ResponseData.m_StatusCode > (HttpStatusCode)299)
			{
				this.m_CurrentRequest.ErrorStatusCodeNotify(this, this.m_KeepAlive, false);
			}
			int num3;
			if (flag2)
			{
				num3 = 0;
				flag = false;
			}
			else
			{
				num3 = -1;
				if (!flag && num <= 2147483647L)
				{
					num3 = (int)num;
				}
			}
			DataParseStatus dataParseStatus;
			if (this.m_CurrentRequest.IsWebSocketRequest && this.m_ResponseData.m_StatusCode == HttpStatusCode.SwitchingProtocols)
			{
				this.m_ResponseData.m_ConnectStream = new ConnectStream(this, this.m_ReadBuffer, this.m_BytesScanned, num2, (long)num2, flag, this.m_CurrentRequest);
				dataParseStatus = DataParseStatus.Done;
				this.ClearReaderState();
			}
			else if (num3 != -1 && num3 <= num2)
			{
				this.m_ResponseData.m_ConnectStream = new ConnectStream(this, this.m_ReadBuffer, this.m_BytesScanned, num3, flag2 ? 0L : num, flag, this.m_CurrentRequest);
				dataParseStatus = DataParseStatus.ContinueParsing;
				this.m_BytesScanned += num3;
			}
			else
			{
				this.m_ResponseData.m_ConnectStream = new ConnectStream(this, this.m_ReadBuffer, this.m_BytesScanned, num2, flag2 ? 0L : num, flag, this.m_CurrentRequest);
				dataParseStatus = DataParseStatus.Done;
				this.ClearReaderState();
			}
			this.m_ResponseData.m_ContentLength = num;
			ConnectionReturnResult.Add(ref returnResult, this.m_CurrentRequest, this.m_ResponseData.Clone());
			return dataParseStatus;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00055326 File Offset: 0x00053526
		private void ClearReaderState()
		{
			this.m_BytesRead = 0;
			this.m_BytesScanned = 0;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00055338 File Offset: 0x00053538
		private DataParseStatus ParseResponseData(ref ConnectionReturnResult returnResult, out bool requestDone, out CoreResponseData continueResponseData)
		{
			DataParseStatus dataParseStatus = DataParseStatus.NeedMoreData;
			requestDone = false;
			continueResponseData = null;
			switch (this.m_ReadState)
			{
			case ReadState.Start:
				break;
			case ReadState.StatusLine:
				goto IL_F4;
			case ReadState.Headers:
				goto IL_28D;
			case ReadState.Data:
				goto IL_546;
			default:
				goto IL_551;
			}
			IL_2A:
			if (this.m_CurrentRequest == null)
			{
				lock (this)
				{
					if (this.m_WriteList.Count == 0 || (this.m_CurrentRequest = this.m_WriteList[0] as HttpWebRequest) == null)
					{
						this.m_ParseError.Section = WebParseErrorSection.Generic;
						this.m_ParseError.Code = WebParseErrorCode.Generic;
						dataParseStatus = DataParseStatus.Invalid;
						goto IL_551;
					}
				}
			}
			this.m_KeepAlive &= this.m_CurrentRequest.KeepAlive || this.m_CurrentRequest.NtlmKeepAlive;
			this.m_MaximumResponseHeadersLength = this.m_CurrentRequest.MaximumResponseHeadersLength * 1024;
			this.m_ResponseData = new CoreResponseData();
			this.m_ReadState = ReadState.StatusLine;
			this.m_TotalResponseHeadersLength = 0;
			this.InitializeParseStatusLine();
			IL_F4:
			DataParseStatus dataParseStatus2;
			if (SettingsSectionInternal.Section.UseUnsafeHeaderParsing)
			{
				int[] array = new int[]
				{
					0,
					this.m_StatusLineValues.MajorVersion,
					this.m_StatusLineValues.MinorVersion,
					this.m_StatusLineValues.StatusCode
				};
				if (this.m_StatusLineValues.StatusDescription == null)
				{
					this.m_StatusLineValues.StatusDescription = "";
				}
				dataParseStatus2 = this.ParseStatusLine(this.m_ReadBuffer, this.m_BytesRead, ref this.m_BytesScanned, ref array, ref this.m_StatusLineValues.StatusDescription, ref this.m_StatusState, ref this.m_ParseError);
				this.m_StatusLineValues.MajorVersion = array[1];
				this.m_StatusLineValues.MinorVersion = array[2];
				this.m_StatusLineValues.StatusCode = array[3];
			}
			else
			{
				dataParseStatus2 = Connection.ParseStatusLineStrict(this.m_ReadBuffer, this.m_BytesRead, ref this.m_BytesScanned, ref this.m_StatusState, this.m_StatusLineValues, this.m_MaximumResponseHeadersLength, ref this.m_TotalResponseHeadersLength, ref this.m_ParseError);
			}
			if (dataParseStatus2 == DataParseStatus.Done)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_received_status_line", new object[]
					{
						this.m_StatusLineValues.MajorVersion.ToString() + "." + this.m_StatusLineValues.MinorVersion.ToString(),
						this.m_StatusLineValues.StatusCode,
						this.m_StatusLineValues.StatusDescription
					}));
				}
				this.SetStatusLineParsed();
				this.m_ReadState = ReadState.Headers;
				this.m_ResponseData.m_ResponseHeaders = new WebHeaderCollection(WebHeaderCollectionType.HttpWebResponse);
			}
			else
			{
				if (dataParseStatus2 != DataParseStatus.NeedMoreData)
				{
					dataParseStatus = dataParseStatus2;
					goto IL_551;
				}
				goto IL_551;
			}
			IL_28D:
			if (this.m_BytesScanned >= this.m_BytesRead)
			{
				goto IL_551;
			}
			if (SettingsSectionInternal.Section.UseUnsafeHeaderParsing)
			{
				dataParseStatus2 = this.m_ResponseData.m_ResponseHeaders.ParseHeaders(this.m_ReadBuffer, this.m_BytesRead, ref this.m_BytesScanned, ref this.m_TotalResponseHeadersLength, this.m_MaximumResponseHeadersLength, ref this.m_ParseError);
			}
			else
			{
				dataParseStatus2 = this.m_ResponseData.m_ResponseHeaders.ParseHeadersStrict(this.m_ReadBuffer, this.m_BytesRead, ref this.m_BytesScanned, ref this.m_TotalResponseHeadersLength, this.m_MaximumResponseHeadersLength, ref this.m_ParseError);
			}
			if (dataParseStatus2 == DataParseStatus.Invalid || dataParseStatus2 == DataParseStatus.DataTooBig)
			{
				dataParseStatus = dataParseStatus2;
				goto IL_551;
			}
			if (dataParseStatus2 != DataParseStatus.Done)
			{
				goto IL_551;
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_received_headers", new object[] { this.m_ResponseData.m_ResponseHeaders.ToString(true) }));
			}
			if (this.m_IISVersion == -1)
			{
				string server = this.m_ResponseData.m_ResponseHeaders.Server;
				if (server != null && server.ToLower(CultureInfo.InvariantCulture).Contains("microsoft-iis"))
				{
					int num = server.IndexOf("/");
					if (num++ > 0 && num < server.Length)
					{
						this.m_IISVersion = (int)(server[num++] - '0');
						while (num < server.Length && char.IsDigit(server[num]))
						{
							this.m_IISVersion = this.m_IISVersion * 10 + (int)server[num++] - 48;
						}
					}
				}
				if (this.m_IISVersion == -1 && this.m_ResponseData.m_StatusCode != HttpStatusCode.Continue)
				{
					this.m_IISVersion = 0;
				}
			}
			bool flag2 = ServicePointManager.UseStrictRfcInterimResponseHandling && this.m_ResponseData.m_StatusCode > HttpStatusCode.SwitchingProtocols && this.m_ResponseData.m_StatusCode < HttpStatusCode.OK;
			if (this.m_ResponseData.m_StatusCode == HttpStatusCode.Continue || this.m_ResponseData.m_StatusCode == HttpStatusCode.BadRequest || flag2)
			{
				if (this.m_ResponseData.m_StatusCode == HttpStatusCode.BadRequest)
				{
					if (this.ServicePoint.HttpBehaviour == HttpBehaviour.HTTP11 && this.m_CurrentRequest.HttpWriteMode == HttpWriteMode.Chunked && this.m_ResponseData.m_ResponseHeaders.Via != null && string.Compare(this.m_ResponseData.m_StatusDescription, "Bad Request ( The HTTP request includes a non-supported header. Contact the Server administrator.  )", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.ServicePoint.HttpBehaviour = HttpBehaviour.HTTP11PartiallyCompliant;
					}
				}
				else
				{
					if (this.m_ResponseData.m_StatusCode == HttpStatusCode.Continue)
					{
						this.m_CurrentRequest.Saw100Continue = true;
						if (!this.ServicePoint.Understands100Continue)
						{
							this.ServicePoint.Understands100Continue = true;
						}
						continueResponseData = this.m_ResponseData;
						goto IL_2A;
					}
					goto IL_2A;
				}
			}
			this.m_ReadState = ReadState.Data;
			IL_546:
			requestDone = true;
			dataParseStatus = this.ParseStreamData(ref returnResult);
			IL_551:
			if (this.m_BytesScanned == this.m_BytesRead)
			{
				this.ClearReaderState();
			}
			return dataParseStatus;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x000558BC File Offset: 0x00053ABC
		internal void CloseOnIdle()
		{
			lock (this)
			{
				this.m_KeepAlive = false;
				this.m_RemovedFromConnectionList = true;
				if (!this.m_Idle)
				{
					this.CheckIdle();
				}
				if (this.m_Idle)
				{
					this.AbortSocket(false);
					GC.SuppressFinalize(this);
				}
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00055924 File Offset: 0x00053B24
		internal bool AbortOrDisassociate(HttpWebRequest request, WebException webException)
		{
			ConnectionReturnResult connectionReturnResult = null;
			lock (this)
			{
				int num = this.m_WriteList.IndexOf(request);
				if (num == -1)
				{
					Connection.WaitListItem waitListItem = null;
					if (this.m_WaitList.Count > 0)
					{
						waitListItem = this.m_WaitList.Find((Connection.WaitListItem o) => o.Request == request);
					}
					if (waitListItem != null)
					{
						NetworkingPerfCounters.Instance.IncrementAverage(NetworkingPerfCounterName.HttpWebRequestAvgQueueTime, waitListItem.QueueStartTime);
						this.m_WaitList.Remove(waitListItem);
						this.UnlockIfNeeded(waitListItem.Request);
					}
					return true;
				}
				this.m_KeepAlive = false;
				if (webException != null && this.m_InnerException == null)
				{
					this.m_InnerException = webException;
					this.m_Error = webException.Status;
				}
				else
				{
					this.m_Error = WebExceptionStatus.RequestCanceled;
				}
				this.PrepareCloseConnectionSocket(ref connectionReturnResult, num);
				base.Close(0);
			}
			ConnectionReturnResult.SetResponses(connectionReturnResult);
			return false;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00055A2C File Offset: 0x00053C2C
		internal void AbortSocket(bool isAbortState)
		{
			this.m_AbortSocketCalledUtc = DateTime.UtcNow;
			if (isAbortState)
			{
				this.UnlockRequest();
				this.CheckIdle();
			}
			else
			{
				this.m_Error = WebExceptionStatus.KeepAliveFailure;
			}
			lock (this)
			{
				base.Close(0);
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00055A8C File Offset: 0x00053C8C
		private void PrepareCloseConnectionSocket(ref ConnectionReturnResult returnResult, int abortedPipelinedRequestIndex = 0)
		{
			this.m_PrepareCloseConnectionSocketCalledUtc = DateTime.UtcNow;
			this.m_IdleSinceUtc = DateTime.MinValue;
			base.CanBePooled = false;
			if (this.m_WriteList.Count != 0 || this.m_WaitList.Count != 0)
			{
				HttpWebRequest lockedRequest = this.LockedRequest;
				if (lockedRequest != null)
				{
					bool flag = false;
					foreach (object obj in this.m_WriteList)
					{
						HttpWebRequest httpWebRequest = (HttpWebRequest)obj;
						if (httpWebRequest == lockedRequest)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						foreach (Connection.WaitListItem waitListItem in this.m_WaitList)
						{
							if (waitListItem.Request == lockedRequest)
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						this.UnlockRequest();
					}
				}
				if (this.m_WaitList.Count != 0)
				{
					HttpWebRequest[] array = new HttpWebRequest[this.m_WaitList.Count];
					for (int i = 0; i < this.m_WaitList.Count; i++)
					{
						array[i] = this.m_WaitList[i].Request;
					}
					ConnectionReturnResult.AddExceptionRange(ref returnResult, array, ExceptionHelper.IsolatedException);
				}
				if (this.m_WriteList.Count != 0)
				{
					Exception ex = this.m_InnerException;
					if (!(ex is WebException) && !(ex is SecurityException))
					{
						if (this.m_Error == WebExceptionStatus.ServerProtocolViolation)
						{
							string text = NetRes.GetWebStatusString(this.m_Error);
							string text2 = "";
							if (this.m_ParseError.Section != WebParseErrorSection.Generic)
							{
								text2 = text2 + " Section=" + this.m_ParseError.Section.ToString();
							}
							if (this.m_ParseError.Code != WebParseErrorCode.Generic)
							{
								text2 = text2 + " Detail=" + SR.GetString("net_WebResponseParseError_" + this.m_ParseError.Code.ToString());
							}
							if (text2.Length != 0)
							{
								text = text + "." + text2;
							}
							ex = new WebException(text, ex, this.m_Error, null, WebExceptionInternalStatus.RequestFatal);
						}
						else if (this.m_Error == WebExceptionStatus.SecureChannelFailure)
						{
							ex = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.SecureChannelFailure), WebExceptionStatus.SecureChannelFailure);
						}
						else if (this.m_Error == WebExceptionStatus.Timeout)
						{
							ex = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.Timeout), WebExceptionStatus.Timeout);
						}
						else if (this.m_Error == WebExceptionStatus.RequestCanceled)
						{
							ex = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled, WebExceptionInternalStatus.RequestFatal, ex);
						}
						else if (this.m_Error == WebExceptionStatus.MessageLengthLimitExceeded || this.m_Error == WebExceptionStatus.TrustFailure)
						{
							ex = new WebException(NetRes.GetWebStatusString("net_connclosed", this.m_Error), this.m_Error, WebExceptionInternalStatus.RequestFatal, ex);
						}
						else
						{
							if (this.m_Error == WebExceptionStatus.Success)
							{
								throw new InternalException();
							}
							bool flag2 = false;
							bool flag3 = false;
							if (this.m_WriteList.Count != 1)
							{
								flag2 = true;
							}
							else if (this.m_Error == WebExceptionStatus.KeepAliveFailure)
							{
								HttpWebRequest httpWebRequest2 = (HttpWebRequest)this.m_WriteList[0];
								if (!httpWebRequest2.BodyStarted)
								{
									flag3 = true;
								}
							}
							else
							{
								flag2 = !this.AtLeastOneResponseReceived && !((HttpWebRequest)this.m_WriteList[0]).BodyStarted;
							}
							ex = new WebException(NetRes.GetWebStatusString("net_connclosed", this.m_Error), this.m_Error, flag3 ? WebExceptionInternalStatus.Isolated : (flag2 ? WebExceptionInternalStatus.Recoverable : WebExceptionInternalStatus.RequestFatal), ex);
						}
					}
					WebException ex2 = new WebException(NetRes.GetWebStatusString("net_connclosed", WebExceptionStatus.PipelineFailure), WebExceptionStatus.PipelineFailure, WebExceptionInternalStatus.Recoverable, ex);
					HttpWebRequest[] array = new HttpWebRequest[this.m_WriteList.Count];
					this.m_WriteList.CopyTo(array, 0);
					ConnectionReturnResult.AddExceptionRange(ref returnResult, array, abortedPipelinedRequestIndex, ex2, ex);
				}
				this.m_WriteList.Clear();
				foreach (Connection.WaitListItem waitListItem2 in this.m_WaitList)
				{
					NetworkingPerfCounters.Instance.IncrementAverage(NetworkingPerfCounterName.HttpWebRequestAvgQueueTime, waitListItem2.QueueStartTime);
				}
				this.m_WaitList.Clear();
			}
			this.CheckIdle();
			if (this.m_Idle)
			{
				GC.SuppressFinalize(this);
			}
			if (!this.m_RemovedFromConnectionList && this.ConnectionGroup != null)
			{
				this.RemoveFromConnectionList();
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00055EF8 File Offset: 0x000540F8
		internal void RemoveFromConnectionList()
		{
			this.m_RemovedFromConnectionList = true;
			this.ConnectionGroup.Disassociate(this);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00055F10 File Offset: 0x00054110
		internal void HandleConnectStreamException(bool writeDone, bool readDone, WebExceptionStatus webExceptionStatus, ref ConnectionReturnResult returnResult, Exception e)
		{
			if (this.m_InnerException == null)
			{
				this.m_InnerException = e;
				if (!(e is WebException) && base.NetworkStream is TlsStream)
				{
					webExceptionStatus = ((TlsStream)base.NetworkStream).ExceptionStatus;
				}
				else if (e is ObjectDisposedException)
				{
					webExceptionStatus = WebExceptionStatus.RequestCanceled;
				}
			}
			this.HandleError(writeDone, readDone, webExceptionStatus, ref returnResult);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00055F6E File Offset: 0x0005416E
		private void HandleErrorWithReadDone(WebExceptionStatus webExceptionStatus, ref ConnectionReturnResult returnResult)
		{
			this.HandleError(false, true, webExceptionStatus, ref returnResult);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00055F7C File Offset: 0x0005417C
		private void HandleError(bool writeDone, bool readDone, WebExceptionStatus webExceptionStatus, ref ConnectionReturnResult returnResult)
		{
			lock (this)
			{
				if (writeDone)
				{
					this.m_WriteDone = true;
				}
				if (readDone)
				{
					this.m_ReadDone = true;
				}
				if (webExceptionStatus == WebExceptionStatus.Success)
				{
					throw new InternalException();
				}
				this.m_Error = webExceptionStatus;
				this.PrepareCloseConnectionSocket(ref returnResult, 0);
				base.Close(0);
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00055FE8 File Offset: 0x000541E8
		private static void ReadCallbackWrapper(IAsyncResult asyncResult)
		{
			if (asyncResult.CompletedSynchronously)
			{
				return;
			}
			((Connection)asyncResult.AsyncState).ReadCallback(asyncResult);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00056004 File Offset: 0x00054204
		private void ReadCallback(IAsyncResult asyncResult)
		{
			int num = -1;
			WebExceptionStatus webExceptionStatus = WebExceptionStatus.ReceiveFailure;
			try
			{
				num = this.EndRead(asyncResult);
				if (num == 0)
				{
					num = -1;
				}
				webExceptionStatus = WebExceptionStatus.Success;
			}
			catch (Exception ex)
			{
				HttpWebRequest currentRequest = this.m_CurrentRequest;
				if (currentRequest != null)
				{
					currentRequest.ErrorStatusCodeNotify(this, false, true);
				}
				if (this.m_InnerException == null)
				{
					this.m_InnerException = ex;
				}
				if (ex.GetType() == typeof(ObjectDisposedException))
				{
					webExceptionStatus = WebExceptionStatus.RequestCanceled;
				}
				if (base.NetworkStream is TlsStream)
				{
					webExceptionStatus = ((TlsStream)base.NetworkStream).ExceptionStatus;
				}
				else
				{
					webExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
			}
			this.ReadComplete(num, webExceptionStatus);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000560A0 File Offset: 0x000542A0
		internal void PollAndRead(HttpWebRequest request, bool userRetrievedStream)
		{
			request.NeedsToReadForResponse = true;
			if (request.ConnectionReaderAsyncResult.InternalPeekCompleted && request.ConnectionReaderAsyncResult.Result == null && base.CanBePooled)
			{
				this.SyncRead(request, userRetrievedStream, true);
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000560D4 File Offset: 0x000542D4
		internal void SyncRead(HttpWebRequest request, bool userRetrievedStream, bool probeRead)
		{
			if (Connection.t_SyncReadNesting > 0)
			{
				return;
			}
			bool flag = !probeRead;
			try
			{
				Connection.t_SyncReadNesting++;
				int num = (probeRead ? request.RequestContinueCount : 0);
				int num2 = -1;
				WebExceptionStatus webExceptionStatus = WebExceptionStatus.ReceiveFailure;
				if (this.m_BytesScanned < this.m_BytesRead)
				{
					flag = true;
					num2 = 0;
					webExceptionStatus = WebExceptionStatus.Success;
				}
				bool flag2;
				do
				{
					flag2 = true;
					try
					{
						if (num2 != 0)
						{
							webExceptionStatus = WebExceptionStatus.ReceiveFailure;
							if (!flag)
							{
								flag = base.Poll(request.ContinueTimeout * 1000, SelectMode.SelectRead);
							}
							if (flag)
							{
								this.ReadTimeout = request.Timeout;
								num2 = this.Read(this.m_ReadBuffer, this.m_BytesRead, this.m_ReadBuffer.Length - this.m_BytesRead);
								webExceptionStatus = WebExceptionStatus.Success;
								if (num2 == 0)
								{
									num2 = -1;
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
						if (this.m_InnerException == null)
						{
							this.m_InnerException = ex;
						}
						if (ex.GetType() == typeof(ObjectDisposedException))
						{
							webExceptionStatus = WebExceptionStatus.RequestCanceled;
						}
						else if (base.NetworkStream is TlsStream)
						{
							webExceptionStatus = ((TlsStream)base.NetworkStream).ExceptionStatus;
						}
						else
						{
							SocketException ex2 = ex.InnerException as SocketException;
							if (ex2 != null)
							{
								if (ex2.ErrorCode == 10060)
								{
									webExceptionStatus = WebExceptionStatus.Timeout;
								}
								else
								{
									webExceptionStatus = WebExceptionStatus.ReceiveFailure;
								}
							}
						}
					}
					if (flag)
					{
						flag2 = this.ReadComplete(num2, webExceptionStatus);
					}
					num2 = -1;
				}
				while (!flag2 && (userRetrievedStream || num == request.RequestContinueCount));
			}
			finally
			{
				Connection.t_SyncReadNesting--;
			}
			if (probeRead)
			{
				request.FinishContinueWait();
				if (flag)
				{
					if (!request.Saw100Continue && !userRetrievedStream)
					{
						request.NeedsToReadForResponse = false;
						return;
					}
				}
				else
				{
					request.SetRequestContinue();
				}
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00056298 File Offset: 0x00054498
		private bool ReadComplete(int bytesRead, WebExceptionStatus errorStatus)
		{
			bool flag = true;
			CoreResponseData coreResponseData = null;
			ConnectionReturnResult connectionReturnResult = null;
			HttpWebRequest httpWebRequest = null;
			try
			{
				if (bytesRead < 0)
				{
					if (this.m_ReadState == ReadState.Start && this.m_AtLeastOneResponseReceived)
					{
						if (errorStatus == WebExceptionStatus.Success || errorStatus == WebExceptionStatus.ReceiveFailure)
						{
							errorStatus = WebExceptionStatus.KeepAliveFailure;
						}
					}
					else if (errorStatus == WebExceptionStatus.Success)
					{
						errorStatus = WebExceptionStatus.ConnectionClosed;
					}
					HttpWebRequest currentRequest = this.m_CurrentRequest;
					if (currentRequest != null)
					{
						currentRequest.ErrorStatusCodeNotify(this, false, true);
					}
					this.HandleErrorWithReadDone(errorStatus, ref connectionReturnResult);
				}
				else
				{
					bytesRead += this.m_BytesRead;
					if (bytesRead > this.m_ReadBuffer.Length)
					{
						throw new InternalException();
					}
					this.m_BytesRead = bytesRead;
					DataParseStatus dataParseStatus = this.ParseResponseData(ref connectionReturnResult, out flag, out coreResponseData);
					httpWebRequest = this.m_CurrentRequest;
					if (dataParseStatus != DataParseStatus.NeedMoreData)
					{
						this.m_CurrentRequest = null;
					}
					if (dataParseStatus == DataParseStatus.Invalid || dataParseStatus == DataParseStatus.DataTooBig)
					{
						if (httpWebRequest != null)
						{
							httpWebRequest.ErrorStatusCodeNotify(this, false, false);
						}
						if (dataParseStatus == DataParseStatus.Invalid)
						{
							this.HandleErrorWithReadDone(WebExceptionStatus.ServerProtocolViolation, ref connectionReturnResult);
						}
						else
						{
							this.HandleErrorWithReadDone(WebExceptionStatus.MessageLengthLimitExceeded, ref connectionReturnResult);
						}
					}
					else if (dataParseStatus != DataParseStatus.Done)
					{
						if (dataParseStatus == DataParseStatus.NeedMoreData)
						{
							int num = this.m_BytesRead - this.m_BytesScanned;
							if (num != 0)
							{
								if (this.m_BytesScanned == 0 && this.m_BytesRead == this.m_ReadBuffer.Length)
								{
									byte[] array = new byte[this.m_ReadBuffer.Length * 2];
									Buffer.BlockCopy(this.m_ReadBuffer, 0, array, 0, this.m_BytesRead);
									this.FreeReadBuffer();
									this.m_ReadBuffer = array;
								}
								else
								{
									Buffer.BlockCopy(this.m_ReadBuffer, this.m_BytesScanned, this.m_ReadBuffer, 0, num);
								}
							}
							this.m_BytesRead = num;
							this.m_BytesScanned = 0;
							if (httpWebRequest != null && httpWebRequest.Async)
							{
								if (Thread.CurrentThread.IsThreadPoolThread)
								{
									this.PostReceive();
								}
								else
								{
									ThreadPool.UnsafeQueueUserWorkItem(Connection.m_PostReceiveDelegate, this);
								}
							}
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
				flag = true;
				if (this.m_InnerException == null)
				{
					this.m_InnerException = ex;
				}
				HttpWebRequest currentRequest2 = this.m_CurrentRequest;
				if (currentRequest2 != null)
				{
					currentRequest2.ErrorStatusCodeNotify(this, false, true);
				}
				this.HandleErrorWithReadDone(WebExceptionStatus.ReceiveFailure, ref connectionReturnResult);
			}
			try
			{
				if (httpWebRequest != null && httpWebRequest.HttpWriteMode != HttpWriteMode.None && (coreResponseData != null || (connectionReturnResult != null && connectionReturnResult.IsNotEmpty && httpWebRequest.AllowWriteStreamBuffering)) && httpWebRequest.FinishContinueWait())
				{
					httpWebRequest.SetRequestContinue(coreResponseData);
				}
			}
			finally
			{
				ConnectionReturnResult.SetResponses(connectionReturnResult);
			}
			return flag;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x000564E8 File Offset: 0x000546E8
		internal void Write(ScatterGatherBuffers writeBuffer)
		{
			BufferOffsetSize[] buffers = writeBuffer.GetBuffers();
			if (buffers != null)
			{
				base.MultipleWrite(buffers);
			}
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00056508 File Offset: 0x00054708
		private static void PostReceiveWrapper(object state)
		{
			Connection connection = state as Connection;
			connection.PostReceive();
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00056524 File Offset: 0x00054724
		private void PostReceive()
		{
			try
			{
				if (this.m_LastAsyncResult != null && !this.m_LastAsyncResult.IsCompleted)
				{
					throw new InternalException();
				}
				this.m_LastAsyncResult = this.UnsafeBeginRead(this.m_ReadBuffer, this.m_BytesRead, this.m_ReadBuffer.Length - this.m_BytesRead, Connection.m_ReadCallback, this);
				if (this.m_LastAsyncResult.CompletedSynchronously)
				{
					this.ReadCallback(this.m_LastAsyncResult);
				}
			}
			catch (Exception ex)
			{
				HttpWebRequest currentRequest = this.m_CurrentRequest;
				if (currentRequest != null)
				{
					currentRequest.ErrorStatusCodeNotify(this, false, true);
				}
				ConnectionReturnResult connectionReturnResult = null;
				this.HandleErrorWithReadDone(WebExceptionStatus.ReceiveFailure, ref connectionReturnResult);
				ConnectionReturnResult.SetResponses(connectionReturnResult);
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000565CC File Offset: 0x000547CC
		private static void TunnelThroughProxyWrapper(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			bool flag = false;
			WebExceptionStatus webExceptionStatus = WebExceptionStatus.ConnectFailure;
			HttpWebRequest httpWebRequest = (HttpWebRequest)((LazyAsyncResult)result).AsyncObject;
			Connection connection = ((TunnelStateObject)result.AsyncState).Connection;
			HttpWebRequest originalRequest = ((TunnelStateObject)result.AsyncState).OriginalRequest;
			try
			{
				httpWebRequest.EndGetResponse(result);
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				ConnectStream connectStream = (ConnectStream)httpWebResponse.GetResponseStream();
				connection.NetworkStream = new NetworkStream(connectStream.Connection.NetworkStream, true);
				connectStream.Connection.NetworkStream.ConvertToNotSocketOwner();
				if (ServicePointManager.FinishProxyTunnelConnectionEarly)
				{
					connectStream.Connection.ForceFinishTunnelConnection();
				}
				else
				{
					originalRequest.TunnelConnection = connectStream.Connection;
				}
				flag = true;
			}
			catch (Exception ex)
			{
				if (connection.m_InnerException == null)
				{
					connection.m_InnerException = ex;
				}
				if (ex is WebException)
				{
					webExceptionStatus = ((WebException)ex).Status;
				}
			}
			if (!flag)
			{
				ConnectionReturnResult connectionReturnResult = null;
				connection.HandleError(false, false, webExceptionStatus, ref connectionReturnResult);
				ConnectionReturnResult.SetResponses(connectionReturnResult);
				return;
			}
			connection.CompleteConnection(true, originalRequest);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x000566EC File Offset: 0x000548EC
		private bool TunnelThroughProxy(Uri proxy, HttpWebRequest originalRequest, bool async)
		{
			bool flag = false;
			HttpWebRequest httpWebRequest = null;
			try
			{
				new WebPermission(NetworkAccess.Connect, proxy).Assert();
				try
				{
					httpWebRequest = new HttpWebRequest(proxy, originalRequest.Address, originalRequest);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				httpWebRequest.Credentials = ((originalRequest.InternalProxy == null) ? null : originalRequest.InternalProxy.Credentials);
				httpWebRequest.InternalProxy = null;
				httpWebRequest.PreAuthenticate = true;
				httpWebRequest.UserAgent = originalRequest.UserAgent;
				HttpWebResponse httpWebResponse;
				if (async)
				{
					TunnelStateObject tunnelStateObject = new TunnelStateObject(originalRequest, this);
					IAsyncResult asyncResult = httpWebRequest.BeginGetResponse(Connection.m_TunnelCallback, tunnelStateObject);
					if (!asyncResult.CompletedSynchronously)
					{
						return true;
					}
					httpWebResponse = (HttpWebResponse)httpWebRequest.EndGetResponse(asyncResult);
				}
				else
				{
					httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				}
				ConnectStream connectStream = (ConnectStream)httpWebResponse.GetResponseStream();
				base.NetworkStream = new NetworkStream(connectStream.Connection.NetworkStream, true);
				connectStream.Connection.NetworkStream.ConvertToNotSocketOwner();
				if (ServicePointManager.FinishProxyTunnelConnectionEarly)
				{
					connectStream.Connection.ForceFinishTunnelConnection();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				if (this.m_InnerException == null)
				{
					this.m_InnerException = ex;
				}
			}
			return flag;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00056820 File Offset: 0x00054A20
		private void ForceFinishTunnelConnection()
		{
			this.ServicePoint.DecrementConnection();
			this.ConnectionGroup.DecrementConnection();
			this.RemoveFromConnectionList();
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0005683E File Offset: 0x00054A3E
		private void CheckNonIdle()
		{
			if (this.m_Idle && this.BusyCount != 0)
			{
				this.m_Idle = false;
				this.ServicePoint.IncrementConnection();
				this.ConnectionGroup.IncrementConnection();
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00056870 File Offset: 0x00054A70
		private void CheckIdle()
		{
			if (!this.m_Idle && this.BusyCount == 0)
			{
				this.m_Idle = true;
				this.ServicePoint.DecrementConnection();
				if (this.ConnectionGroup != null)
				{
					this.ConnectionGroup.DecrementConnection();
					this.ConnectionGroup.ConnectionGoneIdle();
				}
				this.m_IdleSinceUtc = DateTime.UtcNow;
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000568C8 File Offset: 0x00054AC8
		[Conditional("TRAVE")]
		private void DebugDumpWriteListEntries()
		{
			for (int i = 0; i < this.m_WriteList.Count; i++)
			{
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000568EC File Offset: 0x00054AEC
		[Conditional("TRAVE")]
		private void DebugDumpWaitListEntries()
		{
			for (int i = 0; i < this.m_WaitList.Count; i++)
			{
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0005690F File Offset: 0x00054B0F
		[Conditional("TRAVE")]
		private void DebugDumpListEntry(int currentPos, HttpWebRequest req, string listType)
		{
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00056911 File Offset: 0x00054B11
		[Conditional("DEBUG")]
		internal void DebugMembers(int requestHash)
		{
		}

		// Token: 0x04001322 RID: 4898
		[ThreadStatic]
		private static int t_SyncReadNesting;

		// Token: 0x04001323 RID: 4899
		private const int CRLFSize = 2;

		// Token: 0x04001324 RID: 4900
		private const long c_InvalidContentLength = -2L;

		// Token: 0x04001325 RID: 4901
		private const int CachedBufferSize = 4096;

		// Token: 0x04001326 RID: 4902
		private static System.PinnableBufferCache s_PinnableBufferCache = new System.PinnableBufferCache("System.Net.Connection", 4096);

		// Token: 0x04001327 RID: 4903
		private WebExceptionStatus m_Error;

		// Token: 0x04001328 RID: 4904
		internal Exception m_InnerException;

		// Token: 0x04001329 RID: 4905
		internal int m_IISVersion = -1;

		// Token: 0x0400132A RID: 4906
		private byte[] m_ReadBuffer;

		// Token: 0x0400132B RID: 4907
		private bool m_ReadBufferFromPinnableCache;

		// Token: 0x0400132C RID: 4908
		private int m_BytesRead;

		// Token: 0x0400132D RID: 4909
		private int m_BytesScanned;

		// Token: 0x0400132E RID: 4910
		private int m_TotalResponseHeadersLength;

		// Token: 0x0400132F RID: 4911
		private int m_MaximumResponseHeadersLength;

		// Token: 0x04001330 RID: 4912
		private long m_MaximumUnauthorizedUploadLength;

		// Token: 0x04001331 RID: 4913
		private CoreResponseData m_ResponseData;

		// Token: 0x04001332 RID: 4914
		private ReadState m_ReadState;

		// Token: 0x04001333 RID: 4915
		private Connection.StatusLineValues m_StatusLineValues;

		// Token: 0x04001334 RID: 4916
		private int m_StatusState;

		// Token: 0x04001335 RID: 4917
		private List<Connection.WaitListItem> m_WaitList;

		// Token: 0x04001336 RID: 4918
		private ArrayList m_WriteList;

		// Token: 0x04001337 RID: 4919
		private IAsyncResult m_LastAsyncResult;

		// Token: 0x04001338 RID: 4920
		private TimerThread.Timer m_RecycleTimer;

		// Token: 0x04001339 RID: 4921
		private WebParseError m_ParseError;

		// Token: 0x0400133A RID: 4922
		private bool m_AtLeastOneResponseReceived;

		// Token: 0x0400133B RID: 4923
		private static readonly WaitCallback m_PostReceiveDelegate = new WaitCallback(Connection.PostReceiveWrapper);

		// Token: 0x0400133C RID: 4924
		private static readonly AsyncCallback m_ReadCallback = new AsyncCallback(Connection.ReadCallbackWrapper);

		// Token: 0x0400133D RID: 4925
		private static readonly AsyncCallback m_TunnelCallback = new AsyncCallback(Connection.TunnelThroughProxyWrapper);

		// Token: 0x0400133E RID: 4926
		private static byte[] s_NullBuffer = new byte[0];

		// Token: 0x0400133F RID: 4927
		private HttpAbortDelegate m_AbortDelegate;

		// Token: 0x04001340 RID: 4928
		private ConnectionGroup m_ConnectionGroup;

		// Token: 0x04001341 RID: 4929
		private UnlockConnectionDelegate m_ConnectionUnlock;

		// Token: 0x04001342 RID: 4930
		private DateTime m_PrepareCloseConnectionSocketCalledUtc;

		// Token: 0x04001343 RID: 4931
		private DateTime m_AbortSocketCalledUtc;

		// Token: 0x04001344 RID: 4932
		private DateTime m_IdleSinceUtc;

		// Token: 0x04001345 RID: 4933
		private HttpWebRequest m_LockedRequest;

		// Token: 0x04001346 RID: 4934
		private HttpWebRequest m_CurrentRequest;

		// Token: 0x04001347 RID: 4935
		private bool m_CanPipeline;

		// Token: 0x04001348 RID: 4936
		private bool m_Free = true;

		// Token: 0x04001349 RID: 4937
		private bool m_Idle = true;

		// Token: 0x0400134A RID: 4938
		private bool m_KeepAlive = true;

		// Token: 0x0400134B RID: 4939
		private bool m_Pipelining;

		// Token: 0x0400134C RID: 4940
		private int m_ReservedCount;

		// Token: 0x0400134D RID: 4941
		private bool m_ReadDone;

		// Token: 0x0400134E RID: 4942
		private bool m_WriteDone;

		// Token: 0x0400134F RID: 4943
		private bool m_RemovedFromConnectionList;

		// Token: 0x04001350 RID: 4944
		private bool m_NonKeepAliveRequestPipelined;

		// Token: 0x04001351 RID: 4945
		private bool m_IsPipelinePaused;

		// Token: 0x04001352 RID: 4946
		private static int s_MaxPipelinedCount = 10;

		// Token: 0x04001353 RID: 4947
		private static int s_MinPipelinedCount = 5;

		// Token: 0x04001354 RID: 4948
		private const int BeforeVersionNumbers = 0;

		// Token: 0x04001355 RID: 4949
		private const int MajorVersionNumber = 1;

		// Token: 0x04001356 RID: 4950
		private const int MinorVersionNumber = 2;

		// Token: 0x04001357 RID: 4951
		private const int StatusCodeNumber = 3;

		// Token: 0x04001358 RID: 4952
		private const int AfterStatusCode = 4;

		// Token: 0x04001359 RID: 4953
		private const int AfterCarriageReturn = 5;

		// Token: 0x0400135A RID: 4954
		private const string BeforeVersionNumberBytes = "HTTP/";

		// Token: 0x0400135B RID: 4955
		private static readonly string[] s_ShortcutStatusDescriptions = new string[] { "OK", "Continue", "Unauthorized" };

		// Token: 0x02000747 RID: 1863
		private class StatusLineValues
		{
			// Token: 0x040031C9 RID: 12745
			internal int MajorVersion;

			// Token: 0x040031CA RID: 12746
			internal int MinorVersion;

			// Token: 0x040031CB RID: 12747
			internal int StatusCode;

			// Token: 0x040031CC RID: 12748
			internal string StatusDescription;
		}

		// Token: 0x02000748 RID: 1864
		private class WaitListItem
		{
			// Token: 0x17000F0A RID: 3850
			// (get) Token: 0x060041C9 RID: 16841 RVA: 0x00111513 File Offset: 0x0010F713
			public HttpWebRequest Request
			{
				get
				{
					return this.request;
				}
			}

			// Token: 0x17000F0B RID: 3851
			// (get) Token: 0x060041CA RID: 16842 RVA: 0x0011151B File Offset: 0x0010F71B
			public long QueueStartTime
			{
				get
				{
					return this.queueStartTime;
				}
			}

			// Token: 0x060041CB RID: 16843 RVA: 0x00111523 File Offset: 0x0010F723
			public WaitListItem(HttpWebRequest request, long queueStartTime)
			{
				this.request = request;
				this.queueStartTime = queueStartTime;
			}

			// Token: 0x040031CD RID: 12749
			private HttpWebRequest request;

			// Token: 0x040031CE RID: 12750
			private long queueStartTime;
		}

		// Token: 0x02000749 RID: 1865
		private class AsyncTriState
		{
			// Token: 0x060041CC RID: 16844 RVA: 0x00111539 File Offset: 0x0010F739
			public AsyncTriState(TriState newValue)
			{
				this.Value = newValue;
			}

			// Token: 0x040031CF RID: 12751
			public TriState Value;
		}
	}
}
