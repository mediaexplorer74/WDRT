using System;

namespace System.Net
{
	// Token: 0x020001C9 RID: 457
	internal class NestedMultipleAsyncResult : LazyAsyncResult
	{
		// Token: 0x0600121E RID: 4638 RVA: 0x00060C14 File Offset: 0x0005EE14
		internal NestedMultipleAsyncResult(object asyncObject, object asyncState, AsyncCallback asyncCallback, BufferOffsetSize[] buffers)
			: base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffers = buffers;
			this.Size = 0;
			for (int i = 0; i < this.Buffers.Length; i++)
			{
				this.Size += this.Buffers[i].Size;
			}
		}

		// Token: 0x04001471 RID: 5233
		internal BufferOffsetSize[] Buffers;

		// Token: 0x04001472 RID: 5234
		internal int Size;
	}
}
