using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public class DownloadPackageException : Exception
	{
		// Token: 0x0600020F RID: 527 RVA: 0x00006346 File Offset: 0x00004546
		public DownloadPackageException()
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006350 File Offset: 0x00004550
		public DownloadPackageException(string message)
			: base(message)
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000635B File Offset: 0x0000455B
		public DownloadPackageException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006367 File Offset: 0x00004567
		protected DownloadPackageException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
