using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A5C RID: 2652
	[Serializable]
	internal class DecoderNLS : Decoder, ISerializable
	{
		// Token: 0x06006787 RID: 26503 RVA: 0x0015EF0B File Offset: 0x0015D10B
		internal DecoderNLS(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("NotSupported_TypeCannotDeserialized"), base.GetType()));
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x0015EF32 File Offset: 0x0015D132
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.SerializeDecoder(info);
			info.AddValue("encoding", this.m_encoding);
			info.SetType(typeof(Encoding.DefaultDecoder));
		}

		// Token: 0x06006789 RID: 26505 RVA: 0x0015EF5C File Offset: 0x0015D15C
		internal DecoderNLS(Encoding encoding)
		{
			this.m_encoding = encoding;
			this.m_fallback = this.m_encoding.DecoderFallback;
			this.Reset();
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x0015EF82 File Offset: 0x0015D182
		internal DecoderNLS()
		{
			this.m_encoding = null;
			this.Reset();
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x0015EF97 File Offset: 0x0015D197
		public override void Reset()
		{
			if (this.m_fallbackBuffer != null)
			{
				this.m_fallbackBuffer.Reset();
			}
		}

		// Token: 0x0600678C RID: 26508 RVA: 0x0015EFAC File Offset: 0x0015D1AC
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(bytes, index, count, false);
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x0015EFB8 File Offset: 0x0015D1B8
		[SecuritySafeCritical]
		public unsafe override int GetCharCount(byte[] bytes, int index, int count, bool flush)
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
				bytes = new byte[1];
			}
			byte[] array;
			byte* ptr;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return this.GetCharCount(ptr + index, count, flush);
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x0015F054 File Offset: 0x0015D254
		[SecurityCritical]
		public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = true;
			return this.m_encoding.GetCharCount(bytes, count, this);
		}

		// Token: 0x0600678F RID: 26511 RVA: 0x0015F0B0 File Offset: 0x0015D2B0
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x0015F0C0 File Offset: 0x0015D2C0
		[SecuritySafeCritical]
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
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
				bytes = new byte[1];
			}
			int num = chars.Length - charIndex;
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			byte[] array;
			byte* ptr;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char[] array2;
			char* ptr2;
			if ((array2 = chars) == null || array2.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array2[0];
			}
			return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, num, flush);
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x0015F1C4 File Offset: 0x0015D3C4
		[SecurityCritical]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = true;
			return this.m_encoding.GetChars(bytes, byteCount, chars, charCount, this);
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x0015F248 File Offset: 0x0015D448
		[SecuritySafeCritical]
		public unsafe override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
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
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			byte[] array;
			byte* ptr;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char[] array2;
			char* ptr2;
			if ((array2 = chars) == null || array2.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array2[0];
			}
			this.Convert(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, flush, out bytesUsed, out charsUsed, out completed);
			array2 = null;
			array = null;
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x0015F374 File Offset: 0x0015D574
		[SecurityCritical]
		public unsafe override void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = false;
			this.m_bytesUsed = 0;
			charsUsed = this.m_encoding.GetChars(bytes, byteCount, chars, charCount, this);
			bytesUsed = this.m_bytesUsed;
			completed = bytesUsed == byteCount && (!flush || !this.HasState) && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06006794 RID: 26516 RVA: 0x0015F439 File Offset: 0x0015D639
		public bool MustFlush
		{
			get
			{
				return this.m_mustFlush;
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06006795 RID: 26517 RVA: 0x0015F441 File Offset: 0x0015D641
		internal virtual bool HasState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006796 RID: 26518 RVA: 0x0015F444 File Offset: 0x0015D644
		internal void ClearMustFlush()
		{
			this.m_mustFlush = false;
		}

		// Token: 0x04002E44 RID: 11844
		protected Encoding m_encoding;

		// Token: 0x04002E45 RID: 11845
		[NonSerialized]
		protected bool m_mustFlush;

		// Token: 0x04002E46 RID: 11846
		[NonSerialized]
		internal bool m_throwOnOverflow;

		// Token: 0x04002E47 RID: 11847
		[NonSerialized]
		internal int m_bytesUsed;
	}
}
