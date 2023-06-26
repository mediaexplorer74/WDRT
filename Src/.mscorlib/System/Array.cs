using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;

namespace System
{
	/// <summary>Provides methods for creating, manipulating, searching, and sorting arrays, thereby serving as the base class for all arrays in the common language runtime.</summary>
	// Token: 0x02000055 RID: 85
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Array : ICloneable, IList, ICollection, IEnumerable, IStructuralComparable, IStructuralEquatable
	{
		// Token: 0x0600028B RID: 651 RVA: 0x00005CF4 File Offset: 0x00003EF4
		internal Array()
		{
		}

		/// <summary>Returns a read-only wrapper for the specified array.</summary>
		/// <param name="array">The one-dimensional, zero-based array to wrap in a read-only <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> wrapper.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>A read-only <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> wrapper for the specified array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		// Token: 0x0600028C RID: 652 RVA: 0x00005CFC File Offset: 0x00003EFC
		public static ReadOnlyCollection<T> AsReadOnly<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return new ReadOnlyCollection<T>(array);
		}

		/// <summary>Changes the number of elements of a one-dimensional array to the specified new size.</summary>
		/// <param name="array">The one-dimensional, zero-based array to resize, or <see langword="null" /> to create a new array with the specified size.</param>
		/// <param name="newSize">The size of the new array.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="newSize" /> is less than zero.</exception>
		// Token: 0x0600028D RID: 653 RVA: 0x00005D14 File Offset: 0x00003F14
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Resize<T>(ref T[] array, int newSize)
		{
			if (newSize < 0)
			{
				throw new ArgumentOutOfRangeException("newSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			T[] array2 = array;
			if (array2 == null)
			{
				array = new T[newSize];
				return;
			}
			if (array2.Length != newSize)
			{
				T[] array3 = new T[newSize];
				Array.Copy(array2, 0, array3, 0, (array2.Length > newSize) ? newSize : array2.Length);
				array = array3;
			}
		}

		/// <summary>Creates a one-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and length, with zero-based indexing.</summary>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length">The size of the <see cref="T:System.Array" /> to create.</param>
		/// <returns>A new one-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length, using zero-based indexing.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported.  
		/// -or-  
		/// <paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero.</exception>
		// Token: 0x0600028E RID: 654 RVA: 0x00005D70 File Offset: 0x00003F70
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static Array CreateInstance(Type elementType, int length)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 1, &length, null);
		}

		/// <summary>Creates a two-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and dimension lengths, with zero-based indexing.</summary>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length1">The size of the first dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length2">The size of the second dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <returns>A new two-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length for each dimension, using zero-based indexing.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported.  
		/// -or-  
		/// <paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length1" /> is less than zero.  
		/// -or-  
		/// <paramref name="length2" /> is less than zero.</exception>
		// Token: 0x0600028F RID: 655 RVA: 0x00005DEC File Offset: 0x00003FEC
		[SecuritySafeCritical]
		public unsafe static Array CreateInstance(Type elementType, int length1, int length2)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (length1 < 0 || length2 < 0)
			{
				throw new ArgumentOutOfRangeException((length1 < 0) ? "length1" : "length2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = length1;
			ptr[1] = length2;
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 2, ptr, null);
		}

