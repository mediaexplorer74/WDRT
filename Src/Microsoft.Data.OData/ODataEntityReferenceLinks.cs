using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x0200025C RID: 604
	public sealed class ODataEntityReferenceLinks : ODataAnnotatable
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x0004B6CE File Offset: 0x000498CE
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x0004B6D6 File Offset: 0x000498D6
		public long? Count { get; set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x0004B6DF File Offset: 0x000498DF
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x0004B6E7 File Offset: 0x000498E7
		public Uri NextPageLink { get; set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0004B6F0 File Offset: 0x000498F0
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x0004B6F8 File Offset: 0x000498F8
		public IEnumerable<ODataEntityReferenceLink> Links { get; set; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0004B701 File Offset: 0x00049901
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x0004B709 File Offset: 0x00049909
		internal ODataEntityReferenceLinksSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataEntityReferenceLinksSerializationInfo.Validate(value);
			}
		}

		// Token: 0x0400071B RID: 1819
		private ODataEntityReferenceLinksSerializationInfo serializationInfo;
	}
}
