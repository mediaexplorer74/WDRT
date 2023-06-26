using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Represents a synchronization primitive that is signaled when its count reaches zero.</summary>
	// Token: 0x02000539 RID: 1337
	[ComVisible(false)]
	[DebuggerDisplay("Initial Count={InitialCount}, Current Count={CurrentCount}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class CountdownEvent : IDisposable
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Threading.CountdownEvent" /> class with the specified count.</summary>
		/// <param name="initialCount">The number of signals initially required to set the <see cref="T:System.Threading.CountdownEvent" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCount" /> is less than 0.</exception>
		// Token: 0x06003EDD RID: 16093 RVA: 0x000EB206 File Offset: 0x000E9406
		[__DynamicallyInvokable]
		public CountdownEvent(int initialCount)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount");
			}
			this.m_initialCount = initialCount;
			this.m_currentCount = initialCount;
			this.m_event = new ManualResetEventSlim();
			if (initialCount == 0)
			{
				this.m_event.Set();
			}
		}

		/// <summary>Gets the number of remaining signals required to set the event.</summary>
		/// <returns>The number of remaining signals required to set the event.</returns>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06003EDE RID: 16094 RVA: 0x000EB248 File Offset: 0x000E9448
		[__DynamicallyInvokable]
		public int CurrentCount
		{
			[__DynamicallyInvokable]
			get
			{
				int currentCount = this.m_currentCount;
				if (currentCount >= 0)
				{
					return currentCount;
				}
				return 0;
			}
		}

		/// <summary>Gets the numbers of signals initially required to set the event.</summary>
		/// <returns>The number of signals initially required to set the event.</returns>
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06003EDF RID: 16095 RVA: 0x000EB265 File Offset: 0x000E9465
		[__DynamicallyInvokable]
		public int InitialCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_initialCount;
			}
		}

		/// <summary>Indicates whether the <see cref="T:System.Threading.CountdownEvent" /> object's current count has reached zero.</summary>
		/// <returns>
		///   <see langword="true" /> if the current count is zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x000EB26D File Offset: 0x000E946D
		[__DynamicallyInvokable]
		public bool IsSet
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_currentCount <= 0;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for the event to be set.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for the event to be set.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x000EB27D File Offset: 0x000E947D
		[__DynamicallyInvokable]
		public WaitHandle WaitHandle
		{
			[__DynamicallyInvokable]
			get
			{
				this.ThrowIfDisposed();
				return this.m_event.WaitHandle;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.CountdownEvent" /> class.</summary>
		// Token: 0x06003EE2 RID: 16098 RVA: 0x000EB290 File Offset: 0x000E9490
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.CountdownEvent" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06003EE3 RID: 16099 RVA: 0x000EB29F File Offset: 0x000E949F
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.m_event.Dispose();
				this.m_disposed = true;
			}
		}

		/// <summary>Registers a signal with the <see cref="T:System.Threading.CountdownEvent" />, decrementing the value of <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</summary>
		/// <returns>true if the signal caused the count to reach zero and the event was set; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.</exception>
		// Token: 0x06003EE4 RID: 16100 RVA: 0x000EB2B8 File Offset: 0x000E94B8
		[__DynamicallyInvokable]
		public bool Signal()
		{
			this.ThrowIfDisposed();
			if (this.m_currentCount <= 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
			}
			int num = Interlocked.Decrement(ref this.m_currentCount);
			if (num == 0)
			{
				this.m_event.Set();
				return true;
			}
			if (num < 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
			}
			return false;
		}

		/// <summary>Registers multiple signals with the <see cref="T:System.Threading.CountdownEvent" />, decrementing the value of <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by the specified amount.</summary>
		/// <param name="signalCount">The number of signals to register.</param>
		/// <returns>true if the signals caused the count to reach zero and the event was set; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set. -or- Or <paramref name="signalCount" /> is greater than <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</exception>
		// Token: 0x06003EE5 RID: 16101 RVA: 0x000EB318 File Offset: 0x000E9518
		[__DynamicallyInvokable]
		public bool Signal(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			int currentCount;
			for (;;)
			{
				currentCount = this.m_currentCount;
				if (currentCount < signalCount)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_currentCount, currentCount - signalCount, currentCount) == currentCount)
				{
					goto IL_55;
				}
				spinWait.SpinOnce();
			}
			throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
			IL_55:
			if (currentCount == signalCount)
			{
				this.m_event.Set();
				return true;
			}
			return false;
		}

		/// <summary>Increments the <see cref="T:System.Threading.CountdownEvent" />'s current count by one.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.  
		///  -or-  
		///  <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003EE6 RID: 16102 RVA: 0x000EB38C File Offset: 0x000E958C
		[__DynamicallyInvokable]
		public void AddCount()
		{
			this.AddCount(1);
		}

		/// <summary>Attempts to increment <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by one.</summary>
		/// <returns>true if the increment succeeded; otherwise, false. If <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is already at zero, this method will return false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003EE7 RID: 16103 RVA: 0x000EB395 File Offset: 0x000E9595
		[__DynamicallyInvokable]
		public bool TryAddCount()
		{
			return this.TryAddCount(1);
		}

		/// <summary>Increments the <see cref="T:System.Threading.CountdownEvent" />'s current count by a specified value.</summary>
		/// <param name="signalCount">The value by which to increase <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than or equal to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.  
		///  -or-  
		///  <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" /> after count is incremented by <paramref name="signalCount." /></exception>
		// Token: 0x06003EE8 RID: 16104 RVA: 0x000EB39E File Offset: 0x000E959E
		[__DynamicallyInvokable]
		public void AddCount(int signalCount)
		{
			if (!this.TryAddCount(signalCount))
			{
				throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Increment_AlreadyZero"));
			}
		}

		/// <summary>Attempts to increment <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by a specified value.</summary>
		/// <param name="signalCount">The value by which to increase <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</param>
		/// <returns>true if the increment succeeded; otherwise, false. If <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is already at zero this will return false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than or equal to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> + <paramref name="signalCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003EE9 RID: 16105 RVA: 0x000EB3BC File Offset: 0x000E95BC
		[__DynamicallyInvokable]
		public bool TryAddCount(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int currentCount = this.m_currentCount;
				if (currentCount <= 0)
				{
					break;
				}
				if (currentCount > 2147483647 - signalCount)
				{
					goto Block_3;
				}
				if (Interlocked.CompareExchange(ref this.m_currentCount, currentCount + signalCount, currentCount) == currentCount)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
			Block_3:
			throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Increment_AlreadyMax"));
		}

		/// <summary>Resets the <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> to the value of <see cref="P:System.Threading.CountdownEvent.InitialCount" />.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x06003EEA RID: 16106 RVA: 0x000EB42B File Offset: 0x000E962B
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.Reset(this.m_initialCount);
		}

		/// <summary>Resets the <see cref="P:System.Threading.CountdownEvent.InitialCount" /> property to a specified value.</summary>
		/// <param name="count">The number of signals required to set the <see cref="T:System.Threading.CountdownEvent" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has alread been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 0.</exception>
		// Token: 0x06003EEB RID: 16107 RVA: 0x000EB43C File Offset: 0x000E963C
		[__DynamicallyInvokable]
		public void Reset(int count)
		{
			this.ThrowIfDisposed();
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.m_currentCount = count;
			this.m_initialCount = count;
			if (count == 0)
			{
				this.m_event.Set();
				return;
			}
			this.m_event.Reset();
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x06003EEC RID: 16108 RVA: 0x000EB488 File Offset: 0x000E9688
		[__DynamicallyInvokable]
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003EED RID: 16109 RVA: 0x000EB4A6 File Offset: 0x000E96A6
		[__DynamicallyInvokable]
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the timeout.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003EEE RID: 16110 RVA: 0x000EB4B4 File Offset: 0x000E96B4
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

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003EEF RID: 16111 RVA: 0x000EB4F4 File Offset: 0x000E96F4
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

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a 32-bit signed integer to measure the timeout.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06003EF0 RID: 16112 RVA: 0x000EB52C File Offset: 0x000E972C
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a 32-bit signed integer to measure the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06003EF1 RID: 16113 RVA: 0x000EB54C File Offset: 0x000E974C
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			bool flag = this.IsSet;
			if (!flag)
			{
				flag = this.m_event.Wait(millisecondsTimeout, cancellationToken);
			}
			return flag;
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x000EB58E File Offset: 0x000E978E
		private void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("CountdownEvent");
			}
		}

		// Token: 0x04001A71 RID: 6769
		private int m_initialCount;

		// Token: 0x04001A72 RID: 6770
		private volatile int m_currentCount;

		// Token: 0x04001A73 RID: 6771
		private ManualResetEventSlim m_event;

		// Token: 0x04001A74 RID: 6772
		private volatile bool m_disposed;
	}
}
