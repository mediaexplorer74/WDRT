using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x0200028F RID: 655
	internal sealed class ODataRequestMessage : ODataMessage, IODataRequestMessageAsync, IODataRequestMessage
	{
		// Token: 0x06001613 RID: 5651 RVA: 0x00051299 File Offset: 0x0004F499
		internal ODataRequestMessage(IODataRequestMessage requestMessage, bool writing, bool disableMessageStreamDisposal, long maxMessageSize)
			: base(writing, disableMessageStreamDisposal, maxMessageSize)
		{
			this.requestMessage = requestMessage;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x000512AC File Offset: 0x0004F4AC
		// (set) Token: 0x06001615 RID: 5653 RVA: 0x000512B9 File Offset: 0x0004F4B9
		public Uri Url
		{
			get
			{
				return this.requestMessage.Url;
			}
			set
			{
				throw new ODataException(Strings.ODataMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x000512C5 File Offset: 0x0004F4C5
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x000512D2 File Offset: 0x0004F4D2
		public string Method
		{
			get
			{
				return this.requestMessage.Method;
			}
			set
			{
				throw new ODataException(Strings.ODataMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x000512DE File Offset: 0x0004F4DE
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.requestMessage.Headers;
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x000512EB File Offset: 0x0004F4EB
		public override string GetHeader(string headerName)
		{
			return this.requestMessage.GetHeader(headerName);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x000512F9 File Offset: 0x0004F4F9
		public override void SetHeader(string headerName, string headerValue)
		{
			base.VerifyCanSetHeader();
			this.requestMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0005130E File Offset: 0x0004F50E
		public override Stream GetStream()
		{
			return base.GetStream(new Func<Stream>(this.requestMessage.GetStream), true);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0005132C File Offset: 0x0004F52C
		public override Task<Stream> GetStreamAsync()
		{
			IODataRequestMessageAsync iodataRequestMessageAsync = this.requestMessage as IODataRequestMessageAsync;
			if (iodataRequestMessageAsync == null)
			{
				throw new ODataException(Strings.ODataRequestMessage_AsyncNotAvailable);
			}
			return base.GetStreamAsync(new Func<Task<Stream>>(iodataRequestMessageAsync.GetStreamAsync), true);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00051367 File Offset: 0x0004F567
		internal override TInterface QueryInterface<TInterface>()
		{
			return this.requestMessage as TInterface;
		}

		// Token: 0x04000868 RID: 2152
		private readonly IODataRequestMessage requestMessage;
	}
}
