using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000104 RID: 260
	public sealed class ChangeOperationResponse : OperationResponse
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x00023712 File Offset: 0x00021912
		internal ChangeOperationResponse(HeaderCollection headers, Descriptor descriptor)
			: base(headers)
		{
			this.descriptor = descriptor;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00023722 File Offset: 0x00021922
		public Descriptor Descriptor
		{
			get
			{
				return this.descriptor;
			}
		}

		// Token: 0x040004FC RID: 1276
		private Descriptor descriptor;
	}
}
