using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001DA RID: 474
	internal sealed class ODataBatchReaderStreamBuffer
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00033894 File Offset: 0x00031A94
		internal byte[] Bytes
		{
			get
			{
				return this.bytes;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0003389C File Offset: 0x00031A9C
		internal int CurrentReadPosition
		{
			get
			{
				return this.currentReadPosition;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x000338A4 File Offset: 0x00031AA4
		internal int NumberOfBytesInBuffer
		{
			get
			{
				return this.numberOfBytesInBuffer;
			}
		}

		// Token: 0x1700031A RID: 794
		internal byte this[int index]
		{
			get
			{
				return this.bytes[index];
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x000338B8 File Offset: 0x00031AB8
		internal void SkipTo(int newPosition)
		{
			int num = newPosition - this.currentReadPosition;
			this.currentReadPosition = newPosition;
			this.numberOfBytesInBuffer -= num;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000338E4 File Offset: 0x00031AE4
		internal bool RefillFrom(Stream stream, int preserveFrom)
		{
			this.ShiftToBeginning(preserveFrom);
			int num = 8000 - this.numberOfBytesInBuffer;
			int num2 = stream.Read(this.bytes, this.numberOfBytesInBuffer, num);
			this.numberOfBytesInBuffer += num2;
			return num2 == 0;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003392C File Offset: 0x00031B2C
		internal ODataBatchReaderStreamScanResult ScanForLineEnd(out int lineEndStartPosition, out int lineEndEndPosition)
		{
			bool flag;
			return this.ScanForLineEnd(this.currentReadPosition, 8000, false, out lineEndStartPosition, out lineEndEndPosition, out flag);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00033950 File Offset: 0x00031B50
		internal ODataBatchReaderStreamScanResult ScanForBoundary(IEnumerable<string> boundaries, int maxDataBytesToScan, out int boundaryStartPosition, out int boundaryEndPosition, out bool isEndBoundary, out bool isParentBoundary)
		{
			boundaryStartPosition = -1;
			boundaryEndPosition = -1;
			isEndBoundary = false;
			isParentBoundary = false;
			int num = this.currentReadPosition;
			int num2;
			int num3;
			for (;;)
			{
				switch (this.ScanForBoundaryStart(num, maxDataBytesToScan, out num2, out num3))
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
					return ODataBatchReaderStreamScanResult.NoMatch;
				case ODataBatchReaderStreamScanResult.PartialMatch:
					goto IL_40;
				case ODataBatchReaderStreamScanResult.Match:
					isParentBoundary = false;
					foreach (string text in boundaries)
					{
						switch (this.MatchBoundary(num2, num3, text, out boundaryStartPosition, out boundaryEndPosition, out isEndBoundary))
						{
						case ODataBatchReaderStreamScanResult.NoMatch:
							boundaryStartPosition = -1;
							boundaryEndPosition = -1;
							isEndBoundary = false;
							isParentBoundary = true;
							break;
						case ODataBatchReaderStreamScanResult.PartialMatch:
							boundaryEndPosition = -1;
							isEndBoundary = false;
							return ODataBatchReaderStreamScanResult.PartialMatch;
						case ODataBatchReaderStreamScanResult.Match:
							return ODataBatchReaderStreamScanResult.Match;
						default:
							throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStreamBuffer_ScanForBoundary));
						}
					}
					num = ((num == num3) ? (num3 + 1) : num3);
					continue;
				}
				break;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStreamBuffer_ScanForBoundary));
			IL_40:
			boundaryStartPosition = ((num2 < 0) ? num3 : num2);
			return ODataBatchReaderStreamScanResult.PartialMatch;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00033A6C File Offset: 0x00031C6C
		private ODataBatchReaderStreamScanResult ScanForBoundaryStart(int scanStartIx, int maxDataBytesToScan, out int lineEndStartPosition, out int boundaryDelimiterStartPosition)
		{
			int num = this.currentReadPosition + Math.Min(maxDataBytesToScan, this.numberOfBytesInBuffer) - 1;
			int i = scanStartIx;
			while (i <= num)
			{
				char c = (char)this.bytes[i];
				if (c == '\r' || c == '\n')
				{
					lineEndStartPosition = i;
					if (c == '\r' && i == num && maxDataBytesToScan >= this.numberOfBytesInBuffer)
					{
						boundaryDelimiterStartPosition = i;
						return ODataBatchReaderStreamScanResult.PartialMatch;
					}
					boundaryDelimiterStartPosition = ((c == '\r' && this.bytes[i + 1] == 10) ? (i + 2) : (i + 1));
					return ODataBatchReaderStreamScanResult.Match;
				}
				else
				{
					if (c == '-')
					{
						lineEndStartPosition = -1;
						if (i == num && maxDataBytesToScan >= this.numberOfBytesInBuffer)
						{
							boundaryDelimiterStartPosition = i;
							return ODataBatchReaderStreamScanResult.PartialMatch;
						}
						if (this.bytes[i + 1] == 45)
						{
							boundaryDelimiterStartPosition = i;
							return ODataBatchReaderStreamScanResult.Match;
						}
					}
					i++;
				}
			}
			lineEndStartPosition = -1;
			boundaryDelimiterStartPosition = -1;
			return ODataBatchReaderStreamScanResult.NoMatch;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00033B28 File Offset: 0x00031D28
		private ODataBatchReaderStreamScanResult ScanForLineEnd(int scanStartIx, int maxDataBytesToScan, bool allowLeadingWhitespaceOnly, out int lineEndStartPosition, out int lineEndEndPosition, out bool endOfBufferReached)
		{
			endOfBufferReached = false;
			int num = this.currentReadPosition + Math.Min(maxDataBytesToScan, this.numberOfBytesInBuffer) - 1;
			int i = scanStartIx;
			while (i <= num)
			{
				char c = (char)this.bytes[i];
				if (c == '\r' || c == '\n')
				{
					lineEndStartPosition = i;
					if (c == '\r' && i == num && maxDataBytesToScan >= this.numberOfBytesInBuffer)
					{
						lineEndEndPosition = -1;
						return ODataBatchReaderStreamScanResult.PartialMatch;
					}
					lineEndEndPosition = lineEndStartPosition;
					if (c == '\r' && this.bytes[i + 1] == 10)
					{
						lineEndEndPosition++;
					}
					return ODataBatchReaderStreamScanResult.Match;
				}
				else
				{
					if (allowLeadingWhitespaceOnly && !char.IsWhiteSpace(c))
					{
						lineEndStartPosition = -1;
						lineEndEndPosition = -1;
						return ODataBatchReaderStreamScanResult.NoMatch;
					}
					i++;
				}
			}
			endOfBufferReached = true;
			lineEndStartPosition = -1;
			lineEndEndPosition = -1;
			return ODataBatchReaderStreamScanResult.NoMatch;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00033BD0 File Offset: 0x00031DD0
		private ODataBatchReaderStreamScanResult MatchBoundary(int lineEndStartPosition, int boundaryDelimiterStartPosition, string boundary, out int boundaryStartPosition, out int boundaryEndPosition, out bool isEndBoundary)
		{
			boundaryStartPosition = -1;
			boundaryEndPosition = -1;
			int num = this.currentReadPosition + this.numberOfBytesInBuffer - 1;
			int num2 = boundaryDelimiterStartPosition + 2 + boundary.Length + 2 - 1;
			bool flag;
			int num3;
			if (num < num2 + 2)
			{
				flag = true;
				num3 = Math.Min(num, num2) - boundaryDelimiterStartPosition + 1;
			}
			else
			{
				flag = false;
				num3 = num2 - boundaryDelimiterStartPosition + 1;
			}
			if (this.MatchBoundary(boundary, boundaryDelimiterStartPosition, num3, out isEndBoundary))
			{
				boundaryStartPosition = ((lineEndStartPosition < 0) ? boundaryDelimiterStartPosition : lineEndStartPosition);
				if (flag)
				{
					isEndBoundary = false;
					return ODataBatchReaderStreamScanResult.PartialMatch;
				}
				boundaryEndPosition = boundaryDelimiterStartPosition + 2 + boundary.Length - 1;
				if (isEndBoundary)
				{
					boundaryEndPosition += 2;
				}
				int num4;
				int num5;
				bool flag2;
				switch (this.ScanForLineEnd(boundaryEndPosition + 1, 2147483647, true, out num4, out num5, out flag2))
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
					if (flag2)
					{
						if (boundaryStartPosition == 0)
						{
							throw new ODataException(Strings.ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached(8000));
						}
						isEndBoundary = false;
						return ODataBatchReaderStreamScanResult.PartialMatch;
					}
					break;
				case ODataBatchReaderStreamScanResult.PartialMatch:
					if (boundaryStartPosition == 0)
					{
						throw new ODataException(Strings.ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached(8000));
					}
					isEndBoundary = false;
					return ODataBatchReaderStreamScanResult.PartialMatch;
				case ODataBatchReaderStreamScanResult.Match:
					boundaryEndPosition = num5;
					return ODataBatchReaderStreamScanResult.Match;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStreamBuffer_ScanForBoundary));
				}
			}
			return ODataBatchReaderStreamScanResult.NoMatch;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00033CF4 File Offset: 0x00031EF4
		private bool MatchBoundary(string boundary, int startIx, int matchLength, out bool isEndBoundary)
		{
			isEndBoundary = false;
			if (matchLength == 0)
			{
				return true;
			}
			int num = 0;
			int num2 = startIx;
			for (int i = -2; i < matchLength - 2; i++)
			{
				if (i < 0)
				{
					if (this.bytes[num2] != 45)
					{
						return false;
					}
				}
				else if (i < boundary.Length)
				{
					if ((char)this.bytes[num2] != boundary[i])
					{
						return false;
					}
				}
				else
				{
					if (this.bytes[num2] != 45)
					{
						return true;
					}
					num++;
				}
				num2++;
			}
			isEndBoundary = num == 2;
			return true;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00033D6C File Offset: 0x00031F6C
		private void ShiftToBeginning(int startIndex)
		{
			int num = this.currentReadPosition + this.numberOfBytesInBuffer - startIndex;
			this.currentReadPosition = 0;
			if (num <= 0)
			{
				this.numberOfBytesInBuffer = 0;
				return;
			}
			this.numberOfBytesInBuffer = num;
			Buffer.BlockCopy(this.bytes, startIndex, this.bytes, 0, num);
		}

		// Token: 0x04000509 RID: 1289
		internal const int BufferLength = 8000;

		// Token: 0x0400050A RID: 1290
		private const int MaxLineFeedLength = 2;

		// Token: 0x0400050B RID: 1291
		private const int TwoDashesLength = 2;

		// Token: 0x0400050C RID: 1292
		private readonly byte[] bytes = new byte[8000];

		// Token: 0x0400050D RID: 1293
		private int currentReadPosition;

		// Token: 0x0400050E RID: 1294
		private int numberOfBytesInBuffer;
	}
}
