using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001BD RID: 445
	internal class ListenerAsyncResult : LazyAsyncResult
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0005E875 File Offset: 0x0005CA75
		internal static IOCompletionCallback IOCallback
		{
			get
			{
				return ListenerAsyncResult.s_IOCallback;
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0005E87C File Offset: 0x0005CA7C
		internal ListenerAsyncResult(object asyncObject, object userState, AsyncCallback callback)
			: base(asyncObject, userState, callback)
		{
			this.m_RequestContext = new AsyncRequestContext(this);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0005E894 File Offset: 0x0005CA94
		private unsafe static void IOCompleted(ListenerAsyncResult asyncResult, uint errorCode, uint numBytes)
		{
			object obj = null;
			try
			{
				if (errorCode != 0U && errorCode != 234U)
				{
					asyncResult.ErrorCode = (int)errorCode;
					obj = new HttpListenerException((int)errorCode);
				}
				else
				{
					HttpListener httpListener = asyncResult.AsyncObject as HttpListener;
					if (errorCode == 0U)
					{
						bool flag = false;
						try
						{
							if (httpListener.ValidateRequest(asyncResult.m_RequestContext))
							{
								obj = httpListener.HandleAuthentication(asyncResult.m_RequestContext, out flag);
							}
							goto IL_92;
						}
						finally
						{
							if (flag)
							{
								asyncResult.m_RequestContext = ((obj == null) ? new AsyncRequestContext(asyncResult) : null);
							}
							else
							{
								asyncResult.m_RequestContext.Reset(0UL, 0U);
							}
						}
					}
					asyncResult.m_RequestContext.Reset(asyncResult.m_RequestContext.RequestBlob->RequestId, numBytes);
					IL_92:
					if (obj == null)
					{
						uint num = asyncResult.QueueBeginGetContext();
						if (num != 0U && num != 997U)
						{
							obj = new HttpListenerException((int)num);
						}
					}
					if (obj == null)
					{
						return;
					}
				}
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
				if (Logging.On)
				{
					Logging.PrintError(Logging.HttpListener, ValidationHelper.HashString(asyncResult), "IOCompleted", ex.ToString());
				}
				obj = ex;
			}
			asyncResult.InvokeCallback(obj);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0005E9B0 File Offset: 0x0005CBB0
		private unsafe static void WaitCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
			ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)overlapped.AsyncResult;
			ListenerAsyncResult.IOCompleted(listenerAsyncResult, errorCode, numBytes);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0005E9D8 File Offset: 0x0005CBD8
		internal unsafe uint QueueBeginGetContext()
		{
			uint num;
			uint num2;
			for (;;)
			{
				(base.AsyncObject as HttpListener).EnsureBoundHandle();
				num = 0U;
				num2 = UnsafeNclNativeMethods.HttpApi.HttpReceiveHttpRequest((base.AsyncObject as HttpListener).RequestQueueHandle, this.m_RequestContext.RequestBlob->RequestId, 1U, this.m_RequestContext.RequestBlob, this.m_RequestContext.Size, &num, this.m_RequestContext.NativeOverlapped);
				if (num2 == 87U && this.m_RequestContext.RequestBlob->RequestId != 0UL)
				{
					this.m_RequestContext.RequestBlob->RequestId = 0UL;
				}
				else
				{
					if (num2 != 234U)
					{
						break;
					}
					this.m_RequestContext.Reset(this.m_RequestContext.RequestBlob->RequestId, num);
				}
			}
			if (num2 == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
			{
				ListenerAsyncResult.IOCompleted(this, num2, num);
			}
			return num2;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0005EAAE File Offset: 0x0005CCAE
		protected override void Cleanup()
		{
			if (this.m_RequestContext != null)
			{
				this.m_RequestContext.ReleasePins();
				this.m_RequestContext.Close();
			}
			base.Cleanup();
		}

		// Token: 0x04001444 RID: 5188
		private static readonly IOCompletionCallback s_IOCallback = new IOCompletionCallback(ListenerAsyncResult.WaitCallback);

		// Token: 0x04001445 RID: 5189
		private AsyncRequestContext m_RequestContext;
	}
}
