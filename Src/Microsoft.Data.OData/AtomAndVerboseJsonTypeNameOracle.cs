using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x020000E5 RID: 229
	internal sealed class AtomAndVerboseJsonTypeNameOracle : TypeNameOracle
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x00013C68 File Offset: 0x00011E68
		internal string GetEntryTypeNameForWriting(ODataEntry entry)
		{
			SerializationTypeNameAnnotation annotation = entry.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null)
			{
				return annotation.TypeName;
			}
			return entry.TypeName;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00013C8C File Offset: 0x00011E8C
		internal string GetValueTypeNameForWriting(object value, IEdmTypeReference typeReferenceFromValue, SerializationTypeNameAnnotation typeNameAnnotation, CollectionWithoutExpectedTypeValidator collectionValidator, out string collectionItemTypeName)
		{
			collectionItemTypeName = null;
			string text = TypeNameOracle.GetTypeNameFromValue(value);
			if (text == null && typeReferenceFromValue != null)
			{
				text = typeReferenceFromValue.ODataFullName();
			}
			if (text != null)
			{
				if (collectionValidator != null && string.CompareOrdinal(collectionValidator.ItemTypeNameFromCollection, text) == 0)
				{
					text = null;
				}
				if (text != null && value is ODataCollectionValue)
				{
					collectionItemTypeName = ValidationUtils.ValidateCollectionTypeName(text);
				}
			}
			if (typeNameAnnotation != null)
			{
				text = typeNameAnnotation.TypeName;
			}
			return text;
		}
	}
}
