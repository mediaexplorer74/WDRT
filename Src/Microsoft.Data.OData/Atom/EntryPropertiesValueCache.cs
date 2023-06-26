using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200026E RID: 622
	internal sealed class EntryPropertiesValueCache : EpmValueCache
	{
		// Token: 0x06001480 RID: 5248 RVA: 0x0004CA0E File Offset: 0x0004AC0E
		internal EntryPropertiesValueCache(ODataEntry entry)
		{
			if (entry.Properties != null)
			{
				this.entryPropertiesCache = new List<ODataProperty>(entry.Properties);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0004CA47 File Offset: 0x0004AC47
		internal IEnumerable<ODataProperty> EntryProperties
		{
			get
			{
				if (this.entryPropertiesCache == null)
				{
					return null;
				}
				return this.entryPropertiesCache.Where((ODataProperty p) => p == null || !(p.Value is ODataStreamReferenceValue));
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0004CA90 File Offset: 0x0004AC90
		internal IEnumerable<ODataProperty> EntryStreamProperties
		{
			get
			{
				if (this.entryPropertiesCache == null)
				{
					return null;
				}
				return this.entryPropertiesCache.Where((ODataProperty p) => p != null && p.Value is ODataStreamReferenceValue);
			}
		}

		// Token: 0x0400073D RID: 1853
		private readonly List<ODataProperty> entryPropertiesCache;
	}
}
