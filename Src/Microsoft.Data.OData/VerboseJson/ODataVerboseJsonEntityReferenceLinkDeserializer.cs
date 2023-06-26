using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000234 RID: 564
	internal sealed class ODataVerboseJsonEntityReferenceLinkDeserializer : ODataVerboseJsonDeserializer
	{
		// Token: 0x060011F3 RID: 4595 RVA: 0x00042DA8 File Offset: 0x00040FA8
		internal ODataVerboseJsonEntityReferenceLinkDeserializer(ODataVerboseJsonInputContext verboseJsonInputContext)
			: base(verboseJsonInputContext)
		{
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00042DB4 File Offset: 0x00040FB4
		internal ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			bool flag = base.Version >= ODataVersion.V2 && base.ReadingResponse;
			ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask entityReferenceLinksWrapperPropertyBitMask = ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask.None;
			ODataEntityReferenceLinks odataEntityReferenceLinks = new ODataEntityReferenceLinks();
			base.ReadPayloadStart(false);
			if (flag)
			{
				base.JsonReader.ReadStartObject();
				if (!this.ReadEntityReferenceLinkProperties(odataEntityReferenceLinks, ref entityReferenceLinksWrapperPropertyBitMask))
				{
					throw new ODataException(Strings.ODataJsonEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksResultsPropertyNotFound);
				}
			}
			base.JsonReader.ReadStartArray();
			List<ODataEntityReferenceLink> list = new List<ODataEntityReferenceLink>();
			while (base.JsonReader.NodeType != JsonNodeType.EndArray)
			{
				ODataEntityReferenceLink odataEntityReferenceLink = this.ReadSingleEntityReferenceLink();
				list.Add(odataEntityReferenceLink);
			}
			base.JsonReader.ReadEndArray();
			if (flag)
			{
				this.ReadEntityReferenceLinkProperties(odataEntityReferenceLinks, ref entityReferenceLinksWrapperPropertyBitMask);
				base.JsonReader.ReadEndObject();
			}
			odataEntityReferenceLinks.Links = new ReadOnlyEnumerable<ODataEntityReferenceLink>(list);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLinks;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00042E74 File Offset: 0x00041074
		internal ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			base.ReadPayloadStart(false);
			ODataEntityReferenceLink odataEntityReferenceLink = this.ReadSingleEntityReferenceLink();
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLink;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00042E98 File Offset: 0x00041098
		private bool ReadEntityReferenceLinkProperties(ODataEntityReferenceLinks entityReferenceLinks, ref ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask propertiesFoundBitField)
		{
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				string text2;
				if ((text2 = text) != null)
				{
					if (text2 == "results")
					{
						ODataVerboseJsonReaderUtils.VerifyEntityReferenceLinksWrapperPropertyNotFound(ref propertiesFoundBitField, ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask.Results, "results");
						return true;
					}
					if (text2 == "__count")
					{
						ODataVerboseJsonReaderUtils.VerifyEntityReferenceLinksWrapperPropertyNotFound(ref propertiesFoundBitField, ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask.Count, "__count");
						object obj = base.JsonReader.ReadPrimitiveValue();
						long? num = (long?)ODataVerboseJsonReaderUtils.ConvertValue(obj, EdmCoreModel.Instance.GetInt64(true), base.MessageReaderSettings, base.Version, true, text);
						ODataVerboseJsonReaderUtils.ValidateCountPropertyInEntityReferenceLinks(num);
						entityReferenceLinks.Count = num;
						continue;
					}
					if (text2 == "__next")
					{
						ODataVerboseJsonReaderUtils.VerifyEntityReferenceLinksWrapperPropertyNotFound(ref propertiesFoundBitField, ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask.NextPageLink, "__next");
						string text3 = base.JsonReader.ReadStringValue("__next");
						ODataVerboseJsonReaderUtils.ValidateEntityReferenceLinksStringProperty(text3, "__next");
						entityReferenceLinks.NextPageLink = base.ProcessUriFromPayload(text3);
						continue;
					}
				}
				base.JsonReader.SkipValue();
			}
			return false;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00042F9C File Offset: 0x0004119C
		private ODataEntityReferenceLink ReadSingleEntityReferenceLink()
		{
			if (base.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue(base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartObject();
			ODataEntityReferenceLink odataEntityReferenceLink = new ODataEntityReferenceLink();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("uri", text) == 0)
				{
					if (odataEntityReferenceLink.Url != null)
					{
						throw new ODataException(Strings.ODataJsonEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink);
					}
					string text2 = base.JsonReader.ReadStringValue("uri");
					if (text2 == null)
					{
						throw new ODataException(Strings.ODataJsonEntityReferenceLinkDeserializer_EntityReferenceLinkUriCannotBeNull);
					}
					odataEntityReferenceLink.Url = base.ProcessUriFromPayload(text2);
				}
				else
				{
					base.JsonReader.SkipValue();
				}
			}
			ReaderValidationUtils.ValidateEntityReferenceLink(odataEntityReferenceLink);
			base.JsonReader.ReadEndObject();
			return odataEntityReferenceLink;
		}
	}
}
