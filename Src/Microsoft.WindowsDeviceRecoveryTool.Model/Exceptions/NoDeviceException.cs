using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000032 RID: 50
	public class NoDeviceException : Exception
	{
		// Token: 0x0600021B RID: 539 RVA: 0x00006346 File Offset: 0x00004546
		public NoDeviceException()
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006350 File Offset: 0x00004550
		public NoDeviceException(string message)
			: base(message)
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000635B File Offset: 0x0000455B
		public NoDeviceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006367 File Offset: 0x00004567
		protected NoDeviceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
