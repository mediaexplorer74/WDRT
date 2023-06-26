using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class FlashModeChangeException : Exception
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00006346 File Offset: 0x00004546
		public FlashModeChangeException()
		{
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006350 File Offset: 0x00004550
		public FlashModeChangeException(string message)
			: base(message)
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000635B File Offset: 0x0000455B
		public FlashModeChangeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006367 File Offset: 0x00004567
		protected FlashModeChangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
