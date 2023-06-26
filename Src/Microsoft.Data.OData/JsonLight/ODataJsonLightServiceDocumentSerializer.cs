using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x020001A3 RID: 419
	internal sealed class ODataJsonLightServiceDocumentSerializer : ODataJsonLightSerializer
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002C707 File Offset: 0x0002A907
		internal ODataJsonLightServiceDocumentSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext)
		{
			this.metadataUriBuilder = jsonLightOutputContext.CreateMetadataUriBuilder();
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002C88C File Offset: 0x0002AA8C
		internal void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			IEnumerable<ODataResourceCollectionInfo> collections = defaultWorkspace.Collections;
			base.WriteTopLevelPayload(delegate
			{
				this.JsonWriter.StartObjectScope();
				Uri uri;
				if (this.metadataUriBuilder.TryBuildServiceDocumentMetadataUri(out uri))
				{
					this.WriteMetadataUriProperty(uri);
				}
				this.JsonWriter.WriteValuePropertyName();
				this.JsonWriter.StartArrayScope();
				if (collections != null)
				{
					foreach (ODataResourceCollectionInfo odataResourceCollectionInfo in collections)
					{
						ValidationUtils.ValidateResourceCollectionInfo(odataResourceCollectionInfo);
						if (string.IsNullOrEmpty(odataResourceCollectionInfo.Name))
						{
							throw new ODataException(Strings.ODataJsonLightServiceDocumentSerializer_ResourceCollectionMustSpecifyName);
						}
						this.JsonWriter.StartObjectScope();
						this.JsonWriter.WriteName("name");
						this.JsonWriter.WriteValue(odataResourceCollectionInfo.Name);
						this.JsonWriter.WriteName("url");
						this.JsonWriter.WriteValue(this.UriToString(odataResourceCollectionInfo.Url));
						this.JsonWriter.EndObjectScope();
					}
				}
				this.JsonWriter.EndArrayScope();
				this.JsonWriter.EndObjectScope();
			});
		}

		// Token: 0x04000451 RID: 1105
		private readonly ODataJsonLightMetadataUriBuilder metadataUriBuilder;
	}
}
