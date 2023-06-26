using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200010A RID: 266
	public abstract class DataServiceRequest
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x000243C0 File Offset: 0x000225C0
		internal DataServiceRequest()
		{
			this.PayloadKind = ODataPayloadKind.Unsupported;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060008B2 RID: 2226
		public abstract Type ElementType { get; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060008B3 RID: 2227
		// (set) Token: 0x060008B4 RID: 2228
		public abstract Uri RequestUri { get; internal set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060008B5 RID: 2229
		internal abstract ProjectionPlan Plan { get; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x000243D3 File Offset: 0x000225D3
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x000243DB File Offset: 0x000225DB
		internal ODataPayloadKind PayloadKind { get; set; }

		// Token: 0x060008B8 RID: 2232 RVA: 0x000243E4 File Offset: 0x000225E4
		internal static MaterializeAtom Materialize(ResponseInfo responseInfo, QueryComponents queryComponents, ProjectionPlan plan, string contentType, IODataResponseMessage message, ODataPayloadKind expectedPayloadKind)
		{
			if (message.StatusCode == 204 || string.IsNullOrEmpty(contentType))
			{
				return MaterializeAtom.EmptyResults;
			}
			return new MaterializeAtom(responseInfo, queryComponents, plan, message, expectedPayloadKind);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00024410 File Offset: 0x00022610
		internal static DataServiceRequest GetInstance(Type elementType, Uri requestUri)
		{
			Type type = typeof(DataServiceRequest<>).MakeGenericType(new Type[] { elementType });
			return (DataServiceRequest)Activator.CreateInstance(type, new object[] { requestUri });
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00024450 File Offset: 0x00022650
		internal static IEnumerable<TElement> EndExecute<TElement>(object source, DataServiceContext context, string method, IAsyncResult asyncResult)
		{
			IEnumerable<TElement> enumerable;
			try
			{
				QueryResult queryResult = QueryResult.EndExecuteQuery<TElement>(source, method, asyncResult);
				enumerable = queryResult.ProcessResult<TElement>(queryResult.ServiceRequest.Plan);
			}
			catch (DataServiceQueryException ex)
			{
				Exception ex2 = ex;
				while (ex2.InnerException != null)
				{
					ex2 = ex2.InnerException;
				}
				DataServiceClientException ex3 = ex2 as DataServiceClientException;
				if (!context.IgnoreResourceNotFoundException || ex3 == null || ex3.StatusCode != 404)
				{
					throw;
				}
				enumerable = (IEnumerable<TElement>)new QueryOperationResponse<TElement>(ex.Response.HeaderCollection, ex.Response.Query, MaterializeAtom.EmptyResults)
				{
					StatusCode = 404
				};
			}
			return enumerable;
		}

		// Token: 0x060008BB RID: 2235
		internal abstract QueryComponents QueryComponents(ClientEdmModel model);

		// Token: 0x060008BC RID: 2236 RVA: 0x00024500 File Offset: 0x00022700
		internal QueryOperationResponse<TElement> Execute<TElement>(DataServiceContext context, QueryComponents queryComponents)
		{
			QueryResult queryResult = null;
			QueryOperationResponse<TElement> queryOperationResponse;
			try
			{
				Uri uri = queryComponents.Uri;
				DataServiceRequest<TElement> dataServiceRequest = new DataServiceRequest<TElement>(uri, queryComponents, this.Plan);
				queryResult = dataServiceRequest.CreateExecuteResult(this, context, null, null, "Execute");
				queryResult.AllowDirectNetworkStreamReading = context.AllowDirectNetworkStreamReading;
				queryResult.ExecuteQuery();
				queryOperationResponse = queryResult.ProcessResult<TElement>(this.Plan);
			}
			catch (InvalidOperationException ex)
			{
				if (queryResult != null)
				{
					QueryOperationResponse response = queryResult.GetResponse<TElement>(MaterializeAtom.EmptyResults);
					if (response != null)
					{
						if (context.IgnoreResourceNotFoundException)
						{
							DataServiceClientException ex2 = ex as DataServiceClientException;
							if (ex2 != null && ex2.StatusCode == 404)
							{
								return (QueryOperationResponse<TElement>)response;
							}
						}
						response.Error = ex;
						throw new DataServiceQueryException(Strings.DataServiceException_GeneralError, ex, response);
					}
				}
				throw;
			}
			return queryOperationResponse;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000245C4 File Offset: 0x000227C4
		internal long GetQuerySetCount(DataServiceContext context)
		{
			Version version = this.QueryComponents(context.Model).Version;
			if (version == null || version.Major < 2)
			{
				version = Util.DataServiceVersion2;
			}
			QueryResult queryResult = null;
			QueryComponents queryComponents = this.QueryComponents(context.Model);
			Uri uri = queryComponents.Uri;
			DataServiceRequest<long> dataServiceRequest = new DataServiceRequest<long>(uri, queryComponents, null);
			HeaderCollection headerCollection = new HeaderCollection();
			headerCollection.SetRequestVersion(version, context.MaxProtocolVersionAsVersion);
			context.Format.SetRequestAcceptHeaderForCount(headerCollection);
			string text = "GET";
			ODataRequestMessageWrapper odataRequestMessageWrapper = context.CreateODataRequestMessage(context.CreateRequestArgsAndFireBuildingRequest(text, uri, headerCollection, context.HttpStack, null), new string[] { "Accept" }, null);
			queryResult = new QueryResult(this, "Execute", dataServiceRequest, odataRequestMessageWrapper, new RequestInfo(context), null, null);
			queryResult.AllowDirectNetworkStreamReading = context.AllowDirectNetworkStreamReading;
			IODataResponseMessage iodataResponseMessage = null;
			long num2;
			try
			{
				iodataResponseMessage = queryResult.ExecuteQuery();
				if (HttpStatusCode.NoContent == queryResult.StatusCode)
				{
					throw new DataServiceQueryException(Strings.DataServiceRequest_FailGetCount, queryResult.Failure);
				}
				StreamReader streamReader = new StreamReader(queryResult.GetResponseStream());
				long num = -1L;
				try
				{
					num = XmlConvert.ToInt64(streamReader.ReadToEnd());
				}
				finally
				{
					streamReader.Close();
				}
				num2 = num;
			}
			catch (InvalidOperationException ex)
			{
				QueryOperationResponse response = queryResult.GetResponse<long>(MaterializeAtom.EmptyResults);
				if (response != null)
				{
					response.Error = ex;
					throw new DataServiceQueryException(Strings.DataServiceException_GeneralError, ex, response);
				}
				throw;
			}
			finally
			{
				WebUtil.DisposeMessage(iodataResponseMessage);
			}
			return num2;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00024750 File Offset: 0x00022950
		internal IAsyncResult BeginExecute(object source, DataServiceContext context, AsyncCallback callback, object state, string method)
		{
			QueryResult queryResult = this.CreateExecuteResult(source, context, callback, state, method);
			queryResult.BeginExecuteQuery();
			return queryResult;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00024774 File Offset: 0x00022974
		private QueryResult CreateExecuteResult(object source, DataServiceContext context, AsyncCallback callback, object state, string method)
		{
			QueryComponents queryComponents = this.QueryComponents(context.Model);
			RequestInfo requestInfo = new RequestInfo(context);
			if (queryComponents.UriOperationParameters != null)
			{
				Serializer serializer = new Serializer(requestInfo);
				this.RequestUri = serializer.WriteUriOperationParametersToUri(this.RequestUri, queryComponents.UriOperationParameters);
			}
			HeaderCollection headerCollection = new HeaderCollection();
			if (string.CompareOrdinal("POST", queryComponents.HttpMethod) == 0)
			{
				if (queryComponents.BodyOperationParameters == null)
				{
					headerCollection.SetHeader("Content-Length", "0");
				}
				else
				{
					context.Format.SetRequestContentTypeForOperationParameters(headerCollection);
				}
			}
			headerCollection.SetRequestVersion(queryComponents.Version, requestInfo.MaxProtocolVersionAsVersion);
			requestInfo.Format.SetRequestAcceptHeaderForQuery(headerCollection, queryComponents);
			ODataRequestMessageWrapper odataRequestMessageWrapper = new RequestInfo(context).WriteHelper.CreateRequestMessage(context.CreateRequestArgsAndFireBuildingRequest(queryComponents.HttpMethod, this.RequestUri, headerCollection, context.HttpStack, null));
			odataRequestMessageWrapper.FireSendingRequest2(null);
			QueryResult queryResult;
			if (queryComponents.BodyOperationParameters != null)
			{
				Serializer serializer2 = new Serializer(requestInfo);
				serializer2.WriteBodyOperationParameters(queryComponents.BodyOperationParameters, odataRequestMessageWrapper);
				queryResult = new QueryResult(source, method, this, odataRequestMessageWrapper, requestInfo, callback, state, odataRequestMessageWrapper.CachedRequestStream);
			}
			else
			{
				queryResult = new QueryResult(source, method, this, odataRequestMessageWrapper, requestInfo, callback, state);
			}
			return queryResult;
		}
	}
}
