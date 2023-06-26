using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000A5 RID: 165
	public class FfuIntegrityCheckMessage
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x0001B4B7 File Offset: 0x000196B7
		public FfuIntegrityCheckMessage(bool result)
		{
			this.Result = result;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001B4C9 File Offset: 0x000196C9
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x0001B4D1 File Offset: 0x000196D1
		public bool Result { get; private set; }
	}
}
