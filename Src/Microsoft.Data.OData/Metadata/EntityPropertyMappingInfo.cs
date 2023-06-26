using System;
using System.Data.Services.Common;
using System.Diagnostics;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000268 RID: 616
	[DebuggerDisplay("EntityPropertyMappingInfo {DefiningType}")]
	internal sealed class EntityPropertyMappingInfo
	{
		// Token: 0x0600145A RID: 5210 RVA: 0x0004C0DF File Offset: 0x0004A2DF
		internal EntityPropertyMappingInfo(EntityPropertyMappingAttribute attribute, IEdmEntityType definingType, IEdmEntityType actualTypeDeclaringProperty)
		{
			this.attribute = attribute;
			this.definingType = definingType;
			this.actualPropertyType = actualTypeDeclaringProperty;
			this.isSyndicationMapping = this.attribute.TargetSyndicationItem != SyndicationItemProperty.CustomProperty;
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0004C113 File Offset: 0x0004A313
		internal EntityPropertyMappingAttribute Attribute
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0004C11B File Offset: 0x0004A31B
		internal IEdmEntityType DefiningType
		{
			get
			{
				return this.definingType;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0004C123 File Offset: 0x0004A323
		internal IEdmEntityType ActualPropertyType
		{
			get
			{
				return this.actualPropertyType;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0004C12B File Offset: 0x0004A32B
		internal EpmSourcePathSegment[] PropertyValuePath
		{
			get
			{
				return this.propertyValuePath;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0004C133 File Offset: 0x0004A333
		internal bool IsSyndicationMapping
		{
			get
			{
				return this.isSyndicationMapping;
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0004C13B File Offset: 0x0004A33B
		internal void SetPropertyValuePath(EpmSourcePathSegment[] path)
		{
			this.propertyValuePath = path;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0004C144 File Offset: 0x0004A344
		internal bool DefiningTypesAreEqual(EntityPropertyMappingInfo other)
		{
			return this.DefiningType.IsEquivalentTo(other.DefiningType);
		}

		// Token: 0x04000731 RID: 1841
		private readonly EntityPropertyMappingAttribute attribute;

		// Token: 0x04000732 RID: 1842
		private readonly IEdmEntityType definingType;

		// Token: 0x04000733 RID: 1843
		private readonly IEdmEntityType actualPropertyType;

		// Token: 0x04000734 RID: 1844
		private EpmSourcePathSegment[] propertyValuePath;

		// Token: 0x04000735 RID: 1845
		private bool isSyndicationMapping;
	}
}
