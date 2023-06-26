using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x020001B2 RID: 434
	internal class FtpDataStream : Stream, ICloseEx
	{
		// Token: 0x0600110E RID: 4366 RVA: 0x0005D21C File Offset: 0x0005B41C
		internal FtpDataStream(NetworkStream networkStream, FtpWebRequest request, TriState writeOnly)
		{
			this.m_Readable = true;
			this.m_Writeable = true;
			if (writeOnly == TriState.True)
			{
				this.m_Readable = false;
			}
			else if (writeOnly == TriState.False)
			{
				this.m_Writeable = false;
			}
			this.m_NetworkStream = networkStream;
			this.m_Request = request;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0005D258 File Offset: 0x0005B458
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((ICloseEx)this).CloseEx(CloseExState.Normal);
				}
				else
				{
					((ICloseEx)this).CloseEx(CloseExState.Abort | CloseExState.Silent);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0005D294 File Offset: 0x0005B494
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			lock (this)
			{
				if (this.m_Closing)
				{
					return;
				}
				this.m_Closing = true;
				this.m_Writeable = false;
				this.m_Readable = false;
			}
			try
			{
				try
				{
					if ((closeState & CloseExState.Abort) == CloseExState.Normal)
					{
						this.m_NetworkStream.Close(-1);
					}
					else
					{
						this.m_NetworkStream.Close(0);
					}
				}
				finally
				{
					this.m_Request.DataStreamClosed(closeState);
				}
			}
			catch (Exception ex)
			{
				bool flag2 = true;
				WebException ex2 = ex as WebException;
				if (ex2 != null)
				{
					FtpWebResponse ftpWebResponse = ex2.Response as FtpWebResponse;
					if (ftpWebResponse != null && !this.m_IsFullyRead && ftpWebResponse.StatusCode == FtpStatusCode.ConnectionClosed)
					{
						flag2 = false;
					}
				}
				if (flag2 && (closeState & CloseExState.Silent) == CloseExState.Normal)
				{
					throw;
				}
			}
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0005D37C File Offset: 0x0005B57C
		private void CheckError()
		{
			if (this.m_Request.Aborted)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0005D39D File Offset: 0x0005B59D
		public override bool CanRead
		{
			get
			{
				return this.m_Readable;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x0005D3A5 File Offset: 0x0005B5A5
		public override bool CanSeek
		{
			get
			{
				return this.m_NetworkStream.CanSeek;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0005D3B2 File Offset: 0x0005B5B2
		public override bool CanWrite
		{
			get
			{
				return this.m_Writeable;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0005D3BA File Offset: 0x0005B5BA
		public override long Length
		{
			get
			{
				return this.m_NetworkStream.Length;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0005D3C7 File Offset: 0x0005B5C7
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x0005D3D4 File Offset: 0x0005B5D4
		public override long Position
		{
			get
			{
				return this.m_NetworkStream.Position;
			}
			set
			{
				this.m_NetworkStream.Position = value;
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0005D3E4 File Offset: 0x0005B5E4
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckError();
			long num;
			try
			{
				num = this.m_NetworkStream.Seek(offset, origin);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return num;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0005D424 File Offset: 0x0005B624
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			int num;
			try
			{
				num = this.m_NetworkStream.Read(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			if (num == 0)
			{
				this.m_IsFullyRead = true;
				this.Close();
			}
			return num;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0005D474 File Offset: 0x0005B674
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			try
			{
				this.m_NetworkStream.Write(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0005D4B0 File Offset: 0x0005B6B0
		private void AsyncReadCallback(IAsyncResult ar)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)ar.AsyncState;
			try
			{
				try
				{
					int num = this.m_NetworkStream.EndRead(ar);
					if (num == 0)
					{
						this.m_IsFullyRead = true;
						this.Close();
					}
					lazyAsyncResult.InvokeCallback(num);
				}
				catch (Exception ex)
				{
					if (!lazyAsyncResult.IsCompleted)
					{
						lazyAsyncResult.InvokeCallback(ex);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0005D528 File Offset: 0x0005B728
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, state, callback);
			try
			{
				this.m_NetworkStream.BeginRead(buffer, offset, size, new AsyncCallback(this.AsyncReadCallback), lazyAsyncResult);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return lazyAsyncResult;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0005D580 File Offset: 0x0005B780
		public override int EndRead(IAsyncResult ar)
		{
			int num;
			try
			{
				object obj = ((LazyAsyncResult)ar).InternalWaitForCompletion();
				if (obj is Exception)
				{
					throw (Exception)obj;
				}
				num = (int)obj;
			}
			finally
			{
				this.CheckError();
			}
			return num;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0005D5CC File Offset: 0x0005B7CC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.m_NetworkStream.BeginWrite(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return asyncResult;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0005D610 File Offset: 0x0005B810
		public override void EndWrite(IAsyncResult asyncResult)
		{
			try
			{
				this.m_NetworkStream.EndWrite(asyncResult);
			}
			finally
			{
				this.CheckError();
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0005D644 File Offset: 0x0005B844
		public override void Flush()
		{
			this.m_NetworkStream.Flush();
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0005D651 File Offset: 0x0005B851
		public override void SetLength(long value)
		{
			this.m_NetworkStream.SetLength(value);
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0005D65F File Offset: 0x0005B85F
		public override bool CanTimeout
		{
			get
			{
				return this.m_NetworkStream.CanTimeout;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0005D66C File Offset: 0x0005B86C
		// (set) Token: 0x06001124 RID: 4388 RVA: 0x0005D679 File Offset: 0x0005B879
		public override int ReadTimeout
		{
			get
			{
				return this.m_NetworkStream.ReadTimeout;
			}
			set
			{
				this.m_NetworkStream.ReadTimeout = value;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0005D687 File Offset: 0x0005B887
		// (set) Token: 0x06001126 RID: 4390 RVA: 0x0005D694 File Offset: 0x0005B894
		public override int WriteTimeout
		{
			get
			{
				return this.m_NetworkStream.WriteTimeout;
			}
			set
			{
				this.m_NetworkStream.WriteTimeout = value;
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0005D6A2 File Offset: 0x0005B8A2
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			this.m_NetworkStream.SetSocketTimeoutOption(mode, timeout, silent);
		}

		// Token: 0x040013F8 RID: 5112
		private FtpWebRequest m_Request;

		// Token: 0x040013F9 RID: 5113
		private NetworkStream m_NetworkStream;

		// Token: 0x040013FA RID: 5114
		private bool m_Writeable;

		// Token: 0x040013FB RID: 5115
		private bool m_Readable;

		// Token: 0x040013FC RID: 5116
		private bool m_IsFullyRead;

		// Token: 0x040013FD RID: 5117
		private bool m_Closing;
	}
}
