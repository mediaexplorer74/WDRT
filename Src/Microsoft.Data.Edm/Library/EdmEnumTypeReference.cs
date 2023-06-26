using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x02000195 RID: 405
	public class EdmEnumTypeReference : EdmTypeReference, IEdmEnumTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x00018467 File Offset: 0x00016667
		public EdmEnumTypeReference(IEdmEnumType enumType, bool isNullable)
			: base(enumType, isNullable)
		{
		}
	}
}
