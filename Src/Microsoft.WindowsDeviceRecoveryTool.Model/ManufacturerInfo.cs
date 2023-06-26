using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000012 RID: 18
	public class ManufacturerInfo
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00004127 File Offset: 0x00002327
		public ManufacturerInfo(PhoneTypes type, bool recoverySupport, string manufacturerName, byte[] imageData, string reportName, string reportProductLine)
		{
			this.Type = type;
			this.RecoverySupport = recoverySupport;
			this.Name = manufacturerName;
			this.ImageData = imageData;
			this.ReportManufacturerName = reportName;
			this.ReportProductLine = reportProductLine;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004164 File Offset: 0x00002364
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000416C File Offset: 0x0000236C
		public PhoneTypes Type { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004175 File Offset: 0x00002375
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000417D File Offset: 0x0000237D
		public bool RecoverySupport { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004186 File Offset: 0x00002386
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x0000418E File Offset: 0x0000238E
		public string Name { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004197 File Offset: 0x00002397
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000419F File Offset: 0x0000239F
		public byte[] ImageData { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000041A8 File Offset: 0x000023A8
		// (set) Token: 0x060000ED RID: 237 RVA: 0x000041B0 File Offset: 0x000023B0
		public string ReportManufacturerName { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000041B9 File Offset: 0x000023B9
		// (set) Token: 0x060000EF RID: 239 RVA: 0x000041C1 File Offset: 0x000023C1
		public string ReportProductLine { get; set; }
	}
}
