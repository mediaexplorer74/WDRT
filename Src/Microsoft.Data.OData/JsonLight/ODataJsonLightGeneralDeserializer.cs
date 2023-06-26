using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000116 RID: 278
	internal sealed class ODataJsonLightGeneralDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x000197F5 File Offset: 0x000179F5
		internal ODataJsonLightGeneralDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00019800 File Offset: 0x00017A00
		public object ReadValue()
		{
			if (base.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
			{
				return base.JsonReader.ReadPrimitiveValue();
			}
			if (base.JsonReader.NodeType == JsonNodeType.StartObject)
			{
				return this.ReadAsComplexValue();
			}
			if (base.JsonReader.NodeType == JsonNodeType.StartArray)
			{
				return this.ReadAsCollectionValue();
			}
			return null;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00019854 File Offset: 0x00017A54
		private ODataComplexValue ReadAsComplexValue()
		{
			base.JsonReader.ReadStartObject();
			List<ODataProperty> list = new List<ODataProperty>();
			while (base.JsonReader.NodeType != JsonNodeType.EndObject)
			{
				string text = base.JsonReader.ReadPropertyName();
				object obj = this.ReadValue();
				list.Add(new ODataProperty
				{
					Name = text,
					Value = obj
				});
			}
			base.JsonReader.ReadEndObject();
			return new ODataComplexValue
			{
				Properties = list,
				TypeName = null
			};
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000198D4 File Offset: 0x00017AD4
		private ODataCollectionValue ReadAsCollectionValue()
		{
			base.JsonReader.ReadStartArray();
			List<object> list = new List<object>();
			while (base.JsonReader.NodeType != JsonNodeType.EndArray)
			{
				object obj = this.ReadValue();
				list.Add(obj);
			}
			base.JsonReader.ReadEndArray();
			return new ODataCollectionValue
			{
				Items = list,
				TypeName = null
			};
		}
	}
}
