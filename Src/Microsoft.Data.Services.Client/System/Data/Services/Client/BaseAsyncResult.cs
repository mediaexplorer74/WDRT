using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000022 RID: 34
	internal abstract class BaseAsyncResult : IAsyncResult
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00004CED File Offset: 0x00002EED
		internal BaseAsyncResult(object source, string method, AsyncCallback callback, object state)
		{
			this.Source = source;
			this.Method = method;
			this.userCallback = callback;
			this.userState = state;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004D19 File Offset: 0x00002F19
		public object AsyncState
		{
			get
			{
				return this.userState;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004D21 File Offset: 0x00002F21
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.asyncWait == null)
				{
					Interlocked.CompareExchange<ManualResetEvent>(ref this.asyncWait, new ManualResetEvent(this.IsCompleted), null);
					if (this.IsCompleted)
					{
						this.SetAsyncWaitHandle();
					}
				}
				return this.asyncWait;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004D57 File Offset: 0x00002F57
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously == 1;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004D62 File Offset: 0x00002F62
		public bool IsCompleted
		{
			get
			{
				return this.userCompleted;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004D6A File Offset: 0x00002F6A
		internal bool IsCompletedInternally
		{
			get
			{
				return 0 != this.completed;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004D78 File Offset: 0x00002F78
		internal bool IsAborted
		{
			get
			{
				return 2 == this.completed;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004D83 File Offset: 0x00002F83
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004D8B File Offset: 0x00002F8B
		internal ODataRequestMessageWrapper Abortable
		{
			get
			{
				return this.abortable;
			}
			set
			{
				this.abortable = value;
				if (value != null && this.IsAborted)
				{
					value.Abort();
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004DA5 File Offset: 0x00002FA5
		internal Exception Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004DB0 File Offset: 0x00002FB0
		internal static T EndExecute<T>(object source, string method, IAsyncResult asyncResult) where T : BaseAsyncResult
		{
			Util.CheckArgumentNull<IAsyncResult>(asyncResult, "asyncResult");
			T t = asyncResult as T;
			if (t == null || source != t.Source || t.Method != method)
			{
				throw Error.Argument(Strings.Context_DidNotOriginateAsync, "asyncResult");
			}
			if (!t.IsCompleted)
			{
				t.AsyncWaitHandle.WaitOne();
			}
			if (Interlocked.Exchange(ref t.done, 1) != 0)
			{
				throw Error.Argument(Strings.Context_AsyncAlreadyDone, "asyncResult");
			}
			if (t.asyncWait != null)
			{
				Interlocked.CompareExchange(ref t.asyncWaitDisposeLock, new object(), null);
				lock (t.asyncWaitDisposeLock)
				{
					t.asyncWaitDisposed = true;
					Util.Dispose<ManualResetEvent>(t.asyncWait);
				}
			}
			if (t.IsAborted)
			{
				throw Error.InvalidOperation(Strings.Context_OperationCanceled);
			}
			if (t.Failure == null)
			{
				return t;
			}
			if (Util.IsKnownClientExcption(t.Failure))
			{
				throw t.Failure;
			}
			throw Error.InvalidOperation(Strings.DataServiceException_GeneralError, t.Failure);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004F2C File Offset: 0x0000312C
		internal static IAsyncResult InvokeAsync(Func<AsyncCallback, object, IAsyncResult> asyncAction, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult = asyncAction(BaseAsyncResult.GetDataServiceAsyncCallback(callback), state);
			return BaseAsyncResult.PostInvokeAsync(asyncResult, callback);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004F50 File Offset: 0x00003150
		internal static IAsyncResult InvokeAsync(BaseAsyncResult.AsyncAction asyncAction, byte[] buffer, int offset, int length, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult = asyncAction(buffer, offset, length, BaseAsyncResult.GetDataServiceAsyncCallback(callback), state);
			return BaseAsyncResult.PostInvokeAsync(asyncResult, callback);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004F78 File Offset: 0x00003178
		internal void SetCompletedSynchronously(bool isCompletedSynchronously)
		{
			Interlocked.CompareExchange(ref this.completedSynchronously, isCompletedSynchronously ? 1 : 0, 1);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004F90 File Offset: 0x00003190
		internal void HandleCompleted()
		{
			if (this.IsCompletedInternally && Interlocked.Exchange(ref this.userNotified, 1) == 0)
			{
				this.abortable = null;
				try
				{
					if (CommonUtil.IsCatchableExceptionType(this.Failure))
					{
						this.CompletedRequest();
					}
				}
				catch (Exception ex)
				{
					if (this.HandleFailure(ex))
					{
						throw;
					}
				}
				finally
				{
					this.userCompleted = true;
					this.SetAsyncWaitHandle();
					if (this.userCallback != null && !(this.Failure is ThreadAbortException) && !(this.Failure is StackOverflowException))
					{
						this.userCallback(this);
					}
				}
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005038 File Offset: 0x00003238
		internal bool HandleFailure(Exception e)
		{
			Interlocked.CompareExchange<Exception>(ref this.failure, e, null);
			this.SetCompleted();
			return !CommonUtil.IsCatchableExceptionType(e);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005057 File Offset: 0x00003257
		internal void SetAborted()
		{
			Interlocked.Exchange(ref this.completed, 2);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005066 File Offset: 0x00003266
		internal void SetCompleted()
		{
			Interlocked.CompareExchange(ref this.completed, 1, 0);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005076 File Offset: 0x00003276
		protected static void EqualRefCheck(BaseAsyncResult.PerRequest actual, BaseAsyncResult.PerRequest expected, InternalError errorcode)
		{
			if (!object.ReferenceEquals(actual, expected))
			{
				Error.ThrowInternalError(errorcode);
			}
		}

		// Token: 0x060000D7 RID: 215
		protected abstract void CompletedRequest();

		// Token: 0x060000D8 RID: 216
		protected abstract void HandleCompleted(BaseAsyncResult.PerRequest pereq);

		// Token: 0x060000D9 RID: 217
		protected abstract void AsyncEndGetResponse(IAsyncResult asyncResult);

		// Token: 0x060000DA RID: 218 RVA: 0x00005087 File Offset: 0x00003287
		protected virtual void CompleteCheck(BaseAsyncResult.PerRequest value, InternalError errorcode)
		{
			if (value == null || value.RequestCompleted)
			{
				Error.ThrowInternalError(errorcode);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000509C File Offset: 0x0000329C
		protected virtual void FinishCurrentChange(BaseAsyncResult.PerRequest pereq)
		{
			if (!pereq.RequestCompleted)
			{
				Error.ThrowInternalError(InternalError.SaveNextChangeIncomplete);
			}
			BaseAsyncResult.PerRequest perRequest = this.perRequest;
			if (perRequest != null)
			{
				BaseAsyncResult.EqualRefCheck(perRequest, pereq, InternalError.InvalidSaveNextChange);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000050CB File Offset: 0x000032CB
		protected bool HandleFailure(BaseAsyncResult.PerRequest pereq, Exception e)
		{
			if (pereq != null)
			{
				if (this.IsAborted)
				{
					pereq.SetAborted();
				}
				else
				{
					pereq.SetComplete();
				}
			}
			return this.HandleFailure(e);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000050F0 File Offset: 0x000032F0
		protected void AsyncEndGetRequestStream(IAsyncResult asyncResult)
		{
			BaseAsyncResult.AsyncStateBag asyncStateBag = asyncResult.AsyncState as BaseAsyncResult.AsyncStateBag;
			BaseAsyncResult.PerRequest perRequest = ((asyncStateBag == null) ? null : asyncStateBag.PerRequest);
			try
			{
				this.CompleteCheck(perRequest, InternalError.InvalidEndGetRequestCompleted);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				BaseAsyncResult.EqualRefCheck(this.perRequest, perRequest, InternalError.InvalidEndGetRequestStream);
				ODataRequestMessageWrapper odataRequestMessageWrapper = Util.NullCheck<ODataRequestMessageWrapper>(perRequest.Request, InternalError.InvalidEndGetRequestStreamRequest);
				Stream stream = Util.NullCheck<Stream>(odataRequestMessageWrapper.EndGetRequestStream(asyncResult), InternalError.InvalidEndGetRequestStreamStream);
				perRequest.RequestStream = stream;
				ContentStream requestContentStream = perRequest.RequestContentStream;
				Util.NullCheck<ContentStream>(requestContentStream, InternalError.InvalidEndGetRequestStreamContent);
				Util.NullCheck<Stream>(requestContentStream.Stream, InternalError.InvalidEndGetRequestStreamContent);
				if (requestContentStream.IsKnownMemoryStream)
				{
					MemoryStream memoryStream = requestContentStream.Stream as MemoryStream;
					byte[] buffer = memoryStream.GetBuffer();
					int num = checked((int)memoryStream.Position);
					int num2 = checked((int)memoryStream.Length) - num;
					if (buffer == null || num2 == 0)
					{
						Error.ThrowInternalError(InternalError.InvalidEndGetRequestStreamContentLength);
					}
				}
				perRequest.RequestContentBufferValidLength = -1;
				asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(requestContentStream.Stream.BeginRead), perRequest.RequestContentBuffer, 0, perRequest.RequestContentBuffer.Length, new AsyncCallback(this.AsyncRequestContentEndRead), asyncStateBag);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
			}
			catch (Exception ex)
			{
				if (this.HandleFailure(perRequest, ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005260 File Offset: 0x00003460
		private static IAsyncResult PostInvokeAsync(IAsyncResult asyncResult, AsyncCallback callback)
		{
			if (asyncResult.CompletedSynchronously)
			{
				callback(asyncResult);
			}
			return asyncResult;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005294 File Offset: 0x00003494
		private static AsyncCallback GetDataServiceAsyncCallback(AsyncCallback callback)
		{
			return delegate(IAsyncResult asyncResult)
			{
				if (asyncResult.CompletedSynchronously)
				{
					return;
				}
				callback(asyncResult);
			};
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000052BC File Offset: 0x000034BC
		private void SetAsyncWaitHandle()
		{
			if (this.asyncWait != null)
			{
				Interlocked.CompareExchange(ref this.asyncWaitDisposeLock, new object(), null);
				lock (this.asyncWaitDisposeLock)
				{
					if (!this.asyncWaitDisposed)
					{
						this.asyncWait.Set();
					}
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005324 File Offset: 0x00003524
		private void AsyncRequestContentEndRead(IAsyncResult asyncResult)
		{
			BaseAsyncResult.AsyncStateBag asyncStateBag = asyncResult.AsyncState as BaseAsyncResult.AsyncStateBag;
			BaseAsyncResult.PerRequest perRequest = ((asyncStateBag == null) ? null : asyncStateBag.PerRequest);
			try
			{
				this.CompleteCheck(perRequest, InternalError.InvalidEndReadCompleted);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				BaseAsyncResult.EqualRefCheck(this.perRequest, perRequest, InternalError.InvalidEndRead);
				ContentStream requestContentStream = perRequest.RequestContentStream;
				Util.NullCheck<ContentStream>(requestContentStream, InternalError.InvalidEndReadStream);
				Util.NullCheck<Stream>(requestContentStream.Stream, InternalError.InvalidEndReadStream);
				Stream stream = Util.NullCheck<Stream>(perRequest.RequestStream, InternalError.InvalidEndReadStream);
				int num = requestContentStream.Stream.EndRead(asyncResult);
				if (0 < num)
				{
					bool flag = perRequest.RequestContentBufferValidLength == -1;
					perRequest.RequestContentBufferValidLength = num;
					if (!asyncResult.CompletedSynchronously || flag)
					{
						do
						{
							asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(stream.BeginWrite), perRequest.RequestContentBuffer, 0, perRequest.RequestContentBufferValidLength, new AsyncCallback(this.AsyncEndWrite), asyncStateBag);
							perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
							if (asyncResult.CompletedSynchronously && !perRequest.RequestCompleted && !this.IsCompletedInternally)
							{
								asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(requestContentStream.Stream.BeginRead), perRequest.RequestContentBuffer, 0, perRequest.RequestContentBuffer.Length, new AsyncCallback(this.AsyncRequestContentEndRead), asyncStateBag);
								perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
							}
							if (!asyncResult.CompletedSynchronously || perRequest.RequestCompleted || this.IsCompletedInternally)
							{
								break;
							}
						}
						while (perRequest.RequestContentBufferValidLength > 0);
					}
				}
				else
				{
					perRequest.RequestContentBufferValidLength = 0;
					perRequest.RequestStream = null;
					stream.Close();
					ODataRequestMessageWrapper odataRequestMessageWrapper = Util.NullCheck<ODataRequestMessageWrapper>(perRequest.Request, InternalError.InvalidEndWriteRequest);
					asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(odataRequestMessageWrapper.BeginGetResponse), new AsyncCallback(this.AsyncEndGetResponse), asyncStateBag);
					perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				}
			}
			catch (Exception ex)
			{
				if (this.HandleFailure(perRequest, ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000552C File Offset: 0x0000372C
		private void AsyncEndWrite(IAsyncResult asyncResult)
		{
			BaseAsyncResult.AsyncStateBag asyncStateBag = asyncResult.AsyncState as BaseAsyncResult.AsyncStateBag;
			BaseAsyncResult.PerRequest perRequest = ((asyncStateBag == null) ? null : asyncStateBag.PerRequest);
			try
			{
				this.CompleteCheck(perRequest, InternalError.InvalidEndWriteCompleted);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				BaseAsyncResult.EqualRefCheck(this.perRequest, perRequest, InternalError.InvalidEndWrite);
				ContentStream requestContentStream = perRequest.RequestContentStream;
				Util.NullCheck<ContentStream>(requestContentStream, InternalError.InvalidEndWriteStream);
				Util.NullCheck<Stream>(requestContentStream.Stream, InternalError.InvalidEndWriteStream);
				Stream stream = Util.NullCheck<Stream>(perRequest.RequestStream, InternalError.InvalidEndWriteStream);
				stream.EndWrite(asyncResult);
				if (!asyncResult.CompletedSynchronously)
				{
					asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(requestContentStream.Stream.BeginRead), perRequest.RequestContentBuffer, 0, perRequest.RequestContentBuffer.Length, new AsyncCallback(this.AsyncRequestContentEndRead), asyncStateBag);
					perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				}
			}
			catch (Exception ex)
			{
				if (this.HandleFailure(perRequest, ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x040001A1 RID: 417
		private const int True = 1;

		// Token: 0x040001A2 RID: 418
		private const int False = 0;

		// Token: 0x040001A3 RID: 419
		internal readonly object Source;

		// Token: 0x040001A4 RID: 420
		internal readonly string Method;

		// Token: 0x040001A5 RID: 421
		protected BaseAsyncResult.PerRequest perRequest;

		// Token: 0x040001A6 RID: 422
		private readonly AsyncCallback userCallback;

		// Token: 0x040001A7 RID: 423
		private readonly object userState;

		// Token: 0x040001A8 RID: 424
		private ManualResetEvent asyncWait;

		// Token: 0x040001A9 RID: 425
		private Exception failure;

		// Token: 0x040001AA RID: 426
		private ODataRequestMessageWrapper abortable;

		// Token: 0x040001AB RID: 427
		private int completedSynchronously = 1;

		// Token: 0x040001AC RID: 428
		private bool userCompleted;

		// Token: 0x040001AD RID: 429
		private int completed;

		// Token: 0x040001AE RID: 430
		private int userNotified;

		// Token: 0x040001AF RID: 431
		private int done;

		// Token: 0x040001B0 RID: 432
		private bool asyncWaitDisposed;

		// Token: 0x040001B1 RID: 433
		private object asyncWaitDisposeLock;

		// Token: 0x02000023 RID: 35
		// (Invoke) Token: 0x060000E4 RID: 228
		internal delegate IAsyncResult AsyncAction(byte[] buffer, int offset, int length, AsyncCallback asyncCallback, object state);

		// Token: 0x02000024 RID: 36
		protected sealed class AsyncStateBag
		{
			// Token: 0x060000E7 RID: 231 RVA: 0x0000562C File Offset: 0x0000382C
			internal AsyncStateBag(BaseAsyncResult.PerRequest pereq)
			{
				this.PerRequest = pereq;
			}

			// Token: 0x040001B2 RID: 434
			internal readonly BaseAsyncResult.PerRequest PerRequest;
		}

		// Token: 0x02000025 RID: 37
		protected sealed class PerRequest
		{
			// Token: 0x060000E8 RID: 232 RVA: 0x0000563B File Offset: 0x0000383B
			internal PerRequest()
			{
				this.requestCompletedSynchronously = 1;
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005655 File Offset: 0x00003855
			// (set) Token: 0x060000EA RID: 234 RVA: 0x0000565D File Offset: 0x0000385D
			internal ODataRequestMessageWrapper Request { get; set; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060000EB RID: 235 RVA: 0x00005666 File Offset: 0x00003866
			// (set) Token: 0x060000EC RID: 236 RVA: 0x0000566E File Offset: 0x0000386E
			internal Stream RequestStream { get; set; }

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000ED RID: 237 RVA: 0x00005677 File Offset: 0x00003877
			// (set) Token: 0x060000EE RID: 238 RVA: 0x0000567F File Offset: 0x0000387F
			internal ContentStream RequestContentStream { get; set; }

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060000EF RID: 239 RVA: 0x00005688 File Offset: 0x00003888
			// (set) Token: 0x060000F0 RID: 240 RVA: 0x00005690 File Offset: 0x00003890
			internal IODataResponseMessage ResponseMessage { get; set; }

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005699 File Offset: 0x00003899
			// (set) Token: 0x060000F2 RID: 242 RVA: 0x000056A1 File Offset: 0x000038A1
			internal Stream ResponseStream { get; set; }

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000F3 RID: 243 RVA: 0x000056AA File Offset: 0x000038AA
			internal bool RequestCompletedSynchronously
			{
				get
				{
					return this.requestCompletedSynchronously == 1;
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000F4 RID: 244 RVA: 0x000056B5 File Offset: 0x000038B5
			internal bool RequestCompleted
			{
				get
				{
					return this.requestStatus != 0;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000F5 RID: 245 RVA: 0x000056C3 File Offset: 0x000038C3
			internal bool RequestAborted
			{
				get
				{
					return this.requestStatus == 2;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x000056CE File Offset: 0x000038CE
			internal byte[] RequestContentBuffer
			{
				get
				{
					if (this.requestContentBuffer == null)
					{
						this.requestContentBuffer = new byte[65536];
					}
					return this.requestContentBuffer;
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000F7 RID: 247 RVA: 0x000056EE File Offset: 0x000038EE
			// (set) Token: 0x060000F8 RID: 248 RVA: 0x000056F6 File Offset: 0x000038F6
			internal int RequestContentBufferValidLength { get; set; }

			// Token: 0x060000F9 RID: 249 RVA: 0x000056FF File Offset: 0x000038FF
			internal void SetRequestCompletedSynchronously(bool completedSynchronously)
			{
				Interlocked.CompareExchange(ref this.requestCompletedSynchronously, completedSynchronously ? 1 : 0, 1);
			}

			// Token: 0x060000FA RID: 250 RVA: 0x00005715 File Offset: 0x00003915
			internal void SetComplete()
			{
				Interlocked.CompareExchange(ref this.requestStatus, 1, 0);
			}

			// Token: 0x060000FB RID: 251 RVA: 0x00005725 File Offset: 0x00003925
			internal void SetAborted()
			{
				Interlocked.Exchange(ref this.requestStatus, 2);
			}

			// Token: 0x060000FC RID: 252 RVA: 0x00005734 File Offset: 0x00003934
			internal void Dispose()
			{
				if (this.isDisposed)
				{
					return;
				}
				lock (this.disposeLock)
				{
					if (!this.isDisposed)
					{
						this.isDisposed = true;
						if (this.ResponseStream != null)
						{
							this.ResponseStream.Dispose();
							this.ResponseStream = null;
						}
						if (this.RequestContentStream != null)
						{
							if (this.RequestContentStream.Stream != null && this.RequestContentStream.IsKnownMemoryStream)
							{
								this.RequestContentStream.Stream.Dispose();
							}
							this.RequestContentStream = null;
						}
						if (this.RequestStream != null)
						{
							try
							{
								this.RequestStream.Dispose();
								this.RequestStream = null;
							}
							catch (Exception ex)
							{
								if (!this.RequestAborted || !CommonUtil.IsCatchableExceptionType(ex))
								{
									throw;
								}
							}
						}
						WebUtil.DisposeMessage(this.ResponseMessage);
						this.Request = null;
						this.SetComplete();
					}
				}
			}

			// Token: 0x040001B3 RID: 435
			private const int True = 1;

			// Token: 0x040001B4 RID: 436
			private const int False = 0;

			// Token: 0x040001B5 RID: 437
			private int requestStatus;

			// Token: 0x040001B6 RID: 438
			private byte[] requestContentBuffer;

			// Token: 0x040001B7 RID: 439
			private bool isDisposed;

			// Token: 0x040001B8 RID: 440
			private object disposeLock = new object();

			// Token: 0x040001B9 RID: 441
			private int requestCompletedSynchronously;
		}
	}
}
