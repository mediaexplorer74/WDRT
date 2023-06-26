using System;
using System.Runtime.Serialization;

namespace SoftwareRepository.Discovery
{
	// Token: 0x02000021 RID: 33
	[DataContract]
	public class DiscoveryQueryParameters
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004600 File Offset: 0x00002800
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004608 File Offset: 0x00002808
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004611 File Offset: 0x00002811
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00004619 File Offset: 0x00002819
		[DataMember(Name = "customerName", EmitDefaultValue = false)]
		public string CustomerName { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004622 File Offset: 0x00002822
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x0000462A File Offset: 0x0000282A
		[DataMember(Name = "extendedAttributes", EmitDefaultValue = false)]
		public ExtendedAttributes ExtendedAttributes { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004633 File Offset: 0x00002833
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000463B File Offset: 0x0000283B
		[DataMember(Name = "manufacturerHardwareModel", EmitDefaultValue = false)]
		public string ManufacturerHardwareModel { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004644 File Offset: 0x00002844
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000464C File Offset: 0x0000284C
		[DataMember(Name = "manufacturerHardwareVariant", EmitDefaultValue = false)]
		public string ManufacturerHardwareVariant { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004655 File Offset: 0x00002855
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000465D File Offset: 0x0000285D
		[DataMember(Name = "manufacturerModelName", EmitDefaultValue = false)]
		public string ManufacturerModelName { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004666 File Offset: 0x00002866
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000466E File Offset: 0x0000286E
		[DataMember(Name = "manufacturerName")]
		public string ManufacturerName { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004677 File Offset: 0x00002877
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000467F File Offset: 0x0000287F
		[DataMember(Name = "manufacturerPackageId", EmitDefaultValue = false)]
		public string ManufacturerPackageId { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004688 File Offset: 0x00002888
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00004690 File Offset: 0x00002890
		[DataMember(Name = "manufacturerPlatformId", EmitDefaultValue = false)]
		public string ManufacturerPlatformId { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004699 File Offset: 0x00002899
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000046A1 File Offset: 0x000028A1
		[DataMember(Name = "manufacturerProductLine")]
		public string ManufacturerProductLine { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000046AA File Offset: 0x000028AA
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000046B2 File Offset: 0x000028B2
		[DataMember(Name = "manufacturerVariantName", EmitDefaultValue = false)]
		public string ManufacturerVariantName { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000046BB File Offset: 0x000028BB
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000046C3 File Offset: 0x000028C3
		[DataMember(Name = "operatorName", EmitDefaultValue = false)]
		public string OperatorName { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000046CC File Offset: 0x000028CC
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000046D4 File Offset: 0x000028D4
		[DataMember(Name = "packageClass")]
		public string PackageClass { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000046DD File Offset: 0x000028DD
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000046E5 File Offset: 0x000028E5
		[DataMember(Name = "packageRevision", EmitDefaultValue = false)]
		public string PackageRevision { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000046EE File Offset: 0x000028EE
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000046F6 File Offset: 0x000028F6
		[DataMember(Name = "packageState", EmitDefaultValue = false)]
		public string PackageState { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000046FF File Offset: 0x000028FF
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00004707 File Offset: 0x00002907
		[DataMember(Name = "packageSubRevision", EmitDefaultValue = false)]
		public string PackageSubRevision { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00004710 File Offset: 0x00002910
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00004718 File Offset: 0x00002918
		[DataMember(Name = "packageSubtitle", EmitDefaultValue = false)]
		public string PackageSubtitle { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004721 File Offset: 0x00002921
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00004729 File Offset: 0x00002929
		[DataMember(Name = "packageTitle", EmitDefaultValue = false)]
		public string PackageTitle { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004732 File Offset: 0x00002932
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000473A File Offset: 0x0000293A
		[DataMember(Name = "packageType")]
		public string PackageType { get; set; }
	}
}
