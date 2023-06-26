using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Atom;

namespace Microsoft.Data.OData
{
	// Token: 0x020001D0 RID: 464
	internal sealed class ODataMetadataFormat : ODataFormat
	{
		// Token: 0x06000E7A RID: 3706 RVA: 0x000332BA File Offset: 0x000314BA
		public override string ToString()
		{
			return "Metadata";
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x000332C4 File Offset: 0x000314C4
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataResponseMessage responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			Stream stream = ((ODataMessage)responseMessage).GetStream();
			return ODataMetadataFormat.DetectPayloadKindImplementation(stream, detectionInfo);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000332FA File Offset: 0x000314FA
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataRequestMessage requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return Enumerable.Empty<ODataPayloadKind>();
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00033318 File Offset: 0x00031518
		internal override ODataInputContext CreateInputContext(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			Stream stream = message.GetStream();
			return new ODataMetadataInputContext(this, stream, encoding, messageReaderSettings, version, readingResponse, true, model, urlResolver);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00033358 File Offset: 0x00031558
		internal override ODataOutputContext CreateOutputContext(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			Stream stream = message.GetStream();
			return new ODataMetadataOutputContext(this, stream, encoding, messageWriterSettings, writingResponse, true, model, urlResolver);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x000333B0 File Offset: 0x000315B0
		internal override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(IODataResponseMessageAsync responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessageAsync>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return ((ODataMessage)responseMessage).GetStreamAsync().FollowOnSuccessWith((Task<Stream> streamTask) => ODataMetadataFormat.DetectPayloadKindImplementation(streamTask.Result, detectionInfo));
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00033401 File Offset: 0x00031601
		internal override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(IODataRequestMessageAsync requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessageAsync>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return TaskUtils.GetCompletedTask<IEnumerable<ODataPayloadKind>>(Enumerable.Empty<ODataPayloadKind>());
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00033423 File Offset: 0x00031623
		internal override Task<ODataInputContext> CreateInputContextAsync(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataMetadataFormat_CreateInputContextAsync));
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003344D File Offset: 0x0003164D
		internal override Task<ODataOutputContext> CreateOutputContextAsync(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataMetadataFormat_CreateOutputContextAsync));
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00033478 File Offset: 0x00031678
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(Stream messageStream, ODataPayloadKindDetectionInfo detectionInfo)
		{
			try
			{
				using (XmlReader xmlReader = ODataAtomReaderUtils.CreateXmlReader(messageStream, detectionInfo.GetEncoding(), detectionInfo.MessageReaderSettings))
				{
					string namespaceURI;
					if (xmlReader.TryReadToNextElement() && string.CompareOrdinal("Edmx", xmlReader.LocalName) == 0 && (namespaceURI = xmlReader.NamespaceURI) != null && (namespaceURI == "http://schemas.microsoft.com/ado/2007/06/edmx" || namespaceURI == "http://schemas.microsoft.com/ado/2008/10/edmx" || namespaceURI == "http://schemas.microsoft.com/ado/2009/11/edmx"))
					{
						return new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument };
					}
				}
			}
			catch (XmlException)
			{
			}
			return Enumerable.Empty<ODataPayloadKind>();
		}
	}
}
