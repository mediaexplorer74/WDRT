using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009AB RID: 2475
	[SecurityCritical]
	internal class ComEventsInfo
	{
		// Token: 0x0600632A RID: 25386 RVA: 0x0015339C File Offset: 0x0015159C
		private ComEventsInfo(object rcw)
		{
			this._rcw = rcw;
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x001533AC File Offset: 0x001515AC
		[SecuritySafeCritical]
		~ComEventsInfo()
		{
			this._sinks = ComEventsSink.RemoveAll(this._sinks);
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x001533E4 File Offset: 0x001515E4
		[SecurityCritical]
		internal static ComEventsInfo Find(object rcw)
		{
			return (ComEventsInfo)Marshal.GetComObjectData(rcw, typeof(ComEventsInfo));
		}

		// Token: 0x0600632D RID: 25389 RVA: 0x001533FC File Offset: 0x001515FC
		[SecurityCritical]
		internal static ComEventsInfo FromObject(object rcw)
		{
			ComEventsInfo comEventsInfo = ComEventsInfo.Find(rcw);
			if (comEventsInfo == null)
			{
				comEventsInfo = new ComEventsInfo(rcw);
				Marshal.SetComObjectData(rcw, typeof(ComEventsInfo), comEventsInfo);
			}
			return comEventsInfo;
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x0015342D File Offset: 0x0015162D
		internal ComEventsSink FindSink(ref Guid iid)
		{
			return ComEventsSink.Find(this._sinks, ref iid);
		}

		// Token: 0x0600632F RID: 25391 RVA: 0x0015343C File Offset: 0x0015163C
		internal ComEventsSink AddSink(ref Guid iid)
		{
			ComEventsSink comEventsSink = new ComEventsSink(this._rcw, iid);
			this._sinks = ComEventsSink.Add(this._sinks, comEventsSink);
			return this._sinks;
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x00153473 File Offset: 0x00151673
		[SecurityCritical]
		internal ComEventsSink RemoveSink(ComEventsSink sink)
		{
			this._sinks = ComEventsSink.Remove(this._sinks, sink);
			return this._sinks;
		}

		// Token: 0x04002CBD RID: 11453
		private ComEventsSink _sinks;

		// Token: 0x04002CBE RID: 11454
		private object _rcw;
	}
}
