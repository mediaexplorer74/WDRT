using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Interfaces
{
	// Token: 0x0200001F RID: 31
	public interface IReport
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000273 RID: 627
		string SessionId { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000274 RID: 628
		// (set) Token: 0x06000275 RID: 629
		string LocalPath { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000276 RID: 630
		// (set) Token: 0x06000277 RID: 631
		string ManufacturerName { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000278 RID: 632
		// (set) Token: 0x06000279 RID: 633
		string ManufacturerProductLine { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600027A RID: 634
		// (set) Token: 0x0600027B RID: 635
		string ManufacturerHardwareModel { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600027C RID: 636
		// (set) Token: 0x0600027D RID: 637
		string ManufacturerHardwareVariant { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600027E RID: 638
		// (set) Token: 0x0600027F RID: 639
		string Imei { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000280 RID: 640
		// (set) Token: 0x06000281 RID: 641
		string ActionDescription { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000282 RID: 642
		PhoneTypes PhoneType { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000283 RID: 643
		bool Sent { get; }

		// Token: 0x06000284 RID: 644
		void MarkAsSent();

		// Token: 0x06000285 RID: 645
		string GetReportAsXml();

		// Token: 0x06000286 RID: 646
		string GetReportAsCsv();

		// Token: 0x06000287 RID: 647
		ReportUpdateStatus4Parameters CreateReportStatusParameters();
	}
}
