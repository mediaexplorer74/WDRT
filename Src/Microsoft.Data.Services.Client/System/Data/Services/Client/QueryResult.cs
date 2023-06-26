using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000068 RID: 104
	internal class QueryResult : BaseAsyncResult
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0000ED8E File Offset: 0x0000CF8E
		internal QueryResult(object source, string method, DataServiceRequest serviceRequest, ODataRequestMessageWrapper request, RequestInfo requestInfo, AsyncCallback callback, object state)
			: base(source, method, callback, state)
		{
			this.ServiceRequest = serviceRequest;
			this.Request = request;
			this.RequestInfo = requestInfo;
			base.Abortable = request;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000EDBB File Offset: 0x0000CFBB
		internal QueryResult(object source, string method, DataServiceRequest serviceRequest, ODataRequestMessageWrapper request, RequestInfo requestInfo, AsyncCallback callback, object state, ContentStream requestContentStream)
			: this(source, method, serviceRequest, request, requestInfo, callback, state)
		{
			this.requestContentStream = requestContentStream;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
		internal long ContentLength
		{
			get
			{
				return this.contentLength;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000EDDE File Offset: 0x0000CFDE
		internal string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000EDE6 File Offset: 0x0000CFE6
		internal HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000EDEE File Offset: 0x0000CFEE
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000EDF6 File Offset: 0x0000CFF6
		internal bool AllowDirectNetworkStreamReading { get; set; }

		// Token: 0x06000379 RID: 889 RVA: 0x0000EE00 File Offset: 0x0000D000
		internal static QueryResult EndExecuteQuery<TElement>(object source, string method, IAsyncResult asyncResult)
		{
			QueryResult queryResult = null;
			try
			{
				queryResult = BaseAsyncResult.EndExecute<QueryResult>(source, method, asyncResult);
			}
			catch (InvalidOperationException ex)
			{
				queryResult = asyncResult as QueryResult;
				QueryOperationResponse response = queryResult.GetResponse<TElement>(MaterializeAtom.EmptyResults);
				if (response != null)
				{
					response.Error = ex;
					throw new DataServiceQueryException(Strings.DataServiceException_GeneralError, ex, response);
				}
				throw;
			}
			return queryResult;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000EE58 File Offset: 0x0000D058
		internal Stream GetResponseStream()
		{
			return this.outputResponseStream;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000EE60 File Offset: 0x0000D060
		internal void BeginExecuteQuery()
		{
			BaseAsyncResult.PerRequest perRequest = new BaseAsyncResult.PerRequest();
			BaseAsyncResult.AsyncStateBag asyncStateBag = new BaseAsyncResult.AsyncStateBag(perRequest);
			perRequest.Request = this.Request;
			this.perRequest = perRequest;
			try
			{
				IAsyncResult asyncResult;
				if (this.requestContentStream != null && this.requestContentStream.Stream != null)
				{
					if (this.requestContentStream.IsKnownMemoryStream)
					{
						this.Request.SetContentLengthHeader();
					}
					this.perRequest.RequestContentStream = this.requestContentStream;
					asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(this.Request.BeginGetRequestStream), new AsyncCallback(base.AsyncEndGetRequestStream), asyncStateBag);
				}
				else
				{
					asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(this.Request.BeginGetResponse), new AsyncCallback(this.AsyncEndGetResponse), asyncStateBag);
				}
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
			}
			catch (Exception ex)
			{
				base.HandleFailure(ex);
				throw;
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000EF64 File Offset: 0x0000D164
		internal IODataResponseMessage ExecuteQuery()
		{
			try
			{
				if (this.requestContentStream != null && this.requestContentStream.Stream != null)
				{
					this.Request.SetRequestStream(this.requestContentStream);
				}
				IODataResponseMessage syncronousResponse = this.RequestInfo.GetSyncronousResponse(this.Request, true);
				this.SetHttpWebResponse(Util.NullCheck<IODataResponseMessage>(syncronousResponse, InternalError.InvalidGetResponse));
				if (this.AllowDirectNetworkStreamReading)
				{
					this.GetStreamFromResponseMessage();
				}
				else
				{
					this.CopyStreamFromResponseMessage();
				}
			}
			catch (Exception ex)
			{
				base.HandleFailure(ex);
				throw;
			}
			finally
			{
				base.SetCompleted();
				this.CompletedRequest();
			}
			if (base.Failure != null)
			{
				throw base.Failure;
			}
			if (!this.AllowDirectNetworkStreamReading)
			{
				return null;
			}
			return this.responseMessage;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000F028 File Offset: 0x0000D228
		internal QueryOperationResponse<TElement> GetResponse<TElement>(MaterializeAtom results)
		{
			if (this.responseMessage != null)
			{
				HeaderCollection headerCollection = new HeaderCollection(this.responseMessage);
				return new QueryOperationResponse<TElement>(headerCollection, this.ServiceRequest, results)
				{
					StatusCode = this.responseMessage.StatusCode
				};
			}
			return null;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000F06C File Offset: 0x0000D26C
		internal QueryOperationResponse GetResponseWithType(MaterializeAtom results, Type elementType)
		{
			if (this.responseMessage != null)
			{
				HeaderCollection headerCollection = new HeaderCollection(this.responseMessage);
				QueryOperationResponse instance = QueryOperationResponse.GetInstance(elementType, headerCollection, this.ServiceRequest, results);
				instance.StatusCode = this.responseMessage.StatusCode;
				return instance;
			}
			return null;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		internal MaterializeAtom GetMaterializer(ProjectionPlan plan)
		{
			MaterializeAtom materializeAtom;
			if (HttpStatusCode.NoContent != this.StatusCode)
			{
				materializeAtom = this.CreateMaterializer(plan, ODataPayloadKind.Unsupported);
			}
			else
			{
				materializeAtom = MaterializeAtom.EmptyResults;
			}
			return materializeAtom;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		internal QueryOperationResponse<TElement> ProcessResult<TElement>(ProjectionPlan plan)
		{
			MaterializeAtom materializeAtom = this.CreateMaterializer(plan, this.ServiceRequest.PayloadKind);
			return this.GetResponse<TElement>(materializeAtom);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000F108 File Offset: 0x0000D308
		protected override void CompletedRequest()
		{
			byte[] array = this.asyncStreamCopyBuffer;
			this.asyncStreamCopyBuffer = null;
			if (array != null && !this.usingBuffer)
			{
				this.PutAsyncResponseStreamCopyBuffer(array);
			}
			if (this.responseStreamOwner && this.outputResponseStream != null)
			{
				this.outputResponseStream.Position = 0L;
			}
			if (this.responseMessage != null)
			{
				if (!this.AllowDirectNetworkStreamReading)
				{
					WebUtil.DisposeMessage(this.responseMessage);
				}
				Version version;
				Exception ex = BaseSaveResult.HandleResponse(this.RequestInfo, this.StatusCode, this.responseMessage.GetHeader("DataServiceVersion"), new Func<Stream>(this.GetResponseStream), false, out version);
				if (ex != null)
				{
					base.HandleFailure(ex);
					return;
				}
				this.responseInfo = this.CreateResponseInfo();
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		protected virtual ResponseInfo CreateResponseInfo()
		{
			return this.RequestInfo.GetDeserializationInfo(null);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000F1DC File Offset: 0x0000D3DC
		protected virtual Stream GetAsyncResponseStreamCopy()
		{
			this.responseStreamOwner = true;
			long num = this.contentLength;
			if (0L < num && num <= 2147483647L)
			{
				return new MemoryStream((int)num);
			}
			return new MemoryStream();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000F212 File Offset: 0x0000D412
		protected virtual byte[] GetAsyncResponseStreamCopyBuffer()
		{
			return Interlocked.Exchange<byte[]>(ref QueryResult.reusableAsyncCopyBuffer, null) ?? new byte[8000];
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000F22D File Offset: 0x0000D42D
		protected virtual void PutAsyncResponseStreamCopyBuffer(byte[] buffer)
		{
			QueryResult.reusableAsyncCopyBuffer = buffer;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000F238 File Offset: 0x0000D438
		protected virtual void SetHttpWebResponse(IODataResponseMessage response)
		{
			this.responseMessage = response;
			this.statusCode = (HttpStatusCode)response.StatusCode;
			string header = response.GetHeader("Content-Length");
			if (header != null)
			{
				this.contentLength = (long)int.Parse(header, CultureInfo.InvariantCulture);
			}
			else
			{
				this.contentLength = -1L;
			}
			this.contentType = response.GetHeader("Content-Type");
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000F294 File Offset: 0x0000D494
		protected override void HandleCompleted(BaseAsyncResult.PerRequest pereq)
		{
			if (pereq != null)
			{
				base.SetCompletedSynchronously(pereq.RequestCompletedSynchronously);
				if (pereq.RequestCompleted)
				{
					Interlocked.CompareExchange<BaseAsyncResult.PerRequest>(ref this.perRequest, null, pereq);
					pereq.Dispose();
				}
			}
			base.HandleCompleted();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		protected override void AsyncEndGetResponse(IAsyncResult asyncResult)
		{
			BaseAsyncResult.AsyncStateBag asyncStateBag = asyncResult.AsyncState as BaseAsyncResult.AsyncStateBag;
			BaseAsyncResult.PerRequest perRequest = ((asyncStateBag == null) ? null : asyncStateBag.PerRequest);
			try
			{
				if (base.IsAborted)
				{
					if (perRequest != null)
					{
						perRequest.SetComplete();
					}
					base.SetCompleted();
				}
				else
				{
					this.CompleteCheck(perRequest, InternalError.InvalidEndGetResponseCompleted);
					perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
					base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
					ODataRequestMessageWrapper odataRequestMessageWrapper = Util.NullCheck<ODataRequestMessageWrapper>(perRequest.Request, InternalError.InvalidEndGetResponseRequest);
					IODataResponseMessage iodataResponseMessage = this.RequestInfo.EndGetResponse(odataRequestMessageWrapper, asyncResult);
					perRequest.ResponseMessage = Util.NullCheck<IODataResponseMessage>(iodataResponseMessage, InternalError.InvalidEndGetResponseResponse);
					this.SetHttpWebResponse(perRequest.ResponseMessage);
					Stream stream = null;
					if (204 != iodataResponseMessage.StatusCode)
					{
						stream = iodataResponseMessage.GetStream();
						perRequest.ResponseStream = stream;
					}
					if (stream != null && stream.CanRead)
					{
						if (this.outputResponseStream == null)
						{
							this.outputResponseStream = Util.NullCheck<Stream>(this.GetAsyncResponseStreamCopy(), InternalError.InvalidAsyncResponseStreamCopy);
						}
						if (this.asyncStreamCopyBuffer == null)
						{
							this.asyncStreamCopyBuffer = Util.NullCheck<byte[]>(this.GetAsyncResponseStreamCopyBuffer(), InternalError.InvalidAsyncResponseStreamCopyBuffer);
						}
						this.ReadResponseStream(asyncStateBag);
					}
					else
					{
						perRequest.SetComplete();
						base.SetCompleted();
					}
				}
			}
			catch (Exception ex)
			{
				if (base.HandleFailure(ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000F414 File Offset: 0x0000D614
		protected override void CompleteCheck(BaseAsyncResult.PerRequest pereq, InternalError errorcode)
		{
			if (pereq == null || ((pereq.RequestCompleted || base.IsCompletedInternally) && !base.IsAborted && !pereq.RequestAborted))
			{
				Error.ThrowInternalError(errorcode);
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000F440 File Offset: 0x0000D640
		private void ReadResponseStream(BaseAsyncResult.AsyncStateBag asyncStateBag)
		{
			BaseAsyncResult.PerRequest perRequest = asyncStateBag.PerRequest;
			byte[] array = this.asyncStreamCopyBuffer;
			Stream responseStream = perRequest.ResponseStream;
			IAsyncResult asyncResult;
			do
			{
				int num = 0;
				int num2 = array.Length;
				this.usingBuffer = true;
				asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(responseStream.BeginRead), array, num, num2, new AsyncCallback(this.AsyncEndRead), asyncStateBag);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
			}
			while (asyncResult.CompletedSynchronously && !perRequest.RequestCompleted && !base.IsCompletedInternally && responseStream.CanRead);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		private void AsyncEndRead(IAsyncResult asyncResult)
		{
			BaseAsyncResult.AsyncStateBag asyncStateBag = asyncResult.AsyncState as BaseAsyncResult.AsyncStateBag;
			BaseAsyncResult.PerRequest perRequest = ((asyncStateBag == null) ? null : asyncStateBag.PerRequest);
			try
			{
				this.CompleteCheck(perRequest, InternalError.InvalidEndReadCompleted);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
				Stream stream = Util.NullCheck<Stream>(perRequest.ResponseStream, InternalError.InvalidEndReadStream);
				Stream stream2 = Util.NullCheck<Stream>(this.outputResponseStream, InternalError.InvalidEndReadCopy);
				byte[] array = Util.NullCheck<byte[]>(this.asyncStreamCopyBuffer, InternalError.InvalidEndReadBuffer);
				int num = stream.EndRead(asyncResult);
				this.usingBuffer = false;
				if (0 < num)
				{
					stream2.Write(array, 0, num);
				}
				if (0 < num && 0 < array.Length && stream.CanRead)
				{
					if (!asyncResult.CompletedSynchronously)
					{
						this.ReadResponseStream(asyncStateBag);
					}
				}
				else
				{
					if (stream2.Position < stream2.Length)
					{
						((MemoryStream)stream2).SetLength(stream2.Position);
					}
					perRequest.SetComplete();
					base.SetCompleted();
				}
			}
			catch (Exception ex)
			{
				if (base.HandleFailure(ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
		private MaterializeAtom CreateMaterializer(ProjectionPlan plan, ODataPayloadKind payloadKind)
		{
			QueryComponents queryComponents = this.ServiceRequest.QueryComponents(this.responseInfo.Model);
			if (plan != null || queryComponents.Projection != null)
			{
				this.RequestInfo.TypeResolver.IsProjectionRequest();
			}
			HttpWebResponseMessage httpWebResponseMessage = new HttpWebResponseMessage(new HeaderCollection(this.responseMessage), this.responseMessage.StatusCode, new Func<Stream>(this.GetResponseStream), this.AllowDirectNetworkStreamReading ? this.responseMessage : null);
			return DataServiceRequest.Materialize(this.responseInfo, queryComponents, plan, this.ContentType, httpWebResponseMessage, payloadKind);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000F680 File Offset: 0x0000D880
		private void CopyStreamFromResponseMessage()
		{
			if (HttpStatusCode.NoContent != this.StatusCode)
			{
				using (Stream stream = this.responseMessage.GetStream())
				{
					if (stream != null)
					{
						Stream asyncResponseStreamCopy = this.GetAsyncResponseStreamCopy();
						this.outputResponseStream = asyncResponseStreamCopy;
						byte[] asyncResponseStreamCopyBuffer = this.GetAsyncResponseStreamCopyBuffer();
						long num = WebUtil.CopyStream(stream, asyncResponseStreamCopy, ref asyncResponseStreamCopyBuffer);
						if (this.responseStreamOwner)
						{
							if (0L == num)
							{
								this.outputResponseStream = null;
							}
							else if (asyncResponseStreamCopy.Position < asyncResponseStreamCopy.Length)
							{
								((MemoryStream)asyncResponseStreamCopy).SetLength(asyncResponseStreamCopy.Position);
							}
						}
						this.PutAsyncResponseStreamCopyBuffer(asyncResponseStreamCopyBuffer);
					}
				}
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000F720 File Offset: 0x0000D920
		private void GetStreamFromResponseMessage()
		{
			if (HttpStatusCode.NoContent != this.StatusCode)
			{
				Stream stream = this.responseMessage.GetStream();
				if (stream != null)
				{
					this.outputResponseStream = stream;
					return;
				}
			}
			else
			{
				WebUtil.DisposeMessage(this.responseMessage);
			}
		}

		// Token: 0x04000299 RID: 665
		internal readonly DataServiceRequest ServiceRequest;

		// Token: 0x0400029A RID: 666
		internal readonly RequestInfo RequestInfo;

		// Token: 0x0400029B RID: 667
		internal readonly ODataRequestMessageWrapper Request;

		// Token: 0x0400029C RID: 668
		private static byte[] reusableAsyncCopyBuffer;

		// Token: 0x0400029D RID: 669
		private ContentStream requestContentStream;

		// Token: 0x0400029E RID: 670
		private IODataResponseMessage responseMessage;

		// Token: 0x0400029F RID: 671
		private ResponseInfo responseInfo;

		// Token: 0x040002A0 RID: 672
		private byte[] asyncStreamCopyBuffer;

		// Token: 0x040002A1 RID: 673
		private Stream outputResponseStream;

		// Token: 0x040002A2 RID: 674
		private string contentType;

		// Token: 0x040002A3 RID: 675
		private long contentLength;

		// Token: 0x040002A4 RID: 676
		private HttpStatusCode statusCode;

		// Token: 0x040002A5 RID: 677
		private bool responseStreamOwner;

		// Token: 0x040002A6 RID: 678
		private bool usingBuffer;
	}
}
