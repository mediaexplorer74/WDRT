using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000013 RID: 19
	public class Phone : NotificationObject
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x000041CA File Offset: 0x000023CA
		public Phone()
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000041DF File Offset: 0x000023DF
		public Phone(string salesName, string type)
		{
			this.SalesName = salesName;
			this.HardwareModel = type;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004204 File Offset: 0x00002404
		public Phone(Guid id, PlatformId platformId)
		{
			this.ConnectionId = id;
			this.PlatformId = platformId;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000422C File Offset: 0x0000242C
		public Phone(string portId, string vid, string pid, string locationPath, string hardwareModel, string hardwareVariant, string salesName, string softwareVersion, string path, PhoneTypes phoneType, string instanceId, ISalesNameProvider salesNameProvider, bool deviceReady = false, string mid = "", string cid = "")
		{
			this.PortId = portId;
			this.LocationPath = locationPath;
			this.Vid = vid;
			this.Pid = pid;
			this.HardwareModel = hardwareModel;
			this.HardwareVariant = hardwareVariant;
			this.SalesName = salesName;
			this.Type = phoneType;
			this.SoftwareVersion = softwareVersion;
			this.Path = path;
			this.Mid = mid;
			this.Cid = cid;
			this.DeviceReady = deviceReady;
			this.InstanceId = instanceId;
			this.SalesNameProvider = salesNameProvider;
			bool flag;
			if (this.Type == PhoneTypes.Htc && ApplicationInfo.IsInternal())
			{
				flag = this.Cid.All((char c) => c.Equals('1'));
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				this.Cid = null;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000431C File Offset: 0x0000251C
		public Phone(UsbDevice usbDevice, PhoneTypes phoneType, ISalesNameProvider salesNameProvider = null, bool deviceReady = false, string mid = "", string cid = "")
			: this(usbDevice.PortId, usbDevice.Vid, usbDevice.Pid, usbDevice.LocationPath, usbDevice.TypeDesignator, usbDevice.ProductCode, usbDevice.SalesName, usbDevice.SoftwareVersion, usbDevice.Path, phoneType, usbDevice.InstanceId, salesNameProvider, deviceReady, mid, cid)
		{
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004375 File Offset: 0x00002575
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000437D File Offset: 0x0000257D
		public ISalesNameProvider SalesNameProvider { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004386 File Offset: 0x00002586
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000438E File Offset: 0x0000258E
		public string PortId { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004397 File Offset: 0x00002597
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000439F File Offset: 0x0000259F
		public string LocationPath { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000043A8 File Offset: 0x000025A8
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000043B0 File Offset: 0x000025B0
		public string Vid { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000043B9 File Offset: 0x000025B9
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000043C1 File Offset: 0x000025C1
		public string Pid { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000043CA File Offset: 0x000025CA
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000043D2 File Offset: 0x000025D2
		public string HardwareModel { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000043DC File Offset: 0x000025DC
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000043F4 File Offset: 0x000025F4
		public string HardwareVariant
		{
			get
			{
				return this.hardwareVariant;
			}
			set
			{
				base.SetValue<string>(() => this.HardwareVariant, ref this.hardwareVariant, value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004434 File Offset: 0x00002634
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000443C File Offset: 0x0000263C
		public string ModelIdentificationInstruction { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004445 File Offset: 0x00002645
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000444D File Offset: 0x0000264D
		public string InstanceId { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004456 File Offset: 0x00002656
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000445E File Offset: 0x0000265E
		public PlatformId PlatformId { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004467 File Offset: 0x00002667
		// (set) Token: 0x0600010A RID: 266 RVA: 0x0000446F File Offset: 0x0000266F
		public PhoneTypes Type { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00004478 File Offset: 0x00002678
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00004480 File Offset: 0x00002680
		public Guid ConnectionId { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00004489 File Offset: 0x00002689
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00004491 File Offset: 0x00002691
		public string PackageFilePath { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000449A File Offset: 0x0000269A
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000044A2 File Offset: 0x000026A2
		public byte[] ImageData { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000044AB File Offset: 0x000026AB
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000044B3 File Offset: 0x000026B3
		public List<PhoneTypes> MatchedAdaptationTypes { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000044BC File Offset: 0x000026BC
		// (set) Token: 0x06000114 RID: 276 RVA: 0x000044D4 File Offset: 0x000026D4
		public PackageFileInfo PackageFileInfo
		{
			get
			{
				return this.packageFileInfo;
			}
			set
			{
				base.SetValue<PackageFileInfo>(() => this.PackageFileInfo, ref this.packageFileInfo, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004514 File Offset: 0x00002714
		// (set) Token: 0x06000116 RID: 278 RVA: 0x000045E3 File Offset: 0x000027E3
		public string SalesName
		{
			get
			{
				bool flag = this.SalesNameProvider != null;
				if (flag)
				{
					string text = this.SalesNameProvider.NameForVidPid(this.Vid, this.Pid);
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						return text;
					}
					bool flag3 = !string.IsNullOrWhiteSpace(this.salesName);
					if (flag3)
					{
						text = this.SalesNameProvider.NameForString(this.salesName);
						bool flag4 = !string.IsNullOrEmpty(text);
						if (flag4)
						{
							return text;
						}
					}
					bool flag5 = !string.IsNullOrWhiteSpace(this.HardwareModel);
					if (flag5)
					{
						text = this.SalesNameProvider.NameForTypeDesignator(this.HardwareModel);
						bool flag6 = !string.IsNullOrEmpty(text);
						if (flag6)
						{
							return text;
						}
					}
				}
				return this.salesName;
			}
			set
			{
				base.SetValue<string>(() => this.SalesName, ref this.salesName, value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00004624 File Offset: 0x00002824
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0000463C File Offset: 0x0000283C
		public string SoftwareVersion
		{
			get
			{
				return this.softwareVersion;
			}
			set
			{
				base.SetValue<string>(() => this.SoftwareVersion, ref this.softwareVersion, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000467C File Offset: 0x0000287C
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00004694 File Offset: 0x00002894
		public string AkVersion
		{
			get
			{
				return this.adaptationKitVersion;
			}
			set
			{
				base.SetValue<string>(() => this.AkVersion, ref this.adaptationKitVersion, value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000046D4 File Offset: 0x000028D4
		public string NewAkVersion
		{
			get
			{
				return (this.packageFileInfo == null) ? null : this.packageFileInfo.AkVersion;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000046FC File Offset: 0x000028FC
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00004714 File Offset: 0x00002914
		public string Imei
		{
			get
			{
				return this.imei;
			}
			set
			{
				base.SetValue<string>(() => this.Imei, ref this.imei, value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004754 File Offset: 0x00002954
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00004798 File Offset: 0x00002998
		public string NewSoftwareVersion
		{
			get
			{
				bool flag = string.IsNullOrWhiteSpace(this.newSoftwareVersion) && this.packageFileInfo != null;
				string text;
				if (flag)
				{
					text = this.packageFileInfo.SoftwareVersion;
				}
				else
				{
					text = this.newSoftwareVersion;
				}
				return text;
			}
			set
			{
				base.SetValue<string>(() => this.NewSoftwareVersion, ref this.newSoftwareVersion, value);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000047D8 File Offset: 0x000029D8
		public string SerialNumber
		{
			get
			{
				try
				{
					string[] array = this.Path.Split(new char[] { '#' });
					return array[2].Replace("&", "_");
				}
				catch
				{
				}
				return null;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004830 File Offset: 0x00002A30
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00004838 File Offset: 0x00002A38
		public string Path { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004841 File Offset: 0x00002A41
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00004849 File Offset: 0x00002A49
		public string Mid { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004852 File Offset: 0x00002A52
		// (set) Token: 0x06000126 RID: 294 RVA: 0x0000485A File Offset: 0x00002A5A
		public string Cid { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004863 File Offset: 0x00002A63
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000486B File Offset: 0x00002A6B
		public bool DeviceReady { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00004874 File Offset: 0x00002A74
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000487C File Offset: 0x00002A7C
		public int BatteryLevel { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00004885 File Offset: 0x00002A85
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000488D File Offset: 0x00002A8D
		public List<string> PackageFiles { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00004896 File Offset: 0x00002A96
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000489E File Offset: 0x00002A9E
		public string ReportManufacturerName { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000048A7 File Offset: 0x00002AA7
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000048AF File Offset: 0x00002AAF
		public string ReportManufacturerProductLine { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000048B8 File Offset: 0x00002AB8
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000048C0 File Offset: 0x00002AC0
		public EmergencyPackageInfo EmergencyPackageFileInfo { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000048C9 File Offset: 0x00002AC9
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000048D1 File Offset: 0x00002AD1
		public QueryParameters QueryParameters { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000048DA File Offset: 0x00002ADA
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000048E2 File Offset: 0x00002AE2
		public string HardwareId { get; set; }

		// Token: 0x06000137 RID: 311 RVA: 0x000048EC File Offset: 0x00002AEC
		public bool IsWp10Device()
		{
			return this.Vid == "045E";
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004910 File Offset: 0x00002B10
		public override string ToString()
		{
			bool flag = this.Type == PhoneTypes.Htc;
			string text;
			if (flag)
			{
				text = string.Format("VID: {0}, PID: {1}, Mid: {2}, Cid: {3}, Type: {4}", new object[] { this.Vid, this.Pid, this.Mid, this.Cid, this.Type });
			}
			else
			{
				bool flag2 = this.Type == PhoneTypes.Lumia;
				if (flag2)
				{
					text = string.Format("VID: {0}, PID: {1}, Type Designator: {2}, Product Code: {3}, Type: {4}", new object[] { this.Vid, this.Pid, this.HardwareModel, this.HardwareVariant, this.Type });
				}
				else
				{
					bool flag3 = this.Type == PhoneTypes.Analog;
					if (flag3)
					{
						text = string.Format("ConnectionId - {0}, PortId - {1}, Path - {2}, LocationPath - {3}, InstanceId - {4}", new object[] { this.ConnectionId, this.PortId, this.Path, this.LocationPath, this.InstanceId });
					}
					else
					{
						text = string.Format("VID: {0}, PID: {1}, Hardware Model: {2}, Hardware Variant: {3}, Mid: {4}, Cid: {5}, Type: {6}, PlatformID: {7}", new object[] { this.Vid, this.Pid, this.HardwareModel, this.HardwareVariant, this.Mid, this.Cid, this.Type, this.PlatformId });
					}
				}
			}
			return text;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004A80 File Offset: 0x00002C80
		public bool IsDeviceInEmergencyMode()
		{
			return this.Type == PhoneTypes.Lumia && this.Vid == "05C6" && this.Pid == "9008";
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public bool IsProductCodeTypeEmpty()
		{
			return string.IsNullOrWhiteSpace(this.HardwareModel) || string.IsNullOrWhiteSpace(this.HardwareVariant);
		}

		// Token: 0x04000049 RID: 73
		private PackageFileInfo packageFileInfo;

		// Token: 0x0400004A RID: 74
		private string salesName;

		// Token: 0x0400004B RID: 75
		private string hardwareVariant;

		// Token: 0x0400004C RID: 76
		private string softwareVersion;

		// Token: 0x0400004D RID: 77
		private string imei;

		// Token: 0x0400004E RID: 78
		private string adaptationKitVersion;

		// Token: 0x0400004F RID: 79
		private string newSoftwareVersion;

		// Token: 0x04000050 RID: 80
		private string manufacturerProductLine = "WindowsPhone";
	}
}
