using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides a mutual exclusion lock primitive where a thread trying to acquire the lock waits in a loop repeatedly checking until the lock becomes available.</summary>
	// Token: 0x02000535 RID: 1333
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(SpinLock.SystemThreading_SpinLockDebugView))]
	[DebuggerDisplay("IsHeld = {IsHeld}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct SpinLock
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SpinLock" /> structure with the option to track thread IDs to improve debugging.</summary>
		/// <param name="enableThreadOwnerTracking">Whether to capture and use thread IDs for debugging purposes.</param>
		// Token: 0x06003EC3 RID: 16067 RVA: 0x000EAA85 File Offset: 0x000E8C85
		[__DynamicallyInvokable]
		public SpinLock(bool enableThreadOwnerTracking)
		{
			this.m_owner = 0;
			if (!enableThreadOwnerTracking)
			{
				this.m_owner |= int.MinValue;
			}
		}

		/// <summary>Acquires the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling Enter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06003EC4 RID: 16068 RVA: 0x000EAAAC File Offset: 0x000E8CAC
		[__DynamicallyInvokable]
		public void Enter(ref bool lockTaken)
		{
			Thread.BeginCriticalRegion();
			int owner = this.m_owner;
			if (lockTaken || (owner & -2147483647) != -2147483648 || Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(-1, ref lockTaken);
			}
		}

		/// <summary>Attempts to acquire the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling TryEnter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06003EC5 RID: 16069 RVA: 0x000EAAF4 File Offset: 0x000E8CF4
		[__DynamicallyInvokable]
		public void TryEnter(ref bool lockTaken)
		{
			this.TryEnter(0, ref lockTaken);
		}

		/// <summary>Attempts to acquire the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling TryEnter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06003EC6 RID: 16070 RVA: 0x000EAB00 File Offset: 0x000E8D00
		[__DynamicallyInvokable]
		public void TryEnter(TimeSpan timeout, ref bool lockTaken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, Environment.GetResourceString("SpinLock_TryEnter_ArgumentOutOfRange"));
			}
			this.TryEnter((int)timeout.TotalMilliseconds, ref lockTaken);
		}

		/// <summary>Attempts to acquire the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling TryEnter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06003EC7 RID: 16071 RVA: 0x000EAB50 File Offset: 0x000E8D50
		[__DynamicallyInvokable]
		public void TryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			Thread.BeginCriticalRegion();
			int owner = this.m_owner;
			if (((millisecondsTimeout < -1) | lockTaken) || (owner & -2147483647) != -2147483648 || Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(millisecondsTimeout, ref lockTaken);
			}
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x000EABA0 File Offset: 0x000E8DA0
		private void ContinueTryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			Thread.EndCriticalRegion();
			if (lockTaken)
			{
				lockTaken = false;
				throw new ArgumentException(Environment.GetResourceString("SpinLock_TryReliableEnter_ArgumentException"));
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, Environment.GetResourceString("SpinLock_TryEnter_ArgumentOutOfRange"));
			}
			uint num = 0U;
			if (millisecondsTimeout != -1 && millisecondsTimeout != 0)
			{
				num = TimeoutHelper.GetTime();
			}
			if (CdsSyncEtwBCLProvider.Log.IsEnabled())
			{
				CdsSyncEtwBCLProvider.Log.SpinLock_FastPathFailed(this.m_owner);
			}
			if (this.IsThreadOwnerTrackingEnabled)
			{
				this.ContinueTryEnterWithThreadTracking(millisecondsTimeout, num, ref lockTaken);
				return;
			}
			int num2 = int.MaxValue;
			int num3 = this.m_owner;
			if ((num3 & 1) == 0)
			{
				Thread.BeginCriticalRegion();
				if (Interlocked.CompareExchange(ref this.m_owner, num3 | 1, num3, ref lockTaken) == num3)
				{
					return;
				}
				Thread.EndCriticalRegion();
			}
			else if ((num3 & 2147483646) != SpinLock.MAXIMUM_WAITERS)
			{
				num2 = (Interlocked.Add(ref this.m_owner, 2) & 2147483646) >> 1;
			}
			if (millisecondsTimeout == 0 || (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout) <= 0))
			{
				this.DecrementWaiters();
				return;
			}
			int processorCount = PlatformHelper.ProcessorCount;
			if (num2 < processorCount)
			{
				int num4 = 1;
				for (int i = 1; i <= num2 * 100; i++)
				{
					Thread.SpinWait((num2 + i) * 100 * num4);
					if (num4 < processorCount)
					{
						num4++;
					}
					num3 = this.m_owner;
					if ((num3 & 1) == 0)
					{
						Thread.BeginCriticalRegion();
						int num5 = (((num3 & 2147483646) == 0) ? (num3 | 1) : ((num3 - 2) | 1));
						if (Interlocked.CompareExchange(ref this.m_owner, num5, num3, ref lockTaken) == num3)
						{
							return;
						}
						Thread.EndCriticalRegion();
					}
				}
			}
			if (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout) <= 0)
			{
				this.DecrementWaiters();
				return;
			}
			int num6 = 0;
			for (;;)
			{
				num3 = this.m_owner;
				if ((num3 & 1) == 0)
				{
					Thread.BeginCriticalRegion();
					int num7 = (((num3 & 2147483646) == 0) ? (num3 | 1) : ((num3 - 2) | 1));
					if (Interlocked.CompareExchange(ref this.m_owner, num7, num3, ref lockTaken) == num3)
					{
						break;
					}
					Thread.EndCriticalRegion();
				}
				if (num6 % 40 == 0)
				{
					Thread.Sleep(1);
				}
				else if (num6 % 10 == 0)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Yield();
				}
				if (num6 % 10 == 0 && millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout) <= 0)
				{
					goto Block_26;
				}
				num6++;
			}
			return;
			Block_26:
			this.DecrementWaiters();
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000EADB4 File Offset: 0x000E8FB4
		private void DecrementWaiters()
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int owner = this.m_owner;
				if ((owner & 2147483646) == 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_owner, owner - 2, owner) == owner)
				{
					return;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x000EADF8 File Offset: 0x000E8FF8
		private void ContinueTryEnterWithThreadTracking(int millisecondsTimeout, uint startTime, ref bool lockTaken)
		{
			int num = 0;
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			if (this.m_owner == managedThreadId)
			{
				throw new LockRecursionException(Environment.GetResourceString("SpinLock_TryEnter_LockRecursionException"));
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				spinWait.SpinOnce();
				if (this.m_owner == num)
				{
					Thread.BeginCriticalRegion();
					if (Interlocked.CompareExchange(ref this.m_owner, managedThreadId, num, ref lockTaken) == num)
					{
						break;
					}
					Thread.EndCriticalRegion();
				}
				if (millisecondsTimeout == 0 || (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0))
				{
					return;
				}
			}
		}

		/// <summary>Releases the lock.</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">Thread ownership tracking is enabled, and the current thread is not the owner of this lock.</exception>
		// Token: 0x06003ECB RID: 16075 RVA: 0x000EAE7D File Offset: 0x000E907D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void Exit()
		{
			if ((this.m_owner & -2147483648) == 0)
			{
				this.ExitSlowPath(true);
			}
			else
			{
				Interlocked.Decrement(ref this.m_owner);
			}
			Thread.EndCriticalRegion();
		}

		/// <summary>Releases the lock.</summary>
		/// <param name="useMemoryBarrier">A Boolean value that indicates whether a memory fence should be issued in order to immediately publish the exit operation to other threads.</param>
		/// <exception cref="T:System.Threading.SynchronizationLockException">Thread ownership tracking is enabled, and the current thread is not the owner of this lock.</exception>
		// Token: 0x06003ECC RID: 16076 RVA: 0x000EAEAC File Offset: 0x000E90AC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void Exit(bool useMemoryBarrier)
		{
			if ((this.m_owner & -2147483648) != 0 && !useMemoryBarrier)
			{
				int owner = this.m_owner;
				this.m_owner = owner & -2;
			}
			else
			{
				this.ExitSlowPath(useMemoryBarrier);
			}
			Thread.EndCriticalRegion();
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x000EAEF0 File Offset: 0x000E90F0
		private void ExitSlowPath(bool useMemoryBarrier)
		{
			bool flag = (this.m_owner & int.MinValue) == 0;
			if (flag && !this.IsHeldByCurrentThread)
			{
				throw new SynchronizationLockException(Environment.GetResourceString("SpinLock_Exit_SynchronizationLockException"));
			}
			if (useMemoryBarrier)
			{
				if (flag)
				{
					Interlocked.Exchange(ref this.m_owner, 0);
					return;
				}
				Interlocked.Decrement(ref this.m_owner);
				return;
			}
			else
			{
				if (flag)
				{
					this.m_owner = 0;
					return;
				}
				int owner = this.m_owner;
				this.m_owner = owner & -2;
				return;
			}
		}

		/// <summary>Gets whether the lock is currently held by any thread.</summary>
		/// <returns>true if the lock is currently held by any thread; otherwise false.</returns>
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06003ECE RID: 16078 RVA: 0x000EAF6D File Offset: 0x000E916D
		[__DynamicallyInvokable]
		public bool IsHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				if (this.IsThreadOwnerTrackingEnabled)
				{
					return this.m_owner != 0;
				}
				return (this.m_owner & 1) != 0;
			}
		}

		/// <summary>Gets whether the lock is held by the current thread.</summary>
		/// <returns>true if the lock is held by the current thread; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">Thread ownership tracking is disabled.</exception>
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06003ECF RID: 16079 RVA: 0x000EAF90 File Offset: 0x000E9190
		[__DynamicallyInvokable]
		public bool IsHeldByCurrentThread
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				if (!this.IsThreadOwnerTrackingEnabled)
				{
					throw new InvalidOperationException(Environment.GetResourceString("SpinLock_IsHeldByCurrentThread"));
				}
				return (this.m_owner & int.MaxValue) == Thread.CurrentThread.ManagedThreadId;
			}
		}

		/// <summary>Gets whether thread ownership tracking is enabled for this instance.</summary>
		/// <returns>true if thread ownership tracking is enabled for this instance; otherwise false.</returns>
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x000EAFC4 File Offset: 0x000E91C4
		[__DynamicallyInvokable]
		public bool IsThreadOwnerTrackingEnabled
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return (this.m_owner & int.MinValue) == 0;
			}
		}

		// Token: 0x04001A5F RID: 6751
		private volatile int m_owner;

		// Token: 0x04001A60 RID: 6752
		private const int SPINNING_FACTOR = 100;

		// Token: 0x04001A61 RID: 6753
		private const int SLEEP_ONE_FREQUENCY = 40;

		// Token: 0x04001A62 RID: 6754
		private const int SLEEP_ZERO_FREQUENCY = 10;

		// Token: 0x04001A63 RID: 6755
		private const int TIMEOUT_CHECK_FREQUENCY = 10;

		// Token: 0x04001A64 RID: 6756
		private const int LOCK_ID_DISABLE_MASK = -2147483648;

		// Token: 0x04001A65 RID: 6757
		private const int LOCK_ANONYMOUS_OWNED = 1;

		// Token: 0x04001A66 RID: 6758
		private const int WAITERS_MASK = 2147483646;

		// Token: 0x04001A67 RID: 6759
		private const int ID_DISABLED_AND_ANONYMOUS_OWNED = -2147483647;

		// Token: 0x04001A68 RID: 6760
		private const int LOCK_UNOWNED = 0;

		// Token: 0x04001A69 RID: 6761
		private static int MAXIMUM_WAITERS = 2147483646;

		// Token: 0x02000BF5 RID: 3061
		internal class SystemThreading_SpinLockDebugView
		{
			// Token: 0x06006F9B RID: 28571 RVA: 0x00181E99 File Offset: 0x00180099
			public SystemThreading_SpinLockDebugView(SpinLock spinLock)
			{
				this.m_spinLock = spinLock;
			}

			// Token: 0x17001325 RID: 4901
			// (get) Token: 0x06006F9C RID: 28572 RVA: 0x00181EA8 File Offset: 0x001800A8
			public bool? IsHeldByCurrentThread
			{
				get
				{
					bool? flag;
					try
					{
						flag = new bool?(this.m_spinLock.IsHeldByCurrentThread);
					}
					catch (InvalidOperationException)
					{
						flag = null;
					}
					return flag;
				}
			}

			// Token: 0x17001326 RID: 4902
			// (get) Token: 0x06006F9D RID: 28573 RVA: 0x00181EE8 File Offset: 0x001800E8
			public int? OwnerThreadID
			{
				get
				{
					if (this.m_spinLock.IsThreadOwnerTrackingEnabled)
					{
						return new int?(this.m_spinLock.m_owner);
					}
					return null;
				}
			}

			// Token: 0x17001327 RID: 4903
			// (get) Token: 0x06006F9E RID: 28574 RVA: 0x00181F1E File Offset: 0x0018011E
			public bool IsHeld
			{
				get
				{
					return this.m_spinLock.IsHeld;
				}
			}

			// Token: 0x0400363F RID: 13887
			private SpinLock m_spinLock;
		}
	}
}
