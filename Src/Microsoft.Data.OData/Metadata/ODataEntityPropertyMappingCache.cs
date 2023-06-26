using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000205 RID: 517
	internal sealed class ODataEntityPropertyMappingCache
	{
		// Token: 0x06000FD8 RID: 4056 RVA: 0x00039E7C File Offset: 0x0003807C
		internal ODataEntityPropertyMappingCache(ODataEntityPropertyMappingCollection mappings, IEdmModel model, int totalMappingCount)
		{
			this.mappings = mappings;
			this.model = model;
			this.totalMappingCount = totalMappingCount;
			this.mappingsForInheritedProperties = new List<EntityPropertyMappingAttribute>();
			this.mappingsForDeclaredProperties = ((mappings == null) ? new List<EntityPropertyMappingAttribute>() : new List<EntityPropertyMappingAttribute>(mappings));
			this.epmTargetTree = new EpmTargetTree();
			this.epmSourceTree = new EpmSourceTree(this.epmTargetTree);
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00039EE1 File Offset: 0x000380E1
		internal List<EntityPropertyMappingAttribute> MappingsForInheritedProperties
		{
			get
			{
				return this.mappingsForInheritedProperties;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00039EE9 File Offset: 0x000380E9
		internal List<EntityPropertyMappingAttribute> MappingsForDeclaredProperties
		{
			get
			{
				return this.mappingsForDeclaredProperties;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00039EF1 File Offset: 0x000380F1
		internal EpmSourceTree EpmSourceTree
		{
			get
			{
				return this.epmSourceTree;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00039EF9 File Offset: 0x000380F9
		internal EpmTargetTree EpmTargetTree
		{
			get
			{
				return this.epmTargetTree;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00039F01 File Offset: 0x00038101
		internal IEnumerable<EntityPropertyMappingAttribute> AllMappings
		{
			get
			{
				return this.MappingsForDeclaredProperties.Concat(this.MappingsForInheritedProperties);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00039F14 File Offset: 0x00038114
		internal int TotalMappingCount
		{
			get
			{
				return this.totalMappingCount;
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00039F1C File Offset: 0x0003811C
		internal void BuildEpmForType(IEdmEntityType definingEntityType, IEdmEntityType affectedEntityType)
		{
			if (definingEntityType.BaseType != null)
			{
				this.BuildEpmForType(definingEntityType.BaseEntityType(), affectedEntityType);
			}
			ODataEntityPropertyMappingCollection entityPropertyMappings = this.model.GetEntityPropertyMappings(definingEntityType);
			if (entityPropertyMappings == null)
			{
				return;
			}
			foreach (EntityPropertyMappingAttribute entityPropertyMappingAttribute in entityPropertyMappings)
			{
				this.epmSourceTree.Add(new EntityPropertyMappingInfo(entityPropertyMappingAttribute, definingEntityType, affectedEntityType));
				if (definingEntityType == affectedEntityType && !ODataEntityPropertyMappingCache.PropertyExistsOnType(affectedEntityType, entityPropertyMappingAttribute))
				{
					this.MappingsForInheritedProperties.Add(entityPropertyMappingAttribute);
					this.MappingsForDeclaredProperties.Remove(entityPropertyMappingAttribute);
				}
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00039FBC File Offset: 0x000381BC
		internal bool IsDirty(ODataEntityPropertyMappingCollection propertyMappings)
		{
			return (this.mappings != null || propertyMappings != null) && (!object.ReferenceEquals(this.mappings, propertyMappings) || this.mappings.Count != propertyMappings.Count);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0003A00C File Offset: 0x0003820C
		private static bool PropertyExistsOnType(IEdmStructuredType structuredType, EntityPropertyMappingAttribute epmAttribute)
		{
			int num = epmAttribute.SourcePath.IndexOf('/');
			string propertyToLookFor = ((num == -1) ? epmAttribute.SourcePath : epmAttribute.SourcePath.Substring(0, num));
			return structuredType.DeclaredProperties.Any((IEdmProperty p) => p.Name == propertyToLookFor);
		}

		// Token: 0x040005BD RID: 1469
		private readonly ODataEntityPropertyMappingCollection mappings;

		// Token: 0x040005BE RID: 1470
		private readonly List<EntityPropertyMappingAttribute> mappingsForInheritedProperties;

		// Token: 0x040005BF RID: 1471
		private readonly List<EntityPropertyMappingAttribute> mappingsForDeclaredProperties;

		// Token: 0x040005C0 RID: 1472
		private readonly EpmSourceTree epmSourceTree;

		// Token: 0x040005C1 RID: 1473
		private readonly EpmTargetTree epmTargetTree;

		// Token: 0x040005C2 RID: 1474
		private readonly IEdmModel model;

		// Token: 0x040005C3 RID: 1475
		private readonly int totalMappingCount;
	}
}
