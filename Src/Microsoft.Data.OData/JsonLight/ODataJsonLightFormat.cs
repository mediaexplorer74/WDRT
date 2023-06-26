using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000192 RID: 402
	internal sealed class ODataJsonLightFormat : ODataFormat
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x00028F6F File Offset: 0x0002716F
		public override string ToString()
		{
			return "JsonLight";
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00028F78 File Offset: 0x00027178
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataResponseMessage responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			ODataMessage odataMessage = (ODataMessage)responseMessage;
			Stream stream = odataMessage.GetStream();
			return this.DetectPayloadKindImplementation(stream, odataMessage, true, detectionInfo);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00028FB4 File Offset: 0x000271B4
		internal override IEnumerable<ODataPayloadKind> DetectPayloadKind(IODataRequestMessage requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			ODataMessage odataMessage = (ODataMessage)requestMessage;
			Stream stream = odataMessage.GetStream();
			return this.DetectPayloadKindImplementation(stream, odataMessage, false, detectionInfo);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00028FF0 File Offset: 0x000271F0
		internal override ODataInputContext CreateInputContext(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			Stream stream = message.GetStream();
			return new ODataJsonLightInputContext(this, stream, contentType, encoding, messageReaderSettings, version, readingResponse, true, model, urlResolver, (ODataJsonLightPayloadKindDetectionState)payloadKindDetectionFormatState);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00029038 File Offset: 0x00027238
		internal override ODataOutputContext CreateOutputContext(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			Stream stream = message.GetStream();
			return new ODataJsonLightOutputContext(this, stream, mediaType, encoding, messageWriterSettings, writingResponse, true, model, urlResolver);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000290A0 File Offset: 0x000272A0
		internal override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(IODataResponseMessageAsync responseMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessageAsync>(responseMessage, "responseMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			ODataMessage message = (ODataMessage)responseMessage;
			return message.GetStreamAsync().FollowOnSuccessWithTask((Task<Stream> streamTask) => this.DetectPayloadKindImplementationAsync(streamTask.Result, message, true, detectionInfo));
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002912C File Offset: 0x0002732C
		internal override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(IODataRequestMessageAsync requestMessage, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessageAsync>(requestMessage, "requestMessage");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadKindDetectionInfo>(detectionInfo, "detectionInfo");
			ODataMessage message = (ODataMessage)requestMessage;
			return message.GetStreamAsync().FollowOnSuccessWithTask((Task<Stream> streamTask) => this.DetectPayloadKindImplementationAsync(streamTask.Result, message, false, detectionInfo));
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000291EC File Offset: 0x000273EC
		internal override Task<ODataInputContext> CreateInputContextAsync(ODataPayloadKind readerPayloadKind, ODataMessage message, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, IEdmModel model, IODataUrlResolver urlResolver, object payloadKindDetectionFormatState)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return message.GetStreamAsync().FollowOnSuccessWith((Task<Stream> streamTask) => new ODataJsonLightInputContext(this, streamTask.Result, contentType, encoding, messageReaderSettings, version, readingResponse, false, model, urlResolver, (ODataJsonLightPayloadKindDetectionState)payloadKindDetectionFormatState));
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000292C4 File Offset: 0x000274C4
		internal override Task<ODataOutputContext> CreateOutputContextAsync(ODataMessage message, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessage>(message, "message");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return message.GetStreamAsync().FollowOnSuccessWith((Task<Stream> streamTask) => new ODataJsonLightOutputContext(this, streamTask.Result, mediaType, encoding, messageWriterSettings, writingResponse, false, model, urlResolver));
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00029340 File Offset: 0x00027540
		private IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(Stream messageStream, ODataMessage message, bool readingResponse, ODataPayloadKindDetectionInfo detectionInfo)
		{
			IEnumerable<ODataPayloadKind> enumerable;
			using (ODataJsonLightInputContext odataJsonLightInputContext = new ODataJsonLightInputContext(this, messageStream, detectionInfo.ContentType, detectionInfo.GetEncoding(), detectionInfo.MessageReaderSettings, ODataVersion.V3, readingResponse, true, detectionInfo.Model, null, null))
			{
				enumerable = odataJsonLightInputContext.DetectPayloadKind(detectionInfo);
			}
			return enumerable;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000293B4 File Offset: 0x000275B4
		private Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindImplementationAsync(Stream messageStream, ODataMessage message, bool readingResponse, ODataPayloadKindDetectionInfo detectionInfo)
		{
			ODataJsonLightInputContext jsonLightInputContext = new ODataJsonLightInputContext(this, messageStream, detectionInfo.ContentType, detectionInfo.GetEncoding(), detectionInfo.MessageReaderSettings, ODataVersion.V3, readingResponse, false, detectionInfo.Model, null, null);
			return jsonLightInputContext.DetectPayloadKindAsync(detectionInfo).FollowAlwaysWith(delegate(Task<IEnumerable<ODataPayloadKind>> t)
			{
				jsonLightInputContext.Dispose();
			});
		}
	}
}
