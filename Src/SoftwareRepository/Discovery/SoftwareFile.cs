using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SoftwareRepository.Discovery
{
	// Token: 0x02000025 RID: 37
	[DataContract]
	public class SoftwareFile
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004995 File Offset: 0x00002B95
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000499D File Offset: 0x00002B9D
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[DataMember(Name = "checksum")]
		public List<SoftwareFileChecksum> Checksum { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000049A6 File Offset: 0x00002BA6
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000049AE File Offset: 0x00002BAE
		[DataMember(Name = "fileName")]
		public string FileName { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000049B7 File Offset: 0x00002BB7
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000049BF File Offset: 0x00002BBF
		[DataMember(Name = "fileSize")]
		public long FileSize { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000049C8 File Offset: 0x00002BC8
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000049D0 File Offset: 0x00002BD0
		[DataMember(Name = "fileType")]
		public string FileType { get; set; }
	}
}
