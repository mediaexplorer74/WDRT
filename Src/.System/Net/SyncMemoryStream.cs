using System;
using System.IO;

namespace System.Net
{
	// Token: 0x020001A7 RID: 423
	internal sealed class SyncMemoryStream : MemoryStream, IRequestLifetimeTracker
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x000598A8 File Offset: 0x00057AA8
		internal SyncMemoryStream(byte[] bytes)
			: base(bytes, false)
		{
			this.m_ReadTimeout = (this.m_WriteTimeout = -1);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000598D0 File Offset: 0x00057AD0
		internal SyncMemoryStream(int initialCapacity)
			: base(initialCapacity)
		{
			this.m_ReadTimeout = (this.m_WriteTimeout = -1);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000598F4 File Offset: 0x00057AF4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			int num = this.Read(buffer, offset, count);
			return new LazyAsyncResult(null, state, callback, num);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0005991C File Offset: 0x00057B1C
		public override int EndRead(IAsyncResult asyncResult)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
			return (int)lazyAsyncResult.InternalWaitForCompletion();
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0005993B File Offset: 0x00057B3B
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.Write(buffer, offset, count);
			return new LazyAsyncResult(null, state, callback, null);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00059954 File Offset: 0x00057B54
		public override void EndWrite(IAsyncResult asyncResult)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
			lazyAsyncResult.InternalWaitForCompletion();
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x0005996F File Offset: 0x00057B6F
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00059972 File Offset: 0x00057B72
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x0005997A File Offset: 0x00057B7A
		public override int ReadTimeout
		{
			get
			{
				return this.m_ReadTimeout;
			}
			set
			{
				this.m_ReadTimeout = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00059983 File Offset: 0x00057B83
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x0005998B File Offset: 0x00057B8B
		public override int WriteTimeout
		{
			get
			{
				return this.m_WriteTimeout;
			}
			set
			{
				this.m_WriteTimeout = value;
			}
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00059994 File Offset: 0x00057B94
		public void TrackRequestLifetime(long requestStartTimestamp)
		{
			this.m_RequestLifetimeSetter = new RequestLifetimeSetter(requestStartTimestamp);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000599A2 File Offset: 0x00057BA2
		protected override void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			this.m_Disposed = true;
			if (disposing)
			{
				RequestLifetimeSetter.Report(this.m_RequestLifetimeSetter);
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001390 RID: 5008
		private int m_ReadTimeout;

		// Token: 0x04001391 RID: 5009
		private int m_WriteTimeout;

		// Token: 0x04001392 RID: 5010
		private RequestLifetimeSetter m_RequestLifetimeSetter;

		// Token: 0x04001393 RID: 5011
		private bool m_Disposed;
	}
}
