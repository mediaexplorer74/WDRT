using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200026D RID: 621
	internal class EpmValueCache
	{
		// Token: 0x0600147C RID: 5244 RVA: 0x0004C974 File Offset: 0x0004AB74
		internal EpmValueCache()
		{
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0004C97C File Offset: 0x0004AB7C
		internal static IEnumerable<ODataProperty> GetComplexValueProperties(EpmValueCache epmValueCache, ODataComplexValue complexValue, bool writingContent)
		{
			if (epmValueCache == null)
			{
				return complexValue.Properties;
			}
			return epmValueCache.GetComplexValueProperties(complexValue, writingContent);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0004C990 File Offset: 0x0004AB90
		internal IEnumerable<ODataProperty> CacheComplexValueProperties(ODataComplexValue complexValue)
		{
			if (complexValue == null)
			{
				return null;
			}
			IEnumerable<ODataProperty> properties = complexValue.Properties;
			List<ODataProperty> list = null;
			if (properties != null)
			{
				list = new List<ODataProperty>(properties);
			}
			if (this.epmValuesCache == null)
			{
				this.epmValuesCache = new Dictionary<object, object>(ReferenceEqualityComparer<object>.Instance);
			}
			this.epmValuesCache.Add(complexValue, list);
			return list;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0004C9DC File Offset: 0x0004ABDC
		private IEnumerable<ODataProperty> GetComplexValueProperties(ODataComplexValue complexValue, bool writingContent)
		{
			object obj;
			if (this.epmValuesCache != null && this.epmValuesCache.TryGetValue(complexValue, out obj))
			{
				return (IEnumerable<ODataProperty>)obj;
			}
			return complexValue.Properties;
		}

		// Token: 0x0400073C RID: 1852
		private Dictionary<object, object> epmValuesCache;
	}
}
