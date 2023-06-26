using System;
using System.Collections.Generic;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000A8 RID: 168
	public class FfuFilePlatformIdMessage
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x0001B539 File Offset: 0x00019739
		public FfuFilePlatformIdMessage(PlatformId platformId, string version)
		{
			this.PlatformId = platformId;
			this.Version = version;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001B553 File Offset: 0x00019753
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0001B55B File Offset: 0x0001975B
		public PlatformId PlatformId { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0001B564 File Offset: 0x00019764
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0001B56C File Offset: 0x0001976C
		public string Version { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0001B575 File Offset: 0x00019775
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0001B57D File Offset: 0x0001977D
		public IEnumerable<PlatformId> AllPlatformIds { get; set; }
	}
}
