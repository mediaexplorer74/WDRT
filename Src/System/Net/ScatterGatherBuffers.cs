using System;

namespace System.Net
{
	// Token: 0x02000208 RID: 520
	internal class ScatterGatherBuffers
	{
		// Token: 0x06001365 RID: 4965 RVA: 0x0006606F File Offset: 0x0006426F
		internal ScatterGatherBuffers()
		{
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00066082 File Offset: 0x00064282
		internal ScatterGatherBuffers(long totalSize)
		{
			if (totalSize > 0L)
			{
				this.currentChunk = this.AllocateMemoryChunk((totalSize > 2147483647L) ? int.MaxValue : ((int)totalSize));
			}
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x000660B8 File Offset: 0x000642B8
		internal BufferOffsetSize[] GetBuffers()
		{
			if (this.Empty)
			{
				return null;
			}
			BufferOffsetSize[] array = new BufferOffsetSize[this.chunkCount];
			int num = 0;
			for (ScatterGatherBuffers.MemoryChunk next = this.headChunk; next != null; next = next.Next)
			{
				array[num] = new BufferOffsetSize(next.Buffer, 0, next.FreeOffset, false);
				num++;
			}
			return array;
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0006610B File Offset: 0x0006430B
		private bool Empty
		{
			get
			{
				return this.headChunk == null || this.chunkCount == 0;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x00066120 File Offset: 0x00064320
		internal int Length
		{
			get
			{
				return this.totalLength;
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00066128 File Offset: 0x00064328
		internal void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				int num = (this.Empty ? 0 : (this.currentChunk.Buffer.Length - this.currentChunk.FreeOffset));
				if (num == 0)
				{
					ScatterGatherBuffers.MemoryChunk memoryChunk = this.AllocateMemoryChunk(count);
					if (this.currentChunk != null)
					{
						this.currentChunk.Next = memoryChunk;
					}
					this.currentChunk = memoryChunk;
				}
				int num2 = ((count < num) ? count : num);
				Buffer.BlockCopy(buffer, offset, this.currentChunk.Buffer, this.currentChunk.FreeOffset, num2);
				offset += num2;
				count -= num2;
				this.totalLength += num2;
				this.currentChunk.FreeOffset += num2;
			}
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000661E0 File Offset: 0x000643E0
		private ScatterGatherBuffers.MemoryChunk AllocateMemoryChunk(int newSize)
		{
			if (newSize > this.nextChunkLength)
			{
				this.nextChunkLength = newSize;
			}
			ScatterGatherBuffers.MemoryChunk memoryChunk = new ScatterGatherBuffers.MemoryChunk(this.nextChunkLength);
			if (this.Empty)
			{
				this.headChunk = memoryChunk;
			}
			this.nextChunkLength *= 2;
			this.chunkCount++;
			return memoryChunk;
		}

		// Token: 0x0400154B RID: 5451
		private ScatterGatherBuffers.MemoryChunk headChunk;

		// Token: 0x0400154C RID: 5452
		private ScatterGatherBuffers.MemoryChunk currentChunk;

		// Token: 0x0400154D RID: 5453
		private int nextChunkLength = 1024;

		// Token: 0x0400154E RID: 5454
		private int totalLength;

		// Token: 0x0400154F RID: 5455
		private int chunkCount;

		// Token: 0x02000757 RID: 1879
		private class MemoryChunk
		{
			// Token: 0x060041EB RID: 16875 RVA: 0x00111A67 File Offset: 0x0010FC67
			internal MemoryChunk(int bufferSize)
			{
				this.Buffer = new byte[bufferSize];
			}

			// Token: 0x04003200 RID: 12800
			internal byte[] Buffer;

			// Token: 0x04003201 RID: 12801
			internal int FreeOffset;

			// Token: 0x04003202 RID: 12802
			internal ScatterGatherBuffers.MemoryChunk Next;
		}
	}
}
