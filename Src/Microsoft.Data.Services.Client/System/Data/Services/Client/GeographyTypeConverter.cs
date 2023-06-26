using System;
using System.Spatial;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x020000A8 RID: 168
	internal sealed class GeographyTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x00015254 File Offset: 0x00013454
		internal override PrimitiveParserToken TokenizeFromXml(XmlReader reader)
		{
			reader.ReadStartElement();
			return new InstancePrimitiveParserToken<Geography>(GmlFormatter.Create().Read<Geography>(reader));
		}
	}
}
