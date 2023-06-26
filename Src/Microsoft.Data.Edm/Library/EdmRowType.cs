using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001DB RID: 475
	public class EdmRowType : EdmStructuredType, IEdmRowType, IEdmStructuredType, IEdmType, IEdmElement
	{
		// Token: 0x06000B4D RID: 2893 RVA: 0x00020E7B File Offset: 0x0001F07B
		public EdmRowType()
			: base(false, false, null)
		{
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00020E86 File Offset: 0x0001F086
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Row;
			}
		}
	}
}
