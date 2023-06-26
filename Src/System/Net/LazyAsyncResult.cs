using System;
using System.Diagnostics;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001BC RID: 444
	internal class LazyAsyncResult : IAsyncResult
	{
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0005E3FC File Offset: 0x0005C5FC
		private static LazyAsyncResult.ThreadContext CurrentThreadContext
		{
			get
			{
				LazyAsyncResult.ThreadContext threadContext = LazyAsyncResult.t_ThreadContext;
				if (threadContext == null)
				{
					threadContext = new LazyAsyncResult.ThreadContext();
					LazyAsyncResult.t_ThreadContext = threadContext;
				}
				return threadContext;
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0005E41F File Offset: 0x0005C61F
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = DBNull.Value;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0005E447 File Offset: 0x0005C647
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack, object result)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = result;
			this.m_IntCompleted = 1;
			if (this.m_AsyncCallback != null)
			{
				this.m_AsyncCallback(this);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0005E487 File Offset: 0x0005C687
		internal object AsyncObject
		{
			get
			{
				return this.m_AsyncObject;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0005E48F File Offset: 0x0005C68F
		public object AsyncState
		{
			get
			{
				return this.m_AsyncState;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0005E497 File Offset: 0x0005C697
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x0005E49F File Offset: 0x0005C69F
		protected AsyncCallback AsyncCallback
		{
			get
			{
				return this.m_AsyncCallback;
			}
			set
			{
				this.m_AsyncCallback = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0005E4A8 File Offset: 0x0005C6A8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				this.m_UserEvent = true;
				if (this.m_IntCompleted == 0)
				{
					Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				while (manualResetEvent == null)
				{
					this.LazilyCreateEvent(out manualResetEvent);
				}
				return manualResetEvent;
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0005E4F4 File Offset: 0x0005C6F4
		private bool LazilyCreateEvent(out ManualResetEvent waitHandle)
		{
			waitHandle = new ManualResetEvent(false);
			bool flag;
			try
			{
				if (Interlocked.CompareExchange(ref this.m_Event, waitHandle, null) == null)
				{
					if (this.InternalPeekCompleted)
					{
						waitHandle.Set();
					}
					flag = true;
				}
				else
				{
					waitHandle.Close();
					waitHandle = (ManualResetEvent)this.m_Event;
					flag = false;
				}
			}
			catch
			{
				this.m_Event = null;
				if (waitHandle != null)
				{
					waitHandle.Close();
				}
				throw;
			}
			return flag;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0005E56C File Offset: 0x0005C76C
		[Conditional("DEBUG")]
		protected void DebugProtectState(bool protect)
		{
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0005E570 File Offset: 0x0005C770
		public bool CompletedSynchronously
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return num > 0;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0005E5A0 File Offset: 0x0005C7A0
		public bool IsCompleted
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return (num & int.MaxValue) != 0;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0005E5D3 File Offset: 0x0005C7D3
		internal bool InternalPeekCompleted
		{
			get
			{
				return (this.m_IntCompleted & int.MaxValue) != 0;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0005E5E4 File Offset: 0x0005C7E4
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x0005E5FB File Offset: 0x0005C7FB
		internal object Result
		{
			get
			{
				if (this.m_Result != DBNull.Value)
				{
					return this.m_Result;
				}
				return null;
			}
			set
			{
				this.m_Result = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0005E604 File Offset: 0x0005C804
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x0005E60C File Offset: 0x0005C80C
		internal bool EndCalled
		{
			get
			{
				return this.m_EndCalled;
			}
			set
			{
				this.m_EndCalled = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0005E615 File Offset: 0x0005C815
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x0005E61D File Offset: 0x0005C81D
		internal int ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
			set
			{
				this.m_ErrorCode = value;
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0005E628 File Offset: 0x0005C828
		protected void ProtectedInvokeCallback(object result, IntPtr userToken)
		{
			if (result == DBNull.Value)
			{
				throw new ArgumentNullException("result");
			}
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				if (this.m_Result == DBNull.Value)
				{
					this.m_Result = result;
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent != null)
				{
					try
					{
						manualResetEvent.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.Complete(userToken);
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0005E6B0 File Offset: 0x0005C8B0
		internal void InvokeCallback(object result)
		{
			this.ProtectedInvokeCallback(result, IntPtr.Zero);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0005E6BE File Offset: 0x0005C8BE
		internal void InvokeCallback()
		{
			this.ProtectedInvokeCallback(null, IntPtr.Zero);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0005E6CC File Offset: 0x0005C8CC
		protected virtual void Complete(IntPtr userToken)
		{
			bool flag = false;
			LazyAsyncResult.ThreadContext currentThreadContext = LazyAsyncResult.CurrentThreadContext;
			try
			{
				currentThreadContext.m_NestedIOCount++;
				if (this.m_AsyncCallback != null)
				{
					if (currentThreadContext.m_NestedIOCount >= 50)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.WorkerThreadComplete));
						flag = true;
					}
					else
					{
						this.m_AsyncCallback(this);
					}
				}
			}
			finally
			{
				currentThreadContext.m_NestedIOCount--;
				if (!flag)
				{
					this.Cleanup();
				}
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0005E750 File Offset: 0x0005C950
		private void WorkerThreadComplete(object state)
		{
			try
			{
				this.m_AsyncCallback(this);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0005E784 File Offset: 0x0005C984
		protected virtual void Cleanup()
		{
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0005E786 File Offset: 0x0005C986
		internal object InternalWaitForCompletion()
		{
			return this.WaitForCompletion(true);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0005E790 File Offset: 0x0005C990
		private object WaitForCompletion(bool snap)
		{
			ManualResetEvent manualResetEvent = null;
			bool flag = false;
			if (!(snap ? this.IsCompleted : this.InternalPeekCompleted))
			{
				manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent == null)
				{
					flag = this.LazilyCreateEvent(out manualResetEvent);
				}
			}
			if (manualResetEvent == null)
			{
				goto IL_75;
			}
			try
			{
				manualResetEvent.WaitOne(-1, false);
				goto IL_75;
			}
			catch (ObjectDisposedException)
			{
				goto IL_75;
			}
			finally
			{
				if (flag && !this.m_UserEvent)
				{
					ManualResetEvent manualResetEvent2 = (ManualResetEvent)this.m_Event;
					this.m_Event = null;
					if (!this.m_UserEvent)
					{
						manualResetEvent2.Close();
					}
				}
			}
			IL_6F:
			Thread.SpinWait(1);
			IL_75:
			if (this.m_Result != DBNull.Value)
			{
				return this.m_Result;
			}
			goto IL_6F;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0005E844 File Offset: 0x0005CA44
		internal void InternalCleanup()
		{
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				this.m_Result = null;
				this.Cleanup();
			}
		}

		// Token: 0x04001438 RID: 5176
		private const int c_HighBit = -2147483648;

		// Token: 0x04001439 RID: 5177
		private const int c_ForceAsyncCount = 50;

		// Token: 0x0400143A RID: 5178
		[ThreadStatic]
		private static LazyAsyncResult.ThreadContext t_ThreadContext;

		// Token: 0x0400143B RID: 5179
		private object m_AsyncObject;

		// Token: 0x0400143C RID: 5180
		private object m_AsyncState;

		// Token: 0x0400143D RID: 5181
		private AsyncCallback m_AsyncCallback;

		// Token: 0x0400143E RID: 5182
		private object m_Result;

		// Token: 0x0400143F RID: 5183
		private int m_ErrorCode;

		// Token: 0x04001440 RID: 5184
		private int m_IntCompleted;

		// Token: 0x04001441 RID: 5185
		private bool m_EndCalled;

		// Token: 0x04001442 RID: 5186
		private bool m_UserEvent;

		// Token: 0x04001443 RID: 5187
		private object m_Event;

		// Token: 0x0200074F RID: 1871
		private class ThreadContext
		{
			// Token: 0x040031E5 RID: 12773
			internal int m_NestedIOCount;
		}
	}
}
