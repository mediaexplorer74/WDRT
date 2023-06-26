using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001A8 RID: 424
	public class EdmCollectionTypeReference : EdmTypeReference, IEdmCollectionTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x00018936 File Offset: 0x00016B36
		public EdmCollectionTypeReference(IEdmCollectionType collectionType, bool isNullable)
			: base(collectionType, isNullable)
		{
		}
	}
}
