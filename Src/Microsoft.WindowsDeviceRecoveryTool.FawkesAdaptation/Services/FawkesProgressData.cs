using System;

namespace Microsoft.WindowsDeviceRecoveryTool.FawkesAdaptation.Services
{
	// Token: 0x0200000A RID: 10
	internal class FawkesProgressData
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00003396 File Offset: 0x00001596
		internal FawkesProgressData(double? value, string message)
		{
			this.Value = value;
			this.Message = message;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000033AC File Offset: 0x000015AC
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000033B4 File Offset: 0x000015B4
		public double? Value { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000033BD File Offset: 0x000015BD
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000033C5 File Offset: 0x000015C5
		public string Message { get; private set; }
	}
}
