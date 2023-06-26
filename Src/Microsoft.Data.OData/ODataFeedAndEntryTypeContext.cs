using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000117 RID: 279
	internal class ODataFeedAndEntryTypeContext : IODataFeedAndEntryTypeContext
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x00019930 File Offset: 0x00017B30
		private ODataFeedAndEntryTypeContext(bool throwIfMissingTypeInfo)
		{
			this.throwIfMissingTypeInfo = throwIfMissingTypeInfo;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0001993F File Offset: 0x00017B3F
		public virtual string EntitySetName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00019948 File Offset: 0x00017B48
		public virtual string EntitySetElementTypeName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00019951 File Offset: 0x00017B51
		public virtual string ExpectedEntityTypeName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00019954 File Offset: 0x00017B54
		public virtual bool IsMediaLinkEntry
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00019957 File Offset: 0x00017B57
		public virtual UrlConvention UrlConvention
		{
			get
			{
				return ODataFeedAndEntryTypeContext.DefaultUrlConvention;
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001995E File Offset: 0x00017B5E
		internal static ODataFeedAndEntryTypeContext Create(ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmEntityType entitySetElementType, IEdmEntityType expectedEntityType, IEdmModel model, bool throwIfMissingTypeInfo)
		{
			if (serializationInfo != null)
			{
				return new ODataFeedAndEntryTypeContext.ODataFeedAndEntryTypeContextWithoutModel(serializationInfo);
			}
			if (entitySet != null && model.IsUserModel())
			{
				return new ODataFeedAndEntryTypeContext.ODataFeedAndEntryTypeContextWithModel(entitySet, entitySetElementType, expectedEntityType, model);
			}
			return new ODataFeedAndEntryTypeContext(throwIfMissingTypeInfo);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00019988 File Offset: 0x00017B88
		private T ValidateAndReturn<T>(T value) where T : class
		{
			if (this.throwIfMissingTypeInfo && value == null)
			{
				throw new ODataException(Strings.ODataFeedAndEntryTypeContext_MetadataOrSerializationInfoMissing);
			}
			return value;
		}

		// Token: 0x040002CF RID: 719
		private static readonly UrlConvention DefaultUrlConvention = UrlConvention.CreateWithExplicitValue(false);

		// Token: 0x040002D0 RID: 720
		private readonly bool throwIfMissingTypeInfo;

		// Token: 0x02000118 RID: 280
		private sealed class ODataFeedAndEntryTypeContextWithoutModel : ODataFeedAndEntryTypeContext
		{
			// Token: 0x06000786 RID: 1926 RVA: 0x000199B3 File Offset: 0x00017BB3
			internal ODataFeedAndEntryTypeContextWithoutModel(ODataFeedAndEntrySerializationInfo serializationInfo)
				: base(false)
			{
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000787 RID: 1927 RVA: 0x000199C3 File Offset: 0x00017BC3
			public override string EntitySetName
			{
				get
				{
					return this.serializationInfo.EntitySetName;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000788 RID: 1928 RVA: 0x000199D0 File Offset: 0x00017BD0
			public override string EntitySetElementTypeName
			{
				get
				{
					return this.serializationInfo.EntitySetElementTypeName;
				}
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06000789 RID: 1929 RVA: 0x000199DD File Offset: 0x00017BDD
			public override string ExpectedEntityTypeName
			{
				get
				{
					return this.serializationInfo.ExpectedTypeName;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x0600078A RID: 1930 RVA: 0x000199EA File Offset: 0x00017BEA
			public override bool IsMediaLinkEntry
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x0600078B RID: 1931 RVA: 0x000199ED File Offset: 0x00017BED
			public override UrlConvention UrlConvention
			{
				get
				{
					return ODataFeedAndEntryTypeContext.DefaultUrlConvention;
				}
			}

			// Token: 0x040002D1 RID: 721
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;
		}

		// Token: 0x02000119 RID: 281
		private sealed class ODataFeedAndEntryTypeContextWithModel : ODataFeedAndEntryTypeContext
		{
			// Token: 0x0600078C RID: 1932 RVA: 0x00019A78 File Offset: 0x00017C78
			internal ODataFeedAndEntryTypeContextWithModel(IEdmEntitySet entitySet, IEdmEntityType entitySetElementType, IEdmEntityType expectedEntityType, IEdmModel model)
				: base(false)
			{
				this.entitySet = entitySet;
				this.entitySetElementType = entitySetElementType;
				this.expectedEntityType = expectedEntityType;
				this.model = model;
				this.lazyEntitySetName = new SimpleLazy<string>(delegate
				{
					if (!this.model.IsDefaultEntityContainer(this.entitySet.Container))
					{
						return this.entitySet.Container.FullName() + "." + this.entitySet.Name;
					}
					return this.entitySet.Name;
				});
				this.lazyIsMediaLinkEntry = new SimpleLazy<bool>(() => this.model.HasDefaultStream(this.expectedEntityType));
				this.lazyUrlConvention = new SimpleLazy<UrlConvention>(() => UrlConvention.ForEntityContainer(this.model, this.entitySet.Container));
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x0600078D RID: 1933 RVA: 0x00019B03 File Offset: 0x00017D03
			public override string EntitySetName
			{
				get
				{
					return this.lazyEntitySetName.Value;
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x0600078E RID: 1934 RVA: 0x00019B10 File Offset: 0x00017D10
			public override string EntitySetElementTypeName
			{
				get
				{
					return this.entitySetElementType.FullName();
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x0600078F RID: 1935 RVA: 0x00019B1D File Offset: 0x00017D1D
			public override string ExpectedEntityTypeName
			{
				get
				{
					return this.expectedEntityType.FullName();
				}
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06000790 RID: 1936 RVA: 0x00019B2A File Offset: 0x00017D2A
			public override bool IsMediaLinkEntry
			{
				get
				{
					return this.lazyIsMediaLinkEntry.Value;
				}
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x06000791 RID: 1937 RVA: 0x00019B37 File Offset: 0x00017D37
			public override UrlConvention UrlConvention
			{
				get
				{
					return this.lazyUrlConvention.Value;
				}
			}

			// Token: 0x040002D2 RID: 722
			private readonly IEdmModel model;

			// Token: 0x040002D3 RID: 723
			private readonly IEdmEntitySet entitySet;

			// Token: 0x040002D4 RID: 724
			private readonly IEdmEntityType entitySetElementType;

			// Token: 0x040002D5 RID: 725
			private readonly IEdmEntityType expectedEntityType;

			// Token: 0x040002D6 RID: 726
			private readonly SimpleLazy<string> lazyEntitySetName;

			// Token: 0x040002D7 RID: 727
			private readonly SimpleLazy<bool> lazyIsMediaLinkEntry;

			// Token: 0x040002D8 RID: 728
			private readonly SimpleLazy<UrlConvention> lazyUrlConvention;
		}
	}
}
