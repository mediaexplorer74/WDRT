using System;
using System.Threading;

namespace System.IO.Compression
{
	// Token: 0x02000427 RID: 1063
	internal class DeflateStreamAsyncResult : IAsyncResult
	{
		// Token: 0x060027D1 RID: 10193 RVA: 0x000B71BA File Offset: 0x000B53BA
		public DeflateStreamAsyncResult(object asyncObject, object asyncState, AsyncCallback asyncCallback, byte[] buffer, int offset, int count)
		{
			this.buffer = buffer;
			this.offset = offset;
			this.count = count;
			this.m_CompletedSynchronously = true;
			this.m_AsyncObject = asyncObject;
			this.m_AsyncState = asyncState;
			this.m_AsyncCallback = asyncCallback;
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060027D2 RID: 10194 RVA: 0x000B71F6 File Offset: 0x000B53F6
		public object AsyncState
		{
			get
			{
				return this.m_AsyncState;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x000B7200 File Offset: 0x000B5400
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				int completed = this.m_Completed;
				if (this.m_Event == null)
				{
					Interlocked.CompareExchange(ref this.m_Event, new ManualResetEvent(completed != 0), null);
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				if (completed == 0 && this.m_Completed != 0)
				{
					manualResetEvent.Set();
				}
				return manualResetEvent;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060027D4 RID: 10196 RVA: 0x000B7251 File Offset: 0x000B5451
		public bool CompletedSynchronously
		{
			get
			{
				return this.m_CompletedSynchronously;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x000B7259 File Offset: 0x000B5459
		public bool IsCompleted
		{
			get
			{
				return this.m_Completed != 0;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x000B7264 File Offset: 0x000B5464
		internal object Result
		{
			get
			{
				return this.m_Result;
			}
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000B726C File Offset: 0x000B546C
		internal void Close()
		{
			if (this.m_Event != null)
			{
				((ManualResetEvent)this.m_Event).Close();
			}
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000B7286 File Offset: 0x000B5486
		internal void InvokeCallback(bool completedSynchronously, object result)
		{
			this.Complete(completedSynchronously, result);
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x000B7290 File Offset: 0x000B5490
		internal void InvokeCallback(object result)
		{
			this.Complete(result);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000B7299 File Offset: 0x000B5499
		private void Complete(bool completedSynchronously, object result)
		{
			this.m_CompletedSynchronously = completedSynchronously;
			this.Complete(result);
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000B72AC File Offset: 0x000B54AC
		private void Complete(object result)
		{
			this.m_Result = result;
			Interlocked.Increment(ref this.m_Completed);
			if (this.m_Event != null)
			{
				((ManualResetEvent)this.m_Event).Set();
			}
			if (Interlocked.Increment(ref this.m_InvokedCallback) == 1 && this.m_AsyncCallback != null)
			{
				this.m_AsyncCallback(this);
			}
		}

		// Token: 0x0400218A RID: 8586
		public byte[] buffer;

		// Token: 0x0400218B RID: 8587
		public int offset;

		// Token: 0x0400218C RID: 8588
		public int count;

		// Token: 0x0400218D RID: 8589
		public bool isWrite;

		// Token: 0x0400218E RID: 8590
		private object m_AsyncObject;

		// Token: 0x0400218F RID: 8591
		private object m_AsyncState;

		// Token: 0x04002190 RID: 8592
		private AsyncCallback m_AsyncCallback;

		// Token: 0x04002191 RID: 8593
		private object m_Result;

		// Token: 0x04002192 RID: 8594
		internal bool m_CompletedSynchronously;

		// Token: 0x04002193 RID: 8595
		private int m_InvokedCallback;

		// Token: 0x04002194 RID: 8596
		private int m_Completed;

		// Token: 0x04002195 RID: 8597
		private object m_Event;
	}
}
