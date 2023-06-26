using System;
using System.Security;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000A69 RID: 2665
	internal sealed class InternalEncoderBestFitFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x060067F8 RID: 26616 RVA: 0x00160750 File Offset: 0x0015E950
		private static object InternalSyncObject
		{
			get
			{
				if (InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject, obj, null);
				}
				return InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x060067F9 RID: 26617 RVA: 0x0016077C File Offset: 0x0015E97C
		public InternalEncoderBestFitFallbackBuffer(InternalEncoderBestFitFallback fallback)
		{
			this.oFallback = fallback;
			if (this.oFallback.arrayBestFit == null)
			{
				object internalSyncObject = InternalEncoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this.oFallback.arrayBestFit == null)
					{
						this.oFallback.arrayBestFit = fallback.encoding.GetBestFitUnicodeToBytesData();
					}
				}
			}
		}

		// Token: 0x060067FA RID: 26618 RVA: 0x001607FC File Offset: 0x0015E9FC
		public override bool Fallback(char charUnknown, int index)
		{
			this.iCount = (this.iSize = 1);
			this.cBestFit = this.TryBestFit(charUnknown);
			if (this.cBestFit == '\0')
			{
				this.cBestFit = '?';
			}
			return true;
		}

		// Token: 0x060067FB RID: 26619 RVA: 0x00160838 File Offset: 0x0015EA38
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 55296, 56319 }));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 56320, 57343 }));
			}
			this.cBestFit = '?';
			this.iCount = (this.iSize = 2);
			return true;
		}

		// Token: 0x060067FC RID: 26620 RVA: 0x001608D8 File Offset: 0x0015EAD8
		public override char GetNextChar()
		{
			this.iCount--;
			if (this.iCount < 0)
			{
				return '\0';
			}
			if (this.iCount == 2147483647)
			{
				this.iCount = -1;
				return '\0';
			}
			return this.cBestFit;
		}

		// Token: 0x060067FD RID: 26621 RVA: 0x0016090F File Offset: 0x0015EB0F
		public override bool MovePrevious()
		{
			if (this.iCount >= 0)
			{
				this.iCount++;
			}
			return this.iCount >= 0 && this.iCount <= this.iSize;
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x060067FE RID: 26622 RVA: 0x00160944 File Offset: 0x0015EB44
		public override int Remaining
		{
			get
			{
				if (this.iCount <= 0)
				{
					return 0;
				}
				return this.iCount;
			}
		}

		// Token: 0x060067FF RID: 26623 RVA: 0x00160957 File Offset: 0x0015EB57
		[SecuritySafeCritical]
		public override void Reset()
		{
			this.iCount = -1;
			this.charStart = null;
			this.bFallingBack = false;
		}

		// Token: 0x06006800 RID: 26624 RVA: 0x00160970 File Offset: 0x0015EB70
		private char TryBestFit(char cUnknown)
		{
			int num = 0;
			int num2 = this.oFallback.arrayBestFit.Length;
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = (num3 / 2 + num) & 65534;
				char c = this.oFallback.arrayBestFit[i];
				if (c == cUnknown)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
				if (c < cUnknown)
				{
					num = i;
				}
				else
				{
					num2 = i;
				}
			}
			for (int i = num; i < num2; i += 2)
			{
				if (this.oFallback.arrayBestFit[i] == cUnknown)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04002E65 RID: 11877
		private char cBestFit;

		// Token: 0x04002E66 RID: 11878
		private InternalEncoderBestFitFallback oFallback;

		// Token: 0x04002E67 RID: 11879
		private int iCount = -1;

		// Token: 0x04002E68 RID: 11880
		private int iSize;

		// Token: 0x04002E69 RID: 11881
		private static object s_InternalSyncObject;
	}
}
