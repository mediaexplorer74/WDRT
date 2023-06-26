using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000262 RID: 610
	internal sealed class ODataBatchOperationMessage : ODataMessage
	{
		// Token: 0x0600142C RID: 5164 RVA: 0x0004BAB8 File Offset: 0x00049CB8
		internal ODataBatchOperationMessage(Func<Stream> contentStreamCreatorFunc, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver, bool writing)
			: base(writing, false, -1L)
		{
			this.contentStreamCreatorFunc = contentStreamCreatorFunc;
			this.operationListener = operationListener;
			this.headers = headers;
			this.urlResolver = urlResolver;
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0004BAE2 File Offset: 0x00049CE2
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.headers ?? Enumerable.Empty<KeyValuePair<string, string>>();
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0004BAF4 File Offset: 0x00049CF4
		public override string GetHeader(string headerName)
		{
			string text;
			if (this.headers != null && this.headers.TryGetValue(headerName, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0004BB1C File Offset: 0x00049D1C
		public override void SetHeader(string headerName, string headerValue)
		{
			this.VerifyNotCompleted();
			base.VerifyCanSetHeader();
			if (headerValue == null)
			{
				if (this.headers != null)
				{
					this.headers.Remove(headerName);
					return;
				}
			}
			else
			{
				if (this.headers == null)
				{
					this.headers = new ODataBatchOperationHeaders();
				}
				this.headers[headerName] = headerValue;
			}
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0004BB70 File Offset: 0x00049D70
		public override Stream GetStream()
		{
			this.VerifyNotCompleted();
			this.operationListener.BatchOperationContentStreamRequested();
			Stream stream = this.contentStreamCreatorFunc();
			this.PartHeaderProcessingCompleted();
			return stream;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0004BBB4 File Offset: 0x00049DB4
		public override Task<Stream> GetStreamAsync()
		{
			this.VerifyNotCompleted();
			Task task2 = this.operationListener.BatchOperationContentStreamRequestedAsync();
			Stream contentStream = this.contentStreamCreatorFunc();
			this.PartHeaderProcessingCompleted();
			return task2.FollowOnSuccessWith((Task task) => contentStream);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0004BC04 File Offset: 0x00049E04
		internal override TInterface QueryInterface<TInterface>()
		{
			return default(TInterface);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0004BC1A File Offset: 0x00049E1A
		internal Uri ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(payloadUri, "payloadUri");
			if (this.urlResolver != null)
			{
				return this.urlResolver.ResolveUrl(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0004BC3E File Offset: 0x00049E3E
		internal void PartHeaderProcessingCompleted()
		{
			this.contentStreamCreatorFunc = null;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0004BC47 File Offset: 0x00049E47
		internal void VerifyNotCompleted()
		{
			if (this.contentStreamCreatorFunc == null)
			{
				throw new ODataException(Strings.ODataBatchOperationMessage_VerifyNotCompleted);
			}
		}

		// Token: 0x04000728 RID: 1832
		private readonly IODataBatchOperationListener operationListener;

		// Token: 0x04000729 RID: 1833
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x0400072A RID: 1834
		private Func<Stream> contentStreamCreatorFunc;

		// Token: 0x0400072B RID: 1835
		private ODataBatchOperationHeaders headers;
	}
}
