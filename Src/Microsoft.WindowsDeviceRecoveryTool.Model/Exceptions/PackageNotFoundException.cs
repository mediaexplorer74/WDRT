using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public class PackageNotFoundException : Exception
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00006346 File Offset: 0x00004546
		public PackageNotFoundException()
		{
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00006350 File Offset: 0x00004550
		public PackageNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000635B File Offset: 0x0000455B
		public PackageNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
