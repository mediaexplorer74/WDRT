using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x0200017F RID: 383
	internal static class ODataEdmValueUtils
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x00024040 File Offset: 0x00022240
		internal static IEdmPropertyValue GetEdmPropertyValue(this ODataProperty property, IEdmStructuredTypeReference declaringType)
		{
			IEdmTypeReference edmTypeReference = null;
			if (declaringType != null)
			{
				IEdmProperty edmProperty = declaringType.FindProperty(property.Name);
				if (edmProperty == null && !declaringType.IsOpen())
				{
					throw new ODataException(Strings.ODataEdmStructuredValue_UndeclaredProperty(property.Name, declaringType.FullName()));
				}
				edmTypeReference = ((edmProperty == null) ? null : edmProperty.Type);
			}
			return new EdmPropertyValue(property.Name, ODataEdmValueUtils.ConvertValue(property.Value, edmTypeReference).Value);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000240AC File Offset: 0x000222AC
		internal static IEdmDelayedValue ConvertValue(object value, IEdmTypeReference type)
		{
			if (value == null)
			{
				if (type != null)
				{
					return new ODataEdmNullValue(type);
				}
				return ODataEdmNullValue.UntypedInstance;
			}
			else
			{
				ODataComplexValue odataComplexValue = value as ODataComplexValue;
				if (odataComplexValue != null)
				{
					return new ODataEdmStructuredValue(odataComplexValue);
				}
				ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					return new ODataEdmCollectionValue(odataCollectionValue);
				}
				return EdmValueUtils.ConvertPrimitiveValue(value, (type == null) ? null : type.AsPrimitive());
			}
		}
	}
}
