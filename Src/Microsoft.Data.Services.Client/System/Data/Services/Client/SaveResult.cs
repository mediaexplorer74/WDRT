using System;
using System.Collections.Generic;
using System.Data.Services.Client.Materialization;
using System.Data.Services.Client.Metadata;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200011F RID: 287
	internal class SaveResult : BaseSaveResult
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x000262C2 File Offset: 0x000244C2
		internal SaveResult(DataServiceContext context, string method, SaveChangesOptions options, AsyncCallback callback, object state)
			: base(context, method, null, options, callback, state)
		{
			this.cachedResponses = new List<SaveResult.CachedResponse>();
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x000262DD File Offset: 0x000244DD
		internal override bool IsBatchRequest
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x000262E0 File Offset: 0x000244E0
		protected override bool ProcessResponsePayload
		{
			get
			{
				return this.cachedResponse.MaterializerEntry != null;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x000262F3 File Offset: 0x000244F3
		protected override Stream ResponseStream
		{
			get
			{
				return this.inMemoryResponseStream;
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000262FC File Offset: 0x000244FC
		internal void BeginCreateNextChange()
		{
			this.inMemoryResponseStream = new MemoryStream();
			BaseAsyncResult.PerRequest perRequest = null;
			for (;;)
			{
				IODataResponseMessage iodataResponseMessage = null;
				try
				{
					if (this.perRequest != null)
					{
						base.SetCompleted();
						Error.ThrowInternalError(InternalError.InvalidBeginNextChange);
					}
					ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateNextRequest();
					if (odataRequestMessageWrapper == null)
					{
						base.Abortable = null;
					}
					if (odataRequestMessageWrapper != null || this.entryIndex < this.ChangedEntries.Count)
					{
						if (this.ChangedEntries[this.entryIndex].ContentGeneratedForSave)
						{
							goto IL_183;
						}
						base.Abortable = odataRequestMessageWrapper;
						ContentStream contentStream = this.CreateNonBatchChangeData(this.entryIndex, odataRequestMessageWrapper);
						perRequest = (this.perRequest = new BaseAsyncResult.PerRequest());
						perRequest.Request = odataRequestMessageWrapper;
						BaseAsyncResult.AsyncStateBag asyncStateBag = new BaseAsyncResult.AsyncStateBag(perRequest);
						IAsyncResult asyncResult;
						if (contentStream == null || contentStream.Stream == null)
						{
							asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(odataRequestMessageWrapper.BeginGetResponse), new AsyncCallback(this.AsyncEndGetResponse), asyncStateBag);
						}
						else
						{
							if (contentStream.IsKnownMemoryStream)
							{
								odataRequestMessageWrapper.SetContentLengthHeader();
							}
							perRequest.RequestContentStream = contentStream;
							asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(odataRequestMessageWrapper.BeginGetRequestStream), new AsyncCallback(base.AsyncEndGetRequestStream), asyncStateBag);
						}
						perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
						base.SetCompletedSynchronously(perRequest.RequestCompletedSynchronously);
					}
					else
					{
						base.SetCompleted();
						if (base.CompletedSynchronously)
						{
							this.HandleCompleted(perRequest);
						}
					}
				}
				catch (InvalidOperationException httpWebResponse)
				{
					httpWebResponse = WebUtil.GetHttpWebResponse(httpWebResponse, ref iodataResponseMessage);
					this.HandleOperationException(httpWebResponse, iodataResponseMessage);
					this.HandleCompleted(perRequest);
				}
				finally
				{
					WebUtil.DisposeMessage(iodataResponseMessage);
				}
				goto IL_161;
				IL_183:
				if ((perRequest != null && (!perRequest.RequestCompleted || !perRequest.RequestCompletedSynchronously)) || base.IsCompletedInternally)
				{
					break;
				}
				continue;
				IL_161:
				if (perRequest != null && perRequest.RequestCompleted && perRequest.RequestCompletedSynchronously && !base.IsCompletedInternally)
				{
					this.FinishCurrentChange(perRequest);
					goto IL_183;
				}
				goto IL_183;
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000264E0 File Offset: 0x000246E0
		internal void CreateNextChange()
		{
			do
			{
				IODataResponseMessage iodataResponseMessage = null;
				try
				{
					ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateNextRequest();
					if (odataRequestMessageWrapper != null || this.entryIndex < this.ChangedEntries.Count)
					{
						if (!this.ChangedEntries[this.entryIndex].ContentGeneratedForSave)
						{
							ContentStream contentStream = this.CreateNonBatchChangeData(this.entryIndex, odataRequestMessageWrapper);
							if (contentStream != null && contentStream.Stream != null)
							{
								odataRequestMessageWrapper.SetRequestStream(contentStream);
							}
							iodataResponseMessage = this.RequestInfo.GetSyncronousResponse(odataRequestMessageWrapper, false);
							this.HandleOperationResponse(iodataResponseMessage);
							base.HandleOperationResponseHeaders((HttpStatusCode)iodataResponseMessage.StatusCode, new HeaderCollection(iodataResponseMessage));
							this.HandleOperationResponseData(iodataResponseMessage);
							this.perRequest = null;
						}
					}
				}
				catch (InvalidOperationException httpWebResponse)
				{
					httpWebResponse = WebUtil.GetHttpWebResponse(httpWebResponse, ref iodataResponseMessage);
					this.HandleOperationException(httpWebResponse, iodataResponseMessage);
				}
				finally
				{
					WebUtil.DisposeMessage(iodataResponseMessage);
				}
			}
			while (this.entryIndex < this.ChangedEntries.Count && !base.IsCompletedInternally);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000265D8 File Offset: 0x000247D8
		protected override void FinishCurrentChange(BaseAsyncResult.PerRequest pereq)
		{
			base.FinishCurrentChange(pereq);
			if (this.ResponseStream.Position != 0L)
			{
				this.ResponseStream.Position = 0L;
				this.HandleOperationResponseData(this.responseMessage, this.ResponseStream);
			}
			else
			{
				this.HandleOperationResponseData(this.responseMessage, null);
			}
			pereq.Dispose();
			this.perRequest = null;
			if (!pereq.RequestCompletedSynchronously && !base.IsCompletedInternally)
			{
				this.BeginCreateNextChange();
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002664C File Offset: 0x0002484C
		protected override void HandleOperationResponse(IODataResponseMessage responseMsg)
		{
			this.responseMessage = responseMsg;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00026658 File Offset: 0x00024858
		protected override DataServiceResponse HandleResponse()
		{
			List<OperationResponse> list = new List<OperationResponse>((this.cachedResponses != null) ? this.cachedResponses.Count : 0);
			DataServiceResponse dataServiceResponse = new DataServiceResponse(null, -1, list, false);
			Exception ex = null;
			try
			{
				foreach (SaveResult.CachedResponse cachedResponse in this.cachedResponses)
				{
					Descriptor descriptor = cachedResponse.Descriptor;
					base.SaveResultProcessed(descriptor);
					OperationResponse operationResponse = new ChangeOperationResponse(cachedResponse.Headers, descriptor);
					operationResponse.StatusCode = (int)cachedResponse.StatusCode;
					if (cachedResponse.Exception != null)
					{
						operationResponse.Error = cachedResponse.Exception;
						if (ex == null)
						{
							ex = cachedResponse.Exception;
						}
					}
					else
					{
						this.cachedResponse = cachedResponse;
						base.HandleOperationResponse(descriptor, cachedResponse.Headers);
					}
					list.Add(operationResponse);
				}
			}
			catch (InvalidOperationException ex2)
			{
				ex = ex2;
			}
			if (ex != null)
			{
				throw new DataServiceRequestException(Strings.DataServiceException_GeneralError, ex, dataServiceResponse);
			}
			return dataServiceResponse;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00026768 File Offset: 0x00024968
		protected override MaterializeAtom GetMaterializer(EntityDescriptor entityDescriptor, ResponseInfo responseInfo)
		{
			ODataEntry odataEntry = ((this.cachedResponse.MaterializerEntry == null) ? null : this.cachedResponse.MaterializerEntry.Entry);
			return new MaterializeAtom(responseInfo, new ODataEntry[] { odataEntry }, entityDescriptor.Entity.GetType(), this.cachedResponse.MaterializerEntry.Format);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000267C3 File Offset: 0x000249C3
		protected override ODataRequestMessageWrapper CreateRequestMessage(string method, Uri requestUri, HeaderCollection headers, HttpStack httpStack, Descriptor descriptor)
		{
			return base.CreateTopLevelRequest(method, requestUri, headers, httpStack, descriptor);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000267D4 File Offset: 0x000249D4
		protected ContentStream CreateNonBatchChangeData(int index, ODataRequestMessageWrapper requestMessage)
		{
			Descriptor descriptor = this.ChangedEntries[index];
			if (descriptor.DescriptorKind == DescriptorKind.Entity && this.streamRequestKind != BaseSaveResult.StreamRequestKind.None)
			{
				if (this.streamRequestKind != BaseSaveResult.StreamRequestKind.None)
				{
					return new ContentStream(this.mediaResourceRequestStream, false);
				}
			}
			else
			{
				if (descriptor.DescriptorKind == DescriptorKind.NamedStream)
				{
					descriptor.ContentGeneratedForSave = true;
					return new ContentStream(this.mediaResourceRequestStream, false);
				}
				if (base.CreateChangeData(index, requestMessage))
				{
					return requestMessage.CachedRequestStream;
				}
			}
			return null;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00026844 File Offset: 0x00024A44
		private ODataRequestMessageWrapper CreateNextRequest()
		{
			bool flag = this.streamRequestKind == BaseSaveResult.StreamRequestKind.None;
			if (this.entryIndex < this.ChangedEntries.Count)
			{
				Descriptor descriptor = this.ChangedEntries[this.entryIndex];
				if (descriptor.DescriptorKind == DescriptorKind.Entity)
				{
					EntityDescriptor entityDescriptor = (EntityDescriptor)descriptor;
					entityDescriptor.CloseSaveStream();
					if (this.streamRequestKind == BaseSaveResult.StreamRequestKind.PutMediaResource && EntityStates.Unchanged == entityDescriptor.State)
					{
						entityDescriptor.ContentGeneratedForSave = true;
						flag = true;
					}
				}
				else if (descriptor.DescriptorKind == DescriptorKind.NamedStream)
				{
					((StreamDescriptor)descriptor).CloseSaveStream();
				}
			}
			if (flag)
			{
				this.entryIndex++;
			}
			ODataRequestMessageWrapper odataRequestMessageWrapper = null;
			if (this.entryIndex < this.ChangedEntries.Count)
			{
				Descriptor descriptor2 = this.ChangedEntries[this.entryIndex];
				Descriptor descriptor3 = descriptor2;
				if (descriptor2.DescriptorKind == DescriptorKind.Entity)
				{
					EntityDescriptor entityDescriptor2 = (EntityDescriptor)descriptor2;
					if ((EntityStates.Unchanged == descriptor2.State || EntityStates.Modified == descriptor2.State) && (odataRequestMessageWrapper = this.CheckAndProcessMediaEntryPut(entityDescriptor2)) != null)
					{
						this.streamRequestKind = BaseSaveResult.StreamRequestKind.PutMediaResource;
						descriptor3 = entityDescriptor2.DefaultStreamDescriptor;
					}
					else if (EntityStates.Added == descriptor2.State && (odataRequestMessageWrapper = this.CheckAndProcessMediaEntryPost(entityDescriptor2)) != null)
					{
						this.streamRequestKind = BaseSaveResult.StreamRequestKind.PostMediaResource;
						entityDescriptor2.StreamState = EntityStates.Added;
					}
					else
					{
						this.streamRequestKind = BaseSaveResult.StreamRequestKind.None;
						odataRequestMessageWrapper = base.CreateRequest(entityDescriptor2);
					}
				}
				else if (descriptor2.DescriptorKind == DescriptorKind.NamedStream)
				{
					odataRequestMessageWrapper = this.CreateNamedStreamRequest((StreamDescriptor)descriptor2);
				}
				else
				{
					odataRequestMessageWrapper = base.CreateRequest((LinkDescriptor)descriptor2);
				}
				if (odataRequestMessageWrapper != null)
				{
					odataRequestMessageWrapper.FireSendingEventHandlers(descriptor3);
				}
			}
			return odataRequestMessageWrapper;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000269B8 File Offset: 0x00024BB8
		private ODataRequestMessageWrapper CheckAndProcessMediaEntryPost(EntityDescriptor entityDescriptor)
		{
			ClientEdmModel model = this.RequestInfo.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entityDescriptor.Entity.GetType()));
			if (!clientTypeAnnotation.IsMediaLinkEntry && !entityDescriptor.IsMediaLinkEntry)
			{
				return null;
			}
			if (clientTypeAnnotation.MediaDataMember == null && entityDescriptor.SaveStream == null)
			{
				throw Error.InvalidOperation(Strings.Context_MLEWithoutSaveStream(clientTypeAnnotation.ElementTypeName));
			}
			ODataRequestMessageWrapper odataRequestMessageWrapper;
			if (clientTypeAnnotation.MediaDataMember != null)
			{
				int num = 0;
				string text;
				if (clientTypeAnnotation.MediaDataMember.MimeTypeProperty == null)
				{
					text = "application/octet-stream";
				}
				else
				{
					object value = clientTypeAnnotation.MediaDataMember.MimeTypeProperty.GetValue(entityDescriptor.Entity);
					string text2 = ((value != null) ? value.ToString() : null);
					if (string.IsNullOrEmpty(text2))
					{
						throw Error.InvalidOperation(Strings.Context_NoContentTypeForMediaLink(clientTypeAnnotation.ElementTypeName, clientTypeAnnotation.MediaDataMember.MimeTypeProperty.PropertyName));
					}
					text = text2;
				}
				object value2 = clientTypeAnnotation.MediaDataMember.GetValue(entityDescriptor.Entity);
				if (value2 == null)
				{
					this.mediaResourceRequestStream = null;
				}
				else
				{
					byte[] array = value2 as byte[];
					if (array == null)
					{
						string text3;
						Encoding utf;
						ContentTypeUtil.ReadContentType(text, out text3, out utf);
						if (utf == null)
						{
							utf = Encoding.UTF8;
							text += ";charset=UTF-8";
						}
						array = utf.GetBytes(ClientConvert.ToString(value2));
					}
					num = array.Length;
					this.mediaResourceRequestStream = new MemoryStream(array, 0, array.Length, false, true);
				}
				HeaderCollection headerCollection = new HeaderCollection();
				headerCollection.SetHeader("Content-Length", num.ToString(CultureInfo.InvariantCulture));
				headerCollection.SetHeader("Content-Type", text);
				odataRequestMessageWrapper = this.CreateMediaResourceRequest(entityDescriptor.GetResourceUri(this.RequestInfo.BaseUriResolver, false), "POST", Util.DataServiceVersion1, clientTypeAnnotation.MediaDataMember == null, true, headerCollection, entityDescriptor);
				odataRequestMessageWrapper.AddHeadersToReset(new string[] { "Content-Length" });
				odataRequestMessageWrapper.AddHeadersToReset(new string[] { "Content-Type" });
			}
			else
			{
				HeaderCollection headerCollection2 = new HeaderCollection();
				IEnumerable<string> enumerable = this.SetupMediaResourceRequest(headerCollection2, entityDescriptor.SaveStream, null);
				odataRequestMessageWrapper = this.CreateMediaResourceRequest(entityDescriptor.GetResourceUri(this.RequestInfo.BaseUriResolver, false), "POST", Util.DataServiceVersion1, clientTypeAnnotation.MediaDataMember == null, true, headerCollection2, entityDescriptor);
				odataRequestMessageWrapper.AddHeadersToReset(enumerable);
			}
			entityDescriptor.State = EntityStates.Modified;
			return odataRequestMessageWrapper;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00026BFC File Offset: 0x00024DFC
		private ODataRequestMessageWrapper CheckAndProcessMediaEntryPut(EntityDescriptor entityDescriptor)
		{
			if (entityDescriptor.SaveStream == null)
			{
				return null;
			}
			Uri latestEditStreamUri = entityDescriptor.GetLatestEditStreamUri();
			if (latestEditStreamUri == null)
			{
				throw Error.InvalidOperation(Strings.Context_SetSaveStreamWithoutEditMediaLink);
			}
			HeaderCollection headerCollection = new HeaderCollection();
			IEnumerable<string> enumerable = this.SetupMediaResourceRequest(headerCollection, entityDescriptor.SaveStream, entityDescriptor.GetLatestStreamETag());
			ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateMediaResourceRequest(latestEditStreamUri, "PUT", Util.DataServiceVersion1, true, false, headerCollection, entityDescriptor.DefaultStreamDescriptor);
			odataRequestMessageWrapper.AddHeadersToReset(enumerable);
			return odataRequestMessageWrapper;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00026C6C File Offset: 0x00024E6C
		private ODataRequestMessageWrapper CreateMediaResourceRequest(Uri requestUri, string method, Version version, bool sendChunked, bool applyResponsePreference, HeaderCollection headers, Descriptor descriptor)
		{
			headers.SetHeaderIfUnset("Content-Type", "*/*");
			if (applyResponsePreference)
			{
				BaseSaveResult.ApplyPreferences(headers, method, this.RequestInfo.AddAndUpdateResponsePreference, ref version);
			}
			headers.SetRequestVersion(version, this.RequestInfo.MaxProtocolVersionAsVersion);
			this.RequestInfo.Format.SetRequestAcceptHeader(headers);
			ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateRequestMessage(method, requestUri, headers, this.RequestInfo.HttpStack, descriptor);
			odataRequestMessageWrapper.SendChunked = sendChunked;
			return odataRequestMessageWrapper;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00026D14 File Offset: 0x00024F14
		private IEnumerable<string> SetupMediaResourceRequest(HeaderCollection headers, DataServiceSaveStream saveStream, string etag)
		{
			this.mediaResourceRequestStream = saveStream.Stream;
			headers.SetHeaders(saveStream.Args.Headers.Where((KeyValuePair<string, string> h) => !string.Equals(h.Key, "Accept", StringComparison.OrdinalIgnoreCase)));
			Dictionary<string, string> headers2 = saveStream.Args.Headers;
			List<string> list;
			if (headers2.ContainsKey("Accept"))
			{
				list = new List<string>(headers2.Count - 1);
				list.AddRange(headers2.Keys.Where((string h) => !string.Equals(h, "Accept", StringComparison.OrdinalIgnoreCase)));
			}
			else
			{
				list = headers2.Keys.ToList<string>();
			}
			if (etag != null)
			{
				headers.SetHeader("If-Match", etag);
				list.Add("If-Match");
			}
			return list;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00026DE0 File Offset: 0x00024FE0
		private void HandleOperationException(InvalidOperationException e, IODataResponseMessage response)
		{
			Descriptor descriptor = this.ChangedEntries[this.entryIndex];
			HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
			Version version = null;
			HeaderCollection headerCollection;
			if (response != null)
			{
				headerCollection = new HeaderCollection(response);
				httpStatusCode = (HttpStatusCode)response.StatusCode;
				base.HandleOperationResponseHeaders(httpStatusCode, headerCollection);
				e = BaseSaveResult.HandleResponse(this.RequestInfo, httpStatusCode, response.GetHeader("DataServiceVersion"), new Func<Stream>(response.GetStream), false, out version);
			}
			else
			{
				headerCollection = new HeaderCollection();
				headerCollection.SetHeader("Content-Type", "text/plain");
				if (e.GetType() != typeof(DataServiceClientException))
				{
					e = new DataServiceClientException(e.Message, e);
				}
			}
			this.cachedResponses.Add(new SaveResult.CachedResponse(descriptor, headerCollection, httpStatusCode, version, null, e));
			this.perRequest = null;
			this.CheckContinueOnError();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00026EAB File Offset: 0x000250AB
		private void CheckContinueOnError()
		{
			if (!Util.IsFlagSet(this.Options, SaveChangesOptions.ContinueOnError))
			{
				base.SetCompleted();
				return;
			}
			this.streamRequestKind = BaseSaveResult.StreamRequestKind.None;
			this.ChangedEntries[this.entryIndex].ContentGeneratedForSave = true;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00026EE0 File Offset: 0x000250E0
		private void HandleOperationResponseData(IODataResponseMessage response)
		{
			using (Stream stream = response.GetStream())
			{
				if (stream != null)
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						if (WebUtil.CopyStream(stream, memoryStream, ref this.buildBatchBuffer) != 0L)
						{
							memoryStream.Position = 0L;
							this.HandleOperationResponseData(response, memoryStream);
						}
						else
						{
							this.HandleOperationResponseData(response, null);
						}
					}
				}
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00026F74 File Offset: 0x00025174
		private void HandleOperationResponseData(IODataResponseMessage responseMsg, Stream responseStream)
		{
			Descriptor descriptor = this.ChangedEntries[this.entryIndex];
			MaterializerEntry materializerEntry = null;
			Version version;
			Exception ex = BaseSaveResult.HandleResponse(this.RequestInfo, (HttpStatusCode)responseMsg.StatusCode, responseMsg.GetHeader("DataServiceVersion"), () => responseStream, false, out version);
			HeaderCollection headerCollection = new HeaderCollection(responseMsg);
			if (responseStream != null && descriptor.DescriptorKind == DescriptorKind.Entity && ex == null)
			{
				EntityDescriptor entityDescriptor = (EntityDescriptor)descriptor;
				if (entityDescriptor.State != EntityStates.Added && entityDescriptor.StreamState != EntityStates.Added && entityDescriptor.State != EntityStates.Modified)
				{
					if (entityDescriptor.StreamState != EntityStates.Modified)
					{
						goto IL_10F;
					}
				}
				try
				{
					ResponseInfo responseInfo = base.CreateResponseInfo(entityDescriptor);
					HttpWebResponseMessage httpWebResponseMessage = new HttpWebResponseMessage(headerCollection, responseMsg.StatusCode, () => responseStream);
					materializerEntry = ODataReaderEntityMaterializer.ParseSingleEntityPayload(httpWebResponseMessage, responseInfo, entityDescriptor.Entity.GetType());
					entityDescriptor.TransientEntityDescriptor = materializerEntry.EntityDescriptor;
				}
				catch (Exception ex2)
				{
					ex = ex2;
					if (!CommonUtil.IsCatchableExceptionType(ex2))
					{
						throw;
					}
				}
			}
			IL_10F:
			this.cachedResponses.Add(new SaveResult.CachedResponse(descriptor, headerCollection, (HttpStatusCode)responseMsg.StatusCode, version, materializerEntry, ex));
			if (ex != null)
			{
				descriptor.SaveError = ex;
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000270C8 File Offset: 0x000252C8
		private ODataRequestMessageWrapper CreateNamedStreamRequest(StreamDescriptor namedStreamInfo)
		{
			Uri latestEditLink = namedStreamInfo.GetLatestEditLink();
			if (latestEditLink == null)
			{
				throw Error.InvalidOperation(Strings.Context_SetSaveStreamWithoutNamedStreamEditLink(namedStreamInfo.Name));
			}
			HeaderCollection headerCollection = new HeaderCollection();
			IEnumerable<string> enumerable = this.SetupMediaResourceRequest(headerCollection, namedStreamInfo.SaveStream, namedStreamInfo.GetLatestETag());
			ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateMediaResourceRequest(latestEditLink, "PUT", Util.DataServiceVersion3, true, false, headerCollection, namedStreamInfo);
			odataRequestMessageWrapper.AddHeadersToReset(enumerable);
			return odataRequestMessageWrapper;
		}

		// Token: 0x04000589 RID: 1417
		private readonly List<SaveResult.CachedResponse> cachedResponses;

		// Token: 0x0400058A RID: 1418
		private MemoryStream inMemoryResponseStream;

		// Token: 0x0400058B RID: 1419
		private IODataResponseMessage responseMessage;

		// Token: 0x0400058C RID: 1420
		private SaveResult.CachedResponse cachedResponse;

		// Token: 0x02000120 RID: 288
		private struct CachedResponse
		{
			// Token: 0x060009A5 RID: 2469 RVA: 0x0002712E File Offset: 0x0002532E
			internal CachedResponse(Descriptor descriptor, HeaderCollection headers, HttpStatusCode statusCode, Version responseVersion, MaterializerEntry entry, Exception exception)
			{
				this.Descriptor = descriptor;
				this.MaterializerEntry = entry;
				this.Exception = exception;
				this.Headers = headers;
				this.StatusCode = statusCode;
				this.Version = responseVersion;
			}

			// Token: 0x0400058F RID: 1423
			public readonly HeaderCollection Headers;

			// Token: 0x04000590 RID: 1424
			public readonly HttpStatusCode StatusCode;

			// Token: 0x04000591 RID: 1425
			public readonly Version Version;

			// Token: 0x04000592 RID: 1426
			public readonly MaterializerEntry MaterializerEntry;

			// Token: 0x04000593 RID: 1427
			public readonly Exception Exception;

			// Token: 0x04000594 RID: 1428
			public readonly Descriptor Descriptor;
		}
	}
}
