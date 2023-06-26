using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public class FlashException : Exception
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00006346 File Offset: 0x00004546
		public FlashException()
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006350 File Offset: 0x00004550
		public FlashException(string message)
			: base(message)
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000635B File Offset: 0x0000455B
		public FlashException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00006367 File Offset: 0x00004567
		protected FlashException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
