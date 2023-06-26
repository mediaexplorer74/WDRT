using System;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000158 RID: 344
	public sealed class ODataPrimitiveValue : ODataValue
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x0001D353 File Offset: 0x0001B553
		public ODataPrimitiveValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(Strings.ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull, null);
			}
			if (!EdmLibraryExtensions.IsPrimitiveType(value.GetType()))
			{
				throw new ODataException(Strings.ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType(value.GetType()));
			}
			this.Value = value;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001D38F File Offset: 0x0001B58F
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0001D397 File Offset: 0x0001B597
		public object Value { get; private set; }
	}
}
