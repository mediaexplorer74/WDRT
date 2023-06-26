using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
	/// <summary>Contains methods for performing volatile memory operations.</summary>
	// Token: 0x02000530 RID: 1328
	[__DynamicallyInvokable]
	public static class Volatile
	{
		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E7B RID: 15995 RVA: 0x000EA180 File Offset: 0x000E8380
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static bool Read(ref bool location)
		{
			bool flag = location;
			Thread.MemoryBarrier();
			return flag;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E7C RID: 15996 RVA: 0x000EA198 File Offset: 0x000E8398
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Read(ref sbyte location)
		{
			sbyte b = location;
			Thread.MemoryBarrier();
			return b;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E7D RID: 15997 RVA: 0x000EA1B0 File Offset: 0x000E83B0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static byte Read(ref byte location)
		{
			byte b = location;
			Thread.MemoryBarrier();
			return b;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E7E RID: 15998 RVA: 0x000EA1C8 File Offset: 0x000E83C8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static short Read(ref short location)
		{
			short num = location;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E7F RID: 15999 RVA: 0x000EA1E0 File Offset: 0x000E83E0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort Read(ref ushort location)
		{
			ushort num = location;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E80 RID: 16000 RVA: 0x000EA1F8 File Offset: 0x000E83F8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int Read(ref int location)
		{
			int num = location;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E81 RID: 16001 RVA: 0x000EA210 File Offset: 0x000E8410
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint Read(ref uint location)
		{
			uint num = location;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E82 RID: 16002 RVA: 0x000EA226 File Offset: 0x000E8426
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static long Read(ref long location)
		{
			return Interlocked.CompareExchange(ref location, 0L, 0L);
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E83 RID: 16003 RVA: 0x000EA234 File Offset: 0x000E8434
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static ulong Read(ref ulong location)
		{
			fixed (ulong* ptr = &location)
			{
				ulong* ptr2 = ptr;
				return (ulong)Interlocked.CompareExchange(ref *(long*)ptr2, 0L, 0L);
			}
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E84 RID: 16004 RVA: 0x000EA250 File Offset: 0x000E8450
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr Read(ref IntPtr location)
		{
			IntPtr intPtr = location;
			Thread.MemoryBarrier();
			return intPtr;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E85 RID: 16005 RVA: 0x000EA268 File Offset: 0x000E8468
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public static UIntPtr Read(ref UIntPtr location)
		{
			UIntPtr uintPtr = location;
			Thread.MemoryBarrier();
			return uintPtr;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E86 RID: 16006 RVA: 0x000EA280 File Offset: 0x000E8480
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static float Read(ref float location)
		{
			float num = location;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E87 RID: 16007 RVA: 0x000EA296 File Offset: 0x000E8496
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static double Read(ref double location)
		{
			return Interlocked.CompareExchange(ref location, 0.0, 0.0);
		}

		/// <summary>Reads the object reference from the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <typeparam name="T">The type of field to read. This must be a reference type, not a value type.</typeparam>
		/// <returns>The reference to <paramref name="T" /> that was read. This reference is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06003E88 RID: 16008 RVA: 0x000EA2B0 File Offset: 0x000E84B0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static T Read<T>(ref T location) where T : class
		{
			T t = location;
			Thread.MemoryBarrier();
			return t;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E89 RID: 16009 RVA: 0x000EA2CA File Offset: 0x000E84CA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref bool location, bool value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E8A RID: 16010 RVA: 0x000EA2D4 File Offset: 0x000E84D4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static void Write(ref sbyte location, sbyte value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E8B RID: 16011 RVA: 0x000EA2DE File Offset: 0x000E84DE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref byte location, byte value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E8C RID: 16012 RVA: 0x000EA2E8 File Offset: 0x000E84E8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref short location, short value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E8D RID: 16013 RVA: 0x000EA2F2 File Offset: 0x000E84F2
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static void Write(ref ushort location, ushort value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E8E RID: 16014 RVA: 0x000EA2FC File Offset: 0x000E84FC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref int location, int value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E8F RID: 16015 RVA: 0x000EA306 File Offset: 0x000E8506
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static void Write(ref uint location, uint value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a memory operation appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E90 RID: 16016 RVA: 0x000EA310 File Offset: 0x000E8510
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref long location, long value)
		{
			Interlocked.Exchange(ref location, value);
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E91 RID: 16017 RVA: 0x000EA31C File Offset: 0x000E851C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static void Write(ref ulong location, ulong value)
		{
			fixed (ulong* ptr = &location)
			{
				ulong* ptr2 = ptr;
				Interlocked.Exchange(ref *(long*)ptr2, (long)value);
			}
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E92 RID: 16018 RVA: 0x000EA339 File Offset: 0x000E8539
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void Write(ref IntPtr location, IntPtr value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E93 RID: 16019 RVA: 0x000EA343 File Offset: 0x000E8543
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public static void Write(ref UIntPtr location, UIntPtr value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E94 RID: 16020 RVA: 0x000EA34D File Offset: 0x000E854D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref float location, float value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003E95 RID: 16021 RVA: 0x000EA357 File Offset: 0x000E8557
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref double location, double value)
		{
			Interlocked.Exchange(ref location, value);
		}

		/// <summary>Writes the specified object reference to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the object reference is written.</param>
		/// <param name="value">The object reference to write. The reference is written immediately so that it is visible to all processors in the computer.</param>
		/// <typeparam name="T">The type of field to write. This must be a reference type, not a value type.</typeparam>
		// Token: 0x06003E96 RID: 16022 RVA: 0x000EA361 File Offset: 0x000E8561
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Write<T>(ref T location, T value) where T : class
		{
			Thread.MemoryBarrier();
			location = value;
		}
	}
}
