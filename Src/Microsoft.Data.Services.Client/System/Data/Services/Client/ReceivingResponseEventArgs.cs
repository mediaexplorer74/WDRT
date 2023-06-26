using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000084 RID: 132
	public class ReceivingResponseEventArgs : EventArgs
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x0001369E File Offset: 0x0001189E
		public ReceivingResponseEventArgs(IODataResponseMessage responseMessage, Descriptor descriptor)
			: this(responseMessage, descriptor, false)
		{
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000136A9 File Offset: 0x000118A9
		public ReceivingResponseEventArgs(IODataResponseMessage responseMessage, Descriptor descriptor, bool isBatchPart)
		{
			this.ResponseMessage = responseMessage;
			this.Descriptor = descriptor;
			this.IsBatchPart = isBatchPart;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000136C6 File Offset: 0x000118C6
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x000136CE File Offset: 0x000118CE
		public IODataResponseMessage ResponseMessage { get; private set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000136D7 File Offset: 0x000118D7
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x000136DF File Offset: 0x000118DF
		public bool IsBatchPart { get; private set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x000136E8 File Offset: 0x000118E8
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x000136F0 File Offset: 0x000118F0
		public Descriptor Descriptor { get; private set; }
	}
}
