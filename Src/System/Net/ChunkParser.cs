using System;
using System.IO;
using System.Net.Configuration;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000197 RID: 407
	internal sealed class ChunkParser
	{
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00052149 File Offset: 0x00050349
		private bool IsAsync
		{
			get
			{
				return this.userAsyncResult != null;
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00052154 File Offset: 0x00050354
		static ChunkParser()
		{
			for (int i = 33; i < 127; i++)
			{
				ChunkParser.tokenChars[i] = true;
			}
			ChunkParser.tokenChars[40] = false;
			ChunkParser.tokenChars[41] = false;
			ChunkParser.tokenChars[60] = false;
			ChunkParser.tokenChars[62] = false;
			ChunkParser.tokenChars[64] = false;
			ChunkParser.tokenChars[44] = false;
			ChunkParser.tokenChars[59] = false;
			ChunkParser.tokenChars[58] = false;
			ChunkParser.tokenChars[92] = false;
			ChunkParser.tokenChars[34] = false;
			ChunkParser.tokenChars[47] = false;
			ChunkParser.tokenChars[91] = false;
			ChunkParser.tokenChars[93] = false;
			ChunkParser.tokenChars[63] = false;
			ChunkParser.tokenChars[61] = false;
			ChunkParser.tokenChars[123] = false;
			ChunkParser.tokenChars[125] = false;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0005221F File Offset: 0x0005041F
		public ChunkParser(Stream dataSource, byte[] internalBuffer, int initialBufferOffset, int initialBufferCount, int maxBufferLength)
		{
			this.dataSource = dataSource;
			this.buffer = internalBuffer;
			this.bufferCurrentPos = initialBufferOffset;
			this.bufferFillLength = initialBufferOffset + initialBufferCount;
			this.maxBufferLength = maxBufferLength;
			this.currentChunkLength = -1;
			this.readState = ChunkParser.ReadState.ChunkLength;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0005225C File Offset: 0x0005045C
		public IAsyncResult ReadAsync(object caller, byte[] userBuffer, int userBufferOffset, int userBufferCount, AsyncCallback callback, object state)
		{
			this.SetReadParameters(userBuffer, userBufferOffset, userBufferCount);
			this.userAsyncResult = new LazyAsyncResult(caller, state, callback);
			IAsyncResult asyncResult = this.userAsyncResult;
			try
			{
				this.ProcessResponse();
			}
			catch (Exception ex)
			{
				this.CompleteUserRead(ex);
			}
			return asyncResult;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000522B0 File Offset: 0x000504B0
		public int Read(byte[] userBuffer, int userBufferOffset, int userBufferCount)
		{
			this.SetReadParameters(userBuffer, userBufferOffset, userBufferCount);
			try
			{
				this.ProcessResponse();
			}
			catch (Exception)
			{
				this.TransitionToErrorState();
				throw;
			}
			return this.syncResult;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000522F0 File Offset: 0x000504F0
		private void SetReadParameters(byte[] userBuffer, int userBufferOffset, int userBufferCount)
		{
			if (Interlocked.CompareExchange<byte[]>(ref this.userBuffer, userBuffer, null) != null)
			{
				throw new InvalidOperationException(SR.GetString("net_inasync"));
			}
			this.userBufferCount = userBufferCount;
			this.userBufferOffset = userBufferOffset;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00052320 File Offset: 0x00050520
		public bool TryGetLeftoverBytes(out byte[] buffer, out int leftoverBufferOffset, out int leftoverBufferSize)
		{
			leftoverBufferOffset = 0;
			leftoverBufferSize = 0;
			buffer = null;
			if (this.readState != ChunkParser.ReadState.Done)
			{
				return false;
			}
			if (this.bufferCurrentPos == this.bufferFillLength)
			{
				return false;
			}
			leftoverBufferOffset = this.bufferCurrentPos;
			leftoverBufferSize = this.bufferFillLength - this.bufferCurrentPos;
			buffer = this.buffer;
			return true;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00052374 File Offset: 0x00050574
		private void ProcessResponse()
		{
			while (this.readState < ChunkParser.ReadState.Done)
			{
				DataParseStatus dataParseStatus;
				switch (this.readState)
				{
				case ChunkParser.ReadState.ChunkLength:
					dataParseStatus = this.ParseChunkLength();
					break;
				case ChunkParser.ReadState.Extension:
					dataParseStatus = this.ParseExtension();
					break;
				case ChunkParser.ReadState.Payload:
					dataParseStatus = this.HandlePayload();
					break;
				case ChunkParser.ReadState.PayloadEnd:
					dataParseStatus = this.ParsePayloadEnd();
					break;
				case ChunkParser.ReadState.Trailer:
					dataParseStatus = this.ParseTrailer();
					break;
				default:
					throw new InternalException();
				}
				switch (dataParseStatus)
				{
				case DataParseStatus.NeedMoreData:
					if (!this.TryGetMoreData())
					{
						return;
					}
					break;
				case DataParseStatus.ContinueParsing:
					break;
				case DataParseStatus.Done:
					return;
				case DataParseStatus.Invalid:
				case DataParseStatus.DataTooBig:
					this.CompleteUserRead(new IOException(SR.GetString("net_io_readfailure", new object[] { SR.GetString("net_io_connectionclosed") })));
					return;
				default:
					throw new InternalException();
				}
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00052440 File Offset: 0x00050640
		private void CompleteUserRead(object result)
		{
			bool flag = result is Exception;
			this.userBuffer = null;
			this.userBufferCount = 0;
			this.userBufferOffset = 0;
			if (flag)
			{
				this.TransitionToErrorState();
			}
			if (this.IsAsync)
			{
				LazyAsyncResult lazyAsyncResult = this.userAsyncResult;
				this.userAsyncResult = null;
				lazyAsyncResult.InvokeCallback(result);
				return;
			}
			if (flag)
			{
				throw result as Exception;
			}
			this.syncResult = (int)result;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000524A9 File Offset: 0x000506A9
		private void TransitionToErrorState()
		{
			this.readState = ChunkParser.ReadState.Error;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000524B4 File Offset: 0x000506B4
		private bool TryGetMoreData()
		{
			this.PrepareBufferForMoreData();
			int num = this.buffer.Length - this.bufferFillLength;
			if (this.readState == ChunkParser.ReadState.ChunkLength)
			{
				num = Math.Min(12, num);
			}
			int num2;
			if (this.IsAsync)
			{
				IAsyncResult asyncResult = this.dataSource.BeginRead(this.buffer, this.bufferFillLength, num, new AsyncCallback(this.ReadCallback), null);
				this.CheckAsyncResult(asyncResult);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				num2 = this.dataSource.EndRead(asyncResult);
			}
			else
			{
				num2 = this.dataSource.Read(this.buffer, this.bufferFillLength, num);
			}
			this.CompleteMetaDataReadOperation(num2);
			return true;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0005255C File Offset: 0x0005075C
		private void PrepareBufferForMoreData()
		{
			int num = this.bufferCurrentPos;
			this.bufferCurrentPos = 0;
			if (num == this.bufferFillLength)
			{
				this.bufferFillLength = 0;
				return;
			}
			if (num > 0 || this.bufferFillLength < this.buffer.Length)
			{
				if (num > 0)
				{
					int num2 = this.bufferFillLength - num;
					Buffer.BlockCopy(this.buffer, num, this.buffer, 0, num2);
					this.bufferFillLength = num2;
				}
				return;
			}
			if (this.buffer.Length == this.maxBufferLength)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			int num3 = Math.Min(this.maxBufferLength, this.buffer.Length * 2);
			byte[] array = new byte[num3];
			Buffer.BlockCopy(this.buffer, 0, array, 0, this.buffer.Length);
			this.buffer = array;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00052632 File Offset: 0x00050832
		private void CheckAsyncResult(IAsyncResult ar)
		{
			if (ar == null)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00052649 File Offset: 0x00050849
		private void CompleteMetaDataReadOperation(int bytesRead)
		{
			if (bytesRead == 0)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { SR.GetString("net_io_connectionclosed") }));
			}
			this.bufferFillLength += bytesRead;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00052680 File Offset: 0x00050880
		public void ReadCallback(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			try
			{
				int num = this.dataSource.EndRead(ar);
				if (this.readState == ChunkParser.ReadState.Payload)
				{
					this.CompletePayloadReadOperation(num);
				}
				else
				{
					this.CompleteMetaDataReadOperation(num);
					this.ProcessResponse();
				}
			}
			catch (Exception ex)
			{
				this.CompleteUserRead(ex);
			}
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x000526E0 File Offset: 0x000508E0
		private DataParseStatus HandlePayload()
		{
			if (this.bufferCurrentPos < this.bufferFillLength)
			{
				int num = Math.Min(Math.Min(this.userBufferCount, this.bufferFillLength - this.bufferCurrentPos), this.currentChunkLength - this.currentChunkBytesRead);
				Buffer.BlockCopy(this.buffer, this.bufferCurrentPos, this.userBuffer, this.userBufferOffset, num);
				this.bufferCurrentPos += num;
				if (this.currentChunkBytesRead + num == this.currentChunkLength || num == this.userBufferCount)
				{
					this.CompletePayloadReadOperation(num);
					return DataParseStatus.Done;
				}
				this.currentOperationBytesRead += num;
				this.currentChunkBytesRead += num;
			}
			int num2 = Math.Min(this.userBufferCount - this.currentOperationBytesRead, this.currentChunkLength - this.currentChunkBytesRead);
			if (this.IsAsync)
			{
				IAsyncResult asyncResult = this.dataSource.BeginRead(this.userBuffer, this.userBufferOffset + this.currentOperationBytesRead, num2, new AsyncCallback(this.ReadCallback), null);
				this.CheckAsyncResult(asyncResult);
				if (asyncResult.CompletedSynchronously)
				{
					this.CompletePayloadReadOperation(this.dataSource.EndRead(asyncResult));
				}
			}
			else
			{
				int num3 = this.dataSource.Read(this.userBuffer, this.userBufferOffset + this.currentOperationBytesRead, num2);
				this.CompletePayloadReadOperation(num3);
			}
			return DataParseStatus.Done;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00052834 File Offset: 0x00050A34
		private void CompletePayloadReadOperation(int bytesRead)
		{
			if (bytesRead == 0)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.ConnectionClosed), WebExceptionStatus.ConnectionClosed);
			}
			this.currentChunkBytesRead += bytesRead;
			int num = this.currentOperationBytesRead + bytesRead;
			if (this.currentChunkBytesRead == this.currentChunkLength)
			{
				this.readState = ChunkParser.ReadState.PayloadEnd;
			}
			this.currentOperationBytesRead = 0;
			this.CompleteUserRead(num);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00052898 File Offset: 0x00050A98
		private DataParseStatus ParseChunkLength()
		{
			int num = -1;
			int i = this.bufferCurrentPos;
			while (i < this.bufferFillLength)
			{
				byte b = this.buffer[i];
				if ((b < 48 || b > 57) && (b < 65 || b > 70) && (b < 97 || b > 102))
				{
					if (num == -1)
					{
						return DataParseStatus.Invalid;
					}
					this.bufferCurrentPos = i;
					this.currentChunkLength = num;
					this.readState = ChunkParser.ReadState.Extension;
					return DataParseStatus.ContinueParsing;
				}
				else
				{
					byte b2 = ((b < 65) ? (b - 48) : (10 + ((b < 97) ? (b - 65) : (b - 97))));
					if (num == -1)
					{
						num = (int)b2;
					}
					else
					{
						if (num >= 134217728)
						{
							return DataParseStatus.Invalid;
						}
						num = (num << 4) + (int)b2;
					}
					i++;
				}
			}
			return DataParseStatus.NeedMoreData;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0005293C File Offset: 0x00050B3C
		private DataParseStatus ParseExtension()
		{
			int num = this.bufferCurrentPos;
			DataParseStatus dataParseStatus = this.ParseWhitespaces(ref num);
			if (dataParseStatus != DataParseStatus.ContinueParsing)
			{
				return dataParseStatus;
			}
			dataParseStatus = this.ParseExtensionNameValuePairs(ref num);
			if (dataParseStatus != DataParseStatus.ContinueParsing)
			{
				return dataParseStatus;
			}
			dataParseStatus = this.ParseCRLF(ref num);
			if (dataParseStatus != DataParseStatus.ContinueParsing)
			{
				return dataParseStatus;
			}
			this.bufferCurrentPos = num;
			if (this.currentChunkLength == 0)
			{
				this.readState = ChunkParser.ReadState.Trailer;
			}
			else
			{
				this.readState = ChunkParser.ReadState.Payload;
			}
			return DataParseStatus.ContinueParsing;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000529A0 File Offset: 0x00050BA0
		private DataParseStatus ParsePayloadEnd()
		{
			DataParseStatus dataParseStatus = this.ParseCRLF(ref this.bufferCurrentPos);
			if (dataParseStatus != DataParseStatus.ContinueParsing)
			{
				return dataParseStatus;
			}
			this.currentChunkLength = -1;
			this.currentChunkBytesRead = 0;
			this.readState = ChunkParser.ReadState.ChunkLength;
			return DataParseStatus.ContinueParsing;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000529D8 File Offset: 0x00050BD8
		private DataParseStatus ParseTrailer()
		{
			if (this.ParseWhitespaces(ref this.bufferCurrentPos) == DataParseStatus.NeedMoreData)
			{
				return DataParseStatus.NeedMoreData;
			}
			int num = this.bufferCurrentPos;
			WebParseError webParseError;
			webParseError.Section = WebParseErrorSection.Generic;
			webParseError.Code = WebParseErrorCode.Generic;
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
			DataParseStatus dataParseStatus;
			if (SettingsSectionInternal.Section.UseUnsafeHeaderParsing)
			{
				dataParseStatus = webHeaderCollection.ParseHeaders(this.buffer, this.bufferFillLength, ref num, ref this.totalTrailerHeadersLength, this.maxBufferLength, ref webParseError);
			}
			else
			{
				dataParseStatus = webHeaderCollection.ParseHeadersStrict(this.buffer, this.bufferFillLength, ref num, ref this.totalTrailerHeadersLength, this.maxBufferLength, ref webParseError);
			}
			if (dataParseStatus == DataParseStatus.NeedMoreData || dataParseStatus == DataParseStatus.Done)
			{
				this.bufferCurrentPos = num;
			}
			if (dataParseStatus != DataParseStatus.Done)
			{
				return dataParseStatus;
			}
			this.readState = ChunkParser.ReadState.Done;
			this.CompleteUserRead(0);
			return DataParseStatus.Done;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00052A8E File Offset: 0x00050C8E
		private DataParseStatus ParseCRLF(ref int pos)
		{
			if (pos + 2 > this.bufferFillLength)
			{
				return DataParseStatus.NeedMoreData;
			}
			if (this.buffer[pos] != 13 || this.buffer[pos + 1] != 10)
			{
				return DataParseStatus.Invalid;
			}
			pos += 2;
			return DataParseStatus.ContinueParsing;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00052AC4 File Offset: 0x00050CC4
		private DataParseStatus ParseWhitespaces(ref int pos)
		{
			for (int i = pos; i < this.bufferFillLength; i++)
			{
				byte b = this.buffer[i];
				if (!ChunkParser.IsWhiteSpace(b))
				{
					pos = i;
					return DataParseStatus.ContinueParsing;
				}
			}
			return DataParseStatus.NeedMoreData;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00052AFA File Offset: 0x00050CFA
		private static bool IsWhiteSpace(byte c)
		{
			return c == 32 || c == 9;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00052B08 File Offset: 0x00050D08
		private DataParseStatus ParseExtensionNameValuePairs(ref int pos)
		{
			int num = pos;
			while (this.buffer[num] == 59)
			{
				num++;
				DataParseStatus dataParseStatus = this.ParseWhitespaces(ref num);
				if (dataParseStatus != DataParseStatus.ContinueParsing)
				{
					return dataParseStatus;
				}
				dataParseStatus = this.ParseToken(ref num);
				if (dataParseStatus != DataParseStatus.ContinueParsing)
				{
					return dataParseStatus;
				}
				dataParseStatus = this.ParseWhitespaces(ref num);
				if (dataParseStatus != DataParseStatus.ContinueParsing)
				{
					return dataParseStatus;
				}
				if (this.buffer[num] == 61)
				{
					num++;
					dataParseStatus = this.ParseWhitespaces(ref num);
					if (dataParseStatus != DataParseStatus.ContinueParsing)
					{
						return dataParseStatus;
					}
					dataParseStatus = this.ParseToken(ref num);
					if (dataParseStatus == DataParseStatus.Invalid)
					{
						dataParseStatus = this.ParseQuotedString(ref num);
					}
					if (dataParseStatus != DataParseStatus.ContinueParsing)
					{
						return dataParseStatus;
					}
					dataParseStatus = this.ParseWhitespaces(ref num);
					if (dataParseStatus != DataParseStatus.ContinueParsing)
					{
						return dataParseStatus;
					}
				}
			}
			pos = num;
			return DataParseStatus.ContinueParsing;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00052BA8 File Offset: 0x00050DA8
		private DataParseStatus ParseQuotedString(ref int pos)
		{
			if (pos == this.bufferFillLength)
			{
				return DataParseStatus.NeedMoreData;
			}
			if (this.buffer[pos] != 34)
			{
				return DataParseStatus.Invalid;
			}
			for (int i = pos + 1; i < this.bufferFillLength; i++)
			{
				if (this.buffer[i] == 34)
				{
					pos = i + 1;
					return DataParseStatus.ContinueParsing;
				}
				if (this.buffer[i] == 92)
				{
					i++;
					if (i == this.bufferFillLength)
					{
						return DataParseStatus.NeedMoreData;
					}
					if (this.buffer[i] <= 127)
					{
						i++;
						continue;
					}
				}
			}
			return DataParseStatus.NeedMoreData;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00052C28 File Offset: 0x00050E28
		private DataParseStatus ParseToken(ref int pos)
		{
			int i = pos;
			while (i < this.bufferFillLength)
			{
				if (!ChunkParser.IsTokenChar(this.buffer[i]))
				{
					if (i > pos)
					{
						pos = i;
						return DataParseStatus.ContinueParsing;
					}
					return DataParseStatus.Invalid;
				}
				else
				{
					i++;
				}
			}
			return DataParseStatus.NeedMoreData;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00052C63 File Offset: 0x00050E63
		private static bool IsTokenChar(byte character)
		{
			return character <= 127 && ChunkParser.tokenChars[(int)character];
		}

		// Token: 0x040012D1 RID: 4817
		private const int chunkLengthBuffer = 12;

		// Token: 0x040012D2 RID: 4818
		private const int noChunkLength = -1;

		// Token: 0x040012D3 RID: 4819
		private static readonly bool[] tokenChars = new bool[128];

		// Token: 0x040012D4 RID: 4820
		private byte[] buffer;

		// Token: 0x040012D5 RID: 4821
		private int bufferCurrentPos;

		// Token: 0x040012D6 RID: 4822
		private int bufferFillLength;

		// Token: 0x040012D7 RID: 4823
		private int maxBufferLength;

		// Token: 0x040012D8 RID: 4824
		private byte[] userBuffer;

		// Token: 0x040012D9 RID: 4825
		private int userBufferOffset;

		// Token: 0x040012DA RID: 4826
		private int userBufferCount;

		// Token: 0x040012DB RID: 4827
		private LazyAsyncResult userAsyncResult;

		// Token: 0x040012DC RID: 4828
		private Stream dataSource;

		// Token: 0x040012DD RID: 4829
		private ChunkParser.ReadState readState;

		// Token: 0x040012DE RID: 4830
		private int totalTrailerHeadersLength;

		// Token: 0x040012DF RID: 4831
		private int currentChunkLength;

		// Token: 0x040012E0 RID: 4832
		private int currentChunkBytesRead;

		// Token: 0x040012E1 RID: 4833
		private int currentOperationBytesRead;

		// Token: 0x040012E2 RID: 4834
		private int syncResult;

		// Token: 0x02000742 RID: 1858
		private enum ReadState
		{
			// Token: 0x040031B3 RID: 12723
			ChunkLength,
			// Token: 0x040031B4 RID: 12724
			Extension,
			// Token: 0x040031B5 RID: 12725
			Payload,
			// Token: 0x040031B6 RID: 12726
			PayloadEnd,
			// Token: 0x040031B7 RID: 12727
			Trailer,
			// Token: 0x040031B8 RID: 12728
			Done,
			// Token: 0x040031B9 RID: 12729
			Error
		}
	}
}
