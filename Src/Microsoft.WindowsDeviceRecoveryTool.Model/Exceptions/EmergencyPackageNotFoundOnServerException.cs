using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class EmergencyPackageNotFoundOnServerException : Exception
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x000063A6 File Offset: 0x000045A6
		public EmergencyPackageNotFoundOnServerException()
			: base("Emergency package was not found on server")
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006350 File Offset: 0x00004550
		public EmergencyPackageNotFoundOnServerException(string message)
			: base(message)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000635B File Offset: 0x0000455B
		public EmergencyPackageNotFoundOnServerException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006367 File Offset: 0x00004567
		protected EmergencyPackageNotFoundOnServerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
