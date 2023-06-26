using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000B5 RID: 181
	public class ErrorMessage
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x0001B8AA File Offset: 0x00019AAA
		public ErrorMessage(Exception exception)
		{
			this.Exception = exception;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001B8BC File Offset: 0x00019ABC
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0001B8C4 File Offset: 0x00019AC4
		public Exception Exception { get; set; }
	}
}
