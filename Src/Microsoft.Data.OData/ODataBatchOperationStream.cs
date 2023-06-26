using System;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001D3 RID: 467
	internal abstract class ODataBatchOperationStream : Stream
	{
		// Token: 0x06000E85 RID: 3717 RVA: 0x00033530 File Offset: 0x00031730
		internal ODataBatchOperationStream(IODataBatchOperationListener listener)
		{
			this.listener = listener;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0003353F File Offset: 0x0003173F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00033546 File Offset: 0x00031746
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listener != null)
			{
				this.listener.BatchOperationContentStreamDisposed();
				this.listener = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003356C File Offset: 0x0003176C
		protected void ValidateNotDisposed()
		{
			if (this.listener == null)
			{
				throw new ObjectDisposedException(null, Strings.ODataBatchOperationStream_Disposed);
			}
		}

		// Token: 0x04000500 RID: 1280
		private IODataBatchOperationListener listener;
	}
}
