using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000102 RID: 258
	internal abstract class ODataEntityMetadataBuilder
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x000182F1 File Offset: 0x000164F1
		internal static ODataEntityMetadataBuilder Null
		{
			get
			{
				return ODataEntityMetadataBuilder.NullEntityMetadataBuilder.Instance;
			}
		}

		// Token: 0x060006E5 RID: 1765
		internal abstract Uri GetEditLink();

		// Token: 0x060006E6 RID: 1766
		internal abstract Uri GetReadLink();

		// Token: 0x060006E7 RID: 1767
		internal abstract string GetId();

		// Token: 0x060006E8 RID: 1768
		internal abstract string GetETag();

		// Token: 0x060006E9 RID: 1769 RVA: 0x000182F8 File Offset: 0x000164F8
		internal virtual ODataStreamReferenceValue GetMediaResource()
		{
			return null;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001830E File Offset: 0x0001650E
		internal virtual IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			if (nonComputedProperties != null)
			{
				return nonComputedProperties.Where((ODataProperty p) => !(p.ODataValue is ODataStreamReferenceValue));
			}
			return null;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00018338 File Offset: 0x00016538
		internal virtual IEnumerable<ODataAction> GetActions()
		{
			return null;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001833B File Offset: 0x0001653B
		internal virtual IEnumerable<ODataFunction> GetFunctions()
		{
			return null;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001833E File Offset: 0x0001653E
		internal virtual void MarkNavigationLinkProcessed(string navigationPropertyName)
		{
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00018340 File Offset: 0x00016540
		internal virtual ODataJsonLightReaderNavigationLinkInfo GetNextUnprocessedNavigationLink()
		{
			return null;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00018343 File Offset: 0x00016543
		internal virtual Uri GetStreamEditLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00018351 File Offset: 0x00016551
		internal virtual Uri GetStreamReadLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001835F File Offset: 0x0001655F
		internal virtual Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNavigationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001836D File Offset: 0x0001656D
		internal virtual Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001837B File Offset: 0x0001657B
		internal virtual Uri GetOperationTargetUri(string operationName, string bindingParameterTypeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00018389 File Offset: 0x00016589
		internal virtual string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x02000103 RID: 259
		private sealed class NullEntityMetadataBuilder : ODataEntityMetadataBuilder
		{
			// Token: 0x060006F7 RID: 1783 RVA: 0x0001839F File Offset: 0x0001659F
			private NullEntityMetadataBuilder()
			{
			}

			// Token: 0x060006F8 RID: 1784 RVA: 0x000183A7 File Offset: 0x000165A7
			internal override Uri GetEditLink()
			{
				return null;
			}

			// Token: 0x060006F9 RID: 1785 RVA: 0x000183AA File Offset: 0x000165AA
			internal override Uri GetReadLink()
			{
				return null;
			}

			// Token: 0x060006FA RID: 1786 RVA: 0x000183AD File Offset: 0x000165AD
			internal override string GetId()
			{
				return null;
			}

			// Token: 0x060006FB RID: 1787 RVA: 0x000183B0 File Offset: 0x000165B0
			internal override string GetETag()
			{
				return null;
			}

			// Token: 0x040002A6 RID: 678
			internal static readonly ODataEntityMetadataBuilder.NullEntityMetadataBuilder Instance = new ODataEntityMetadataBuilder.NullEntityMetadataBuilder();
		}
	}
}
