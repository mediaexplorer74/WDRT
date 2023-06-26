using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x020000FF RID: 255
	internal sealed class ODataJsonLightRawAnnotationSet
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x000182BF File Offset: 0x000164BF
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x000182C7 File Offset: 0x000164C7
		public IDictionary<string, string> Annotations
		{
			get
			{
				return this.annotations;
			}
			set
			{
				this.annotations = value;
			}
		}

		// Token: 0x040002A3 RID: 675
		private IDictionary<string, string> annotations;
	}
}
