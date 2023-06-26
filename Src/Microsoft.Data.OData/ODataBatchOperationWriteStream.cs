using System;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001D4 RID: 468
	internal sealed class ODataBatchOperationWriteStream : ODataBatchOperationStream
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x00033582 File Offset: 0x00031782
		internal ODataBatchOperationWriteStream(Stream batchStream, IODataBatchOperationListener listener)
			: base(listener)
		{
			this.batchStream = batchStream;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00033592 File Offset: 0x00031792
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00033595 File Offset: 0x00031795
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00033598 File Offset: 0x00031798
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0003359B File Offset: 0x0003179B
		public override long Length
		{
			get
			{
				base.ValidateNotDisposed();
				return this.batchStream.Length;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x000335AE File Offset: 0x000317AE
		// (set) Token: 0x06000E8F RID: 3727 RVA: 0x000335C1 File Offset: 0x000317C1
		public override long Position
		{
			get
			{
				base.ValidateNotDisposed();
				return this.batchStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x000335C8 File Offset: 0x000317C8
		public override void SetLength(long value)
		{
			base.ValidateNotDisposed();
			this.batchStream.SetLength(value);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000335DC File Offset: 0x000317DC
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.ValidateNotDisposed();
			this.batchStream.Write(buffer, offset, count);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000335F2 File Offset: 0x000317F2
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			base.ValidateNotDisposed();
			return this.batchStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003360C File Offset: 0x0003180C
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.ValidateNotDisposed();
			this.batchStream.EndWrite(asyncResult);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00033620 File Offset: 0x00031820
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00033627 File Offset: 0x00031827
		public override void Flush()
		{
			base.ValidateNotDisposed();
			this.batchStream.Flush();
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003363A File Offset: 0x0003183A
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.batchStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000501 RID: 1281
		private Stream batchStream;
	}
}
