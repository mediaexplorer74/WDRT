using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation.Services
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class DeviceException : Exception
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003EB8 File Offset: 0x000020B8
		public DeviceException()
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003EC2 File Offset: 0x000020C2
		public DeviceException(string message)
			: base(message)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003ECD File Offset: 0x000020CD
		public DeviceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003ED9 File Offset: 0x000020D9
		protected DeviceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
