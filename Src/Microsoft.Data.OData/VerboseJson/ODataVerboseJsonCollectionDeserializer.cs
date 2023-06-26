using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000233 RID: 563
	internal sealed class ODataVerboseJsonCollectionDeserializer : ODataVerboseJsonPropertyAndValueDeserializer
	{
		// Token: 0x060011EF RID: 4591 RVA: 0x00042C73 File Offset: 0x00040E73
		internal ODataVerboseJsonCollectionDeserializer(ODataVerboseJsonInputContext jsonInputContext)
			: base(jsonInputContext)
		{
			this.duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00042C88 File Offset: 0x00040E88
		internal ODataCollectionStart ReadCollectionStart(bool isResultsWrapperExpected)
		{
			if (isResultsWrapperExpected)
			{
				base.JsonReader.ReadStartObject();
				bool flag = false;
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = base.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal("results", text) == 0)
					{
						flag = true;
						break;
					}
					base.JsonReader.SkipValue();
				}
				if (!flag)
				{
					throw new ODataException(Strings.ODataJsonCollectionDeserializer_MissingResultsPropertyForCollection);
				}
			}
			if (base.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonCollectionDeserializer_CannotReadCollectionContentStart(base.JsonReader.NodeType));
			}
			return new ODataCollectionStart
			{
				Name = null
			};
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00042D24 File Offset: 0x00040F24
		internal object ReadCollectionItem(IEdmTypeReference expectedItemTypeReference, CollectionWithoutExpectedTypeValidator collectionValidator)
		{
			return base.ReadNonEntityValue(expectedItemTypeReference, this.duplicatePropertyNamesChecker, collectionValidator, true, null);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00042D44 File Offset: 0x00040F44
		internal void ReadCollectionEnd(bool isResultsWrapperExpected)
		{
			base.JsonReader.ReadEndArray();
			if (!isResultsWrapperExpected)
			{
				return;
			}
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("results", text) == 0)
				{
					throw new ODataException(Strings.ODataJsonCollectionDeserializer_MultipleResultsPropertiesForCollection);
				}
				base.JsonReader.SkipValue();
			}
			base.JsonReader.ReadEndObject();
		}

		// Token: 0x04000690 RID: 1680
		private readonly DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
	}
}
