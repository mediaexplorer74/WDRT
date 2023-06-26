using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200018C RID: 396
	internal sealed class ODataJsonLightEntityReferenceLinkSerializer : ODataJsonLightSerializer
	{
		// Token: 0x06000B3F RID: 2879 RVA: 0x00025955 File Offset: 0x00023B55
		internal ODataJsonLightEntityReferenceLinkSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext)
		{
			this.metadataUriBuilder = jsonLightOutputContext.CreateMetadataUriBuilder();
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00025994 File Offset: 0x00023B94
		internal void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinkImplementation(link, entitySet, navigationProperty, true);
			});
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x000259FC File Offset: 0x00023BFC
		internal void WriteEntityReferenceLinks(ODataEntityReferenceLinks entityReferenceLinks, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinksImplementation(entityReferenceLinks, entitySet, navigationProperty);
			});
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00025A40 File Offset: 0x00023C40
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink entityReferenceLink, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, bool isTopLevel)
		{
			WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
			base.JsonWriter.StartObjectScope();
			Uri uri;
			if (isTopLevel && this.metadataUriBuilder.TryBuildEntityReferenceLinkMetadataUri(entityReferenceLink.SerializationInfo, entitySet, navigationProperty, out uri))
			{
				base.WriteMetadataUriProperty(uri);
			}
			base.JsonWriter.WriteName("url");
			base.JsonWriter.WriteValue(base.UriToString(entityReferenceLink.Url));
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00025AB4 File Offset: 0x00023CB4
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks entityReferenceLinks, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			bool flag = false;
			bool flag2 = false;
			base.JsonWriter.StartObjectScope();
			Uri uri;
			if (this.metadataUriBuilder.TryBuildEntityReferenceLinksMetadataUri(entityReferenceLinks.SerializationInfo, entitySet, navigationProperty, out uri))
			{
				base.WriteMetadataUriProperty(uri);
			}
			if (entityReferenceLinks.Count != null)
			{
				flag2 = true;
				this.WriteCountAnnotation(entityReferenceLinks.Count.Value);
			}
			if (entityReferenceLinks.NextPageLink != null)
			{
				flag = true;
				this.WriteNextLinkAnnotation(entityReferenceLinks.NextPageLink);
			}
			base.JsonWriter.WriteValuePropertyName();
			base.JsonWriter.StartArrayScope();
			IEnumerable<ODataEntityReferenceLink> links = entityReferenceLinks.Links;
			if (links != null)
			{
				foreach (ODataEntityReferenceLink odataEntityReferenceLink in links)
				{
					WriterValidationUtils.ValidateEntityReferenceLinkNotNull(odataEntityReferenceLink);
					this.WriteEntityReferenceLinkImplementation(odataEntityReferenceLink, entitySet, null, false);
				}
			}
			base.JsonWriter.EndArrayScope();
			if (!flag2 && entityReferenceLinks.Count != null)
			{
				this.WriteCountAnnotation(entityReferenceLinks.Count.Value);
			}
			if (!flag && entityReferenceLinks.NextPageLink != null)
			{
				this.WriteNextLinkAnnotation(entityReferenceLinks.NextPageLink);
			}
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00025BFC File Offset: 0x00023DFC
		private void WriteNextLinkAnnotation(Uri nextPageLink)
		{
			base.JsonWriter.WriteName("odata.nextLink");
			base.JsonWriter.WriteValue(base.UriToString(nextPageLink));
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00025C20 File Offset: 0x00023E20
		private void WriteCountAnnotation(long countValue)
		{
			base.JsonWriter.WriteName("odata.count");
			base.JsonWriter.WriteValue(countValue);
		}

		// Token: 0x04000419 RID: 1049
		private readonly ODataJsonLightMetadataUriBuilder metadataUriBuilder;
	}
}
