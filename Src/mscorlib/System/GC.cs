using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Controls the system garbage collector, a service that automatically reclaims unused memory.</summary>
	// Token: 0x020000E9 RID: 233
	[__DynamicallyInvokable]
	public static class GC
	{
		// Token: 0x06000E90 RID: 3728
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetGCLatencyMode();

		// Token: 0x06000E91 RID: 3729
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int SetGCLatencyMode(int newLatencyMode);

		// Token: 0x06000E92 RID: 3730
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int _StartNoGCRegion(long totalSize, bool lohSizeKnown, long lohSize, bool disallowFullBlockingGC);

		// Token: 0x06000E93 RID: 3731
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int _EndNoGCRegion();

		// Token: 0x06000E94 RID: 3732
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLOHCompactionMode();

		// Token: 0x06000E95 RID: 3733
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetLOHCompactionMode(int newLOHCompactionyMode);

		// Token: 0x06000E96 RID: 3734
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGenerationWR(IntPtr handle);

		// Token: 0x06000E97 RID: 3735
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern long GetTotalMemory();

		// Token: 0x06000E98 RID: 3736
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _Collect(int generation, int mode);

		// Token: 0x06000E99 RID: 3737
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxGeneration();

		// Token: 0x06000E9A RID: 3738
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _CollectionCount(int generation, int getSpecialGCCount);

		// Token: 0x06000E9B RID: 3739
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsServerGC();

		// Token: 0x06000E9C RID: 3740
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _AddMemoryPressure(ulong bytesAllocated);

		// Token: 0x06000E9D RID: 3741
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _RemoveMemoryPressure(ulong bytesAllocated);

		/// <summary>Informs the runtime of a large allocation of unmanaged memory that should be taken into account when scheduling garbage collection.</summary>
		/// <param name="bytesAllocated">The incremental amount of unmanaged memory that has been allocated.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bytesAllocated" /> is less than or equal to 0.  
		/// -or-  
		/// On a 32-bit computer, <paramref name="bytesAllocated" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000E9E RID: 3742 RVA: 0x0002CD48 File Offset: 0x0002AF48
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void AddMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("pressure", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegInt32"));
			}
			GC._AddMemoryPressure((ulong)bytesAllocated);
		}

		/// <summary>Informs the runtime that unmanaged memory has been released and no longer needs to be taken into account when scheduling garbage collection.</summary>
		/// <param name="bytesAllocated">The amount of unmanaged memory that has been released.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bytesAllocated" /> is less than or equal to 0.  
		/// -or-  
		/// On a 32-bit computer, <paramref name="bytesAllocated" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000E9F RID: 3743 RVA: 0x0002CD9C File Offset: 0x0002AF9C
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void RemoveMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegInt32"));
			}
			GC._RemoveMemoryPressure((ulong)bytesAllocated);
		}

		/// <summary>Returns the current generation number of the specified object.</summary>
		/// <param name="obj">The object that generation information is retrieved for.</param>
		/// <returns>The current generation number of <paramref name="obj" />.</returns>
		// Token: 0x06000EA0 RID: 3744
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetGeneration(object obj);

		/// <summary>Forces an immediate garbage collection from generation 0 through a specified generation.</summary>
		/// <param name="generation">The number of the oldest generation to be garbage collected.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is not valid.</exception>
		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002CDEF File Offset: 0x0002AFEF
		[__DynamicallyInvokable]
		public static void Collect(int generation)
		{
			GC.Collect(generation, GCCollectionMode.Default);
		}

		/// <summary>Forces an immediate garbage collection of all generations.</summary>
		// Token: 0x06000EA2 RID: 3746 RVA: 0x0002CDF8 File Offset: 0x0002AFF8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Collect()
		{
			GC._Collect(-1, 2);
		}

		/// <summary>Forces a garbage collection from generation 0 through a specified generation, at a time specified by a <see cref="T:System.GCCollectionMode" /> value.</summary>
		/// <param name="generation">The number of the oldest generation to be garbage collected.</param>
		/// <param name="mode">An enumeration value that specifies whether the garbage collection is forced (<see cref="F:System.GCCollectionMode.Default" /> or <see cref="F:System.GCCollectionMode.Forced" />) or optimized (<see cref="F:System.GCCollectionMode.Optimized" />).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is not valid.  
		/// -or-  
		/// <paramref name="mode" /> is not one of the <see cref="T:System.GCCollectionMode" /> values.</exception>
		// Token: 0x06000EA3 RID: 3747 RVA: 0x0002CE01 File Offset: 0x0002B001
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Collect(int generation, GCCollectionMode mode)
		{
			GC.Collect(generation, mode, true);
		}

		/// <summary>Forces a garbage collection from generation 0 through a specified generation, at a time specified by a <see cref="T:System.GCCollectionMode" /> value, with a value specifying whether the collection should be blocking.</summary>
		/// <param name="generation">The number of the oldest generation to be garbage collected.</param>
		/// <param name="mode">An enumeration value that specifies whether the garbage collection is forced (<see cref="F:System.GCCollectionMode.Default" /> or <see cref="F:System.GCCollectionMode.Forced" />) or optimized (<see cref="F:System.GCCollectionMode.Optimized" />).</param>
		/// <param name="blocking">
		///   <see langword="true" /> to perform a blocking garbage collection; <see langword="false" /> to perform a background garbage collection where possible.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is not valid.  
		/// -or-  
		/// <paramref name="mode" /> is not one of the <see cref="T:System.GCCollectionMode" /> values.</exception>
		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002CE0B File Offset: 0x0002B00B
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Collect(int generation, GCCollectionMode mode, bool blocking)
		{
			GC.Collect(generation, mode, blocking, false);
		}

		/// <summary>Forces a garbage collection from generation 0 through a specified generation, at a time specified by a <see cref="T:System.GCCollectionMode" /> value, with values that specify whether the collection should be blocking and compacting.</summary>
		/// <param name="generation">The number of the oldest generation to be garbage collected.</param>
		/// <param name="mode">An enumeration value that specifies whether the garbage collection is forced (<see cref="F:System.GCCollectionMode.Default" /> or <see cref="F:System.GCCollectionMode.Forced" />) or optimized (<see cref="F:System.GCCollectionMode.Optimized" />).</param>
		/// <param name="blocking">
		///   <see langword="true" /> to perform a blocking garbage collection; <see langword="false" /> to perform a background garbage collection where possible.</param>
		/// <param name="compacting">
		///   <see langword="true" /> to compact the small object heap; <see langword="false" /> to sweep only.</param>
		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002CE18 File Offset: 0x0002B018
		[SecuritySafeCritical]
		public static void Collect(int generation, GCCollectionMode mode, bool blocking, bool compacting)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (mode < GCCollectionMode.Default || mode > GCCollectionMode.Optimized)
			{
				throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			int num = 0;
			if (mode == GCCollectionMode.Optimized)
			{
				num |= 4;
			}
			if (compacting)
			{
				num |= 8;
			}
			if (blocking)
			{
				num |= 2;
			}
			else if (!compacting)
			{
				num |= 1;
			}
			GC._Collect(generation, num);
		}

		/// <summary>Returns the number of times garbage collection has occurred for the specified generation of objects.</summary>
		/// <param name="generation">The generation of objects for which the garbage collection count is to be determined.</param>
		/// <returns>The number of times garbage collection has occurred for the specified generation since the process was started.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is less than 0.</exception>
		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002CE7E File Offset: 0x0002B07E
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int CollectionCount(int generation)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			return GC._CollectionCount(generation, 0);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0002CEA0 File Offset: 0x0002B0A0
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static int CollectionCount(int generation, bool getSpecialGCCount)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			return GC._CollectionCount(generation, getSpecialGCCount ? 1 : 0);
		}

		/// <summary>References the specified object, which makes it ineligible for garbage collection from the start of the current routine to the point where this method is called.</summary>
		/// <param name="obj">The object to reference.</param>
		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002CEC8 File Offset: 0x0002B0C8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void KeepAlive(object obj)
		{
		}

		/// <summary>Returns the current generation number of the target of a specified weak reference.</summary>
		/// <param name="wo">A <see cref="T:System.WeakReference" /> that refers to the target object whose generation number is to be determined.</param>
		/// <returns>The current generation number of the target of <paramref name="wo" />.</returns>
		/// <exception cref="T:System.ArgumentException">Garbage collection has already been performed on <paramref name="wo" />.</exception>
		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002CECC File Offset: 0x0002B0CC
		[SecuritySafeCritical]
		public static int GetGeneration(WeakReference wo)
		{
			int generationWR = GC.GetGenerationWR(wo.m_handle);
			GC.KeepAlive(wo);
			return generationWR;
		}

		/// <summary>Gets the maximum number of generations that the system currently supports.</summary>
		/// <returns>A value that ranges from zero to the maximum number of supported generations.</returns>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0002CEEC File Offset: 0x0002B0EC
		[__DynamicallyInvokable]
		public static int MaxGeneration
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return GC.GetMaxGeneration();
			}
		}

		// Token: 0x06000EAB RID: 3755
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _WaitForPendingFinalizers();

		/// <summary>Suspends the current thread until the thread that is processing the queue of finalizers has emptied that queue.</summary>
		// Token: 0x06000EAC RID: 3756 RVA: 0x0002CEF3 File Offset: 0x0002B0F3
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void WaitForPendingFinalizers()
		{
			GC._WaitForPendingFinalizers();
		}

		// Token: 0x06000EAD RID: 3757
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _SuppressFinalize(object o);

		/// <summary>Requests that the common language runtime not call the finalizer for the specified object.</summary>
		/// <param name="obj">The object whose finalizer must not be executed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x06000EAE RID: 3758 RVA: 0x0002CEFA File Offset: 0x0002B0FA
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void SuppressFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._SuppressFinalize(obj);
		}

		// Token: 0x06000EAF RID: 3759
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _ReRegisterForFinalize(object o);

		/// <summary>Requests that the system call the finalizer for the specified object for which <see cref="M:System.GC.SuppressFinalize(System.Object)" /> has previously been called.</summary>
		/// <param name="obj">The object that a finalizer must be called for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x06000EB0 RID: 3760 RVA: 0x0002CF10 File Offset: 0x0002B110
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void ReRegisterForFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._ReRegisterForFinalize(obj);
		}

		/// <summary>Retrieves the number of bytes currently thought to be allocated. A parameter indicates whether this method can wait a short interval before returning, to allow the system to collect garbage and finalize objects.</summary>
		/// <param name="forceFullCollection">
		///   <see langword="true" /> to indicate that this method can wait for garbage collection to occur before returning; otherwise, <see langword="false" />.</param>
		/// <returns>A number that is the best available approximation of the number of bytes currently allocated in managed memory.</returns>
		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002CF28 File Offset: 0x0002B128
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static long GetTotalMemory(bool forceFullCollection)
		{
			long num = GC.GetTotalMemory();
			if (!forceFullCollection)
			{
				return num;
			}
			int num2 = 20;
			long num3 = num;
			float num4;
			do
			{
				GC.WaitForPendingFinalizers();
				GC.Collect();
				num = num3;
				num3 = GC.GetTotalMemory();
				num4 = (float)(num3 - num) / (float)num;
			}
			while (num2-- > 0 && (-0.05 >= (double)num4 || (double)num4 >= 0.05));
			return num3;
		}

		// Token: 0x06000EB2 RID: 3762
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long _GetAllocatedBytesForCurrentThread();

		/// <summary>Gets the total number of bytes allocated to the current thread since the beginning of its lifetime.</summary>
		/// <returns>The total number of bytes allocated to the current thread since the beginning of its lifetime.</returns>
		// Token: 0x06000EB3 RID: 3763 RVA: 0x0002CF82 File Offset: 0x0002B182
		[SecuritySafeCritical]
		public static long GetAllocatedBytesForCurrentThread()
		{
			return GC._GetAllocatedBytesForCurrentThread();
		}

		// Token: 0x06000EB4 RID: 3764
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _RegisterForFullGCNotification(int maxGenerationPercentage, int largeObjectHeapPercentage);

		// Token: 0x06000EB5 RID: 3765
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _CancelFullGCNotification();

		// Token: 0x06000EB6 RID: 3766
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _WaitForFullGCApproach(int millisecondsTimeout);

		// Token: 0x06000EB7 RID: 3767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _WaitForFullGCComplete(int millisecondsTimeout);

		/// <summary>Specifies that a garbage collection notification should be raised when conditions favor full garbage collection and when the collection has been completed.</summary>
		/// <param name="maxGenerationThreshold">A number between 1 and 99 that specifies when the notification should be raised based on the objects allocated in generation 2.</param>
		/// <param name="largeObjectHeapThreshold">A number between 1 and 99 that specifies when the notification should be raised based on objects allocated in the large object heap.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxGenerationThreshold" /> or <paramref name="largeObjectHeapThreshold" /> is not between 1 and 99.</exception>
		/// <exception cref="T:System.InvalidOperationException">This member is not available when concurrent garbage collection is enabled. See the &lt;gcConcurrent&gt; runtime setting for information about how to disable concurrent garbage collection.</exception>
		// Token: 0x06000EB8 RID: 3768 RVA: 0x0002CF8C File Offset: 0x0002B18C
		[SecurityCritical]
		public static void RegisterForFullGCNotification(int maxGenerationThreshold, int largeObjectHeapThreshold)
		{
			if (maxGenerationThreshold <= 0 || maxGenerationThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("maxGenerationThreshold", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), 1, 99));
			}
			if (largeObjectHeapThreshold <= 0 || largeObjectHeapThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("largeObjectHeapThreshold", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), 1, 99));
			}
			if (!GC._RegisterForFullGCNotification(maxGenerationThreshold, largeObjectHeapThreshold))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotWithConcurrentGC"));
			}
		}

		/// <summary>Cancels the registration of a garbage collection notification.</summary>
		/// <exception cref="T:System.InvalidOperationException">This member is not available when concurrent garbage collection is enabled. See the &lt;gcConcurrent&gt; runtime setting for information about how to disable concurrent garbage collection.</exception>
		// Token: 0x06000EB9 RID: 3769 RVA: 0x0002D01C File Offset: 0x0002B21C
		[SecurityCritical]
		public static void CancelFullGCNotification()
		{
			if (!GC._CancelFullGCNotification())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotWithConcurrentGC"));
			}
		}

		/// <summary>Returns the status of a registered notification for determining whether a full, blocking garbage collection by the common language runtime is imminent.</summary>
		/// <returns>The status of the registered garbage collection notification.</returns>
		// Token: 0x06000EBA RID: 3770 RVA: 0x0002D035 File Offset: 0x0002B235
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCApproach()
		{
			return (GCNotificationStatus)GC._WaitForFullGCApproach(-1);
		}

		/// <summary>Returns, in a specified time-out period, the status of a registered notification for determining whether a full, blocking garbage collection by the common language runtime is imminent.</summary>
		/// <param name="millisecondsTimeout">The length of time to wait before a notification status can be obtained. Specify -1 to wait indefinitely.</param>
		/// <returns>The status of the registered garbage collection notification.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> must be either non-negative or less than or equal to <see cref="F:System.Int32.MaxValue" /> or -1.</exception>
		// Token: 0x06000EBB RID: 3771 RVA: 0x0002D03D File Offset: 0x0002B23D
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return (GCNotificationStatus)GC._WaitForFullGCApproach(millisecondsTimeout);
		}

		/// <summary>Returns the status of a registered notification for determining whether a full, blocking garbage collection by the common language runtime has completed.</summary>
		/// <returns>The status of the registered garbage collection notification.</returns>
		// Token: 0x06000EBC RID: 3772 RVA: 0x0002D05E File Offset: 0x0002B25E
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCComplete()
		{
			return (GCNotificationStatus)GC._WaitForFullGCComplete(-1);
		}

		/// <summary>Returns, in a specified time-out period, the status of a registered notification for determining whether a full, blocking garbage collection by common language the runtime has completed.</summary>
		/// <param name="millisecondsTimeout">The length of time to wait before a notification status can be obtained. Specify -1 to wait indefinitely.</param>
		/// <returns>The status of the registered garbage collection notification.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="millisecondsTimeout" /> must be either non-negative or less than or equal to <see cref="F:System.Int32.MaxValue" /> or -1.</exception>
		// Token: 0x06000EBD RID: 3773 RVA: 0x0002D066 File Offset: 0x0002B266
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return (GCNotificationStatus)GC._WaitForFullGCComplete(millisecondsTimeout);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0002D088 File Offset: 0x0002B288
		[SecurityCritical]
		private static bool StartNoGCRegionWorker(long totalSize, bool hasLohSize, long lohSize, bool disallowFullBlockingGC)
		{
			GC.StartNoGCRegionStatus startNoGCRegionStatus = (GC.StartNoGCRegionStatus)GC._StartNoGCRegion(totalSize, hasLohSize, lohSize, disallowFullBlockingGC);
			if (startNoGCRegionStatus == GC.StartNoGCRegionStatus.AmountTooLarge)
			{
				throw new ArgumentOutOfRangeException("totalSize", "totalSize is too large. For more information about setting the maximum size, see \"Latency Modes\" in http://go.microsoft.com/fwlink/?LinkId=522706");
			}
			if (startNoGCRegionStatus == GC.StartNoGCRegionStatus.AlreadyInProgress)
			{
				throw new InvalidOperationException("The NoGCRegion mode was already in progress");
			}
			return startNoGCRegionStatus != GC.StartNoGCRegionStatus.NotEnoughMemory;
		}

		/// <summary>Attempts to disallow garbage collection during the execution of a critical path if a specified amount of memory is available.</summary>
		/// <param name="totalSize">The amount of memory in bytes to allocate without triggering a garbage collection. It must be less than or equal to the size of an ephemeral segment. For information on the size of an ephemeral segment, see the "Ephemeral generations and segments" section in the Fundamentals of Garbage Collection article.</param>
		/// <returns>
		///   <see langword="true" /> if the runtime was able to commit the required amount of memory and the garbage collector is able to enter no GC region latency mode; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalSize" /> exceeds the ephemeral segment size.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process is already in no GC region latency mode.</exception>
		// Token: 0x06000EBF RID: 3775 RVA: 0x0002D0C9 File Offset: 0x0002B2C9
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, false);
		}

		/// <summary>Attempts to disallow garbage collection during the execution of a critical path if a specified amount of memory is available for the large object heap and the small object heap.</summary>
		/// <param name="totalSize">The amount of memory in bytes to allocate without triggering a garbage collection. <paramref name="totalSize" /> -<paramref name="lohSize" /> must be less than or equal to the size of an ephemeral segment. For information on the size of an ephemeral segment, see the "Ephemeral generations and segments" section in the Fundamentals of Garbage Collection article.</param>
		/// <param name="lohSize">The number of bytes in <paramref name="totalSize" /> to use for large object heap (LOH) allocations.</param>
		/// <returns>
		///   <see langword="true" /> if the runtime was able to commit the required amount of memory and the garbage collector is able to enter no GC region latency mode; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalSize" /> - <paramref name="lohSize" /> exceeds the ephemeral segment size.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process is already in no GC region latency mode.</exception>
		// Token: 0x06000EC0 RID: 3776 RVA: 0x0002D0D5 File Offset: 0x0002B2D5
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, long lohSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, false);
		}

		/// <summary>Attempts to disallow garbage collection during the execution of a critical path if a specified amount of memory is available, and controls whether the garbage collector does a full blocking garbage collection if not enough memory is initially available.</summary>
		/// <param name="totalSize">The amount of memory in bytes to allocate without triggering a garbage collection. It must be less than or equal to the size of an ephemeral segment. For information on the size of an ephemeral segment, see the "Ephemeral generations and segments" section in the Fundamentals of Garbage Collection article.</param>
		/// <param name="disallowFullBlockingGC">
		///   <see langword="true" /> to omit a full blocking garbage collection if the garbage collector is initially unable to allocate <paramref name="totalSize" /> bytes; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the runtime was able to commit the required amount of memory and the garbage collector is able to enter no GC region latency mode; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalSize" /> exceeds the ephemeral segment size.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process is already in no GC region latency mode.</exception>
		// Token: 0x06000EC1 RID: 3777 RVA: 0x0002D0E0 File Offset: 0x0002B2E0
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, disallowFullBlockingGC);
		}

		/// <summary>Attempts to disallow garbage collection during the execution of a critical path if a specified amount of memory is available for the large object heap and the small object heap, and controls whether the garbage collector does a full blocking garbage collection if not enough memory is initially available.</summary>
		/// <param name="totalSize">The amount of memory in bytes to allocate without triggering a garbage collection. <paramref name="totalSize" /> -<paramref name="lohSize" /> must be less than or equal to the size of an ephemeral segment. For information on the size of an ephemeral segment, see the "Ephemeral generations and segments" section in the Fundamentals of Garbage Collection article.</param>
		/// <param name="lohSize">The number of bytes in <paramref name="totalSize" /> to use for large object heap (LOH) allocations.</param>
		/// <param name="disallowFullBlockingGC">
		///   <see langword="true" /> to omit a full blocking garbage collection if the garbage collector is initially unable to allocate the specified memory on the small object heap (SOH) and LOH; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the runtime was able to commit the required amount of memory and the garbage collector is able to enter no GC region latency mode; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalSize" /> - <paramref name="lohSize" /> exceeds the ephemeral segment size.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process is already in no GC region latency mode.</exception>
		// Token: 0x06000EC2 RID: 3778 RVA: 0x0002D0EC File Offset: 0x0002B2EC
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, long lohSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, disallowFullBlockingGC);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0002D0F8 File Offset: 0x0002B2F8
		[SecurityCritical]
		private static GC.EndNoGCRegionStatus EndNoGCRegionWorker()
		{
			GC.EndNoGCRegionStatus endNoGCRegionStatus = (GC.EndNoGCRegionStatus)GC._EndNoGCRegion();
			if (endNoGCRegionStatus == GC.EndNoGCRegionStatus.NotInProgress)
			{
				throw new InvalidOperationException("NoGCRegion mode must be set");
			}
			if (endNoGCRegionStatus == GC.EndNoGCRegionStatus.GCInduced)
			{
				throw new InvalidOperationException("Garbage collection was induced in NoGCRegion mode");
			}
			if (endNoGCRegionStatus == GC.EndNoGCRegionStatus.AllocationExceeded)
			{
				throw new InvalidOperationException("Allocated memory exceeds specified memory for NoGCRegion mode");
			}
			return GC.EndNoGCRegionStatus.Succeeded;
		}

		/// <summary>Ends the no GC region latency mode.</summary>
		/// <exception cref="T:System.InvalidOperationException">The garbage collector is not in no GC region latency mode.  
		///  -or-  
		///  The no GC region latency mode was ended previously because a garbage collection was induced.  
		///  -or-  
		///  A memory allocation exceeded the amount specified in the call to the <see cref="M:System.GC.TryStartNoGCRegion(System.Int64)" /> method.</exception>
		// Token: 0x06000EC4 RID: 3780 RVA: 0x0002D139 File Offset: 0x0002B339
		[SecurityCritical]
		public static void EndNoGCRegion()
		{
			GC.EndNoGCRegionWorker();
		}

		// Token: 0x02000AE6 RID: 2790
		private enum StartNoGCRegionStatus
		{
			// Token: 0x04003187 RID: 12679
			Succeeded,
			// Token: 0x04003188 RID: 12680
			NotEnoughMemory,
			// Token: 0x04003189 RID: 12681
			AmountTooLarge,
			// Token: 0x0400318A RID: 12682
			AlreadyInProgress
		}

		// Token: 0x02000AE7 RID: 2791
		private enum EndNoGCRegionStatus
		{
			// Token: 0x0400318C RID: 12684
			Succeeded,
			// Token: 0x0400318D RID: 12685
			NotInProgress,
			// Token: 0x0400318E RID: 12686
			GCInduced,
			// Token: 0x0400318F RID: 12687
			AllocationExceeded
		}
	}
}
