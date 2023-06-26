using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001EB RID: 491
	public class EdmRowTypeReference : EdmTypeReference, IEdmRowTypeReference, IEdmStructuredTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x00021CCD File Offset: 0x0001FECD
		public EdmRowTypeReference(IEdmRowType rowType, bool isNullable)
			: base(rowType, isNullable)
		{
		}
	}
}
