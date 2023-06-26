using System;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001B6 RID: 438
	internal static class MessageStreamWrapper
	{
		// Token: 0x06000D96 RID: 3478 RVA: 0x0002F6C4 File Offset: 0x0002D8C4
		internal static Stream CreateNonDisposingStream(Stream innerStream)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, true, -1L);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0002F6CF File Offset: 0x0002D8CF
		internal static Stream CreateStreamWithMaxSize(Stream innerStream, long maxBytesToBeRead)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, false, maxBytesToBeRead);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0002F6D9 File Offset: 0x0002D8D9
		internal static Stream CreateNonDisposingStreamWithMaxSize(Stream innerStream, long maxBytesToBeRead)
		{
			return new MessageStreamWrapper.MessageStreamWrappingStream(innerStream, true, maxBytesToBeRead);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0002F6E4 File Offset: 0x0002D8E4
		internal static bool IsNonDisposingStream(Stream stream)
		{
			MessageStreamWrapper.MessageStreamWrappingStream messageStreamWrappingStream = stream as MessageStreamWrapper.MessageStreamWrappingStream;
			return messageStreamWrappingStream != null && messageStreamWrappingStream.IgnoreDispose;
		}

		// Token: 0x020001B7 RID: 439
		private sealed class MessageStreamWrappingStream : Stream
		{
			// Token: 0x06000D9A RID: 3482 RVA: 0x0002F703 File Offset: 0x0002D903
			internal MessageStreamWrappingStream(Stream innerStream, bool ignoreDispose, long maxBytesToBeRead)
			{
				this.innerStream = innerStream;
				this.ignoreDispose = ignoreDispose;
				this.maxBytesToBeRead = maxBytesToBeRead;
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0002F720 File Offset: 0x0002D920
			public override bool CanRead
			{
				get
				{
					return this.innerStream.CanRead;
				}
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0002F72D File Offset: 0x0002D92D
			public override bool CanSeek
			{
				get
				{
					return this.innerStream.CanSeek;
				}
			}

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0002F73A File Offset: 0x0002D93A
			public override bool CanWrite
			{
				get
				{
					return this.innerStream.CanWrite;
				}
			}

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0002F747 File Offset: 0x0002D947
			public override long Length
			{
				get
				{
					return this.innerStream.Length;
				}
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0002F754 File Offset: 0x0002D954
			// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x0002F761 File Offset: 0x0002D961
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

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0002F76F File Offset: 0x0002D96F
			internal bool IgnoreDispose
			{
				get
				{
					return this.ignoreDispose;
				}
			}

			// Token: 0x06000DA2 RID: 3490 RVA: 0x0002F777 File Offset: 0x0002D977
			public override void Flush()
			{
				this.innerStream.Flush();
			}

			// Token: 0x06000DA3 RID: 3491 RVA: 0x0002F784 File Offset: 0x0002D984
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = this.innerStream.Read(buffer, offset, count);
				this.IncreaseTotalBytesRead(num);
				return num;
			}

			// Token: 0x06000DA4 RID: 3492 RVA: 0x0002F7A8 File Offset: 0x0002D9A8
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				return this.innerStream.BeginRead(buffer, offset, count, callback, state);
			}

			// Token: 0x06000DA5 RID: 3493 RVA: 0x0002F7BC File Offset: 0x0002D9BC
			public override int EndRead(IAsyncResult asyncResult)
			{
				int num = this.innerStream.EndRead(asyncResult);
				this.IncreaseTotalBytesRead(num);
				return num;
			}

			// Token: 0x06000DA6 RID: 3494 RVA: 0x0002F7DE File Offset: 0x0002D9DE
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.innerStream.Seek(offset, origin);
			}

			// Token: 0x06000DA7 RID: 3495 RVA: 0x0002F7ED File Offset: 0x0002D9ED
			public override void SetLength(long value)
			{
				this.innerStream.SetLength(value);
			}

			// Token: 0x06000DA8 RID: 3496 RVA: 0x0002F7FB File Offset: 0x0002D9FB
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.innerStream.Write(buffer, offset, count);
			}

			// Token: 0x06000DA9 RID: 3497 RVA: 0x0002F80B File Offset: 0x0002DA0B
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				return this.innerStream.BeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06000DAA RID: 3498 RVA: 0x0002F81F File Offset: 0x0002DA1F
			public override void EndWrite(IAsyncResult asyncResult)
			{
				this.innerStream.EndWrite(asyncResult);
			}

			// Token: 0x06000DAB RID: 3499 RVA: 0x0002F82D File Offset: 0x0002DA2D
			protected override void Dispose(bool disposing)
			{
				if (disposing && !this.ignoreDispose && this.innerStream != null)
				{
					this.innerStream.Dispose();
					this.innerStream = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x06000DAC RID: 3500 RVA: 0x0002F85C File Offset: 0x0002DA5C
			private void IncreaseTotalBytesRead(int bytesRead)
			{
				if (this.maxBytesToBeRead <= 0L)
				{
					return;
				}
				this.totalBytesRead += (long)((bytesRead < 0) ? 0 : bytesRead);
				if (this.totalBytesRead > this.maxBytesToBeRead)
				{
					throw new ODataException(Strings.MessageStreamWrappingStream_ByteLimitExceeded(this.totalBytesRead, this.maxBytesToBeRead));
				}
			}

			// Token: 0x04000490 RID: 1168
			private readonly long maxBytesToBeRead;

			// Token: 0x04000491 RID: 1169
			private readonly bool ignoreDispose;

			// Token: 0x04000492 RID: 1170
			private Stream innerStream;

			// Token: 0x04000493 RID: 1171
			private long totalBytesRead;
		}
	}
}
