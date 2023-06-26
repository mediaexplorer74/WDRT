using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x0200009B RID: 155
	public class DetectionTypeMessage
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x0001B327 File Offset: 0x00019527
		public DetectionTypeMessage(DetectionType detectionType)
		{
			this.DetectionType = detectionType;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001B339 File Offset: 0x00019539
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0001B341 File Offset: 0x00019541
		public DetectionType DetectionType { get; private set; }
	}
}
