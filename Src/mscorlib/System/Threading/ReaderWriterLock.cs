using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Defines a lock that supports single writers and multiple readers.</summary>
	// Token: 0x0200050E RID: 1294
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class ReaderWriterLock : CriticalFinalizerObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ReaderWriterLock" /> class.</summary>
		// Token: 0x06003D09 RID: 15625 RVA: 0x000E76EE File Offset: 0x000E58EE
		[SecuritySafeCritical]
		public ReaderWriterLock()
		{
			this.PrivateInitialize();
		}

		/// <summary>Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the <see cref="T:System.Threading.ReaderWriterLock" /> object.</summary>
		// Token: 0x06003D0A RID: 15626 RVA: 0x000E76FC File Offset: 0x000E58FC
		[SecuritySafeCritical]
		~ReaderWriterLock()
		{
			this.PrivateDestruct();
		}

		/// <summary>Gets a value indicating whether the current thread holds a reader lock.</summary>
		/// <returns>
		///   <see langword="true" /> if the current thread holds a reader lock; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06003D0B RID: 15627 RVA: 0x000E7728 File Offset: 0x000E5928
		public bool IsReaderLockHeld
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.PrivateGetIsReaderLockHeld();
			}
		}

		/// <summary>Gets a value indicating whether the current thread holds the writer lock.</summary>
		/// <returns>
		///   <see langword="true" /> if the current thread holds the writer lock; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x000E7730 File Offset: 0x000E5930
		public bool IsWriterLockHeld
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.PrivateGetIsWriterLockHeld();
			}
		}

		/// <summary>Gets the current sequence number.</summary>
		/// <returns>The current sequence number.</returns>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06003D0D RID: 15629 RVA: 0x000E7738 File Offset: 0x000E5938
		public int WriterSeqNum
		{
			[SecuritySafeCritical]
			get
			{
				return this.PrivateGetWriterSeqNum();
			}
		}

		// Token: 0x06003D0E RID: 15630
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AcquireReaderLockInternal(int millisecondsTimeout);

		/// <summary>Acquires a reader lock, using an <see cref="T:System.Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds.</param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="millisecondsTimeout" /> expires before the lock request is granted.</exception>
		// Token: 0x06003D0F RID: 15631 RVA: 0x000E7740 File Offset: 0x000E5940
		[SecuritySafeCritical]
		public void AcquireReaderLock(int millisecondsTimeout)
		{
			this.AcquireReaderLockInternal(millisecondsTimeout);
		}

		/// <summary>Acquires a reader lock, using a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">A <see langword="TimeSpan" /> specifying the time-out period.</param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds.</exception>
		// Token: 0x06003D10 RID: 15632 RVA: 0x000E774C File Offset: 0x000E594C
		[SecuritySafeCritical]
		public void AcquireReaderLock(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			this.AcquireReaderLockInternal((int)num);
		}

		// Token: 0x06003D11 RID: 15633
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AcquireWriterLockInternal(int millisecondsTimeout);

		/// <summary>Acquires the writer lock, using an <see cref="T:System.Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds.</param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted.</exception>
		// Token: 0x06003D12 RID: 15634 RVA: 0x000E778D File Offset: 0x000E598D
		[SecuritySafeCritical]
		public void AcquireWriterLock(int millisecondsTimeout)
		{
			this.AcquireWriterLockInternal(millisecondsTimeout);
		}

		/// <summary>Acquires the writer lock, using a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">The <see langword="TimeSpan" /> specifying the time-out period.</param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds.</exception>
		// Token: 0x06003D13 RID: 15635 RVA: 0x000E7798 File Offset: 0x000E5998
		[SecuritySafeCritical]
		public void AcquireWriterLock(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			this.AcquireWriterLockInternal((int)num);
		}

		// Token: 0x06003D14 RID: 15636
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReleaseReaderLockInternal();

		/// <summary>Decrements the lock count.</summary>
		/// <exception cref="T:System.ApplicationException">The thread does not have any reader or writer locks.</exception>
		// Token: 0x06003D15 RID: 15637 RVA: 0x000E77D9 File Offset: 0x000E59D9
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleaseReaderLock()
		{
			this.ReleaseReaderLockInternal();
		}

		// Token: 0x06003D16 RID: 15638
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReleaseWriterLockInternal();

		/// <summary>Decrements the lock count on the writer lock.</summary>
		/// <exception cref="T:System.ApplicationException">The thread does not have the writer lock.</exception>
		// Token: 0x06003D17 RID: 15639 RVA: 0x000E77E1 File Offset: 0x000E59E1
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleaseWriterLock()
		{
			this.ReleaseWriterLockInternal();
		}

		/// <summary>Upgrades a reader lock to the writer lock, using an <see langword="Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds.</param>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value.</returns>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="millisecondsTimeout" /> expires before the lock request is granted.</exception>
		// Token: 0x06003D18 RID: 15640 RVA: 0x000E77EC File Offset: 0x000E59EC
		[SecuritySafeCritical]
		public LockCookie UpgradeToWriterLock(int millisecondsTimeout)
		{
			LockCookie lockCookie = default(LockCookie);
			this.FCallUpgradeToWriterLock(ref lockCookie, millisecondsTimeout);
			return lockCookie;
		}

		// Token: 0x06003D19 RID: 15641
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void FCallUpgradeToWriterLock(ref LockCookie result, int millisecondsTimeout);

		/// <summary>Upgrades a reader lock to the writer lock, using a <see langword="TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">The <see langword="TimeSpan" /> specifying the time-out period.</param>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value.</returns>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds.</exception>
		// Token: 0x06003D1A RID: 15642 RVA: 0x000E780C File Offset: 0x000E5A0C
		public LockCookie UpgradeToWriterLock(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return this.UpgradeToWriterLock((int)num);
		}

		// Token: 0x06003D1B RID: 15643
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DowngradeFromWriterLockInternal(ref LockCookie lockCookie);

		/// <summary>Restores the lock status of the thread to what it was before <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" /> was called.</summary>
		/// <param name="lockCookie">A <see cref="T:System.Threading.LockCookie" /> returned by <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" />.</param>
		/// <exception cref="T:System.ApplicationException">The thread does not have the writer lock.</exception>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="lockCookie" /> is a null pointer.</exception>
		// Token: 0x06003D1C RID: 15644 RVA: 0x000E784D File Offset: 0x000E5A4D
		[SecuritySafeCritical]
		public void DowngradeFromWriterLock(ref LockCookie lockCookie)
		{
			this.DowngradeFromWriterLockInternal(ref lockCookie);
		}

		/// <summary>Releases the lock, regardless of the number of times the thread acquired the lock.</summary>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value representing the released lock.</returns>
		// Token: 0x06003D1D RID: 15645 RVA: 0x000E7858 File Offset: 0x000E5A58
		[SecuritySafeCritical]
		public LockCookie ReleaseLock()
		{
			LockCookie lockCookie = default(LockCookie);
			this.FCallReleaseLock(ref lockCookie);
			return lockCookie;
		}

		// Token: 0x06003D1E RID: 15646
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void FCallReleaseLock(ref LockCookie result);

		// Token: 0x06003D1F RID: 15647
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RestoreLockInternal(ref LockCookie lockCookie);

		/// <summary>Restores the lock status of the thread to what it was before calling <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />.</summary>
		/// <param name="lockCookie">A <see cref="T:System.Threading.LockCookie" /> returned by <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />.</param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="lockCookie" /> is a null pointer.</exception>
		// Token: 0x06003D20 RID: 15648 RVA: 0x000E7876 File Offset: 0x000E5A76
		[SecuritySafeCritical]
		public void RestoreLock(ref LockCookie lockCookie)
		{
			this.RestoreLockInternal(ref lockCookie);
		}

		// Token: 0x06003D21 RID: 15649
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool PrivateGetIsReaderLockHeld();

		// Token: 0x06003D22 RID: 15650
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool PrivateGetIsWriterLockHeld();

		// Token: 0x06003D23 RID: 15651
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int PrivateGetWriterSeqNum();

		/// <summary>Indicates whether the writer lock has been granted to any thread since the sequence number was obtained.</summary>
		/// <param name="seqNum">The sequence number.</param>
		/// <returns>
		///   <see langword="true" /> if the writer lock has been granted to any thread since the sequence number was obtained; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003D24 RID: 15652
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AnyWritersSince(int seqNum);

		// Token: 0x06003D25 RID: 15653
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void PrivateInitialize();

		// Token: 0x06003D26 RID: 15654
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void PrivateDestruct();

		// Token: 0x040019E2 RID: 6626
		private IntPtr _hWriterEvent;

		// Token: 0x040019E3 RID: 6627
		private IntPtr _hReaderEvent;

		// Token: 0x040019E4 RID: 6628
		private IntPtr _hObjectHandle;

		// Token: 0x040019E5 RID: 6629
		private int _dwState;

		// Token: 0x040019E6 RID: 6630
		private int _dwULockID;

		// Token: 0x040019E7 RID: 6631
		private int _dwLLockID;

		// Token: 0x040019E8 RID: 6632
		private int _dwWriterID;

		// Token: 0x040019E9 RID: 6633
		private int _dwWriterSeqNum;

		// Token: 0x040019EA RID: 6634
		private short _wWriterLevel;
	}
}
