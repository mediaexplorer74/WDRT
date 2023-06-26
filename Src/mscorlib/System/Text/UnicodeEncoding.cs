using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	/// <summary>Represents a UTF-16 encoding of Unicode characters.</summary>
	// Token: 0x02000A7E RID: 2686
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class UnicodeEncoding : Encoding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.UnicodeEncoding" /> class.</summary>
		// Token: 0x060068F5 RID: 26869 RVA: 0x00165807 File Offset: 0x00163A07
		[__DynamicallyInvokable]
		public UnicodeEncoding()
			: this(false, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.UnicodeEncoding" /> class. Parameters specify whether to use the big endian byte order and whether the <see cref="M:System.Text.UnicodeEncoding.GetPreamble" /> method returns a Unicode byte order mark.</summary>
		/// <param name="bigEndian">
		///   <see langword="true" /> to use the big endian byte order (most significant byte first), or <see langword="false" /> to use the little endian byte order (least significant byte first).</param>
		/// <param name="byteOrderMark">
		///   <see langword="true" /> to specify that the <see cref="M:System.Text.UnicodeEncoding.GetPreamble" /> method returns a Unicode byte order mark; otherwise, <see langword="false" />.</param>
		// Token: 0x060068F6 RID: 26870 RVA: 0x00165811 File Offset: 0x00163A11
		[__DynamicallyInvokable]
		public UnicodeEncoding(bool bigEndian, bool byteOrderMark)
			: this(bigEndian, byteOrderMark, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.UnicodeEncoding" /> class. Parameters specify whether to use the big endian byte order, whether to provide a Unicode byte order mark, and whether to throw an exception when an invalid encoding is detected.</summary>
		/// <param name="bigEndian">
		///   <see langword="true" /> to use the big endian byte order (most significant byte first); <see langword="false" /> to use the little endian byte order (least significant byte first).</param>
		/// <param name="byteOrderMark">
		///   <see langword="true" /> to specify that the <see cref="M:System.Text.UnicodeEncoding.GetPreamble" /> method returns a Unicode byte order mark; otherwise, <see langword="false" />.</param>
		/// <param name="throwOnInvalidBytes">
		///   <see langword="true" /> to specify that an exception should be thrown when an invalid encoding is detected; otherwise, <see langword="false" />.</param>
		// Token: 0x060068F7 RID: 26871 RVA: 0x0016581C File Offset: 0x00163A1C
		[__DynamicallyInvokable]
		public UnicodeEncoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidBytes)
			: base(bigEndian ? 1201 : 1200)
		{
			this.isThrowException = throwOnInvalidBytes;
			this.bigEndian = bigEndian;
			this.byteOrderMark = byteOrderMark;
			if (this.isThrowException)
			{
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x060068F8 RID: 26872 RVA: 0x00165868 File Offset: 0x00163A68
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.isThrowException = false;
		}

		// Token: 0x060068F9 RID: 26873 RVA: 0x00165874 File Offset: 0x00163A74
		internal override void SetDefaultFallbacks()
		{
			if (this.isThrowException)
			{
				this.encoderFallback = EncoderFallback.ExceptionFallback;
				this.decoderFallback = DecoderFallback.ExceptionFallback;
				return;
			}
			this.encoderFallback = new EncoderReplacementFallback("\ufffd");
			this.decoderFallback = new DecoderReplacementFallback("\ufffd");
		}

		/// <summary>Calculates the number of bytes produced by encoding a set of characters from the specified character array.</summary>
		/// <param name="chars">The character array containing the set of characters to encode.</param>
		/// <param name="index">The index of the first character to encode.</param>
		/// <param name="count">The number of characters to encode.</param>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />.  
		/// -or-  
		/// The resulting number of bytes is greater than the maximum number that can be returned as an integer.</exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="chars" /> contains an invalid sequence of characters.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060068FA RID: 26874 RVA: 0x001658C0 File Offset: 0x00163AC0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetByteCount(char[] chars, int index, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (chars.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (chars.Length == 0)
			{
				return 0;
			}
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			return this.GetByteCount(ptr + index, count, null);
		}

		/// <summary>Calculates the number of bytes produced by encoding the characters in the specified string.</summary>
		/// <param name="s">The string that contains the set of characters to encode.</param>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting number of bytes is greater than the maximum number that can be returned as an integer.</exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="s" /> contains an invalid sequence of characters.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060068FB RID: 26875 RVA: 0x00165958 File Offset: 0x00163B58
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return this.GetByteCount(ptr, s.Length, null);
		}

		/// <summary>Calculates the number of bytes produced by encoding a set of characters starting at the specified character pointer.</summary>
		/// <param name="chars">A pointer to the first character to encode.</param>
		/// <param name="count">The number of characters to encode.</param>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.  
		/// -or-  
		/// The resulting number of bytes is greater than the maximum number that can be returned as an integer.</exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled and <paramref name="chars" /> contains an invalid sequence of characters.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060068FC RID: 26876 RVA: 0x00165991 File Offset: 0x00163B91
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetByteCount(chars, count, null);
		}

		/// <summary>Encodes a set of characters from the specified <see cref="T:System.String" /> into the specified byte array.</summary>
		/// <param name="s">The string containing the set of characters to encode.</param>
		/// <param name="charIndex">The index of the first character to encode.</param>
		/// <param name="charCount">The number of characters to encode.</param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes.</param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes.</param>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.  
		/// -or-  
		/// <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.  
		/// -or-  
		/// <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />.</exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="s" /> contains an invalid sequence of characters.  
		///  -or-  
		///  <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060068FD RID: 26877 RVA: 0x001659D0 File Offset: 0x00163BD0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null || bytes == null)
			{
				throw new ArgumentNullException((s == null) ? "s" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (s.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("s", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int num = bytes.Length - byteIndex;
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			byte[] array;
			byte* ptr2;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, num, null);
		}

		/// <summary>Encodes a set of characters from the specified character array into the specified byte array.</summary>
		/// <param name="chars">The character array containing the set of characters to encode.</param>
		/// <param name="charIndex">The index of the first character to encode.</param>
		/// <param name="charCount">The number of characters to encode.</param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes.</param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes.</param>
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
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="chars" /> contains an invalid sequence of characters.  
		///  -or-  
		///  <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060068FE RID: 26878 RVA: 0x00165AC4 File Offset: 0x00163CC4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (chars.Length == 0)
			{
				return 0;
			}
			int num = bytes.Length - byteIndex;
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			byte[] array;
			byte* ptr2;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, num, null);
		}

		/// <summary>Encodes a set of characters starting at the specified character pointer into a sequence of bytes that are stored starting at the specified byte pointer.</summary>
		/// <param name="chars">A pointer to the first character to encode.</param>
		/// <param name="charCount">The number of characters to encode.</param>
		/// <param name="bytes">A pointer to the location at which to start writing the resulting sequence of bytes.</param>
		/// <param name="byteCount">The maximum number of bytes to write.</param>
		/// <returns>The actual number of bytes written at the location indicated by the <paramref name="bytes" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" /> (<see langword="Nothing" />).  
		/// -or-  
		/// <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> or <paramref name="byteCount" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="chars" /> contains an invalid sequence of characters.  
		///  -or-  
		///  <paramref name="byteCount" /> is less than the resulting number of bytes.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x060068FF RID: 26879 RVA: 0x00165BC0 File Offset: 0x00163DC0
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetBytes(chars, charCount, bytes, byteCount, null);
		}

		/// <summary>Calculates the number of characters produced by decoding a sequence of bytes from the specified byte array.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="index">The index of the first byte to decode.</param>
		/// <param name="count">The number of bytes to decode.</param>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />.  
		/// -or-  
		/// The resulting number of bytes is greater than the maximum number that can be returned as an integer.</exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="bytes" /> contains an invalid sequence of bytes.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006900 RID: 26880 RVA: 0x00165C30 File Offset: 0x00163E30
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetCharCount(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (bytes.Length == 0)
			{
				return 0;
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return this.GetCharCount(ptr + index, count, null);
		}

		/// <summary>Calculates the number of characters produced by decoding a sequence of bytes starting at the specified byte pointer.</summary>
		/// <param name="bytes">A pointer to the first byte to decode.</param>
		/// <param name="count">The number of bytes to decode.</param>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.  
		/// -or-  
		/// The resulting number of bytes is greater than the maximum number that can be returned as an integer.</exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="bytes" /> contains an invalid sequence of bytes.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006901 RID: 26881 RVA: 0x00165CC3 File Offset: 0x00163EC3
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetCharCount(bytes, count, null);
		}

		/// <summary>Decodes a sequence of bytes from the specified byte array into the specified character array.</summary>
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
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="bytes" /> contains an invalid sequence of bytes.  
		///  -or-  
		///  <paramref name="chars" /> does not have enough capacity from <paramref name="charIndex" /> to the end of the array to accommodate the resulting characters.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006902 RID: 26882 RVA: 0x00165D04 File Offset: 0x00163F04
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (bytes.Length == 0)
			{
				return 0;
			}
			int num = chars.Length - charIndex;
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			char[] array;
			char* ptr2;
			if ((array = chars) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, num, null);
		}

		/// <summary>Decodes a sequence of bytes starting at the specified byte pointer into a set of characters that are stored starting at the specified character pointer.</summary>
		/// <param name="bytes">A pointer to the first byte to decode.</param>
		/// <param name="byteCount">The number of bytes to decode.</param>
		/// <param name="chars">A pointer to the location at which to start writing the resulting set of characters.</param>
		/// <param name="charCount">The maximum number of characters to write.</param>
		/// <returns>The actual number of characters written at the location indicated by the <paramref name="chars" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).  
		/// -or-  
		/// <paramref name="chars" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> or <paramref name="charCount" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="bytes" /> contains an invalid sequence of bytes.  
		///  -or-  
		///  <paramref name="charCount" /> is less than the resulting number of characters.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006903 RID: 26883 RVA: 0x00165E00 File Offset: 0x00164000
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetChars(bytes, byteCount, chars, charCount, null);
		}

		/// <summary>Decodes a range of bytes from a byte array into a string.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="index">The index of the first byte to decode.</param>
		/// <param name="count">The number of bytes to decode.</param>
		/// <returns>A <see cref="T:System.String" /> object containing the results of decoding the specified sequence of bytes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />.</exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="bytes" /> contains an invalid sequence of bytes.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006904 RID: 26884 RVA: 0x00165E70 File Offset: 0x00164070
		[SecuritySafeCritical]
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public unsafe override string GetString(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return string.CreateStringFromEncoding(ptr + index, count, this);
		}

		// Token: 0x06006905 RID: 26885 RVA: 0x00165F08 File Offset: 0x00164108
		[SecurityCritical]
		internal unsafe override int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			int num = count << 1;
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			char* ptr = chars;
			char* ptr2 = chars + count;
			char c = '\0';
			bool flag = false;
			ulong* ptr3 = (ulong*)(ptr2 - 3);
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			if (encoder != null)
			{
				c = encoder.charLeftOver;
				if (c > '\0')
				{
					num += 2;
				}
				if (encoder.InternalHasFallbackBuffer)
				{
					encoderFallbackBuffer = encoder.FallbackBuffer;
					if (encoderFallbackBuffer.Remaining > 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
						{
							this.EncodingName,
							encoder.Fallback.GetType()
						}));
					}
					encoderFallbackBuffer.InternalInitialize(ptr, ptr2, encoder, false);
				}
			}
			for (;;)
			{
				char c2;
				if ((c2 = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr2)
				{
					if (c2 == '\0')
					{
						if (!this.bigEndian && c == '\0' && (chars & 7L) == null)
						{
							ulong* ptr4;
							for (ptr4 = (ulong*)chars; ptr4 < ptr3; ptr4++)
							{
								if ((9223512776490647552UL & *ptr4) != 0UL)
								{
									ulong num2 = (17870556004450629632UL & *ptr4) ^ 15564677810327967744UL;
									if (((num2 & 18446462598732840960UL) == 0UL || (num2 & 281470681743360UL) == 0UL || (num2 & (ulong)(-65536)) == 0UL || (num2 & 65535UL) == 0UL) && ((18158790778715962368UL & *ptr4) ^ 15852908186546788352UL) != 0UL)
									{
										break;
									}
								}
							}
							chars = (char*)ptr4;
							if (chars >= ptr2)
							{
								goto IL_278;
							}
						}
						c2 = *chars;
						chars++;
					}
					else
					{
						num += 2;
					}
					if (c2 >= '\ud800' && c2 <= '\udfff')
					{
						if (c2 <= '\udbff')
						{
							if (c > '\0')
							{
								chars--;
								num -= 2;
								if (encoderFallbackBuffer == null)
								{
									if (encoder == null)
									{
										encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
									}
									else
									{
										encoderFallbackBuffer = encoder.FallbackBuffer;
									}
									encoderFallbackBuffer.InternalInitialize(ptr, ptr2, encoder, false);
								}
								encoderFallbackBuffer.InternalFallback(c, ref chars);
								c = '\0';
								continue;
							}
							c = c2;
							continue;
						}
						else
						{
							if (c == '\0')
							{
								num -= 2;
								if (encoderFallbackBuffer == null)
								{
									if (encoder == null)
									{
										encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
									}
									else
									{
										encoderFallbackBuffer = encoder.FallbackBuffer;
									}
									encoderFallbackBuffer.InternalInitialize(ptr, ptr2, encoder, false);
								}
								encoderFallbackBuffer.InternalFallback(c2, ref chars);
								continue;
							}
							c = '\0';
							continue;
						}
					}
					else
					{
						if (c > '\0')
						{
							chars--;
							if (encoderFallbackBuffer == null)
							{
								if (encoder == null)
								{
									encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
								}
								else
								{
									encoderFallbackBuffer = encoder.FallbackBuffer;
								}
								encoderFallbackBuffer.InternalInitialize(ptr, ptr2, encoder, false);
							}
							encoderFallbackBuffer.InternalFallback(c, ref chars);
							num -= 2;
							c = '\0';
							continue;
						}
						continue;
					}
				}
				IL_278:
				if (c <= '\0')
				{
					return num;
				}
				num -= 2;
				if (encoder != null && !encoder.MustFlush)
				{
					return num;
				}
				if (flag)
				{
					break;
				}
				if (encoderFallbackBuffer == null)
				{
					if (encoder == null)
					{
						encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
					}
					else
					{
						encoderFallbackBuffer = encoder.FallbackBuffer;
					}
					encoderFallbackBuffer.InternalInitialize(ptr, ptr2, encoder, false);
				}
				encoderFallbackBuffer.InternalFallback(c, ref chars);
				c = '\0';
				flag = true;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", new object[] { c }), "chars");
		}

		// Token: 0x06006906 RID: 26886 RVA: 0x00166208 File Offset: 0x00164408
		[SecurityCritical]
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			char c = '\0';
			bool flag = false;
			byte* ptr = bytes + byteCount;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			char* ptr4 = chars;
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			if (encoder != null)
			{
				c = encoder.charLeftOver;
				if (encoder.InternalHasFallbackBuffer)
				{
					encoderFallbackBuffer = encoder.FallbackBuffer;
					if (encoderFallbackBuffer.Remaining > 0 && encoder.m_throwOnOverflow)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
						{
							this.EncodingName,
							encoder.Fallback.GetType()
						}));
					}
					encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, false);
				}
			}
			for (;;)
			{
				char c2;
				if ((c2 = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr2)
				{
					if (c2 == '\0')
					{
						if (!this.bigEndian && (chars & 7L) == null && (bytes & 7L) == null && c == '\0')
						{
							ulong* ptr5 = (ulong*)(chars - 3 + (((long)(ptr - bytes) >> 1 < (long)(ptr2 - chars)) ? ((long)(ptr - bytes) >> 1) : ((long)(ptr2 - chars))) * 2L / 8L);
							ulong* ptr6 = (ulong*)chars;
							ulong* ptr7 = (ulong*)bytes;
							while (ptr6 < ptr5)
							{
								if ((9223512776490647552UL & *ptr6) != 0UL)
								{
									ulong num = (17870556004450629632UL & *ptr6) ^ 15564677810327967744UL;
									if (((num & 18446462598732840960UL) == 0UL || (num & 281470681743360UL) == 0UL || (num & (ulong)(-65536)) == 0UL || (num & 65535UL) == 0UL) && ((18158790778715962368UL & *ptr6) ^ 15852908186546788352UL) != 0UL)
									{
										break;
									}
								}
								*ptr7 = *ptr6;
								ptr6++;
								ptr7++;
							}
							chars = (char*)ptr6;
							bytes = (byte*)ptr7;
							if (chars >= ptr2)
							{
								goto IL_477;
							}
						}
						else if (c == '\0' && !this.bigEndian && ((byte*)chars & 7L) != (bytes & 7L) && (bytes & 1) == 0)
						{
							long num2 = (((long)(ptr - bytes) >> 1 < (long)(ptr2 - chars)) ? ((long)(ptr - bytes) >> 1) : ((long)(ptr2 - chars)));
							char* ptr8 = (char*)bytes;
							char* ptr9 = chars + num2 * 2L / 2L - 1;
							while (chars < ptr9)
							{
								if (*chars >= '\ud800' && *chars <= '\udfff')
								{
									if (*chars >= '\udc00' || chars[1] < '\udc00')
									{
										break;
									}
									if (chars[1] > '\udfff')
									{
										break;
									}
								}
								else if (chars[1] >= '\ud800' && chars[1] <= '\udfff')
								{
									*ptr8 = *chars;
									ptr8++;
									chars++;
									continue;
								}
								*ptr8 = *chars;
								ptr8[1] = chars[1];
								ptr8 += 2;
								chars += 2;
							}
							bytes = (byte*)ptr8;
							if (chars >= ptr2)
							{
								goto IL_477;
							}
						}
						c2 = *chars;
						chars++;
					}
					if (c2 >= '\ud800' && c2 <= '\udfff')
					{
						if (c2 <= '\udbff')
						{
							if (c > '\0')
							{
								chars--;
								if (encoderFallbackBuffer == null)
								{
									if (encoder == null)
									{
										encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
									}
									else
									{
										encoderFallbackBuffer = encoder.FallbackBuffer;
									}
									encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
								}
								encoderFallbackBuffer.InternalFallback(c, ref chars);
								c = '\0';
								continue;
							}
							c = c2;
							continue;
						}
						else
						{
							if (c == '\0')
							{
								if (encoderFallbackBuffer == null)
								{
									if (encoder == null)
									{
										encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
									}
									else
									{
										encoderFallbackBuffer = encoder.FallbackBuffer;
									}
									encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
								}
								encoderFallbackBuffer.InternalFallback(c2, ref chars);
								continue;
							}
							if (bytes + 3 >= ptr)
							{
								if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
								{
									encoderFallbackBuffer.MovePrevious();
									encoderFallbackBuffer.MovePrevious();
								}
								else
								{
									chars -= 2;
								}
								base.ThrowBytesOverflow(encoder, bytes == ptr3);
								c = '\0';
								goto IL_477;
							}
							if (this.bigEndian)
							{
								*(bytes++) = (byte)(c >> 8);
								*(bytes++) = (byte)c;
							}
							else
							{
								*(bytes++) = (byte)c;
								*(bytes++) = (byte)(c >> 8);
							}
							c = '\0';
						}
					}
					else if (c > '\0')
					{
						chars--;
						if (encoderFallbackBuffer == null)
						{
							if (encoder == null)
							{
								encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
							}
							else
							{
								encoderFallbackBuffer = encoder.FallbackBuffer;
							}
							encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
						}
						encoderFallbackBuffer.InternalFallback(c, ref chars);
						c = '\0';
						continue;
					}
					if (bytes + 1 >= ptr)
					{
						if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
						{
							encoderFallbackBuffer.MovePrevious();
						}
						else
						{
							chars--;
						}
						base.ThrowBytesOverflow(encoder, bytes == ptr3);
					}
					else
					{
						if (this.bigEndian)
						{
							*(bytes++) = (byte)(c2 >> 8);
							*(bytes++) = (byte)c2;
							continue;
						}
						*(bytes++) = (byte)c2;
						*(bytes++) = (byte)(c2 >> 8);
						continue;
					}
				}
				IL_477:
				if (c <= '\0' || (encoder != null && !encoder.MustFlush))
				{
					goto IL_4F1;
				}
				if (flag)
				{
					break;
				}
				if (encoderFallbackBuffer == null)
				{
					if (encoder == null)
					{
						encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
					}
					else
					{
						encoderFallbackBuffer = encoder.FallbackBuffer;
					}
					encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
				}
				encoderFallbackBuffer.InternalFallback(c, ref chars);
				c = '\0';
				flag = true;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", new object[] { c }), "chars");
			IL_4F1:
			if (encoder != null)
			{
				encoder.charLeftOver = c;
				encoder.m_charsUsed = (int)((long)(chars - ptr4));
			}
			return (int)((long)(bytes - ptr3));
		}

		// Token: 0x06006907 RID: 26887 RVA: 0x0016672C File Offset: 0x0016492C
		[SecurityCritical]
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			UnicodeEncoding.Decoder decoder = (UnicodeEncoding.Decoder)baseDecoder;
			byte* ptr = bytes + count;
			byte* ptr2 = bytes;
			int num = -1;
			char c = '\0';
			int num2 = count >> 1;
			ulong* ptr3 = (ulong*)(ptr - 7);
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			if (decoder != null)
			{
				num = decoder.lastByte;
				c = decoder.lastChar;
				if (c > '\0')
				{
					num2++;
				}
				if (num >= 0 && (count & 1) == 1)
				{
					num2++;
				}
			}
			while (bytes < ptr)
			{
				if (!this.bigEndian && (bytes & 7L) == null && num == -1 && c == '\0')
				{
					ulong* ptr4;
					for (ptr4 = (ulong*)bytes; ptr4 < ptr3; ptr4++)
					{
						if ((9223512776490647552UL & *ptr4) != 0UL)
						{
							ulong num3 = (17870556004450629632UL & *ptr4) ^ 15564677810327967744UL;
							if (((num3 & 18446462598732840960UL) == 0UL || (num3 & 281470681743360UL) == 0UL || (num3 & (ulong)(-65536)) == 0UL || (num3 & 65535UL) == 0UL) && ((18158790778715962368UL & *ptr4) ^ 15852908186546788352UL) != 0UL)
							{
								break;
							}
						}
					}
					bytes = (byte*)ptr4;
					if (bytes >= ptr)
					{
						break;
					}
				}
				if (num < 0)
				{
					num = (int)(*(bytes++));
					if (bytes >= ptr)
					{
						break;
					}
				}
				char c2;
				if (this.bigEndian)
				{
					c2 = (char)((num << 8) | (int)(*(bytes++)));
				}
				else
				{
					c2 = (char)(((int)(*(bytes++)) << 8) | num);
				}
				num = -1;
				if (c2 >= '\ud800' && c2 <= '\udfff')
				{
					if (c2 <= '\udbff')
					{
						if (c > '\0')
						{
							num2--;
							byte[] array;
							if (this.bigEndian)
							{
								array = new byte[]
								{
									(byte)(c >> 8),
									(byte)c
								};
							}
							else
							{
								array = new byte[]
								{
									(byte)c,
									(byte)(c >> 8)
								};
							}
							if (decoderFallbackBuffer == null)
							{
								if (decoder == null)
								{
									decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
								}
								else
								{
									decoderFallbackBuffer = decoder.FallbackBuffer;
								}
								decoderFallbackBuffer.InternalInitialize(ptr2, null);
							}
							num2 += decoderFallbackBuffer.InternalFallback(array, bytes);
						}
						c = c2;
					}
					else if (c == '\0')
					{
						num2--;
						byte[] array2;
						if (this.bigEndian)
						{
							array2 = new byte[]
							{
								(byte)(c2 >> 8),
								(byte)c2
							};
						}
						else
						{
							array2 = new byte[]
							{
								(byte)c2,
								(byte)(c2 >> 8)
							};
						}
						if (decoderFallbackBuffer == null)
						{
							if (decoder == null)
							{
								decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
							}
							else
							{
								decoderFallbackBuffer = decoder.FallbackBuffer;
							}
							decoderFallbackBuffer.InternalInitialize(ptr2, null);
						}
						num2 += decoderFallbackBuffer.InternalFallback(array2, bytes);
					}
					else
					{
						c = '\0';
					}
				}
				else if (c > '\0')
				{
					num2--;
					byte[] array3;
					if (this.bigEndian)
					{
						array3 = new byte[]
						{
							(byte)(c >> 8),
							(byte)c
						};
					}
					else
					{
						array3 = new byte[]
						{
							(byte)c,
							(byte)(c >> 8)
						};
					}
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr2, null);
					}
					num2 += decoderFallbackBuffer.InternalFallback(array3, bytes);
					c = '\0';
				}
			}
			if (decoder == null || decoder.MustFlush)
			{
				if (c > '\0')
				{
					num2--;
					byte[] array4;
					if (this.bigEndian)
					{
						array4 = new byte[]
						{
							(byte)(c >> 8),
							(byte)c
						};
					}
					else
					{
						array4 = new byte[]
						{
							(byte)c,
							(byte)(c >> 8)
						};
					}
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr2, null);
					}
					num2 += decoderFallbackBuffer.InternalFallback(array4, bytes);
					c = '\0';
				}
				if (num >= 0)
				{
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr2, null);
					}
					num2 += decoderFallbackBuffer.InternalFallback(new byte[] { (byte)num }, bytes);
				}
			}
			if (c > '\0')
			{
				num2--;
			}
			return num2;
		}

		// Token: 0x06006908 RID: 26888 RVA: 0x00166B10 File Offset: 0x00164D10
		[SecurityCritical]
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			UnicodeEncoding.Decoder decoder = (UnicodeEncoding.Decoder)baseDecoder;
			int num = -1;
			char c = '\0';
			if (decoder != null)
			{
				num = decoder.lastByte;
				c = decoder.lastChar;
			}
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			byte* ptr = bytes + byteCount;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			char* ptr4 = chars;
			while (bytes < ptr)
			{
				if (!this.bigEndian && (chars & 7L) == null && (bytes & 7L) == null && num == -1 && c == '\0')
				{
					ulong* ptr5 = (ulong*)(bytes - 7 + (IntPtr)(((long)(ptr - bytes) >> 1 < (long)(ptr2 - chars)) ? ((long)(ptr - bytes)) : ((long)(ptr2 - chars) << 1)) / 8);
					ulong* ptr6 = (ulong*)bytes;
					ulong* ptr7 = (ulong*)chars;
					while (ptr6 < ptr5)
					{
						if ((9223512776490647552UL & *ptr6) != 0UL)
						{
							ulong num2 = (17870556004450629632UL & *ptr6) ^ 15564677810327967744UL;
							if (((num2 & 18446462598732840960UL) == 0UL || (num2 & 281470681743360UL) == 0UL || (num2 & (ulong)(-65536)) == 0UL || (num2 & 65535UL) == 0UL) && ((18158790778715962368UL & *ptr6) ^ 15852908186546788352UL) != 0UL)
							{
								break;
							}
						}
						*ptr7 = *ptr6;
						ptr6++;
						ptr7++;
					}
					chars = (char*)ptr7;
					bytes = (byte*)ptr6;
					if (bytes >= ptr)
					{
						break;
					}
				}
				if (num < 0)
				{
					num = (int)(*(bytes++));
				}
				else
				{
					char c2;
					if (this.bigEndian)
					{
						c2 = (char)((num << 8) | (int)(*(bytes++)));
					}
					else
					{
						c2 = (char)(((int)(*(bytes++)) << 8) | num);
					}
					num = -1;
					if (c2 >= '\ud800' && c2 <= '\udfff')
					{
						if (c2 <= '\udbff')
						{
							if (c > '\0')
							{
								byte[] array;
								if (this.bigEndian)
								{
									array = new byte[]
									{
										(byte)(c >> 8),
										(byte)c
									};
								}
								else
								{
									array = new byte[]
									{
										(byte)c,
										(byte)(c >> 8)
									};
								}
								if (decoderFallbackBuffer == null)
								{
									if (decoder == null)
									{
										decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
									}
									else
									{
										decoderFallbackBuffer = decoder.FallbackBuffer;
									}
									decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
								}
								if (!decoderFallbackBuffer.InternalFallback(array, bytes, ref chars))
								{
									bytes -= 2;
									decoderFallbackBuffer.InternalReset();
									base.ThrowCharsOverflow(decoder, chars == ptr4);
									break;
								}
							}
							c = c2;
							continue;
						}
						if (c == '\0')
						{
							byte[] array2;
							if (this.bigEndian)
							{
								array2 = new byte[]
								{
									(byte)(c2 >> 8),
									(byte)c2
								};
							}
							else
							{
								array2 = new byte[]
								{
									(byte)c2,
									(byte)(c2 >> 8)
								};
							}
							if (decoderFallbackBuffer == null)
							{
								if (decoder == null)
								{
									decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
								}
								else
								{
									decoderFallbackBuffer = decoder.FallbackBuffer;
								}
								decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
							}
							if (!decoderFallbackBuffer.InternalFallback(array2, bytes, ref chars))
							{
								bytes -= 2;
								decoderFallbackBuffer.InternalReset();
								base.ThrowCharsOverflow(decoder, chars == ptr4);
								break;
							}
							continue;
						}
						else
						{
							if (chars >= ptr2 - 1)
							{
								bytes -= 2;
								base.ThrowCharsOverflow(decoder, chars == ptr4);
								break;
							}
							*(chars++) = c;
							c = '\0';
						}
					}
					else if (c > '\0')
					{
						byte[] array3;
						if (this.bigEndian)
						{
							array3 = new byte[]
							{
								(byte)(c >> 8),
								(byte)c
							};
						}
						else
						{
							array3 = new byte[]
							{
								(byte)c,
								(byte)(c >> 8)
							};
						}
						if (decoderFallbackBuffer == null)
						{
							if (decoder == null)
							{
								decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
							}
							else
							{
								decoderFallbackBuffer = decoder.FallbackBuffer;
							}
							decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
						}
						if (!decoderFallbackBuffer.InternalFallback(array3, bytes, ref chars))
						{
							bytes -= 2;
							decoderFallbackBuffer.InternalReset();
							base.ThrowCharsOverflow(decoder, chars == ptr4);
							break;
						}
						c = '\0';
					}
					if (chars >= ptr2)
					{
						bytes -= 2;
						base.ThrowCharsOverflow(decoder, chars == ptr4);
						break;
					}
					*(chars++) = c2;
				}
			}
			if (decoder == null || decoder.MustFlush)
			{
				if (c > '\0')
				{
					byte[] array4;
					if (this.bigEndian)
					{
						array4 = new byte[]
						{
							(byte)(c >> 8),
							(byte)c
						};
					}
					else
					{
						array4 = new byte[]
						{
							(byte)c,
							(byte)(c >> 8)
						};
					}
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
					}
					if (!decoderFallbackBuffer.InternalFallback(array4, bytes, ref chars))
					{
						bytes -= 2;
						if (num >= 0)
						{
							bytes--;
						}
						decoderFallbackBuffer.InternalReset();
						base.ThrowCharsOverflow(decoder, chars == ptr4);
						bytes += 2;
						if (num >= 0)
						{
							bytes++;
							goto IL_4A2;
						}
						goto IL_4A2;
					}
					else
					{
						c = '\0';
					}
				}
				if (num >= 0)
				{
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
					}
					if (!decoderFallbackBuffer.InternalFallback(new byte[] { (byte)num }, bytes, ref chars))
					{
						bytes--;
						decoderFallbackBuffer.InternalReset();
						base.ThrowCharsOverflow(decoder, chars == ptr4);
						bytes++;
					}
					else
					{
						num = -1;
					}
				}
			}
			IL_4A2:
			if (decoder != null)
			{
				decoder.m_bytesUsed = (int)((long)(bytes - ptr3));
				decoder.lastChar = c;
				decoder.lastByte = num;
			}
			return (int)((long)(chars - ptr4));
		}

		/// <summary>Obtains an encoder that converts a sequence of Unicode characters into a UTF-16 encoded sequence of bytes.</summary>
		/// <returns>A <see cref="T:System.Text.Encoder" /> object that converts a sequence of Unicode characters into a UTF-16 encoded sequence of bytes.</returns>
		// Token: 0x06006909 RID: 26889 RVA: 0x00166FE6 File Offset: 0x001651E6
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}

		/// <summary>Obtains a decoder that converts a UTF-16 encoded sequence of bytes into a sequence of Unicode characters.</summary>
		/// <returns>A <see cref="T:System.Text.Decoder" /> that converts a UTF-16 encoded sequence of bytes into a sequence of Unicode characters.</returns>
		// Token: 0x0600690A RID: 26890 RVA: 0x00166FEE File Offset: 0x001651EE
		[__DynamicallyInvokable]
		public override System.Text.Decoder GetDecoder()
		{
			return new UnicodeEncoding.Decoder(this);
		}

		/// <summary>Returns a Unicode byte order mark encoded in UTF-16 format, if the constructor for this instance requests a byte order mark.</summary>
		/// <returns>A byte array containing the Unicode byte order mark, if the <see cref="T:System.Text.UnicodeEncoding" /> object is configured to supply one. Otherwise, this method returns a zero-length byte array.</returns>
		// Token: 0x0600690B RID: 26891 RVA: 0x00166FF8 File Offset: 0x001651F8
		[__DynamicallyInvokable]
		public override byte[] GetPreamble()
		{
			if (!this.byteOrderMark)
			{
				return EmptyArray<byte>.Value;
			}
			if (this.bigEndian)
			{
				return new byte[] { 254, byte.MaxValue };
			}
			return new byte[] { byte.MaxValue, 254 };
		}

		/// <summary>Calculates the maximum number of bytes produced by encoding the specified number of characters.</summary>
		/// <param name="charCount">The number of characters to encode.</param>
		/// <returns>The maximum number of bytes produced by encoding the specified number of characters.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero.  
		/// -or-  
		/// The resulting number of bytes is greater than the maximum number that can be returned as an integer.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x0600690C RID: 26892 RVA: 0x00167048 File Offset: 0x00165248
		[__DynamicallyInvokable]
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			long num = (long)charCount + 1L;
			if (base.EncoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.EncoderFallback.MaxCharCount;
			}
			num <<= 1;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			return (int)num;
		}

		/// <summary>Calculates the maximum number of characters produced by decoding the specified number of bytes.</summary>
		/// <param name="byteCount">The number of bytes to decode.</param>
		/// <returns>The maximum number of characters produced by decoding the specified number of bytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is less than zero.  
		/// -or-  
		/// The resulting number of bytes is greater than the maximum number that can be returned as an integer.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x0600690D RID: 26893 RVA: 0x001670B8 File Offset: 0x001652B8
		[__DynamicallyInvokable]
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			long num = (long)(byteCount >> 1) + (long)(byteCount & 1) + 1L;
			if (base.DecoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.DecoderFallback.MaxCharCount;
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
			}
			return (int)num;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Text.UnicodeEncoding" /> object.</summary>
		/// <param name="value">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is an instance of <see cref="T:System.Text.UnicodeEncoding" /> and is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600690E RID: 26894 RVA: 0x00167128 File Offset: 0x00165328
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			UnicodeEncoding unicodeEncoding = value as UnicodeEncoding;
			return unicodeEncoding != null && (this.CodePage == unicodeEncoding.CodePage && this.byteOrderMark == unicodeEncoding.byteOrderMark && this.bigEndian == unicodeEncoding.bigEndian && base.EncoderFallback.Equals(unicodeEncoding.EncoderFallback)) && base.DecoderFallback.Equals(unicodeEncoding.DecoderFallback);
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>The hash code for the current <see cref="T:System.Text.UnicodeEncoding" /> object.</returns>
		// Token: 0x0600690F RID: 26895 RVA: 0x00167191 File Offset: 0x00165391
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.CodePage + base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode() + (this.byteOrderMark ? 4 : 0) + (this.bigEndian ? 8 : 0);
		}

		// Token: 0x04002F16 RID: 12054
		[OptionalField(VersionAdded = 2)]
		internal bool isThrowException;

		// Token: 0x04002F17 RID: 12055
		internal bool bigEndian;

		// Token: 0x04002F18 RID: 12056
		internal bool byteOrderMark = true;

		/// <summary>Represents the Unicode character size in bytes. This field is a constant.</summary>
		// Token: 0x04002F19 RID: 12057
		public const int CharSize = 2;

		// Token: 0x02000CB3 RID: 3251
		[Serializable]
		private class Decoder : DecoderNLS, ISerializable
		{
			// Token: 0x060071AC RID: 29100 RVA: 0x00188220 File Offset: 0x00186420
			public Decoder(UnicodeEncoding encoding)
				: base(encoding)
			{
			}

			// Token: 0x060071AD RID: 29101 RVA: 0x00188230 File Offset: 0x00186430
			internal Decoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.lastByte = (int)info.GetValue("lastByte", typeof(int));
				try
				{
					this.m_encoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
					this.lastChar = (char)info.GetValue("lastChar", typeof(char));
					this.m_fallback = (DecoderFallback)info.GetValue("m_fallback", typeof(DecoderFallback));
				}
				catch (SerializationException)
				{
					bool flag = (bool)info.GetValue("bigEndian", typeof(bool));
					this.m_encoding = new UnicodeEncoding(flag, false);
				}
			}

			// Token: 0x060071AE RID: 29102 RVA: 0x00188318 File Offset: 0x00186518
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("m_encoding", this.m_encoding);
				info.AddValue("m_fallback", this.m_fallback);
				info.AddValue("lastChar", this.lastChar);
				info.AddValue("lastByte", this.lastByte);
				info.AddValue("bigEndian", ((UnicodeEncoding)this.m_encoding).bigEndian);
			}

			// Token: 0x060071AF RID: 29103 RVA: 0x00188392 File Offset: 0x00186592
			public override void Reset()
			{
				this.lastByte = -1;
				this.lastChar = '\0';
				if (this.m_fallbackBuffer != null)
				{
					this.m_fallbackBuffer.Reset();
				}
			}

			// Token: 0x17001378 RID: 4984
			// (get) Token: 0x060071B0 RID: 29104 RVA: 0x001883B5 File Offset: 0x001865B5
			internal override bool HasState
			{
				get
				{
					return this.lastByte != -1 || this.lastChar > '\0';
				}
			}

			// Token: 0x040038C5 RID: 14533
			internal int lastByte = -1;

			// Token: 0x040038C6 RID: 14534
			internal char lastChar;
		}
	}
}
