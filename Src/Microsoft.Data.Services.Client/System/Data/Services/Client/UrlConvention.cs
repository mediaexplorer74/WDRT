using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000015 RID: 21
	internal sealed class UrlConvention
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003945 File Offset: 0x00001B45
		private UrlConvention(bool generateKeyAsSegment)
		{
			this.generateKeyAsSegment = generateKeyAsSegment;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003954 File Offset: 0x00001B54
		internal bool GenerateKeyAsSegment
		{
			get
			{
				return this.generateKeyAsSegment;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000395C File Offset: 0x00001B5C
		internal static UrlConvention CreateWithExplicitValue(bool generateKeyAsSegment)
		{
			return new UrlConvention(generateKeyAsSegment);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003964 File Offset: 0x00001B64
		internal void AddRequiredHeaders(HeaderCollection requestHeaders)
		{
			if (this.GenerateKeyAsSegment)
			{
				requestHeaders.SetHeader("DataServiceUrlConventions", "KeyAsSegment");
			}
		}

		// Token: 0x04000018 RID: 24
		private const string ConventionTermNamespace = "Com.Microsoft.Data.Services.Conventions.V1";

		// Token: 0x04000019 RID: 25
		private const string ConventionTermName = "UrlConventions";

		// Token: 0x0400001A RID: 26
		private const string KeyAsSegmentConventionName = "KeyAsSegment";

		// Token: 0x0400001B RID: 27
		private const string UrlConventionHeaderName = "DataServiceUrlConventions";

		// Token: 0x0400001C RID: 28
		private readonly bool generateKeyAsSegment;
	}
}
