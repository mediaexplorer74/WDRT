using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000222 RID: 546
	internal class AsyncProtocolRequest
	{
		// Token: 0x06001407 RID: 5127 RVA: 0x0006A63A File Offset: 0x0006883A
		public AsyncProtocolRequest(LazyAsyncResult userAsyncResult)
		{
			this.UserAsyncResult = userAsyncResult;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0006A649 File Offset: 0x00068849
		public void SetNextRequest(byte[] buffer, int offset, int count, AsyncProtocolCallback callback)
		{
			if (this._CompletionStatus != 0)
			{
				throw new InternalException();
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Count = count;
			this._Callback = callback;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0006A676 File Offset: 0x00068876
		internal object AsyncObject
		{
			get
			{
				return this.UserAsyncResult.AsyncObject;
			}
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0006A684 File Offset: 0x00068884
		internal void CompleteRequest(int result)
		{
			this.Result = result;
			int num = Interlocked.Exchange(ref this._CompletionStatus, 1);
			if (num == 1)
			{
				throw new InternalException();
			}
			if (num == 2)
			{
				this._CompletionStatus = 0;
				this._Callback(this);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0006A6C8 File Offset: 0x000688C8
		public bool MustCompleteSynchronously
		{
			get
			{
				int num = Interlocked.Exchange(ref this._CompletionStatus, 2);
				if (num == 2)
				{
					throw new InternalException();
				}
				if (num == 1)
				{
					this._CompletionStatus = 0;
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0006A6FA File Offset: 0x000688FA
		internal void CompleteWithError(Exception e)
		{
			this.UserAsyncResult.InvokeCallback(e);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0006A708 File Offset: 0x00068908
		internal void CompleteUser()
		{
			this.UserAsyncResult.InvokeCallback();
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0006A715 File Offset: 0x00068915
		internal void CompleteUser(object userResult)
		{
			this.UserAsyncResult.InvokeCallback(userResult);
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0006A723 File Offset: 0x00068923
		internal bool IsUserCompleted
		{
			get
			{
				return this.UserAsyncResult.InternalPeekCompleted;
			}
		}

		// Token: 0x040015FB RID: 5627
		private AsyncProtocolCallback _Callback;

		// Token: 0x040015FC RID: 5628
		private int _CompletionStatus;

		// Token: 0x040015FD RID: 5629
		private const int StatusNotStarted = 0;

		// Token: 0x040015FE RID: 5630
		private const int StatusCompleted = 1;

		// Token: 0x040015FF RID: 5631
		private const int StatusCheckedOnSyncCompletion = 2;

		// Token: 0x04001600 RID: 5632
		public LazyAsyncResult UserAsyncResult;

		// Token: 0x04001601 RID: 5633
		public int Result;

		// Token: 0x04001602 RID: 5634
		public object AsyncState;

		// Token: 0x04001603 RID: 5635
		public byte[] Buffer;

		// Token: 0x04001604 RID: 5636
		public int Offset;

		// Token: 0x04001605 RID: 5637
		public int Count;
	}
}
