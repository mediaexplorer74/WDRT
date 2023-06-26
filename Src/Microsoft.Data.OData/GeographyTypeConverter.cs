using System;
using System.Collections.Generic;
using System.Spatial;
using System.Xml;
using Microsoft.Data.OData.Atom;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData
{
	// Token: 0x0200021F RID: 543
	internal sealed class GeographyTypeConverter : IPrimitiveTypeConverter
	{
		// Token: 0x060010EE RID: 4334 RVA: 0x0003FB84 File Offset: 0x0003DD84
		public object TokenizeFromXml(XmlReader reader)
		{
			reader.ReadStartElement();
			Geography geography = GmlFormatter.Create().Read<Geography>(reader);
			reader.SkipInsignificantNodes();
			return geography;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0003FBAA File Offset: 0x0003DDAA
		public void WriteAtom(object instance, XmlWriter writer)
		{
			((Geography)instance).SendTo(GmlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0003FBE0 File Offset: 0x0003DDE0
		public void WriteVerboseJson(object instance, IJsonWriter jsonWriter, string typeName, ODataVersion odataVersion)
		{
			IDictionary<string, object> dictionary = GeoJsonObjectFormatter.Create().Write((ISpatial)instance);
			jsonWriter.WriteJsonObjectValue(dictionary, delegate(IJsonWriter jw)
			{
				ODataJsonWriterUtils.WriteMetadataWithTypeName(jw, typeName);
			}, odataVersion);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0003FC20 File Offset: 0x0003DE20
		public void WriteJsonLight(object instance, IJsonWriter jsonWriter, ODataVersion odataVersion)
		{
			IDictionary<string, object> dictionary = GeoJsonObjectFormatter.Create().Write((ISpatial)instance);
			jsonWriter.WriteJsonObjectValue(dictionary, null, odataVersion);
		}
	}
}
