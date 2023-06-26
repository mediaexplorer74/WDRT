using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class DeviceNotFoundException : Exception
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00006346 File Offset: 0x00004546
		public DeviceNotFoundException()
		{
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006350 File Offset: 0x00004550
		public DeviceNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000635B File Offset: 0x0000455B
		public DeviceNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006367 File Offset: 0x00004567
		protected DeviceNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
