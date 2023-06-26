using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000104 RID: 260
	internal sealed class NoOpEntityMetadataBuilder : ODataEntityMetadataBuilder
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x000183BF File Offset: 0x000165BF
		internal NoOpEntityMetadataBuilder(ODataEntry entry)
		{
			this.entry = entry;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000183CE File Offset: 0x000165CE
		internal override Uri GetEditLink()
		{
			return this.entry.NonComputedEditLink;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000183DB File Offset: 0x000165DB
		internal override Uri GetReadLink()
		{
			return this.entry.NonComputedReadLink;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000183E8 File Offset: 0x000165E8
		internal override string GetId()
		{
			return this.entry.NonComputedId;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000183F5 File Offset: 0x000165F5
		internal override string GetETag()
		{
			return this.entry.NonComputedETag;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00018402 File Offset: 0x00016602
		internal override ODataStreamReferenceValue GetMediaResource()
		{
			return this.entry.NonComputedMediaResource;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001840F File Offset: 0x0001660F
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			return nonComputedProperties;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00018412 File Offset: 0x00016612
		internal override IEnumerable<ODataAction> GetActions()
		{
			return this.entry.NonComputedActions;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001841F File Offset: 0x0001661F
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return this.entry.NonComputedFunctions;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001842C File Offset: 0x0001662C
		internal override Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNavigationLinkUrl)
		{
			return navigationLinkUrl;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001842F File Offset: 0x0001662F
		internal override Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			return associationLinkUrl;
		}

		// Token: 0x040002A7 RID: 679
		private readonly ODataEntry entry;
	}
}
