using System;
using System.Runtime.Serialization;

namespace SoftwareRepository.Discovery
{
	// Token: 0x02000026 RID: 38
	[DataContract]
	public class SoftwareFileChecksum
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000049D9 File Offset: 0x00002BD9
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000049E1 File Offset: 0x00002BE1
		[DataMember(Name = "type")]
		public string ChecksumType { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000049EA File Offset: 0x00002BEA
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000049F2 File Offset: 0x00002BF2
		[DataMember(Name = "value")]
		public string Value { get; set; }
	}
}
