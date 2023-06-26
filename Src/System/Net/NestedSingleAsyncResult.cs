using System;

namespace System.Net
{
	// Token: 0x020001CA RID: 458
	internal class NestedSingleAsyncResult : LazyAsyncResult
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x00060C66 File Offset: 0x0005EE66
		internal NestedSingleAsyncResult(object asyncObject, object asyncState, AsyncCallback asyncCallback, object result)
			: base(asyncObject, asyncState, asyncCallback, result)
		{
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00060C73 File Offset: 0x0005EE73
		internal NestedSingleAsyncResult(object asyncObject, object asyncState, AsyncCallback asyncCallback, byte[] buffer, int offset, int size)
			: base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x04001473 RID: 5235
		internal byte[] Buffer;

		// Token: 0x04001474 RID: 5236
		internal int Offset;

		// Token: 0x04001475 RID: 5237
		internal int Size;
	}
}
