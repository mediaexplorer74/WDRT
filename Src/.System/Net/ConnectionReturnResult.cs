using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001A2 RID: 418
	internal class ConnectionReturnResult
	{
		// Token: 0x06000FED RID: 4077 RVA: 0x000536EF File Offset: 0x000518EF
		internal ConnectionReturnResult()
		{
			this.m_Context = new List<ConnectionReturnResult.RequestContext>(5);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00053703 File Offset: 0x00051903
		internal ConnectionReturnResult(int capacity)
		{
			this.m_Context = new List<ConnectionReturnResult.RequestContext>(capacity);
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00053717 File Offset: 0x00051917
		internal bool IsNotEmpty
		{
			get
			{
				return this.m_Context.Count != 0;
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00053727 File Offset: 0x00051927
		internal static void Add(ref ConnectionReturnResult returnResult, HttpWebRequest request, CoreResponseData coreResponseData)
		{
			if (coreResponseData == null)
			{
				throw new InternalException();
			}
			if (returnResult == null)
			{
				returnResult = new ConnectionReturnResult();
			}
			returnResult.m_Context.Add(new ConnectionReturnResult.RequestContext(request, coreResponseData));
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00053750 File Offset: 0x00051950
		internal static void AddExceptionRange(ref ConnectionReturnResult returnResult, HttpWebRequest[] requests, Exception exception)
		{
			ConnectionReturnResult.AddExceptionRange(ref returnResult, requests, 0, exception, exception);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0005375C File Offset: 0x0005195C
		internal static void AddExceptionRange(ref ConnectionReturnResult returnResult, HttpWebRequest[] requests, int abortedPipelinedRequestIndex, Exception exception, Exception firstRequestException)
		{
			if (exception == null)
			{
				throw new InternalException();
			}
			if (returnResult == null)
			{
				returnResult = new ConnectionReturnResult(requests.Length);
			}
			for (int i = 0; i < requests.Length; i++)
			{
				if (i == abortedPipelinedRequestIndex)
				{
					returnResult.m_Context.Add(new ConnectionReturnResult.RequestContext(requests[i], firstRequestException));
				}
				else
				{
					returnResult.m_Context.Add(new ConnectionReturnResult.RequestContext(requests[i], exception));
				}
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x000537C0 File Offset: 0x000519C0
		internal static void SetResponses(ConnectionReturnResult returnResult)
		{
			if (returnResult == null)
			{
				return;
			}
			for (int i = 0; i < returnResult.m_Context.Count; i++)
			{
				try
				{
					HttpWebRequest request = returnResult.m_Context[i].Request;
					request.SetAndOrProcessResponse(returnResult.m_Context[i].CoreResponse);
				}
				catch (Exception ex)
				{
					returnResult.m_Context.RemoveRange(0, i + 1);
					if (returnResult.m_Context.Count > 0)
					{
						ThreadPool.UnsafeQueueUserWorkItem(ConnectionReturnResult.s_InvokeConnectionCallback, returnResult);
					}
					throw;
				}
			}
			returnResult.m_Context.Clear();
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0005385C File Offset: 0x00051A5C
		private static void InvokeConnectionCallback(object objectReturnResult)
		{
			ConnectionReturnResult connectionReturnResult = (ConnectionReturnResult)objectReturnResult;
			ConnectionReturnResult.SetResponses(connectionReturnResult);
		}

		// Token: 0x04001320 RID: 4896
		private static readonly WaitCallback s_InvokeConnectionCallback = new WaitCallback(ConnectionReturnResult.InvokeConnectionCallback);

		// Token: 0x04001321 RID: 4897
		private List<ConnectionReturnResult.RequestContext> m_Context;

		// Token: 0x02000746 RID: 1862
		private struct RequestContext
		{
			// Token: 0x060041C7 RID: 16839 RVA: 0x001114FB File Offset: 0x0010F6FB
			internal RequestContext(HttpWebRequest request, object coreResponse)
			{
				this.Request = request;
				this.CoreResponse = coreResponse;
			}

			// Token: 0x040031C7 RID: 12743
			internal HttpWebRequest Request;

			// Token: 0x040031C8 RID: 12744
			internal object CoreResponse;
		}
	}
}
