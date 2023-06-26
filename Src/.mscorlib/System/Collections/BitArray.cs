using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections
{
	/// <summary>Manages a compact array of bit values, which are represented as Booleans, where <see langword="true" /> indicates that the bit is on (1) and <see langword="false" /> indicates the bit is off (0).</summary>
	// Token: 0x0200048F RID: 1167
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class BitArray : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x0600381F RID: 14367 RVA: 0x000D8789 File Offset: 0x000D6989
		private BitArray()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that can hold the specified number of bit values, which are initially set to <see langword="false" />.</summary>
		/// <param name="length">The number of bit values in the new <see cref="T:System.Collections.BitArray" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero.</exception>
		// Token: 0x06003820 RID: 14368 RVA: 0x000D8791 File Offset: 0x000D6991
		[__DynamicallyInvokable]
		public BitArray(int length)
			: this(length, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that can hold the specified number of bit values, which are initially set to the specified value.</summary>
		/// <param name="length">The number of bit values in the new <see cref="T:System.Collections.BitArray" />.</param>
		/// <param name="defaultValue">The Boolean value to assign to each bit.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero.</exception>
		// Token: 0x06003821 RID: 14369 RVA: 0x000D879C File Offset: 0x000D699C
		[__DynamicallyInvokable]
		public BitArray(int length, bool defaultValue)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_array = new int[BitArray.GetArrayLength(length, 32)];
			this.m_length = length;
			int num = (defaultValue ? (-1) : 0);
			for (int i = 0; i < this.m_array.Length; i++)
			{
				this.m_array[i] = num;
			}
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified array of bytes.</summary>
		/// <param name="bytes">An array of bytes containing the values to copy, where each byte represents eight consecutive bits.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="bytes" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003822 RID: 14370 RVA: 0x000D8810 File Offset: 0x000D6A10
		[__DynamicallyInvokable]
		public BitArray(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (bytes.Length > 268435455)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ArrayTooLarge", new object[] { 8 }), "bytes");
			}
			this.m_array = new int[BitArray.GetArrayLength(bytes.Length, 4)];
			this.m_length = bytes.Length * 8;
			int num = 0;
			int num2 = 0;
			while (bytes.Length - num2 >= 4)
			{
				this.m_array[num++] = (int)(bytes[num2] & byte.MaxValue) | ((int)(bytes[num2 + 1] & byte.MaxValue) << 8) | ((int)(bytes[num2 + 2] & byte.MaxValue) << 16) | ((int)(bytes[num2 + 3] & byte.MaxValue) << 24);
				num2 += 4;
			}
			switch (bytes.Length - num2)
			{
			case 1:
				goto IL_103;
			case 2:
				break;
			case 3:
				this.m_array[num] = (int)(bytes[num2 + 2] & byte.MaxValue) << 16;
				break;
			default:
				goto IL_11C;
			}
			this.m_array[num] |= (int)(bytes[num2 + 1] & byte.MaxValue) << 8;
			IL_103:
			this.m_array[num] |= (int)(bytes[num2] & byte.MaxValue);
			IL_11C:
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified array of Booleans.</summary>
		/// <param name="values">An array of Booleans to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is <see langword="null" />.</exception>
		// Token: 0x06003823 RID: 14371 RVA: 0x000D8940 File Offset: 0x000D6B40
		[__DynamicallyInvokable]
		public BitArray(bool[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.m_array = new int[BitArray.GetArrayLength(values.Length, 32)];
			this.m_length = values.Length;
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i])
				{
					this.m_array[i / 32] |= 1 << i % 32;
				}
			}
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified array of 32-bit integers.</summary>
		/// <param name="values">An array of integers containing the values to copy, where each integer represents 32 consecutive bits.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="values" /> is greater than <see cref="F:System.Int32.MaxValue" /></exception>
		// Token: 0x06003824 RID: 14372 RVA: 0x000D89B8 File Offset: 0x000D6BB8
		[__DynamicallyInvokable]
		public BitArray(int[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length > 67108863)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ArrayTooLarge", new object[] { 32 }), "values");
			}
			this.m_array = new int[values.Length];
			this.m_length = values.Length * 32;
			Array.Copy(values, this.m_array, values.Length);
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified <see cref="T:System.Collections.BitArray" />.</summary>
		/// <param name="bits">The <see cref="T:System.Collections.BitArray" /> to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bits" /> is <see langword="null" />.</exception>
		// Token: 0x06003825 RID: 14373 RVA: 0x000D8A38 File Offset: 0x000D6C38
		[__DynamicallyInvokable]
		public BitArray(BitArray bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			int arrayLength = BitArray.GetArrayLength(bits.m_length, 32);
			this.m_array = new int[arrayLength];
			this.m_length = bits.m_length;
			Array.Copy(bits.m_array, this.m_array, arrayLength);
			this._version = bits._version;
		}

		/// <summary>Gets or sets the value of the bit at a specific position in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <param name="index">The zero-based index of the value to get or set.</param>
		/// <returns>The value of the bit at position <paramref name="index" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.BitArray.Count" />.</exception>
		// Token: 0x17000844 RID: 2116
		[__DynamicallyInvokable]
		public bool this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Get(index);
			}
			[__DynamicallyInvokable]
			set
			{
				this.Set(index, value);
			}
		}

		/// <summary>Gets the value of the bit at a specific position in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <param name="index">The zero-based index of the value to get.</param>
		/// <returns>The value of the bit at position <paramref name="index" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than or equal to the number of elements in the <see cref="T:System.Collections.BitArray" />.</exception>
		// Token: 0x06003828 RID: 14376 RVA: 0x000D8AB0 File Offset: 0x000D6CB0
		[__DynamicallyInvokable]
		public bool Get(int index)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return (this.m_array[index / 32] & (1 << index % 32)) != 0;
		}

		/// <summary>Sets the bit at a specific position in the <see cref="T:System.Collections.BitArray" /> to the specified value.</summary>
		/// <param name="index">The zero-based index of the bit to set.</param>
		/// <param name="value">The Boolean value to assign to the bit.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than or equal to the number of elements in the <see cref="T:System.Collections.BitArray" />.</exception>
		// Token: 0x06003829 RID: 14377 RVA: 0x000D8AEC File Offset: 0x000D6CEC
		[__DynamicallyInvokable]
		public void Set(int index, bool value)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (value)
			{
				this.m_array[index / 32] |= 1 << index % 32;
			}
			else
			{
				this.m_array[index / 32] &= ~(1 << index % 32);
			}
			this._version++;
		}

		/// <summary>Sets all bits in the <see cref="T:System.Collections.BitArray" /> to the specified value.</summary>
		/// <param name="value">The Boolean value to assign to all bits.</param>
		// Token: 0x0600382A RID: 14378 RVA: 0x000D8B68 File Offset: 0x000D6D68
		[__DynamicallyInvokable]
		public void SetAll(bool value)
		{
			int num = (value ? (-1) : 0);
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] = num;
			}
			this._version++;
		}

		/// <summary>Performs the bitwise AND operation between the elements of the current <see cref="T:System.Collections.BitArray" /> object and the corresponding elements in the specified array. The current <see cref="T:System.Collections.BitArray" /> object will be modified to store the result of the bitwise AND operation.</summary>
		/// <param name="value">The array with which to perform the bitwise AND operation.</param>
		/// <returns>An array containing the result of the bitwise AND operation, which is a reference to the current <see cref="T:System.Collections.BitArray" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements.</exception>
		// Token: 0x0600382B RID: 14379 RVA: 0x000D8BB0 File Offset: 0x000D6DB0
		[__DynamicallyInvokable]
		public BitArray And(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] &= value.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Performs the bitwise OR operation between the elements of the current <see cref="T:System.Collections.BitArray" /> object and the corresponding elements in the specified array. The current <see cref="T:System.Collections.BitArray" /> object will be modified to store the result of the bitwise OR operation.</summary>
		/// <param name="value">The array with which to perform the bitwise OR operation.</param>
		/// <returns>An array containing the result of the bitwise OR operation, which is a reference to the current <see cref="T:System.Collections.BitArray" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements.</exception>
		// Token: 0x0600382C RID: 14380 RVA: 0x000D8C2C File Offset: 0x000D6E2C
		[__DynamicallyInvokable]
		public BitArray Or(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] |= value.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Performs the bitwise exclusive OR operation between the elements of the current <see cref="T:System.Collections.BitArray" /> object against the corresponding elements in the specified array. The current <see cref="T:System.Collections.BitArray" /> object will be modified to store the result of the bitwise exclusive OR operation.</summary>
		/// <param name="value">The array with which to perform the bitwise exclusive OR operation.</param>
		/// <returns>An array containing the result of the bitwise exclusive OR operation, which is a reference to the current <see cref="T:System.Collections.BitArray" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements.</exception>
		// Token: 0x0600382D RID: 14381 RVA: 0x000D8CA8 File Offset: 0x000D6EA8
		[__DynamicallyInvokable]
		public BitArray Xor(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] ^= value.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Inverts all the bit values in the current <see cref="T:System.Collections.BitArray" />, so that elements set to <see langword="true" /> are changed to <see langword="false" />, and elements set to <see langword="false" /> are changed to <see langword="true" />.</summary>
		/// <returns>The current instance with inverted bit values.</returns>
		// Token: 0x0600382E RID: 14382 RVA: 0x000D8D24 File Offset: 0x000D6F24
		[__DynamicallyInvokable]
		public BitArray Not()
		{
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] = ~this.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Gets or sets the number of elements in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Collections.BitArray" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a value that is less than zero.</exception>
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600382F RID: 14383 RVA: 0x000D8D6B File Offset: 0x000D6F6B
		// (set) Token: 0x06003830 RID: 14384 RVA: 0x000D8D74 File Offset: 0x000D6F74
		[__DynamicallyInvokable]
		public int Length
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_length;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				int arrayLength = BitArray.GetArrayLength(value, 32);
				if (arrayLength > this.m_array.Length || arrayLength + 256 < this.m_array.Length)
				{
					int[] array = new int[arrayLength];
					Array.Copy(this.m_array, array, (arrayLength > this.m_array.Length) ? this.m_array.Length : arrayLength);
					this.m_array = array;
				}
				if (value > this.m_length)
				{
					int num = BitArray.GetArrayLength(this.m_length, 32) - 1;
					int num2 = this.m_length % 32;
					if (num2 > 0)
					{
						this.m_array[num] &= (1 << num2) - 1;
					}
					Array.Clear(this.m_array, num + 1, arrayLength - num - 1);
				}
				this.m_length = value;
				this._version++;
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.BitArray" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.BitArray" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.BitArray" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.BitArray" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003831 RID: 14385 RVA: 0x000D8E58 File Offset: 0x000D7058
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (array is int[])
			{
				Array.Copy(this.m_array, 0, array, index, BitArray.GetArrayLength(this.m_length, 32));
				return;
			}
			if (array is byte[])
			{
				int arrayLength = BitArray.GetArrayLength(this.m_length, 8);
				if (array.Length - index < arrayLength)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				byte[] array2 = (byte[])array;
				for (int i = 0; i < arrayLength; i++)
				{
					array2[index + i] = (byte)((this.m_array[i / 4] >> i % 4 * 8) & 255);
				}
				return;
			}
			else
			{
				if (!(array is bool[]))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_BitArrayTypeUnsupported"));
				}
				if (array.Length - index < this.m_length)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				bool[] array3 = (bool[])array;
				for (int j = 0; j < this.m_length; j++)
				{
					array3[index + j] = ((this.m_array[j / 32] >> j % 32) & 1) != 0;
				}
				return;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.BitArray" />.</returns>
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06003832 RID: 14386 RVA: 0x000D8FA0 File Offset: 0x000D71A0
		public int Count
		{
			get
			{
				return this.m_length;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.BitArray" />.</returns>
		// Token: 0x06003833 RID: 14387 RVA: 0x000D8FA8 File Offset: 0x000D71A8
		public object Clone()
		{
			return new BitArray(this.m_array)
			{
				_version = this._version,
				m_length = this.m_length
			};
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.BitArray" />.</returns>
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000D8FDA File Offset: 0x000D71DA
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.BitArray" /> is read-only.</summary>
		/// <returns>This property is always <see langword="false" />.</returns>
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06003835 RID: 14389 RVA: 0x000D8FFC File Offset: 0x000D71FC
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.BitArray" /> is synchronized (thread safe).</summary>
		/// <returns>This property is always <see langword="false" />.</returns>
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06003836 RID: 14390 RVA: 0x000D8FFF File Offset: 0x000D71FF
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Collections.BitArray" />.</returns>
		// Token: 0x06003837 RID: 14391 RVA: 0x000D9002 File Offset: 0x000D7202
		[__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return new BitArray.BitArrayEnumeratorSimple(this);
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000D900A File Offset: 0x000D720A
		private static int GetArrayLength(int n, int div)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / div + 1;
		}

		// Token: 0x040018D2 RID: 6354
		private const int BitsPerInt32 = 32;

		// Token: 0x040018D3 RID: 6355
		private const int BytesPerInt32 = 4;

		// Token: 0x040018D4 RID: 6356
		private const int BitsPerByte = 8;

		// Token: 0x040018D5 RID: 6357
		private int[] m_array;

		// Token: 0x040018D6 RID: 6358
		private int m_length;

		// Token: 0x040018D7 RID: 6359
		private int _version;

		// Token: 0x040018D8 RID: 6360
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040018D9 RID: 6361
		private const int _ShrinkThreshold = 256;

		// Token: 0x02000BA9 RID: 2985
		[Serializable]
		private class BitArrayEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x06006DDC RID: 28124 RVA: 0x0017CB47 File Offset: 0x0017AD47
			internal BitArrayEnumeratorSimple(BitArray bitarray)
			{
				this.bitarray = bitarray;
				this.index = -1;
				this.version = bitarray._version;
			}

			// Token: 0x06006DDD RID: 28125 RVA: 0x0017CB69 File Offset: 0x0017AD69
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006DDE RID: 28126 RVA: 0x0017CB74 File Offset: 0x0017AD74
			public virtual bool MoveNext()
			{
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.index < this.bitarray.Count - 1)
				{
					this.index++;
					this.currentElement = this.bitarray.Get(this.index);
					return true;
				}
				this.index = this.bitarray.Count;
				return false;
			}

			// Token: 0x1700129F RID: 4767
			// (get) Token: 0x06006DDF RID: 28127 RVA: 0x0017CBF4 File Offset: 0x0017ADF4
			public virtual object Current
			{
				get
				{
					if (this.index == -1)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this.index >= this.bitarray.Count)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this.currentElement;
				}
			}

			// Token: 0x06006DE0 RID: 28128 RVA: 0x0017CC48 File Offset: 0x0017AE48
			public void Reset()
			{
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.index = -1;
			}

			// Token: 0x04003558 RID: 13656
			private BitArray bitarray;

			// Token: 0x04003559 RID: 13657
			private int index;

			// Token: 0x0400355A RID: 13658
			private int version;

			// Token: 0x0400355B RID: 13659
			private bool currentElement;
		}
	}
}
