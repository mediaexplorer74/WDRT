using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C6 RID: 454
	internal sealed class ODataVerboseJsonEntityReferenceLinkSerializer : ODataVerboseJsonSerializer
	{
		// Token: 0x06000E0B RID: 3595 RVA: 0x00031827 File Offset: 0x0002FA27
		internal ODataVerboseJsonEntityReferenceLinkSerializer(ODataVerboseJsonOutputContext verboseJsonOutputContext)
			: base(verboseJsonOutputContext)
		{
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003184C File Offset: 0x0002FA4C
		internal void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinkImplementation(link);
			});
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000318B8 File Offset: 0x0002FAB8
		internal void WriteEntityReferenceLinks(ODataEntityReferenceLinks entityReferenceLinks)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinksImplementation(entityReferenceLinks, this.Version >= ODataVersion.V2 && this.WritingResponse);
			});
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000318EC File Offset: 0x0002FAEC
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink entityReferenceLink)
		{
			WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
			base.JsonWriter.StartObjectScope();
			base.JsonWriter.WriteName("uri");
			base.JsonWriter.WriteValue(base.UriToAbsoluteUriString(entityReferenceLink.Url));
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003193C File Offset: 0x0002FB3C
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks entityReferenceLinks, bool includeResultsWrapper)
		{
			if (includeResultsWrapper)
			{
				base.JsonWriter.StartObjectScope();
			}
			if (entityReferenceLinks.Count != null)
			{
				base.JsonWriter.WriteName("__count");
				base.JsonWriter.WriteValue(entityReferenceLinks.Count.Value);
			}
			if (includeResultsWrapper)
			{
				base.JsonWriter.WriteDataArrayName();
			}
			base.JsonWriter.StartArrayScope();
			IEnumerable<ODataEntityReferenceLink> links = entityReferenceLinks.Links;
			if (links != null)
			{
				foreach (ODataEntityReferenceLink odataEntityReferenceLink in links)
				{
					WriterValidationUtils.ValidateEntityReferenceLinkNotNull(odataEntityReferenceLink);
					this.WriteEntityReferenceLinkImplementation(odataEntityReferenceLink);
				}
			}
			base.JsonWriter.EndArrayScope();
			if (entityReferenceLinks.NextPageLink != null)
			{
				base.JsonWriter.WriteName("__next");
				base.JsonWriter.WriteValue(base.UriToAbsoluteUriString(entityReferenceLinks.NextPageLink));
			}
			if (includeResultsWrapper)
			{
				base.JsonWriter.EndObjectScope();
			}
		}
	}
}
