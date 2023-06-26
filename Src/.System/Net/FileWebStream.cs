using System;
using System.IO;

namespace System.Net
{
	// Token: 0x020000E8 RID: 232
	internal sealed class FileWebStream : FileStream, ICloseEx
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x0002BF5D File Offset: 0x0002A15D
		public FileWebStream(FileWebRequest request, string path, FileMode mode, FileAccess access, FileShare sharing)
			: base(path, mode, access, sharing)
		{
			this.m_request = request;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0002BF72 File Offset: 0x0002A172
		public FileWebStream(FileWebRequest request, string path, FileMode mode, FileAccess access, FileShare sharing, int length, bool async)
			: base(path, mode, access, sharing, length, async)
		{
			this.m_request = request;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0002BF8C File Offset: 0x0002A18C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.m_request != null)
				{
					this.m_request.UnblockReader();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			if ((closeState & CloseExState.Abort) != CloseExState.Normal)
			{
				this.SafeFileHandle.Close();
				return;
			}
			this.Close();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0002BFE8 File Offset: 0x0002A1E8
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			int num;
			try
			{
				num = base.Read(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return num;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002C024 File Offset: 0x0002A224
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			try
			{
				base.Write(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0002C05C File Offset: 0x0002A25C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult asyncResult;
			try
			{
				asyncResult = base.BeginRead(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return asyncResult;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002C09C File Offset: 0x0002A29C
		public override int EndRead(IAsyncResult ar)
		{
			int num;
			try
			{
				num = base.EndRead(ar);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return num;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002C0D0 File Offset: 0x0002A2D0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult asyncResult;
			try
			{
				asyncResult = base.BeginWrite(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return asyncResult;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002C110 File Offset: 0x0002A310
		public override void EndWrite(IAsyncResult ar)
		{
			try
			{
				base.EndWrite(ar);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0002C140 File Offset: 0x0002A340
		private void CheckError()
		{
			if (this.m_request.Aborted)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x04000D4C RID: 3404
		private FileWebRequest m_request;
	}
}
