﻿using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001A7 RID: 423
	public class EdmCollectionType : EdmType, IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x00018910 File Offset: 0x00016B10
		public EdmCollectionType(IEdmTypeReference elementType)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(elementType, "elementType");
			this.elementType = elementType;
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001892B File Offset: 0x00016B2B
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001892E File Offset: 0x00016B2E
		public IEdmTypeReference ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x04000476 RID: 1142
		private readonly IEdmTypeReference elementType;
	}
}
