using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001CE RID: 462
	public class EdmEntityReferenceType : EdmType, IEdmEntityReferenceType, IEdmType, IEdmElement
	{
		// Token: 0x06000AEE RID: 2798 RVA: 0x000202AC File Offset: 0x0001E4AC
		public EdmEntityReferenceType(IEdmEntityType entityType)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(entityType, "entityType");
			this.entityType = entityType;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x000202C7 File Offset: 0x0001E4C7
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.EntityReference;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000202CA File Offset: 0x0001E4CA
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x04000527 RID: 1319
		private readonly IEdmEntityType entityType;
	}
}
