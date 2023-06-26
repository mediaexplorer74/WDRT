using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Signals to a <see cref="T:System.Threading.CancellationToken" /> that it should be canceled.</summary>
	// Token: 0x02000543 RID: 1347
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class CancellationTokenSource : IDisposable
	{
		// Token: 0x06003F5E RID: 16222 RVA: 0x000ED038 File Offset: 0x000EB238
		private static void LinkedTokenCancelDelegate(object source)
		{
			CancellationTokenSource cancellationTokenSource = source as CancellationTokenSource;
			cancellationTokenSource.Cancel();
		}

		/// <summary>Gets whether cancellation has been requested for this <see cref="T:System.Threading.CancellationTokenSource" />.</summary>
		/// <returns>
		///   <see langword="true" /> if cancellation has been requested for this <see cref="T:System.Threading.CancellationTokenSource" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06003F5F RID: 16223 RVA: 0x000ED052 File Offset: 0x000EB252
		[__DynamicallyInvokable]
		public bool IsCancellationRequested
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_state >= 2;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x000ED062 File Offset: 0x000EB262
		internal bool IsCancellationCompleted
		{
			get
			{
				return this.m_state == 3;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x000ED06F File Offset: 0x000EB26F
		internal bool IsDisposed
		{
			get
			{
				return this.m_disposed;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x000ED082 File Offset: 0x000EB282
		// (set) Token: 0x06003F62 RID: 16226 RVA: 0x000ED077 File Offset: 0x000EB277
		internal int ThreadIDExecutingCallbacks
		{
			get
			{
				return this.m_threadIDExecutingCallbacks;
			}
			set
			{
				this.m_threadIDExecutingCallbacks = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.CancellationToken" /> associated with this <see cref="T:System.Threading.CancellationTokenSource" />.</summary>
		/// <returns>The <see cref="T:System.Threading.CancellationToken" /> associated with this <see cref="T:System.Threading.CancellationTokenSource" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The token source has been disposed.</exception>
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x000ED08C File Offset: 0x000EB28C
		[__DynamicallyInvokable]
		public CancellationToken Token
		{
			[__DynamicallyInvokable]
			get
			{
				this.ThrowIfDisposed();
				return new CancellationToken(this);
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x000ED09A File Offset: 0x000EB29A
		internal bool CanBeCanceled
		{
			get
			{
				return this.m_state != 0;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000ED0A8 File Offset: 0x000EB2A8
		internal WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.m_kernelEvent != null)
				{
					return this.m_kernelEvent;
				}
				ManualResetEvent manualResetEvent = new ManualResetEvent(false);
				if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_kernelEvent, manualResetEvent, null) != null)
				{
					((IDisposable)manualResetEvent).Dispose();
				}
				if (this.IsCancellationRequested)
				{
					this.m_kernelEvent.Set();
				}
				return this.m_kernelEvent;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06003F67 RID: 16231 RVA: 0x000ED108 File Offset: 0x000EB308
		internal CancellationCallbackInfo ExecutingCallback
		{
			get
			{
				return this.m_executingCallback;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class.</summary>
		// Token: 0x06003F68 RID: 16232 RVA: 0x000ED112 File Offset: 0x000EB312
		[__DynamicallyInvokable]
		public CancellationTokenSource()
		{
			this.m_state = 1;
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x000ED12C File Offset: 0x000EB32C
		private CancellationTokenSource(bool set)
		{
			this.m_state = (set ? 3 : 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class that will be canceled after the specified time span.</summary>
		/// <param name="delay">The time interval to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="delay" />.<see cref="P:System.TimeSpan.TotalMilliseconds" /> is less than -1 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003F6A RID: 16234 RVA: 0x000ED14C File Offset: 0x000EB34C
		[__DynamicallyInvokable]
		public CancellationTokenSource(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.InitializeWithTimer((int)num);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class that will be canceled after the specified delay in milliseconds.</summary>
		/// <param name="millisecondsDelay">The time interval in milliseconds to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsDelay" /> is less than -1.</exception>
		// Token: 0x06003F6B RID: 16235 RVA: 0x000ED192 File Offset: 0x000EB392
		[__DynamicallyInvokable]
		public CancellationTokenSource(int millisecondsDelay)
		{
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			this.InitializeWithTimer(millisecondsDelay);
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x000ED1B9 File Offset: 0x000EB3B9
		private void InitializeWithTimer(int millisecondsDelay)
		{
			this.m_state = 1;
			this.m_timer = new Timer(CancellationTokenSource.s_timerCallback, this, millisecondsDelay, -1);
		}

		/// <summary>Communicates a request for cancellation.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">An aggregate exception containing all the exceptions thrown by the registered callbacks on the associated <see cref="T:System.Threading.CancellationToken" />.</exception>
		// Token: 0x06003F6D RID: 16237 RVA: 0x000ED1D9 File Offset: 0x000EB3D9
		[__DynamicallyInvokable]
		public void Cancel()
		{
			this.Cancel(false);
		}

		/// <summary>Communicates a request for cancellation, and specifies whether remaining callbacks and cancelable operations should be processed if an exception occurs.</summary>
		/// <param name="throwOnFirstException">
		///   <see langword="true" /> if exceptions should immediately propagate; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">An aggregate exception containing all the exceptions thrown by the registered callbacks on the associated <see cref="T:System.Threading.CancellationToken" />.</exception>
		// Token: 0x06003F6E RID: 16238 RVA: 0x000ED1E2 File Offset: 0x000EB3E2
		[__DynamicallyInvokable]
		public void Cancel(bool throwOnFirstException)
		{
			this.ThrowIfDisposed();
			this.NotifyCancellation(throwOnFirstException);
		}

		/// <summary>Schedules a cancel operation on this <see cref="T:System.Threading.CancellationTokenSource" /> after the specified time span.</summary>
		/// <param name="delay">The time span to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception thrown when this <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when <paramref name="delay" /> is less than -1 or greater than Int32.MaxValue.</exception>
		// Token: 0x06003F6F RID: 16239 RVA: 0x000ED1F4 File Offset: 0x000EB3F4
		[__DynamicallyInvokable]
		public void CancelAfter(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.CancelAfter((int)num);
		}

		/// <summary>Schedules a cancel operation on this <see cref="T:System.Threading.CancellationTokenSource" /> after the specified number of milliseconds.</summary>
		/// <param name="millisecondsDelay">The time span to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception thrown when this <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception thrown when <paramref name="millisecondsDelay" /> is less than -1.</exception>
		// Token: 0x06003F70 RID: 16240 RVA: 0x000ED22C File Offset: 0x000EB42C
		[__DynamicallyInvokable]
		public void CancelAfter(int millisecondsDelay)
		{
			this.ThrowIfDisposed();
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			if (this.IsCancellationRequested)
			{
				return;
			}
			if (this.m_timer == null)
			{
				Timer timer = new Timer(CancellationTokenSource.s_timerCallback, this, -1, -1);
				if (Interlocked.CompareExchange<Timer>(ref this.m_timer, timer, null) != null)
				{
					timer.Dispose();
				}
			}
			try
			{
				this.m_timer.Change(millisecondsDelay, -1);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x000ED2AC File Offset: 0x000EB4AC
		private static void TimerCallbackLogic(object obj)
		{
			CancellationTokenSource cancellationTokenSource = (CancellationTokenSource)obj;
			if (!cancellationTokenSource.IsDisposed)
			{
				try
				{
					cancellationTokenSource.Cancel();
				}
				catch (ObjectDisposedException)
				{
					if (!cancellationTokenSource.IsDisposed)
					{
						throw;
					}
				}
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class.</summary>
		// Token: 0x06003F72 RID: 16242 RVA: 0x000ED2F0 File Offset: 0x000EB4F0
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.CancellationTokenSource" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003F73 RID: 16243 RVA: 0x000ED300 File Offset: 0x000EB500
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_disposed)
				{
					return;
				}
				if (this.m_timer != null)
				{
					this.m_timer.Dispose();
				}
				CancellationTokenRegistration[] linkingRegistrations = this.m_linkingRegistrations;
				if (linkingRegistrations != null)
				{
					this.m_linkingRegistrations = null;
					for (int i = 0; i < linkingRegistrations.Length; i++)
					{
						linkingRegistrations[i].Dispose();
					}
				}
				this.m_registeredCallbacksLists = null;
				if (this.m_kernelEvent != null)
				{
					this.m_kernelEvent.Close();
					this.m_kernelEvent = null;
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x000ED38B File Offset: 0x000EB58B
		internal void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				CancellationTokenSource.ThrowObjectDisposedException();
			}
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x000ED39A File Offset: 0x000EB59A
		private static void ThrowObjectDisposedException()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("CancellationTokenSource_Disposed"));
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x000ED3AC File Offset: 0x000EB5AC
		internal static CancellationTokenSource InternalGetStaticSource(bool set)
		{
			if (!set)
			{
				return CancellationTokenSource._staticSource_NotCancelable;
			}
			return CancellationTokenSource._staticSource_Set;
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x000ED3BC File Offset: 0x000EB5BC
		internal CancellationTokenRegistration InternalRegister(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext executionContext)
		{
			if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
			{
				this.ThrowIfDisposed();
			}
			if (!this.IsCancellationRequested)
			{
				if (this.m_disposed && !AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
				{
					return default(CancellationTokenRegistration);
				}
				int num = Thread.CurrentThread.ManagedThreadId % CancellationTokenSource.s_nLists;
				CancellationCallbackInfo cancellationCallbackInfo = new CancellationCallbackInfo(callback, stateForCallback, targetSyncContext, executionContext, this);
				SparselyPopulatedArray<CancellationCallbackInfo>[] array = this.m_registeredCallbacksLists;
				if (array == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo>[] array2 = new SparselyPopulatedArray<CancellationCallbackInfo>[CancellationTokenSource.s_nLists];
					array = Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>[]>(ref this.m_registeredCallbacksLists, array2, null);
					if (array == null)
					{
						array = array2;
					}
				}
				SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref array[num]);
				if (sparselyPopulatedArray == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray2 = new SparselyPopulatedArray<CancellationCallbackInfo>(4);
					Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>>(ref array[num], sparselyPopulatedArray2, null);
					sparselyPopulatedArray = array[num];
				}
				SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> sparselyPopulatedArrayAddInfo = sparselyPopulatedArray.Add(cancellationCallbackInfo);
				CancellationTokenRegistration cancellationTokenRegistration = new CancellationTokenRegistration(cancellationCallbackInfo, sparselyPopulatedArrayAddInfo);
				if (!this.IsCancellationRequested)
				{
					return cancellationTokenRegistration;
				}
				if (!cancellationTokenRegistration.TryDeregister())
				{
					return cancellationTokenRegistration;
				}
			}
			callback(stateForCallback);
			return default(CancellationTokenRegistration);
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x000ED4B0 File Offset: 0x000EB6B0
		private void NotifyCancellation(bool throwOnFirstException)
		{
			if (this.IsCancellationRequested)
			{
				return;
			}
			if (Interlocked.CompareExchange(ref this.m_state, 2, 1) == 1)
			{
				Timer timer = this.m_timer;
				if (timer != null)
				{
					timer.Dispose();
				}
				this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
				if (this.m_kernelEvent != null)
				{
					this.m_kernelEvent.Set();
				}
				this.ExecuteCallbackHandlers(throwOnFirstException);
			}
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x000ED518 File Offset: 0x000EB718
		private void ExecuteCallbackHandlers(bool throwOnFirstException)
		{
			List<Exception> list = null;
			SparselyPopulatedArray<CancellationCallbackInfo>[] registeredCallbacksLists = this.m_registeredCallbacksLists;
			if (registeredCallbacksLists == null)
			{
				Interlocked.Exchange(ref this.m_state, 3);
				return;
			}
			try
			{
				for (int i = 0; i < registeredCallbacksLists.Length; i++)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref registeredCallbacksLists[i]);
					if (sparselyPopulatedArray != null)
					{
						for (SparselyPopulatedArrayFragment<CancellationCallbackInfo> sparselyPopulatedArrayFragment = sparselyPopulatedArray.Tail; sparselyPopulatedArrayFragment != null; sparselyPopulatedArrayFragment = sparselyPopulatedArrayFragment.Prev)
						{
							for (int j = sparselyPopulatedArrayFragment.Length - 1; j >= 0; j--)
							{
								this.m_executingCallback = sparselyPopulatedArrayFragment[j];
								if (this.m_executingCallback != null)
								{
									CancellationCallbackCoreWorkArguments cancellationCallbackCoreWorkArguments = new CancellationCallbackCoreWorkArguments(sparselyPopulatedArrayFragment, j);
									try
									{
										if (this.m_executingCallback.TargetSyncContext != null)
										{
											this.m_executingCallback.TargetSyncContext.Send(new SendOrPostCallback(this.CancellationCallbackCoreWork_OnSyncContext), cancellationCallbackCoreWorkArguments);
											this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
										}
										else
										{
											this.CancellationCallbackCoreWork(cancellationCallbackCoreWorkArguments);
										}
									}
									catch (Exception ex)
									{
										if (throwOnFirstException)
										{
											throw;
										}
										if (list == null)
										{
											list = new List<Exception>();
										}
										list.Add(ex);
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				this.m_state = 3;
				this.m_executingCallback = null;
				Thread.MemoryBarrier();
			}
			if (list != null)
			{
				throw new AggregateException(list);
			}
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x000ED674 File Offset: 0x000EB874
		private void CancellationCallbackCoreWork_OnSyncContext(object obj)
		{
			this.CancellationCallbackCoreWork((CancellationCallbackCoreWorkArguments)obj);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x000ED684 File Offset: 0x000EB884
		private void CancellationCallbackCoreWork(CancellationCallbackCoreWorkArguments args)
		{
			CancellationCallbackInfo cancellationCallbackInfo = args.m_currArrayFragment.SafeAtomicRemove(args.m_currArrayIndex, this.m_executingCallback);
			if (cancellationCallbackInfo == this.m_executingCallback)
			{
				if (cancellationCallbackInfo.TargetExecutionContext != null)
				{
					cancellationCallbackInfo.CancellationTokenSource.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
				}
				cancellationCallbackInfo.ExecuteCallback();
			}
		}

		/// <summary>Creates a <see cref="T:System.Threading.CancellationTokenSource" /> that will be in the canceled state when any of the source tokens are in the canceled state.</summary>
		/// <param name="token1">The first cancellation token to observe.</param>
		/// <param name="token2">The second cancellation token to observe.</param>
		/// <returns>A <see cref="T:System.Threading.CancellationTokenSource" /> that is linked to the source tokens.</returns>
		/// <exception cref="T:System.ObjectDisposedException">A <see cref="T:System.Threading.CancellationTokenSource" /> associated with one of the source tokens has been disposed.</exception>
		// Token: 0x06003F7C RID: 16252 RVA: 0x000ED6DC File Offset: 0x000EB8DC
		[__DynamicallyInvokable]
		public static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token1, CancellationToken token2)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			bool canBeCanceled = token2.CanBeCanceled;
			if (token1.CanBeCanceled)
			{
				cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[canBeCanceled ? 2 : 1];
				cancellationTokenSource.m_linkingRegistrations[0] = token1.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource);
			}
			if (canBeCanceled)
			{
				int num = 1;
				if (cancellationTokenSource.m_linkingRegistrations == null)
				{
					cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[1];
					num = 0;
				}
				cancellationTokenSource.m_linkingRegistrations[num] = token2.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource);
			}
			return cancellationTokenSource;
		}

		/// <summary>Creates a <see cref="T:System.Threading.CancellationTokenSource" /> that will be in the canceled state when any of the source tokens in the specified array are in the canceled state.</summary>
		/// <param name="tokens">An array that contains the cancellation token instances to observe.</param>
		/// <returns>A <see cref="T:System.Threading.CancellationTokenSource" /> that is linked to the source tokens.</returns>
		/// <exception cref="T:System.ObjectDisposedException">A <see cref="T:System.Threading.CancellationTokenSource" /> associated with one of the source tokens has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tokens" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tokens" /> is empty.</exception>
		// Token: 0x06003F7D RID: 16253 RVA: 0x000ED760 File Offset: 0x000EB960
		[__DynamicallyInvokable]
		public static CancellationTokenSource CreateLinkedTokenSource(params CancellationToken[] tokens)
		{
			if (tokens == null)
			{
				throw new ArgumentNullException("tokens");
			}
			if (tokens.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("CancellationToken_CreateLinkedToken_TokensIsEmpty"));
			}
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[tokens.Length];
			for (int i = 0; i < tokens.Length; i++)
			{
				if (tokens[i].CanBeCanceled)
				{
					cancellationTokenSource.m_linkingRegistrations[i] = tokens[i].InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource);
				}
			}
			return cancellationTokenSource;
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x000ED7E0 File Offset: 0x000EB9E0
		internal void WaitForCallbackToComplete(CancellationCallbackInfo callbackInfo)
		{
			SpinWait spinWait = default(SpinWait);
			while (this.ExecutingCallback == callbackInfo)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x04001AA3 RID: 6819
		private static readonly CancellationTokenSource _staticSource_Set = new CancellationTokenSource(true);

		// Token: 0x04001AA4 RID: 6820
		private static readonly CancellationTokenSource _staticSource_NotCancelable = new CancellationTokenSource(false);

		// Token: 0x04001AA5 RID: 6821
		private static readonly int s_nLists = ((PlatformHelper.ProcessorCount > 24) ? 24 : PlatformHelper.ProcessorCount);

		// Token: 0x04001AA6 RID: 6822
		private volatile ManualResetEvent m_kernelEvent;

		// Token: 0x04001AA7 RID: 6823
		private volatile SparselyPopulatedArray<CancellationCallbackInfo>[] m_registeredCallbacksLists;

		// Token: 0x04001AA8 RID: 6824
		private const int CANNOT_BE_CANCELED = 0;

		// Token: 0x04001AA9 RID: 6825
		private const int NOT_CANCELED = 1;

		// Token: 0x04001AAA RID: 6826
		private const int NOTIFYING = 2;

		// Token: 0x04001AAB RID: 6827
		private const int NOTIFYINGCOMPLETE = 3;

		// Token: 0x04001AAC RID: 6828
		private volatile int m_state;

		// Token: 0x04001AAD RID: 6829
		private volatile int m_threadIDExecutingCallbacks = -1;

		// Token: 0x04001AAE RID: 6830
		private bool m_disposed;

		// Token: 0x04001AAF RID: 6831
		private CancellationTokenRegistration[] m_linkingRegistrations;

		// Token: 0x04001AB0 RID: 6832
		private static readonly Action<object> s_LinkedTokenCancelDelegate = new Action<object>(CancellationTokenSource.LinkedTokenCancelDelegate);

		// Token: 0x04001AB1 RID: 6833
		private volatile CancellationCallbackInfo m_executingCallback;

		// Token: 0x04001AB2 RID: 6834
		private volatile Timer m_timer;

		// Token: 0x04001AB3 RID: 6835
		private static readonly TimerCallback s_timerCallback = new TimerCallback(CancellationTokenSource.TimerCallbackLogic);
	}
}
