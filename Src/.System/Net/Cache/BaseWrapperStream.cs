using System;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x0200031A RID: 794
	internal abstract class BaseWrapperStream : Stream, IRequestLifetimeTracker
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x000866A6 File Offset: 0x000848A6
		protected Stream WrappedStream
		{
			get
			{
				return this.m_WrappedStream;
			}
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x000866AE File Offset: 0x000848AE
		public BaseWrapperStream(Stream wrappedStream)
		{
			this.m_WrappedStream = wrappedStream;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000866C0 File Offset: 0x000848C0
		public void TrackRequestLifetime(long requestStartTimestamp)
		{
			IRequestLifetimeTracker requestLifetimeTracker = this.m_WrappedStream as IRequestLifetimeTracker;
			requestLifetimeTracker.TrackRequestLifetime(requestStartTimestamp);
		}

		// Token: 0x04001B7D RID: 7037
		private Stream m_WrappedStream;
	}
}
