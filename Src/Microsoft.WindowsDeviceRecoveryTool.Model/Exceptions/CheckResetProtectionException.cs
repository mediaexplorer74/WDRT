using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class CheckResetProtectionException : Exception
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00006346 File Offset: 0x00004546
		public CheckResetProtectionException()
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006350 File Offset: 0x00004550
		public CheckResetProtectionException(string message)
			: base(message)
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000635B File Offset: 0x0000455B
		public CheckResetProtectionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006367 File Offset: 0x00004567
		protected CheckResetProtectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
