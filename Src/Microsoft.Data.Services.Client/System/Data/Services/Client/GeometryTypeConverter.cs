using System;
using System.Spatial;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x020000A9 RID: 169
	internal sealed class GeometryTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600058A RID: 1418 RVA: 0x00015284 File Offset: 0x00013484
		internal override PrimitiveParserToken TokenizeFromXml(XmlReader reader)
		{
			reader.ReadStartElement();
			return new InstancePrimitiveParserToken<Geometry>(GmlFormatter.Create().Read<Geometry>(reader));
		}
	}
}
