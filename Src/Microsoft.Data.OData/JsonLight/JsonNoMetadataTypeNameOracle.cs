using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000113 RID: 275
	internal sealed class JsonNoMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x000190FA File Offset: 0x000172FA
		internal override string GetEntryTypeNameForWriting(string expectedTypeName, ODataEntry entry)
		{
			return null;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000190FD File Offset: 0x000172FD
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			return null;
		}
	}
}
