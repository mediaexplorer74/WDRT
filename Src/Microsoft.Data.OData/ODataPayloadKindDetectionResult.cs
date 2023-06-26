using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001BE RID: 446
	public sealed class ODataPayloadKindDetectionResult
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x00030936 File Offset: 0x0002EB36
		internal ODataPayloadKindDetectionResult(ODataPayloadKind payloadKind, ODataFormat format)
		{
			this.payloadKind = payloadKind;
			this.format = format;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0003094C File Offset: 0x0002EB4C
		public ODataPayloadKind PayloadKind
		{
			get
			{
				return this.payloadKind;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00030954 File Offset: 0x0002EB54
		public ODataFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x040004AB RID: 1195
		private readonly ODataPayloadKind payloadKind;

		// Token: 0x040004AC RID: 1196
		private readonly ODataFormat format;
	}
}
