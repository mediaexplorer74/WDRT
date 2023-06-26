using System;

namespace System.Data.Services.Client
{
	// Token: 0x020000F2 RID: 242
	public sealed class EntityChangedParams
	{
		// Token: 0x0600080C RID: 2060 RVA: 0x00022684 File Offset: 0x00020884
		internal EntityChangedParams(DataServiceContext context, object entity, string propertyName, object propertyValue, string sourceEntitySet, string targetEntitySet)
		{
			this.context = context;
			this.entity = entity;
			this.propertyName = propertyName;
			this.propertyValue = propertyValue;
			this.sourceEntitySet = sourceEntitySet;
			this.targetEntitySet = targetEntitySet;
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x000226B9 File Offset: 0x000208B9
		public DataServiceContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x000226C1 File Offset: 0x000208C1
		public object Entity
		{
			get
			{
				return this.entity;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x000226C9 File Offset: 0x000208C9
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x000226D1 File Offset: 0x000208D1
		public object PropertyValue
		{
			get
			{
				return this.propertyValue;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x000226D9 File Offset: 0x000208D9
		public string SourceEntitySet
		{
			get
			{
				return this.sourceEntitySet;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x000226E1 File Offset: 0x000208E1
		public string TargetEntitySet
		{
			get
			{
				return this.targetEntitySet;
			}
		}

		// Token: 0x040004C8 RID: 1224
		private readonly DataServiceContext context;

		// Token: 0x040004C9 RID: 1225
		private readonly object entity;

		// Token: 0x040004CA RID: 1226
		private readonly string propertyName;

		// Token: 0x040004CB RID: 1227
		private readonly object propertyValue;

		// Token: 0x040004CC RID: 1228
		private readonly string sourceEntitySet;

		// Token: 0x040004CD RID: 1229
		private readonly string targetEntitySet;
	}
}
