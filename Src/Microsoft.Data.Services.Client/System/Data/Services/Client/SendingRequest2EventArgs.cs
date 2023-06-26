using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000089 RID: 137
	public class SendingRequest2EventArgs : EventArgs
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x00013F2D File Offset: 0x0001212D
		internal SendingRequest2EventArgs(IODataRequestMessage requestMessage, Descriptor descriptor, bool isBatchPart)
		{
			this.RequestMessage = requestMessage;
			this.Descriptor = descriptor;
			this.IsBatchPart = isBatchPart;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00013F4A File Offset: 0x0001214A
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00013F52 File Offset: 0x00012152
		public IODataRequestMessage RequestMessage { get; private set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00013F5B File Offset: 0x0001215B
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00013F63 File Offset: 0x00012163
		public Descriptor Descriptor { get; private set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00013F6C File Offset: 0x0001216C
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00013F74 File Offset: 0x00012174
		public bool IsBatchPart { get; private set; }
	}
}
