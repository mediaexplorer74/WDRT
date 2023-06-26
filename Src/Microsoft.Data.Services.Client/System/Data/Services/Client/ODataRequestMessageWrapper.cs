using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000079 RID: 121
	internal abstract class ODataRequestMessageWrapper
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x00011402 File Offset: 0x0000F602
		protected ODataRequestMessageWrapper(DataServiceClientRequestMessage requestMessage, RequestInfo requestInfo, Descriptor descriptor)
		{
			this.requestMessage = requestMessage;
			this.requestInfo = requestInfo;
			this.Descriptor = descriptor;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001141F File Offset: 0x0000F61F
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00011427 File Offset: 0x0000F627
		internal Descriptor Descriptor { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600040E RID: 1038
		internal abstract ContentStream CachedRequestStream { get; }

		// Token: 0x17000103 RID: 259
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x00011430 File Offset: 0x0000F630
		internal bool SendChunked
		{
			set
			{
				this.requestMessage.SendChunked = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000410 RID: 1040
		internal abstract bool IsBatchPartRequest { get; }

		// Token: 0x06000411 RID: 1041 RVA: 0x00011440 File Offset: 0x0000F640
		internal static ODataRequestMessageWrapper CreateBatchPartRequestMessage(ODataBatchWriter batchWriter, BuildingRequestEventArgs requestMessageArgs, RequestInfo requestInfo)
		{
			IODataRequestMessage iodataRequestMessage = batchWriter.CreateOperationRequestMessage(requestMessageArgs.Method, requestMessageArgs.RequestUri);
			foreach (KeyValuePair<string, string> keyValuePair in requestMessageArgs.Headers)
			{
				iodataRequestMessage.SetHeader(keyValuePair.Key, keyValuePair.Value);
			}
			InternalODataRequestMessage internalODataRequestMessage = new InternalODataRequestMessage(iodataRequestMessage, false);
			return new ODataRequestMessageWrapper.InnerBatchRequestMessageWrapper(internalODataRequestMessage, iodataRequestMessage, requestInfo, requestMessageArgs.Descriptor);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000114CC File Offset: 0x0000F6CC
		internal static ODataRequestMessageWrapper CreateRequestMessageWrapper(BuildingRequestEventArgs requestMessageArgs, RequestInfo requestInfo)
		{
			DataServiceClientRequestMessage dataServiceClientRequestMessage = requestInfo.CreateRequestMessage(requestMessageArgs);
			if (requestInfo.Credentials != null)
			{
				dataServiceClientRequestMessage.Credentials = requestInfo.Credentials;
			}
			if (requestInfo.Timeout != 0)
			{
				dataServiceClientRequestMessage.Timeout = requestInfo.Timeout;
			}
			return new ODataRequestMessageWrapper.TopLevelRequestMessageWrapper(dataServiceClientRequestMessage, requestInfo, requestMessageArgs.Descriptor);
		}

		// Token: 0x06000413 RID: 1043
		internal abstract ODataMessageWriter CreateWriter(ODataMessageWriterSettings writerSettings, bool isParameterPayload);

		// Token: 0x06000414 RID: 1044 RVA: 0x00011516 File Offset: 0x0000F716
		internal void Abort()
		{
			this.requestMessage.Abort();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00011523 File Offset: 0x0000F723
		internal void SetHeader(string headerName, string headerValue)
		{
			this.requestMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00011532 File Offset: 0x0000F732
		internal IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			return this.requestMessage.BeginGetRequestStream(callback, state);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00011541 File Offset: 0x0000F741
		internal Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			return this.requestMessage.EndGetRequestStream(asyncResult);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00011550 File Offset: 0x0000F750
		internal void SetRequestStream(ContentStream requestStreamContent)
		{
			if (requestStreamContent.IsKnownMemoryStream)
			{
				this.SetContentLengthHeader();
			}
			using (Stream stream = this.requestMessage.GetStream())
			{
				if (requestStreamContent.IsKnownMemoryStream)
				{
					MemoryStream memoryStream = (MemoryStream)requestStreamContent.Stream;
					byte[] buffer = memoryStream.GetBuffer();
					int num = checked((int)memoryStream.Position);
					int num2 = checked((int)memoryStream.Length) - num;
					stream.Write(buffer, num, num2);
				}
				else
				{
					byte[] array = new byte[65536];
					WebUtil.CopyStream(requestStreamContent.Stream, stream, ref array);
				}
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000115E8 File Offset: 0x0000F7E8
		internal IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			return this.requestMessage.BeginGetResponse(callback, state);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000115F7 File Offset: 0x0000F7F7
		internal IODataResponseMessage EndGetResponse(IAsyncResult asyncResult)
		{
			return this.requestMessage.EndGetResponse(asyncResult);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00011605 File Offset: 0x0000F805
		internal IODataResponseMessage GetResponse()
		{
			return this.requestMessage.GetResponse();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00011614 File Offset: 0x0000F814
		internal void SetContentLengthHeader()
		{
			if (this.requestInfo.HasSendingRequestEventHandlers || this.requestInfo.HasSendingRequest2EventHandlers)
			{
				this.SetHeader("Content-Length", this.CachedRequestStream.Stream.Length.ToString(CultureInfo.InvariantCulture));
				if (this.requestInfo.HasSendingRequestEventHandlers)
				{
					this.AddHeadersToReset(new string[] { "Content-Length" });
				}
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00011688 File Offset: 0x0000F888
		internal void AddHeadersToReset(IEnumerable<string> headerNames)
		{
			if (this.requestInfo.HasSendingRequestEventHandlers)
			{
				HttpWebRequestMessage httpWebRequestMessage = this.requestMessage as HttpWebRequestMessage;
				if (httpWebRequestMessage != null)
				{
					httpWebRequestMessage.AddHeadersToReset(headerNames);
				}
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000116B8 File Offset: 0x0000F8B8
		internal void AddHeadersToReset(params string[] headerNames)
		{
			this.AddHeadersToReset((IEnumerable<string>)headerNames);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000116C6 File Offset: 0x0000F8C6
		internal void FireSendingEventHandlers(Descriptor descriptor)
		{
			this.FireSendingRequest2(descriptor);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000116D0 File Offset: 0x0000F8D0
		internal void FireSendingRequest2(Descriptor descriptor)
		{
			if (this.requestInfo.HasSendingRequest2EventHandlers)
			{
				HttpWebRequestMessage httpWebRequestMessage = this.requestMessage as HttpWebRequestMessage;
				if (httpWebRequestMessage != null)
				{
					httpWebRequestMessage.BeforeSendingRequest2Event();
				}
				try
				{
					this.requestInfo.FireSendingRequest2(new SendingRequest2EventArgs(this.requestMessage, descriptor, this.IsBatchPartRequest));
				}
				finally
				{
					if (httpWebRequestMessage != null)
					{
						httpWebRequestMessage.AfterSendingRequest2Event();
					}
				}
			}
		}

		// Token: 0x040002C0 RID: 704
		private readonly DataServiceClientRequestMessage requestMessage;

		// Token: 0x040002C1 RID: 705
		private readonly RequestInfo requestInfo;

		// Token: 0x0200007A RID: 122
		private class RequestMessageWithCachedStream : IODataRequestMessage
		{
			// Token: 0x06000421 RID: 1057 RVA: 0x00011738 File Offset: 0x0000F938
			internal RequestMessageWithCachedStream(DataServiceClientRequestMessage requestMessage)
			{
				this.requestMessage = requestMessage;
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000422 RID: 1058 RVA: 0x00011747 File Offset: 0x0000F947
			public IEnumerable<KeyValuePair<string, string>> Headers
			{
				get
				{
					return this.requestMessage.Headers;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000423 RID: 1059 RVA: 0x00011754 File Offset: 0x0000F954
			// (set) Token: 0x06000424 RID: 1060 RVA: 0x00011761 File Offset: 0x0000F961
			public Uri Url
			{
				get
				{
					return this.requestMessage.Url;
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000425 RID: 1061 RVA: 0x00011768 File Offset: 0x0000F968
			// (set) Token: 0x06000426 RID: 1062 RVA: 0x00011775 File Offset: 0x0000F975
			public string Method
			{
				get
				{
					return this.requestMessage.Method;
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001177C File Offset: 0x0000F97C
			internal ContentStream CachedRequestStream
			{
				get
				{
					this.cachedRequestStream.Stream.Position = 0L;
					return this.cachedRequestStream;
				}
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x00011796 File Offset: 0x0000F996
			public string GetHeader(string headerName)
			{
				return this.requestMessage.GetHeader(headerName);
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x000117A4 File Offset: 0x0000F9A4
			public void SetHeader(string headerName, string headerValue)
			{
				this.requestMessage.SetHeader(headerName, headerValue);
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x000117B3 File Offset: 0x0000F9B3
			public Stream GetStream()
			{
				if (this.cachedRequestStream == null)
				{
					this.cachedRequestStream = new ContentStream(new MemoryStream(), true);
				}
				return this.cachedRequestStream.Stream;
			}

			// Token: 0x040002C3 RID: 707
			private readonly DataServiceClientRequestMessage requestMessage;

			// Token: 0x040002C4 RID: 708
			private ContentStream cachedRequestStream;
		}

		// Token: 0x0200007B RID: 123
		private class TopLevelRequestMessageWrapper : ODataRequestMessageWrapper
		{
			// Token: 0x0600042B RID: 1067 RVA: 0x000117D9 File Offset: 0x0000F9D9
			internal TopLevelRequestMessageWrapper(DataServiceClientRequestMessage requestMessage, RequestInfo requestInfo, Descriptor descriptor)
				: base(requestMessage, requestInfo, descriptor)
			{
				this.messageWithCachedStream = new ODataRequestMessageWrapper.RequestMessageWithCachedStream(this.requestMessage);
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x0600042C RID: 1068 RVA: 0x000117F5 File Offset: 0x0000F9F5
			internal override bool IsBatchPartRequest
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x0600042D RID: 1069 RVA: 0x000117F8 File Offset: 0x0000F9F8
			internal override ContentStream CachedRequestStream
			{
				get
				{
					return this.messageWithCachedStream.CachedRequestStream;
				}
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x00011805 File Offset: 0x0000FA05
			internal override ODataMessageWriter CreateWriter(ODataMessageWriterSettings writerSettings, bool isParameterPayload)
			{
				return this.requestInfo.WriteHelper.CreateWriter(this.messageWithCachedStream, writerSettings, isParameterPayload);
			}

			// Token: 0x040002C5 RID: 709
			private readonly ODataRequestMessageWrapper.RequestMessageWithCachedStream messageWithCachedStream;
		}

		// Token: 0x0200007C RID: 124
		private class InnerBatchRequestMessageWrapper : ODataRequestMessageWrapper
		{
			// Token: 0x0600042F RID: 1071 RVA: 0x0001181F File Offset: 0x0000FA1F
			internal InnerBatchRequestMessageWrapper(DataServiceClientRequestMessage clientRequestMessage, IODataRequestMessage odataRequestMessage, RequestInfo requestInfo, Descriptor descriptor)
				: base(clientRequestMessage, requestInfo, descriptor)
			{
				this.innerBatchRequestMessage = odataRequestMessage;
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000430 RID: 1072 RVA: 0x00011832 File Offset: 0x0000FA32
			internal override bool IsBatchPartRequest
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000431 RID: 1073 RVA: 0x00011835 File Offset: 0x0000FA35
			internal override ContentStream CachedRequestStream
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x0001183C File Offset: 0x0000FA3C
			internal override ODataMessageWriter CreateWriter(ODataMessageWriterSettings writerSettings, bool isParameterPayload)
			{
				return this.requestInfo.WriteHelper.CreateWriter(this.innerBatchRequestMessage, writerSettings, isParameterPayload);
			}

			// Token: 0x040002C6 RID: 710
			private readonly IODataRequestMessage innerBatchRequestMessage;
		}
	}
}
