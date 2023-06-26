using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	/// <summary>Converts a set of characters into a sequence of bytes.</summary>
	// Token: 0x02000A66 RID: 2662
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Encoder
	{
		// Token: 0x060067D7 RID: 26583 RVA: 0x0015FD72 File Offset: 0x0015DF72
		internal void SerializeEncoder(SerializationInfo info)
		{
			info.AddValue("m_fallback", this.m_fallback);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.Encoder" /> class.</summary>
		// Token: 0x060067D8 RID: 26584 RVA: 0x0015FD85 File Offset: 0x0015DF85
		[__DynamicallyInvokable]
		protected Encoder()
		{
		}

		/// <summary>Gets or sets a <see cref="T:System.Text.EncoderFallback" /> object for the current <see cref="T:System.Text.Encoder" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.EncoderFallback" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentException">A new value cannot be assigned in a set operation because the current <see cref="T:System.Text.EncoderFallbackBuffer" /> object contains data that has not been encoded yet.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoder.Fallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x060067D9 RID: 26585 RVA: 0x0015FD8D File Offset: 0x0015DF8D
		// (set) Token: 0x060067DA RID: 26586 RVA: 0x0015FD98 File Offset: 0x0015DF98
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public EncoderFallback Fallback
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_fallback;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.m_fallbackBuffer != null && this.m_fallbackBuffer.Remaining > 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_FallbackBufferNotEmpty"), "value");
				}
				this.m_fallback = value;
				this.m_fallbackBuffer = null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Text.EncoderFallbackBuffer" /> object associated with the current <see cref="T:System.Text.Encoder" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.EncoderFallbackBuffer" /> object.</returns>
		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x060067DB RID: 26587 RVA: 0x0015FDEC File Offset: 0x0015DFEC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public EncoderFallbackBuffer FallbackBuffer
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_fallbackBuffer == null)
				{
					if (this.m_fallback != null)
					{
						this.m_fallbackBuffer = this.m_fallback.CreateFallbackBuffer();
					}
					else
					{
						this.m_fallbackBuffer = EncoderFallback.ReplacementFallback.CreateFallbackBuffer();
					}
				}
				return this.m_fallbackBuffer;
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x060067DC RID: 26588 RVA: 0x0015FE27 File Offset: 0x0015E027
		internal bool InternalHasFallbackBuffer
		{
			get
			{
				return this.m_fallbackBuffer != null;
			}
		}

		/// <summary>When overridden in a derived class, sets the encoder back to its initial state.</summary>
		// Token: 0x060067DD RID: 26589 RVA: 0x0015FE34 File Offset: 0x0015E034
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual void Reset()
		{
			char[] array = new char[0];
			byte[] array2 = new byte[this.GetByteCount(array, 0, 0, true)];
			this.GetBytes(array, 0, 0, array2, 0, true);
			if (this.m_fallbackBuffer != null)
			{
				this.m_fallbackBuffer.Reset();
			}
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding a set of characters from the specified character array. A parameter indicates whether to clear the internal state of the encoder after the calculation.</summary>
		/// <param name="chars">The character array containing the set of characters to encode.</param>
		/// <param name="index">The index of the first character to encode.</param>
		/// <param name="count">The number of characters to encode.</param>
		/// <param name="flush">
		///   <see langword="true" /> to simulate clearing the internal state of the encoder after the calculation; otherwise, <see langword="false" />.</param>
		/// <returns>The number of bytes produced by encoding the specified characters and any characters in the internal buffer.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoder.Fallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060067DE RID: 26590
		[__DynamicallyInvokable]
		public abstract int GetByteCount(char[] chars, int index, int count, bool flush);

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding a set of characters starting at the specified character pointer. A parameter indicates whether to clear the internal state of the encoder after the calculation.</summary>
		/// <param name="chars">A pointer to the first character to encode.</param>
		/// <param name="count">The number of characters to encode.</param>
		/// <param name="flush">
		///   <see langword="true" /> to simulate clearing the internal state of the encoder after the calculation; otherwise, <see langword="false" />.</param>
		/// <returns>The number of bytes produced by encoding the specified characters and any characters in the internal buffer.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic .NET).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoder.Fallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060067DF RID: 26591 RVA: 0x0015FE78 File Offset: 0x0015E078
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetByteCount(char* chars, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = chars[i];
			}
			return this.GetByteCount(array, 0, count, flush);
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified character array and any characters in the internal buffer into the specified byte array. A parameter indicates whether to clear the internal state of the encoder after the conversion.</summary>
		/// <param name="chars">The character array containing the set of characters to encode.</param>
		/// <param name="charIndex">The index of the first character to encode.</param>
		/// <param name="charCount">The number of characters to encode.</param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes.</param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes.</param>
		/// <param name="flush">
		///   <see langword="true" /> to clear the internal state of the encoder after the conversion; otherwise, <see langword="false" />.</param>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" /> (<see langword="Nothing" />).  
		/// -or-  
		/// <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.  
		/// -or-  
		/// <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.  
		/// -or-  
		/// <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoder.Fallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060067E0 RID: 26592
		[__DynamicallyInvokable]
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush);

		/// <summary>When overridden in a derived class, encodes a set of characters starting at the specified character pointer and any characters in the internal buffer into a sequence of bytes that are stored starting at the specified byte pointer. A parameter indicates whether to clear the internal state of the encoder after the conversion.</summary>
		/// <param name="chars">A pointer to the first character to encode.</param>
		/// <param name="charCount">The number of characters to encode.</param>
		/// <param name="bytes">A pointer to the location at which to start writing the resulting sequence of bytes.</param>
		/// <param name="byteCount">The maximum number of bytes to write.</param>
		/// <param name="flush">
		///   <see langword="true" /> to clear the internal state of the encoder after the conversion; otherwise, <see langword="false" />.</param>
		/// <returns>The actual number of bytes written at the location indicated by the <paramref name="bytes" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" /> (<see langword="Nothing" />).  
		/// -or-  
		/// <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> or <paramref name="byteCount" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="byteCount" /> is less than the resulting number of bytes.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoder.Fallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060067E1 RID: 26593 RVA: 0x0015FEE0 File Offset: 0x0015E0E0
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			char[] array = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = chars[i];
			}
			byte[] array2 = new byte[byteCount];
			int bytes2 = this.GetBytes(array, 0, charCount, array2, 0, flush);
			if (bytes2 < byteCount)
			{
				byteCount = bytes2;
			}
			for (int i = 0; i < byteCount; i++)
			{
				bytes[i] = array2[i];
			}
			return byteCount;
		}

		/// <summary>Converts an array of Unicode characters to an encoded byte sequence and stores the result in an array of bytes.</summary>
		/// <param name="chars">An array of characters to convert.</param>
		/// <param name="charIndex">The first element of <paramref name="chars" /> to convert.</param>
		/// <param name="charCount">The number of elements of <paramref name="chars" /> to convert.</param>
		/// <param name="bytes">An array where the converted bytes are stored.</param>
		/// <param name="byteIndex">The first element of <paramref name="bytes" /> in which data is stored.</param>
		/// <param name="byteCount">The maximum number of elements of <paramref name="bytes" /> to use in the conversion.</param>
		/// <param name="flush">
		///   <see langword="true" /> to indicate no further data is to be converted; otherwise, <see langword="false" />.</param>
		/// <param name="charsUsed">When this method returns, contains the number of characters from <paramref name="chars" /> that were used in the conversion. This parameter is passed uninitialized.</param>
		/// <param name="bytesUsed">When this method returns, contains the number of bytes that were produced by the conversion. This parameter is passed uninitialized.</param>
		/// <param name="completed">When this method returns, contains <see langword="true" /> if all the characters specified by <paramref name="charCount" /> were converted; otherwise, <see langword="false" />. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> or <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" />, <paramref name="charCount" />, <paramref name="byteIndex" />, or <paramref name="byteCount" /> is less than zero.  
		/// -or-  
		/// The length of <paramref name="chars" /> - <paramref name="charIndex" /> is less than <paramref name="charCount" />.  
		/// -or-  
		/// The length of <paramref name="bytes" /> - <paramref name="byteIndex" /> is less than <paramref name="byteCount" />.</exception>
		/// <exception cref="T:System.ArgumentException">The output buffer is too small to contain any of the converted input. The output buffer should be greater than or equal to the size indicated by the <see cref="Overload:System.Text.Encoder.GetByteCount" /> method.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoder.Fallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060067E2 RID: 26594 RVA: 0x0015FF94 File Offset: 0x0015E194
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			for (charsUsed = charCount; charsUsed > 0; charsUsed /= 2)
			{
				if (this.GetByteCount(chars, charIndex, charsUsed, flush) <= byteCount)
				{
					bytesUsed = this.GetBytes(chars, charIndex, charsUsed, bytes, byteIndex, flush);
					completed = charsUsed == charCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
					return;
				}
				flush = false;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
		}

		/// <summary>Converts a buffer of Unicode characters to an encoded byte sequence and stores the result in another buffer.</summary>
		/// <param name="chars">The address of a string of UTF-16 encoded characters to convert.</param>
		/// <param name="charCount">The number of characters in <paramref name="chars" /> to convert.</param>
		/// <param name="bytes">The address of a buffer to store the converted bytes.</param>
		/// <param name="byteCount">The maximum number of bytes in <paramref name="bytes" /> to use in the conversion.</param>
		/// <param name="flush">
		///   <see langword="true" /> to indicate no further data is to be converted; otherwise, <see langword="false" />.</param>
		/// <param name="charsUsed">When this method returns, contains the number of characters from <paramref name="chars" /> that were used in the conversion. This parameter is passed uninitialized.</param>
		/// <param name="bytesUsed">When this method returns, contains the number of bytes that were used in the conversion. This parameter is passed uninitialized.</param>
		/// <param name="completed">When this method returns, contains <see langword="true" /> if all the characters specified by <paramref name="charCount" /> were converted; otherwise, <see langword="false" />. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> or <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> or <paramref name="byteCount" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The output buffer is too small to contain any of the converted input. The output buffer should be greater than or equal to the size indicated by the <see cref="Overload:System.Text.Encoder.GetByteCount" /> method.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoder.Fallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060067E3 RID: 26595 RVA: 0x001600C8 File Offset: 0x0015E2C8
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			for (charsUsed = charCount; charsUsed > 0; charsUsed /= 2)
			{
				if (this.GetByteCount(chars, charsUsed, flush) <= byteCount)
				{
					bytesUsed = this.GetBytes(chars, charsUsed, bytes, byteCount, flush);
					completed = charsUsed == charCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
					return;
				}
				flush = false;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
		}

		// Token: 0x04002E5C RID: 11868
		internal EncoderFallback m_fallback;

		// Token: 0x04002E5D RID: 11869
		[NonSerialized]
		internal EncoderFallbackBuffer m_fallbackBuffer;
	}
}
