using System;
using System.Data.Services.Client.Metadata;
using System.Data.Services.Common;
using System.Diagnostics;

namespace System.Data.Services.Client.Serializers
{
	// Token: 0x0200001B RID: 27
	[DebuggerDisplay("EntityPropertyMappingInfo {DefiningType}")]
	internal sealed class EntityPropertyMappingInfo
	{
		// Token: 0x06000098 RID: 152 RVA: 0x000041E4 File Offset: 0x000023E4
		public EntityPropertyMappingInfo(EntityPropertyMappingAttribute attribute, Type definingType, ClientTypeAnnotation actualPropertyType)
		{
			this.attribute = attribute;
			this.definingType = definingType;
			this.actualPropertyType = actualPropertyType;
			this.propertyValuePath = attribute.SourcePath.Split(new char[] { '/' });
			this.isSyndicationMapping = this.attribute.TargetSyndicationItem != SyndicationItemProperty.CustomProperty;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004241 File Offset: 0x00002441
		public EntityPropertyMappingAttribute Attribute
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004249 File Offset: 0x00002449
		public Type DefiningType
		{
			get
			{
				return this.definingType;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004251 File Offset: 0x00002451
		public ClientTypeAnnotation ActualPropertyType
		{
			get
			{
				return this.actualPropertyType;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004259 File Offset: 0x00002459
		public string[] PropertyValuePath
		{
			get
			{
				return this.propertyValuePath;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004261 File Offset: 0x00002461
		public bool IsSyndicationMapping
		{
			get
			{
				return this.isSyndicationMapping;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004269 File Offset: 0x00002469
		internal bool DefiningTypesAreEqual(EntityPropertyMappingInfo other)
		{
			return this.DefiningType == other.DefiningType;
		}

		// Token: 0x04000177 RID: 375
		private readonly EntityPropertyMappingAttribute attribute;

		// Token: 0x04000178 RID: 376
		private readonly Type definingType;

		// Token: 0x04000179 RID: 377
		private readonly ClientTypeAnnotation actualPropertyType;

		// Token: 0x0400017A RID: 378
		private string[] propertyValuePath;

		// Token: 0x0400017B RID: 379
		private bool isSyndicationMapping;
	}
}
