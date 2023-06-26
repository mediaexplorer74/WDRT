using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	public class PlannedServiceBreakException : Exception
	{
		// Token: 0x0600022C RID: 556 RVA: 0x00006346 File Offset: 0x00004546
		public PlannedServiceBreakException()
		{
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006350 File Offset: 0x00004550
		public PlannedServiceBreakException(string message)
			: base(message)
		{
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000635B File Offset: 0x0000455B
		public PlannedServiceBreakException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006367 File Offset: 0x00004567
		protected PlannedServiceBreakException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006471 File Offset: 0x00004671
		public PlannedServiceBreakException(DateTime start, DateTime end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000648B File Offset: 0x0000468B
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00006493 File Offset: 0x00004693
		public DateTime Start { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000649C File Offset: 0x0000469C
		// (set) Token: 0x06000234 RID: 564 RVA: 0x000064A4 File Offset: 0x000046A4
		public DateTime End { get; private set; }

		// Token: 0x06000235 RID: 565 RVA: 0x000064B0 File Offset: 0x000046B0
		public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			bool flag = info == null;
			if (!flag)
			{
				base.GetObjectData(info, context);
			}
		}
	}
}
