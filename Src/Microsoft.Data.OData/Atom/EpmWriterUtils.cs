using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200026F RID: 623
	internal static class EpmWriterUtils
	{
		// Token: 0x06001485 RID: 5253 RVA: 0x0004CAC4 File Offset: 0x0004ACC4
		internal static string GetPropertyValueAsText(object propertyValue)
		{
			if (propertyValue == null)
			{
				return null;
			}
			return AtomValueUtils.ConvertPrimitiveToString(propertyValue);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0004CAD4 File Offset: 0x0004ACD4
		internal static EntityPropertyMappingAttribute GetEntityPropertyMapping(EpmSourcePathSegment epmParentSourcePathSegment, string propertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(propertyName, "propertyName");
			EpmSourcePathSegment propertySourcePathSegment = EpmWriterUtils.GetPropertySourcePathSegment(epmParentSourcePathSegment, propertyName);
			return EpmWriterUtils.GetEntityPropertyMapping(propertySourcePathSegment);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0004CAFC File Offset: 0x0004ACFC
		internal static EntityPropertyMappingAttribute GetEntityPropertyMapping(EpmSourcePathSegment epmSourcePathSegment)
		{
			if (epmSourcePathSegment == null)
			{
				return null;
			}
			EntityPropertyMappingInfo epmInfo = epmSourcePathSegment.EpmInfo;
			if (epmInfo == null)
			{
				return null;
			}
			return epmInfo.Attribute;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0004CB40 File Offset: 0x0004AD40
		internal static EpmSourcePathSegment GetPropertySourcePathSegment(EpmSourcePathSegment epmParentSourcePathSegment, string propertyName)
		{
			EpmSourcePathSegment epmSourcePathSegment = null;
			if (epmParentSourcePathSegment != null)
			{
				epmSourcePathSegment = epmParentSourcePathSegment.SubProperties.FirstOrDefault((EpmSourcePathSegment subProperty) => subProperty.PropertyName == propertyName);
			}
			return epmSourcePathSegment;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0004CB80 File Offset: 0x0004AD80
		internal static void CacheEpmProperties(EntryPropertiesValueCache propertyValueCache, EpmSourceTree sourceTree)
		{
			EpmSourcePathSegment root = sourceTree.Root;
			EpmWriterUtils.CacheEpmSourcePathSegments(propertyValueCache, root.SubProperties, propertyValueCache.EntryProperties);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0004CBA8 File Offset: 0x0004ADA8
		private static void CacheEpmSourcePathSegments(EpmValueCache valueCache, List<EpmSourcePathSegment> segments, IEnumerable<ODataProperty> properties)
		{
			if (properties == null)
			{
				return;
			}
			foreach (EpmSourcePathSegment epmSourcePathSegment in segments)
			{
				ODataComplexValue odataComplexValue;
				if (epmSourcePathSegment.EpmInfo == null && EpmWriterUtils.TryGetPropertyValue<ODataComplexValue>(properties, epmSourcePathSegment.PropertyName, out odataComplexValue))
				{
					IEnumerable<ODataProperty> enumerable = valueCache.CacheComplexValueProperties(odataComplexValue);
					EpmWriterUtils.CacheEpmSourcePathSegments(valueCache, epmSourcePathSegment.SubProperties, enumerable);
				}
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0004CC40 File Offset: 0x0004AE40
		private static bool TryGetPropertyValue<T>(IEnumerable<ODataProperty> properties, string propertyName, out T propertyValue) where T : class
		{
			propertyValue = default(T);
			ODataProperty odataProperty = properties.Where((ODataProperty p) => string.CompareOrdinal(p.Name, propertyName) == 0).FirstOrDefault<ODataProperty>();
			if (odataProperty != null)
			{
				propertyValue = odataProperty.Value as T;
				return propertyValue != null || odataProperty.Value == null;
			}
			return false;
		}
	}
}
