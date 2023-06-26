using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x0200006F RID: 111
	internal static class ODataItemExtensions
	{
		// Token: 0x060003BF RID: 959 RVA: 0x00010200 File Offset: 0x0000E400
		public static object GetMaterializedValue(this ODataProperty property)
		{
			ODataAnnotatable odataAnnotatable = (property.Value as ODataAnnotatable) ?? property;
			return ODataItemExtensions.GetMaterializedValueCore(odataAnnotatable);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00010224 File Offset: 0x0000E424
		public static bool HasMaterializedValue(this ODataProperty property)
		{
			ODataAnnotatable odataAnnotatable = (property.Value as ODataAnnotatable) ?? property;
			return ODataItemExtensions.HasMaterializedValueCore(odataAnnotatable);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00010248 File Offset: 0x0000E448
		public static void SetMaterializedValue(this ODataProperty property, object materializedValue)
		{
			ODataAnnotatable odataAnnotatable = (property.Value as ODataAnnotatable) ?? property;
			ODataItemExtensions.SetMaterializedValueCore(odataAnnotatable, materializedValue);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001026D File Offset: 0x0000E46D
		public static object GetMaterializedValue(this ODataComplexValue complexValue)
		{
			return ODataItemExtensions.GetMaterializedValueCore(complexValue);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00010275 File Offset: 0x0000E475
		public static bool HasMaterializedValue(this ODataComplexValue complexValue)
		{
			return ODataItemExtensions.HasMaterializedValueCore(complexValue);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001027D File Offset: 0x0000E47D
		public static void SetMaterializedValue(this ODataComplexValue complexValue, object materializedValue)
		{
			ODataItemExtensions.SetMaterializedValueCore(complexValue, materializedValue);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00010288 File Offset: 0x0000E488
		private static object GetMaterializedValueCore(ODataAnnotatable annotatableObject)
		{
			ODataItemExtensions.MaterializerPropertyValue annotation = annotatableObject.GetAnnotation<ODataItemExtensions.MaterializerPropertyValue>();
			return annotation.Value;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000102A2 File Offset: 0x0000E4A2
		private static bool HasMaterializedValueCore(ODataAnnotatable annotatableObject)
		{
			return annotatableObject.GetAnnotation<ODataItemExtensions.MaterializerPropertyValue>() != null;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000102B0 File Offset: 0x0000E4B0
		private static void SetMaterializedValueCore(ODataAnnotatable annotatableObject, object materializedValue)
		{
			ODataItemExtensions.MaterializerPropertyValue materializerPropertyValue = new ODataItemExtensions.MaterializerPropertyValue
			{
				Value = materializedValue
			};
			annotatableObject.SetAnnotation<ODataItemExtensions.MaterializerPropertyValue>(materializerPropertyValue);
		}

		// Token: 0x02000070 RID: 112
		private class MaterializerPropertyValue
		{
			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x000102D3 File Offset: 0x0000E4D3
			// (set) Token: 0x060003C9 RID: 969 RVA: 0x000102DB File Offset: 0x0000E4DB
			public object Value { get; set; }
		}
	}
}
