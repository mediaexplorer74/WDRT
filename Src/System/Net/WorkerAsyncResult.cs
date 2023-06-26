using System;

namespace System.Net
{
	// Token: 0x0200021A RID: 538
	internal class WorkerAsyncResult : LazyAsyncResult
	{
		// Token: 0x060013C8 RID: 5064 RVA: 0x00068F99 File Offset: 0x00067199
		public WorkerAsyncResult(object asyncObject, object asyncState, AsyncCallback savedAsyncCallback, byte[] buffer, int offset, int end)
			: base(asyncObject, asyncState, savedAsyncCallback)
		{
			this.Buffer = buffer;
			this.Offset = offset;
			this.End = end;
		}

		// Token: 0x040015C5 RID: 5573
		public byte[] Buffer;

		// Token: 0x040015C6 RID: 5574
		public int Offset;

		// Token: 0x040015C7 RID: 5575
		public int End;

		// Token: 0x040015C8 RID: 5576
		public bool IsWrite;

		// Token: 0x040015C9 RID: 5577
		public WorkerAsyncResult ParentResult;

		// Token: 0x040015CA RID: 5578
		public bool HeaderDone;

		// Token: 0x040015CB RID: 5579
		public bool HandshakeDone;
	}
}
