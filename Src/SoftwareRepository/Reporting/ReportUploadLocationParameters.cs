using System;
using System.Runtime.Serialization;

namespace SoftwareRepository.Reporting
{
	// Token: 0x0200001A RID: 26
	[DataContract]
	internal class ReportUploadLocationParameters
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003ECC File Offset: 0x000020CC
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003ED4 File Offset: 0x000020D4
		[DataMember(Name = "manufacturerName")]
		internal string ManufacturerName { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003EDD File Offset: 0x000020DD
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003EE5 File Offset: 0x000020E5
		[DataMember(Name = "manufacturerProductLine")]
		internal string ManufacturerProductLine { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003EEE File Offset: 0x000020EE
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003EF6 File Offset: 0x000020F6
		[DataMember(Name = "reportClassification")]
		internal string ReportClassification { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003EFF File Offset: 0x000020FF
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003F07 File Offset: 0x00002107
		[DataMember(Name = "fileName")]
		internal string FileName { get; set; }
	}
}
