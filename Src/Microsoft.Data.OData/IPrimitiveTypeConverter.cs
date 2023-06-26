using System;
using System.Xml;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData
{
	// Token: 0x020001DF RID: 479
	internal interface IPrimitiveTypeConverter
	{
		// Token: 0x06000ED7 RID: 3799
		object TokenizeFromXml(XmlReader reader);

		// Token: 0x06000ED8 RID: 3800
		void WriteAtom(object instance, XmlWriter writer);

		// Token: 0x06000ED9 RID: 3801
		void WriteVerboseJson(object instance, IJsonWriter jsonWriter, string typeName, ODataVersion odataVersion);

		// Token: 0x06000EDA RID: 3802
		void WriteJsonLight(object instance, IJsonWriter jsonWriter, ODataVersion odataVersion);
	}
}
