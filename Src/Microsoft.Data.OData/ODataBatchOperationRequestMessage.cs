using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000266 RID: 614
	public sealed class ODataBatchOperationRequestMessage : IODataRequestMessageAsync, IODataRequestMessage, IODataUrlResolver
	{
		// Token: 0x06001444 RID: 5188 RVA: 0x0004BDBB File Offset: 0x00049FBB
		private ODataBatchOperationRequestMessage(Func<Stream> contentStreamCreatorFunc, string method, Uri requestUrl, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver, bool writing)
		{
			this.Method = method;
			this.Url = requestUrl;
			this.message = new ODataBatchOperationMessage(contentStreamCreatorFunc, headers, operationListener, urlResolver, writing);
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0004BDE5 File Offset: 0x00049FE5
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0004BDF2 File Offset: 0x00049FF2
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x0004BDFA File Offset: 0x00049FFA
		public Uri Url { get; set; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0004BE03 File Offset: 0x0004A003
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x0004BE0B File Offset: 0x0004A00B
		public string Method { get; set; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0004BE14 File Offset: 0x0004A014
		internal ODataBatchOperationMessage OperationMessage
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0004BE1C File Offset: 0x0004A01C
		public string GetHeader(string headerName)
		{
			return this.message.GetHeader(headerName);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0004BE2A File Offset: 0x0004A02A
		public void SetHeader(string headerName, string headerValue)
		{
			this.message.SetHeader(headerName, headerValue);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0004BE39 File Offset: 0x0004A039
		public Stream GetStream()
		{
			return this.message.GetStream();
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0004BE46 File Offset: 0x0004A046
		public Task<Stream> GetStreamAsync()
		{
			return this.message.GetStreamAsync();
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0004BE53 File Offset: 0x0004A053
		Uri IODataUrlResolver.ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			return this.message.ResolveUrl(baseUri, payloadUri);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0004BE80 File Offset: 0x0004A080
		internal static ODataBatchOperationRequestMessage CreateWriteMessage(Stream outputStream, string method, Uri requestUrl, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationWriteStream(outputStream, operationListener);
			return new ODataBatchOperationRequestMessage(func, method, requestUrl, null, operationListener, urlResolver, true);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0004BEE4 File Offset: 0x0004A0E4
		internal static ODataBatchOperationRequestMessage CreateReadMessage(ODataBatchReaderStream batchReaderStream, string method, Uri requestUrl, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationReadStream(batchReaderStream, headers, operationListener);
			return new ODataBatchOperationRequestMessage(func, method, requestUrl, headers, operationListener, urlResolver, false);
		}

		// Token: 0x0400072E RID: 1838
		private readonly ODataBatchOperationMessage message;
	}
}
