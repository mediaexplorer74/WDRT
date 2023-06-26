using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020001EB RID: 491
	internal sealed class ODataRawValueFormat : ODataFormat
	{
		// Token: 0x06000F22 RID: 3874 RVA: 0x00036735 File Offset: 0x00034935
		public override string ToString()
		{
			return "RawValue";
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0003673C File Offset: 0x0003493C
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataResponseMessage responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return ODataRawValueFormat.DetectPayloadKindImplementation(detectionInfo.ContentType);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003675F File Offset: 0x0003495F
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataRequestMessage requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return ODataRawValueFormat.DetectPayloadKindImplementation(detectionInfo.ContentType);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00036784 File Offset: 0x00034984
		internal override ODataInputContext CreateInputContext(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			Stream stream = message.GetStream();
			return new ODataRawInputContext(this, stream, encoding, messageReaderSettings, version, readingResponse, true, model, urlResolver, readerPayloadKind);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x000367C4 File Offset: 0x000349C4
		internal override ODataOutputContext CreateOutputContext(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			Stream stream = message.GetStream();
			return new ODataRawOutputContext(this, stream, encoding, messageWriterSettings, writingResponse, true, model, urlResolver);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0003681C File Offset: 0x00034A1C
		internal override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(IODataResponseMessageAsync responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessageAsync>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return TaskUtils.GetTaskForSynchronousOperation<IEnumerable<ODataPayloadKind>>(() => ODataRawValueFormat.DetectPayloadKindImplementation(detectionInfo.ContentType));
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0003687C File Offset: 0x00034A7C
		internal override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(IODataRequestMessageAsync requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessageAsync>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return TaskUtils.GetTaskForSynchronousOperation<IEnumerable<ODataPayloadKind>>(() => ODataRawValueFormat.DetectPayloadKindImplementation(detectionInfo.ContentType));
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00036918 File Offset: 0x00034B18
		internal override Task<ODataInputContext> CreateInputContextAsync(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return message.GetStreamAsync().FollowOnSuccessWith((Task<Stream> streamTask) => new ODataRawInputContext(this, streamTask.Result, encoding, messageReaderSettings, version, readingResponse, false, model, urlResolver, readerPayloadKind));
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x000369D8 File Offset: 0x00034BD8
		internal override Task<ODataOutputContext> CreateOutputContextAsync(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return message.GetStreamAsync().FollowOnSuccessWith((Task<Stream> streamTask) => new ODataRawOutputContext(this, streamTask.Result, encoding, messageWriterSettings, writingResponse, false, model, urlResolver));
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00036A4C File Offset: 0x00034C4C
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(MediaType contentType)
		{
			if (HttpUtils.CompareMediaTypeNames("text", contentType.TypeName) && HttpUtils.CompareMediaTypeNames("text/plain", contentType.SubTypeName))
			{
				return new ODataPayloadKind[] { ODataPayloadKind.Value };
			}
			return new ODataPayloadKind[] { ODataPayloadKind.BinaryValue };
		}
	}
}
