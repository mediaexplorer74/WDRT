using System;
using System.IO;

namespace System.Security.Cryptography
{
	// Token: 0x0200026F RID: 623
	internal sealed class TailStream : Stream
	{
		// Token: 0x0600220E RID: 8718 RVA: 0x00078703 File Offset: 0x00076903
		public TailStream(int bufferSize)
		{
			this._Buffer = new byte[bufferSize];
			this._BufferSize = bufferSize;
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0007871E File Offset: 0x0007691E
		public void Clear()
		{
			this.Close();
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00078728 File Offset: 0x00076928
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._Buffer != null)
					{
						Array.Clear(this._Buffer, 0, this._Buffer.Length);
					}
					this._Buffer = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x00078778 File Offset: 0x00076978
		public byte[] Buffer
		{
			get
			{
				return (byte[])this._Buffer.Clone();
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x0007878A File Offset: 0x0007698A
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x0007878D File Offset: 0x0007698D
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x00078790 File Offset: 0x00076990
		public override bool CanWrite
		{
			get
			{
				return this._Buffer != null;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x0007879B File Offset: 0x0007699B
		public override long Length
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x000787AC File Offset: 0x000769AC
		// (set) Token: 0x06002217 RID: 8727 RVA: 0x000787BD File Offset: 0x000769BD
		public override long Position
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
			set
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000787CE File Offset: 0x000769CE
		public override void Flush()
		{
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000787D0 File Offset: 0x000769D0
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000787E1 File Offset: 0x000769E1
		public override void SetLength(long value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000787F2 File Offset: 0x000769F2
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00078804 File Offset: 0x00076A04
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._Buffer == null)
			{
				throw new ObjectDisposedException("TailStream");
			}
			if (count == 0)
			{
				return;
			}
			if (this._BufferFull)
			{
				if (count > this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					return;
				}
				System.Buffer.InternalBlockCopy(this._Buffer, this._BufferSize - count, this._Buffer, 0, this._BufferSize - count);
				System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferSize - count, count);
				return;
			}
			else
			{
				if (count > this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					this._BufferFull = true;
					return;
				}
				if (count + this._BufferIndex >= this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(this._Buffer, this._BufferIndex + count - this._BufferSize, this._Buffer, 0, this._BufferSize - count);
					System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
					this._BufferFull = true;
					return;
				}
				System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
				this._BufferIndex += count;
				return;
			}
		}

		// Token: 0x04000C60 RID: 3168
		private byte[] _Buffer;

		// Token: 0x04000C61 RID: 3169
		private int _BufferSize;

		// Token: 0x04000C62 RID: 3170
		private int _BufferIndex;

		// Token: 0x04000C63 RID: 3171
		private bool _BufferFull;
	}
}
