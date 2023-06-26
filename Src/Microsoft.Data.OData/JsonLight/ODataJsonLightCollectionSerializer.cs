using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000184 RID: 388
	internal sealed class ODataJsonLightCollectionSerializer : ODataJsonLightValueSerializer
	{
		// Token: 0x06000AEE RID: 2798 RVA: 0x00024967 File Offset: 0x00022B67
		internal ODataJsonLightCollectionSerializer(ODataJsonLightOutputContext jsonLightOutputContext, bool writingTopLevelCollection)
			: base(jsonLightOutputContext)
		{
			this.writingTopLevelCollection = writingTopLevelCollection;
			this.metadataUriBuilder = jsonLightOutputContext.CreateMetadataUriBuilder();
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00024984 File Offset: 0x00022B84
		internal void WriteCollectionStart(ODataCollectionStart collectionStart, IEdmTypeReference itemTypeReference)
		{
			if (this.writingTopLevelCollection)
			{
				base.JsonWriter.StartObjectScope();
				Uri uri;
				if (this.metadataUriBuilder.TryBuildCollectionMetadataUri(collectionStart.SerializationInfo, itemTypeReference, out uri))
				{
					base.WriteMetadataUriProperty(uri);
				}
				base.JsonWriter.WriteValuePropertyName();
			}
			base.JsonWriter.StartArrayScope();
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000249D7 File Offset: 0x00022BD7
		internal void WriteCollectionEnd()
		{
			base.JsonWriter.EndArrayScope();
			if (this.writingTopLevelCollection)
			{
				base.JsonWriter.EndObjectScope();
			}
		}

		// Token: 0x04000405 RID: 1029
		private readonly bool writingTopLevelCollection;

		// Token: 0x04000406 RID: 1030
		private readonly ODataJsonLightMetadataUriBuilder metadataUriBuilder;
	}
}
