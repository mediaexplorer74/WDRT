using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x02000243 RID: 579
	internal class EightBitStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x000717E8 File Offset: 0x0006F9E8
		private WriteStateInfoBase WriteState
		{
			get
			{
				if (this.writeState == null)
				{
					this.writeState = new WriteStateInfoBase();
				}
				return this.writeState;
			}
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00071803 File Offset: 0x0006FA03
		internal EightBitStream(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0007180C File Offset: 0x0006FA0C
		internal EightBitStream(Stream stream, bool shouldEncodeLeadingDots)
			: this(stream)
		{
			this.shouldEncodeLeadingDots = shouldEncodeLeadingDots;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0007181C File Offset: 0x0006FA1C
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			IAsyncResult asyncResult;
			if (this.shouldEncodeLeadingDots)
			{
				this.EncodeLines(buffer, offset, count);
				asyncResult = base.BeginWrite(this.WriteState.Buffer, 0, this.WriteState.Length, callback, state);
			}
			else
			{
				asyncResult = base.BeginWrite(buffer, offset, count, callback, state);
			}
			return asyncResult;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000718A3 File Offset: 0x0006FAA3
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.EndWrite(asyncResult);
			this.WriteState.BufferFlushed();
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000718B8 File Offset: 0x0006FAB8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.shouldEncodeLeadingDots)
			{
				this.EncodeLines(buffer, offset, count);
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.BufferFlushed();
				return;
			}
			base.Write(buffer, offset, count);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00071940 File Offset: 0x0006FB40
		private void EncodeLines(byte[] buffer, int offset, int count)
		{
			int num = offset;
			while (num < offset + count && num < buffer.Length)
			{
				if (buffer[num] == 13 && num + 1 < offset + count && buffer[num + 1] == 10)
				{
					this.WriteState.AppendCRLF(false);
					num++;
				}
				else if (this.WriteState.CurrentLineLength == 0 && buffer[num] == 46)
				{
					this.WriteState.Append(46);
					this.WriteState.Append(buffer[num]);
				}
				else
				{
					this.WriteState.Append(buffer[num]);
				}
				num++;
			}
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x000719CA File Offset: 0x0006FBCA
		public int DecodeBytes(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x000719D1 File Offset: 0x0006FBD1
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x000719D8 File Offset: 0x0006FBD8
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000719DB File Offset: 0x0006FBDB
		public string GetEncodedString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040016F4 RID: 5876
		private WriteStateInfoBase writeState;

		// Token: 0x040016F5 RID: 5877
		private bool shouldEncodeLeadingDots;
	}
}
