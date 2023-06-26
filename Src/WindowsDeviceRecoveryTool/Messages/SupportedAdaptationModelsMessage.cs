using System;
using System.Collections.Generic;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x02000096 RID: 150
	public class SupportedAdaptationModelsMessage
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x0001B25F File Offset: 0x0001945F
		public SupportedAdaptationModelsMessage(List<Phone> adaptationsModels)
		{
			this.Models = adaptationsModels;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001B271 File Offset: 0x00019471
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x0001B279 File Offset: 0x00019479
		public List<Phone> Models { get; private set; }
	}
}
