using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000239 RID: 569
	internal sealed class ODataVerboseJsonServiceDocumentDeserializer : ODataVerboseJsonDeserializer
	{
		// Token: 0x06001244 RID: 4676 RVA: 0x00044E06 File Offset: 0x00043006
		internal ODataVerboseJsonServiceDocumentDeserializer(ODataVerboseJsonInputContext verboseJsonInputContext)
			: base(verboseJsonInputContext)
		{
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00044E10 File Offset: 0x00043010
		internal ODataWorkspace ReadServiceDocument()
		{
			List<ODataResourceCollectionInfo> list = null;
			base.ReadPayloadStart(false);
			base.JsonReader.ReadStartObject();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("EntitySets", text) == 0)
				{
					if (list != null)
					{
						throw new ODataException(Strings.ODataJsonServiceDocumentDeserializer_MultipleEntitySetsPropertiesForServiceDocument);
					}
					list = new List<ODataResourceCollectionInfo>();
					base.JsonReader.ReadStartArray();
					while (base.JsonReader.NodeType != JsonNodeType.EndArray)
					{
						string text2 = base.JsonReader.ReadStringValue();
						ValidationUtils.ValidateResourceCollectionInfoUrl(text2);
						ODataResourceCollectionInfo odataResourceCollectionInfo = new ODataResourceCollectionInfo
						{
							Url = base.ProcessUriFromPayload(text2, false)
						};
						list.Add(odataResourceCollectionInfo);
					}
					base.JsonReader.ReadEndArray();
				}
				else
				{
					base.JsonReader.SkipValue();
				}
			}
			if (list == null)
			{
				throw new ODataException(Strings.ODataJsonServiceDocumentDeserializer_NoEntitySetsPropertyForServiceDocument);
			}
			base.JsonReader.ReadEndObject();
			base.ReadPayloadEnd(false);
			return new ODataWorkspace
			{
				Collections = new ReadOnlyEnumerable<ODataResourceCollectionInfo>(list)
			};
		}
	}
}
