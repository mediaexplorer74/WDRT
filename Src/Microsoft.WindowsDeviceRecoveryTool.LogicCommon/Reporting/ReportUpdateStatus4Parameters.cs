using System;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting
{
	// Token: 0x0200001C RID: 28
	public sealed class ReportUpdateStatus4Parameters
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000081C7 File Offset: 0x000063C7
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000081CF File Offset: 0x000063CF
		public string OmsiModuleSessionId { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000081D8 File Offset: 0x000063D8
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x000081E0 File Offset: 0x000063E0
		public string UserSiteLanguageId { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000081E9 File Offset: 0x000063E9
		// (set) Token: 0x060001EB RID: 491 RVA: 0x000081F1 File Offset: 0x000063F1
		public long TimeStamp { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000081FA File Offset: 0x000063FA
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00008202 File Offset: 0x00006402
		public string IP { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000820B File Offset: 0x0000640B
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00008213 File Offset: 0x00006413
		public string ApplicationName { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000821C File Offset: 0x0000641C
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00008224 File Offset: 0x00006424
		public string ApplicationVersion { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000822D File Offset: 0x0000642D
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00008235 File Offset: 0x00006435
		public string ApplicationVendorName { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000823E File Offset: 0x0000643E
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00008246 File Offset: 0x00006446
		public string ProductType { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000824F File Offset: 0x0000644F
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00008257 File Offset: 0x00006457
		public string ProductCode { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00008260 File Offset: 0x00006460
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00008268 File Offset: 0x00006468
		public string BasicProductCode { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00008271 File Offset: 0x00006471
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00008279 File Offset: 0x00006479
		public string HwId { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00008282 File Offset: 0x00006482
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000828A File Offset: 0x0000648A
		public string Imei { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00008293 File Offset: 0x00006493
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000829B File Offset: 0x0000649B
		public string Imsi { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000082A4 File Offset: 0x000064A4
		// (set) Token: 0x06000201 RID: 513 RVA: 0x000082AC File Offset: 0x000064AC
		public string ActionDescription { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000082B5 File Offset: 0x000064B5
		// (set) Token: 0x06000203 RID: 515 RVA: 0x000082BD File Offset: 0x000064BD
		public string FlashDevice { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000082C6 File Offset: 0x000064C6
		// (set) Token: 0x06000205 RID: 517 RVA: 0x000082CE File Offset: 0x000064CE
		public long Duration { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000082D7 File Offset: 0x000064D7
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000082DF File Offset: 0x000064DF
		public long DownloadDuration { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000082E8 File Offset: 0x000064E8
		// (set) Token: 0x06000209 RID: 521 RVA: 0x000082F0 File Offset: 0x000064F0
		public long UpdateDuration { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000082F9 File Offset: 0x000064F9
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00008301 File Offset: 0x00006501
		public string FirmwareVersionOld { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000830A File Offset: 0x0000650A
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00008312 File Offset: 0x00006512
		public string FirmwareVersionNew { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000831B File Offset: 0x0000651B
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00008323 File Offset: 0x00006523
		public string FwGrading { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000832C File Offset: 0x0000652C
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00008334 File Offset: 0x00006534
		public string RdInfo { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000833D File Offset: 0x0000653D
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00008345 File Offset: 0x00006545
		public string CurrentMode { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000834E File Offset: 0x0000654E
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00008356 File Offset: 0x00006556
		public string TargetMode { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000835F File Offset: 0x0000655F
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00008367 File Offset: 0x00006567
		public string LanguagePackageOld { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00008370 File Offset: 0x00006570
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00008378 File Offset: 0x00006578
		public string LanguagePackageNew { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00008381 File Offset: 0x00006581
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00008389 File Offset: 0x00006589
		public long Uri { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00008392 File Offset: 0x00006592
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000839A File Offset: 0x0000659A
		public string UriDescription { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000083A3 File Offset: 0x000065A3
		// (set) Token: 0x0600021F RID: 543 RVA: 0x000083AB File Offset: 0x000065AB
		public string ApiError { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000083B4 File Offset: 0x000065B4
		// (set) Token: 0x06000221 RID: 545 RVA: 0x000083BC File Offset: 0x000065BC
		public string ApiErrorText { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000083C5 File Offset: 0x000065C5
		// (set) Token: 0x06000223 RID: 547 RVA: 0x000083CD File Offset: 0x000065CD
		public string DebugField { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000083D6 File Offset: 0x000065D6
		// (set) Token: 0x06000225 RID: 549 RVA: 0x000083DE File Offset: 0x000065DE
		public string Ext1 { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000083E7 File Offset: 0x000065E7
		// (set) Token: 0x06000227 RID: 551 RVA: 0x000083EF File Offset: 0x000065EF
		public string Ext2 { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000083F8 File Offset: 0x000065F8
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00008400 File Offset: 0x00006600
		public string Ext3 { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00008409 File Offset: 0x00006609
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00008411 File Offset: 0x00006611
		public string Ext4 { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000841A File Offset: 0x0000661A
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00008422 File Offset: 0x00006622
		public string Ext5 { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000842B File Offset: 0x0000662B
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00008433 File Offset: 0x00006633
		public string Ext6 { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000843C File Offset: 0x0000663C
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00008444 File Offset: 0x00006644
		public string Ext7 { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000844D File Offset: 0x0000664D
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00008455 File Offset: 0x00006655
		public string Ext8 { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000845E File Offset: 0x0000665E
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00008466 File Offset: 0x00006666
		public string Ext9 { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000846F File Offset: 0x0000666F
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00008477 File Offset: 0x00006677
		public string ServiceLayerInfo { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00008480 File Offset: 0x00006680
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00008488 File Offset: 0x00006688
		public string SystemInfo { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00008491 File Offset: 0x00006691
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00008499 File Offset: 0x00006699
		public string FlashDeviceInfo { get; set; }
	}
}
