using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000211 RID: 529
	internal sealed class EpmSourcePathSegment
	{
		// Token: 0x06001052 RID: 4178 RVA: 0x0003BE59 File Offset: 0x0003A059
		internal EpmSourcePathSegment()
			: this(null)
		{
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0003BE62 File Offset: 0x0003A062
		internal EpmSourcePathSegment(string propertyName)
		{
			this.propertyName = propertyName;
			this.subProperties = new List<EpmSourcePathSegment>();
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0003BE7C File Offset: 0x0003A07C
		internal string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0003BE84 File Offset: 0x0003A084
		internal List<EpmSourcePathSegment> SubProperties
		{
			get
			{
				return this.subProperties;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0003BE8C File Offset: 0x0003A08C
		// (set) Token: 0x06001057 RID: 4183 RVA: 0x0003BE94 File Offset: 0x0003A094
		internal EntityPropertyMappingInfo EpmInfo
		{
			get
			{
				return this.epmInfo;
			}
			set
			{
				this.epmInfo = value;
			}
		}

		// Token: 0x040005F8 RID: 1528
		private readonly string propertyName;

		// Token: 0x040005F9 RID: 1529
		private readonly List<EpmSourcePathSegment> subProperties;

		// Token: 0x040005FA RID: 1530
		private EntityPropertyMappingInfo epmInfo;
	}
}
