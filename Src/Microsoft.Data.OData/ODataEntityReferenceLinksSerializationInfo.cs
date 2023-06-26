using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200011C RID: 284
	public sealed class ODataEntityReferenceLinksSerializationInfo
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00019BFD File Offset: 0x00017DFD
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x00019C05 File Offset: 0x00017E05
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

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00019C19 File Offset: 0x00017E19
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00019C21 File Offset: 0x00017E21
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

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00019C35 File Offset: 0x00017E35
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x00019C3D File Offset: 0x00017E3D
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

		// Token: 0x060007A5 RID: 1957 RVA: 0x00019C51 File Offset: 0x00017E51
		internal static ODataEntityReferenceLinksSerializationInfo Validate(ODataEntityReferenceLinksSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.SourceEntitySetName, "serializationInfo.SourceEntitySetName");
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.NavigationPropertyName, "serializationInfo.NavigationPropertyName");
			}
			return serializationInfo;
		}

		// Token: 0x040002DA RID: 730
		private string sourceEntitySetName;

		// Token: 0x040002DB RID: 731
		private string typecast;

		// Token: 0x040002DC RID: 732
		private string navigationPropertyName;
	}
}
