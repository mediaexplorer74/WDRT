using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Atom;
using Microsoft.Data.OData.JsonLight;
using Microsoft.Data.OData.VerboseJson;

namespace Microsoft.Data.OData
{
	// Token: 0x020000F8 RID: 248
	public abstract class ODataFormat
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001713C File Offset: 0x0001533C
		public static ODataFormat Atom
		{
			get
			{
				return ODataFormat.atomFormat;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00017143 File Offset: 0x00015343
		public static ODataFormat VerboseJson
		{
			get
			{
				return ODataFormat.verboseJsonFormat;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001714A File Offset: 0x0001534A
		public static ODataFormat Json
		{
			get
			{
				return ODataFormat.jsonLightFormat;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00017151 File Offset: 0x00015351
		public static ODataFormat RawValue
		{
			get
			{
				return ODataFormat.rawValueFormat;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00017158 File Offset: 0x00015358
		public static ODataFormat Batch
		{
			get
			{
				return ODataFormat.batchFormat;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0001715F File Offset: 0x0001535F
		public static ODataFormat Metadata
		{
			get
			{
				return ODataFormat.metadataFormat;
			}
		}

		// Token: 0x06000656 RID: 1622
		internal abstract IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataResponseMessage responseMessage, ODataPayloadKindDetectionInfo detectionInfo);

		// Token: 0x06000657 RID: 1623
		internal abstract IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataRequestMessage requestMessage, ODataPayloadKindDetectionInfo detectionInfo);

		// Token: 0x06000658 RID: 1624
		internal abstract ODataInputContext CreateInputContext(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState);

		// Token: 0x06000659 RID: 1625
		internal abstract ODataOutputContext CreateOutputContext(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver);

		// Token: 0x0600065A RID: 1626
		internal abstract Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(IODataResponseMessageAsync responseMessage, ODataPayloadKindDetectionInfo detectionInfo);

		// Token: 0x0600065B RID: 1627
		internal abstract Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(IODataRequestMessageAsync requestMessage, ODataPayloadKindDetectionInfo detectionInfo);

		// Token: 0x0600065C RID: 1628
		internal abstract Task<ODataInputContext> CreateInputContextAsync(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState);

		// Token: 0x0600065D RID: 1629
		internal abstract Task<ODataOutputContext> CreateOutputContextAsync(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver);

		// Token: 0x04000284 RID: 644
		private static ODataAtomFormat atomFormat = new ODataAtomFormat();

		// Token: 0x04000285 RID: 645
		private static ODataVerboseJsonFormat verboseJsonFormat = new ODataVerboseJsonFormat();

		// Token: 0x04000286 RID: 646
		private static ODataJsonLightFormat jsonLightFormat = new ODataJsonLightFormat();

		// Token: 0x04000287 RID: 647
		private static ODataRawValueFormat rawValueFormat = new ODataRawValueFormat();

		// Token: 0x04000288 RID: 648
		private static ODataBatchFormat batchFormat = new ODataBatchFormat();

		// Token: 0x04000289 RID: 649
		private static ODataMetadataFormat metadataFormat = new ODataMetadataFormat();
	}
}
