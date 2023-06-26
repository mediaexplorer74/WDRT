using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	public class SoftwareIsNotCorrectlySignedException : Exception
	{
		// Token: 0x06000236 RID: 566 RVA: 0x00006346 File Offset: 0x00004546
		public SoftwareIsNotCorrectlySignedException()
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006350 File Offset: 0x00004550
		public SoftwareIsNotCorrectlySignedException(string message)
			: base(message)
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000635B File Offset: 0x0000455B
		public SoftwareIsNotCorrectlySignedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006367 File Offset: 0x00004567
		protected SoftwareIsNotCorrectlySignedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
