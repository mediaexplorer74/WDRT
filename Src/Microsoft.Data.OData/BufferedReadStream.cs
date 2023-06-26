using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000253 RID: 595
	internal sealed class BufferedReadStream : Stream
	{
		// Token: 0x0600138D RID: 5005 RVA: 0x00049B9C File Offset: 0x00047D9C
		private BufferedReadStream(Stream inputStream)
		{
			this.buffers = new List<BufferedReadStream.DataBuffer>();
			this.inputStream = inputStream;
			this.currentBufferIndex = -1;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00049BBD File Offset: 0x00047DBD
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x00049BC0 File Offset: 0x00047DC0
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00049BC3 File Offset: 0x00047DC3
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x00049BC6 File Offset: 0x00047DC6
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00049BCD File Offset: 0x00047DCD
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x00049BD4 File Offset: 0x00047DD4
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00049BDB File Offset: 0x00047DDB
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00049BE4 File Offset: 0x00047DE4
		public override int Read(byte[] buffer, int offset, int count)
		{
			ExceptionUtils.CheckArgumentNotNull<byte[]>(buffer, "buffer");
			if (this.currentBufferIndex == -1)
			{
				return 0;
			}
			BufferedReadStream.DataBuffer dataBuffer = this.buffers[this.currentBufferIndex];
			while (this.currentBufferReadCount >= dataBuffer.StoredCount)
			{
				this.currentBufferIndex++;
				if (this.currentBufferIndex >= this.buffers.Count)
				{
					this.currentBufferIndex = -1;
					return 0;
				}
				dataBuffer = this.buffers[this.currentBufferIndex];
				this.currentBufferReadCount = 0;
			}
			int num = count;
			if (count > dataBuffer.StoredCount - this.currentBufferReadCount)
			{
				num = dataBuffer.StoredCount - this.currentBufferReadCount;
			}
			Array.Copy(dataBuffer.Buffer, this.currentBufferReadCount, buffer, offset, num);
			this.currentBufferReadCount += num;
			return num;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00049CAF File Offset: 0x00047EAF
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00049CB6 File Offset: 0x00047EB6
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00049CBD File Offset: 0x00047EBD
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00049CEC File Offset: 0x00047EEC
		internal static Task<BufferedReadStream> BufferStreamAsync(Stream inputStream)
		{
			BufferedReadStream bufferedReadStream = new BufferedReadStream(inputStream);
			return Task.Factory.Iterate(bufferedReadStream.BufferInputStream()).FollowAlwaysWith(delegate(Task task)
			{
				inputStream.Dispose();
			}).FollowOnSuccessWith(delegate(Task task)
			{
				bufferedReadStream.ResetForReading();
				return bufferedReadStream;
			});
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00049D4E File Offset: 0x00047F4E
		internal void ResetForReading()
		{
			this.currentBufferIndex = ((this.buffers.Count == 0) ? (-1) : 0);
			this.currentBufferReadCount = 0;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00049D6E File Offset: 0x00047F6E
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00049FD4 File Offset: 0x000481D4
		private IEnumerable<Task> BufferInputStream()
		{
			while (this.inputStream != null)
			{
				BufferedReadStream.DataBuffer currentBuffer = ((this.currentBufferIndex == -1) ? null : this.buffers[this.currentBufferIndex]);
				if (currentBuffer != null && currentBuffer.FreeBytes < 1024)
				{
					currentBuffer = null;
				}
				if (currentBuffer == null)
				{
					currentBuffer = this.AddNewBuffer();
				}
				yield return Task.Factory.FromAsync((AsyncCallback asyncCallback, object asyncState) => this.inputStream.BeginRead(currentBuffer.Buffer, currentBuffer.OffsetToWriteTo, currentBuffer.FreeBytes, asyncCallback, asyncState), delegate(IAsyncResult asyncResult)
				{
					try
					{
						int num = this.inputStream.EndRead(asyncResult);
						if (num == 0)
						{
							this.inputStream = null;
						}
						else
						{
							currentBuffer.MarkBytesAsWritten(num);
						}
					}
					catch (Exception ex)
					{
						if (!ExceptionUtils.IsCatchableExceptionType(ex))
						{
							throw;
						}
						this.inputStream = null;
						throw;
					}
				}, null);
			}
			yield break;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00049FF4 File Offset: 0x000481F4
		private BufferedReadStream.DataBuffer AddNewBuffer()
		{
			BufferedReadStream.DataBuffer dataBuffer = new BufferedReadStream.DataBuffer();
			this.buffers.Add(dataBuffer);
			this.currentBufferIndex = this.buffers.Count - 1;
			return dataBuffer;
		}

		// Token: 0x040006FE RID: 1790
		private readonly List<BufferedReadStream.DataBuffer> buffers;

		// Token: 0x040006FF RID: 1791
		private Stream inputStream;

		// Token: 0x04000700 RID: 1792
		private int currentBufferIndex;

		// Token: 0x04000701 RID: 1793
		private int currentBufferReadCount;

		// Token: 0x02000254 RID: 596
		private sealed class DataBuffer
		{
			// Token: 0x0600139E RID: 5022 RVA: 0x0004A027 File Offset: 0x00048227
			public DataBuffer()
			{
				this.buffer = new byte[65536];
				this.StoredCount = 0;
			}

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x0600139F RID: 5023 RVA: 0x0004A046 File Offset: 0x00048246
			public byte[] Buffer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0004A04E File Offset: 0x0004824E
			public int OffsetToWriteTo
			{
				get
				{
					return this.StoredCount;
				}
			}

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0004A056 File Offset: 0x00048256
			// (set) Token: 0x060013A2 RID: 5026 RVA: 0x0004A05E File Offset: 0x0004825E
			public int StoredCount { get; private set; }

			// Token: 0x17000402 RID: 1026
			// (get) Token: 0x060013A3 RID: 5027 RVA: 0x0004A067 File Offset: 0x00048267
			public int FreeBytes
			{
				get
				{
					return this.buffer.Length - this.StoredCount;
				}
			}

			// Token: 0x060013A4 RID: 5028 RVA: 0x0004A078 File Offset: 0x00048278
			public void MarkBytesAsWritten(int count)
			{
				this.StoredCount += count;
			}

			// Token: 0x04000702 RID: 1794
			internal const int MinReadBufferSize = 1024;

			// Token: 0x04000703 RID: 1795
			private const int BufferSize = 65536;

			// Token: 0x04000704 RID: 1796
			private readonly byte[] buffer;
		}
	}
}
