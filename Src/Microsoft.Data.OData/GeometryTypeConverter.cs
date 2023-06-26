using System;
using System.Collections.Generic;
using System.Spatial;
using System.Xml;
using Microsoft.Data.OData.Atom;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData
{
	// Token: 0x020001E0 RID: 480
	internal sealed class GeometryTypeConverter : IPrimitiveTypeConverter
	{
		// Token: 0x06000EDB RID: 3803 RVA: 0x00034ACC File Offset: 0x00032CCC
		public object TokenizeFromXml(XmlReader reader)
		{
			reader.ReadStartElement();
			Geometry geometry = GmlFormatter.Create().Read<Geometry>(reader);
			reader.SkipInsignificantNodes();
			return geometry;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00034AF2 File Offset: 0x00032CF2
		public void WriteAtom(object instance, XmlWriter writer)
		{
			((Geometry)instance).SendTo(GmlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00034B28 File Offset: 0x00032D28
		public void WriteVerboseJson(object instance, IJsonWriter jsonWriter, string typeName, ODataVersion odataVersion)
		{
			IDictionary<string, object> dictionary = GeoJsonObjectFormatter.Create().Write((ISpatial)instance);
			jsonWriter.WriteJsonObjectValue(dictionary, delegate(IJsonWriter jw)
			{
				ODataJsonWriterUtils.WriteMetadataWithTypeName(jw, typeName);
			}, odataVersion);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00034B68 File Offset: 0x00032D68
		public void WriteJsonLight(object instance, IJsonWriter jsonWriter, ODataVersion odataVersion)
		{
			IDictionary<string, object> dictionary = GeoJsonObjectFormatter.Create().Write((ISpatial)instance);
			jsonWriter.WriteJsonObjectValue(dictionary, null, odataVersion);
		}
	}
}
