using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SoftwareRepository.Discovery
{
	// Token: 0x02000028 RID: 40
	[DataContract]
	public class SoftwarePackages
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00004B60 File Offset: 0x00002D60
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00004B68 File Offset: 0x00002D68
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[DataMember(Name = "softwarePackages")]
		public List<SoftwarePackage> SoftwarePackageList { get; set; }
	}
}
