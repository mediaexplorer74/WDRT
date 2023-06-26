using System;

namespace System.Net.Sockets
{
	// Token: 0x02000377 RID: 887
	internal class ConnectAsyncResult : ContextAwareResult
	{
		// Token: 0x0600211B RID: 8475 RVA: 0x0009ED6B File Offset: 0x0009CF6B
		internal ConnectAsyncResult(object myObject, EndPoint endPoint, object myState, AsyncCallback myCallBack)
			: base(myObject, myState, myCallBack)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x0009ED7E File Offset: 0x0009CF7E
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.m_EndPoint;
			}
		}

		// Token: 0x04001E4D RID: 7757
		private EndPoint m_EndPoint;
	}
}
