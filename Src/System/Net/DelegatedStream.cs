using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000227 RID: 551
	internal class DelegatedStream : Stream
	{
		// Token: 0x06001437 RID: 5175 RVA: 0x0006B55C File Offset: 0x0006975C
		protected DelegatedStream()
		{
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0006B564 File Offset: 0x00069764
		protected DelegatedStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
			this.netStream = stream as NetworkStream;
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0006B58D File Offset: 0x0006978D
		protected Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0006B595 File Offset: 0x00069795
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0006B5A2 File Offset: 0x000697A2
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0006B5AF File Offset: 0x000697AF
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0006B5BC File Offset: 0x000697BC
		public override long Length
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException(SR.GetString("SeekNotSupported"));
				}
				return this.stream.Length;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0006B5E1 File Offset: 0x000697E1
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x0006B606 File Offset: 0x00069806
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException(SR.GetString("SeekNotSupported"));
				}
				return this.stream.Position;
			}
			set
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException(SR.GetString("SeekNotSupported"));
				}
				this.stream.Position = value;
			}
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0006B62C File Offset: 0x0006982C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.GetString("ReadNotSupported"));
			}
			IAsyncResult asyncResult;
			if (this.netStream != null)
			{
				asyncResult = this.netStream.UnsafeBeginRead(buffer, offset, count, callback, state);
			}
			else
			{
				asyncResult = this.stream.BeginRead(buffer, offset, count, callback, state);
			}
			return asyncResult;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0006B684 File Offset: 0x00069884
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(SR.GetString("WriteNotSupported"));
			}
			IAsyncResult asyncResult;
			if (this.netStream != null)
			{
				asyncResult = this.netStream.UnsafeBeginWrite(buffer, offset, count, callback, state);
			}
			else
			{
				asyncResult = this.stream.BeginWrite(buffer, offset, count, callback, state);
			}
			return asyncResult;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0006B6DC File Offset: 0x000698DC
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0006B6EC File Offset: 0x000698EC
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.GetString("ReadNotSupported"));
			}
			return this.stream.EndRead(asyncResult);
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0006B71F File Offset: 0x0006991F
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(SR.GetString("WriteNotSupported"));
			}
			this.stream.EndWrite(asyncResult);
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0006B745 File Offset: 0x00069945
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0006B752 File Offset: 0x00069952
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this.stream.FlushAsync(cancellationToken);
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0006B760 File Offset: 0x00069960
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.GetString("ReadNotSupported"));
			}
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0006B795 File Offset: 0x00069995
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.GetString("ReadNotSupported"));
			}
			return this.stream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0006B7C0 File Offset: 0x000699C0
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException(SR.GetString("SeekNotSupported"));
			}
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0006B7F4 File Offset: 0x000699F4
		public override void SetLength(long value)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException(SR.GetString("SeekNotSupported"));
			}
			this.stream.SetLength(value);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0006B81A File Offset: 0x00069A1A
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(SR.GetString("WriteNotSupported"));
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0006B842 File Offset: 0x00069A42
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(SR.GetString("WriteNotSupported"));
			}
			return this.stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x04001614 RID: 5652
		private Stream stream;

		// Token: 0x04001615 RID: 5653
		private NetworkStream netStream;
	}
}
