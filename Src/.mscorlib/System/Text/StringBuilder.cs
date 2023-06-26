using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	/// <summary>Represents a mutable string of characters. This class cannot be inherited.</summary>
	// Token: 0x02000A56 RID: 2646
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class StringBuilder : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class.</summary>
		// Token: 0x060066EE RID: 26350 RVA: 0x0015B9A4 File Offset: 0x00159BA4
		[__DynamicallyInvokable]
		public StringBuilder()
			: this(16)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified capacity.</summary>
		/// <param name="capacity">The suggested starting size of this instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x060066EF RID: 26351 RVA: 0x0015B9AE File Offset: 0x00159BAE
		[__DynamicallyInvokable]
		public StringBuilder(int capacity)
			: this(string.Empty, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified string.</summary>
		/// <param name="value">The string used to initialize the value of the instance. If <paramref name="value" /> is <see langword="null" />, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />).</param>
		// Token: 0x060066F0 RID: 26352 RVA: 0x0015B9BC File Offset: 0x00159BBC
		[__DynamicallyInvokable]
		public StringBuilder(string value)
			: this(value, 16)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified string and capacity.</summary>
		/// <param name="value">The string used to initialize the value of the instance. If <paramref name="value" /> is <see langword="null" />, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />).</param>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x060066F1 RID: 26353 RVA: 0x0015B9C7 File Offset: 0x00159BC7
		[__DynamicallyInvokable]
		public StringBuilder(string value, int capacity)
			: this(value, 0, (value != null) ? value.Length : 0, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class from the specified substring and capacity.</summary>
		/// <param name="value">The string that contains the substring used to initialize the value of this instance. If <paramref name="value" /> is <see langword="null" />, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />).</param>
		/// <param name="startIndex">The position within <paramref name="value" /> where the substring begins.</param>
		/// <param name="length">The number of characters in the substring.</param>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> plus <paramref name="length" /> is not a position within <paramref name="value" />.</exception>
		// Token: 0x060066F2 RID: 26354 RVA: 0x0015B9E0 File Offset: 0x00159BE0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe StringBuilder(string value, int startIndex, int length, int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", new object[] { "capacity" }));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", new object[] { "length" }));
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
			}
			if (value == null)
			{
				value = string.Empty;
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
			}
			this.m_MaxCapacity = int.MaxValue;
			if (capacity == 0)
			{
				capacity = 16;
			}
			if (capacity < length)
			{
				capacity = length;
			}
			this.m_ChunkChars = new char[capacity];
			this.m_ChunkLength = length;
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				StringBuilder.ThreadSafeCopy(ptr + startIndex, this.m_ChunkChars, 0, length);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class that starts with a specified capacity and can grow to a specified maximum.</summary>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <param name="maxCapacity">The maximum number of characters the current string can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxCapacity" /> is less than one, <paramref name="capacity" /> is less than zero, or <paramref name="capacity" /> is greater than <paramref name="maxCapacity" />.</exception>
		// Token: 0x060066F3 RID: 26355 RVA: 0x0015BADC File Offset: 0x00159CDC
		[__DynamicallyInvokable]
		public StringBuilder(int capacity, int maxCapacity)
		{
			if (capacity > maxCapacity)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
			}
			if (maxCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("maxCapacity", Environment.GetResourceString("ArgumentOutOfRange_SmallMaxCapacity"));
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", new object[] { "capacity" }));
			}
			if (capacity == 0)
			{
				capacity = Math.Min(16, maxCapacity);
			}
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkChars = new char[capacity];
		}

		// Token: 0x060066F4 RID: 26356 RVA: 0x0015BB68 File Offset: 0x00159D68
		[SecurityCritical]
		private StringBuilder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			int num = 0;
			string text = null;
			int num2 = int.MaxValue;
			bool flag = false;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "m_MaxCapacity"))
				{
					if (!(name == "m_StringValue"))
					{
						if (name == "Capacity")
						{
							num = info.GetInt32("Capacity");
							flag = true;
						}
					}
					else
					{
						text = info.GetString("m_StringValue");
					}
				}
				else
				{
					num2 = info.GetInt32("m_MaxCapacity");
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if (num2 < 1 || text.Length > num2)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderMaxCapacity"));
			}
			if (!flag)
			{
				num = 16;
				if (num < text.Length)
				{
					num = text.Length;
				}
				if (num > num2)
				{
					num = num2;
				}
			}
			if (num < 0 || num < text.Length || num > num2)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderCapacity"));
			}
			this.m_MaxCapacity = num2;
			this.m_ChunkChars = new char[num];
			text.CopyTo(0, this.m_ChunkChars, 0, text.Length);
			this.m_ChunkLength = text.Length;
			this.m_ChunkPrevious = null;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. Reserved for future use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060066F5 RID: 26357 RVA: 0x0015BCA4 File Offset: 0x00159EA4
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("m_MaxCapacity", this.m_MaxCapacity);
			info.AddValue("Capacity", this.Capacity);
			info.AddValue("m_StringValue", this.ToString());
			info.AddValue("m_currentThread", 0);
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x0015BD00 File Offset: 0x00159F00
		[Conditional("_DEBUG")]
		private void VerifyClassInvariant()
		{
			StringBuilder stringBuilder = this;
			int maxCapacity = this.m_MaxCapacity;
			for (;;)
			{
				StringBuilder chunkPrevious = stringBuilder.m_ChunkPrevious;
				if (chunkPrevious == null)
				{
					break;
				}
				stringBuilder = chunkPrevious;
			}
		}

		/// <summary>Gets or sets the maximum number of characters that can be contained in the memory allocated by the current instance.</summary>
		/// <returns>The maximum number of characters that can be contained in the memory allocated by the current instance. Its value can range from <see cref="P:System.Text.StringBuilder.Length" /> to <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than the current length of this instance.  
		///  -or-  
		///  The value specified for a set operation is greater than the maximum capacity.</exception>
		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x060066F7 RID: 26359 RVA: 0x0015BD24 File Offset: 0x00159F24
		// (set) Token: 0x060066F8 RID: 26360 RVA: 0x0015BD38 File Offset: 0x00159F38
		[__DynamicallyInvokable]
		public int Capacity
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_ChunkChars.Length + this.m_ChunkOffset;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
				}
				if (value < this.Length)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
				}
				if (this.Capacity != value)
				{
					int num = value - this.m_ChunkOffset;
					char[] array = new char[num];
					Array.Copy(this.m_ChunkChars, array, this.m_ChunkLength);
					this.m_ChunkChars = array;
				}
			}
		}

		/// <summary>Gets the maximum capacity of this instance.</summary>
		/// <returns>The maximum number of characters this instance can hold.</returns>
		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x060066F9 RID: 26361 RVA: 0x0015BDCC File Offset: 0x00159FCC
		[__DynamicallyInvokable]
		public int MaxCapacity
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_MaxCapacity;
			}
		}

		/// <summary>Ensures that the capacity of this instance of <see cref="T:System.Text.StringBuilder" /> is at least the specified value.</summary>
		/// <param name="capacity">The minimum capacity to ensure.</param>
		/// <returns>The new capacity of this instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060066FA RID: 26362 RVA: 0x0015BDD4 File Offset: 0x00159FD4
		[__DynamicallyInvokable]
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
			}
			if (this.Capacity < capacity)
			{
				this.Capacity = capacity;
			}
			return this.Capacity;
		}

		/// <summary>Converts the value of this instance to a <see cref="T:System.String" />.</summary>
		/// <returns>A string whose value is the same as this instance.</returns>
		// Token: 0x060066FB RID: 26363 RVA: 0x0015BE08 File Offset: 0x0015A008
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override string ToString()
		{
			if (this.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(this.Length);
			StringBuilder stringBuilder = this;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				for (;;)
				{
					if (stringBuilder.m_ChunkLength > 0)
					{
						char[] chunkChars = stringBuilder.m_ChunkChars;
						int chunkOffset = stringBuilder.m_ChunkOffset;
						int chunkLength = stringBuilder.m_ChunkLength;
						if ((ulong)(chunkLength + chunkOffset) > (ulong)((long)text.Length) || chunkLength > chunkChars.Length)
						{
							break;
						}
						char[] array;
						char* ptr2;
						if ((array = chunkChars) == null || array.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array[0];
						}
						string.wstrcpy(ptr + chunkOffset, ptr2, chunkLength);
						array = null;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_7;
					}
				}
				throw new ArgumentOutOfRangeException("chunkLength", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				Block_7:;
			}
			return text;
		}

		/// <summary>Converts the value of a substring of this instance to a <see cref="T:System.String" />.</summary>
		/// <param name="startIndex">The starting position of the substring in this instance.</param>
		/// <param name="length">The length of the substring.</param>
		/// <returns>A string whose value is the same as the specified substring of this instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.  
		/// -or-  
		/// The sum of <paramref name="startIndex" /> and <paramref name="length" /> is greater than the length of the current instance.</exception>
		// Token: 0x060066FC RID: 26364 RVA: 0x0015BED4 File Offset: 0x0015A0D4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe string ToString(int startIndex, int length)
		{
			int length2 = this.Length;
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
			}
			if (startIndex > length2)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndexLargerThanLength"));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
			}
			if (startIndex > length2 - length)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
			}
			StringBuilder stringBuilder = this;
			int num = startIndex + length;
			string text = string.FastAllocateString(length);
			int i = length;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				while (i > 0)
				{
					int num2 = num - stringBuilder.m_ChunkOffset;
					if (num2 >= 0)
					{
						if (num2 > stringBuilder.m_ChunkLength)
						{
							num2 = stringBuilder.m_ChunkLength;
						}
						int num3 = i;
						int num4 = num3;
						int num5 = num2 - num3;
						if (num5 < 0)
						{
							num4 += num5;
							num5 = 0;
						}
						i -= num4;
						if (num4 > 0)
						{
							char[] chunkChars = stringBuilder.m_ChunkChars;
							if ((ulong)(num4 + i) > (ulong)((long)length) || num4 + num5 > chunkChars.Length)
							{
								throw new ArgumentOutOfRangeException("chunkCount", Environment.GetResourceString("ArgumentOutOfRange_Index"));
							}
							fixed (char* ptr2 = &chunkChars[num5])
							{
								char* ptr3 = ptr2;
								string.wstrcpy(ptr + i, ptr3, num4);
							}
						}
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
				}
			}
			return text;
		}

		/// <summary>Removes all characters from the current <see cref="T:System.Text.StringBuilder" /> instance.</summary>
		/// <returns>An object whose <see cref="P:System.Text.StringBuilder.Length" /> is 0 (zero).</returns>
		// Token: 0x060066FD RID: 26365 RVA: 0x0015C036 File Offset: 0x0015A236
		[__DynamicallyInvokable]
		public StringBuilder Clear()
		{
			this.Length = 0;
			return this;
		}

		/// <summary>Gets or sets the length of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>The length of this instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero or greater than <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x060066FE RID: 26366 RVA: 0x0015C040 File Offset: 0x0015A240
		// (set) Token: 0x060066FF RID: 26367 RVA: 0x0015C050 File Offset: 0x0015A250
		[__DynamicallyInvokable]
		public int Length
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_ChunkOffset + this.m_ChunkLength;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
				}
				int capacity = this.Capacity;
				if (value == 0 && this.m_ChunkPrevious == null)
				{
					this.m_ChunkLength = 0;
					this.m_ChunkOffset = 0;
					return;
				}
				int num = value - this.Length;
				if (num > 0)
				{
					this.Append('\0', num);
					return;
				}
				StringBuilder stringBuilder = this.FindChunkForIndex(value);
				if (stringBuilder != this)
				{
					int num2 = capacity - stringBuilder.m_ChunkOffset;
					char[] array = new char[num2];
					Array.Copy(stringBuilder.m_ChunkChars, array, stringBuilder.m_ChunkLength);
					this.m_ChunkChars = array;
					this.m_ChunkPrevious = stringBuilder.m_ChunkPrevious;
					this.m_ChunkOffset = stringBuilder.m_ChunkOffset;
				}
				this.m_ChunkLength = value - stringBuilder.m_ChunkOffset;
			}
		}

		/// <summary>Gets or sets the character at the specified character position in this instance.</summary>
		/// <param name="index">The position of the character.</param>
		/// <returns>The Unicode character at position <paramref name="index" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the bounds of this instance while setting a character.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is outside the bounds of this instance while getting a character.</exception>
		// Token: 0x17001197 RID: 4503
		[__DynamicallyInvokable]
		[IndexerName("Chars")]
		public char this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new IndexOutOfRangeException();
				}
				return stringBuilder.m_ChunkChars[num];
				Block_3:
				throw new IndexOutOfRangeException();
			}
			[__DynamicallyInvokable]
			set
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				stringBuilder.m_ChunkChars[num] = value;
				return;
				Block_3:
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
		}

		/// <summary>Appends a specified number of copies of the string representation of a Unicode character to this instance.</summary>
		/// <param name="value">The character to append.</param>
		/// <param name="repeatCount">The number of times to append <paramref name="value" />.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="repeatCount" /> is less than zero.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		// Token: 0x06006702 RID: 26370 RVA: 0x0015C1D4 File Offset: 0x0015A3D4
		[__DynamicallyInvokable]
		public StringBuilder Append(char value, int repeatCount)
		{
			if (repeatCount < 0)
			{
				throw new ArgumentOutOfRangeException("repeatCount", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
			}
			if (repeatCount == 0)
			{
				return this;
			}
			int num = this.m_ChunkLength;
			while (repeatCount > 0)
			{
				if (num < this.m_ChunkChars.Length)
				{
					this.m_ChunkChars[num++] = value;
					repeatCount--;
				}
				else
				{
					this.m_ChunkLength = num;
					this.ExpandByABlock(repeatCount);
					num = 0;
				}
			}
			this.m_ChunkLength = num;
			return this;
		}

		/// <summary>Appends the string representation of a specified subarray of Unicode characters to this instance.</summary>
		/// <param name="value">A character array.</param>
		/// <param name="startIndex">The starting position in <paramref name="value" />.</param>
		/// <param name="charCount">The number of characters to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />, and <paramref name="startIndex" /> and <paramref name="charCount" /> are not zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> + <paramref name="charCount" /> is greater than the length of <paramref name="value" />.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006703 RID: 26371 RVA: 0x0015C244 File Offset: 0x0015A444
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe StringBuilder Append(char[] value, int startIndex, int charCount)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && charCount == 0)
			{
				return this;
			}
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (charCount > value.Length - startIndex)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (charCount == 0)
				{
					return this;
				}
				fixed (char* ptr = &value[startIndex])
				{
					char* ptr2 = ptr;
					this.Append(ptr2, charCount);
				}
				return this;
			}
		}

		/// <summary>Appends a copy of the specified string to this instance.</summary>
		/// <param name="value">The string to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006704 RID: 26372 RVA: 0x0015C2E0 File Offset: 0x0015A4E0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe StringBuilder Append(string value)
		{
			if (value != null)
			{
				char[] chunkChars = this.m_ChunkChars;
				int chunkLength = this.m_ChunkLength;
				int length = value.Length;
				int num = chunkLength + length;
				if (num < chunkChars.Length)
				{
					if (length <= 2)
					{
						if (length > 0)
						{
							chunkChars[chunkLength] = value[0];
						}
						if (length > 1)
						{
							chunkChars[chunkLength + 1] = value[1];
						}
					}
					else
					{
						fixed (string text = value)
						{
							char* ptr = text;
							if (ptr != null)
							{
								ptr += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (char* ptr2 = &chunkChars[chunkLength])
							{
								char* ptr3 = ptr2;
								string.wstrcpy(ptr3, ptr, length);
							}
						}
					}
					this.m_ChunkLength = num;
				}
				else
				{
					this.AppendHelper(value);
				}
			}
			return this;
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x0015C37C File Offset: 0x0015A57C
		[SecuritySafeCritical]
		private unsafe void AppendHelper(string value)
		{
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.Append(ptr, value.Length);
			}
		}

		// Token: 0x06006706 RID: 26374
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe extern void ReplaceBufferInternal(char* newBuffer, int newLength);

		// Token: 0x06006707 RID: 26375
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe extern void ReplaceBufferAnsiInternal(sbyte* newBuffer, int newLength);

		/// <summary>Appends a copy of a specified substring to this instance.</summary>
		/// <param name="value">The string that contains the substring to append.</param>
		/// <param name="startIndex">The starting position of the substring within <paramref name="value" />.</param>
		/// <param name="count">The number of characters in <paramref name="value" /> to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />, and <paramref name="startIndex" /> and <paramref name="count" /> are not zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="value" />.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006708 RID: 26376 RVA: 0x0015C3AC File Offset: 0x0015A5AC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe StringBuilder Append(string value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && count == 0)
			{
				return this;
			}
			if (value == null)
			{
				if (startIndex == 0 && count == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (count == 0)
				{
					return this;
				}
				if (startIndex > value.Length - count)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					this.Append(ptr + startIndex, count);
				}
				return this;
			}
		}

		/// <summary>Appends the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006709 RID: 26377 RVA: 0x0015C453 File Offset: 0x0015A653
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public StringBuilder AppendLine()
		{
			return this.Append(Environment.NewLine);
		}

		/// <summary>Appends a copy of the specified string followed by the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="value">The string to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600670A RID: 26378 RVA: 0x0015C460 File Offset: 0x0015A660
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public StringBuilder AppendLine(string value)
		{
			this.Append(value);
			return this.Append(Environment.NewLine);
		}

		/// <summary>Copies the characters from a specified segment of this instance to a specified segment of a destination <see cref="T:System.Char" /> array.</summary>
		/// <param name="sourceIndex">The starting position in this instance where characters will be copied from. The index is zero-based.</param>
		/// <param name="destination">The array where characters will be copied.</param>
		/// <param name="destinationIndex">The starting position in <paramref name="destination" /> where characters will be copied. The index is zero-based.</param>
		/// <param name="count">The number of characters to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" />, <paramref name="destinationIndex" />, or <paramref name="count" />, is less than zero.  
		/// -or-  
		/// <paramref name="sourceIndex" /> is greater than the length of this instance.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceIndex" /> + <paramref name="count" /> is greater than the length of this instance.  
		/// -or-  
		/// <paramref name="destinationIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="destination" />.</exception>
		// Token: 0x0600670B RID: 26379 RVA: 0x0015C478 File Offset: 0x0015A678
		[ComVisible(false)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Arg_NegativeArgCount"));
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", new object[] { "destinationIndex" }));
			}
			if (destinationIndex > destination.Length - count)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_OffsetOut"));
			}
			if (sourceIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (sourceIndex > this.Length - count)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_LongerThanSrcString"));
			}
			StringBuilder stringBuilder = this;
			int num = sourceIndex + count;
			int num2 = destinationIndex + count;
			while (count > 0)
			{
				int num3 = num - stringBuilder.m_ChunkOffset;
				if (num3 >= 0)
				{
					if (num3 > stringBuilder.m_ChunkLength)
					{
						num3 = stringBuilder.m_ChunkLength;
					}
					int num4 = count;
					int num5 = num3 - count;
					if (num5 < 0)
					{
						num4 += num5;
						num5 = 0;
					}
					num2 -= num4;
					count -= num4;
					StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num5, destination, num2, num4);
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
		}

		/// <summary>Inserts one or more copies of a specified string into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The string to insert.</param>
		/// <param name="count">The number of times to insert <paramref name="value" />.</param>
		/// <returns>A reference to this instance after insertion has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the current length of this instance.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.OutOfMemoryException">The current length of this <see cref="T:System.Text.StringBuilder" /> object plus the length of <paramref name="value" /> times <paramref name="count" /> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600670C RID: 26380 RVA: 0x0015C594 File Offset: 0x0015A794
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe StringBuilder Insert(int index, string value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			int length = this.Length;
			if (index > length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (value == null || value.Length == 0 || count == 0)
			{
				return this;
			}
			long num = (long)value.Length * (long)count;
			if (num > (long)(this.MaxCapacity - this.Length))
			{
				throw new OutOfMemoryException();
			}
			StringBuilder stringBuilder;
			int num2;
			this.MakeRoom(index, (int)num, out stringBuilder, out num2, false);
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				while (count > 0)
				{
					this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr, value.Length);
					count--;
				}
			}
			return this;
		}

		/// <summary>Removes the specified range of characters from this instance.</summary>
		/// <param name="startIndex">The zero-based position in this instance where removal begins.</param>
		/// <param name="length">The number of characters to remove.</param>
		/// <returns>A reference to this instance after the excise operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">If <paramref name="startIndex" /> or <paramref name="length" /> is less than zero, or <paramref name="startIndex" /> + <paramref name="length" /> is greater than the length of this instance.</exception>
		// Token: 0x0600670D RID: 26381 RVA: 0x0015C654 File Offset: 0x0015A854
		[__DynamicallyInvokable]
		public StringBuilder Remove(int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
			}
			if (length > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (this.Length == length && startIndex == 0)
			{
				this.Length = 0;
				return this;
			}
			if (length > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.Remove(startIndex, length, out stringBuilder, out num);
			}
			return this;
		}

		/// <summary>Appends the string representation of a specified Boolean value to this instance.</summary>
		/// <param name="value">The Boolean value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600670E RID: 26382 RVA: 0x0015C6D9 File Offset: 0x0015A8D9
		[__DynamicallyInvokable]
		public StringBuilder Append(bool value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified 8-bit signed integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600670F RID: 26383 RVA: 0x0015C6E8 File Offset: 0x0015A8E8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public StringBuilder Append(sbyte value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 8-bit unsigned integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006710 RID: 26384 RVA: 0x0015C6FC File Offset: 0x0015A8FC
		[__DynamicallyInvokable]
		public StringBuilder Append(byte value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified <see cref="T:System.Char" /> object to this instance.</summary>
		/// <param name="value">The UTF-16-encoded code unit to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006711 RID: 26385 RVA: 0x0015C710 File Offset: 0x0015A910
		[__DynamicallyInvokable]
		public StringBuilder Append(char value)
		{
			if (this.m_ChunkLength < this.m_ChunkChars.Length)
			{
				char[] chunkChars = this.m_ChunkChars;
				int chunkLength = this.m_ChunkLength;
				this.m_ChunkLength = chunkLength + 1;
				chunkChars[chunkLength] = value;
			}
			else
			{
				this.Append(value, 1);
			}
			return this;
		}

		/// <summary>Appends the string representation of a specified 16-bit signed integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006712 RID: 26386 RVA: 0x0015C752 File Offset: 0x0015A952
		[__DynamicallyInvokable]
		public StringBuilder Append(short value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 32-bit signed integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006713 RID: 26387 RVA: 0x0015C766 File Offset: 0x0015A966
		[__DynamicallyInvokable]
		public StringBuilder Append(int value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 64-bit signed integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006714 RID: 26388 RVA: 0x0015C77A File Offset: 0x0015A97A
		[__DynamicallyInvokable]
		public StringBuilder Append(long value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified single-precision floating-point number to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006715 RID: 26389 RVA: 0x0015C78E File Offset: 0x0015A98E
		[__DynamicallyInvokable]
		public StringBuilder Append(float value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified double-precision floating-point number to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006716 RID: 26390 RVA: 0x0015C7A2 File Offset: 0x0015A9A2
		[__DynamicallyInvokable]
		public StringBuilder Append(double value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified decimal number to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006717 RID: 26391 RVA: 0x0015C7B6 File Offset: 0x0015A9B6
		[__DynamicallyInvokable]
		public StringBuilder Append(decimal value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 16-bit unsigned integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006718 RID: 26392 RVA: 0x0015C7CA File Offset: 0x0015A9CA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public StringBuilder Append(ushort value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 32-bit unsigned integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006719 RID: 26393 RVA: 0x0015C7DE File Offset: 0x0015A9DE
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public StringBuilder Append(uint value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 64-bit unsigned integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600671A RID: 26394 RVA: 0x0015C7F2 File Offset: 0x0015A9F2
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public StringBuilder Append(ulong value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified object to this instance.</summary>
		/// <param name="value">The object to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600671B RID: 26395 RVA: 0x0015C806 File Offset: 0x0015AA06
		[__DynamicallyInvokable]
		public StringBuilder Append(object value)
		{
			if (value == null)
			{
				return this;
			}
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of the Unicode characters in a specified array to this instance.</summary>
		/// <param name="value">The array of characters to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600671C RID: 26396 RVA: 0x0015C81C File Offset: 0x0015AA1C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe StringBuilder Append(char[] value)
		{
			if (value != null && value.Length != 0)
			{
				fixed (char* ptr = &value[0])
				{
					char* ptr2 = ptr;
					this.Append(ptr2, value.Length);
				}
			}
			return this;
		}

		/// <summary>Inserts a string into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The string to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the current length of this instance.  
		/// -or-  
		/// The current length of this <see cref="T:System.Text.StringBuilder" /> object plus the length of <paramref name="value" /> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600671D RID: 26397 RVA: 0x0015C84C File Offset: 0x0015AA4C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe StringBuilder Insert(int index, string value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (value != null)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					this.Insert(index, ptr, value.Length);
				}
			}
			return this;
		}

		/// <summary>Inserts the string representation of a Boolean value into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600671E RID: 26398 RVA: 0x0015C89B File Offset: 0x0015AA9B
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, bool value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a specified 8-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600671F RID: 26399 RVA: 0x0015C8AC File Offset: 0x0015AAAC
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, sbyte value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a specified 8-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006720 RID: 26400 RVA: 0x0015C8C2 File Offset: 0x0015AAC2
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, byte value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a specified 16-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006721 RID: 26401 RVA: 0x0015C8D8 File Offset: 0x0015AAD8
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, short value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a specified Unicode character into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006722 RID: 26402 RVA: 0x0015C8EE File Offset: 0x0015AAEE
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe StringBuilder Insert(int index, char value)
		{
			this.Insert(index, &value, 1);
			return this;
		}

		/// <summary>Inserts the string representation of a specified array of Unicode characters into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The character array to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006723 RID: 26403 RVA: 0x0015C8FC File Offset: 0x0015AAFC
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, char[] value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (value != null)
			{
				this.Insert(index, value, 0, value.Length);
			}
			return this;
		}

		/// <summary>Inserts the string representation of a specified subarray of Unicode characters into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">A character array.</param>
		/// <param name="startIndex">The starting index within <paramref name="value" />.</param>
		/// <param name="charCount">The number of characters to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />, and <paramref name="startIndex" /> and <paramref name="charCount" /> are not zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" />, <paramref name="startIndex" />, or <paramref name="charCount" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of this instance.  
		/// -or-  
		/// <paramref name="startIndex" /> plus <paramref name="charCount" /> is not a position within <paramref name="value" />.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006724 RID: 26404 RVA: 0x0015C930 File Offset: 0x0015AB30
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
		{
			int length = this.Length;
			if (index > length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_String"));
			}
			else
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
				}
				if (charCount < 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
				}
				if (startIndex > value.Length - charCount)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (charCount > 0)
				{
					fixed (char* ptr = &value[startIndex])
					{
						char* ptr2 = ptr;
						this.Insert(index, ptr2, charCount);
					}
				}
				return this;
			}
		}

		/// <summary>Inserts the string representation of a specified 32-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006725 RID: 26405 RVA: 0x0015C9E8 File Offset: 0x0015ABE8
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, int value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a 64-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006726 RID: 26406 RVA: 0x0015C9FE File Offset: 0x0015ABFE
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, long value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a single-precision floating point number into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006727 RID: 26407 RVA: 0x0015CA14 File Offset: 0x0015AC14
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, float value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a double-precision floating-point number into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006728 RID: 26408 RVA: 0x0015CA2A File Offset: 0x0015AC2A
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, double value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a decimal number into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006729 RID: 26409 RVA: 0x0015CA40 File Offset: 0x0015AC40
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, decimal value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a 16-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600672A RID: 26410 RVA: 0x0015CA56 File Offset: 0x0015AC56
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, ushort value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a 32-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600672B RID: 26411 RVA: 0x0015CA6C File Offset: 0x0015AC6C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, uint value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a 64-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600672C RID: 26412 RVA: 0x0015CA82 File Offset: 0x0015AC82
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, ulong value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of an object into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The object to insert, or <see langword="null" />.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600672D RID: 26413 RVA: 0x0015CA98 File Offset: 0x0015AC98
		[__DynamicallyInvokable]
		public StringBuilder Insert(int index, object value)
		{
			if (value == null)
			{
				return this;
			}
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a single argument.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">An object to format.</param>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of <paramref name="arg0" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to 1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600672E RID: 26414 RVA: 0x0015CAAD File Offset: 0x0015ACAD
		[__DynamicallyInvokable]
		public StringBuilder AppendFormat(string format, object arg0)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of two arguments.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format.</param>
		/// <param name="arg1">The second object to format.</param>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to 2.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600672F RID: 26415 RVA: 0x0015CABD File Offset: 0x0015ACBD
		[__DynamicallyInvokable]
		public StringBuilder AppendFormat(string format, object arg0, object arg1)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of three arguments.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format.</param>
		/// <param name="arg1">The second object to format.</param>
		/// <param name="arg2">The third object to format.</param>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to 3.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006730 RID: 26416 RVA: 0x0015CACE File Offset: 0x0015ACCE
		[__DynamicallyInvokable]
		public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1, arg2));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a corresponding argument in a parameter array.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="args">An array of objects to format.</param>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="args" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args" /> array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006731 RID: 26417 RVA: 0x0015CAE1 File Offset: 0x0015ACE1
		[__DynamicallyInvokable]
		public StringBuilder AppendFormat(string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return this.AppendFormatHelper(null, format, new ParamsArray(args));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a single argument using a specified format provider.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The object to format.</param>
		/// <returns>A reference to this instance after the append operation has completed. After the append operation, this instance contains any data that existed before the operation, suffixed by a copy of <paramref name="format" /> in which any format specification is replaced by the string representation of <paramref name="arg0" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to one (1).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006732 RID: 26418 RVA: 0x0015CB09 File Offset: 0x0015AD09
		[__DynamicallyInvokable]
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of two arguments using a specified format provider.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format.</param>
		/// <param name="arg1">The second object to format.</param>
		/// <returns>A reference to this instance after the append operation has completed. After the append operation, this instance contains any data that existed before the operation, suffixed by a copy of <paramref name="format" /> where any format specification is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to 2 (two).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006733 RID: 26419 RVA: 0x0015CB19 File Offset: 0x0015AD19
		[__DynamicallyInvokable]
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of three arguments using a specified format provider.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format.</param>
		/// <param name="arg1">The second object to format.</param>
		/// <param name="arg2">The third object to format.</param>
		/// <returns>A reference to this instance after the append operation has completed. After the append operation, this instance contains any data that existed before the operation, suffixed by a copy of <paramref name="format" /> where any format specification is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to 3 (three).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006734 RID: 26420 RVA: 0x0015CB2B File Offset: 0x0015AD2B
		[__DynamicallyInvokable]
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a corresponding argument in a parameter array using a specified format provider.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <param name="format">A composite format string.</param>
		/// <param name="args">An array of objects to format.</param>
		/// <returns>A reference to this instance after the append operation has completed. After the append operation, this instance contains any data that existed before the operation, suffixed by a copy of <paramref name="format" /> where any format specification is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args" /> array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006735 RID: 26421 RVA: 0x0015CB3F File Offset: 0x0015AD3F
		[__DynamicallyInvokable]
		public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return this.AppendFormatHelper(provider, format, new ParamsArray(args));
		}

		// Token: 0x06006736 RID: 26422 RVA: 0x0015CB67 File Offset: 0x0015AD67
		private static void FormatError()
		{
			throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
		}

		// Token: 0x06006737 RID: 26423 RVA: 0x0015CB78 File Offset: 0x0015AD78
		internal StringBuilder AppendFormatHelper(IFormatProvider provider, string format, ParamsArray args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int i = 0;
			int length = format.Length;
			char c = '\0';
			ICustomFormatter customFormatter = null;
			if (provider != null)
			{
				customFormatter = (ICustomFormatter)provider.GetFormat(typeof(ICustomFormatter));
			}
			for (;;)
			{
				while (i < length)
				{
					c = format[i];
					i++;
					if (c == '}')
					{
						if (i < length && format[i] == '}')
						{
							i++;
						}
						else
						{
							StringBuilder.FormatError();
						}
					}
					if (c == '{')
					{
						if (i >= length || format[i] != '{')
						{
							i--;
							break;
						}
						i++;
					}
					this.Append(c);
				}
				if (i == length)
				{
					return this;
				}
				i++;
				if (i == length || (c = format[i]) < '0' || c > '9')
				{
					StringBuilder.FormatError();
				}
				int num = 0;
				do
				{
					num = num * 10 + (int)c - 48;
					i++;
					if (i == length)
					{
						StringBuilder.FormatError();
					}
					c = format[i];
				}
				while (c >= '0' && c <= '9' && num < 1000000);
				if (num >= args.Length)
				{
					break;
				}
				while (i < length && (c = format[i]) == ' ')
				{
					i++;
				}
				bool flag = false;
				int num2 = 0;
				if (c == ',')
				{
					i++;
					while (i < length && format[i] == ' ')
					{
						i++;
					}
					if (i == length)
					{
						StringBuilder.FormatError();
					}
					c = format[i];
					if (c == '-')
					{
						flag = true;
						i++;
						if (i == length)
						{
							StringBuilder.FormatError();
						}
						c = format[i];
					}
					if (c < '0' || c > '9')
					{
						StringBuilder.FormatError();
					}
					do
					{
						num2 = num2 * 10 + (int)c - 48;
						i++;
						if (i == length)
						{
							StringBuilder.FormatError();
						}
						c = format[i];
						if (c < '0' || c > '9')
						{
							break;
						}
					}
					while (num2 < 1000000);
				}
				while (i < length && (c = format[i]) == ' ')
				{
					i++;
				}
				object obj = args[num];
				StringBuilder stringBuilder = null;
				if (c == ':')
				{
					i++;
					for (;;)
					{
						if (i == length)
						{
							StringBuilder.FormatError();
						}
						c = format[i];
						i++;
						if (c == '{')
						{
							if (i < length && format[i] == '{')
							{
								i++;
							}
							else
							{
								StringBuilder.FormatError();
							}
						}
						else if (c == '}')
						{
							if (i >= length || format[i] != '}')
							{
								break;
							}
							i++;
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						stringBuilder.Append(c);
					}
					i--;
				}
				if (c != '}')
				{
					StringBuilder.FormatError();
				}
				i++;
				string text = null;
				string text2 = null;
				if (customFormatter != null)
				{
					if (stringBuilder != null)
					{
						text = stringBuilder.ToString();
					}
					text2 = customFormatter.Format(text, obj, provider);
				}
				if (text2 == null)
				{
					IFormattable formattable = obj as IFormattable;
					if (formattable != null)
					{
						if (text == null && stringBuilder != null)
						{
							text = stringBuilder.ToString();
						}
						text2 = formattable.ToString(text, provider);
					}
					else if (obj != null)
					{
						text2 = obj.ToString();
					}
				}
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				int num3 = num2 - text2.Length;
				if (!flag && num3 > 0)
				{
					this.Append(' ', num3);
				}
				this.Append(text2);
				if (flag && num3 > 0)
				{
					this.Append(' ', num3);
				}
			}
			throw new FormatException(Environment.GetResourceString("Format_IndexOutOfRange"));
		}

		/// <summary>Replaces all occurrences of a specified string in this instance with another specified string.</summary>
		/// <param name="oldValue">The string to replace.</param>
		/// <param name="newValue">The string that replaces <paramref name="oldValue" />, or <see langword="null" />.</param>
		/// <returns>A reference to this instance with all instances of <paramref name="oldValue" /> replaced by <paramref name="newValue" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="oldValue" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06006738 RID: 26424 RVA: 0x0015CE9F File Offset: 0x0015B09F
		[__DynamicallyInvokable]
		public StringBuilder Replace(string oldValue, string newValue)
		{
			return this.Replace(oldValue, newValue, 0, this.Length);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="sb">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if this instance and <paramref name="sb" /> have equal string, <see cref="P:System.Text.StringBuilder.Capacity" />, and <see cref="P:System.Text.StringBuilder.MaxCapacity" /> values; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006739 RID: 26425 RVA: 0x0015CEB0 File Offset: 0x0015B0B0
		[__DynamicallyInvokable]
		public bool Equals(StringBuilder sb)
		{
			if (sb == null)
			{
				return false;
			}
			if (this.Capacity != sb.Capacity || this.MaxCapacity != sb.MaxCapacity || this.Length != sb.Length)
			{
				return false;
			}
			if (sb == this)
			{
				return true;
			}
			StringBuilder stringBuilder = this;
			int i = stringBuilder.m_ChunkLength;
			StringBuilder stringBuilder2 = sb;
			int j = stringBuilder2.m_ChunkLength;
			for (;;)
			{
				IL_49:
				i--;
				j--;
				while (i < 0)
				{
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder != null)
					{
						i = stringBuilder.m_ChunkLength + i;
					}
					else
					{
						IL_7F:
						while (j < 0)
						{
							stringBuilder2 = stringBuilder2.m_ChunkPrevious;
							if (stringBuilder2 == null)
							{
								break;
							}
							j = stringBuilder2.m_ChunkLength + j;
						}
						if (i < 0)
						{
							goto Block_8;
						}
						if (j < 0)
						{
							return false;
						}
						if (stringBuilder.m_ChunkChars[i] != stringBuilder2.m_ChunkChars[j])
						{
							return false;
						}
						goto IL_49;
					}
				}
				goto IL_7F;
			}
			Block_8:
			return j < 0;
		}

		/// <summary>Replaces, within a substring of this instance, all occurrences of a specified string with another specified string.</summary>
		/// <param name="oldValue">The string to replace.</param>
		/// <param name="newValue">The string that replaces <paramref name="oldValue" />, or <see langword="null" />.</param>
		/// <param name="startIndex">The position in this instance where the substring begins.</param>
		/// <param name="count">The length of the substring.</param>
		/// <returns>A reference to this instance with all instances of <paramref name="oldValue" /> replaced by <paramref name="newValue" /> in the range from <paramref name="startIndex" /> to <paramref name="startIndex" /> + <paramref name="count" /> - 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="oldValue" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> plus <paramref name="count" /> indicates a character position not within this instance.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600673A RID: 26426 RVA: 0x0015CF64 File Offset: 0x0015B164
		[__DynamicallyInvokable]
		public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex > length - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "oldValue");
			}
			if (newValue == null)
			{
				newValue = "";
			}
			int num = newValue.Length - oldValue.Length;
			int[] array = null;
			int num2 = 0;
			StringBuilder stringBuilder = this.FindChunkForIndex(startIndex);
			int num3 = startIndex - stringBuilder.m_ChunkOffset;
			while (count > 0)
			{
				if (this.StartsWith(stringBuilder, num3, count, oldValue))
				{
					if (array == null)
					{
						array = new int[5];
					}
					else if (num2 >= array.Length)
					{
						int[] array2 = new int[array.Length * 3 / 2 + 4];
						Array.Copy(array, array2, array.Length);
						array = array2;
					}
					array[num2++] = num3;
					num3 += oldValue.Length;
					count -= oldValue.Length;
				}
				else
				{
					num3++;
					count--;
				}
				if (num3 >= stringBuilder.m_ChunkLength || count == 0)
				{
					int num4 = num3 + stringBuilder.m_ChunkOffset;
					this.ReplaceAllInChunk(array, num2, stringBuilder, oldValue.Length, newValue);
					num4 += (newValue.Length - oldValue.Length) * num2;
					num2 = 0;
					stringBuilder = this.FindChunkForIndex(num4);
					num3 = num4 - stringBuilder.m_ChunkOffset;
				}
			}
			return this;
		}

		/// <summary>Replaces all occurrences of a specified character in this instance with another specified character.</summary>
		/// <param name="oldChar">The character to replace.</param>
		/// <param name="newChar">The character that replaces <paramref name="oldChar" />.</param>
		/// <returns>A reference to this instance with <paramref name="oldChar" /> replaced by <paramref name="newChar" />.</returns>
		// Token: 0x0600673B RID: 26427 RVA: 0x0015D0E3 File Offset: 0x0015B2E3
		[__DynamicallyInvokable]
		public StringBuilder Replace(char oldChar, char newChar)
		{
			return this.Replace(oldChar, newChar, 0, this.Length);
		}

		/// <summary>Replaces, within a substring of this instance, all occurrences of a specified character with another specified character.</summary>
		/// <param name="oldChar">The character to replace.</param>
		/// <param name="newChar">The character that replaces <paramref name="oldChar" />.</param>
		/// <param name="startIndex">The position in this instance where the substring begins.</param>
		/// <param name="count">The length of the substring.</param>
		/// <returns>A reference to this instance with <paramref name="oldChar" /> replaced by <paramref name="newChar" /> in the range from <paramref name="startIndex" /> to <paramref name="startIndex" /> + <paramref name="count" /> -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of the value of this instance.  
		/// -or-  
		/// <paramref name="startIndex" /> or <paramref name="count" /> is less than zero.</exception>
		// Token: 0x0600673C RID: 26428 RVA: 0x0015D0F4 File Offset: 0x0015B2F4
		[__DynamicallyInvokable]
		public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex > length - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int num = startIndex + count;
			StringBuilder stringBuilder = this;
			for (;;)
			{
				int num2 = num - stringBuilder.m_ChunkOffset;
				int num3 = startIndex - stringBuilder.m_ChunkOffset;
				if (num2 >= 0)
				{
					int i = Math.Max(num3, 0);
					int num4 = Math.Min(stringBuilder.m_ChunkLength, num2);
					while (i < num4)
					{
						if (stringBuilder.m_ChunkChars[i] == oldChar)
						{
							stringBuilder.m_ChunkChars[i] = newChar;
						}
						i++;
					}
				}
				if (num3 >= 0)
				{
					break;
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return this;
		}

		/// <summary>Appends an array of Unicode characters starting at a specified address to this instance.</summary>
		/// <param name="value">A pointer to an array of characters.</param>
		/// <param name="valueCount">The number of characters in the array.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="valueCount" /> is less than zero.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="value" /> is a null pointer.</exception>
		// Token: 0x0600673D RID: 26429 RVA: 0x0015D1AC File Offset: 0x0015B3AC
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe StringBuilder Append(char* value, int valueCount)
		{
			if (valueCount < 0)
			{
				throw new ArgumentOutOfRangeException("valueCount", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
			}
			int num = valueCount + this.m_ChunkLength;
			if (num <= this.m_ChunkChars.Length)
			{
				StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, valueCount);
				this.m_ChunkLength = num;
			}
			else
			{
				int num2 = this.m_ChunkChars.Length - this.m_ChunkLength;
				if (num2 > 0)
				{
					StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, num2);
					this.m_ChunkLength = this.m_ChunkChars.Length;
				}
				int num3 = valueCount - num2;
				this.ExpandByABlock(num3);
				StringBuilder.ThreadSafeCopy(value + num2, this.m_ChunkChars, 0, num3);
				this.m_ChunkLength = num3;
			}
			return this;
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x0015D260 File Offset: 0x0015B460
		[SecurityCritical]
		private unsafe void Insert(int index, char* value, int valueCount)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (valueCount > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.MakeRoom(index, valueCount, out stringBuilder, out num, false);
				this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num, value, valueCount);
			}
		}

		// Token: 0x0600673F RID: 26431 RVA: 0x0015D2A8 File Offset: 0x0015B4A8
		[SecuritySafeCritical]
		private unsafe void ReplaceAllInChunk(int[] replacements, int replacementsCount, StringBuilder sourceChunk, int removeCount, string value)
		{
			if (replacementsCount <= 0)
			{
				return;
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num = (value.Length - removeCount) * replacementsCount;
				StringBuilder stringBuilder = sourceChunk;
				int num2 = replacements[0];
				if (num > 0)
				{
					this.MakeRoom(stringBuilder.m_ChunkOffset + num2, num, out stringBuilder, out num2, true);
				}
				int num3 = 0;
				for (;;)
				{
					this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr, value.Length);
					int num4 = replacements[num3] + removeCount;
					num3++;
					if (num3 >= replacementsCount)
					{
						break;
					}
					int num5 = replacements[num3];
					if (num != 0)
					{
						fixed (char* ptr2 = &sourceChunk.m_ChunkChars[num4])
						{
							char* ptr3 = ptr2;
							this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr3, num5 - num4);
						}
					}
					else
					{
						num2 += num5 - num4;
					}
				}
				if (num < 0)
				{
					this.Remove(stringBuilder.m_ChunkOffset + num2, -num, out stringBuilder, out num2);
				}
			}
		}

		// Token: 0x06006740 RID: 26432 RVA: 0x0015D37C File Offset: 0x0015B57C
		private bool StartsWith(StringBuilder chunk, int indexInChunk, int count, string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (count == 0)
				{
					return false;
				}
				if (indexInChunk >= chunk.m_ChunkLength)
				{
					chunk = this.Next(chunk);
					if (chunk == null)
					{
						return false;
					}
					indexInChunk = 0;
				}
				if (value[i] != chunk.m_ChunkChars[indexInChunk])
				{
					return false;
				}
				indexInChunk++;
				count--;
			}
			return true;
		}

		// Token: 0x06006741 RID: 26433 RVA: 0x0015D3DC File Offset: 0x0015B5DC
		[SecurityCritical]
		private unsafe void ReplaceInPlaceAtChunk(ref StringBuilder chunk, ref int indexInChunk, char* value, int count)
		{
			if (count != 0)
			{
				for (;;)
				{
					int num = chunk.m_ChunkLength - indexInChunk;
					int num2 = Math.Min(num, count);
					StringBuilder.ThreadSafeCopy(value, chunk.m_ChunkChars, indexInChunk, num2);
					indexInChunk += num2;
					if (indexInChunk >= chunk.m_ChunkLength)
					{
						chunk = this.Next(chunk);
						indexInChunk = 0;
					}
					count -= num2;
					if (count == 0)
					{
						break;
					}
					value += num2;
				}
			}
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x0015D444 File Offset: 0x0015B644
		[SecurityCritical]
		private unsafe static void ThreadSafeCopy(char* sourcePtr, char[] destination, int destinationIndex, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (destinationIndex <= destination.Length && destinationIndex + count <= destination.Length)
			{
				fixed (char* ptr = &destination[destinationIndex])
				{
					char* ptr2 = ptr;
					string.wstrcpy(ptr2, sourcePtr, count);
				}
				return;
			}
			throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x0015D490 File Offset: 0x0015B690
		[SecurityCritical]
		private unsafe static void ThreadSafeCopy(char[] source, int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (sourceIndex <= source.Length && sourceIndex + count <= source.Length)
			{
				fixed (char* ptr = &source[sourceIndex])
				{
					char* ptr2 = ptr;
					StringBuilder.ThreadSafeCopy(ptr2, destination, destinationIndex, count);
				}
				return;
			}
			throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x0015D4E0 File Offset: 0x0015B6E0
		[SecurityCritical]
		internal unsafe void InternalCopy(IntPtr dest, int len)
		{
			if (len == 0)
			{
				return;
			}
			bool flag = true;
			byte* ptr = (byte*)dest.ToPointer();
			StringBuilder stringBuilder = this.FindChunkForByte(len);
			do
			{
				int num = stringBuilder.m_ChunkOffset * 2;
				int num2 = stringBuilder.m_ChunkLength * 2;
				fixed (char* ptr2 = &stringBuilder.m_ChunkChars[0])
				{
					char* ptr3 = ptr2;
					byte* ptr4 = (byte*)ptr3;
					if (flag)
					{
						flag = false;
						Buffer.Memcpy(ptr + num, ptr4, len - num);
					}
					else
					{
						Buffer.Memcpy(ptr + num, ptr4, num2);
					}
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			while (stringBuilder != null);
		}

		// Token: 0x06006745 RID: 26437 RVA: 0x0015D55C File Offset: 0x0015B75C
		private StringBuilder FindChunkForIndex(int index)
		{
			StringBuilder stringBuilder = this;
			while (stringBuilder.m_ChunkOffset > index)
			{
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return stringBuilder;
		}

		// Token: 0x06006746 RID: 26438 RVA: 0x0015D580 File Offset: 0x0015B780
		private StringBuilder FindChunkForByte(int byteIndex)
		{
			StringBuilder stringBuilder = this;
			while (stringBuilder.m_ChunkOffset * 2 > byteIndex)
			{
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return stringBuilder;
		}

		// Token: 0x06006747 RID: 26439 RVA: 0x0015D5A4 File Offset: 0x0015B7A4
		private StringBuilder Next(StringBuilder chunk)
		{
			if (chunk == this)
			{
				return null;
			}
			return this.FindChunkForIndex(chunk.m_ChunkOffset + chunk.m_ChunkLength);
		}

		// Token: 0x06006748 RID: 26440 RVA: 0x0015D5C0 File Offset: 0x0015B7C0
		private void ExpandByABlock(int minBlockCharCount)
		{
			if (minBlockCharCount + this.Length < minBlockCharCount || minBlockCharCount + this.Length > this.m_MaxCapacity)
			{
				throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
			}
			int num = Math.Max(minBlockCharCount, Math.Min(this.Length, 8000));
			this.m_ChunkPrevious = new StringBuilder(this);
			this.m_ChunkOffset += this.m_ChunkLength;
			this.m_ChunkLength = 0;
			if (this.m_ChunkOffset + num < num)
			{
				this.m_ChunkChars = null;
				throw new OutOfMemoryException();
			}
			this.m_ChunkChars = new char[num];
		}

		// Token: 0x06006749 RID: 26441 RVA: 0x0015D660 File Offset: 0x0015B860
		private StringBuilder(StringBuilder from)
		{
			this.m_ChunkLength = from.m_ChunkLength;
			this.m_ChunkOffset = from.m_ChunkOffset;
			this.m_ChunkChars = from.m_ChunkChars;
			this.m_ChunkPrevious = from.m_ChunkPrevious;
			this.m_MaxCapacity = from.m_MaxCapacity;
		}

		// Token: 0x0600674A RID: 26442 RVA: 0x0015D6B0 File Offset: 0x0015B8B0
		[SecuritySafeCritical]
		private unsafe void MakeRoom(int index, int count, out StringBuilder chunk, out int indexInChunk, bool doneMoveFollowingChars)
		{
			if (count + this.Length < count || count + this.Length > this.m_MaxCapacity)
			{
				throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
			}
			chunk = this;
			while (chunk.m_ChunkOffset > index)
			{
				chunk.m_ChunkOffset += count;
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = index - chunk.m_ChunkOffset;
			if (!doneMoveFollowingChars && chunk.m_ChunkLength <= 32 && chunk.m_ChunkChars.Length - chunk.m_ChunkLength >= count)
			{
				int i = chunk.m_ChunkLength;
				while (i > indexInChunk)
				{
					i--;
					chunk.m_ChunkChars[i + count] = chunk.m_ChunkChars[i];
				}
				chunk.m_ChunkLength += count;
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(Math.Max(count, 16), chunk.m_MaxCapacity, chunk.m_ChunkPrevious);
			stringBuilder.m_ChunkLength = count;
			int num = Math.Min(count, indexInChunk);
			if (num > 0)
			{
				char[] array;
				char* ptr;
				if ((array = chunk.m_ChunkChars) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				StringBuilder.ThreadSafeCopy(ptr, stringBuilder.m_ChunkChars, 0, num);
				int num2 = indexInChunk - num;
				if (num2 >= 0)
				{
					StringBuilder.ThreadSafeCopy(ptr + num, chunk.m_ChunkChars, 0, num2);
					indexInChunk = num2;
				}
				array = null;
			}
			chunk.m_ChunkPrevious = stringBuilder;
			chunk.m_ChunkOffset += count;
			if (num < count)
			{
				chunk = stringBuilder;
				indexInChunk = num;
			}
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x0015D826 File Offset: 0x0015BA26
		private StringBuilder(int size, int maxCapacity, StringBuilder previousBlock)
		{
			this.m_ChunkChars = new char[size];
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkPrevious = previousBlock;
			if (previousBlock != null)
			{
				this.m_ChunkOffset = previousBlock.m_ChunkOffset + previousBlock.m_ChunkLength;
			}
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x0015D860 File Offset: 0x0015BA60
		[SecuritySafeCritical]
		private void Remove(int startIndex, int count, out StringBuilder chunk, out int indexInChunk)
		{
			int num = startIndex + count;
			chunk = this;
			StringBuilder stringBuilder = null;
			int num2 = 0;
			for (;;)
			{
				if (num - chunk.m_ChunkOffset >= 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = chunk;
						num2 = num - stringBuilder.m_ChunkOffset;
					}
					if (startIndex - chunk.m_ChunkOffset >= 0)
					{
						break;
					}
				}
				else
				{
					chunk.m_ChunkOffset -= count;
				}
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = startIndex - chunk.m_ChunkOffset;
			int num3 = indexInChunk;
			int num4 = stringBuilder.m_ChunkLength - num2;
			if (stringBuilder != chunk)
			{
				num3 = 0;
				chunk.m_ChunkLength = indexInChunk;
				stringBuilder.m_ChunkPrevious = chunk;
				stringBuilder.m_ChunkOffset = chunk.m_ChunkOffset + chunk.m_ChunkLength;
				if (indexInChunk == 0)
				{
					stringBuilder.m_ChunkPrevious = chunk.m_ChunkPrevious;
					chunk = stringBuilder;
				}
			}
			stringBuilder.m_ChunkLength -= num2 - num3;
			if (num3 != num2)
			{
				StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num2, stringBuilder.m_ChunkChars, num3, num4);
			}
		}

		// Token: 0x04002E24 RID: 11812
		internal char[] m_ChunkChars;

		// Token: 0x04002E25 RID: 11813
		internal StringBuilder m_ChunkPrevious;

		// Token: 0x04002E26 RID: 11814
		internal int m_ChunkLength;

		// Token: 0x04002E27 RID: 11815
		internal int m_ChunkOffset;

		// Token: 0x04002E28 RID: 11816
		internal int m_MaxCapacity;

		// Token: 0x04002E29 RID: 11817
		internal const int DefaultCapacity = 16;

		// Token: 0x04002E2A RID: 11818
		private const string CapacityField = "Capacity";

		// Token: 0x04002E2B RID: 11819
		private const string MaxCapacityField = "m_MaxCapacity";

		// Token: 0x04002E2C RID: 11820
		private const string StringValueField = "m_StringValue";

		// Token: 0x04002E2D RID: 11821
		private const string ThreadIDField = "m_currentThread";

		// Token: 0x04002E2E RID: 11822
		internal const int MaxChunkSize = 8000;
	}
}
