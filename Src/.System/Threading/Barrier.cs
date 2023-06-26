using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Enables multiple tasks to cooperatively work on an algorithm in parallel through multiple phases.</summary>
	// Token: 0x020003D6 RID: 982
	[ComVisible(false)]
	[DebuggerDisplay("Participant Count={ParticipantCount},Participants Remaining={ParticipantsRemaining}")]
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Barrier : IDisposable
	{
		/// <summary>Gets the number of participants in the barrier that haven't yet signaled in the current phase.</summary>
		/// <returns>Returns the number of participants in the barrier that haven't yet signaled in the current phase.</returns>
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x000AF310 File Offset: 0x000AD510
		[global::__DynamicallyInvokable]
		public int ParticipantsRemaining
		{
			[global::__DynamicallyInvokable]
			get
			{
				int currentTotalCount = this.m_currentTotalCount;
				int num = currentTotalCount & 32767;
				int num2 = (currentTotalCount & 2147418112) >> 16;
				return num - num2;
			}
		}

		/// <summary>Gets the total number of participants in the barrier.</summary>
		/// <returns>Returns the total number of participants in the barrier.</returns>
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x000AF33C File Offset: 0x000AD53C
		[global::__DynamicallyInvokable]
		public int ParticipantCount
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_currentTotalCount & 32767;
			}
		}

		/// <summary>Gets the number of the barrier's current phase.</summary>
		/// <returns>Returns the number of the barrier's current phase.</returns>
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x000AF34C File Offset: 0x000AD54C
		// (set) Token: 0x060025B9 RID: 9657 RVA: 0x000AF359 File Offset: 0x000AD559
		[global::__DynamicallyInvokable]
		public long CurrentPhaseNumber
		{
			[global::__DynamicallyInvokable]
			get
			{
				return Volatile.Read(ref this.m_currentPhase);
			}
			internal set
			{
				Volatile.Write(ref this.m_currentPhase, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Barrier" /> class.</summary>
		/// <param name="participantCount">The number of participating threads.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="participantCount" /> is less than 0 or greater than 32,767.</exception>
		// Token: 0x060025BA RID: 9658 RVA: 0x000AF367 File Offset: 0x000AD567
		[global::__DynamicallyInvokable]
		public Barrier(int participantCount)
			: this(participantCount, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Barrier" /> class.</summary>
		/// <param name="participantCount">The number of participating threads.</param>
		/// <param name="postPhaseAction">The <see cref="T:System.Action`1" /> to be executed after each phase. null (Nothing in Visual Basic) may be passed to indicate no action is taken.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="participantCount" /> is less than 0 or greater than 32,767.</exception>
		// Token: 0x060025BB RID: 9659 RVA: 0x000AF374 File Offset: 0x000AD574
		[global::__DynamicallyInvokable]
		public Barrier(int participantCount, Action<Barrier> postPhaseAction)
		{
			if (participantCount < 0 || participantCount > 32767)
			{
				throw new ArgumentOutOfRangeException("participantCount", participantCount, SR.GetString("Barrier_ctor_ArgumentOutOfRange"));
			}
			this.m_currentTotalCount = participantCount;
			this.m_postPhaseAction = postPhaseAction;
			this.m_oddEvent = new ManualResetEventSlim(true);
			this.m_evenEvent = new ManualResetEventSlim(false);
			if (postPhaseAction != null && !ExecutionContext.IsFlowSuppressed())
			{
				this.m_ownerThreadContext = ExecutionContext.Capture();
			}
			this.m_actionCallerID = 0;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000AF3F2 File Offset: 0x000AD5F2
		private void GetCurrentTotal(int currentTotal, out int current, out int total, out bool sense)
		{
			total = currentTotal & 32767;
			current = (currentTotal & 2147418112) >> 16;
			sense = (currentTotal & int.MinValue) == 0;
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000AF41C File Offset: 0x000AD61C
		private bool SetCurrentTotal(int currentTotal, int current, int total, bool sense)
		{
			int num = (current << 16) | total;
			if (!sense)
			{
				num |= int.MinValue;
			}
			return Interlocked.CompareExchange(ref this.m_currentTotalCount, num, currentTotal) == currentTotal;
		}

		/// <summary>Notifies the <see cref="T:System.Threading.Barrier" /> that there will be an additional participant.</summary>
		/// <returns>The phase number of the barrier in which the new participants will first participate.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Adding a participant would cause the barrier's participant count to exceed 32,767.  
		///  -or-  
		///  The method was invoked from within a post-phase action.</exception>
		// Token: 0x060025BE RID: 9662 RVA: 0x000AF44C File Offset: 0x000AD64C
		[global::__DynamicallyInvokable]
		public long AddParticipant()
		{
			long num;
			try
			{
				num = this.AddParticipants(1);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new InvalidOperationException(SR.GetString("Barrier_AddParticipants_Overflow_ArgumentOutOfRange"));
			}
			return num;
		}

		/// <summary>Notifies the <see cref="T:System.Threading.Barrier" /> that there will be additional participants.</summary>
		/// <param name="participantCount">The number of additional participants to add to the barrier.</param>
		/// <returns>The phase number of the barrier in which the new participants will first participate.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="participantCount" /> is less than 0.  
		/// -or-  
		/// Adding <paramref name="participantCount" /> participants would cause the barrier's participant count to exceed 32,767.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action.</exception>
		// Token: 0x060025BF RID: 9663 RVA: 0x000AF488 File Offset: 0x000AD688
		[global::__DynamicallyInvokable]
		public long AddParticipants(int participantCount)
		{
			this.ThrowIfDisposed();
			if (participantCount < 1)
			{
				throw new ArgumentOutOfRangeException("participantCount", participantCount, SR.GetString("Barrier_AddParticipants_NonPositive_ArgumentOutOfRange"));
			}
			if (participantCount > 32767)
			{
				throw new ArgumentOutOfRangeException("participantCount", SR.GetString("Barrier_AddParticipants_Overflow_ArgumentOutOfRange"));
			}
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(SR.GetString("Barrier_InvalidOperation_CalledFromPHA"));
			}
			SpinWait spinWait = default(SpinWait);
			bool flag;
			for (;;)
			{
				int currentTotalCount = this.m_currentTotalCount;
				int num;
				int num2;
				this.GetCurrentTotal(currentTotalCount, out num, out num2, out flag);
				if (participantCount + num2 > 32767)
				{
					break;
				}
				if (this.SetCurrentTotal(currentTotalCount, num, num2 + participantCount, flag))
				{
					goto Block_6;
				}
				spinWait.SpinOnce();
			}
			throw new ArgumentOutOfRangeException("participantCount", SR.GetString("Barrier_AddParticipants_Overflow_ArgumentOutOfRange"));
			Block_6:
			long currentPhaseNumber = this.CurrentPhaseNumber;
			long num3 = ((flag != (currentPhaseNumber % 2L == 0L)) ? (currentPhaseNumber + 1L) : currentPhaseNumber);
			if (num3 != currentPhaseNumber)
			{
				if (flag)
				{
					this.m_oddEvent.Wait();
				}
				else
				{
					this.m_evenEvent.Wait();
				}
			}
			else if (flag && this.m_evenEvent.IsSet)
			{
				this.m_evenEvent.Reset();
			}
			else if (!flag && this.m_oddEvent.IsSet)
			{
				this.m_oddEvent.Reset();
			}
			return num3;
		}

		/// <summary>Notifies the <see cref="T:System.Threading.Barrier" /> that there will be one less participant.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The barrier already has 0 participants.  
		///  -or-  
		///  The method was invoked from within a post-phase action.</exception>
		// Token: 0x060025C0 RID: 9664 RVA: 0x000AF5DC File Offset: 0x000AD7DC
		[global::__DynamicallyInvokable]
		public void RemoveParticipant()
		{
			this.RemoveParticipants(1);
		}

		/// <summary>Notifies the <see cref="T:System.Threading.Barrier" /> that there will be fewer participants.</summary>
		/// <param name="participantCount">The number of additional participants to remove from the barrier.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The total participant count is less than the specified <paramref name="participantCount" /></exception>
		/// <exception cref="T:System.InvalidOperationException">The barrier already has 0 participants.  
		///  -or-  
		///  The method was invoked from within a post-phase action.  
		///  -or-  
		///  current participant count is less than the specified participantCount</exception>
		// Token: 0x060025C1 RID: 9665 RVA: 0x000AF5E8 File Offset: 0x000AD7E8
		[global::__DynamicallyInvokable]
		public void RemoveParticipants(int participantCount)
		{
			this.ThrowIfDisposed();
			if (participantCount < 1)
			{
				throw new ArgumentOutOfRangeException("participantCount", participantCount, SR.GetString("Barrier_RemoveParticipants_NonPositive_ArgumentOutOfRange"));
			}
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(SR.GetString("Barrier_InvalidOperation_CalledFromPHA"));
			}
			SpinWait spinWait = default(SpinWait);
			bool flag;
			for (;;)
			{
				int currentTotalCount = this.m_currentTotalCount;
				int num;
				int num2;
				this.GetCurrentTotal(currentTotalCount, out num, out num2, out flag);
				if (num2 < participantCount)
				{
					break;
				}
				if (num2 - participantCount < num)
				{
					goto Block_5;
				}
				int num3 = num2 - participantCount;
				if (num3 > 0 && num == num3)
				{
					if (this.SetCurrentTotal(currentTotalCount, 0, num2 - participantCount, !flag))
					{
						goto Block_8;
					}
				}
				else if (this.SetCurrentTotal(currentTotalCount, num, num2 - participantCount, flag))
				{
					return;
				}
				spinWait.SpinOnce();
			}
			throw new ArgumentOutOfRangeException("participantCount", SR.GetString("Barrier_RemoveParticipants_ArgumentOutOfRange"));
			Block_5:
			throw new InvalidOperationException(SR.GetString("Barrier_RemoveParticipants_InvalidOperation"));
			Block_8:
			this.FinishPhase(flag);
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier as well.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		/// <exception cref="T:System.Threading.BarrierPostPhaseException">If an exception is thrown from the post phase action of a Barrier after all participating threads have called SignalAndWait, the exception will be wrapped in a BarrierPostPhaseException and be thrown on all participating threads.</exception>
		// Token: 0x060025C2 RID: 9666 RVA: 0x000AF6D8 File Offset: 0x000AD8D8
		[global::__DynamicallyInvokable]
		public void SignalAndWait()
		{
			this.SignalAndWait(default(CancellationToken));
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier, while observing a cancellation token.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		// Token: 0x060025C3 RID: 9667 RVA: 0x000AF6F4 File Offset: 0x000AD8F4
		[global::__DynamicallyInvokable]
		public void SignalAndWait(CancellationToken cancellationToken)
		{
			this.SignalAndWait(-1, cancellationToken);
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier as well, using a <see cref="T:System.TimeSpan" /> object to measure the time interval.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>true if all other participants reached the barrier; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out, or it is greater than 32,767.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		// Token: 0x060025C4 RID: 9668 RVA: 0x000AF700 File Offset: 0x000AD900
		[global::__DynamicallyInvokable]
		public bool SignalAndWait(TimeSpan timeout)
		{
			return this.SignalAndWait(timeout, default(CancellationToken));
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier as well, using a <see cref="T:System.TimeSpan" /> object to measure the time interval, while observing a cancellation token.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>true if all other participants reached the barrier; otherwise, false.</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		// Token: 0x060025C5 RID: 9669 RVA: 0x000AF720 File Offset: 0x000AD920
		[global::__DynamicallyInvokable]
		public bool SignalAndWait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("Barrier_SignalAndWait_ArgumentOutOfRange"));
			}
			return this.SignalAndWait((int)timeout.TotalMilliseconds, cancellationToken);
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier as well, using a 32-bit signed integer to measure the timeout.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <returns>if all participants reached the barrier within the specified time; otherwise false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		/// <exception cref="T:System.Threading.BarrierPostPhaseException">If an exception is thrown from the post phase action of a Barrier after all participating threads have called SignalAndWait, the exception will be wrapped in a BarrierPostPhaseException and be thrown on all participating threads.</exception>
		// Token: 0x060025C6 RID: 9670 RVA: 0x000AF770 File Offset: 0x000AD970
		[global::__DynamicallyInvokable]
		public bool SignalAndWait(int millisecondsTimeout)
		{
			return this.SignalAndWait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier as well, using a 32-bit signed integer to measure the timeout, while observing a cancellation token.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>if all participants reached the barrier within the specified time; otherwise false</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		// Token: 0x060025C7 RID: 9671 RVA: 0x000AF790 File Offset: 0x000AD990
		[global::__DynamicallyInvokable]
		public bool SignalAndWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, SR.GetString("Barrier_SignalAndWait_ArgumentOutOfRange"));
			}
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(SR.GetString("Barrier_InvalidOperation_CalledFromPHA"));
			}
			SpinWait spinWait = default(SpinWait);
			bool flag;
			long currentPhaseNumber;
			for (;;)
			{
				int num = this.m_currentTotalCount;
				int num2;
				int num3;
				this.GetCurrentTotal(num, out num2, out num3, out flag);
				currentPhaseNumber = this.CurrentPhaseNumber;
				if (num3 == 0)
				{
					break;
				}
				if (num2 == 0 && flag != (this.CurrentPhaseNumber % 2L == 0L))
				{
					goto Block_6;
				}
				if (num2 + 1 == num3)
				{
					if (this.SetCurrentTotal(num, 0, num3, !flag))
					{
						goto Block_8;
					}
				}
				else if (this.SetCurrentTotal(num, num2 + 1, num3, flag))
				{
					goto IL_107;
				}
				spinWait.SpinOnce();
			}
			throw new InvalidOperationException(SR.GetString("Barrier_SignalAndWait_InvalidOperation_ZeroTotal"));
			Block_6:
			throw new InvalidOperationException(SR.GetString("Barrier_SignalAndWait_InvalidOperation_ThreadsExceeded"));
			Block_8:
			if (CdsSyncEtwBCLProvider.Log.IsEnabled())
			{
				CdsSyncEtwBCLProvider.Log.Barrier_PhaseFinished(flag, this.CurrentPhaseNumber);
			}
			this.FinishPhase(flag);
			return true;
			IL_107:
			ManualResetEventSlim manualResetEventSlim = (flag ? this.m_evenEvent : this.m_oddEvent);
			bool flag2 = false;
			bool flag3 = false;
			try
			{
				flag3 = this.DiscontinuousWait(manualResetEventSlim, millisecondsTimeout, cancellationToken, currentPhaseNumber);
			}
			catch (OperationCanceledException)
			{
				flag2 = true;
			}
			catch (ObjectDisposedException)
			{
				if (currentPhaseNumber >= this.CurrentPhaseNumber)
				{
					throw;
				}
				flag3 = true;
			}
			if (!flag3)
			{
				spinWait.Reset();
				for (;;)
				{
					int num = this.m_currentTotalCount;
					int num2;
					int num3;
					bool flag4;
					this.GetCurrentTotal(num, out num2, out num3, out flag4);
					if (currentPhaseNumber < this.CurrentPhaseNumber || flag != flag4)
					{
						break;
					}
					if (this.SetCurrentTotal(num, num2 - 1, num3, flag))
					{
						goto Block_14;
					}
					spinWait.SpinOnce();
				}
				this.WaitCurrentPhase(manualResetEventSlim, currentPhaseNumber);
				goto IL_1B4;
				Block_14:
				if (flag2)
				{
					throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), cancellationToken);
				}
				return false;
			}
			IL_1B4:
			if (this.m_exception != null)
			{
				throw new BarrierPostPhaseException(this.m_exception);
			}
			return true;
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000AF984 File Offset: 0x000ADB84
		[SecuritySafeCritical]
		private void FinishPhase(bool observedSense)
		{
			if (this.m_postPhaseAction != null)
			{
				try
				{
					this.m_actionCallerID = Thread.CurrentThread.ManagedThreadId;
					if (this.m_ownerThreadContext != null)
					{
						ExecutionContext ownerThreadContext = this.m_ownerThreadContext;
						this.m_ownerThreadContext = this.m_ownerThreadContext.CreateCopy();
						ContextCallback contextCallback = Barrier.s_invokePostPhaseAction;
						if (contextCallback == null)
						{
							contextCallback = (Barrier.s_invokePostPhaseAction = new ContextCallback(Barrier.InvokePostPhaseAction));
						}
						ExecutionContext.Run(ownerThreadContext, contextCallback, this);
						ownerThreadContext.Dispose();
					}
					else
					{
						this.m_postPhaseAction(this);
					}
					this.m_exception = null;
					return;
				}
				catch (Exception ex)
				{
					this.m_exception = ex;
					return;
				}
				finally
				{
					this.m_actionCallerID = 0;
					this.SetResetEvents(observedSense);
					if (this.m_exception != null)
					{
						throw new BarrierPostPhaseException(this.m_exception);
					}
				}
			}
			this.SetResetEvents(observedSense);
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x000AFA60 File Offset: 0x000ADC60
		[SecurityCritical]
		private static void InvokePostPhaseAction(object obj)
		{
			Barrier barrier = (Barrier)obj;
			barrier.m_postPhaseAction(barrier);
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000AFA80 File Offset: 0x000ADC80
		private void SetResetEvents(bool observedSense)
		{
			this.CurrentPhaseNumber += 1L;
			if (observedSense)
			{
				this.m_oddEvent.Reset();
				this.m_evenEvent.Set();
				return;
			}
			this.m_evenEvent.Reset();
			this.m_oddEvent.Set();
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000AFACC File Offset: 0x000ADCCC
		private void WaitCurrentPhase(ManualResetEventSlim currentPhaseEvent, long observedPhase)
		{
			SpinWait spinWait = default(SpinWait);
			while (!currentPhaseEvent.IsSet && this.CurrentPhaseNumber - observedPhase <= 1L)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000AFB00 File Offset: 0x000ADD00
		private bool DiscontinuousWait(ManualResetEventSlim currentPhaseEvent, int totalTimeout, CancellationToken token, long observedPhase)
		{
			int num = 100;
			int num2 = 10000;
			while (observedPhase == this.CurrentPhaseNumber)
			{
				int num3 = ((totalTimeout == -1) ? num : Math.Min(num, totalTimeout));
				if (currentPhaseEvent.Wait(num3, token))
				{
					return true;
				}
				if (totalTimeout != -1)
				{
					totalTimeout -= num3;
					if (totalTimeout <= 0)
					{
						return false;
					}
				}
				num = ((num >= num2) ? num2 : Math.Min(num << 1, num2));
			}
			this.WaitCurrentPhase(currentPhaseEvent, observedPhase);
			return true;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.Barrier" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action.</exception>
		// Token: 0x060025CD RID: 9677 RVA: 0x000AFB67 File Offset: 0x000ADD67
		[global::__DynamicallyInvokable]
		public void Dispose()
		{
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(SR.GetString("Barrier_InvalidOperation_CalledFromPHA"));
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.Barrier" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060025CE RID: 9678 RVA: 0x000AFBA0 File Offset: 0x000ADDA0
		[global::__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
					this.m_oddEvent.Dispose();
					this.m_evenEvent.Dispose();
					if (this.m_ownerThreadContext != null)
					{
						this.m_ownerThreadContext.Dispose();
						this.m_ownerThreadContext = null;
					}
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000AFBEF File Offset: 0x000ADDEF
		private void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("Barrier", SR.GetString("Barrier_Dispose"));
			}
		}

		// Token: 0x0400204C RID: 8268
		private volatile int m_currentTotalCount;

		// Token: 0x0400204D RID: 8269
		private const int CURRENT_MASK = 2147418112;

		// Token: 0x0400204E RID: 8270
		private const int TOTAL_MASK = 32767;

		// Token: 0x0400204F RID: 8271
		private const int SENSE_MASK = -2147483648;

		// Token: 0x04002050 RID: 8272
		private const int MAX_PARTICIPANTS = 32767;

		// Token: 0x04002051 RID: 8273
		private long m_currentPhase;

		// Token: 0x04002052 RID: 8274
		private bool m_disposed;

		// Token: 0x04002053 RID: 8275
		private ManualResetEventSlim m_oddEvent;

		// Token: 0x04002054 RID: 8276
		private ManualResetEventSlim m_evenEvent;

		// Token: 0x04002055 RID: 8277
		private ExecutionContext m_ownerThreadContext;

		// Token: 0x04002056 RID: 8278
		[SecurityCritical]
		private static ContextCallback s_invokePostPhaseAction;

		// Token: 0x04002057 RID: 8279
		private Action<Barrier> m_postPhaseAction;

		// Token: 0x04002058 RID: 8280
		private Exception m_exception;

		// Token: 0x04002059 RID: 8281
		private int m_actionCallerID;
	}
}
