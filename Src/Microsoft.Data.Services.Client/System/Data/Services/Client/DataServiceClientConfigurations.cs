using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000040 RID: 64
	public class DataServiceClientConfigurations
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000AD56 File Offset: 0x00008F56
		internal DataServiceClientConfigurations(object sender)
		{
			this.ResponsePipeline = new DataServiceClientResponsePipelineConfiguration(sender);
			this.RequestPipeline = new DataServiceClientRequestPipelineConfiguration();
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000AD75 File Offset: 0x00008F75
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000AD7D File Offset: 0x00008F7D
		public DataServiceClientResponsePipelineConfiguration ResponsePipeline { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000AD86 File Offset: 0x00008F86
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000AD8E File Offset: 0x00008F8E
		public DataServiceClientRequestPipelineConfiguration RequestPipeline { get; private set; }
	}
}
