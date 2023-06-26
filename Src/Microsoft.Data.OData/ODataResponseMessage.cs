using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x0200028E RID: 654
	internal sealed class ODataResponseMessage : ODataMessage, IODataResponseMessageAsync, IODataResponseMessage
	{
		// Token: 0x0600160A RID: 5642 RVA: 0x000511D3 File Offset: 0x0004F3D3
		internal ODataResponseMessage(IODataResponseMessage responseMessage, bool writing, bool disableMessageStreamDisposal, long maxMessageSize)
			: base(writing, disableMessageStreamDisposal, maxMessageSize)
		{
			this.responseMessage = responseMessage;
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x000511E6 File Offset: 0x0004F3E6
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x000511F3 File Offset: 0x0004F3F3
		public int StatusCode
		{
			get
			{
				return this.responseMessage.StatusCode;
			}
			set
			{
				throw new ODataException(Strings.ODataMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x000511FF File Offset: 0x0004F3FF
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.responseMessage.Headers;
			}
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0005120C File Offset: 0x0004F40C
		public override string GetHeader(string headerName)
		{
			return this.responseMessage.GetHeader(headerName);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0005121A File Offset: 0x0004F41A
		public override void SetHeader(string headerName, string headerValue)
		{
			base.VerifyCanSetHeader();
			this.responseMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0005122F File Offset: 0x0004F42F
		public override Stream GetStream()
		{
			return base.GetStream(new Func<Stream>(this.responseMessage.GetStream), false);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0005124C File Offset: 0x0004F44C
		public override Task<Stream> GetStreamAsync()
		{
			IODataResponseMessageAsync iodataResponseMessageAsync = this.responseMessage as IODataResponseMessageAsync;
			if (iodataResponseMessageAsync == null)
			{
				throw new ODataException(Strings.ODataResponseMessage_AsyncNotAvailable);
			}
			return base.GetStreamAsync(new Func<Task<Stream>>(iodataResponseMessageAsync.GetStreamAsync), false);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00051287 File Offset: 0x0004F487
		internal override TInterface QueryInterface<TInterface>()
		{
			return this.responseMessage as TInterface;
		}

		// Token: 0x04000867 RID: 2151
		private readonly IODataResponseMessage responseMessage;
	}
}
