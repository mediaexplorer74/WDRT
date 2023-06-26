using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x0200000E RID: 14
	public class DeviceIdentifier
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002E94 File Offset: 0x00001094
		public DeviceIdentifier(string vid, string pid, params int[] mi)
		{
			this.Vid = vid;
			this.Pid = pid;
			this.Mi = mi;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002EB6 File Offset: 0x000010B6
		public DeviceIdentifier(string vid, string pid)
		{
			this.Vid = vid;
			this.Pid = pid;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002ED0 File Offset: 0x000010D0
		public DeviceIdentifier(string vid)
		{
			this.Vid = vid;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002EE2 File Offset: 0x000010E2
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002EEA File Offset: 0x000010EA
		public string Vid { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002EF3 File Offset: 0x000010F3
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002EFB File Offset: 0x000010FB
		public string Pid { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002F04 File Offset: 0x00001104
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002F0C File Offset: 0x0000110C
		public int[] Mi { get; private set; }
	}
}
