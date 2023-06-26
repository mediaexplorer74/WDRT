using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000150 RID: 336
	internal sealed class ODataMetadataDocumentUri
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x0001CD26 File Offset: 0x0001AF26
		internal ODataMetadataDocumentUri(Uri baseUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(baseUri, "baseUri");
			if (!baseUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.WriterValidationUtils_MessageWriterSettingsMetadataDocumentUriMustBeNullOrAbsolute(UriUtilsCommon.UriToString(baseUri)));
			}
			this.baseUri = baseUri;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001CD59 File Offset: 0x0001AF59
		internal Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0001CD61 File Offset: 0x0001AF61
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0001CD69 File Offset: 0x0001AF69
		internal string SelectClause
		{
			get
			{
				return this.selectClause;
			}
			set
			{
				this.selectClause = value;
			}
		}

		// Token: 0x04000365 RID: 869
		private readonly Uri baseUri;

		// Token: 0x04000366 RID: 870
		private string selectClause;
	}
}
