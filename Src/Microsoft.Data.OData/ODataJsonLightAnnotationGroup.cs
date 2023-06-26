using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Data.OData
{
	// Token: 0x02000162 RID: 354
	[DebuggerDisplay("{Name}")]
	internal sealed class ODataJsonLightAnnotationGroup
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0001EAFC File Offset: 0x0001CCFC
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x0001EB04 File Offset: 0x0001CD04
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0001EB0D File Offset: 0x0001CD0D
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x0001EB15 File Offset: 0x0001CD15
		public IDictionary<string, object> Annotations
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

		// Token: 0x04000398 RID: 920
		private string name;

		// Token: 0x04000399 RID: 921
		private IDictionary<string, object> annotations;
	}
}
