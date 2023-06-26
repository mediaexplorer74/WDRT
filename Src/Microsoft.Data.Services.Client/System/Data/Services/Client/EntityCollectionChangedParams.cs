using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Data.Services.Client
{
	// Token: 0x020000F3 RID: 243
	public sealed class EntityCollectionChangedParams
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x000226EC File Offset: 0x000208EC
		internal EntityCollectionChangedParams(DataServiceContext context, object sourceEntity, string propertyName, string sourceEntitySet, ICollection collection, object targetEntity, string targetEntitySet, NotifyCollectionChangedAction action)
		{
			this.context = context;
			this.sourceEntity = sourceEntity;
			this.propertyName = propertyName;
			this.sourceEntitySet = sourceEntitySet;
			this.collection = collection;
			this.targetEntity = targetEntity;
			this.targetEntitySet = targetEntitySet;
			this.action = action;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0002273C File Offset: 0x0002093C
		public DataServiceContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00022744 File Offset: 0x00020944
		public object SourceEntity
		{
			get
			{
				return this.sourceEntity;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0002274C File Offset: 0x0002094C
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00022754 File Offset: 0x00020954
		public string SourceEntitySet
		{
			get
			{
				return this.sourceEntitySet;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0002275C File Offset: 0x0002095C
		public object TargetEntity
		{
			get
			{
				return this.targetEntity;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00022764 File Offset: 0x00020964
		public string TargetEntitySet
		{
			get
			{
				return this.targetEntitySet;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0002276C File Offset: 0x0002096C
		public ICollection Collection
		{
			get
			{
				return this.collection;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00022774 File Offset: 0x00020974
		public NotifyCollectionChangedAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x040004CE RID: 1230
		private readonly DataServiceContext context;

		// Token: 0x040004CF RID: 1231
		private readonly object sourceEntity;

		// Token: 0x040004D0 RID: 1232
		private readonly string propertyName;

		// Token: 0x040004D1 RID: 1233
		private readonly string sourceEntitySet;

		// Token: 0x040004D2 RID: 1234
		private readonly ICollection collection;

		// Token: 0x040004D3 RID: 1235
		private readonly object targetEntity;

		// Token: 0x040004D4 RID: 1236
		private readonly string targetEntitySet;

		// Token: 0x040004D5 RID: 1237
		private readonly NotifyCollectionChangedAction action;
	}
}
