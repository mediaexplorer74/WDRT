using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000006 RID: 6
	public class ConnectedDevice
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002940 File Offset: 0x00000B40
		public ConnectedDevice(string portId, string vid, string pid, ConnectedDeviceMode mode, bool deviceIsConnected, string typeDesignator, string salesName, string path, string instanceId)
		{
			this.PortId = portId;
			this.Vid = vid;
			this.Pid = pid;
			this.Mode = mode;
			this.IsDeviceConnected = deviceIsConnected;
			this.SuppressConnectedDisconnectedEvents = false;
			this.TypeDesignator = typeDesignator;
			this.SalesName = salesName;
			this.DeviceReady = false;
			this.DevicePath = string.Empty;
			this.Path = path;
			this.InstanceId = instanceId;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000029BF File Offset: 0x00000BBF
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000029C7 File Offset: 0x00000BC7
		public string TypeDesignator { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000029D0 File Offset: 0x00000BD0
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000029D8 File Offset: 0x00000BD8
		public string SalesName { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000029E1 File Offset: 0x00000BE1
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000029E9 File Offset: 0x00000BE9
		public string PortId { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000029F2 File Offset: 0x00000BF2
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000029FA File Offset: 0x00000BFA
		public string Vid { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002A03 File Offset: 0x00000C03
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002A0B File Offset: 0x00000C0B
		public string Pid { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A14 File Offset: 0x00000C14
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002A1C File Offset: 0x00000C1C
		public string DevicePath { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002A25 File Offset: 0x00000C25
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002A2D File Offset: 0x00000C2D
		public string InstanceId { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A36 File Offset: 0x00000C36
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002A3E File Offset: 0x00000C3E
		public ConnectedDeviceMode Mode { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A47 File Offset: 0x00000C47
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002A4F File Offset: 0x00000C4F
		public bool IsDeviceConnected { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002A58 File Offset: 0x00000C58
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002A60 File Offset: 0x00000C60
		public bool DeviceReady { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002A69 File Offset: 0x00000C69
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002A71 File Offset: 0x00000C71
		public string Path { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002A7A File Offset: 0x00000C7A
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002A82 File Offset: 0x00000C82
		public bool SuppressConnectedDisconnectedEvents { get; set; }
	}
}
