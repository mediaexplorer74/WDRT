using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SoftwareRepository.Streaming
{
	// Token: 0x02000012 RID: 18
	[DataContract]
	public class UrlSelectionResult
	{
		// Token: 0x06000064 RID: 100 RVA: 0x000035F0 File Offset: 0x000017F0
		public UrlSelectionResult()
		{
			this.UrlResults = new List<UrlResult>();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003606 File Offset: 0x00001806
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000360E File Offset: 0x0000180E
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[DataMember(Name = "urlResults")]
		public List<UrlResult> UrlResults { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003617 File Offset: 0x00001817
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000361F File Offset: 0x0000181F
		[DataMember(Name = "testBytes")]
		public long TestBytes { get; set; }
	}
}
