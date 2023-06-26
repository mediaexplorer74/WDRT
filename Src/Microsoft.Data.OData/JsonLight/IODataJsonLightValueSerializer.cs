using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200013E RID: 318
	internal interface IODataJsonLightValueSerializer
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000879 RID: 2169
		IJsonWriter JsonWriter { get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600087A RID: 2170
		ODataVersion Version { get; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600087B RID: 2171
		IEdmModel Model { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600087C RID: 2172
		ODataMessageWriterSettings Settings { get; }

		// Token: 0x0600087D RID: 2173
		void WriteNullValue();

		// Token: 0x0600087E RID: 2174
		void WriteComplexValue(ODataComplexValue complexValue, IEdmTypeReference metadataTypeReference, bool isTopLevel, bool isOpenPropertyType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker);

		// Token: 0x0600087F RID: 2175
		void WriteCollectionValue(ODataCollectionValue collectionValue, IEdmTypeReference metadataTypeReference, bool isTopLevelProperty, bool isInUri, bool isOpenPropertyType);

		// Token: 0x06000880 RID: 2176
		void WritePrimitiveValue(object value, IEdmTypeReference expectedTypeReference);

		// Token: 0x06000881 RID: 2177
		DuplicatePropertyNamesChecker CreateDuplicatePropertyNamesChecker();
	}
}
