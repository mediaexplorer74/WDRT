using System;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x020001B1 RID: 433
	internal static class ODataJsonLightWriterUtils
	{
		// Token: 0x06000D74 RID: 3444 RVA: 0x0002E854 File Offset: 0x0002CA54
		internal static void WriteODataTypeInstanceAnnotation(IJsonWriter jsonWriter, string typeName)
		{
			jsonWriter.WriteName("odata.type");
			jsonWriter.WriteValue(typeName);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0002E868 File Offset: 0x0002CA68
		internal static void WriteODataTypePropertyAnnotation(IJsonWriter jsonWriter, string propertyName, string typeName)
		{
			jsonWriter.WritePropertyAnnotationName(propertyName, "odata.type");
			jsonWriter.WriteValue(typeName);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0002E87D File Offset: 0x0002CA7D
		internal static void WriteValuePropertyName(this IJsonWriter jsonWriter)
		{
			jsonWriter.WriteName("value");
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0002E88A File Offset: 0x0002CA8A
		internal static void WritePropertyAnnotationName(this IJsonWriter jsonWriter, string propertyName, string annotationName)
		{
			jsonWriter.WriteName(propertyName + '@' + annotationName);
		}
	}
}
