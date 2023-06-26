using System;
using System.Runtime.Serialization;

namespace SoftwareRepository
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class ReportException : Exception
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000222C File Offset: 0x0000042C
		public ReportException()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002236 File Offset: 0x00000436
		public ReportException(string message)
			: base(message)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002241 File Offset: 0x00000441
		public ReportException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000224D File Offset: 0x0000044D
		protected ReportException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
