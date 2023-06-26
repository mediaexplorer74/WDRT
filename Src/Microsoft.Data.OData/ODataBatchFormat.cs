using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020001CF RID: 463
	internal sealed class ODataBatchFormat : ODataFormat
	{
		// Token: 0x06000E6E RID: 3694 RVA: 0x00032F10 File Offset: 0x00031110
		public override string ToString()
		{
			return "Batch";
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00032F17 File Offset: 0x00031117
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataResponseMessage responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return ODataBatchFormat.DetectPayloadKindImplementation(detectionInfo.ContentType);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00032F3A File Offset: 0x0003113A
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataRequestMessage requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return ODataBatchFormat.DetectPayloadKindImplementation(detectionInfo.ContentType);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00032F60 File Offset: 0x00031160
		internal override ODataInputContext CreateInputContext(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			Stream stream = message.GetStream();
			return new ODataRawInputContext(this, stream, encoding, messageReaderSettings, version, readingResponse, true, model, urlResolver, readerPayloadKind);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00032FA0 File Offset: 0x000311A0
		internal override ODataOutputContext CreateOutputContext(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			Stream stream = message.GetStream();
			return new ODataRawOutputContext(this, stream, encoding, messageWriterSettings, writingResponse, true, model, urlResolver);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00032FF8 File Offset: 0x000311F8
		internal override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(IODataResponseMessageAsync responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessageAsync>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return TaskUtils.GetTaskForSynchronousOperation<IEnumerable<ODataPayloadKind>>(() => ODataBatchFormat.DetectPayloadKindImplementation(detectionInfo.ContentType));
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00033058 File Offset: 0x00031258
		internal override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(IODataRequestMessageAsync requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessageAsync>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			return TaskUtils.GetTaskForSynchronousOperation<IEnumerable<ODataPayloadKind>>(() => ODataBatchFormat.DetectPayloadKindImplementation(detectionInfo.ContentType));
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000330F4 File Offset: 0x000312F4
		internal override Task<ODataInputContext> CreateInputContextAsync(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return message.GetStreamAsync().FollowOnSuccessWith((Task<Stream> streamTask) => new ODataRawInputContext(this, streamTask.Result, encoding, messageReaderSettings, version, readingResponse, false, model, urlResolver, readerPayloadKind));
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x000331B4 File Offset: 0x000313B4
		internal override Task<ODataOutputContext> CreateOutputContextAsync(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return message.GetStreamAsync().FollowOnSuccessWith((Task<Stream> streamTask) => new ODataRawOutputContext(this, streamTask.Result, encoding, messageWriterSettings, writingResponse, false, model, urlResolver));
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003323C File Offset: 0x0003143C
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(MediaType contentType)
		{
			if (HttpUtils.CompareMediaTypeNames("multipart", contentType.TypeName) && HttpUtils.CompareMediaTypeNames("mixed", contentType.SubTypeName) && contentType.Parameters != null)
			{
				if (contentType.Parameters.Any((KeyValuePair<string, string> kvp) => HttpUtils.CompareMediaTypeParameterNames("boundary", kvp.Key)))
				{
					return new ODataPayloadKind[] { ODataPayloadKind.Batch };
				}
			}
			return Enumerable.Empty<ODataPayloadKind>();
		}
	}
}
