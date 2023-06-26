using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200010F RID: 271
	internal sealed class JsonFullMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x06000750 RID: 1872 RVA: 0x00018FA4 File Offset: 0x000171A4
		internal override string GetEntryTypeNameForWriting(string expectedTypeName, ODataEntry entry)
		{
			SerializationTypeNameAnnotation annotation = entry.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null)
			{
				return annotation.TypeName;
			}
			return entry.TypeName;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00018FC8 File Offset: 0x000171C8
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			SerializationTypeNameAnnotation annotation = value.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null)
			{
				return annotation.TypeName;
			}
			if (typeReferenceFromValue != null && typeReferenceFromValue.IsPrimitive() && JsonSharedUtils.ValueTypeMatchesJsonType((ODataPrimitiveValue)value, typeReferenceFromValue.AsPrimitive()))
			{
				return null;
			}
			return TypeNameOracle.GetTypeNameFromValue(value);
		}
	}
}
