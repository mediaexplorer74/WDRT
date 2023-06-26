using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	/// <summary>Converts a sequence of encoded bytes into a set of characters.</summary>
	// Token: 0x02000A5B RID: 2651
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Decoder
	{
		// Token: 0x06006778 RID: 26488 RVA: 0x0015EAD1 File Offset: 0x0015CCD1
		internal void SerializeDecoder(SerializationInfo info)
		{
			info.AddValue("m_fallback", this.m_fallback);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.Decoder" /> class.</summary>
		// Token: 0x06006779 RID: 26489 RVA: 0x0015EAE4 File Offset: 0x0015CCE4
		[__DynamicallyInvokable]
		protected Decoder()
		{
		}

		/// <summary>Gets or sets a <see cref="T:System.Text.DecoderFallback" /> object for the current <see cref="T:System.Text.Decoder" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.DecoderFallback" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentException">A new value cannot be assigned in a set operation because the current <see cref="T:System.Text.DecoderFallbackBuffer" /> object contains data that has not been decoded yet.</exception>
		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x0600677A RID: 26490 RVA: 0x0015EAEC File Offset: 0x0015CCEC
		// (set) Token: 0x0600677B RID: 26491 RVA: 0x0015EAF4 File Offset: 0x0015CCF4
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public DecoderFallback Fallback
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

		/// <summary>Gets the <see cref="T:System.Text.DecoderFallbackBuffer" /> object associated with the current <see cref="T:System.Text.Decoder" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.DecoderFallbackBuffer" /> object.</returns>
		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x0600677C RID: 26492 RVA: 0x0015EB48 File Offset: 0x0015CD48
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public DecoderFallbackBuffer FallbackBuffer
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
						this.m_fallbackBuffer = DecoderFallback.ReplacementFallback.CreateFallbackBuffer();
					}
				}
				return this.m_fallbackBuffer;
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x0600677D RID: 26493 RVA: 0x0015EB83 File Offset: 0x0015CD83
		internal bool InternalHasFallbackBuffer
		{
			get
			{
				return this.m_fallbackBuffer != null;
			}
		}

		/// <summary>When overridden in a derived class, sets the decoder back to its initial state.</summary>
		// Token: 0x0600677E RID: 26494 RVA: 0x0015EB90 File Offset: 0x0015CD90
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual void Reset()
		{
			byte[] array = new byte[0];
			char[] array2 = new char[this.GetCharCount(array, 0, 0, true)];
			this.GetChars(array, 0, 0, array2, 0, true);
			if (this.m_fallbackBuffer != null)
			{
				this.m_fallbackBuffer.Reset();
			}
		}

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding a sequence of bytes from the specified byte array.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="index">The index of the first byte to decode.</param>
		/// <param name="count">The number of bytes to decode.</param>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes and any bytes in the internal buffer.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Decoder.Fallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x0600677F RID: 26495
		[__DynamicallyInvokable]
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding a sequence of bytes from the specified byte array. A parameter indicates whether to clear the internal state of the decoder after the calculation.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="index">The index of the first byte to decode.</param>
		/// <param name="count">The number of bytes to decode.</param>
		/// <param name="flush">
		///   <see langword="true" /> to simulate clearing the internal state of the encoder after the calculation; otherwise, <see langword="false" />.</param>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes and any bytes in the internal buffer.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Decoder.Fallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006780 RID: 26496 RVA: 0x0015EBD4 File Offset: 0x0015CDD4
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			return this.GetCharCount(bytes, index, count);
		}

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding a sequence of bytes starting at the specified byte pointer. A parameter indicates whether to clear the internal state of the decoder after the calculation.</summary>
		/// <param name="bytes">A pointer to the first byte to decode.</param>
		/// <param name="count">The number of bytes to decode.</param>
		/// <param name="flush">
		///   <see langword="true" /> to simulate clearing the internal state of the encoder after the calculation; otherwise, <see langword="false" />.</param>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes and any bytes in the internal buffer.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic .NET).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Decoder.Fallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006781 RID: 26497 RVA: 0x0015EBE0 File Offset: 0x0015CDE0
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetCharCount(byte* bytes, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = bytes[i];
			}
			return this.GetCharCount(array, 0, count);
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array and any bytes in the internal buffer into the specified character array.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="byteIndex">The index of the first byte to decode.</param>
		/// <param name="byteCount">The number of bytes to decode.</param>
		/// <param name="chars">The character array to contain the resulting set of characters.</param>
		/// <param name="charIndex">The index at which to start writing the resulting set of characters.</param>
		/// <returns>The actual number of characters written into <paramref name="chars" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).  
		/// -or-  
		/// <paramref name="chars" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteIndex" /> or <paramref name="byteCount" /> or <paramref name="charIndex" /> is less than zero.  
		/// -or-  
		/// <paramref name="byteindex" /> and <paramref name="byteCount" /> do not denote a valid range in <paramref name="bytes" />.  
		/// -or-  
		/// <paramref name="charIndex" /> is not a valid index in <paramref name="chars" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="chars" /> does not have enough capacity from <paramref name="charIndex" /> to the end of the array to accommodate the resulting characters.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Decoder.Fallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006782 RID: 26498
		[__DynamicallyInvokable]
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array and any bytes in the internal buffer into the specified character array. A parameter indicates whether to clear the internal state of the decoder after the conversion.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="byteIndex">The index of the first byte to decode.</param>
		/// <param name="byteCount">The number of bytes to decode.</param>
		/// <param name="chars">The character array to contain the resulting set of characters.</param>
		/// <param name="charIndex">The index at which to start writing the resulting set of characters.</param>
		/// <param name="flush">
		///   <see langword="true" /> to clear the internal state of the decoder after the conversion; otherwise, <see langword="false" />.</param>
		/// <returns>The actual number of characters written into the <paramref name="chars" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).  
		/// -or-  
		/// <paramref name="chars" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteIndex" /> or <paramref name="byteCount" /> or <paramref name="charIndex" /> is less than zero.  
		/// -or-  
		/// <paramref name="byteindex" /> and <paramref name="byteCount" /> do not denote a valid range in <paramref name="bytes" />.  
		/// -or-  
		/// <paramref name="charIndex" /> is not a valid index in <paramref name="chars" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="chars" /> does not have enough capacity from <paramref name="charIndex" /> to the end of the array to accommodate the resulting characters.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Decoder.Fallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006783 RID: 26499 RVA: 0x0015EC43 File Offset: 0x0015CE43
		[__DynamicallyInvokable]
		public virtual int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes starting at the specified byte pointer and any bytes in the internal buffer into a set of characters that are stored starting at the specified character pointer. A parameter indicates whether to clear the internal state of the decoder after the conversion.</summary>
		/// <param name="bytes">A pointer to the first byte to decode.</param>
		/// <param name="byteCount">The number of bytes to decode.</param>
		/// <param name="chars">A pointer to the location at which to start writing the resulting set of characters.</param>
		/// <param name="charCount">The maximum number of characters to write.</param>
		/// <param name="flush">
		///   <see langword="true" /> to clear the internal state of the decoder after the conversion; otherwise, <see langword="false" />.</param>
		/// <returns>The actual number of characters written at the location indicated by the <paramref name="chars" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).  
		/// -or-  
		/// <paramref name="chars" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> or <paramref name="charCount" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="charCount" /> is less than the resulting number of characters.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Decoder.Fallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006784 RID: 26500 RVA: 0x0015EC54 File Offset: 0x0015CE54
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			byte[] array = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				array[i] = bytes[i];
			}
			char[] array2 = new char[charCount];
			int chars2 = this.GetChars(array, 0, byteCount, array2, 0, flush);
			if (chars2 < charCount)
			{
				charCount = chars2;
			}
			for (int i = 0; i < charCount; i++)
			{
				chars[i] = array2[i];
			}
			return charCount;
		}

		/// <summary>Converts an array of encoded bytes to UTF-16 encoded characters and stores the result in a character array.</summary>
		/// <param name="bytes">A byte array to convert.</param>
		/// <param name="byteIndex">The first element of <paramref name="bytes" /> to convert.</param>
		/// <param name="byteCount">The number of elements of <paramref name="bytes" /> to convert.</param>
		/// <param name="chars">An array to store the converted characters.</param>
		/// <param name="charIndex">The first element of <paramref name="chars" /> in which data is stored.</param>
		/// <param name="charCount">The maximum number of elements of <paramref name="chars" /> to use in the conversion.</param>
		/// <param name="flush">
		///   <see langword="true" /> to indicate that no further data is to be converted; otherwise, <see langword="false" />.</param>
		/// <param name="bytesUsed">When this method returns, contains the number of bytes that were used in the conversion. This parameter is passed uninitialized.</param>
		/// <param name="charsUsed">When this method returns, contains the number of characters from <paramref name="chars" /> that were produced by the conversion. This parameter is passed uninitialized.</param>
		/// <param name="completed">When this method returns, contains <see langword="true" /> if all the characters specified by <paramref name="byteCount" /> were converted; otherwise, <see langword="false" />. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> or <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" />, <paramref name="charCount" />, <paramref name="byteIndex" />, or <paramref name="byteCount" /> is less than zero.  
		/// -or-  
		/// The length of <paramref name="chars" /> - <paramref name="charIndex" /> is less than <paramref name="charCount" />.  
		/// -or-  
		/// The length of <paramref name="bytes" /> - <paramref name="byteIndex" /> is less than <paramref name="byteCount" />.</exception>
		/// <exception cref="T:System.ArgumentException">The output buffer is too small to contain any of the converted input. The output buffer should be greater than or equal to the size indicated by the <see cref="Overload:System.Text.Decoder.GetCharCount" /> method.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Decoder.Fallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006785 RID: 26501 RVA: 0x0015ED08 File Offset: 0x0015CF08
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			for (bytesUsed = byteCount; bytesUsed > 0; bytesUsed /= 2)
			{
				if (this.GetCharCount(bytes, byteIndex, bytesUsed, flush) <= charCount)
				{
					charsUsed = this.GetChars(bytes, byteIndex, bytesUsed, chars, charIndex, flush);
					completed = bytesUsed == byteCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
					return;
				}
				flush = false;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
		}

		/// <summary>Converts a buffer of encoded bytes to UTF-16 encoded characters and stores the result in another buffer.</summary>
		/// <param name="bytes">The address of a buffer that contains the byte sequences to convert.</param>
		/// <param name="byteCount">The number of bytes in <paramref name="bytes" /> to convert.</param>
		/// <param name="chars">The address of a buffer to store the converted characters.</param>
		/// <param name="charCount">The maximum number of characters in <paramref name="chars" /> to use in the conversion.</param>
		/// <param name="flush">
		///   <see langword="true" /> to indicate no further data is to be converted; otherwise, <see langword="false" />.</param>
		/// <param name="bytesUsed">When this method returns, contains the number of bytes that were produced by the conversion. This parameter is passed uninitialized.</param>
		/// <param name="charsUsed">When this method returns, contains the number of characters from <paramref name="chars" /> that were used in the conversion. This parameter is passed uninitialized.</param>
		/// <param name="completed">When this method returns, contains <see langword="true" /> if all the characters specified by <paramref name="byteCount" /> were converted; otherwise, <see langword="false" />. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> or <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> or <paramref name="byteCount" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The output buffer is too small to contain any of the converted input. The output buffer should be greater than or equal to the size indicated by the <see cref="Overload:System.Text.Decoder.GetCharCount" /> method.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Decoder.Fallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006786 RID: 26502 RVA: 0x0015EE3C File Offset: 0x0015D03C
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			for (bytesUsed = byteCount; bytesUsed > 0; bytesUsed /= 2)
			{
				if (this.GetCharCount(bytes, bytesUsed, flush) <= charCount)
				{
					charsUsed = this.GetChars(bytes, bytesUsed, chars, charCount, flush);
					completed = bytesUsed == byteCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
					return;
				}
				flush = false;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
		}

		// Token: 0x04002E42 RID: 11842
		internal DecoderFallback m_fallback;

		// Token: 0x04002E43 RID: 11843
		[NonSerialized]
		internal DecoderFallbackBuffer m_fallbackBuffer;
	}
}
