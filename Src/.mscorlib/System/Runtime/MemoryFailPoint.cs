using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Win32;

namespace System.Runtime
{
	/// <summary>Checks for sufficient memory resources before executing an operation. This class cannot be inherited.</summary>
	// Token: 0x02000712 RID: 1810
	public sealed class MemoryFailPoint : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06005134 RID: 20788 RVA: 0x0011F974 File Offset: 0x0011DB74
		// (set) Token: 0x06005135 RID: 20789 RVA: 0x0011F980 File Offset: 0x0011DB80
		private static long LastKnownFreeAddressSpace
		{
			get
			{
				return Volatile.Read(ref MemoryFailPoint.hiddenLastKnownFreeAddressSpace);
			}
			set
			{
				Volatile.Write(ref MemoryFailPoint.hiddenLastKnownFreeAddressSpace, value);
			}
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x0011F98D File Offset: 0x0011DB8D
		private static long AddToLastKnownFreeAddressSpace(long addend)
		{
			return Interlocked.Add(ref MemoryFailPoint.hiddenLastKnownFreeAddressSpace, addend);
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06005137 RID: 20791 RVA: 0x0011F99A File Offset: 0x0011DB9A
		// (set) Token: 0x06005138 RID: 20792 RVA: 0x0011F9A6 File Offset: 0x0011DBA6
		private static long LastTimeCheckingAddressSpace
		{
			get
			{
				return Volatile.Read(ref MemoryFailPoint.hiddenLastTimeCheckingAddressSpace);
			}
			set
			{
				Volatile.Write(ref MemoryFailPoint.hiddenLastTimeCheckingAddressSpace, value);
			}
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x0011F9B3 File Offset: 0x0011DBB3
		[SecuritySafeCritical]
		static MemoryFailPoint()
		{
			MemoryFailPoint.GetMemorySettings(out MemoryFailPoint.GCSegmentSize, out MemoryFailPoint.TopOfMemory);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.MemoryFailPoint" /> class, specifying the amount of memory required for successful execution.</summary>
		/// <param name="sizeInMegabytes">The required memory size, in megabytes. This must be a positive value.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified memory size is negative.</exception>
		/// <exception cref="T:System.InsufficientMemoryException">There is insufficient memory to begin execution of the code protected by the gate.</exception>
		// Token: 0x0600513A RID: 20794 RVA: 0x0011F9C4 File Offset: 0x0011DBC4
		[SecurityCritical]
		public unsafe MemoryFailPoint(int sizeInMegabytes)
		{
			if (sizeInMegabytes <= 0)
			{
				throw new ArgumentOutOfRangeException("sizeInMegabytes", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			ulong num = (ulong)((ulong)((long)sizeInMegabytes) << 20);
			this._reservedMemory = num;
			ulong num2 = (ulong)(Math.Ceiling(num / MemoryFailPoint.GCSegmentSize) * MemoryFailPoint.GCSegmentSize);
			if (num2 >= MemoryFailPoint.TopOfMemory)
			{
				throw new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint_TooBig"));
			}
			ulong num3 = (ulong)(Math.Ceiling((double)sizeInMegabytes / 16.0) * 16.0);
			num3 <<= 20;
			ulong num4 = 0UL;
			ulong num5 = 0UL;
			int i = 0;
			while (i < 3)
			{
				MemoryFailPoint.CheckForAvailableMemory(out num4, out num5);
				ulong memoryFailPointReservedMemory = SharedStatics.MemoryFailPointReservedMemory;
				ulong num6 = num2 + memoryFailPointReservedMemory;
				bool flag = num6 < num2 || num6 < memoryFailPointReservedMemory;
				bool flag2 = num4 < num3 + memoryFailPointReservedMemory + 16777216UL || flag;
				bool flag3 = num5 < num6 || flag;
				long num7 = (long)Environment.TickCount;
				if (num7 > MemoryFailPoint.LastTimeCheckingAddressSpace + 10000L || num7 < MemoryFailPoint.LastTimeCheckingAddressSpace || MemoryFailPoint.LastKnownFreeAddressSpace < (long)num2)
				{
					MemoryFailPoint.CheckForFreeAddressSpace(num2, false);
				}
				bool flag4 = MemoryFailPoint.LastKnownFreeAddressSpace < (long)num2;
				if (!flag2 && !flag3 && !flag4)
				{
					break;
				}
				switch (i)
				{
				case 0:
					GC.Collect();
					break;
				case 1:
					if (flag2)
					{
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
							break;
						}
						finally
						{
							UIntPtr uintPtr = new UIntPtr(num2);
							void* ptr = Win32Native.VirtualAlloc(null, uintPtr, 4096, 4);
							if (ptr != null && !Win32Native.VirtualFree(ptr, UIntPtr.Zero, 32768))
							{
								__Error.WinIOError();
							}
						}
						goto IL_183;
					}
					break;
				case 2:
					goto IL_183;
				}
				IL_1B6:
				i++;
				continue;
				IL_183:
				if (flag2 || flag3)
				{
					InsufficientMemoryException ex = new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint"));
					throw ex;
				}
				if (flag4)
				{
					InsufficientMemoryException ex2 = new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint_VAFrag"));
					throw ex2;
				}
				goto IL_1B6;
			}
			MemoryFailPoint.AddToLastKnownFreeAddressSpace((long)(-(long)num));
			if (MemoryFailPoint.LastKnownFreeAddressSpace < 0L)
			{
				MemoryFailPoint.CheckForFreeAddressSpace(num2, true);
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				SharedStatics.AddMemoryFailPointReservation((long)num);
				this._mustSubtractReservation = true;
			}
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x0011FBE0 File Offset: 0x0011DDE0
		[SecurityCritical]
		private static void CheckForAvailableMemory(out ulong availPageFile, out ulong totalAddressSpaceFree)
		{
			Win32Native.MEMORYSTATUSEX memorystatusex = default(Win32Native.MEMORYSTATUSEX);
			if (!Win32Native.GlobalMemoryStatusEx(ref memorystatusex))
			{
				__Error.WinIOError();
			}
			availPageFile = memorystatusex.availPageFile;
			totalAddressSpaceFree = memorystatusex.availVirtual;
		}

		// Token: 0x0600513C RID: 20796 RVA: 0x0011FC18 File Offset: 0x0011DE18
		[SecurityCritical]
		private static bool CheckForFreeAddressSpace(ulong size, bool shouldThrow)
		{
			ulong num = MemoryFailPoint.MemFreeAfterAddress(null, size);
			MemoryFailPoint.LastKnownFreeAddressSpace = (long)num;
			MemoryFailPoint.LastTimeCheckingAddressSpace = (long)Environment.TickCount;
			if (num < size && shouldThrow)
			{
				throw new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint_VAFrag"));
			}
			return num >= size;
		}

		// Token: 0x0600513D RID: 20797 RVA: 0x0011FC60 File Offset: 0x0011DE60
		[SecurityCritical]
		private unsafe static ulong MemFreeAfterAddress(void* address, ulong size)
		{
			if (size >= MemoryFailPoint.TopOfMemory)
			{
				return 0UL;
			}
			ulong num = 0UL;
			Win32Native.MEMORY_BASIC_INFORMATION memory_BASIC_INFORMATION = default(Win32Native.MEMORY_BASIC_INFORMATION);
			UIntPtr uintPtr = (UIntPtr)((ulong)((long)Marshal.SizeOf<Win32Native.MEMORY_BASIC_INFORMATION>(memory_BASIC_INFORMATION)));
			while ((byte*)address + size < MemoryFailPoint.TopOfMemory)
			{
				UIntPtr uintPtr2 = Win32Native.VirtualQuery(address, ref memory_BASIC_INFORMATION, uintPtr);
				if (uintPtr2 == UIntPtr.Zero)
				{
					__Error.WinIOError();
				}
				ulong num2 = memory_BASIC_INFORMATION.RegionSize.ToUInt64();
				if (memory_BASIC_INFORMATION.State == 65536U)
				{
					if (num2 >= size)
					{
						return num2;
					}
					num = Math.Max(num, num2);
				}
				address = (void*)((byte*)address + num2);
			}
			return num;
		}

		// Token: 0x0600513E RID: 20798
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMemorySettings(out ulong maxGCSegmentSize, out ulong topOfMemory);

		/// <summary>Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the <see cref="T:System.Runtime.MemoryFailPoint" /> object.</summary>
		// Token: 0x0600513F RID: 20799 RVA: 0x0011FCF0 File Offset: 0x0011DEF0
		[SecuritySafeCritical]
		~MemoryFailPoint()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.MemoryFailPoint" />.</summary>
		// Token: 0x06005140 RID: 20800 RVA: 0x0011FD20 File Offset: 0x0011DF20
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x0011FD30 File Offset: 0x0011DF30
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Dispose(bool disposing)
		{
			if (this._mustSubtractReservation)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					SharedStatics.AddMemoryFailPointReservation((long)(-(long)this._reservedMemory));
					this._mustSubtractReservation = false;
				}
			}
		}

		// Token: 0x040023F1 RID: 9201
		private static readonly ulong TopOfMemory;

		// Token: 0x040023F2 RID: 9202
		private static long hiddenLastKnownFreeAddressSpace;

		// Token: 0x040023F3 RID: 9203
		private static long hiddenLastTimeCheckingAddressSpace;

		// Token: 0x040023F4 RID: 9204
		private const int CheckThreshold = 10000;

		// Token: 0x040023F5 RID: 9205
		private const int LowMemoryFudgeFactor = 16777216;

		// Token: 0x040023F6 RID: 9206
		private const int MemoryCheckGranularity = 16;

		// Token: 0x040023F7 RID: 9207
		private static readonly ulong GCSegmentSize;

		// Token: 0x040023F8 RID: 9208
		private ulong _reservedMemory;

		// Token: 0x040023F9 RID: 9209
		private bool _mustSubtractReservation;
	}
}
