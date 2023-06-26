using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000264 RID: 612
	public sealed class ODataBatchOperationResponseMessage : IODataResponseMessageAsync, IODataResponseMessage, IODataUrlResolver
	{
		// Token: 0x06001437 RID: 5175 RVA: 0x0004BC5C File Offset: 0x00049E5C
		private ODataBatchOperationResponseMessage(Func<Stream> contentStreamCreatorFunc, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver, bool writing)
		{
			this.message = new ODataBatchOperationMessage(contentStreamCreatorFunc, headers, operationListener, urlResolver, writing);
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0004BC76 File Offset: 0x00049E76
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x0004BC7E File Offset: 0x00049E7E
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.message.VerifyNotCompleted();
				this.statusCode = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0004BC92 File Offset: 0x00049E92
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0004BC9F File Offset: 0x00049E9F
		internal ODataBatchOperationMessage OperationMessage
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0004BCA7 File Offset: 0x00049EA7
		public string GetHeader(string headerName)
		{
			return this.message.GetHeader(headerName);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0004BCB5 File Offset: 0x00049EB5
		public void SetHeader(string headerName, string headerValue)
		{
			this.message.SetHeader(headerName, headerValue);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0004BCC4 File Offset: 0x00049EC4
		public Stream GetStream()
		{
			return this.message.GetStream();
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0004BCD1 File Offset: 0x00049ED1
		public Task<Stream> GetStreamAsync()
		{
			return this.message.GetStreamAsync();
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0004BCDE File Offset: 0x00049EDE
		Uri IODataUrlResolver.ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			return this.message.ResolveUrl(baseUri, payloadUri);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0004BD08 File Offset: 0x00049F08
		internal static ODataBatchOperationResponseMessage CreateWriteMessage(Stream outputStream, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationWriteStream(outputStream, operationListener);
			return new ODataBatchOperationResponseMessage(func, null, operationListener, urlResolver, true);
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0004BD68 File Offset: 0x00049F68
		internal static ODataBatchOperationResponseMessage CreateReadMessage(ODataBatchReaderStream batchReaderStream, int statusCode, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationReadStream(batchReaderStream, headers, operationListener);
			return new ODataBatchOperationResponseMessage(func, headers, operationListener, urlResolver, false)
			{
				statusCode = statusCode
			};
		}

		// Token: 0x0400072C RID: 1836
		private readonly ODataBatchOperationMessage message;

		// Token: 0x0400072D RID: 1837
		private int statusCode;
	}
}
