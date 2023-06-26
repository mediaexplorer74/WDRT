using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000244 RID: 580
	internal class EncodedStreamFactory
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x000719E2 File Offset: 0x0006FBE2
		internal static int DefaultMaxLineLength
		{
			get
			{
				return 70;
			}
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000719E6 File Offset: 0x0006FBE6
		internal IEncodableStream GetEncoder(TransferEncoding encoding, Stream stream)
		{
			if (encoding == TransferEncoding.Base64)
			{
				return new Base64Stream(stream, new Base64WriteStateInfo());
			}
			if (encoding == TransferEncoding.QuotedPrintable)
			{
				return new QuotedPrintableStream(stream, true);
			}
			if (encoding == TransferEncoding.SevenBit || encoding == TransferEncoding.EightBit)
			{
				return new EightBitStream(stream);
			}
			throw new NotSupportedException("Encoding Stream");
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00071A1C File Offset: 0x0006FC1C
		internal IEncodableStream GetEncoderForHeader(Encoding encoding, bool useBase64Encoding, int headerTextLength)
		{
			byte[] array = this.CreateHeader(encoding, useBase64Encoding);
			byte[] array2 = this.CreateFooter();
			WriteStateInfoBase writeStateInfoBase;
			if (useBase64Encoding)
			{
				writeStateInfoBase = new Base64WriteStateInfo(1024, array, array2, EncodedStreamFactory.DefaultMaxLineLength, headerTextLength);
				return new Base64Stream((Base64WriteStateInfo)writeStateInfoBase);
			}
			writeStateInfoBase = new WriteStateInfoBase(1024, array, array2, EncodedStreamFactory.DefaultMaxLineLength, headerTextLength);
			return new QEncodedStream(writeStateInfoBase);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00071A74 File Offset: 0x0006FC74
		protected byte[] CreateHeader(Encoding encoding, bool useBase64Encoding)
		{
			string text = string.Format("=?{0}?{1}?", encoding.HeaderName, useBase64Encoding ? "B" : "Q");
			return Encoding.ASCII.GetBytes(text);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00071AAC File Offset: 0x0006FCAC
		protected byte[] CreateFooter()
		{
			return new byte[] { 63, 61 };
		}

		// Token: 0x040016F6 RID: 5878
		private const int defaultMaxLineLength = 70;

		// Token: 0x040016F7 RID: 5879
		private const int initialBufferSize = 1024;
	}
}
