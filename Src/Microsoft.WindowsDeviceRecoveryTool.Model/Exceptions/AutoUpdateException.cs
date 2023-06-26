using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public class AutoUpdateException : Exception
	{
		// Token: 0x06000203 RID: 515 RVA: 0x00006346 File Offset: 0x00004546
		public AutoUpdateException()
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006350 File Offset: 0x00004550
		public AutoUpdateException(string message)
			: base(message)
		{
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000635B File Offset: 0x0000455B
		public AutoUpdateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006367 File Offset: 0x00004567
		protected AutoUpdateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
