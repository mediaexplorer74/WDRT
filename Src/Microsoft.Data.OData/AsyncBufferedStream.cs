using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000287 RID: 647
	internal sealed class AsyncBufferedStream : Stream
	{
		// Token: 0x06001583 RID: 5507 RVA: 0x0004ECA0 File Offset: 0x0004CEA0
		internal AsyncBufferedStream(Stream stream)
		{
			this.innerStream = stream;
			this.bufferQueue = new Queue<AsyncBufferedStream.DataBuffer>();
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0004ECBA File Offset: 0x0004CEBA
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0004ECBD File Offset: 0x0004CEBD
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x0004ECC0 File Offset: 0x0004CEC0
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0004ECC3 File Offset: 0x0004CEC3
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0004ECCA File Offset: 0x0004CECA
		// (set) Token: 0x06001589 RID: 5513 RVA: 0x0004ECD1 File Offset: 0x0004CED1
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

		// Token: 0x0600158A RID: 5514 RVA: 0x0004ECD8 File Offset: 0x0004CED8
		public override void Flush()
		{
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0004ECDA File Offset: 0x0004CEDA
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0004ECE1 File Offset: 0x0004CEE1
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0004ECE8 File Offset: 0x0004CEE8
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0004ECF0 File Offset: 0x0004CEF0
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				if (this.bufferToAppendTo == null)
				{
					this.QueueNewBuffer();
				}
				while (count > 0)
				{
					int num = this.bufferToAppendTo.Write(buffer, offset, count);
					if (num < count)
					{
						this.QueueNewBuffer();
					}
					count -= num;
					offset += num;
				}
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0004ED38 File Offset: 0x0004CF38
		internal void Clear()
		{
			this.bufferQueue.Clear();
			this.bufferToAppendTo = null;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0004ED4C File Offset: 0x0004CF4C
		internal void FlushSync()
		{
			Queue<AsyncBufferedStream.DataBuffer> queue = this.PrepareFlushBuffers();
			if (queue == null)
			{
				return;
			}
			while (queue.Count > 0)
			{
				AsyncBufferedStream.DataBuffer dataBuffer = queue.Dequeue();
				dataBuffer.WriteToStream(this.innerStream);
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0004ED80 File Offset: 0x0004CF80
		internal new Task FlushAsync()
		{
			return this.FlushAsyncInternal();
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0004ED88 File Offset: 0x0004CF88
		internal Task FlushAsyncInternal()
		{
			Queue<AsyncBufferedStream.DataBuffer> queue = this.PrepareFlushBuffers();
			if (queue == null)
			{
				return TaskUtils.CompletedTask;
			}
			return Task.Factory.Iterate(this.FlushBuffersAsync(queue));
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0004EDB6 File Offset: 0x0004CFB6
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.bufferQueue.Count > 0)
			{
				throw new ODataException(Strings.AsyncBufferedStream_WriterDisposedWithoutFlush);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0004EDDB File Offset: 0x0004CFDB
		private void QueueNewBuffer()
		{
			this.bufferToAppendTo = new AsyncBufferedStream.DataBuffer();
			this.bufferQueue.Enqueue(this.bufferToAppendTo);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0004EDFC File Offset: 0x0004CFFC
		private Queue<AsyncBufferedStream.DataBuffer> PrepareFlushBuffers()
		{
			if (this.bufferQueue.Count == 0)
			{
				return null;
			}
			this.bufferToAppendTo = null;
			Queue<AsyncBufferedStream.DataBuffer> queue = this.bufferQueue;
			this.bufferQueue = new Queue<AsyncBufferedStream.DataBuffer>();
			return queue;
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0004EF44 File Offset: 0x0004D144
		private IEnumerable<Task> FlushBuffersAsync(Queue<AsyncBufferedStream.DataBuffer> buffers)
		{
			while (buffers.Count > 0)
			{
				AsyncBufferedStream.DataBuffer buffer = buffers.Dequeue();
				yield return buffer.WriteToStreamAsync(this.innerStream);
			}
			yield break;
		}

		// Token: 0x040007D4 RID: 2004
		private readonly Stream innerStream;

		// Token: 0x040007D5 RID: 2005
		private Queue<AsyncBufferedStream.DataBuffer> bufferQueue;

		// Token: 0x040007D6 RID: 2006
		private AsyncBufferedStream.DataBuffer bufferToAppendTo;

		// Token: 0x02000288 RID: 648
		private sealed class DataBuffer
		{
			// Token: 0x06001597 RID: 5527 RVA: 0x0004EF68 File Offset: 0x0004D168
			public DataBuffer()
			{
				this.buffer = new byte[80896];
				this.storedCount = 0;
			}

			// Token: 0x06001598 RID: 5528 RVA: 0x0004EF88 File Offset: 0x0004D188
			public int Write(byte[] data, int index, int count)
			{
				int num = count;
				if (num > this.buffer.Length - this.storedCount)
				{
					num = this.buffer.Length - this.storedCount;
				}
				if (num > 0)
				{
					Array.Copy(data, index, this.buffer, this.storedCount, num);
					this.storedCount += num;
				}
				return num;
			}

			// Token: 0x06001599 RID: 5529 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
			public void WriteToStream(Stream stream)
			{
				stream.Write(this.buffer, 0, this.storedCount);
			}

			// Token: 0x0600159A RID: 5530 RVA: 0x0004F024 File Offset: 0x0004D224
			public Task WriteToStreamAsync(Stream stream)
			{
				return Task.Factory.FromAsync((AsyncCallback callback, object state) => stream.BeginWrite(this.buffer, 0, this.storedCount, callback, state), new Action<IAsyncResult>(stream.EndWrite), null);
			}

			// Token: 0x040007D7 RID: 2007
			private const int BufferSize = 80896;

			// Token: 0x040007D8 RID: 2008
			private readonly byte[] buffer;

			// Token: 0x040007D9 RID: 2009
			private int storedCount;
		}
	}
}
