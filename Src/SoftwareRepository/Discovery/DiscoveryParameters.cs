using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SoftwareRepository.Discovery
{
	// Token: 0x02000023 RID: 35
	[DataContract]
	public class DiscoveryParameters
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00004765 File Offset: 0x00002965
		public DiscoveryParameters()
			: this(DiscoveryCondition.Default)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004770 File Offset: 0x00002970
		public DiscoveryParameters(DiscoveryCondition condition)
		{
			this.APIVersion = "1";
			this.Query = new DiscoveryQueryParameters();
			this.Condition = new List<string>();
			bool flag = condition == DiscoveryCondition.All;
			if (flag)
			{
				this.Condition.Add("all");
			}
			else
			{
				bool flag2 = condition == DiscoveryCondition.Latest;
				if (flag2)
				{
					this.Condition.Add("latest");
				}
				else
				{
					this.Condition.Add("default");
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000047F6 File Offset: 0x000029F6
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000047FE File Offset: 0x000029FE
		[DataMember(Name = "api-version")]
		public string APIVersion { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004807 File Offset: 0x00002A07
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000480F File Offset: 0x00002A0F
		[DataMember(Name = "query")]
		public DiscoveryQueryParameters Query { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004818 File Offset: 0x00002A18
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00004820 File Offset: 0x00002A20
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[DataMember(Name = "condition")]
		public List<string> Condition { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004829 File Offset: 0x00002A29
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00004831 File Offset: 0x00002A31
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[DataMember(Name = "response")]
		public List<string> Response { get; set; }
	}
}
