using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001E6 RID: 486
	public class EdmEntityReferenceTypeReference : EdmTypeReference, IEdmEntityReferenceTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000B72 RID: 2930 RVA: 0x0002144D File Offset: 0x0001F64D
		public EdmEntityReferenceTypeReference(IEdmEntityReferenceType entityReferenceType, bool isNullable)
			: base(entityReferenceType, isNullable)
		{
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00021457 File Offset: 0x0001F657
		public IEdmEntityReferenceType EntityReferenceDefinition
		{
			get
			{
				return (IEdmEntityReferenceType)base.Definition;
			}
		}
	}
}