		/// <summary>Creates a three-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and dimension lengths, with zero-based indexing.</summary>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length1">The size of the first dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length2">The size of the second dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length3">The size of the third dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <returns>A new three-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length for each dimension, using zero-based indexing.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported.  
		/// -or-  
		/// <paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length1" /> is less than zero.  
		/// -or-  
		/// <paramref name="length2" /> is less than zero.  
		/// -or-  
		/// <paramref name="length3" /> is less than zero.</exception>
		// Token: 0x06000290 RID: 656 RVA: 0x00005E84 File Offset: 0x00004084
		[SecuritySafeCritical]
		public unsafe static Array CreateInstance(Type elementType, int length1, int length2, int length3)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (length1 < 0)
			{
				throw new ArgumentOutOfRangeException("length1", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (length2 < 0)
			{
				throw new ArgumentOutOfRangeException("length2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (length3 < 0)
			{
				throw new ArgumentOutOfRangeException("length3", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			int* ptr = stackalloc int[(UIntPtr)12];
			*ptr = length1;
			ptr[1] = length2;
			ptr[2] = length3;
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 3, ptr, null);
		}

		/// <summary>Creates a multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and dimension lengths, with zero-based indexing. The dimension lengths are specified in an array of 32-bit integers.</summary>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="lengths">An array of 32-bit integers that represent the size of each dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <returns>A new multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length for each dimension, using zero-based indexing.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="lengths" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.  
		/// -or-  
		/// The <paramref name="lengths" /> array contains less than one element.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported.  
		/// -or-  
		/// <paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Any value in <paramref name="lengths" /> is less than zero.</exception>
		// Token: 0x06000291 RID: 657 RVA: 0x00005F48 File Offset: 0x00004148
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static Array CreateInstance(Type elementType, params int[] lengths)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lengths.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			for (int i = 0; i < lengths.Length; i++)
			{
				if (lengths[i] < 0)
				{
					throw new ArgumentOutOfRangeException("lengths[" + i.ToString() + "]", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
			}
			int* ptr;
			if (lengths == null || lengths.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &lengths[0];
			}
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, lengths.Length, ptr, null);
		}

		/// <summary>Creates a multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and dimension lengths, with zero-based indexing. The dimension lengths are specified in an array of 64-bit integers.</summary>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="lengths">An array of 64-bit integers that represent the size of each dimension of the <see cref="T:System.Array" /> to create. Each integer in the array must be between zero and <see cref="F:System.Int32.MaxValue" />, inclusive.</param>
		/// <returns>A new multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length for each dimension, using zero-based indexing.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="lengths" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.  
		/// -or-  
		/// The <paramref name="lengths" /> array contains less than one element.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported.  
		/// -or-  
		/// <paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Any value in <paramref name="lengths" /> is less than zero or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000292 RID: 658 RVA: 0x00006020 File Offset: 0x00004220
		public static Array CreateInstance(Type elementType, params long[] lengths)
		{
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lengths.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
			}
			int[] array = new int[lengths.Length];
			for (int i = 0; i < lengths.Length; i++)
			{
				long num = lengths[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("len", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
				}
				array[i] = (int)num;
			}
			return Array.CreateInstance(elementType, array);
		}

		/// <summary>Creates a multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and dimension lengths, with the specified lower bounds.</summary>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="lengths">A one-dimensional array that contains the size of each dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="lowerBounds">A one-dimensional array that contains the lower bound (starting index) of each dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <returns>A new multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length and lower bound for each dimension.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="lengths" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="lowerBounds" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.  
		/// -or-  
		/// The <paramref name="lengths" /> array contains less than one element.  
		/// -or-  
		/// The <paramref name="lengths" /> and <paramref name="lowerBounds" /> arrays do not contain the same number of elements.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported.  
		/// -or-  
		/// <paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Any value in <paramref name="lengths" /> is less than zero.  
		///  -or-  
		///  Any value in <paramref name="lowerBounds" /> is very large, such that the sum of a dimension's lower bound and length is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000293 RID: 659 RVA: 0x000060A0 File Offset: 0x000042A0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static Array CreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lowerBounds == null)
			{
				throw new ArgumentNullException("lowerBounds");
			}
			if (lengths.Length != lowerBounds.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RanksAndBounds"));
			}
			if (lengths.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			for (int i = 0; i < lengths.Length; i++)
			{
				if (lengths[i] < 0)
				{
					throw new ArgumentOutOfRangeException("lengths[" + i.ToString() + "]", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
			}
			int* ptr;
			if (lengths == null || lengths.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &lengths[0];
			}
			int* ptr2;
			if (lowerBounds == null || lowerBounds.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &lowerBounds[0];
			}
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, lengths.Length, ptr, ptr2);
		}

		// Token: 0x06000294 RID: 660
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Array InternalCreate(void* elementType, int rank, int* pLengths, int* pLowerBounds);

		// Token: 0x06000295 RID: 661 RVA: 0x000061C1 File Offset: 0x000043C1
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, int length)
		{
			return Array.CreateInstance(elementType, length);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000061CA File Offset: 0x000043CA
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, int length1, int length2)
		{
			return Array.CreateInstance(elementType, length1, length2);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000061D4 File Offset: 0x000043D4
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, params int[] lengths)
		{
			return Array.CreateInstance(elementType, lengths);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000061DD File Offset: 0x000043DD
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			return Array.CreateInstance(elementType, lengths, lowerBounds);
		}

		/// <summary>Copies a range of elements from an <see cref="T:System.Array" /> starting at the first element and pastes them into another <see cref="T:System.Array" /> starting at the first element. The length is specified as a 32-bit integer.</summary>
		/// <param name="sourceArray">The <see cref="T:System.Array" /> that contains the data to copy.</param>
		/// <param name="destinationArray">The <see cref="T:System.Array" /> that receives the data.</param>
		/// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceArray" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="destinationArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> have different ranks.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> are of incompatible types.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in <paramref name="sourceArray" /> cannot be cast to the type of <paramref name="destinationArray" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="length" /> is greater than the number of elements in <paramref name="sourceArray" />.  
		/// -or-  
		/// <paramref name="length" /> is greater than the number of elements in <paramref name="destinationArray" />.</exception>
		// Token: 0x06000299 RID: 665 RVA: 0x000061E7 File Offset: 0x000043E7
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Copy(Array sourceArray, Array destinationArray, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			Array.Copy(sourceArray, sourceArray.GetLowerBound(0), destinationArray, destinationArray.GetLowerBound(0), length, false);
		}

		/// <summary>Copies a range of elements from an <see cref="T:System.Array" /> starting at the specified source index and pastes them to another <see cref="T:System.Array" /> starting at the specified destination index. The length and the indexes are specified as 32-bit integers.</summary>
		/// <param name="sourceArray">The <see cref="T:System.Array" /> that contains the data to copy.</param>
		/// <param name="sourceIndex">A 32-bit integer that represents the index in the <paramref name="sourceArray" /> at which copying begins.</param>
		/// <param name="destinationArray">The <see cref="T:System.Array" /> that receives the data.</param>
		/// <param name="destinationIndex">A 32-bit integer that represents the index in the <paramref name="destinationArray" /> at which storing begins.</param>
		/// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceArray" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="destinationArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> have different ranks.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> are of incompatible types.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in <paramref name="sourceArray" /> cannot be cast to the type of <paramref name="destinationArray" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" /> is less than the lower bound of the first dimension of <paramref name="sourceArray" />.  
		/// -or-  
		/// <paramref name="destinationIndex" /> is less than the lower bound of the first dimension of <paramref name="destinationArray" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="length" /> is greater than the number of elements from <paramref name="sourceIndex" /> to the end of <paramref name="sourceArray" />.  
		/// -or-  
		/// <paramref name="length" /> is greater than the number of elements from <paramref name="destinationIndex" /> to the end of <paramref name="destinationArray" />.</exception>
		// Token: 0x0600029A RID: 666 RVA: 0x0000621C File Offset: 0x0000441C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length, false);
		}

		// Token: 0x0600029B RID: 667
		[SecurityCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length, bool reliable);

		/// <summary>Copies a range of elements from an <see cref="T:System.Array" /> starting at the specified source index and pastes them to another <see cref="T:System.Array" /> starting at the specified destination index.  Guarantees that all changes are undone if the copy does not succeed completely.</summary>
		/// <param name="sourceArray">The <see cref="T:System.Array" /> that contains the data to copy.</param>
		/// <param name="sourceIndex">A 32-bit integer that represents the index in the <paramref name="sourceArray" /> at which copying begins.</param>
		/// <param name="destinationArray">The <see cref="T:System.Array" /> that receives the data.</param>
		/// <param name="destinationIndex">A 32-bit integer that represents the index in the <paramref name="destinationArray" /> at which storing begins.</param>
		/// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceArray" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="destinationArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> have different ranks.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The <paramref name="sourceArray" /> type is neither the same as nor derived from the <paramref name="destinationArray" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in <paramref name="sourceArray" /> cannot be cast to the type of <paramref name="destinationArray" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" /> is less than the lower bound of the first dimension of <paramref name="sourceArray" />.  
		/// -or-  
		/// <paramref name="destinationIndex" /> is less than the lower bound of the first dimension of <paramref name="destinationArray" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="length" /> is greater than the number of elements from <paramref name="sourceIndex" /> to the end of <paramref name="sourceArray" />.  
		/// -or-  
		/// <paramref name="length" /> is greater than the number of elements from <paramref name="destinationIndex" /> to the end of <paramref name="destinationArray" />.</exception>
		// Token: 0x0600029C RID: 668 RVA: 0x0000622A File Offset: 0x0000442A
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void ConstrainedCopy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length, true);
		}

		/// <summary>Copies a range of elements from an <see cref="T:System.Array" /> starting at the first element and pastes them into another <see cref="T:System.Array" /> starting at the first element. The length is specified as a 64-bit integer.</summary>
		/// <param name="sourceArray">The <see cref="T:System.Array" /> that contains the data to copy.</param>
		/// <param name="destinationArray">The <see cref="T:System.Array" /> that receives the data.</param>
		/// <param name="length">A 64-bit integer that represents the number of elements to copy. The integer must be between zero and <see cref="F:System.Int32.MaxValue" />, inclusive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceArray" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="destinationArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> have different ranks.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> are of incompatible types.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in <paramref name="sourceArray" /> cannot be cast to the type of <paramref name="destinationArray" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="length" /> is greater than the number of elements in <paramref name="sourceArray" />.  
		/// -or-  
		/// <paramref name="length" /> is greater than the number of elements in <paramref name="destinationArray" />.</exception>
		// Token: 0x0600029D RID: 669 RVA: 0x00006238 File Offset: 0x00004438
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, Array destinationArray, long length)
		{
			if (length > 2147483647L || length < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			Array.Copy(sourceArray, destinationArray, (int)length);
		}

		/// <summary>Copies a range of elements from an <see cref="T:System.Array" /> starting at the specified source index and pastes them to another <see cref="T:System.Array" /> starting at the specified destination index. The length and the indexes are specified as 64-bit integers.</summary>
		/// <param name="sourceArray">The <see cref="T:System.Array" /> that contains the data to copy.</param>
		/// <param name="sourceIndex">A 64-bit integer that represents the index in the <paramref name="sourceArray" /> at which copying begins.</param>
		/// <param name="destinationArray">The <see cref="T:System.Array" /> that receives the data.</param>
		/// <param name="destinationIndex">A 64-bit integer that represents the index in the <paramref name="destinationArray" /> at which storing begins.</param>
		/// <param name="length">A 64-bit integer that represents the number of elements to copy. The integer must be between zero and <see cref="F:System.Int32.MaxValue" />, inclusive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceArray" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="destinationArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> have different ranks.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> are of incompatible types.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in <paramref name="sourceArray" /> cannot be cast to the type of <paramref name="destinationArray" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" /> is outside the range of valid indexes for the <paramref name="sourceArray" />.  
		/// -or-  
		/// <paramref name="destinationIndex" /> is outside the range of valid indexes for the <paramref name="destinationArray" />.  
		/// -or-  
		/// <paramref name="length" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="length" /> is greater than the number of elements from <paramref name="sourceIndex" /> to the end of <paramref name="sourceArray" />.  
		/// -or-  
		/// <paramref name="length" /> is greater than the number of elements from <paramref name="destinationIndex" /> to the end of <paramref name="destinationArray" />.</exception>
		// Token: 0x0600029E RID: 670 RVA: 0x0000626C File Offset: 0x0000446C
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
		{
			if (sourceIndex > 2147483647L || sourceIndex < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (destinationIndex > 2147483647L || destinationIndex < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (length > 2147483647L || length < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			Array.Copy(sourceArray, (int)sourceIndex, destinationArray, (int)destinationIndex, (int)length);
		}

		/// <summary>Sets a range of elements in an array to the default value of each element type.</summary>
		/// <param name="array">The array whose elements need to be cleared.</param>
		/// <param name="index">The starting index of the range of elements to clear.</param>
		/// <param name="length">The number of elements to clear.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.  
		/// -or-  
		/// The sum of <paramref name="index" /> and <paramref name="length" /> is greater than the size of <paramref name="array" />.</exception>
		// Token: 0x0600029F RID: 671
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Clear(Array array, int index, int length);

		/// <summary>Gets the value at the specified position in the multidimensional <see cref="T:System.Array" />. The indexes are specified as an array of 32-bit integers.</summary>
		/// <param name="indices">A one-dimensional array of 32-bit integers that represent the indexes specifying the position of the <see cref="T:System.Array" /> element to get.</param>
		/// <returns>The value at the specified position in the multidimensional <see cref="T:System.Array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="indices" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of dimensions in the current <see cref="T:System.Array" /> is not equal to the number of elements in <paramref name="indices" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Any element in <paramref name="indices" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002A0 RID: 672 RVA: 0x00006300 File Offset: 0x00004500
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe object GetValue(params int[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			TypedReference typedReference = default(TypedReference);
			fixed (int[] array = indices)
			{
				int* ptr;
				if (indices == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				this.InternalGetReference((void*)(&typedReference), indices.Length, ptr);
			}
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		/// <summary>Gets the value at the specified position in the one-dimensional <see cref="T:System.Array" />. The index is specified as a 32-bit integer.</summary>
		/// <param name="index">A 32-bit integer that represents the position of the <see cref="T:System.Array" /> element to get.</param>
		/// <returns>The value at the specified position in the one-dimensional <see cref="T:System.Array" />.</returns>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly one dimension.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002A1 RID: 673 RVA: 0x00006370 File Offset: 0x00004570
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe object GetValue(int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need1DArray"));
			}
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 1, &index);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		/// <summary>Gets the value at the specified position in the two-dimensional <see cref="T:System.Array" />. The indexes are specified as 32-bit integers.</summary>
		/// <param name="index1">A 32-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index2">A 32-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <returns>The value at the specified position in the two-dimensional <see cref="T:System.Array" />.</returns>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly two dimensions.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Either <paramref name="index1" /> or <paramref name="index2" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002A2 RID: 674 RVA: 0x000063B4 File Offset: 0x000045B4
		[SecuritySafeCritical]
		public unsafe object GetValue(int index1, int index2)
		{
			if (this.Rank != 2)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need2DArray"));
			}
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = index1;
			ptr[1] = index2;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 2, ptr);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		/// <summary>Gets the value at the specified position in the three-dimensional <see cref="T:System.Array" />. The indexes are specified as 32-bit integers.</summary>
		/// <param name="index1">A 32-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index2">A 32-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index3">A 32-bit integer that represents the third-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <returns>The value at the specified position in the three-dimensional <see cref="T:System.Array" />.</returns>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly three dimensions.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index1" /> or <paramref name="index2" /> or <paramref name="index3" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002A3 RID: 675 RVA: 0x00006404 File Offset: 0x00004604
		[SecuritySafeCritical]
		public unsafe object GetValue(int index1, int index2, int index3)
		{
			if (this.Rank != 3)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need3DArray"));
			}
			int* ptr = stackalloc int[(UIntPtr)12];
			*ptr = index1;
			ptr[1] = index2;
			ptr[2] = index3;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 3, ptr);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		/// <summary>Gets the value at the specified position in the one-dimensional <see cref="T:System.Array" />. The index is specified as a 64-bit integer.</summary>
		/// <param name="index">A 64-bit integer that represents the position of the <see cref="T:System.Array" /> element to get.</param>
		/// <returns>The value at the specified position in the one-dimensional <see cref="T:System.Array" />.</returns>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly one dimension.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002A4 RID: 676 RVA: 0x0000645B File Offset: 0x0000465B
		[ComVisible(false)]
		public object GetValue(long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			return this.GetValue((int)index);
		}

		/// <summary>Gets the value at the specified position in the two-dimensional <see cref="T:System.Array" />. The indexes are specified as 64-bit integers.</summary>
		/// <param name="index1">A 64-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index2">A 64-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <returns>The value at the specified position in the two-dimensional <see cref="T:System.Array" />.</returns>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly two dimensions.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Either <paramref name="index1" /> or <paramref name="index2" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002A5 RID: 677 RVA: 0x0000648C File Offset: 0x0000468C
		[ComVisible(false)]
		public object GetValue(long index1, long index2)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			return this.GetValue((int)index1, (int)index2);
		}

		/// <summary>Gets the value at the specified position in the three-dimensional <see cref="T:System.Array" />. The indexes are specified as 64-bit integers.</summary>
		/// <param name="index1">A 64-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index2">A 64-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index3">A 64-bit integer that represents the third-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <returns>The value at the specified position in the three-dimensional <see cref="T:System.Array" />.</returns>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly three dimensions.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index1" /> or <paramref name="index2" /> or <paramref name="index3" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002A6 RID: 678 RVA: 0x000064F4 File Offset: 0x000046F4
		[ComVisible(false)]
		public object GetValue(long index1, long index2, long index3)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index3 > 2147483647L || index3 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index3", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			return this.GetValue((int)index1, (int)index2, (int)index3);
		}

		/// <summary>Gets the value at the specified position in the multidimensional <see cref="T:System.Array" />. The indexes are specified as an array of 64-bit integers.</summary>
		/// <param name="indices">A one-dimensional array of 64-bit integers that represent the indexes specifying the position of the <see cref="T:System.Array" /> element to get.</param>
		/// <returns>The value at the specified position in the multidimensional <see cref="T:System.Array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="indices" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of dimensions in the current <see cref="T:System.Array" /> is not equal to the number of elements in <paramref name="indices" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Any element in <paramref name="indices" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002A7 RID: 679 RVA: 0x00006584 File Offset: 0x00004784
		[ComVisible(false)]
		public object GetValue(params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
				}
				array[i] = (int)num;
			}
			return this.GetValue(array);
		}

