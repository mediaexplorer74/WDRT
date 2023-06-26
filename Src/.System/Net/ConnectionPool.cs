using System;
using System.Collections;
using System.Threading;

namespace System.Net
{
	// Token: 0x020000CD RID: 205
	internal class ConnectionPool
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x000254CA File Offset: 0x000236CA
		private Mutex CreationMutex
		{
			get
			{
				return (Mutex)this.m_WaitHandles[2];
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x000254D9 File Offset: 0x000236D9
		private ManualResetEvent ErrorEvent
		{
			get
			{
				return (ManualResetEvent)this.m_WaitHandles[1];
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000254E8 File Offset: 0x000236E8
		private Semaphore Semaphore
		{
			get
			{
				return (Semaphore)this.m_WaitHandles[0];
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000254F8 File Offset: 0x000236F8
		internal ConnectionPool(ServicePoint servicePoint, int maxPoolSize, int minPoolSize, int idleTimeout, CreateConnectionDelegate createConnectionCallback)
		{
			this.m_State = ConnectionPool.State.Initializing;
			this.m_CreateConnectionCallback = createConnectionCallback;
			this.m_MaxPoolSize = maxPoolSize;
			this.m_MinPoolSize = minPoolSize;
			this.m_ServicePoint = servicePoint;
			this.Initialize();
			if (idleTimeout > 0)
			{
				this.m_CleanupQueue = TimerThread.GetOrCreateQueue((idleTimeout == 1) ? 1 : (idleTimeout / 2));
				this.m_CleanupQueue.CreateTimer(ConnectionPool.s_CleanupCallback, this);
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00025564 File Offset: 0x00023764
		private void Initialize()
		{
			this.m_StackOld = new InterlockedStack();
			this.m_StackNew = new InterlockedStack();
			this.m_QueuedRequests = new Queue();
			this.m_WaitHandles = new WaitHandle[3];
			this.m_WaitHandles[0] = new Semaphore(0, 1048576);
			this.m_WaitHandles[1] = new ManualResetEvent(false);
			this.m_WaitHandles[2] = new Mutex();
			this.m_ErrorTimer = null;
			this.m_ObjectList = new ArrayList();
			this.m_State = ConnectionPool.State.Running;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000255E8 File Offset: 0x000237E8
		private void QueueRequest(ConnectionPool.AsyncConnectionPoolRequest asyncRequest)
		{
			Queue queuedRequests = this.m_QueuedRequests;
			lock (queuedRequests)
			{
				this.m_QueuedRequests.Enqueue(asyncRequest);
				if (this.m_AsyncThread == null)
				{
					this.m_AsyncThread = new Thread(new ThreadStart(this.AsyncThread));
					this.m_AsyncThread.IsBackground = true;
					this.m_AsyncThread.Start();
				}
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00025664 File Offset: 0x00023864
		private void AsyncThread()
		{
			for (;;)
			{
				if (this.m_QueuedRequests.Count <= 0)
				{
					Thread.Sleep(500);
					Queue queuedRequests = this.m_QueuedRequests;
					lock (queuedRequests)
					{
						if (this.m_QueuedRequests.Count != 0)
						{
							continue;
						}
						this.m_AsyncThread = null;
					}
					break;
				}
				bool flag2 = true;
				ConnectionPool.AsyncConnectionPoolRequest asyncConnectionPoolRequest = null;
				Queue queuedRequests2 = this.m_QueuedRequests;
				lock (queuedRequests2)
				{
					asyncConnectionPoolRequest = (ConnectionPool.AsyncConnectionPoolRequest)this.m_QueuedRequests.Dequeue();
				}
				WaitHandle[] waitHandles = this.m_WaitHandles;
				PooledStream pooledStream = null;
				try
				{
					while (pooledStream == null && flag2)
					{
						int num = WaitHandle.WaitAny(waitHandles, asyncConnectionPoolRequest.CreationTimeout, false);
						pooledStream = this.Get(asyncConnectionPoolRequest.OwningObject, num, ref flag2, ref waitHandles);
					}
					pooledStream.Activate(asyncConnectionPoolRequest.OwningObject, asyncConnectionPoolRequest.AsyncCallback);
				}
				catch (Exception ex)
				{
					if (pooledStream != null)
					{
						this.PutConnection(pooledStream, asyncConnectionPoolRequest.OwningObject, asyncConnectionPoolRequest.CreationTimeout, false);
					}
					asyncConnectionPoolRequest.AsyncCallback(asyncConnectionPoolRequest.OwningObject, ex);
				}
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x000257A0 File Offset: 0x000239A0
		internal int Count
		{
			get
			{
				return this.m_TotalObjects;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x000257A8 File Offset: 0x000239A8
		internal ServicePoint ServicePoint
		{
			get
			{
				return this.m_ServicePoint;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x000257B0 File Offset: 0x000239B0
		internal int MaxPoolSize
		{
			get
			{
				return this.m_MaxPoolSize;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x000257B8 File Offset: 0x000239B8
		internal int MinPoolSize
		{
			get
			{
				return this.m_MinPoolSize;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x000257C0 File Offset: 0x000239C0
		private bool ErrorOccurred
		{
			get
			{
				return this.m_ErrorOccured;
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000257CC File Offset: 0x000239CC
		private static void CleanupCallbackWrapper(TimerThread.Timer timer, int timeNoticed, object context)
		{
			ConnectionPool connectionPool = (ConnectionPool)context;
			try
			{
				connectionPool.CleanupCallback();
			}
			finally
			{
				connectionPool.m_CleanupQueue.CreateTimer(ConnectionPool.s_CleanupCallback, context);
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0002580C File Offset: 0x00023A0C
		internal void ForceCleanup()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, "ConnectionPool::ForceCleanup");
			}
			while (this.Count > 0 && this.Semaphore.WaitOne(0, false))
			{
				PooledStream pooledStream = (PooledStream)this.m_StackNew.Pop();
				if (pooledStream == null)
				{
					pooledStream = (PooledStream)this.m_StackOld.Pop();
				}
				this.Destroy(pooledStream);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, "ConnectionPool::ForceCleanup");
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0002588C File Offset: 0x00023A8C
		private void CleanupCallback()
		{
			while (this.Count > this.MinPoolSize && this.Semaphore.WaitOne(0, false))
			{
				PooledStream pooledStream = (PooledStream)this.m_StackOld.Pop();
				if (pooledStream == null)
				{
					this.Semaphore.ReleaseSemaphore();
					break;
				}
				this.Destroy(pooledStream);
			}
			if (this.Semaphore.WaitOne(0, false))
			{
				for (;;)
				{
					PooledStream pooledStream2 = (PooledStream)this.m_StackNew.Pop();
					if (pooledStream2 == null)
					{
						break;
					}
					this.m_StackOld.Push(pooledStream2);
				}
				this.Semaphore.ReleaseSemaphore();
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00025920 File Offset: 0x00023B20
		private PooledStream Create(CreateConnectionDelegate createConnectionCallback)
		{
			PooledStream pooledStream = null;
			try
			{
				pooledStream = createConnectionCallback(this);
				if (pooledStream == null)
				{
					throw new InternalException();
				}
				if (!pooledStream.CanBePooled)
				{
					throw new InternalException();
				}
				pooledStream.PrePush(null);
				object syncRoot = this.m_ObjectList.SyncRoot;
				lock (syncRoot)
				{
					this.m_ObjectList.Add(pooledStream);
					this.m_TotalObjects = this.m_ObjectList.Count;
				}
			}
			catch (Exception ex)
			{
				pooledStream = null;
				this.m_ResError = ex;
				this.Abort();
			}
			return pooledStream;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000259C8 File Offset: 0x00023BC8
		private void Destroy(PooledStream pooledStream)
		{
			if (pooledStream != null)
			{
				try
				{
					object syncRoot = this.m_ObjectList.SyncRoot;
					lock (syncRoot)
					{
						this.m_ObjectList.Remove(pooledStream);
						this.m_TotalObjects = this.m_ObjectList.Count;
					}
				}
				finally
				{
					pooledStream.Dispose();
				}
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00025A3C File Offset: 0x00023C3C
		private static void CancelErrorCallbackWrapper(TimerThread.Timer timer, int timeNoticed, object context)
		{
			((ConnectionPool)context).CancelErrorCallback();
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00025A4C File Offset: 0x00023C4C
		private void CancelErrorCallback()
		{
			TimerThread.Timer errorTimer = this.m_ErrorTimer;
			if (errorTimer != null && errorTimer.Cancel())
			{
				this.m_ErrorOccured = false;
				this.ErrorEvent.Reset();
				this.m_ErrorTimer = null;
				this.m_ResError = null;
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00025A90 File Offset: 0x00023C90
		private PooledStream GetFromPool(object owningObject)
		{
			PooledStream pooledStream = (PooledStream)this.m_StackNew.Pop();
			if (pooledStream == null)
			{
				pooledStream = (PooledStream)this.m_StackOld.Pop();
			}
			if (pooledStream != null)
			{
				pooledStream.PostPop(owningObject);
			}
			return pooledStream;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00025AD0 File Offset: 0x00023CD0
		private PooledStream Get(object owningObject, int result, ref bool continueLoop, ref WaitHandle[] waitHandles)
		{
			PooledStream pooledStream = null;
			if (result != 1)
			{
				if (result != 2)
				{
					if (result == 258)
					{
						Interlocked.Decrement(ref this.m_WaitCount);
						continueLoop = false;
						throw new WebException(NetRes.GetWebStatusString("net_timeout", WebExceptionStatus.ConnectFailure), WebExceptionStatus.Timeout);
					}
				}
				else
				{
					try
					{
						continueLoop = true;
						pooledStream = this.UserCreateRequest();
						if (pooledStream != null)
						{
							pooledStream.PostPop(owningObject);
							Interlocked.Decrement(ref this.m_WaitCount);
							continueLoop = false;
							return pooledStream;
						}
						if (this.Count >= this.MaxPoolSize && this.MaxPoolSize != 0 && !this.ReclaimEmancipatedObjects())
						{
							waitHandles = new WaitHandle[2];
							waitHandles[0] = this.m_WaitHandles[0];
							waitHandles[1] = this.m_WaitHandles[1];
						}
						return pooledStream;
					}
					finally
					{
						this.CreationMutex.ReleaseMutex();
					}
				}
				Interlocked.Decrement(ref this.m_WaitCount);
				pooledStream = this.GetFromPool(owningObject);
				continueLoop = false;
				return pooledStream;
			}
			int num = Interlocked.Decrement(ref this.m_WaitCount);
			continueLoop = false;
			Exception resError = this.m_ResError;
			if (num == 0)
			{
				this.CancelErrorCallback();
			}
			throw resError;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00025BD4 File Offset: 0x00023DD4
		internal void Abort()
		{
			if (this.m_ResError == null)
			{
				this.m_ResError = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
			this.ErrorEvent.Set();
			this.m_ErrorOccured = true;
			this.m_ErrorTimer = ConnectionPool.s_CancelErrorQueue.CreateTimer(ConnectionPool.s_CancelErrorCallback, this);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00025C2C File Offset: 0x00023E2C
		internal PooledStream GetConnection(object owningObject, GeneralAsyncDelegate asyncCallback, int creationTimeout)
		{
			PooledStream pooledStream = null;
			bool flag = true;
			bool flag2 = asyncCallback != null;
			if (this.m_State != ConnectionPool.State.Running)
			{
				throw new InternalException();
			}
			Interlocked.Increment(ref this.m_WaitCount);
			WaitHandle[] waitHandles = this.m_WaitHandles;
			if (flag2)
			{
				int num = WaitHandle.WaitAny(waitHandles, 0, false);
				if (num != 258)
				{
					pooledStream = this.Get(owningObject, num, ref flag, ref waitHandles);
				}
				if (pooledStream == null)
				{
					ConnectionPool.AsyncConnectionPoolRequest asyncConnectionPoolRequest = new ConnectionPool.AsyncConnectionPoolRequest(this, owningObject, asyncCallback, creationTimeout);
					this.QueueRequest(asyncConnectionPoolRequest);
				}
			}
			else
			{
				while (pooledStream == null && flag)
				{
					int num = WaitHandle.WaitAny(waitHandles, creationTimeout, false);
					pooledStream = this.Get(owningObject, num, ref flag, ref waitHandles);
				}
			}
			if (pooledStream != null)
			{
				if (!pooledStream.IsInitalizing)
				{
					asyncCallback = null;
				}
				try
				{
					if (!pooledStream.Activate(owningObject, asyncCallback))
					{
						pooledStream = null;
					}
					return pooledStream;
				}
				catch
				{
					this.PutConnection(pooledStream, owningObject, creationTimeout, false);
					throw;
				}
			}
			if (!flag2)
			{
				throw new InternalException();
			}
			return pooledStream;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00025D08 File Offset: 0x00023F08
		internal void PutConnection(PooledStream pooledStream, object owningObject, int creationTimeout)
		{
			this.PutConnection(pooledStream, owningObject, creationTimeout, true);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00025D14 File Offset: 0x00023F14
		internal void PutConnection(PooledStream pooledStream, object owningObject, int creationTimeout, bool canReuse)
		{
			if (pooledStream == null)
			{
				throw new ArgumentNullException("pooledStream");
			}
			pooledStream.PrePush(owningObject);
			if (this.m_State != ConnectionPool.State.ShuttingDown)
			{
				pooledStream.Deactivate();
				if (this.m_WaitCount == 0)
				{
					this.CancelErrorCallback();
				}
				if (canReuse && pooledStream.CanBePooled)
				{
					this.PutNew(pooledStream);
					return;
				}
				try
				{
					this.Destroy(pooledStream);
					return;
				}
				finally
				{
					if (this.m_WaitCount > 0)
					{
						if (!this.CreationMutex.WaitOne(creationTimeout, false))
						{
							this.Abort();
						}
						else
						{
							try
							{
								pooledStream = this.UserCreateRequest();
								if (pooledStream != null)
								{
									this.PutNew(pooledStream);
								}
							}
							finally
							{
								this.CreationMutex.ReleaseMutex();
							}
						}
					}
				}
			}
			this.Destroy(pooledStream);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00025DD8 File Offset: 0x00023FD8
		private void PutNew(PooledStream pooledStream)
		{
			this.m_StackNew.Push(pooledStream);
			this.Semaphore.ReleaseSemaphore();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00025DF4 File Offset: 0x00023FF4
		private bool ReclaimEmancipatedObjects()
		{
			bool flag = false;
			object syncRoot = this.m_ObjectList.SyncRoot;
			lock (syncRoot)
			{
				object[] array = this.m_ObjectList.ToArray();
				if (array != null)
				{
					foreach (PooledStream pooledStream in array)
					{
						if (pooledStream != null)
						{
							bool flag3 = false;
							try
							{
								Monitor.TryEnter(pooledStream, ref flag3);
								if (flag3 && pooledStream.IsEmancipated)
								{
									this.PutConnection(pooledStream, null, -1);
									flag = true;
								}
							}
							finally
							{
								if (flag3)
								{
									Monitor.Exit(pooledStream);
								}
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00025EA8 File Offset: 0x000240A8
		private PooledStream UserCreateRequest()
		{
			PooledStream pooledStream = null;
			if (!this.ErrorOccurred && (this.Count < this.MaxPoolSize || this.MaxPoolSize == 0) && ((this.Count & 1) == 1 || !this.ReclaimEmancipatedObjects()))
			{
				pooledStream = this.Create(this.m_CreateConnectionCallback);
			}
			return pooledStream;
		}

		// Token: 0x04000C91 RID: 3217
		private static TimerThread.Callback s_CleanupCallback = new TimerThread.Callback(ConnectionPool.CleanupCallbackWrapper);

		// Token: 0x04000C92 RID: 3218
		private static TimerThread.Callback s_CancelErrorCallback = new TimerThread.Callback(ConnectionPool.CancelErrorCallbackWrapper);

		// Token: 0x04000C93 RID: 3219
		private static TimerThread.Queue s_CancelErrorQueue = TimerThread.GetOrCreateQueue(5000);

		// Token: 0x04000C94 RID: 3220
		private const int MaxQueueSize = 1048576;

		// Token: 0x04000C95 RID: 3221
		private const int SemaphoreHandleIndex = 0;

		// Token: 0x04000C96 RID: 3222
		private const int ErrorHandleIndex = 1;

		// Token: 0x04000C97 RID: 3223
		private const int CreationHandleIndex = 2;

		// Token: 0x04000C98 RID: 3224
		private const int WaitTimeout = 258;

		// Token: 0x04000C99 RID: 3225
		private const int WaitAbandoned = 128;

		// Token: 0x04000C9A RID: 3226
		private const int ErrorWait = 5000;

		// Token: 0x04000C9B RID: 3227
		private readonly TimerThread.Queue m_CleanupQueue;

		// Token: 0x04000C9C RID: 3228
		private ConnectionPool.State m_State;

		// Token: 0x04000C9D RID: 3229
		private InterlockedStack m_StackOld;

		// Token: 0x04000C9E RID: 3230
		private InterlockedStack m_StackNew;

		// Token: 0x04000C9F RID: 3231
		private int m_WaitCount;

		// Token: 0x04000CA0 RID: 3232
		private WaitHandle[] m_WaitHandles;

		// Token: 0x04000CA1 RID: 3233
		private Exception m_ResError;

		// Token: 0x04000CA2 RID: 3234
		private volatile bool m_ErrorOccured;

		// Token: 0x04000CA3 RID: 3235
		private TimerThread.Timer m_ErrorTimer;

		// Token: 0x04000CA4 RID: 3236
		private ArrayList m_ObjectList;

		// Token: 0x04000CA5 RID: 3237
		private int m_TotalObjects;

		// Token: 0x04000CA6 RID: 3238
		private Queue m_QueuedRequests;

		// Token: 0x04000CA7 RID: 3239
		private Thread m_AsyncThread;

		// Token: 0x04000CA8 RID: 3240
		private int m_MaxPoolSize;

		// Token: 0x04000CA9 RID: 3241
		private int m_MinPoolSize;

		// Token: 0x04000CAA RID: 3242
		private ServicePoint m_ServicePoint;

		// Token: 0x04000CAB RID: 3243
		private CreateConnectionDelegate m_CreateConnectionCallback;

		// Token: 0x020006EF RID: 1775
		private enum State
		{
			// Token: 0x0400306E RID: 12398
			Initializing,
			// Token: 0x0400306F RID: 12399
			Running,
			// Token: 0x04003070 RID: 12400
			ShuttingDown
		}

		// Token: 0x020006F0 RID: 1776
		private class AsyncConnectionPoolRequest
		{
			// Token: 0x0600404F RID: 16463 RVA: 0x0010DA86 File Offset: 0x0010BC86
			public AsyncConnectionPoolRequest(ConnectionPool pool, object owningObject, GeneralAsyncDelegate asyncCallback, int creationTimeout)
			{
				this.Pool = pool;
				this.OwningObject = owningObject;
				this.AsyncCallback = asyncCallback;
				this.CreationTimeout = creationTimeout;
			}

			// Token: 0x04003071 RID: 12401
			public object OwningObject;

			// Token: 0x04003072 RID: 12402
			public GeneralAsyncDelegate AsyncCallback;

			// Token: 0x04003073 RID: 12403
			public bool Completed;

			// Token: 0x04003074 RID: 12404
			public ConnectionPool Pool;

			// Token: 0x04003075 RID: 12405
			public int CreationTimeout;
		}
	}
}
