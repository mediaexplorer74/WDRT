using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200011B RID: 283
	public sealed class ODataCollectionStartSerializationInfo
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00019BBC File Offset: 0x00017DBC
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x00019BC4 File Offset: 0x00017DC4
		public string CollectionTypeName
		{
			get
			{
				return this.collectionTypeName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "CollectionTypeName");
				ValidationUtils.ValidateCollectionTypeName(value);
				this.collectionTypeName = value;
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00019BDF File Offset: 0x00017DDF
		internal static ODataCollectionStartSerializationInfo Validate(ODataCollectionStartSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.CollectionTypeName, "serializationInfo.CollectionTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x040002D9 RID: 729
		private string collectionTypeName;
	}
}
