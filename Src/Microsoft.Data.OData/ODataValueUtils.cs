using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200015F RID: 351
	internal static class ODataValueUtils
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x0001DD30 File Offset: 0x0001BF30
		internal static ODataValue ToODataValue(this object objectToConvert)
		{
			if (objectToConvert == null)
			{
				return new ODataNullValue();
			}
			ODataValue odataValue = objectToConvert as ODataValue;
			if (odataValue != null)
			{
				return odataValue;
			}
			return new ODataPrimitiveValue(objectToConvert);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001DD58 File Offset: 0x0001BF58
		internal static object FromODataValue(this ODataValue odataValue)
		{
			if (odataValue is ODataNullValue)
			{
				return null;
			}
			ODataPrimitiveValue odataPrimitiveValue = odataValue as ODataPrimitiveValue;
			if (odataPrimitiveValue != null)
			{
				return odataPrimitiveValue.Value;
			}
			return odataValue;
		}
	}
}
