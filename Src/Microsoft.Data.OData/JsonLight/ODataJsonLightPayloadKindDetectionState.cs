using System;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000142 RID: 322
	internal sealed class ODataJsonLightPayloadKindDetectionState
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x0001BE90 File Offset: 0x0001A090
		internal ODataJsonLightPayloadKindDetectionState(ODataJsonLightMetadataUriParseResult metadataUriParseResult)
		{
			this.metadataUriParseResult = metadataUriParseResult;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0001BE9F File Offset: 0x0001A09F
		internal ODataJsonLightMetadataUriParseResult MetadataUriParseResult
		{
			get
			{
				return this.metadataUriParseResult;
			}
		}

		// Token: 0x0400034D RID: 845
		private readonly ODataJsonLightMetadataUriParseResult metadataUriParseResult;
	}
}
