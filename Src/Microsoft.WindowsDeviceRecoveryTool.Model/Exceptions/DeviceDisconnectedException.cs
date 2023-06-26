using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class DeviceDisconnectedException : Exception
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00006346 File Offset: 0x00004546
		public DeviceDisconnectedException()
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006350 File Offset: 0x00004550
		public DeviceDisconnectedException(string message)
			: base(message)
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000635B File Offset: 0x0000455B
		public DeviceDisconnectedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006367 File Offset: 0x00004567
		protected DeviceDisconnectedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
