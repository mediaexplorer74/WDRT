using System;
using System.IO;

namespace System.Net
{
	// Token: 0x0200021F RID: 543
	internal class FixedSizeReader
	{
		// Token: 0x060013F9 RID: 5113 RVA: 0x0006A419 File Offset: 0x00068619
		public FixedSizeReader(Stream transport)
		{
			this._Transport = transport;
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0006A428 File Offset: 0x00068628
		public int ReadPacket(byte[] buffer, int offset, int count)
		{
			int num = count;
			for (;;)
			{
				int num2 = this._Transport.Read(buffer, offset, num);
				if (num2 == 0)
				{
					break;
				}
				num -= num2;
				offset += num2;
				if (num == 0)
				{
					return count;
				}
			}
			if (num != count)
			{
				throw new IOException(SR.GetString("net_io_eof"));
			}
			return 0;
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0006A46C File Offset: 0x0006866C
		public void AsyncReadPacket(AsyncProtocolRequest request)
		{
			this._Request = request;
			this._TotalRead = 0;
			this.StartReading();
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0006A484 File Offset: 0x00068684
		private void StartReading()
		{
			int num;
			do
			{
				IAsyncResult asyncResult = this._Transport.BeginRead(this._Request.Buffer, this._Request.Offset + this._TotalRead, this._Request.Count - this._TotalRead, FixedSizeReader._ReadCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					break;
				}
				num = this._Transport.EndRead(asyncResult);
			}
			while (!this.CheckCompletionBeforeNextRead(num));
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0006A4F0 File Offset: 0x000686F0
		private bool CheckCompletionBeforeNextRead(int bytes)
		{
			if (bytes == 0)
			{
				if (this._TotalRead == 0)
				{
					this._Request.CompleteRequest(0);
					return true;
				}
				throw new IOException(SR.GetString("net_io_eof"));
			}
			else
			{
				if ((this._TotalRead += bytes) == this._Request.Count)
				{
					this._Request.CompleteRequest(this._Request.Count);
					return true;
				}
				return false;
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0006A560 File Offset: 0x00068760
		private static void ReadCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			FixedSizeReader fixedSizeReader = (FixedSizeReader)transportResult.AsyncState;
			AsyncProtocolRequest request = fixedSizeReader._Request;
			try
			{
				int num = fixedSizeReader._Transport.EndRead(transportResult);
				if (!fixedSizeReader.CheckCompletionBeforeNextRead(num))
				{
					fixedSizeReader.StartReading();
				}
			}
			catch (Exception ex)
			{
				if (request.IsUserCompleted)
				{
					throw;
				}
				request.CompleteWithError(ex);
			}
		}

		// Token: 0x040015F2 RID: 5618
		private static readonly AsyncCallback _ReadCallback = new AsyncCallback(FixedSizeReader.ReadCallback);

		// Token: 0x040015F3 RID: 5619
		private readonly Stream _Transport;

		// Token: 0x040015F4 RID: 5620
		private AsyncProtocolRequest _Request;

		// Token: 0x040015F5 RID: 5621
		private int _TotalRead;
	}
}
