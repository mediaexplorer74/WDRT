using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200011D RID: 285
	public sealed class ODataEntityReferenceLinkSerializationInfo
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00019C7F File Offset: 0x00017E7F
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x00019C87 File Offset: 0x00017E87
		public string SourceEntitySetName
		{
			get
			{
				return this.sourceEntitySetName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "SourceEntitySetName");
				this.sourceEntitySetName = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00019C9B File Offset: 0x00017E9B
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x00019CA3 File Offset: 0x00017EA3
		public string Typecast
		{
			get
			{
				return this.typecast;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotEmpty(value, "Typecast");
				this.typecast = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00019CB7 File Offset: 0x00017EB7
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x00019CBF File Offset: 0x00017EBF
		public string NavigationPropertyName
		{
			get
			{
				return this.navigationPropertyName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "NavigationPropertyName");
				this.navigationPropertyName = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00019CD3 File Offset: 0x00017ED3
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x00019CDB File Offset: 0x00017EDB
		public bool IsCollectionNavigationProperty { get; set; }

		// Token: 0x060007AF RID: 1967 RVA: 0x00019CE4 File Offset: 0x00017EE4
		internal static ODataEntityReferenceLinkSerializationInfo Validate(ODataEntityReferenceLinkSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.SourceEntitySetName, "serializationInfo.SourceEntitySetName");
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.NavigationPropertyName, "serializationInfo.NavigationPropertyName");
			}
			return serializationInfo;
		}

		// Token: 0x040002DD RID: 733
		private string sourceEntitySetName;

		// Token: 0x040002DE RID: 734
		private string typecast;

		// Token: 0x040002DF RID: 735
		private string navigationPropertyName;
	}
}
