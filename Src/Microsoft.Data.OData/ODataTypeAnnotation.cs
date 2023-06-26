using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x0200017C RID: 380
	internal sealed class ODataTypeAnnotation
	{
		// Token: 0x06000ABD RID: 2749 RVA: 0x00023D85 File Offset: 0x00021F85
		public ODataTypeAnnotation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmEntitySet>(entitySet, "entitySet");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			this.entitySet = entitySet;
			this.type = entityType.ToTypeReference(true);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00023DB7 File Offset: 0x00021FB7
		public ODataTypeAnnotation(IEdmComplexTypeReference complexType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmComplexTypeReference>(complexType, "complexType");
			this.type = complexType;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00023DD1 File Offset: 0x00021FD1
		public ODataTypeAnnotation(IEdmCollectionTypeReference collectionType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(collectionType, "collectionType");
			this.type = collectionType;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00023DEB File Offset: 0x00021FEB
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00023DF3 File Offset: 0x00021FF3
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x040003FC RID: 1020
		private readonly IEdmTypeReference type;

		// Token: 0x040003FD RID: 1021
		private readonly IEdmEntitySet entitySet;
	}
}