		/// <summary>Sets a value to the element at the specified position in the one-dimensional <see cref="T:System.Array" />. The index is specified as a 32-bit integer.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index">A 32-bit integer that represents the position of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly one dimension.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002A8 RID: 680 RVA: 0x00006608 File Offset: 0x00004808
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe void SetValue(object value, int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need1DArray"));
			}
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 1, &index);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		/// <summary>Sets a value to the element at the specified position in the two-dimensional <see cref="T:System.Array" />. The indexes are specified as 32-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index1">A 32-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index2">A 32-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly two dimensions.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Either <paramref name="index1" /> or <paramref name="index2" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002A9 RID: 681 RVA: 0x0000664C File Offset: 0x0000484C
		[SecuritySafeCritical]
		public unsafe void SetValue(object value, int index1, int index2)
		{
			if (this.Rank != 2)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need2DArray"));
			}
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = index1;
			ptr[1] = index2;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 2, ptr);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		/// <summary>Sets a value to the element at the specified position in the three-dimensional <see cref="T:System.Array" />. The indexes are specified as 32-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index1">A 32-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index2">A 32-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index3">A 32-bit integer that represents the third-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly three dimensions.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index1" /> or <paramref name="index2" /> or <paramref name="index3" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002AA RID: 682 RVA: 0x0000669C File Offset: 0x0000489C
		[SecuritySafeCritical]
		public unsafe void SetValue(object value, int index1, int index2, int index3)
		{
			if (this.Rank != 3)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need3DArray"));
			}
			int* ptr = stackalloc int[(UIntPtr)12];
			*ptr = index1;
			ptr[1] = index2;
			ptr[2] = index3;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 3, ptr);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		/// <summary>Sets a value to the element at the specified position in the multidimensional <see cref="T:System.Array" />. The indexes are specified as an array of 32-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="indices">A one-dimensional array of 32-bit integers that represent the indexes specifying the position of the element to set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="indices" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of dimensions in the current <see cref="T:System.Array" /> is not equal to the number of elements in <paramref name="indices" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Any element in <paramref name="indices" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002AB RID: 683 RVA: 0x000066F8 File Offset: 0x000048F8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe void SetValue(object value, params int[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			TypedReference typedReference = default(TypedReference);
			fixed (int[] array = indices)
			{
				int* ptr;
				if (indices == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				this.InternalGetReference((void*)(&typedReference), indices.Length, ptr);
			}
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		/// <summary>Sets a value to the element at the specified position in the one-dimensional <see cref="T:System.Array" />. The index is specified as a 64-bit integer.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index">A 64-bit integer that represents the position of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly one dimension.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002AC RID: 684 RVA: 0x00006766 File Offset: 0x00004966
		[ComVisible(false)]
		public void SetValue(object value, long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.SetValue(value, (int)index);
		}

		/// <summary>Sets a value to the element at the specified position in the two-dimensional <see cref="T:System.Array" />. The indexes are specified as 64-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index1">A 64-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index2">A 64-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly two dimensions.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Either <paramref name="index1" /> or <paramref name="index2" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002AD RID: 685 RVA: 0x00006798 File Offset: 0x00004998
		[ComVisible(false)]
		public void SetValue(object value, long index1, long index2)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.SetValue(value, (int)index1, (int)index2);
		}

		/// <summary>Sets a value to the element at the specified position in the three-dimensional <see cref="T:System.Array" />. The indexes are specified as 64-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index1">A 64-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index2">A 64-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index3">A 64-bit integer that represents the third-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly three dimensions.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index1" /> or <paramref name="index2" /> or <paramref name="index3" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002AE RID: 686 RVA: 0x00006800 File Offset: 0x00004A00
		[ComVisible(false)]
		public void SetValue(object value, long index1, long index2, long index3)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index3 > 2147483647L || index3 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index3", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.SetValue(value, (int)index1, (int)index2, (int)index3);
		}

		/// <summary>Sets a value to the element at the specified position in the multidimensional <see cref="T:System.Array" />. The indexes are specified as an array of 64-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="indices">A one-dimensional array of 64-bit integers that represent the indexes specifying the position of the element to set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="indices" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of dimensions in the current <see cref="T:System.Array" /> is not equal to the number of elements in <paramref name="indices" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Any element in <paramref name="indices" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		// Token: 0x060002AF RID: 687 RVA: 0x00006894 File Offset: 0x00004A94
		[ComVisible(false)]
		public void SetValue(object value, params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
				}
				array[i] = (int)num;
			}
			this.SetValue(value, array);
		}

		// Token: 0x060002B0 RID: 688
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void InternalGetReference(void* elemRef, int rank, int* pIndices);

		// Token: 0x060002B1 RID: 689
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void InternalSetValue(void* target, object value);

		/// <summary>Gets the total number of elements in all the dimensions of the <see cref="T:System.Array" />.</summary>
		/// <returns>The total number of elements in all the dimensions of the <see cref="T:System.Array" />; zero if there are no elements in the array.</returns>
		/// <exception cref="T:System.OverflowException">The array is multidimensional and contains more than <see cref="F:System.Int32.MaxValue" /> elements.</exception>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002B2 RID: 690
		[__DynamicallyInvokable]
		public extern int Length
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00006919 File Offset: 0x00004B19
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static int GetMedian(int low, int hi)
		{
			return low + (hi - low >> 1);
		}

		/// <summary>Gets a 64-bit integer that represents the total number of elements in all the dimensions of the <see cref="T:System.Array" />.</summary>
		/// <returns>A 64-bit integer that represents the total number of elements in all the dimensions of the <see cref="T:System.Array" />.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002B4 RID: 692
		[ComVisible(false)]
		public extern long LongLength
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Gets a 32-bit integer that represents the number of elements in the specified dimension of the <see cref="T:System.Array" />.</summary>
		/// <param name="dimension">A zero-based dimension of the <see cref="T:System.Array" /> whose length needs to be determined.</param>
		/// <returns>A 32-bit integer that represents the number of elements in the specified dimension.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="dimension" /> is less than zero.  
		/// -or-  
		/// <paramref name="dimension" /> is equal to or greater than <see cref="P:System.Array.Rank" />.</exception>
		// Token: 0x060002B5 RID: 693
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLength(int dimension);

		/// <summary>Gets a 64-bit integer that represents the number of elements in the specified dimension of the <see cref="T:System.Array" />.</summary>
		/// <param name="dimension">A zero-based dimension of the <see cref="T:System.Array" /> whose length needs to be determined.</param>
		/// <returns>A 64-bit integer that represents the number of elements in the specified dimension.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="dimension" /> is less than zero.  
		/// -or-  
		/// <paramref name="dimension" /> is equal to or greater than <see cref="P:System.Array.Rank" />.</exception>
		// Token: 0x060002B6 RID: 694 RVA: 0x00006922 File Offset: 0x00004B22
		[ComVisible(false)]
		public long GetLongLength(int dimension)
		{
			return (long)this.GetLength(dimension);
		}

		/// <summary>Gets the rank (number of dimensions) of the <see cref="T:System.Array" />. For example, a one-dimensional array returns 1, a two-dimensional array returns 2, and so on.</summary>
		/// <returns>The rank (number of dimensions) of the <see cref="T:System.Array" />.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002B7 RID: 695
		[__DynamicallyInvokable]
		public extern int Rank
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Gets the index of the last element of the specified dimension in the array.</summary>
		/// <param name="dimension">A zero-based dimension of the array whose upper bound needs to be determined.</param>
		/// <returns>The index of the last element of the specified dimension in the array, or -1 if the specified dimension is empty.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="dimension" /> is less than zero.  
		/// -or-  
		/// <paramref name="dimension" /> is equal to or greater than <see cref="P:System.Array.Rank" />.</exception>
		// Token: 0x060002B8 RID: 696
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetUpperBound(int dimension);

		/// <summary>Gets the index of the first element of the specified dimension in the array.</summary>
		/// <param name="dimension">A zero-based dimension of the array whose starting index needs to be determined.</param>
		/// <returns>The index of the first element of the specified dimension in the array.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="dimension" /> is less than zero.  
		/// -or-  
		/// <paramref name="dimension" /> is equal to or greater than <see cref="P:System.Array.Rank" />.</exception>
		// Token: 0x060002B9 RID: 697
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLowerBound(int dimension);

		// Token: 0x060002BA RID: 698
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetDataPtrOffsetInternal();

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Array" />.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000692C File Offset: 0x00004B2C
		[__DynamicallyInvokable]
		int ICollection.Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Length;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Array" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Array" />.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00006934 File Offset: 0x00004B34
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Array" /> is read-only.</summary>
		/// <returns>This property is always <see langword="false" /> for all arrays.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00006937 File Offset: 0x00004B37
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Array" /> has a fixed size.</summary>
		/// <returns>This property is always <see langword="true" /> for all arrays.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000693A File Offset: 0x00004B3A
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Array" /> is synchronized (thread safe).</summary>
		/// <returns>This property is always <see langword="false" /> for all arrays.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000693D File Offset: 0x00004B3D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly one dimension.</exception>
		// Token: 0x17000037 RID: 55
		[__DynamicallyInvokable]
		object IList.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValue(index);
			}
			[__DynamicallyInvokable]
			set
			{
				this.SetValue(value, index);
			}
		}

		/// <summary>Calling this method always throws a <see cref="T:System.NotSupportedException" /> exception.</summary>
		/// <param name="value">The object to be added to the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>Adding a value to an array is not supported. No value is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> has a fixed size.</exception>
		// Token: 0x060002C2 RID: 706 RVA: 0x00006953 File Offset: 0x00004B53
		[__DynamicallyInvokable]
		int IList.Add(object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The object to locate in the current list. The element to locate can be <see langword="null" /> for reference types.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002C3 RID: 707 RVA: 0x00006964 File Offset: 0x00004B64
		[__DynamicallyInvokable]
		bool IList.Contains(object value)
		{
			return Array.IndexOf(this, value) >= this.GetLowerBound(0);
		}

		/// <summary>Removes all items from the <see cref="T:System.Collections.IList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.</exception>
		// Token: 0x060002C4 RID: 708 RVA: 0x00006979 File Offset: 0x00004B79
		[__DynamicallyInvokable]
		void IList.Clear()
		{
			Array.Clear(this, this.GetLowerBound(0), this.Length);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The object to locate in the current list.</param>
		/// <returns>The index of value if found in the list; otherwise, -1.</returns>
		// Token: 0x060002C5 RID: 709 RVA: 0x0000698E File Offset: 0x00004B8E
		[__DynamicallyInvokable]
		int IList.IndexOf(object value)
		{
			return Array.IndexOf(this, value);
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.IList" /> has a fixed size.</exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="value" /> is null reference in the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x060002C6 RID: 710 RVA: 0x00006997 File Offset: 0x00004B97
		[__DynamicallyInvokable]
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.IList" /> has a fixed size.</exception>
		// Token: 0x060002C7 RID: 711 RVA: 0x000069A8 File Offset: 0x00004BA8
		[__DynamicallyInvokable]
		void IList.Remove(object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		/// <summary>Removes the <see cref="T:System.Collections.IList" /> item at the specified index.</summary>
		/// <param name="index">The index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="T:System.Collections.IList" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.IList" /> has a fixed size.</exception>
		// Token: 0x060002C8 RID: 712 RVA: 0x000069B9 File Offset: 0x00004BB9
		[__DynamicallyInvokable]
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Array" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Array" />.</returns>
		// Token: 0x060002C9 RID: 713 RVA: 0x000069CA File Offset: 0x00004BCA
		[__DynamicallyInvokable]
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the current collection object precedes, occurs in the same position as, or follows another object in the sort order.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <param name="comparer">An object that compares the current object and <paramref name="other" />.</param>
		/// <returns>An integer that indicates the relationship of the current collection object to other, as shown in the following table.  
		///   Return value  
		///
		///   Description  
		///
		///   -1  
		///
		///   The current instance precedes <paramref name="other" />.  
		///
		///   0  
		///
		///   The current instance and <paramref name="other" /> are equal.  
		///
		///   1  
		///
		///   The current instance follows <paramref name="other" />.</returns>
		// Token: 0x060002CA RID: 714 RVA: 0x000069D4 File Offset: 0x00004BD4
		[__DynamicallyInvokable]
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Array array = other as Array;
			if (array == null || this.Length != array.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_OtherNotArrayOfCorrectLength"), "other");
			}
			int num = 0;
			int num2 = 0;
			while (num < array.Length && num2 == 0)
			{
				object value = this.GetValue(num);
				object value2 = array.GetValue(num);
				num2 = comparer.Compare(value, value2);
				num++;
			}
			return num2;
		}

		/// <summary>Determines whether an object is equal to the current instance.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <param name="comparer">An object that determines whether the current instance and <paramref name="other" /> are equal.</param>
		/// <returns>
		///   <see langword="true" /> if the two objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002CB RID: 715 RVA: 0x00006A48 File Offset: 0x00004C48
		[__DynamicallyInvokable]
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			Array array = other as Array;
			if (array == null || array.Length != this.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				object value = this.GetValue(i);
				object value2 = array.GetValue(i);
				if (!comparer.Equals(value, value2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00006AA8 File Offset: 0x00004CA8
		internal static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		/// <summary>Returns a hash code for the current instance.</summary>
		/// <param name="comparer">An object that computes the hash code of the current object.</param>
		/// <returns>The hash code for the current instance.</returns>
		// Token: 0x060002CD RID: 717 RVA: 0x00006AB4 File Offset: 0x00004CB4
		[__DynamicallyInvokable]
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			int num = 0;
			for (int i = ((this.Length >= 8) ? (this.Length - 8) : 0); i < this.Length; i++)
			{
				num = Array.CombineHashCodes(num, comparer.GetHashCode(this.GetValue(i)));
			}
			return num;
		}

		/// <summary>Searches an entire one-dimensional sorted array for a specific element, using the <see cref="T:System.IComparable" /> interface implemented by each element of the array and by the specified object.</summary>
		/// <param name="array">The sorted one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found; otherwise, a negative number. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, the negative number returned is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than all elements in <paramref name="array" />, the negative number returned is the bitwise complement of (the index of the last element plus 1). If this method is called with a non-sorted <paramref name="array" />, the return value can be incorrect and a negative number could be returned, even if <paramref name="value" /> is present in <paramref name="array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IComparable" /> interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060002CE RID: 718 RVA: 0x00006B0C File Offset: 0x00004D0C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.BinarySearch(array, lowerBound, array.Length, value, null);
		}

		/// <summary>Searches a range of elements in a one-dimensional sorted array for a value, using the <see cref="T:System.IComparable" /> interface implemented by each element of the array and by the specified value.</summary>
		/// <param name="array">The sorted one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="index">The starting index of the range to search.</param>
		/// <param name="length">The length of the range to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found; otherwise, a negative number. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, the negative number returned is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than all elements in <paramref name="array" />, the negative number returned is the bitwise complement of (the index of the last element plus 1). If this method is called with a non-sorted <paramref name="array" />, the return value can be incorrect and a negative number could be returned, even if <paramref name="value" /> is present in <paramref name="array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.  
		/// -or-  
		/// <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IComparable" /> interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060002CF RID: 719 RVA: 0x00006B3E File Offset: 0x00004D3E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, int index, int length, object value)
		{
			return Array.BinarySearch(array, index, length, value, null);
		}

		/// <summary>Searches an entire one-dimensional sorted array for a value using the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="array">The sorted one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found; otherwise, a negative number. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, the negative number returned is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than all elements in <paramref name="array" />, the negative number returned is the bitwise complement of (the index of the last element plus 1). If this method is called with a non-sorted <paramref name="array" />, the return value can be incorrect and a negative number could be returned, even if <paramref name="value" /> is present in <paramref name="array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparer" /> is <see langword="null" />, and <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, <paramref name="value" /> does not implement the <see cref="T:System.IComparable" /> interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060002D0 RID: 720 RVA: 0x00006B4C File Offset: 0x00004D4C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.BinarySearch(array, lowerBound, array.Length, value, comparer);
		}

		/// <summary>Searches a range of elements in a one-dimensional sorted array for a value, using the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="array">The sorted one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="index">The starting index of the range to search.</param>
		/// <param name="length">The length of the range to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found; otherwise, a negative number. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, the negative number returned is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than all elements in <paramref name="array" />, the negative number returned is the bitwise complement of (the index of the last element plus 1). If this method is called with a non-sorted <paramref name="array" />, the return value can be incorrect and a negative number could be returned, even if <paramref name="value" /> is present in <paramref name="array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.  
		/// -or-  
		/// <paramref name="comparer" /> is <see langword="null" />, and <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, <paramref name="value" /> does not implement the <see cref="T:System.IComparable" /> interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060002D1 RID: 721 RVA: 0x00006B80 File Offset: 0x00004D80
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, int index, int length, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			if (index < lowerBound || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < lowerBound) ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - (index - lowerBound) < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			if (comparer == null)
			{
				comparer = Comparer.Default;
			}
			if (comparer == Comparer.Default)
			{
				int num;
				bool flag = Array.TrySZBinarySearch(array, index, length, value, out num);
				if (flag)
				{
					return num;
				}
			}
			int i = index;
			int num2 = index + length - 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i <= num2)
				{
					int median = Array.GetMedian(i, num2);
					int num3;
					try
					{
						num3 = comparer.Compare(array2[median], value);
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
					}
					if (num3 == 0)
					{
						return median;
					}
					if (num3 < 0)
					{
						i = median + 1;
					}
					else
					{
						num2 = median - 1;
					}
				}
			}
			else
			{
				while (i <= num2)
				{
					int median2 = Array.GetMedian(i, num2);
					int num4;
					try
					{
						num4 = comparer.Compare(array.GetValue(median2), value);
					}
					catch (Exception ex2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex2);
					}
					if (num4 == 0)
					{
						return median2;
					}
					if (num4 < 0)
					{
						i = median2 + 1;
					}
					else
					{
						num2 = median2 - 1;
					}
				}
			}
			return ~i;
		}

		// Token: 0x060002D2 RID: 722
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZBinarySearch(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

		/// <summary>Searches an entire one-dimensional sorted array for a specific element, using the <see cref="T:System.IComparable`1" /> generic interface implemented by each element of the <see cref="T:System.Array" /> and by the specified object.</summary>
		/// <param name="array">The sorted one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found; otherwise, a negative number. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, the negative number returned is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than all elements in <paramref name="array" />, the negative number returned is the bitwise complement of (the index of the last element plus 1). If this method is called with a non-sorted <paramref name="array" />, the return value can be incorrect and a negative number could be returned, even if <paramref name="value" /> is present in <paramref name="array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="T" /> does not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x060002D3 RID: 723 RVA: 0x00006CF8 File Offset: 0x00004EF8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, null);
		}

		/// <summary>Searches an entire one-dimensional sorted array for a value using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <param name="array">The sorted one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable`1" /> implementation of each element.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found; otherwise, a negative number. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, the negative number returned is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than all elements in <paramref name="array" />, the negative number returned is the bitwise complement of (the index of the last element plus 1). If this method is called with a non-sorted <paramref name="array" />, the return value can be incorrect and a negative number could be returned, even if <paramref name="value" /> is present in <paramref name="array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparer" /> is <see langword="null" />, and <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and <paramref name="T" /> does not implement the <see cref="T:System.IComparable`1" /> generic interface</exception>
		// Token: 0x060002D4 RID: 724 RVA: 0x00006D14 File Offset: 0x00004F14
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, comparer);
		}

		/// <summary>Searches a range of elements in a one-dimensional sorted array for a value, using the <see cref="T:System.IComparable`1" /> generic interface implemented by each element of the <see cref="T:System.Array" /> and by the specified value.</summary>
		/// <param name="array">The sorted one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="index">The starting index of the range to search.</param>
		/// <param name="length">The length of the range to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found; otherwise, a negative number. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, the negative number returned is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than all elements in <paramref name="array" />, the negative number returned is the bitwise complement of (the index of the last element plus 1). If this method is called with a non-sorted <paramref name="array" />, the return value can be incorrect and a negative number could be returned, even if <paramref name="value" /> is present in <paramref name="array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.  
		/// -or-  
		/// <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="T" /> does not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x060002D5 RID: 725 RVA: 0x00006D30 File Offset: 0x00004F30
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array, index, length, value, null);
		}

		/// <summary>Searches a range of elements in a one-dimensional sorted array for a value, using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <param name="array">The sorted one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="index">The starting index of the range to search.</param>
		/// <param name="length">The length of the range to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable`1" /> implementation of each element.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found; otherwise, a negative number. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, the negative number returned is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than all elements in <paramref name="array" />, the negative number returned is the bitwise complement of (the index of the last element plus 1). If this method is called with a non-sorted <paramref name="array" />, the return value can be incorrect and a negative number could be returned, even if <paramref name="value" /> is present in <paramref name="array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.  
		/// -or-  
		/// <paramref name="comparer" /> is <see langword="null" />, and <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and <paramref name="T" /> does not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x060002D6 RID: 726 RVA: 0x00006D3C File Offset: 0x00004F3C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			return ArraySortHelper<T>.Default.BinarySearch(array, index, length, value, comparer);
		}

		/// <summary>Converts an array of one type to an array of another type.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to convert to a target type.</param>
		/// <param name="converter">A <see cref="T:System.Converter`2" /> that converts each element from one type to another type.</param>
		/// <typeparam name="TInput">The type of the elements of the source array.</typeparam>
		/// <typeparam name="TOutput">The type of the elements of the target array.</typeparam>
		/// <returns>An array of the target type containing the converted elements from the source array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="converter" /> is <see langword="null" />.</exception>
		// Token: 0x060002D7 RID: 727 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public static TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			TOutput[] array2 = new TOutput[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = converter(array[i]);
			}
			return array2;
		}

		/// <summary>Copies all the elements of the current one-dimensional array to the specified one-dimensional array starting at the specified destination array index. The index is specified as a 32-bit integer.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the current array.</param>
		/// <param name="index">A 32-bit integer that represents the index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source array is greater than the available number of elements from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Array" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">The source array is multidimensional.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in the source <see cref="T:System.Array" /> cannot be cast to the type of destination <paramref name="array" />.</exception>
		// Token: 0x060002D8 RID: 728 RVA: 0x00006DFD File Offset: 0x00004FFD
		[__DynamicallyInvokable]
		public void CopyTo(Array array, int index)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			Array.Copy(this, this.GetLowerBound(0), array, index, this.Length);
		}

		/// <summary>Copies all the elements of the current one-dimensional array to the specified one-dimensional array starting at the specified destination array index. The index is specified as a 64-bit integer.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the current array.</param>
		/// <param name="index">A 64-bit integer that represents the index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source array is greater than the available number of elements from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Array" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">The source <see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in the source <see cref="T:System.Array" /> cannot be cast to the type of destination <paramref name="array" />.</exception>
		// Token: 0x060002D9 RID: 729 RVA: 0x00006E30 File Offset: 0x00005030
		[ComVisible(false)]
		public void CopyTo(Array array, long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.CopyTo(array, (int)index);
		}

		/// <summary>Returns an empty array.</summary>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>An empty array.</returns>
		// Token: 0x060002DA RID: 730 RVA: 0x00006E62 File Offset: 0x00005062
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static T[] Empty<T>()
		{
			return EmptyArray<T>.Value;
		}

		/// <summary>Determines whether the specified array contains elements that match the conditions defined by the specified predicate.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the elements to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>
		///   <see langword="true" /> if <paramref name="array" /> contains one or more elements that match the conditions defined by the specified predicate; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x060002DB RID: 731 RVA: 0x00006E69 File Offset: 0x00005069
		[__DynamicallyInvokable]
		public static bool Exists<T>(T[] array, Predicate<T> match)
		{
			return Array.FindIndex<T>(array, match) != -1;
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional, zero-based array to search.</param>
		/// <param name="match">The predicate that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type <paramref name="T" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x060002DC RID: 732 RVA: 0x00006E78 File Offset: 0x00005078
		[__DynamicallyInvokable]
		public static T Find<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		/// <summary>Retrieves all the elements that match the conditions defined by the specified predicate.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the elements to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>An <see cref="T:System.Array" /> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="T:System.Array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x060002DD RID: 733 RVA: 0x00006ED0 File Offset: 0x000050D0
		[__DynamicallyInvokable]
		public static T[] FindAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<T> list = new List<T>();
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					list.Add(array[i]);
				}
			}
			return list.ToArray();
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x060002DE RID: 734 RVA: 0x00006F2F File Offset: 0x0000512F
		[__DynamicallyInvokable]
		public static int FindIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, 0, array.Length, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Array" /> that extends from the specified index to the last element.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="match" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		// Token: 0x060002DF RID: 735 RVA: 0x00006F4A File Offset: 0x0000514A
		[__DynamicallyInvokable]
		public static int FindIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, startIndex, array.Length - startIndex, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Array" /> that starts at the specified index and contains the specified number of elements.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="match" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		// Token: 0x060002E0 RID: 736 RVA: 0x00006F68 File Offset: 0x00005168
		[__DynamicallyInvokable]
		public static int FindIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex > array.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the last occurrence within the entire <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The last element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type <paramref name="T" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x060002E1 RID: 737 RVA: 0x00006FF4 File Offset: 0x000051F4
		[__DynamicallyInvokable]
		public static T FindLast<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the entire <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x060002E2 RID: 738 RVA: 0x0000704D File Offset: 0x0000524D
		[__DynamicallyInvokable]
		public static int FindLastIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindLastIndex<T>(array, array.Length - 1, array.Length, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Array" /> that extends from the first element to the specified index.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="match" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		// Token: 0x060002E3 RID: 739 RVA: 0x0000706C File Offset: 0x0000526C
		[__DynamicallyInvokable]
		public static int FindLastIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindLastIndex<T>(array, startIndex, startIndex + 1, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Array" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="match" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		// Token: 0x060002E4 RID: 740 RVA: 0x00007088 File Offset: 0x00005288
		[__DynamicallyInvokable]
		public static int FindLastIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			if (array.Length == 0)
			{
				if (startIndex != -1)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
			}
			else if (startIndex < 0 || startIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			int num = startIndex - count;
			for (int i = startIndex; i > num; i--)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Performs the specified action on each element of the specified array.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> on whose elements the action is to be performed.</param>
		/// <param name="action">The <see cref="T:System.Action`1" /> to perform on each element of <paramref name="array" />.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="action" /> is <see langword="null" />.</exception>
		// Token: 0x060002E5 RID: 741 RVA: 0x00007130 File Offset: 0x00005330
		public static void ForEach<T>(T[] array, Action<T> action)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			for (int i = 0; i < array.Length; i++)
			{
				action(array[i]);
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Array" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Array" />.</returns>
		// Token: 0x060002E6 RID: 742 RVA: 0x00007174 File Offset: 0x00005374
		[__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			int lowerBound = this.GetLowerBound(0);
			if (this.Rank == 1 && lowerBound == 0)
			{
				return new Array.SZArrayEnumerator(this);
			}
			return new Array.ArrayEnumerator(this, lowerBound, this.Length);
		}

		/// <summary>Searches for the specified object and returns the index of its first occurrence in a one-dimensional array.</summary>
		/// <param name="array">The one-dimensional array to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <returns>The index of the first occurrence of <paramref name="value" /> in <paramref name="array" />, if found; otherwise, the lower bound of the array minus 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		// Token: 0x060002E7 RID: 743 RVA: 0x000071AC File Offset: 0x000053AC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int IndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.IndexOf(array, value, lowerBound, array.Length);
		}

		/// <summary>Searches for the specified object in a range of elements of a one-dimensional array, and returns the index of its first occurrence. The range extends from a specified index to the end of the array.</summary>
		/// <param name="array">The one-dimensional array to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The starting index of the search. 0 (zero) is valid in an empty array.</param>
		/// <returns>The index of the first occurrence of <paramref name="value" />, if it's found, within the range of elements in <paramref name="array" /> that extends from <paramref name="startIndex" /> to the last element; otherwise, the lower bound of the array minus 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		// Token: 0x060002E8 RID: 744 RVA: 0x000071E0 File Offset: 0x000053E0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int IndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.IndexOf(array, value, startIndex, array.Length - startIndex + lowerBound);
		}

		/// <summary>Searches for the specified object in a range of elements of a one-dimensional array, and returns the index of ifs first occurrence. The range extends from a specified index for a specified number of elements.</summary>
		/// <param name="array">The one-dimensional array to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The starting index of the search. 0 (zero) is valid in an empty array.</param>
		/// <param name="count">The number of elements to search.</param>
		/// <returns>The index of the first occurrence of <paramref name="value" />, if it's found in the <paramref name="array" /> from index <paramref name="startIndex" /> to <paramref name="startIndex" /> + <paramref name="count" /> - 1; otherwise, the lower bound of the array minus 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		// Token: 0x060002E9 RID: 745 RVA: 0x00007218 File Offset: 0x00005418
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int IndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			int lowerBound = array.GetLowerBound(0);
			if (startIndex < lowerBound || startIndex > array.Length + lowerBound)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || count > array.Length - startIndex + lowerBound)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			int num;
			bool flag = Array.TrySZIndexOf(array, startIndex, count, value, out num);
			if (flag)
			{
				return num;
			}
			object[] array2 = array as object[];
			int num2 = startIndex + count;
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i < num2; i++)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j < num2; j++)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
			}
			else
			{
				for (int k = startIndex; k < num2; k++)
				{
					object value2 = array.GetValue(k);
					if (value2 == null)
					{
						if (value == null)
						{
							return k;
						}
					}
					else if (value2.Equals(value))
					{
						return k;
					}
				}
			}
			return lowerBound - 1;
		}

		/// <summary>Searches for the specified object and returns the index of its first occurrence in a one-dimensional array.</summary>
		/// <param name="array">The one-dimensional, zero-based array to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> in the entire <paramref name="array" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		// Token: 0x060002EA RID: 746 RVA: 0x0000733C File Offset: 0x0000553C
		[__DynamicallyInvokable]
		public static int IndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf<T>(array, value, 0, array.Length);
		}

		/// <summary>Searches for the specified object in a range of elements of a one dimensional array, and returns the index of its first occurrence. The range extends from a specified index to the end of the array.</summary>
		/// <param name="array">The one-dimensional, zero-based array to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty array.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that extends from <paramref name="startIndex" /> to the last element, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		// Token: 0x060002EB RID: 747 RVA: 0x00007357 File Offset: 0x00005557
		[__DynamicallyInvokable]
		public static int IndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf<T>(array, value, startIndex, array.Length - startIndex);
		}

		/// <summary>Searches for the specified object in a range of elements of a one-dimensional array, and returns the index of its first occurrence. The range extends from a specified index for a specified number of elements.</summary>
		/// <param name="array">The one-dimensional, zero-based array to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty array.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified in <paramref name="count" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		// Token: 0x060002EC RID: 748 RVA: 0x00007374 File Offset: 0x00005574
		[__DynamicallyInvokable]
		public static int IndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || count > array.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			return EqualityComparer<T>.Default.IndexOf(array, value, startIndex, count);
		}

		// Token: 0x060002ED RID: 749
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZIndexOf(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the entire one-dimensional <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <returns>The index of the last occurrence of <paramref name="value" /> within the entire <paramref name="array" />, if found; otherwise, the lower bound of the array minus 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		// Token: 0x060002EE RID: 750 RVA: 0x000073E0 File Offset: 0x000055E0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int LastIndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.LastIndexOf(array, value, array.Length - 1 + lowerBound, array.Length);
		}

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the range of elements in the one-dimensional <see cref="T:System.Array" /> that extends from the first element to the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The starting index of the backward search.</param>
		/// <returns>The index of the last occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that extends from the first element to <paramref name="startIndex" />, if found; otherwise, the lower bound of the array minus 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		// Token: 0x060002EF RID: 751 RVA: 0x0000741C File Offset: 0x0000561C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int LastIndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.LastIndexOf(array, value, startIndex, startIndex + 1 - lowerBound);
		}

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the range of elements in the one-dimensional <see cref="T:System.Array" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The index of the last occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that contains the number of elements specified in <paramref name="count" /> and ends at <paramref name="startIndex" />, if found; otherwise, the lower bound of the array minus 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		// Token: 0x060002F0 RID: 752 RVA: 0x0000744C File Offset: 0x0000564C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int LastIndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			if (array.Length == 0)
			{
				return lowerBound - 1;
			}
			if (startIndex < lowerBound || startIndex >= array.Length + lowerBound)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			if (count > startIndex - lowerBound + 1)
			{
				throw new ArgumentOutOfRangeException("endIndex", Environment.GetResourceString("ArgumentOutOfRange_EndIndexStartIndex"));
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			int num;
			bool flag = Array.TrySZLastIndexOf(array, startIndex, count, value, out num);
			if (flag)
			{
				return num;
			}
			object[] array2 = array as object[];
			int num2 = startIndex - count + 1;
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i >= num2; i--)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j >= num2; j--)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
			}
			else
			{
				for (int k = startIndex; k >= num2; k--)
				{
					object value2 = array.GetValue(k);
					if (value2 == null)
					{
						if (value == null)
						{
							return k;
						}
					}
					else if (value2.Equals(value))
					{
						return k;
					}
				}
			}
			return lowerBound - 1;
		}

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the entire <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the entire <paramref name="array" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		// Token: 0x060002F1 RID: 753 RVA: 0x0000758E File Offset: 0x0000578E
		[__DynamicallyInvokable]
		public static int LastIndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf<T>(array, value, array.Length - 1, array.Length);
		}

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the range of elements in the <see cref="T:System.Array" /> that extends from the first element to the specified index.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that extends from the first element to <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		// Token: 0x060002F2 RID: 754 RVA: 0x000075AD File Offset: 0x000057AD
		[__DynamicallyInvokable]
		public static int LastIndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf<T>(array, value, startIndex, (array.Length == 0) ? 0 : (startIndex + 1));
		}

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the range of elements in the <see cref="T:System.Array" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that contains the number of elements specified in <paramref name="count" /> and ends at <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		// Token: 0x060002F3 RID: 755 RVA: 0x000075D0 File Offset: 0x000057D0
		[__DynamicallyInvokable]
		public static int LastIndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				if (startIndex != -1 && startIndex != 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count != 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				return -1;
			}
			else
			{
				if (startIndex < 0 || startIndex >= array.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				return EqualityComparer<T>.Default.LastIndexOf(array, value, startIndex, count);
			}
		}

		// Token: 0x060002F4 RID: 756
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZLastIndexOf(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

		/// <summary>Reverses the sequence of the elements in the entire one-dimensional <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to reverse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		// Token: 0x060002F5 RID: 757 RVA: 0x00007673 File Offset: 0x00005873
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Reverse(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Reverse(array, array.GetLowerBound(0), array.Length);
		}

		/// <summary>Reverses the sequence of the elements in a range of elements in the one-dimensional <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to reverse.</param>
		/// <param name="index">The starting index of the section to reverse.</param>
		/// <param name="length">The number of elements in the section to reverse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.</exception>
		// Token: 0x060002F6 RID: 758 RVA: 0x00007698 File Offset: 0x00005898
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Reverse(Array array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < array.GetLowerBound(0) || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - (index - array.GetLowerBound(0)) < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			bool flag = Array.TrySZReverse(array, index, length);
			if (flag)
			{
				return;
			}
			int i = index;
			int num = index + length - 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i < num)
				{
					object obj = array2[i];
					array2[i] = array2[num];
					array2[num] = obj;
					i++;
					num--;
				}
				return;
			}
			while (i < num)
			{
				object value = array.GetValue(i);
				array.SetValue(array.GetValue(num), i);
				array.SetValue(value, num);
				i++;
				num--;
			}
		}

		// Token: 0x060002F7 RID: 759
		[SecurityCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZReverse(Array array, int index, int count);

		/// <summary>Sorts the elements in an entire one-dimensional <see cref="T:System.Array" /> using the <see cref="T:System.IComparable" /> implementation of each element of the <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to sort.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060002F8 RID: 760 RVA: 0x00007787 File Offset: 0x00005987
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, null);
		}

		/// <summary>Sorts a pair of one-dimensional <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the <see cref="T:System.IComparable" /> implementation of each key.</summary>
		/// <param name="keys">The one-dimensional <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional <see cref="T:System.Array" /> that contains the items that correspond to each of the keys in the <paramref name="keys" /><see cref="T:System.Array" />.  
		///  -or-  
		///  <see langword="null" /> to sort only the <paramref name="keys" /><see cref="T:System.Array" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">The <paramref name="keys" /><see cref="T:System.Array" /> is multidimensional.  
		///  -or-  
		///  The <paramref name="items" /><see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not <see langword="null" />, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060002F9 RID: 761 RVA: 0x000077AC File Offset: 0x000059AC
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, null);
		}

		/// <summary>Sorts the elements in a range of elements in a one-dimensional <see cref="T:System.Array" /> using the <see cref="T:System.IComparable" /> implementation of each element of the <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to sort.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060002FA RID: 762 RVA: 0x000077D1 File Offset: 0x000059D1
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array, int index, int length)
		{
			Array.Sort(array, null, index, length, null);
		}

		/// <summary>Sorts a range of elements in a pair of one-dimensional <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the <see cref="T:System.IComparable" /> implementation of each key.</summary>
		/// <param name="keys">The one-dimensional <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional <see cref="T:System.Array" /> that contains the items that correspond to each of the keys in the <paramref name="keys" /><see cref="T:System.Array" />.  
		///  -or-  
		///  <see langword="null" /> to sort only the <paramref name="keys" /><see cref="T:System.Array" />.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">The <paramref name="keys" /><see cref="T:System.Array" /> is multidimensional.  
		///  -or-  
		///  The <paramref name="items" /><see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="keys" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not <see langword="null" />, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="keys" /><see cref="T:System.Array" />.  
		/// -or-  
		/// <paramref name="items" /> is not <see langword="null" />, and <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="items" /><see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060002FB RID: 763 RVA: 0x000077DD File Offset: 0x000059DD
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items, int index, int length)
		{
			Array.Sort(keys, items, index, length, null);
		}

		/// <summary>Sorts the elements in a one-dimensional <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="array">The one-dimensional array to sort.</param>
		/// <param name="comparer">The implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and one or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <exception cref="T:System.ArgumentException">The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		// Token: 0x060002FC RID: 764 RVA: 0x000077E9 File Offset: 0x000059E9
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, comparer);
		}

		/// <summary>Sorts a pair of one-dimensional <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="keys">The one-dimensional <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional <see cref="T:System.Array" /> that contains the items that correspond to each of the keys in the <paramref name="keys" /><see cref="T:System.Array" />.  
		///  -or-  
		///  <see langword="null" /> to sort only the <paramref name="keys" /><see cref="T:System.Array" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">The <paramref name="keys" /><see cref="T:System.Array" /> is multidimensional.  
		///  -or-  
		///  The <paramref name="items" /><see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not <see langword="null" />, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.  
		/// -or-  
		/// The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and one or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060002FD RID: 765 RVA: 0x0000780E File Offset: 0x00005A0E
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, comparer);
		}

		/// <summary>Sorts the elements in a range of elements in a one-dimensional <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to sort.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.  
		/// -or-  
		/// The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and one or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060002FE RID: 766 RVA: 0x00007833 File Offset: 0x00005A33
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array, int index, int length, IComparer comparer)
		{
			Array.Sort(array, null, index, length, comparer);
		}

		/// <summary>Sorts a range of elements in a pair of one-dimensional <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="keys">The one-dimensional <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional <see cref="T:System.Array" /> that contains the items that correspond to each of the keys in the <paramref name="keys" /><see cref="T:System.Array" />.  
		///  -or-  
		///  <see langword="null" /> to sort only the <paramref name="keys" /><see cref="T:System.Array" />.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.RankException">The <paramref name="keys" /><see cref="T:System.Array" /> is multidimensional.  
		///  -or-  
		///  The <paramref name="items" /><see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="keys" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not <see langword="null" />, and the lower bound of <paramref name="keys" /> does not match the lower bound of <paramref name="items" />.  
		/// -or-  
		/// <paramref name="items" /> is not <see langword="null" />, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="keys" /><see cref="T:System.Array" />.  
		/// -or-  
		/// <paramref name="items" /> is not <see langword="null" />, and <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="items" /><see cref="T:System.Array" />.  
		/// -or-  
		/// The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and one or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060002FF RID: 767 RVA: 0x00007840 File Offset: 0x00005A40
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items, int index, int length, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (keys.Rank != 1 || (items != null && items.Rank != 1))
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			if (items != null && keys.GetLowerBound(0) != items.GetLowerBound(0))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_LowerBoundsMustMatch"));
			}
			if (index < keys.GetLowerBound(0) || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (keys.Length - (index - keys.GetLowerBound(0)) < length || (items != null && index - items.GetLowerBound(0) > items.Length - length))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (length > 1)
			{
				if (comparer == Comparer.Default || comparer == null)
				{
					bool flag = Array.TrySZSort(keys, items, index, index + length - 1);
					if (flag)
					{
						return;
					}
				}
				object[] array = keys as object[];
				object[] array2 = null;
				if (array != null)
				{
					array2 = items as object[];
				}
				if (array != null && (items == null || array2 != null))
				{
					Array.SorterObjectArray sorterObjectArray = new Array.SorterObjectArray(array, array2, comparer);
					sorterObjectArray.Sort(index, length);
					return;
				}
				Array.SorterGenericArray sorterGenericArray = new Array.SorterGenericArray(keys, items, comparer);
				sorterGenericArray.Sort(index, length);
			}
		}

		// Token: 0x06000300 RID: 768
		[SecurityCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZSort(Array keys, Array items, int left, int right);

		/// <summary>Sorts the elements in an entire <see cref="T:System.Array" /> using the <see cref="T:System.IComparable`1" /> generic interface implementation of each element of the <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to sort.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000301 RID: 769 RVA: 0x00007972 File Offset: 0x00005B72
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T>(array, array.GetLowerBound(0), array.Length, null);
		}

		/// <summary>Sorts a pair of <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the <see cref="T:System.IComparable`1" /> generic interface implementation of each key.</summary>
		/// <param name="keys">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the items that correspond to the keys in <paramref name="keys" />, or <see langword="null" /> to sort only <paramref name="keys" />.</param>
		/// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
		/// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not <see langword="null" />, and the lower bound of <paramref name="keys" /> does not match the lower bound of <paramref name="items" />.  
		/// -or-  
		/// <paramref name="items" /> is not <see langword="null" />, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000302 RID: 770 RVA: 0x00007993 File Offset: 0x00005B93
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, null);
		}

		/// <summary>Sorts the elements in a range of elements in an <see cref="T:System.Array" /> using the <see cref="T:System.IComparable`1" /> generic interface implementation of each element of the <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to sort</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000303 RID: 771 RVA: 0x000079AF File Offset: 0x00005BAF
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, int index, int length)
		{
			Array.Sort<T>(array, index, length, null);
		}

		/// <summary>Sorts a range of elements in a pair of <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the <see cref="T:System.IComparable`1" /> generic interface implementation of each key.</summary>
		/// <param name="keys">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the items that correspond to the keys in <paramref name="keys" />, or <see langword="null" /> to sort only <paramref name="keys" />.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
		/// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="keys" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not <see langword="null" />, and the lower bound of <paramref name="keys" /> does not match the lower bound of <paramref name="items" />.  
		/// -or-  
		/// <paramref name="items" /> is not <see langword="null" />, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="keys" /><see cref="T:System.Array" />.  
		/// -or-  
		/// <paramref name="items" /> is not <see langword="null" />, and <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="items" /><see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000304 RID: 772 RVA: 0x000079BA File Offset: 0x00005BBA
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length)
		{
			Array.Sort<TKey, TValue>(keys, items, index, length, null);
		}

		/// <summary>Sorts the elements in an <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <param name="array">The one-dimensional, zero-base <see cref="T:System.Array" /> to sort</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface implementation to use when comparing elements, or <see langword="null" /> to use the <see cref="T:System.IComparable`1" /> generic interface implementation of each element.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and one or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		/// <exception cref="T:System.ArgumentException">The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		// Token: 0x06000305 RID: 773 RVA: 0x000079C6 File Offset: 0x00005BC6
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T>(array, 0, array.Length, comparer);
		}

		/// <summary>Sorts a pair of <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <param name="keys">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the items that correspond to the keys in <paramref name="keys" />, or <see langword="null" /> to sort only <paramref name="keys" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface implementation to use when comparing elements, or <see langword="null" /> to use the <see cref="T:System.IComparable`1" /> generic interface implementation of each element.</param>
		/// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
		/// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not <see langword="null" />, and the lower bound of <paramref name="keys" /> does not match the lower bound of <paramref name="items" />.  
		/// -or-  
		/// <paramref name="items" /> is not <see langword="null" />, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.  
		/// -or-  
		/// The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and one or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000306 RID: 774 RVA: 0x000079E1 File Offset: 0x00005BE1
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, comparer);
		}

		/// <summary>Sorts the elements in a range of elements in an <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to sort.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface implementation to use when comparing elements, or <see langword="null" /> to use the <see cref="T:System.IComparable`1" /> generic interface implementation of each element.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.  
		/// -or-  
		/// The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and one or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000307 RID: 775 RVA: 0x00007A00 File Offset: 0x00005C00
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, int index, int length, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (length > 1)
			{
				if ((comparer == null || comparer == Comparer<T>.Default) && Array.TrySZSort(array, null, index, index + length - 1))
				{
					return;
				}
				ArraySortHelper<T>.Default.Sort(array, index, length, comparer);
			}
		}

		/// <summary>Sorts a range of elements in a pair of <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <param name="keys">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the items that correspond to the keys in <paramref name="keys" />, or <see langword="null" /> to sort only <paramref name="keys" />.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface implementation to use when comparing elements, or <see langword="null" /> to use the <see cref="T:System.IComparable`1" /> generic interface implementation of each element.</param>
		/// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
		/// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="keys" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not <see langword="null" />, and the lower bound of <paramref name="keys" /> does not match the lower bound of <paramref name="items" />.  
		/// -or-  
		/// <paramref name="items" /> is not <see langword="null" />, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="keys" /><see cref="T:System.Array" />.  
		/// -or-  
		/// <paramref name="items" /> is not <see langword="null" />, and <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="items" /><see cref="T:System.Array" />.  
		/// -or-  
		/// The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and one or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000308 RID: 776 RVA: 0x00007A88 File Offset: 0x00005C88
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (keys.Length - index < length || (items != null && index > items.Length - length))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (length > 1)
			{
				if ((comparer == null || comparer == Comparer<TKey>.Default) && Array.TrySZSort(keys, items, index, index + length - 1))
				{
					return;
				}
				if (items == null)
				{
					Array.Sort<TKey>(keys, index, length, comparer);
					return;
				}
				ArraySortHelper<TKey, TValue>.Default.Sort(keys, items, index, length, comparer);
			}
		}

		/// <summary>Sorts the elements in an <see cref="T:System.Array" /> using the specified <see cref="T:System.Comparison`1" />.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to sort</param>
		/// <param name="comparison">The <see cref="T:System.Comparison`1" /> to use when comparing elements.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="comparison" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The implementation of <paramref name="comparison" /> caused an error during the sort. For example, <paramref name="comparison" /> might not return 0 when comparing an item with itself.</exception>
		// Token: 0x06000309 RID: 777 RVA: 0x00007B30 File Offset: 0x00005D30
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, Comparison<T> comparison)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			IComparer<T> comparer = new Array.FunctorComparer<T>(comparison);
			Array.Sort<T>(array, comparer);
		}

		/// <summary>Determines whether every element in the array matches the conditions defined by the specified predicate.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to check against the conditions.</param>
		/// <param name="match">The predicate that defines the conditions to check against the elements.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <returns>
		///   <see langword="true" /> if every element in <paramref name="array" /> matches the conditions defined by the specified predicate; otherwise, <see langword="false" />. If there are no elements in the array, the return value is <see langword="true" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x0600030A RID: 778 RVA: 0x00007B68 File Offset: 0x00005D68
		[__DynamicallyInvokable]
		public static bool TrueForAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!match(array[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Initializes every element of the value-type <see cref="T:System.Array" /> by calling the default constructor of the value type.</summary>
		// Token: 0x0600030B RID: 779
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Initialize();

		// Token: 0x040001EE RID: 494
		internal const int MaxArrayLength = 2146435071;

		// Token: 0x040001EF RID: 495
		internal const int MaxByteArrayLength = 2147483591;

		// Token: 0x02000ABE RID: 2750
		internal sealed class FunctorComparer<T> : IComparer<T>
		{
			// Token: 0x060069B2 RID: 27058 RVA: 0x0016D0AC File Offset: 0x0016B2AC
			public FunctorComparer(Comparison<T> comparison)
			{
				this.comparison = comparison;
			}

			// Token: 0x060069B3 RID: 27059 RVA: 0x0016D0BB File Offset: 0x0016B2BB
			public int Compare(T x, T y)
			{
				return this.comparison(x, y);
			}

			// Token: 0x040030CE RID: 12494
			private Comparison<T> comparison;
		}

		// Token: 0x02000ABF RID: 2751
		private struct SorterObjectArray
		{
			// Token: 0x060069B4 RID: 27060 RVA: 0x0016D0CA File Offset: 0x0016B2CA
			internal SorterObjectArray(object[] keys, object[] items, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x060069B5 RID: 27061 RVA: 0x0016D0EC File Offset: 0x0016B2EC
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys[a], this.keys[b]) > 0)
				{
					object obj = this.keys[a];
					this.keys[a] = this.keys[b];
					this.keys[b] = obj;
					if (this.items != null)
					{
						object obj2 = this.items[a];
						this.items[a] = this.items[b];
						this.items[b] = obj2;
					}
				}
			}

			// Token: 0x060069B6 RID: 27062 RVA: 0x0016D168 File Offset: 0x0016B368
			private void Swap(int i, int j)
			{
				object obj = this.keys[i];
				this.keys[i] = this.keys[j];
				this.keys[j] = obj;
				if (this.items != null)
				{
					object obj2 = this.items[i];
					this.items[i] = this.items[j];
					this.items[j] = obj2;
				}
			}

			// Token: 0x060069B7 RID: 27063 RVA: 0x0016D1C1 File Offset: 0x0016B3C1
			internal void Sort(int left, int length)
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					this.IntrospectiveSort(left, length);
					return;
				}
				this.DepthLimitedQuickSort(left, length + left - 1, 32);
			}

			// Token: 0x060069B8 RID: 27064 RVA: 0x0016D1E4 File Offset: 0x0016B3E4
			private void DepthLimitedQuickSort(int left, int right, int depthLimit)
			{
				do
				{
					if (depthLimit == 0)
					{
						try
						{
							this.Heapsort(left, right);
							break;
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[] { this.comparer }));
						}
						catch (Exception ex)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
						}
					}
					int num = left;
					int num2 = right;
					int median = Array.GetMedian(num, num2);
					try
					{
						this.SwapIfGreaterWithItems(num, median);
						this.SwapIfGreaterWithItems(num, num2);
						this.SwapIfGreaterWithItems(median, num2);
					}
					catch (Exception ex2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex2);
					}
					object obj = this.keys[median];
					do
					{
						try
						{
							while (this.comparer.Compare(this.keys[num], obj) < 0)
							{
								num++;
							}
							while (this.comparer.Compare(obj, this.keys[num2]) < 0)
							{
								num2--;
							}
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[] { this.comparer }));
						}
						catch (Exception ex3)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex3);
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							object obj2 = this.keys[num];
							this.keys[num] = this.keys[num2];
							this.keys[num2] = obj2;
							if (this.items != null)
							{
								object obj3 = this.items[num];
								this.items[num] = this.items[num2];
								this.items[num2] = obj3;
							}
						}
						num++;
						num2--;
					}
					while (num <= num2);
					depthLimit--;
					if (num2 - left <= right - num)
					{
						if (left < num2)
						{
							this.DepthLimitedQuickSort(left, num2, depthLimit);
						}
						left = num;
					}
					else
					{
						if (num < right)
						{
							this.DepthLimitedQuickSort(num, right, depthLimit);
						}
						right = num2;
					}
				}
				while (left < right);
			}

			// Token: 0x060069B9 RID: 27065 RVA: 0x0016D3C8 File Offset: 0x0016B5C8
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(this.keys.Length));
				}
				catch (IndexOutOfRangeException)
				{
					IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(this.comparer);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
				}
			}

			// Token: 0x060069BA RID: 27066 RVA: 0x0016D434 File Offset: 0x0016B634
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 1)
						{
							return;
						}
						if (num == 2)
						{
							this.SwapIfGreaterWithItems(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreaterWithItems(lo, hi - 1);
							this.SwapIfGreaterWithItems(lo, hi);
							this.SwapIfGreaterWithItems(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x060069BB RID: 27067 RVA: 0x0016D4B8 File Offset: 0x0016B6B8
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreaterWithItems(lo, num);
				this.SwapIfGreaterWithItems(lo, hi);
				this.SwapIfGreaterWithItems(num, hi);
				object obj = this.keys[num];
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys[++i], obj) < 0)
					{
					}
					while (this.comparer.Compare(obj, this.keys[--num2]) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				this.Swap(i, hi - 1);
				return i;
			}

			// Token: 0x060069BC RID: 27068 RVA: 0x0016D554 File Offset: 0x0016B754
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x060069BD RID: 27069 RVA: 0x0016D5A4 File Offset: 0x0016B7A4
			private void DownHeap(int i, int n, int lo)
			{
				object obj = this.keys[lo + i - 1];
				object obj2 = ((this.items != null) ? this.items[lo + i - 1] : null);
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys[lo + num - 1], this.keys[lo + num]) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(obj, this.keys[lo + num - 1]) >= 0)
					{
						break;
					}
					this.keys[lo + i - 1] = this.keys[lo + num - 1];
					if (this.items != null)
					{
						this.items[lo + i - 1] = this.items[lo + num - 1];
					}
					i = num;
				}
				this.keys[lo + i - 1] = obj;
				if (this.items != null)
				{
					this.items[lo + i - 1] = obj2;
				}
			}

			// Token: 0x060069BE RID: 27070 RVA: 0x0016D68C File Offset: 0x0016B88C
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object obj = this.keys[i + 1];
					object obj2 = ((this.items != null) ? this.items[i + 1] : null);
					while (num >= lo && this.comparer.Compare(obj, this.keys[num]) < 0)
					{
						this.keys[num + 1] = this.keys[num];
						if (this.items != null)
						{
							this.items[num + 1] = this.items[num];
						}
						num--;
					}
					this.keys[num + 1] = obj;
					if (this.items != null)
					{
						this.items[num + 1] = obj2;
					}
				}
			}

			// Token: 0x040030CF RID: 12495
			private object[] keys;

			// Token: 0x040030D0 RID: 12496
			private object[] items;

			// Token: 0x040030D1 RID: 12497
			private IComparer comparer;
		}

		// Token: 0x02000AC0 RID: 2752
		private struct SorterGenericArray
		{
			// Token: 0x060069BF RID: 27071 RVA: 0x0016D739 File Offset: 0x0016B939
			internal SorterGenericArray(Array keys, Array items, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x060069C0 RID: 27072 RVA: 0x0016D75C File Offset: 0x0016B95C
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys.GetValue(a), this.keys.GetValue(b)) > 0)
				{
					object value = this.keys.GetValue(a);
					this.keys.SetValue(this.keys.GetValue(b), a);
					this.keys.SetValue(value, b);
					if (this.items != null)
					{
						object value2 = this.items.GetValue(a);
						this.items.SetValue(this.items.GetValue(b), a);
						this.items.SetValue(value2, b);
					}
				}
			}

			// Token: 0x060069C1 RID: 27073 RVA: 0x0016D804 File Offset: 0x0016BA04
			private void Swap(int i, int j)
			{
				object value = this.keys.GetValue(i);
				this.keys.SetValue(this.keys.GetValue(j), i);
				this.keys.SetValue(value, j);
				if (this.items != null)
				{
					object value2 = this.items.GetValue(i);
					this.items.SetValue(this.items.GetValue(j), i);
					this.items.SetValue(value2, j);
				}
			}

			// Token: 0x060069C2 RID: 27074 RVA: 0x0016D87D File Offset: 0x0016BA7D
			internal void Sort(int left, int length)
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					this.IntrospectiveSort(left, length);
					return;
				}
				this.DepthLimitedQuickSort(left, length + left - 1, 32);
			}

			// Token: 0x060069C3 RID: 27075 RVA: 0x0016D8A0 File Offset: 0x0016BAA0
			private void DepthLimitedQuickSort(int left, int right, int depthLimit)
			{
				do
				{
					if (depthLimit == 0)
					{
						try
						{
							this.Heapsort(left, right);
							break;
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[] { this.comparer }));
						}
						catch (Exception ex)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
						}
					}
					int num = left;
					int num2 = right;
					int median = Array.GetMedian(num, num2);
					try
					{
						this.SwapIfGreaterWithItems(num, median);
						this.SwapIfGreaterWithItems(num, num2);
						this.SwapIfGreaterWithItems(median, num2);
					}
					catch (Exception ex2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex2);
					}
					object value = this.keys.GetValue(median);
					do
					{
						try
						{
							while (this.comparer.Compare(this.keys.GetValue(num), value) < 0)
							{
								num++;
							}
							while (this.comparer.Compare(value, this.keys.GetValue(num2)) < 0)
							{
								num2--;
							}
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[] { this.comparer }));
						}
						catch (Exception ex3)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex3);
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							object value2 = this.keys.GetValue(num);
							this.keys.SetValue(this.keys.GetValue(num2), num);
							this.keys.SetValue(value2, num2);
							if (this.items != null)
							{
								object value3 = this.items.GetValue(num);
								this.items.SetValue(this.items.GetValue(num2), num);
								this.items.SetValue(value3, num2);
							}
						}
						if (num != 2147483647)
						{
							num++;
						}
						if (num2 != -2147483648)
						{
							num2--;
						}
					}
					while (num <= num2);
					depthLimit--;
					if (num2 - left <= right - num)
					{
						if (left < num2)
						{
							this.DepthLimitedQuickSort(left, num2, depthLimit);
						}
						left = num;
					}
					else
					{
						if (num < right)
						{
							this.DepthLimitedQuickSort(num, right, depthLimit);
						}
						right = num2;
					}
				}
				while (left < right);
			}

			// Token: 0x060069C4 RID: 27076 RVA: 0x0016DAC4 File Offset: 0x0016BCC4
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(this.keys.Length));
				}
				catch (IndexOutOfRangeException)
				{
					IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(this.comparer);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
				}
			}

			// Token: 0x060069C5 RID: 27077 RVA: 0x0016DB34 File Offset: 0x0016BD34
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 1)
						{
							return;
						}
						if (num == 2)
						{
							this.SwapIfGreaterWithItems(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreaterWithItems(lo, hi - 1);
							this.SwapIfGreaterWithItems(lo, hi);
							this.SwapIfGreaterWithItems(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x060069C6 RID: 27078 RVA: 0x0016DBB8 File Offset: 0x0016BDB8
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreaterWithItems(lo, num);
				this.SwapIfGreaterWithItems(lo, hi);
				this.SwapIfGreaterWithItems(num, hi);
				object value = this.keys.GetValue(num);
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys.GetValue(++i), value) < 0)
					{
					}
					while (this.comparer.Compare(value, this.keys.GetValue(--num2)) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				this.Swap(i, hi - 1);
				return i;
			}

			// Token: 0x060069C7 RID: 27079 RVA: 0x0016DC60 File Offset: 0x0016BE60
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x060069C8 RID: 27080 RVA: 0x0016DCB0 File Offset: 0x0016BEB0
			private void DownHeap(int i, int n, int lo)
			{
				object value = this.keys.GetValue(lo + i - 1);
				object obj = ((this.items != null) ? this.items.GetValue(lo + i - 1) : null);
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys.GetValue(lo + num - 1), this.keys.GetValue(lo + num)) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(value, this.keys.GetValue(lo + num - 1)) >= 0)
					{
						break;
					}
					this.keys.SetValue(this.keys.GetValue(lo + num - 1), lo + i - 1);
					if (this.items != null)
					{
						this.items.SetValue(this.items.GetValue(lo + num - 1), lo + i - 1);
					}
					i = num;
				}
				this.keys.SetValue(value, lo + i - 1);
				if (this.items != null)
				{
					this.items.SetValue(obj, lo + i - 1);
				}
			}

			// Token: 0x060069C9 RID: 27081 RVA: 0x0016DDC4 File Offset: 0x0016BFC4
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object value = this.keys.GetValue(i + 1);
					object obj = ((this.items != null) ? this.items.GetValue(i + 1) : null);
					while (num >= lo && this.comparer.Compare(value, this.keys.GetValue(num)) < 0)
					{
						this.keys.SetValue(this.keys.GetValue(num), num + 1);
						if (this.items != null)
						{
							this.items.SetValue(this.items.GetValue(num), num + 1);
						}
						num--;
					}
					this.keys.SetValue(value, num + 1);
					if (this.items != null)
					{
						this.items.SetValue(obj, num + 1);
					}
				}
			}

			// Token: 0x040030D2 RID: 12498
			private Array keys;

			// Token: 0x040030D3 RID: 12499
			private Array items;

			// Token: 0x040030D4 RID: 12500
			private IComparer comparer;
		}

		// Token: 0x02000AC1 RID: 2753
		[Serializable]
		private sealed class SZArrayEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x060069CA RID: 27082 RVA: 0x0016DE95 File Offset: 0x0016C095
			internal SZArrayEnumerator(Array array)
			{
				this._array = array;
				this._index = -1;
				this._endIndex = array.Length;
			}

			// Token: 0x060069CB RID: 27083 RVA: 0x0016DEB7 File Offset: 0x0016C0B7
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x060069CC RID: 27084 RVA: 0x0016DEBF File Offset: 0x0016C0BF
			public bool MoveNext()
			{
				if (this._index < this._endIndex)
				{
					this._index++;
					return this._index < this._endIndex;
				}
				return false;
			}

			// Token: 0x170011E7 RID: 4583
			// (get) Token: 0x060069CD RID: 27085 RVA: 0x0016DEF0 File Offset: 0x0016C0F0
			public object Current
			{
				get
				{
					if (this._index < 0)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._index >= this._endIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this._array.GetValue(this._index);
				}
			}

			// Token: 0x060069CE RID: 27086 RVA: 0x0016DF45 File Offset: 0x0016C145
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x040030D5 RID: 12501
			private Array _array;

			// Token: 0x040030D6 RID: 12502
			private int _index;

			// Token: 0x040030D7 RID: 12503
			private int _endIndex;
		}

		// Token: 0x02000AC2 RID: 2754
		[Serializable]
		private sealed class ArrayEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x060069CF RID: 27087 RVA: 0x0016DF50 File Offset: 0x0016C150
			internal ArrayEnumerator(Array array, int index, int count)
			{
				this.array = array;
				this.index = index - 1;
				this.startIndex = index;
				this.endIndex = index + count;
				this._indices = new int[array.Rank];
				int num = 1;
				for (int i = 0; i < array.Rank; i++)
				{
					this._indices[i] = array.GetLowerBound(i);
					num *= array.GetLength(i);
				}
				this._indices[this._indices.Length - 1]--;
				this._complete = num == 0;
			}

			// Token: 0x060069D0 RID: 27088 RVA: 0x0016DFE4 File Offset: 0x0016C1E4
			private void IncArray()
			{
				int rank = this.array.Rank;
				this._indices[rank - 1]++;
				for (int i = rank - 1; i >= 0; i--)
				{
					if (this._indices[i] > this.array.GetUpperBound(i))
					{
						if (i == 0)
						{
							this._complete = true;
							return;
						}
						for (int j = i; j < rank; j++)
						{
							this._indices[j] = this.array.GetLowerBound(j);
						}
						this._indices[i - 1]++;
					}
				}
			}

			// Token: 0x060069D1 RID: 27089 RVA: 0x0016E072 File Offset: 0x0016C272
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x060069D2 RID: 27090 RVA: 0x0016E07A File Offset: 0x0016C27A
			public bool MoveNext()
			{
				if (this._complete)
				{
					this.index = this.endIndex;
					return false;
				}
				this.index++;
				this.IncArray();
				return !this._complete;
			}

			// Token: 0x170011E8 RID: 4584
			// (get) Token: 0x060069D3 RID: 27091 RVA: 0x0016E0B0 File Offset: 0x0016C2B0
			public object Current
			{
				get
				{
					if (this.index < this.startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._complete)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this.array.GetValue(this._indices);
				}
			}

			// Token: 0x060069D4 RID: 27092 RVA: 0x0016E104 File Offset: 0x0016C304
			public void Reset()
			{
				this.index = this.startIndex - 1;
				int num = 1;
				for (int i = 0; i < this.array.Rank; i++)
				{
					this._indices[i] = this.array.GetLowerBound(i);
					num *= this.array.GetLength(i);
				}
				this._complete = num == 0;
				this._indices[this._indices.Length - 1]--;
			}

			// Token: 0x040030D8 RID: 12504
			private Array array;

			// Token: 0x040030D9 RID: 12505
			private int index;

			// Token: 0x040030DA RID: 12506
			private int endIndex;

			// Token: 0x040030DB RID: 12507
			private int startIndex;

			// Token: 0x040030DC RID: 12508
			private int[] _indices;

			// Token: 0x040030DD RID: 12509
			private bool _complete;
		}
	}
}
