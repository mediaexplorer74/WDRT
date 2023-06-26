using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200016D RID: 365
	internal sealed class ODataJsonLightMetadataUriParseResult
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x000215AB File Offset: 0x0001F7AB
		internal ODataJsonLightMetadataUriParseResult(Uri metadataUriFromPayload)
		{
			this.metadataUriFromPayload = metadataUriFromPayload;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x000215BA File Offset: 0x0001F7BA
		internal Uri MetadataUri
		{
			get
			{
				return this.metadataUriFromPayload;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x000215C2 File Offset: 0x0001F7C2
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x000215CA File Offset: 0x0001F7CA
		internal Uri MetadataDocumentUri
		{
			get
			{
				return this.metadataDocumentUri;
			}
			set
			{
				this.metadataDocumentUri = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000215D3 File Offset: 0x0001F7D3
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x000215DB File Offset: 0x0001F7DB
		internal string Fragment
		{
			get
			{
				return this.fragment;
			}
			set
			{
				this.fragment = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x000215E4 File Offset: 0x0001F7E4
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x000215EC File Offset: 0x0001F7EC
		internal string SelectQueryOption
		{
			get
			{
				return this.selectQueryOption;
			}
			set
			{
				this.selectQueryOption = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x000215F5 File Offset: 0x0001F7F5
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x000215FD File Offset: 0x0001F7FD
		internal IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
			set
			{
				this.entitySet = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00021606 File Offset: 0x0001F806
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x0002160E File Offset: 0x0001F80E
		internal IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
			set
			{
				this.edmType = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00021617 File Offset: 0x0001F817
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0002161F File Offset: 0x0001F81F
		internal IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
			set
			{
				this.navigationProperty = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00021628 File Offset: 0x0001F828
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00021630 File Offset: 0x0001F830
		internal IEnumerable<ODataPayloadKind> DetectedPayloadKinds
		{
			get
			{
				return this.detectedPayloadKinds;
			}
			set
			{
				this.detectedPayloadKinds = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00021639 File Offset: 0x0001F839
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00021641 File Offset: 0x0001F841
		internal bool IsNullProperty
		{
			get
			{
				return this.isNullProperty;
			}
			set
			{
				this.isNullProperty = value;
			}
		}

		// Token: 0x040003BF RID: 959
		private readonly Uri metadataUriFromPayload;

		// Token: 0x040003C0 RID: 960
		private Uri metadataDocumentUri;

		// Token: 0x040003C1 RID: 961
		private string fragment;

		// Token: 0x040003C2 RID: 962
		private string selectQueryOption;

		// Token: 0x040003C3 RID: 963
		private IEdmEntitySet entitySet;

		// Token: 0x040003C4 RID: 964
		private IEdmType edmType;

		// Token: 0x040003C5 RID: 965
		private IEdmNavigationProperty navigationProperty;

		// Token: 0x040003C6 RID: 966
		private IEnumerable<ODataPayloadKind> detectedPayloadKinds;

		// Token: 0x040003C7 RID: 967
		private bool isNullProperty;
	}
}
