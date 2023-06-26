using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SoftwareRepository.Discovery
{
	// Token: 0x02000027 RID: 39
	[DataContract]
	public class SoftwarePackage
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000049FB File Offset: 0x00002BFB
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00004A03 File Offset: 0x00002C03
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[DataMember(Name = "customerName")]
		public List<string> CustomerName { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004A0C File Offset: 0x00002C0C
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00004A14 File Offset: 0x00002C14
		[DataMember(Name = "extendedAttributes")]
		public ExtendedAttributes ExtendedAttributes { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004A1D File Offset: 0x00002C1D
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004A25 File Offset: 0x00002C25
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[DataMember(Name = "files")]
		public List<SoftwareFile> Files { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004A2E File Offset: 0x00002C2E
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00004A36 File Offset: 0x00002C36
		[DataMember(Name = "id")]
		public string Id { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004A3F File Offset: 0x00002C3F
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00004A47 File Offset: 0x00002C47
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[DataMember(Name = "manufacturerHardwareModel")]
		public List<string> ManufacturerHardwareModel { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004A50 File Offset: 0x00002C50
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00004A58 File Offset: 0x00002C58
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[DataMember(Name = "manufacturerHardwareVariant")]
		public List<string> ManufacturerHardwareVariant { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004A61 File Offset: 0x00002C61
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00004A69 File Offset: 0x00002C69
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[DataMember(Name = "manufacturerModelName")]
		public List<string> ManufacturerModelName { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004A72 File Offset: 0x00002C72
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00004A7A File Offset: 0x00002C7A
		[DataMember(Name = "manufacturerName")]
		public string ManufacturerName { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004A83 File Offset: 0x00002C83
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00004A8B File Offset: 0x00002C8B
		[DataMember(Name = "manufacturerPackageId")]
		public string ManufacturerPackageId { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00004A94 File Offset: 0x00002C94
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00004A9C File Offset: 0x00002C9C
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[DataMember(Name = "manufacturerPlatformId")]
		public List<string> ManufacturerPlatformId { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004AA5 File Offset: 0x00002CA5
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00004AAD File Offset: 0x00002CAD
		[DataMember(Name = "manufacturerProductLine")]
		public string ManufacturerProductLine { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004AB6 File Offset: 0x00002CB6
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00004ABE File Offset: 0x00002CBE
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[DataMember(Name = "manufacturerVariantName")]
		public List<string> ManufacturerVariantName { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004AC7 File Offset: 0x00002CC7
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00004ACF File Offset: 0x00002CCF
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[DataMember(Name = "operatorName")]
		public List<string> OperatorName { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00004AD8 File Offset: 0x00002CD8
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00004AE0 File Offset: 0x00002CE0
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[DataMember(Name = "packageClass")]
		public List<string> PackageClass { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00004AE9 File Offset: 0x00002CE9
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00004AF1 File Offset: 0x00002CF1
		[DataMember(Name = "packageDescription")]
		public string PackageDescription { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004AFA File Offset: 0x00002CFA
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00004B02 File Offset: 0x00002D02
		[DataMember(Name = "packageRevision")]
		public string PackageRevision { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00004B0B File Offset: 0x00002D0B
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00004B13 File Offset: 0x00002D13
		[DataMember(Name = "packageState")]
		public string PackageState { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00004B1C File Offset: 0x00002D1C
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00004B24 File Offset: 0x00002D24
		[DataMember(Name = "packageSubRevision")]
		public string PackageSubRevision { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004B2D File Offset: 0x00002D2D
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00004B35 File Offset: 0x00002D35
		[DataMember(Name = "packageSubtitle")]
		public string PackageSubtitle { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004B3E File Offset: 0x00002D3E
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00004B46 File Offset: 0x00002D46
		[DataMember(Name = "packageTitle")]
		public string PackageTitle { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00004B4F File Offset: 0x00002D4F
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00004B57 File Offset: 0x00002D57
		[DataMember(Name = "packageType")]
		public string PackageType { get; set; }
	}
}
