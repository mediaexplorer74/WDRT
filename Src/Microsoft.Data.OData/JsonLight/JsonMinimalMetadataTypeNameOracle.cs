using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000111 RID: 273
	internal sealed class JsonMinimalMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x0001902C File Offset: 0x0001722C
		internal override string GetEntryTypeNameForWriting(string expectedTypeName, ODataEntry entry)
		{
			SerializationTypeNameAnnotation annotation = entry.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null)
			{
				return annotation.TypeName;
			}
			string typeName = entry.TypeName;
			if (expectedTypeName != typeName)
			{
				return typeName;
			}
			return null;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00019060 File Offset: 0x00017260
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			SerializationTypeNameAnnotation annotation = value.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null)
			{
				return annotation.TypeName;
			}
			if (typeReferenceFromValue != null)
			{
				if (typeReferenceFromMetadata != null && typeReferenceFromMetadata.ODataFullName() != typeReferenceFromValue.ODataFullName())
				{
					return typeReferenceFromValue.ODataFullName();
				}
				if (typeReferenceFromValue.IsPrimitive() && JsonSharedUtils.ValueTypeMatchesJsonType((ODataPrimitiveValue)value, typeReferenceFromValue.AsPrimitive()))
				{
					return null;
				}
			}
			if (!isOpenProperty)
			{
				return null;
			}
			return TypeNameOracle.GetTypeNameFromValue(value);
		}
	}
}
