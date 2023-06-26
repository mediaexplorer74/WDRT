using System;

namespace System.Text
{
	// Token: 0x02000A68 RID: 2664
	[Serializable]
	internal class InternalEncoderBestFitFallback : EncoderFallback
	{
		// Token: 0x060067F3 RID: 26611 RVA: 0x001606ED File Offset: 0x0015E8ED
		internal InternalEncoderBestFitFallback(Encoding encoding)
		{
			this.encoding = encoding;
			this.bIsMicrosoftBestFitFallback = true;
		}

		// Token: 0x060067F4 RID: 26612 RVA: 0x00160703 File Offset: 0x0015E903
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalEncoderBestFitFallbackBuffer(this);
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x060067F5 RID: 26613 RVA: 0x0016070B File Offset: 0x0015E90B
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060067F6 RID: 26614 RVA: 0x00160710 File Offset: 0x0015E910
		public override bool Equals(object value)
		{
			InternalEncoderBestFitFallback internalEncoderBestFitFallback = value as InternalEncoderBestFitFallback;
			return internalEncoderBestFitFallback != null && this.encoding.CodePage == internalEncoderBestFitFallback.encoding.CodePage;
		}

		// Token: 0x060067F7 RID: 26615 RVA: 0x00160741 File Offset: 0x0015E941
		public override int GetHashCode()
		{
			return this.encoding.CodePage;
		}

		// Token: 0x04002E63 RID: 11875
		internal Encoding encoding;

		// Token: 0x04002E64 RID: 11876
		internal char[] arrayBestFit;
	}
}
