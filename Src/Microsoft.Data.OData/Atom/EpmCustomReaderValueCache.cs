using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001F8 RID: 504
	internal sealed class EpmCustomReaderValueCache
	{
		// Token: 0x06000F69 RID: 3945 RVA: 0x00037510 File Offset: 0x00035710
		internal EpmCustomReaderValueCache()
		{
			this.customEpmValues = new List<KeyValuePair<EntityPropertyMappingInfo, string>>();
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00037523 File Offset: 0x00035723
		internal IEnumerable<KeyValuePair<EntityPropertyMappingInfo, string>> CustomEpmValues
		{
			get
			{
				return this.customEpmValues;
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00037548 File Offset: 0x00035748
		internal bool Contains(EntityPropertyMappingInfo epmInfo)
		{
			return this.customEpmValues.Any((KeyValuePair<EntityPropertyMappingInfo, string> epmValue) => object.ReferenceEquals(epmValue.Key, epmInfo));
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00037579 File Offset: 0x00035779
		internal void Add(EntityPropertyMappingInfo epmInfo, string value)
		{
			this.customEpmValues.Add(new KeyValuePair<EntityPropertyMappingInfo, string>(epmInfo, value));
		}

		// Token: 0x04000574 RID: 1396
		private readonly List<KeyValuePair<EntityPropertyMappingInfo, string>> customEpmValues;
	}
}
