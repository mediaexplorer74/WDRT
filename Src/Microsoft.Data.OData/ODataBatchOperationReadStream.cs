using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001D5 RID: 469
	internal abstract class ODataBatchOperationReadStream : ODataBatchOperationStream
	{
		// Token: 0x06000E97 RID: 3735 RVA: 0x0003364D File Offset: 0x0003184D
		internal ODataBatchOperationReadStream(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
			: base(listener)
		{
			this.batchReaderStream = batchReaderStream;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0003365D File Offset: 0x0003185D
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00033660 File Offset: 0x00031860
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00033663 File Offset: 0x00031863
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00033666 File Offset: 0x00031866
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0003366D File Offset: 0x0003186D
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x00033674 File Offset: 0x00031874
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

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003367B File Offset: 0x0003187B
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00033682 File Offset: 0x00031882
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00033689 File Offset: 0x00031889
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00033690 File Offset: 0x00031890
		internal static ODataBatchOperationReadStream Create(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener, int length)
		{
			return new ODataBatchOperationReadStream.ODataBatchOperationReadStreamWithLength(batchReaderStream, listener, length);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003369A File Offset: 0x0003189A
		internal static ODataBatchOperationReadStream Create(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
		{
			return new ODataBatchOperationReadStream.ODataBatchOperationReadStreamWithDelimiter(batchReaderStream, listener);
		}

		// Token: 0x04000502 RID: 1282
		protected ODataBatchReaderStream batchReaderStream;

		// Token: 0x020001D6 RID: 470
		private sealed class ODataBatchOperationReadStreamWithLength : ODataBatchOperationReadStream
		{
			// Token: 0x06000EA3 RID: 3747 RVA: 0x000336A3 File Offset: 0x000318A3
			internal ODataBatchOperationReadStreamWithLength(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener, int length)
				: base(batchReaderStream, listener)
			{
				ExceptionUtils.CheckIntegerNotNegative(length, "length");
				this.length = length;
			}

			// Token: 0x06000EA4 RID: 3748 RVA: 0x000336C0 File Offset: 0x000318C0
			public override int Read(byte[] buffer, int offset, int count)
			{
				ExceptionUtils.CheckArgumentNotNull<byte[]>(buffer, "buffer");
				ExceptionUtils.CheckIntegerNotNegative(offset, "offset");
				ExceptionUtils.CheckIntegerNotNegative(count, "count");
				base.ValidateNotDisposed();
				if (this.length == 0)
				{
					return 0;
				}
				int num = this.batchReaderStream.ReadWithLength(buffer, offset, Math.Min(count, this.length));
				this.length -= num;
				return num;
			}

			// Token: 0x04000503 RID: 1283
			private int length;
		}

		// Token: 0x020001D7 RID: 471
		private sealed class ODataBatchOperationReadStreamWithDelimiter : ODataBatchOperationReadStream
		{
			// Token: 0x06000EA5 RID: 3749 RVA: 0x00033727 File Offset: 0x00031927
			internal ODataBatchOperationReadStreamWithDelimiter(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
				: base(batchReaderStream, listener)
			{
			}

			// Token: 0x06000EA6 RID: 3750 RVA: 0x00033734 File Offset: 0x00031934
			public override int Read(byte[] buffer, int offset, int count)
			{
				ExceptionUtils.CheckArgumentNotNull<byte[]>(buffer, "buffer");
				ExceptionUtils.CheckIntegerNotNegative(offset, "offset");
				ExceptionUtils.CheckIntegerNotNegative(count, "count");
				base.ValidateNotDisposed();
				if (this.exhausted)
				{
					return 0;
				}
				int num = this.batchReaderStream.ReadWithDelimiter(buffer, offset, count);
				if (num < count)
				{
					this.exhausted = true;
				}
				return num;
			}

			// Token: 0x04000504 RID: 1284
			private bool exhausted;
		}
	}
}
