using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000145 RID: 325
	internal class ODataJsonLightSerializer : ODataSerializer
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x0001C100 File Offset: 0x0001A300
		internal ODataJsonLightSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.instanceAnnotationWriter = new SimpleLazy<JsonLightInstanceAnnotationWriter>(() => new JsonLightInstanceAnnotationWriter(new ODataJsonLightValueSerializer(jsonLightOutputContext), jsonLightOutputContext.TypeNameOracle));
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0001C150 File Offset: 0x0001A350
		internal ODataJsonLightOutputContext JsonLightOutputContext
		{
			get
			{
				return this.jsonLightOutputContext;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0001C158 File Offset: 0x0001A358
		internal IJsonWriter JsonWriter
		{
			get
			{
				return this.jsonLightOutputContext.JsonWriter;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001C165 File Offset: 0x0001A365
		internal JsonLightInstanceAnnotationWriter InstanceAnnotationWriter
		{
			get
			{
				return this.instanceAnnotationWriter.Value;
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001C172 File Offset: 0x0001A372
		internal void WritePayloadStart()
		{
			ODataJsonWriterUtils.StartJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001C185 File Offset: 0x0001A385
		internal void WritePayloadEnd()
		{
			ODataJsonWriterUtils.EndJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001C198 File Offset: 0x0001A398
		internal void WriteMetadataUriProperty(Uri metadataUri)
		{
			this.JsonWriter.WriteName("odata.metadata");
			this.JsonWriter.WritePrimitiveValue(metadataUri.AbsoluteUri, base.Version);
			this.allowRelativeUri = true;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001C1C8 File Offset: 0x0001A3C8
		internal void WriteTopLevelPayload(Action payloadWriterAction)
		{
			this.WritePayloadStart();
			payloadWriterAction();
			this.WritePayloadEnd();
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001C240 File Offset: 0x0001A440
		internal void WriteTopLevelError(ODataError error, bool includeDebugInformation)
		{
			this.WriteTopLevelPayload(delegate
			{
				ODataJsonWriterUtils.WriteError(this.JsonLightOutputContext.JsonWriter, new Action<IEnumerable<ODataInstanceAnnotation>>(this.InstanceAnnotationWriter.WriteInstanceAnnotations), error, includeDebugInformation, this.MessageWriterSettings.MessageQuotas.MaxNestingDepth, true);
			});
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001C27C File Offset: 0x0001A47C
		internal string UriToString(Uri uri)
		{
			ODataMetadataDocumentUri metadataDocumentUri = this.jsonLightOutputContext.MessageWriterSettings.MetadataDocumentUri;
			Uri uri2 = ((metadataDocumentUri == null) ? null : metadataDocumentUri.BaseUri);
			Uri uri3;
			if (this.jsonLightOutputContext.UrlResolver != null)
			{
				uri3 = this.jsonLightOutputContext.UrlResolver.ResolveUrl(uri2, uri);
				if (uri3 != null)
				{
					return UriUtilsCommon.UriToString(uri3);
				}
			}
			uri3 = uri;
			if (!uri3.IsAbsoluteUri)
			{
				if (!this.allowRelativeUri)
				{
					if (uri2 == null)
					{
						throw new ODataException(Strings.ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata(UriUtilsCommon.UriToString(uri3)));
					}
					uri3 = UriUtils.UriToAbsoluteUri(uri2, uri);
				}
				else
				{
					uri3 = UriUtils.EnsureEscapedRelativeUri(uri3);
				}
			}
			return UriUtilsCommon.UriToString(uri3);
		}

		// Token: 0x04000350 RID: 848
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000351 RID: 849
		private readonly SimpleLazy<JsonLightInstanceAnnotationWriter> instanceAnnotationWriter;

		// Token: 0x04000352 RID: 850
		private bool allowRelativeUri;
	}
}
