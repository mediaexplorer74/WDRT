using System;

namespace System.Net
{
	// Token: 0x02000220 RID: 544
	internal class BufferAsyncResult : LazyAsyncResult
	{
		// Token: 0x06001400 RID: 5120 RVA: 0x0006A5E3 File Offset: 0x000687E3
		public BufferAsyncResult(object asyncObject, BufferOffsetSize[] buffers, object asyncState, AsyncCallback asyncCallback)
			: base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffers = buffers;
			this.IsWrite = true;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0006A5FD File Offset: 0x000687FD
		public BufferAsyncResult(object asyncObject, byte[] buffer, int offset, int count, object asyncState, AsyncCallback asyncCallback)
			: this(asyncObject, buffer, offset, count, false, asyncState, asyncCallback)
		{
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0006A60F File Offset: 0x0006880F
		public BufferAsyncResult(object asyncObject, byte[] buffer, int offset, int count, bool isWrite, object asyncState, AsyncCallback asyncCallback)
			: base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffer = buffer;
			this.Offset = offset;
			this.Count = count;
			this.IsWrite = isWrite;
		}

		// Token: 0x040015F6 RID: 5622
		public byte[] Buffer;

		// Token: 0x040015F7 RID: 5623
		public BufferOffsetSize[] Buffers;

		// Token: 0x040015F8 RID: 5624
		public int Offset;

		// Token: 0x040015F9 RID: 5625
		public int Count;

		// Token: 0x040015FA RID: 5626
		public bool IsWrite;
	}
}
