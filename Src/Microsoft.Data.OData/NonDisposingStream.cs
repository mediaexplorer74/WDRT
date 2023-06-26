using System;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001CA RID: 458
	internal sealed class NonDisposingStream : Stream
	{
		// Token: 0x06000E39 RID: 3641 RVA: 0x0003257C File Offset: 0x0003077C
		internal NonDisposingStream(Stream innerStream)
		{
			this.innerStream = innerStream;
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0003258B File Offset: 0x0003078B
		public override bool CanRead
		{
			get
			{
				return this.innerStream.CanRead;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x00032598 File Offset: 0x00030798
		public override bool CanSeek
		{
			get
			{
				return this.innerStream.CanSeek;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x000325A5 File Offset: 0x000307A5
		public override bool CanWrite
		{
			get
			{
				return this.innerStream.CanWrite;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x000325B2 File Offset: 0x000307B2
		public override long Length
		{
			get
			{
				return this.innerStream.Length;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x000325BF File Offset: 0x000307BF
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x000325CC File Offset: 0x000307CC
		public override long Position
		{
			get
			{
				return this.innerStream.Position;
			}
			set
			{
				this.innerStream.Position = value;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x000325DA File Offset: 0x000307DA
		public override void Flush()
		{
			this.innerStream.Flush();
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000325E7 File Offset: 0x000307E7
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.innerStream.Read(buffer, offset, count);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000325F7 File Offset: 0x000307F7
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0003260B File Offset: 0x0003080B
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.innerStream.EndRead(asyncResult);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00032619 File Offset: 0x00030819
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.innerStream.Seek(offset, origin);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00032628 File Offset: 0x00030828
		public override void SetLength(long value)
		{
			this.innerStream.SetLength(value);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00032636 File Offset: 0x00030836
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.innerStream.Write(buffer, offset, count);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00032646 File Offset: 0x00030846
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.innerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003265A File Offset: 0x0003085A
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.innerStream.EndWrite(asyncResult);
		}

		// Token: 0x040004B2 RID: 1202
		private readonly Stream innerStream;
	}
}
