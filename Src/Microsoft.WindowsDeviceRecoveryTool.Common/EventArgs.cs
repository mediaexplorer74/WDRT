using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Common
{
	// Token: 0x02000005 RID: 5
	public class EventArgs<T> : EventArgs
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000220E File Offset: 0x0000040E
		public EventArgs()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002218 File Offset: 0x00000418
		public EventArgs(T value)
			: this()
		{
			this.Value = value;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000222A File Offset: 0x0000042A
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002232 File Offset: 0x00000432
		public T Value { get; set; }
	}
}
