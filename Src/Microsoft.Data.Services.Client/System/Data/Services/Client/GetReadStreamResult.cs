using System;
using System.IO;
using System.Net;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x020000B4 RID: 180
	internal class GetReadStreamResult : BaseAsyncResult
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x00015E4C File Offset: 0x0001404C
		internal GetReadStreamResult(DataServiceContext context, string method, ODataRequestMessageWrapper request, AsyncCallback callback, object state, StreamDescriptor streamDescriptor)
			: base(context, method, callback, state)
		{
			this.requestMessage = request;
			base.Abortable = request;
			this.streamDescriptor = streamDescriptor;
			this.requestInfo = new RequestInfo(context);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00015E7C File Offset: 0x0001407C
		internal void Begin()
		{
			try
			{
				IAsyncResult asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(this.requestMessage.BeginGetResponse), new AsyncCallback(this.AsyncEndGetResponse), null);
				base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
			}
			catch (Exception ex)
			{
				base.HandleFailure(ex);
				throw;
			}
			finally
			{
				base.HandleCompleted();
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00015EEC File Offset: 0x000140EC
		internal DataServiceStreamResponse End()
		{
			if (this.responseMessage != null)
			{
				this.streamDescriptor.ETag = this.responseMessage.GetHeader("ETag");
				this.streamDescriptor.ContentType = this.responseMessage.GetHeader("Content-Type");
				return new DataServiceStreamResponse(this.responseMessage);
			}
			return null;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00015F48 File Offset: 0x00014148
		internal DataServiceStreamResponse Execute()
		{
			try
			{
				this.responseMessage = this.requestInfo.GetSyncronousResponse(this.requestMessage, true);
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
			return this.End();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00015FBC File Offset: 0x000141BC
		protected override void CompletedRequest()
		{
			if (this.responseMessage != null)
			{
				InvalidOperationException ex = null;
				if (!WebUtil.SuccessStatusCode((HttpStatusCode)this.responseMessage.StatusCode))
				{
					ex = BaseSaveResult.GetResponseText(new Func<Stream>(this.responseMessage.GetStream), (HttpStatusCode)this.responseMessage.StatusCode);
				}
				if (ex != null)
				{
					WebUtil.DisposeMessage(this.responseMessage);
					base.HandleFailure(ex);
				}
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001601E File Offset: 0x0001421E
		protected override void HandleCompleted(BaseAsyncResult.PerRequest pereq)
		{
			Error.ThrowInternalError(InternalError.InvalidHandleCompleted);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00016028 File Offset: 0x00014228
		protected override void AsyncEndGetResponse(IAsyncResult asyncResult)
		{
			try
			{
				base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
				ODataRequestMessageWrapper odataRequestMessageWrapper = Util.NullCheck<ODataRequestMessageWrapper>(this.requestMessage, InternalError.InvalidEndGetResponseRequest);
				this.responseMessage = this.requestInfo.EndGetResponse(odataRequestMessageWrapper, asyncResult);
				base.SetCompleted();
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
				base.HandleCompleted();
			}
		}

		// Token: 0x0400031C RID: 796
		private readonly ODataRequestMessageWrapper requestMessage;

		// Token: 0x0400031D RID: 797
		private readonly StreamDescriptor streamDescriptor;

		// Token: 0x0400031E RID: 798
		private readonly RequestInfo requestInfo;

		// Token: 0x0400031F RID: 799
		private IODataResponseMessage responseMessage;
	}
}
