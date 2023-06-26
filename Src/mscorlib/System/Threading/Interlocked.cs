using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	/// <summary>Provides atomic operations for variables that are shared by multiple threads.</summary>
	// Token: 0x020004F7 RID: 1271
	[__DynamicallyInvokable]
	public static class Interlocked
	{
		/// <summary>Increments a specified variable and stores the result, as an atomic operation.</summary>
		/// <param name="location">The variable whose value is to be incremented.</param>
		/// <returns>The incremented value.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location" /> is a null pointer.</exception>
		// Token: 0x06003C37 RID: 15415 RVA: 0x000E565B File Offset: 0x000E385B
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int Increment(ref int location)
		{
			return Interlocked.Add(ref location, 1);
		}

		/// <summary>Increments a specified variable and stores the result, as an atomic operation.</summary>
		/// <param name="location">The variable whose value is to be incremented.</param>
		/// <returns>The incremented value.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location" /> is a null pointer.</exception>
		// Token: 0x06003C38 RID: 15416 RVA: 0x000E5664 File Offset: 0x000E3864
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static long Increment(ref long location)
		{
			return Interlocked.Add(ref location, 1L);
		}

		/// <summary>Decrements a specified variable and stores the result, as an atomic operation.</summary>
		/// <param name="location">The variable whose value is to be decremented.</param>
		/// <returns>The decremented value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location" /> is a null pointer.</exception>
		// Token: 0x06003C39 RID: 15417 RVA: 0x000E566E File Offset: 0x000E386E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int Decrement(ref int location)
		{
			return Interlocked.Add(ref location, -1);
		}

		/// <summary>Decrements the specified variable and stores the result, as an atomic operation.</summary>
		/// <param name="location">The variable whose value is to be decremented.</param>
		/// <returns>The decremented value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location" /> is a null pointer.</exception>
		// Token: 0x06003C3A RID: 15418 RVA: 0x000E5677 File Offset: 0x000E3877
		[__DynamicallyInvokable]
		public static long Decrement(ref long location)
		{
			return Interlocked.Add(ref location, -1L);
		}

		/// <summary>Sets a 32-bit signed integer to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C3B RID: 15419
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Exchange(ref int location1, int value);

		/// <summary>Sets a 64-bit signed integer to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C3C RID: 15420
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Exchange(ref long location1, long value);

		/// <summary>Sets a single-precision floating point number to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C3D RID: 15421
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Exchange(ref float location1, float value);

		/// <summary>Sets a double-precision floating point number to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C3E RID: 15422
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Exchange(ref double location1, double value);

		/// <summary>Sets an object to a specified value and returns a reference to the original object, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C3F RID: 15423
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object Exchange(ref object location1, object value);

		/// <summary>Sets a platform-specific handle or pointer to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C40 RID: 15424
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Exchange(ref IntPtr location1, IntPtr value);

		/// <summary>Sets a variable of the specified type <paramref name="T" /> to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value. This is a reference parameter (<see langword="ref" /> in C#, <see langword="ByRef" /> in Visual Basic).</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <typeparam name="T">The type to be used for <paramref name="location1" /> and <paramref name="value" />. This type must be a reference type.</typeparam>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C41 RID: 15425 RVA: 0x000E5681 File Offset: 0x000E3881
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[ComVisible(false)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static T Exchange<T>(ref T location1, T value) where T : class
		{
			Interlocked._Exchange(__makeref(location1), __makeref(value));
			return value;
		}

		// Token: 0x06003C42 RID: 15426
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _Exchange(TypedReference location1, TypedReference value);

		/// <summary>Compares two 32-bit signed integers for equality and, if they are equal, replaces the first value.</summary>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced.</param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality.</param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C43 RID: 15427
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CompareExchange(ref int location1, int value, int comparand);

		/// <summary>Compares two 64-bit signed integers for equality and, if they are equal, replaces the first value.</summary>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced.</param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality.</param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C44 RID: 15428
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CompareExchange(ref long location1, long value, long comparand);

		/// <summary>Compares two single-precision floating point numbers for equality and, if they are equal, replaces the first value.</summary>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced.</param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality.</param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C45 RID: 15429
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CompareExchange(ref float location1, float value, float comparand);

		/// <summary>Compares two double-precision floating point numbers for equality and, if they are equal, replaces the first value.</summary>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced.</param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality.</param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C46 RID: 15430
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CompareExchange(ref double location1, double value, double comparand);

		/// <summary>Compares two objects for reference equality and, if they are equal, replaces the first object.</summary>
		/// <param name="location1">The destination object that is compared by reference with <paramref name="comparand" /> and possibly replaced.</param>
		/// <param name="value">The object that replaces the destination object if the reference comparison results in equality.</param>
		/// <param name="comparand">The object that is compared by reference to the object at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C47 RID: 15431
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object CompareExchange(ref object location1, object value, object comparand);

		/// <summary>Compares two platform-specific handles or pointers for equality and, if they are equal, replaces the first one.</summary>
		/// <param name="location1">The destination <see cref="T:System.IntPtr" />, whose value is compared with the value of <paramref name="comparand" /> and possibly replaced by <paramref name="value" />.</param>
		/// <param name="value">The <see cref="T:System.IntPtr" /> that replaces the destination value if the comparison results in equality.</param>
		/// <param name="comparand">The <see cref="T:System.IntPtr" /> that is compared to the value at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C48 RID: 15432
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CompareExchange(ref IntPtr location1, IntPtr value, IntPtr comparand);

		/// <summary>Compares two instances of the specified reference type <paramref name="T" /> for reference equality and, if they are equal, replaces the first one.</summary>
		/// <param name="location1">The destination, whose value is compared by reference with <paramref name="comparand" /> and possibly replaced. This is a reference parameter (<see langword="ref" /> in C#, <see langword="ByRef" /> in Visual Basic).</param>
		/// <param name="value">The value that replaces the destination value if the comparison by reference results in equality.</param>
		/// <param name="comparand">The value that is compared by reference to the value at <paramref name="location1" />.</param>
		/// <typeparam name="T">The type to be used for <paramref name="location1" />, <paramref name="value" />, and <paramref name="comparand" />. This type must be a reference type.</typeparam>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C49 RID: 15433 RVA: 0x000E5696 File Offset: 0x000E3896
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[ComVisible(false)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static T CompareExchange<T>(ref T location1, T value, T comparand) where T : class
		{
			Interlocked._CompareExchange(__makeref(location1), __makeref(value), comparand);
			return value;
		}

		// Token: 0x06003C4A RID: 15434
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _CompareExchange(TypedReference location1, TypedReference value, object comparand);

		// Token: 0x06003C4B RID: 15435
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int CompareExchange(ref int location1, int value, int comparand, ref bool succeeded);

		// Token: 0x06003C4C RID: 15436
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int ExchangeAdd(ref int location1, int value);

		// Token: 0x06003C4D RID: 15437
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long ExchangeAdd(ref long location1, long value);

		/// <summary>Adds two 32-bit integers and replaces the first integer with the sum, as an atomic operation.</summary>
		/// <param name="location1">A variable containing the first value to be added. The sum of the two values is stored in <paramref name="location1" />.</param>
		/// <param name="value">The value to be added to the integer at <paramref name="location1" />.</param>
		/// <returns>The new value stored at <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C4E RID: 15438 RVA: 0x000E56B1 File Offset: 0x000E38B1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int Add(ref int location1, int value)
		{
			return Interlocked.ExchangeAdd(ref location1, value) + value;
		}

		/// <summary>Adds two 64-bit integers and replaces the first integer with the sum, as an atomic operation.</summary>
		/// <param name="location1">A variable containing the first value to be added. The sum of the two values is stored in <paramref name="location1" />.</param>
		/// <param name="value">The value to be added to the integer at <paramref name="location1" />.</param>
		/// <returns>The new value stored at <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x06003C4F RID: 15439 RVA: 0x000E56BC File Offset: 0x000E38BC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static long Add(ref long location1, long value)
		{
			return Interlocked.ExchangeAdd(ref location1, value) + value;
		}

		/// <summary>Returns a 64-bit value, loaded as an atomic operation.</summary>
		/// <param name="location">The 64-bit value to be loaded.</param>
		/// <returns>The loaded value.</returns>
		// Token: 0x06003C50 RID: 15440 RVA: 0x000E56C7 File Offset: 0x000E38C7
		[__DynamicallyInvokable]
		public static long Read(ref long location)
		{
			return Interlocked.CompareExchange(ref location, 0L, 0L);
		}

		/// <summary>Synchronizes memory access as follows: The processor that executes the current thread cannot reorder instructions in such a way that memory accesses before the call to <see cref="M:System.Threading.Interlocked.MemoryBarrier" /> execute after memory accesses that follow the call to <see cref="M:System.Threading.Interlocked.MemoryBarrier" />.</summary>
		// Token: 0x06003C51 RID: 15441 RVA: 0x000E56D3 File Offset: 0x000E38D3
		[__DynamicallyInvokable]
		public static void MemoryBarrier()
		{
			Thread.MemoryBarrier();
		}

		/// <summary>Defines a memory fence that blocks speculative execution past this point until pending reads and writes are complete.</summary>      
		// Token: 0x06003C52 RID: 15442
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SpeculationBarrier();
	}
}
