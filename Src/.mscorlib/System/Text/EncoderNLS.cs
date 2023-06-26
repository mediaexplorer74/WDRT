using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A67 RID: 2663
	[Serializable]
	internal class EncoderNLS : Encoder, ISerializable
	{
		// Token: 0x060067E4 RID: 26596 RVA: 0x00160197 File Offset: 0x0015E397
		internal EncoderNLS(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("NotSupported_TypeCannotDeserialized"), base.GetType()));
		}

		// Token: 0x060067E5 RID: 26597 RVA: 0x001601BE File Offset: 0x0015E3BE
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.SerializeEncoder(info);
			info.AddValue("encoding", this.m_encoding);
			info.AddValue("charLeftOver", this.charLeftOver);
			info.SetType(typeof(Encoding.DefaultEncoder));
		}

		// Token: 0x060067E6 RID: 26598 RVA: 0x001601F9 File Offset: 0x0015E3F9
		internal EncoderNLS(Encoding encoding)
		{
			this.m_encoding = encoding;
			this.m_fallback = this.m_encoding.EncoderFallback;
			this.Reset();
		}

		// Token: 0x060067E7 RID: 26599 RVA: 0x0016021F File Offset: 0x0015E41F
		internal EncoderNLS()
		{
			this.m_encoding = null;
			this.Reset();
		}

		// Token: 0x060067E8 RID: 26600 RVA: 0x00160234 File Offset: 0x0015E434
		public override void Reset()
		{
			this.charLeftOver = '\0';
			if (this.m_fallbackBuffer != null)
			{
				this.m_fallbackBuffer.Reset();
			}
		}

		// Token: 0x060067E9 RID: 26601 RVA: 0x00160250 File Offset: 0x0015E450
		[SecuritySafeCritical]
		public unsafe override int GetByteCount(char[] chars, int index, int count, bool flush)
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
				chars = new char[1];
			}
			char[] array;
			char* ptr;
			if ((array = chars) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			int byteCount = this.GetByteCount(ptr + index, count, flush);
			array = null;
			return byteCount;
		}

		// Token: 0x060067EA RID: 26602 RVA: 0x001602F4 File Offset: 0x0015E4F4
		[SecurityCritical]
		public unsafe override int GetByteCount(char* chars, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = true;
			return this.m_encoding.GetByteCount(chars, count, this);
		}

		// Token: 0x060067EB RID: 26603 RVA: 0x00160350 File Offset: 0x0015E550
		[SecuritySafeCritical]
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
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
				chars = new char[1];
			}
			int num = bytes.Length - byteIndex;
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			char[] array;
			char* ptr;
			if ((array = chars) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte[] array2;
			byte* ptr2;
			if ((array2 = bytes) == null || array2.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array2[0];
			}
			return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, num, flush);
		}

		// Token: 0x060067EC RID: 26604 RVA: 0x00160454 File Offset: 0x0015E654
		[SecurityCritical]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
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
			return this.m_encoding.GetBytes(chars, charCount, bytes, byteCount, this);
		}

		// Token: 0x060067ED RID: 26605 RVA: 0x001604D8 File Offset: 0x0015E6D8
		[SecuritySafeCritical]
		public unsafe override void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
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
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			char[] array;
			char* ptr;
			if ((array = chars) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte[] array2;
			byte* ptr2;
			if ((array2 = bytes) == null || array2.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array2[0];
			}
			this.Convert(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, flush, out charsUsed, out bytesUsed, out completed);
			array2 = null;
			array = null;
		}

		// Token: 0x060067EE RID: 26606 RVA: 0x00160604 File Offset: 0x0015E804
		[SecurityCritical]
		public unsafe override void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = false;
			this.m_charsUsed = 0;
			bytesUsed = this.m_encoding.GetBytes(chars, charCount, bytes, byteCount, this);
			charsUsed = this.m_charsUsed;
			completed = charsUsed == charCount && (!flush || !this.HasState) && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x060067EF RID: 26607 RVA: 0x001606C9 File Offset: 0x0015E8C9
		public Encoding Encoding
		{
			get
			{
				return this.m_encoding;
			}
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x060067F0 RID: 26608 RVA: 0x001606D1 File Offset: 0x0015E8D1
		public bool MustFlush
		{
			get
			{
				return this.m_mustFlush;
			}
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x060067F1 RID: 26609 RVA: 0x001606D9 File Offset: 0x0015E8D9
		internal virtual bool HasState
		{
			get
			{
				return this.charLeftOver > '\0';
			}
		}

		// Token: 0x060067F2 RID: 26610 RVA: 0x001606E4 File Offset: 0x0015E8E4
		internal void ClearMustFlush()
		{
			this.m_mustFlush = false;
		}

		// Token: 0x04002E5E RID: 11870
		internal char charLeftOver;

		// Token: 0x04002E5F RID: 11871
		protected Encoding m_encoding;

		// Token: 0x04002E60 RID: 11872
		[NonSerialized]
		protected bool m_mustFlush;

		// Token: 0x04002E61 RID: 11873
		[NonSerialized]
		internal bool m_throwOnOverflow;

		// Token: 0x04002E62 RID: 11874
		[NonSerialized]
		internal int m_charsUsed;
	}
}
