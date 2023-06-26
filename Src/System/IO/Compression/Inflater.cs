using System;

namespace System.IO.Compression
{
	// Token: 0x02000432 RID: 1074
	internal class Inflater : IInflater, IDisposable
	{
		// Token: 0x06002830 RID: 10288 RVA: 0x000B88BD File Offset: 0x000B6ABD
		public Inflater()
			: this(null)
		{
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000B88C8 File Offset: 0x000B6AC8
		internal Inflater(IFileFormatReader reader)
		{
			this.output = new OutputWindow();
			this.input = new InputBuffer();
			this.codeList = new byte[320];
			this.codeLengthTreeCodeLength = new byte[19];
			if (reader != null)
			{
				this.formatReader = reader;
				this.hasFormatReader = true;
			}
			this.Reset();
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000B8931 File Offset: 0x000B6B31
		internal void SetFileFormatReader(IFileFormatReader reader)
		{
			this.formatReader = reader;
			this.hasFormatReader = true;
			this.Reset();
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000B8947 File Offset: 0x000B6B47
		private void Reset()
		{
			if (this.hasFormatReader)
			{
				this.state = InflaterState.ReadingHeader;
				return;
			}
			this.state = InflaterState.ReadingBFinal;
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000B8960 File Offset: 0x000B6B60
		public void SetInput(byte[] inputBytes, int offset, int length)
		{
			this.input.SetInput(inputBytes, offset, length);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000B8970 File Offset: 0x000B6B70
		public bool Finished()
		{
			return this.state == InflaterState.Done || this.state == InflaterState.VerifyingFooter;
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x000B8988 File Offset: 0x000B6B88
		public int AvailableOutput
		{
			get
			{
				return this.output.AvailableBytes;
			}
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000B8995 File Offset: 0x000B6B95
		public bool NeedsInput()
		{
			return this.input.NeedsInput();
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000B89A4 File Offset: 0x000B6BA4
		public int Inflate(byte[] bytes, int offset, int length)
		{
			int num = 0;
			do
			{
				int num2 = this.output.CopyTo(bytes, offset, length);
				if (num2 > 0)
				{
					if (this.hasFormatReader)
					{
						this.formatReader.UpdateWithBytesRead(bytes, offset, num2);
					}
					offset += num2;
					num += num2;
					length -= num2;
				}
			}
			while (length != 0 && !this.Finished() && this.Decode());
			if (this.state == InflaterState.VerifyingFooter && this.output.AvailableBytes == 0)
			{
				this.formatReader.Validate();
			}
			return num;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000B8A20 File Offset: 0x000B6C20
		private bool Decode()
		{
			bool flag = false;
			if (this.Finished())
			{
				return true;
			}
			if (this.hasFormatReader)
			{
				if (this.state == InflaterState.ReadingHeader)
				{
					if (!this.formatReader.ReadHeader(this.input))
					{
						return false;
					}
					this.state = InflaterState.ReadingBFinal;
				}
				else if (this.state == InflaterState.StartReadingFooter || this.state == InflaterState.ReadingFooter)
				{
					if (!this.formatReader.ReadFooter(this.input))
					{
						return false;
					}
					this.state = InflaterState.VerifyingFooter;
					return true;
				}
			}
			if (this.state == InflaterState.ReadingBFinal)
			{
				if (!this.input.EnsureBitsAvailable(1))
				{
					return false;
				}
				this.bfinal = this.input.GetBits(1);
				this.state = InflaterState.ReadingBType;
			}
			if (this.state == InflaterState.ReadingBType)
			{
				if (!this.input.EnsureBitsAvailable(2))
				{
					this.state = InflaterState.ReadingBType;
					return false;
				}
				this.blockType = (BlockType)this.input.GetBits(2);
				if (this.blockType == BlockType.Dynamic)
				{
					this.state = InflaterState.ReadingNumLitCodes;
				}
				else if (this.blockType == BlockType.Static)
				{
					this.literalLengthTree = HuffmanTree.StaticLiteralLengthTree;
					this.distanceTree = HuffmanTree.StaticDistanceTree;
					this.state = InflaterState.DecodeTop;
				}
				else
				{
					if (this.blockType != BlockType.Uncompressed)
					{
						throw new InvalidDataException(SR.GetString("UnknownBlockType"));
					}
					this.state = InflaterState.UncompressedAligning;
				}
			}
			bool flag2;
			if (this.blockType == BlockType.Dynamic)
			{
				if (this.state < InflaterState.DecodeTop)
				{
					flag2 = this.DecodeDynamicBlockHeader();
				}
				else
				{
					flag2 = this.DecodeBlock(out flag);
				}
			}
			else if (this.blockType == BlockType.Static)
			{
				flag2 = this.DecodeBlock(out flag);
			}
			else
			{
				if (this.blockType != BlockType.Uncompressed)
				{
					throw new InvalidDataException(SR.GetString("UnknownBlockType"));
				}
				flag2 = this.DecodeUncompressedBlock(out flag);
			}
			if (flag && this.bfinal != 0)
			{
				if (this.hasFormatReader)
				{
					this.state = InflaterState.StartReadingFooter;
				}
				else
				{
					this.state = InflaterState.Done;
				}
			}
			return flag2;
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000B8BE4 File Offset: 0x000B6DE4
		private bool DecodeUncompressedBlock(out bool end_of_block)
		{
			end_of_block = false;
			for (;;)
			{
				switch (this.state)
				{
				case InflaterState.UncompressedAligning:
					this.input.SkipToByteBoundary();
					this.state = InflaterState.UncompressedByte1;
					goto IL_43;
				case InflaterState.UncompressedByte1:
				case InflaterState.UncompressedByte2:
				case InflaterState.UncompressedByte3:
				case InflaterState.UncompressedByte4:
					goto IL_43;
				case InflaterState.DecodingUncompressed:
					goto IL_D6;
				}
				break;
				IL_43:
				int bits = this.input.GetBits(8);
				if (bits < 0)
				{
					return false;
				}
				this.blockLengthBuffer[this.state - InflaterState.UncompressedByte1] = (byte)bits;
				if (this.state == InflaterState.UncompressedByte4)
				{
					this.blockLength = (int)this.blockLengthBuffer[0] + (int)this.blockLengthBuffer[1] * 256;
					int num = (int)this.blockLengthBuffer[2] + (int)this.blockLengthBuffer[3] * 256;
					if ((ushort)this.blockLength != (ushort)(~(ushort)num))
					{
						goto Block_4;
					}
				}
				this.state++;
			}
			throw new InvalidDataException(SR.GetString("UnknownState"));
			Block_4:
			throw new InvalidDataException(SR.GetString("InvalidBlockLength"));
			IL_D6:
			int num2 = this.output.CopyFrom(this.input, this.blockLength);
			this.blockLength -= num2;
			if (this.blockLength == 0)
			{
				this.state = InflaterState.ReadingBFinal;
				end_of_block = true;
				return true;
			}
			return this.output.FreeBytes == 0;
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000B8D24 File Offset: 0x000B6F24
		private bool DecodeBlock(out bool end_of_block_code_seen)
		{
			end_of_block_code_seen = false;
			int i = this.output.FreeBytes;
			while (i > 258)
			{
				switch (this.state)
				{
				case InflaterState.DecodeTop:
				{
					int num = this.literalLengthTree.GetNextSymbol(this.input);
					if (num < 0)
					{
						return false;
					}
					if (num < 256)
					{
						this.output.Write((byte)num);
						i--;
						continue;
					}
					if (num == 256)
					{
						end_of_block_code_seen = true;
						this.state = InflaterState.ReadingBFinal;
						return true;
					}
					num -= 257;
					if (num < 8)
					{
						num += 3;
						this.extraBits = 0;
					}
					else if (num == 28)
					{
						num = 258;
						this.extraBits = 0;
					}
					else
					{
						if (num < 0 || num >= Inflater.extraLengthBits.Length)
						{
							throw new InvalidDataException(SR.GetString("GenericInvalidData"));
						}
						this.extraBits = (int)Inflater.extraLengthBits[num];
					}
					this.length = num;
					goto IL_E2;
				}
				case InflaterState.HaveInitialLength:
					goto IL_E2;
				case InflaterState.HaveFullLength:
					goto IL_152;
				case InflaterState.HaveDistCode:
					break;
				default:
					throw new InvalidDataException(SR.GetString("UnknownState"));
				}
				IL_1B4:
				int num2;
				if (this.distanceCode > 3)
				{
					this.extraBits = this.distanceCode - 2 >> 1;
					int bits = this.input.GetBits(this.extraBits);
					if (bits < 0)
					{
						return false;
					}
					num2 = Inflater.distanceBasePosition[this.distanceCode] + bits;
				}
				else
				{
					num2 = this.distanceCode + 1;
				}
				this.output.WriteLengthDistance(this.length, num2);
				i -= this.length;
				this.state = InflaterState.DecodeTop;
				continue;
				IL_152:
				if (this.blockType == BlockType.Dynamic)
				{
					this.distanceCode = this.distanceTree.GetNextSymbol(this.input);
				}
				else
				{
					this.distanceCode = this.input.GetBits(5);
					if (this.distanceCode >= 0)
					{
						this.distanceCode = (int)Inflater.staticDistanceTreeTable[this.distanceCode];
					}
				}
				if (this.distanceCode < 0)
				{
					return false;
				}
				this.state = InflaterState.HaveDistCode;
				goto IL_1B4;
				IL_E2:
				if (this.extraBits > 0)
				{
					this.state = InflaterState.HaveInitialLength;
					int bits2 = this.input.GetBits(this.extraBits);
					if (bits2 < 0)
					{
						return false;
					}
					if (this.length < 0 || this.length >= Inflater.lengthBase.Length)
					{
						throw new InvalidDataException(SR.GetString("GenericInvalidData"));
					}
					this.length = Inflater.lengthBase[this.length] + bits2;
				}
				this.state = InflaterState.HaveFullLength;
				goto IL_152;
			}
			return true;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000B8F74 File Offset: 0x000B7174
		private bool DecodeDynamicBlockHeader()
		{
			switch (this.state)
			{
			case InflaterState.ReadingNumLitCodes:
				this.literalLengthCodeCount = this.input.GetBits(5);
				if (this.literalLengthCodeCount < 0)
				{
					return false;
				}
				this.literalLengthCodeCount += 257;
				this.state = InflaterState.ReadingNumDistCodes;
				goto IL_62;
			case InflaterState.ReadingNumDistCodes:
				goto IL_62;
			case InflaterState.ReadingNumCodeLengthCodes:
				goto IL_94;
			case InflaterState.ReadingCodeLengthCodes:
				break;
			case InflaterState.ReadingTreeCodesBefore:
			case InflaterState.ReadingTreeCodesAfter:
				goto IL_327;
			default:
				throw new InvalidDataException(SR.GetString("UnknownState"));
			}
			IL_105:
			while (this.loopCounter < this.codeLengthCodeCount)
			{
				int bits = this.input.GetBits(3);
				if (bits < 0)
				{
					return false;
				}
				this.codeLengthTreeCodeLength[(int)Inflater.codeOrder[this.loopCounter]] = (byte)bits;
				this.loopCounter++;
			}
			for (int i = this.codeLengthCodeCount; i < Inflater.codeOrder.Length; i++)
			{
				this.codeLengthTreeCodeLength[(int)Inflater.codeOrder[i]] = 0;
			}
			this.codeLengthTree = new HuffmanTree(this.codeLengthTreeCodeLength);
			this.codeArraySize = this.literalLengthCodeCount + this.distanceCodeCount;
			this.loopCounter = 0;
			this.state = InflaterState.ReadingTreeCodesBefore;
			IL_327:
			while (this.loopCounter < this.codeArraySize)
			{
				if (this.state == InflaterState.ReadingTreeCodesBefore && (this.lengthCode = this.codeLengthTree.GetNextSymbol(this.input)) < 0)
				{
					return false;
				}
				if (this.lengthCode <= 15)
				{
					byte[] array = this.codeList;
					int num = this.loopCounter;
					this.loopCounter = num + 1;
					array[num] = (byte)this.lengthCode;
				}
				else
				{
					if (!this.input.EnsureBitsAvailable(7))
					{
						this.state = InflaterState.ReadingTreeCodesAfter;
						return false;
					}
					if (this.lengthCode == 16)
					{
						if (this.loopCounter == 0)
						{
							throw new InvalidDataException();
						}
						byte b = this.codeList[this.loopCounter - 1];
						int num2 = this.input.GetBits(2) + 3;
						if (this.loopCounter + num2 > this.codeArraySize)
						{
							throw new InvalidDataException();
						}
						for (int j = 0; j < num2; j++)
						{
							byte[] array2 = this.codeList;
							int num = this.loopCounter;
							this.loopCounter = num + 1;
							array2[num] = b;
						}
					}
					else if (this.lengthCode == 17)
					{
						int num2 = this.input.GetBits(3) + 3;
						if (this.loopCounter + num2 > this.codeArraySize)
						{
							throw new InvalidDataException();
						}
						for (int k = 0; k < num2; k++)
						{
							byte[] array3 = this.codeList;
							int num = this.loopCounter;
							this.loopCounter = num + 1;
							array3[num] = 0;
						}
					}
					else
					{
						int num2 = this.input.GetBits(7) + 11;
						if (this.loopCounter + num2 > this.codeArraySize)
						{
							throw new InvalidDataException();
						}
						for (int l = 0; l < num2; l++)
						{
							byte[] array4 = this.codeList;
							int num = this.loopCounter;
							this.loopCounter = num + 1;
							array4[num] = 0;
						}
					}
				}
				this.state = InflaterState.ReadingTreeCodesBefore;
			}
			byte[] array5 = new byte[288];
			byte[] array6 = new byte[32];
			Array.Copy(this.codeList, array5, this.literalLengthCodeCount);
			Array.Copy(this.codeList, this.literalLengthCodeCount, array6, 0, this.distanceCodeCount);
			if (array5[256] == 0)
			{
				throw new InvalidDataException();
			}
			this.literalLengthTree = new HuffmanTree(array5);
			this.distanceTree = new HuffmanTree(array6);
			this.state = InflaterState.DecodeTop;
			return true;
			IL_62:
			this.distanceCodeCount = this.input.GetBits(5);
			if (this.distanceCodeCount < 0)
			{
				return false;
			}
			this.distanceCodeCount++;
			this.state = InflaterState.ReadingNumCodeLengthCodes;
			IL_94:
			this.codeLengthCodeCount = this.input.GetBits(4);
			if (this.codeLengthCodeCount < 0)
			{
				return false;
			}
			this.codeLengthCodeCount += 4;
			this.loopCounter = 0;
			this.state = InflaterState.ReadingCodeLengthCodes;
			goto IL_105;
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000B9339 File Offset: 0x000B7539
		public void Dispose()
		{
		}

		// Token: 0x040021DE RID: 8670
		private static readonly byte[] extraLengthBits = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0
		};

		// Token: 0x040021DF RID: 8671
		private static readonly int[] lengthBase = new int[]
		{
			3, 4, 5, 6, 7, 8, 9, 10, 11, 13,
			15, 17, 19, 23, 27, 31, 35, 43, 51, 59,
			67, 83, 99, 115, 131, 163, 195, 227, 258
		};

		// Token: 0x040021E0 RID: 8672
		private static readonly int[] distanceBasePosition = new int[]
		{
			1, 2, 3, 4, 5, 7, 9, 13, 17, 25,
			33, 49, 65, 97, 129, 193, 257, 385, 513, 769,
			1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577,
			0, 0
		};

		// Token: 0x040021E1 RID: 8673
		private static readonly byte[] codeOrder = new byte[]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		// Token: 0x040021E2 RID: 8674
		private static readonly byte[] staticDistanceTreeTable = new byte[]
		{
			0, 16, 8, 24, 4, 20, 12, 28, 2, 18,
			10, 26, 6, 22, 14, 30, 1, 17, 9, 25,
			5, 21, 13, 29, 3, 19, 11, 27, 7, 23,
			15, 31
		};

		// Token: 0x040021E3 RID: 8675
		private OutputWindow output;

		// Token: 0x040021E4 RID: 8676
		private InputBuffer input;

		// Token: 0x040021E5 RID: 8677
		private HuffmanTree literalLengthTree;

		// Token: 0x040021E6 RID: 8678
		private HuffmanTree distanceTree;

		// Token: 0x040021E7 RID: 8679
		private InflaterState state;

		// Token: 0x040021E8 RID: 8680
		private bool hasFormatReader;

		// Token: 0x040021E9 RID: 8681
		private int bfinal;

		// Token: 0x040021EA RID: 8682
		private BlockType blockType;

		// Token: 0x040021EB RID: 8683
		private byte[] blockLengthBuffer = new byte[4];

		// Token: 0x040021EC RID: 8684
		private int blockLength;

		// Token: 0x040021ED RID: 8685
		private int length;

		// Token: 0x040021EE RID: 8686
		private int distanceCode;

		// Token: 0x040021EF RID: 8687
		private int extraBits;

		// Token: 0x040021F0 RID: 8688
		private int loopCounter;

		// Token: 0x040021F1 RID: 8689
		private int literalLengthCodeCount;

		// Token: 0x040021F2 RID: 8690
		private int distanceCodeCount;

		// Token: 0x040021F3 RID: 8691
		private int codeLengthCodeCount;

		// Token: 0x040021F4 RID: 8692
		private int codeArraySize;

		// Token: 0x040021F5 RID: 8693
		private int lengthCode;

		// Token: 0x040021F6 RID: 8694
		private byte[] codeList;

		// Token: 0x040021F7 RID: 8695
		private byte[] codeLengthTreeCodeLength;

		// Token: 0x040021F8 RID: 8696
		private HuffmanTree codeLengthTree;

		// Token: 0x040021F9 RID: 8697
		private IFileFormatReader formatReader;
	}
}
