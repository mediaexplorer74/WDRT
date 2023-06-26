using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200010E RID: 270
	internal abstract class JsonLightTypeNameOracle : TypeNameOracle
	{
		// Token: 0x0600074D RID: 1869
		internal abstract string GetEntryTypeNameForWriting(string expectedTypeName, ODataEntry entry);

		// Token: 0x0600074E RID: 1870
		internal abstract string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty);
	}
}
