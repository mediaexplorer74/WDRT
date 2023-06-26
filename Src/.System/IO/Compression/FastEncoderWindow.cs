using System;
using System.Diagnostics;

namespace System.IO.Compression
{
	// Token: 0x0200042A RID: 1066
	internal class FastEncoderWindow
	{
		// Token: 0x060027F0 RID: 10224 RVA: 0x000B77AA File Offset: 0x000B59AA
		public FastEncoderWindow()
		{
			this.ResetWindow();
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x000B77B8 File Offset: 0x000B59B8
		public int BytesAvailable
		{
			get
			{
				return this.bufEnd - this.bufPos;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x000B77C8 File Offset: 0x000B59C8
		public DeflateInput UnprocessedInput
		{
			get
			{
				return new DeflateInput
				{
					Buffer = this.window,
					StartIndex = this.bufPos,
					Count = this.bufEnd - this.bufPos
				};
			}
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000B7807 File Offset: 0x000B5A07
		public void FlushWindow()
		{
			this.ResetWindow();
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000B7810 File Offset: 0x000B5A10
		private void ResetWindow()
		{
			this.window = new byte[16646];
			this.prev = new ushort[8450];
			this.lookup = new ushort[2048];
			this.bufPos = 8192;
			this.bufEnd = this.bufPos;
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x000B7864 File Offset: 0x000B5A64
		public int FreeWindowSpace
		{
			get
			{
				return 16384 - this.bufEnd;
			}
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000B7872 File Offset: 0x000B5A72
		public void CopyBytes(byte[] inputBuffer, int startIndex, int count)
		{
			Array.Copy(inputBuffer, startIndex, this.window, this.bufEnd, count);
			this.bufEnd += count;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000B7898 File Offset: 0x000B5A98
		public void MoveWindows()
		{
			Array.Copy(this.window, this.bufPos - 8192, this.window, 0, 8192);
			for (int i = 0; i < 2048; i++)
			{
				int num = (int)(this.lookup[i] - 8192);
				if (num <= 0)
				{
					this.lookup[i] = 0;
				}
				else
				{
					this.lookup[i] = (ushort)num;
				}
			}
			for (int i = 0; i < 8192; i++)
			{
				long num2 = (long)((ulong)this.prev[i] - 8192UL);
				if (num2 <= 0L)
				{
					this.prev[i] = 0;
				}
				else
				{
					this.prev[i] = (ushort)num2;
				}
			}
			this.bufPos = 8192;
			this.bufEnd = this.bufPos;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000B7952 File Offset: 0x000B5B52
		private uint HashValue(uint hash, byte b)
		{
			return (hash << 4) ^ (uint)b;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000B795C File Offset: 0x000B5B5C
		private uint InsertString(ref uint hash)
		{
			hash = this.HashValue(hash, this.window[this.bufPos + 2]);
			uint num = (uint)this.lookup[(int)(hash & 2047U)];
			this.lookup[(int)(hash & 2047U)] = (ushort)this.bufPos;
			this.prev[this.bufPos & 8191] = (ushort)num;
			return num;
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000B79C0 File Offset: 0x000B5BC0
		private void InsertStrings(ref uint hash, int matchLen)
		{
			if (this.bufEnd - this.bufPos <= matchLen)
			{
				this.bufPos += matchLen - 1;
				return;
			}
			while (--matchLen > 0)
			{
				this.InsertString(ref hash);
				this.bufPos++;
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000B7A10 File Offset: 0x000B5C10
		internal bool GetNextSymbolOrMatch(Match match)
		{
			uint num = this.HashValue(0U, this.window[this.bufPos]);
			num = this.HashValue(num, this.window[this.bufPos + 1]);
			int num2 = 0;
			int num3;
			if (this.bufEnd - this.bufPos <= 3)
			{
				num3 = 0;
			}
			else
			{
				int num4 = (int)this.InsertString(ref num);
				if (num4 != 0)
				{
					num3 = this.FindMatch(num4, out num2, 32, 32);
					if (this.bufPos + num3 > this.bufEnd)
					{
						num3 = this.bufEnd - this.bufPos;
					}
				}
				else
				{
					num3 = 0;
				}
			}
			if (num3 < 3)
			{
				match.State = MatchState.HasSymbol;
				match.Symbol = this.window[this.bufPos];
				this.bufPos++;
			}
			else
			{
				this.bufPos++;
				if (num3 <= 6)
				{
					int num5 = 0;
					int num6 = (int)this.InsertString(ref num);
					int num7;
					if (num6 != 0)
					{
						num7 = this.FindMatch(num6, out num5, (num3 < 4) ? 32 : 8, 32);
						if (this.bufPos + num7 > this.bufEnd)
						{
							num7 = this.bufEnd - this.bufPos;
						}
					}
					else
					{
						num7 = 0;
					}
					if (num7 > num3)
					{
						match.State = MatchState.HasSymbolAndMatch;
						match.Symbol = this.window[this.bufPos - 1];
						match.Position = num5;
						match.Length = num7;
						this.bufPos++;
						num3 = num7;
						this.InsertStrings(ref num, num3);
					}
					else
					{
						match.State = MatchState.HasMatch;
						match.Position = num2;
						match.Length = num3;
						num3--;
						this.bufPos++;
						this.InsertStrings(ref num, num3);
					}
				}
				else
				{
					match.State = MatchState.HasMatch;
					match.Position = num2;
					match.Length = num3;
					this.InsertStrings(ref num, num3);
				}
			}
			if (this.bufPos == 16384)
			{
				this.MoveWindows();
			}
			return true;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000B7BE0 File Offset: 0x000B5DE0
		private int FindMatch(int search, out int matchPos, int searchDepth, int niceLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = this.bufPos - 8192;
			byte b = this.window[this.bufPos];
			while (search > num3)
			{
				if (this.window[search + num] == b)
				{
					int num4 = 0;
					while (num4 < 258 && this.window[this.bufPos + num4] == this.window[search + num4])
					{
						num4++;
					}
					if (num4 > num)
					{
						num = num4;
						num2 = search;
						if (num4 > 32)
						{
							break;
						}
						b = this.window[this.bufPos + num4];
					}
				}
				if (--searchDepth == 0)
				{
					break;
				}
				search = (int)this.prev[search & 8191];
			}
			matchPos = this.bufPos - num2 - 1;
			if (num == 3 && matchPos >= 16384)
			{
				return 0;
			}
			return num;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000B7CA8 File Offset: 0x000B5EA8
		[Conditional("DEBUG")]
		private void VerifyHashes()
		{
			for (int i = 0; i < 2048; i++)
			{
				ushort num = this.lookup[i];
				while (num != 0 && this.bufPos - (int)num < 8192)
				{
					ushort num2 = this.prev[(int)(num & 8191)];
					if (this.bufPos - (int)num2 >= 8192)
					{
						break;
					}
					num = num2;
				}
			}
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000B7D02 File Offset: 0x000B5F02
		private uint RecalculateHash(int position)
		{
			return (uint)((((int)this.window[position] << 8) ^ ((int)this.window[position + 1] << 4) ^ (int)this.window[position + 2]) & 2047);
		}

		// Token: 0x040021AB RID: 8619
		private byte[] window;

		// Token: 0x040021AC RID: 8620
		private int bufPos;

		// Token: 0x040021AD RID: 8621
		private int bufEnd;

		// Token: 0x040021AE RID: 8622
		private const int FastEncoderHashShift = 4;

		// Token: 0x040021AF RID: 8623
		private const int FastEncoderHashtableSize = 2048;

		// Token: 0x040021B0 RID: 8624
		private const int FastEncoderHashMask = 2047;

		// Token: 0x040021B1 RID: 8625
		private const int FastEncoderWindowSize = 8192;

		// Token: 0x040021B2 RID: 8626
		private const int FastEncoderWindowMask = 8191;

		// Token: 0x040021B3 RID: 8627
		private const int FastEncoderMatch3DistThreshold = 16384;

		// Token: 0x040021B4 RID: 8628
		internal const int MaxMatch = 258;

		// Token: 0x040021B5 RID: 8629
		internal const int MinMatch = 3;

		// Token: 0x040021B6 RID: 8630
		private const int SearchDepth = 32;

		// Token: 0x040021B7 RID: 8631
		private const int GoodLength = 4;

		// Token: 0x040021B8 RID: 8632
		private const int NiceLength = 32;

		// Token: 0x040021B9 RID: 8633
		private const int LazyMatchThreshold = 6;

		// Token: 0x040021BA RID: 8634
		private ushort[] prev;

		// Token: 0x040021BB RID: 8635
		private ushort[] lookup;
	}
}
