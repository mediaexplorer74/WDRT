using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x020001DB RID: 475
	internal sealed class ODataBatchReaderStream
	{
		// Token: 0x06000EB9 RID: 3769 RVA: 0x00033DCF File Offset: 0x00031FCF
		internal ODataBatchReaderStream(ODataRawInputContext inputContext, string batchBoundary, Encoding batchEncoding)
		{
			this.inputContext = inputContext;
			this.batchBoundary = batchBoundary;
			this.batchEncoding = batchEncoding;
			this.batchBuffer = new ODataBatchReaderStreamBuffer();
			this.lineBuffer = new byte[2000];
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00033E07 File Offset: 0x00032007
		internal string BatchBoundary
		{
			get
			{
				return this.batchBoundary;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00033E0F File Offset: 0x0003200F
		internal string ChangeSetBoundary
		{
			get
			{
				return this.changesetBoundary;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00033F20 File Offset: 0x00032120
		private IEnumerable<string> CurrentBoundaries
		{
			get
			{
				if (this.changesetBoundary != null)
				{
					yield return this.changesetBoundary;
				}
				yield return this.batchBoundary;
				yield break;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00033F3D File Offset: 0x0003213D
		private Encoding CurrentEncoding
		{
			get
			{
				return this.changesetEncoding ?? this.batchEncoding;
			}
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00033F4F File Offset: 0x0003214F
		internal void ResetChangeSetBoundary()
		{
			this.changesetBoundary = null;
			this.changesetEncoding = null;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00033F60 File Offset: 0x00032160
		internal bool SkipToBoundary(out bool isEndBoundary, out bool isParentBoundary)
		{
			this.EnsureBatchEncoding();
			ODataBatchReaderStreamScanResult odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.NoMatch;
			while (odataBatchReaderStreamScanResult != ODataBatchReaderStreamScanResult.Match)
			{
				int num;
				int num2;
				odataBatchReaderStreamScanResult = this.batchBuffer.ScanForBoundary(this.CurrentBoundaries, int.MaxValue, out num, out num2, out isEndBoundary, out isParentBoundary);
				switch (odataBatchReaderStreamScanResult)
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
					if (this.underlyingStreamExhausted)
					{
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + this.batchBuffer.NumberOfBytesInBuffer);
						return false;
					}
					this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, 8000);
					break;
				case ODataBatchReaderStreamScanResult.PartialMatch:
					if (this.underlyingStreamExhausted)
					{
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + this.batchBuffer.NumberOfBytesInBuffer);
						return false;
					}
					this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, num);
					break;
				case ODataBatchReaderStreamScanResult.Match:
					this.batchBuffer.SkipTo(isParentBoundary ? num : (num2 + 1));
					return true;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_SkipToBoundary));
				}
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_SkipToBoundary));
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003408C File Offset: 0x0003228C
		internal int ReadWithDelimiter(byte[] userBuffer, int userBufferOffset, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			int num = count;
			ODataBatchReaderStreamScanResult odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.NoMatch;
			while (num > 0 && odataBatchReaderStreamScanResult != ODataBatchReaderStreamScanResult.Match)
			{
				int num2;
				int num3;
				bool flag;
				bool flag2;
				odataBatchReaderStreamScanResult = this.batchBuffer.ScanForBoundary(this.CurrentBoundaries, num, out num2, out num3, out flag, out flag2);
				switch (odataBatchReaderStreamScanResult)
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
				{
					if (this.batchBuffer.NumberOfBytesInBuffer >= num)
					{
						Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num);
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + num);
						return count;
					}
					int numberOfBytesInBuffer = this.batchBuffer.NumberOfBytesInBuffer;
					Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, numberOfBytesInBuffer);
					num -= numberOfBytesInBuffer;
					userBufferOffset += numberOfBytesInBuffer;
					if (this.underlyingStreamExhausted)
					{
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + numberOfBytesInBuffer);
						return count - num;
					}
					this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, 8000);
					break;
				}
				case ODataBatchReaderStreamScanResult.PartialMatch:
				{
					if (this.underlyingStreamExhausted)
					{
						int num4 = Math.Min(this.batchBuffer.NumberOfBytesInBuffer, num);
						Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num4);
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + num4);
						num -= num4;
						return count - num;
					}
					int num5 = num2 - this.batchBuffer.CurrentReadPosition;
					Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num5);
					num -= num5;
					userBufferOffset += num5;
					this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, num2);
					break;
				}
				case ODataBatchReaderStreamScanResult.Match:
				{
					int num5 = num2 - this.batchBuffer.CurrentReadPosition;
					Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num5);
					num -= num5;
					userBufferOffset += num5;
					this.batchBuffer.SkipTo(num2);
					return count - num;
				}
				}
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_ReadWithDelimiter));
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x000342C0 File Offset: 0x000324C0
		internal int ReadWithLength(byte[] userBuffer, int userBufferOffset, int count)
		{
			int i = count;
			while (i > 0)
			{
				if (this.batchBuffer.NumberOfBytesInBuffer >= i)
				{
					Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, i);
					this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + i);
					i = 0;
				}
				else
				{
					int numberOfBytesInBuffer = this.batchBuffer.NumberOfBytesInBuffer;
					Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, numberOfBytesInBuffer);
					i -= numberOfBytesInBuffer;
					userBufferOffset += numberOfBytesInBuffer;
					if (this.underlyingStreamExhausted)
					{
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStreamBuffer_ReadWithLength));
					}
					this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, 8000);
				}
			}
			return count - i;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00034394 File Offset: 0x00032594
		internal bool ProcessPartHeader()
		{
			bool flag;
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.ReadPartHeaders(out flag);
			if (flag)
			{
				this.DetermineChangesetBoundaryAndEncoding(odataBatchOperationHeaders["Content-Type"]);
				if (this.changesetEncoding == null)
				{
					this.changesetEncoding = this.DetectEncoding();
				}
				ReaderValidationUtils.ValidateEncodingSupportedInBatch(this.changesetEncoding);
			}
			return flag;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000343E0 File Offset: 0x000325E0
		internal ODataBatchOperationHeaders ReadHeaders()
		{
			ODataBatchOperationHeaders odataBatchOperationHeaders = new ODataBatchOperationHeaders();
			string text = this.ReadLine();
			while (text != null && text.Length > 0)
			{
				string text2;
				string text3;
				ODataBatchReaderStream.ValidateHeaderLine(text, out text2, out text3);
				if (odataBatchOperationHeaders.ContainsKeyOrdinal(text2))
				{
					throw new ODataException(Strings.ODataBatchReaderStream_DuplicateHeaderFound(text2));
				}
				odataBatchOperationHeaders.Add(text2, text3);
				text = this.ReadLine();
			}
			return odataBatchOperationHeaders;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00034438 File Offset: 0x00032638
		internal string ReadFirstNonEmptyLine()
		{
			for (;;)
			{
				string text = this.ReadLine();
				if (text == null)
				{
					break;
				}
				if (text.Length != 0)
				{
					return text;
				}
			}
			throw new ODataException(Strings.ODataBatchReaderStream_UnexpectedEndOfInput);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00034464 File Offset: 0x00032664
		private static void ValidateHeaderLine(string headerLine, out string headerName, out string headerValue)
		{
			int num = headerLine.IndexOf(':');
			if (num <= 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidHeaderSpecified(headerLine));
			}
			headerName = headerLine.Substring(0, num).Trim();
			headerValue = headerLine.Substring(num + 1).Trim();
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000344AC File Offset: 0x000326AC
		private string ReadLine()
		{
			int num = 0;
			byte[] array = this.lineBuffer;
			ODataBatchReaderStreamScanResult odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.NoMatch;
			while (odataBatchReaderStreamScanResult != ODataBatchReaderStreamScanResult.Match)
			{
				int num2;
				int num3;
				odataBatchReaderStreamScanResult = this.batchBuffer.ScanForLineEnd(out num2, out num3);
				switch (odataBatchReaderStreamScanResult)
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
				{
					int num4 = this.batchBuffer.NumberOfBytesInBuffer;
					if (num4 > 0)
					{
						ODataBatchUtils.EnsureArraySize(ref array, num, num4);
						Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, array, num, num4);
						num += num4;
					}
					if (this.underlyingStreamExhausted)
					{
						if (num == 0)
						{
							return null;
						}
						odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.Match;
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + num4);
					}
					else
					{
						this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, 8000);
					}
					break;
				}
				case ODataBatchReaderStreamScanResult.PartialMatch:
				{
					int num4 = num2 - this.batchBuffer.CurrentReadPosition;
					if (num4 > 0)
					{
						ODataBatchUtils.EnsureArraySize(ref array, num, num4);
						Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, array, num, num4);
						num += num4;
					}
					if (this.underlyingStreamExhausted)
					{
						odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.Match;
						this.batchBuffer.SkipTo(num2 + 1);
					}
					else
					{
						this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, num2);
					}
					break;
				}
				case ODataBatchReaderStreamScanResult.Match:
				{
					int num4 = num2 - this.batchBuffer.CurrentReadPosition;
					if (num4 > 0)
					{
						ODataBatchUtils.EnsureArraySize(ref array, num, num4);
						Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, array, num, num4);
						num += num4;
					}
					this.batchBuffer.SkipTo(num3 + 1);
					break;
				}
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_ReadLine));
				}
			}
			return this.CurrentEncoding.GetString(array, 0, num);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00034673 File Offset: 0x00032873
		private void EnsureBatchEncoding()
		{
			if (this.batchEncoding == null)
			{
				this.batchEncoding = this.DetectEncoding();
			}
			ReaderValidationUtils.ValidateEncodingSupportedInBatch(this.batchEncoding);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00034694 File Offset: 0x00032894
		private Encoding DetectEncoding()
		{
			while (!this.underlyingStreamExhausted && this.batchBuffer.NumberOfBytesInBuffer < 4)
			{
				this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, this.batchBuffer.CurrentReadPosition);
			}
			int numberOfBytesInBuffer = this.batchBuffer.NumberOfBytesInBuffer;
			if (numberOfBytesInBuffer < 2)
			{
				return Encoding.ASCII;
			}
			if (this.batchBuffer[this.batchBuffer.CurrentReadPosition] == 254 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 1] == 255)
			{
				return new UnicodeEncoding(true, true);
			}
			if (this.batchBuffer[this.batchBuffer.CurrentReadPosition] == 255 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 1] == 254)
			{
				if (numberOfBytesInBuffer >= 4 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 2] == 0 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 3] == 0)
				{
					return new UTF32Encoding(false, true);
				}
				return new UnicodeEncoding(false, true);
			}
			else
			{
				if (numberOfBytesInBuffer >= 3 && this.batchBuffer[this.batchBuffer.CurrentReadPosition] == 239 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 1] == 187 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 2] == 191)
				{
					return Encoding.UTF8;
				}
				if (numberOfBytesInBuffer >= 4 && this.batchBuffer[this.batchBuffer.CurrentReadPosition] == 0 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 1] == 0 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 2] == 254 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 3] == 255)
				{
					return new UTF32Encoding(true, true);
				}
				return Encoding.ASCII;
			}
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x000348A4 File Offset: 0x00032AA4
		private ODataBatchOperationHeaders ReadPartHeaders(out bool isChangeSetPart)
		{
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.ReadHeaders();
			return this.ValidatePartHeaders(odataBatchOperationHeaders, out isChangeSetPart);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x000348C0 File Offset: 0x00032AC0
		private ODataBatchOperationHeaders ValidatePartHeaders(ODataBatchOperationHeaders headers, out bool isChangeSetPart)
		{
			string text;
			if (!headers.TryGetValue("Content-Type", out text))
			{
				throw new ODataException(Strings.ODataBatchReaderStream_MissingContentTypeHeader);
			}
			if (MediaTypeUtils.MediaTypeAndSubtypeAreEqual(text, "application/http"))
			{
				isChangeSetPart = false;
				string text2;
				if (!headers.TryGetValue("Content-Transfer-Encoding", out text2) || string.Compare(text2, "binary", StringComparison.OrdinalIgnoreCase) != 0)
				{
					throw new ODataException(Strings.ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader("Content-Transfer-Encoding", "binary"));
				}
			}
			else
			{
				if (!MediaTypeUtils.MediaTypeStartsWithTypeAndSubtype(text, "multipart/mixed"))
				{
					throw new ODataException(Strings.ODataBatchReaderStream_InvalidContentTypeSpecified("Content-Type", text, "multipart/mixed", "application/http"));
				}
				isChangeSetPart = true;
				if (this.changesetBoundary != null)
				{
					throw new ODataException(Strings.ODataBatchReaderStream_NestedChangesetsAreNotSupported);
				}
			}
			return headers;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00034968 File Offset: 0x00032B68
		private void DetermineChangesetBoundaryAndEncoding(string contentType)
		{
			MediaType mediaType;
			ODataPayloadKind odataPayloadKind;
			MediaTypeUtils.GetFormatFromContentType(contentType, new ODataPayloadKind[] { ODataPayloadKind.Batch }, MediaTypeResolver.DefaultMediaTypeResolver, out mediaType, out this.changesetEncoding, out odataPayloadKind, out this.changesetBoundary);
		}

		// Token: 0x0400050F RID: 1295
		private const int LineBufferLength = 2000;

		// Token: 0x04000510 RID: 1296
		private readonly byte[] lineBuffer;

		// Token: 0x04000511 RID: 1297
		private readonly ODataRawInputContext inputContext;

		// Token: 0x04000512 RID: 1298
		private readonly string batchBoundary;

		// Token: 0x04000513 RID: 1299
		private readonly ODataBatchReaderStreamBuffer batchBuffer;

		// Token: 0x04000514 RID: 1300
		private Encoding batchEncoding;

		// Token: 0x04000515 RID: 1301
		private string changesetBoundary;

		// Token: 0x04000516 RID: 1302
		private Encoding changesetEncoding;

		// Token: 0x04000517 RID: 1303
		private bool underlyingStreamExhausted;
	}
}
