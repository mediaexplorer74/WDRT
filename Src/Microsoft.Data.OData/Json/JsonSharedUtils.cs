using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000147 RID: 327
	internal static class JsonSharedUtils
	{
		// Token: 0x060008D3 RID: 2259 RVA: 0x0001C66C File Offset: 0x0001A86C
		internal static bool IsDoubleValueSerializedAsString(double value)
		{
			return double.IsInfinity(value) || double.IsNaN(value);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001C680 File Offset: 0x0001A880
		internal static bool ValueTypeMatchesJsonType(ODataPrimitiveValue primitiveValue, IEdmPrimitiveTypeReference valueTypeReference)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = valueTypeReference.PrimitiveKind();
			if (edmPrimitiveTypeKind <= EdmPrimitiveTypeKind.Double)
			{
				if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.Boolean)
				{
					if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.Double)
					{
						return false;
					}
					double num = (double)primitiveValue.Value;
					return !JsonSharedUtils.IsDoubleValueSerializedAsString(num);
				}
			}
			else if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.Int32 && edmPrimitiveTypeKind != EdmPrimitiveTypeKind.String)
			{
				return false;
			}
			return true;
		}
	}
}
