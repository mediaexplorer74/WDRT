using System;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000051 RID: 81
	public sealed class ODataUrlConventions
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00008244 File Offset: 0x00006444
		private ODataUrlConventions(UrlConvention urlConvention)
		{
			this.urlConvention = urlConvention;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00008253 File Offset: 0x00006453
		public static ODataUrlConventions Default
		{
			get
			{
				return ODataUrlConventions.DefaultInstance;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000825A File Offset: 0x0000645A
		public static ODataUrlConventions KeyAsSegment
		{
			get
			{
				return ODataUrlConventions.KeyAsSegmentInstance;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00008261 File Offset: 0x00006461
		internal UrlConvention UrlConvention
		{
			get
			{
				return this.urlConvention;
			}
		}

		// Token: 0x04000085 RID: 133
		private static readonly ODataUrlConventions DefaultInstance = new ODataUrlConventions(UrlConvention.CreateWithExplicitValue(false));

		// Token: 0x04000086 RID: 134
		private static readonly ODataUrlConventions KeyAsSegmentInstance = new ODataUrlConventions(UrlConvention.CreateWithExplicitValue(true));

		// Token: 0x04000087 RID: 135
		private readonly UrlConvention urlConvention;
	}
}
