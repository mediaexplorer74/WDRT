using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000225 RID: 549
	internal class BufferedReadStream : DelegatedStream
	{
		// Token: 0x06001428 RID: 5160 RVA: 0x0006B08E File Offset: 0x0006928E
		internal BufferedReadStream(Stream stream)
			: this(stream, false)
		{
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0006B098 File Offset: 0x00069298
		internal BufferedReadStream(Stream stream, bool readMore)
			: base(stream)
		{
			this.readMore = readMore;
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0006B0A8 File Offset: 0x000692A8
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0006B0AB File Offset: 0x000692AB
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0006B0B0 File Offset: 0x000692B0
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			BufferedReadStream.ReadAsyncResult readAsyncResult = new BufferedReadStream.ReadAsyncResult(this, callback, state);
			readAsyncResult.Read(buffer, offset, count);
			return readAsyncResult;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0006B0D4 File Offset: 0x000692D4
		public override int EndRead(IAsyncResult asyncResult)
		{
			return BufferedReadStream.ReadAsyncResult.End(asyncResult);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0006B0EC File Offset: 0x000692EC
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (this.storedOffset < this.storedLength)
			{
				num = Math.Min(count, this.storedLength - this.storedOffset);
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, buffer, offset, num);
				this.storedOffset += num;
				if (num == count || !this.readMore)
				{
					return num;
				}
				offset += num;
				count -= num;
			}
			return num + base.Read(buffer, offset, count);
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0006B164 File Offset: 0x00069364
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (this.storedOffset >= this.storedLength)
			{
				return base.ReadAsync(buffer, offset, count, cancellationToken);
			}
			int num = Math.Min(count, this.storedLength - this.storedOffset);
			Buffer.BlockCopy(this.storedBuffer, this.storedOffset, buffer, offset, num);
			this.storedOffset += num;
			if (num == count || !this.readMore)
			{
				return Task.FromResult<int>(num);
			}
			offset += num;
			count -= num;
			return this.ReadMoreAsync(num, buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0006B1EC File Offset: 0x000693EC
		private async Task<int> ReadMoreAsync(int bytesAlreadyRead, byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			int num = await base.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
			int num2 = num;
			return bytesAlreadyRead + num2;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0006B25C File Offset: 0x0006945C
		public override int ReadByte()
		{
			if (this.storedOffset < this.storedLength)
			{
				byte[] array = this.storedBuffer;
				int num = this.storedOffset;
				this.storedOffset = num + 1;
				return array[num];
			}
			return base.ReadByte();
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0006B298 File Offset: 0x00069498
		internal void Push(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			if (this.storedOffset == this.storedLength)
			{
				if (this.storedBuffer == null || this.storedBuffer.Length < count)
				{
					this.storedBuffer = new byte[count];
				}
				this.storedOffset = 0;
				this.storedLength = count;
			}
			else if (count <= this.storedOffset)
			{
				this.storedOffset -= count;
			}
			else if (count <= this.storedBuffer.Length - this.storedLength + this.storedOffset)
			{
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, this.storedBuffer, count, this.storedLength - this.storedOffset);
				this.storedLength += count - this.storedOffset;
				this.storedOffset = 0;
			}
			else
			{
				byte[] array = new byte[count + this.storedLength - this.storedOffset];
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, array, count, this.storedLength - this.storedOffset);
				this.storedLength += count - this.storedOffset;
				this.storedOffset = 0;
				this.storedBuffer = array;
			}
			Buffer.BlockCopy(buffer, offset, this.storedBuffer, this.storedOffset, count);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0006B3D0 File Offset: 0x000695D0
		internal void Append(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			int num;
			if (this.storedOffset == this.storedLength)
			{
				if (this.storedBuffer == null || this.storedBuffer.Length < count)
				{
					this.storedBuffer = new byte[count];
				}
				this.storedOffset = 0;
				this.storedLength = count;
				num = 0;
			}
			else if (count <= this.storedBuffer.Length - this.storedLength)
			{
				num = this.storedLength;
				this.storedLength += count;
			}
			else if (count <= this.storedBuffer.Length - this.storedLength + this.storedOffset)
			{
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, this.storedBuffer, 0, this.storedLength - this.storedOffset);
				num = this.storedLength - this.storedOffset;
				this.storedOffset = 0;
				this.storedLength = count + num;
			}
			else
			{
				byte[] array = new byte[count + this.storedLength - this.storedOffset];
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, array, 0, this.storedLength - this.storedOffset);
				num = this.storedLength - this.storedOffset;
				this.storedOffset = 0;
				this.storedLength = count + num;
				this.storedBuffer = array;
			}
			Buffer.BlockCopy(buffer, offset, this.storedBuffer, num, count);
		}

		// Token: 0x0400160E RID: 5646
		private byte[] storedBuffer;

		// Token: 0x0400160F RID: 5647
		private int storedLength;

		// Token: 0x04001610 RID: 5648
		private int storedOffset;

		// Token: 0x04001611 RID: 5649
		private bool readMore;

		// Token: 0x02000767 RID: 1895
		private class ReadAsyncResult : LazyAsyncResult
		{
			// Token: 0x0600422B RID: 16939 RVA: 0x00112490 File Offset: 0x00110690
			internal ReadAsyncResult(BufferedReadStream parent, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this.parent = parent;
			}

			// Token: 0x0600422C RID: 16940 RVA: 0x001124A4 File Offset: 0x001106A4
			internal void Read(byte[] buffer, int offset, int count)
			{
				if (this.parent.storedOffset < this.parent.storedLength)
				{
					this.read = Math.Min(count, this.parent.storedLength - this.parent.storedOffset);
					Buffer.BlockCopy(this.parent.storedBuffer, this.parent.storedOffset, buffer, offset, this.read);
					this.parent.storedOffset += this.read;
					if (this.read == count || !this.parent.readMore)
					{
						base.InvokeCallback();
						return;
					}
					count -= this.read;
					offset += this.read;
				}
				IAsyncResult asyncResult = this.parent.BaseStream.BeginRead(buffer, offset, count, BufferedReadStream.ReadAsyncResult.onRead, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.read += this.parent.BaseStream.EndRead(asyncResult);
					base.InvokeCallback();
				}
			}

			// Token: 0x0600422D RID: 16941 RVA: 0x001125A4 File Offset: 0x001107A4
			internal static int End(IAsyncResult result)
			{
				BufferedReadStream.ReadAsyncResult readAsyncResult = (BufferedReadStream.ReadAsyncResult)result;
				readAsyncResult.InternalWaitForCompletion();
				return readAsyncResult.read;
			}

			// Token: 0x0600422E RID: 16942 RVA: 0x001125C8 File Offset: 0x001107C8
			private static void OnRead(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					BufferedReadStream.ReadAsyncResult readAsyncResult = (BufferedReadStream.ReadAsyncResult)result.AsyncState;
					try
					{
						readAsyncResult.read += readAsyncResult.parent.BaseStream.EndRead(result);
						readAsyncResult.InvokeCallback();
					}
					catch (Exception ex)
					{
						if (readAsyncResult.IsCompleted)
						{
							throw;
						}
						readAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x04003233 RID: 12851
			private BufferedReadStream parent;

			// Token: 0x04003234 RID: 12852
			private int read;

			// Token: 0x04003235 RID: 12853
			private static AsyncCallback onRead = new AsyncCallback(BufferedReadStream.ReadAsyncResult.OnRead);
		}
	}
}
