using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides a slimmed down version of <see cref="T:System.Threading.ManualResetEvent" />.</summary>
	// Token: 0x02000541 RID: 1345
	[ComVisible(false)]
	[DebuggerDisplay("Set = {IsSet}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ManualResetEventSlim : IDisposable
	{
		/// <summary>Gets the underlying <see cref="T:System.Threading.WaitHandle" /> object for this <see cref="T:System.Threading.ManualResetEventSlim" />.</summary>
		/// <returns>The underlying <see cref="T:System.Threading.WaitHandle" /> event object fore this <see cref="T:System.Threading.ManualResetEventSlim" />.</returns>
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06003F38 RID: 16184 RVA: 0x000EC7C4 File Offset: 0x000EA9C4
		[__DynamicallyInvokable]
		public WaitHandle WaitHandle
		{
			[__DynamicallyInvokable]
			get
			{
				this.ThrowIfDisposed();
				if (this.m_eventObj == null)
				{
					this.LazyInitializeEvent();
				}
				return this.m_eventObj;
			}
		}

		/// <summary>Gets whether the event is set.</summary>
		/// <returns>true if the event has is set; otherwise, false.</returns>
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x000EC7E5 File Offset: 0x000EA9E5
		// (set) Token: 0x06003F3A RID: 16186 RVA: 0x000EC7FC File Offset: 0x000EA9FC
		[__DynamicallyInvokable]
		public bool IsSet
		{
			[__DynamicallyInvokable]
			get
			{
				return ManualResetEventSlim.ExtractStatePortion(this.m_combinedState, int.MinValue) != 0;
			}
			private set
			{
				this.UpdateStateAtomically((value ? 1 : 0) << 31, int.MinValue);
			}
		}

		/// <summary>Gets the number of spin waits that will occur before falling back to a kernel-based wait operation.</summary>
		/// <returns>Returns the number of spin waits that will occur before falling back to a kernel-based wait operation.</returns>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x000EC813 File Offset: 0x000EAA13
		// (set) Token: 0x06003F3C RID: 16188 RVA: 0x000EC829 File Offset: 0x000EAA29
		[__DynamicallyInvokable]
		public int SpinCount
		{
			[__DynamicallyInvokable]
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 1073217536, 19);
			}
			private set
			{
				this.m_combinedState = (this.m_combinedState & -1073217537) | (value << 19);
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x000EC846 File Offset: 0x000EAA46
		// (set) Token: 0x06003F3E RID: 16190 RVA: 0x000EC85B File Offset: 0x000EAA5B
		private int Waiters
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 524287, 0);
			}
			set
			{
				if (value >= 524287)
				{
					throw new InvalidOperationException(string.Format(Environment.GetResourceString("ManualResetEventSlim_ctor_TooManyWaiters"), 524287));
				}
				this.UpdateStateAtomically(value, 524287);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with an initial state of nonsignaled.</summary>
		// Token: 0x06003F3F RID: 16191 RVA: 0x000EC890 File Offset: 0x000EAA90
		[__DynamicallyInvokable]
		public ManualResetEventSlim()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with a Boolean value indicating whether to set the intial state to signaled.</summary>
		/// <param name="initialState">true to set the initial state signaled; false to set the initial state to nonsignaled.</param>
		// Token: 0x06003F40 RID: 16192 RVA: 0x000EC899 File Offset: 0x000EAA99
		[__DynamicallyInvokable]
		public ManualResetEventSlim(bool initialState)
		{
			this.Initialize(initialState, 10);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with a Boolean value indicating whether to set the intial state to signaled and a specified spin count.</summary>
		/// <param name="initialState">true to set the initial state to signaled; false to set the initial state to nonsignaled.</param>
		/// <param name="spinCount">The number of spin waits that will occur before falling back to a kernel-based wait operation.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="spinCount" /> is less than 0 or greater than the maximum allowed value.</exception>
		// Token: 0x06003F41 RID: 16193 RVA: 0x000EC8AC File Offset: 0x000EAAAC
		[__DynamicallyInvokable]
		public ManualResetEventSlim(bool initialState, int spinCount)
		{
			if (spinCount < 0)
			{
				throw new ArgumentOutOfRangeException("spinCount");
			}
			if (spinCount > 2047)
			{
				throw new ArgumentOutOfRangeException("spinCount", string.Format(Environment.GetResourceString("ManualResetEventSlim_ctor_SpinCountOutOfRange"), 2047));
			}
			this.Initialize(initialState, spinCount);
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x000EC902 File Offset: 0x000EAB02
		private void Initialize(bool initialState, int spinCount)
		{
			this.m_combinedState = (initialState ? int.MinValue : 0);
			this.SpinCount = (PlatformHelper.IsSingleProcessor ? 1 : spinCount);
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x000EC928 File Offset: 0x000EAB28
		private void EnsureLockObjectCreated()
		{
			if (this.m_lock != null)
			{
				return;
			}
			object obj = new object();
			Interlocked.CompareExchange(ref this.m_lock, obj, null);
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x000EC954 File Offset: 0x000EAB54
		private bool LazyInitializeEvent()
		{
			bool isSet = this.IsSet;
			ManualResetEvent manualResetEvent = new ManualResetEvent(isSet);
			if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_eventObj, manualResetEvent, null) != null)
			{
				manualResetEvent.Close();
				return false;
			}
			bool isSet2 = this.IsSet;
			if (isSet2 != isSet)
			{
				ManualResetEvent manualResetEvent2 = manualResetEvent;
				lock (manualResetEvent2)
				{
					if (this.m_eventObj == manualResetEvent)
					{
						manualResetEvent.Set();
					}
				}
			}
			return true;
		}

		/// <summary>Sets the state of the event to signaled, which allows one or more threads waiting on the event to proceed.</summary>
		// Token: 0x06003F45 RID: 16197 RVA: 0x000EC9D0 File Offset: 0x000EABD0
		[__DynamicallyInvokable]
		public void Set()
		{
			this.Set(false);
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x000EC9DC File Offset: 0x000EABDC
		private void Set(bool duringCancellation)
		{
			this.IsSet = true;
			if (this.Waiters > 0)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					Monitor.PulseAll(this.m_lock);
				}
			}
			ManualResetEvent eventObj = this.m_eventObj;
			if (eventObj != null && !duringCancellation)
			{
				ManualResetEvent manualResetEvent = eventObj;
				lock (manualResetEvent)
				{
					if (this.m_eventObj != null)
					{
						this.m_eventObj.Set();
					}
				}
			}
		}

		/// <summary>Sets the state of the event to nonsignaled, which causes threads to block.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06003F47 RID: 16199 RVA: 0x000ECA84 File Offset: 0x000EAC84
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.ThrowIfDisposed();
			if (this.m_eventObj != null)
			{
				this.m_eventObj.Reset();
			}
			this.IsSet = false;
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set.</summary>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06003F48 RID: 16200 RVA: 0x000ECAAC File Offset: 0x000EACAC
		[__DynamicallyInvokable]
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> receives a signal, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x06003F49 RID: 16201 RVA: 0x000ECACA File Offset: 0x000EACCA
		[__DynamicallyInvokable]
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the time interval.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// The number of milliseconds in <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06003F4A RID: 16202 RVA: 0x000ECAD8 File Offset: 0x000EACD8
		[__DynamicallyInvokable]
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// The number of milliseconds in <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x06003F4B RID: 16203 RVA: 0x000ECB18 File Offset: 0x000EAD18
		[__DynamicallyInvokable]
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a 32-bit signed integer to measure the time interval.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06003F4C RID: 16204 RVA: 0x000ECB50 File Offset: 0x000EAD50
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a 32-bit signed integer to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x06003F4D RID: 16205 RVA: 0x000ECB70 File Offset: 0x000EAD70
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!this.IsSet)
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				uint num = 0U;
				bool flag = false;
				int num2 = millisecondsTimeout;
				if (millisecondsTimeout != -1)
				{
					num = TimeoutHelper.GetTime();
					flag = true;
				}
				int num3 = 10;
				int num4 = 5;
				int num5 = 20;
				int spinCount = this.SpinCount;
				for (int i = 0; i < spinCount; i++)
				{
					if (this.IsSet)
					{
						return true;
					}
					if (i < num3)
					{
						if (i == num3 / 2)
						{
							Thread.Yield();
						}
						else
						{
							Thread.SpinWait(4 << i);
						}
					}
					else if (i % num5 == 0)
					{
						Thread.Sleep(1);
					}
					else if (i % num4 == 0)
					{
						Thread.Sleep(0);
					}
					else
					{
						Thread.Yield();
					}
					if (i >= 100 && i % 10 == 0)
					{
						cancellationToken.ThrowIfCancellationRequested();
					}
				}
				this.EnsureLockObjectCreated();
				using (cancellationToken.InternalRegisterWithoutEC(ManualResetEventSlim.s_cancellationTokenCallback, this))
				{
					object @lock = this.m_lock;
					lock (@lock)
					{
						while (!this.IsSet)
						{
							cancellationToken.ThrowIfCancellationRequested();
							if (flag)
							{
								num2 = TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout);
								if (num2 <= 0)
								{
									return false;
								}
							}
							this.Waiters++;
							if (this.IsSet)
							{
								int waiters = this.Waiters;
								this.Waiters = waiters - 1;
								return true;
							}
							try
							{
								if (!Monitor.Wait(this.m_lock, num2))
								{
									return false;
								}
							}
							finally
							{
								this.Waiters--;
							}
						}
					}
				}
				return true;
			}
			return true;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class.</summary>
		// Token: 0x06003F4E RID: 16206 RVA: 0x000ECD30 File Offset: 0x000EAF30
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.ManualResetEventSlim" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06003F4F RID: 16207 RVA: 0x000ECD40 File Offset: 0x000EAF40
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				return;
			}
			this.m_combinedState |= 1073741824;
			if (disposing)
			{
				ManualResetEvent eventObj = this.m_eventObj;
				if (eventObj != null)
				{
					ManualResetEvent manualResetEvent = eventObj;
					lock (manualResetEvent)
					{
						eventObj.Close();
						this.m_eventObj = null;
					}
				}
			}
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x000ECDBC File Offset: 0x000EAFBC
		private void ThrowIfDisposed()
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("ManualResetEventSlim_Disposed"));
			}
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x000ECDE0 File Offset: 0x000EAFE0
		private static void CancellationTokenCallback(object obj)
		{
			ManualResetEventSlim manualResetEventSlim = obj as ManualResetEventSlim;
			object @lock = manualResetEventSlim.m_lock;
			lock (@lock)
			{
				Monitor.PulseAll(manualResetEventSlim.m_lock);
			}
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x000ECE30 File Offset: 0x000EB030
		private void UpdateStateAtomically(int newBits, int updateBitsMask)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int combinedState = this.m_combinedState;
				int num = (combinedState & ~updateBitsMask) | newBits;
				if (Interlocked.CompareExchange(ref this.m_combinedState, num, combinedState) == combinedState)
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x000ECE6E File Offset: 0x000EB06E
		private static int ExtractStatePortionAndShiftRight(int state, int mask, int rightBitShiftCount)
		{
			return (int)((uint)(state & mask) >> rightBitShiftCount);
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x000ECE78 File Offset: 0x000EB078
		private static int ExtractStatePortion(int state, int mask)
		{
			return state & mask;
		}

		// Token: 0x04001A92 RID: 6802
		private const int DEFAULT_SPIN_SP = 1;

		// Token: 0x04001A93 RID: 6803
		private const int DEFAULT_SPIN_MP = 10;

		// Token: 0x04001A94 RID: 6804
		private volatile object m_lock;

		// Token: 0x04001A95 RID: 6805
		private volatile ManualResetEvent m_eventObj;

		// Token: 0x04001A96 RID: 6806
		private volatile int m_combinedState;

		// Token: 0x04001A97 RID: 6807
		private const int SignalledState_BitMask = -2147483648;

		// Token: 0x04001A98 RID: 6808
		private const int SignalledState_ShiftCount = 31;

		// Token: 0x04001A99 RID: 6809
		private const int Dispose_BitMask = 1073741824;

		// Token: 0x04001A9A RID: 6810
		private const int SpinCountState_BitMask = 1073217536;

		// Token: 0x04001A9B RID: 6811
		private const int SpinCountState_ShiftCount = 19;

		// Token: 0x04001A9C RID: 6812
		private const int SpinCountState_MaxValue = 2047;

		// Token: 0x04001A9D RID: 6813
		private const int NumWaitersState_BitMask = 524287;

		// Token: 0x04001A9E RID: 6814
		private const int NumWaitersState_ShiftCount = 0;

		// Token: 0x04001A9F RID: 6815
		private const int NumWaitersState_MaxValue = 524287;

		// Token: 0x04001AA0 RID: 6816
		private static Action<object> s_cancellationTokenCallback = new Action<object>(ManualResetEventSlim.CancellationTokenCallback);
	}
}
