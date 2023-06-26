using System;

namespace System.IO.Compression
{
	// Token: 0x02000431 RID: 1073
	internal class HuffmanTree
	{
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x000B84DA File Offset: 0x000B66DA
		public static HuffmanTree StaticLiteralLengthTree
		{
			get
			{
				return HuffmanTree.staticLiteralLengthTree;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x000B84E1 File Offset: 0x000B66E1
		public static HuffmanTree StaticDistanceTree
		{
			get
			{
				return HuffmanTree.staticDistanceTree;
			}
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000B84E8 File Offset: 0x000B66E8
		public HuffmanTree(byte[] codeLengths)
		{
			this.codeLengthArray = codeLengths;
			if (this.codeLengthArray.Length == 288)
			{
				this.tableBits = 9;
			}
			else
			{
				this.tableBits = 7;
			}
			this.tableMask = (1 << this.tableBits) - 1;
			this.CreateTable();
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000B853C File Offset: 0x000B673C
		private static byte[] GetStaticLiteralTreeLength()
		{
			byte[] array = new byte[288];
			for (int i = 0; i <= 143; i++)
			{
				array[i] = 8;
			}
			for (int j = 144; j <= 255; j++)
			{
				array[j] = 9;
			}
			for (int k = 256; k <= 279; k++)
			{
				array[k] = 7;
			}
			for (int l = 280; l <= 287; l++)
			{
				array[l] = 8;
			}
			return array;
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000B85B8 File Offset: 0x000B67B8
		private static byte[] GetStaticDistanceTreeLength()
		{
			byte[] array = new byte[32];
			for (int i = 0; i < 32; i++)
			{
				array[i] = 5;
			}
			return array;
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000B85E0 File Offset: 0x000B67E0
		private uint[] CalculateHuffmanCode()
		{
			uint[] array = new uint[17];
			foreach (int num in this.codeLengthArray)
			{
				array[num] += 1U;
			}
			array[0] = 0U;
			uint[] array3 = new uint[17];
			uint num2 = 0U;
			for (int j = 1; j <= 16; j++)
			{
				num2 = num2 + array[j - 1] << 1;
				array3[j] = num2;
			}
			uint[] array4 = new uint[288];
			for (int k = 0; k < this.codeLengthArray.Length; k++)
			{
				int num3 = (int)this.codeLengthArray[k];
				if (num3 > 0)
				{
					array4[k] = FastEncoderStatics.BitReverse(array3[num3], num3);
					array3[num3] += 1U;
				}
			}
			return array4;
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000B86A4 File Offset: 0x000B68A4
		private void CreateTable()
		{
			uint[] array = this.CalculateHuffmanCode();
			this.table = new short[1 << this.tableBits];
			this.left = new short[2 * this.codeLengthArray.Length];
			this.right = new short[2 * this.codeLengthArray.Length];
			short num = (short)this.codeLengthArray.Length;
			for (int i = 0; i < this.codeLengthArray.Length; i++)
			{
				int num2 = (int)this.codeLengthArray[i];
				if (num2 > 0)
				{
					int num3 = (int)array[i];
					if (num2 > this.tableBits)
					{
						int num4 = num2 - this.tableBits;
						int num5 = 1 << this.tableBits;
						int num6 = num3 & ((1 << this.tableBits) - 1);
						short[] array2 = this.table;
						do
						{
							short num7 = array2[num6];
							if (num7 == 0)
							{
								array2[num6] = -num;
								num7 = -num;
								num += 1;
							}
							if (num7 > 0)
							{
								goto Block_6;
							}
							if ((num3 & num5) == 0)
							{
								array2 = this.left;
							}
							else
							{
								array2 = this.right;
							}
							num6 = (int)(-(int)num7);
							num5 <<= 1;
							num4--;
						}
						while (num4 != 0);
						array2[num6] = (short)i;
						goto IL_163;
						Block_6:
						throw new InvalidDataException(SR.GetString("InvalidHuffmanData"));
					}
					int num8 = 1 << num2;
					if (num3 >= num8)
					{
						throw new InvalidDataException(SR.GetString("InvalidHuffmanData"));
					}
					int num9 = 1 << this.tableBits - num2;
					for (int j = 0; j < num9; j++)
					{
						this.table[num3] = (short)i;
						num3 += num8;
					}
				}
				IL_163:;
			}
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000B8828 File Offset: 0x000B6A28
		public int GetNextSymbol(InputBuffer input)
		{
			uint num = input.TryLoad16Bits();
			if (input.AvailableBits == 0)
			{
				return -1;
			}
			int num2;
			checked
			{
				num2 = (int)this.table[(int)((IntPtr)(unchecked((ulong)num & (ulong)((long)this.tableMask))))];
			}
			if (num2 < 0)
			{
				uint num3 = 1U << this.tableBits;
				do
				{
					num2 = -num2;
					if ((num & num3) == 0U)
					{
						num2 = (int)this.left[num2];
					}
					else
					{
						num2 = (int)this.right[num2];
					}
					num3 <<= 1;
				}
				while (num2 < 0);
			}
			int num4 = (int)this.codeLengthArray[num2];
			if (num4 <= 0)
			{
				throw new InvalidDataException(SR.GetString("InvalidHuffmanData"));
			}
			if (num4 > input.AvailableBits)
			{
				return -1;
			}
			input.SkipBits(num4);
			return num2;
		}

		// Token: 0x040021D2 RID: 8658
		internal const int MaxLiteralTreeElements = 288;

		// Token: 0x040021D3 RID: 8659
		internal const int MaxDistTreeElements = 32;

		// Token: 0x040021D4 RID: 8660
		internal const int EndOfBlockCode = 256;

		// Token: 0x040021D5 RID: 8661
		internal const int NumberOfCodeLengthTreeElements = 19;

		// Token: 0x040021D6 RID: 8662
		private int tableBits;

		// Token: 0x040021D7 RID: 8663
		private short[] table;

		// Token: 0x040021D8 RID: 8664
		private short[] left;

		// Token: 0x040021D9 RID: 8665
		private short[] right;

		// Token: 0x040021DA RID: 8666
		private byte[] codeLengthArray;

		// Token: 0x040021DB RID: 8667
		private int tableMask;

		// Token: 0x040021DC RID: 8668
		private static HuffmanTree staticLiteralLengthTree = new HuffmanTree(HuffmanTree.GetStaticLiteralTreeLength());

		// Token: 0x040021DD RID: 8669
		private static HuffmanTree staticDistanceTree = new HuffmanTree(HuffmanTree.GetStaticDistanceTreeLength());
	}
}
