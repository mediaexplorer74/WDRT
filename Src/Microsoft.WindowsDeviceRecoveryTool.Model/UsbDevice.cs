using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000019 RID: 25
	public class UsbDevice
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00005A48 File Offset: 0x00003C48
		public UsbDevice(string portId, string vid, string pid, string locationPath, string typeDesignator, string salesName, string path, string instanceId)
		{
			this.PortId = portId;
			this.LocationPath = locationPath;
			this.Vid = vid;
			this.Pid = pid;
			this.TypeDesignator = typeDesignator;
			this.SalesName = salesName;
			this.interfaces = new List<UsbDeviceEndpoint>();
			this.Path = path;
			this.InstanceId = instanceId;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005AAD File Offset: 0x00003CAD
		public UsbDevice()
		{
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005AB7 File Offset: 0x00003CB7
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00005ABF File Offset: 0x00003CBF
		public string PortId { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005AC8 File Offset: 0x00003CC8
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public string LocationPath { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005AD9 File Offset: 0x00003CD9
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00005AE1 File Offset: 0x00003CE1
		public string Vid { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005AEA File Offset: 0x00003CEA
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00005AF2 File Offset: 0x00003CF2
		public string Pid { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005AFB File Offset: 0x00003CFB
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00005B03 File Offset: 0x00003D03
		public string TypeDesignator { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00005B0C File Offset: 0x00003D0C
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00005B14 File Offset: 0x00003D14
		public string SalesName { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00005B1D File Offset: 0x00003D1D
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00005B25 File Offset: 0x00003D25
		public string ProductCode { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00005B2E File Offset: 0x00003D2E
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00005B36 File Offset: 0x00003D36
		public string SoftwareVersion { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005B3F File Offset: 0x00003D3F
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00005B47 File Offset: 0x00003D47
		public string Path { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005B50 File Offset: 0x00003D50
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00005B58 File Offset: 0x00003D58
		public string InstanceId { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00005B64 File Offset: 0x00003D64
		public ReadOnlyCollection<UsbDeviceEndpoint> Interfaces
		{
			get
			{
				return this.interfaces.AsReadOnly();
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005B84 File Offset: 0x00003D84
		public bool SamePortAs(UsbDevice usbDevice)
		{
			return this.PortId.Equals(usbDevice.PortId, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public void AddInterface(string devicePath)
		{
			this.interfaces.Add(new UsbDeviceEndpoint
			{
				DevicePath = devicePath
			});
		}

		// Token: 0x04000083 RID: 131
		private readonly List<UsbDeviceEndpoint> interfaces;
	}
}
