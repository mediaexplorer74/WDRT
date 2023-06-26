using System;
using System.Collections.Generic;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x0200009E RID: 158
	public class ConnectedPhonesMessage
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x0001B3C2 File Offset: 0x000195C2
		public ConnectedPhonesMessage(List<Phone> phones)
		{
			this.Phones = phones;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001B3D4 File Offset: 0x000195D4
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0001B3DC File Offset: 0x000195DC
		public List<Phone> Phones { get; private set; }
	}
}
