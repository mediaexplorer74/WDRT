using System;
using System.IO;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000226 RID: 550
	internal class ClosableStream : DelegatedStream
	{
		// Token: 0x06001435 RID: 5173 RVA: 0x0006B523 File Offset: 0x00069723
		internal ClosableStream(Stream stream, EventHandler onClose)
			: base(stream)
		{
			this.onClose = onClose;
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0006B533 File Offset: 0x00069733
		public override void Close()
		{
			if (Interlocked.Increment(ref this.closed) == 1 && this.onClose != null)
			{
				this.onClose(this, new EventArgs());
			}
		}

		// Token: 0x04001612 RID: 5650
		private EventHandler onClose;

		// Token: 0x04001613 RID: 5651
		private int closed;
	}
}
