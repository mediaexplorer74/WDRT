using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000120 RID: 288
	public sealed class ODataFeedAndEntrySerializationInfo
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00019D2B File Offset: 0x00017F2B
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00019D33 File Offset: 0x00017F33
		public string EntitySetName
		{
			get
			{
				return this.entitySetName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "EntitySetName");
				this.entitySetName = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00019D47 File Offset: 0x00017F47
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00019D4F File Offset: 0x00017F4F
		public string EntitySetElementTypeName
		{
			get
			{
				return this.entitySetElementTypeName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "EntitySetElementTypeName");
				this.entitySetElementTypeName = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00019D63 File Offset: 0x00017F63
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00019D75 File Offset: 0x00017F75
		public string ExpectedTypeName
		{
			get
			{
				return this.expectedTypeName ?? this.EntitySetElementTypeName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotEmpty(value, "ExpectedTypeName");
				this.expectedTypeName = value;
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00019D89 File Offset: 0x00017F89
		internal static ODataFeedAndEntrySerializationInfo Validate(ODataFeedAndEntrySerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.EntitySetName, "serializationInfo.EntitySetName");
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.EntitySetElementTypeName, "serializationInfo.EntitySetElementTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x040002E7 RID: 743
		private string entitySetName;

		// Token: 0x040002E8 RID: 744
		private string entitySetElementTypeName;

		// Token: 0x040002E9 RID: 745
		private string expectedTypeName;
	}
}
