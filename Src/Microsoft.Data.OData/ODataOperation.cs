using System;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData
{
	// Token: 0x020001FE RID: 510
	public abstract class ODataOperation : ODataAnnotatable
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00037EBC File Offset: 0x000360BC
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x00037EC4 File Offset: 0x000360C4
		public Uri Metadata { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00037ED0 File Offset: 0x000360D0
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x00037F1A File Offset: 0x0003611A
		public string Title
		{
			get
			{
				string text;
				if (!this.hasNonComputedTitle)
				{
					if ((text = this.computedTitle) == null)
					{
						if (this.metadataBuilder != null)
						{
							return this.computedTitle = this.metadataBuilder.GetOperationTitle(this.operationFullName);
						}
						return null;
					}
				}
				else
				{
					text = this.title;
				}
				return text;
			}
			set
			{
				this.title = value;
				this.hasNonComputedTitle = true;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00037F2C File Offset: 0x0003612C
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x00037F7C File Offset: 0x0003617C
		public Uri Target
		{
			get
			{
				Uri uri;
				if (!this.hasNonComputedTarget)
				{
					if ((uri = this.computedTarget) == null)
					{
						if (this.metadataBuilder != null)
						{
							return this.computedTarget = this.metadataBuilder.GetOperationTargetUri(this.operationFullName, this.bindingParameterTypeName);
						}
						return null;
					}
				}
				else
				{
					uri = this.target;
				}
				return uri;
			}
			set
			{
				this.target = value;
				this.hasNonComputedTarget = true;
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00037F8C File Offset: 0x0003618C
		internal void SetMetadataBuilder(ODataEntityMetadataBuilder builder, Uri metadataDocumentUri)
		{
			ODataJsonLightValidationUtils.ValidateOperation(metadataDocumentUri, this);
			this.metadataBuilder = builder;
			this.operationFullName = ODataJsonLightUtils.GetFullyQualifiedFunctionImportName(metadataDocumentUri, UriUtilsCommon.UriToString(this.Metadata), out this.bindingParameterTypeName);
			this.computedTitle = null;
			this.computedTarget = null;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00037FC7 File Offset: 0x000361C7
		internal ODataEntityMetadataBuilder GetMetadataBuilder()
		{
			return this.metadataBuilder;
		}

		// Token: 0x0400057E RID: 1406
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x0400057F RID: 1407
		private string title;

		// Token: 0x04000580 RID: 1408
		private bool hasNonComputedTitle;

		// Token: 0x04000581 RID: 1409
		private string computedTitle;

		// Token: 0x04000582 RID: 1410
		private Uri target;

		// Token: 0x04000583 RID: 1411
		private bool hasNonComputedTarget;

		// Token: 0x04000584 RID: 1412
		private Uri computedTarget;

		// Token: 0x04000585 RID: 1413
		private string operationFullName;

		// Token: 0x04000586 RID: 1414
		private string bindingParameterTypeName;
	}
}
