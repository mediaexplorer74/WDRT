using System;

namespace System.Text
{
	// Token: 0x02000A5D RID: 2653
	[Serializable]
	internal sealed class InternalDecoderBestFitFallback : DecoderFallback
	{
		// Token: 0x06006797 RID: 26519 RVA: 0x0015F44D File Offset: 0x0015D64D
		internal InternalDecoderBestFitFallback(Encoding encoding)
		{
			this.encoding = encoding;
			this.bIsMicrosoftBestFitFallback = true;
		}

		// Token: 0x06006798 RID: 26520 RVA: 0x0015F46B File Offset: 0x0015D66B
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalDecoderBestFitFallbackBuffer(this);
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06006799 RID: 26521 RVA: 0x0015F473 File Offset: 0x0015D673
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600679A RID: 26522 RVA: 0x0015F478 File Offset: 0x0015D678
		public override bool Equals(object value)
		{
			InternalDecoderBestFitFallback internalDecoderBestFitFallback = value as InternalDecoderBestFitFallback;
			return internalDecoderBestFitFallback != null && this.encoding.CodePage == internalDecoderBestFitFallback.encoding.CodePage;
		}

		// Token: 0x0600679B RID: 26523 RVA: 0x0015F4A9 File Offset: 0x0015D6A9
		public override int GetHashCode()
		{
			return this.encoding.CodePage;
		}

		// Token: 0x04002E48 RID: 11848
		internal Encoding encoding;

		// Token: 0x04002E49 RID: 11849
		internal char[] arrayBestFit;

		// Token: 0x04002E4A RID: 11850
		internal char cReplacement = '?';
	}
}
